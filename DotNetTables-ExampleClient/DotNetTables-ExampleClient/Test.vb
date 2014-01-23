Imports System.Threading

Public Class Test

    Private Trd As Thread


    Private Sub StartBtn_Click(sender As Object, e As EventArgs) Handles StartBtn.Click
        Dim MyClient As New Client
        Trd = New Thread(AddressOf MyClient.run)
        Trd.IsBackground = True
        Trd.Start()

        StartBtn.Enabled = False
    End Sub

    Public Sub UpdateOutput(table As DotNetTable)

    End Sub
End Class
