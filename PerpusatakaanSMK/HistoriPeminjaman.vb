Imports System.Data.SqlClient
Public Class HistoriPeminjaman

    Dim koneksi As New Connection
    Dim dtPeminjaman As New DataTable

    Sub cekKoneksi()
        If koneksi.connect.State = ConnectionState.Closed Then
            koneksi.connect.Open()

        End If
    End Sub

    Sub makeTablePeminjaman()
        dtPeminjaman.Columns.Add("ID PEMINJAMAN")
        dtPeminjaman.Columns.Add("NAMA ANGGOTA")
        dtPeminjaman.Columns.Add("NAMA PETUGAS")
        dtPeminjaman.Columns.Add("TANGGAL PINJAM")
        dtPeminjaman.Columns.Add("TANGGAL KEMBALI")
        dtPeminjaman.Columns.Add("TANGGAL KEMBALI RIIL")
        dtPeminjaman.Columns.Add("DENDA")
        dtPeminjaman.Columns.Add("JUMLAH HARI DENDA")
        dgPeminjaman.DataSource = dtPeminjaman
    End Sub

    Public Sub readPeminjaman(ByVal where As String)
        cekKoneksi()
        dtPeminjaman.Clear()

        Dim command As New SqlDataAdapter("SELECT a.id,c.nama_lengkap,d.nama_petugas,a.tgl_pinjam,a.tgl_kembali,a.tgl_kembali_riil,a.denda,a.jml_hari_denda FROM [dbo].[peminjaman_buku] a INNER JOIN [dbo].[peminjaman] b ON a.id_pinjam = b.id_pinjam INNER JOIN [dbo].[anggota] c ON c.id_anggota=b.id_anggota INNER JOIN [dbo].[petugas] d ON d.id_petugas=b.id_petugas " + where, koneksi.connect)
        Dim dsPinjam As New DataSet
        command.Fill(dsPinjam)
        If dsPinjam.Tables(0).Rows.Count > 0 Then
            Dim i = 0
            While i < dsPinjam.Tables(0).Rows.Count
                dtPeminjaman.Rows.Add(dsPinjam.Tables(0).Rows(i).Item(0), dsPinjam.Tables(0).Rows(i).Item(1), dsPinjam.Tables(0).Rows(i).Item(2), Date.Parse(dsPinjam.Tables(0).Rows(i).Item(3)).ToShortDateString(), Date.Parse(dsPinjam.Tables(0).Rows(i).Item(4)).ToShortDateString(), Date.Parse(dsPinjam.Tables(0).Rows(i).Item(5)).ToShortDateString(), dsPinjam.Tables(0).Rows(i).Item(6), dsPinjam.Tables(0).Rows(i).Item(7))
                i += 1
            End While
        End If
    End Sub
    Private Sub HistoriPeminjaman_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        makeTablePeminjaman()
        readPeminjaman("")
    End Sub

    Private Sub cariPeminjaman_TextChanged(sender As Object, e As EventArgs) Handles cariPinjam.TextChanged
        readPeminjaman("WHERE c.nama_lengkap LIKE '%" + cariPinjam.Text + "%' OR d.nama_petugas LIKE '%" + cariPinjam.Text + "%' OR a.tgl_pinjam LIKE '%" + cariPinjam.Text + "%' OR a.tgl_kembali LIKE '%" + cariPinjam.Text + "%' OR a.tgl_kembali_riil LIKE '%" + cariPinjam.Text + "%' OR a.denda LIKE '%" + cariPinjam.Text + "%' OR a.jml_hari_denda LIKE '%" + cariPinjam.Text + "%'")
    End Sub

    Private Sub Label2_Click_1(sender As Object, e As EventArgs) Handles Label2.Click
        MenuAnggota.Show()
        Hide()
    End Sub
End Class