Imports System.Text.RegularExpressions

Public Class Open
    Dim DoValidation As Boolean = False

    Private Sub Open_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IPTxt.Text = My.Settings.IP
        ServerRB.Checked = My.Settings.ServerMode
        ClientRB.Checked = Not My.Settings.ServerMode
    End Sub

    Private Sub ServerRB_CheckedChanged(sender As Object, e As EventArgs) Handles ServerRB.CheckedChanged
        IPTxt.Enabled = ClientRB.Checked
    End Sub

    Private Sub StartBtn_Click(sender As Object, e As EventArgs) Handles StartBtn.Click
        DoValidation = True
        If ValidateChildren() = True Then
            My.Settings.ServerMode = ServerRB.Checked
            My.Settings.IP = IPTxt.Text
            My.Settings.Save()

            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub


    Private Sub IPTxt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles IPTxt.Validating
        If DoValidation = True Then
            If IPTxt.Enabled = True Then
                If Regex.Match(IPTxt.Text, "^(?:\d{1,3}.){3}\d{1,3}$").Success Or Regex.Match(IPTxt.Text, "^\d{4}$").Success Then
                    ErrorProvider1.SetError(IPTxt, "")
                Else
                    ErrorProvider1.SetError(IPTxt, "Please enter a team number or valid IP address.")
                    e.Cancel = True
                End If
            Else
                ErrorProvider1.SetError(IPTxt, "")
            End If
        End If
    End Sub


End Class