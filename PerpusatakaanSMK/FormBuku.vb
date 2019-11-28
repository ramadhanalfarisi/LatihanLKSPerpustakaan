Imports System.Data.SqlClient
Public Class FormBuku
    Dim koneksi As New Connection
    Dim dtBuku As New DataTable
    Dim dsKat As New DataSet
    Dim dsLok As New DataSet
    Dim id As String
    Dim angka() As Char = "1234567890".ToCharArray
    Sub cekKoneksi()
        If koneksi.connect.State = ConnectionState.Closed Then
            koneksi.connect.Open()

        End If
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

    Public Sub kosong()
        id = Nothing
        cariBuku.Text = ""
        kodeBuku.Text = ""
        judulBuku.Text = ""
        penulisBuku.Text = ""
        penerbitBuku.Text = ""
        deskripsiBuku.Text = ""
        hargaBuku.Text = ""
        stokBuku.Text = ""
        cbKat.SelectedItem = Nothing
        cbLok.SelectedItem = Nothing
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

    Sub makeCbkategori()
        Dim command As New SqlDataAdapter("SELECT * FROM [dbo].[kategori]", koneksi.connect)
        command.Fill(dsKat)
        If dsKat.Tables(0).Rows.Count > 0 Then
            Dim i = 0
            While i < dsKat.Tables(0).Rows.Count
                cbKat.Items.Add(dsKat.Tables(0).Rows(i).Item(1))
                i += 1
            End While
        End If
    End Sub

    Sub makecbLokasi()
        Dim command As New SqlDataAdapter("SELECT kode_lokasi,label FROM [dbo].[lokasi]", koneksi.connect)
        command.Fill(dsLok)
        If dsLok.Tables(0).Rows.Count > 0 Then
            Dim i = 0
            While i < dsLok.Tables(0).Rows.Count
                cbLok.Items.Add(dsLok.Tables(0).Rows(i).Item(1))
                i += 1
            End While
        End If
    End Sub
    Private Sub FormBuku_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        makeCbkategori()
        makecbLokasi()
        maketableBuku()
        readBuku("")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        cekKoneksi()

        If kodeBuku.Text = "" Or judulBuku.Text = "" Or penulisBuku.Text = "" Or penerbitBuku.Text = "" Or deskripsiBuku.Text = "" Or hargaBuku.Text = "" Or stokBuku.Text = "" Or cbKat.SelectedItem = Nothing Or cbLok.SelectedItem = Nothing Then
            MessageBox.Show(Nothing, "Semua field harus terisi", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim i = 0
        While i < angka.Length
            If penulisBuku.Text.Contains(angka(i)) Then
                MessageBox.Show(Nothing, "Field penulis harus terdiri dari huruf", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            i += 1
        End While

        Try
            Integer.Parse(hargaBuku.Text)
            Integer.Parse(stokBuku.Text)
        Catch ex As Exception
            MessageBox.Show(Nothing, "Field Harga dan Stok harus terdiri dari angka", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        Try
            Dim command As New SqlCommand("INSERT INTO [dbo].[buku]([kode_buku],[kode_lokasi],[id_kat],[judul],[penulis],[penerbit],[deskripsi],[harga],[stok]) VALUES(@kode_buku,@kode_lokasi,@id_kat,@judul,@penulis,@penerbit,@deskripsi,@harga,@stok)", koneksi.connect)
            command.Parameters.AddWithValue("@kode_buku", kodeBuku.Text)
            command.Parameters.AddWithValue("@kode_lokasi", dsLok.Tables(0).Rows(cbLok.SelectedIndex).Item(0))
            command.Parameters.AddWithValue("@id_kat", dsKat.Tables(0).Rows(cbKat.SelectedIndex).Item(0))
            command.Parameters.AddWithValue("@judul", judulBuku.Text)
            command.Parameters.AddWithValue("@penulis", penulisBuku.Text)
            command.Parameters.AddWithValue("@penerbit", penerbitBuku.Text)
            command.Parameters.AddWithValue("@deskripsi", deskripsiBuku.Text)
            command.Parameters.AddWithValue("@harga", hargaBuku.Text)
            command.Parameters.AddWithValue("@stok", stokBuku.Text)
            If command.ExecuteNonQuery() > 0 Then
                MessageBox.Show(Nothing, "Berhasil menambah buku", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                readBuku("")
                kosong()

            Else
                MessageBox.Show(Nothing, "Gagal menambah buku", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                readBuku("")
                kosong()

            End If
        Catch ex As Exception
            MessageBox.Show(Nothing, "Kode Buku sudah terpakai", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        cekKoneksi()

        If id = Nothing Then
            MessageBox.Show(Nothing, "Harus memilih Buku dahulu", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If kodeBuku.Text = "" Or judulBuku.Text = "" Or penulisBuku.Text = "" Or penerbitBuku.Text = "" Or deskripsiBuku.Text = "" Or hargaBuku.Text = "" Or stokBuku.Text = "" Or cbKat.SelectedItem = Nothing Or cbLok.SelectedItem = Nothing Then
            MessageBox.Show(Nothing, "Semua field harus terisi", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim i = 0
        While i < angka.Length
            If penulisBuku.Text.Contains(angka(i)) Then
                MessageBox.Show(Nothing, "Field penulis harus terdiri dari huruf", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            i += 1
        End While

        Try
            Integer.Parse(hargaBuku.Text)
            Integer.Parse(stokBuku.Text)
        Catch ex As Exception
            MessageBox.Show(Nothing, "Field Harga dan Stok harus terdiri dari angka", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        Try
            Dim command As New SqlCommand("UPDATE [dbo].[buku] SET [kode_buku] = @kode_buku,[kode_lokasi] = @kode_lokasi,[id_kat] = @id_kat,[judul] = @judul,[penulis] = @penulis,[penerbit] = @penerbit,[deskripsi] = @deskripsi,[harga] = @harga,[stok] = @stok WHERE kode_buku = @kodebaru", koneksi.connect)
            command.Parameters.AddWithValue("@kodebaru", id)
            command.Parameters.AddWithValue("@kode_buku", kodeBuku.Text)
            command.Parameters.AddWithValue("@kode_lokasi", dsLok.Tables(0).Rows(cbLok.SelectedIndex).Item(0))
            command.Parameters.AddWithValue("@id_kat", dsKat.Tables(0).Rows(cbKat.SelectedIndex).Item(0))
            command.Parameters.AddWithValue("@judul", judulBuku.Text)
            command.Parameters.AddWithValue("@penulis", penulisBuku.Text)
            command.Parameters.AddWithValue("@penerbit", penerbitBuku.Text)
            command.Parameters.AddWithValue("@deskripsi", deskripsiBuku.Text)
            command.Parameters.AddWithValue("@harga", hargaBuku.Text)
            command.Parameters.AddWithValue("@stok", stokBuku.Text)
            If command.ExecuteNonQuery() > 0 Then
                MessageBox.Show(Nothing, "Berhasil mengubah buku", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                readBuku("")
                kosong()

            Else
                MessageBox.Show(Nothing, "Gagal mengubah buku", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                readBuku("")
                kosong()

            End If
        Catch ex As Exception
            MessageBox.Show(Nothing, "Kode Buku sudah terpakai", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

    End Sub

    Private Sub dgBuku_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgBuku.CellContentClick
        id = dgBuku.CurrentRow.Cells(0).Value
        kodeBuku.Text = dgBuku.CurrentRow.Cells(0).Value
        judulBuku.Text = dgBuku.CurrentRow.Cells(1).Value
        penulisBuku.Text = dgBuku.CurrentRow.Cells(2).Value
        penerbitBuku.Text = dgBuku.CurrentRow.Cells(3).Value
        deskripsiBuku.Text = dgBuku.CurrentRow.Cells(4).Value
        hargaBuku.Text = dgBuku.CurrentRow.Cells(5).Value
        stokBuku.Text = dgBuku.CurrentRow.Cells(6).Value
        cbKat.SelectedItem = dgBuku.CurrentRow.Cells(7).Value
        cbLok.SelectedItem = dgBuku.CurrentRow.Cells(8).Value
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        cekKoneksi()

        If id = Nothing Then
            MessageBox.Show(Nothing, "Harus memilih Buku dahulu", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim command As New SqlCommand("DELETE FROM [dbo].[buku] WHERE kode_buku = @kode_buku", koneksi.connect)
        command.Parameters.AddWithValue("@kode_buku", id)
        If command.ExecuteNonQuery() > 0 Then
            MessageBox.Show(Nothing, "Berhasil menghapus buku", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            readBuku("")
            kosong()

        Else
            MessageBox.Show(Nothing, "Gagal menghapus buku", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            readBuku("")
            kosong()

        End If
    End Sub

    Private Sub cariBuku_TextChanged(sender As Object, e As EventArgs) Handles cariBuku.TextChanged
        readBuku("WHERE a.kode_buku LIKE '%" + cariBuku.Text + "%' OR a.penulis LIKE '%" + cariBuku.Text + "%' OR a.penerbit LIKE '%" + cariBuku.Text + "%' OR a.deskripsi LIKE '%" + cariBuku.Text + "%' OR a.judul LIKE '%" + cariBuku.Text + "%' OR a.harga LIKE '%" + cariBuku.Text + "%' OR a.stok LIKE '%" + cariBuku.Text + "%' OR b.nama_kat LIKE '%" + cariBuku.Text + "%' OR c.label LIKE '%" + cariBuku.Text + "%'")
    End Sub

    Private Sub Label2_Click_1(sender As Object, e As EventArgs) Handles Label2.Click
        MenuAdmin.Show()
        Hide()
    End Sub
End Class