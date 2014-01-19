
Imports DotNetTables.DotNetTable
Imports DotNetTables.DotNetTables
Imports System.IO

Public Class Server
    Implements DotNetTableEvents

    Public Sub run()

        'Start NetworktTables
        Try
            DotNetTables.DotNetTables.startServer()
        Catch ex As IOException
            'do a log
            'Logger.getLogger(Client.class.getName()).log(Level.SEVERE, null, ex);
            Exit Sub
        End Try

        'Publish and subscribe a table
        Dim server As DotNetTable = DotNetTables.DotNetTables.publish("FromServer") 'write only
        Dim client As DotNetTable = DotNetTables.DotNetTables.subscribe("FromClient") 'read only

        'register for updates from the subscribed table
        client.onChange(Me)

        'put new data into our published table every second
        Dim i As Integer = 0
        While True
            Try
                Threading.Thread.Sleep(1000)
            Catch ex As Exception
                'do a log
                'Logger.getLogger(Client.class.getName()).log(Level.SEVERE, null, ex);
            End Try

            If i Mod 10 = 0 Then
                server.clear()
            End If

            server.setValue("ServerKey-" & i, "ServerVal-" & i)
            server.send()
            i += 2

        End While
    End Sub

    Public Sub changed(table As DotNetTable) Implements DotNetTableEvents.changed
        For Each it As String In table.Keys
            Dim key As String = it
            Debug.Print(key & " >= " & table.getValue(key))
        Next

    End Sub


    Public Sub stale(table As DotNetTable) Implements DotNetTableEvents.stale
        Throw New InvalidOperationException("Not supported yet")
    End Sub


End Class
