<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SSViewer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SSViewer))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ShortenLink = New System.Windows.Forms.CheckBox()
        Me.TweetIt = New System.Windows.Forms.CheckBox()
        Me.ShrunkURL = New System.Windows.Forms.TextBox()
        Me.AutoCopy = New System.Windows.Forms.CheckBox()
        Me.TakeScreenshot = New System.Windows.Forms.Button()
        Me.AfterBox = New System.Windows.Forms.GroupBox()
        Me.CopyImage1 = New System.Windows.Forms.CheckBox()
        Me.UploadImage = New System.Windows.Forms.CheckBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TweetButton = New System.Windows.Forms.Button()
        Me.UploadButton = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CopyImage = New System.Windows.Forms.Button()
        Me.UploadWorker = New System.ComponentModel.BackgroundWorker()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.AfterBox.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(12, 147)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(640, 640)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox1, "Current screenshot of your device")
        '
        'ShortenLink
        '
        Me.ShortenLink.AutoSize = True
        Me.ShortenLink.Enabled = False
        Me.ShortenLink.Location = New System.Drawing.Point(6, 33)
        Me.ShortenLink.Name = "ShortenLink"
        Me.ShortenLink.Size = New System.Drawing.Size(94, 17)
        Me.ShortenLink.TabIndex = 5
        Me.ShortenLink.Text = "Shorten link"
        Me.ToolTip1.SetToolTip(Me.ShortenLink, "Shorten link once image is uploaded")
        Me.ShortenLink.UseVisualStyleBackColor = True
        '
        'TweetIt
        '
        Me.TweetIt.AutoSize = True
        Me.TweetIt.Enabled = False
        Me.TweetIt.Location = New System.Drawing.Point(6, 51)
        Me.TweetIt.Name = "TweetIt"
        Me.TweetIt.Size = New System.Drawing.Size(85, 17)
        Me.TweetIt.TabIndex = 6
        Me.TweetIt.Text = "Tweet link"
        Me.ToolTip1.SetToolTip(Me.TweetIt, "Tweet shortened link")
        Me.TweetIt.UseVisualStyleBackColor = True
        '
        'ShrunkURL
        '
        Me.ShrunkURL.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ShrunkURL.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.ShrunkURL.Location = New System.Drawing.Point(12, 123)
        Me.ShrunkURL.Name = "ShrunkURL"
        Me.ShrunkURL.ReadOnly = True
        Me.ShrunkURL.Size = New System.Drawing.Size(644, 20)
        Me.ShrunkURL.TabIndex = 7
        Me.ShrunkURL.Text = "Your upload link will show here..."
        Me.ToolTip1.SetToolTip(Me.ShrunkURL, "The current link to your uploaded screenshot")
        Me.ShrunkURL.Visible = False
        '
        'AutoCopy
        '
        Me.AutoCopy.AutoSize = True
        Me.AutoCopy.Enabled = False
        Me.AutoCopy.Location = New System.Drawing.Point(6, 87)
        Me.AutoCopy.Name = "AutoCopy"
        Me.AutoCopy.Size = New System.Drawing.Size(149, 17)
        Me.AutoCopy.TabIndex = 8
        Me.AutoCopy.Text = "Copy link to clipboard"
        Me.ToolTip1.SetToolTip(Me.AutoCopy, "Copy upload link to clipboard")
        Me.AutoCopy.UseVisualStyleBackColor = True
        '
        'TakeScreenshot
        '
        Me.TakeScreenshot.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold)
        Me.TakeScreenshot.Location = New System.Drawing.Point(126, 4)
        Me.TakeScreenshot.Name = "TakeScreenshot"
        Me.TakeScreenshot.Size = New System.Drawing.Size(237, 45)
        Me.TakeScreenshot.TabIndex = 9
        Me.TakeScreenshot.Tag = "Take sc"
        Me.TakeScreenshot.Text = "Take Screenshot"
        Me.ToolTip1.SetToolTip(Me.TakeScreenshot, "Take a screenshot of connected device")
        Me.TakeScreenshot.UseVisualStyleBackColor = True
        '
        'AfterBox
        '
        Me.AfterBox.Controls.Add(Me.CopyImage1)
        Me.AfterBox.Controls.Add(Me.UploadImage)
        Me.AfterBox.Controls.Add(Me.ShortenLink)
        Me.AfterBox.Controls.Add(Me.AutoCopy)
        Me.AfterBox.Controls.Add(Me.TweetIt)
        Me.AfterBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.AfterBox.Location = New System.Drawing.Point(369, 4)
        Me.AfterBox.Name = "AfterBox"
        Me.AfterBox.Size = New System.Drawing.Size(177, 109)
        Me.AfterBox.TabIndex = 10
        Me.AfterBox.TabStop = False
        Me.AfterBox.Text = "Next Screenshot:"
        Me.ToolTip1.SetToolTip(Me.AfterBox, "For one click screenshotting")
        '
        'CopyImage1
        '
        Me.CopyImage1.AutoSize = True
        Me.CopyImage1.Enabled = False
        Me.CopyImage1.Location = New System.Drawing.Point(6, 69)
        Me.CopyImage1.Name = "CopyImage1"
        Me.CopyImage1.Size = New System.Drawing.Size(162, 17)
        Me.CopyImage1.TabIndex = 9
        Me.CopyImage1.Text = "Copy image to clipboard"
        Me.ToolTip1.SetToolTip(Me.CopyImage1, "Copy image to clipboard")
        Me.CopyImage1.UseVisualStyleBackColor = True
        '
        'UploadImage
        '
        Me.UploadImage.AutoSize = True
        Me.UploadImage.Location = New System.Drawing.Point(6, 15)
        Me.UploadImage.Name = "UploadImage"
        Me.UploadImage.Size = New System.Drawing.Size(103, 17)
        Me.UploadImage.TabIndex = 7
        Me.UploadImage.Text = "Upload image"
        Me.ToolTip1.SetToolTip(Me.UploadImage, "Upload image after screenshot is taken")
        Me.UploadImage.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 802)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(668, 22)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 11
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(0, 17)
        '
        'TweetButton
        '
        Me.TweetButton.Enabled = False
        Me.TweetButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold)
        Me.TweetButton.Location = New System.Drawing.Point(82, 18)
        Me.TweetButton.Name = "TweetButton"
        Me.TweetButton.Size = New System.Drawing.Size(76, 33)
        Me.TweetButton.TabIndex = 12
        Me.TweetButton.Text = "Tweet"
        Me.ToolTip1.SetToolTip(Me.TweetButton, "Tweet the uploaded image")
        Me.TweetButton.UseVisualStyleBackColor = True
        '
        'UploadButton
        '
        Me.UploadButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold)
        Me.UploadButton.Location = New System.Drawing.Point(3, 18)
        Me.UploadButton.Name = "UploadButton"
        Me.UploadButton.Size = New System.Drawing.Size(76, 33)
        Me.UploadButton.TabIndex = 13
        Me.UploadButton.Text = "Upload"
        Me.ToolTip1.SetToolTip(Me.UploadButton, "Upload the image below")
        Me.UploadButton.UseVisualStyleBackColor = True
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 5000
        Me.ToolTip1.InitialDelay = 1000
        Me.ToolTip1.ReshowDelay = 100
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CopyImage)
        Me.GroupBox1.Controls.Add(Me.UploadButton)
        Me.GroupBox1.Controls.Add(Me.TweetButton)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GroupBox1.Location = New System.Drawing.Point(123, 55)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(240, 58)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Current Screenshot:"
        Me.ToolTip1.SetToolTip(Me.GroupBox1, "For one click screenshotting")
        '
        'CopyImage
        '
        Me.CopyImage.Enabled = False
        Me.CopyImage.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold)
        Me.CopyImage.Location = New System.Drawing.Point(161, 18)
        Me.CopyImage.Name = "CopyImage"
        Me.CopyImage.Size = New System.Drawing.Size(76, 33)
        Me.CopyImage.TabIndex = 15
        Me.CopyImage.Text = "Copy"
        Me.ToolTip1.SetToolTip(Me.CopyImage, "Copy the image below to clipboard")
        Me.CopyImage.UseVisualStyleBackColor = True
        '
        'UploadWorker
        '
        '
        'SSViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(668, 824)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.AfterBox)
        Me.Controls.Add(Me.TakeScreenshot)
        Me.Controls.Add(Me.ShrunkURL)
        Me.Controls.Add(Me.PictureBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "SSViewer"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Screenshot Viewer"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.AfterBox.ResumeLayout(False)
        Me.AfterBox.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents ShortenLink As System.Windows.Forms.CheckBox
    Friend WithEvents TweetIt As System.Windows.Forms.CheckBox
    Friend WithEvents ShrunkURL As System.Windows.Forms.TextBox
    Friend WithEvents AutoCopy As System.Windows.Forms.CheckBox
    Friend WithEvents TakeScreenshot As System.Windows.Forms.Button
    Friend WithEvents AfterBox As System.Windows.Forms.GroupBox
    Friend WithEvents UploadImage As System.Windows.Forms.CheckBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TweetButton As System.Windows.Forms.Button
    Friend WithEvents UploadButton As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents CopyImage1 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents CopyImage As System.Windows.Forms.Button
    Friend WithEvents UploadWorker As System.ComponentModel.BackgroundWorker
End Class
