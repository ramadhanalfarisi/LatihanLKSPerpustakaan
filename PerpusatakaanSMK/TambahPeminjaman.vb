Imports System.Data.SqlClient
Public Class TambahPeminjaman

    Dim koneksi As New Connection
    Dim dtPeminjaman As New DataTable
    Dim dsAnggota As New DataSet
    Public id_petugas, stok As String

    Sub cekKoneksi()
        If koneksi.connect.State = ConnectionState.Closed Then
            koneksi.connect.Open()

        End If
    End Sub

    Public Sub kosong()
        dtPeminjaman.Clear()
        idAnggota.Text = ""
        namaAnggota.Text = ""
        idBuku.Text = ""
        judulBuku.Text = ""
        penerbitBuku.Text = ""
        penulisBuku.Text = ""
        kategoriBuku.Text = ""
    End Sub

    Sub makeTablePeminjaman()
        dtPeminjaman.Columns.Add("ID ANGGOTA")
        dtPeminjaman.Columns.Add("NAMA ANGGOTA")
        dtPeminjaman.Columns.Add("ID BUKU")
        dtPeminjaman.Columns.Add("JUDUL BUKU")
        dtPeminjaman.Columns.Add("PENERBIT")
        dtPeminjaman.Columns.Add("PENULIS")
        dtPeminjaman.Columns.Add("KATEGORI")
        dtPeminjaman.Columns.Add("TANGGAL PINJAM")
        dtPeminjaman.Columns.Add("TANGGAL KEMBALI")
        dgPinjam.DataSource = dtPeminjaman
    End Sub

    Private Sub TambahPeminjaman_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        makeTablePeminjaman()

        dtPinjam.MinDate = Today
        dtKembali.MinDate = Today
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim id_anggota, nama, id_buku, judul, penerbit, penulis, kategori As String
        Dim tgl_pinjam, tgl_kembali As DateTime
        id_anggota = idAnggota.Text
        nama = namaAnggota.Text
        id_buku = idBuku.Text
        judul = judulBuku.Text
        penerbit = penerbitBuku.Text
        penulis = penulisBuku.Text
        kategori = kategoriBuku.Text
        tgl_pinjam = dtPinjam.Value
        tgl_kembali = dtKembali.Value

        If id_anggota = "" Or nama = "" Or id_buku = "" Or judul = "" Or penerbit = "" Or penulis = "" Or kategori = "" Then
            MessageBox.Show(Nothing, "Semua field harus terisi", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        dtPeminjaman.Rows.Add(id_anggota, nama, id_buku, judul, penulis, penerbit, kategori, tgl_pinjam.ToShortDateString, tgl_kembali.ToShortDateString)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DaftarBuku.id_petugas = id_petugas
        DaftarBuku.id_anggota = idAnggota.Text
        DaftarBuku.nama = namaAnggota.Text
        DaftarBuku.Show()
        DaftarBuku.readBuku("")
        Hide()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        cekKoneksi()

        Dim command As New SqlDataAdapter("SELECT nama_lengkap FROM [dbo].[anggota] WHERE id_anggota = '" + idAnggota.Text + "'", koneksi.connect)
        Dim dsAnggota As New DataSet
        command.Fill(dsAnggota)
        If dsAnggota.Tables(0).Rows.Count > 0 Then
            namaAnggota.Text = dsAnggota.Tables(0).Rows(0).Item(0)
        Else
            MessageBox.Show(Nothing, "Id tidak terdaftar", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim id_anggota = dgPinjam.CurrentRow.Cells(0).Value
        Dim id_buku = dgPinjam.CurrentRow.Cells(2).Value
        If id_anggota = "" Or id_buku = "" Then
            MessageBox.Show(Nothing, "Harus memillih pinjaman dulu", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim row = dtPeminjaman.Select("[ID ANGGOTA] = '" + id_anggota + "' AND [ID BUKU] = '" + id_buku + "'").FirstOrDefault
        row.Delete()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        kosong()

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        cekKoneksi()

        If dtPeminjaman.Rows.Count > 0 Then
            Dim i = 0
            While i < dtPeminjaman.Rows.Count
                Dim readCek As New SqlDataAdapter("SELECT id_pinjam FROM [dbo].[peminjaman] WHERE id_petugas = '" + id_petugas + "' AND id_anggota = '" + dtPeminjaman.Rows(i).Item(0) + "'", koneksi.connect)
                Dim ds As New DataSet
                readCek.Fill(ds)
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim command2 As New SqlCommand("INSERT INTO [dbo].[peminjaman_buku](kode_buku,id_pinjam,tgl_pinjam,tgl_kembali) VALUES(@kode_buku,@id_pinjam,@tgl_pinjam,@tgl_kembali)", koneksi.connect)
                    command2.Parameters.AddWithValue("@kode_buku", idBuku.Text)
                    command2.Parameters.AddWithValue("@id_pinjam", ds.Tables(0).Rows(0).Item(0))
                    command2.Parameters.AddWithValue("@tgl_pinjam", dtPinjam.Value)
                    command2.Parameters.AddWithValue("@tgl_kembali", dtKembali.Value)
                    If command2.ExecuteNonQuery() > 0 Then
                        stok -= 1
                        Dim command3 As New SqlCommand("UPDATE [dbo].[buku] SET stok = @stok WHERE kode_buku = @kode", koneksi.connect)
                        command3.Parameters.AddWithValue("@stok", stok)
                        command3.Parameters.AddWithValue("kode", dtPeminjaman.Rows(i).Item(2))
                        command3.ExecuteNonQuery()
                    End If
                Else
                    Dim command As New SqlCommand("INSERT INTO [dbo].[peminjaman](id_petugas,id_anggota) VALUES(@idpetugas,@idanggota)", koneksi.connect)
                    command.Parameters.AddWithValue("@idpetugas", id_petugas)
                    command.Parameters.AddWithValue("@idanggota", dtPeminjaman.Rows(i).Item(0))
                    If command.ExecuteNonQuery() > 0 Then
                        Dim cekID As New SqlDataAdapter("SELECT id_pinjam FROM [dbo].[peminjaman] WHERE id_petugas = '" + id_petugas + "' AND id_anggota = '" + dtPeminjaman.Rows(i).Item(0) + "'", koneksi.connect)
                        Dim dsID As New DataSet
                        cekID.Fill(dsID)
                        If dsID.Tables(0).Rows.Count > 0 Then
                            Dim command2 As New SqlCommand("INSERT INTO [dbo].[peminjaman_buku](kode_buku,id_pinjam,tgl_pinjam,tgl_kembali) VALUES(@kode_buku,@id_pinjam,@tgl_pinjam,@tgl_kembali)", koneksi.connect)
                            command2.Parameters.AddWithValue("@kode_buku", idBuku.Text)
                            command2.Parameters.AddWithValue("@id_pinjam", dsID.Tables(0).Rows(0).Item(0))
                            command2.Parameters.AddWithValue("@tgl_pinjam", dtPinjam.Value)
                            command2.Parameters.AddWithValue("@tgl_kembali", dtKembali.Value)
                            If command2.ExecuteNonQuery() > 0 Then
                                stok -= 1
                                Dim command3 As New SqlCommand("UPDATE [dbo].[buku] SET stok = @stok WHERE kode_buku = @kode", koneksi.connect)
                                command3.Parameters.AddWithValue("@stok", stok)
                                command3.Parameters.AddWithValue("kode", dtPeminjaman.Rows(i).Item(2))
                                command3.ExecuteNonQuery()
                            End If
                        End If
                    End If
                End If
                i += 1
            End While
        Else
            MessageBox.Show(Nothing, "Harus meminjam buku dulu", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        MessageBox.Show(Nothing, "Berhasil meminjam buku", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        kosong()

    End Sub

    Private Sub Label2_Click_1(sender As Object, e As EventArgs) Handles Label2.Click
        DataPeminjaman.readPeminjaman("")
        DataPeminjaman.Show()
        Hide()
    End Sub
End Class