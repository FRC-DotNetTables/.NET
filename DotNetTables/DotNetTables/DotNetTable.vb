Imports edu.wpi.first.wpilibj
Imports edu.wpi.first.wpilibj.tables
Imports edu.wpi.first.wpilibj.networktables2.type
Imports java.lang
Imports System.Collections.Concurrent
Imports System.Timers

Public Class DotNetTable
    Implements ITableListener

    Public Const STALE_FACTOR As Double = 2.1
    Public Const UPDATE_INTERVAL As String = "_UPDATE_INTERVAL"
    Public Const KEY_COLUMN As String = "Key"
    Public Const VALUE_COLUMN As String = "Value"
    Private _name As String
    Private _updateInterval As Integer
    Private _writable As Boolean
    Private timer As Timers.Timer
    Public _data As DataTable
    Private changeCallback As DotNetTableEvents
    Private staleCallback As DotNetTableEvents
    Private _lastUpdate As Long

    Protected Friend Sub New(name As String, writable As Boolean)
        Me._lastUpdate = 0
        Me._name = name
        Me._writable = writable
        Me._updateInterval = -1
        Me.changeCallback = Nothing
        Me.staleCallback = Nothing

        ' DataTable with two columns KEY_COLUMN and VALUE_COLUMN
        Me._data = New DataTable
        With Me._data
            Dim column As DataColumn

            column = New DataColumn
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = KEY_COLUMN
            .Columns.Add(column)

            column = New DataColumn
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = VALUE_COLUMN
            .Columns.Add(column)
        End With

        ' Set KEY_COLUMN as the primary key
        Dim col(0) As DataColumn
        col(0) = _data.Columns(KEY_COLUMN)
        _data.PrimaryKey = col

        Me.timer = New Timers.Timer
        AddHandler timer.Elapsed, AddressOf TimerElaspsed
    End Sub

    Public ReadOnly Property name As String
        Get
            name = _name
        End Get
    End Property

    Public Function isStale() As Boolean
        'Tables with no update interval are never stale
        If _updateInterval <= 0 Then
            Return False
        End If

        'Tables are stale when we miss STALE_FACTOR update intervals
        Dim age As Double = (DateTime.Now - New DateTime(1970, 1, 1)).TotalMilliseconds - _lastUpdate
        If age > (_updateInterval * STALE_FACTOR) Then
            Return True
        End If

        'Otherwise we're fresh
        Return False

    End Function


    Public ReadOnly Property lastUpdate As Double
        Get
            lastUpdate = _lastUpdate
        End Get
    End Property


    Public ReadOnly Property iswritable As Boolean
        Get
            iswritable = _writable
        End Get
    End Property


    Private Sub throwIfNotWritable()
        If _writable = False Then
            Throw New IllegalStateException("Table is read-only: " & _name)
        End If
    End Sub

    Private Sub TimerElaspsed()
        If Me.iswritable Then
            Me.send()
        Else
            If Me.staleCallback IsNot Nothing Then
                Me.staleCallback.stale(Me)
            End If
        End If
    End Sub

    Private Sub resetTimer()
        If timer IsNot Nothing Then
            timer.Stop()
        End If

        If _updateInterval >= 0 Then
            timer.Interval = (_updateInterval)
            timer.Start()
        End If
    End Sub

    Public ReadOnly Property getInterval As Integer
        Get
            getInterval = _updateInterval
        End Get
    End Property

    Public Sub setInterval(update As Integer)
        throwIfNotWritable()
        If update <= 0 Then
            update = -1
        End If
        _updateInterval = update
        resetTimer()
    End Sub

    Public Sub onChange(callback As DotNetTableEvents)
        changeCallback = callback
    End Sub


    Public Sub onStale(callback As DotNetTableEvents)
        If _writable Then
            Throw New IllegalStateException("Table is local: " & _name)
        End If
        staleCallback = callback
    End Sub

    Public Sub clear()
        _data.Clear()
    End Sub

    Public ReadOnly Property Keys As ICollection(Of String)
        Get
            Dim col As ICollection(Of String) = New List(Of String)
            Dim row As DataRow
            For Each row In _data.Rows
                col.Add(row(KEY_COLUMN))
            Next
            Keys = col
        End Get
    End Property

    Public ReadOnly Property exists(key As String) As Boolean
        Get
            exists = _data.Rows.Contains(key)
        End Get
    End Property

    Public Sub setValue(key As String, value As String)
        throwIfNotWritable()

        ' Find or add
        Dim row As DataRow
        row = _data.Rows.Find(key)
        If (row Is Nothing) Then
            row = _data.NewRow()
            row(KEY_COLUMN) = key
            _data.Rows.Add(row)
        End If

        ' Update
        row(VALUE_COLUMN) = value

        ' Bump the update timestamp
        _lastUpdate = (DateTime.Now - New DateTime(1970, 1, 1)).TotalMilliseconds
    End Sub

    Public Sub setValue(key As String, value As Double)
        setValue(key, value.ToString)
    End Sub

    Public Sub setValue(key As String, value As Integer)
        setValue(key, value.ToString)
    End Sub

    Public Sub remove(key As String)
        throwIfNotWritable()
        Dim row As DataRow = _data.Rows.Find(key)
        If (row IsNot Nothing) Then
            row.Delete()
        End If
    End Sub

    Public Function getValue(key As String) As String
        getValue = Nothing
        Dim row As DataRow = _data.Rows.Find(key)
        If (row IsNot Nothing) Then
            getValue = row(VALUE_COLUMN)
        End If
    End Function

    Public Function getDouble(key As String) As Double
        Double.TryParse(getValue(key), getDouble)
    End Function

    Public Function getInt(key As String) As Integer
        Integer.TryParse(getValue(key), getInt)
    End Function

    Private Sub recv(value As StringArray)
        'unpack the new data
        _data = SAtoHM(value)
        _lastUpdate = (DateTime.Now - New DateTime(1970, 1, 1)).TotalMilliseconds

        'note the published update interval
        If exists(UPDATE_INTERVAL) Then
            _updateInterval = getInt(UPDATE_INTERVAL)
            _data.Rows.Find(UPDATE_INTERVAL).Delete()
            resetTimer()
        End If

        'dispatch our callback, if any
        If changeCallback IsNot Nothing Then
            changeCallback.changed(Me)
        End If
    End Sub

    Public Sub send()
        throwIfNotWritable()
        setValue(UPDATE_INTERVAL, getInterval())
        DotNetTables.push(_name, HMtoSA(_data))
        resetTimer()

        'Dispatch our callback, if any
        If (changeCallback IsNot Nothing) Then
            changeCallback.changed(Me)
        End If

    End Sub

    Private Function HMtoSA(data As DataTable) As StringArray
        Dim out As New StringArray
        Dim row As DataRow
        For Each row In _data.Rows
            out.add(row(KEY_COLUMN))
        Next

        'Use the output list of keys as the iterator to ensure correct value ordering
        Dim size As Integer = out.size
        For i = 0 To size - 1
            out.add(_data.Rows.Find(out.get(i))(VALUE_COLUMN))
        Next

        Return out
    End Function

    Private Function SAtoHM(data As StringArray) As DataTable
        Dim out As New DataTable
        out = _data.Clone

        If data.size Mod 2 <> 0 Then
            Throw New ArrayIndexOutOfBoundsException("StringArray contains an odd number of elements")
        End If

        Dim setSize As Integer = data.size / 2
        For i = 0 To setSize - 1
            Dim row As DataRow = out.NewRow()
            row(KEY_COLUMN) = data.get(i)
            row(VALUE_COLUMN) = data.get(i + setSize)
            out.Rows.Add(row)
        Next

        Return out
    End Function

    Public Sub valueChanged(itable As ITable, key As String, val As Object, isNew As Boolean) Implements ITableListener.valueChanged
        'skip updates for other tables
        If _name <> key Then
            Return
        End If

        'store the new data
        Dim value As New StringArray
        itable.retrieveValue(key, value)
        recv(value)
    End Sub

    Public Event changed(table As DotNetTable)
    Public Event stale(table As DotNetTable)

    Public Interface DotNetTableEvents
        Sub changed(table As DotNetTable)
        Sub stale(table As DotNetTable)
    End Interface

End Class


