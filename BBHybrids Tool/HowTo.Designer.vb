<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HowTo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HowTo))
        Me.HowToTabs = New System.Windows.Forms.TabControl()
        Me.HowToShrink = New System.Windows.Forms.TabPage()
        Me.ChangeLog = New System.Windows.Forms.RichTextBox()
        Me.HowToCreate = New System.Windows.Forms.TabPage()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.HowToOTA = New System.Windows.Forms.TabPage()
        Me.RichTextBox2 = New System.Windows.Forms.RichTextBox()
        Me.HowToBuild = New System.Windows.Forms.TabPage()
        Me.RichTextBox3 = New System.Windows.Forms.RichTextBox()
        Me.HowToPhone = New System.Windows.Forms.TabPage()
        Me.RichTextBox4 = New System.Windows.Forms.RichTextBox()
        Me.HowToPlayBook = New System.Windows.Forms.TabPage()
        Me.RichTextBox6 = New System.Windows.Forms.RichTextBox()
        Me.HowToTabs.SuspendLayout()
        Me.HowToShrink.SuspendLayout()
        Me.HowToCreate.SuspendLayout()
        Me.HowToOTA.SuspendLayout()
        Me.HowToBuild.SuspendLayout()
        Me.HowToPhone.SuspendLayout()
        Me.HowToPlayBook.SuspendLayout()
        Me.SuspendLayout()
        '
        'HowToTabs
        '
        Me.HowToTabs.Controls.Add(Me.HowToShrink)
        Me.HowToTabs.Controls.Add(Me.HowToCreate)
        Me.HowToTabs.Controls.Add(Me.HowToOTA)
        Me.HowToTabs.Controls.Add(Me.HowToBuild)
        Me.HowToTabs.Controls.Add(Me.HowToPhone)
        Me.HowToTabs.Controls.Add(Me.HowToPlayBook)
        Me.HowToTabs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HowToTabs.Location = New System.Drawing.Point(0, 2)
        Me.HowToTabs.Name = "HowToTabs"
        Me.HowToTabs.SelectedIndex = 0
        Me.HowToTabs.Size = New System.Drawing.Size(669, 596)
        Me.HowToTabs.TabIndex = 0
        '
        'HowToShrink
        '
        Me.HowToShrink.BackColor = System.Drawing.SystemColors.Control
        Me.HowToShrink.Controls.Add(Me.ChangeLog)
        Me.HowToShrink.Location = New System.Drawing.Point(4, 22)
        Me.HowToShrink.Name = "HowToShrink"
        Me.HowToShrink.Padding = New System.Windows.Forms.Padding(3)
        Me.HowToShrink.Size = New System.Drawing.Size(661, 570)
        Me.HowToShrink.TabIndex = 0
        Me.HowToShrink.Text = "Shrink-A-OS"
        '
        'ChangeLog
        '
        Me.ChangeLog.BackColor = System.Drawing.SystemColors.Control
        Me.ChangeLog.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.ChangeLog.Location = New System.Drawing.Point(0, 0)
        Me.ChangeLog.Margin = New System.Windows.Forms.Padding(4)
        Me.ChangeLog.Name = "ChangeLog"
        Me.ChangeLog.ReadOnly = True
        Me.ChangeLog.Size = New System.Drawing.Size(661, 563)
        Me.ChangeLog.TabIndex = 1
        Me.ChangeLog.Text = resources.GetString("ChangeLog.Text")
        '
        'HowToCreate
        '
        Me.HowToCreate.BackColor = System.Drawing.SystemColors.Control
        Me.HowToCreate.Controls.Add(Me.RichTextBox1)
        Me.HowToCreate.Location = New System.Drawing.Point(4, 22)
        Me.HowToCreate.Name = "HowToCreate"
        Me.HowToCreate.Size = New System.Drawing.Size(661, 570)
        Me.HowToCreate.TabIndex = 2
        Me.HowToCreate.Text = "Create-A-JAD"
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BackColor = System.Drawing.SystemColors.Control
        Me.RichTextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.RichTextBox1.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ReadOnly = True
        Me.RichTextBox1.Size = New System.Drawing.Size(661, 563)
        Me.RichTextBox1.TabIndex = 2
        Me.RichTextBox1.Text = resources.GetString("RichTextBox1.Text")
        '
        'HowToOTA
        '
        Me.HowToOTA.BackColor = System.Drawing.SystemColors.Control
        Me.HowToOTA.Controls.Add(Me.RichTextBox2)
        Me.HowToOTA.Location = New System.Drawing.Point(4, 22)
        Me.HowToOTA.Name = "HowToOTA"
        Me.HowToOTA.Size = New System.Drawing.Size(661, 570)
        Me.HowToOTA.TabIndex = 3
        Me.HowToOTA.Text = "OTA Downloader"
        '
        'RichTextBox2
        '
        Me.RichTextBox2.BackColor = System.Drawing.SystemColors.Control
        Me.RichTextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.RichTextBox2.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.RichTextBox2.Name = "RichTextBox2"
        Me.RichTextBox2.ReadOnly = True
        Me.RichTextBox2.Size = New System.Drawing.Size(661, 563)
        Me.RichTextBox2.TabIndex = 2
        Me.RichTextBox2.Text = resources.GetString("RichTextBox2.Text")
        '
        'HowToBuild
        '
        Me.HowToBuild.BackColor = System.Drawing.SystemColors.Control
        Me.HowToBuild.Controls.Add(Me.RichTextBox3)
        Me.HowToBuild.Location = New System.Drawing.Point(4, 22)
        Me.HowToBuild.Name = "HowToBuild"
        Me.HowToBuild.Padding = New System.Windows.Forms.Padding(3)
        Me.HowToBuild.Size = New System.Drawing.Size(661, 570)
        Me.HowToBuild.TabIndex = 1
        Me.HowToBuild.Text = "Build-A-Hybrid"
        '
        'RichTextBox3
        '
        Me.RichTextBox3.BackColor = System.Drawing.SystemColors.Control
        Me.RichTextBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.RichTextBox3.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.RichTextBox3.Name = "RichTextBox3"
        Me.RichTextBox3.ReadOnly = True
        Me.RichTextBox3.Size = New System.Drawing.Size(661, 563)
        Me.RichTextBox3.TabIndex = 2
        Me.RichTextBox3.Text = resources.GetString("RichTextBox3.Text")
        '
        'HowToPhone
        '
        Me.HowToPhone.BackColor = System.Drawing.SystemColors.Control
        Me.HowToPhone.Controls.Add(Me.RichTextBox4)
        Me.HowToPhone.Location = New System.Drawing.Point(4, 22)
        Me.HowToPhone.Name = "HowToPhone"
        Me.HowToPhone.Size = New System.Drawing.Size(661, 570)
        Me.HowToPhone.TabIndex = 4
        Me.HowToPhone.Text = "Phone Tools"
        '
        'RichTextBox4
        '
        Me.RichTextBox4.BackColor = System.Drawing.SystemColors.Control
        Me.RichTextBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.RichTextBox4.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.RichTextBox4.Name = "RichTextBox4"
        Me.RichTextBox4.ReadOnly = True
        Me.RichTextBox4.Size = New System.Drawing.Size(661, 563)
        Me.RichTextBox4.TabIndex = 2
        Me.RichTextBox4.Text = resources.GetString("RichTextBox4.Text")
        '
        'HowToPlayBook
        '
        Me.HowToPlayBook.BackColor = System.Drawing.SystemColors.Control
        Me.HowToPlayBook.Controls.Add(Me.RichTextBox6)
        Me.HowToPlayBook.Location = New System.Drawing.Point(4, 22)
        Me.HowToPlayBook.Name = "HowToPlayBook"
        Me.HowToPlayBook.Size = New System.Drawing.Size(661, 570)
        Me.HowToPlayBook.TabIndex = 6
        Me.HowToPlayBook.Text = "PlayBook"
        '
        'RichTextBox6
        '
        Me.RichTextBox6.BackColor = System.Drawing.SystemColors.Control
        Me.RichTextBox6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.RichTextBox6.Location = New System.Drawing.Point(0, 4)
        Me.RichTextBox6.Margin = New System.Windows.Forms.Padding(4)
        Me.RichTextBox6.Name = "RichTextBox6"
        Me.RichTextBox6.ReadOnly = True
        Me.RichTextBox6.Size = New System.Drawing.Size(661, 563)
        Me.RichTextBox6.TabIndex = 3
        Me.RichTextBox6.Text = resources.GetString("RichTextBox6.Text")
        '
        'HowTo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(668, 594)
        Me.Controls.Add(Me.HowToTabs)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "HowTo"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "How To Use..."
        Me.HowToTabs.ResumeLayout(False)
        Me.HowToShrink.ResumeLayout(False)
        Me.HowToCreate.ResumeLayout(False)
        Me.HowToOTA.ResumeLayout(False)
        Me.HowToBuild.ResumeLayout(False)
        Me.HowToPhone.ResumeLayout(False)
        Me.HowToPlayBook.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents HowToTabs As System.Windows.Forms.TabControl
    Friend WithEvents HowToShrink As System.Windows.Forms.TabPage
    Friend WithEvents HowToBuild As System.Windows.Forms.TabPage
    Friend WithEvents HowToCreate As System.Windows.Forms.TabPage
    Friend WithEvents HowToOTA As System.Windows.Forms.TabPage
    Friend WithEvents HowToPhone As System.Windows.Forms.TabPage
    Friend WithEvents ChangeLog As System.Windows.Forms.RichTextBox
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents RichTextBox2 As System.Windows.Forms.RichTextBox
    Friend WithEvents RichTextBox3 As System.Windows.Forms.RichTextBox
    Friend WithEvents RichTextBox4 As System.Windows.Forms.RichTextBox
    Friend WithEvents HowToPlayBook As System.Windows.Forms.TabPage
    Friend WithEvents RichTextBox6 As System.Windows.Forms.RichTextBox
End Class
