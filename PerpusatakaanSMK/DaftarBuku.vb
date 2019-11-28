Imports System.Data.SqlClient
Public Class DaftarBuku
    Dim koneksi As New Connection
    Dim dtBuku As New DataTable
    Dim kode_buku, judul, penulis, penerbit, kategori, stok As String
    Public id_anggota, nama, id_petugas As String
    Sub cekKoneksi()
        If koneksi.connect.State = ConnectionState.Closed Then
            koneksi.connect.Open()

        End If
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label2_Click_1(sender As Object, e As EventArgs) Handles Label2.Click
        DataPeminjaman.Show()
        Hide()

    End Sub

    Sub maketableBuku()
        dtBuku.Columns.Add("ID BUKU")
        dtBuku.Columns.Add("JUDUL")
        dtBuku.Columns.Add("PENULIS")
        dtBuku.Columns.Add("PENERBIT")
        dtBuku.Columns.Add("DESKRIPSI")
        dtBuku.Columns.Add("HARGA")
        dtBuku.Columns.Add("STOK")
        dtBuku.Columns.Add("KATEGORI")
        dtBuku.Columns.Add("LOKASI")
        dgBuku.DataSource = dtBuku
    End Sub

    Public Sub readBuku(ByVal where As String)
        cekKoneksi()
        dtBuku.Clear()

        Dim command As New SqlDataAdapter("SELECT a.kode_buku,a.judul,a.penulis,a.penerbit,a.deskripsi,a.harga,a.stok,b.nama_kat,c.label FROM [dbo].[buku] a INNER JOIN [dbo].[kategori] b ON a.id_kat = b.id_kat INNER JOIN [dbo].[lokasi] c ON a.kode_lokasi = c.kode_lokasi " + where, koneksi.connect)
        Dim dsBuku As New DataSet
        command.Fill(dsBuku)
        If dsBuku.Tables(0).Rows.Count > 0 Then
            Dim i = 0
            While i < dsBuku.Tables(0).Rows.Count
                dtBuku.Rows.Add(dsBuku.Tables(0).Rows(i).Item(0), dsBuku.Tables(0).Rows(i).Item(1), dsBuku.Tables(0).Rows(i).Item(2), dsBuku.Tables(0).Rows(i).Item(3), dsBuku.Tables(0).Rows(i).Item(4), dsBuku.Tables(0).Rows(i).Item(5), dsBuku.Tables(0).Rows(i).Item(6), dsBuku.Tables(0).Rows(i).Item(7), dsBuku.Tables(0).Rows(i).Item(8))
                i += 1
            End While
        End If
    End Sub
    Private Sub DaftarBuku_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        maketableBuku()
        readBuku("")
    End Sub

    Private Sub cariPinjam_TextChanged(sender As Object, e As EventArgs) Handles cariBuku.TextChanged
        readBuku("WHERE a.kode_buku LIKE '%" + cariBuku.Text + "%' OR a.penulis LIKE '%" + cariBuku.Text + "%' OR a.penerbit LIKE '%" + cariBuku.Text + "%' OR a.deskripsi LIKE '%" + cariBuku.Text + "%' OR a.judul LIKE '%" + cariBuku.Text + "%' OR a.harga LIKE '%" + cariBuku.Text + "%' OR a.stok LIKE '%" + cariBuku.Text + "%' OR b.nama_kat LIKE '%" + cariBuku.Text + "%' OR c.label LIKE '%" + cariBuku.Text + "%'")
    End Sub

    Private Sub dgBuku_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgBuku.CellContentClick
        kode_buku = dgBuku.CurrentRow.Cells(0).Value
        judul = dgBuku.CurrentRow.Cells(1).Value
        penulis = dgBuku.CurrentRow.Cells(2).Value
        penerbit = dgBuku.CurrentRow.Cells(3).Value
        kategori = dgBuku.CurrentRow.Cells(7).Value
        stok = dgBuku.CurrentRow.Cells(6).Value
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim tambahPinjam As New TambahPeminjaman
        tambahPinjam.stok = stok
        tambahPinjam.id_petugas = id_petugas
        tambahPinjam.idAnggota.Text = id_anggota
        tambahPinjam.namaAnggota.Text = nama
        tambahPinjam.idBuku.Text = kode_buku
        tambahPinjam.judulBuku.Text = judul
        tambahPinjam.penulisBuku.Text = penulis
        tambahPinjam.penerbitBuku.Text = penerbit
        tambahPinjam.kategoriBuku.Text = kategori
        tambahPinjam.Show()
        Hide()

    End Sub
End Class