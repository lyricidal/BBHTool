<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class APKConverter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(APKConverter))
        Me.Browse = New System.Windows.Forms.Button()
        Me.CAJHelp = New System.Windows.Forms.LinkLabel()
        Me.BrowseSaveTo = New System.Windows.Forms.Button()
        Me.SaveTo = New System.Windows.Forms.TextBox()
        Me.CaJSaveTo = New System.Windows.Forms.Label()
        Me.ConvertIt = New System.Windows.Forms.Button()
        Me.DragAPK = New System.Windows.Forms.Label()
        Me.FileCount = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.BrowseSDK = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.SignBAR = New System.Windows.Forms.CheckBox()
        Me.RememberConversionFolder = New System.Windows.Forms.CheckBox()
        Me.SDKFolder = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.DownloadSDK = New System.Windows.Forms.LinkLabel()
        Me.RememberAndroidSDK = New System.Windows.Forms.CheckBox()
        Me.SigningInfo = New System.Windows.Forms.GroupBox()
        Me.GetKeys = New System.Windows.Forms.LinkLabel()
        Me.RememberSigningInfo = New System.Windows.Forms.CheckBox()
        Me.CSKPass = New System.Windows.Forms.MaskedTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.KeyStorePass = New System.Windows.Forms.MaskedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.P12file = New System.Windows.Forms.TextBox()
        Me.BrowseP12 = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.DisplayEventLog = New System.Windows.Forms.CheckBox()
        Me.ConvertedFile = New System.Windows.Forms.TextBox()
        Me.InstallConverted = New System.Windows.Forms.CheckBox()
        Me.FilesGroupBox = New System.Windows.Forms.GroupBox()
        Me.CheckAllBarsApks = New System.Windows.Forms.LinkLabel()
        Me.ClearAPKs = New System.Windows.Forms.Button()
        Me.FileBox = New System.Windows.Forms.CheckedListBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.txtResults = New System.Windows.Forms.RichTextBox()
        Me.PlayBookProgressBar = New System.Windows.Forms.ProgressBar()
        Me.UnparsedInstallFilesBox = New System.Windows.Forms.RichTextBox()
        Me.ParsedInstallFiles = New System.Windows.Forms.RichTextBox()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SigningInfo.SuspendLayout()
        Me.FilesGroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'Browse
        '
        Me.Browse.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Browse.Location = New System.Drawing.Point(291, 295)
        Me.Browse.Name = "Browse"
        Me.Browse.Size = New System.Drawing.Size(99, 27)
        Me.Browse.TabIndex = 259
        Me.Browse.Text = "Add Files"
        Me.Browse.UseVisualStyleBackColor = True
        '
        'CAJHelp
        '
        Me.CAJHelp.AutoSize = True
        Me.CAJHelp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CAJHelp.Location = New System.Drawing.Point(765, 534)
        Me.CAJHelp.Name = "CAJHelp"
        Me.CAJHelp.Size = New System.Drawing.Size(33, 13)
        Me.CAJHelp.TabIndex = 260
        Me.CAJHelp.TabStop = True
        Me.CAJHelp.Text = "Help"
        '
        'BrowseSaveTo
        '
        Me.BrowseSaveTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.BrowseSaveTo.Location = New System.Drawing.Point(259, 54)
        Me.BrowseSaveTo.Name = "BrowseSaveTo"
        Me.BrowseSaveTo.Size = New System.Drawing.Size(69, 27)
        Me.BrowseSaveTo.TabIndex = 243
        Me.BrowseSaveTo.Text = "Browse"
        Me.BrowseSaveTo.UseVisualStyleBackColor = True
        '
        'SaveTo
        '
        Me.SaveTo.Location = New System.Drawing.Point(6, 32)
        Me.SaveTo.Name = "SaveTo"
        Me.SaveTo.Size = New System.Drawing.Size(323, 20)
        Me.SaveTo.TabIndex = 254
        Me.SaveTo.Text = "Browse for Save To folder..."
        Me.ToolTip1.SetToolTip(Me.SaveTo, "Browse for Save To folder...")
        '
        'CaJSaveTo
        '
        Me.CaJSaveTo.AutoSize = True
        Me.CaJSaveTo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CaJSaveTo.Location = New System.Drawing.Point(6, 16)
        Me.CaJSaveTo.Name = "CaJSaveTo"
        Me.CaJSaveTo.Size = New System.Drawing.Size(59, 13)
        Me.CaJSaveTo.TabIndex = 255
        Me.CaJSaveTo.Text = "Save To:"
        '
        'ConvertIt
        '
        Me.ConvertIt.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Bold)
        Me.ConvertIt.Location = New System.Drawing.Point(215, 483)
        Me.ConvertIt.Name = "ConvertIt"
        Me.ConvertIt.Size = New System.Drawing.Size(304, 44)
        Me.ConvertIt.TabIndex = 251
        Me.ConvertIt.Text = "Convert It!"
        Me.ConvertIt.UseVisualStyleBackColor = True
        '
        'DragAPK
        '
        Me.DragAPK.AllowDrop = True
        Me.DragAPK.AutoSize = True
        Me.DragAPK.BackColor = System.Drawing.Color.White
        Me.DragAPK.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.5!, System.Drawing.FontStyle.Bold)
        Me.DragAPK.Location = New System.Drawing.Point(58, 128)
        Me.DragAPK.Name = "DragAPK"
        Me.DragAPK.Size = New System.Drawing.Size(269, 17)
        Me.DragAPK.TabIndex = 248
        Me.DragAPK.Text = "Drag & Drop .APK(s) / .BAR(s) Here"
        Me.DragAPK.UseMnemonic = False
        '
        'FileCount
        '
        Me.FileCount.AutoSize = True
        Me.FileCount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FileCount.Location = New System.Drawing.Point(374, 351)
        Me.FileCount.Name = "FileCount"
        Me.FileCount.Size = New System.Drawing.Size(0, 13)
        Me.FileCount.TabIndex = 247
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 550)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(804, 22)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 262
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(359, 17)
        Me.ToolStripStatusLabel1.Text = "Welcome to BBH Tool by lyricidal / @theiexplorers"
        '
        'BrowseSDK
        '
        Me.BrowseSDK.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.BrowseSDK.Location = New System.Drawing.Point(667, 42)
        Me.BrowseSDK.Name = "BrowseSDK"
        Me.BrowseSDK.Size = New System.Drawing.Size(69, 23)
        Me.BrowseSDK.TabIndex = 263
        Me.BrowseSDK.Text = "Browse"
        Me.BrowseSDK.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.SignBAR)
        Me.GroupBox1.Controls.Add(Me.RememberConversionFolder)
        Me.GroupBox1.Controls.Add(Me.CaJSaveTo)
        Me.GroupBox1.Controls.Add(Me.SaveTo)
        Me.GroupBox1.Controls.Add(Me.BrowseSaveTo)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(31, 100)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(337, 107)
        Me.GroupBox1.TabIndex = 269
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Conversion Info:"
        '
        'SignBAR
        '
        Me.SignBAR.AutoSize = True
        Me.SignBAR.Checked = Global.BBHTool.My.MySettings.Default.SignConvertedAPK
        Me.SignBAR.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.BBHTool.My.MySettings.Default, "SignConvertedAPK", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.SignBAR.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SignBAR.Location = New System.Drawing.Point(6, 81)
        Me.SignBAR.Name = "SignBAR"
        Me.SignBAR.Size = New System.Drawing.Size(299, 17)
        Me.SignBAR.TabIndex = 267
        Me.SignBAR.Text = "Sign converted file (requires Signing Info below)"
        Me.SignBAR.UseVisualStyleBackColor = True
        '
        'RememberConversionFolder
        '
        Me.RememberConversionFolder.AutoSize = True
        Me.RememberConversionFolder.Checked = Global.BBHTool.My.MySettings.Default.RememberConversionFolder
        Me.RememberConversionFolder.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.BBHTool.My.MySettings.Default, "RememberConversionFolder", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.RememberConversionFolder.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RememberConversionFolder.Location = New System.Drawing.Point(6, 58)
        Me.RememberConversionFolder.Name = "RememberConversionFolder"
        Me.RememberConversionFolder.Size = New System.Drawing.Size(176, 17)
        Me.RememberConversionFolder.TabIndex = 266
        Me.RememberConversionFolder.Text = "Remember Save To Folder"
        Me.RememberConversionFolder.UseVisualStyleBackColor = True
        '
        'SDKFolder
        '
        Me.SDKFolder.Location = New System.Drawing.Point(6, 19)
        Me.SDKFolder.Name = "SDKFolder"
        Me.SDKFolder.Size = New System.Drawing.Size(730, 20)
        Me.SDKFolder.TabIndex = 264
        Me.SDKFolder.Text = "Browse for Android SDK folder..."
        Me.ToolTip1.SetToolTip(Me.SDKFolder, "Browse for Android SDK folder...")
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.DownloadSDK)
        Me.GroupBox2.Controls.Add(Me.RememberAndroidSDK)
        Me.GroupBox2.Controls.Add(Me.SDKFolder)
        Me.GroupBox2.Controls.Add(Me.BrowseSDK)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(31, 23)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(742, 71)
        Me.GroupBox2.TabIndex = 270
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Android SDK (Required):"
        '
        'DownloadSDK
        '
        Me.DownloadSDK.AutoSize = True
        Me.DownloadSDK.Location = New System.Drawing.Point(522, 47)
        Me.DownloadSDK.Name = "DownloadSDK"
        Me.DownloadSDK.Size = New System.Drawing.Size(139, 13)
        Me.DownloadSDK.TabIndex = 275
        Me.DownloadSDK.TabStop = True
        Me.DownloadSDK.Text = "Download Android SDK"
        Me.ToolTip1.SetToolTip(Me.DownloadSDK, "Click to Download Android SDK")
        '
        'RememberAndroidSDK
        '
        Me.RememberAndroidSDK.AutoSize = True
        Me.RememberAndroidSDK.Checked = Global.BBHTool.My.MySettings.Default.RememberSDKFolder
        Me.RememberAndroidSDK.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.BBHTool.My.MySettings.Default, "RememberSDKFolder", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.RememberAndroidSDK.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RememberAndroidSDK.Location = New System.Drawing.Point(6, 42)
        Me.RememberAndroidSDK.Name = "RememberAndroidSDK"
        Me.RememberAndroidSDK.Size = New System.Drawing.Size(200, 17)
        Me.RememberAndroidSDK.TabIndex = 265
        Me.RememberAndroidSDK.Text = "Remember Android SDK Folder"
        Me.RememberAndroidSDK.UseVisualStyleBackColor = True
        '
        'SigningInfo
        '
        Me.SigningInfo.Controls.Add(Me.GetKeys)
        Me.SigningInfo.Controls.Add(Me.RememberSigningInfo)
        Me.SigningInfo.Controls.Add(Me.CSKPass)
        Me.SigningInfo.Controls.Add(Me.Label3)
        Me.SigningInfo.Controls.Add(Me.KeyStorePass)
        Me.SigningInfo.Controls.Add(Me.Label1)
        Me.SigningInfo.Controls.Add(Me.Label2)
        Me.SigningInfo.Controls.Add(Me.P12file)
        Me.SigningInfo.Controls.Add(Me.BrowseP12)
        Me.SigningInfo.Enabled = False
        Me.SigningInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SigningInfo.Location = New System.Drawing.Point(31, 213)
        Me.SigningInfo.Name = "SigningInfo"
        Me.SigningInfo.Size = New System.Drawing.Size(337, 151)
        Me.SigningInfo.TabIndex = 270
        Me.SigningInfo.TabStop = False
        Me.SigningInfo.Text = "Signing Info:"
        '
        'GetKeys
        '
        Me.GetKeys.AutoSize = True
        Me.GetKeys.Location = New System.Drawing.Point(224, 132)
        Me.GetKeys.Name = "GetKeys"
        Me.GetKeys.Size = New System.Drawing.Size(104, 13)
        Me.GetKeys.TabIndex = 267
        Me.GetKeys.TabStop = True
        Me.GetKeys.Text = "Get Signing Keys"
        Me.ToolTip1.SetToolTip(Me.GetKeys, "Click to Get Signing Keys")
        '
        'RememberSigningInfo
        '
        Me.RememberSigningInfo.AutoSize = True
        Me.RememberSigningInfo.Checked = Global.BBHTool.My.MySettings.Default.RememberSigningInfo
        Me.RememberSigningInfo.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.BBHTool.My.MySettings.Default, "RememberSigningInfo", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.RememberSigningInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RememberSigningInfo.Location = New System.Drawing.Point(6, 132)
        Me.RememberSigningInfo.Name = "RememberSigningInfo"
        Me.RememberSigningInfo.Size = New System.Drawing.Size(157, 17)
        Me.RememberSigningInfo.TabIndex = 274
        Me.RememberSigningInfo.Text = "Remember Signing Info"
        Me.RememberSigningInfo.UseVisualStyleBackColor = True
        '
        'CSKPass
        '
        Me.CSKPass.Location = New System.Drawing.Point(9, 109)
        Me.CSKPass.Name = "CSKPass"
        Me.CSKPass.Size = New System.Drawing.Size(139, 20)
        Me.CSKPass.TabIndex = 273
        Me.ToolTip1.SetToolTip(Me.CSKPass, "The password specified when registering your RIM®-issued RDK file")
        Me.CSKPass.UseSystemPasswordChar = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 93)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(93, 13)
        Me.Label3.TabIndex = 272
        Me.Label3.Text = "CSK Password:"
        '
        'KeyStorePass
        '
        Me.KeyStorePass.Location = New System.Drawing.Point(9, 70)
        Me.KeyStorePass.Name = "KeyStorePass"
        Me.KeyStorePass.Size = New System.Drawing.Size(139, 20)
        Me.KeyStorePass.TabIndex = 271
        Me.ToolTip1.SetToolTip(Me.KeyStorePass, "The password specified when creating your P12 file")
        Me.KeyStorePass.UseSystemPasswordChar = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 13)
        Me.Label1.TabIndex = 252
        Me.Label1.Text = "Keytore Password:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 13)
        Me.Label2.TabIndex = 255
        Me.Label2.Text = "Keystore File:"
        '
        'P12file
        '
        Me.P12file.Location = New System.Drawing.Point(6, 31)
        Me.P12file.Name = "P12file"
        Me.P12file.Size = New System.Drawing.Size(323, 20)
        Me.P12file.TabIndex = 254
        Me.P12file.Text = "Browse for Keystore file (.p12)..."
        Me.ToolTip1.SetToolTip(Me.P12file, "The name of your developer certificate file")
        '
        'BrowseP12
        '
        Me.BrowseP12.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.BrowseP12.Location = New System.Drawing.Point(259, 57)
        Me.BrowseP12.Name = "BrowseP12"
        Me.BrowseP12.Size = New System.Drawing.Size(69, 27)
        Me.BrowseP12.TabIndex = 243
        Me.BrowseP12.Text = "Browse"
        Me.BrowseP12.UseVisualStyleBackColor = True
        '
        'ToolTip1
        '
        Me.ToolTip1.AutoPopDelay = 5000
        Me.ToolTip1.InitialDelay = 1000
        Me.ToolTip1.ReshowDelay = 100
        '
        'DisplayEventLog
        '
        Me.DisplayEventLog.AutoSize = True
        Me.DisplayEventLog.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DisplayEventLog.Location = New System.Drawing.Point(395, 530)
        Me.DisplayEventLog.Name = "DisplayEventLog"
        Me.DisplayEventLog.Size = New System.Drawing.Size(124, 17)
        Me.DisplayEventLog.TabIndex = 274
        Me.DisplayEventLog.Text = "Display event log"
        Me.ToolTip1.SetToolTip(Me.DisplayEventLog, "Install .bar/.apk on drag/drop")
        Me.DisplayEventLog.UseVisualStyleBackColor = True
        '
        'ConvertedFile
        '
        Me.ConvertedFile.Location = New System.Drawing.Point(13, 65)
        Me.ConvertedFile.Name = "ConvertedFile"
        Me.ConvertedFile.Size = New System.Drawing.Size(10, 20)
        Me.ConvertedFile.TabIndex = 275
        Me.ToolTip1.SetToolTip(Me.ConvertedFile, "The name of your developer certificate file")
        Me.ConvertedFile.Visible = False
        '
        'InstallConverted
        '
        Me.InstallConverted.AutoSize = True
        Me.InstallConverted.Checked = Global.BBHTool.My.MySettings.Default.InstallConverted
        Me.InstallConverted.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.BBHTool.My.MySettings.Default, "InstallConverted", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.InstallConverted.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InstallConverted.Location = New System.Drawing.Point(215, 530)
        Me.InstallConverted.Name = "InstallConverted"
        Me.InstallConverted.Size = New System.Drawing.Size(155, 17)
        Me.InstallConverted.TabIndex = 275
        Me.InstallConverted.Text = "Install after completion"
        Me.ToolTip1.SetToolTip(Me.InstallConverted, "Install .bar/.apk on drag/drop")
        Me.InstallConverted.UseVisualStyleBackColor = True
        '
        'FilesGroupBox
        '
        Me.FilesGroupBox.Controls.Add(Me.CheckAllBarsApks)
        Me.FilesGroupBox.Controls.Add(Me.ClearAPKs)
        Me.FilesGroupBox.Controls.Add(Me.DragAPK)
        Me.FilesGroupBox.Controls.Add(Me.FileBox)
        Me.FilesGroupBox.Controls.Add(Me.Browse)
        Me.FilesGroupBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FilesGroupBox.Location = New System.Drawing.Point(374, 100)
        Me.FilesGroupBox.Name = "FilesGroupBox"
        Me.FilesGroupBox.Size = New System.Drawing.Size(399, 327)
        Me.FilesGroupBox.TabIndex = 271
        Me.FilesGroupBox.TabStop = False
        Me.FilesGroupBox.Text = "Files:"
        '
        'CheckAllBarsApks
        '
        Me.CheckAllBarsApks.AutoSize = True
        Me.CheckAllBarsApks.Location = New System.Drawing.Point(67, 0)
        Me.CheckAllBarsApks.Name = "CheckAllBarsApks"
        Me.CheckAllBarsApks.Size = New System.Drawing.Size(61, 13)
        Me.CheckAllBarsApks.TabIndex = 266
        Me.CheckAllBarsApks.TabStop = True
        Me.CheckAllBarsApks.Text = "Check All"
        '
        'ClearAPKs
        '
        Me.ClearAPKs.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ClearAPKs.Location = New System.Drawing.Point(212, 295)
        Me.ClearAPKs.Name = "ClearAPKs"
        Me.ClearAPKs.Size = New System.Drawing.Size(73, 27)
        Me.ClearAPKs.TabIndex = 263
        Me.ClearAPKs.Text = "Clear"
        Me.ClearAPKs.UseVisualStyleBackColor = True
        '
        'FileBox
        '
        Me.FileBox.AllowDrop = True
        Me.FileBox.CheckOnClick = True
        Me.FileBox.FormattingEnabled = True
        Me.FileBox.HorizontalExtent = 1250
        Me.FileBox.HorizontalScrollbar = True
        Me.FileBox.Location = New System.Drawing.Point(8, 16)
        Me.FileBox.Name = "FileBox"
        Me.FileBox.Size = New System.Drawing.Size(382, 274)
        Me.FileBox.TabIndex = 262
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'txtResults
        '
        Me.txtResults.Location = New System.Drawing.Point(13, 50)
        Me.txtResults.Name = "txtResults"
        Me.txtResults.Size = New System.Drawing.Size(10, 10)
        Me.txtResults.TabIndex = 273
        Me.txtResults.Text = ""
        Me.txtResults.Visible = False
        '
        'PlayBookProgressBar
        '
        Me.PlayBookProgressBar.Location = New System.Drawing.Point(31, 457)
        Me.PlayBookProgressBar.MarqueeAnimationSpeed = 0
        Me.PlayBookProgressBar.Name = "PlayBookProgressBar"
        Me.PlayBookProgressBar.Size = New System.Drawing.Size(705, 20)
        Me.PlayBookProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.PlayBookProgressBar.TabIndex = 268
        '
        'UnparsedInstallFilesBox
        '
        Me.UnparsedInstallFilesBox.Location = New System.Drawing.Point(13, 103)
        Me.UnparsedInstallFilesBox.Name = "UnparsedInstallFilesBox"
        Me.UnparsedInstallFilesBox.Size = New System.Drawing.Size(10, 10)
        Me.UnparsedInstallFilesBox.TabIndex = 276
        Me.UnparsedInstallFilesBox.Text = ""
        Me.UnparsedInstallFilesBox.Visible = False
        '
        'ParsedInstallFiles
        '
        Me.ParsedInstallFiles.Location = New System.Drawing.Point(13, 91)
        Me.ParsedInstallFiles.Name = "ParsedInstallFiles"
        Me.ParsedInstallFiles.Size = New System.Drawing.Size(10, 10)
        Me.ParsedInstallFiles.TabIndex = 277
        Me.ParsedInstallFiles.Text = ""
        Me.ParsedInstallFiles.Visible = False
        '
        'APKConverter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(804, 572)
        Me.Controls.Add(Me.ParsedInstallFiles)
        Me.Controls.Add(Me.UnparsedInstallFilesBox)
        Me.Controls.Add(Me.ConvertedFile)
        Me.Controls.Add(Me.InstallConverted)
        Me.Controls.Add(Me.PlayBookProgressBar)
        Me.Controls.Add(Me.DisplayEventLog)
        Me.Controls.Add(Me.txtResults)
        Me.Controls.Add(Me.FilesGroupBox)
        Me.Controls.Add(Me.SigningInfo)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.CAJHelp)
        Me.Controls.Add(Me.ConvertIt)
        Me.Controls.Add(Me.FileCount)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "APKConverter"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = ".APK Converter/Signer"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.SigningInfo.ResumeLayout(False)
        Me.SigningInfo.PerformLayout()
        Me.FilesGroupBox.ResumeLayout(False)
        Me.FilesGroupBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Browse As System.Windows.Forms.Button
    Friend WithEvents CAJHelp As System.Windows.Forms.LinkLabel
    Friend WithEvents BrowseSaveTo As System.Windows.Forms.Button
    Friend WithEvents SaveTo As System.Windows.Forms.TextBox
    Friend WithEvents CaJSaveTo As System.Windows.Forms.Label
    Friend WithEvents ConvertIt As System.Windows.Forms.Button
    Friend WithEvents DragAPK As System.Windows.Forms.Label
    Friend WithEvents FileCount As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents BrowseSDK As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents SDKFolder As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents SigningInfo As System.Windows.Forms.GroupBox
    Friend WithEvents KeyStorePass As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents P12file As System.Windows.Forms.TextBox
    Friend WithEvents BrowseP12 As System.Windows.Forms.Button
    Friend WithEvents CSKPass As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents FilesGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents FileBox As System.Windows.Forms.CheckedListBox
    Friend WithEvents txtResults As System.Windows.Forms.RichTextBox
    Friend WithEvents DisplayEventLog As System.Windows.Forms.CheckBox
    Friend WithEvents RememberConversionFolder As System.Windows.Forms.CheckBox
    Friend WithEvents RememberAndroidSDK As System.Windows.Forms.CheckBox
    Friend WithEvents ClearAPKs As System.Windows.Forms.Button
    Friend WithEvents SignBAR As System.Windows.Forms.CheckBox
    Friend WithEvents PlayBookProgressBar As System.Windows.Forms.ProgressBar
    Friend WithEvents RememberSigningInfo As System.Windows.Forms.CheckBox
    Friend WithEvents CheckAllBarsApks As System.Windows.Forms.LinkLabel
    Friend WithEvents GetKeys As System.Windows.Forms.LinkLabel
    Friend WithEvents DownloadSDK As System.Windows.Forms.LinkLabel
    Friend WithEvents InstallConverted As System.Windows.Forms.CheckBox
    Friend WithEvents ConvertedFile As System.Windows.Forms.TextBox
    Friend WithEvents UnparsedInstallFilesBox As System.Windows.Forms.RichTextBox
    Friend WithEvents ParsedInstallFiles As System.Windows.Forms.RichTextBox
End Class
