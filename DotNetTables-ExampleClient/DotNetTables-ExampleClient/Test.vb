Imports System.Threading

Public Class Test

    Private Trd As Thread
    Private _update As Boolean

    Private Sub StartBtn_Click(sender As Object, e As EventArgs) Handles StartBtn.Click

        If _update = False Then
            Dim MyClient As New Client
            Trd = New Thread(AddressOf MyClient.run)
            Trd.IsBackground = True
            Trd.Start()

            _update = True
            StartBtn.Text = "Update"
        Else
            UpdateOutput()
        End If

    End Sub

    Public Sub UpdateOutput()
        Dim GetServerTables As DotNetTable = DotNetTables.DotNetTables.findTable("FromServer")

        Label1.Text = ""

        For Each it As String In GetServerTables.Keys
            Dim key As String = it
            Label1.Text = Label1.Text & vbNewLine & key & " >= " & GetServerTables.getValue(key)
        Next

    End Sub
End Class
