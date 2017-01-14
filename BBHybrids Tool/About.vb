Public Class About
    
    Private Sub BBHybrids_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BBHybrids.Click
        System.Diagnostics.Process.Start("http://www.bbh-plus.net/")
    End Sub

    Private Sub About_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BBHybrids.Select()
        TextBox2.Text = TextBox2.Text + " " + My.Settings.VersionNumber
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        ChangesLog.ShowDialog()
    End Sub

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        System.Diagnostics.Process.Start("www.twitter.com/theiexplorers/")
    End Sub

    Private Sub LinkLabel3_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        System.Diagnostics.Process.Start("www.twitter.com/bbhplus/")
    End Sub

    Private Sub LinkLabel4_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        System.Diagnostics.Process.Start("www.theiexplorers.com/")
    End Sub
End Class