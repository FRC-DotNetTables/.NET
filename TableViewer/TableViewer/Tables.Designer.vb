<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Tables
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
        Me.TableDGV = New System.Windows.Forms.DataGridView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteRowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabelLast = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabelInterval = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.IntervalTxt = New System.Windows.Forms.TextBox()
        Me.IntervalBtn = New System.Windows.Forms.Button()
        Me.ToolStripStatusStale = New System.Windows.Forms.ToolStripStatusLabel()
        CType(Me.TableDGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableDGV
        '
        Me.TableDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TableDGV.ContextMenuStrip = Me.ContextMenuStrip1
        Me.TableDGV.Location = New System.Drawing.Point(12, 12)
        Me.TableDGV.Name = "TableDGV"
        Me.TableDGV.Size = New System.Drawing.Size(610, 323)
        Me.TableDGV.TabIndex = 0
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteRowToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(134, 26)
        '
        'DeleteRowToolStripMenuItem
        '
        Me.DeleteRowToolStripMenuItem.Name = "DeleteRowToolStripMenuItem"
        Me.DeleteRowToolStripMenuItem.Size = New System.Drawing.Size(133, 22)
        Me.DeleteRowToolStripMenuItem.Text = "Delete Row"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabelLast, Me.ToolStripStatusLabelInterval, Me.ToolStripStatusStale})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 345)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(634, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabelLast
        '
        Me.ToolStripStatusLabelLast.Name = "ToolStripStatusLabelLast"
        Me.ToolStripStatusLabelLast.Size = New System.Drawing.Size(75, 17)
        Me.ToolStripStatusLabelLast.Text = "Last Update: "
        '
        'ToolStripStatusLabelInterval
        '
        Me.ToolStripStatusLabelInterval.Name = "ToolStripStatusLabelInterval"
        Me.ToolStripStatusLabelInterval.Size = New System.Drawing.Size(93, 17)
        Me.ToolStripStatusLabelInterval.Text = "Update Interval: "
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(357, 351)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Update Interval:"
        Me.Label1.Visible = False
        '
        'IntervalTxt
        '
        Me.IntervalTxt.Location = New System.Drawing.Point(446, 348)
        Me.IntervalTxt.Name = "IntervalTxt"
        Me.IntervalTxt.Size = New System.Drawing.Size(87, 20)
        Me.IntervalTxt.TabIndex = 3
        Me.IntervalTxt.Visible = False
        '
        'IntervalBtn
        '
        Me.IntervalBtn.Location = New System.Drawing.Point(539, 345)
        Me.IntervalBtn.Name = "IntervalBtn"
        Me.IntervalBtn.Size = New System.Drawing.Size(83, 23)
        Me.IntervalBtn.TabIndex = 7
        Me.IntervalBtn.Text = "Update"
        Me.IntervalBtn.UseVisualStyleBackColor = True
        Me.IntervalBtn.Visible = False
        '
        'ToolStripStatusStale
        '
        Me.ToolStripStatusStale.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripStatusStale.ForeColor = System.Drawing.Color.Red
        Me.ToolStripStatusStale.Name = "ToolStripStatusStale"
        Me.ToolStripStatusStale.Size = New System.Drawing.Size(0, 17)
        '
        'Tables
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(634, 367)
        Me.Controls.Add(Me.IntervalBtn)
        Me.Controls.Add(Me.IntervalTxt)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.TableDGV)
        Me.Name = "Tables"
        Me.Text = "Subscribed to"
        CType(Me.TableDGV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TableDGV As System.Windows.Forms.DataGridView
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabelLast As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabelInterval As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents IntervalTxt As System.Windows.Forms.TextBox
    Friend WithEvents IntervalBtn As System.Windows.Forms.Button
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DeleteRowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripStatusStale As System.Windows.Forms.ToolStripStatusLabel

End Class
