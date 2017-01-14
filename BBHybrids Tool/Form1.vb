Imports System
Imports System.IO
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Deployment
Imports System.ComponentModel
Imports System.Xml
Imports System.Net
Imports System.Threading
Imports IWshRuntimeLibrary

Public Class Form1
    Dim WithEvents WC As New WebClient
    Private m_DataSet As DataSet
    Private Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)
    Dim UpdateLink As String = "http://bit.ly/GetBBHToolAlt"
    Dim WelcomeText As String = "Welcome to BBHTool by lyricidal / @theiexplorers"
    Dim filelist As String
    Dim PlayBookInfo As String
    Dim a As NumberOnlyTextbox
    Dim DesktopFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
    Dim SystemDrive As String = Environ("SystemDrive") & "\"
    Public Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory)
        My.Settings.VersionNumber = "2.5 FINAL"
        'Pub release
        'BaHTab.Enabled = False
        If BaHTab.Enabled = False Then
            Dim UpdateLink As String = "http://bit.ly/GetBBHToolAlt"
            AfterCAJGroupBox.Hide()
            VIPOnly.Show()
            ShrinkAOSToolStripMenuItem.Checked = True
            If My.Settings.NewToApp = True Then
                Dim msg1 = "Are you new to BBH Tool? Click 'Yes' to be taken to the Help/How To Menu. Click 'No' if you already know what to do. Selections will be saved to avoid future start-up message boxes."
                Dim title1 = "Are you new to BBH Tool?"
                Dim style1 = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or _
                            MsgBoxStyle.Critical
                ' Display the message box and save the response, Yes or No.
                Dim response1 = MsgBox(msg1, style1, title1)
                If response1 = MsgBoxResult.Yes Then
                    My.Settings.NewToApp = True
                    HowTo.Show()
                    HowTo.TopMost = True
                Else
                    My.Settings.NewToApp = False
                End If
            Else
            End If
        End If
        'End Pub release
        Version.Text = "Version: " + My.Settings.VersionNumber
        a = New NumberOnlyTextbox(Me.VersionText, True, True, AddressOf M_OnKeyDown)
        Call SkinsMenu()
        Call AlwaysOnTop()
        Call RefreshOSFolders()
        OSFolder.Text = My.Settings.InstallDir
        OTASave.Text = My.Settings.OTADir
        OSFolderBox.SelectedItem = My.Settings.OSFolderBox
        DeviceComboBox.SelectedItem = "Choose Device"
        FileType.SelectedItem = "File Type"
        If ShrinkAOSToolStripMenuItem.Checked = True Then
            TabControl1.SelectedIndex = 1
        ElseIf CreateAJADToolStripMenuItem.Checked = True Then
            TabControl1.SelectedIndex = 2
        ElseIf OTADownloaderToolStripMenuItem.Checked = True Then
            TabControl1.SelectedIndex = 3
        ElseIf BuildAHybridToolStripMenuItem.Checked = True Then
            TabControl1.SelectedIndex = 4
        ElseIf InstallAHybridToolStripMenuItem.Checked = True Then
            TabControl1.SelectedIndex = 5
        ElseIf PhoneToolsToolStripMenuItem.Checked = True Then
            TabControl1.SelectedIndex = 6
        ElseIf PlayBookToolStripMenuItem.Checked = True Then
            TabControl1.SelectedIndex = 7
        Else
            TabControl1.SelectedIndex = 0
        End If
        If Save2Shrink.Checked = True Then
            SaveTo.Text = My.Settings.InstallDir
        Else
            SaveTo.Text = My.Settings.CaJDir
        End If
        If My.Settings.ShowOverTheTop = True Then
            Advanced.Show()
        Else
            Advanced.Hide()
        End If
        Call FindDeviceType()
        If CheckAtStartToolStripMenuItem.Checked = True Then
            If CheckForUpdates.IsBusy Then Exit Sub
            CheckForUpdates.RunWorkerAsync()
        End If
        If SavePlayBookIP.Checked = True Then
            PlayBookIP.Text = My.Settings.PlayBookIP
        End If
        If RememberPlayBookPass.Checked = True Then
            PlayBookPassword.Text = My.Settings.PlayBookPass
        End If
        If AutoAddBAR.Checked = True Then
            BarFolder.Text = My.Settings.BarFolder
            Call AddFiles()
        End If
        Call JavaCheck()
        If My.Computer.FileSystem.FileExists(DesktopFolder & "\BBHTool.lnk") Then
            CreateShortcut.Text = "Remove Desktop Shortcut"
        End If
    End Sub
    Sub RefreshOSFolders()
        OSFolderBox.Items.Clear()
        'Add OS Folders to list Start
        Dim fo As Object
        Dim fs As Object
        fs = CreateObject("Scripting.FileSystemObject")
        Call FindLoaderFolder()
        If My.Settings.LoaderDir = "" Then
        Else
            fo = fs.GetFolder(My.Settings.LoaderDir)
            For Each x In fo.SubFolders
                OSFolderBox.Items.Add(x.Name)
            Next
        End If
        OSFolderBox.Items.Add("Current OS Folder")
        'Add OS Folders to list End
    End Sub
    Sub FindLoaderFolder()
        If My.Settings.LoaderDir = "" Then
            If My.Computer.FileSystem.DirectoryExists("C:\Program Files (x86)\Common Files\Research In Motion\Shared\Loader Files") Then
                My.Settings.LoaderDir = "C:\Program Files (x86)\Common Files\Research In Motion\Shared\Loader Files\"
            ElseIf My.Computer.FileSystem.DirectoryExists("C:\Program Files\Common Files\Research In Motion\Shared\Loader Files") Then
                My.Settings.LoaderDir = "C:\Program Files\Common Files\Research In Motion\Shared\Loader Files\"
            ElseIf My.Computer.FileSystem.DirectoryExists("D:\Program Files (x86)\Common Files\Research In Motion\Shared\Loader Files") Then
                My.Settings.LoaderDir = "D:\Program Files (x86)\Common Files\Research In Motion\Shared\Loader Files\"
            ElseIf My.Computer.FileSystem.DirectoryExists("D:\Program Files\Common Files\Research In Motion\Shared\Loader Files") Then
                My.Settings.LoaderDir = "D:\Program Files\Common Files\Research In Motion\Shared\Loader Files\"
            ElseIf My.Computer.FileSystem.DirectoryExists("E:\Program Files (x86)\Common Files\Research In Motion\Shared\Loader Files") Then
                My.Settings.LoaderDir = "E:\Program Files (x86)\Common Files\Research In Motion\Shared\Loader Files\"
            ElseIf My.Computer.FileSystem.DirectoryExists("E:\Program Files\Common Files\Research In Motion\Shared\Loader Files") Then
                My.Settings.LoaderDir = "E:\Program Files\Common Files\Research In Motion\Shared\Loader Files\"
            Else
                My.Settings.LoaderDir = ""
            End If
        Else
        End If
    End Sub
    Private Sub Form1_Closing(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.FormClosing
        If ChangesLog.Visible Then
            ChangesLog.Close()
        End If
        If HowTo.Visible Then
            HowTo.Close()
        End If
        If SavePlayBookIP.Checked = True Then
            My.Settings.PlayBookIP = PlayBookIP.Text
        End If
    End Sub
    Sub FindDeviceType()
        If OSFolder.Text Is Nothing Then
        Else
            If OSFolder.Text.Contains(9930) Then
                DeviceComboBox.SelectedItem = "9930 (Bold)"
                OTADevice.Text = "9930"
            ElseIf OSFolder.Text.Contains(9900) Then
                DeviceComboBox.SelectedItem = "9900 (Bold)"
                OTADevice.Text = "9900"
            ElseIf OSFolder.Text.Contains(9981) Then
                DeviceComboBox.SelectedItem = "P'9981 (Porsche)"
                OTADevice.Text = "9981"
            ElseIf OSFolder.Text.Contains(9860) Then
                DeviceComboBox.SelectedItem = "9860 (Torch)"
                OTADevice.Text = "9860"
            ElseIf OSFolder.Text.Contains(9850) Then
                DeviceComboBox.SelectedItem = "9850 (Torch)"
                OTADevice.Text = "9850"
            ElseIf OSFolder.Text.Contains(9810) Then
                DeviceComboBox.SelectedItem = "9810 (Torch)"
                OTADevice.Text = "9810"
            ElseIf OSFolder.Text.Contains(9800) Then
                DeviceComboBox.SelectedItem = "9800 (Torch)"
                OTADevice.Text = "9800"
            ElseIf OSFolder.Text.Contains(9790) Then
                DeviceComboBox.SelectedItem = "9790 (Bold)"
                OTADevice.Text = "9790"
            ElseIf OSFolder.Text.Contains(9780) Then
                DeviceComboBox.SelectedItem = "9780 (Bold)"
                OTADevice.Text = "9780"
            ElseIf OSFolder.Text.Contains(9700) Then
                DeviceComboBox.SelectedItem = "9700 (Bold)"
                OTADevice.Text = "9700"
            ElseIf OSFolder.Text.Contains(9670) Then
                DeviceComboBox.SelectedItem = "9670 (Style)"
                OTADevice.Text = "9670"
            ElseIf OSFolder.Text.Contains(9650) Then
                DeviceComboBox.SelectedItem = "9650 (Bold)"
                OTADevice.Text = "9650"
            ElseIf OSFolder.Text.Contains(9630) Then
                DeviceComboBox.SelectedItem = "9630 (Tour)"
                OTADevice.Text = "9630"
            ElseIf OSFolder.Text.Contains(9550) Then
                DeviceComboBox.SelectedItem = "9550 (Storm 2)"
                OTADevice.Text = "9550"
            ElseIf OSFolder.Text.Contains(9530) Then
                DeviceComboBox.SelectedItem = "9530 (Storm)"
                OTADevice.Text = "9530"
            ElseIf OSFolder.Text.Contains(9520) Then
                DeviceComboBox.SelectedItem = "9520 (Storm 2)"
                OTADevice.Text = "9520"
            ElseIf OSFolder.Text.Contains(9500) Then
                DeviceComboBox.SelectedItem = "9500 (Storm)"
                OTADevice.Text = "9500"
            ElseIf OSFolder.Text.Contains(9380) Then
                DeviceComboBox.SelectedItem = "9380 (Curve)"
                OTADevice.Text = "9380"
            ElseIf OSFolder.Text.Contains(9370) Then
                DeviceComboBox.SelectedItem = "9370 (Curve)"
                OTADevice.Text = "9370"
            ElseIf OSFolder.Text.Contains(9360) Then
                DeviceComboBox.SelectedItem = "9360 (Curve)"
                OTADevice.Text = "9360"
            ElseIf OSFolder.Text.Contains(9350) Then
                DeviceComboBox.SelectedItem = "9350 (Curve)"
                OTADevice.Text = "9350"
            ElseIf OSFolder.Text.Contains(9330) Then
                DeviceComboBox.SelectedItem = "9330 (Curve 3G)"
                OTADevice.Text = "9330"
            ElseIf OSFolder.Text.Contains(9320) Then
                DeviceComboBox.SelectedItem = "9320 (Curve)"
                OTADevice.Text = "9320"
            ElseIf OSFolder.Text.Contains(9310) Then
                DeviceComboBox.SelectedItem = "9310 (Curve)"
                OTADevice.Text = "9310"
            ElseIf OSFolder.Text.Contains(9300) Then
                DeviceComboBox.SelectedItem = "9300 (Curve 3G)"
                OTADevice.Text = "9300"
            ElseIf OSFolder.Text.Contains(9220) Then
                DeviceComboBox.SelectedItem = "9220 (Curve)"
                OTADevice.Text = "9220"
            ElseIf OSFolder.Text.Contains(9100) Then
                DeviceComboBox.SelectedItem = "9100/9105 (Pearl 3G)"
            ElseIf OSFolder.Text.Contains(9105) Then
                DeviceComboBox.SelectedItem = "9100/9105 (Pearl 3G)"
            ElseIf OSFolder.Text.Contains(9000) Then
                DeviceComboBox.SelectedItem = "9000 (Bold)"
            ElseIf OSFolder.Text.Contains(8900) Then
                DeviceComboBox.SelectedItem = "8900 (Curve)"
            ElseIf OSFolder.Text.Contains(8520) Then
                DeviceComboBox.SelectedItem = "8520 (Gemini)"
            ElseIf OSFolder.Text.Contains(8330) Then
                DeviceComboBox.SelectedItem = "8330 (Curve)"
            ElseIf OSFolder.Text.Contains(8130) Then
                DeviceComboBox.Items.Add("8130 (Pearl)")
                DeviceComboBox.SelectedItem = "8130 (Pearl)"
            ElseIf OSFolder.Text.Contains(8120) Then
                DeviceComboBox.Items.Add("8120 (Pearl)")
                DeviceComboBox.SelectedItem = "8120 (Pearl)"
            ElseIf OSFolder.Text.Contains(8110) Then
                DeviceComboBox.Items.Add("8110 (Pearl)")
                DeviceComboBox.SelectedItem = "8110 (Pearl)"
            ElseIf OSFolder.Text.Contains(8100) Then
                DeviceComboBox.Items.Add("8100 (Pearl)")
                DeviceComboBox.SelectedItem = "8100 (Pearl)"
            End If
        End If
    End Sub
    Public Sub Browse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Browse.Click
        ToolStripStatusLabel1.Text = "Browsing... (Select the install folder of your OS)"
        If My.Computer.FileSystem.DirectoryExists("C:\Program Files\Common Files\Research In Motion\Shared\Loader Files\") Then
            With FolderBrowserDialog1
                .SelectedPath = "C:\Program Files\Common Files\Research In Motion\Shared\Loader Files"
                .ShowNewFolderButton = False
                .Description = "Navigate to your current OS Install folder (Not the Java or CDMA):"
            End With
        Else
            With FolderBrowserDialog1
                .SelectedPath = "C:\Program Files (x86)\Common Files\Research In Motion\Shared\Loader Files"
                .ShowNewFolderButton = False
                .Description = "Navigate to your current OS Install folder (Not the Java or CDMA):"
            End With
        End If
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            OSFolder.Text = FolderBrowserDialog1.SelectedPath & "\"
            My.Settings.InstallDir = FolderBrowserDialog1.SelectedPath & "\"
            GetFileCount("Before")
        End If
        My.Settings.Save()
        ToolStripStatusLabel1.Text = "OS Folder selected. Shrink Away!"
    End Sub
    'Shrink-A-OS Start'
    Protected Sub RemoveLangs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RemoveLangs.Click
        If RemoveLangs.Checked = True Then
            Call RemoveAllLangs(True)
        ElseIf RemoveLangs.Checked = False Then
            Call RemoveAllLangs(False)
        End If
    End Sub
    Sub RemoveAllLangs(ByVal Variable As Boolean)
        For Each ctrl As Control In Me.LangGroupBox.Controls
            If TypeOf ctrl Is CheckBox Then
                DirectCast(ctrl, CheckBox).Checked = Variable
            End If
        Next
        For Each ctrl As Control In Me.EngGroupBox.Controls
            If TypeOf ctrl Is CheckBox Then
                DirectCast(ctrl, CheckBox).Checked = Variable
            End If
        Next
    End Sub
    Public Sub RemoveGames_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveGames.Click
        If RemoveGames.Checked = True Then
            Call RemoveAllGames(True)
        End If
        If RemoveGames.Checked = False Then
            Call RemoveAllGames(False)
        End If
    End Sub
    Sub RemoveAllGames(ByVal Variable As Boolean)
        For Each ctrl As Control In Me.GamesGroupBox.Controls
            If TypeOf ctrl Is CheckBox Then
                DirectCast(ctrl, CheckBox).Checked = Variable
            End If
        Next
    End Sub
    Public Sub RemoveBlackBerry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveBlackBerry.Click
        If RemoveBlackBerry.Checked = True Then
            Call RemoveAllBlackBerry(True)
        End If
        If RemoveBlackBerry.Checked = False Then
            Call RemoveAllBlackBerry(False)
        End If
    End Sub
    Sub RemoveAllBlackBerry(ByVal Variable As Boolean)
        For Each ctrl As Control In Me.BBBox1.Controls
            If TypeOf ctrl Is CheckBox Then
                DirectCast(ctrl, CheckBox).Checked = Variable
            End If
        Next
    End Sub


    Protected Sub RemoveDocsToGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RemoveDocsToGo.Click
        If RemoveDocsToGo.Checked = True Then
            Call RemoveDocs2Go(True)
        End If
        If RemoveDocsToGo.Checked = False Then
            Call RemoveDocs2Go(False)
        End If
    End Sub
    Sub RemoveDocs2Go(ByVal Variable As Boolean)
        RemoveDocsToGo.Checked = Variable
        PDFToGo.Checked = Variable
        SheetToGo.Checked = Variable
        SlideshowsToGo.Checked = Variable
        WordToGo.Checked = Variable
    End Sub
    Protected Sub RemoveIM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RemoveIM.Click
        If RemoveIM.Checked = True Then
            Call RemoveIMs(True)
        End If
        If RemoveIM.Checked = False Then
            Call RemoveIMs(False)
        End If
    End Sub
    Sub RemoveIMs(ByVal Variable As Boolean)
        For Each ctrl As CheckBox In IMGroupBox.Controls
            ctrl.Checked = Variable
        Next
    End Sub
    Protected Sub RemoveFonts_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RemoveFonts.Click
        If RemoveFonts.Checked = True Then
            Call RemoveAllFonts(True)
        End If
        If RemoveFonts.Checked = False Then
            Call RemoveAllFonts(False)
        End If
    End Sub
    Sub RemoveAllFonts(ByVal Variable As Boolean)
        For Each ctrl As CheckBox In DefFontsBox.Controls
            ctrl.Checked = Variable
        Next
        If Variable = False Then
            Latin.Checked = Variable
        Else
            '//Latin
            Dim msg = "This is the default BlackBerry font. Removal may result in undesired effecs. Are you sure you would like to remove the Latin Truetype font?"
            Dim title = "Remove Default Font?"
            Dim style = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or _
                        MsgBoxStyle.Critical
            ' Display the message box and save the response, Yes or No.
            Dim response = MsgBox(msg, style, title)
            If response = MsgBoxResult.Yes Then
                Latin.Checked = True
                ToolStripStatusLabel1.Text = "Latin Truetype font has been selected."
            Else
                Latin.Checked = False
                ToolStripStatusLabel1.Text = "Latin Truetype font has not been selected."
            End If
        End If

    End Sub
    Protected Sub RemoveMedia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RemoveMedia.Click
        If RemoveMedia.Checked = True Then
            Call RemoveDefault(True)
        End If
        If RemoveMedia.Checked = False Then
            Call RemoveDefault(False)
        End If
    End Sub
    Sub RemoveDefault(ByVal Variable As Boolean)
        For Each ctrl As CheckBox In DefaultsBox.Controls
            ctrl.Checked = Variable
        Next
        If Variable = False Then
            Ringtones.Checked = Variable
        Else
            '//Ringtones
            Dim msg1 = "Would you also like to remove the Default Ringtones?"
            Dim title1 = "Remove Default Ringtones?"
            Dim style1 = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or _
                        MsgBoxStyle.Critical
            ' Display the message box and save the response, Yes or No.
            Dim response1 = MsgBox(msg1, style1, title1)
            If response1 = MsgBoxResult.Yes Then
                Ringtones.Checked = True
                ToolStripStatusLabel1.Text = "All options have been selected."
            Else
                Ringtones.Checked = False
                ToolStripStatusLabel1.Text = "All options have been selected except for the what you've said no to."
            End If
        End If
    End Sub
    Protected Sub RemoveAllApps_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RemoveAllApps.Click
        If RemoveAllApps.Checked = True Then
            Call RemoveApps(True)
        ElseIf RemoveAllApps.Checked = False Then
            Call RemoveApps(False)
        End If
    End Sub
    Sub RemoveApps(ByVal Variable As Boolean)
        For Each ctrl As CheckBox In AppGroupBox.Controls
            ctrl.Checked = Variable
            '//MsgBox(ctrl.Name.ToString() & "=" & ctrl.Checked.ToString())
        Next
    End Sub
    Protected Sub SelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SelectAll.Click
        '//Apps
        Call RemoveApps(True)
        '//Docs2Go
        Call RemoveDocs2Go(True)
        '//BlackBerry Apps
        Call RemoveAllBlackBerry(True)
        '//Defaults
        Call RemoveDefault(True)
        '//Fonts
        Call RemoveAllFonts(True)
        '//Games
        Call RemoveAllGames(True)
        '//IMs
        Call RemoveIMs(True)
        '//Langs
        Call RemoveAllLangs(True)
        ToolStripStatusLabel1.Text = "All options have been selected."
    End Sub
    Protected Sub SelectNone_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SelectNone.Click
        '//Apps
        Call RemoveApps(False)
        '//Docs2Go
        Call RemoveDocs2Go(False)
        '//BlackBerry Apps
        Call RemoveAllBlackBerry(False)
        '//Defaults
        Call RemoveDefault(False)
        '//Fonts
        Call RemoveAllFonts(False)
        '//Games
        Call RemoveAllGames(False)
        '//IMs
        Call RemoveIMs(False)
        '//Langs
        Call RemoveAllLangs(False)
        ToolStripStatusLabel1.Text = "All options have been deselected."
    End Sub
    Public Sub Shrink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Shrink.Click
        Call ShrinkInitiate()
    End Sub
    Sub ShrinkInitiate()
        If String.IsNullOrEmpty(OSFolder.Text) Then
            MsgBox("You must place this .exe in your OS's Install folder and check the box below Browse. Otherwise, you must click Browse and choose the OS folder you would like Shrunk.")
            ToolStripStatusLabel1.Text = "You must place this .exe in your OS's Install folder and check the box below Browse. Otherwise, you must click Browse and choose the OS folder you would like Shrunk."
        Else
            If ShrinkOSWorker.IsBusy Then Exit Sub
            ShrinkOSWorker.RunWorkerAsync()
            Shrink.Enabled = False
            UseWaitCursor = True
        End If
    End Sub
    Sub MovingFiles(ByVal SearchString As String, ByVal Command As String)
        Dim Install As String = My.Settings.InstallDir
        If OSFolder.Text.EndsWith("\") Then
        Else
            OSFolder.Text = OSFolder.Text & "\"
        End If
        GetFileCount("Before")
        If Command = "Shrink" Then
            If My.Computer.FileSystem.DirectoryExists(Install & "\Java\RemovedFiles\") Then
            Else
                My.Computer.FileSystem.CreateDirectory(Install & "\Java\RemovedFiles\")
            End If

            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Install & "\Java\", Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, SearchString)
                Dim foundFileInfo As New System.IO.FileInfo(foundFile)
                If SeperateALXFilesCODFiles.Checked = True Then
                    My.Computer.FileSystem.MoveFile(foundFile, Install & "\Java\RemovedFiles\COD\" & foundFileInfo.Name, True)
                Else
                    My.Computer.FileSystem.MoveFile(foundFile, Install & "\Java\RemovedFiles\" & foundFileInfo.Name, True)
                End If
            Next
        ElseIf Command = "ShrinkALX" Then
            If My.Computer.FileSystem.DirectoryExists(Install & "\Java\RemovedFiles\") Then
            Else
                My.Computer.FileSystem.CreateDirectory(Install & "\Java\RemovedFiles\")
            End If
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Install & "\", Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, SearchString)
                Dim foundFileInfo As New System.IO.FileInfo(foundFile)
                If SeperateALXFilesCODFiles.Checked = True Then
                    My.Computer.FileSystem.MoveFile(foundFile, Install & "\Java\RemovedFiles\ALX\" & foundFileInfo.Name, True)
                Else
                    My.Computer.FileSystem.MoveFile(foundFile, Install & "\Java\RemovedFiles\" & foundFileInfo.Name, True)
                End If
            Next
        ElseIf Command = "BB10Shrink" Then
            If My.Computer.FileSystem.DirectoryExists(Install & "\RemovedFiles\") Then
            Else
                My.Computer.FileSystem.CreateDirectory(Install & "\RemovedFiles\")
            End If
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Install & "\", Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, SearchString)
                Dim foundFileInfo As New System.IO.FileInfo(foundFile)
                My.Computer.FileSystem.MoveFile(foundFile, Install & "\RemovedFiles\" & foundFileInfo.Name, True)
            Next

        ElseIf Command = "PutBack" Then
            If SeperateALXFilesCODFiles.Checked = True Then
                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Install & "\Java\RemovedFiles\COD\", Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, SearchString)
                    Dim foundFileInfo As New System.IO.FileInfo(foundFile)
                    My.Computer.FileSystem.MoveFile(foundFile, Install & "\Java\" & foundFileInfo.Name, True)
                Next
            Else
                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Install & "\Java\RemovedFiles\", Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, SearchString)
                    Dim foundFileInfo As New System.IO.FileInfo(foundFile)
                    My.Computer.FileSystem.MoveFile(foundFile, Install & "\Java\" & foundFileInfo.Name, True)
                Next
            End If
        ElseIf Command = "PutBackALX" Then
            If SeperateALXFilesCODFiles.Checked = True Then
                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Install & "\Java\RemovedFiles\ALX\", Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, SearchString)
                    Dim foundFileInfo As New System.IO.FileInfo(foundFile)
                    My.Computer.FileSystem.MoveFile(foundFile, Install & foundFileInfo.Name, True)
                Next
            Else
                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Install & "\Java\RemovedFiles\", Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, SearchString)
                    Dim foundFileInfo As New System.IO.FileInfo(foundFile)
                    My.Computer.FileSystem.MoveFile(foundFile, Install & foundFileInfo.Name, True)
                Next
            End If
        ElseIf Command = "PutBackBB10" Then
            If SeperateALXFilesCODFiles.Checked = True Then
                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Install & "\RemovedFiles\", Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, SearchString)
                    Dim foundFileInfo As New System.IO.FileInfo(foundFile)
                    My.Computer.FileSystem.MoveFile(foundFile, Install & foundFileInfo.Name, True)
                Next
            End If
        ElseIf Command = "LiveShrink" Then
            For Each S As String In JavaLoaderBox.Items
                If S.Contains(SearchString) Then
                    If My.Settings.CodList.Contains(S) Then
                    Else
                        MsgBox(S)
                        My.Settings.CodList = S & " " & My.Settings.CodList
                    End If
                End If
            Next
        End If
    End Sub

    Public Sub ShrinkOSWorker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles ShrinkOSWorker.DoWork
        If OSFolder.Text.EndsWith("\") Then
        Else
            OSFolder.Text = OSFolder.Text & "\"
        End If
        Dim Install As String = My.Settings.InstallDir
        If My.Computer.FileSystem.DirectoryExists(Install & "\Java\RemovedFiles\") Then
        Else
            My.Computer.FileSystem.CreateDirectory(Install & "\Java\RemovedFiles\")
        End If
        ToolStripStatusLabel1.Text = "Shrinking your OS..."
        ' start the progress bar doing its thing:
        ShrinkProgressBar.MarqueeAnimationSpeed = 80
        'Fonts Start
        If Arial.CheckState = 1 Then
            'ToolStripStatusLabel1.Text = "Shrinking Arial Font..."
            Call MovingFiles("net_rim_font_arial*", "Shrink")
        End If
        If ArabicFont.CheckState = 1 Then
            'ToolStripStatusLabel1.Text = "Shrinking Arabic Font..."
            Call MovingFiles("net_rim_font_arabic*", "Shrink")
        End If
        If Courier.CheckState = 1 Then
            'ToolStripStatusLabel1.Text = "Shrinking Courier Font..."
            Call MovingFiles("net_rim_font_courier*", "Shrink")
        End If
        If Emoji.CheckState = 1 Then
            'ToolStripStatusLabel1.Text = "Shrinking Emoji Font..."
            Call MovingFiles("net_rim_font_emoji*", "Shrink")
        End If
        If European.CheckState = 1 Then
            'ToolStripStatusLabel1.Text = "Shrinking European Font..."
            Call MovingFiles("net_rim_font_european*", "Shrink")
        End If
        If Georgia.CheckState = 1 Then
            'ToolStripStatusLabel1.Text = "Shrinking Georgia Font..."
            Call MovingFiles("net_rim_font_georgia*", "Shrink")
        End If
        If GlobalFont.CheckState = 1 Then
            'ToolStripStatusLabel1.Text = "Shrinking Global Font..."
            Call MovingFiles("net_rim_font_global*", "Shrink")
        End If
        If Indic.CheckState = 1 Then
            'ToolStripStatusLabel1.Text = "Shrinking Indic Font..."
            Call MovingFiles("net_rim_font_indic*", "Shrink")
        End If
        If JapaneseFont.CheckState = 1 Then
            'ToolStripStatusLabel1.Text = "Shrinking Japanese Font..."
            Call MovingFiles("net_rim_font_japanese*", "Shrink")
        End If
        If Latin.CheckState = 1 Then
            If OSFolderBox.SelectedItem.ToString.Contains("v7.0.0") Then
            ElseIf OSFolderBox.SelectedItem.ToString.Contains("v7.1.0") Then
            Else
                'ToolStripStatusLabel1.Text = "Shrinking Latin Font..."
                Call MovingFiles("net_rim_font_latin*", "Shrink")
            End If
        End If
        If Misc.CheckState = 1 Then
            'ToolStripStatusLabel1.Text = "Shrinking Misc Font..."
            Call MovingFiles("net_rim_font_misc*", "Shrink")
        End If
        If Monotype.CheckState = 1 Then
            'ToolStripStatusLabel1.Text = "Shrinking Monotype Font..."
            Call MovingFiles("net_rim_font_monotype*", "Shrink")
        End If
        If Tahoma.CheckState = 1 Then
            'ToolStripStatusLabel1.Text = "Shrinking Tahoma Font..."
            Call MovingFiles("net_rim_font_tahoma*", "Shrink")
        End If
        If ThaiFont.CheckState = 1 Then
            'ToolStripStatusLabel1.Text = "Shrinking Thai Font..."
            Call MovingFiles("net_rim_font_thai*", "Shrink")
        End If
        If Times.CheckState = 1 Then
            'ToolStripStatusLabel1.Text = "Shrinking Times Font..."
            Call MovingFiles("net_rim_font_times*", "Shrink")
        End If
        If Trebuchet.CheckState = 1 Then
            'ToolStripStatusLabel1.Text = "Shrinking Trebuchet Font..."
            Call MovingFiles("net_rim_font_trebuchet*", "Shrink")
        End If
        If Verdana.CheckState = 1 Then
            'ToolStripStatusLabel1.Text = "Shrinking Verdana Font..."
            Call MovingFiles("net_rim_font_verdana*", "Shrink")
        End If
        'Fonts End
        'OTT Start
        If OTTCamera.CheckState = 1 Then
            'ToolStripStatusLabel1.Text = "Shrinking Camera..."
            Call MovingFiles("net_rim_bb_camera*", "Shrink")
        End If
        If OTTBluetooth.CheckState = 1 Then
            'ToolStripStatusLabel1.Text = "Shrinking Bluetooth..."
            Call MovingFiles("*bluetooth*", "Shrink")
            Call MovingFiles("net_rim_bb_BT*", "Shrink")
        End If
        If OTTVideoCamera.CheckState = 1 Then
            'ToolStripStatusLabel1.Text = "Shrinking Video Camera..."
            Call MovingFiles("net_rim_bb_videorecorder*", "Shrink")
        End If
        If AccessOptions.CheckState = 1 Then
            'ToolStripStatusLabel1.Text = "Shrinking Accessibility Options..."
            Call MovingFiles("net_rim_bb_medialoader_accessibility.cod", "Shrink")
        End If
        If DefaultDMTree.CheckState = 1 Then
            'ToolStripStatusLabel1.Text = "Shrinking Default DM Tree..."
            Call MovingFiles("net_rim_bb_default102DMtree*", "Shrink")
        End If
        If eScreen.CheckState = 1 Then
            'ToolStripStatusLabel1.Text = "Shrinking eScreen..."
            Call MovingFiles("net_rim_escreen_app.cod", "Shrink")
        End If
        'OTT End
        'Apps Start
        If ATTHotSpot.CheckState = 1 Then
            Call MovingFiles("net_rim_hotspotclient_att*", "Shrink")
        End If
        If DoDCerts.CheckState = 1 Then
            Call MovingFiles("net_rim_DoDRootCerts*", "Shrink")
        End If
        If ITPolicyViewer.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_itpolicyviewer*", "Shrink")
        End If
        If MemoPad.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_memo_app*", "Shrink")
            Call MovingFiles("MemoPad-Java.alx", "ShrinkALX")
        End If
        If Slacker.CheckState = 1 Then
            Call MovingFiles("SlackerRadio*.cod", "Shrink")
            Call MovingFiles("SlackerRadio*.alx", "ShrinkALX")
            Call MovingFiles("slacker_radio*.cod", "Shrink")
        End If
        If Tasks.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_task_app*", "Shrink")
            Call MovingFiles("Tasks-Java.alx", "ShrinkALX")
        End If
        If SuretypeT12.CheckState = 1 Then
            Call MovingFiles("net_rim_tid_suretypeT12*", "Shrink")
        End If
        If ARM.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_application_monitor*.cod", "Shrink")
            Call MovingFiles("net_rim_bb_application_monitor.alx", "ShrinkALX")
        End If
        If AppWorld.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_appworld*.cod", "Shrink")
            Call MovingFiles("net_rim_bb_appworld.alx", "ShrinkALX")
        End If
        If DeviceAnalyzer.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_device_analyzer*.cod", "Shrink")
            Call MovingFiles("net_rim_bb_device_analyzer.alx", "ShrinkALX")
        End If
        If News.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_news*.cod", "Shrink")
            Call MovingFiles("net_rim_bb_news*.alx", "ShrinkALX")
        End If
        If Plans.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_plans*", "Shrink")
            'Call MovingFiles("net_rim_bb_api_catalyst_serviceplanengine.cod", "Shrink")
            Call MovingFiles("net_rim_bb_plans.alx*", "ShrinkALX")
        End If
        If BBProtect.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_dryad*.cod", "Shrink")
            Call MovingFiles("net_rim_bbcommons_dryad.cod", "Shrink")
            Call MovingFiles("net_rim_bb_dryad.alx", "ShrinkALX")
        End If
        If BBRadio.CheckState = 1 Then
            Call MovingFiles("CorusAdapter.cod", "Shrink")
            Call MovingFiles("ClearChannelAdapter.cod", "Shrink")
            Call MovingFiles("IRadioStreamProvider.cod", "Shrink")
            Call MovingFiles("PlayerLibrary.cod", "Shrink")
            Call MovingFiles("net_rim_bb_radio_dial.cod", "Shrink")
            Call MovingFiles("SlackerAdapter.cod", "Shrink")
            Call MovingFiles("net_rim_bb_radio_dial.alx", "ShrinkALX")
        End If
        If Travel.CheckState = 1 Then
            Call MovingFiles("BlackBerryTravel.alx", "ShrinkALX")
        End If
        If Backup.CheckState = 1 Then
            Call MovingFiles("BackupAssistant*.alx", "ShrinkALX")
            Call MovingFiles("BackupAssistant*.cod", "Shrink")
        End If
        If BBMMusic.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_bbcm*.cod", "Shrink")
            Call MovingFiles("net_rim_bb_bbcm.alx", "ShrinkALX")
        End If
        If Movitalk.CheckState = 1 Then
            Call MovingFiles("KnJCDE.cod", "Shrink")
            Call MovingFiles("net_rim_bb_phone_ptt_app.cod", "Shrink")
            Call MovingFiles("KodiakPTT*.alx", "ShrinkALX")
        End If
        If YouTube.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_youtube*.cod", "Shrink")
            Call MovingFiles("net_rim_bb_youtube*.alx", "ShrinkALX")
        End If
        If Podcasts.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_podcasts*.cod", "Shrink")
            Call MovingFiles("net_rim_bb_podcasts*.alx", "ShrinkALX")
        End If
        If VZWNameID.CheckState = 1 Then
            Call MovingFiles("BBCequintVerizon.alx", "ShrinkALX")
            Call MovingFiles("CequintAPI.cod", "Shrink")
            Call MovingFiles("cequint_vzw_bold9930_*.cod", "Shrink")
            Call MovingFiles("cequint_vzw_torch9850_*.cod", "Shrink")
            Call MovingFiles("net_rim_bb_phone_cequint_proxy.cod", "Shrink")
            Call MovingFiles("BBCequint.cod", "Shrink")
        End If
        If VZWPTT.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_phone_vz_ptt*", "Shrink")
            Call MovingFiles("MPTT.cod", "Shrink")
            Call MovingFiles("net_rim_bb_medialoader_ringtones_verizon_alert_tones.cod", "Shrink")
            Call MovingFiles("VerizonPTT.alx", "ShrinkALX")
        End If
        If TmoError.CheckState = 1 Then
            Call MovingFiles("net_rim_errortranslator_tmobile*", "Shrink")
        End If
        If MobileBackup.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_nabsync*", "Shrink")
        End If
        If TMONameID.CheckState = 1 Then
            Call MovingFiles("cequint_tmo_*.cod", "Shrink")
            Call MovingFiles("net_rim_bb_phone_cequint_proxy*.cod", "Shrink")
            Call MovingFiles("BBCequint*", "Shrink")
            Call MovingFiles("CequintAPI*", "Shrink")
            Call MovingFiles("BBCequintTMO.alx", "ShrinkALX")
        End If
        If TeleNav.CheckState = 1 Then
            Call MovingFiles("TeleNav*.cod", "Shrink")
            Call MovingFiles("TeleNav*.alx", "ShrinkALX")
        End If
        If LunarCalendar.CheckState = 1 Then
            Call MovingFiles("LunarCalendar*.cod", "Shrink")
            Call MovingFiles("LunarCalendar*.alx", "ShrinkALX")
        End If
        If MobileReader.CheckState = 1 Then
            Call MovingFiles("CMRead_v60*.cod", "Shrink")
            Call MovingFiles("CMRead_v60*.alx", "ShrinkALX")
        End If
        If MobileMarket.CheckState = 1 Then
            Call MovingFiles("MobileMarket*.cod", "Shrink")
            Call MovingFiles("MobileMarket*.alx", "ShrinkALX")
        End If
        If MusicCN.CheckState = 1 Then
            Call MovingFiles("Music*.cod", "Shrink")
            Call MovingFiles("Music*.alx", "ShrinkALX")
        End If
        If MyFaves.CheckState = 1 Then
            Call MovingFiles("net_rim_tmo_five.cod", "Shrink")
            Call MovingFiles("*five_icon_library.cod", "Shrink")
        End If

        If Stock.CheckState = 1 Then
            Call MovingFiles("MobileStock*.cod", "Shrink")
            Call MovingFiles("MobileStock*.alx", "ShrinkALX")
        End If
        If Fetion.CheckState = 1 Then
            Call MovingFiles("Fetion*.cod", "Shrink")
            Call MovingFiles("Fetion*.alx", "ShrinkALX")

        End If
        If Music.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_medialoader_music_*", "Shrink")
        End If
        If Videos.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_medialoader_toolkit*", "Shrink")
            Call MovingFiles("net_rim_bb_medialoader_video*", "Shrink")
            Call MovingFiles("net_rim_bb_mediaOTA_toolkit*", "Shrink")
        End If
        If VVM.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_vvm*.cod", "Shrink")
            Call MovingFiles("net_rim_bb_mbp*", "Shrink")
            Call MovingFiles("net_rim_bb_vvm.alx", "ShrinkALX")
        End If
        If Romanian.CheckState = 1 Then
            Call MovingFiles("*_ro.cod", "Shrink")
            Call MovingFiles("*romanian*.cod", "Shrink")
        End If
        If Norwegian.CheckState = 1 Then
            Call MovingFiles("*_no.cod", "Shrink")
            Call MovingFiles("*norwegian*.cod", "Shrink")
        End If
        If Swedish.CheckState = 1 Then
            Call MovingFiles("*_sv.cod", "Shrink")
            Call MovingFiles("*swedish*.cod", "Shrink")
        End If
        If Polish.CheckState = 1 Then
            Call MovingFiles("*_pl.cod", "Shrink")
            Call MovingFiles("*polish*", "Shrink")
        End If
        If Thai.CheckState = 1 Then
            Call MovingFiles("*_th.cod", "Shrink")
            Call MovingFiles("*thai*.cod", "Shrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_TIS620.cod", "Shrink")
        End If
        If Korean.CheckState = 1 Then
            Call MovingFiles("*_ko.cod", "Shrink")
            Call MovingFiles("*korean*.cod", "Shrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_EUC_KR.cod", "Shrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_x_Johab.cod", "Shrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_KSC5601.cod", "Shrink")
        End If
        If German.CheckState = 1 Then
            Call MovingFiles("*german*.cod", "Shrink")
            Call MovingFiles("*_de.cod", "Shrink")
        End If
        If Greek.CheckState = 1 Then
            Call MovingFiles("*greek*.cod", "Shrink")
            Call MovingFiles("*_el.cod", "Shrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_CP1253.cod", "Shrink")
        End If
        If Hebrew.CheckState = 1 Then
            Call MovingFiles("*hebrew*.cod", "Shrink")
            Call MovingFiles("*_he_*.cod", "Shrink")
            Call MovingFiles("*_he.cod", "Shrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_CP1255.cod", "Shrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_CP1256.cod", "Shrink")
        End If
        If Hindi.CheckState = 1 Then
            Call MovingFiles("*_hi.cod", "Shrink")
            Call MovingFiles("net_rim_tid_indic.cod", "Shrink")
        End If
        If Chinese.CheckState = 1 Then
            Call MovingFiles("net_rim_tid_dynamic_ling_data_chinese*.cod", "Shrink")
            Call MovingFiles("net_rim_tid_chinese*.cod", "Shrink")
            Call MovingFiles("*pinyin*.cod", "Shrink")
            Call MovingFiles("*_zh.cod", "Shrink")
            Call MovingFiles("*_zh_*.cod", "Shrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_CP1255.cod", "Shrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_CP1256.cod", "Shrink")
            Call MovingFiles("net_rim_tid_dynamic_ling_data_HK*.cod", "Shrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_Big5_HKSCS.cod", "Shrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_EUC_CN.cod", "Shrink")
        End If
        If Croatian.CheckState = 1 Then
            Call MovingFiles("*croatian*.cod", "Shrink")
            Call MovingFiles("*_hr.cod", "Shrink")
        End If
        If Czech.CheckState = 1 Then
            Call MovingFiles("*czech*.cod", "Shrink")
            Call MovingFiles("*_cs.cod", "Shrink")
        End If
        If Hungarian.CheckState = 1 Then
            Call MovingFiles("*hungarian*.cod", "Shrink")
            Call MovingFiles("*_hu.cod", "Shrink")
        End If
        If Turkish.CheckState = 1 Then
            Call MovingFiles("*turkish*.cod", "Shrink")
            Call MovingFiles("*_tr.cod", "Shrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_CP1254.cod", "Shrink")
        End If
        If UKEnglish.CheckState = 1 Then
            Call MovingFiles("*_gb*", "Shrink")
        End If
        If Vietnamese.CheckState = 1 Then
            Call MovingFiles("*vietnamese*.cod", "Shrink")
            Call MovingFiles("*_vi.cod", "Shrink")
            Call MovingFiles("*net_rim_platform_resource__vi__MultiTap.cod", "Shrink")
        End If
        If Indonesian.CheckState = 1 Then
            Call MovingFiles("*indonesian*.cod", "Shrink")
            Call MovingFiles("*_id.cod", "Shrink")
        End If
        If Financial.CheckState = 1 Then
            Call MovingFiles("net_rim_tid_dynamic_ling_data_financial*.cod", "Shrink")
        End If
        If Medical.CheckState = 1 Then
            Call MovingFiles("net_rim_tid_dynamic_ling_data_medical*.cod", "Shrink")
        End If
        If Legal.CheckState = 1 Then
            Call MovingFiles("net_rim_tid_dynamic_ling_data_legal*.cod", "Shrink")
        End If
        If Afrikaans.CheckState = 1 Then
            Call MovingFiles("*_af.cod", "Shrink")
            Call MovingFiles("*afrikaans*.cod", "Shrink")
        End If
        If Arabic.CheckState = 1 Then
            Call MovingFiles("*_ar.cod", "Shrink")
            Call MovingFiles("*arabic*.cod", "Shrink")
        End If
        If Basque.CheckState = 1 Then
            Call MovingFiles("*_eu.cod", "Shrink")
            Call MovingFiles("*basque*.cod", "Shrink")
        End If
        If Catalan.CheckState = 1 Then
            Call MovingFiles("*catalan*.cod", "Shrink")
            Call MovingFiles("*_ca.cod", "Shrink")
        End If
        If Finnish.CheckState = 1 Then
            Call MovingFiles("*finnish*.cod", "Shrink")
            Call MovingFiles("*_fi.cod", "Shrink")
        End If
        If French.CheckState = 1 Then
            Call MovingFiles("*french*.cod", "Shrink")
            Call MovingFiles("*_fr.cod", "Shrink")
        End If
        If Spanish.CheckState = 1 Then
            Call MovingFiles("*spanish*.cod", "Shrink")
            Call MovingFiles("*_es.cod", "Shrink")
            Call MovingFiles("*_es_MX.cod", "Shrink")
        End If
        If Portuguese.CheckState = 1 Then
            Call MovingFiles("*portuguese*.cod", "Shrink")
            Call MovingFiles("*_pt.cod", "Shrink")
            Call MovingFiles("*_pt_br.cod", "Shrink")
        End If
        If Dutch.CheckState = 1 Then
            Call MovingFiles("*dutch*.cod", "Shrink")
            Call MovingFiles("*_du.cod", "Shrink")
            Call MovingFiles("*_nl.cod", "Shrink")
        End If
        If Danish.CheckState = 1 Then
            Call MovingFiles("*danish*.cod", "Shrink")
            Call MovingFiles("*_da.cod", "Shrink")
        End If
        If Galician.CheckState = 1 Then
            Call MovingFiles("*galician*.cod", "Shrink")
            Call MovingFiles("*_gl.cod", "Shrink")
        End If
        If Italian.CheckState = 1 Then
            Call MovingFiles("*italian*.cod", "Shrink")
            Call MovingFiles("*_it.cod", "Shrink")
        End If
        If Japanese.CheckState = 1 Then
            Call MovingFiles("*japanese*.cod", "Shrink")
            Call MovingFiles("*_ja.cod", "Shrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_Shift_JIS.cod", "Shrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_EUC_JP.cod", "Shrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_ISO_2022_JP.cod", "Shrink")
        End If
        If Russian.CheckState = 1 Then
            Call MovingFiles("*russian*.cod", "Shrink")
            Call MovingFiles("*_ru.cod", "Shrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_CP1251.cod", "Shrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_KOI8_R.cod", "Shrink")
        End If
        If SMime.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_smime*.cod", "Shrink")
        End If
        If BBM.CheckState = 1 Then
            Call MovingFiles("BlackBerryMessenger.alx", "ShrinkALX")
            Call MovingFiles("net_rim_bb_qm_peer*.cod", "Shrink")
            Call MovingFiles("net_rim_bb_qm_bbm_internal_api.cod", "Shrink")
            Call MovingFiles("net_rim_bb_qm_lib_barcode.cod", "Shrink")
            Call MovingFiles("net_rim_bb_qm_platform.cod", "Shrink")
            Call MovingFiles("net_rim_bb_qm_api.cod", "Shrink")
            Call MovingFiles("net_rim_bbgroup*.cod", "Shrink")
        End If
        If Maps.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_maps.alx", "ShrinkALX")
            If OSFolderBox.SelectedItem.ToString.Contains("v5.0.0") Then
                Call MovingFiles("net_rim_bb_lbs.cod", "Shrink")
                Call MovingFiles("net_rim_bb_lbs_api.cod", "Shrink")
                Call MovingFiles("net_rim_bb_lbs_lib.cod", "Shrink")
                Call MovingFiles("net_rim_bb_lbs_resource*.cod", "Shrink")
                Call MovingFiles("net_rim_bb_gas.cod", "Shrink")
                Call MovingFiles("LBS.alx", "ShrinkALX")
            ElseIf OSFolderBox.SelectedItem.ToString.Contains("v6.0.0") Then
                Call MovingFiles("net_rim_bb_maps.cod", "Shrink")
                Call MovingFiles("net_rim_bb_maps_resource*.cod", "Shrink")
                Call MovingFiles("net_rim_bb_maps_help*.cod", "Shrink")
            ElseIf OSFolderBox.SelectedItem.ToString.Contains("v7.0.0") Then
                Call MovingFiles("net_rim_bb_maps.cod", "Shrink")
                Call MovingFiles("net_rim_bb_maps_resource*.cod", "Shrink")
                Call MovingFiles("net_rim_bb_maps_help*.cod", "Shrink")
            ElseIf OSFolderBox.SelectedItem.ToString.Contains("v7.1.0") Then
                Call MovingFiles("net_rim_bb_maps.cod", "Shrink")
                Call MovingFiles("net_rim_bb_maps_resource*.cod", "Shrink")
                Call MovingFiles("net_rim_bb_maps_help*.cod", "Shrink")
                Call MovingFiles("net_rim_bb_mapsCatalystLib.cod", "Shrink")
                Call MovingFiles("net_rim_bb_mapsLib.cod", "Shrink")
            End If
        End If
        If MobileConf.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_conf_api.cod", "Shrink")
            Call MovingFiles("net_rim_bb_conf.alx", "ShrinkALX")
        End If
        If AIM.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_qm_aim*.cod", "Shrink")
            Call MovingFiles("aim.alx", "ShrinkALX")
        End If
        If Feeds.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_feeds*.cod", "Shrink")
            Call MovingFiles("net_rim_bb_feeds.alx", "ShrinkALX")
        End If
        If Gmail.CheckState = 1 Then
            Call MovingFiles("*gmail*.cod", "Shrink")
            Call MovingFiles("*gmail*.alx", "ShrinkALX")
        End If
        If Google.CheckState = 1 Then
            Call MovingFiles("*google*.cod", "Shrink")
            Call MovingFiles("GoogleTalk.alx", "ShrinkALX")
        End If
        If ICQ.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_qm_icq*.cod", "Shrink")
            Call MovingFiles("icq*.alx", "ShrinkALX")
        End If
        If MSN.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_qm_msn*.cod", "Shrink")
            Call MovingFiles("WindowsLive*.alx", "ShrinkALX")
        End If
        If MySpace.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_myspace*.cod", "Shrink")
            Call MovingFiles("net_rim_bb_myspace.alx", "ShrinkALX")
        End If
        If Twitter.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_twitter*.cod", "Shrink")
            Call MovingFiles("net_rim_bb_twitter.alx", "ShrinkALX")
        End If
        If Yahoo.CheckState = 1 Then
            Call MovingFiles("*yahoo*.cod", "Shrink")
            Call MovingFiles("Yahoo*.alx", "ShrinkALX")
        End If
        If Facebook.CheckState = 1 Then
            Call MovingFiles("*facebook*.cod", "Shrink")
            Call MovingFiles("net_rim_bb_facebook.alx", "ShrinkALX")
        End If
        If Flickr.CheckState = 1 Then
            Call MovingFiles("*flickr*.cod", "Shrink")
            Call MovingFiles("Flickr*", "ShrinkALX")
        End If
        If RemoveDocsToGo.CheckState = 1 Then
            Call MovingFiles("DocsToGo*.cod", "Shrink")
            Call MovingFiles("PDFToGo*.cod", "Shrink")
            Call MovingFiles("SheetToGo*.cod", "Shrink")
            Call MovingFiles("SlideshowToGo*.cod", "Shrink")
            Call MovingFiles("WordToGo*.cod", "Shrink")
            Call MovingFiles("Documents*.alx", "ShrinkALX")
        End If
        If PDFToGo.CheckState = 1 Then
            Call MovingFiles("PDFToGo*.cod", "Shrink")
        End If
        If SheetToGo.CheckState = 1 Then
            Call MovingFiles("SheetToGo*.cod", "Shrink")
        End If
        If SlideshowsToGo.CheckState = 1 Then
            Call MovingFiles("SlideshowToGo*.cod", "Shrink")
        End If
        If WordToGo.CheckState = 1 Then
            Call MovingFiles("WordToGo*.cod", "Shrink")
        End If
        If Themes.CheckState = 1 Then
            Call MovingFiles("net_rim_theme_bell*", "Shrink")
            Call MovingFiles("net_rim_theme_vodafone*", "Shrink")
            Call MovingFiles("net_rim_theme_telekom*", "Shrink")
            Call MovingFiles("net_rim_theme_att*", "Shrink")
            Call MovingFiles("net_rim_theme_orange*", "Shrink")
            Call MovingFiles("net_rim_theme_rogers*", "Shrink")
            Call MovingFiles("net_rim_theme_telefonica*", "Shrink")
            Call MovingFiles("net_rim_theme_tim*", "Shrink")
            Call MovingFiles("net_rim_theme_tmobile*", "Shrink")
            Call MovingFiles("net_rim_theme_wind*", "Shrink")
            Call MovingFiles("net_rim_theme_100_*", "Shrink")
            Call MovingFiles("net_rim_theme_102a_*", "Shrink")
            Call MovingFiles("net_rim_theme_104_*", "Shrink")
            Call MovingFiles("net_rim_theme_105_*", "Shrink")
            Call MovingFiles("net_rim_theme_107_*", "Shrink")
            Call MovingFiles("net_rim_theme_109_*", "Shrink")
            Call MovingFiles("net_rim_theme_114_*", "Shrink")
            Call MovingFiles("net_rim_theme_115_*", "Shrink")
            Call MovingFiles("net_rim_theme_119_*", "Shrink")
            Call MovingFiles("net_rim_theme_120_*", "Shrink")
            Call MovingFiles("net_rim_theme_125_*", "Shrink")
            Call MovingFiles("net_rim_theme_129_*", "Shrink")
            Call MovingFiles("net_rim_theme_china_*", "Shrink")
        End If
        If CityID.CheckState = 1 Then
            Call MovingFiles("CityID*.cod", "Shrink")
            Call MovingFiles("CityID*.alx", "ShrinkALX")
        End If
        If ChineseDictionary.CheckState = 1 Then
            Call MovingFiles("ChineseDictionary*.cod", "Shrink")
            Call MovingFiles("ChineseDictionary*.alx", "ShrinkALX")
        End If
        If BlockBreaker.CheckState = 1 Then
            Call MovingFiles("BBD2*.cod", "Shrink")
            Call MovingFiles("BBD2*", "ShrinkALX")
        End If
        If Bejeweled.CheckState = 1 Then
            Call MovingFiles("DemoBTwist.cod", "Shrink")
            Call MovingFiles("BejeweledTwist*.cod", "Shrink")
            Call MovingFiles("Bejeweled*", "ShrinkALX")
        End If
        If Monopoly.CheckState = 1 Then
            Call MovingFiles("Monopoly*.cod", "Shrink")
            Call MovingFiles("Monopoly*", "ShrinkALX")
        End If
        If Peggle.CheckState = 1 Then
            Call MovingFiles("Peggle*.cod", "Shrink")
            Call MovingFiles("Peggle*", "ShrinkALX")
        End If
        If TheSims.CheckState = 1 Then
            Call MovingFiles("TheSims3*.cod", "Shrink")
            Call MovingFiles("TheSims3*", "ShrinkALX")
        End If
        If Tetris.CheckState = 1 Then
            Call MovingFiles("Tetris*.cod", "Shrink")
            Call MovingFiles("Tetris*", "ShrinkALX")
        End If
        If Brickbreaker.CheckState = 1 Then
            Call MovingFiles("*brickbreaker*.cod", "Shrink")
            Call MovingFiles("BrickBreaker*", "ShrinkALX")
        End If
        If WordMole.CheckState = 1 Then
            Call MovingFiles("*wordmole*.cod", "Shrink")
            Call MovingFiles("WordMole.alx", "ShrinkALX")
        End If
        If Sudoku.CheckState = 1 Then
            Call MovingFiles("Sudoku*.cod", "Shrink")
            Call MovingFiles("Sudoku*", "ShrinkALX")
        End If
        If Texas.CheckState = 1 Then
            Call MovingFiles("TexasPoker*.cod", "Shrink")
            Call MovingFiles("THK2*.cod", "Shrink")
            Call MovingFiles("THK2*", "ShrinkALX")
            Call MovingFiles("TexasPoker*", "ShrinkALX")
        End If
        If Klondike.CheckState = 1 Then
            Call MovingFiles("Klondike*.cod", "Shrink")
            Call MovingFiles("Klondike*", "ShrinkALX")
        End If
        If VcastMusic.CheckState = 1 Then
            Call MovingFiles("VCastMusic*.cod", "Shrink")
            Call MovingFiles("VCastMusic*", "ShrinkALX")
        End If
        If Amazon.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_storefront*.cod", "Shrink")
            Call MovingFiles("net_rim_bb_storefront_music.alx", "ShrinkALX")
            Call MovingFiles("net_rim_bb_storefront.alx", "ShrinkALX")
        End If
        If AppCenter.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_app_center.cod", "Shrink")
        End If
        If Backgrounds.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_medialoader_backgrounds*", "Shrink")
            Call MovingFiles("net_rim_bb_mediaOTA_backgrounds*", "Shrink")
        End If
        If HelpFiles.CheckState = 1 Then
            Call MovingFiles("*help*.cod", "Shrink")
            Call MovingFiles("Help.alx", "ShrinkALX")
        End If
        If ShrinkEventLog.CheckState = 1 Then
            Call MovingFiles("net_rim_event_log_viewer_app.cod", "Shrink")
        End If
        If G3eWalk.CheckState = 1 Then
            Call MovingFiles("G3eWalk*.cod", "Shrink")
            Call MovingFiles("G3eWalk*.alx", "ShrinkALX")
        End If
        If OTAUpgrade.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_otaupgrade.cod", "Shrink")
        End If
        If PassApps.CheckState = 1 Then
            Call MovingFiles("*password*.cod", "Shrink")
            Call MovingFiles("PasswordKeeper*", "ShrinkALX")
        End If
        If PIMClient.CheckState = 1 Then
            Call MovingFiles("PIMClientBB9*.cod", "Shrink")
            Call MovingFiles("PIMClientBB9*", "ShrinkALX")
        End If
        If SetupWiz.CheckState = 1 Then
            If OSFolderBox.SelectedItem.ToString.Contains("v6.0.0") Then
                Call MovingFiles("net_rim_bb_setupwizard.cod", "Shrink")
            Else
            End If
            Call MovingFiles("net_rim_bb_setupwizard_app.cod", "Shrink")
            Call MovingFiles("net_rim_bb_setupwizard_images_*.cod", "Shrink")
        End If
        If TTY.CheckState = 1 Then
            If OSFolderBox.SelectedItem.ToString.Contains("v6.0.0") Then
            Else
                Call MovingFiles("net_rim_phone_tty_enabler.cod", "Shrink")
            End If
        End If
        If Vodafone.CheckState = 1 Then
            Call MovingFiles("VodafoneMusic*.cod", "Shrink")
            Call MovingFiles("VodafoneMusic*", "ShrinkALX")
        End If
        If VAD.CheckState = 1 Then
            Call MovingFiles("net_rim_vad*", "Shrink")
        End If
        If VNR.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_voicenotesrecorder.cod", "Shrink")
        End If
        If VZNav.CheckState = 1 Then
            Call MovingFiles("VZNavigator.cod", "Shrink")
            Call MovingFiles("SMSWakeup.cod", "Shrink")
            Call MovingFiles("VZNavigator*", "ShrinkALX")
        End If
        If Vcast.CheckState = 1 Then
            Call MovingFiles("MOD.cod", "Shrink")
            Call MovingFiles("MOD*", "ShrinkALX")
        End If
        If Wikitude.CheckState = 1 Then
            Call MovingFiles("Wikitude*.cod", "Shrink")
            Call MovingFiles("Wikitude*.alx", "ShrinkALX")
        End If
        If Ringtones.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_mediaOTA_ringtones*.cod", "Shrink")
            Call MovingFiles("net_rim_bb_medialoader_ringtones*.cod", "Shrink")
            Call MovingFiles("net_rim_bb_medialoader_music*.cod", "Shrink")
        End If
        If Backgrounds.CheckState = 1 And Ringtones.CheckState = 1 And Videos.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_medialoader_*.cod", "Shrink")
            Call MovingFiles("net_rim_bb_mediaOTA_*.cod", "Shrink")
        End If
        If CustomShrinkBox.Text = "" Then
        ElseIf CustomShrinkBox.Text = "Enter your custom shrinking items, seperated by line. Example: net_rim_bb_*" Then
        ElseIf CustomShrinkBox.Text = "net_rim_bb_*" Then
        Else
            ToolStripStatusLabel1.Text = "Shrinking your Custom Shrink files..."
            Dim FileNameString As String
            For Each FileNameString In CustomShrinkBox.Lines
                If My.Computer.FileSystem.FileExists(My.Settings.InputFolder & "\Java\" & FileNameString) Then
                    Call MovingFiles(FileNameString, "Shrink")
                Else
                End If
            Next
        End If
        Call DeleteVendorXml()
        If SaveShrunkFileList.Checked = True Then
            ToolStripStatusLabel1.Text = "Saving Shrunk File List to " & OSFolderBox.SelectedItem.ToString & "-ShrunkFiles.txt"
            If My.Computer.FileSystem.FileExists(OSFolderBox.SelectedItem.ToString & "-ShrunkFiles.txt") Then
                My.Computer.FileSystem.DeleteFile(OSFolderBox.SelectedItem.ToString & "-ShrunkFiles.txt")
            Else
            End If
            Dim SystemRead As New StreamWriter(OSFolderBox.SelectedItem.ToString & "-ShrunkFiles.txt")
            ' Read the standard output of the spawned process.
            SystemRead.WriteLine(OSFolderBox.SelectedItem.ToString & " Shrunk Files")
            SystemRead.WriteLine("----------------------------------------------------------------")
            SystemRead.WriteLine("")
            SystemRead.WriteLine(".ALX Files")
            SystemRead.WriteLine("----------------------------------")
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Install & "\Java\RemovedFiles\", Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, "*.alx")
                Dim foundFileInfo As New System.IO.FileInfo(foundFile)
                SystemRead.WriteLine(foundFileInfo.Name)
            Next
            SystemRead.WriteLine(".COD Files")
            SystemRead.WriteLine("----------------------------------")
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Install & "\Java\RemovedFiles\", Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, "*.cod")
                Dim foundFileInfo As New System.IO.FileInfo(foundFile)
                SystemRead.WriteLine(foundFileInfo.Name)
            Next
            ToolStripStatusLabel1.Text = "Saved Shrunk File List to " & OSFolderBox.SelectedItem.ToString & "-ShrunkFiles.txt in the BBHTool folder."
            'MsgBox("Saving Shrunk File List to " & OSFolderBox.SelectedItem.ToString & "-ShrunkFiles.txt in the BBHTool folder")
            SystemRead.Close()
        End If
        'END SHRINK
    End Sub
    Sub DeleteVendorXml()
        'Vendor.XML removal
        ToolStripStatusLabel1.Text = "Deleting Vendor.xml if it exists..."
        If My.Computer.FileSystem.FileExists(Environ("APPDATA") & "\Research In Motion\BlackBerry\Loader XML\Vendor.xml") Then
            Kill(Environ("APPDATA") & "\Research In Motion\BlackBerry\Loader XML\Vendor.xml")
        End If
        If My.Computer.FileSystem.FileExists(Environ("APPDATA") & "\Roaming\Research In Motion\BlackBerry\Loader XML\Vendor.xml") Then
            Kill(Environ("APPDATA") & "\Roaming\Research In Motion\BlackBerry\Loader XML\Vendor.xml")
        End If
        If My.Computer.FileSystem.FileExists(SystemDrive & "Program Files (x86)\Common Files\Research In Motion\AppLoader\Vendor.xml") Then
            Kill(SystemDrive & "Program Files (x86)\Common Files\Research In Motion\AppLoader\Vendor.xml")
        ElseIf My.Computer.FileSystem.FileExists(SystemDrive & "Program Files\Common Files\Research In Motion\AppLoader\Vendor.xml") Then
            Kill(SystemDrive & "Program Files\Common Files\Research In Motion\AppLoader\Vendor.xml")
        End If
        ToolStripStatusLabel1.Text = "Vendor.xml deleted."
    End Sub
    Public Sub OSFolderBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OSFolderBox.SelectedIndexChanged
        My.Settings.OSFolderBox = OSFolderBox.SelectedItem.ToString
        OSFolder.Text = My.Settings.LoaderDir & OSFolderBox.SelectedItem.ToString & "\"
        My.Settings.InstallDir = OSFolder.Text
        If OSFolderBox.SelectedItem.ToString.Contains("Current OS Folder") Then
            OSFolder.Text = My.Computer.FileSystem.CurrentDirectory & "\"
            My.Settings.InstallDir = My.Computer.FileSystem.CurrentDirectory
            My.Settings.OSFolderBox = "Current OS Folder"
        End If
        Call FindDeviceType()
        GetFileCount("Before")
        If OSFolderBox.SelectedItem.ToString.Contains("6.0.0") Then
            ' MsgBox("Be aware that shrinking too much in a 6.0 OS may cause issues with your install/the OS on device. Please do so at YOUR OWN RISK - You have been warned!")
        Else
        End If
    End Sub

    Sub GetFileCount(ByVal BeforeAfter As String, Optional IncludeSubFolders As Boolean = False)
        If BeforeAfter = "Before" Then
            Dim counter As System.Collections.ObjectModel.ReadOnlyCollection(Of String)


            If My.Computer.FileSystem.DirectoryExists(My.Settings.InstallDir & "Java") Then
                counter = My.Computer.FileSystem.GetFiles(My.Settings.InstallDir & "Java")
                BeforeFiles.Text = (CStr(counter.Count))
                GetFolderSize(My.Settings.InstallDir & "Java", "False")
            Else
                counter = My.Computer.FileSystem.GetFiles(My.Settings.InstallDir)
                BB10BeforeFiles.Text = (CStr(counter.Count))
                GetFolderSize(My.Settings.InstallDir, "False")
            End If


        End If
        If BeforeAfter = "After" Then
            Dim counter As System.Collections.ObjectModel.ReadOnlyCollection(Of String)
            If My.Computer.FileSystem.DirectoryExists(My.Settings.InstallDir & "Java") Then
                counter = My.Computer.FileSystem.GetFiles(My.Settings.InstallDir & "Java\RemovedFiles")
                AfterFiles.Text = (CStr(counter.Count))
                GetFolderSizeShrunk(My.Settings.InstallDir & "Java\RemovedFiles", "False")
            Else
                counter = My.Computer.FileSystem.GetFiles(My.Settings.InstallDir)
                BB10AfterFiles.Text = (CStr(counter.Count))
                GetFolderSizeShrunk(My.Settings.InstallDir & "RemovedFiles", "False")
            End If
        End If
    End Sub
    Function GetFolderSize(ByVal DirPath As String, Optional IncludeSubFolders As Boolean = False)
        Dim lngDirSize
        Dim objFileInfo As FileInfo
        Dim objDir As DirectoryInfo = New DirectoryInfo(DirPath)
        Dim objSubFolder As DirectoryInfo

        Try

            'add length of each file
            For Each objFileInfo In objDir.GetFiles()
                lngDirSize += objFileInfo.Length
            Next

            'call recursively to get sub folders
            'if you don't want this set optional
            'parameter to false 
            If IncludeSubFolders Then
                For Each objSubFolder In objDir.GetDirectories()
                    lngDirSize += GetFolderSize(objSubFolder.FullName)
                Next
            End If

        Catch Ex As Exception
        End Try
        If lngDirSize >= 1073741824 Then
            lngDirSize = Format(lngDirSize / 1024 / 1024 / 1024, "#0.00") & " GB"
        ElseIf lngDirSize >= 1048576 Then
            lngDirSize = Format(lngDirSize / 1024 / 1024, "#0.00") & " MB"
        ElseIf lngDirSize >= 1024 Then
            lngDirSize = Format(lngDirSize / 1024, "#0.00") & " KB"
        ElseIf lngDirSize < 1024 Then
            lngDirSize = Fix(lngDirSize) & " Bytes"
        End If
        If My.Computer.FileSystem.DirectoryExists(My.Settings.InstallDir & "Java") Then
            JavaSize.Text = lngDirSize
        Else
            BB10BeforeSize.Text = lngDirSize
        End If
        Return lngDirSize
    End Function

    Function GetFolderSizeShrunk(ByVal DirPath As String, Optional ByVal IncludeSubFolders As Boolean = True) As Long
        Dim lngDirSize
        Dim objFileInfo As FileInfo
        Dim objDir As DirectoryInfo = New DirectoryInfo(DirPath)
        Dim objSubFolder As DirectoryInfo

        Try

            'add length of each file
            For Each objFileInfo In objDir.GetFiles()
                lngDirSize += objFileInfo.Length
            Next

            'call recursively to get sub folders
            'if you don't want this set optional
            'parameter to false 
            If IncludeSubFolders Then
                For Each objSubFolder In objDir.GetDirectories()
                    lngDirSize += GetFolderSize(objSubFolder.FullName)
                Next
            End If

        Catch Ex As Exception
        End Try
        If lngDirSize >= 1073741824 Then
            lngDirSize = Format(lngDirSize / 1024 / 1024 / 1024, "#0.00") & " GB"
        ElseIf lngDirSize >= 1048576 Then
            lngDirSize = Format(lngDirSize / 1024 / 1024, "#0.00") & " MB"
        ElseIf lngDirSize >= 1024 Then
            lngDirSize = Format(lngDirSize / 1024, "#0.00") & " KB"
        ElseIf lngDirSize < 1024 Then
            lngDirSize = Fix(lngDirSize) & " Bytes"
        End If
        If My.Computer.FileSystem.DirectoryExists(My.Settings.InstallDir & "Java") Then
            JavaSizeShrunk.Text = lngDirSize
        Else
            BB10AfterSize.Text = lngDirSize
        End If
        Return lngDirSize
    End Function
    Public Sub ShrinkOSWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles ShrinkOSWorker.RunWorkerCompleted
        If String.IsNullOrEmpty(OSFolder.Text) Then
            ToolStripStatusLabel1.Text = "Please select your OS install folder before Shrinking your OS."
        ElseIf OSFolder.Text = "C:\Program Files (x86)\Common Files\Research In Motion\Shared\Loader Files" Then
            ToolStripStatusLabel1.Text = "Make sure you choose your OS's install folder!"
        ElseIf OSFolder.Text = "C:\Program Files\Common Files\Research In Motion\Shared\Loader Files" Then
            ToolStripStatusLabel1.Text = "Make sure you choose your OS's install folder!"
        ElseIf OSFolder.Text = "D:\Program Files (x86)\Common Files\Research In Motion\Shared\Loader Files" Then
            ToolStripStatusLabel1.Text = "Make sure you choose your OS's install folder!"
        ElseIf OSFolder.Text = "D:\Program Files\Common Files\Research In Motion\Shared\Loader Files" Then
            ToolStripStatusLabel1.Text = "Make sure you choose your OS's install folder!"
        ElseIf OSFolder.Text = "E:\Program Files (x86)\Common Files\Research In Motion\Shared\Loader Files" Then
            ToolStripStatusLabel1.Text = "Make sure you choose your OS's install folder!"
        ElseIf OSFolder.Text = "E:\Program Files\Common Files\Research In Motion\Shared\Loader Files" Then
            ToolStripStatusLabel1.Text = "Make sure you choose your OS's install folder!"
        Else
            GetFolderSizeShrunk(OSFolder.Text & "/Java/", False)
            Dim counter As System.Collections.ObjectModel.ReadOnlyCollection(Of String)
            counter = My.Computer.FileSystem.GetFiles(My.Settings.InstallDir & "Java")
            AfterFiles.Text = (CStr(counter.Count))
            Dim Size As Integer = JavaSize.Text
            Dim ShrunkSize As UInteger = JavaSizeShrunk.Text
            JavaSize.Text = JavaSize.Text + " MB"
            JavaSizeShrunk.Text = JavaSizeShrunk.Text + " MB"
            ShrinkTotal.Text = "-" & Size - ShrunkSize & " MB"
            ToolStripStatusLabel1.Text = "Your OS has been shrunk. Launch Loader/Desktop Manager to update to your phone."
            MsgBox("Your OS has been shrunk. Launch Loader/Desktop Manager to update to your phone.")
            Shrink.Enabled = True
            If My.Settings.SaveSettingsOnShrink = True Then
                Call SaveSettingsToFile()
            End If
            If My.Settings.AutoLoader = True Then
                Call LaunchLoader()
            End If
            If My.Settings.AutoTweet = True Then
                System.Diagnostics.Process.Start("http://twitter.com/intent/tweet?text=@BBHPlus I just Shrunk My " & My.Settings.OSFolderBox & " OS from " & My.Settings.TweetSize & " to " & My.Settings.TweetShrunkSize & " using BBH Tool: Shrink-A-OS %23BBHPlus")
            End If
        End If
        ShrinkProgressBar.MarqueeAnimationSpeed = 0
        UseWaitCursor = False
    End Sub
    
    'Put It Back Function
    Public Sub Putitback_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Putitback.Click
        Putitback.Enabled = False
        UseWaitCursor = True
        ShrinkProgressBar.MarqueeAnimationSpeed = 80
        ToolStripStatusLabel1.Text = "Your OS is being put back..."
        Dim Install As String = My.Settings.InstallDir
        If My.Computer.FileSystem.DirectoryExists(Install & "\Java\RemovedFiles\") Then
            'Fonts Start
            If Arial.CheckState = 1 Then
                Call MovingFiles("net_rim_font_arial*", "PutBack")
            End If
            If ArabicFont.CheckState = 1 Then
                Call MovingFiles("net_rim_font_arabic*", "PutBack")
            End If
            If Courier.CheckState = 1 Then
                Call MovingFiles("net_rim_font_courier*", "PutBack")
            End If
            If Emoji.CheckState = 1 Then
                Call MovingFiles("net_rim_font_emoji*", "PutBack")
            End If
            If European.CheckState = 1 Then
                Call MovingFiles("net_rim_font_european*", "PutBack")
            End If
            If Georgia.CheckState = 1 Then
                Call MovingFiles("net_rim_font_georgia*", "PutBack")
            End If
            If GlobalFont.CheckState = 1 Then
                Call MovingFiles("net_rim_font_global*", "PutBack")
            End If
            If Indic.CheckState = 1 Then
                Call MovingFiles("net_rim_font_indic*", "PutBack")
            End If
            If JapaneseFont.CheckState = 1 Then
                Call MovingFiles("net_rim_font_japanese*", "PutBack")
            End If
            If Latin.CheckState = 1 Then
                Call MovingFiles("net_rim_font_latin*", "PutBack")
            End If
            If Misc.CheckState = 1 Then
                Call MovingFiles("net_rim_font_misc*", "PutBack")
            End If
            If Monotype.CheckState = 1 Then
                Call MovingFiles("net_rim_font_monotype*", "PutBack")
            End If
            If Tahoma.CheckState = 1 Then
                Call MovingFiles("net_rim_font_tahoma*", "PutBack")
            End If
            If ThaiFont.CheckState = 1 Then
                Call MovingFiles("net_rim_font_thai*", "PutBack")
            End If
            If Times.CheckState = 1 Then
                Call MovingFiles("net_rim_font_times*", "PutBack")
            End If
            If Trebuchet.CheckState = 1 Then
                Call MovingFiles("net_rim_font_trebuchet*", "PutBack")
            End If
            If Verdana.CheckState = 1 Then
                Call MovingFiles("net_rim_font_verdana*", "PutBack")
            End If
            'Fonts End
            'OTT Start
            If OTTCamera.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_camera*", "PutBack")
            End If
            If OTTBluetooth.CheckState = 1 Then
                Call MovingFiles("*bluetooth*", "PutBack")
                Call MovingFiles("net_rim_bb_BT*", "PutBack")
            End If
            If OTTVideoCamera.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_videorecorder*", "PutBack")
            End If
            If DefaultDMTree.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_default102DMtree.cod", "PutBack")
            End If
            If eScreen.CheckState = 1 Then
                Call MovingFiles("net_rim_escreen_app.cod", "PutBack")
            End If
            If AccessOptions.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_medialoader_accessibility.cod", "PutBack")
            End If
            'OTT End
            'Apps Start
            If ATTHotSpot.CheckState = 1 Then
                Call MovingFiles("net_rim_hotspotclient_att*", "PutBack")
            End If
            If DoDCerts.CheckState = 1 Then
                Call MovingFiles("net_rim_DoDRootCerts*", "PutBack")
            End If
            If ITPolicyViewer.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_itpolicyviewer*", "PutBack")
            End If
            If MemoPad.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_memo_app*", "PutBack")
                Call MovingFiles("MemoPad-Java.alx", "PutBackALX")
            End If
            If Slacker.CheckState = 1 Then
                Call MovingFiles("SlackerRadio*.cod", "PutBack")
                Call MovingFiles("SlackerRadio*.alx", "PutBackALX")
                Call MovingFiles("slacker_radio*", "PutBack")
            End If
            If Tasks.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_task_app*", "PutBack")
                Call MovingFiles("Tasks-Java.alx", "PutBackALX")
            End If
            If SuretypeT12.CheckState = 1 Then
                Call MovingFiles("net_rim_tid_suretypeT12*", "PutBack")
            End If
            If ARM.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_application_monitor*.cod", "PutBack")
                Call MovingFiles("net_rim_bb_application_monitor.alx", "PutBackALX")
            End If
            If AppWorld.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_appworld*.cod", "PutBack")
                Call MovingFiles("net_rim_bb_appworld.alx", "PutBackALX")
            End If
            If DeviceAnalyzer.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_device_analyzer*.cod", "PutBack")
                Call MovingFiles("net_rim_bb_device_analyzer.alx", "PutBackALX")
            End If
            If News.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_news*.cod", "PutBack")
                Call MovingFiles("net_rim_bb_news.alx", "PutBackALX")
            End If
            If Plans.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_plans*.cod", "PutBack")
                'Call MovingFiles("net_rim_bb_api_catalyst_serviceplanengine.cod", "PutBack")
                Call MovingFiles("net_rim_bb_plans.alx*", "PutBackALX")
            End If
            If BBProtect.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_dryad*.cod", "PutBack")
                Call MovingFiles("net_rim_bbcommons_dryad.cod", "PutBack")
                Call MovingFiles("net_rim_bb_dryad*.alx", "PutBackALX")
            End If
            If Travel.CheckState = 1 Then
                Call MovingFiles("BlackBerryTravel.alx", "PutBackALX")
            End If
            If BBRadio.CheckState = 1 Then
                Call MovingFiles("CorusAdapter.cod", "PutBack")
                Call MovingFiles("ClearChannelAdapter.cod", "PutBack")
                Call MovingFiles("IRadioStreamProvider.cod", "PutBack")
                Call MovingFiles("PlayerLibrary.cod", "PutBack")
                Call MovingFiles("net_rim_bb_radio_dial.cod", "PutBack")
                Call MovingFiles("SlackerAdapter.cod", "PutBack")
                Call MovingFiles("net_rim_bb_radio_dial.alx", "PutBackALX")
            End If
            If Backup.CheckState = 1 Then
                Call MovingFiles("BackupAssistant*.alx", "PutBackALX")
                Call MovingFiles("BackupAssistant*.cod", "PutBack")
            End If
            If BBMMusic.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_bbcm*.cod", "PutBack")
                Call MovingFiles("net_rim_bb_bbcm.alx", "PutBackALX")
            End If
            If Movitalk.CheckState = 1 Then
                Call MovingFiles("KnJCDE.cod", "PutBack")
                Call MovingFiles("net_rim_bb_phone_ptt_app.cod", "PutBack")
                Call MovingFiles("KodiakPTT.alx", "PutBackALX")
            End If
            If YouTube.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_youtube*.cod", "PutBack")
                Call MovingFiles("net_rim_bb_youtube.alx", "PutBackALX")
            End If
            If Podcasts.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_podcasts*.cod", "PutBack")
                Call MovingFiles("net_rim_bb_podcasts*.alx", "PutBackALX")
            End If
            If VZWNameID.CheckState = 1 Then
                Call MovingFiles("BBCequintVerizon.alx", "PutBackALX")
                Call MovingFiles("CequintAPI.cod", "PutBack")
                Call MovingFiles("cequint_vzw_bold9930_*.cod", "PutBack")
                Call MovingFiles("cequint_vzw_torch9850_*.cod", "PutBack")
                Call MovingFiles("net_rim_bb_phone_cequint_proxy.cod", "PutBack")
                Call MovingFiles("BBCequint.cod", "PutBack")
            End If
            If VZWPTT.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_phone_vz_ptt*", "PutBack")
                Call MovingFiles("MPTT.cod", "PutBack")
                Call MovingFiles("net_rim_bb_medialoader_ringtones_verizon_alert_tones.cod", "PutBack")
                Call MovingFiles("VerizonPTT.alx", "PutBackALX")
            End If
            If TmoError.CheckState = 1 Then
                Call MovingFiles("net_rim_errortranslator_tmobile", "PutBack")
            End If
            If MobileBackup.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_nabsync*", "PutBack")
            End If
            If TMONameID.CheckState = 1 Then
                Call MovingFiles("cequint_tmo_*.cod", "PutBack")
                Call MovingFiles("net_rim_bb_phone_cequint_proxy*.cod", "PutBack")
                Call MovingFiles("BBCequint*", "PutBack")
                Call MovingFiles("CequintAPI", "PutBack")
                Call MovingFiles("BBCequintTMO.alx", "PutBackALX")
            End If
            If TeleNav.CheckState = 1 Then
                Call MovingFiles("TeleNav*.cod", "PutBack")
                Call MovingFiles("TeleNav*.alx", "PutBackALX")
            End If
            If LunarCalendar.CheckState = 1 Then
                Call MovingFiles("LunarCalendar*.cod", "PutBack")
                Call MovingFiles("LunarCalendar*.alx", "PutBackALX")
            End If
            If MobileReader.CheckState = 1 Then
                Call MovingFiles("CMRead_v60*.cod", "PutBack")
                Call MovingFiles("CMRead_V60*.alx", "PutBackALX")
            End If
            If MobileMarket.CheckState = 1 Then
                Call MovingFiles("MobileMarket*.cod", "PutBack")
                Call MovingFiles("MobileMarket*.alx", "PutBackALX")
            End If
            If MusicCN.CheckState = 1 Then
                Call MovingFiles("Music*.cod", "PutBack")
                Call MovingFiles("Music*.alx", "PutBackALX")
            End If
            If MyFaves.CheckState = 1 Then
                Call MovingFiles("net_rim_tmo_five.cod", "PutBack")
                Call MovingFiles("*five_icon_library.cod", "PutBack")
            End If
            If Stock.CheckState = 1 Then
                Call MovingFiles("MobileStock*.cod", "PutBack")
                Call MovingFiles("MobileStock*.alx", "PutBackALX")
            End If
            If Fetion.CheckState = 1 Then
                Call MovingFiles("Fetion*.cod", "PutBack")
                Call MovingFiles("Fetion*.alx", "PutBackALX")
            End If
            If Music.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_medialoader_music_*", "PutBack")
            End If
            If Videos.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_medialoader_video*", "PutBack")
                Call MovingFiles("net_rim_bb_mediaOTA_toolkit*", "PutBack")
                Call MovingFiles("net_rim_bb_medialoader_toolkit*", "PutBack")
            End If
            If VVM.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_mbp*.cod", "PutBack")
                Call MovingFiles("net_rim_bb_vvm*.cod", "PutBack")
                Call MovingFiles("net_rim_bb_vvm.alx", "PutBackALX")
            End If
            If Romanian.CheckState = 1 Then
                Call MovingFiles("*_ro.cod", "PutBack")
                Call MovingFiles("*romanian*.cod", "PutBack")
            End If
            If Norwegian.CheckState = 1 Then
                Call MovingFiles("*_no.cod", "PutBack")
                Call MovingFiles("*norwegian*.cod", "PutBack")
            End If
            If Swedish.CheckState = 1 Then
                Call MovingFiles("*_sv.cod", "PutBack")
                Call MovingFiles("*swedish*.cod", "PutBack")
            End If
            If Polish.CheckState = 1 Then
                Call MovingFiles("*_pl.cod", "PutBack")
                Call MovingFiles("*polish*", "PutBack")
            End If
            If Thai.CheckState = 1 Then
                Call MovingFiles("*_th.cod", "PutBack")
                Call MovingFiles("*thai*.cod", "PutBack")
                Call MovingFiles("net_rim_tid_dynamic_transcoding_data_TIS620.cod", "PutBack")
            End If
            If Korean.CheckState = 1 Then
                Call MovingFiles("*_ko.cod", "PutBack")
                Call MovingFiles("*korean*.cod", "PutBack")
                Call MovingFiles("net_rim_tid_dynamic_transcoding_data_EUC_KR.cod", "PutBack")
                Call MovingFiles("net_rim_tid_dynamic_transcoding_data_x_Johab.cod", "PutBack")
                Call MovingFiles("net_rim_tid_dynamic_transcoding_data_KSC5601.cod", "PutBack")
            End If
            If German.CheckState = 1 Then
                Call MovingFiles("*german*.cod", "PutBack")
                Call MovingFiles("*_de.cod", "PutBack")
            End If
            If Greek.CheckState = 1 Then
                Call MovingFiles("*greek*.cod", "PutBack")
                Call MovingFiles("*_el.cod", "PutBack")
                Call MovingFiles("net_rim_tid_dynamic_transcoding_data_CP1253.cod", "PutBack")
            End If
            If Hebrew.CheckState = 1 Then
                Call MovingFiles("*hebrew*.cod", "PutBack")
                Call MovingFiles("*_he_*.cod", "PutBack")
                Call MovingFiles("*_he.cod", "PutBack")
                Call MovingFiles("net_rim_tid_dynamic_transcoding_data_CP1255.cod", "PutBack")
                Call MovingFiles("net_rim_tid_dynamic_transcoding_data_CP1256.cod", "PutBack")
            End If
            If Hindi.CheckState = 1 Then
                Call MovingFiles("*_hi.cod", "PutBack")
                Call MovingFiles("net_rim_tid_indic.cod", "PutBack")
            End If
            If Chinese.CheckState = 1 Then
                Call MovingFiles("net_rim_tid_dynamic_ling_data_chinese*.cod", "PutBack")
                Call MovingFiles("net_rim_tid_chinese*.cod", "PutBack")
                Call MovingFiles("*pinyin*.cod", "PutBack")
                Call MovingFiles("*_zh.cod", "PutBack")
                Call MovingFiles("*_zh_*.cod", "PutBack")
                Call MovingFiles("net_rim_tid_dynamic_transcoding_data_CP1255.cod", "PutBack")
                Call MovingFiles("net_rim_tid_dynamic_transcoding_data_CP1256.cod", "PutBack")
                Call MovingFiles("net_rim_tid_dynamic_ling_data_HK*.cod", "PutBack")
                Call MovingFiles("net_rim_tid_dynamic_transcoding_data_Big5_HKSCS.cod", "PutBack")
                Call MovingFiles("net_rim_tid_dynamic_transcoding_data_EUC_CN.cod", "PutBack")
            End If
            If Czech.CheckState = 1 Then
                Call MovingFiles("*croatian*.cod", "PutBack")
                Call MovingFiles("*_hr.cod", "PutBack")
            End If
            If Czech.CheckState = 1 Then
                Call MovingFiles("*czech*.cod", "PutBack")
                Call MovingFiles("*_cs.cod", "PutBack")
            End If
            If Hungarian.CheckState = 1 Then
                Call MovingFiles("*hungarian*.cod", "PutBack")
                Call MovingFiles("*_hu.cod", "PutBack")
            End If
            If Turkish.CheckState = 1 Then
                Call MovingFiles("*turkish*.cod", "PutBack")
                Call MovingFiles("*_tr.cod", "PutBack")
                Call MovingFiles("net_rim_tid_dynamic_transcoding_data_CP1254.cod", "PutBack")
            End If
            If UKEnglish.CheckState = 1 Then
                Call MovingFiles("*_gb*", "PutBack")
            End If
            If Vietnamese.CheckState = 1 Then
                Call MovingFiles("*vietnamese*.cod", "PutBack")
                Call MovingFiles("net_rim_platform_resource__vi__MultiTap.cod", "PutBack")
                Call MovingFiles("*_vi.cod", "PutBack")
            End If
            If Indonesian.CheckState = 1 Then
                Call MovingFiles("*indonesian*.cod", "PutBack")
                Call MovingFiles("*_id.cod", "PutBack")
            End If
            If Financial.CheckState = 1 Then
                Call MovingFiles("net_rim_tid_dynamic_ling_data_financial*.cod", "PutBack")
            End If
            If Medical.CheckState = 1 Then
                Call MovingFiles("net_rim_tid_dynamic_ling_data_medical*.cod", "PutBack")
            End If
            If Legal.CheckState = 1 Then
                Call MovingFiles("net_rim_tid_dynamic_ling_data_legal*.cod", "PutBack")
            End If
            If Afrikaans.CheckState = 1 Then
                Call MovingFiles("*_af.cod", "PutBack")
                Call MovingFiles("*afrikaans*.cod", "PutBack")
            End If
            If Arabic.CheckState = 1 Then
                Call MovingFiles("*_ar.cod", "PutBack")
                Call MovingFiles("*arabic*.cod", "PutBack")
            End If
            If Basque.CheckState = 1 Then
                Call MovingFiles("*_eu.cod", "PutBack")
                Call MovingFiles("*basque*.cod", "PutBack")
            End If
            If Catalan.CheckState = 1 Then
                Call MovingFiles("*catalan*.cod", "PutBack")
                Call MovingFiles("*_ca.cod", "PutBack")
            End If
            If Finnish.CheckState = 1 Then
                Call MovingFiles("*finnish*.cod", "PutBack")
                Call MovingFiles("*_fi.cod", "PutBack")
            End If
            If French.CheckState = 1 Then
                Call MovingFiles("*french*.cod", "PutBack")
                Call MovingFiles("*_fr.cod", "PutBack")
            End If
            If Spanish.CheckState = 1 Then
                Call MovingFiles("*spanish*.cod", "PutBack")
                Call MovingFiles("*_es.cod", "PutBack")
                Call MovingFiles("*_es_MX.cod", "PutBack")
            End If
            If Portuguese.CheckState = 1 Then
                Call MovingFiles("*portuguese*.cod", "PutBack")
                Call MovingFiles("*_pt.cod", "PutBack")
                Call MovingFiles("*_pt_br.cod", "PutBack")
            End If
            If Dutch.CheckState = 1 Then
                Call MovingFiles("*dutch*.cod", "PutBack")
                Call MovingFiles("*_du.cod", "PutBack")
                Call MovingFiles("*_nl.cod", "PutBack")
            End If
            If Danish.CheckState = 1 Then
                Call MovingFiles("*danish*.cod", "PutBack")
                Call MovingFiles("*_da.cod", "PutBack")
            End If
            If Galician.CheckState = 1 Then
                Call MovingFiles("*galician*.cod", "PutBack")
                Call MovingFiles("*_gl.cod", "PutBack")
            End If
            If Italian.CheckState = 1 Then
                Call MovingFiles("*italian*.cod", "PutBack")
                Call MovingFiles("*_it.cod", "PutBack")
            End If
            If Japanese.CheckState = 1 Then
                Call MovingFiles("*japanese*.cod", "PutBack")
                Call MovingFiles("*_ja.cod", "PutBack")
                Call MovingFiles("net_rim_tid_dynamic_transcoding_data_Shift_JIS.cod", "PutBack")
                Call MovingFiles("net_rim_tid_dynamic_transcoding_data_EUC_JP.cod", "PutBack")
                Call MovingFiles("net_rim_tid_dynamic_transcoding_data_ISO_2022_JP.cod", "PutBack")
            End If
            If Russian.CheckState = 1 Then
                Call MovingFiles("*russian*.cod", "PutBack")
                Call MovingFiles("*_ru.cod", "PutBack")
                Call MovingFiles("net_rim_tid_dynamic_transcoding_data_CP1251.cod", "PutBack")
                Call MovingFiles("net_rim_tid_dynamic_transcoding_data_KOI8_R.cod", "PutBack")
            End If
            If SMime.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_smime*.cod", "PutBack")
            End If
            If BBM.CheckState = 1 Then
                Call MovingFiles("BlackBerryMessenger.alx", "PutBackALX")
                Call MovingFiles("net_rim_bb_qm_bbm_internal_api.cod", "PutBack")
                Call MovingFiles("net_rim_bb_qm_platform.cod", "PutBack")
                Call MovingFiles("net_rim_bb_qm_peer*.cod", "PutBack")
                Call MovingFiles("net_rim_bb_qm_lib_barcode.cod", "PutBack")
                Call MovingFiles("net_rim_bb_qm_api.cod", "PutBack")
                Call MovingFiles("net_rim_bbgroup*.cod", "PutBack")
            End If
            If Maps.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_maps.alx", "PutBackALX")
                If OSFolderBox.SelectedItem.ToString.Contains("v5.0.0") Then
                    Call MovingFiles("net_rim_bb_lbs.cod", "PutBack")
                    Call MovingFiles("net_rim_bb_lbs_api.cod", "PutBack")
                    Call MovingFiles("net_rim_bb_lbs_lib.cod", "PutBack")
                    Call MovingFiles("net_rim_bb_lbs_resource*.cod", "PutBack")
                    Call MovingFiles("net_rim_bb_gas.cod", "PutBack")
                    Call MovingFiles("LBS.alx", "PutBackALX")
                ElseIf OSFolderBox.SelectedItem.ToString.Contains("v6.0.0") Then
                    Call MovingFiles("net_rim_bb_maps.cod", "PutBack")
                    Call MovingFiles("net_rim_bb_maps_resource*.cod", "PutBack")
                    Call MovingFiles("net_rim_bb_maps_help*.cod", "PutBack")
                ElseIf OSFolderBox.SelectedItem.ToString.Contains("v7.0.0") Then
                    Call MovingFiles("net_rim_bb_maps.cod", "PutBack")
                    Call MovingFiles("net_rim_bb_maps_resource*.cod", "PutBack")
                    Call MovingFiles("net_rim_bb_maps_help*.cod", "PutBack")
                ElseIf OSFolderBox.SelectedItem.ToString.Contains("v7.1.0") Then
                    Call MovingFiles("net_rim_bb_maps.cod", "PutBack")
                    Call MovingFiles("net_rim_bb_maps_resource*.cod", "PutBack")
                    Call MovingFiles("net_rim_bb_maps_help*.cod", "PutBack")
                    Call MovingFiles("net_rim_bb_mapsCatalystLib.cod", "PutBack")
                    Call MovingFiles("net_rim_bb_mapsLib.cod", "PutBack")
                End If
            End If
            If MobileConf.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_conf_api.cod", "PutBack")
                Call MovingFiles("net_rim_bb_conf.alx", "PutBackALX")
            End If
            If AIM.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_qm_aim*.cod", "PutBack")
                Call MovingFiles("aim.alx", "PutBackALX")
            End If
            If Feeds.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_feeds*.cod", "PutBack")
                Call MovingFiles("net_rim_bb_feeds.alx", "PutBackALX")
            End If
            If Gmail.CheckState = 1 Then
                Call MovingFiles("*gmail*.cod", "PutBack")
                Call MovingFiles("*gmail*.alx", "PutBackALX")
            End If
            If Google.CheckState = 1 Then
                Call MovingFiles("*google*.cod", "PutBack")
                Call MovingFiles("GoogleTalk.alx", "PutBackALX")
            End If
            If ICQ.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_qm_icq*.cod", "PutBack")
                Call MovingFiles("icq*", "PutBackALX")
            End If
            If MSN.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_qm_msn*.cod", "PutBack")
                Call MovingFiles("WindowsLiveMessenger.alx", "PutBackALX")
            End If
            If MySpace.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_myspace*.cod", "PutBack")
                Call MovingFiles("*myspace.alx", "PutBackALX")
            End If
            If Twitter.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_twitter*.cod", "PutBack")
                Call MovingFiles("*twitter.alx", "PutBackALX")
            End If
            If Yahoo.CheckState = 1 Then
                Call MovingFiles("*yahoo*.cod", "PutBack")
                Call MovingFiles("YahooMessenger.alx", "PutBackALX")
            End If
            If Facebook.CheckState = 1 Then
                Call MovingFiles("*facebook*.cod", "PutBack")
                Call MovingFiles("net_rim_bb_facebook.alx", "PutBackALX")
            End If
            If Flickr.CheckState = 1 Then
                Call MovingFiles("*flickr*.cod", "PutBack")
                Call MovingFiles("Flickr.alx", "PutBackALX")
            End If
            If RemoveDocsToGo.CheckState = 1 Then
                Call MovingFiles("DocsToGo*.cod", "PutBack")
                Call MovingFiles("PDFToGo*.cod", "PutBack")
                Call MovingFiles("SheetToGo*.cod", "PutBack")
                Call MovingFiles("SlideshowToGo*.cod", "PutBack")
                Call MovingFiles("WordToGo*.cod", "PutBack")
                Call MovingFiles("DocumentsToGo*.alx", "PutBackALX")
            End If
            If PDFToGo.CheckState = 1 Then
                Call MovingFiles("PDFToGo*.cod", "PutBack")
            End If
            If SheetToGo.CheckState = 1 Then
                Call MovingFiles("SheetToGo*.cod", "PutBack")
            End If
            If SlideshowsToGo.CheckState = 1 Then
                Call MovingFiles("SlideshowToGo*.cod", "PutBack")
            End If
            If WordToGo.CheckState = 1 Then
                Call MovingFiles("WordToGo*.cod", "PutBack")
            End If
            If Themes.CheckState = 1 Then
                Call MovingFiles("net_rim_theme_bell*", "PutBack")
                Call MovingFiles("net_rim_theme_vodafone*", "PutBack")
                Call MovingFiles("net_rim_theme_telekom*", "PutBack")
                Call MovingFiles("net_rim_theme_att*", "PutBack")
                Call MovingFiles("net_rim_theme_orange*", "PutBack")
                Call MovingFiles("net_rim_theme_rogers*", "PutBack")
                Call MovingFiles("net_rim_theme_telefonica*", "PutBack")
                Call MovingFiles("net_rim_theme_tim*", "PutBack")
                Call MovingFiles("net_rim_theme_tmobile*", "PutBack")
                Call MovingFiles("net_rim_theme_wind*", "PutBack")
                Call MovingFiles("net_rim_theme_100_*", "PutBack")
                Call MovingFiles("net_rim_theme_102a_*", "PutBack")
                Call MovingFiles("net_rim_theme_104_*", "PutBack")
                Call MovingFiles("net_rim_theme_105_*", "PutBack")
                Call MovingFiles("net_rim_theme_107_*", "PutBack")
                Call MovingFiles("net_rim_theme_109_*", "PutBack")
                Call MovingFiles("net_rim_theme_114_*", "PutBack")
                Call MovingFiles("net_rim_theme_115_*", "PutBack")
                Call MovingFiles("net_rim_theme_119_*", "PutBack")
                Call MovingFiles("net_rim_theme_120_*", "PutBack")
                Call MovingFiles("net_rim_theme_125_*", "PutBack")
                Call MovingFiles("net_rim_theme_129_*", "PutBack")
                Call MovingFiles("net_rim_theme_china_*", "PutBack")
            End If
            If CityID.CheckState = 1 Then
                Call MovingFiles("CityID*.cod", "PutBack")
                Call MovingFiles("CityID*", "PutBackALX")
            End If
            If ChineseDictionary.CheckState = 1 Then
                Call MovingFiles("ChineseDictionary*.cod", "PutBack")
                Call MovingFiles("ChineseDictionary*.alx", "PutBackALX")
            End If
            If BlockBreaker.CheckState = 1 Then
                Call MovingFiles("BBD2*.cod", "PutBack")
                Call MovingFiles("BBD2*.alx", "PutBackALX")
            End If
            If Bejeweled.CheckState = 1 Then
                Call MovingFiles("DemoBTwist.cod", "PutBack")
                Call MovingFiles("BejeweledTwist*.cod", "PutBack")
                Call MovingFiles("BejeweledTwist.alx", "PutBackALX")
            End If
            If Monopoly.CheckState = 1 Then
                Call MovingFiles("Monopoly*.cod", "PutBack")
                Call MovingFiles("Monopoly*.alx", "PutBackALX")
            End If
            If Peggle.CheckState = 1 Then
                Call MovingFiles("Peggle*.cod", "PutBack")
                Call MovingFiles("Peggle.alx", "PutBackALX")
            End If
            If TheSims.CheckState = 1 Then
                Call MovingFiles("TheSims3*.cod", "PutBack")
                Call MovingFiles("TheSims3*.alx", "PutBackALX")
            End If
            If Tetris.CheckState = 1 Then
                Call MovingFiles("Tetris*.cod", "PutBack")
                Call MovingFiles("Tetris*", "PutBackALX")
            End If
            If Brickbreaker.CheckState = 1 Then
                Call MovingFiles("*brickbreaker*.cod", "PutBack")
                Call MovingFiles("BrickBreaker.alx", "PutBackALX")
            End If
            If WordMole.CheckState = 1 Then
                Call MovingFiles("*wordmole*.cod", "PutBack")
                Call MovingFiles("WordMole.alx", "PutBackALX")
            End If
            If Sudoku.CheckState = 1 Then
                Call MovingFiles("Sudoku*.cod", "PutBack")
                Call MovingFiles("Sudoku*.alx", "PutBackALX")
            End If
            If Texas.CheckState = 1 Then
                Call MovingFiles("TexasPoker*.cod", "PutBack")
                Call MovingFiles("THK2*.cod", "PutBack")
                Call MovingFiles("TexasPoker*.alx", "PutBackALX")
                Call MovingFiles("THK2*.alx", "PutBackALX")
            End If
            If Klondike.CheckState = 1 Then
                Call MovingFiles("Klondike*.cod", "PutBack")
                Call MovingFiles("Klondike*.alx", "PutBackALX")
            End If
            If VcastMusic.CheckState = 1 Then
                Call MovingFiles("VCastMusic*.cod", "PutBack")
                Call MovingFiles("VCastMusic*", "PutBackALX")
            End If
            If Amazon.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_storefront*.cod", "PutBack")
                Call MovingFiles("net_rim_bb_storefront_music.alx", "PutBackALX")
                Call MovingFiles("net_rim_bb_storefront.alx", "PutBackALX")
            End If
            If AppCenter.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_app_center.cod", "PutBack")
            End If
            If Backgrounds.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_medialoader_backgrounds*.cod", "PutBack")
                Call MovingFiles("net_rim_bb_mediaOTA_backgrounds*.cod", "PutBack")
            End If
            If HelpFiles.CheckState = 1 Then
                Call MovingFiles("*help*.cod", "PutBack")
                Call MovingFiles("Help.alx", "PutBackALX")
            End If
            If ShrinkEventLog.CheckState = 1 Then
                Call MovingFiles("net_rim_event_log_viewer_app.cod", "PutBack")
            End If
            If G3eWalk.CheckState = 1 Then
                Call MovingFiles("G3eWalk*.cod", "PutBack")
                Call MovingFiles("G3eWalk*.alx", "PutBackALX")
            End If
            If OTAUpgrade.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_otaupgrade.cod", "PutBack")
            End If
            If PassApps.CheckState = 1 Then
                Call MovingFiles("*password*.cod", "PutBack")
                Call MovingFiles("PasswordKeeper*", "PutBackALX")
            End If
            If PIMClient.CheckState = 1 Then
                Call MovingFiles("PIMClientBB9*.cod", "PutBack")
                Call MovingFiles("PIMClientBB9*", "PutBackALX")
            End If
            If SetupWiz.CheckState = 1 Then
                If OSFolderBox.SelectedItem.ToString.Contains("v6.0.0") Then
                    Call MovingFiles("net_rim_bb_setupwizard.cod", "PutBack")
                Else
                End If
                Call MovingFiles("net_rim_bb_setupwizard_app.cod", "PutBack")
                Call MovingFiles("net_rim_bb_setupwizard_images_*.cod", "PutBack")
            End If
            If TTY.CheckState = 1 Then
                If OSFolderBox.SelectedItem.ToString.Contains("v6.0.0") Then
                Else
                    Call MovingFiles("net_rim_phone_tty_enabler.cod", "PutBack")
                End If
            End If
            If Vodafone.CheckState = 1 Then
                Call MovingFiles("VodafoneMusic*", "PutBack")
                Call MovingFiles("VodafoneMusic*", "PutBackALX")
            End If
            If VAD.CheckState = 1 Then
                Call MovingFiles("net_rim_vad*", "PutBack")
            End If
            If VNR.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_voicenotesrecorder.cod", "PutBack")
            End If
            If VZNav.CheckState = 1 Then
                Call MovingFiles("VZNavigator.cod", "PutBack")
                Call MovingFiles("SMSWakeup.cod", "PutBack")
                Call MovingFiles("VZNavigator.alx", "PutBackALX")
            End If
            If Vcast.CheckState = 1 Then
                Call MovingFiles("MOD.cod", "PutBack")
                Call MovingFiles("MOD.alx", "PutBackALX")
            End If
            If Wikitude.CheckState = 1 Then
                Call MovingFiles("Wikitude*.cod", "PutBack")
                Call MovingFiles("Wikitude*.alx", "PutBackALX")
            End If
            If Ringtones.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_mediaOTA_ringtones*.cod", "PutBack")
                Call MovingFiles("net_rim_bb_medialoader_ringtones*.cod", "PutBack")
                Call MovingFiles("net_rim_bb_medialoader_music*.cod", "PutBack")
            End If
            If Backgrounds.CheckState = 1 And Ringtones.CheckState = 1 And Videos.CheckState = 1 Then
                Call MovingFiles("net_rim_bb_medialoader_*.cod", "PutBack")
                Call MovingFiles("net_rim_bb_mediaOTA_*.cod", "PutBack")
            End If
            If CustomShrinkBox.Text = "" Then
            ElseIf CustomShrinkBox.Text = "Enter your custom shrinking items, seperated by line. Example: net_rim_bb_*" Then
            ElseIf CustomShrinkBox.Text = "net_rim_bb_*" Then
            Else
                ToolStripStatusLabel1.Text = "Shrinking your Custom Shrink files..."
                Dim FileNameString As String
                For Each FileNameString In CustomShrinkBox.Lines
                    If My.Computer.FileSystem.FileExists(My.Settings.InputFolder & "\Java\" & FileNameString) Then
                        Call MovingFiles(FileNameString, "PutBack")
                    Else
                    End If
                Next
            End If
            UseWaitCursor = False
            MsgBox("Your OS has been put back.")
            ToolStripStatusLabel1.Text = "Your OS has been put back. Launch Loader/Desktop Manager to update to your phone."
        Else
            MsgBox("No RemovedFiles folder to restore.")
            ToolStripStatusLabel1.Text = "No RemovedFiles folder to restore."
        End If
        Putitback.Enabled = True
        ShrinkProgressBar.MarqueeAnimationSpeed = 0
    End Sub
    Public Sub Loader_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call LaunchLoader()
    End Sub
    Public Sub Swap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Swap.Click
        ' start the progress bar doing its thing:
        BaHProgressBar.MarqueeAnimationSpeed = 80
        If SwapWorker.IsBusy Then Exit Sub
        Me.SwapWorker.RunWorkerAsync()
    End Sub
    Public Sub SwapWorker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles SwapWorker.DoWork
        Dim Install As String = My.Settings.InstallDir
        If DeviceComboBox.SelectedItem Is "8330 (Curve)" Then
            If ComboBox8330.SelectedItem Is Nothing Then
                ToolStripStatusLabel1.Text = "No Radio File To Be Swapped!"
                MsgBox("No Radio File To Be Swapped!")
            End If
        End If
        If DeviceComboBox.SelectedItem Is "8520 (Gemini)" Then
            If ComboBox8520.SelectedItem Is Nothing Then
                ToolStripStatusLabel1.Text = "No Radio File To Be Swapped!"
                MsgBox("No Radio File To Be Swapped!")
            End If
        End If
        If DeviceComboBox.SelectedItem Is "9630 (Tour)" Then
            If ComboBox9630.SelectedItem Is Nothing Then
                ToolStripStatusLabel1.Text = "No Radio File To Be Swapped!"
                MsgBox("No Radio File To Be Swapped!")
            End If
        End If
        If DeviceComboBox.SelectedItem Is "9000 (Bold)" Then
            If ComboBox9000.SelectedItem Is Nothing Then
                ToolStripStatusLabel1.Text = "No Radio File To Be Swapped!"
                MsgBox("No Radio File To Be Swapped!")
            End If
        End If
        If DeviceComboBox.SelectedItem Is "9500 (Storm)" Then
            If ComboBox9500.SelectedItem Is Nothing Then
                ToolStripStatusLabel1.Text = "No Radio File To Be Swapped!"
                MsgBox("No Radio File To Be Swapped!")
            End If
        End If
        If DeviceComboBox.SelectedItem Is "9530 (Storm)" Then
            If ComboBox9530.SelectedItem Is Nothing Then
                ToolStripStatusLabel1.Text = "No Radio File To Be Swapped!"
                MsgBox("No Radio File To Be Swapped!")
            End If
        End If
        If DeviceComboBox.SelectedItem Is "9700 (Bold)" Then
            If ComboBox9700.SelectedItem Is Nothing Then
                ToolStripStatusLabel1.Text = "No Radio File To Be Swapped!"
                MsgBox("No Radio File To Be Swapped!")
            End If
        End If
        If DeviceComboBox.SelectedItem Is "9550 (Storm 2)" Then
            If ComboBox9550.SelectedItem Is Nothing Then
                ToolStripStatusLabel1.Text = "No Radio File To Be Swapped!"
                MsgBox("No Radio File To Be Swapped!")
            End If
        End If
        If DeviceComboBox.SelectedItem Is "9520 (Storm 2)" Then
            If ComboBox9520.SelectedItem Is Nothing Then
                ToolStripStatusLabel1.Text = "No Radio File To Be Swapped!"
                MsgBox("No Radio File To Be Swapped!")
            End If
        End If
        If DeviceComboBox.SelectedItem Is "9650 (Bold)" Then
            'NOTE: Add 9650 Box
            If ComboBox9630.SelectedItem Is Nothing Then
                ToolStripStatusLabel1.Text = "No Radio File To Be Swapped!"
                MsgBox("No Radio File To Be Swapped!")
            End If
        End If
        If DeviceComboBox.SelectedItem Is "9100/9105 (Pearl 3G)" Then
            'NOTE: Add 910x Box
            If ComboBox9630.SelectedItem Is Nothing Then
                ToolStripStatusLabel1.Text = "No Radio File To Be Swapped!"
                MsgBox("No Radio File To Be Swapped!")
            End If
        End If
        If DeviceComboBox.SelectedItem Is "9800 (Torch)" Then
            'NOTE: Add 9800 Box
            If ComboBox9630.SelectedItem Is Nothing Then
                ToolStripStatusLabel1.Text = "No Radio File To Be Swapped!"
                MsgBox("No Radio File To Be Swapped!")
            End If
        End If
        If DeviceComboBox.SelectedItem Is "9330 (Curve 3G)" Then
            'NOTE: Add 9330 Box
            If ComboBox9630.SelectedItem Is Nothing Then
                ToolStripStatusLabel1.Text = "No Radio File To Be Swapped!"
                MsgBox("No Radio File To Be Swapped!")
            End If
        End If
        If DeviceComboBox.SelectedItem Is "9300 (Curve 3G)" Then
            'NOTE: Add 9300 Box
            If ComboBox9630.SelectedItem Is Nothing Then
                ToolStripStatusLabel1.Text = "No Radio File To Be Swapped!"
                MsgBox("No Radio File To Be Swapped!")
            End If
        End If
        'Start 8900
        If ComboBox8900.SelectedItem Is Nothing Then
        Else
            If My.Computer.FileSystem.FileExists(Install & "\GPRS\rim0x84001503.sfi") Then
                My.Computer.FileSystem.CopyFile(Install & "\GPRS\rim0x84001503.sfi", Install & "\GPRS\Backup.sfi", True)
                System.IO.File.Delete(Install & "\GPRS\rim0x84001503.sfi")
                ToolStripStatusLabel1.Text = "Downloading " & ComboBox8900.SelectedItem & " Radio to be swapped..."
                My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/bbradios/8900/" & ComboBox8900.SelectedItem & ".sfi", Install & "\GPRS\rim0x84001503.sfi")
            Else
                ToolStripStatusLabel1.Text = "No rim0x84001503.sfi exists. Make sure you have selected the right device and OS folder."
                MsgBox("No rim0x84001503.sfi exists. Make sure you have selected the right device and OS folder.")
            End If
        End If
        'End 8900
        'Start Bold
        If ComboBox9700.SelectedItem Is Nothing Then
        Else
            If My.Computer.FileSystem.FileExists(Install & "\UMTS\rim0x04001507.sfi") Then
                My.Computer.FileSystem.CopyFile(Install & "\UMTS\rim0x04001507.sfi", Install & "\UMTS\Backup.sfi", True)
                System.IO.File.Delete(Install & "\UMTS\rim0x04001507.sfi")
                ToolStripStatusLabel1.Text = "Downloading " & ComboBox9700.SelectedItem & " Radio to be swapped..."
                My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/bbradios/9700/" & ComboBox9700.SelectedItem & ".sfi", Install & "\UMTS\rim0x04001507.sfi")
            Else
                ToolStripStatusLabel1.Text = "No rim0x04001507.sfi exists. Make sure you have selected the right device and OS folder."
                MsgBox("No rim0x04001507.sfi exists. Make sure you have selected the right device and OS folder.")
            End If

        End If
        'End Bold
        'Start 9550
        If ComboBox9550.SelectedItem Is Nothing Then
        Else
            If My.Computer.FileSystem.FileExists(Install & "\CDMA\rim0x0C001404.sfi") Then
                My.Computer.FileSystem.CopyFile(Install & "\CDMA\rim0x0C001404.sfi", Install & "\CDMA\Backup.sfi", True)
                System.IO.File.Delete(Install & "\CDMA\rim0x0C001404.sfi")
                ToolStripStatusLabel1.Text = "Downloading " & ComboBox9550.SelectedItem & " Radio to be swapped..."
                My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/bbradios/9550/" & ComboBox9550.SelectedItem & ".sfi", Install & "\CDMA\rim0x0C001404.sfi")
            Else
                ToolStripStatusLabel1.Text = "No rim0x0C001404.sfi exists. Make sure you have selected the right device and OS folder."
                MsgBox("No rim0x0C001404.sfi exists. Make sure you have selected the right device and OS folder.")
            End If
        End If
        'End 9550
        'Start 9520
        If ComboBox9520.SelectedItem Is Nothing Then
        Else
            If My.Computer.FileSystem.FileExists(Install & "\CDMA\rim0x0E001404.sfi") Then
                My.Computer.FileSystem.CopyFile(Install & "\CDMA\rim0x0E001404.sfi", Install & "\CDMA\Backup.sfi", True)
                System.IO.File.Delete(Install & "\CDMA\rim0x0E001404.sfi")
                ToolStripStatusLabel1.Text = "Downloading " & ComboBox9520.SelectedItem & " Radio to be swapped..."
                My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/bbradios/9520/" & ComboBox9520.SelectedItem & ".sfi", Install & "\CDMA\rim0x0E001404.sfi")
            Else
                ToolStripStatusLabel1.Text = "No rim0x0E001404.sfi exists. Make sure you have selected the right device and OS folder."
                MsgBox("No rim0x0E001404.sfi exists. Make sure you have selected the right device and OS folder.")
            End If
        End If
        'End 9520
        'Start Curve
        If ComboBox8330.SelectedItem Is Nothing Then
        Else
            If My.Computer.FileSystem.FileExists(Install & "\CDMA\rim8330c.sfi") Then
                My.Computer.FileSystem.CopyFile(Install & "\CDMA\rim8330c.sfi", Install & "\CDMA\Backup.sfi", True)
                System.IO.File.Delete(Install & "\CDMA\rim8330c.sfi")
                ToolStripStatusLabel1.Text = "Downloading " & ComboBox8330.SelectedItem & " Radio to be swapped..."
                My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/bbradios/8330/" & ComboBox8330.SelectedItem & ".sfi", Install & "\CDMA\rim8330c.sfi")
            Else
                ToolStripStatusLabel1.Text = "No rim8330c.sfi exists. Make sure you have selected the right device and OS folder."
                MsgBox("No rim8330c.sfi exists. Make sure you have selected the right device and OS folder.")
            End If
        End If
        'End Curve
        'Start Bold
        If ComboBox9000.SelectedItem Is Nothing Then
        Else
            If My.Computer.FileSystem.FileExists(Install & "\UMTS\rim9000.sfi") Then
                My.Computer.FileSystem.CopyFile(Install & "\UMTS\rim9000.sfi", Install & "\UMTS\Backup.sfi", True)
                System.IO.File.Delete(Install & "\UMTS\rim9000.sfi")
                ToolStripStatusLabel1.Text = "Downloading " & ComboBox9000.SelectedItem & " Radio to be swapped..."
                My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/bbradios/9000/" & ComboBox9000.SelectedItem & ".sfi", Install & "\UMTS\rim9000.sfi")
            Else
                ToolStripStatusLabel1.Text = "No rim9000.sfi exists. Make sure you have selected the right device and OS folder."
                MsgBox("No rim9000.sfi exists. Make sure you have selected the right device and OS folder.")
            End If
        End If
        'End Bold
        'Start Tour
        If ComboBox9630.SelectedItem Is Nothing Then
        Else
            If My.Computer.FileSystem.FileExists(Install & "\CDMA\rim0x0D000D04.sfi") Then
                My.Computer.FileSystem.CopyFile(Install & "\CDMA\rim0x0D000D04.sfi", Install & "\CDMA\Backup.sfi", True)
                System.IO.File.Delete(Install & "\CDMA\rim0x0D000D04.sfi")
                ToolStripStatusLabel1.Text = "Downloading " & ComboBox9630.SelectedItem & " Radio to be swapped..."
                My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/bbradios/9000/" & ComboBox9630.SelectedItem & ".sfi", Install & "\CDMA\rim0x0D000D04.sfi")
            Else
                ToolStripStatusLabel1.Text = "No rim0x0D000D04.sfi exists. Make sure you have selected the right device and OS folder."
                MsgBox("No rim0x0D000D04.sfi exists. Make sure you have selected the right device and OS folder.")
            End If
        End If
        'End Tour
        'Start Gemini
        If ComboBox8520.SelectedItem Is Nothing Then
        Else
            If My.Computer.FileSystem.FileExists(Install & "\GPRS\rim0x8C000F03.sfi") Then
                My.Computer.FileSystem.CopyFile(Install & "\GPRS\rim0x8C000F03.sfi", Install & "\GPRS\Backup.sfi", True)
                System.IO.File.Delete(Install & "\GPRS\rim0x8C000F03.sfi")
                ToolStripStatusLabel1.Text = "Downloading " & ComboBox8520.SelectedItem & " Radio to be swapped..."
                My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/bbradios/8520/" & ComboBox8520.SelectedItem & ".sfi", Install & "\GPRS\rim0x8C000F03.sfi")
            Else
                ToolStripStatusLabel1.Text = "No rim0x8C000F03.sfi exists. Make sure you have selected the right device and OS folder."
                MsgBox("No rim0x8C000F03.sfi exists. Make sure you have selected the right device and OS folder.")
            End If
        End If
        'End Gemini
        'Start Radio9500
        If ComboBox9500.SelectedItem Is Nothing Then
        Else
            If My.Computer.FileSystem.FileExists(Install & "\CDMA\rim0x06001404OMADRM.sfi") Then
                My.Computer.FileSystem.CopyFile(Install & "\CDMA\rim0x06001404OMADRM.sfi", Install & "\CDMA\Backup.sfi", True)
                System.IO.File.Delete(Install & "\CDMA\rim0x06001404OMADRM.sfi")
                ToolStripStatusLabel1.Text = "Downloading " & ComboBox9500.SelectedItem & " Radio to be swapped..."
                My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/bbradios/9500/" & ComboBox9500.SelectedItem & ".sfi", Install & "\CDMA\rim0x06001404OMADRM.sfi")
            Else
                ToolStripStatusLabel1.Text = "No rim0x06001404OMADRM.sfi exists. Make sure you have selected the right device and OS folder."
                MsgBox("No rim0x06001404OMADRM.sfi exists. Make sure you have selected the right device and OS folder.")
            End If
        End If
        'End Radio9500
        'Start Radio9530
        If ComboBox9530.SelectedItem Is Nothing Then
        Else
            If My.Computer.FileSystem.FileExists(Install & "\CDMA\rim0x04001404.sfi") Then
                My.Computer.FileSystem.CopyFile(Install & "\CDMA\rim0x04001404.sfi", Install & "\CDMA\Backup.sfi", True)
                System.IO.File.Delete(Install & "\CDMA\rim0x04001404.sfi")
                ToolStripStatusLabel1.Text = "Downloading " & ComboBox9530.SelectedItem & " Radio to be swapped..."
                My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/bbradios/9530/" & ComboBox9530.SelectedItem & ".sfi", Install & "\CDMA\rim0x04001404.sfi")
            Else
                ToolStripStatusLabel1.Text = "No rim0x04001404.sfi exists. Make sure you have selected the right device and OS folder."
                MsgBox("No rim0x04001404.sfi exists. Make sure you have selected the right device and OS folder.")
            End If
        End If
        'End Radio9530
    End Sub
    Public Sub SwapDir_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles SwapWorker.RunWorkerCompleted
        Dim Install As String = My.Settings.InstallDir
        If DeviceComboBox.SelectedItem = "9500 (Storm)" Then
            If My.Computer.FileSystem.FileExists(Install & "\CDMA\rim0x06001404OMADRM.sfi") Then
                ToolStripStatusLabel1.Text = "Your Radio has been swapped to " & ComboBox9500.SelectedItem & ". Launch Loader/Desktop Manager to update to your phone."
                MsgBox("Your Radio has been swapped to " & ComboBox9500.SelectedItem & ". Launch Loader/Desktop Manager to update to your phone.")
            End If
        End If
        If DeviceComboBox.SelectedItem = "9530 (Storm)" Then
            If My.Computer.FileSystem.FileExists(Install & "\CDMA\rim0x04001404.sfi") Then
                ToolStripStatusLabel1.Text = "Your Radio has been swapped to " & ComboBox9530.SelectedItem & ". Launch Loader/Desktop Manager to update to your phone."
                MsgBox("Your Radio has been swapped to " & ComboBox9530.SelectedItem & ". Launch Loader/Desktop Manager to update to your phone.")
            End If
        End If
        If DeviceComboBox.SelectedItem = "9630 (Tour)" Then
            If My.Computer.FileSystem.FileExists(Install & "\CDMA\rim0x0D000D04.sfi") Then
                ToolStripStatusLabel1.Text = "Your Radio has been swapped to " & ComboBox9630.SelectedItem & ". Launch Loader/Desktop Manager to update to your phone."
                MsgBox("Your Radio has been swapped to " & ComboBox9630.SelectedItem & ". Launch Loader/Desktop Manager to update to your phone.")
            End If
        End If
        If DeviceComboBox.SelectedItem = "Curve (8330)" Then
            If My.Computer.FileSystem.FileExists(Install & "\CDMA\rim8330c.sfi") Then
                ToolStripStatusLabel1.Text = "Your Radio has been swapped to " & ComboBox8330.SelectedItem & ". Launch Loader/Desktop Manager to update to your phone."
                MsgBox("Your Radio has been swapped to " & ComboBox8330.SelectedItem & ". Launch Loader/Desktop Manager to update to your phone.")
            End If
        End If
        If DeviceComboBox.SelectedItem = "Gemini (8520)" Then
            If My.Computer.FileSystem.FileExists(Install & "\GPRS\rim0x8C000F03.sfi") Then
                ToolStripStatusLabel1.Text = "Your Radio has been swapped to " & ComboBox8520.SelectedItem & ". Launch Loader/Desktop Manager to update to your phone."
                MsgBox("Your Radio has been swapped to " & ComboBox8520.SelectedItem & ". Launch Loader/Desktop Manager to update to your phone.")
            End If
        End If
        If DeviceComboBox.SelectedItem = "9000 (Bold)" Then
            If My.Computer.FileSystem.FileExists(Install & "\UMTS\rim9000.sfi") Then
                ToolStripStatusLabel1.Text = "Your Radio has been swapped to " & ComboBox9000.SelectedItem & ". Launch Loader/Desktop Manager to update to your phone."
                MsgBox("Your Radio has been swapped to " & ComboBox9000.SelectedItem & ". Launch Loader/Desktop Manager to update to your phone.")
            End If
        End If
    End Sub
    Public Sub TabControl1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 5 Then

            If My.Settings.ShowWarning = 0 Then
                Dim msg = "This feature is for Advanced Users Only. Make sure you know what you are doing as this may BRICK your Blackberry. Back-up all of your data before proceeding!" & (Chr(13)) & (Chr(13)) & "If you would like to continue to see this warning each time you select this tab - click 'Yes', otherwise click 'No' to not be warned again"""
                Dim title = "Warning!"
                Dim style = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or _
                            MsgBoxStyle.Critical
                ' Display the message box and save the response, Yes or No.
                Dim response = MsgBox(msg, style, title)

                If response = MsgBoxResult.Yes Then
                    My.Settings.ShowWarning = 0
                Else
                    My.Settings.ShowWarning = 1
                End If
            Else
            End If
        End If
    End Sub
    Public Sub DeviceComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeviceComboBox.SelectedIndexChanged
        If DeviceComboBox.SelectedItem Is "9700 (Bold)" Then
            ComboBox9700.Show()
            ComboBox9520.Hide()
            ComboBox9550.Hide()
            ComboBox9530.Hide()
            ComboBox9500.Hide()
            ComboBox8520.Hide()
            ComboBox8330.Hide()
            ComboBox9000.Hide()
            ComboBox9630.Hide()
            ComboBox8900.Hide()
            KeyboardBox.Enabled = False
            KeyboardTitle.Hide()
        End If
        If DeviceComboBox.SelectedItem Is "9520 (Storm 2)" Then
            ComboBox9520.Show()
            ComboBox9550.Hide()
            ComboBox9530.Hide()
            ComboBox9500.Hide()
            ComboBox8520.Hide()
            ComboBox8330.Hide()
            ComboBox9000.Hide()
            ComboBox9630.Hide()
            ComboBox8900.Hide()
            KeyboardBox.Enabled = True
            KeyboardTitle.Show()
        End If
        If DeviceComboBox.SelectedItem Is "9550 (Storm 2)" Then
            ComboBox9550.Show()
            ComboBox9530.Hide()
            ComboBox9500.Hide()
            ComboBox8520.Hide()
            ComboBox8330.Hide()
            ComboBox9000.Hide()
            ComboBox9630.Hide()
            ComboBox8900.Hide()
            KeyboardBox.Enabled = True
            KeyboardTitle.Show()
        End If
        If DeviceComboBox.SelectedItem Is "9530 (Storm)" Then
            ComboBox9530.Show()
            ComboBox9500.Hide()
            ComboBox8520.Hide()
            ComboBox8330.Hide()
            ComboBox9000.Hide()
            ComboBox9630.Hide()
            ComboBox9700.Hide()
            ComboBox9520.Hide()
            ComboBox9550.Hide()
            ComboBox8900.Hide()
            KeyboardBox.Enabled = True
            KeyboardTitle.Show()
        End If
        If DeviceComboBox.SelectedItem Is "9500 (Storm)" Then
            ComboBox9500.Show()
            ComboBox9530.Hide()
            ComboBox8520.Hide()
            ComboBox8330.Hide()
            ComboBox9000.Hide()
            ComboBox9630.Hide()
            ComboBox9700.Hide()
            ComboBox9520.Hide()
            ComboBox9550.Hide()
            ComboBox8900.Hide()
            KeyboardBox.Enabled = True
            KeyboardTitle.Show()
        End If
        If DeviceComboBox.SelectedItem Is "8900 (Curve)" Then
            ComboBox8900.Show()
            ComboBox9700.Hide()
            ComboBox9520.Hide()
            ComboBox9550.Hide()
            ComboBox9530.Hide()
            ComboBox9500.Hide()
            ComboBox8520.Hide()
            ComboBox8330.Hide()
            ComboBox9000.Hide()
            ComboBox9630.Hide()
            KeyboardBox.Enabled = False
            KeyboardTitle.Hide()
        End If
        If DeviceComboBox.SelectedItem Is "8520 (Gemini)" Then
            ComboBox9530.Hide()
            ComboBox9500.Hide()
            ComboBox8330.Hide()
            ComboBox9000.Hide()
            ComboBox9630.Hide()
            ComboBox8520.Show()
            ComboBox9700.Hide()
            ComboBox9520.Hide()
            ComboBox9550.Hide()
            ComboBox8900.Hide()
            KeyboardBox.Enabled = False
            KeyboardTitle.Hide()
        End If
        If DeviceComboBox.SelectedItem Is "8330 (Curve)" Then
            ComboBox9530.Hide()
            ComboBox9500.Hide()
            ComboBox8520.Hide()
            ComboBox9000.Hide()
            ComboBox9630.Hide()
            ComboBox8330.Show()
            ComboBox9700.Hide()
            ComboBox9520.Hide()
            ComboBox9550.Hide()
            ComboBox8900.Hide()
            KeyboardBox.Enabled = False
            KeyboardTitle.Hide()
        End If
        If DeviceComboBox.SelectedItem Is "9000 (Bold)" Then
            ComboBox9530.Hide()
            ComboBox9500.Hide()
            ComboBox8520.Hide()
            ComboBox8330.Hide()
            ComboBox9630.Hide()
            ComboBox9000.Show()
            ComboBox9700.Hide()
            ComboBox9520.Hide()
            ComboBox9550.Hide()
            ComboBox8900.Hide()
            KeyboardBox.Enabled = False
            KeyboardTitle.Hide()
        End If
        If DeviceComboBox.SelectedItem Is "9630 (Tour)" Then
            ComboBox9530.Hide()
            ComboBox9500.Hide()
            ComboBox8520.Hide()
            ComboBox8330.Hide()
            ComboBox9000.Hide()
            ComboBox9630.Show()
            ComboBox9700.Hide()
            ComboBox9520.Hide()
            ComboBox9550.Hide()
            ComboBox8900.Hide()
            KeyboardBox.Enabled = False
            KeyboardTitle.Hide()
        End If
    End Sub

    Public Sub AppText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AppText.Click
        If AppText.Text = "Application Name" Then
            AppText.Text = ""
        Else
        End If
    End Sub

    Public Sub Description_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Description.Click
        If Description.Text = "Description" Then
            Description.Text = ""
        Else
        End If
    End Sub

    Public Sub VersionText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VersionText.Click
        If VersionText.Text = "Version" Then
            VersionText.Text = ""
        Else
        End If
    End Sub

    Public Sub VendorText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VendorText.Click
        If VendorText.Text = "Vendor" Then
            VendorText.Text = ""
        Else
        End If
    End Sub

    Public Sub FileText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileText.Click
        If FileText.Text = "File Name" Then
            FileText.Text = ""
        Else
        End If
    End Sub

    Public Sub SaveTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveTo.Click
        If Save2Shrink.Checked = True Then
            SaveTo.Text = OSFolder.Text
            My.Settings.CaJDir = My.Settings.InstallDir
        ElseIf SaveTo.Text = "Browse for Save To folder..." Then
            SaveTo.Text = ""
        End If
    End Sub
    Public Sub StartShrink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartShrink.Click
        TabControl1.SelectedIndex = 1
    End Sub
    Public Sub StartBB10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartBB10.Click
        TabControl1.SelectedIndex = 2
    End Sub
    Public Sub StartCAJ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartCAJ.Click
        TabControl1.SelectedIndex = 3
    End Sub
    Public Sub StartOTA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartOTA.Click
        TabControl1.SelectedIndex = 4
    End Sub
    Public Sub StartBaH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartBaH.Click
        TabControl1.SelectedIndex = 5
    End Sub
    Public Sub StartTools_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartTools.Click
        TabControl1.SelectedIndex = 6
    End Sub
    Public Sub StartPlayBook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartPlayBook.Click
        TabControl1.SelectedIndex = 7
    End Sub
    Public Sub StartOther_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TabControl1.SelectedIndex = 8
    End Sub
    Public Sub BBSAK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If My.Computer.FileSystem.FileExists("C:\Program Files (x86)\BBSAK\BBSAK.exe") Then
            Shell("C:\Program Files (x86)\BBSAK\BBSAK.exe")
        ElseIf My.Computer.FileSystem.FileExists("C:\Program Files\BBSAK\BBSAK.exe") Then
            Shell("C:\Program Files\BBSAK\BBSAK.exe")
        ElseIf My.Computer.FileSystem.FileExists("D:\Program Files (x86)\BBSAK\BBSAK.exe") Then
            Shell("D:\Program Files (x86)\BBSAK\BBSAK.exe")
        ElseIf My.Computer.FileSystem.FileExists("D:\Program Files\BBSAK\BBSAK.exe") Then
            Shell("D:\Program Files\BBSAK\BBSAK.exe")
        ElseIf My.Computer.FileSystem.FileExists("E:\Program Files\BBSAK\BBSAK.exe") Then
            Shell("E:\Program Files\BBSAK\BBSAK.exe")
        ElseIf My.Computer.FileSystem.FileExists("E:\Program Files (x86)\BBSAK\BBSAK.exe") Then
            Shell("E:\Program Files (x86)\BBSAK\BBSAK.exe")
        ElseIf My.Computer.FileSystem.FileExists("F:\Program Files\BBSAK\BBSAK.exe") Then
            Shell("F:\Program Files\BBSAK\BBSAK.exe")
        Else
            Dim msg = "You do not have BBSAK installed. Would you like to download it?"
            Dim title = "Download BBSAK?"
            Dim style = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or _
                        MsgBoxStyle.Critical
            ' Display the message box and save the response, Yes or No.
            Dim response = MsgBox(msg, style, title)

            If response = MsgBoxResult.Yes Then
                System.Diagnostics.Process.Start("http://bbsak.org/download.php")
            Else
            End If
        End If
    End Sub
    Public Sub FindLatestHybrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = "Checking for Latest " & DeviceComboBox.SelectedItem & " Hybrid..."
        If My.Computer.FileSystem.FileExists(My.Settings.InstallDir & "\Hybrids.txt") Then
            System.IO.File.Delete(My.Settings.InstallDir & "\Hybrids.txt")
            My.Computer.Network.DownloadFile _
            ("http://www.theiexplorers.com/BBHTool/Hybrids.txt", My.Settings.InstallDir & "\Hybrids.txt")
            Sleep(500)
        Else
            My.Computer.Network.DownloadFile _
            ("http://www.theiexplorers.com/BBHTool/Hybrids.txt", My.Settings.InstallDir & "\Hybrids.txt")
            Sleep(500)
        End If
        If DeviceComboBox.SelectedItem Is "9800 (Torch)" Then
            Call GrabLatest(9800, "Hybrids.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9780 (Bold)" Then
            Call GrabLatest(9780, "Hybrids.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9700 (Bold)" Then
            Call GrabLatest(9700, "Hybrids.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9670 (Style)" Then
            Call GrabLatest(9670, "Hybrids.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9650 (Bold)" Then
            Call GrabLatest(9650, "Hybrids.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9630 (Tour)" Then
            Call GrabLatest(9630, "Hybrids.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9550 (Storm 2)" Then
            Call GrabLatest(9550, "Hybrids.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9530 (Storm)" Then
            Call GrabLatest(9530, "Hybrids.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9520 (Storm 2)" Then
            Call GrabLatest(9520, "Hybrids.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9500 (Storm)" Then
            Call GrabLatest(9500, "Hybrids.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9330 (Curve 3G)" Then
            Call GrabLatest(9330, "Hybrids.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9300 (Curve 3G)" Then
            Call GrabLatest(9300, "Hybrids.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9105/9100 (Pearl 3G)" Then
            Call GrabLatest(910, "Hybrids.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9000 (Bold)" Then
            Call GrabLatest(9000, "Hybrids.txt")
        End If
        If DeviceComboBox.SelectedItem Is "8900 (Curve)" Then
            Call GrabLatest(8900, "Hybrids.txt")
        End If
        If DeviceComboBox.SelectedItem Is "8530 (Curve)" Then
            Call GrabLatest(8530, "Hybrids.txt")
        End If
        If DeviceComboBox.SelectedItem Is "8520 (Curve)" Then
            Call GrabLatest(8520, "Hybrids.txt")
        End If
        If DeviceComboBox.SelectedItem Is "8330 (Curve)" Then
            Call GrabLatest(8330, "Hybrids.txt")
        End If
    End Sub
    Sub GrabLatest(ByVal Device As String, ByVal GrabType As String)
        If GrabType = "Hybrids.txt" Then
            Try
                ' Create an instance of StreamReader to read from a file.
                Using hybrid As StreamReader = New StreamReader(My.Settings.InstallDir & "\" & GrabType)
                    Dim line As String
                    Dim DownloadLink As String
                    ' Read and display the lines from the file until the end 
                    ' of the file is reached.
                    Do
                        line = hybrid.ReadLine()
                        DownloadLink = ""
                        If line.Contains(Device) Then
                            For Each X As String In line.Split("|")
                                If X.Contains("http://") Then
                                    DownloadLink = X
                                Else
                                    line = X
                                End If
                            Next
                            Dim msg1 = "Latest " & line & " available at BBH-Plus.net. Would you like to download it?"
                            Dim title1 = "Download Latest " & Device & " Hybrid?"
                            Dim style1 = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or _
                                        MsgBoxStyle.Information
                            ' Display the message box and save the response, Yes or No.
                            Dim response1 = MsgBox(msg1, style1, title1)
                            If response1 = MsgBoxResult.Yes Then
                                System.Diagnostics.Process.Start(DownloadLink)
                            Else
                            End If
                            ToolStripStatusLabel1.Text = "Latest " & line & " at BBH-Plus.net"
                        Else
                        End If
                    Loop Until line.Contains(Device)
                    hybrid.Close()
                End Using
            Catch V As Exception
                ' Let the user know what went wrong.
                MsgBox("The latest Hybrid could not be read...", MsgBoxStyle.Critical)
            End Try
        ElseIf GrabType = "LatestOS.txt" Then
            Try
                ' Create an instance of StreamReader to read from a file.
                Using latestOS As StreamReader = New StreamReader(My.Settings.InstallDir & "\" & GrabType)
                    Dim line As String
                    ' Read and display the lines from the file until the end 
                    ' of the file is reached.
                    Do
                        line = latestOS.ReadLine()
                        If line.Contains(Device) Then
                            ToolStripStatusLabel1.Text = "The latest OS available for the " & line
                            MsgBox("The latest OS available for the " & line, MsgBoxStyle.Information)
                        Else
                        End If
                    Loop Until line.Contains(Device)
                    latestOS.Close()
                End Using
            Catch V As Exception
                ' Let the user know what went wrong.
                MsgBox("The latest OS could not be read...", MsgBoxStyle.Critical)
            End Try
        End If
        System.IO.File.Delete(My.Settings.InstallDir & "\" & GrabType)
        ToolStripStatusLabel1.Text = WelcomeText
    End Sub
    Private Sub FindLatestOS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = "Checking for Latest " & DeviceComboBox.SelectedItem & " Operating System..."
        If DeviceComboBox.SelectedItem Is "9800 (Torch)" Then
            Call GrabLatest(9800, "LatestOS.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9780 (Bold)" Then
            Call GrabLatest(9780, "LatestOS.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9700 (Bold)" Then
            Call GrabLatest(9700, "LatestOS.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9670 (Style)" Then
            Call GrabLatest(9670, "LatestOS.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9650 (Bold)" Then
            Call GrabLatest(9650, "LatestOS.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9630 (Tour)" Then
            Call GrabLatest(9630, "LatestOS.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9550 (Storm 2)" Then
            Call GrabLatest(9550, "LatestOS.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9530 (Storm)" Then
            Call GrabLatest(9530, "LatestOS.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9520 (Storm 2)" Then
            Call GrabLatest(9520, "LatestOS.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9500 (Storm)" Then
            Call GrabLatest(9500, "LatestOS.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9330 (Curve 3G)" Then
            Call GrabLatest(9330, "LatestOS.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9300 (Curve 3G)" Then
            Call GrabLatest(9300, "LatestOS.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9105/9100 (Pearl 3G)" Then
            Call GrabLatest(9105, "LatestOS.txt")
        End If
        If DeviceComboBox.SelectedItem Is "9000 (Bold)" Then
            Call GrabLatest(9000, "LatestOS.txt")
        End If
        If DeviceComboBox.SelectedItem Is "8900 (Curve)" Then
            Call GrabLatest(8900, "LatestOS.txt")
        End If
        If DeviceComboBox.SelectedItem Is "8530 (Curve)" Then
            Call GrabLatest(8530, "LatestOS.txt")
        End If
        If DeviceComboBox.SelectedItem Is "8520 (Curve)" Then
            Call GrabLatest(8520, "LatestOS.txt")
        End If
        If DeviceComboBox.SelectedItem Is "8330 (Curve)" Then
            Call GrabLatest(8330, "LatestOS.txt")
        End If
    End Sub

    '///////////////////
    'Create-A-JAD START
    '///////////////////
    Public Sub M_OnKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If TabControl1.SelectedIndex = 1 Then
            Me.AcceptButton = Shrink
        ElseIf TabControl1.SelectedIndex = 2 Then
            Me.AcceptButton = Create
        ElseIf TabControl1.SelectedIndex = 3 Then
            Me.AcceptButton = BuildHybrid
        End If
    End Sub
    Public Sub FileBox_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles FileBox.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub
    Public Sub FileBox_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles FileBox.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then

            Dim Files() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())

            'Determine whether the file is a .cod or not
            Dim fileDetail As New System.IO.FileInfo(Files(0))
            If fileDetail.Extension = ".cod" Then

                'Set the cursors etc for file copying
                e.Effect = DragDropEffects.All
                Dim MyFiles() As String
                Dim i As Integer
                Dim infoReader As System.IO.FileInfo

                ' Assign the files to an array.
                MyFiles = e.Data.GetData(DataFormats.FileDrop)
                ' Loop through the array and add the files to the list.
                DragCOD.Hide()
                FilePathBox.Items.Clear()
                Alx2Jadbox.Items.Clear()
                For i = 0 To MyFiles.Length - 1
                    Dim dirName As String = IO.Path.GetFileName(MyFiles(i))
                    infoReader = My.Computer.FileSystem.GetFileInfo(MyFiles(i))
                    FilePathBox.Items.Add(MyFiles(i))
                    ToolStripStatusLabel1.Text = dirName & " is being added to the file list..."
                    FileBox.Items.Add(dirName)
                    ToolStripStatusLabel1.Text = "Detecting size of " & dirName & "..."
                    SizeBox.Items.Add(infoReader.Length)
                    Dim FileCounts = FileBox.Items.Count.ToString
                    FileCount.Text = "Total Files: " & FileCounts
                    ToolStripStatusLabel1.Text = WelcomeText
                Next
            ElseIf fileDetail.Extension = ".alx" Then

                'Set the cursors etc for file copying
                e.Effect = DragDropEffects.All
                Dim MyFiles() As String
                Dim i As Integer
                Dim infoReader As System.IO.FileInfo

                ' Assign the files to an array.
                MyFiles = e.Data.GetData(DataFormats.FileDrop)
                ' Loop through the array and add the files to the list.
                For i = 0 To MyFiles.Length - 1
                    FilePathBox.Items.Clear()
                    Alx2Jadbox.Items.Clear()
                    AppText.Text = ""
                    Description.Text = ""
                    VersionText.Text = ""
                    VendorText.Text = ""
                    Dim dirName As String = IO.Path.GetFileName(MyFiles(i))
                    infoReader = My.Computer.FileSystem.GetFileInfo(MyFiles(i))
                    ToolStripStatusLabel1.Text = dirName & "'s info being input for you..."
                    FilePathBox.Items.Add(MyFiles(i))
                    Dim AlxFile As String = (MyFiles(i))
                    Dim reader As XmlTextReader = New XmlTextReader(AlxFile)
                    Do While (reader.Read())
                        Select Case reader.NodeType
                            Case XmlNodeType.Element 'Display beginning of element.
                                If reader.HasAttributes Then 'If attributes exist
                                End If
                            Case XmlNodeType.Text 'Display the text in each element.
                                Alx2Jadbox.Items.Add(reader.Value.Trim)
                            Case XmlNodeType.EndElement 'Display end of element.
                        End Select
                    Loop
                    AppText.Text = Alx2Jadbox.Items(0)
                    Description.Text = Alx2Jadbox.Items(1)
                    VersionText.Text = Alx2Jadbox.Items(2)
                    VendorText.Text = Alx2Jadbox.Items(3)
                    FilePathBox.Items.Clear()
                    Dim Lines() As String = System.IO.File.ReadAllLines(MyFiles(i))
                    For Each line In Lines
                        If AddCODsToList.Checked = True Then
                            If line.Contains(".cod") = True Then
                                Dim parsedline As String
                                Dim parsedline1 As String
                                Dim CODFile As String
                                ' Relace all occurances of a substring, case sensitive
                                parsedline = line.TrimStart
                                DragCOD.Hide()
                                For x = 0 To MyFiles.Length - 1
                                    infoReader = My.Computer.FileSystem.GetFileInfo(MyFiles(x))
                                    parsedline = Replace(line, "<files>", "")
                                    parsedline1 = Replace(parsedline, "</files>", "")
                                    CODFile = Replace(parsedline1, "</fileset>", "")
                                    ToolStripStatusLabel1.Text = CODFile.TrimStart & " is being added to the file list..."
                                    FileBox.Items.Add(CODFile.TrimStart)
                                    Dim FileCounts = FileBox.Items.Count.ToString
                                    FileCount.Text = "Total Files: " & FileCounts
                                    ToolStripStatusLabel1.Text = WelcomeText
                                Next
                            End If
                        End If
                    Next
                    ToolStripStatusLabel1.Text = WelcomeText
                Next
            ElseIf fileDetail.Extension = ".jad" Then
                'Set the cursors etc for file copying
                e.Effect = DragDropEffects.All
                Dim MyFiles() As String
                Dim i As Integer
                Dim infoReader As System.IO.FileInfo

                ' Assign the files to an array.
                MyFiles = e.Data.GetData(DataFormats.FileDrop)
                ' Loop through the array and add the files to the list.
                For i = 0 To MyFiles.Length - 1
                    AppText.Text = ""
                    Description.Text = ""
                    VersionText.Text = ""
                    VendorText.Text = ""
                    Dim dirName As String = IO.Path.GetFileName(MyFiles(i))
                    infoReader = My.Computer.FileSystem.GetFileInfo(MyFiles(i))
                    ToolStripStatusLabel1.Text = dirName & "'s info being input for you..."
                    FilePathBox.Items.Add(MyFiles(i))
                    Dim JadFile As String = (MyFiles(i))
                    Dim reader As XmlTextReader = New XmlTextReader(JadFile)
                    Dim Lines() As String = System.IO.File.ReadAllLines(MyFiles(i))
                    For Each line In Lines
                        If line.StartsWith("MIDlet-Name:") = True Then
                            Dim parsedline As String
                            ' Relace all occurances of a substring, case sensitive
                            parsedline = Replace(line, "MIDlet-Name: ", "")
                            AppText.Text &= parsedline
                        End If
                        If line.StartsWith("MIDlet-Description:") = True Then
                            Dim parsedline As String
                            ' Relace all occurances of a substring, case sensitive
                            parsedline = Replace(line, "MIDlet-Description: ", "")
                            Description.Text &= parsedline
                        End If
                        If line.StartsWith("MIDlet-Version:") = True Then
                            Dim parsedline As String
                            ' Relace all occurances of a substring, case sensitive
                            parsedline = Replace(line, "MIDlet-Version: ", "")
                            VersionText.Text &= parsedline
                        End If
                        If line.StartsWith("MIDlet-Vendor:") = True Then
                            Dim parsedline As String
                            ' Relace all occurances of a substring, case sensitive
                            parsedline = Replace(line, "MIDlet-Vendor: ", "")
                            VendorText.Text &= parsedline
                        End If
                        If AddCODsToList.Checked = True Then
                            If line.StartsWith("RIM-COD-Size-") = True Then
                                'Relace all occurances of a substring, case sensitive
                                line = line.Replace("RIM-COD-Size-", "")
                                line = line.TrimStart
                                Dim filenum As Integer
                                For filenum = 1 To 99
                                    line = line.Replace(filenum & filenum & ": ", "")
                                    If line.StartsWith(filenum) Then
                                        line = line.Replace(filenum & ": ", "")
                                    End If
                                Next
                                SizeBox.Items.Add(line)
                            End If

                            If line.StartsWith("RIM-COD-URL-") = True Then
                                ' Relace all occurances of a substring, case sensitive
                                line = line.Replace("RIM-COD-URL-", "")
                                line = line.TrimStart
                                Dim filenum As Integer
                                For filenum = 0 To 99
                                    line = line.Replace(filenum & filenum & ": ", "")
                                    If line.StartsWith(filenum) Then
                                        line = line.Replace(filenum & ": ", "")
                                    End If
                                Next
                                DragCOD.Hide()
                                For x = 0 To MyFiles.Length - 1
                                    infoReader = My.Computer.FileSystem.GetFileInfo(MyFiles(x))
                                    ToolStripStatusLabel1.Text = line & " is being added to the file list..."
                                    FileBox.Items.Add(line)
                                    Dim FileCounts = FileBox.Items.Count.ToString
                                    FileCount.Text = "Total Files: " & FileCounts
                                    ToolStripStatusLabel1.Text = WelcomeText
                                Next
                            End If
                        End If
                    Next
                Next
                ToolStripStatusLabel1.Text = WelcomeText
            Else
                MsgBox("Files must be .cod to create an Install OR an .alx/.jad to auto-enter the info!")
            End If
        End If
    End Sub
    Public Sub DragCOD_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DragCOD.DragEnter

        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub
    Public Sub DragCOD_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DragCOD.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then

            Dim Files() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())

            'Determine whether the file is a .cod or not
            Dim fileDetail As New System.IO.FileInfo(Files(0))
            If fileDetail.Extension = ".cod" Then
                'Set the cursors etc for file copying
                e.Effect = DragDropEffects.All
                Dim MyFiles() As String
                Dim i As Integer
                Dim infoReader As System.IO.FileInfo

                ' Assign the files to an array.
                MyFiles = e.Data.GetData(DataFormats.FileDrop)
                ' Loop through the array and add the files to the list.
                DragCOD.Hide()
                For i = 0 To MyFiles.Length - 1
                    Dim dirName As String = IO.Path.GetFileName(MyFiles(i))
                    infoReader = My.Computer.FileSystem.GetFileInfo(MyFiles(i))
                    ToolStripStatusLabel1.Text = dirName & " is being added to the file list..."
                    FileBox.Items.Add(dirName)
                    ToolStripStatusLabel1.Text = "Detecting size of " & dirName & "..."
                    SizeBox.Items.Add(infoReader.Length)
                    FileCount.Text = "Total Files: " & (i) + 1
                    ToolStripStatusLabel1.Text = WelcomeText
                Next
            ElseIf fileDetail.Extension = ".alx" Then
                'Set the cursors etc for file copying
                e.Effect = DragDropEffects.All
                Dim MyFiles() As String
                Dim i As Integer
                Dim infoReader As System.IO.FileInfo

                ' Assign the files to an array.
                MyFiles = e.Data.GetData(DataFormats.FileDrop)
                ' Loop through the array and add the files to the list.
                For i = 0 To MyFiles.Length - 1
                    FilePathBox.Items.Clear()
                    Alx2Jadbox.Items.Clear()
                    AppText.Text = ""
                    Description.Text = ""
                    VersionText.Text = ""
                    VendorText.Text = ""
                    Dim dirName As String = IO.Path.GetFileName(MyFiles(i))
                    infoReader = My.Computer.FileSystem.GetFileInfo(MyFiles(i))
                    ToolStripStatusLabel1.Text = dirName & "'s info being input for you..."
                    FilePathBox.Items.Add(MyFiles(i))
                    Dim AlxFile As String = (MyFiles(i))
                    Dim reader As XmlTextReader = New XmlTextReader(AlxFile)
                    Do While (reader.Read())
                        Select Case reader.NodeType
                            Case XmlNodeType.Element 'Display beginning of element.
                                If reader.HasAttributes Then 'If attributes exist
                                End If
                            Case XmlNodeType.Text 'Display the text in each element.
                                Alx2Jadbox.Items.Add(reader.Value)
                            Case XmlNodeType.EndElement 'Display end of element.
                        End Select
                    Loop
                    AppText.Text = Alx2Jadbox.Items(0)
                    Description.Text = Alx2Jadbox.Items(1)
                    VersionText.Text = Alx2Jadbox.Items(2)
                    VendorText.Text = Alx2Jadbox.Items(3)
                    FilePathBox.Items.Clear()
                    Dim Lines() As String = System.IO.File.ReadAllLines(MyFiles(i))
                    For Each line In Lines
                        If line.StartsWith("MIDlet-Name:") = True Then
                            Dim parsedline As String
                            ' Relace all occurances of a substring, case sensitive
                            parsedline = Replace(line, "MIDlet-Name: ", "")
                            AppText.Text &= parsedline
                        End If
                        If line.StartsWith("MIDlet-Description:") = True Then
                            Dim parsedline As String
                            ' Relace all occurances of a substring, case sensitive
                            parsedline = Replace(line, "MIDlet-Description: ", "")
                            Description.Text &= parsedline
                        End If
                        If line.StartsWith("MIDlet-Version:") = True Then
                            Dim parsedline As String
                            ' Relace all occurances of a substring, case sensitive
                            parsedline = Replace(line, "MIDlet-Version: ", "")
                            VersionText.Text &= parsedline
                        End If
                        If line.StartsWith("MIDlet-Vendor:") = True Then
                            Dim parsedline As String
                            ' Relace all occurances of a substring, case sensitive
                            parsedline = Replace(line, "MIDlet-Vendor: ", "")
                            VendorText.Text &= parsedline
                        End If
                        If AddCODsToList.Checked = True Then
                            If line.Contains(".cod") = True Then
                                Dim parsedline As String
                                Dim parsedline1 As String
                                Dim CODFile As String
                                ' Relace all occurances of a substring, case sensitive
                                parsedline = line.TrimStart
                                DragCOD.Hide()
                                For x = 0 To MyFiles.Length - 1
                                    infoReader = My.Computer.FileSystem.GetFileInfo(MyFiles(x))
                                    parsedline = Replace(line, "<files>", "")
                                    parsedline1 = Replace(parsedline, "</files>", "")
                                    CODFile = Replace(parsedline1, "</fileset>", "")
                                    ToolStripStatusLabel1.Text = CODFile.TrimStart & " is being added to the file list..."
                                    FileBox.Items.Add(CODFile.TrimStart)
                                    Dim FileCounts = FileBox.Items.Count.ToString
                                    FileCount.Text = "Total Files: " & FileCounts
                                    ToolStripStatusLabel1.Text = WelcomeText
                                Next
                            End If
                        End If
                    Next
                    ToolStripStatusLabel1.Text = WelcomeText
                Next
            ElseIf fileDetail.Extension = ".jad" Then
                'Set the cursors etc for file copying
                e.Effect = DragDropEffects.All
                Dim MyFiles() As String
                Dim i As Integer
                Dim infoReader As System.IO.FileInfo

                ' Assign the files to an array.
                MyFiles = e.Data.GetData(DataFormats.FileDrop)
                ' Loop through the array and add the files to the list.
                For i = 0 To MyFiles.Length - 1
                    AppText.Text = ""
                    Description.Text = ""
                    VersionText.Text = ""
                    VendorText.Text = ""
                    Dim dirName As String = IO.Path.GetFileName(MyFiles(i))
                    infoReader = My.Computer.FileSystem.GetFileInfo(MyFiles(i))
                    ToolStripStatusLabel1.Text = dirName & "'s info being input for you..."
                    FilePathBox.Items.Add(MyFiles(i))
                    Dim JadFile As String = (MyFiles(i))
                    Dim reader As XmlTextReader = New XmlTextReader(JadFile)
                    Dim Lines() As String = System.IO.File.ReadAllLines(MyFiles(i))
                    For Each line In Lines
                        If line.StartsWith("MIDlet-Name:") = True Then
                            Dim parsedline As String
                            ' Relace all occurances of a substring, case sensitive
                            parsedline = Replace(line, "MIDlet-Name: ", "")
                            AppText.Text &= parsedline
                        End If
                        If line.StartsWith("MIDlet-Description:") = True Then
                            Dim parsedline As String
                            ' Relace all occurances of a substring, case sensitive
                            parsedline = Replace(line, "MIDlet-Description: ", "")
                            Description.Text &= parsedline
                        End If
                        If line.StartsWith("MIDlet-Version:") = True Then
                            Dim parsedline As String
                            ' Relace all occurances of a substring, case sensitive
                            parsedline = Replace(line, "MIDlet-Version: ", "")
                            VersionText.Text &= parsedline
                        End If
                        If line.StartsWith("MIDlet-Vendor:") = True Then
                            Dim parsedline As String
                            ' Relace all occurances of a substring, case sensitive
                            parsedline = Replace(line, "MIDlet-Vendor: ", "")
                            VendorText.Text &= parsedline
                        End If
                        If AddCODsToList.Checked = True Then
                            If line.StartsWith("RIM-COD-Size-") = True Then
                                'Relace all occurances of a substring, case sensitive
                                line = line.Replace("RIM-COD-Size-", "")
                                line = line.TrimStart
                                Dim filenum As Integer
                                For filenum = 1 To 99
                                    line = line.Replace(filenum & filenum & ": ", "")
                                    If line.StartsWith(filenum) Then
                                        line = line.Replace(filenum & ": ", "")
                                    End If
                                Next
                                SizeBox.Items.Add(line)
                            End If
                            If line.StartsWith("RIM-COD-URL-") = True Then
                                ' Relace all occurances of a substring, case sensitive
                                line = line.Replace("RIM-COD-URL-", "")
                                line = line.TrimStart
                                Dim filenum As Integer
                                For filenum = 0 To 99
                                    line = line.Replace(filenum & filenum & ": ", "")
                                    If line.StartsWith(filenum) Then
                                        line = line.Replace(filenum & ": ", "")
                                    End If
                                Next
                                DragCOD.Hide()
                                For x = 0 To MyFiles.Length - 1
                                    infoReader = My.Computer.FileSystem.GetFileInfo(MyFiles(x))

                                    ToolStripStatusLabel1.Text = line & " is being added to the file list..."
                                    FileBox.Items.Add(line)
                                    Dim FileCounts = FileBox.Items.Count.ToString
                                    FileCount.Text = "Total Files: " & FileCounts
                                    ToolStripStatusLabel1.Text = WelcomeText
                                Next
                            End If
                        End If
                    Next
                Next
                ToolStripStatusLabel1.Text = WelcomeText
            Else
                MsgBox("Files must be .cod to create an Install OR an .alx/.jad to auto enter the info!")
            End If

        End If
    End Sub
    Public Sub JavaLoaderBoxDragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles JavaLoaderBox.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub
    Public Sub JavaLoaderBoxDragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles JavaLoaderBox.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            If InstallCODonDrag.Checked = True Then
                If UserPIN.Text = "Not Connected" Then
                    MsgBox("No device connected to install .COD files to!")
                Else
                    Dim Files() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
                    'Determine whether the file is a .cod or not
                    Dim fileDetail As New System.IO.FileInfo(Files(0))
                    If fileDetail.Extension = ".cod" Then
                        'Set the cursors etc for file copying
                        e.Effect = DragDropEffects.All
                        Dim MyFiles() As String
                        Dim i As Integer
                        'Dim infoReader As System.IO.FileInfo
                        ' Assign the files to an array.
                        MyFiles = e.Data.GetData(DataFormats.FileDrop)
                        ' Loop through the array and add the files to the list.
                        JavaLoaderProgress.Value = 0
                        JavaLoaderProgress.Maximum = MyFiles.Length - 1
                        filelist = ""
                        For i = 0 To MyFiles.Length - 1
                            ToolStripStatusLabel1.Text = "Installing .COD(s)..."
                            filelist = filelist & """" & MyFiles(i) & """ "
                            JavaLoaderProgress.PerformStep()
                        Next
                        Dim myProcess As New Process()
                        Dim myProcessStartInfo As New ProcessStartInfo("JavaLoader.exe", " -w" & DevicePass.Text & " load " & filelist)
                        myProcessStartInfo.UseShellExecute = False
                        myProcessStartInfo.CreateNoWindow = True
                        myProcessStartInfo.RedirectStandardOutput = True
                        myProcess.StartInfo = myProcessStartInfo
                        myProcess.Start()
                        myProcess.Close()
                        ToolStripStatusLabel1.Text = "Dragged .COD(s) have been installed to the connected device. Enjoy!"
                        MsgBox("Dragged .COD(s) have been installed to the connected device. Enjoy!")
                    End If
                End If
            End If
        End If
    End Sub
    Public Sub DataGridView1DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub
    Public Sub DataGridView1DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DataGridView1.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            If InstallCODonDrag.Checked = True Then
                If UserPIN.Text = "Not Connected" Then
                    MsgBox("No device connected to install .COD files to!")
                Else
                    Dim Files() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
                    'Determine whether the file is a .cod or not
                    Dim fileDetail As New System.IO.FileInfo(Files(0))
                    If fileDetail.Extension = ".cod" Then
                        'Set the cursors etc for file copying
                        e.Effect = DragDropEffects.All
                        Dim MyFiles() As String
                        Dim i As Integer
                        'Dim infoReader As System.IO.FileInfo
                        ' Assign the files to an array.
                        MyFiles = e.Data.GetData(DataFormats.FileDrop)
                        ' Loop through the array and add the files to the list.
                        JavaLoaderProgress.Value = 0
                        JavaLoaderProgress.Maximum = MyFiles.Length - 1
                        filelist = ""
                        For i = 0 To MyFiles.Length - 1
                            ToolStripStatusLabel1.Text = "Installing .COD(s)..."
                            filelist = filelist & """" & MyFiles(i) & """ "
                            JavaLoaderProgress.PerformStep()
                        Next
                        Dim myProcess As New Process()
                        Dim myProcessStartInfo As New ProcessStartInfo("JavaLoader.exe", " -w" & DevicePass.Text & " load " & filelist)
                        myProcessStartInfo.UseShellExecute = False
                        myProcessStartInfo.CreateNoWindow = True
                        myProcessStartInfo.RedirectStandardOutput = True
                        myProcess.StartInfo = myProcessStartInfo
                        myProcess.Start()
                        myProcess.Close()
                        ToolStripStatusLabel1.Text = "Dragged .COD(s) have been installed to the connected device. Enjoy!"
                        MsgBox("Dragged .COD(s) have been installed to the connected device. Enjoy!")
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub PBBoxDragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PBBox.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub
    Public Sub PBBoxDragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles PBBox.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim Files() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
            'Determine whether the file is a .bar or not
            Dim fileDetail As New System.IO.FileInfo(Files(0))
            If fileDetail.Extension = ".bar" Then
                If InstallFileOnDrag.Checked = True Then
                    PlayBookProgressBar.MarqueeAnimationSpeed = 20
                Else
                End If
                'Set the cursors etc for file copying
                e.Effect = DragDropEffects.All
                Dim MyFiles() As String
                Dim i As Integer
                'Dim infoReader As System.IO.FileInfo
                ' Assign the files to an array.
                MyFiles = e.Data.GetData(DataFormats.FileDrop)
                ' Loop through the array and add the files to the list.
                DragDropBar.Hide()
                For i = 0 To MyFiles.Length - 1
                    Dim BarFileName As String = IO.Path.GetFileName(MyFiles(i))
                    'infoReader = My.Computer.FileSystem.GetFileInfo(MyFiles(i))
                    'SizeBox.Items.Add(infoReader.Length)
                    ToolStripStatusLabel1.Text = BarFileName & " is being added to the file list..."
                    PBBox.Items.Add(MyFiles(i))
                    If InstallFileOnDrag.Checked = True Then
                        Call AltInstallBar("""" & MyFiles(i) & """")
                    End If
                    'ToolStripStatusLabel1.Text = "Detecting size of " & BarFileName & "..."
                    'Dim FileCounts = PBBox.Items.Count.ToString
                    FilesGroupBox.Text = "Files (" & PBBox.Items.Count & "):"
                Next
            ElseIf fileDetail.Extension = ".apk" Then
                If ConvertAPKOption.Checked = True Then
                    'Set the cursors etc for file copying
                    e.Effect = DragDropEffects.All
                    Dim MyFiles() As String
                    Dim i As Integer
                    'Dim infoReader As System.IO.FileInfo
                    ' Assign the files to an array.
                    MyFiles = e.Data.GetData(DataFormats.FileDrop)
                    ' Loop through the array and add the files to the list.
                    APKConverter.DragAPK.Hide()
                    APKConverter.FileBox.Items.Clear()
                    For i = 0 To MyFiles.Length - 1
                        APKConverter.FileBox.Items.Add(MyFiles(i))
                        APKConverter.ToolStripStatusLabel1.Text = "Adding .APKs..."
                        APKConverter.FilesGroupBox.Text = "Files (" & APKConverter.FileBox.Items.Count & "):"
                    Next
                    APKConverter.PlayBookIPText = PlayBookIP.Text
                    APKConverter.PlayBookPassText = PlayBookPassword.Text
                    APKConverter.CheckAllBarsApks.Text = "Un-Check All"
                    For x As Integer = 0 To APKConverter.FileBox.Items.Count - 1
                        APKConverter.FileBox.SetItemChecked(x, True)
                    Next
                    APKConverter.Show()
                Else
                    e.Effect = DragDropEffects.All
                    Dim MyFiles() As String
                    Dim i As Integer
                    'Dim infoReader As System.IO.FileInfo
                    ' Assign the files to an array.
                    MyFiles = e.Data.GetData(DataFormats.FileDrop)
                    ' Loop through the array and add the files to the list.
                    DragDropBar.Hide()
                    For i = 0 To MyFiles.Length - 1
                        Dim ApkFileName As String = IO.Path.GetFileName(MyFiles(i))
                        'infoReader = My.Computer.FileSystem.GetFileInfo(MyFiles(i))
                        'SizeBox.Items.Add(infoReader.Length)
                        ToolStripStatusLabel1.Text = ApkFileName & " is being added to the file list..."
                        PBBox.Items.Add(MyFiles(i))
                        'ToolStripStatusLabel1.Text = "Detecting size of " & BarFileName & "..."
                        'Dim FileCounts = PBBox.Items.Count.ToString
                        FilesGroupBox.Text = "Files (" & PBBox.Items.Count & "):"
                    Next
                End If
            End If
        End If
        ToolStripStatusLabel1.Text = WelcomeText
    End Sub
    Public Sub DragDropBarDragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DragDropBar.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub
    Public Sub DragDropBarDragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DragDropBar.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim Files() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
            'Determine whether the file is a .bar or not
            Dim fileDetail As New System.IO.FileInfo(Files(0))
            If fileDetail.Extension = ".bar" Then
                If InstallFileOnDrag.Checked = True Then
                    PlayBookProgressBar.MarqueeAnimationSpeed = 20
                Else
                End If
                'Set the cursors etc for file copying
                e.Effect = DragDropEffects.All
                Dim MyFiles() As String
                Dim i As Integer
                'Dim infoReader As System.IO.FileInfo
                ' Assign the files to an array.
                MyFiles = e.Data.GetData(DataFormats.FileDrop)
                ' Loop through the array and add the files to the list.
                DragDropBar.Hide()
                For i = 0 To MyFiles.Length - 1
                    Dim BarFileName As String = IO.Path.GetFileName(MyFiles(i))
                    'infoReader = My.Computer.FileSystem.GetFileInfo(MyFiles(i))
                    'SizeBox.Items.Add(infoReader.Length)
                    ToolStripStatusLabel1.Text = BarFileName & " is being added to the file list..."
                    PBBox.Items.Add(MyFiles(i))
                    If InstallFileOnDrag.Checked = True Then
                        Call AltInstallBar("""" & MyFiles(i) & """")
                    End If
                    'ToolStripStatusLabel1.Text = "Detecting size of " & BarFileName & "..."
                    'Dim FileCounts = PBBox.Items.Count.ToString
                    FilesGroupBox.Text = "Files (" & PBBox.Items.Count & "):"
                Next
            ElseIf fileDetail.Extension = ".apk" Then
                If ConvertAPKOption.Checked = True Then
                    'Set the cursors etc for file copying
                    e.Effect = DragDropEffects.All
                    Dim MyFiles() As String
                    Dim i As Integer
                    'Dim infoReader As System.IO.FileInfo
                    ' Assign the files to an array.
                    MyFiles = e.Data.GetData(DataFormats.FileDrop)
                    ' Loop through the array and add the files to the list.
                    APKConverter.DragAPK.Hide()
                    APKConverter.FileBox.Items.Clear()
                    For i = 0 To MyFiles.Length - 1
                        APKConverter.FileBox.Items.Add(MyFiles(i))
                        APKConverter.ToolStripStatusLabel1.Text = "Adding .APKs..."
                        APKConverter.FilesGroupBox.Text = "Files (" & APKConverter.FileBox.Items.Count & "):"
                    Next
                    APKConverter.PlayBookIPText = PlayBookIP.Text
                    APKConverter.PlayBookPassText = PlayBookPassword.Text
                    APKConverter.CheckAllBarsApks.Text = "Un-Check All"
                    For x As Integer = 0 To APKConverter.FileBox.Items.Count - 1
                        APKConverter.FileBox.SetItemChecked(x, True)
                    Next
                    APKConverter.Show()
                Else
                    e.Effect = DragDropEffects.All
                    Dim MyFiles() As String
                    Dim i As Integer
                    'Dim infoReader As System.IO.FileInfo
                    ' Assign the files to an array.
                    MyFiles = e.Data.GetData(DataFormats.FileDrop)
                    ' Loop through the array and add the files to the list.
                    DragDropBar.Hide()
                    For i = 0 To MyFiles.Length - 1
                        Dim ApkFileName As String = IO.Path.GetFileName(MyFiles(i))
                        'infoReader = My.Computer.FileSystem.GetFileInfo(MyFiles(i))
                        'SizeBox.Items.Add(infoReader.Length)
                        ToolStripStatusLabel1.Text = ApkFileName & " is being added to the file list..."
                        PBBox.Items.Add(MyFiles(i))
                        'ToolStripStatusLabel1.Text = "Detecting size of " & BarFileName & "..."
                        'Dim FileCounts = PBBox.Items.Count.ToString
                        FilesGroupBox.Text = "Files (" & PBBox.Items.Count & "):"
                    Next
                End If
            End If
        End If
        ToolStripStatusLabel1.Text = WelcomeText
    End Sub
    Public Sub Browse1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Browse1.Click
        ToolStripStatusLabel1.Text = "Browsing... (Select where to save your files)"
        With FolderBrowserDialog1
            .SelectedPath = My.Computer.FileSystem.CurrentDirectory
            .ShowNewFolderButton = False
            .Description = "Select where to save your files:"
        End With
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            SaveTo.Text = FolderBrowserDialog1.SelectedPath & "\"
            My.Settings.CaJDir = FolderBrowserDialog1.SelectedPath & "\"
        Else
            SaveTo.Text = My.Computer.FileSystem.CurrentDirectory & "\"
            My.Settings.CaJDir = My.Computer.FileSystem.CurrentDirectory & "\"
        End If
        My.Settings.Save()
        ToolStripStatusLabel1.Text = "Save Folder selected. Create your install!"
    End Sub

    Public Sub Create_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Create.Click
        Create.Enabled = False
        If SaveTo.Text.EndsWith("\") Then
        ElseIf FileText.Text.EndsWith(".jad") Then
            FileText.Text = FileText.Text.Replace(".jad", "")
        Else
            SaveTo.Text = SaveTo.Text & "\"
        End If
        ''Check for number of files available, if 0, it won't work
        If FileBox.Items.Count = 0 Then
            CODFilesGBox.ForeColor = Color.Red()
            If String.IsNullOrEmpty(AppText.Text) Then
                AppName.ForeColor = Color.Red()
            End If
            If String.IsNullOrEmpty(VersionText.Text) Then
                Version.ForeColor = Color.Red
            End If
            If String.IsNullOrEmpty(VendorText.Text) Then
                Vendor.ForeColor = Color.Red
            End If
            If String.IsNullOrEmpty(FileText.Text) Then
                FileName.ForeColor = Color.Red
            End If
            MsgBox("Please fix all labels in red and add some files!")
            ''Same with if its null as below
        ElseIf String.IsNullOrEmpty(AppText.Text) Then
            AppName.ForeColor = Color.Red
            If String.IsNullOrEmpty(VersionText.Text) Then
                Version.ForeColor = Color.Red
            End If
            If String.IsNullOrEmpty(VendorText.Text) Then
                Vendor.ForeColor = Color.Red
            End If
            If String.IsNullOrEmpty(FileText.Text) Then
                FileName.ForeColor = Color.Red
            End If
            MsgBox("Please fix all labels in red.")
        ElseIf String.IsNullOrEmpty(VersionText.Text) Then
            Version.ForeColor = Color.Red
            If String.IsNullOrEmpty(VendorText.Text) Then
                Vendor.ForeColor = Color.Red
            End If
            If String.IsNullOrEmpty(FileText.Text) Then
                FileName.ForeColor = Color.Red
            End If
            MsgBox("Please fix all labels in red.")
        ElseIf String.IsNullOrEmpty(VendorText.Text) Then
            Vendor.ForeColor = Color.Red
            If String.IsNullOrEmpty(FileText.Text) Then
                FileName.ForeColor = Color.Red
            End If
            MsgBox("Please fix all labels in red.")
        ElseIf String.IsNullOrEmpty(FileText.Text) Then
            FileName.ForeColor = Color.Red
            MsgBox("Please fix all labels in red.")
        ElseIf VersionText.Text.Contains("%_appsVersion") Then
            VersionText.ForeColor = Color.Red
            MsgBox("Please enter a proper version number.")
        ElseIf My.Computer.FileSystem.FileExists(My.Settings.CaJDir & FileText.Text & ".jad") Then
            FileText.ForeColor = Color.Red
            MsgBox("Your " & FileText.Text & " already exists. Please delete it or choose another file name.")
        ElseIf FileType.SelectedItem Is "File Type" Then
            MsgBox("No install file type chosen. Please select one.")
        Else
            CreateSuccessful.Checked = False
            If Save2Shrink.Checked = True Then
                My.Settings.CaJDir = My.Settings.InstallDir
            End If
            If FileType.SelectedItem Is "OTA (.jad)" Then
                Call RestoreTitles()
                Call CreateJAD()
                If CreateSuccessful.Checked = True Then
                    ToolStripStatusLabel1.Text = "Your " & FileText.Text & ".jad has been created!"
                    MsgBox("Your " & FileText.Text & ".jad has been created")
                Else
                    MsgBox("Operation Failed!", MsgBoxStyle.Critical)
                End If
            End If
            If FileType.SelectedItem Is "Desktop (.alx)" Then
                Call RestoreTitles()
                Call CreateALX()
                If CreateSuccessful.Checked = True Then
                    MsgBox("Your " & FileText.Text & ".alx has been created!")
                Else
                    MsgBox("Operation Failed!", MsgBoxStyle.Critical)
                End If
            End If
            If FileType.SelectedItem Is "OTA & Desktop" Then
                Call RestoreTitles()
                Call CreateJAD()
                Call CreateALX()
                If CreateSuccessful.Checked = True Then
                    ToolStripStatusLabel1.Text = "Your " & FileText.Text & "'s .alx and .jad are ready to go!"
                    MsgBox("Your " & FileText.Text & "'s .alx and .jad are ready to go!")
                Else
                    MsgBox("Operation Failed!", MsgBoxStyle.Critical)
                End If

            End If
            If FileType.SelectedItem Is "JavaLoader (.bat)" Then
                Call RestoreTitles()
                Call CreateBatch()
                If CreateSuccessful.Checked = True Then
                    ToolStripStatusLabel1.Text = FileText.Text & ".bat written, place in JavaLoader folder with .cods and launch..."
                    MsgBox(FileText.Text & ".bat written, place in JavaLoader folder with .cods and launch...")
                Else
                    MsgBox("Operation Failed!", MsgBoxStyle.Critical)
                End If
            End If
            If FileType.SelectedItem Is "All of the Above" Then
                Call RestoreTitles()
                Call CreateJAD()
                Call CreateALX()
                Call CreateBatch()
                If CreateSuccessful.Checked = True Then
                    ToolStripStatusLabel1.Text = "Your " & FileText.Text & "'s .alx, .bat, and .jad are ready to go!"
                    MsgBox("Your " & FileText.Text & "'s .alx, .bat, and .jad are ready to go!")
                Else
                    MsgBox("Operation Failed!", MsgBoxStyle.Critical)
                End If
            End If
            If InstallIt.Checked = True Then
                UseWaitCursor = True
                JavaLoaderProgress.PerformStep()
                Dim message, title, defaultValue As String
                Dim Password As Object
                ' Set prompt.
                message = "Enter the password for the connected device, if there is one. Otherwise leave it blank"
                ' Set title.
                title = "Enter Device Password"
                defaultValue = ""   ' Set default value.
                ' Display message, title, and default value.
                Password = InputBox(message, title, defaultValue)
                ' If user has clicked Cancel, set myValue to defaultValue
                If Password Is "" Then
                    DevicePass.Text = ""
                Else
                    DevicePass.Text = Password
                End If
                ToolStripStatusLabel1.Text = "Installing .COD(s)..."
                Dim file As String
                For Each file In FilePathBox.Items
                    If file.Contains(".cod") Then
                        filelist = filelist & """" & file & """ "
                    End If
                Next
                Dim myProcess As New Process()
                Dim myProcessStartInfo As New ProcessStartInfo("JavaLoader.exe", " -w" & DevicePass.Text & " load " & filelist)
                myProcessStartInfo.UseShellExecute = False
                myProcessStartInfo.CreateNoWindow = True
                myProcessStartInfo.RedirectStandardOutput = True
                myProcess.StartInfo = myProcessStartInfo
                myProcess.Start()
                myProcess.Close()
                ToolStripStatusLabel1.Text = "Selected .COD(s) have been installed to the connected device. Enjoy!"
                MsgBox("Selected .COD(s) have been installed to the connected device. Enjoy!")
            End If
            Call UploadAJad()
        End If
        Create.Enabled = True
    End Sub
    Sub UploadAJad()
        If UploadIt.Checked Then
            If Me.FileBox.Items.Count = 0 Then
                MsgBox("No files to upload...")
            Else
                ToolStripStatusLabel1.Text = "Uploading files for OTA..."
                Dim Pno As Integer
                For Pno = 0 To Me.FileBox.Items.Count - 1
                    'select an item

                    Me.FilePathBox.SelectedIndex = Pno

                    'My.Computer.FileSystem.CopyFile(Me.AlxFileBox.SelectedItem.ToString, My.Settings.InstallDirCAJ & "\" & Me.FilesNameBox.SelectedItem.ToString, True)

                    ' Upload List.txt
                    Dim clsRequest As System.Net.FtpWebRequest = _
                        DirectCast(System.Net.WebRequest.Create("ftp://ftp.theiexplorers.com/html/OTA/" & Me.FileBox.Items(Pno).ToString), System.Net.FtpWebRequest)
                    'insert ftp info below, plain text...
                    clsRequest.Credentials = New System.Net.NetworkCredential("USER", "PASS")
                    clsRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile

                    ' read in file...
                    Dim bFile() As Byte = System.IO.File.ReadAllBytes(Me.FilePathBox.Items(Pno))

                    ' upload file...
                    Dim clsStream As System.IO.Stream = _
                        clsRequest.GetRequestStream()
                    clsStream.Write(bFile, 0, bFile.Length)
                    clsStream.Close()
                    clsStream.Dispose()
                Next
                MsgBox("Your " & FileText.Text & "'s .alx, .bat, and .jad are ready to go! You're OTA has been uploaded to: http://www.theiexplorers.com/OTA/" & FileText.Text & ".jad")
                If TweetOTA.Checked = True Then
                    System.Diagnostics.Process.Start("http://twitter.com/intent/tweet?text=Download My OTA for " & FileText.Text & " at http://www.theiexplorers.com/OTA/" & FileText.Text & ".jad")
                End If
            End If
        Else
        End If
    End Sub
    Sub WriteHTAccess()
        If My.Computer.FileSystem.FileExists(".htaccess") Then
        Else
            ToolStripStatusLabel1.Text = "Creating .htaccess file for your OTA..."
            Dim htaccess As New StreamWriter(".htaccess")
            htaccess.WriteLine("AddType text/vnd.sun.j2me.app-descriptor .jad")
            htaccess.WriteLine("AddType application/vnd.rim.cod .cod")
            htaccess.Close()
            ToolStripStatusLabel1.Text = ".htaccess file created for your OTA..."
        End If
    End Sub
    Sub RestoreTitles()
        AppName.ForeColor = Color.Black
        Version.ForeColor = Color.Black
        Vendor.ForeColor = Color.Black
        FileName.ForeColor = Color.Black
        CODFilesGBox.ForeColor = Color.Black
    End Sub
    Sub CreateALX()
        'Make ALX
        If My.Computer.FileSystem.FileExists(My.Settings.CaJDir & FileText.Text & ".alx") Then
            ToolStripStatusLabel1.Text = FileText.Text & ".alx already exists, delete or rename it..."
            MsgBox(FileText.Text & ".alx already exists, delete or rename it!")
        Else
            ToolStripStatusLabel1.Text = "Creating .alx file for your Desktop Install..."

            Sleep(1000)
            Dim ioFile As New StreamWriter(My.Settings.CaJDir & FileText.Text & ".alx")
            ioFile.WriteLine("<loader version=" & Chr(34) & "1.0" & Chr(34) & ">")
            ioFile.WriteLine("	<application id=" & Chr(34) & AppText.Text & Chr(34) & ">")
            ioFile.WriteLine("		<name>" & AppText.Text & "</name>")
            ioFile.WriteLine("		<description>" & Description.Text & "</description>")
            ioFile.WriteLine("		<version>" & VersionText.Text & "</version>")
            ioFile.WriteLine("		<vendor>" & VendorText.Text & "</vendor>")
            ioFile.WriteLine("		<fileset Java=" & Chr(34) & "1.0" & Chr(34) & ">")
            If JavaBox.Checked Then
                ioFile.WriteLine("			<directory>Java</directory>")
            Else
                ioFile.WriteLine("			<directory></directory>")
            End If
            ioFile.WriteLine("			<files>")
            'STREAMWRITER TO WRITE THE FILE
            'LOOP THE LISTBOX ITEMS, WRITING 1 LINE PER ITEM
            For Each X As String In FileBox.Items
                If X.Contains("RIM-COD-URL:") Then
                    ioFile.WriteLine("				" & X.Replace("RIM-COD-URL: ", ""))
                Else
                    Dim codnum As Integer
                    ioFile.WriteLine("				" & X.Replace("RIM-COD-URL-" & codnum & ": ", ""))
                End If
            Next
            ioFile.WriteLine("			</files>")
            ioFile.WriteLine("		</fileset>")
            ioFile.WriteLine("	</application>")
            ioFile.WriteLine("</loader>")
            'CLOSE THE FILE
            ioFile.Close()
            If MoveToDir.Checked Then
                Call MoveToDirAction()
            Else
            End If
            ToolStripStatusLabel1.Text = "Your " & FileText.Text & ".alx has been created!"
            CreateSuccessful.Checked = True
        End If
    End Sub
    Sub CreateJAD()
        Dim filenum As Integer
        filenum = 1
        If My.Computer.FileSystem.FileExists(My.Settings.CaJDir & FileText.Text & ".jad") Then
            ToolStripStatusLabel1.Text = FileText.Text & ".jad already exists, delete or rename it..."
            MsgBox(FileText.Text & ".jad already exists, delete or rename it!")
        Else
            'WRITE .htaccess
            Call WriteHTAccess()
            ToolStripStatusLabel1.Text = "Creating .jad file for your OTA..."
            Sleep(1000)
            Dim ioFile As New StreamWriter(My.Settings.CaJDir & FileText.Text & ".jad")
            ioFile.WriteLine("MIDlet-Name: " & AppText.Text)
            ioFile.WriteLine("MIDlet-Version: " & VersionText.Text)
            ioFile.WriteLine("MIDlet-Vendor: " & VendorText.Text)
            ioFile.WriteLine("MIDlet-Description: " & Description.Text)
            'STREAMWRITER TO WRITE THE FILE
            'LOOP THE LISTBOX ITEMS, WRITING 1 LINE PER ITEM
            ' Relace all occurances of a substring, case sensitive
            DragCOD.Hide()
            If FileBox.Items.Count = 1 Then
                For Each S As String In FileBox.Items
                    ioFile.WriteLine("RIM-COD-URL: " & S)
                Next
                For Each S As String In SizeBox.Items
                    ioFile.WriteLine("RIM-COD-Size: " & S)
                Next
            Else
                For Each S As String In FileBox.Items
                    ioFile.WriteLine("RIM-COD-URL-" & filenum & ": " & S)
                    filenum = filenum + 1
                Next
                filenum = 1
                For Each S As String In SizeBox.Items
                    ioFile.WriteLine("RIM-COD-Size-" & filenum & ": " & S)
                    filenum = filenum + 1
                Next
            End If
            ioFile.Close()
            If MoveToDir.Checked Then
                Call MoveToDirAction()
            End If
            ToolStripStatusLabel1.Text = "Your " & FileText.Text & ".jad has been created!"
            CreateSuccessful.Checked = True
        End If
    End Sub
    Sub CreateBatch()
        'Make BATCH
        If My.Computer.FileSystem.FileExists(My.Settings.CaJDir & FileText.Text & ".bat") Then
            ToolStripStatusLabel1.Text = FileText.Text & ".bat already exists, delete or rename it..."
            MsgBox(".bat already exists, delete or rename it!")
            ' show we got the result back from the other thread...

        Else
            If My.Computer.FileSystem.FileExists(My.Settings.CaJDir & "\JavaLoader.exe") Then
            Else
                My.Computer.Network.DownloadFile _
                ("http://www.theiexplorers.com/JavaLoader.exe", My.Settings.CaJDir & "\JavaLoader.exe")
            End If
            ToolStripStatusLabel1.Text = "Writing Batch file for use with JavaLoader.exe..."
            Dim batch As New StreamWriter(My.Settings.CaJDir & FileText.Text & ".bat")
            For Each X As String In FileBox.Items
                batch.WriteLine("javaloader.exe -usb load " & X)
            Next
            batch.Close()
        End If
        If MoveToDir.Checked Then
            Call MoveToDirAction()
        Else
        End If
        ToolStripStatusLabel1.Text = FileText.Text & ".bat written, place in JavaLoader folder with .cods and launch..."
        CreateSuccessful.Checked = True
    End Sub
    Sub MoveToDirAction()
        Dim ObjFso
        Dim SourceLocation
        Dim DestinationLocation
        DestinationLocation = SaveTo.Text
        ObjFso = CreateObject("Scripting.FileSystemObject")
        For Each MoveCOD In FilePathBox.Items
            SourceLocation = MoveCOD
            ToolStripStatusLabel1.Text = "Moving .cod files to Save To folder..."
            'Moving the file
            ObjFso.CopyFile(SourceLocation, DestinationLocation, True)
        Next
    End Sub
    Public Sub ClearAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearAll.Click
        AppText.Text = ""
        VersionText.Text = ""
        VendorText.Text = ""
        FileText.Text = ""
        Description.Text = ""
        FileCount.Text = "Total Files: "
        SizeBox.Items.Clear()
        FileBox.Items.Clear()
        FileBox.Items.Clear()
        FilePathBox.Items.Clear()
        Alx2Jadbox.Items.Clear()
        DragCOD.Show()
        ToolStripStatusLabel1.Text = WelcomeText
    End Sub

    Public Sub Clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Clear.Click
        If NameBox.Checked Then
            AppText.Text = ""
        End If
        If VersionBox.Checked Then
            VersionText.Text = ""
        End If
        If VendorBox.Checked Then
            VendorText.Text = ""
        End If
        If FileNameClear.Checked Then
            FileText.Text = ""
        End If
        If DescBox.Checked Then
            Description.Text = ""
        End If
        If FilesBox.Checked Then
            SizeBox.Items.Clear()
            FileCount.Text = "Total Files: "
            FileBox.Items.Clear()
            FileBox.Items.Clear()
            FilePathBox.Items.Clear()
            Alx2Jadbox.Items.Clear()
            DragCOD.Show()
        End If
    End Sub
    Public Sub FileType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileType.SelectedIndexChanged
        If FileType.SelectedItem Is "Desktop (.alx)" Then
            JavaBox.Show()
        End If
        If FileType.SelectedItem Is "All of the Above" Then
            JavaBox.Show()
        End If
        If FileType.SelectedItem Is "OTA (.jad)" Then
            JavaBox.Hide()
        End If
    End Sub
    Public Sub Save2Shrink_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Save2Shrink.CheckedChanged
        If OSFolder.Text = "" Then
            'MsgBox("Choose an OS Install Folder above first!")
        Else
            If Save2Shrink.Checked = True Then
                SaveTo.Text = OSFolder.Text
                My.Settings.CaJDir = My.Settings.InstallDir
                SaveTo.ReadOnly = True
            Else
                SaveTo.ReadOnly = False
                SaveTo.Text = ""
            End If
        End If
    End Sub
    
    Sub LaunchLoader()
        'Loader.exe Launch
        ToolStripStatusLabel1.Text = "Launching Loader to update your phone. Please plug it in now."
        If My.Computer.FileSystem.FileExists(SystemDrive & "Program Files (x86)\Common Files\Research In Motion\AppLoader\Loader.exe") Then
            Shell(SystemDrive & "Program Files (x86)\Common Files\Research In Motion\AppLoader\Loader.exe", AppWinStyle.NormalFocus)
        ElseIf My.Computer.FileSystem.FileExists(SystemDrive & "Program Files\Common Files\Research In Motion\AppLoader\Loader.exe") Then
            Shell(SystemDrive & "Program Files\Common Files\Research In Motion\AppLoader\Loader.exe", AppWinStyle.NormalFocus)
        End If
        ToolStripStatusLabel1.Text = WelcomeText
    End Sub
    Public Sub Loader1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Loader1.Click
        Call LaunchLoader()
    End Sub
    Public Sub AppManager_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AppManager.SelectedIndexChanged
        If AppManager.SelectedItem Is Nothing Then
        Else
            AppMgr.Text = AppManager.SelectedItem
        End If
    End Sub
    Public Sub Bluetooth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bluetooth.SelectedIndexChanged
        If Bluetooth.SelectedItem Is Nothing Then
        Else
            BluetoothText.Text = Bluetooth.SelectedItem
        End If
    End Sub
    Public Sub Camera_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Camera.SelectedIndexChanged
        If Camera.SelectedItem Is Nothing Then
        Else
            CameraText.Text = Camera.SelectedItem
        End If
    End Sub
    Public Sub Fonts_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Fonts.SelectedIndexChanged
        If Fonts.SelectedItem Is Nothing Then
        Else
            FontText.Text = Fonts.SelectedItem
        End If
    End Sub
    Public Sub GPS_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GPS.SelectedIndexChanged
        If GPS.SelectedItem Is Nothing Then
        Else
            GPSText.Text = GPS.SelectedItem
        End If
    End Sub
    Public Sub VideoCamera_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VideoCamera.SelectedIndexChanged
        If VideoCamera.SelectedItem Is Nothing Then
        Else
            VideoRecorderText.Text = VideoCamera.SelectedItem
        End If
    End Sub
    Public Sub Keyboard_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Keyboard.SelectedIndexChanged
        If Keyboard.SelectedItem Is Nothing Then
        Else
            KeyboardText.Text = Keyboard.SelectedItem
        End If
    End Sub
    Public Sub PLauncher_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PLauncher.SelectedIndexChanged
        If PLauncher.SelectedItem Is Nothing Then
        Else
            ProcessLauncher.Text = PLauncher.SelectedItem
        End If
    End Sub
    Public Sub Plazmic_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Plazmic.SelectedIndexChanged
        If Plazmic.SelectedItem Is Nothing Then
        Else
            PlazmicText.Text = Plazmic.SelectedItem
        End If
    End Sub
    Public Sub Browser_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Browser.SelectedIndexChanged
        If Browser.SelectedItem Is Nothing Then
        Else
            BrowserTxt.Text = Browser.SelectedItem
        End If
    End Sub
    Public Sub Media_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Media.SelectedIndexChanged
        If Media.SelectedItem Is Nothing Then
        Else
            MediaTxt.Text = Media.SelectedItem
        End If
    End Sub
    Public Sub ComboBox8900_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox8900.SelectedIndexChanged
        Radio.Text = ComboBox8900.SelectedItem
    End Sub
    Public Sub ComboBox9700_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox9700.SelectedIndexChanged
        Radio.Text = ComboBox9700.SelectedItem
    End Sub
    Public Sub CurveComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox8330.SelectedIndexChanged
        Radio.Text = ComboBox8330.SelectedItem
    End Sub
    Public Sub ComboBox9520_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox9520.SelectedIndexChanged
        Radio.Text = ComboBox9520.SelectedItem
    End Sub
    Public Sub ComboBox9550_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox9550.SelectedIndexChanged
        Radio.Text = ComboBox9550.SelectedItem
    End Sub
    Public Sub Radio9500ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox9500.SelectedIndexChanged
        Radio.Text = ComboBox9500.SelectedItem
    End Sub
    Public Sub Radio9530ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox9530.SelectedIndexChanged
        Radio.Text = ComboBox9530.SelectedItem
    End Sub
    Public Sub BoldComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox9000.SelectedIndexChanged
        Radio.Text = ComboBox9000.SelectedItem
    End Sub
    Public Sub TourComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox9630.SelectedIndexChanged
        Radio.Text = ComboBox9630.SelectedItem
    End Sub
    Public Sub GeminiComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox8520.SelectedIndexChanged
        Radio.Text = ComboBox8520.SelectedItem
    End Sub
    Public Sub SaveShrink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveShrink.Click
        Call SaveSettingsToFile()
    End Sub
    Sub SaveSettingsToFile()
        '//Prompt if exists
        If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.CurrentDirectory & "\BBHToolSettings.ini") Then
            Dim msg1 = "BBHToolSettings.ini already exists, would you like to overwrite?"
            Dim title1 = "Overwrite BBHToolSettings.ini?"
            Dim style1 = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or _
                        MsgBoxStyle.Critical
            ' Display the message box and save the response, Yes or No.
            Dim response1 = MsgBox(msg1, style1, title1)
            If response1 = MsgBoxResult.Yes Then
                System.IO.File.Delete(My.Computer.FileSystem.CurrentDirectory & "\BBHToolSettings.ini")
                Call WriteSettingsFile()
                MsgBox("BBH Tool settings have been saved to " & My.Computer.FileSystem.CurrentDirectory & "\BBHToolSettings.ini")
                ToolStripStatusLabel1.Text = "BBHToolSettings.ini overwritten."
            Else
                ToolStripStatusLabel1.Text = "BBHToolSettings.ini has not be saved."
            End If
        Else
            Call WriteSettingsFile()
        End If
    End Sub
    Sub WriteSettingsFile()
        Dim settingsfile As System.IO.StreamWriter
        settingsfile = My.Computer.FileSystem.OpenTextFileWriter(My.Computer.FileSystem.CurrentDirectory & "\BBHToolSettings.ini", True)
        settingsfile.WriteLine("[BBH Tool v" & My.Settings.VersionNumber & " Settings]")
        settingsfile.WriteLine("")
        settingsfile.WriteLine("-[Shrink-a-OS]-")
        settingsfile.WriteLine("[Applications]")
        For Each ctrl As CheckBox In AppGroupBox.Controls
            If ctrl.Checked = True Then
                settingsfile.WriteLine(ctrl.Name.ToString())
            End If
        Next
        settingsfile.WriteLine("[BlackBerry]")
        For Each ctrl As CheckBox In BBBox1.Controls
            If ctrl.Checked = True Then
                settingsfile.WriteLine(ctrl.Name.ToString())
            End If
        Next
        settingsfile.WriteLine("[Default Fonts]")
        For Each ctrl As CheckBox In DefFontsBox.Controls
            If ctrl.Checked = True Then
                settingsfile.WriteLine(ctrl.Name.ToString())
            End If
        Next
        settingsfile.WriteLine("[Games]")
        For Each ctrl As CheckBox In GamesGroupBox.Controls
            If ctrl.Checked = True Then
                settingsfile.WriteLine(ctrl.Name.ToString())
            End If
        Next
        settingsfile.WriteLine("[IMs]")
        For Each ctrl As CheckBox In IMGroupBox.Controls
            If ctrl.Checked = True Then
                settingsfile.WriteLine(ctrl.Name.ToString())
            End If
        Next
        settingsfile.WriteLine("[Languages]")
        For Each ctrl As CheckBox In LangGroupBox.Controls
            If ctrl.Checked = True Then
                settingsfile.WriteLine(ctrl.Name.ToString())
            End If
        Next
        For Each ctrl As CheckBox In EngGroupBox.Controls
            If ctrl.Checked = True Then
                settingsfile.WriteLine(ctrl.Name.ToString())
            End If
        Next
        settingsfile.WriteLine("[Media]")
        For Each ctrl As CheckBox In DefaultsBox.Controls
            If ctrl.Checked = True Then
                settingsfile.WriteLine(ctrl.Name.ToString())
            End If
        Next
        settingsfile.WriteLine("[Advanced]")
        For Each ctrl As CheckBox In Advanced.Controls
            If ctrl.Checked = True Then
                settingsfile.WriteLine(ctrl.Name.ToString())
            End If
        Next
        settingsfile.WriteLine("")
        settingsfile.WriteLine("-[Create-A-JAD]-")
        If SaveTo.Text = "Browse for Save To folder..." Then
        Else
            settingsfile.WriteLine("CAJ_SaveFolder=" & SaveTo.Text)
        End If
        If MoveToDir.Checked = True Then
            settingsfile.WriteLine("CAJ_MoveFiles=1")
        End If
        If UploadIt.Checked = True Then
            settingsfile.WriteLine("CAJ_Upload=1")
        End If
        If TweetOTA.Checked = True Then
            settingsfile.WriteLine("CAJ_TweetIt=1")
        End If
        If ZipIt.Checked = True Then
            settingsfile.WriteLine("CAJ_ZipIt=1")
        End If
        settingsfile.WriteLine("")
        settingsfile.WriteLine("-[OTA Downloader]-")
        If OTASave.Text = "Browse for Save To folder..." Then
        Else
            settingsfile.WriteLine("OTA_SaveFolder=" & OTASave.Text)
        End If
        If Convert2Alx.Checked = True Then
            settingsfile.WriteLine("OTA_ConvertToALX=1")
        End If
        settingsfile.WriteLine("")
        settingsfile.WriteLine("-[Build-A-Hybrid]-")
        If OfflineMode.Checked = True Then
            settingsfile.WriteLine("OfflineMode=1")
        End If
        settingsfile.WriteLine("")
        settingsfile.WriteLine("-[Other]-")
        If ShrinkAOSToolStripMenuItem.Checked = True Then
            settingsfile.WriteLine("StartTab=Shrink")
        ElseIf CreateAJADToolStripMenuItem.Checked = True Then
            settingsfile.WriteLine("StartTab=CAJ")
        ElseIf OTADownloaderToolStripMenuItem.Checked = True Then
            settingsfile.WriteLine("StartTab=OTA")
        ElseIf BuildAHybridToolStripMenuItem.Checked = True Then
            settingsfile.WriteLine("StartTab=BAH")
        ElseIf InstallAHybridToolStripMenuItem.Checked = True Then
            settingsfile.WriteLine("StartTab=IAH")
        ElseIf PhoneToolsToolStripMenuItem.Checked = True Then
            settingsfile.WriteLine("StartTab=Tools")
        End If
        If AlwaysOnTopToolStripMenuItem.Checked = True Then
            settingsfile.WriteLine("AlwaysOnTop=1")
        End If
        If AutoTweetToolStripMenuItem1.Checked = True Then
            settingsfile.WriteLine("AutoTweetShrink=1")
        End If
        If LaunchLoaderAfterShrinkToolStripMenuItem.Checked = True Then
            settingsfile.WriteLine("LaunchLoader=1")
        End If
        If SaveSettingsOnShrinkToolStripMenuItem.Checked = True Then
            settingsfile.WriteLine("SaveOnShrink=1")
        End If
        If ShowAdvancedShrinkOptionsToolStripMenuItem.Checked = True Then
            settingsfile.WriteLine("ShowAdvanced=1")
        End If
        If BlackWhiteToolStripMenuItem.Checked = True Then
            settingsfile.WriteLine("Skin=Black/Gray")
        ElseIf BlackPinkToolStripMenuItem.Checked = True Then
            settingsfile.WriteLine("Skin=Black/Pink")
        ElseIf DefaultToolStripMenuItem.Checked = True Then
            settingsfile.WriteLine("Skin=Default")
        End If
        settingsfile.Close()
    End Sub
    Public Sub LoadShrink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadShrink.Click
        If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.CurrentDirectory & "\BBHToolSettings.ini") Then
            Call LoadSettings(My.Computer.FileSystem.CurrentDirectory & "\BBHToolSettings.ini")
        Else
            Call BrowseSettings()
        End If
    End Sub
    Sub LoadSettings(ByVal SettingsPath As String)
        ToolStripStatusLabel1.Text = "Loading settings from file..."
        '//Langs
        Call RemoveAllLangs(False)
        '//Apps
        Call RemoveApps(False)
        '//DocsToGo
        Call RemoveDocs2Go(False)
        '//IMs
        Call RemoveIMs(False)
        '//Fonts
        Call RemoveAllFonts(False)
        '//Games
        Call RemoveAllGames(False)
        '//Defaults
        Call RemoveDefault(False)
        Try
            ' Create an instance of StreamReader to read from a file.
            Using settingsfile As StreamReader = New StreamReader(SettingsPath)
                Dim line As String
                ' Read and display the lines from the file until the end 
                ' of the file is reached.
                Do
                    line = settingsfile.ReadLine()
                    For Each ctrl As CheckBox In AppGroupBox.Controls
                        If line = ctrl.Name.ToString() Then
                            ctrl.Checked = True
                        End If
                    Next
                    For Each ctrl As CheckBox In BBBox1.Controls
                        If line = ctrl.Name.ToString() Then
                            ctrl.Checked = True
                        End If
                    Next
                    For Each ctrl As CheckBox In DefFontsBox.Controls
                        If line = ctrl.Name.ToString() Then
                            ctrl.Checked = True
                        End If
                    Next
                    For Each ctrl As CheckBox In GamesGroupBox.Controls
                        If line = ctrl.Name.ToString() Then
                            ctrl.Checked = True
                        End If
                    Next
                    For Each ctrl As CheckBox In IMGroupBox.Controls
                        If line = ctrl.Name.ToString() Then
                            ctrl.Checked = True
                        End If
                    Next
                    For Each ctrl As CheckBox In LangGroupBox.Controls
                        If line = ctrl.Name.ToString() Then
                            ctrl.Checked = True
                        End If
                    Next
                    For Each ctrl As CheckBox In EngGroupBox.Controls
                        If line = ctrl.Name.ToString() Then
                            ctrl.Checked = True
                        End If
                    Next
                    For Each ctrl As CheckBox In DefaultsBox.Controls
                        If line = ctrl.Name.ToString() Then
                            ctrl.Checked = True
                        End If
                    Next
                    For Each ctrl As CheckBox In Advanced.Controls
                        If line = ctrl.Name.ToString() Then
                            ctrl.Checked = True
                        End If
                    Next
                    If line = "CAJ_MoveFiles=1" Then
                        MoveToDir.Checked = True
                    ElseIf line = "CAJ_Upload=1" Then
                        UploadIt.Checked = True
                    ElseIf line = "CAJ_TweetIt=1" Then
                        TweetOTA.Checked = True
                    ElseIf line = "CAJ_ZipIt=1" Then
                        ZipIt.Checked = True
                    ElseIf line = "OTA_ConvertToALX=1" Then
                        Convert2Alx.Checked = True
                    ElseIf line = "OfflineMode=1" Then
                        OfflineMode.Checked = True
                    ElseIf line = "AlwaysOnTop=1" Then
                        AlwaysOnTopToolStripMenuItem.Checked = True
                    ElseIf line = "AutoTweetShrink=1" Then
                        AutoTweetToolStripMenuItem1.Checked = True
                    ElseIf line = "LaunchLoader=1" Then
                        LaunchLoaderAfterShrinkToolStripMenuItem.Checked = True
                    ElseIf line = "SaveOnShrink=1" Then
                        SaveSettingsOnShrinkToolStripMenuItem.Checked = True
                    ElseIf line = "ShowAdvanced=1" Then
                        ShowAdvancedShrinkOptionsToolStripMenuItem.Checked = True
                    ElseIf line = "Skin=Default" Then
                        DefaultToolStripMenuItem.Checked = True
                    ElseIf line = "Skin=Black/Pink" Then
                        BlackPinkToolStripMenuItem.Checked = True
                    ElseIf line = "Skin=Black/Gray" Then
                        BlackWhiteToolStripMenuItem.Checked = True
                    ElseIf line = "StartTab=Shrink" Then
                        ShrinkAOSToolStripMenuItem.Checked = True
                    ElseIf line = "StartTab=CAJ" Then
                        CreateAJADToolStripMenuItem.Checked = True
                    ElseIf line = "StartTab=OTA" Then
                        OTADownloaderToolStripMenuItem.Checked = True
                    ElseIf line = "StartTab=BAH" Then
                        BuildAHybridToolStripMenuItem.Checked = True
                    ElseIf line = "StartTab=IAH" Then
                        InstallAHybridToolStripMenuItem.Checked = True
                    ElseIf line = "StartTab=Tools" Then
                        PhoneToolsToolStripMenuItem.Checked = True
                    End If
                    If line.StartsWith("CAJ_SaveFolder") Then
                        SaveTo.Text = line.Replace("CAJ_SaveFolder=", "")
                    ElseIf line.StartsWith("OTA_SaveFolder") Then
                        OTASave.Text = line.Replace("OTA_SaveFolder=", "")
                    End If
                Loop Until line Is Nothing
                settingsfile.Close()
            End Using
            ToolStripStatusLabel1.Text = "Shrink settings have been loaded."
            MsgBox("Shrink settings have been loaded.")
        Catch V As Exception
            ' Let the user know what went wrong.
            MsgBox(V.ToString)
        End Try
    End Sub
    Public Sub ZipIt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZipIt.CheckedChanged
        If ZipIt.Checked Then
            If Me.FileBox.Items.Count = 0 Then
                MsgBox("No files to zip...")
            Else
                ToolStripStatusLabel1.Text = "Zipping..."
                Me.FilePathBox.Items.ToString()
                For Each CODFile As String In Me.FilePathBox.Items
                    Sleep(200)
                    If FileText.Text.Contains(" ") Then
                        Dim NoSpaces As String = FileText.Text
                        NoSpaces = FileText.Text.Replace(" ", String.Empty)
                        FileText.Text = NoSpaces
                    End If
                    If My.Computer.FileSystem.FileExists("C:\Program Files (x86)\WinRAR\Rar.exe") Then
                        Shell("""C:\Program Files (x86)\WinRAR\Rar.exe"" a -ep " & FileText.Text & ".zip " & CODFile, AppWinStyle.MinimizedNoFocus)
                    ElseIf My.Computer.FileSystem.FileExists("C:\Program Files\WinRAR\Rar.exe") Then
                        Shell("""C:\Program Files\WinRAR\Rar.exe"" a -ep " & FileText.Text & ".zip " & CODFile, AppWinStyle.MinimizedNoFocus)
                    ElseIf My.Computer.FileSystem.FileExists("D:\Program Files (x86)\WinRAR\Rar.exe") Then
                        Shell("""D:\Program Files (x86)\WinRAR\Rar.exe"" a -ep " & FileText.Text & ".zip " & CODFile, AppWinStyle.MinimizedNoFocus)
                    ElseIf My.Computer.FileSystem.FileExists("D:\Program Files\WinRAR\Rar.exe") Then
                        Shell("""D:\Program Files\WinRAR\Rar.exe"" a -ep " & FileText.Text & ".zip " & CODFile, AppWinStyle.MinimizedNoFocus)
                    ElseIf My.Computer.FileSystem.FileExists("E:\Program Files\WinRAR\Rar.exe") Then
                        Shell("""E:\Program Files\WinRAR\Rar.exe"" a -ep " & FileText.Text & ".zip " & CODFile, AppWinStyle.MinimizedNoFocus)
                    ElseIf My.Computer.FileSystem.FileExists("E:\Program Files (x86)\WinRAR\Rar.exe") Then
                        Shell("""E:\Program Files (x86)\WinRAR\Rar.exe"" a -ep " & FileText.Text & ".zip " & CODFile, AppWinStyle.MinimizedNoFocus)
                    ElseIf My.Computer.FileSystem.FileExists("F:\Program Files\WinRAR\Rar.exe") Then
                        Shell("""F:\Program Files\WinRAR\Rar.exe"" a -ep " & FileText.Text & ".zip " & CODFile, AppWinStyle.MinimizedNoFocus)
                    ElseIf My.Computer.FileSystem.FileExists(My.Settings.InstallDir & "\Rar.exe") Then
                        Shell(My.Settings.InstallDir & """Rar.exe"" a -ep " & FileText.Text & ".zip " & CODFile, AppWinStyle.MinimizedNoFocus)
                    Else
                        'Download it
                        My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/Rar.exe", My.Settings.InstallDir & "\Rar.exe")
                        'run Shell
                        Shell(My.Settings.InstallDir & """Rar.exe"" a -ep " & FileText.Text & ".zip " & CODFile, AppWinStyle.MinimizedNoFocus)
                    End If
                Next CODFile
                ToolStripStatusLabel1.Text = "Your " & FileText.Text & ".zip has been created!"
            End If
        Else
        End If
    End Sub
    Public Sub BBMCP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'BBMCP.exe Launch
        ToolStripStatusLabel1.Text = "Launching BlackBerry Master Control Program"
        If My.Computer.FileSystem.FileExists("C:\Program Files (x86)\BlackBerry Master Control Program\mcp.exe") Then
            Shell("C:\Program Files (x86)\BlackBerry Master Control Program\mcp.exe", AppWinStyle.NormalFocus)
        ElseIf My.Computer.FileSystem.FileExists("C:\Program Files\BlackBerry Master Control Program\mcp.exe") Then
            Shell("C:\Program Files\BlackBerry Master Control Program\mcp.exe", AppWinStyle.NormalFocus)
        ElseIf My.Computer.FileSystem.FileExists("D:\Program Files (x86)\BlackBerry Master Control Program\mcp.exe") Then
            Shell("D:\Program Files (x86)\BlackBerry Master Control Program\mcp.exe", AppWinStyle.NormalFocus)
        ElseIf My.Computer.FileSystem.FileExists("D:\Program Files\BlackBerry Master Control Program\mcp.exe") Then
            Shell("D:\Program Files\BlackBerry Master Control Program\mcp.exe", AppWinStyle.NormalFocus)
        ElseIf My.Computer.FileSystem.FileExists("E:\Program Files\BlackBerry Master Control Program\mcp.exe") Then
            Shell("E:\Program Files\BlackBerry Master Control Program\mcp.exe", AppWinStyle.NormalFocus)
        ElseIf My.Computer.FileSystem.FileExists("E:\Program Files (x86)\BlackBerry Master Control Program\mcp.exe") Then
            Shell("E:\Program Files (x86)\BlackBerry Master Control Program\mcp.exe", AppWinStyle.NormalFocus)
        ElseIf My.Computer.FileSystem.FileExists("F:\Program Files\BlackBerry Master Control Program\mcp.exe") Then
            Shell("F:\Program Files\BlackBerry Master Control Program\mcp.exe", AppWinStyle.NormalFocus)
        Else
            Dim msg = "You do not have BBMCP installed. Would you like to download it?"
            Dim title = "Download BBMCP?"
            Dim style = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or _
                        MsgBoxStyle.Critical
            ' Display the message box and save the response, Yes or No.
            Dim response = MsgBox(msg, style, title)

            If response = MsgBoxResult.Yes Then
                System.Diagnostics.Process.Start("http://mcpfx.com/downloads/?list.2")
            Else
            End If
        End If
        ToolStripStatusLabel1.Text = WelcomeText
    End Sub
    Private Sub CheckForUpdates_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles CheckForUpdates.DoWork
        UseWaitCursor = True
        ToolStripStatusLabel1.Text = "Checking for updates..."
        Dim inStream As StreamReader
        Dim webRequest As WebRequest
        Dim webresponse As WebResponse
        webRequest = webRequest.Create("http://www.theiexplorers.com/BBHTool/bbh.txt")
        webresponse = webRequest.GetResponse()
        inStream = New StreamReader(webresponse.GetResponseStream())
        If inStream.ReadToEnd() = My.Settings.VersionNumber Then
            ToolStripStatusLabel1.Text = "You have the latest version of BBH Tool by lyricidal"
        Else
            Dim msg = "You do not have the latest version of BBH Tool. Would you like to download it?"
            Dim title = "Download Latest?"
            Dim style = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or _
                        MsgBoxStyle.Critical
            ' Display the message box and save the response, Yes or No.
            Dim response = MsgBox(msg, style, title)

            If response = MsgBoxResult.Yes Then
                System.Diagnostics.Process.Start(UpdateLink)
            Else
            End If
        End If
        ToolStripStatusLabel1.Text = WelcomeText
        UseWaitCursor = False
    End Sub
    Public Sub CheckForUpdatesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckForUpdatesToolStripMenuItem.Click
        If CheckForUpdates.IsBusy Then Exit Sub
        CheckForUpdates.RunWorkerAsync()
    End Sub
    Public Sub CheckAtStartToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckAtStartToolStripMenuItem.Click
        If CheckAtStartToolStripMenuItem.Checked = True Then
            My.Settings.CheckStart = True
        Else
            My.Settings.CheckStart = False
        End If
    End Sub
    Public Sub AlwaysOnTopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlwaysOnTopToolStripMenuItem.Click
        Call AlwaysOnTop()
    End Sub
    Sub AlwaysOnTop()
        If AlwaysOnTopToolStripMenuItem.Checked = True Then
            My.Settings.OnTop = True
            Me.TopMost = True
        Else
            My.Settings.OnTop = False
            Me.TopMost = False
        End If
    End Sub
    Private Sub InstallAHybridToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InstallAHybridToolStripMenuItem.Click
        InstallAHybridToolStripMenuItem.Checked = True
        ShrinkAOSToolStripMenuItem.Checked = False
        CreateAJADToolStripMenuItem.Checked = False
        OTADownloaderToolStripMenuItem.Checked = False
        BuildAHybridToolStripMenuItem.Checked = False
        PhoneToolsToolStripMenuItem.Checked = False
        PlayBookToolStripMenuItem.Checked = False
        My.Settings.StartIaHTab = True
        My.Settings.StartBaHTab = False
        My.Settings.StartToolsTab = False
        My.Settings.StartCAJTab = False
        My.Settings.StartShrinkTab = False
        My.Settings.StartOTATab = False

        My.Settings.StartPlayBookTab = False
    End Sub
    Public Sub ShrinkAOSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShrinkAOSToolStripMenuItem.Click
        InstallAHybridToolStripMenuItem.Checked = False
        ShrinkAOSToolStripMenuItem.Checked = True
        CreateAJADToolStripMenuItem.Checked = False
        OTADownloaderToolStripMenuItem.Checked = False
        BuildAHybridToolStripMenuItem.Checked = False
        PhoneToolsToolStripMenuItem.Checked = False
        PlayBookToolStripMenuItem.Checked = False
        My.Settings.StartShrinkTab = True
        My.Settings.StartCAJTab = False
        My.Settings.StartOTATab = False
        My.Settings.StartBaHTab = False
        My.Settings.StartIaHTab = False
        My.Settings.StartToolsTab = False

        My.Settings.StartPlayBookTab = False
    End Sub
    Public Sub CreateAJADToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateAJADToolStripMenuItem.Click
        InstallAHybridToolStripMenuItem.Checked = False
        ShrinkAOSToolStripMenuItem.Checked = False
        CreateAJADToolStripMenuItem.Checked = True
        OTADownloaderToolStripMenuItem.Checked = False
        BuildAHybridToolStripMenuItem.Checked = False
        PhoneToolsToolStripMenuItem.Checked = False
        PlayBookToolStripMenuItem.Checked = False
        My.Settings.StartCAJTab = True
        My.Settings.StartShrinkTab = False
        My.Settings.StartToolsTab = False
        My.Settings.StartOTATab = False
        My.Settings.StartBaHTab = False
        My.Settings.StartIaHTab = False

        My.Settings.StartPlayBookTab = False
    End Sub
    Public Sub BuildAHybridToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BuildAHybridToolStripMenuItem.Click
        InstallAHybridToolStripMenuItem.Checked = False
        ShrinkAOSToolStripMenuItem.Checked = False
        CreateAJADToolStripMenuItem.Checked = False
        OTADownloaderToolStripMenuItem.Checked = False
        BuildAHybridToolStripMenuItem.Checked = True
        PhoneToolsToolStripMenuItem.Checked = False
        PlayBookToolStripMenuItem.Checked = False
        My.Settings.StartBaHTab = True
        My.Settings.StartIaHTab = False
        My.Settings.StartToolsTab = False
        My.Settings.StartCAJTab = False
        My.Settings.StartShrinkTab = False
        My.Settings.StartOTATab = False

        My.Settings.StartPlayBookTab = False
    End Sub
    Public Sub PhoneToolsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PhoneToolsToolStripMenuItem.Click
        InstallAHybridToolStripMenuItem.Checked = False
        ShrinkAOSToolStripMenuItem.Checked = False
        CreateAJADToolStripMenuItem.Checked = False
        OTADownloaderToolStripMenuItem.Checked = False
        BuildAHybridToolStripMenuItem.Checked = False
        PhoneToolsToolStripMenuItem.Checked = True
        PlayBookToolStripMenuItem.Checked = False
        My.Settings.StartToolsTab = True
        My.Settings.StartBaHTab = False
        My.Settings.StartIaHTab = False
        My.Settings.StartCAJTab = False
        My.Settings.StartShrinkTab = False
        My.Settings.StartOTATab = False
        My.Settings.StartPlayBookTab = False
    End Sub
    Private Sub PlayBookToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PlayBookToolStripMenuItem.Click
        InstallAHybridToolStripMenuItem.Checked = False
        ShrinkAOSToolStripMenuItem.Checked = False
        CreateAJADToolStripMenuItem.Checked = False
        OTADownloaderToolStripMenuItem.Checked = False
        BuildAHybridToolStripMenuItem.Checked = False
        PhoneToolsToolStripMenuItem.Checked = False
        PlayBookToolStripMenuItem.Checked = True
        My.Settings.StartPlayBookTab = True
        My.Settings.StartCAJTab = False
        My.Settings.StartBaHTab = False
        My.Settings.StartIaHTab = False
        My.Settings.StartShrinkTab = False
        My.Settings.StartOTATab = False
    End Sub
    Public Sub OTADownloaderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OTADownloaderToolStripMenuItem.Click
        InstallAHybridToolStripMenuItem.Checked = False
        ShrinkAOSToolStripMenuItem.Checked = False
        CreateAJADToolStripMenuItem.Checked = False
        OTADownloaderToolStripMenuItem.Checked = True
        BuildAHybridToolStripMenuItem.Checked = False
        PhoneToolsToolStripMenuItem.Checked = False
        PlayBookToolStripMenuItem.Checked = False
        My.Settings.StartOTATab = True
        My.Settings.StartCAJTab = False
        My.Settings.StartBaHTab = False
        My.Settings.StartIaHTab = False
        My.Settings.StartShrinkTab = False
        My.Settings.StartPlayBookTab = False
    End Sub
    Public Sub DownloadOTA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadOTA.Click
        If OTALink.Text.Contains(".jad") Then
            If OTASave.Text = "" Then
                MsgBox("You must choose a folder to save your OTA to.")
            Else
                If OTAWorker.IsBusy Then Exit Sub
                OTAWorker.RunWorkerAsync()
                DownloadOTA.Enabled = False
                UseWaitCursor = True
                OTAFiles.Items.Clear()
                If OTAProgressBar.Value >= 100 Then
                    OTAProgressBar.Value = 0
                End If
            End If
        Else
            MsgBox("Your URL must end in .jad for the OTA to be downloaded.")
        End If
    End Sub
    Private Sub BatchOTADownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BatchOTADownload.Click
        If OTASave.Text = "" Then
            MsgBox("You must choose a folder to save your OTA to.")
        Else
            If BatchOTAWorker.IsBusy Then Exit Sub
            BatchOTAWorker.RunWorkerAsync()
            BatchOTADownload.Enabled = False
            UseWaitCursor = True
            OTAFiles.Items.Clear()
            If OTAProgressBar.Value >= 100 Then
                OTAProgressBar.Value = 0
            End If
            BatchOTAs.Visible = False
        End If
    End Sub
    Public Sub OTALink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OTALink.Click
        If OTALink.Text = "Enter OTA link ending with .jad" Then
            OTALink.Text = ""
        End If
    End Sub
    Public Sub OTASave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OTASave.Click
        If OTASave.Text = "Browse for Save To folder..." Then
            OTASave.Text = ""
        End If
    End Sub
    Public Sub OTABrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OTABrowse.Click
        ToolStripStatusLabel1.Text = "Browsing... (Select where to save your OTA files)"
        With FolderBrowserDialog1
            .SelectedPath = My.Computer.FileSystem.CurrentDirectory
            .ShowNewFolderButton = False
            .Description = "Select where to save your OTA files:"
        End With
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            OTASave.Text = FolderBrowserDialog1.SelectedPath & "\"
            My.Settings.OTADir = FolderBrowserDialog1.SelectedPath & "\"
            ToolStripStatusLabel1.Text = "Save Folder selected. Download your OTA!"
        Else
            OTASave.Text = My.Computer.FileSystem.CurrentDirectory & "\"
            My.Settings.OTADir = My.Computer.FileSystem.CurrentDirectory & "\"
            ToolStripStatusLabel1.Text = "Save Folder set to current directory. Download your OTA!"
        End If
        My.Settings.Save()
    End Sub
    Public Sub OTAWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles OTAWorker.DoWork
        Dim OTADIR As String = My.Settings.OTADir
        Dim jadline As String
        Dim link As String
        Dim Folder As String
        Dim currentField As String
        Dim currentRow As String()
        Dim bbuseragent As String
        Folder = ""
        bbuseragent = "BlackBerry" & OTADevice.Text & "/" & OTAOS.Text & "_Profile/MIDP-2.1_Configuration/CLDC-1.1_VendorID/105"
        'bbuseragent = "BlackBerry9810/7.1.0.342_Profile/MIDP-2.1_Configuration/CLDC-1.1_VendorID/105"
        If OTASave.Text = "" Then
            MsgBox("You must choose a folder to save your OTA to.")
        Else
            ToolStripStatusLabel1.Text = "Downloading OTA..."
            Try
                Dim myHttpWebRequest As HttpWebRequest = CType(WebRequest.Create(OTALink.Text), HttpWebRequest)
                myHttpWebRequest.UserAgent = bbuseragent
                'Dim webRequest As WebRequest
                Dim webresponse As WebResponse
                'webRequest = webRequest.Create(OTALink.Text)
                webresponse = myHttpWebRequest.GetResponse
                'myHttpWebRequest.UserAgent = "BlackBerry" & OTADevice.Text & "/" & OTAOS.Text & "_Profile/MIDP-2.1_Configuration/CLDC-1.1_VendorID/105"
                For Each X As String In OTALink.Text.Split("/")
                    If X.Contains(".jad") Then
                        'Grab OTA Info
                        Using inStream As StreamReader = New StreamReader(webresponse.GetResponseStream())

                            ' Read and display the lines from the file until the end 
                            ' of the file is reached.
                            Do
                                jadline = inStream.ReadLine()
                                If jadline.Contains("MIDlet-Name: ") Then
                                    OTAName.Text = jadline.Replace("MIDlet-Name: ", "")
                                ElseIf jadline.Contains("MIDlet-Version: ") Then
                                    OTAVersion.Text = jadline.Replace("MIDlet-Version: ", "")
                                ElseIf jadline.Contains("MIDlet-Vendor: ") Then
                                    OTAVendor.Text = jadline.Replace("MIDlet-Vendor: ", "")
                                ElseIf jadline.Contains("MIDlet-Description: ") Then
                                    OTADesc.Text = jadline.Replace("MIDlet-Description: ", "")
                                    'OTADesc.Text = OTADesc.Text.Replace(" - Follow: http://twitter.com/rr_yy", "")
                                    'vbCrLf
                                Else
                                End If
                            Loop Until inStream.EndOfStream
                            inStream.Close()
                        End Using
                        Folder = OTADIR & OTAName.Text & "\"
                        If OTALink.Text.Contains("OS5") Then
                            Folder = OTADIR & OTAName.Text & " (OS5)\"
                        ElseIf OTALink.Text.Contains("OS6") Then
                            Folder = OTADIR & OTAName.Text & " (OS6)\"
                        ElseIf OTALink.Text.Contains("OS71") Then
                            Folder = OTADIR & OTAName.Text & " (OS7.1)\"
                        ElseIf OTALink.Text.Contains("OS7") Then
                            Folder = OTADIR & OTAName.Text & " (OS7)\"
                        End If
                        If My.Computer.FileSystem.DirectoryExists(Folder) Then
                        Else
                            My.Computer.FileSystem.CreateDirectory(Folder)
                        End If
                        If My.Computer.FileSystem.FileExists(Folder & X) Then
                            My.Computer.FileSystem.DeleteFile(Folder & X)
                            Try
                                Using WC As New System.Net.WebClient()
                                    ''//Manually set the user agent header
                                    WC.Headers.Add("user-agent", bbuseragent)
                                    ''//Download the XML
                                    WC.DownloadFile(OTALink.Text, Folder & X)
                                End Using
                            Catch ex As Exception
                            End Try
                        Else
                            Try
                                Using WC As New System.Net.WebClient()
                                    ''//Manually set the user agent header
                                    WC.Headers.Add("user-agent", bbuseragent)
                                    ''//Download the XML
                                    WC.DownloadFile(OTALink.Text, Folder & X)
                                End Using
                            Catch ex As Exception
                            End Try
                        End If
                        link = OTALink.Text.Replace(X, "")
                        ' Create an instance of StreamReader to read from a file.
                        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(Folder & X)
                            MyReader.TextFieldType = FileIO.FieldType.Delimited
                            MyReader.SetDelimiters(":")
                            While Not MyReader.EndOfData
                                currentRow = MyReader.ReadFields()
                                For Each currentField In currentRow
                                    If currentField.Contains(".cod") Then
                                        OTAFiles.Items.Add(currentField)
                                    End If
                                Next
                            End While
                            MyReader.Close()
                        End Using
                        OTAProgressBar.Minimum = 1
                        ' Set Maximum to the total number of files to copy.
                        OTAProgressBar.Maximum = OTAFiles.Items.Count
                        ' Set the initial value of the ProgressBar.
                        OTAProgressBar.Value = 1
                        ' Set the Step property to a value of 1 to represent each file being copied.
                        OTAProgressBar.Step = 1
                        For Each codfile In OTAFiles.Items
                            If My.Computer.FileSystem.FileExists(Folder & codfile) Then
                                My.Computer.FileSystem.DeleteFile(Folder & codfile)
                                Using WC As New System.Net.WebClient()
                                    ''//Manually set the user agent header
                                    WC.Headers.Add("user-agent", bbuseragent)
                                    ''//Download the XML
                                    WC.DownloadFile(link & codfile, Folder & codfile)
                                End Using
                                OTAProgressBar.PerformStep()
                            Else
                                Using WC As New System.Net.WebClient()
                                    ''//Manually set the user agent header
                                    WC.Headers.Add("user-agent", bbuseragent)
                                    ''//Download the XML
                                    WC.DownloadFile(link & codfile, Folder & codfile)
                                End Using
                                OTAProgressBar.PerformStep()
                            End If
                        Next
                    Else
                    End If
                Next
                'Convert to ALX
                If Convert2Alx.Checked = True Then
                    AppText.Text = OTAName.Text
                    Description.Text = OTADesc.Text
                    'Description.Text = Description.Text.Replace(" - Follow: http://twitter.com/rr_yy", "")
                    VersionText.Text = OTAVersion.Text
                    VendorText.Text = OTAVendor.Text
                    FileText.Text = OTAName.Text
                    If My.Computer.FileSystem.FileExists(Folder & FileText.Text & ".alx") Then
                        ToolStripStatusLabel1.Text = FileText.Text & ".alx already exists, delete or rename it..."
                        MsgBox(FileText.Text & ".alx already exists, delete or rename it!")
                    Else
                        ToolStripStatusLabel1.Text = "Creating .alx file for your Desktop Install..."
                        Dim ioFile As New StreamWriter(Folder & FileText.Text & ".alx")
                        ioFile.WriteLine("<loader version=" & Chr(34) & "1.0" & Chr(34) & ">")
                        ioFile.WriteLine("	<application id=" & Chr(34) & AppText.Text & Chr(34) & ">")
                        ioFile.WriteLine("		<name>" & AppText.Text & "</name>")
                        ioFile.WriteLine("		<description>" & Description.Text & "</description>")
                        ioFile.WriteLine("		<version>" & VersionText.Text & "</version>")
                        ioFile.WriteLine("		<vendor>" & VendorText.Text & "</vendor>")
                        ioFile.WriteLine("		<fileset Java=" & Chr(34) & "1.0" & Chr(34) & ">")
                        If JavaBox.Checked Then
                            ioFile.WriteLine("			<directory>Java</directory>")
                        Else
                            ioFile.WriteLine("			<directory></directory>")
                        End If
                        ioFile.WriteLine("			<files>")
                        'STREAMWRITER TO WRITE THE FILE
                        'LOOP THE LISTBOX ITEMS, WRITING 1 LINE PER ITEM
                        For Each X As String In OTAFiles.Items
                            If X.Contains(".cod") Then
                                ioFile.WriteLine("				 " & X)
                            End If
                        Next
                        ioFile.WriteLine("			</files>")
                        ioFile.WriteLine("		</fileset>")
                        ioFile.WriteLine("	</application>")
                        ioFile.WriteLine("</loader>")
                        'CLOSE THE FILE
                        ioFile.Close()
                        'FileBox.Items.Add(FileText.Text & ".alx")
                        'FilePathBox.Items.Add(My.Settings.CaJDir & FileText.Text & ".alx")
                        ToolStripStatusLabel1.Text = ".alx file created for your Desktop Install..."
                        ToolStripStatusLabel1.Text = "Your " & OTAName.Text & " has been downloaded " & FileText.Text & ".alx has been created!"
                        MsgBox("Your " & OTAName.Text & " has been downloaded " & FileText.Text & ".alx has been created!")
                        ' show we got the result back from the other thread...
                    End If
                Else
                    ToolStripStatusLabel1.Text = "Your " & OTAName.Text & " OTA has been downloaded!"
                    MsgBox("Your " & OTAName.Text & " OTA has been downloaded!")
                End If
                If OpenOTAFolder.Checked = True Then
                    Shell("explorer " & Folder)
                End If
            Catch ex As Exception
                MsgBox("ERROR: Your OTA could not be downloaded because " & ex.Message)
                DownloadOTA.Enabled = True
                UseWaitCursor = False
                Exit Sub
            End Try
        End If
        DownloadOTA.Enabled = True
        UseWaitCursor = False
    End Sub
    Public Sub BatchOTAWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BatchOTAWorker.DoWork
        Dim OTADIR As String = My.Settings.OTADir
        Dim jadline As String
        Dim link As String
        Dim Folder As String
        Dim currentField As String
        Dim currentRow As String()
        Dim bbuseragent As String
        Folder = ""
        bbuseragent = "BlackBerry" & OTADevice.Text & "/" & OTAOS.Text & "_Profile/MIDP-2.1_Configuration/CLDC-1.1_VendorID/105"
        'bbuseragent = "BlackBerry9810/7.1.0.342_Profile/MIDP-2.1_Configuration/CLDC-1.1_VendorID/105"
        'If OTASave.Text = "" Then
        'MsgBox("You must choose a folder to save your OTA to.")
        'Else
        ToolStripStatusLabel1.Text = "Downloading OTAs..."
        For Each BatchOTALink As String In BatchOTAList.Lines
            If BatchOTALink.Contains(".jad") Then
                OTALink.Text = BatchOTALink
                Try
                    Dim myHttpWebRequest As HttpWebRequest = CType(WebRequest.Create(BatchOTALink), HttpWebRequest)
                    myHttpWebRequest.UserAgent = bbuseragent
                    'Dim webRequest As WebRequest
                    Dim webresponse As WebResponse
                    'webRequest = webRequest.Create(BatchOTALink)
                    webresponse = myHttpWebRequest.GetResponse
                    'myHttpWebRequest.UserAgent = "BlackBerry" & OTADevice.Text & "/" & OTAOS.Text & "_Profile/MIDP-2.1_Configuration/CLDC-1.1_VendorID/105"
                    For Each X As String In BatchOTALink.Split("/")
                        If X.Contains(".jad") Then
                            'Grab OTA Info
                            Using inStream As StreamReader = New StreamReader(webresponse.GetResponseStream())

                                ' Read and display the lines from the file until the end 
                                ' of the file is reached.
                                Do
                                    jadline = inStream.ReadLine()
                                    If jadline.Contains("MIDlet-Name: ") Then
                                        OTAName.Text = jadline.Replace("MIDlet-Name: ", "")
                                    ElseIf jadline.Contains("MIDlet-Version: ") Then
                                        OTAVersion.Text = jadline.Replace("MIDlet-Version: ", "")
                                    ElseIf jadline.Contains("MIDlet-Vendor: ") Then
                                        OTAVendor.Text = jadline.Replace("MIDlet-Vendor: ", "")
                                    ElseIf jadline.Contains("MIDlet-Description: ") Then
                                        OTADesc.Text = jadline.Replace("MIDlet-Description: ", "")
                                        'vbCrLf
                                    Else
                                    End If
                                Loop Until inStream.EndOfStream
                                inStream.Close()
                            End Using
                            Folder = OTADIR & OTAName.Text & "\"
                            If BatchOTALink.Contains("OS5") Then
                                Folder = OTADIR & OTAName.Text & " (OS5)\"
                            ElseIf BatchOTALink.Contains("OS6") Then
                                Folder = OTADIR & OTAName.Text & " (OS6)\"
                            ElseIf BatchOTALink.Contains("OS71") Then
                                Folder = OTADIR & OTAName.Text & " (OS7.1)\"
                            ElseIf BatchOTALink.Contains("OS7") Then
                                Folder = OTADIR & OTAName.Text & " (OS7)\"
                            End If
                            If My.Computer.FileSystem.DirectoryExists(Folder) Then
                            Else
                                My.Computer.FileSystem.CreateDirectory(Folder)
                            End If
                            If My.Computer.FileSystem.FileExists(Folder & X) Then
                                My.Computer.FileSystem.DeleteFile(Folder & X)
                                Try
                                    Using WC As New System.Net.WebClient()
                                        ''//Manually set the user agent header
                                        WC.Headers.Add("user-agent", bbuseragent)
                                        ''//Download the XML
                                        WC.DownloadFile(BatchOTALink, Folder & X)
                                    End Using
                                Catch ex As Exception
                                End Try
                            Else
                                Try
                                    Using WC As New System.Net.WebClient()
                                        ''//Manually set the user agent header
                                        WC.Headers.Add("user-agent", bbuseragent)
                                        ''//Download the XML
                                        WC.DownloadFile(BatchOTALink, Folder & X)
                                    End Using
                                Catch ex As Exception
                                End Try
                            End If
                            link = BatchOTALink.Replace(X, "")
                            ' Create an instance of StreamReader to read from a file.
                            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(Folder & X)
                                MyReader.TextFieldType = FileIO.FieldType.Delimited
                                MyReader.SetDelimiters(":")
                                While Not MyReader.EndOfData
                                    currentRow = MyReader.ReadFields()
                                    For Each currentField In currentRow
                                        If currentField.Contains(".cod") Then
                                            OTAFiles.Items.Add(currentField)
                                        End If
                                    Next
                                End While
                                MyReader.Close()
                            End Using
                            OTAProgressBar.Minimum = 1
                            ' Set Maximum to the total number of files to copy.
                            OTAProgressBar.Maximum = OTAFiles.Items.Count
                            ' Set the initial value of the ProgressBar.
                            OTAProgressBar.Value = 1
                            ' Set the Step property to a value of 1 to represent each file being copied.
                            OTAProgressBar.Step = 1
                            For Each codfile In OTAFiles.Items
                                If My.Computer.FileSystem.FileExists(Folder & codfile) Then
                                    My.Computer.FileSystem.DeleteFile(Folder & codfile)
                                    Using WC As New System.Net.WebClient()
                                        ''//Manually set the user agent header
                                        WC.Headers.Add("user-agent", bbuseragent)
                                        ''//Download the XML
                                        WC.DownloadFile(link & codfile, Folder & codfile)
                                    End Using
                                    OTAProgressBar.PerformStep()
                                Else
                                    Using WC As New System.Net.WebClient()
                                        ''//Manually set the user agent header
                                        WC.Headers.Add("user-agent", bbuseragent)
                                        ''//Download the XML
                                        WC.DownloadFile(link & codfile, Folder & codfile)
                                    End Using
                                    OTAProgressBar.PerformStep()
                                End If
                            Next
                        Else
                        End If
                    Next
                    'Convert to ALX
                    If Convert2Alx.Checked = True Then
                        AppText.Text = OTAName.Text
                        Description.Text = OTADesc.Text
                        'Description.Text = Description.Text.Replace(" - Follow: http://twitter.com/rr_yy", "")
                        VersionText.Text = OTAVersion.Text
                        VendorText.Text = OTAVendor.Text
                        FileText.Text = OTAName.Text
                        If My.Computer.FileSystem.FileExists(Folder & FileText.Text & ".alx") Then
                            ToolStripStatusLabel1.Text = FileText.Text & ".alx already exists, delete or rename it..."
                            MsgBox(FileText.Text & ".alx already exists, delete or rename it!")
                        Else
                            ToolStripStatusLabel1.Text = "Creating .alx file for your Desktop Install..."
                            Dim ioFile As New StreamWriter(Folder & FileText.Text & ".alx")
                            ioFile.WriteLine("<loader version=" & Chr(34) & "1.0" & Chr(34) & ">")
                            ioFile.WriteLine("	<application id=" & Chr(34) & AppText.Text & Chr(34) & ">")
                            ioFile.WriteLine("		<name>" & AppText.Text & "</name>")
                            ioFile.WriteLine("		<description>" & Description.Text & "</description>")
                            ioFile.WriteLine("		<version>" & VersionText.Text & "</version>")
                            ioFile.WriteLine("		<vendor>" & VendorText.Text & "</vendor>")
                            ioFile.WriteLine("		<fileset Java=" & Chr(34) & "1.0" & Chr(34) & ">")
                            If JavaBox.Checked Then
                                ioFile.WriteLine("			<directory>Java</directory>")
                            Else
                                ioFile.WriteLine("			<directory></directory>")
                            End If
                            ioFile.WriteLine("			<files>")
                            'STREAMWRITER TO WRITE THE FILE
                            'LOOP THE LISTBOX ITEMS, WRITING 1 LINE PER ITEM
                            For Each X As String In OTAFiles.Items
                                If X.Contains(".cod") Then
                                    ioFile.WriteLine("				 " & X)
                                End If
                            Next
                            ioFile.WriteLine("			</files>")
                            ioFile.WriteLine("		</fileset>")
                            ioFile.WriteLine("	</application>")
                            ioFile.WriteLine("</loader>")
                            'CLOSE THE FILE
                            ioFile.Close()
                            'FileBox.Items.Add(FileText.Text & ".alx")
                            'FilePathBox.Items.Add(My.Settings.CaJDir & FileText.Text & ".alx")
                            ToolStripStatusLabel1.Text = ".alx file created for your Desktop Install..."
                            ToolStripStatusLabel1.Text = "Your " & OTAName.Text & " has been downloaded " & FileText.Text & ".alx has been created!"
                            'MsgBox("Your " & OTAName.Text & " has been downloaded " & FileText.Text & ".alx has been created!")
                            ' show we got the result back from the other thread...
                        End If
                    Else
                        ToolStripStatusLabel1.Text = "Your " & OTAName.Text & " OTA has been downloaded!"
                        'MsgBox("Your " & OTAName.Text & " OTA has been downloaded!")
                    End If
                    OTAFiles.Items.Clear()
                Catch ex As Exception
                    MsgBox("ERROR: " & BatchOTALink & " could not be downloaded because " & ex.Message)
                    OTAFiles.Items.Clear()
                End Try
            Else
            End If
        Next
        If OpenOTAFolder.Checked = True Then
            Shell("explorer " & OTASave.Text)
        End If
        'End If
        OTAName.Text = ""
        OTAVersion.Text = ""
        OTAVendor.Text = ""
        OTADesc.Text = ""
        BatchOTADownload.Enabled = True
        BatchOTAs.Visible = True
        ToolStripStatusLabel1.Text = "Your OTAs have been downloaded!"
        MsgBox("Your OTAs have been downloaded!")
        UseWaitCursor = False
    End Sub
    Sub BuildAHybrid(ByVal SearchString As String, ByVal Selection As ComboBox)
        Dim Install As String = My.Settings.InstallDir
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(Install & "\Java\", Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, SearchString)
            Dim foundFileInfo As New System.IO.FileInfo(foundFile)
            My.Computer.FileSystem.CopyFile(foundFile, Install & "\Java\ChangedFiles\" & foundFileInfo.Name, True)
        Next
    End Sub
    Public Sub BuildHybrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BuildHybrid.Click
        If OSFolder.Text = "" Then
            MsgBox("You must choose a folder to save your hybrid files to.")
        Else
            If BaHWorker.IsBusy Then Exit Sub
            BaHWorker.RunWorkerAsync()
            BuildHybrid.Enabled = False
            If BaHProgressBar.Value >= 100 Then
                BaHProgressBar.Value = 0
            End If
        End If
    End Sub
    Public Sub BaHWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BaHWorker.DoWork
        Dim Install = My.Settings.InstallDir
        Me.BaHProgressBar.Minimum = 0
        Me.BaHProgressBar.Maximum = 11
        Me.BaHProgressBar.Value = 1
        ' Set the Step property to a value of 1 to represent each file being copied.
        Me.BaHProgressBar.Step = 1

        If My.Computer.FileSystem.DirectoryExists(My.Settings.InstallDir & "JavaBackup\") Then
            Dim msg1 = "There is currently already a back up of your Java folder at " & My.Settings.InstallDir & "JavaBackup\ - Would you like to continue anyways?"
            Dim title1 = "Warning - Another Backup Exists!"
            Dim style1 = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or _
                        MsgBoxStyle.Critical
            ' Display the message box and save the response, Yes or No.
            Dim response1 = MsgBox(msg1, style1, title1)
            If response1 = MsgBoxResult.Yes Then
            Else
                ToolStripStatusLabel1.Text = "There's already a Java backup at " & My.Settings.InstallDir & "JavaBackup\ - Please remove/rename it before you continue."
            End If
        Else
            My.Computer.FileSystem.CreateDirectory(My.Settings.InstallDir & "JavaBackup\")
        End If

        ToolStripStatusLabel1.Text = "Backing up current Java..."
        My.Computer.FileSystem.CopyDirectory(My.Settings.InstallDir & "\Java\", My.Settings.InstallDir & "\JavaBackup\", True)
        ToolStripStatusLabel1.Text = "Building your Hybrid..."
        If AppManager.SelectedItem Is Nothing Then
        Else
            If OfflineMode.Checked = True Then
                Call BuildAHybrid("net_rim_app_manager*", AppManager)
            Else
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_app_manager.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_app_manager.cod")
                    ToolStripStatusLabel1.Text = "Downloading " & AppMgrBox.Text & " from OS " & AppManager.SelectedItem & " for your Hybrid..."
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & AppManager.SelectedItem & "/net_rim_app_manager.cod", Install & "\Java\net_rim_app_manager.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_app_manager.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_app_manager.cod exists. Make sure you have selected the right OS folder.")
                End If
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_app_manager_console.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_app_manager_console.cod")
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & AppManager.SelectedItem & "/net_rim_app_manager_console.cod", Install & "\Java\net_rim_app_manager_console.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_app_manager_console.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_app_manager_console.cod exists. Make sure you have selected the right OS folder.")
                End If
            End If
            BaHProgressBar.PerformStep()
        End If
        If Bluetooth.SelectedItem Is Nothing Then
        Else
            If OfflineMode.Checked = True Then
                Call BuildAHybrid("*bluetooth*", Bluetooth)
            Else
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_bluetooth.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_bluetooth.cod")
                    ToolStripStatusLabel1.Text = "Downloading " & BTBox.Text & " from OS " & Bluetooth.SelectedItem & " for your Hybrid..."
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & Bluetooth.SelectedItem & "/net_rim_bluetooth.cod", Install & "\Java\net_rim_bluetooth.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_bluetooth.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_bluetooth.cod exists. Make sure you have selected the right OS folder.")
                End If
            End If
            BaHProgressBar.PerformStep()
        End If
        If Camera.SelectedItem Is Nothing Then
        Else
            If OfflineMode.Checked = True Then
                Call BuildAHybrid("net_rim_bb_camera*", Camera)
            Else
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_bb_camera.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_bb_camera.cod")
                    ToolStripStatusLabel1.Text = "Downloading " & CamBox.Text & " from OS " & Camera.SelectedItem & " for your Hybrid..."
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & Camera.SelectedItem & "/net_rim_bb_camera.cod", Install & "\Java\net_rim_bb_camera.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_bb_camera.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_bb_camera.cod exists. Make sure you have selected the right OS folder.")
                End If
            End If
            BaHProgressBar.PerformStep()
        End If
        If Fonts.SelectedItem Is Nothing Then
        Else
            If OfflineMode.Checked = True Then
                Call BuildAHybrid("net_rim_font*", Fonts)
            Else
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_font_arabic_keypad.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_font_arabic_keypad.cod")
                    ToolStripStatusLabel1.Text = "Downloading " & FontsBox.Text & " from OS " & Fonts.SelectedItem & " for your Hybrid..."
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & Fonts.SelectedItem & "/net_rim_font_arabic_keypad.cod", Install & "\Java\net_rim_font_arabic_keypad.cod")
                Else
                End If
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_font_european_sff.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_font_european_sff.cod")
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & Fonts.SelectedItem & "/net_rim_font_european_sff.cod", Install & "\Java\net_rim_font_european_sff.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_font_european_sff.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_font_european_sff.cod exists. Make sure you have selected the right OS folder.")
                End If
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_font_latin_truetype.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_font_latin_truetype.cod")
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & Fonts.SelectedItem & "/net_rim_font_latin_truetype.cod", Install & "\Java\net_rim_font_latin_truetype.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_font_latin_truetype.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_font_latin_truetype.cod exists. Make sure you have selected the right OS folder.")
                End If
            End If
            BaHProgressBar.PerformStep()
        End If
        If GPS.SelectedItem Is Nothing Then
        Else
            If OfflineMode.Checked = True Then
                Call BuildAHybrid("net_rim_bb_gps_ee*", GPS)
                Call BuildAHybrid("net_rim_bb_gas*", GPS)
            Else
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_bb_gps_ee.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_bb_gps_ee.cod")
                    ToolStripStatusLabel1.Text = "Downloading " & GPSBox.Text & " from OS " & GPS.SelectedItem & " for your Hybrid..."
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & GPS.SelectedItem & "/net_rim_bb_gps_ee.cod", Install & "\Java\net_rim_bb_gps_ee.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_bb_gps_ee.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_bb_gps_ee.cod exists. Make sure you have selected the right OS folder.")
                End If
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_bb_gas.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_bb_gas.cod")
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & GPS.SelectedItem & "/net_rim_bb_gas.cod", Install & "\Java\net_rim_bb_gas.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_bb_gas.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_bb_gas.cod exists. Make sure you have selected the right OS folder.")
                End If
            End If
            BaHProgressBar.PerformStep()
        End If
        If Media.SelectedItem Is Nothing Then
        Else
            If OfflineMode.Checked = True Then
                Call BuildAHybrid("net_rim_bb_media*", Media)
                Call BuildAHybrid("net_rim_media*", Media)
            Else
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_bb_medialibrary.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_bb_medialibrary.cod")
                    ToolStripStatusLabel1.Text = "Downloading " & MediaBox.Text & " from OS " & Media.SelectedItem & " for your Hybrid..."
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & Media.SelectedItem & "/net_rim_bb_medialibrary.cod", Install & "\Java\net_rim_bb_medialibrary.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_bb_medialibrary.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_bb_medialibrary.cod exists. Make sure you have selected the right OS folder.")
                End If
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_bb_media_plugin.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_bb_media_plugin.cod")
                    ToolStripStatusLabel1.Text = "Downloading " & MediaBox.Text & " from OS " & Media.SelectedItem & " for your Hybrid..."
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & Media.SelectedItem & "/net_rim_bb_media_plugin.cod", Install & "\Java\net_rim_bb_media_plugin.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_bb_media_plugin.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_bb_media_plugin.cod exists. Make sure you have selected the right OS folder.")
                End If
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_media_actions.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_media_actions.cod")
                    ToolStripStatusLabel1.Text = "Downloading " & MediaBox.Text & " from OS " & Media.SelectedItem & " for your Hybrid..."
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & Media.SelectedItem & "/net_rim_media_actions.cod", Install & "\Java\net_rim_media_actions.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_media_actions.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_media_actions.cod exists. Make sure you have selected the right OS folder.")
                End If
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_media_actions_daemon.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_media_actions_daemon.cod")
                    ToolStripStatusLabel1.Text = "Downloading " & MediaBox.Text & " from OS " & Media.SelectedItem & " for your Hybrid..."
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & Media.SelectedItem & "/net_rim_media_actions_daemon.cod", Install & "\Java\net_rim_media_actions_daemon.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_media_actions_daemon.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_media_actions_daemon.cod exists. Make sure you have selected the right OS folder.")
                End If
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_bb_medialibraryplayer.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_bb_medialibraryplayer.cod")
                    ToolStripStatusLabel1.Text = "Downloading " & MediaBox.Text & " from OS " & Media.SelectedItem & " for your Hybrid..."
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & Media.SelectedItem & "/net_rim_bb_medialibraryplayer.cod", Install & "\Java\net_rim_bb_medialibraryplayer.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_bb_medialibraryplayer.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_bb_medialibraryplayer.cod exists. Make sure you have selected the right OS folder.")
                End If
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_bb_mediaapp_launcher_app.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_bb_mediaapp_launcher_app.cod")
                    ToolStripStatusLabel1.Text = "Downloading " & MediaBox.Text & " from OS " & Media.SelectedItem & " for your Hybrid..."
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & Media.SelectedItem & "/net_rim_bb_mediaapp_launcher_app.cod", Install & "\Java\net_rim_bb_mediaapp_launcher_app.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_bb_mediaapp_launcher_app.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_bb_mediaapp_launcher_app.cod exists. Make sure you have selected the right OS folder.")
                End If
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_bb_mediacontenthandler.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_bb_mediacontenthandler.cod")
                    ToolStripStatusLabel1.Text = "Downloading " & MediaBox.Text & " from OS " & Media.SelectedItem & " for your Hybrid..."
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & Media.SelectedItem & "/net_rim_bb_mediacontenthandler.cod", Install & "\Java\net_rim_bb_mediacontenthandler.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_bb_mediacontenthandler.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_bb_mediacontenthandler.cod exists. Make sure you have selected the right OS folder.")
                End If
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_media.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_media.cod")
                    ToolStripStatusLabel1.Text = "Downloading " & MediaBox.Text & " from OS " & Media.SelectedItem & " for your Hybrid..."
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & Media.SelectedItem & "/net_rim_media.cod", Install & "\Java\net_rim_media.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_media.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_media.cod exists. Make sure you have selected the right OS folder.")
                End If
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_media_api.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_media_api.cod")
                    ToolStripStatusLabel1.Text = "Downloading " & MediaBox.Text & " from OS " & Media.SelectedItem & " for your Hybrid..."
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & Media.SelectedItem & "/net_rim_media_api.cod", Install & "\Java\net_rim_media_api.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_media_api.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_media_api.cod exists. Make sure you have selected the right OS folder.")
                End If
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_plazmic_mediaengine.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_plazmic_mediaengine.cod")
                    ToolStripStatusLabel1.Text = "Downloading " & MediaBox.Text & " from OS " & Media.SelectedItem & " for your Hybrid..."
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & Media.SelectedItem & "/net_rim_plazmic_mediaengine.cod", Install & "\Java\net_rim_plazmic_mediaengine.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_plazmic_mediaengine.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_plazmic_mediaengine.cod exists. Make sure you have selected the right OS folder.")
                End If
            End If
            BaHProgressBar.PerformStep()
        End If
        If Plazmic.SelectedItem Is Nothing Then
        Else
            If OfflineMode.Checked = True Then
                Call BuildAHybrid("net_rim_plazmic*", Plazmic)
            Else
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_plazmic_mediaengine_smil_00.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_plazmic_mediaengine_smil_00.cod")
                    ToolStripStatusLabel1.Text = "Downloading " & PlazmicBox.Text & " from OS " & Plazmic.SelectedItem & " for your Hybrid..."
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & Plazmic.SelectedItem & "/net_rim_plazmic_mediaengine_smil_00.cod", Install & "\Java\net_rim_plazmic_mediaengine_smil_00.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_plazmic_mediaengine_smil_00.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_plazmic_mediaengine_smil_00.cod exists. Make sure you have selected the right OS folder.")
                End If
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_plazmic_mediaengine_smil_format.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_plazmic_mediaengine_smil_format.cod")
                    ToolStripStatusLabel1.Text = "Downloading " & PlazmicBox.Text & " from OS " & Plazmic.SelectedItem & " for your Hybrid..."
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & Plazmic.SelectedItem & "/net_rim_plazmic_mediaengine_smil_format.cod", Install & "\Java\net_rim_plazmic_mediaengine_smil_format.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_plazmic_mediaengine_smil_format.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_plazmic_mediaengine_smil_format.cod exists. Make sure you have selected the right OS folder.")
                End If
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_plazmic_mediaengine_smil_mms.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_plazmic_mediaengine_smil_mms.cod")
                    ToolStripStatusLabel1.Text = "Downloading " & PlazmicBox.Text & " from OS " & Plazmic.SelectedItem & " for your Hybrid..."
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & Plazmic.SelectedItem & "/net_rim_plazmic_mediaengine_smil_mms.cod", Install & "\Java\net_rim_plazmic_mediaengine_smil_mms.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_plazmic_mediaengine_smil_mms.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_plazmic_mediaengine_smil_mms.cod exists. Make sure you have selected the right OS folder.")
                End If
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_plazmic_pushactivator.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_plazmic_pushactivator.cod")
                    ToolStripStatusLabel1.Text = "Downloading " & PlazmicBox.Text & " from OS " & Plazmic.SelectedItem & " for your Hybrid..."
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & Plazmic.SelectedItem & "/net_rim_plazmic_pushactivator.cod", Install & "\Java\net_rim_plazmic_pushactivator.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_plazmic_pushactivator.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_plazmic_pushactivator.cod exists. Make sure you have selected the right OS folder.")
                End If
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_plazmic_themereader.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_plazmic_themereader.cod")
                    ToolStripStatusLabel1.Text = "Downloading " & PlazmicBox.Text & " from OS " & Plazmic.SelectedItem & " for your Hybrid..."
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & Plazmic.SelectedItem & "/net_rim_plazmic_themereader.cod", Install & "\Java\net_rim_plazmic_themereader.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_plazmic_themereader.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_plazmic_themereader.cod exists. Make sure you have selected the right OS folder.")
                End If
            End If
            BaHProgressBar.PerformStep()
        End If
        If PLauncher.SelectedItem Is Nothing Then
        Else
            If OfflineMode.Checked = True Then
                Call BuildAHybrid("net_rim_process_launcher*", PLauncher)
            Else
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_process_launcher.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_process_launcher.cod")
                    ToolStripStatusLabel1.Text = "Downloading " & PLBox.Text & " from OS " & PLauncher.SelectedItem & " for your Hybrid..."
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & PLauncher.SelectedItem & "/net_rim_process_launcher.cod", Install & "\Java\net_rim_process_launcher.cod")
                Else
                    ToolStripStatusLabel1.Text = "net_rim_process_launcher.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_process_launcher.cod exists. Make sure you have selected the right OS folder.")
                End If
            End If
            BaHProgressBar.PerformStep()
        End If
        If VideoCamera.SelectedItem Is Nothing Then
        Else
            If OfflineMode.Checked = True Then
                Call BuildAHybrid("net_rim_bb_videorecorder.cod", VideoCamera)
            Else
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_bb_videorecorder.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_bb_videorecorder.cod")
                    ToolStripStatusLabel1.Text = "Downloading " & VidBox.Text & " from OS " & VideoCamera.SelectedItem & " for your Hybrid..."
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & VideoCamera.SelectedItem & "/net_rim_bb_videorecorder.cod", Install & "\Java\net_rim_bb_videorecorder.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_bb_videorecorder.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_bb_videorecorder.cod exists. Make sure you have selected the right OS folder.")
                End If
            End If
            BaHProgressBar.PerformStep()
        End If
        If Keyboard.SelectedItem Is Nothing Then
        Else
            If OfflineMode.Checked = True Then
                Call BuildAHybrid("net_rim_bb_virtual_keyboard.cod", Keyboard)
            Else
                If My.Computer.FileSystem.FileExists(Install & "\Java\net_rim_bb_virtual_keyboard.cod") Then
                    System.IO.File.Delete(Install & "\Java\net_rim_bb_virtual_keyboard.cod")
                    ToolStripStatusLabel1.Text = "Downloading " & KeyboardBox.Text & " from OS " & Keyboard.SelectedItem & " for your Hybrid..."
                    My.Computer.Network.DownloadFile("http://www.theiexplorers.com/BBHTool/BaH/" & Keyboard.SelectedItem & "/net_rim_bb_virtual_keyboard.cod", Install & "\Java\net_rim_bb_virtual_keyboard.cod")
                Else
                    ToolStripStatusLabel1.Text = "No net_rim_bb_virtual_keyboard.cod exists. Make sure you have selected the right OS folder."
                    MsgBox("No net_rim_bb_virtual_keyboard.cod exists. Make sure you have selected the right OS folder.")
                End If
            End If
            BaHProgressBar.PerformStep()
        End If
        If Me.BaHProgressBar.Value = 11 Then
        Else
            Me.BaHProgressBar.Value = 11
        End If
        ToolStripStatusLabel1.Text = "Your hybrid has been built!"
        MsgBox("Your hybrid has been built!")
        BuildHybrid.Enabled = True
    End Sub
    Public Sub ReadFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReadFiles.Click
        JavaLoaderProgress.Value = 0
        JavaLoaderProgress.Maximum = 600
        DataGridView1.Rows.Clear()
        ReadEventLog.Enabled = False
        Call ReadDeviceFiles()
    End Sub
    Sub ReadDeviceFiles()
        DataGridView1.Visible = True
        ToolStripStatusLabel1.Text = "Reading File System from Device..."
        Dim myProcess As New Process()
        Dim myProcessStartInfo As New ProcessStartInfo("JavaLoader.exe", "-w" & DevicePass.Text & " dir -1")
        myProcessStartInfo.UseShellExecute = False
        myProcessStartInfo.CreateNoWindow = True
        myProcessStartInfo.RedirectStandardOutput = True
        myProcess.StartInfo = myProcessStartInfo
        myProcess.Start()
        Dim myStreamReader As StreamReader = myProcess.StandardOutput
        Dim myString As String
        ' Read the standard output of the spawned process.

        Do
            myString = myStreamReader.ReadLine()
            DataGridView1.Rows.Add(myString + ".cod")
            'DataGridView1.Rows.Add(myString + ".cod", "version", "size")
            JavaLoaderBox.Items.Add(myString + ".cod")
        Loop Until myProcess.StandardOutput.EndOfStream
        RemoveCOD.Enabled = True
        SaveCOD.Enabled = True
        SaveAllCods.Enabled = True
        ReadEventLog.Enabled = True
        SaveEventLog.Enabled = False
        ReadCount.Text = "Total Files: " & DataGridView1.Rows.Count
        ToolStripStatusLabel1.Text = "File System read, Displaying File System above"
        JavaLoaderProgress.Value = JavaLoaderProgress.Maximum
    End Sub
    Public Sub SaveRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveRead.Click
        If SaveReadWorker.IsBusy Then Exit Sub
        JavaLoaderProgress.Value = 0
        JavaLoaderProgress.Maximum = 600
        SaveReadWorker.RunWorkerAsync()
    End Sub
    Public Sub SaveReadWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles SaveReadWorker.DoWork
        JavaLoaderProgress.PerformStep()
        ToolStripStatusLabel1.Text = "Saving File System read to SystemRead-" & UserPIN.Text & ".txt..."
        If My.Computer.FileSystem.FileExists("SystemRead-" & UserPIN.Text & ".txt") Then
            My.Computer.FileSystem.DeleteFile("SystemRead-" & UserPIN.Text & ".txt")
        Else
        End If
        Dim myProcess As New Process()
        Dim myProcessStartInfo As New ProcessStartInfo("JavaLoader.exe", " -w" & DevicePass.Text & " dir")
        myProcessStartInfo.UseShellExecute = False
        myProcessStartInfo.CreateNoWindow = True
        myProcessStartInfo.RedirectStandardOutput = True
        myProcess.StartInfo = myProcessStartInfo
        myProcess.Start()
        Dim myStreamReader As StreamReader = myProcess.StandardOutput
        Dim myString As String
        Dim SystemRead As New StreamWriter("SystemRead-" & UserPIN.Text & ".txt")
        ' Read the standard output of the spawned process.
        SystemRead.WriteLine("PIN: " & UserPIN.Text)
        SystemRead.WriteLine("Name                                           Version          Size      Created")
        Do
            For Each myString In myStreamReader.ReadLine
                myString = myStreamReader.ReadLine()
                If myString = "Disconnected" Then
                ElseIf myString = "" Then
                Else
                    SystemRead.WriteLine(myString)
                End If
                JavaLoaderProgress.PerformStep()
            Next
        Loop Until myProcess.StandardOutput.EndOfStream
        ToolStripStatusLabel1.Text = "System Read saved to SystemRead-" & UserPIN.Text & ".txt"
        MsgBox("File System Read saved to SystemRead-" & UserPIN.Text & ".txt")
        myProcess.Close()
        SystemRead.Close()
        JavaLoaderProgress.Value = JavaLoaderProgress.Maximum
    End Sub
    Public Sub ReadEventLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReadEventLog.Click
        If JavaLoaderBox.Items.Count = 0 Then
        Else
            JavaLoaderBox.Items.Clear()
        End If
        If ReadLogWorker.IsBusy Then Exit Sub
        JavaLoaderProgress.Value = 0
        JavaLoaderProgress.Maximum = 3
        SaveCOD.Enabled = False
        SaveEventLog.Enabled = True
        DataGridView1.Visible = False
        ReadLogWorker.RunWorkerAsync()
    End Sub
    Public Sub ReadLogWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles ReadLogWorker.DoWork
        ToolStripStatusLabel1.Text = "Reading Event Log..."
        ReadCount.Text = ""
        JavaLoaderProgress.PerformStep()
        Dim myProcess As New Process()
        Dim myProcessStartInfo As New ProcessStartInfo("JavaLoader.exe", " -w" & DevicePass.Text & " eventlog")
        myProcessStartInfo.UseShellExecute = False
        myProcessStartInfo.CreateNoWindow = True
        myProcessStartInfo.RedirectStandardOutput = True
        myProcess.StartInfo = myProcessStartInfo
        myProcess.Start()
        Dim myStreamReader As StreamReader = myProcess.StandardOutput
        Dim myString As String
        ' Read the standard output of the spawned process.
        Do
            For Each myString In myStreamReader.ReadLine
                myString = myStreamReader.ReadLine()
                If myString = "" Then
                Else
                    JavaLoaderBox.Items.Add(myString)
                End If
            Next
        Loop Until myProcess.StandardOutput.EndOfStream
        JavaLoaderProgress.PerformStep()
        myProcess.Close()
        ReadFiles.Enabled = True
        ToolStripStatusLabel1.Text = "Event Log read, displaying above."
        JavaLoaderProgress.PerformStep()
    End Sub
    Public Sub RemoveCOD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveCOD.Click
        If RemoveFilesWorker.IsBusy Then Exit Sub
        JavaLoaderProgress.Value = 0
        JavaLoaderProgress.Maximum = 1
        RemoveFilesWorker.RunWorkerAsync()
    End Sub
    Public Sub RemoveFilesWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles RemoveFilesWorker.DoWork
        filelist = ""
        JavaLoaderProgress.Maximum = DataGridView1.SelectedCells.Count + 1
        ToolStripStatusLabel1.Text = "Removing .COD File(s)..."
        Dim selectedCellCount As Integer = DataGridView1.GetCellCount(DataGridViewElementStates.Selected)
        If selectedCellCount = 0 Then
            MsgBox("There are no files selected for removal...")
        Else
            Dim i As Integer
            For i = 0 To DataGridView1.SelectedCells.Count - 1
                If DataGridView1.SelectedCells.Item(i).Value = "" Then
                    JavaLoaderProgress.PerformStep()
                Else
                    filelist = DataGridView1.SelectedCells.Item(i).Value & " " & filelist
                    JavaLoaderProgress.PerformStep()
                End If
            Next
            Dim myProcess As New Process()
            Dim myProcessStartInfo As New ProcessStartInfo("JavaLoader.exe", " -w" & DevicePass.Text & " erase -f " & filelist)
            myProcessStartInfo.UseShellExecute = False
            myProcessStartInfo.CreateNoWindow = True
            myProcessStartInfo.RedirectStandardOutput = True
            myProcess.StartInfo = myProcessStartInfo
            myProcess.Start()
            myProcess.Close()
            ToolStripStatusLabel1.Text = ".COD file(s) were successfully removed."
            MsgBox(".COD file(s) were successfully removed.")
            JavaLoaderProgress.PerformStep()
        End If
    End Sub
    Public Sub SaveCOD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveCOD.Click
        If SaveFilesWorker.IsBusy Then Exit Sub
        JavaLoaderProgress.Value = 0
        JavaLoaderProgress.Maximum = 1
        SaveFilesWorker.RunWorkerAsync()
    End Sub
    Public Sub SaveFilesWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles SaveFilesWorker.DoWork
        filelist = ""
        Dim selectedCellCount As Integer = DataGridView1.GetCellCount(DataGridViewElementStates.Selected)
        If selectedCellCount = 0 Then
            MsgBox("There are no files selected for saving...")
        Else
            JavaLoaderProgress.Maximum = DataGridView1.SelectedCells.Count + 1
            If My.Computer.FileSystem.DirectoryExists("CODs") Then
            Else
                My.Computer.FileSystem.CreateDirectory("CODs")
            End If
            ToolStripStatusLabel1.Text = "Saving selected .COD(s) to " & My.Computer.FileSystem.CurrentDirectory & "\CODs\"
            Dim i As Integer
            For i = 0 To DataGridView1.SelectedCells.Count - 1
                If DataGridView1.SelectedCells.Item(i).Value = "" Then
                    JavaLoaderProgress.PerformStep()
                Else
                    filelist = DataGridView1.SelectedCells.Item(i).Value & " " & filelist
                    JavaLoaderProgress.PerformStep()
                End If
            Next
            Dim myProcess As New Process()
            Dim myProcessStartInfo As New ProcessStartInfo("""" & My.Computer.FileSystem.CurrentDirectory & "\JavaLoader.exe""", " -w" & DevicePass.Text & " save " & filelist)
            myProcessStartInfo.UseShellExecute = False
            myProcessStartInfo.WorkingDirectory = My.Computer.FileSystem.CurrentDirectory & "\CODs\"
            myProcessStartInfo.CreateNoWindow = True
            myProcessStartInfo.RedirectStandardOutput = True
            myProcess.EnableRaisingEvents = True
            ' add an Exited event handler
            myProcess.StartInfo = myProcessStartInfo
            myProcess.Start()
            AddHandler myProcess.Exited, AddressOf Me.SaveCODProcessExited
        End If
    End Sub
    Private Sub SaveCODProcessExited(ByVal sender As Object, ByVal e As System.EventArgs)
        ToolStripStatusLabel1.Text = "Selected .COD(s) have been saved to " & My.Computer.FileSystem.CurrentDirectory & "\CODs\"
        MsgBox("Selected .COD(s) have been saved to " & My.Computer.FileSystem.CurrentDirectory & "\CODs\")
        JavaLoaderProgress.PerformStep()
    End Sub
    Public Sub SaveAllCods_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAllCods.Click
        If SaveAllWorker.IsBusy Then Exit Sub
        Call ReadDeviceFiles()
        SaveAllWorker.RunWorkerAsync()
    End Sub
    Public Sub SaveAllWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles SaveAllWorker.DoWork
        filelist = ""
        If UserPIN.Text = "Not Connected" Then
            ToolStripStatusLabel1.Text = "Please plug your phone in and try again - If it is plugged in, try a different USB port!"
            MsgBox("Please plug your phone in and try again - If it is plugged in, try a different USB port!")
        Else
            If My.Computer.FileSystem.DirectoryExists("Dump-" & UserPIN.Text) Then
            Else
                My.Computer.FileSystem.CreateDirectory("Dump-" & UserPIN.Text)
            End If
            ToolStripStatusLabel1.Text = "Saving all .COD(s) from the Device to " & My.Computer.FileSystem.CurrentDirectory & "\" & "Dump-" & UserPIN.Text
            For Each S As String In JavaLoaderBox.Items
                If S = "" Then
                Else
                    filelist = S & " " & filelist
                    JavaLoaderProgress.PerformStep()
                End If
            Next
            Dim myProcess1 As New Process()
            Dim myProcessStartInfo1 As New ProcessStartInfo("""" & My.Computer.FileSystem.CurrentDirectory & "\JavaLoader.exe""", " -w" & DevicePass.Text & " save " & filelist)
            myProcessStartInfo1.UseShellExecute = False
            myProcessStartInfo1.WorkingDirectory = My.Computer.FileSystem.CurrentDirectory & "\Dump-" & UserPIN.Text & "\"
            myProcessStartInfo1.CreateNoWindow = True
            myProcessStartInfo1.RedirectStandardOutput = True
            myProcess1.EnableRaisingEvents = True
            ' add an Exited event handler
            AddHandler myProcess1.Exited, AddressOf Me.SaveAllCODsProcessExited
            myProcess1.StartInfo = myProcessStartInfo1
            myProcess1.Start()
        End If
    End Sub
    Private Sub SaveAllCODsProcessExited(ByVal sender As Object, ByVal e As System.EventArgs)
        MsgBox("All .COD(s) have been saved to \Dump-" & UserPIN.Text)
        ToolStripStatusLabel1.Text = "All .COD(s) have been saved to \Dump-" & UserPIN.Text
    End Sub
    Public Sub SaveEventLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveEventLog.Click
        If SaveLogWorker.IsBusy Then Exit Sub
        JavaLoaderProgress.Value = 0
        JavaLoaderProgress.Maximum = 1
        SaveLogWorker.RunWorkerAsync()
    End Sub
    Public Sub SaveLogWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles SaveLogWorker.DoWork
        If JavaLoaderBox.Items.Count = 0 Then
            MsgBox("Please Read your file system Event Log first before trying to save it.")
        Else
            JavaLoaderProgress.Maximum = JavaLoaderBox.Items.Count + 1
            ToolStripStatusLabel1.Text = "Saving Event Log to EventLog-" & UserPIN.Text & ".txt"
            ' Read the standard output of the spawned process.
            If My.Computer.FileSystem.FileExists("EventLog-" & UserPIN.Text & ".txt") Then
                My.Computer.FileSystem.DeleteFile("EventLog-" & UserPIN.Text & ".txt")
            Else
            End If
            Dim EventLogRead As New StreamWriter("EventLog-" & UserPIN.Text & ".txt")
            ' Read the standard output of the spawned process.
            EventLogRead.WriteLine("Event Log for PIN: " & UserPIN.Text)
            EventLogRead.WriteLine("----------------------------")
            For Each S As String In JavaLoaderBox.Items
                If S = "" Then
                Else
                    EventLogRead.WriteLine(S)
                End If
            Next
            ToolStripStatusLabel1.Text = "Your Event Log has been saved as EventLog-" & UserPIN.Text & ".txt in the current directory."
            MsgBox("Your Event Log has been saved as EventLog-" & UserPIN.Text & ".txt in the current directory.")
        End If
        JavaLoaderProgress.PerformStep()
    End Sub
    Public Sub ClearEventLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearEventLog.Click
        JavaLoaderProgress.Value = 0
        JavaLoaderProgress.Maximum = 2
        JavaLoaderProgress.PerformStep()
        ToolStripStatusLabel1.Text = "Clearing Event Log..."
        Dim myProcess As New Process()
        Dim myProcessStartInfo As New ProcessStartInfo("JavaLoader.exe", " -w" & DevicePass.Text & " cleareventlog")
        myProcessStartInfo.UseShellExecute = False
        myProcessStartInfo.CreateNoWindow = True
        myProcessStartInfo.RedirectStandardOutput = True
        myProcess.StartInfo = myProcessStartInfo
        myProcess.Start()
        myProcess.Close()
        ToolStripStatusLabel1.Text = "Your Event Log has been cleared."
        MsgBox("Your Event Log has been cleared.")
        JavaLoaderProgress.PerformStep()
    End Sub
    Public Sub ConnectPhone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectPhone.Click
        JavaLoaderProgress.Value = 0
        JavaLoaderProgress.Maximum = 1
        Call ConnectToPhone()
    End Sub
    Sub ConnectToPhone()
        ToolStripStatusLabel1.Text = "Connecting to your Device..."
        Dim myProcess As New Process()
        Dim JavaCommand As String
        Dim password As String = DevicePass.Text
        JavaCommand = "-w" & password & " deviceinfo"
        Dim myProcessStartInfo As New ProcessStartInfo("JavaLoader.exe", JavaCommand)
        myProcessStartInfo.UseShellExecute = False
        myProcessStartInfo.CreateNoWindow = True
        myProcessStartInfo.RedirectStandardOutput = True
        myProcess.StartInfo = myProcessStartInfo
        myProcess.Start()

        Dim myStreamReader As StreamReader = myProcess.StandardOutput
        Dim myString As String
        ' Read the standard output of the spawned process.
        Do
            myString = myStreamReader.ReadLine()
            Try
                If myString.Contains("PIN:") Then
                    myString = myString.Replace("PIN:                0x", "")
                    UserPIN.Text = myString.ToUpper
                    ReadFiles.Enabled = True
                    SaveRead.Enabled = True
                    InstallCOD.Enabled = True
                    SaveAllCods.Enabled = True
                    SaveApps.Enabled = True
                    RestoreApps.Enabled = True
                    ReadEventLog.Enabled = True
                    ClearEventLog.Enabled = True
                    RecoveryMem.Enabled = True
                    Screenshot.Enabled = True
                    SyncTime.Enabled = True
                    FactoryReset.Enabled = True
                    WipeDevice.Enabled = True
                    ToolStripStatusLabel1.Text = "Successfully connected to your Device: " & UserPIN.Text & "."
                    JavaLoaderBox.Items.Clear()
                    JavaLoaderProgress.PerformStep()

                End If
            Catch V As Exception
            End Try
        Loop Until myProcess.StandardOutput.EndOfStream
        myProcess.Close()
        If UserPIN.Text = "Not Connected" Then
            ToolStripStatusLabel1.Text = "Please plug your phone in and try again - If it is plugged in, try a different USB port!"
            MsgBox("Please plug your phone in and try again - If it is plugged in, try a different USB port!")
        End If
    End Sub
    Public Sub InstallCOD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InstallCOD.Click
        JavaLoaderProgress.Value = 0
        filelist = ""
        ToolStripStatusLabel1.Text = "Browsing... (Select your .cod/.jad files to install)"
        ' Create an instance of the open file dialog box.
        ' Set filter options and filter index.
        OpenFileDialog1.Filter = "All Formats|*.cod;*.jad;*.rem|COD Files (*.cod)|*.cod|JAD Files (*.jad)|*.jad|REM Files (*.rem)|*.rem"
        OpenFileDialog1.FilterIndex = 1
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Multiselect = True
        ' Call the ShowDialog method to show the dialogbox.

        ' Process input if the user clicked OK.
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            JavaLoaderProgress.Maximum = OpenFileDialog1.FileNames.Count + 1
            ToolStripStatusLabel1.Text = "Installing .COD(s)..."
            Dim file As String
            For Each file In OpenFileDialog1.FileNames
                If file.Contains(".rem") Then
                    My.Computer.FileSystem.CopyFile(file, file.Replace(".rem", "") & ".cod", True)
                    file = file.Replace(".rem", "") & ".cod"
                End If
                filelist = filelist & """" & file & """ "
                JavaLoaderProgress.PerformStep()
            Next
            Dim myProcess As New Process()
            Dim myProcessStartInfo As New ProcessStartInfo("JavaLoader.exe", " -w" & DevicePass.Text & " load " & filelist)
            myProcessStartInfo.UseShellExecute = False
            myProcessStartInfo.CreateNoWindow = True
            myProcessStartInfo.RedirectStandardOutput = True
            myProcess.StartInfo = myProcessStartInfo
            myProcess.Start()
            myProcess.Close()
            ToolStripStatusLabel1.Text = "Selected .COD(s) have been installed to the connected device. Enjoy!"
            MsgBox("Selected .COD(s) have been installed to the connected device. Enjoy!")
            JavaLoaderProgress.PerformStep()
        Else
            ToolStripStatusLabel1.Text = "You did not choose any .COD(s). Please choose some .COD(s) to install."
            MsgBox("You did not choose any .COD(s). Please choose some .COD(s) to install.")
        End If
    End Sub
    Public Sub Screenshot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Screenshot.Click
        UseWaitCursor = True
        If My.Computer.FileSystem.DirectoryExists("Screenshots") Then
        Else
            My.Computer.FileSystem.CreateDirectory("Screenshots")
        End If
        JavaLoaderProgress.Value = 0
        JavaLoaderProgress.Maximum = 1
        ToolStripStatusLabel1.Text = "Taking screenshot of your Device's screen..."
        Dim x As Integer = Now.Millisecond
        Dim FileName As String = "Screenshot-" & Format(Now(), "mm_dd_yyyy_" & x) & ".bmp"
        Shell("""JavaLoader.exe"" -w" & DevicePass.Text & " screenshot Screenshots\" & FileName, AppWinStyle.MinimizedNoFocus)
        ToolStripStatusLabel1.Text = "Screenshot saved to " & FileName
        JavaLoaderProgress.PerformStep()
        Sleep(5000)
        Dim img As Image = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Screenshots\" & FileName)
        SSViewer.PictureBox1.Image = img
        SSViewer.DevicePasswordText = DevicePass.Text
        SSViewer.FileNameText = FileName
        UseWaitCursor = False
        SSViewer.ShowDialog()
    End Sub
    Public Sub SyncTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SyncTime.Click
        JavaLoaderProgress.Value = 0
        JavaLoaderProgress.Maximum = 1
        ToolStripStatusLabel1.Text = "Syncing PC time to Device..."
        Dim myProcess As New Process()
        Dim myProcessStartInfo As New ProcessStartInfo("JavaLoader.exe", "-w" & DevicePass.Text & " settime")
        myProcessStartInfo.UseShellExecute = False
        myProcessStartInfo.CreateNoWindow = True
        myProcessStartInfo.RedirectStandardOutput = True
        myProcess.StartInfo = myProcessStartInfo
        myProcess.Start()
        myProcess.Close()
        JavaLoaderProgress.PerformStep()
        ToolStripStatusLabel1.Text = "Your Device's time has been synced to your computer's time."
        MsgBox("Your Device's time has been synced to your computer's time.")
    End Sub
    Public Sub FactoryReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FactoryReset.Click
        JavaLoaderProgress.Value = 0
        JavaLoaderProgress.Maximum = 2
        Dim myProcess As New Process()
        Dim myProcessStartInfo As New ProcessStartInfo("JavaLoader.exe", " -w" & DevicePass.Text & " resettofactory")
        myProcessStartInfo.UseShellExecute = False
        myProcessStartInfo.CreateNoWindow = True
        myProcessStartInfo.RedirectStandardOutput = True
        myProcess.StartInfo = myProcessStartInfo
        Dim msg1 = "Are you sure you would like to restore your Device's IT Policy to factory settings?"
        Dim title1 = "Restore Factory Settings?"
        Dim style1 = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or _
                    MsgBoxStyle.Critical
        ' Display the message box and save the response, Yes or No.
        Dim response1 = MsgBox(msg1, style1, title1)
        If response1 = MsgBoxResult.Yes Then
            JavaLoaderProgress.PerformStep()
            Dim strName$
            strName = InputBox("Please enter the word reset below to confirm, otherwise click the X to cancel.", "")
            If strName = "reset" Then
                ToolStripStatusLabel1.Text = "Restoring your Device's IT Policy to factory settings..."
                myProcess.Start()
                myProcess.Close()
                ToolStripStatusLabel1.Text = "Your Device's IT Policy has been restored to factory settings."
                MsgBox("Your Device's IT Policy has been restored to factory settings.")
            Else
                ToolStripStatusLabel1.Text = "Your Device's IT Policy has not been restored to factory settings, as you selected."
                MsgBox("Your Device's IT Policy has not been restored to factory settings, as you selected.")
            End If
        Else
            JavaLoaderProgress.PerformStep()
            ToolStripStatusLabel1.Text = "Your Device's IT Policy has not been restored to factory settings, as you selected."
            MsgBox("Your Device's IT Policy has not been restored to factory settings, as you selected.")
        End If
        JavaLoaderProgress.PerformStep()
    End Sub
    Public Sub WipeDevice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WipeDevice.Click
        JavaLoaderProgress.Value = 0
        JavaLoaderProgress.Maximum = 2
        Dim myProcess As New Process()
        Dim myProcessStartInfo As New ProcessStartInfo("JavaLoader.exe", " -w" & DevicePass.Text & " wipe")
        myProcessStartInfo.UseShellExecute = False
        myProcessStartInfo.CreateNoWindow = True
        myProcessStartInfo.RedirectStandardOutput = True
        myProcess.StartInfo = myProcessStartInfo
        Dim msg1 = "Are you sure you would like to wipe your Device?"
        Dim title1 = "Wipe Device?"
        Dim style1 = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or _
                    MsgBoxStyle.Critical
        ' Display the message box and save the response, Yes or No.
        Dim response1 = MsgBox(msg1, style1, title1)
        If response1 = MsgBoxResult.Yes Then
            JavaLoaderProgress.PerformStep()
            Dim strName$
            strName = InputBox("Please enter the word wipe below to confirm, otherwise click the X to cancel.", "")
            If strName = "wipe" Then
                ToolStripStatusLabel1.Text = "Wiping your Device..."
                myProcess.Start()
                myProcess.Close()
                ToolStripStatusLabel1.Text = "Your Device has been successfully wiped."
                MsgBox("Your Device has been successfully wiped.")
            Else
                ToolStripStatusLabel1.Text = "Your Device has not been successfully wiped, as you did not enter the confirmation word."
                MsgBox("Your Device has not been successfully wiped, as you did not enter the confirmation word.")
            End If
        Else
            JavaLoaderProgress.PerformStep()
            ToolStripStatusLabel1.Text = "Your Device has not been successfully wiped, as you selected."
            MsgBox("Your Device has not been successfully wiped, as you selected.")
        End If
        JavaLoaderProgress.PerformStep()
    End Sub
    Public Sub TweetShrink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TweetShrink.Click
        System.Diagnostics.Process.Start("http://twitter.com/intent/tweet?text=@BBHPlus I just Shrunk My " & My.Settings.OSFolderBox & " OS from " & My.Settings.TweetSize & " to " & My.Settings.TweetShrunkSize & " using BBH Tool: Shrink-A-OS %23BBHPlus")
    End Sub
    Private Sub LaunchLoaderAfterShrinkToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LaunchLoaderAfterShrinkToolStripMenuItem.Click
        If LaunchLoaderAfterShrinkToolStripMenuItem.Checked = True Then
            My.Settings.AutoLoader = True
        Else
            My.Settings.AutoLoader = False
        End If
    End Sub

    Private Sub SaveSettingsOnShrinkToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveSettingsOnShrinkToolStripMenuItem.Click
        If SaveSettingsOnShrinkToolStripMenuItem.Checked = True Then
            My.Settings.SaveSettingsOnShrink = True
        Else
            My.Settings.SaveSettingsOnShrink = False
        End If
    End Sub
    Public Sub ShowOverTheTopShrinkOptionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowAdvancedShrinkOptionsToolStripMenuItem.Click
        If ShowAdvancedShrinkOptionsToolStripMenuItem.Checked = True Then
            Dim msg1 = "Shrinking these may cause boot-up errors. Shrink At Your Own Risk!" & vbCrLf & "Are you sure you would like to Show the Advanced Options?"
            Dim title1 = "Show Advanced Options?"
            Dim style1 = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or _
                        MsgBoxStyle.Critical
            ' Display the message box and save the response, Yes or No.
            Dim response1 = MsgBox(msg1, style1, title1)
            If response1 = MsgBoxResult.Yes Then
                My.Settings.ShowOverTheTop = True
                Advanced.Show()
            Else
                My.Settings.ShowOverTheTop = False
                Advanced.Hide()
            End If
        Else
            My.Settings.ShowOverTheTop = False
            Advanced.Hide()
        End If
    End Sub
    Public Sub ChangeLogToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeLogToolStripMenuItem.Click
        ChangesLog.ShowDialog()
    End Sub
    Public Sub AutoTweetToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoTweetToolStripMenuItem1.Click
        If AutoTweetToolStripMenuItem1.Checked = True Then
            My.Settings.AutoTweet = True
        Else
            My.Settings.AutoTweet = False
        End If
    End Sub
    Public Sub SaveApps_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveApps.Click
        If SaveAppsWorker.IsBusy Then Exit Sub
        JavaLoaderProgress.Value = 0
        JavaLoaderProgress.Maximum = 3
        SaveAppsWorker.RunWorkerAsync()
    End Sub
    Public Sub SaveAppsWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles SaveAppsWorker.DoWork
        JavaLoaderProgress.PerformStep()
        If UserPIN.Text = "Not Connected" Then
            JavaLoaderProgress.PerformStep()
            MsgBox("Please connect a device to backup third party apps.")
        Else
            UseWaitCursor = True
            JavaLoaderProgress.PerformStep()
            Dim message, title, defaultValue As String
            Dim BackupFolder As Object
            ' Set prompt.
            message = "Enter a title for your Backup folder"
            ' Set title.
            title = "Backup Folder Title"
            defaultValue = "Backup"   ' Set default value.
            ' Display message, title, and default value.
            BackupFolder = InputBox(message, title, defaultValue)
            ' If user has clicked Cancel, set myValue to defaultValue
            If BackupFolder Is "" Then
                UseWaitCursor = False
                MsgBox("No Backup folder entered. No Backup has been created.")
                ToolStripStatusLabel1.Text = "No Backup folder entered. No Backup has been created."
            Else
                ToolStripStatusLabel1.Text = "Backing up apps..."
                If My.Computer.FileSystem.DirectoryExists(BackupFolder) Then
                Else
                    My.Computer.FileSystem.CreateDirectory(BackupFolder)
                End If
                Dim myProcess As New Process()
                Dim myProcessStartInfo As New ProcessStartInfo("""" & My.Computer.FileSystem.CurrentDirectory & "\JavaLoader.exe""", " -w" & DevicePass.Text & " backupgroups")
                myProcessStartInfo.UseShellExecute = False
                myProcessStartInfo.WorkingDirectory = My.Computer.FileSystem.CurrentDirectory & "\" & BackupFolder & "\"
                myProcessStartInfo.CreateNoWindow = True
                myProcessStartInfo.RedirectStandardOutput = True
                myProcess.EnableRaisingEvents = True
                AddHandler myProcess.Exited, AddressOf Me.SaveAppsProcessExited
                myProcess.StartInfo = myProcessStartInfo
                myProcess.Start()
                My.Settings.CodList = My.Computer.FileSystem.CurrentDirectory & "\" & BackupFolder & "\JavaLoader.exe"
            End If
        End If
    End Sub
    Private Sub SaveAppsProcessExited(ByVal sender As Object, ByVal e As System.EventArgs)
        UseWaitCursor = False
        ToolStripStatusLabel1.Text = "All Apps + .jads have been saved to your Backup folder."
        JavaLoaderProgress.PerformStep()
        MsgBox("All Apps + .jads have been saved to your Backup folder.")
    End Sub
    Public Sub RestoreApps_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RestoreApps.Click
        JavaLoaderProgress.Value = 0
        JavaLoaderProgress.Maximum = 2
        JavaLoaderProgress.PerformStep()
        With FolderBrowserDialog1
            .SelectedPath = My.Computer.FileSystem.CurrentDirectory
            .ShowNewFolderButton = False
            .Description = "Select your Backup folder"
        End With
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            UseWaitCursor = True
            ToolStripStatusLabel1.Text = "Restoring Backup folder..."
            Dim myProcess As New Process()
            Dim myProcessStartInfo As New ProcessStartInfo("""" & My.Computer.FileSystem.CurrentDirectory & "\JavaLoader.exe""", " -w" & DevicePass.Text & " restoregroups")
            myProcessStartInfo.WorkingDirectory = FolderBrowserDialog1.SelectedPath
            myProcessStartInfo.UseShellExecute = False
            myProcessStartInfo.CreateNoWindow = True
            myProcessStartInfo.RedirectStandardOutput = True
            myProcess.EnableRaisingEvents = True
            AddHandler myProcess.Exited, AddressOf Me.RestoreAppsProcessExited
            myProcess.StartInfo = myProcessStartInfo
            myProcess.Start()
        Else
            ToolStripStatusLabel1.Text = "No Backup folder selected, unable to restore. Please try again."
            MsgBox("No Backup folder selected, unable to restore. Please try again.")
            UseWaitCursor = False
            JavaLoaderProgress.PerformStep()
        End If
    End Sub
    Private Sub RestoreAppsProcessExited(ByVal sender As Object, ByVal e As System.EventArgs)
        UseWaitCursor = False
        ToolStripStatusLabel1.Text = "All Apps have been successfully restored to the current device."
        JavaLoaderProgress.PerformStep()
        MsgBox("All Apps have been successfully restored to the current device.")
    End Sub
    Sub SkinsMenu()
        If My.Settings.BlackWhiteTheme = True Then
            BlackWhiteToolStripMenuItem.Checked = True
            Call Theme(Color.Black, System.Drawing.SystemColors.Control)
            BlackPinkToolStripMenuItem.Checked = False
            DefaultToolStripMenuItem.Checked = False
        Else
        End If
        If My.Settings.BlackPinkTheme = True Then
            BlackPinkToolStripMenuItem.Checked = True
            Call Theme(Color.Black, Color.DeepPink)
            DefaultToolStripMenuItem.Checked = False
            BlackWhiteToolStripMenuItem.Checked = False
        Else
        End If
        If My.Settings.DefaultTheme = True Then
            DefaultToolStripMenuItem.Checked = True
            Call Theme(System.Drawing.SystemColors.Control, Color.Black)
            BlackPinkToolStripMenuItem.Checked = False
            BlackWhiteToolStripMenuItem.Checked = False
        Else
        End If
    End Sub
    Private Sub BlackWhiteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BlackWhiteToolStripMenuItem.Click
        If BlackWhiteToolStripMenuItem.Checked = True Then
            Call Theme(Color.Black, System.Drawing.SystemColors.Control)
            My.Settings.BlackWhiteTheme = True
            My.Settings.DefaultTheme = False
            My.Settings.BlackPinkTheme = False
            BlackPinkToolStripMenuItem.Checked = False
            DefaultToolStripMenuItem.Checked = False
        ElseIf BlackWhiteToolStripMenuItem.Checked = False Then
            My.Settings.BlackWhiteTheme = False
        Else
            My.Settings.BlackWhiteTheme = False
        End If
    End Sub
    Private Sub BlackPinkToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BlackPinkToolStripMenuItem.Click
        If BlackPinkToolStripMenuItem.Checked = True Then
            Call Theme(Color.Black, Color.DeepPink)
            My.Settings.BlackPinkTheme = True
            DefaultToolStripMenuItem.Checked = False
            BlackWhiteToolStripMenuItem.Checked = False
            My.Settings.DefaultTheme = False
            My.Settings.BlackWhiteTheme = False
        ElseIf BlackPinkToolStripMenuItem.Checked = False Then
            My.Settings.BlackPinkTheme = False
        Else
            My.Settings.BlackPinkTheme = False
        End If
    End Sub
    Private Sub DefaultToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DefaultToolStripMenuItem.Click
        If DefaultToolStripMenuItem.Checked = True Then
            Call Theme(System.Drawing.SystemColors.Control, Color.Black)
            My.Settings.DefaultTheme = True
            BlackPinkToolStripMenuItem.Checked = False
            BlackWhiteToolStripMenuItem.Checked = False
            My.Settings.BlackWhiteTheme = False
            My.Settings.BlackPinkTheme = False
        ElseIf DefaultToolStripMenuItem.Checked = False Then
            My.Settings.DefaultTheme = False
        Else
            My.Settings.DefaultTheme = False
        End If
    End Sub
    Sub Theme(ByVal BackColor As System.Drawing.Color, ByVal ForeColor As System.Drawing.Color)
        'Background Colors
        Me.BackColor = BackColor
        MenuStrip1.BackColor = BackColor
        StatusStrip1.BackColor = BackColor
        SelectOSText.BackColor = BackColor
        DeviceText.BackColor = BackColor
        WWYLTD.BackColor = BackColor
        StartTab.BackColor = BackColor
        ShrinkTab.BackColor = BackColor
        CaJTab.BackColor = BackColor
        OTADownloader.BackColor = BackColor
        BaHTab.BackColor = BackColor
        ToolsTab.BackColor = BackColor
        PlayBookTab.BackColor = BackColor
        'Shrink Tab
        JavaSize.BackColor = BackColor
        JavaSizeShrunk.BackColor = BackColor
        BeforeFiles.BackColor = BackColor
        AfterFiles.BackColor = BackColor
        ShrinkTotal.BackColor = BackColor
        'OTA Downloader Tab
        OTAInfoBox.BackColor = BackColor
        OTAName.BackColor = BackColor
        OTAVersion.BackColor = BackColor
        OTAVendor.BackColor = BackColor
        OTADesc.BackColor = BackColor
        OTANameTitle.BackColor = BackColor
        OTAVersionTitle.BackColor = BackColor
        OTAVendorTitle.BackColor = BackColor
        OTADescTitle.BackColor = BackColor
        'Build-A-Hybrid Tab
        TextBox13.BackColor = BackColor
        Radio.BackColor = BackColor
        AppMgrtitle.BackColor = BackColor
        AppMgr.BackColor = BackColor
        BluetoothTitle.BackColor = BackColor
        BluetoothText.BackColor = BackColor
        TextBox20.BackColor = BackColor
        BrowserTxt.BackColor = BackColor
        TextBox6.BackColor = BackColor
        CameraText.BackColor = BackColor
        TextBox19.BackColor = BackColor
        FontText.BackColor = BackColor
        TextBox8.BackColor = BackColor
        GPSText.BackColor = BackColor
        TextBox18.BackColor = BackColor
        MediaTxt.BackColor = BackColor
        TextBox11.BackColor = BackColor
        PlazmicText.BackColor = BackColor
        KeyboardTitle.BackColor = BackColor
        KeyboardText.BackColor = BackColor
        TextBox14.BackColor = BackColor
        VideoRecorderText.BackColor = BackColor
        TextBox15.BackColor = BackColor
        ProcessLauncher.BackColor = BackColor
        TextBox16.BackColor = BackColor
        Voice.BackColor = BackColor
        OfflineMode.BackColor = BackColor
        'Phone Tools Tab
        PIN.BackColor = BackColor
        UserPIN.BackColor = BackColor
        PassBox.BackColor = BackColor

        'Fore Colors and Texts
        WWYLTD.ForeColor = ForeColor
        MenuStrip1.ForeColor = ForeColor
        SelectOSText.ForeColor = ForeColor
        StatusStrip1.ForeColor = ForeColor
        ToolStripStatusLabel1.ForeColor = ForeColor
        Version.ForeColor = ForeColor
        DeviceText.ForeColor = ForeColor
        'Shrink Tab
        AppGroupBox.ForeColor = ForeColor
        LangGroupBox.ForeColor = ForeColor
        IMGroupBox.ForeColor = ForeColor
        EngGroupBox.ForeColor = ForeColor
        GamesGroupBox.ForeColor = ForeColor
        BBBox1.ForeColor = ForeColor
        Advanced.ForeColor = ForeColor
        CustomGroupBox.ForeColor = ForeColor
        DefaultsBox.ForeColor = ForeColor
        DefFontsBox.ForeColor = ForeColor
        SelectBox.ForeColor = ForeColor
        SettingsBox.ForeColor = ForeColor
        ShrinkInfoBox.ForeColor = ForeColor
        JavaSize.ForeColor = ForeColor
        JavaSizeShrunk.ForeColor = ForeColor
        BeforeFiles.ForeColor = ForeColor
        AfterFiles.ForeColor = ForeColor
        ShrinkTotal.ForeColor = ForeColor
        'CAJ Tab
        CODFilesGBox.ForeColor = ForeColor
        FileCount.ForeColor = ForeColor
        AppName.ForeColor = ForeColor
        DescText.ForeColor = ForeColor
        CaJVersion.ForeColor = ForeColor
        Vendor.ForeColor = ForeColor
        FileName.ForeColor = ForeColor
        CaJSaveTo.ForeColor = ForeColor
        OptionsGroupBox.ForeColor = ForeColor
        AfterCAJGroupBox.ForeColor = ForeColor
        ClearGroupBox.ForeColor = ForeColor
        Clear.ForeColor = BackColor
        ClearAll.ForeColor = BackColor
        'OTA Downloader Tab
        URL2OTAGroupBox.ForeColor = ForeColor
        SaveToGroupBox.ForeColor = ForeColor
        OTAInfoBox.ForeColor = ForeColor
        Convert2Alx.ForeColor = ForeColor
        OTANameTitle.ForeColor = ForeColor
        OTAVersionTitle.ForeColor = ForeColor
        OTAVendorTitle.ForeColor = ForeColor
        OTADescTitle.ForeColor = ForeColor
        OTAName.ForeColor = ForeColor
        OTAVersion.ForeColor = ForeColor
        OTAVendor.ForeColor = ForeColor
        OTADesc.ForeColor = ForeColor
        'Build-A-Hybrid Tab
        AppMgrBox.ForeColor = ForeColor
        BTBox.ForeColor = ForeColor
        BrowserBox.ForeColor = ForeColor
        CamBox.ForeColor = ForeColor
        FontsBox.ForeColor = ForeColor
        GPSBox.ForeColor = ForeColor
        MediaBox.ForeColor = ForeColor
        PlazmicBox.ForeColor = ForeColor
        PLBox.ForeColor = ForeColor
        VidBox.ForeColor = ForeColor
        KeyboardBox.ForeColor = ForeColor
        BaHBreakdown.ForeColor = ForeColor
        TextBox13.ForeColor = ForeColor
        Radio.ForeColor = ForeColor
        AppMgrtitle.ForeColor = ForeColor
        AppMgr.ForeColor = ForeColor
        BluetoothTitle.ForeColor = ForeColor
        BluetoothText.ForeColor = ForeColor
        TextBox20.ForeColor = ForeColor
        BrowserTxt.ForeColor = ForeColor
        TextBox6.ForeColor = ForeColor
        CameraText.ForeColor = ForeColor
        TextBox19.ForeColor = ForeColor
        FontText.ForeColor = ForeColor
        TextBox8.ForeColor = ForeColor
        GPSText.ForeColor = ForeColor
        TextBox18.ForeColor = ForeColor
        MediaTxt.ForeColor = ForeColor
        TextBox11.ForeColor = ForeColor
        PlazmicText.ForeColor = ForeColor
        KeyboardTitle.ForeColor = ForeColor
        KeyboardText.ForeColor = ForeColor
        TextBox14.ForeColor = ForeColor
        VideoRecorderText.ForeColor = ForeColor
        TextBox15.ForeColor = ForeColor
        ProcessLauncher.ForeColor = ForeColor
        TextBox16.ForeColor = ForeColor
        Voice.ForeColor = ForeColor
        OfflineMode.ForeColor = ForeColor
        'Install-A-Hybrid
        'Phone Tools Tab
        InstallCODonDrag.ForeColor = ForeColor
        PhoneInfoBox.ForeColor = ForeColor
        PIN.ForeColor = ForeColor
        UserPIN.ForeColor = ForeColor
        PassBox.ForeColor = ForeColor
        FileSystemBox.ForeColor = ForeColor
        CODFilesBox.ForeColor = ForeColor
        ThirdPartyAppsBox.ForeColor = ForeColor
        EventLogBox.ForeColor = ForeColor
        OtherBox.ForeColor = ForeColor
        ReadCount.ForeColor = ForeColor
        ConnectPhone.ForeColor = BackColor
        ReadFiles.ForeColor = BackColor
        SaveRead.ForeColor = BackColor
        InstallCOD.ForeColor = BackColor
        SaveCOD.ForeColor = BackColor
        RemoveCOD.ForeColor = BackColor
        SaveApps.ForeColor = BackColor
        RestoreApps.ForeColor = BackColor
        SaveAllCods.ForeColor = BackColor
        ReadEventLog.ForeColor = BackColor
        SaveEventLog.ForeColor = BackColor
        ClearEventLog.ForeColor = BackColor
        RecoveryMem.ForeColor = BackColor
        Screenshot.ForeColor = BackColor
        SyncTime.ForeColor = BackColor
        'IaH Tab
        'PlayBook Tab
        GroupBox6.ForeColor = ForeColor
        GroupBox7.ForeColor = ForeColor
        AutoAddBAR.ForeColor = ForeColor
        InstallFileOnDrag.ForeColor = ForeColor
        LaunchBarAfterInstall.ForeColor = ForeColor
        DisplayEventLog.ForeColor = ForeColor
        HideSystemApps.ForeColor = ForeColor
        FilesGroupBox.ForeColor = ForeColor
        If BackColor = System.Drawing.SystemColors.Control Then
            StartShrink.ForeColor = ForeColor
            StartCAJ.ForeColor = ForeColor
            StartOTA.ForeColor = ForeColor
            StartBaH.ForeColor = ForeColor
            StartTools.ForeColor = ForeColor
            StartPlayBook.ForeColor = ForeColor
            CustomShrinkOK.ForeColor = ForeColor
            Clear.ForeColor = ForeColor
            ClearAll.ForeColor = ForeColor
            ConnectPhone.ForeColor = ForeColor
            ReadFiles.ForeColor = ForeColor
            SaveRead.ForeColor = ForeColor
            InstallCOD.ForeColor = ForeColor
            SaveCOD.ForeColor = ForeColor
            RemoveCOD.ForeColor = ForeColor
            SaveApps.ForeColor = ForeColor
            RestoreApps.ForeColor = ForeColor
            SaveAllCods.ForeColor = ForeColor
            ReadEventLog.ForeColor = ForeColor
            SaveEventLog.ForeColor = ForeColor
            ClearEventLog.ForeColor = ForeColor
            RecoveryMem.ForeColor = ForeColor
            Screenshot.ForeColor = ForeColor
            SyncTime.ForeColor = ForeColor
            Swap.ForeColor = ForeColor
            SelectAll.ForeColor = ForeColor
            SelectNone.ForeColor = ForeColor
            LoadShrink.ForeColor = ForeColor
            SaveShrink.ForeColor = ForeColor
            CaJBrowse.ForeColor = ForeColor
            DragCOD.ForeColor = ForeColor
            DownloadOTA.ForeColor = ForeColor
            OTABrowse.ForeColor = ForeColor
            ClearBars.ForeColor = ForeColor
            AddBar.ForeColor = ForeColor
            DragDropBar.ForeColor = ForeColor
            PlayBookWarning.BackColor = BackColor
            PlayBookWarning.ForeColor = ForeColor
        Else
            StartShrink.ForeColor = BackColor
            StartCAJ.ForeColor = BackColor
            StartOTA.ForeColor = BackColor
            StartBaH.ForeColor = BackColor
            StartTools.ForeColor = BackColor
            StartPlayBook.ForeColor = BackColor
            CustomShrinkOK.ForeColor = BackColor
            ClearAll.ForeColor = BackColor
            ConnectPhone.ForeColor = BackColor
            ReadFiles.ForeColor = BackColor
            SaveRead.ForeColor = BackColor
            InstallCOD.ForeColor = BackColor
            SaveCOD.ForeColor = BackColor
            RemoveCOD.ForeColor = BackColor
            SaveApps.ForeColor = BackColor
            RestoreApps.ForeColor = BackColor
            SaveAllCods.ForeColor = BackColor
            ReadEventLog.ForeColor = BackColor
            SaveEventLog.ForeColor = BackColor
            ClearEventLog.ForeColor = BackColor
            RecoveryMem.ForeColor = BackColor
            Screenshot.ForeColor = BackColor
            SyncTime.ForeColor = BackColor
            Swap.ForeColor = BackColor
            SelectAll.ForeColor = BackColor
            SelectNone.ForeColor = BackColor
            LoadShrink.ForeColor = BackColor
            SaveShrink.ForeColor = BackColor
            CaJBrowse.ForeColor = BackColor
            DragCOD.ForeColor = BackColor
            DownloadOTA.ForeColor = BackColor
            OTABrowse.ForeColor = BackColor
            ClearBars.ForeColor = BackColor
            AddBar.ForeColor = BackColor
            DragDropBar.ForeColor = BackColor
            PlayBookWarning.BackColor = BackColor
            PlayBookWarning.ForeColor = ForeColor
        End If
    End Sub
    Private Sub LiveShrink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LiveShrink.Click

        Dim msg1 = "This is a BETA feature. Are you sure you would like to Shrink the OS on the current device? Please make sure to create a back-up, just in case!"
        Dim title1 = "Shrink Your OS Live? [BETA]"
        Dim style1 = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or _
                    MsgBoxStyle.Critical
        ' Display the message box and save the response, Yes or No.
        Dim response1 = MsgBox(msg1, style1, title1)
        If response1 = MsgBoxResult.Yes Then
            Dim message, title, defaultValue As String
            Dim Password As Object
            ' Set prompt.
            message = "Enter the password for the connected device, if there is one. Otherwise leave it blank"
            ' Set title.
            title = "Enter Device Password"
            defaultValue = ""   ' Set default value.
            ' Display message, title, and default value.
            Password = InputBox(message, title, defaultValue)
            ' If user has clicked Cancel, set myValue to defaultValue
            If Password Is "" Then
                DevicePass.Text = ""
            Else
                DevicePass.Text = Password
            End If
            If LiveShrinkWorker.IsBusy Then Exit Sub
            LiveShrinkWorker.RunWorkerAsync()
        Else
            ToolStripStatusLabel1.Text = "You have selected not to Shrink Your OS Live."
            MsgBox("You have selected not to Shrink Your OS Live.")
        End If
    End Sub
    Private Sub LiveShrinkWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles LiveShrinkWorker.DoWork
        Call ReadDeviceFiles()
        My.Settings.CodList = ""
        ToolStripStatusLabel1.Text = "Shrinking your OS Live..."
        If OTTCamera.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_camera", "LiveShrink")
        End If
        If OTTBluetooth.CheckState = 1 Then
            Call MovingFiles("bluetooth", "LiveShrink")
            Call MovingFiles("net_rim_bb_BT", "LiveShrink")
        End If
        If OTTVideoCamera.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_videorecorder", "LiveShrink")
        End If
        If DefaultDMTree.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_default102DMtree.cod", "LiveShrink")
        End If
        If eScreen.CheckState = 1 Then
            Call MovingFiles("net_rim_escreen_app.cod", "LiveShrink")
        End If
        If AccessOptions.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_medialoader_accessibility", "LiveShrink")
        End If
        If ARM.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_application_monitor", "LiveShrink")
        End If
        If AppWorld.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_appworld", "LiveShrink")
        End If
        If DeviceAnalyzer.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_device_analyzer*.cod", "LiveShrink")
        End If
        If News.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_news", "LiveShrink")
        End If
        If Plans.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_plans*", "LiveShrink")
            'Call MovingFiles("net_rim_bb_api_catalyst_serviceplanengine.cod", "LiveShrink")
        End If
        If BBProtect.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_dryad", "LiveShrink")
            Call MovingFiles("net_rim_bbcommons_dryad", "LiveShrink")
        End If
        If BBRadio.CheckState = 1 Then
            Call MovingFiles("CorusAdapter", "LiveShrink")
            Call MovingFiles("ClearChannelAdapter", "LiveShrink")
            Call MovingFiles("IRadioStreamProvider", "LiveShrink")
            Call MovingFiles("PlayerLibrary", "LiveShrink")
            Call MovingFiles("net_rim_bb_radio_dial", "LiveShrink")
            Call MovingFiles("SlackerAdapter", "LiveShrink")
        End If
        If ATTHotSpot.CheckState = 1 Then
            Call MovingFiles("net_rim_hotspotclient", "LiveShrink")
        End If
        If DoDCerts.CheckState = 1 Then
            Call MovingFiles("net_rim_DoDRootCerts", "LiveShrink")
        End If
        If ITPolicyViewer.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_itpolicyviewer", "LiveShrink")
        End If
        If MemoPad.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_memo_app", "LiveShrink")
        End If
        If Slacker.CheckState = 1 Then
            Call MovingFiles("SlackerRadio", "LiveShrink")
            Call MovingFiles("slacker_radio*", "LiveShrink")
        End If
        If Tasks.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_task_app", "LiveShrink")
        End If
        If SuretypeT12.CheckState = 1 Then
            Call MovingFiles("net_rim_tid_suretypeT12", "LiveShrink")
        End If
        If VZWNameID.CheckState = 1 Then
            Call MovingFiles("CequintAPI.cod", "LiveShrink")
            Call MovingFiles("cequint_vzw_bold9930", "LiveShrink")
            Call MovingFiles("cequint_vzw_torch9850", "LiveShrink")
            Call MovingFiles("net_rim_bb_phone_cequint_proxy.cod", "LiveShrink")
            Call MovingFiles("BBCequint.cod", "LiveShrink")
        End If
        If VZWPTT.CheckState = 1 Then
            Call MovingFiles("MPTT.cod", "LiveShrink")
            Call MovingFiles("net_rim_bb_phone_vz_ptt", "LiveShrink")
            Call MovingFiles("net_rim_bb_medialoader_ringtones_verizon_alert_tones.cod", "LiveShrink")
        End If
        If TmoError.CheckState = 1 Then
            Call MovingFiles("net_rim_errortranslator_tmobile", "LiveShrink")
        End If
        If MobileBackup.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_nabsync", "LiveShrink")
        End If
        If TMONameID.CheckState = 1 Then
            Call MovingFiles("cequint_tmo", "LiveShrink")
            Call MovingFiles("net_rim_bb_phone_cequint_proxy", "LiveShrink")
            Call MovingFiles("BBCequint", "LiveShrink")
            Call MovingFiles("CequintAPI", "LiveShrink")
        End If
        If TeleNav.CheckState = 1 Then
            Call MovingFiles("TeleNav", "LiveShrink")
        End If
        If LunarCalendar.CheckState = 1 Then
            Call MovingFiles("LunarCalendar", "LiveShrink")
        End If
        If MobileMarket.CheckState = 1 Then
            Call MovingFiles("MobileMarket", "LiveShrink")
        End If
        If MobileReader.CheckState = 1 Then
            Call MovingFiles("CMRead_v60", "LiveShrink")
        End If
        If MusicCN.CheckState = 1 Then
            Call MovingFiles("Music", "LiveShrink")
        End If
        If MyFaves.CheckState = 1 Then
            Call MovingFiles("net_rim_tmo_five.cod", "LiveShrink")
            Call MovingFiles("five_icon_library.cod", "LiveShrink")
        End If
        If Stock.CheckState = 1 Then
            Call MovingFiles("MobileStock", "LiveShrink")
        End If
        If Fetion.CheckState = 1 Then
            Call MovingFiles("Fetion", "LiveShrink")
        End If
        If Podcasts.CheckState = 1 Then
            Call MovingFiles("podcasts", "LiveShrink")
        End If
        If Music.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_medialoader_music", "LiveShrink")
        End If
        If Videos.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_medialoader_video", "LiveShrink")
            Call MovingFiles("net_rim_bb_mediaOTA_toolkit_", "LiveShrink")
            Call MovingFiles("net_rim_bb_medialoader_toolkit_", "LiveShrink")
        End If
        If VVM.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_vvm", "LiveShrink")
            Call MovingFiles("net_rim_bb_mbp", "LiveShrink")
        End If
        If YouTube.CheckState = 1 Then
            Call MovingFiles("Wikitude", "LiveShrink")
        End If
        If YouTube.CheckState = 1 Then
            Call MovingFiles("youtube", "LiveShrink")
        End If
        If Movitalk.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_phone_ptt_app.cod", "LiveShrink")
            Call MovingFiles("KnJCDE.cod", "LiveShrink")
        End If
        If Romanian.CheckState = 1 Then
            Call MovingFiles("_ro.cod", "LiveShrink")
            Call MovingFiles("romanian", "LiveShrink")
        End If
        If Norwegian.CheckState = 1 Then
            Call MovingFiles("_no.cod", "LiveShrink")
            Call MovingFiles("norwegian", "LiveShrink")
        End If
        If Swedish.CheckState = 1 Then
            Call MovingFiles("_sv.cod", "LiveShrink")
            Call MovingFiles("swedish", "LiveShrink")
        End If
        If Polish.CheckState = 1 Then
            Call MovingFiles("_pl.cod", "LiveShrink")
            Call MovingFiles("polish", "LiveShrink")
        End If
        If Thai.CheckState = 1 Then
            Call MovingFiles("_th.cod", "LiveShrink")
            Call MovingFiles("thai", "LiveShrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_TIS620.cod", "LiveShrink")
        End If
        If Korean.CheckState = 1 Then
            Call MovingFiles("_ko.cod", "LiveShrink")
            Call MovingFiles("korean", "LiveShrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_EUC_KR.cod", "LiveShrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_x_Johab.cod", "LiveShrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_KSC5601.cod", "LiveShrink")
        End If
        If German.CheckState = 1 Then
            Call MovingFiles("_de.cod", "LiveShrink")
            Call MovingFiles("german", "LiveShrink")
        End If
        If Greek.CheckState = 1 Then
            Call MovingFiles("_el.cod", "LiveShrink")
            Call MovingFiles("greek", "LiveShrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_CP1253.cod", "LiveShrink")
        End If
        If Hebrew.CheckState = 1 Then
            Call MovingFiles("_he.cod", "LiveShrink")
            Call MovingFiles("hebrew", "LiveShrink")
            Call MovingFiles("_he_", "LiveShrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_CP1255.cod", "LiveShrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_CP1256.cod", "LiveShrink")
        End If
        If Hindi.CheckState = 1 Then
            Call MovingFiles("_hi.cod", "LiveShrink")
            Call MovingFiles("net_rim_tid_indic.cod", "LiveShrink")
        End If
        If Chinese.CheckState = 1 Then
            Call MovingFiles("net_rim_tid_dynamic_ling_data_chinese", "LiveShrink")
            Call MovingFiles("net_rim_tid_chinese", "LiveShrink")
            Call MovingFiles("pinyin", "LiveShrink")
            Call MovingFiles("_zh.cod", "LiveShrink")
            Call MovingFiles("_zh_CN", "LiveShrink")
            Call MovingFiles("_zh_TW", "LiveShrink")
            Call MovingFiles("_zh_HK", "LiveShrink")
            Call MovingFiles("net_rim_tid_dynamic_ling_data_HK", "LiveShrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_Big5_HKSCS.cod", "LiveShrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_EUC_CN.cod", "LiveShrink")
        End If
        If Croatian.CheckState = 1 Then
            Call MovingFiles("_hr.cod", "LiveShrink")
            Call MovingFiles("croatian", "LiveShrink")
        End If
        If Czech.CheckState = 1 Then
            Call MovingFiles("_cs.cod", "LiveShrink")
            Call MovingFiles("czech", "LiveShrink")
        End If
        If Hungarian.CheckState = 1 Then
            Call MovingFiles("_hu.cod", "LiveShrink")
            Call MovingFiles("hungarian", "LiveShrink")
        End If
        If Indonesian.CheckState = 1 Then
            Call MovingFiles("_id", "LiveShrink")
            Call MovingFiles("indonesian", "LiveShrink")
        End If
        If Vietnamese.CheckState = 1 Then
            Call MovingFiles("_vi.cod", "LiveShrink")
            Call MovingFiles("vietnamese", "LiveShrink")
            Call MovingFiles("net_rim_platform_resource__vi__MultiTap", "LiveShrink")
        End If
        If Turkish.CheckState = 1 Then
            Call MovingFiles("_tr.cod", "LiveShrink")
            Call MovingFiles("turkish", "LiveShrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_CP1254.cod", "LiveShrink")
        End If
        If UKEnglish.CheckState = 1 Then
            Call MovingFiles("_gb", "LiveShrink")
        End If
        If Financial.CheckState = 1 Then
            Call MovingFiles("net_rim_tid_dynamic_ling_data_financial", "LiveShrink")
        End If
        If Medical.CheckState = 1 Then
            Call MovingFiles("net_rim_tid_dynamic_ling_data_medical", "LiveShrink")
        End If
        If Legal.CheckState = 1 Then
            Call MovingFiles("net_rim_tid_dynamic_ling_data_legal", "LiveShrink")
        End If
        If Afrikaans.CheckState = 1 Then
            Call MovingFiles("_af.cod", "LiveShrink")
            Call MovingFiles("afrikaans", "LiveShrink")
        End If
        If Arabic.CheckState = 1 Then
            Call MovingFiles("_ar.cod", "LiveShrink")
            Call MovingFiles("arabic", "LiveShrink")
        End If
        If Basque.CheckState = 1 Then
            Call MovingFiles("_eu.cod", "LiveShrink")
            Call MovingFiles("basque", "LiveShrink")
        End If
        If Catalan.CheckState = 1 Then
            Call MovingFiles("_ca.cod", "LiveShrink")
            Call MovingFiles("catalan", "LiveShrink")
        End If
        If Finnish.CheckState = 1 Then
            Call MovingFiles("*finnish*.cod", "LiveShrink")
            Call MovingFiles("*_fi.cod", "LiveShrink")
        End If
        If French.CheckState = 1 Then
            Call MovingFiles("_fr.cod", "LiveShrink")
            Call MovingFiles("french", "LiveShrink")
        End If
        If Spanish.CheckState = 1 Then
            Call MovingFiles("_es.cod", "LiveShrink")
            Call MovingFiles("_es_MX.cod", "LiveShrink")
            Call MovingFiles("spanish", "LiveShrink")
        End If
        If Portuguese.CheckState = 1 Then
            Call MovingFiles("_pt.cod", "LiveShrink")
            Call MovingFiles("portuguese", "LiveShrink")
            Call MovingFiles("_pl_br.cod", "LiveShrink")
        End If
        If Dutch.CheckState = 1 Then
            Call MovingFiles("_du.cod", "LiveShrink")
            Call MovingFiles("dutch", "LiveShrink")
            Call MovingFiles("_nl.cod", "LiveShrink")
        End If
        If Danish.CheckState = 1 Then
            Call MovingFiles("_dal.cod", "LiveShrink")
            Call MovingFiles("danish", "LiveShrink")
        End If
        If Galician.CheckState = 1 Then
            Call MovingFiles("_gl.cod", "LiveShrink")
            Call MovingFiles("galician", "LiveShrink")
        End If
        If Italian.CheckState = 1 Then
            Call MovingFiles("_it.cod", "LiveShrink")
            Call MovingFiles("italian", "LiveShrink")
        End If
        If Japanese.CheckState = 1 Then
            Call MovingFiles("_ja.cod", "LiveShrink")
            Call MovingFiles("japanese", "LiveShrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_Shift_JIS.cod", "LiveShrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_EUC_JP.cod", "LiveShrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_ISO_2022_JP.cod", "LiveShrink")
        End If
        If Russian.CheckState = 1 Then
            Call MovingFiles("_ru.cod", "LiveShrink")
            Call MovingFiles("russian", "LiveShrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_CP1251.cod", "LiveShrink")
            Call MovingFiles("net_rim_tid_dynamic_transcoding_data_KOI8_R.cod", "LiveShrink")
        End If
        If SMime.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_smime", "LiveShrink")
        End If
        If BBM.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_qm_bbm_internal_api.cod", "LiveShrink")
            Call MovingFiles("net_rim_bb_qm_platform.cod", "LiveShrink")
            Call MovingFiles("net_rim_bb_qm_peer", "LiveShrink")
            Call MovingFiles("net_rim_bb_qm_lib_barcode.cod", "LiveShrink")
            Call MovingFiles("net_rim_bb_qm_api.cod", "LiveShrink")
            Call MovingFiles("net_rim_bbgroup", "LiveShrink")
            Call MovingFiles("net_rim_bb_qm_peer_resource_en.cod", "LiveShrink")
        End If
        If Maps.CheckState = 1 Then
            If OSFolderBox.SelectedItem.ToString.Contains("v5.0.0") Then
                Call MovingFiles("net_rim_bb_lbs.cod", "LiveShrink")
                Call MovingFiles("net_rim_bb_lbs_api.cod", "LiveShrink")
                Call MovingFiles("net_rim_bb_lbs_lib.cod", "LiveShrink")
                Call MovingFiles("net_rim_bb_lbs_resource*.cod", "LiveShrink")
                Call MovingFiles("net_rim_bb_gas.cod", "LiveShrink")
            ElseIf OSFolderBox.SelectedItem.ToString.Contains("v6.0.0") Then
                Call MovingFiles("net_rim_bb_maps.cod", "LiveShrink")
                Call MovingFiles("net_rim_bb_maps_resource*.cod", "LiveShrink")
                Call MovingFiles("net_rim_bb_maps_help*.cod", "LiveShrink")
            ElseIf OSFolderBox.SelectedItem.ToString.Contains("v7.0.0") Then
                Call MovingFiles("net_rim_bb_maps.cod", "LiveShrink")
                Call MovingFiles("net_rim_bb_maps_resource*.cod", "LiveShrink")
                Call MovingFiles("net_rim_bb_maps_help*.cod", "LiveShrink")
            ElseIf OSFolderBox.SelectedItem.ToString.Contains("v7.1.0") Then
                Call MovingFiles("net_rim_bb_maps.cod", "LiveShrink")
                Call MovingFiles("net_rim_bb_maps_resource*.cod", "LiveShrink")
                Call MovingFiles("net_rim_bb_maps_help*.cod", "LiveShrink")
                Call MovingFiles("net_rim_bb_mapsCatalystLib.cod", "LiveShrink")
                Call MovingFiles("net_rim_bb_mapsLib.cod", "LiveShrink")
            End If
        End If
        If Maps.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_conf_api", "LiveShrink")
        End If
        If AIM.CheckState = 1 Then
            Call MovingFiles("qm_aim", "LiveShrink")
        End If
        If Feeds.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_feeds", "LiveShrink")
        End If
        If Gmail.CheckState = 1 Then
            Call MovingFiles("gmail", "LiveShrink")
        End If
        If Google.CheckState = 1 Then
            Call MovingFiles("google", "LiveShrink")
        End If
        If ICQ.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_qm_icq", "LiveShrink")
        End If
        If MSN.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_qm_msn", "LiveShrink")
        End If
        If MySpace.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_myspace", "LiveShrink")
        End If
        If Twitter.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_twitter", "LiveShrink")
        End If
        If Yahoo.CheckState = 1 Then
            Call MovingFiles("yahoo", "LiveShrink")
        End If
        If Facebook.CheckState = 1 Then
            Call MovingFiles("facebook", "LiveShrink")
        End If
        If Flickr.CheckState = 1 Then
            Call MovingFiles("flickr", "LiveShrink")
        End If
        If RemoveDocsToGo.CheckState = 1 Then
            Call MovingFiles("DocsToGo", "LiveShrink")
            Call MovingFiles("PDFToGo", "LiveShrink")
            Call MovingFiles("SheetToGo", "LiveShrink")
            Call MovingFiles("SlideshowToGo", "LiveShrink")
            Call MovingFiles("WordToGo", "LiveShrink")
        End If
        If PDFToGo.CheckState = 1 Then
            Call MovingFiles("PDFToGo", "LiveShrink")
        End If
        If SheetToGo.CheckState = 1 Then
            Call MovingFiles("SheetToGo", "LiveShrink")
        End If
        If SlideshowsToGo.CheckState = 1 Then
            Call MovingFiles("SlideshowToGo", "LiveShrink")
        End If
        If WordToGo.CheckState = 1 Then
            Call MovingFiles("WordToGo", "LiveShrink")
        End If
        If Arial.CheckState = 1 Then
            Call MovingFiles("net_rim_font_arial", "LiveShrink")
        End If
        If ArabicFont.CheckState = 1 Then
            Call MovingFiles("net_rim_font_arabic", "LiveShrink")
        End If
        If Courier.CheckState = 1 Then
            Call MovingFiles("net_rim_font_courier", "LiveShrink")
        End If
        If Emoji.CheckState = 1 Then
            Call MovingFiles("net_rim_font_emoji", "LiveShrink")
        End If
        If European.CheckState = 1 Then
            Call MovingFiles("net_rim_font_european", "LiveShrink")
        End If
        If Georgia.CheckState = 1 Then
            Call MovingFiles("net_rim_font_georgia", "LiveShrink")
        End If
        If GlobalFont.CheckState = 1 Then
            Call MovingFiles("net_rim_font_global", "LiveShrink")
        End If
        If Indic.CheckState = 1 Then
            Call MovingFiles("net_rim_font_indic", "LiveShrink")
        End If
        If JapaneseFont.CheckState = 1 Then
            Call MovingFiles("net_rim_font_japanese", "LiveShrink")
        End If
        If Latin.CheckState = 1 Then
            'Call MovingFiles("net_rim_font_latin", "LiveShrink")
        End If
        If Misc.CheckState = 1 Then
            Call MovingFiles("net_rim_font_misc", "LiveShrink")
        End If
        If Misc.CheckState = 1 Then
            Call MovingFiles("net_rim_font_monotype", "LiveShrink")
        End If
        If Tahoma.CheckState = 1 Then
            Call MovingFiles("net_rim_font_tahoma", "LiveShrink")
        End If
        If ThaiFont.CheckState = 1 Then
            Call MovingFiles("net_rim_font_thai", "LiveShrink")
        End If
        If Times.CheckState = 1 Then
            Call MovingFiles("net_rim_font_times", "LiveShrink")
        End If
        If Trebuchet.CheckState = 1 Then
            Call MovingFiles("net_rim_font_trebuchet", "LiveShrink")
        End If
        If Verdana.CheckState = 1 Then
            Call MovingFiles("net_rim_font_verdana", "LiveShrink")
        End If
        If Themes.CheckState = 1 Then
            Call MovingFiles("net_rim_theme_china", "LiveShrink")
            Call MovingFiles("net_rim_theme_bell", "LiveShrink")
            Call MovingFiles("net_rim_theme_telekom", "LiveShrink")
            Call MovingFiles("net_rim_theme_vodafone", "LiveShrink")
            Call MovingFiles("net_rim_theme_100_320x240_b.cod", "LiveShrink")
            Call MovingFiles("net_rim_theme_102a_icon_320x240_b.cod", "LiveShrink")
            Call MovingFiles("net_rim_theme_104_zen_320x240_b.cod", "LiveShrink")
            Call MovingFiles("net_rim_theme_105_zen_320x240_b.cod", "LiveShrink")
            Call MovingFiles("net_rim_theme_107_zen_320x240_b.cod", "LiveShrink")
            Call MovingFiles("net_rim_theme_109_zen_320x240_b.cod", "LiveShrink")
            Call MovingFiles("net_rim_theme_114_320x240_b.cod", "LiveShrink")
            Call MovingFiles("net_rim_theme_115_320x240_b.cod", "LiveShrink")
            Call MovingFiles("net_rim_theme_119_icon_320x240_b.cod", "LiveShrink")
            Call MovingFiles("net_rim_theme_120_green_320x240_b.cod", "LiveShrink")
            Call MovingFiles("net_rim_theme_120_today_320x240_b.cod", "LiveShrink")
            Call MovingFiles("net_rim_theme_125_today_320x240_b.cod", "LiveShrink")
            Call MovingFiles("net_rim_theme_125_zen_320x240_b.cod", "LiveShrink")
            Call MovingFiles("net_rim_theme_129_320x240_b.cod", "LiveShrink")
            Call MovingFiles("net_rim_theme_att", "LiveShrink")
            Call MovingFiles("net_rim_theme_orange", "LiveShrink")
            Call MovingFiles("net_rim_theme_rogers", "LiveShrink")
            Call MovingFiles("net_rim_theme_telefonica", "LiveShrink")
            Call MovingFiles("net_rim_theme_tim", "LiveShrink")
            Call MovingFiles("net_rim_theme_tmobile", "LiveShrink")
            Call MovingFiles("net_rim_theme_wind", "LiveShrink")
        End If
        If CityID.CheckState = 1 Then
            Call MovingFiles("CityID", "LiveShrink")
        End If
        If ChineseDictionary.CheckState = 1 Then
            Call MovingFiles("ChineseDictionary", "LiveShrink")
        End If
        If BlockBreaker.CheckState = 1 Then
            Call MovingFiles("BBD2", "LiveShrink")
        End If
        If Bejeweled.CheckState = 1 Then
            Call MovingFiles("DemoBTwist.cod", "LiveShrink")
            Call MovingFiles("BejeweledTwist", "LiveShrink")
        End If
        If Monopoly.CheckState = 1 Then
            Call MovingFiles("Monopoly", "LiveShrink")
        End If
        If Peggle.CheckState = 1 Then
            Call MovingFiles("Peggle", "LiveShrink")
        End If
        If TheSims.CheckState = 1 Then
            Call MovingFiles("TheSims3", "LiveShrink")
        End If
        If Tetris.CheckState = 1 Then
            Call MovingFiles("Tetris", "LiveShrink")
        End If
        If Brickbreaker.CheckState = 1 Then
            Call MovingFiles("brickbreaker", "LiveShrink")
        End If
        If WordMole.CheckState = 1 Then
            Call MovingFiles("wordmole", "LiveShrink")
        End If
        If Sudoku.CheckState = 1 Then
            Call MovingFiles("Sudoku", "LiveShrink")
        End If
        If Texas.CheckState = 1 Then
            Call MovingFiles("TexasPoker", "LiveShrink")
            Call MovingFiles("THK2", "LiveShrink")
        End If
        If Klondike.CheckState = 1 Then
            Call MovingFiles("Klondike", "LiveShrink")
        End If
        If VcastMusic.CheckState = 1 Then
            Call MovingFiles("VCastMusic", "LiveShrink")
        End If
        If Amazon.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_storefront", "LiveShrink")
        End If
        If AppCenter.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_app_center.cod", "LiveShrink")
        End If
        If Backgrounds.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_medialoader_backgrounds", "LiveShrink")
        End If
        If HelpFiles.CheckState = 1 Then
            Call MovingFiles("help", "LiveShrink")
        End If
        If ShrinkEventLog.CheckState = 1 Then
            Call MovingFiles("net_rim_event_log_viewer_app.cod", "LiveShrink")
        End If
        If G3eWalk.CheckState = 1 Then
            Call MovingFiles("G3eWalk", "LiveShrink")
        End If
        If OTAUpgrade.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_otaupgrade.cod", "LiveShrink")
        End If
        If PassApps.CheckState = 1 Then
            Call MovingFiles("password", "LiveShrink")
        End If
        If PassApps.CheckState = 1 Then
            Call MovingFiles("PIMClientBB9", "LiveShrink")
        End If
        If SetupWiz.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_setupwizard_app.cod", "LiveShrink")
            Call MovingFiles("net_rim_bb_setupwizard_images_", "LiveShrink")
        End If
        If TTY.CheckState = 1 Then
            Call MovingFiles("net_rim_phone_tty_enabler.cod", "LiveShrink")
        End If
        If Vodafone.CheckState = 1 Then
            Call MovingFiles("VodafoneMusic", "LiveShrink")
        End If
        If VAD.CheckState = 1 Then
            Call MovingFiles("net_rim_vad", "LiveShrink")
        End If
        If VNR.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_voicenotesrecorder.cod", "LiveShrink")
        End If
        If VZNav.CheckState = 1 Then
            Call MovingFiles("VZNavigator.cod", "LiveShrink")
            Call MovingFiles("SMSWakeup.cod", "LiveShrink")
        End If
        If Vcast.CheckState = 1 Then
            Call MovingFiles("MOD.cod", "LiveShrink")
        End If
        If Backup.CheckState = 1 Then
            Call MovingFiles("BackupAssistant", "LiveShrink")
        End If
        If BBMMusic.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_bbcm", "LiveShrink")
        End If
        If Ringtones.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_mediaOTA_ringtones", "LiveShrink")
            Call MovingFiles("net_rim_bb_medialoader_ringtones", "LiveShrink")
            Call MovingFiles("net_rim_bb_medialoader_music", "LiveShrink")
        End If
        If Backgrounds.CheckState = 1 And Ringtones.CheckState = 1 And Videos.CheckState = 1 Then
            Call MovingFiles("net_rim_bb_medialoader_", "LiveShrink")
            Call MovingFiles("net_rim_bb_mediaOTA_", "LiveShrink")
        End If
        If CustomShrinkBox.Text = "" Then
        ElseIf CustomShrinkBox.Text = "Enter your custom shrinking items, seperated by line. Example: net_rim_bb_*" Then
        ElseIf CustomShrinkBox.Text = "net_rim_bb_*" Then
        Else
            ToolStripStatusLabel1.Text = "Shrinking your Custom Shrink files..."
            Dim FileNameString As String
            For Each FileNameString In CustomShrinkBox.Lines
                If My.Computer.FileSystem.FileExists(My.Settings.InputFolder & "\Java\" & FileNameString) Then
                    Call MovingFiles(FileNameString, "LiveShrink")
                Else
                End If
            Next
        End If
        Dim myProcess As New Process()
        Dim myProcessStartInfo As New ProcessStartInfo("JavaLoader.exe", " -w" & DevicePass.Text & " erase -f " & My.Settings.CodList)
        myProcessStartInfo.UseShellExecute = False
        myProcessStartInfo.CreateNoWindow = True
        myProcessStartInfo.RedirectStandardOutput = True
        myProcess.StartInfo = myProcessStartInfo
        myProcess.Start()
        myProcess.Close()
        If My.Settings.CodList = "" Then
            ToolStripStatusLabel1.Text = "No .COD file(s) to remove... Your OS has already been shrunk!"
            MsgBox("No .COD file(s) to remove... Your OS has already been shrunk!")
        Else
            ToolStripStatusLabel1.Text = ".COD file(s) were successfully removed."
            MsgBox(".COD file(s) were successfully removed.")
        End If
        My.Settings.CodList = ""
    End Sub

    
    Private Sub SwapList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SwapList.Click
        FileSwapper.ShowDialog()
    End Sub
    Private Sub UploadIt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UploadIt.Click
        If UploadIt.Checked = True Then
            TweetOTA.Enabled = True
        ElseIf UploadIt.Checked = False Then
            TweetOTA.Enabled = False
        End If
    End Sub
    Private Sub LiveShrinkInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LiveShrinkInfo.Click
        MsgBox("This is a BETA feature that shrinks the OS on a device that has one that is unshrunk/untouched. Plug in your device and Please create a back-up before clicking the 'Shrink My OS Live' button", MsgBoxStyle.Information, "About 'Live Shrink-A-OS'")
    End Sub
    Private Sub RefreshFolders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshFolders.Click
        Call RefreshOSFolders()
    End Sub
    Private Sub RefreshFolders_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles RefreshFolders.MouseHover
        ToolStripStatusLabel1.Text = "Refresh OS Folders"
    End Sub
    Private Sub RefreshFolders_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles RefreshFolders.MouseLeave
        ToolStripStatusLabel1.Text = WelcomeText
    End Sub
    Private Sub SelectAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
        '//Langs
        Call RemoveAllLangs(True)
        '//Apps
        Call RemoveApps(True)
        '//Docs2Go
        Call RemoveDocs2Go(True)
        '//IMs
        Call RemoveIMs(True)
        '//Defaults
        Call RemoveDefault(True)
        '//Fonts
        Call RemoveAllFonts(True)
        '//Games
        Call RemoveAllGames(True)
    End Sub
    Private Sub SelectNoneToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectNoneToolStripMenuItem.Click
        '//Langs
        Call RemoveAllLangs(False)
        '//Apps
        Call RemoveApps(False)
        '//DocsToGo
        Call RemoveDocs2Go(False)
        '//IMs
        Call RemoveIMs(False)
        '//Fonts
        Call RemoveAllFonts(False)
        '//Games
        Call RemoveAllGames(False)
        '//Defaults
        Call RemoveDefault(False)
        AccessOptions.Checked = False
        OTTBluetooth.Checked = False
        OTTCamera.Checked = False
        DefaultDMTree.Checked = False
        eScreen.Checked = False
        OTAUpgrade.Checked = False
        OTTVideoCamera.Checked = False
        ToolStripStatusLabel1.Text = "All options have been deselected."
    End Sub

    Private Sub LaunchLoaderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LaunchLoaderToolStripMenuItem.Click
        Call LaunchLoader()
    End Sub
    Private Sub BlackBerryOSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BlackBerryOSToolStripMenuItem.Click
        ToolStripStatusLabel1.Text = "Showing the BlackberryOS BBH Tool topic..."
        System.Diagnostics.Process.Start("http://www.blackberryos.com/forums/software-applications/16252-bbhybrids-tool-shrink-os-create-jad.html")
        ToolStripStatusLabel1.Text = WelcomeText
    End Sub
    Private Sub CrackBerryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CrackBerryToolStripMenuItem.Click
        ToolStripStatusLabel1.Text = "Showing the Crackberry BBH Tool topic..."
        System.Diagnostics.Process.Start("http://forums.crackberry.com/f35/bbhybrids-tool-shrink-os-create-jad-476404/")
        ToolStripStatusLabel1.Text = WelcomeText
    End Sub
    Public Sub TwitterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TwitterToolStripMenuItem.Click
        ToolStripStatusLabel1.Text = "Showing @theiexplorers Twitter Page - Follow along if you'd like..."
        System.Diagnostics.Process.Start("www.twitter.com/theiexplorers/")
        ToolStripStatusLabel1.Text = WelcomeText
    End Sub
    Public Sub Donation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Donation.Click
        System.Diagnostics.Process.Start("www.theiexplorers.com/donate.html")
        ToolStripStatusLabel1.Text = "Thanks for donating! (Or thinking about it!)"
    End Sub
    Public Sub HowToToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HowToToolStripMenuItem1.Click
        ToolStripStatusLabel1.Text = "Opening the How To page..."
        HowTo.Show()
        ToolStripStatusLabel1.Text = WelcomeText
    End Sub
    Public Sub GoToWebsiteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoToWebsiteToolStripMenuItem.Click
        Call LaunchHome()
    End Sub
    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        About.ShowDialog()
    End Sub
    Sub LaunchHome()
        ToolStripStatusLabel1.Text = "Showing theiexplorers.com..."
        System.Diagnostics.Process.Start("http://www.theiexplorers.com")
        ToolStripStatusLabel1.Text = WelcomeText
    End Sub
    Public Sub HomeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HomeToolStripMenuItem.Click
        Call LaunchHome()
    End Sub

    Private Sub DeviceUnlocksToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeviceUnlocksToolStripMenuItem.Click
        ToolStripStatusLabel1.Text = "Showing the BBH-Plus Device Unlocks..."
        System.Diagnostics.Process.Start("http://bb-h.me/unlocks/")
        ToolStripStatusLabel1.Text = WelcomeText
    End Sub
    Private Sub HybridsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HybridsToolStripMenuItem.Click
        ToolStripStatusLabel1.Text = "Showing @theiexplorers Hybrids..."
        System.Diagnostics.Process.Start("http://www.theiexplorers.com/bb/Hybrids/")
        ToolStripStatusLabel1.Text = WelcomeText
    End Sub
    Private Sub GitHubToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GitHubToolStripMenuItem.Click
        ToolStripStatusLabel1.Text = "Showing the BBH-Plus Device Unlocks..."
        System.Diagnostics.Process.Start("http://www.github.com/lyricidal/")
        ToolStripStatusLabel1.Text = WelcomeText
    End Sub
    Private Sub OTAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OTAsToolStripMenuItem.Click
        ToolStripStatusLabel1.Text = "Showing the theiexplorers.com OTA Downloads..."
        System.Diagnostics.Process.Start("http://www.theiexplorers.com/OTA/")
        ToolStripStatusLabel1.Text = WelcomeText
    End Sub
    Private Sub ShrinkMyOSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShrinkMyOSToolStripMenuItem.Click
        Call ShrinkInitiate()
    End Sub
    Private Sub BBDM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call LaunchDM()
    End Sub
    Private Sub BBLinkClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call LaunchLink()
    End Sub
    Sub LaunchDM()
        'DM Launch
        ToolStripStatusLabel1.Text = "Launching Desktop Manager to update your phone. Please plug it in now."
        If My.Computer.FileSystem.FileExists("C:\Program Files (x86)\Research In Motion\BlackBerry Desktop\Rim.Desktop.exe") Then
            Shell("C:\Program Files (x86)\Research In Motion\BlackBerry Desktop\Rim.Desktop.exe", AppWinStyle.NormalFocus)
        ElseIf My.Computer.FileSystem.FileExists("C:\Program Files\Research In Motion\BlackBerry Desktop\Rim.Desktop.exe") Then
            Shell("C:\Program Files\Research In Motion\BlackBerry Desktop\Rim.Desktop.exe", AppWinStyle.NormalFocus)
        ElseIf My.Computer.FileSystem.FileExists("D:\Program Files (x86)\Research In Motion\BlackBerry Desktop\Rim.Desktop.exe") Then
            Shell("D:\Program Files (x86)\Research In Motion\BlackBerry Desktop\Rim.Desktop.exe", AppWinStyle.NormalFocus)
        ElseIf My.Computer.FileSystem.FileExists("D:\Program Files\Research In Motion\BlackBerry Desktop\Rim.Desktop.exe") Then
            Shell("D:\Program Files\Research In Motion\BlackBerry Desktop\Rim.Desktop.exe", AppWinStyle.NormalFocus)
        ElseIf My.Computer.FileSystem.FileExists("E:\Program Files\Research In Motion\BlackBerry Desktop\Rim.Desktop.exe") Then
            Shell("E:\Program Files\Research In Motion\BlackBerry Desktop\Rim.Desktop.exe", AppWinStyle.NormalFocus)
        ElseIf My.Computer.FileSystem.FileExists("E:\Program Files (x86)\Research In Motion\BlackBerry Desktop\Rim.Desktop.exe") Then
            Shell("E:\Program Files (x86)\Research In Motion\BlackBerry Desktop\Rim.Desktop.exe", AppWinStyle.NormalFocus)
        ElseIf My.Computer.FileSystem.FileExists("F:\Program Files\Research In Motion\BlackBerry Desktop\Rim.Desktop.exe") Then
            Shell("F:\Program Files\Research In Motion\BlackBerry Desktop\Rim.Desktop.exe", AppWinStyle.NormalFocus)
        Else
        End If
        ToolStripStatusLabel1.Text = WelcomeText
    End Sub
    Sub LaunchLink()
        'Link Launch
        ToolStripStatusLabel1.Text = "Launching Desktop Manager to update your phone. Please plug it in now."
        If My.Computer.FileSystem.FileExists("C:\Program Files (x86)\Research In Motion\BlackBerry Link\BlackBerryLink.exe") Then
            Shell("C:\Program Files (x86)\Research In Motion\BlackBerry Link\BlackBerryLink.exe", AppWinStyle.NormalFocus)
        ElseIf My.Computer.FileSystem.FileExists("C:\Program Files\Research In Motion\BlackBerry Link\BlackBerryLink.exe") Then
            Shell("C:\Program Files\Research In Motion\BlackBerry Link\BlackBerryLink.exe", AppWinStyle.NormalFocus)
        ElseIf My.Computer.FileSystem.FileExists("D:\Program Files (x86)\Research In Motion\BlackBerry Link\BlackBerryLink.exe") Then
            Shell("D:\Program Files (x86)\Research In Motion\BlackBerry Link\BlackBerryLink.exe", AppWinStyle.NormalFocus)
        ElseIf My.Computer.FileSystem.FileExists("D:\Program Files\Research In Motion\BlackBerry Link\BlackBerryLink.exe") Then
            Shell("D:\Program Files\Research In Motion\BlackBerry Link\BlackBerryLink.exe", AppWinStyle.NormalFocus)
        ElseIf My.Computer.FileSystem.FileExists("E:\Program Files\Research In Motion\BlackBerry Link\BlackBerryLink.exe") Then
            Shell("E:\Program Files\Research In Motion\BlackBerry Link\BlackBerryLink.exe", AppWinStyle.NormalFocus)
        ElseIf My.Computer.FileSystem.FileExists("E:\Program Files (x86)\Research In Motion\BlackBerry Link\BlackBerryLink.exe") Then
            Shell("E:\Program Files (x86)\Research In Motion\BlackBerry Link\BlackBerryLink.exe", AppWinStyle.NormalFocus)
        ElseIf My.Computer.FileSystem.FileExists("F:\Program Files\Research In Motion\BlackBerry Link\BlackBerryLink.exe") Then
            Shell("F:\Program Files\Research In Motion\BlackBerry Link\BlackBerryLink.exe", AppWinStyle.NormalFocus)
        Else
        End If
        ToolStripStatusLabel1.Text = WelcomeText
    End Sub

    Private Sub ShrinkHelp_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles ShrinkHelp.LinkClicked
        HowTo.HowToTabs.SelectedIndex = 0
        HowTo.Show()
    End Sub

    Private Sub CAJHelp_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles CAJHelp.LinkClicked
        HowTo.HowToTabs.SelectedIndex = 1
        HowTo.Show()
    End Sub

    Private Sub OTAHelp_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles OTAHelp.LinkClicked
        HowTo.HowToTabs.SelectedIndex = 2
        HowTo.Show()
    End Sub

    Private Sub BahHelpLinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles BaHHelp.LinkClicked
        HowTo.HowToTabs.SelectedIndex = 3
        HowTo.Show()
    End Sub
    Private Sub PhoneHelp_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles PhoneHelp.LinkClicked
        HowTo.HowToTabs.SelectedIndex = 4
        HowTo.Show()
    End Sub
    Private Sub PlayBookHelp_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles PlayBookHelp.LinkClicked
        HowTo.HowToTabs.SelectedIndex = 5
        HowTo.Show()
    End Sub
    Private Sub OtherHelp_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        HowTo.HowToTabs.SelectedIndex = 6
        HowTo.Show()
    End Sub

    Private Sub OfflineMode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OfflineMode.CheckedChanged
        AppManager.Items.Clear()
        Bluetooth.Items.Clear()
        Browser.Items.Clear()
        Camera.Items.Clear()
        Fonts.Items.Clear()
        GPS.Items.Clear()
        Media.Items.Clear()
        Plazmic.Items.Clear()
        PLauncher.Items.Clear()
        VideoCamera.Items.Clear()
        Keyboard.Items.Clear()
        Dim fo As Object
        Dim fs As Object
        fs = CreateObject("Scripting.FileSystemObject")
        Call FindLoaderFolder()
        If My.Settings.LoaderDir = "" Then
        Else
            fo = fs.GetFolder(My.Settings.LoaderDir)
            For Each x In fo.SubFolders
                AppManager.Items.Add(x.name)
                Bluetooth.Items.Add(x.name)
                Browser.Items.Add(x.name)
                Camera.Items.Add(x.name)
                Fonts.Items.Add(x.name)
                GPS.Items.Add(x.name)
                Media.Items.Add(x.name)
                Plazmic.Items.Add(x.name)
                PLauncher.Items.Add(x.name)
                VideoCamera.Items.Add(x.name)
                If x.name.Contains("95") Then
                    Keyboard.Items.Add(x.name)
                ElseIf x.name.Contains("98") Then
                    Keyboard.Items.Add(x.name)
                End If
            Next
        End If
    End Sub

    Private Sub JavaLoaderBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JavaLoaderBox.SelectedIndexChanged
        If JavaLoaderBox.SelectedItem = "" Then
        Else
            'MsgBox(JavaLoaderBox.SelectedItem)
        End If
    End Sub

    Private Sub CaJBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CaJBrowse.Click
        ToolStripStatusLabel1.Text = "Browsing... (Select your .cod files to create your install)"
        ' Create an instance of the open file dialog box.
        ' Set filter options and filter index.
        OpenFileDialog1.Filter = "COD Files (*.cod)|*.cod"
        OpenFileDialog1.FilterIndex = 1
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Multiselect = True
        ' Call the ShowDialog method to show the dialogbox.

        ' Process input if the user clicked OK.
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            DragCOD.Hide()
            ToolStripStatusLabel1.Text = "Adding .COD(s)..."
            Dim file As String
            Dim filepath As String
            Dim infoReader As System.IO.FileInfo
            For Each file In OpenFileDialog1.SafeFileNames
                ToolStripStatusLabel1.Text = file & " is being added to the file list..."
                FileBox.Items.Add(file)
            Next
            For Each filepath In OpenFileDialog1.FileNames
                infoReader = My.Computer.FileSystem.GetFileInfo(filepath)
                SizeBox.Items.Add(infoReader.Length)
            Next
            Dim FileCounts = FileBox.Items.Count.ToString
            FileCount.Text = "Total Files: " & FileCounts
            ToolStripStatusLabel1.Text = "Selected .COD(s) have been added to the list for your OS install. Enjoy!"
        Else
            ToolStripStatusLabel1.Text = "You did not choose any .COD(s). Please choose some .COD(s) for your install."
            MsgBox("You did not choose any .COD(s). Please choose some .COD(s) for your install.")
        End If
    End Sub
    Private Sub CustomShrinkOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomShrinkOK.Click
        CustomGroupBox.Visible = False
    End Sub
    Private Sub CustomShrink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomShrink.Click
        If CustomGroupBox.Visible = True Then
            CustomGroupBox.Visible = False
            CustomGroupBox.SendToBack()
        Else
            CustomGroupBox.Visible = True
            CustomGroupBox.BringToFront()
        End If
    End Sub
    Private Sub BatchOTAButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BatchOTAButton.Click
        If BatchOTAs.Visible = True Then
            BatchOTAs.Visible = False
        Else
            BatchOTAs.Visible = True
        End If
    End Sub
    Private Sub HideBatchOTAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HideOTABatch.Click
        BatchOTAs.Visible = False
    End Sub
    Private Sub ClearBatchOTAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearBatchOTA.Click
        BatchOTAList.Clear()
    End Sub
    Private Sub BatchOTAList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BatchOTAList.Click
        If BatchOTAList.Text = "Enter your OTA links here, seperated by line. " Then
            BatchOTAList.Text = ""
        End If
    End Sub
    Private Sub LoadOTAList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadOTAList.Click
        ToolStripStatusLabel1.Text = "Attempting to load OTA list from file..."
        '//Prompt if exists
        OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.CurrentDirectory
        OpenFileDialog1.Filter = "All Formats|*.ini;*.txt|.ini Files (*.ini)|*.ini|Text Files (*.txt)|*.txt"
        OpenFileDialog1.FilterIndex = 1
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Multiselect = True
        ' Call the ShowDialog method to show the dialogbox.
        ' Process input if the user clicked OK.
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            BatchOTAList.Clear()
            Using settingsfile As StreamReader = New StreamReader(OpenFileDialog1.FileName)
                ' Read and display the lines from the file until the end 
                ' of the file is reached.
                For Each OTAItem As String In settingsfile.ReadLine
                    OTAItem = settingsfile.ReadLine
                    If OTAItem = "" Then
                    Else
                        BatchOTAList.AppendText(OTAItem & vbCrLf)
                    End If
                Next
            End Using
        Else
            ToolStripStatusLabel1.Text = "OTA list has not been loaded."
        End If
    End Sub
    Private Sub RecoveryMem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecoveryMem.Click
        JavaLoaderProgress.Value = 0
        JavaLoaderProgress.Maximum = 3
        If RecoverMemoryAmount.Value = 0 Then
            MsgBox("Please enter an amount of memory to receover")
            ToolStripStatusLabel1.Text = "Please enter an amount of memory to receover"
        Else
            ToolStripStatusLabel1.Text = "Attempting to recover memory..."
            JavaLoaderProgress.PerformStep()
            Dim myProcess As New Process()
            Dim MemoryAmount As Integer
            MemoryAmount = RecoverMemoryAmount.Value * 1024
            MemoryAmount = MemoryAmount * 1024
            Dim myProcessStartInfo As New ProcessStartInfo("JavaLoader.exe", " -w" & DevicePass.Text & " recoverflash " & RecoverMemoryAmount.Value * 1024)
            myProcessStartInfo.UseShellExecute = False
            myProcessStartInfo.CreateNoWindow = True
            myProcessStartInfo.RedirectStandardOutput = True
            myProcess.StartInfo = myProcessStartInfo
            JavaLoaderProgress.PerformStep()
            myProcess.Start()
            myProcess.Close()
            MsgBox("All possible memory recovered!")
            ToolStripStatusLabel1.Text = "All possible memory recovered!"
            JavaLoaderProgress.PerformStep()
        End If
    End Sub
    Private Sub SaveAllSettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAllSettingsToolStripMenuItem.Click
        Call SaveSettingsToFile()
    End Sub
    Private Sub LoadAllSettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadAllSettingsToolStripMenuItem.Click
        If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.CurrentDirectory & "\BBHToolSettings.ini") Then
            Call LoadSettings(My.Computer.FileSystem.CurrentDirectory & "\BBHToolSettings.ini")
        Else
            Call BrowseSettings()
        End If
    End Sub
    Sub BrowseSettings()
        ToolStripStatusLabel1.Text = "Attempting to load BBHToolSettings.ini..."
        '//Prompt if exists
        Dim msg1 = "BBHToolSettings.ini does not exist in " & My.Computer.FileSystem.CurrentDirectory & " would you like to select it?"
        Dim title1 = "Browse for BBHToolSettings.ini?"
        Dim style1 = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or _
                    MsgBoxStyle.Critical
        ' Display the message box and save the response, Yes or No.
        Dim response1 = MsgBox(msg1, style1, title1)
        If response1 = MsgBoxResult.Yes Then
            OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.CurrentDirectory
            OpenFileDialog1.Filter = "All Formats|*.ini;*.txt|.ini Files (*.ini)|*.ini|Text Files (*.txt)|*.txt"
            OpenFileDialog1.FilterIndex = 1
            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Multiselect = True
            ' Call the ShowDialog method to show the dialogbox.
            ' Process input if the user clicked OK.
            If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Call LoadSettings(OpenFileDialog1.FileName)
            End If
        Else
            ToolStripStatusLabel1.Text = "Shrink settings have not been loaded."
        End If
    End Sub

    Private Sub RestoreJava_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RestoreJava.Click
        Dim Install As String = My.Settings.InstallDir
        '//Prompt if exists
        Dim msg1 = "Are you sure would you like to restore your original Java folder? This will erase all hybrid work you have done."
        Dim title1 = "Resore Original Java?"
        Dim style1 = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or _
                    MsgBoxStyle.Critical
        ' Display the message box and save the response, Yes or No.
        Dim response1 = MsgBox(msg1, style1, title1)
        If response1 = MsgBoxResult.Yes Then
            ToolStripStatusLabel1.Text = "Restoring original Java folder..."
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Install & "\Java\Backup\", Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, "*.cod")
                Dim foundFileInfo As New System.IO.FileInfo(foundFile)
                My.Computer.FileSystem.CopyFile(foundFile, Install & "\Java\" & foundFileInfo.Name, True)
            Next
            ToolStripStatusLabel1.Text = "Original Java has been restored."
        Else
            ToolStripStatusLabel1.Text = "Original Java has not been restored."
        End If
    End Sub
    Private Sub AddCODsToList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddCODsToList.Click
        If AddCODsToList.Checked = True Then
            Dim msg1 = "Please note: Adding the files to the list via the .alx/.jad will not add the file sizes required for a .jad file. Are you sure you would still like to add the file names?"
            Dim title1 = "Please read below..."
            Dim style1 = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or _
                        MsgBoxStyle.Critical
            ' Display the message box and save the response, Yes or No.
            Dim response1 = MsgBox(msg1, style1, title1)
            If response1 = MsgBoxResult.Yes Then
            Else
                AddCODsToList.Checked = False
            End If
        Else
        End If
    End Sub
    Private Sub IaHConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call ConnectToPhone()
    End Sub
    Private Sub InstallBuiltHybrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InstallBuiltHybrid.Click
        TabControl1.SelectedIndex = 5
    End Sub
    Sub InstallHybrid()
        filelist = ""
        Call ConnectToPhone()
        If UserPIN.Text = "Not Connected" Then
            MsgBox("Please connect a device to install the hybrid to!")
            ToolStripStatusLabel1.Text = "Please connect a device to install the hybrid to!"
        Else
            If JavaLoaderBox.Items.Count = 0 Then
                MsgBox("There are no files to install for your built hybrid!")
                ToolStripStatusLabel1.Text = "There are no files to install for your built hybrid!"
            Else
                'For Each S As String In IaHInstallList.Items
                'filelist = S & " " & filelist
                'Next
                Dim myProcess As New Process()
                Dim myProcessStartInfo As New ProcessStartInfo("JavaLoader.exe", " -w" & DevicePass.Text & " load " & filelist)
                myProcessStartInfo.UseShellExecute = False
                myProcessStartInfo.CreateNoWindow = True
                myProcessStartInfo.RedirectStandardOutput = True
                myProcess.StartInfo = myProcessStartInfo
                myProcess.Start()
                myProcess.Close()
            End If
        End If
    End Sub

    Private Sub CustomShrinkBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomShrinkBox.Click
        If CustomShrinkBox.Text = "Enter your custom shrinking items, seperated by line. Example: net_rim_bb_*" Then
            CustomShrinkBox.Text = ""
        End If
    End Sub
    Private Sub MakeASuggestionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportABugToolStripMenuItem.Click
        System.Diagnostics.Process.Start("mailto:bbhtool@theiexplorers.com?subject=BBHTool v" & My.Settings.VersionNumber & " Bug Report")
    End Sub
    Private Sub WC_DownloadProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs) Handles WC.DownloadProgressChanged
        BarDownloadProgress.Value = e.ProgressPercentage
    End Sub
    Private Sub WC_DownloadFileCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs) Handles WC.DownloadFileCompleted
        'RemoteBarStatus.Text = "Installing " & BarFile & "..."
        If My.Settings.CodList.Contains(".bar") Then
            Call AltInstallBar(My.Settings.CodList)
        ElseIf My.Settings.CodList.Contains(".apk") Then
            Call APKInstall(My.Settings.CodList)
        End If
        My.Settings.CodList = ""
    End Sub
    Private Results As String
    Private Delegate Sub delUpdate()
    Private Installed As New delUpdate(AddressOf UpdateText)
    Private APKInstalled As New delUpdate(AddressOf APKUpdateText)
    Private Uninstalled As New delUpdate(AddressOf UnUpdateText)
    Private Sub InstallToPB_Click(sender As System.Object, e As System.EventArgs) Handles InstallToPB.Click
        If My.Computer.FileSystem.DirectoryExists("C:\Program Files (x86)\Java\") Then
        ElseIf My.Computer.FileSystem.DirectoryExists("C:\Program Files\Java\") Then
        ElseIf My.Computer.FileSystem.DirectoryExists("D:\Program Files (x86)\Java\") Then
        ElseIf My.Computer.FileSystem.DirectoryExists("D:\Program Files\Java\") Then
        ElseIf My.Computer.FileSystem.DirectoryExists("E:\Program Files\Java\") Then
        ElseIf My.Computer.FileSystem.DirectoryExists("E:\Program Files (x86)\Java\") Then
        ElseIf My.Computer.FileSystem.DirectoryExists("F:\Program Files\Java\") Then
        Else
            'MsgBox("Java Runtime Environment not detected. Functions on the PlayBook tab may not work correctly.")
        End If
        If InstallToPBWorker.IsBusy Then Exit Sub
        InstallToPBWorker.RunWorkerAsync()
    End Sub
    Private Sub InstallToPBWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles InstallToPBWorker.DoWork
        Dim barfile As String
        Dim apkfile As String
        barfile = ""
        apkfile = ""
        For Each X As String In PBBox.CheckedItems
            If X.Contains(".apk") Then
                apkfile = "yes"
            ElseIf X.Contains(".bar") Then
                barfile = "yes"
            End If
        Next
        If barfile = "yes" Then
            Dim CMDThread As New Threading.Thread(AddressOf InstallBar)
            CMDThread.Start()
        End If
        If apkfile = "yes" Then
            Dim CMDThread As New Threading.Thread(AddressOf APKInstall)
            CMDThread.Start()
        End If
        If RemoteBarLink.Text.EndsWith(".bar") Then
            UseWaitCursor = True
            PlayBookProgressBar.MarqueeAnimationSpeed = 20
            For Each X As String In RemoteBarLink.Text.Split("/")
                If X.Contains(".bar") Then
                    barfile = X
                End If
            Next
            RemoteBarStatus.Text = "Downloading " & barfile & "..."
            WC.DownloadFileAsync(New Uri(RemoteBarLink.Text), barfile)
            My.Settings.CodList = barfile
        ElseIf RemoteBarLink.Text.EndsWith(".apk") Then
            UseWaitCursor = True
            PlayBookProgressBar.MarqueeAnimationSpeed = 20
            For Each X As String In RemoteBarLink.Text.Split("/")
                If X.Contains(".apk") Then
                    apkfile = X
                End If
            Next
            RemoteBarStatus.Text = "Downloading " & apkfile & "..."
            WC.DownloadFileAsync(New Uri(RemoteBarLink.Text), apkfile)
            My.Settings.CodList = apkfile
        ElseIf PBBox.CheckedItems.Count = 0 Then
            MsgBox("No files selected to be installed.")
        Else
        End If
    End Sub
    Private Sub InstallBar()
        Dim Barfile As String
        Barfile = ""
        PlayBookInfo = "java -Xmx512M -jar lib\BarDeploy.jar -installApp -device " & PlayBookIP.Text & " -password " & PlayBookPassword.Text
        If LaunchBarAfterInstall.Checked = True Then
            PlayBookInfo = "java -Xmx512M -jar lib\BarDeploy.jar -installApp -launchApp -device " & PlayBookIP.Text & " -password " & PlayBookPassword.Text
        Else
        End If
        Dim myprocess As New Process
        Dim StartInfo As New System.Diagnostics.ProcessStartInfo
        StartInfo.FileName = "cmd" 'starts cmd window
        StartInfo.RedirectStandardInput = True
        StartInfo.RedirectStandardOutput = True
        StartInfo.UseShellExecute = False 'required to redirect
        StartInfo.CreateNoWindow = True 'creates no cmd window
        myprocess.StartInfo = StartInfo
        myprocess.Start()
        UseWaitCursor = True
        PlayBookProgressBar.MarqueeAnimationSpeed = 20
        Dim SR As System.IO.StreamReader = myprocess.StandardOutput
        Dim SW As System.IO.StreamWriter = myprocess.StandardInput
        SW.WriteLine("cd " & My.Computer.FileSystem.CurrentDirectory)
        For Each Bar As String In PBBox.CheckedItems
            If Bar.Contains(".bar") Then
                SW.WriteLine(PlayBookInfo & " " & """" & Bar & """") 'the command you wish to run.....
                ToolStripStatusLabel1.Text = "Installing .BAR(s)..."
            Else
            End If
        Next
        If PBBox.CheckedItems.Contains(".apk") Then
            'Add A convert command?
        End If
        SW.WriteLine("exit") 'exits command prompt window
        Results = SR.ReadToEnd 'returns results of the command window
        SW.Close()
        SR.Close()
        'invokes Finished delegate, which updates textbox with the results text
        Invoke(Installed)
    End Sub
    Private Sub UpdateText()
        txtResults.AppendText(Results)
        EventLogs.EventLogBox.AppendText(Results)
        UseWaitCursor = False
        PlayBookProgressBar.MarqueeAnimationSpeed = 0
        If Results.Contains("success") Then
            If LaunchBarAfterInstall.Checked = True Then
                ToolStripStatusLabel1.Text = "Selected .BAR(s) have been installed/launched on the connected device. Enjoy!"
                MsgBox("Selected .BAR(s) have been installed/launched on the connected PlayBook. Enjoy!")
            Else
                ToolStripStatusLabel1.Text = "Selected .BAR(s) have been installed to the connected device. Enjoy!"
                MsgBox("Selected .BAR(s) have been installed to the connected PlayBook. Enjoy!")
            End If
        ElseIf Results.Contains("Error: Device is not in the Development Mode. Switch to Development Mode from Security settings on the device.") Then
            MsgBox("Error: Device is not in the Development Mode. Switch to Development Mode from Security settings on the device.")
        Else
            ToolStripStatusLabel1.Text = "Selected .BAR(s) have NOT been successfull installed to the connected device. Please try again."
            MsgBox("Selected .BAR(s) have NOT been successfull installed to the connected device. Please try again.")
        End If
    End Sub
    Sub AltInstallBar(ByVal BarFile As String)
        PlayBookInfo = "-Xmx512M -jar lib\BarDeploy.jar -installApp -device " & PlayBookIP.Text + " -password " + PlayBookPassword.Text
        If LaunchBarAfterInstall.Checked = True Then
            PlayBookInfo = "-Xmx512M -jar lib\BarDeploy.jar -installApp -launchApp -device " & PlayBookIP.Text + " -password " + PlayBookPassword.Text
        Else
        End If
        ToolStripStatusLabel1.Text = "Installing " & BarFile & "..."
        Dim myProcess As New Process()
        Dim myProcessStartInfo As New ProcessStartInfo("java.exe", PlayBookInfo & " " & BarFile)
        myProcessStartInfo.UseShellExecute = False
        myProcessStartInfo.CreateNoWindow = True
        myProcessStartInfo.RedirectStandardOutput = True
        myProcess.StartInfo = myProcessStartInfo
        myProcess.EnableRaisingEvents = True
        myProcess.Start()
        ' add an Exited event handler
        AddHandler myProcess.Exited, AddressOf Me.AltInstallBarProcessExited
    End Sub
    Private Sub AltInstallBarProcessExited(ByVal sender As Object, ByVal e As System.EventArgs)
        UseWaitCursor = False
        PlayBookProgressBar.MarqueeAnimationSpeed = 0
        If LaunchBarAfterInstall.Checked = True Then
            ToolStripStatusLabel1.Text = "Selected .BAR(s) have been installed/launched on the connected device. Enjoy!"
            MsgBox("Selected .BARs(s) have been installed/launched on the connected PlayBook. Enjoy!")
        Else
            ToolStripStatusLabel1.Text = "Selected .BAR(s) have been installed to the connected device. Enjoy!"
            MsgBox("Selected .BARs(s) have been installed to the connected PlayBook. Enjoy!")
        End If
    End Sub
    Private Sub APKInstall(ByVal APKFile As String)
        Dim myprocess As New Process
        Dim StartInfo As New System.Diagnostics.ProcessStartInfo
        StartInfo.FileName = "cmd" 'starts cmd window
        StartInfo.RedirectStandardInput = True
        StartInfo.RedirectStandardOutput = True
        StartInfo.UseShellExecute = False 'required to redirect
        StartInfo.CreateNoWindow = True 'creates no cmd window
        myprocess.StartInfo = StartInfo
        myprocess.Start()
        UseWaitCursor = True
        PlayBookProgressBar.MarqueeAnimationSpeed = 20
        Dim SR As System.IO.StreamReader = myprocess.StandardOutput
        Dim SW As System.IO.StreamWriter = myprocess.StandardInput
        SW.WriteLine("cd """ & My.Computer.FileSystem.CurrentDirectory & "\lib\""")
        'SW.WriteLine("adb connect " & PlayBookIP.Text & ":5555")
        'SW.WriteLine("adb root")
        For Each APK As String In PBBox.CheckedItems
            If APK.Contains(".apk") Then
                SW.WriteLine("set apkfile=" & """" & APK & """")
                SW.WriteLine("adb install %apkfile%")
                ToolStripStatusLabel1.Text = "Installing " & APK & "..."
            Else
            End If
        Next
        If RemoteBarLink.Text.EndsWith(".apk") Then
            'SW.WriteLine("adb install " & """" & My.Computer.FileSystem.CurrentDirectory & "/" & APKFile & """")
            'ToolStripStatusLabel1.Text = "Installing " & APKFile & "..."
            'MsgBox("")
        End If
        SW.WriteLine("exit") 'exits command prompt window
        Results = SR.ReadToEnd 'returns results of the command window
        SW.Close()
        SR.Close()
        'invokes Finished delegate, which updates textbox with the results text
        Invoke(APKInstalled)
    End Sub
    Private Sub APKUpdateText()
        txtResults.AppendText(Results)
        EventLogs.EventLogBox.AppendText(Results)
        UseWaitCursor = False
        PlayBookProgressBar.MarqueeAnimationSpeed = 0
        If Results.Contains("Success") Then
            ToolStripStatusLabel1.Text = "Selected .APK(s) have been installed to the connected device. Enjoy!"
            MsgBox("Selected .APK(s) have been installed to the connected device. Enjoy!")
        Else
            ToolStripStatusLabel1.Text = "Selected .APK(s) have NOT been successfull installed to the connected device. Please try again."
            MsgBox("Selected .APK(s) have NOT been successfull installed to the connected device. Please try again.")
        End If
    End Sub
    Private Sub UninstallFromPB_Click(sender As System.Object, e As System.EventArgs) Handles UninstallFromPB.Click
        If PBBox.CheckedItems.Count = 0 Then
            MsgBox("No files selected to be uninstalled.")
        Else
            Dim CMDThread As New Threading.Thread(AddressOf UnInstallBar)
            CMDThread.Start()
        End If
    End Sub
    Sub UnInstallBar()
        PlayBookInfo = "java -Xmx512M -jar BarDeploy.jar -uninstallApp -device " & PlayBookIP.Text + " -password " + PlayBookPassword.Text
        PlayBookProgressBar.MarqueeAnimationSpeed = 20
        UseWaitCursor = True
        Dim myprocess As New Process
        Dim StartInfo As New System.Diagnostics.ProcessStartInfo
        StartInfo.FileName = "cmd" 'starts cmd window
        StartInfo.RedirectStandardInput = True
        StartInfo.RedirectStandardOutput = True
        StartInfo.UseShellExecute = False 'required to redirect
        StartInfo.CreateNoWindow = True 'creates no cmd window
        myprocess.StartInfo = StartInfo
        myprocess.Start()
        UseWaitCursor = True
        PlayBookProgressBar.MarqueeAnimationSpeed = 20
        Dim SR As System.IO.StreamReader = myprocess.StandardOutput
        Dim SW As System.IO.StreamWriter = myprocess.StandardInput
        SW.WriteLine("cd """ & My.Computer.FileSystem.CurrentDirectory & "\lib\""")
        For Each Bar As String In PBBox.CheckedItems
            If Bar.Contains(".apk") Then
                SW.WriteLine("set apkfile=" & """" & Bar & """")
                SW.WriteLine("adb uninstall %apkfile%")
                ToolStripStatusLabel1.Text = "Uninstalling " & Bar & "..."
            Else
                SW.WriteLine(PlayBookInfo & " " & """" & Bar & """") 'the command you wish to run.....
                ToolStripStatusLabel1.Text = "Uninstalling " & Bar & "..."
            End If
        Next
        SW.WriteLine("exit") 'exits command prompt window
        Results = SR.ReadToEnd 'returns results of the command window
        SW.Close()
        SR.Close()
        'invokes Finished delegate, which updates textbox with the results text
        Invoke(Uninstalled)
        ' add an Exited event handler
    End Sub
    Private Sub UnUpdateText()
        txtResults.AppendText(Results)
        EventLogs.EventLogBox.AppendText(Results)
        UseWaitCursor = False
        PlayBookProgressBar.MarqueeAnimationSpeed = 0
        ToolStripStatusLabel1.Text = "Selected .BAR(s) have been uninstalled from the connected PlayBook. Enjoy!"
        MsgBox("Selected .BAR(s) have been uninstalled from the connected PlayBook. Enjoy!")
    End Sub
    Private Sub PlayBookIP_Click(sender As System.Object, e As System.EventArgs) Handles PlayBookIP.Click
        If PlayBookIP.Text = "192.168.0.1" Then
            PlayBookIP.Text = ""
        Else
        End If
    End Sub
    Private Sub AutoAddBAR_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles AutoAddBAR.CheckedChanged
        If AutoAddBAR.Checked = True Then
            BarFolderBox.Visible = True
        Else
            BarFolderBox.Visible = False
        End If
    End Sub
    Private Sub AddBar_Click(sender As System.Object, e As System.EventArgs) Handles AddBar.Click
        PBDeviceInfoBox.Visible = False
        ToolStripStatusLabel1.Text = "Browsing... (Select your .bar files to install)"
        ' Create an instance of the open file dialog box.
        ' Set filter options and filter index. 
        OpenFileDialog1.Filter = "BlackBerry PlayBook Files (*.bar)|*.bar|Android Files (*.apk)|*.apk|Android and PlayBook Files (*.apk,*.bar)|*.apk;*.bar"
        OpenFileDialog1.FilterIndex = 1
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Multiselect = True
        ' Call the ShowDialog method to show the dialogbox.
        ' Process input if the user clicked OK.
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            JavaLoaderProgress.Maximum = OpenFileDialog1.FileNames.Count + 1
            Dim file As String
            file = ""
            DragDropBar.Visible = False
            APKConverter.DragAPK.Hide()
            APKConverter.FileBox.Items.Clear()
            For Each file In OpenFileDialog1.FileNames
                If file.Contains(".bar") Then
                    ToolStripStatusLabel1.Text = "Adding .BAR(s)..."
                    PBBox.Items.Add(file)
                    FilesGroupBox.Text = "Files (" & PBBox.Items.Count & "):"
                    ToolStripStatusLabel1.Text = "Files added..."
                End If
                If file.Contains(".apk") Then
                    If ConvertAPKOption.Checked = True Then
                        APKConverter.ToolStripStatusLabel1.Text = "Adding .APKs..."
                        APKConverter.FileBox.Items.Add(file)
                        APKConverter.FilesGroupBox.Text = "Files (" & APKConverter.FileBox.Items.Count & "):"
                        APKConverter.PlayBookIPText = PlayBookIP.Text
                        APKConverter.PlayBookPassText = PlayBookPassword.Text
                        APKConverter.Show()
                        APKConverter.ToolStripStatusLabel1.Text = "Files added..."
                    Else
                        ToolStripStatusLabel1.Text = "Adding .APK(s)..."
                        PBBox.Items.Add(file)
                        FilesGroupBox.Text = "Files (" & PBBox.Items.Count & "):"
                        ToolStripStatusLabel1.Text = "Files added..."
                    End If

                End If
            Next
        Else
        ToolStripStatusLabel1.Text = "You did not choose any .APK/.BAR(s). Please choose some compatible files to add."
        MsgBox("You did not choose any .APK/.BAR(s). Please choose some compatible files to add.")
        End If
        ToolStripStatusLabel1.Text = WelcomeText
    End Sub
    Private Sub PBDeviceInfo_Click(sender As System.Object, e As System.EventArgs) Handles PBDeviceInfo.Click
        HideInfoBox.Visible = True
        SavePBDeviceInfo.Enabled = True
        SaveAppRead.Enabled = False
        PlayBookProgressBar.MarqueeAnimationSpeed = 20
        PBDeviceInfoBox.Visible = True
        PBDeviceInfoBox.Clear()
        Dim myProcess As New Process()
        Dim PlayBookInfo As String
        PlayBookInfo = "-Xmx512M -jar lib\BarDeploy.jar -listDeviceInfo -device " & PlayBookIP.Text + " -password " + PlayBookPassword.Text
        Dim myProcessStartInfo As New ProcessStartInfo("java.exe", PlayBookInfo & " " & PBBox.SelectedItem)
        myProcessStartInfo.UseShellExecute = False
        myProcessStartInfo.CreateNoWindow = True
        myProcessStartInfo.RedirectStandardOutput = True
        myProcess.StartInfo = myProcessStartInfo
        myProcess.Start()
        Dim myStreamReader As StreamReader = myProcess.StandardOutput
        Dim myString As String
        Dim PIN As String
        PIN = ""
        ' Read the standard output of the spawned process.
        Do
            myString = myStreamReader.ReadLine()
            Try
                If myString.Contains("Sending Device Info request...") Then
                    'myString = myString.Replace("PIN:                0x", "")
                    'UserPIN.Text = myString.ToUpper
                    PBDeviceInfoBox.AppendText("Device Info" & vbCrLf)
                    PBDeviceInfoBox.AppendText("------------------" & vbCrLf)
                ElseIf myString.Contains("hardwareid::") Then
                    myString = myString.Replace("hardwareid::", "Hardware ID: ")
                    PBDeviceInfoBox.AppendText(myString & vbCrLf)
                ElseIf myString.Contains("scmbundle::") Then
                    myString = myString.Replace("scmbundle::", "OS: BlackBerry Tablet OS v")
                    PBDeviceInfoBox.AppendText(myString & vbCrLf)
                ElseIf myString.Contains("devicepin::") Then
                    myString = myString.Replace("devicepin::0x", "PIN: ")
                    myString = myString.ToUpper()
                    PBDeviceInfoBox.AppendText(myString & vbCrLf)
                    PIN = myString
                ElseIf myString.Contains("deviceserialnumber::") Then
                    myString = myString.Replace("deviceserialnumber::", "Serial Number:")
                    PBDeviceInfoBox.AppendText(myString & vbCrLf)
                ElseIf myString.Contains("[n]dev_mode_enabled:b:true") Then
                    myString = myString.Replace("[n]dev_mode_enabled:b:true", "Development Mode: Enabled")
                    PBDeviceInfoBox.AppendText(myString & vbCrLf)
                ElseIf myString.Contains("dev_mode_enabled:b:false") Then
                    myString = myString.Replace("dev_mode_enabled:b:false", "Development Mode: False")
                    PBDeviceInfoBox.AppendText(myString & vbCrLf)
                ElseIf myString.Contains("[n]debug_token_installed:b:false") Then
                    myString = myString.Replace("[n]debug_token_installed:b:false", "Debug Token Installed: False")
                    PBDeviceInfoBox.AppendText(myString & vbCrLf)
                ElseIf myString.Contains("[n]debug_token_installed:b:true") Then
                    myString = myString.Replace("[n]debug_token_installed:b:true", "Debug Token Installed: True")
                    PBDeviceInfoBox.AppendText(myString & vbCrLf)
                ElseIf myString.Contains("[n]dev_mode_expiration::") Then
                    myString = myString.Replace("[n]dev_mode_expiration::", "")
                    myString = myString.Replace("d", " days")
                    myString = "Development Mode Expiration: " + myString
                    PBDeviceInfoBox.AppendText(myString & vbCrLf)
                ElseIf myString.Contains("air_version::") Then
                    PBDeviceInfoBox.AppendText(vbCrLf)
                    PBDeviceInfoBox.AppendText("Versions" & vbCrLf)
                    PBDeviceInfoBox.AppendText("-------------" & vbCrLf)
                    myString = myString.Replace("air_version::", "Air: ")
                    PBDeviceInfoBox.AppendText(myString & vbCrLf)
                ElseIf myString.Contains("flash_version::") Then
                    myString = myString.Replace("flash_version::", "Flash: ")
                    PBDeviceInfoBox.AppendText(myString & vbCrLf)
                ElseIf myString.Contains("build_id::") Then
                    myString = myString.Replace("build_id::", "Build ID:")
                    PBDeviceInfoBox.AppendText(myString & vbCrLf)
                ElseIf myString.Contains("production_device:b:true") Then
                    myString = myString.Replace("production_device:b:true", "Production Device: True")
                    PBDeviceInfoBox.AppendText(myString & vbCrLf)
                ElseIf myString.Contains("production_device:b:false") Then
                    myString = myString.Replace("production_device:b:false", "Production Device: False")
                    PBDeviceInfoBox.AppendText(myString & vbCrLf)
                Else
                End If
                ToolStripStatusLabel1.Text = "Successfully connected to your PlayBook: " & PIN & "."
            Catch V As Exception
                ToolStripStatusLabel1.Text = "Unable to connect to PlayBook successfully."
            End Try
        Loop Until myProcess.StandardOutput.EndOfStream
        myProcess.Close()
        PlayBookProgressBar.Style = ProgressBarStyle.Blocks
    End Sub
    Private Sub ClearBars_Click(sender As System.Object, e As System.EventArgs) Handles ClearBars.Click
        PBBox.Items.Clear()
        DragDropBar.Visible = True
        PBDeviceInfoBox.Visible = False
        CheckAllBarsApks.Text = "Check All"
        FilesGroupBox.Text = "Files:"
    End Sub
    Private Sub PBIPHelp_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles PBIPHelp.LinkClicked
        Dim msg1 = "To obtain your PlayBook's IP address:" & vbCrLf & "Go to the PlayBook About screen." & vbCrLf & "Change drop-down from 'General' to 'Network.'" & vbCrLf & "The IPv4 is your PlayBook IP."
        Dim title1 = "How To Get Your PlayBook IP"
        Dim style1 = MsgBoxStyle.OkOnly Or MsgBoxStyle.DefaultButton2 Or _
                    MsgBoxStyle.Information
        ' Display the message box and save the response, Yes or No.
        Dim response1 = MsgBox(msg1, style1, title1)
    End Sub
    Private Sub SavePlayBookIP_Click(sender As System.Object, e As System.EventArgs) Handles SavePlayBookIP.Click
        If SavePlayBookIP.Checked = True Then
            My.Settings.PlayBookIP = PlayBookIP.Text
        ElseIf SavePlayBookIP.Checked = False Then
            My.Settings.PlayBookIP = ""
        End If
    End Sub
    Private Sub RememberPlayBookPass_Click(sender As System.Object, e As System.EventArgs) Handles RememberPlayBookPass.Click
        If RememberPlayBookPass.Checked = True Then
            My.Settings.PlayBookPass = PlayBookPassword.Text
        ElseIf SavePlayBookIP.Checked = False Then
            My.Settings.PlayBookPass = ""
        End If
    End Sub
    Private Sub BrowseForBAR_Click(sender As System.Object, e As System.EventArgs) Handles BrowseForBAR.Click
        PBDeviceInfoBox.Visible = False
        ToolStripStatusLabel1.Text = "Browsing... (Select your .BAR file folder)"
        With FolderBrowserDialog1
            .SelectedPath = My.Computer.FileSystem.CurrentDirectory
            .ShowNewFolderButton = False
            .Description = "Navigate to your .BAR file folder:"
        End With
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            BarFolder.Text = FolderBrowserDialog1.SelectedPath & "\"
            My.Settings.BarFolder = FolderBrowserDialog1.SelectedPath & "\"
            Call AddFiles()
            ToolStripStatusLabel1.Text = ".BAR Folder selected. Files have been added to list!"
        Else
            ToolStripStatusLabel1.Text = "No .BAR Folder selected. No files have been added to list!"
        End If
    End Sub
    Sub AddFiles()
        DragDropBar.Visible = False
        PBBox.Items.Clear()
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(My.Settings.BarFolder, Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, "*.bar")
            Dim foundFileInfo As New System.IO.FileInfo(foundFile)
            'ToolStripStatusLabel1.Text = "Adding " & foundFileInfo.Name & "..."
            PBBox.Items.Add(foundFileInfo.FullName)
        Next
    End Sub
    Private Sub RemoteBarLink_Click(sender As System.Object, e As System.EventArgs) Handles RemoteBarLink.Click
        If RemoteBarLink.Text = "Enter a link to a .BAR file you would like to install" Then
            RemoteBarLink.Text = ""
        End If
    End Sub
    Private Sub ListPBApps_Click(sender As System.Object, e As System.EventArgs) Handles ListPBApps.Click
        HideInfoBox.Visible = True
        SavePBDeviceInfo.Enabled = False
        SaveAppRead.Enabled = True
        PlayBookProgressBar.MarqueeAnimationSpeed = 20
        ToolStripStatusLabel1.Text = "Reading PlayBook's Apps..."
        PBDeviceInfoBox.Visible = True
        PBDeviceInfoBox.Clear()
        Dim myProcess As New Process()
        PlayBookInfo = "-Xmx512M -jar lib\BarDeploy.jar -listInstalledApps -device " & PlayBookIP.Text + " -password " + PlayBookPassword.Text
        Dim myProcessStartInfo As New ProcessStartInfo("java.exe", PlayBookInfo & " " & PBBox.SelectedItem)
        myProcessStartInfo.UseShellExecute = False
        myProcessStartInfo.CreateNoWindow = True
        myProcessStartInfo.RedirectStandardOutput = True
        myProcess.StartInfo = myProcessStartInfo
        myProcess.Start()
        Dim myStreamReader As StreamReader = myProcess.StandardOutput
        Dim myString As String
        Dim PIN As String
        PIN = ""
        ' Read the standard output of the spawned process.
        Do
            myString = myStreamReader.ReadLine()
            Try

                For Each X As String In myString.Split(",")
                    If X.Contains("name") Then
                        X = X.Replace("name::", "Name: ")
                        PBDeviceInfoBox.AppendText(X & vbCrLf)
                    ElseIf X.Contains("vendor") Then
                        X = X.Replace("vendor::", "Vendor: ")
                        PBDeviceInfoBox.AppendText(X & vbCrLf)
                    ElseIf X.Contains("version") Then
                        X = X.Replace("version::", "Version: ")
                        PBDeviceInfoBox.AppendText(X & vbCrLf)
                    ElseIf X.Contains("size") Then
                        X = X.Replace("size::", "Size: ")
                        PBDeviceInfoBox.AppendText(X & " bytes" & vbCrLf)
                    ElseIf X.Contains("source::websl") Then
                    ElseIf X.Contains("source::developer") Then
                    ElseIf X.Contains("source::appworld") Then
                        X = "Source: App World"
                        PBDeviceInfoBox.AppendText(X & vbCrLf & vbCrLf)
                    End If
                    If HideSystemApps.Checked = True Then
                    Else
                        If X.Contains("sys.Bing") Then
                            X = "Name: Bing"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.airtunes") Then
                            X = "Name: Air Tunes"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.appworld") Then
                            X = "Name: App World"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.bbbridge") Then
                            X = "Name: BlackBerry Bridge"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.bridgeBBM") Then
                            X = "Name: Bridge BBM"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.bridgeBrowser") Then
                            X = "Name: Bridge Browser"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.bridgeCalendar") Then
                            X = "Name: Bridge Calendar"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.bridgeContacts") Then
                            X = "Name: Bridge Contacts"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.bridgeMemoPad") Then
                            X = "Name: Bridge MemoPad"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.bridgeMessages") Then
                            X = "Name: Bridge Messages"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.bridgeTasks") Then
                            X = "Name: Bridge Tasks"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.browser") Then
                            X = "Name: Browser"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.calculator") Then
                            X = "Name: Calculator"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.camera") Then
                            X = "Name: Camera"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.dxtg.sstg") Then
                            X = "Name: Slideshow To Go"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.dxtg.stg") Then
                            X = "Name: Sheets To Go"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.dxtg.wtg") Then
                            X = "Name: Word To Go"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.filebrowser") Then
                            X = "Name: File Browser"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.help") Then
                            X = "Name: Help"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.musicstore") Then
                            X = "Name: Music Store"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.pictures") Then
                            X = "Name: Pictures"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.podcasts") Then
                            X = "Name: Podcasts"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.screensaver") Then
                            X = "Name: Screensaver Video"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.shutdown") Then
                            X = "Name: Power Off "
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.uri.aol") Then
                            X = "Name: AOL"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.uri.gmail") Then
                            X = "Name: Gmail"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.uri.hotmail") Then
                            X = "Name: Hotmail"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.uri.twitter") Then
                            X = "Name: Twitter"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.uri.yahoo") Then
                            X = "Name: Yahoo"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.videochat") Then
                            X = "Name: Video Chat"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.videoplayer") Then
                            X = "Name: Video Player"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.voicerecorder") Then
                            X = "Name: Voice Notes"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.weather") Then
                            X = "Name: Weather"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.youtube") Then
                            X = "Name: YouTube"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("NFB") Then
                            X = "Name: NFB"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("com.adobe.AdobeReader") Then
                            X = "Name: Adobe Reader"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("com.facebookforplaybook") Then
                            X = "Name: Facebook for PlayBook"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("com.kobobooks") Then
                            X = "Name: Kobo Books"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("slacker.radio") Then
                            X = "Name: Slacker Radio"
                            PBDeviceInfoBox.AppendText(X & vbCrLf)
                        ElseIf X.Contains("sys.scrapbook") Then
                        ElseIf X.Contains("source::websl") Then
                            X = "Source: System Software"
                            PBDeviceInfoBox.AppendText(X & vbCrLf & vbCrLf)
                        End If
                    End If
                Next
                ToolStripStatusLabel1.Text = "Displaying App Read for PlayBook @ IP: " & PlayBookIP.Text & "."
            Catch V As Exception
                ToolStripStatusLabel1.Text = "Unable to connect to PlayBook successfully."
            End Try
        Loop Until myProcess.StandardOutput.EndOfStream
        myProcess.Close()
        PlayBookProgressBar.Style = ProgressBarStyle.Blocks
    End Sub
    Private Sub BarPackager_Click(sender As System.Object, e As System.EventArgs) Handles LaunchBarPackager.Click
        Dim Obj As New APKConverter
        Obj.PlayBookIPText = PlayBookIP.Text
        Obj.PlayBookPassText = PlayBookPassword.Text
        Obj.ShowDialog()
    End Sub

    Private Sub SavePBRead_Click(sender As System.Object, e As System.EventArgs) Handles SavePBDeviceInfo.Click
        ToolStripStatusLabel1.Text = "Saving PlayBook read to PlayBookDeviceInfo.txt.txt..."
        If My.Computer.FileSystem.FileExists("PlayBookDeviceInfo.txt") Then
            My.Computer.FileSystem.DeleteFile("PlayBookDeviceInfo.txt")
        Else
        End If
        Dim SystemRead As New StreamWriter("PlayBookDeviceInfo.txt")
        ' Read the standard output of the spawned process.
        SystemRead.WriteLine("PlayBook Device Info")
        SystemRead.WriteLine("----------------------------")
        For Each myString In PBDeviceInfoBox.Lines
            SystemRead.WriteLine(myString)
        Next
        ToolStripStatusLabel1.Text = "PlayBook Device Info saved to PlayBookDeviceInfo.txt"
        MsgBox("PlayBook Device Info saved to PlayBookDeviceInfo.txt")
        SystemRead.Close()
    End Sub
    Private Sub SaveAppRead_Click(sender As System.Object, e As System.EventArgs) Handles SaveAppRead.Click
        ToolStripStatusLabel1.Text = "Saving PlayBook App Read to PlayBookAppRead.txt..."
        If My.Computer.FileSystem.FileExists("PlayBookAppRead.txt") Then
            My.Computer.FileSystem.DeleteFile("PlayBookAppRead.txt")
        Else
        End If
        Dim SystemRead As New StreamWriter("PlayBookAppRead.txt")
        ' Read the standard output of the spawned process.
        SystemRead.WriteLine("PlayBook App Read")
        SystemRead.WriteLine("----------------------------")
        For Each myString In PBDeviceInfoBox.Lines
            SystemRead.WriteLine(myString)
        Next
        ToolStripStatusLabel1.Text = "PlayBook App Read saved to PlayBookAppRead.txt"
        MsgBox("PlayBook App Read saved to PlayBookAppRead.txt")
        SystemRead.Close()
    End Sub
    Private Sub LaunchPBDrive_Click(sender As System.Object, e As System.EventArgs) Handles LaunchPBDrive.Click
        If My.Computer.FileSystem.DirectoryExists("Z:\") Then
            Shell("explorer Z:\", AppWinStyle.NormalFocus)
        Else
            Shell("explorer \\" & PlayBookIP.Text & "\", AppWinStyle.NormalFocus)
        End If
    End Sub
    Private Sub DisplayEventLog_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles DisplayEventLog.Click
        If DisplayEventLog.Checked = True Then
            EventLogs.Show()
        ElseIf DisplayEventLog.Checked = False Then
            EventLogs.Hide()
        End If
    End Sub
    Sub JavaCheck()
        If My.Computer.FileSystem.DirectoryExists("C:\Program Files (x86)\Java\") Then
        ElseIf My.Computer.FileSystem.DirectoryExists("C:\Program Files\Java\") Then
        ElseIf My.Computer.FileSystem.DirectoryExists("D:\Program Files (x86)\Java\") Then
        ElseIf My.Computer.FileSystem.DirectoryExists("D:\Program Files\Java\") Then
        ElseIf My.Computer.FileSystem.DirectoryExists("E:\Program Files\Java\") Then
        ElseIf My.Computer.FileSystem.DirectoryExists("E:\Program Files (x86)\Java\") Then
        ElseIf My.Computer.FileSystem.DirectoryExists("F:\Program Files\Java\") Then
        Else
            'MsgBox("Java Runtime Environment not detected. Functions on the PlayBook tab may not work correctly.")
        End If
    End Sub
    Private Sub CheckAllBarsApks_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles CheckAllBarsApks.LinkClicked
        If CheckAllBarsApks.Text = "Check All" Then
            CheckAllBarsApks.Text = "Un-Check All"
            For i As Integer = 0 To PBBox.Items.Count - 1
                PBBox.SetItemChecked(i, True)
            Next
        ElseIf CheckAllBarsApks.Text = "Un-Check All" Then
            CheckAllBarsApks.Text = "Check All"
            For i As Integer = 0 To PBBox.Items.Count - 1
                PBBox.SetItemChecked(i, False)
            Next
        End If
    End Sub
    Private Sub CreateShortcut_Click(sender As System.Object, e As System.EventArgs) Handles CreateShortcut.Click
        If CreateShortcut.Text = "Remove Desktop Shortcut" Then
            My.Computer.FileSystem.DeleteFile(DesktopFolder & "\BBHTool.lnk")
            CreateShortcut.Text = "Create Desktop Shortcut"
        Else
            'Dim WshShell As WshShellClass = New WshShellClass
            'Dim MyShortcut As IWshRuntimeLibrary.IWshShortcut
            ' The shortcut will be created on the desktop
            'MyShortcut = CType(WshShell.CreateShortcut(DesktopFolder & "\BBHTool.lnk"), IWshRuntimeLibrary.IWshShortcut)
            'MyShortcut.TargetPath = Application.StartupPath & "\BBHTool.exe" 'Specify target app full path
            'MyShortcut.Save()
            'CreateShortcut.Text = "Remove Desktop Shortcut"
        End If
    End Sub
    Private Sub Latin_Click(sender As System.Object, e As System.EventArgs) Handles Latin.Click
        Dim msg1 = "This is the default BlackBerry font. Removal may result in undesired effecs. Are you sure you would like to remove the Latin Truetype font?"
        Dim title1 = "Remove Default Font?"
        Dim style1 = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or _
                    MsgBoxStyle.Critical
        ' Display the message box and save the response, Yes or No.
        Dim response1 = MsgBox(msg1, style1, title1)
        If response1 = MsgBoxResult.Yes Then
            Latin.Checked = True
            ToolStripStatusLabel1.Text = "Latin Truetype font has been selected."
        Else
            Latin.Checked = False
            ToolStripStatusLabel1.Text = "Latin Truetype font has not been selected."
        End If
    End Sub
    Private Sub HideInfoBox_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles HideInfoBox.LinkClicked
        PBDeviceInfoBox.Visible = False
        HideInfoBox.Visible = False
    End Sub

    Private Sub PlayBookWarning_Click(sender As System.Object, e As System.EventArgs) Handles PlayBookWarning.Click
        HowTo.HowToTabs.SelectedIndex = 6
        HowTo.Show()
    End Sub
    Private Sub DeleteVendor_Click(sender As System.Object, e As System.EventArgs) Handles DeleteVendor.Click
        Call DeleteVendorXml()
    End Sub
    Private Sub SaveShrunkFileList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveShrunkFileList.Click
        If SaveShrunkFileList.Checked = True Then
            My.Settings.SaveShrunkFileList = True
        Else
            My.Settings.SaveShrunkFileList = False
        End If
    End Sub
    Private Sub SeperateALXFilesCODFiles_Click(sender As Object, e As EventArgs) Handles SeperateALXFilesCODFiles.Click
        If SeperateALXFilesCODFiles.Checked = True Then
            My.Settings.SeperateALXFilesCODFiles = True
        Else
            My.Settings.SeperateALXFilesCODFiles = False
        End If
    End Sub
    Private Sub RefreshShortcut_Click(sender As Object, e As EventArgs) Handles RefreshShortcut.Click
        Call RefreshOSFolders()
    End Sub
    Public Sub DownloadsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OSDownloadsToolStripMenuItem.Click
        ToolStripStatusLabel1.Text = "Showing the theiexplorers.com OS Downloads..."
        System.Diagnostics.Process.Start("http://www.theiexplorers.com/bb/OSs/")
        ToolStripStatusLabel1.Text = WelcomeText
    End Sub

    Private Sub ShrinkBB10_Click(sender As Object, e As EventArgs) Handles ShrinkBB10.Click
        Call ShrinkInitiate()
    End Sub
End Class

Public Class NumberOnlyTextbox
    Inherits TextBox
    Protected m_ctrl As TextBox
    Protected m_throw As KeyEventHandler
    Protected m_AllowDecimalsPoint As Boolean

    Public Sub New(ByRef ctrl As TextBox, ByRef AllowDecimalsPoint As Boolean, ByRef AllowMinus As Boolean, ByRef throwb As KeyEventHandler)
        ' Pass a reference to the textbox
        m_ctrl = ctrl
        ' Are Decimal Points allowed through
        m_AllowDecimalsPoint = AllowDecimalsPoint
        ' Pass a reference to the delegate. (This is AddressOf a sub which further process the keypress.)
        m_throw = throwb
        ' Add an handler for the keydown event
        AddHandler m_ctrl.KeyDown, AddressOf M_OnKeyDown
    End Sub
    Protected Sub M_OnKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        e.SuppressKeyPress = False
        Select Case e.KeyCode
            ' Allow Digit keys through.
            Case Keys.Decimal, Keys.OemPeriod, Keys.Control, Keys.V

                If m_AllowDecimalsPoint Then
                    If m_throw IsNot Nothing Then m_throw.Invoke(sender, e)
                End If
            Case Keys.D0 To Keys.D9, Keys.NumPad0 To Keys.NumPad9
                If m_throw IsNot Nothing Then m_throw.Invoke(sender, e)
            Case Keys.Delete, Keys.Back, Keys.Enter, Keys.Return, Keys.Left, Keys.Right, Keys.Tab
                ' Allow "STANDARD" editting keys
                If m_throw IsNot Nothing Then m_throw.Invoke(sender, e)
            Case Else
                ' Supress all other key.
                e.Handled = True
                e.SuppressKeyPress = True
        End Select
    End Sub
    Protected Overrides Sub Finalize()
        ' Remember to remove the handle
        RemoveHandler m_ctrl.KeyDown, AddressOf M_OnKeyDown
        MyBase.Finalize()
    End Sub
End Class

