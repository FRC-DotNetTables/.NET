﻿Imports edu.wpi.first.wpilibj
Imports edu.wpi.first.wpilibj.tables
Imports edu.wpi.first.wpilibj.networktables2.type
Imports java.lang



Public Class DotNetTable
    Implements ITableListener

    Public Const STALE_FACTOR As Double = 2.5
    Public Const UPDATE_INTERVAL As String = "_UPDATE_INTERVAL"
    Private _name As String
    Private _updateInterval As Integer
    Private _writable As Boolean
    Public _data As Dictionary(Of String, String)
    Private changeCallback As DotNetTableEvents
    Private staleCallback As DotNetTableEvents
    Private _lastUpdate As Long


    Public Sub New(name As String, writable As Boolean)
        Me._lastUpdate = 0
        Me._name = name
        Me._writable = writable
        Me._updateInterval = -1
        Me.changeCallback = Nothing
        Me.staleCallback = Nothing
        _data = New Dictionary(Of String, String)
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
        Dim age As Double = Now.Millisecond - _lastUpdate
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
    End Sub

    Public Sub onChange(callback As DotNetTableEvents)
        changeCallback = callback
    End Sub


    Public Sub onStale(callback As DotNetTableEvents)
        If _writable Then
            Throw New IllegalStateException("Table is local: " & _name)
        End If
        staleCallback = callback
        Throw New UnsupportedOperationException("Not supported yet.")
    End Sub

    Public Sub clear()
        _data.Clear()
    End Sub


    Public ReadOnly Property Keys As Dictionary(Of String, String).KeyCollection
        Get
            Keys = _data.Keys
        End Get
    End Property



    Public ReadOnly Property exists(key As String) As Boolean
        Get
            exists = _data.ContainsKey(key)
        End Get
    End Property


    Public Sub setVal(key As String, value As String)
        throwIfNotWritable()
        If _data.ContainsKey(key) Then
            _data.Item(key) = value
        Else
            _data.Add(key, value)
        End If
        _lastUpdate = Now.Millisecond
    End Sub

    Public Sub setVal(key As String, value As Double)
        setVal(key, value.ToString)
    End Sub

    Public Sub setVal(key As String, value As Integer)
        setVal(key, value.ToString)
    End Sub

    Public Sub remove(key As String)
        throwIfNotWritable()
        _data.Remove(key)
    End Sub

    Public Function getVal(key As String) As String
        getVal = _data.Item(key)
    End Function

    Public Function getDouble(key As String) As Double
        Double.TryParse(_data.Item(key), getDouble)
    End Function

    Public Function getInt(key As String) As Integer
        Integer.TryParse(_data.Item(key), getInt)
    End Function








    Private Sub recv(value As StringArray)
        'unpack the new data
        _data = SAtoHM(value)
        _lastUpdate = Now.Millisecond

        'note the published update interval
        If exists(UPDATE_INTERVAL) Then
            _updateInterval = getInt(UPDATE_INTERVAL)
        End If

        'dispatch our callback, if any
        If changeCallback IsNot Nothing Then
            changeCallback.changed(Me)
        End If
    End Sub


    Public Sub send()
        throwIfNotWritable()
        setVal(UPDATE_INTERVAL, getInterval())
        DotNetTables.push(_name, HMtoSA(_data))


        'Dispatch our callback, if any
        If (changeCallback IsNot Nothing) Then
            changeCallback.changed(Me)
        End If

    End Sub



    Private Function HMtoSA(data As Dictionary(Of String, String)) As StringArray
        Dim out As New StringArray
        For Each key In data.Keys
            out.add(key)
        Next

        'Use the output list of keys as the iterator to ensure correct value ordering
        Dim size As Integer = out.size
        For i = 0 To size Step 2
            out.add(data.Item(out.get(i)))
        Next

        Return out
    End Function


    Private Function SAtoHM(data As StringArray) As Dictionary(Of String, String)
        Dim out As New Dictionary(Of String, String)

        If data.size Mod 2 <> 0 Then
            Throw New ArrayIndexOutOfBoundsException("StringArray contains an odd number of elements")
        End If

        Dim setSize As Integer = data.size / 2
        For i = 0 To setSize Step 2
            If out.ContainsKey(data.get(i)) = False Then
                out.Add(data.get(i), data.get(i + setSize))
            Else

            End If
        Next
        Return out
    End Function


    '* Update with new data from a remote subscribed table
    '*
    '* @param itable The underlying NetworkTable table
    '* @param key The array name -- must match our name to trigger an update
    '* @param value The new or updated array
    '* @param isNew True if the array did not previous exist

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
