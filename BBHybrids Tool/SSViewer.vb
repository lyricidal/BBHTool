Imports System.IO

Public Class SSViewer
    Private Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Long)
    Private DevicePassword As String
    Dim ImageFileName As String
    Public Property [DevicePasswordText]() As String
        Get
            Return DevicePassword
        End Get
        Set(ByVal Value As String)
            DevicePassword = Value
        End Set
    End Property
    Public Property [FileNameText]() As String
        Get
            Return ImageFileName
        End Get
        Set(ByVal Value As String)
            ImageFileName = Value
        End Set
    End Property
    Private Sub TakeScreenshot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TakeScreenshot.Click
        UseWaitCursor = True
        If My.Computer.FileSystem.DirectoryExists("Screenshots") Then
        Else
            My.Computer.FileSystem.CreateDirectory("Screenshots")
        End If
        PictureBox1.Image.Dispose()
        ToolStripStatusLabel1.Text = "Taking screenshot of your Device's screen..."
        Dim x As Integer = Now.Millisecond
        ImageFileName = "Screenshot-" & Format(Now(), "mm_dd_yyyy_" & x) & ".bmp"
        Shell("""JavaLoader.exe""  -w" & DevicePassword & " screenshot Screenshots\" & ImageFileName, AppWinStyle.MinimizedNoFocus)
        ToolStripStatusLabel1.Text = "Screenshot saved to Screenshots\" & ImageFileName
        Sleep(5000)
        Dim img As Image = Image.FromFile(My.Computer.FileSystem.CurrentDirectory & "\Screenshots\" & ImageFileName)
        PictureBox1.Image = img
        MsgBox("Screenshot saved to Screenshots\" & ImageFileName)
        If UploadImage.Checked = True Then
            If UploadWorker.IsBusy Then Exit Sub
            UploadWorker.RunWorkerAsync()
        End If
        If TweetIt.Checked = True Then
            Call TweetLink()
        End If
        If CopyImage1.Checked = True Then
            Clipboard.Clear()
            Clipboard.SetImage(PictureBox1.Image)
        Else
        End If
        If AutoCopy.Checked = True Then
            Clipboard.Clear()
            Clipboard.SetText(ShrunkURL.Text)
        Else
        End If
        UseWaitCursor = False
    End Sub
    Private Sub UploadImage_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UploadImage.CheckedChanged
        If UploadImage.Checked = True Then
            TweetIt.Enabled = True
            ShortenLink.Enabled = True
            CopyImage1.Enabled = True
            AutoCopy.Enabled = True
        Else
            TweetIt.Enabled = False
            ShortenLink.Enabled = False
            CopyImage1.Enabled = False
            AutoCopy.Enabled = False
        End If
    End Sub
    Public Sub UploadWorker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles UploadWorker.DoWork
        UseWaitCursor = True
        UploadButton.Enabled = False
        ' Upload File
        ToolStripStatusLabel1.Text = "Uploading screenshot of your Device's screen..."
        Dim clsRequest As System.Net.FtpWebRequest = DirectCast(System.Net.WebRequest.Create("ftp://ftp.theiexplorers.com/" & ImageFileName), System.Net.FtpWebRequest)
        'insert ftp info below, plain text...
        clsRequest.Credentials = New System.Net.NetworkCredential("USER", "PASS")
        clsRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile
        ' read in file...
        Dim bFile() As Byte = System.IO.File.ReadAllBytes(My.Computer.FileSystem.CurrentDirectory & "\Screenshots\" & ImageFileName)
        ' upload file...
        Dim clsStream As System.IO.Stream = clsRequest.GetRequestStream()
        clsStream.Write(bFile, 0, bFile.Length)
        clsStream.Close()
        clsStream.Dispose()
        ToolStripStatusLabel1.Text = "Your Screenshot has been uploaded to http://www.theiexplorers.com/screenshots/" & ImageFileName

        ShrunkURL.Text = "http://www.theiexplorers.com/screenshots/" & ImageFileName
        If ShortenLink.Checked = True Then
            Call ShortenUrl()
            If TweetIt.Checked = True Then
                If ShrunkURL.Text = "" Then
                    MsgBox("There is no link to tweet! Please shrink a link before trying to tweet it.")
                Else
                    System.Diagnostics.Process.Start("http://twitter.com/intent/tweet?text=" & ShrunkURL.Text)
                    MsgBox("Your Screenshot has been uploaded to http://www.theiexplorers.com/screenshots/" & ImageFileName & ". Twitter will now open in your default browser to tweet the upload link.")
                End If
            Else
            End If
        Else
            MsgBox("Your Screenshot has been uploaded to http://www.theiexplorers.com/screenshots/" & ImageFileName)
        End If
        UseWaitCursor = False
        UploadButton.Enabled = True
    End Sub
    Sub AutoCopyLink()
        ToolStripStatusLabel1.Text = "Copying link to clipboard..."
        If AutoCopy.Checked = True Then
            Clipboard.Clear()
            Clipboard.SetText(ShrunkURL.Text)
        End If
        ToolStripStatusLabel1.Text = "Link copied to clipboard."
    End Sub
    Sub ShortenUrl()
        ToolStripStatusLabel1.Text = "Shortening screenshot upload link..."
        Dim inStream As StreamReader
        Dim webRequest As Net.WebRequest
        Dim webresponse As Net.WebResponse
        webRequest = Net.WebRequest.Create("http://bb-h.me/url/api.php?u=http://www.theiexplorers.com/screenshots/" & ImageFileName)
        webresponse = webRequest.GetResponse()
        inStream = New StreamReader(webresponse.GetResponseStream())
        ShrunkURL.Text = inStream.ReadToEnd()
        inStream.Close()
        ToolStripStatusLabel1.Text = "Upload link shortened."
    End Sub
    Sub TweetLink()
        ToolStripStatusLabel1.Text = "Tweeting upload link..."
        If ShrunkURL.Text.Contains("theiexplorers") Then
            Call ShortenUrl()
            System.Diagnostics.Process.Start("http://twitter.com/intent/tweet?text=" & ShrunkURL.Text)
        Else
            System.Diagnostics.Process.Start("http://twitter.com/intent/tweet?text=" & ShrunkURL.Text)
        End If
        ToolStripStatusLabel1.Text = "Upload link has been posted to Twitter, tweet away!"
    End Sub
    Private Sub SSViewer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        PictureBox1.Image.Dispose()
    End Sub
    Private Sub UploadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UploadButton.Click
        If UploadWorker.IsBusy Then Exit Sub
        UploadWorker.RunWorkerAsync()
        TweetButton.Enabled = True
    End Sub
    Private Sub TweetButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TweetButton.Click
        Call TweetLink()
    End Sub
    Private Sub CopyImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyImage.Click
        Clipboard.Clear()
        Clipboard.SetImage(PictureBox1.Image)
    End Sub
    Private Sub SSViewer_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        MsgBox("Screenshot saved to " & "\Screenshots\" & ImageFileName)
    End Sub
    Private Sub CopyImage1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CopyImage1.Click
        If CopyImage1.Checked = True Then
            AutoCopy.Enabled = False
        ElseIf CopyImage1.Checked = False Then
            AutoCopy.Enabled = True
        End If
    End Sub
    Private Sub AutoCopy_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles AutoCopy.CheckedChanged
        If AutoCopy.Checked = True Then
            CopyImage1.Checked = False
        ElseIf AutoCopy.Checked = False Then
            CopyImage1.Checked = True
        End If
    End Sub
End Class