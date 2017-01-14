Public Class APKConverter
    Dim ConvertInfo As String
    Dim ConvertInfo2 As String
    Dim WelcomeText As String = "Welcome to BBH Tool by lyricidal / @theiexplorers"
    Dim SignCommand As String
    Dim InstallCommand As String
    Private Results As String
    Private Delegate Sub delUpdate()
    Private Converted As New delUpdate(AddressOf UpdateText)
    Private PlayBookIP As String
    Private PlayBookPass As String
    Public Property [PlayBookIPText]() As String
        Get
            Return PlayBookIP
        End Get
        Set(ByVal Value As String)
            PlayBookIP = Value
        End Set
    End Property
    Public Property [PlayBookPassText]() As String
        Get
            Return PlayBookPass
        End Get
        Set(ByVal Value As String)
            PlayBookPass = Value
        End Set
    End Property
    Private Sub BarPackager_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If My.Computer.FileSystem.DirectoryExists("C:\Program Files (x86)\Android\android-sdk") Then
            SDKFolder.Text = "C:\Program Files (x86)\Android\android-sdk"
        ElseIf My.Computer.FileSystem.DirectoryExists("C:\Program Files\Android\android-sdk") Then
            SDKFolder.Text = "C:\Program Files\Android\android-sdk"
        ElseIf RememberAndroidSDK.Checked = True Then
            SDKFolder.Text = My.Settings.AndroidSDK
        End If
        If RememberConversionFolder.Checked = True Then
            SaveTo.Text = My.Settings.APKSaveTo
        End If
        If SignBAR.Checked = True Then
            SigningInfo.Enabled = True
            ConvertIt.Text = "Convert/Sign It!"
        Else
            ConvertIt.Text = "Convert It!"
            SigningInfo.Enabled = False
        End If
        If RememberSigningInfo.Checked = True Then
            P12file.Text = My.Settings.KeystoreFile
            KeyStorePass.Text = My.Settings.KeystorePass
            CSKPass.Text = My.Settings.CSKPass
        End If
    End Sub
    Private Sub DisplayEventLog_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles DisplayEventLog.Click
        If DisplayEventLog.Checked = True Then
            EventLogs.Show()
        ElseIf DisplayEventLog.Checked = False Then
            EventLogs.Hide()
        End If
    End Sub
    Private Sub ConvertIt_Click(sender As System.Object, e As System.EventArgs) Handles ConvertIt.Click
        If SDKFolder.Text = "Browse for Android SDK folder..." Then
            MsgBox("Unable to convert file(s) as there is no Android SDK selected.")
        ElseIf SaveTo.Text = "Browse for Save To folder..." Then
            MsgBox("Unable to convert file(s) as some there is no folder to save to.")
        ElseIf SignBAR.Checked = True Then
            If P12file.Text = "" Then
                MsgBox("Unable to sign file(s) as some signing info is missing.")
            ElseIf KeyStorePass.Text = "" Then
                MsgBox("Unable to sign file(s) as some signing info is missing.")
            ElseIf CSKPass.Text = "" Then
                MsgBox("Unable to sign file(s) as some signing info is missing.")
            Else
                Dim CMDThread As New Threading.Thread(AddressOf ConvertFile)
                CMDThread.Start()
            End If
        ElseIf My.Computer.FileSystem.DirectoryExists(SaveTo.Text) Then
            Dim CMDThread As New Threading.Thread(AddressOf ConvertFile)
            CMDThread.Start()
        Else
            My.Computer.FileSystem.CreateDirectory(SaveTo.Text)
            Dim CMDThread As New Threading.Thread(AddressOf ConvertFile)
            CMDThread.Start()
        End If
    End Sub
    Sub ConvertFile()
        Dim APKfile As String
        Dim ConvertedBarFile As String
        If FileBox.CheckedItems.Count = 0 Then
            MsgBox("No files selected to be converted.")
        Else
            APKfile = ""
            ConvertedBarFile = ""
            ConvertInfo = "java -Xmx512M -cp ""lib\BARPackager.jar;lib\Apk2Bar.jar"" net.rim.tools.apk2bar.Apk2Bar "
            ConvertInfo2 = """" & SDKFolder.Text & """" & " -t " & """" & SaveTo.Text & """" & ""
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
            For Each APK As String In FileBox.CheckedItems
                If APK.Contains(".apk") Then
                    SW.WriteLine(ConvertInfo & """" & APK & """ " & ConvertInfo2) 'the command you wish to run.....
                    '//MsgBox(ConvertInfo & """" & APK & """ " & ConvertInfo2) 'the command you wish to run.....
                    ToolStripStatusLabel1.Text = "Converting APK(s)..."
                Else
                    MsgBox("This is not an .apk file. Please select a valid .apk file to convert.")
                End If
            Next
            SW.WriteLine("exit") 'exits command prompt window
            Results = SR.ReadToEnd 'returns results of the command window
            SW.Close()
            SR.Close()
            If SignBAR.Checked = True Then
                If FileBox.CheckedItems.Count = 1 Then
                    For Each File As String In FileBox.CheckedItems
                        ConvertedFile.Text = File.ToString()
                    Next
                    For Each X As String In ConvertedFile.Text.Split("\")
                        If X.Contains(".apk") Then
                            X = X.Replace(".apk", ".bar")
                            ConvertedBarFile = SaveTo.Text & "\" & X
                        End If
                    Next
                    Call SignBars(ConvertedBarFile)
                Else
                    UnparsedInstallFilesBox.Clear()
                    For Each File As String In FileBox.CheckedItems
                        UnparsedInstallFilesBox.AppendText(File.ToString() & vbCrLf)
                    Next
                    For Each ParseLine As String In UnparsedInstallFilesBox.Lines
                        For Each X As String In ParseLine.Split("\")
                            If X.Contains(".apk") Then
                                X = X.Replace(".apk", ".bar")
                                ConvertedBarFile = SaveTo.Text & "\" & X
                                ParsedInstallFiles.AppendText(ConvertedBarFile & vbCrLf)
                            End If
                        Next
                    Next
                    ConvertedBarFile = ""
                    Call SignBars(ConvertedBarFile)
                End If
            Else
                'invokes Finished delegate, which updates textbox with the results text
                Invoke(Converted)
            End If
        End If
    End Sub
    Sub SignBars(ByVal ConvertedFile As String)
        SignCommand = ""
        InstallCommand = ""
        If FileBox.CheckedItems.Count = 1 Then
            SignCommand = "java -cp ""%~dp0\..\lib\EccpressoAll.jar;%~dp0\..\lib\EccpressoJDK15ECC.jar;%~dp0\..\lib\BarSigner.jar;%~dp0\..\lib\BarPackager.jar;%~dp0\..\lib\KeyTool.jar""  net.rim.device.codesigning.barsigner.BarSigner  -keystore """ & P12file.Text & """ -storepass " & KeyStorePass.Text & " -cskpass " & CSKPass.Text & " """ & ConvertedFile & """"
        Else
            SignCommand = "java -Xmx512M -jar lib\BatchBARSigner.jar """ & SaveTo.Text & """ """ & P12file.Text & """ " & KeyStorePass.Text & " " & CSKPass.Text
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
        'PlayBookProgressBar.MarqueeAnimationSpeed = 20
        Dim SR As System.IO.StreamReader = myprocess.StandardOutput
        Dim SW As System.IO.StreamWriter = myprocess.StandardInput
        SW.WriteLine("cd " & My.Computer.FileSystem.CurrentDirectory)
        SW.WriteLine(SignCommand) 'the command you wish to run.....
        ToolStripStatusLabel1.Text = "Signing BAR(s)..."
        If InstallConverted.Checked = True Then
            If FileBox.CheckedItems.Count = 1 Then
                InstallCommand = "java -Xmx512M -jar lib\BarDeploy.jar -installApp -device " & PlayBookIP & " -password " & PlayBookPass & " """ & ConvertedFile & """"
                ToolStripStatusLabel1.Text = "Installing BAR..."
                SW.WriteLine(InstallCommand)
            Else
                ToolStripStatusLabel1.Text = "Installing BAR(s)..."
                For Each ConvertedFiles As String In ParsedInstallFiles.Lines
                    If ConvertedFiles = "" Then
                    Else
                        InstallCommand = "java -Xmx512M -jar lib\BarDeploy.jar -installApp -device " & PlayBookIP & " -password " & PlayBookPass & " """ & ConvertedFiles & """"
                        SW.WriteLine(InstallCommand)
                    End If
                Next
            End If
        Else
        End If
        SW.WriteLine("exit") 'exits command prompt window
        Results = SR.ReadToEnd 'returns results of the command window
        SW.Close()
        SR.Close()
        'invokes Finished delegate, which updates textbox with the results text
        Invoke(Converted)
    End Sub
    Private Sub UpdateText()
        'Results = Results.Replace("Copyright (c) 2009 Microsoft Corporation.  All rights reserved.", "")
        'Results = Results.Replace(My.Computer.FileSystem.CurrentDirectory & ">", "")
        'Results = Results.Replace("cd " & My.Computer.FileSystem.CurrentDirectory, "")
        txtResults.AppendText(Results)
        EventLogs.EventLogBox.AppendText(Results)
        UseWaitCursor = False
        PlayBookProgressBar.MarqueeAnimationSpeed = 0
        If SignBAR.Checked = True Then
            If InstallConverted.Checked = True Then
                If Results.Contains("Error: Device is not in the Development Mode. Switch to Development Mode from Security settings on the device.") Then
                    MsgBox("Error: Device is not in the Development Mode. Switch to Development Mode from Security settings on the device.")
                ElseIf Results.Contains("success") Then
                    ToolStripStatusLabel1.Text = "Selected file(s) have been converted, signed, and installed to the connected device. Enjoy!"
                    MsgBox("Selected files(s) have been converted, signed, and installed to the connected device. Enjoy!")
                End If
            Else
                If Results.Contains("Failed#: 0") Then
                    ToolStripStatusLabel1.Text = "Selected .APK(s) have all been successfully converted and signed to .BAR. You may now install them to your PlayBook."
                    MsgBox("Selected .APK(s) have all been successfully converted and signed to .BAR. You may now install them to your PlayBook.")
                ElseIf Results.Contains("Error") Then
                    ToolStripStatusLabel1.Text = "Selected .APK(s) have NOT all been successfully converted .BAR and signed. Please try again."
                    MsgBox("Selected .APK(s) have NOT all been successfully converted .BAR and signed. Please try again.")
                ElseIf Results.Contains("Info: Bar signed.") Then
                    ToolStripStatusLabel1.Text = "Selected .APK(s) have all been successfully converted and signed to .BAR. You may now install them to your PlayBook."
                    MsgBox("Selected .APK(s) have all been successfully converted and signed to .BAR. You may now install them to your PlayBook.")
                Else
                    ToolStripStatusLabel1.Text = "Selected .APK(s) have NOT all been successfully converted .BAR and signed. Please try again."
                    MsgBox("Selected .APK(s) have NOT all been successfully converted .BAR and signed. Please try again.")
                End If
            End If
        ElseIf SignBAR.Checked = False Then
            If Results.Contains("failed: 0") Then
                ToolStripStatusLabel1.Text = "Selected .APK(s) have all been successfully converted to .BAR. Now they must be signed to install to PlayBook."
                MsgBox("Selected .APK(s) have all been successfully converted to .BAR. Now they must be signed to install to PlayBook.")
            Else
                ToolStripStatusLabel1.Text = "Selected .APKs(s) have not all been successfully converted to .BAR. Please try again."
                MsgBox("Selected .APKs(s) have not all been successfully converted to .BAR. Please try again.")
            End If
        End If
    End Sub
    Private Sub BrowseSDK_Click(sender As System.Object, e As System.EventArgs) Handles BrowseSDK.Click
        ToolStripStatusLabel1.Text = "Browsing... (Select the Android SDK folder)"
        If My.Computer.FileSystem.DirectoryExists("C:\Program Files (x86)\Android\android-sdk") Then
            With FolderBrowserDialog1
                .SelectedPath = "C:\Program Files (x86)\Android\android-sdk"
                .ShowNewFolderButton = False
                .Description = "Navigate to the Blackberry Tablet SDK folder:"
            End With
        Else
            With FolderBrowserDialog1
                .SelectedPath = "C:\Program Files\Android\android-sdk"
                .ShowNewFolderButton = False
                .Description = "Navigate to the Android SDK folder:"
            End With
        End If
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            SDKFolder.Text = FolderBrowserDialog1.SelectedPath
        Else
            MsgBox("You must have the Android SDK installed to convert .APK to .BAR.")
        End If
        ToolStripStatusLabel1.Text = "Android SDK Folder selected."
    End Sub
    Private Sub BrowseSaveTo_Click(sender As System.Object, e As System.EventArgs) Handles BrowseSaveTo.Click
        ToolStripStatusLabel1.Text = "Browsing... (Select the folder to save your .bar to)"
        With FolderBrowserDialog1
            .SelectedPath = My.Computer.FileSystem.CurrentDirectory
            .ShowNewFolderButton = False
            .Description = "Select the folder to save your .bar to:"
            .ShowNewFolderButton = True
        End With
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            SaveTo.Text = FolderBrowserDialog1.SelectedPath
            ToolStripStatusLabel1.Text = "Save To Folder selected."
        Else
            SaveTo.Text = My.Computer.FileSystem.CurrentDirectory
            ToolStripStatusLabel1.Text = "No Save To Folder selected. BBHTool folder has been chosen by default."
        End If
    End Sub
    Private Sub BrowseP12_Click(sender As System.Object, e As System.EventArgs) Handles BrowseP12.Click
        ToolStripStatusLabel1.Text = "Browsing... (Select your .p12 files for signing)"
        ' Create an instance of the open file dialog box.
        ' Set filter options and filter index.
        OpenFileDialog1.Filter = "Keystore File (*.p12)|*.p12"
        OpenFileDialog1.FilterIndex = 1
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Multiselect = False
        ' Call the ShowDialog method to show the dialogbox.
        ' Process input if the user clicked OK.
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim file As String
            For Each file In OpenFileDialog1.FileNames
                P12file.Text = file
            Next
            ToolStripStatusLabel1.Text = "Keystore .p12 file selected."
        Else
            ToolStripStatusLabel1.Text = "You did not choose a .p12 file. Please choose one to sign."
            MsgBox("You did not choose a .p12 file. Please choose one to sign.")
        End If
    End Sub
    Private Sub Browse_Click(sender As System.Object, e As System.EventArgs) Handles Browse.Click
        DragAPK.Visible = False
        ToolStripStatusLabel1.Text = "Browsing... (Select your .apk files to convert)"
        ' Create an instance of the open file dialog box.
        ' Set filter options and filter index.
        OpenFileDialog1.Filter = "BlackBerry PlayBook Files (*.bar)|*.bar|Android Files (*.apk)|*.apk|Android and PlayBook Files (*.apk,*.bar)|*.apk;*.bar"
        OpenFileDialog1.FilterIndex = 1
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Multiselect = True
        ' Call the ShowDialog method to show the dialogbox.
        ' Process input if the user clicked OK.
        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            ToolStripStatusLabel1.Text = "Adding file(s)..."
            Dim file As String
            'For Each file In OpenFileDialog1.SafeFileNames
            For Each file In OpenFileDialog1.FileNames
                FileBox.Items.Add(file)
            Next
            'JavaLoaderProgress.PerformStep()
        Else
            ToolStripStatusLabel1.Text = "You did not choose any .APK/.BAR(s). Please choose some .APK/.BAR(s) to add."
            MsgBox("You did not choose any .APK/.BAR(s). Please choose some .APK/.BAR(s) to add.")
        End If
        FilesGroupBox.Text = "Files (" & FileBox.Items.Count & "):"
        ToolStripStatusLabel1.Text = WelcomeText
    End Sub
    Private Sub ClearAPKs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearAPKs.Click
        FilesGroupBox.Text = "Files:"
        FileBox.Items.Clear()
        ParsedInstallFiles.Clear()
        UnparsedInstallFilesBox.Clear()
        DragAPK.Visible = True
        CheckAllBarsApks.Text = "Check All"
        PlayBookProgressBar.MarqueeAnimationSpeed = 0
        UseWaitCursor = False
    End Sub

    Public Sub FileBoxDragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles FileBox.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub
    Public Sub FileBoxDragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles FileBox.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim Files() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
            'Determine whether the file is a .bar or not
            Dim fileDetail As New System.IO.FileInfo(Files(0))
            If fileDetail.Extension = ".apk" Then
                'Set the cursors etc for file copying
                e.Effect = DragDropEffects.All
                Dim MyFiles() As String
                Dim i As Integer
                'Dim infoReader As System.IO.FileInfo
                ' Assign the files to an array.
                MyFiles = e.Data.GetData(DataFormats.FileDrop)
                ' Loop through the array and add the files to the list.
                DragAPK.Hide()
                For i = 0 To MyFiles.Length - 1
                    Dim APKFileName As String = IO.Path.GetFileName(MyFiles(i))
                    FileBox.Items.Add(MyFiles(i))
                    ToolStripStatusLabel1.Text = "Adding .APKs..."
                    FilesGroupBox.Text = "Files (" & FileBox.Items.Count & "):"
                Next
            ElseIf fileDetail.Extension = ".bar" Then
                'Set the cursors etc for file copying
                e.Effect = DragDropEffects.All
                Dim MyFiles() As String
                Dim i As Integer
                'Dim infoReader As System.IO.FileInfo
                ' Assign the files to an array.
                MyFiles = e.Data.GetData(DataFormats.FileDrop)
                ' Loop through the array and add the files to the list.
                DragAPK.Hide()
                For i = 0 To MyFiles.Length - 1
                    Dim BarFileName As String = IO.Path.GetFileName(MyFiles(i))
                    FileBox.Items.Add(MyFiles(i))
                    ToolStripStatusLabel1.Text = "Adding .BARs..."
                    FilesGroupBox.Text = "Files (" & FileBox.Items.Count & "):"
                Next
            Else
                MsgBox("Only .APK/.BAR files can be converted/signed here.")
            End If
        End If
        ToolStripStatusLabel1.Text = WelcomeText
    End Sub
    Public Sub DragAPKDragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DragAPK.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub
    Public Sub DragAPkDragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles DragAPK.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim Files() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
            'Determine whether the file is a .bar or not
            Dim fileDetail As New System.IO.FileInfo(Files(0))
            If fileDetail.Extension = ".apk" Then
                'Set the cursors etc for file copying
                e.Effect = DragDropEffects.All
                Dim MyFiles() As String
                Dim i As Integer
                'Dim infoReader As System.IO.FileInfo
                ' Assign the files to an array.
                MyFiles = e.Data.GetData(DataFormats.FileDrop)
                ' Loop through the array and add the files to the list.
                DragAPK.Hide()
                For i = 0 To MyFiles.Length - 1
                    Dim APKFileName As String = IO.Path.GetFileName(MyFiles(i))
                    FileBox.Items.Add(MyFiles(i))
                    ToolStripStatusLabel1.Text = "Adding .APKs..."
                    FilesGroupBox.Text = "Files (" & FileBox.Items.Count & "):"
                Next
            ElseIf fileDetail.Extension = ".bar" Then
                'Set the cursors etc for file copying
                e.Effect = DragDropEffects.All
                Dim MyFiles() As String
                Dim i As Integer
                'Dim infoReader As System.IO.FileInfo
                ' Assign the files to an array.
                MyFiles = e.Data.GetData(DataFormats.FileDrop)
                ' Loop through the array and add the files to the list.
                DragAPK.Hide()
                For i = 0 To MyFiles.Length - 1
                    Dim BarFileName As String = IO.Path.GetFileName(MyFiles(i))
                    FileBox.Items.Add(MyFiles(i))
                    ToolStripStatusLabel1.Text = "Adding .BARs..."
                    FilesGroupBox.Text = "Files (" & FileBox.Items.Count & "):"
                Next
            Else
                MsgBox("Only .APK/.BAR files can be converted/signed here.")
            End If
        End If
        ToolStripStatusLabel1.Text = WelcomeText
    End Sub

    Private Sub SignBAR_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles SignBAR.Click
        If SignBAR.Checked = True Then
            ConvertIt.Text = "Convert/Sign It!"
            SigningInfo.Enabled = True
            'InstallConverted.Enabled = True
        Else
            ConvertIt.Text = "Convert It!"
            SigningInfo.Enabled = False
            'InstallConverted.Enabled = False
        End If
    End Sub

    Private Sub CheckAllBarsApks_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles CheckAllBarsApks.LinkClicked
        If CheckAllBarsApks.Text = "Check All" Then
            CheckAllBarsApks.Text = "Un-Check All"
            For i As Integer = 0 To FileBox.Items.Count - 1
                FileBox.SetItemChecked(i, True)
            Next
        ElseIf CheckAllBarsApks.Text = "Un-Check All" Then
            CheckAllBarsApks.Text = "Check All"
            For i As Integer = 0 To FileBox.Items.Count - 1
                FileBox.SetItemChecked(i, False)
            Next
        End If
    End Sub
    Private Sub RememberAndroidSDK_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RememberAndroidSDK.Click
        If RememberAndroidSDK.Checked = True Then
            My.Settings.AndroidSDK = SDKFolder.Text
        ElseIf RememberAndroidSDK.Checked = False Then
            My.Settings.AndroidSDK = ""
        End If
    End Sub
    Private Sub RememberConversionFolder_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RememberConversionFolder.Click
        If RememberConversionFolder.Checked = True Then
            My.Settings.APKSaveTo = SaveTo.Text
        ElseIf RememberConversionFolder.Checked = False Then
            My.Settings.APKSaveTo = ""
        End If
    End Sub
    Private Sub RememberSigningInfo_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RememberSigningInfo.Click
        If RememberSigningInfo.Checked = True Then
            My.Settings.KeystoreFile = P12file.Text
            My.Settings.KeystorePass = KeyStorePass.Text
            My.Settings.CSKPass = CSKPass.Text
        ElseIf RememberSigningInfo.Checked = False Then
            My.Settings.KeystoreFile = ""
            My.Settings.KeystorePass = ""
            My.Settings.CSKPass = ""
        End If
    End Sub
    Private Sub GetKeys_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles GetKeys.LinkClicked
        System.Diagnostics.Process.Start("https://www.blackberry.com/SignedKeys/")
    End Sub

    Private Sub DownloadSDK_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles DownloadSDK.LinkClicked
        System.Diagnostics.Process.Start("http://dl.google.com/android/installer_r17-windows.exe")
    End Sub
    Private Sub InstallConverted_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles InstallConverted.Click
        If InstallConverted.Checked = True Then
            If PlayBookIP = "192.168.0.1" Then
                Dim strName$
                strName = InputBox("Please enter your PlayBook's IP below to complete installation after completion.", "")
                If strName = "" Then
                    ToolStripStatusLabel1.Text = "You must enter your PlayBook's IP to continue the installation after completion process."
                    MsgBox("You must enter your PlayBook's IP to continue the installation after completion process.")
                Else
                    ToolStripStatusLabel1.Text = "PlayBook IP accepted."
                    PlayBookIP = strName
                End If
            ElseIf PlayBookPass = "" Then
                Dim strName$
                strName = InputBox("Please enter your PlayBook's password below to complete installation after completion.", "")
                If strName = "" Then
                    ToolStripStatusLabel1.Text = "You must enter your PlayBook's password to continue the installation after completion process."
                    MsgBox("You must enter your PlayBook's password to continue the installation after completion process.")
                Else
                    ToolStripStatusLabel1.Text = "PlayBook password accepted."
                    PlayBookPass = strName
                End If
            Else
            End If
        Else
        End If
    End Sub
End Class