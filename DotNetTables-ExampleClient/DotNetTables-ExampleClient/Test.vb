Public Class Test

    Private Sub test_Load(sender As Object, e As EventArgs) Handles MyBase.Load

       
    End Sub

    Private Sub StartBtn_Click(sender As Object, e As EventArgs) Handles StartBtn.Click
        Dim MyClient As New Client
        MyClient.run()
    End Sub
End Class
