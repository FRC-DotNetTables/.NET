Imports DotNetTables
Imports DotNetTables.DotNetTable
Imports DotNetTables.DotNetTables
Imports System.IO


Public Class Main

    Public ServerMode As Boolean

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim ModeSelect As New Open
        Dim MyResult As DialogResult
        MyResult = ModeSelect.ShowDialog()

        If MyResult <> Windows.Forms.DialogResult.OK Then
            Me.Close()
        End If

        ServerMode = My.Settings.ServerMode
        If ServerMode = True Then
            Me.Text = "Server Mode"
        Else
            Me.Text = "Client Mode"
        End If

        Start()
    End Sub

    Public Sub Start()
        'Start NetworkTables
        Try
            If ServerMode = True Then
                DotNetTables.DotNetTables.startServer()
            Else
                DotNetTables.DotNetTables.startClient(My.Settings.IP)
            End If
        Catch ex As IOException
            'do a log
            'Logger.getLogger(Client.class.getName()).log(Level.SEVERE, null, ex);
            Exit Sub
        End Try
    End Sub

    Private Sub PublishBtn_Click(sender As Object, e As EventArgs) Handles PublishBtn.Click, PublishToolStripMenuItem.Click
        Dim PublishedTable As New Tables(TableNameTxt.Text, True)
        PublishedTable.Show()
        TableNameTxt.Text = ""
    End Sub

    Private Sub SubscribeBtn_Click(sender As Object, e As EventArgs) Handles SubscribeBtn.Click, SubscribeToolStripMenuItem.Click
        Dim SubscribedTable As New Tables(TableNameTxt.Text, False)
        SubscribedTable.Show()
        TableNameTxt.Text = ""
    End Sub


    Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing, ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub RestoreLastSessionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestoreLastSessionToolStripMenuItem.Click


    End Sub

End Class