Imports System.Threading
Imports DotNetTables

Public Class test

    Private Trd As Thread

    Private Sub StartBtn_Click(sender As Object, e As EventArgs) Handles StartBtn.Click
        Dim MyServer As New Server
        Trd = New Thread(AddressOf MyServer.run)
        Trd.IsBackground = True
        Trd.Start()

        StartBtn.Enabled = False
    End Sub


    Public Sub UpdateOutput(table As DotNetTable)

    End Sub



End Class
