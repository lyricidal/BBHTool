<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChangesLog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChangesLog))
        Me.ChangeLog = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'ChangeLog
        '
        Me.ChangeLog.BackColor = System.Drawing.SystemColors.Control
        Me.ChangeLog.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.ChangeLog.Location = New System.Drawing.Point(5, 5)
        Me.ChangeLog.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ChangeLog.Name = "ChangeLog"
        Me.ChangeLog.ReadOnly = True
        Me.ChangeLog.Size = New System.Drawing.Size(601, 385)
        Me.ChangeLog.TabIndex = 0
        Me.ChangeLog.Text = resources.GetString("ChangeLog.Text")
        '
        'ChangesLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(611, 394)
        Me.Controls.Add(Me.ChangeLog)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "ChangesLog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "BBHTool Change Log"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ChangeLog As System.Windows.Forms.RichTextBox
End Class
