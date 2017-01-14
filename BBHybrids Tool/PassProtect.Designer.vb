<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PassProtect
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PassProtect))
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(9, 8)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(156, 20)
        Me.TextBox1.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Bold)
        Me.Button1.Location = New System.Drawing.Point(9, 34)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(77, 27)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Let Me In"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Bold)
        Me.Button2.Location = New System.Drawing.Point(88, 34)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(77, 27)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "I Give Up"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Button3.Location = New System.Drawing.Point(9, 63)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(156, 27)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "Become A VIP"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'PassProtect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(175, 94)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "PassProtect"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "VIP Only"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
End Class
