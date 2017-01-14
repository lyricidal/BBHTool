Imports System.IO

Public Class EventLogs
    Private Sub SaveTxt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveTxt.Click
        Dim FileName As String = "EventLog-" & Format(Now(), "mm_dd_yyyy") & ".txt"
        If My.Computer.FileSystem.FileExists(FileName) Then
            My.Computer.FileSystem.DeleteFile(FileName)
        Else
        End If
        Dim SystemRead As New StreamWriter(FileName)
        ' Read the standard output of the spawned process.
        SystemRead.WriteLine("Event Log")
        SystemRead.WriteLine("----------------------------")
        For Each myString In EventLogBox.Lines
            SystemRead.WriteLine(myString)
        Next
        MsgBox("Event Log saved to " & FileName)
        SystemRead.Close()
    End Sub

    Private Sub EventLogs_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        APKConverter.DisplayEventLog.Checked = False
        Form1.DisplayEventLog.Checked = False
    End Sub
    Private Sub ClearLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearLog.Click
        EventLogBox.Clear()
    End Sub
End Class