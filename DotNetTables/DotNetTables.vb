Imports System.Text.RegularExpressions
Imports edu.wpi.first.wpilibj
Imports edu.wpi.first.wpilibj.networktables
Imports edu.wpi.first.wpilibj.networktables.NetworkTable
Imports System.IO

Public Class DotNetTables



    'The table name used for the underlying NetworkTable
    Public Const TABLE_NAME As String = "DotNet"
    Private nt_table As NetworkTable
    Private client As Boolean = False
    Private connected As Boolean = False
    Private DotNetTable() As ArrayList
    Private tables As List(Of DotNetTable)

    Private Sub init()
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

    End Sub

    Public Sub startServer()
        init()
    End Sub


    Public Sub startClient(IP As String)
        NetworkTable.setClientMode()

        If Regex.Match(IP, "^\\d{4}$").Success Then
            NetworkTable.setTeam(CInt(IP))
        ElseIf Regex.Match(IP, "^(?:\\d{1,3}.){3}\\d{1,3}$").Success Then
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
    Public Function isClient() As Boolean
        Return client
    End Function

    '/**
    ' * @return True if the connection has been successfully initialized
    ' */
    Public Function isConnected() As Boolean
        Return connected
    End Function

    '/**
    ' * Find the specified table in the subscribed tables list, if it exists
    ' *
    ' * @param name The table to be found
    ' * @return The specified table, if available. NULL if no such table exists.
    ' */

    Public Function findTable(name As String) As DotNetTable
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
    Public Function subscribe(name As String) As DotNetTable
        Return getTable(name, False)
    End Function

    '/**
    ' * Publish a table for remote hosts. Works in both server or client modes.
    ' *
    ' * @param name Name of the table to publish
    ' * @return The published table
    ' */
    Public Function publish(name As String) As DotNetTable
        Return getTable(name, True)
    End Function

    '/**
    ' * Get a table, creating and subscribing/publishing as necessary
    ' *
    ' * @param name New or existing table name
    ' * @return The table to get/create
    ' */
    Private Function getTable(name As String, writable As Boolean) As DotNetTable
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
    End Function


    '/**
    ' * Removes a table from the subscription/publish list
    ' *
    ' * @param name The table to remove
    ' */
    Public Sub drop(name As String)
        Try
            Dim table As DotNetTable = findTable(name)
            nt_table.removeTableListener(table)
            tables.Remove(table)
        Catch ex As IllegalArgumentException
            'Ignore invalid drop requests
        End Try
    End Sub


    '/**
    ' * Push the provided object into the NetworkTable
    ' *
    ' * @param name DotNetTable name
    ' * @param data StringArray-packed DotNetTable data
    ' */
    Public Sub push(name As String, data As Object)
        If Not isClient() Then
            Throw New IllegalStateException("NetworkTable not initalized")
        End If
        nt_table.putValue(name, data)
    End Sub


End Class
