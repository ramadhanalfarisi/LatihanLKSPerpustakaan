
Imports System.Data.SqlClient
Public Class DetilPeminjaman
    Dim koneksi As New Connection
    Dim dtPeminjaman As New DataTable
    Sub cekKoneksi()
        If koneksi.connect.State = ConnectionState.Closed Then
            koneksi.connect.Open()

        End If
    End Sub

    Sub makeTablePeminjaman()
        dtPeminjaman.Columns.Add("ID BUKU")
        dtPeminjaman.Columns.Add("JUDUL")
        dtPeminjaman.Columns.Add("KATEGORI")
        dtPeminjaman.Columns.Add("NAMA ANGGOTA")
        dtPeminjaman.Columns.Add("TANGGAL PINJAM")
        dtPeminjaman.Columns.Add("TANGGAL KEMBALI")
        dgPinjam.DataSource = dtPeminjaman
    End Sub

    Public Sub readPeminjaman(ByVal where As String)
        cekKoneksi()
        dtPeminjaman.Clear()

        Dim command As New SqlDataAdapter("SELECT a.kode_buku,b.judul,c.nama_kat,e.nama_lengkap,a.tgl_pinjam,a.tgl_kembali FROM [dbo].[peminjaman_buku] a INNER JOIN [dbo].[buku] b ON a.kode_buku = b.kode_buku INNER JOIN [dbo].[kategori] c ON b.id_kat=c.id_kat INNER JOIN [dbo].[peminjaman] d ON d.id_pinjam=a.id_pinjam INNER JOIN [dbo].[anggota] e ON d.id_anggota=e.id_anggota " + where, koneksi.connect)
        Dim dsPinjam As New DataSet
        command.Fill(dsPinjam)
        If dsPinjam.Tables(0).Rows.Count > 0 Then
            Dim i = 0
            While i < dsPinjam.Tables(0).Rows.Count
                dtPeminjaman.Rows.Add(dsPinjam.Tables(0).Rows(i).Item(0), dsPinjam.Tables(0).Rows(i).Item(1), dsPinjam.Tables(0).Rows(i).Item(2), dsPinjam.Tables(0).Rows(i).Item(3), Date.Parse(dsPinjam.Tables(0).Rows(i).Item(4)).ToShortDateString(), Date.Parse(dsPinjam.Tables(0).Rows(i).Item(5)).ToShortDateString())
                i += 1
            End While
        End If
    End Sub

    Private Sub DetilPeminjaman_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        makeTablePeminjaman()
        readPeminjaman("")
    End Sub

    Private Sub cariPinjam_TextChanged(sender As Object, e As EventArgs) Handles cariPinjam.TextChanged
        readPeminjaman("WHERE a.kode_buku LIKE '%" + cariPinjam.Text + "%' OR b.judul LIKE '%" + cariPinjam.Text + "%' OR c.nama_kat LIKE '%" + cariPinjam.Text + "%' OR e.nama_lengkap LIKE '%" + cariPinjam.Text + "%' OR a.tgl_pinjam LIKE '%" + cariPinjam.Text + "%' OR a.tgl_kembali LIKE '%" + cariPinjam.Text + "%'")
    End Sub

    Private Sub Label2_Click_1(sender As Object, e As EventArgs) Handles Label2.Click
        MenuAdmin.Show()
        Hide()
    End Sub
End Class