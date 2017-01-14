Public Class PassProtect    
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "BBH" & "VIP" & "Only" Then
            My.Settings.Authenticated = 1
            MsgBox("Correct Password. Enjoy Build-A-Hybrid!")
            Form1.Show()
            Me.Close()
        Else
            MsgBox("Incorrect Password. Become A VIP to use Build-A-Hybrid!")
            My.Settings.Authenticated = 0
        End If
    End Sub
    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Button1_Click(Me, EventArgs.Empty)
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Hide()
        Form1.TabControl1.SelectedIndex = 0
        Form1.Show()
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        System.Diagnostics.Process.Start("http://www.bbhybrids.net/payments.php")
    End Sub
    Private Sub Form_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If My.Settings.Authenticated = 1 Then
        ElseIf My.Settings.Authenticated = 0 Then
            Me.Hide()
            Form1.TabControl1.SelectedIndex = 0
            Form1.Show()
        End If

    End Sub
End Class