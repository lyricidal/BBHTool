<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class About
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(About))
        Me.BBHybrids = New System.Windows.Forms.PictureBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel3 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel4 = New System.Windows.Forms.LinkLabel()
        CType(Me.BBHybrids, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BBHybrids
        '
        Me.BBHybrids.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BBHybrids.Image = CType(resources.GetObject("BBHybrids.Image"), System.Drawing.Image)
        Me.BBHybrids.Location = New System.Drawing.Point(25, 5)
        Me.BBHybrids.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BBHybrids.Name = "BBHybrids"
        Me.BBHybrids.Size = New System.Drawing.Size(178, 52)
        Me.BBHybrids.TabIndex = 207
        Me.BBHybrids.TabStop = False
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.Control
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox1.Location = New System.Drawing.Point(208, 5)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(81, 19)
        Me.TextBox1.TabIndex = 208
        Me.TextBox1.Text = "BBH Tool" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.SystemColors.Control
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TextBox2.Location = New System.Drawing.Point(208, 22)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(144, 18)
        Me.TextBox2.TabIndex = 209
        Me.TextBox2.Text = "Version:"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(38, 61)
        Me.LinkLabel1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(85, 17)
        Me.LinkLabel1.TabIndex = 210
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Change Log"
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.Location = New System.Drawing.Point(129, 61)
        Me.LinkLabel2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(103, 17)
        Me.LinkLabel2.TabIndex = 211
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "@theiexplorers"
        '
        'LinkLabel3
        '
        Me.LinkLabel3.AutoSize = True
        Me.LinkLabel3.Location = New System.Drawing.Point(235, 61)
        Me.LinkLabel3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LinkLabel3.Name = "LinkLabel3"
        Me.LinkLabel3.Size = New System.Drawing.Size(72, 17)
        Me.LinkLabel3.TabIndex = 212
        Me.LinkLabel3.TabStop = True
        Me.LinkLabel3.Text = "@bbhplus"
        '
        'LinkLabel4
        '
        Me.LinkLabel4.AutoSize = True
        Me.LinkLabel4.Location = New System.Drawing.Point(204, 41)
        Me.LinkLabel4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LinkLabel4.Name = "LinkLabel4"
        Me.LinkLabel4.Size = New System.Drawing.Size(150, 17)
        Me.LinkLabel4.TabIndex = 213
        Me.LinkLabel4.TabStop = True
        Me.LinkLabel4.Text = "www.theiexplorers.com"
        '
        'About
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(120.0!, 120.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(359, 86)
        Me.Controls.Add(Me.LinkLabel4)
        Me.Controls.Add(Me.LinkLabel3)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.BBHybrids)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "About"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "About..."
        CType(Me.BBHybrids, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BBHybrids As System.Windows.Forms.PictureBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel3 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel4 As System.Windows.Forms.LinkLabel
End Class
