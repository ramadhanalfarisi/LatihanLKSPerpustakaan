Imports System.Data.SqlClient
Public Class FormUser
    Dim koneksi As New Connection
    Dim dtUser As New DataTable
    Dim id, user, pass As String

    Sub cekKoneksi()
        If koneksi.connect.State = ConnectionState.Closed Then
            koneksi.connect.Open()

        End If
    End Sub

    Sub maketableUser()
        dtUser.Columns.Add("ID USER")
        dtUser.Columns.Add("NAMA PENGGUNA")
        dtUser.Columns.Add("USERNAME")
        dtUser.Columns.Add("LEVEL")
        dgUser.DataSource = dtUser
    End Sub

    Public Sub kosong()
        IDPengguna.Enabled = Enabled
        IDPengguna.Text = ""
        username.Text = ""
        password.Text = ""
        conPassword.Text = ""
        cbLevel.SelectedItem = Nothing
    End Sub

    Function getPassword(ByVal id As String)
        cekKoneksi()
        Dim command As New SqlDataAdapter("SELECT password FROM [dbo].[user] WHERE id_user = '" + id + "'", koneksi.connect)
        Dim ds As New DataSet
        command.Fill(ds)
        Return ds.Tables(0).Rows(0).Item(0)
    End Function

    Public Sub readUser(ByVal wherePet As String, ByVal whereAng As String)
        cekKoneksi()
        dtUser.Clear()

        Dim commandPetugas As New SqlDataAdapter("SELECT a.id_user, b.nama_petugas, a.username, a.level FROM [dbo].[user] a INNER JOIN [dbo].[petugas] b ON a.id_user = b.id_user WHERE a.level = 'admin' " + wherePet, koneksi.connect)
        Dim commandAnggota As New SqlDataAdapter("SELECT a.id_user, b.nama_lengkap, a.username, a.level FROM [dbo].[user] a INNER JOIN [dbo].[anggota] b ON a.id_user = b.id_user WHERE a.level = 'anggota' " + whereAng, koneksi.connect)
        Dim dsPet As New DataSet
        Dim dsAngg As New DataSet
        commandAnggota.Fill(dsAngg)
        commandPetugas.Fill(dsPet)
        If (dsPet.Tables(0).Rows.Count > 0 Or dsAngg.Tables(0).Rows.Count > 0) Then
            Dim i = 0
            Dim a = 0
            While a < dsPet.Tables(0).Rows.Count
                dtUser.Rows.Add(dsPet.Tables(0).Rows(i).Item(0), dsPet.Tables(0).Rows(i).Item(1), dsPet.Tables(0).Rows(i).Item(2), dsPet.Tables(0).Rows(i).Item(3))
                a += 1
            End While
            While i < dsAngg.Tables(0).Rows.Count
                dtUser.Rows.Add(dsAngg.Tables(0).Rows(i).Item(0), dsAngg.Tables(0).Rows(i).Item(1), dsAngg.Tables(0).Rows(i).Item(2), dsAngg.Tables(0).Rows(i).Item(3))
                i += 1
            End While
        End If
    End Sub

    Private Sub FormUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        maketableUser()
        readUser("", "")
    End Sub

    Private Sub cariUser_TextChanged(sender As Object, e As EventArgs) Handles cariUser.TextChanged
        readUser("AND (a.id_user LIKE '%" + cariUser.Text + "%' OR b.nama_petugas LIKE '%" + cariUser.Text + "%' OR a.username LIKE '%" + cariUser.Text + "%' OR a.level LIKE '%" + cariUser.Text + "%')", "AND (a.id_user LIKE '%" + cariUser.Text + "%' OR b.nama_lengkap LIKE '%" + cariUser.Text + "%' OR a.username LIKE '%" + cariUser.Text + "%' OR a.level LIKE '%" + cariUser.Text + "%')")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        cekKoneksi()

        If (username.Text = "" Or password.Text = "" Or conPassword.Text = "" Or cbLevel.SelectedItem = Nothing Or IDPengguna.Text = "") Then
            MessageBox.Show(Nothing, "Semua Field harus terisi", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If Not (password.Text = conPassword.Text) Then
            MessageBox.Show(Nothing, "Confirm password tidak sama", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim cek As New SqlDataAdapter("SELECT id_user FROM [dbo].[user] WHERE username = '" + username.Text + "' AND password = '" + password.Text + "'", koneksi.connect)
        Dim dsCek As New DataSet
        cek.Fill(dsCek)
        If dsCek.Tables(0).Rows.Count > 0 Then
            MessageBox.Show(Nothing, "Username dan Password sudah terdaftar", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim readPegguna As New SqlDataAdapter("SELECT * FROM [dbo].[petugas] WHERE id_petugas = '" + IDPengguna.Text + "'", koneksi.connect)
        Dim ds As New DataSet
        readPegguna.Fill(ds)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim command As New SqlCommand("INSERT INTO [dbo].[user](username,password,[level]) VALUES(@username,@password,@level)", koneksi.connect)
            command.Parameters.AddWithValue("@username", username.Text)
            command.Parameters.AddWithValue("@password", password.Text)
            command.Parameters.AddWithValue("@level", cbLevel.SelectedItem)
            If command.ExecuteNonQuery() > 0 Then
                Dim readIDPetugas As New SqlDataAdapter("SELECT id_user FROM [dbo].[user] WHERE username = '" + username.Text + "' AND password = '" + password.Text + "'", koneksi.connect)
                Dim dsIDPet As New DataSet
                readIDPetugas.Fill(dsIDPet)
                If dsIDPet.Tables(0).Rows.Count > 0 Then
                    Dim update As New SqlCommand("UPDATE [dbo].[petugas] SET id_user = @id_user WHERE id_petugas = @id_petugas", koneksi.connect)
                    update.Parameters.AddWithValue("@id_user", dsIDPet.Tables(0).Rows(0).Item(0))
                    update.Parameters.AddWithValue("@id_petugas", IDPengguna.Text)
                    update.ExecuteNonQuery()
                End If
            End If
        Else
            Dim readAnggota As New SqlDataAdapter("SELECT * FROM [dbo].[anggota] WHERE id_anggota = '" + IDPengguna.Text + "'", koneksi.connect)
            Dim dsAng As New DataSet
            readAnggota.Fill(dsAng)
            If dsAng.Tables(0).Rows.Count > 0 Then
                Dim command As New SqlCommand("INSERT INTO [dbo].[user](username,password,[level]) VALUES(@username,@password,@level)", koneksi.connect)
                command.Parameters.AddWithValue("@username", username.Text)
                command.Parameters.AddWithValue("@password", password.Text)
                command.Parameters.AddWithValue("@level", cbLevel.SelectedItem)
                If command.ExecuteNonQuery() > 0 Then
                    Dim readIDAnggota As New SqlDataAdapter("SELECT id_user FROM [dbo].[user] WHERE username = '" + username.Text + "' AND password = '" + password.Text + "'", koneksi.connect)
                    Dim dsIDAng As New DataSet
                    readIDAnggota.Fill(dsIDAng)
                    If dsIDAng.Tables(0).Rows.Count > 0 Then
                        Dim update As New SqlCommand("UPDATE [dbo].[anggota] SET id_user = @id_user WHERE id_anggota = @id_anggota", koneksi.connect)
                        update.Parameters.AddWithValue("@id_user", dsIDAng.Tables(0).Rows(0).Item(0))
                        update.Parameters.AddWithValue("@id_anggota", IDPengguna.Text)
                        update.ExecuteNonQuery()
                    End If
                End If
            Else
                MessageBox.Show(Nothing, "ID Pengguna tidak ada", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
        End If
        MessageBox.Show(Nothing, "Berhasil Membuat User", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        readUser("", "")
        kosong()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        cekKoneksi()

        If id = Nothing Then
            MessageBox.Show(Nothing, "Harus memilih user dulu", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If (username.Text = "" Or password.Text = "" Or conPassword.Text = "" Or cbLevel.SelectedItem = Nothing Or IDPengguna.Text = "") Then
            MessageBox.Show(Nothing, "Semua Field harus terisi", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If Not (password.Text = conPassword.Text) Then
            MessageBox.Show(Nothing, "Confirm password tidak sama", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim cek As New SqlDataAdapter("SELECT id_user FROM [dbo].[user] WHERE username = '" + user + "' AND password = '" + pass + "'", koneksi.connect)
        Dim dsCek As New DataSet
        cek.Fill(dsCek)
        If dsCek.Tables(0).Rows.Count > 0 Then
            MessageBox.Show(Nothing, "Username dan Password sudah terdaftar", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim command As New SqlCommand("UPDATE [dbo].[user] SET username = @username, password = @password, [level] = @level WHERE id_user = @id_user", koneksi.connect)
        command.Parameters.AddWithValue("@username", username.Text)
        command.Parameters.AddWithValue("@password", password.Text)
        command.Parameters.AddWithValue("@level", cbLevel.SelectedItem)
        command.Parameters.AddWithValue("@id_user", id)
        If command.ExecuteNonQuery() > 0 Then
            MessageBox.Show(Nothing, "Berhasil Mengubah User", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            readUser("", "")
            kosong()

        Else
            MessageBox.Show(Nothing, "Berhasil Mengubah User", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            readUser("", "")
            kosong()

        End If
    End Sub

    Private Sub dgUser_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgUser.CellContentClick
        IDPengguna.Enabled = False
        id = dgUser.CurrentRow.Cells(0).Value
        user = dgUser.CurrentRow.Cells(2).Value
        pass = getPassword(dgUser.CurrentRow.Cells(0).Value)
        username.Text = dgUser.CurrentRow.Cells(2).Value
        password.Text = getPassword(dgUser.CurrentRow.Cells(0).Value)
        cbLevel.SelectedItem = dgUser.CurrentRow.Cells(3).Value
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        cekKoneksi()

        If id = Nothing Then
            MessageBox.Show(Nothing, "Harus memilih user dulu", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim command As New SqlCommand("DELETE FROM [dbo].[user] WHERE id_user = @id_user", koneksi.connect)
        command.Parameters.AddWithValue("@id_user", id)
        If command.ExecuteNonQuery() > 0 Then
            MessageBox.Show(Nothing, "Berhasil Menghapus User", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            readUser("", "")
            kosong()

        Else
            MessageBox.Show(Nothing, "Berhasil Menghapus User", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            readUser("", "")
            kosong()

        End If
    End Sub

    Private Sub Label2_Click_1(sender As Object, e As EventArgs) Handles Label2.Click
        MenuAdmin.Show()
        Hide()
    End Sub
End Class