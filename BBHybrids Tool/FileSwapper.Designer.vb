<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FileSwapper
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FileSwapper))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.OSFolderBox = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.OutputFolder = New System.Windows.Forms.ComboBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.SwapFiles = New System.Windows.Forms.Button()
        Me.FileList = New System.Windows.Forms.RichTextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.SwapFullJava = New System.Windows.Forms.CheckBox()
        Me.SwapperProgressBar = New System.Windows.Forms.ProgressBar()
        Me.ClearFileList = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.OSFolderBox)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(8, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(672, 53)
        Me.GroupBox1.TabIndex = 192
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Input Folder:"
        '
        'OSFolderBox
        '
        Me.OSFolderBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.OSFolderBox.FormattingEnabled = True
        Me.OSFolderBox.Location = New System.Drawing.Point(7, 21)
        Me.OSFolderBox.Name = "OSFolderBox"
        Me.OSFolderBox.Size = New System.Drawing.Size(658, 21)
        Me.OSFolderBox.TabIndex = 197
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.OutputFolder)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(8, 71)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(672, 53)
        Me.GroupBox2.TabIndex = 193
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Output Folder:"
        '
        'OutputFolder
        '
        Me.OutputFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.OutputFolder.FormattingEnabled = True
        Me.OutputFolder.Location = New System.Drawing.Point(7, 21)
        Me.OutputFolder.Name = "OutputFolder"
        Me.OutputFolder.Size = New System.Drawing.Size(658, 21)
        Me.OutputFolder.TabIndex = 198
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 592)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(689, 22)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 194
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(0, 17)
        '
        'SwapFiles
        '
        Me.SwapFiles.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SwapFiles.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold)
        Me.SwapFiles.Location = New System.Drawing.Point(222, 511)
        Me.SwapFiles.Name = "SwapFiles"
        Me.SwapFiles.Size = New System.Drawing.Size(236, 45)
        Me.SwapFiles.TabIndex = 195
        Me.SwapFiles.Text = "Swap My Files!"
        Me.SwapFiles.UseVisualStyleBackColor = True
        '
        'FileList
        '
        Me.FileList.Location = New System.Drawing.Point(7, 19)
        Me.FileList.Name = "FileList"
        Me.FileList.Size = New System.Drawing.Size(659, 357)
        Me.FileList.TabIndex = 196
        Me.FileList.Text = ""
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.FileList)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(8, 121)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(672, 384)
        Me.GroupBox3.TabIndex = 197
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Files:"
        '
        'SwapFullJava
        '
        Me.SwapFullJava.AutoSize = True
        Me.SwapFullJava.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.SwapFullJava.Location = New System.Drawing.Point(15, 512)
        Me.SwapFullJava.Name = "SwapFullJava"
        Me.SwapFullJava.Size = New System.Drawing.Size(112, 17)
        Me.SwapFullJava.TabIndex = 198
        Me.SwapFullJava.Text = "Swap Full Java"
        Me.SwapFullJava.UseVisualStyleBackColor = True
        '
        'SwapperProgressBar
        '
        Me.SwapperProgressBar.Location = New System.Drawing.Point(15, 564)
        Me.SwapperProgressBar.MarqueeAnimationSpeed = 75
        Me.SwapperProgressBar.Name = "SwapperProgressBar"
        Me.SwapperProgressBar.Size = New System.Drawing.Size(658, 18)
        Me.SwapperProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.SwapperProgressBar.TabIndex = 263
        '
        'ClearFileList
        '
        Me.ClearFileList.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ClearFileList.Location = New System.Drawing.Point(600, 512)
        Me.ClearFileList.Name = "ClearFileList"
        Me.ClearFileList.Size = New System.Drawing.Size(80, 27)
        Me.ClearFileList.TabIndex = 264
        Me.ClearFileList.Text = "Clear"
        Me.ClearFileList.UseVisualStyleBackColor = True
        '
        'FileSwapper
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(689, 614)
        Me.Controls.Add(Me.ClearFileList)
        Me.Controls.Add(Me.SwapperProgressBar)
        Me.Controls.Add(Me.SwapFullJava)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.SwapFiles)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FileSwapper"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "File Swapper"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents SwapFiles As System.Windows.Forms.Button
    Friend WithEvents FileList As System.Windows.Forms.RichTextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents OSFolderBox As System.Windows.Forms.ComboBox
    Friend WithEvents OutputFolder As System.Windows.Forms.ComboBox
    Friend WithEvents SwapFullJava As System.Windows.Forms.CheckBox
    Friend WithEvents SwapperProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents ClearFileList As System.Windows.Forms.Button
End Class
