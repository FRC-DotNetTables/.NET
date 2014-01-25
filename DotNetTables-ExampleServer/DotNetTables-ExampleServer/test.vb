Imports System.Threading
Imports DotNetTables

Public Class test

    Private Trd As Thread
    Private _update As Boolean

    Private Sub StartBtn_Click(sender As Object, e As EventArgs) Handles StartBtn.Click

        If _update = False Then
            Dim MyServer As New Server
            Trd = New Thread(AddressOf MyServer.run)
            Trd.IsBackground = True
            Trd.Start()

            _update = True
            StartBtn.Text = "Update"
        Else
            UpdateOutput()
        End If

    End Sub


    Public Sub UpdateOutput()

        Label1.Text = ""

        Dim GetClientTable As DotNetTable = DotNetTables.DotNetTables.findTable("FromClient")

        For Each item As String In GetClientTable.Keys
            Dim key As String = item
            Label1.Text = Label1.Text & vbNewLine & key & " >=" & GetClientTable.getValue(key)
        Next


    End Sub



End Class
