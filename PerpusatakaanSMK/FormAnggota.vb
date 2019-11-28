Imports System.Data.SqlClient
Imports System.IO
Public Class FormAnggota
    Dim koneksi As New Connection
    Dim dtAnggota As New DataTable
    Dim angka() As Char = "1234567890".ToCharArray
    Dim huruf() As Char = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray
    Dim idHasil As String = ""
    Dim id, nik As String
    Sub cekKoneksi()
        If koneksi.connect.State = ConnectionState.Closed Then
            koneksi.connect.Open()
        End If
    End Sub


    Sub makeTableAnggota()
        dtAnggota.Columns.Add("ID ANGGOTA")
        dtAnggota.Columns.Add("NAMA")
        dtAnggota.Columns.Add("NIK")
        dtAnggota.Columns.Add("ALAMAT")
        dtAnggota.Columns.Add("TGL DAFTAR")
        dgAnggota.DataSource = dtAnggota
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

    Function getImage(ByVal id As String)
        cekKoneksi()

        Dim command As New SqlDataAdapter("SELECT foto FROM [dbo].[anggota] WHERE id_anggota = " + id, koneksi.connect)
        Dim read As New DataSet
        command.Fill(read)
        Dim ft As Byte() = CType(read.Tables(0).Rows(0).Item(0), Byte())
        Dim ms As New MemoryStream(ft)
        Return ms
    End Function

    Function getNIK(ByVal nik As String)
        Dim command As New SqlDataAdapter("SELECT * FROM [dbo].[anggota] WHERE nik = '" + nik + "'", koneksi.connect)
        Dim ds As New DataSet
        command.Fill(ds)
        Return ds.Tables(0).Rows.Count
    End Function

    Public Sub kosong()
        id = Nothing
        cariAnggota.Text = ""
        namaAnggota.Text = ""
        nikAnggota.Text = ""
        alamatAnggota.Text = ""
        pbFoto.Image = Nothing
    End Sub

    Public Sub readAnggota(ByVal where As String)
        cekKoneksi()

        dtAnggota.Clear()
        Dim command As New SqlDataAdapter("SELECT id_anggota,nama_lengkap,nik,alamat,tgl_daftar FROM [dbo].[anggota] " + where, koneksi.connect)
        Dim dsAnggota As New DataSet
        command.Fill(dsAnggota)
        If dsAnggota.Tables(0).Rows.Count > 0 Then
            Dim i = 0
            While i < dsAnggota.Tables(0).Rows.Count
                dtAnggota.Rows.Add(dsAnggota.Tables(0).Rows(i).Item(0), dsAnggota.Tables(0).Rows(i).Item(1), dsAnggota.Tables(0).Rows(i).Item(2), dsAnggota.Tables(0).Rows(i).Item(3), Date.Parse(dsAnggota.Tables(0).Rows(i).Item(4)).ToShortDateString())
                i += 1
            End While
        End If
    End Sub

    Private Sub FormAnggota_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        makeTableAnggota()
        readAnggota("")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        cekKoneksi()
        Dim ms As New MemoryStream
        pbFoto.Image.Save(ms, pbFoto.Image.RawFormat)

        If namaAnggota.Text = "" Or nikAnggota.Text = "" Or alamatAnggota.Text = "" Or pbFoto.Image Is Nothing Then
            MessageBox.Show(Nothing, "Semua field harus terisi", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim i = 0
        While i < angka.Length
            If namaAnggota.Text.Contains(angka(i)) Then
                MessageBox.Show(Nothing, "Field nama Anggota harus terdiri dari huruf", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            i += 1
        End While

        Dim a = 0
        While a < huruf.Length
            If nikAnggota.Text.Contains(huruf(a)) Then
                MessageBox.Show(Nothing, "Field NIK harus terdiri dari angka", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            a += 1
        End While

        If getNIK(nikAnggota.Text) > 0 Then
            MessageBox.Show(Nothing, "NIK sudah ada", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim Anggota As New SqlCommand("INSERT INTO [dbo].[anggota](id_anggota,nama_lengkap,nik,alamat,foto,tgl_daftar) VALUES(@id_anggota,@nama_lengkap,@nik,@alamat,@foto,@tanggal)", koneksi.connect)
        Anggota.Parameters.AddWithValue("@id_anggota", makeRandomID())
        Anggota.Parameters.AddWithValue("@nama_lengkap", namaAnggota.Text)
        Anggota.Parameters.AddWithValue("@nik", nikAnggota.Text)
        Anggota.Parameters.AddWithValue("@alamat", alamatAnggota.Text)
        Anggota.Parameters.Add("@foto", SqlDbType.Image).Value = ms.ToArray
        Anggota.Parameters.AddWithValue("@tanggal", Date.Now.ToString("yyyy/MM/dd HH:mm:ss"))
        If Anggota.ExecuteNonQuery() > 0 Then
            MessageBox.Show(Nothing, "Berhasil menambah Anggota", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            readAnggota("")
            kosong()

        Else
            MessageBox.Show(Nothing, "Gagal menambah Anggota", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            readAnggota("")
            kosong()

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        cekKoneksi()
        Dim ms As New MemoryStream
        pbFoto.Image.Save(ms, pbFoto.Image.RawFormat)

        If id = Nothing Then
            MessageBox.Show(Nothing, "Harus Memilih Anggota dulu", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If


        If namaAnggota.Text = "" Or nikAnggota.Text = "" Or alamatAnggota.Text = "" Or pbFoto.Image Is Nothing Then
            MessageBox.Show(Nothing, "Field Nama Anggota, NIK, Alamat, Foto harus terisi", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim i = 0
        While i < angka.Length
            If namaAnggota.Text.Contains(angka(i)) Then
                MessageBox.Show(Nothing, "Field nama Anggota harus terdiri dari huruf", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            i += 1
        End While

        Dim a = 0
        While a < huruf.Length
            If nikAnggota.Text.Contains(huruf(a)) Then
                MessageBox.Show(Nothing, "Field NIK harus terdiri dari angka", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            a += 1
        End While



        If Not nik = nikAnggota.Text Then
            If getNIK(nikAnggota.Text) > 0 Then
                MessageBox.Show(Nothing, "NIK sudah ada", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
        End If


        Dim Anggota As New SqlCommand("UPDATE [dbo].[anggota] SET nama_lengkap = @nama_anggota,nik = @nik,alamat = @alamat, foto = @foto WHERE id_anggota=@id", koneksi.connect)
        Anggota.Parameters.AddWithValue("@id", id)
        Anggota.Parameters.AddWithValue("@nama_anggota", namaAnggota.Text)
        Anggota.Parameters.AddWithValue("@nik", nikAnggota.Text)
        Anggota.Parameters.AddWithValue("@alamat", alamatAnggota.Text)
        Anggota.Parameters.Add("@foto", SqlDbType.Image).Value = ms.ToArray
        If Anggota.ExecuteNonQuery() > 0 Then
            MessageBox.Show(Nothing, "Berhasil mengubah Anggota", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            readAnggota("")
            kosong()

        Else
            MessageBox.Show(Nothing, "Gagal mengubah Anggota", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            readAnggota("")
            kosong()

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        cekKoneksi()

        If id = Nothing Then
            MessageBox.Show(Nothing, "Harus Memilih Anggota dulu", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim Anggota As New SqlCommand("DELETE FROM [dbo].[anggota] WHERE id_anggota=@id", koneksi.connect)
        Anggota.Parameters.AddWithValue("@id", id)
        If Anggota.ExecuteNonQuery() > 0 Then
            MessageBox.Show(Nothing, "Berhasil menghapus Anggota", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            readAnggota("")
            kosong()


        Else
            MessageBox.Show(Nothing, "Gagal menghapus Anggota", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            readAnggota("")
            kosong()

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

    Private Sub cariAnggota_TextChanged(sender As Object, e As EventArgs) Handles cariAnggota.TextChanged
        readAnggota("WHERE nama_lengkap LIKE '%" + cariAnggota.Text + "%' OR nik LIKE '%" + cariAnggota.Text + "%' OR alamat LIKE '%" + cariAnggota.Text + "%' OR tgl_daftar LIKE '%" + cariAnggota.Text + "%'")
    End Sub

    Private Sub dgAnggota_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgAnggota.CellContentClick
        id = dgAnggota.CurrentRow.Cells(0).Value
        namaAnggota.Text = dgAnggota.CurrentRow.Cells(1).Value
        nik = dgAnggota.CurrentRow.Cells(2).Value
        nikAnggota.Text = dgAnggota.CurrentRow.Cells(2).Value
        alamatAnggota.Text = dgAnggota.CurrentRow.Cells(3).Value
        pbFoto.Image = Image.FromStream(getImage(dgAnggota.CurrentRow.Cells(0).Value))
    End Sub

    Private Sub Label2_Click_1(sender As Object, e As EventArgs) Handles Label2.Click
        MenuAdmin.Show()
        Hide()
    End Sub
End Class