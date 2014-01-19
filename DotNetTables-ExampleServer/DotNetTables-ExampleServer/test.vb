Public Class test

    Private Sub test_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Label1.Text = "BOB"



    End Sub


    Private Sub StartBtn_Click(sender As Object, e As EventArgs) Handles StartBtn.Click
        Dim MyServer As New Server
        MyServer.run()
    End Sub
End Class
