<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Open
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
        Me.components = New System.ComponentModel.Container()
        Me.IPTxt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.StartBtn = New System.Windows.Forms.Button()
        Me.ServerRB = New System.Windows.Forms.RadioButton()
        Me.ClientRB = New System.Windows.Forms.RadioButton()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'IPTxt
        '
        Me.IPTxt.Location = New System.Drawing.Point(185, 43)
        Me.IPTxt.Name = "IPTxt"
        Me.IPTxt.Size = New System.Drawing.Size(117, 20)
        Me.IPTxt.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(182, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Team # or IP:"
        '
        'StartBtn
        '
        Me.StartBtn.Location = New System.Drawing.Point(227, 92)
        Me.StartBtn.Name = "StartBtn"
        Me.StartBtn.Size = New System.Drawing.Size(75, 23)
        Me.StartBtn.TabIndex = 4
        Me.StartBtn.Text = "Start"
        Me.StartBtn.UseVisualStyleBackColor = True
        '
        'ServerRB
        '
        Me.ServerRB.AutoSize = True
        Me.ServerRB.Location = New System.Drawing.Point(24, 23)
        Me.ServerRB.Name = "ServerRB"
        Me.ServerRB.Size = New System.Drawing.Size(56, 17)
        Me.ServerRB.TabIndex = 5
        Me.ServerRB.TabStop = True
        Me.ServerRB.Text = "Server"
        Me.ServerRB.UseVisualStyleBackColor = True
        '
        'ClientRB
        '
        Me.ClientRB.AutoSize = True
        Me.ClientRB.Location = New System.Drawing.Point(24, 46)
        Me.ClientRB.Name = "ClientRB"
        Me.ClientRB.Size = New System.Drawing.Size(51, 17)
        Me.ClientRB.TabIndex = 6
        Me.ClientRB.TabStop = True
        Me.ClientRB.Text = "Client"
        Me.ClientRB.UseVisualStyleBackColor = True
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.ErrorProvider1.ContainerControl = Me
        '
        'Open
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(317, 127)
        Me.Controls.Add(Me.ClientRB)
        Me.Controls.Add(Me.ServerRB)
        Me.Controls.Add(Me.StartBtn)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.IPTxt)
        Me.Name = "Open"
        Me.Text = "Mode"
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents IPTxt As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents StartBtn As System.Windows.Forms.Button
    Friend WithEvents ServerRB As System.Windows.Forms.RadioButton
    Friend WithEvents ClientRB As System.Windows.Forms.RadioButton
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
End Class
