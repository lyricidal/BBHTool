<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EventLogs
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EventLogs))
        Me.EventLogBox = New System.Windows.Forms.RichTextBox()
        Me.SaveTxt = New System.Windows.Forms.Button()
        Me.ClearLog = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'EventLogBox
        '
        Me.EventLogBox.BackColor = System.Drawing.Color.White
        Me.EventLogBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.EventLogBox.Location = New System.Drawing.Point(5, 5)
        Me.EventLogBox.Margin = New System.Windows.Forms.Padding(4)
        Me.EventLogBox.Name = "EventLogBox"
        Me.EventLogBox.ReadOnly = True
        Me.EventLogBox.Size = New System.Drawing.Size(601, 355)
        Me.EventLogBox.TabIndex = 0
        Me.EventLogBox.Text = ""
        '
        'SaveTxt
        '
        Me.SaveTxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.SaveTxt.Location = New System.Drawing.Point(484, 367)
        Me.SaveTxt.Name = "SaveTxt"
        Me.SaveTxt.Size = New System.Drawing.Size(122, 27)
        Me.SaveTxt.TabIndex = 230
        Me.SaveTxt.Text = "Save To .txt"
        Me.SaveTxt.UseVisualStyleBackColor = True
        '
        'ClearLog
        '
        Me.ClearLog.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ClearLog.Location = New System.Drawing.Point(341, 367)
        Me.ClearLog.Name = "ClearLog"
        Me.ClearLog.Size = New System.Drawing.Size(137, 27)
        Me.ClearLog.TabIndex = 231
        Me.ClearLog.Text = "Clear Event Log"
        Me.ClearLog.UseVisualStyleBackColor = True
        '
        'EventLogs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(611, 394)
        Me.Controls.Add(Me.ClearLog)
        Me.Controls.Add(Me.SaveTxt)
        Me.Controls.Add(Me.EventLogBox)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "EventLogs"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "BBHTool Event Log"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents EventLogBox As System.Windows.Forms.RichTextBox
    Friend WithEvents SaveTxt As System.Windows.Forms.Button
    Friend WithEvents ClearLog As System.Windows.Forms.Button
End Class
