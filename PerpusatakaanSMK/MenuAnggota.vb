Public Class MenuAnggota
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Close()

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Login.username.Text = ""
        Login.password.Text = ""
        Login.txtCapt.Text = ""
        Login.Show()
        Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        DataBuku.readBuku("")
        DataBuku.Show()
        Hide()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        HistoriPeminjaman.readPeminjaman("")
        HistoriPeminjaman.Show()
        Hide()

    End Sub
End Class