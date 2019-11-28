Imports System.Data.SqlClient
Public Class FormPengembalian
    Dim koneksi As New Connection
    Dim dtPeminjaman As New DataTable
    Dim id As String
    Sub cekKoneksi()
        If koneksi.connect.State = ConnectionState.Closed Then
            koneksi.connect.Open()

        End If
    End Sub

    Sub makeTablePeminjaman()
        dtPeminjaman.Columns.Add("ID")
        dtPeminjaman.Columns.Add("ID BUKU")
        dtPeminjaman.Columns.Add("JUDUL")
        dtPeminjaman.Columns.Add("KATEGORI")
        dtPeminjaman.Columns.Add("NAMA ANGGOTA")
        dtPeminjaman.Columns.Add("TANGGAL PINJAM")
        dtPeminjaman.Columns.Add("TANGGAL KEMBALI")
        dgPeminjaman.DataSource = dtPeminjaman
    End Sub

    Public Sub kosong()
        id = Nothing
        kodeBuku.Text = ""
        judulBuku.Text = ""
        kategoriBuku.Text = ""
    End Sub

    Function getStok(ByVal id As String)
        cekKoneksi()

        Dim readStok As New SqlDataAdapter("SELECT stok FROM [dbo].[buku] WHERE kode_buku = '" + id + "'", koneksi.connect)
        Dim ds As New DataSet
        readStok.Fill(ds)
        Return ds.Tables(0).Rows(0).Item(0)
    End Function

    Public Sub readPeminjaman(ByVal where As String)
        cekKoneksi()
        dtPeminjaman.Clear()

        Dim command As New SqlDataAdapter("SELECT a.id,a.kode_buku,b.judul,c.nama_kat,e.nama_lengkap,a.tgl_pinjam,a.tgl_kembali FROM [dbo].[peminjaman_buku] a INNER JOIN [dbo].[buku] b ON a.kode_buku = b.kode_buku INNER JOIN [dbo].[kategori] c ON b.id_kat=c.id_kat INNER JOIN [dbo].[peminjaman] d ON d.id_pinjam=a.id_pinjam INNER JOIN [dbo].[anggota] e ON d.id_anggota=e.id_anggota " + where, koneksi.connect)
        Dim dsPinjam As New DataSet
        command.Fill(dsPinjam)
        If dsPinjam.Tables(0).Rows.Count > 0 Then
            Dim i = 0
            While i < dsPinjam.Tables(0).Rows.Count
                dtPeminjaman.Rows.Add(dsPinjam.Tables(0).Rows(i).Item(0), dsPinjam.Tables(0).Rows(i).Item(1), dsPinjam.Tables(0).Rows(i).Item(2), dsPinjam.Tables(0).Rows(i).Item(3), dsPinjam.Tables(0).Rows(i).Item(4), Date.Parse(dsPinjam.Tables(0).Rows(i).Item(5)).ToShortDateString(), Date.Parse(dsPinjam.Tables(0).Rows(i).Item(6)).ToShortDateString())
                i += 1
            End While
        End If
    End Sub

    Private Sub FormPengembalian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        makeTablePeminjaman()
        readPeminjaman("")
    End Sub

    Private Sub cariBuku_TextChanged(sender As Object, e As EventArgs) Handles cariPinjam.TextChanged
        readPeminjaman("WHERE a.kode_buku LIKE '%" + cariPinjam.Text + "%' OR b.judul LIKE '%" + cariPinjam.Text + "%' OR c.nama_kat LIKE '%" + cariPinjam.Text + "%' OR e.nama_lengkap LIKE '%" + cariPinjam.Text + "%' OR a.tgl_pinjam LIKE '%" + cariPinjam.Text + "%' OR a.tgl_kembali LIKE '%" + cariPinjam.Text + "%'")
    End Sub

    Private Sub dgPeeminjaman_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgPeminjaman.CellContentClick
        id = dgPeminjaman.CurrentRow.Cells(0).Value
        kodeBuku.Text = dgPeminjaman.CurrentRow.Cells(1).Value
        judulBuku.Text = dgPeminjaman.CurrentRow.Cells(2).Value
        kategoriBuku.Text = dgPeminjaman.CurrentRow.Cells(3).Value
        dtPinjam.Value = dgPeminjaman.CurrentRow.Cells(5).Value
        dtKembali.Text = dgPeminjaman.CurrentRow.Cells(6).Value

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        cekKoneksi()
        Dim jumlah_hari, denda As Integer

        If id = Nothing Then
            MessageBox.Show(Nothing, "Harus memilih peminjaman dulu", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim dateKembali = DateTime.Parse(dtKembali.Value.ToString)
        Dim dateRiil = DateTime.Parse(Date.Now.ToString)

        If (dateKembali.Date = dateRiil.Date) Then
            jumlah_hari = 0
            denda = 0
        Else
            jumlah_hari = dateRiil.Subtract(dateKembali).TotalDays.ToString
            denda = Integer.Parse(jumlah_hari * 2000)
        End If

        Dim command As New SqlCommand("UPDATE [dbo].[peminjaman_buku] SET tgl_kembali_riil = @riil, denda = @denda, jml_hari_denda = @hari_denda WHERE id = @id", koneksi.connect)
        command.Parameters.AddWithValue("@riil", Date.Now.ToString("yyyy/MM/dd HH:mm:ss"))
        command.Parameters.AddWithValue("@denda", denda.ToString)
        command.Parameters.AddWithValue("@hari_denda", jumlah_hari.ToString)
        command.Parameters.AddWithValue("@id", id)
        If command.ExecuteNonQuery() > 0 Then
            Dim stok = Integer.Parse(getStok(kodeBuku.Text) + 1)
            Dim command2 As New SqlCommand("UPDATE [dbo].[buku] SET stok = @stok WHERE kode_buku = @kode", koneksi.connect)
            command2.Parameters.AddWithValue("@stok", stok.ToString)
            command2.Parameters.AddWithValue("@kode", kodeBuku.Text)
            If command2.ExecuteNonQuery() > 0 Then
                MessageBox.Show(Nothing, "Buku sudah dikembalikan, Denda kamu Rp. " + denda.ToString, "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                readPeminjaman("")
            End If
        Else
            MessageBox.Show(Nothing, "Buku gagal dikembalikan", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            readPeminjaman("")
        End If
    End Sub

    Private Sub Label2_Click_1(sender As Object, e As EventArgs) Handles Label2.Click
        MenuAdmin.Show()
        Hide()
    End Sub
End Class