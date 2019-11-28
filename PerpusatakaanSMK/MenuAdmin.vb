Imports System.Data.SqlClient
Public Class MenuAdmin
    Dim koneksi As New Connection
    Public username, password As String
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
        FormKategori.kosong()

        FormKategori.Show()
        FormKategori.readKategori("")
        Hide()

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        FormLokasi.kosong()

        FormLokasi.Show()
        FormLokasi.readLokasi("")
        Hide()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FormBuku.kosong()
        FormBuku.Show()
        FormBuku.readBuku("")
        Hide()

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        FormPetugas.kosong()

        FormPetugas.Show()
        FormPetugas.readPetugas("")
        Hide()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        FormAnggota.kosong()

        FormAnggota.Show()
        FormAnggota.readAnggota("")
        Hide()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim command As New SqlDataAdapter("SELECT b.id_petugas FROM [dbo].[user] a INNER JOIN [dbo].[petugas] b ON a.id_user = b.id_user WHERE a.username = '" + username + "' AND a.password = '" + password + "'", koneksi.connect)
        Dim ds As New DataSet
        command.Fill(ds)
        DataPeminjaman.kosong()

        DataPeminjaman.id_petugas = ds.Tables(0).Rows(0).Item(0)
        DataPeminjaman.Show()
        DataPeminjaman.readPeminjaman("")
        Hide()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        FormUser.kosong()

        FormUser.Show()
        FormUser.readUser("", "")
        Hide()

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        FormReport.readReport()
        FormReport.Show()
        Hide()

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        FormPengembalian.kosong()

        FormPengembalian.Show()
        FormPengembalian.readPeminjaman("")
        Hide()

    End Sub

End Class