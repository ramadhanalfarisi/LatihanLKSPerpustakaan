Imports System.Data.SqlClient
Imports System.IO
Public Class FormPetugas
    Dim koneksi As New Connection
    Dim dtPetugas As New DataTable
    Dim angka() As Char = "1234567890".ToCharArray
    Dim huruf() As Char = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray
    Dim idHasil As String = ""
    Dim id, nik As String
    Sub cekKoneksi()
        If koneksi.connect.State = ConnectionState.Closed Then
            koneksi.connect.Open()

        End If
    End Sub

    Sub makeTablePetugas()
        dtPetugas.Columns.Add("ID PETUGAS")
        dtPetugas.Columns.Add("NAMA")
        dtPetugas.Columns.Add("NIK")
        dtPetugas.Columns.Add("ALAMAT")
        dtPetugas.Columns.Add("TGL DAFTAR")
        dgPetugas.DataSource = dtPetugas
    End Sub

    Sub setKosong()
        namaPetugas.Text = ""
        nikPetugas.Text = ""
        alamatPetugas.Text = ""
        pbFoto.Image = Nothing
    End Sub

    Function makeRandomID()
        Dim r As New Random
        idHasil = ""
        Dim i = 0
        While i < 7
            Dim index = r.Next(0, angka.Length - 1)
            idHasil &= angka(index)
            i += 1
        End While
        Return idHasil
    End Function

    Function getNIK(ByVal nik As String)
        Dim command As New SqlDataAdapter("SELECT * FROM [dbo].[petugas] WHERE nik = '" + nik + "'", koneksi.connect)
        Dim ds As New DataSet
        command.Fill(ds)
        Return ds.Tables(0).Rows.Count
    End Function

    Public Sub kosong()
        id = Nothing
        cariPetugas.Text = ""
        namaPetugas.Text = ""
        nikPetugas.Text = ""
        alamatPetugas.Text = ""
        pbFoto.Image = Nothing
    End Sub

    Function getImage(ByVal id As String)
        cekKoneksi()

        Dim command As New SqlDataAdapter("SELECT foto FROM [dbo].[petugas] WHERE id_petugas = " + id, koneksi.connect)
        Dim read As New DataSet
        command.Fill(read)
        Dim ft As Byte() = CType(read.Tables(0).Rows(0).Item(0), Byte())
        Dim ms As New MemoryStream(ft)
        Return ms
    End Function

    Public Sub readPetugas(ByVal where As String)
        cekKoneksi()

        dtPetugas.Clear()
        Dim command As New SqlDataAdapter("SELECT id_petugas,nama_petugas,nik,alamat,tanggal_masuk FROM [dbo].[petugas] " + where, koneksi.connect)
        Dim dsPetugas As New DataSet
        command.Fill(dsPetugas)
        If dsPetugas.Tables(0).Rows.Count > 0 Then
            Dim i = 0
            While i < dsPetugas.Tables(0).Rows.Count
                dtPetugas.Rows.Add(dsPetugas.Tables(0).Rows(i).Item(0), dsPetugas.Tables(0).Rows(i).Item(1), dsPetugas.Tables(0).Rows(i).Item(2), dsPetugas.Tables(0).Rows(i).Item(3), Date.Parse(dsPetugas.Tables(0).Rows(i).Item(4)).ToShortDateString())
                i += 1
            End While
        End If
    End Sub
    Private Sub FormPetugas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        makeTablePetugas()
        readPetugas("")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        cekKoneksi()
        Dim ms As New MemoryStream
        pbFoto.Image.Save(ms, pbFoto.Image.RawFormat)

        If namaPetugas.Text = "" Or nikPetugas.Text = "" Or alamatPetugas.Text = "" Or pbFoto.Image Is Nothing Then
            MessageBox.Show(Nothing, "Semua field harus terisi", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim i = 0
        While i < angka.Length
            If namaPetugas.Text.Contains(angka(i)) Then
                MessageBox.Show(Nothing, "Field nama petugas harus terdiri dari huruf", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            i += 1
        End While

        Dim a = 0
        While a < huruf.Length
            If nikPetugas.Text.Contains(huruf(a)) Then
                MessageBox.Show(Nothing, "Field NIK harus terdiri dari angka", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            a += 1
        End While

        If getNIK(nikPetugas.Text) > 0 Then
            MessageBox.Show(Nothing, "NIK sudah ada", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim petugas As New SqlCommand("INSERT INTO [dbo].[petugas](id_petugas,nama_petugas,nik,alamat,foto,tanggal_masuk) VALUES(@id_petugas,@nama_petugas,@nik,@alamat,@foto,@tanggal)", koneksi.connect)
        petugas.Parameters.AddWithValue("@id_petugas", makeRandomID())
        petugas.Parameters.AddWithValue("@nama_petugas", namaPetugas.Text)
        petugas.Parameters.AddWithValue("@nik", nikPetugas.Text)
        petugas.Parameters.AddWithValue("@alamat", alamatPetugas.Text)
        petugas.Parameters.Add("@foto", SqlDbType.Image).Value = ms.ToArray
        petugas.Parameters.AddWithValue("@tanggal", Date.Now.ToString("yyyy/MM/dd HH:mm:ss"))
        If petugas.ExecuteNonQuery() > 0 Then
            MessageBox.Show(Nothing, "Berhasil menambah petugas", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            readPetugas("")
        Else
            MessageBox.Show(Nothing, "Gagal menambah petugas", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            readPetugas("")
        End If

    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        With opFoto
            .Filter = "Images File|*.jpg;*.png;*.bmp;*.gif"
            .FilterIndex = 4
        End With

        If opFoto.ShowDialog = DialogResult.OK Then
            pbFoto.Image = Image.FromFile(opFoto.FileName)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        cekKoneksi()
        Dim ms As New MemoryStream
        pbFoto.Image.Save(ms, pbFoto.Image.RawFormat)

        If id = Nothing Then
            MessageBox.Show(Nothing, "Harus Memilih Petugas dulu", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If namaPetugas.Text = "" Or nikPetugas.Text = "" Or alamatPetugas.Text = "" Or pbFoto.Image Is Nothing Then
            MessageBox.Show(Nothing, "Field Nama Petugas, NIK, Alamat, Foto harus terisi", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim i = 0
        While i < angka.Length
            If namaPetugas.Text.Contains(angka(i)) Then
                MessageBox.Show(Nothing, "Field nama petugas harus terdiri dari huruf", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            i += 1
        End While

        Dim a = 0
        While a < huruf.Length
            If nikPetugas.Text.Contains(huruf(a)) Then
                MessageBox.Show(Nothing, "Field NIK harus terdiri dari angka", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            a += 1
        End While

        If Not nik = nikPetugas.Text Then
            If getNIK(nikPetugas.Text) > 0 Then
                MessageBox.Show(Nothing, "NIK sudah ada", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
        End If

        Dim petugas As New SqlCommand("UPDATE [dbo].[petugas] SET nama_petugas = @nama_petugas,nik = @nik,alamat = @alamat, foto = @foto WHERE id_petugas=@id", koneksi.connect)
        petugas.Parameters.AddWithValue("@id", id)
        petugas.Parameters.AddWithValue("@nama_petugas", namaPetugas.Text)
        petugas.Parameters.AddWithValue("@nik", nikPetugas.Text)
        petugas.Parameters.AddWithValue("@alamat", alamatPetugas.Text)
        petugas.Parameters.Add("@foto", SqlDbType.Image).Value = ms.ToArray
        If petugas.ExecuteNonQuery() > 0 Then
            MessageBox.Show(Nothing, "Berhasil mengubah petugas", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            readPetugas("")
        Else
            MessageBox.Show(Nothing, "Gagal mengubah petugas", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            readPetugas("")
        End If
    End Sub

    Private Sub dgPetugas_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgPetugas.CellContentClick
        id = dgPetugas.CurrentRow.Cells(0).Value
        namaPetugas.Text = dgPetugas.CurrentRow.Cells(1).Value
        nik = dgPetugas.CurrentRow.Cells(2).Value
        nikPetugas.Text = dgPetugas.CurrentRow.Cells(2).Value
        alamatPetugas.Text = dgPetugas.CurrentRow.Cells(3).Value
        pbFoto.Image = Image.FromStream(getImage(dgPetugas.CurrentRow.Cells(0).Value))
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        cekKoneksi()

        If id = Nothing Then
            MessageBox.Show(Nothing, "Harus Memilih Petugas dulu", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim petugas As New SqlCommand("DELETE FROM [dbo].[petugas] WHERE id_petugas=@id", koneksi.connect)
        petugas.Parameters.AddWithValue("@id", id)
        If petugas.ExecuteNonQuery() > 0 Then
            MessageBox.Show(Nothing, "Berhasil menghapus petugas", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            readPetugas("")
            setKosong()
        Else
            MessageBox.Show(Nothing, "Gagal menghapus petugas", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            readPetugas("")
        End If

    End Sub

    Private Sub cariPetugas_TextChanged(sender As Object, e As EventArgs) Handles cariPetugas.TextChanged
        readPetugas("WHERE nama_petugas LIKE '%" + cariPetugas.Text + "%' OR nik LIKE '%" + cariPetugas.Text + "%' OR alamat LIKE '%" + cariPetugas.Text + "%' OR tanggal_masuk LIKE '%" + cariPetugas.Text + "%'")
    End Sub

    Private Sub Label2_Click_1(sender As Object, e As EventArgs) Handles Label2.Click
        MenuAdmin.Show()
        Hide()
    End Sub
End Class