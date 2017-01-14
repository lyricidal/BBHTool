Public Class FileSwapper
    Private Sub FileSwapper_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FileList.AllowDrop = True
        'Add OS Folders to list Start
        Dim fo As Object
        Dim fs As Object
        fs = CreateObject("Scripting.FileSystemObject")
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
        End If
        If My.Settings.LoaderDir = "" Then
        Else
            fo = fs.GetFolder(My.Settings.LoaderDir)
            For Each x In fo.SubFolders
                OSFolderBox.Items.Add(x.Name)
                OutputFolder.Items.Add(x.Name)
            Next
            OSFolderBox.Items.Add("Other...")
            OutputFolder.Items.Add("Other...")
        End If
        'Add OS Folders to list End
        OSFolderBox.Text = My.Settings.InputFolder
        OutputFolder.Text = My.Settings.OutputFolder
    End Sub
    Private Sub OSFolderBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OSFolderBox.SelectedIndexChanged
        If OSFolderBox.SelectedItem = "Other..." Then
            ToolStripStatusLabel1.Text = "Browsing... (Select the Input Folder for your .cods)"
            If My.Computer.FileSystem.DirectoryExists(My.Settings.LoaderDir) Then
                With FolderBrowserDialog1
                    .SelectedPath = My.Settings.LoaderDir
                    .ShowNewFolderButton = False
                    .Description = "Navigate to your Input folder (Not the Java or CDMA):"
                End With
            End If
            If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                OSFolderBox.Items.Add(FolderBrowserDialog1.SelectedPath & "\")
                OSFolderBox.SelectedItem = FolderBrowserDialog1.SelectedPath & "\"
                My.Settings.InputFolder = FolderBrowserDialog1.SelectedPath
                ToolStripStatusLabel1.Text = "Input Folder selected."
            Else
                MsgBox("Please select an Input Folder!")
                ToolStripStatusLabel1.Text = "Please select an Input Folder!"
            End If
        Else
            My.Settings.InputFolder = My.Settings.LoaderDir & OSFolderBox.Text
        End If
        My.Settings.Save()
    End Sub
    Private Sub OutputFolder_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutputFolder.SelectedIndexChanged
        If OutputFolder.SelectedItem = "Other..." Then
            ToolStripStatusLabel1.Text = "Browsing... (Select the Output Folder for your .cods)"
            If My.Computer.FileSystem.DirectoryExists(My.Settings.LoaderDir) Then
                With FolderBrowserDialog1
                    .SelectedPath = My.Settings.LoaderDir
                    .ShowNewFolderButton = False
                    .Description = "Navigate to your Output folder (Not the Java or CDMA):"
                End With
            End If
            If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                OutputFolder.Items.Add(FolderBrowserDialog1.SelectedPath & "\")
                OutputFolder.SelectedItem = FolderBrowserDialog1.SelectedPath & "\"
                My.Settings.OutputFolder = FolderBrowserDialog1.SelectedPath
                ToolStripStatusLabel1.Text = "Output Folder selected."
            Else
                MsgBox("Please select an Output Folder!")
                ToolStripStatusLabel1.Text = "Please select an Output Folder!"
            End If
        Else
            My.Settings.OutputFolder = My.Settings.LoaderDir & OSFolderBox.Text
        End If
        My.Settings.Save()
    End Sub
    Private Sub SwapFiles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SwapFiles.Click
        If FileList.Text = "" Then
            If SwapFullJava.Checked = False Then
                MsgBox("Please enter a list of files or check the Swap Full Java box.")
                ToolStripStatusLabel1.Text = "Please enter a list of files or check the Swap Full Java box."
            Else
                SwapperProgressBar.Minimum = 1
                ' Set the initial value of the ProgressBar.
                SwapperProgressBar.Value = 1
                ' Set the Step property to a value of 1 to represent each file being copied.
                SwapperProgressBar.Step = 1
            End If
        End If

        If SwapFullJava.Checked = True Then
            For Each X As String In My.Settings.InputFolder.Split("_")
                For i = 0 To 6
                    For j = 0 To 6
                        If X.Contains("P" & i & "." & j) Then
                            Dim Platform As String
                            Platform = X
                            If My.Settings.OutputFolder.Contains(Platform) Then
                                Dim msg1 = "Doing a Full Java Swap with Platforms that do not match may not work correctly. Please make sure to back up your device before attempting this, if at all! Are you sure you would like to do this?"
                                Dim title1 = "Full Java Swap"
                                Dim style1 = MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Critical
                                ' Display the message box and save the response, Yes or No.
                                Dim response1 = MsgBox(msg1, style1, title1)
                                If response1 = MsgBoxResult.Yes Then
                                    Call FullJavaSwap()
                                Else
                                    ToolStripStatusLabel1.Text = "Swap has been halted!"
                                End If
                            Else
                                Call FullJavaSwap()
                            End If
                        End If
                    Next
                Next
            Next
        Else
            ' Set Maximum to the total number of files to copy.
            SwapperProgressBar.Maximum = FileList.Lines.Count
            ToolStripStatusLabel1.Text = "Swapping your files..."
            Dim FileNameString As String
            For Each FileNameString In FileList.Lines
                If FileNameString.Contains(".cod") Then
                Else
                    FileNameString = FileNameString & ".cod"
                End If
                If My.Computer.FileSystem.FileExists(My.Settings.InputFolder & "\Java\" & FileNameString) Then
                    My.Computer.FileSystem.CopyFile(My.Settings.InputFolder & "\Java\" & FileNameString, My.Settings.OutputFolder & "\Java\" & FileNameString, True)
                Else
                End If
                SwapperProgressBar.PerformStep()
            Next
            ToolStripStatusLabel1.Text = "Your files have been swapped."
        End If
    End Sub
    Sub FullJavaSwap()
        Dim counter As System.Collections.ObjectModel.ReadOnlyCollection(Of String)
        counter = My.Computer.FileSystem.GetFiles(My.Settings.InputFolder & "\Java\")
        ' Set Maximum to the total number of files to copy.
        SwapperProgressBar.Maximum = CStr(counter.Count)
        ToolStripStatusLabel1.Text = "Swapping your files..."
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(My.Settings.InputFolder & "\Java\", Microsoft.VisualBasic.FileIO.SearchOption.SearchTopLevelOnly, "*.cod")
            Dim foundFileInfo As New System.IO.FileInfo(foundFile)
            FileList.ReadOnly = True
            'FileList.AppendText(foundFileInfo.Name & vbCrLf)
            My.Computer.FileSystem.CopyFile(foundFile, My.Settings.OutputFolder & "\Java\" & foundFileInfo.Name, True)
            SwapperProgressBar.PerformStep()
        Next
        ToolStripStatusLabel1.Text = "Your files have been swapped."
    End Sub
    Public Sub FileListDragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles FileList.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub
    Public Sub FileListDragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles FileList.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim Files() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
            'Determine whether the file is a .bar or not
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
                ToolStripStatusLabel1.Text = "Adding .cod files to list..."
                For i = 0 To MyFiles.Length
                    Dim CODFile As String = IO.Path.GetFileName(MyFiles(i))
                    If CODFile = "" Then
                    Else
                        FileList.AppendText(CODFile & vbCrLf)
                        ToolStripStatusLabel1.Text = "Files added to list."
                    End If
                Next
            Else
                MsgBox("Only .cod files can be added to the File Swapper list.")
                ToolStripStatusLabel1.Text = "Only .cod files can be added to the File Swapper list."
            End If
        End If
    End Sub

    Private Sub ClearFileList_Click(sender As System.Object, e As System.EventArgs) Handles ClearFileList.Click
        FileList.Clear()
        ToolStripStatusLabel1.Text = "File Swapper list cleared."
    End Sub
End Class