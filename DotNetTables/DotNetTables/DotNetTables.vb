Imports System.Text.RegularExpressions
Imports edu.wpi.first.wpilibj
Imports edu.wpi.first.wpilibj.networktables
Imports edu.wpi.first.wpilibj.networktables.NetworkTable
Imports System.IO
Imports java.lang

Public Class DotNetTables



    'The table name used for the underlying NetworkTable
    Public Const TABLE_NAME As String = "DotNet"
    Private Shared nt_table As NetworkTable
    Private Shared client As Boolean = False
    Private Shared connected As Boolean = False
    Private Shared DotNetTable() As ArrayList
    Private Shared tables As List(Of DotNetTable)
    Private Shared sync_Lock As New Object

    Private Shared _initialized As Boolean 'for loading dlls



    Private Shared Sub init()
        SyncLock sync_Lock

            tables = New List(Of DotNetTable)

            'Attempt to init the underlying NetworkTable
            Try
                NetworkTable.initialize()
                nt_table = NetworkTable.getTable(TABLE_NAME)
                connected = True
            Catch ex As IOException
                Debug.Print("Unable to initialize NetworkTable: " & TABLE_NAME)
                Throw ex
            End Try
        End SyncLock
    End Sub

    Public Shared Sub startServer()
        init()
    End Sub


    Public Shared Sub startClient(IP As String)
        NetworkTable.setClientMode()

        If Regex.Match(IP, "^\d{4}$").Success Then
            NetworkTable.setTeam(CInt(IP))
        ElseIf Regex.Match(IP, "^(?:\d{1,3}.){3}\d{1,3}$").Success Then
            NetworkTable.setIPAddress(IP)
        Else
            Throw New IllegalArgumentException("Invalid IP address or team number: " & IP)
        End If

        client = True
        init()
    End Sub



    '/**
    '* @return True if this device is configured as a NetworkTable subscriber
    ' */
    Public Shared Function isClient() As Boolean
        Return client
    End Function

    '/**
    ' * @return True if the connection has been successfully initialized
    ' */
    Public Shared Function isConnected() As Boolean
        Return connected
    End Function

    '/**
    ' * Find the specified table in the subscribed tables list, if it exists
    ' *
    ' * @param name The table to be found
    ' * @return The specified table, if available. NULL if no such table exists.
    ' */

    Public Shared Function findTable(name As String) As DotNetTable
        For Each table As DotNetTable In tables
            If table.name = name Then
                Return table
            End If
        Next

        Throw New IllegalArgumentException("No such table: " & name)
    End Function


    '/**
    ' * Subscribe to a table published by a remote host. Works in both server and
    ' * client modes.
    ' *
    ' * @param name Name of the table to subscribe
    ' * @return The subscribed table
    ' */
    Public Shared Function subscribe(name As String) As DotNetTable
        Return getTable(name, False)
    End Function

    '/**
    ' * Publish a table for remote hosts. Works in both server or client modes.
    ' *
    ' * @param name Name of the table to publish
    ' * @return The published table
    ' */
    Public Shared Function publish(name As String) As DotNetTable
        Return getTable(name, True)
    End Function

    '/**
    ' * Get a table, creating and subscribing/publishing as necessary
    ' *
    ' * @param name New or existing table name
    ' * @return The table to get/create
    ' */
    Private Shared Function getTable(name As String, writable As Boolean) As DotNetTable
        SyncLock sync_Lock
            Dim table As DotNetTable
            Try
                table = findTable(name)
            Catch ex As IllegalArgumentException
                table = New DotNetTable(name, writable)
                tables.Add(table)

                'Publish or subscribe the new table
                If writable = True Then
                    table.send()
                Else
                    nt_table.addTableListener(table)
                End If
            End Try

            'Ensure the table has the specified writable state
            If table.iswritable() <> writable Then
                Throw New IllegalStateException("Table already exists but does not share writable state: " & name)
            End If

            Return table
        End SyncLock
    End Function


    '/**
    ' * Removes a table from the subscription/publish list
    ' *
    ' * @param name The table to remove
    ' */
    Public Shared Sub drop(name As String)
        SyncLock sync_Lock
            Try
                Dim table As DotNetTable = findTable(name)
                nt_table.removeTableListener(table)
                tables.Remove(table)
            Catch ex As IllegalArgumentException
                'Ignore invalid drop requests
            End Try
        End SyncLock
    End Sub


    '/**
    ' * Push the provided object into the NetworkTable
    ' *
    ' * @param name DotNetTable name
    ' * @param data StringArray-packed DotNetTable data
    ' */
    Public Shared Sub push(name As String, data As Object)
        SyncLock sync_Lock
            If Not isConnected() Then
                Throw New IllegalStateException("NetworkTable not initalized")
            End If
            Dim table As DotNetTable

            Try
                table = findTable(name)
            Catch ex As IllegalArgumentException
                Throw New IllegalStateException(ex.Message)
            End Try

            If table.iswritable = False Then
                Throw New IllegalStateException("Table not writable: " & name)
            End If

            nt_table.putValue(name, data)
        End SyncLock
    End Sub

    Public Shared Function ResolveAssemblies(sender As Object, e As System.ResolveEventArgs) As Reflection.Assembly
        Dim desiredAssembly = New Reflection.AssemblyName(e.Name)

        Select Case desiredAssembly.Name
            Case "IKVM.OpenJDK.Core"
                Return Reflection.Assembly.Load(My.Resources.IKVM_OpenJDK_Core)
            Case "IKVM.OpenJDK.Util"
                Return Reflection.Assembly.Load(My.Resources.IKVM_OpenJDK_Util)
            Case "IKVM.Runtime"
                Return Reflection.Assembly.Load(My.Resources.IKVM_Runtime)
            Case "networktables-desktop"
                Return Reflection.Assembly.Load(My.Resources.networktables_desktop)
            Case Else
                Return Nothing
        End Select

    End Function



End Class
