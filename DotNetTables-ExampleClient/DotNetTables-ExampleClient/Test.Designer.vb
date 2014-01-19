<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Test
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.StartBtn = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'StartBtn
        '
        Me.StartBtn.Location = New System.Drawing.Point(30, 204)
        Me.StartBtn.Name = "StartBtn"
        Me.StartBtn.Size = New System.Drawing.Size(218, 23)
        Me.StartBtn.TabIndex = 2
        Me.StartBtn.Text = "Start Client"
        Me.StartBtn.UseVisualStyleBackColor = True
        '
        'Test
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.StartBtn)
        Me.Name = "Test"
        Me.Text = "test - client"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents StartBtn As System.Windows.Forms.Button

End Class
