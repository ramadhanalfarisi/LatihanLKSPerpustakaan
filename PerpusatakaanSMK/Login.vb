Imports System.Data.SqlClient
Imports System.IO
Public Class Login

    Dim koneksi As New Connection

    Sub cekKoneksi()
        If koneksi.connect.State = ConnectionState.Closed Then
            koneksi.connect.Open()

        End If
    End Sub

    Function getFoto(ByVal id As String, ByVal level As String)
        cekKoneksi()

        Dim command As New SqlDataAdapter("SELECT foto FROM [dbo].[" + level + "] WHERE id_user = '" + id + "'", koneksi.connect)
        Dim ds As New DataSet
        command.Fill(ds)
        Dim ft As Byte() = CType(ds.Tables(0).Rows(0).Item(0), Byte())
        Dim ms As New MemoryStream(ft)
        Return ms
    End Function

    Sub makeCapt()
        Dim r As New Random
        Dim huruf() As Char = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijkllmnopqrstuvwxyz".ToCharArray
        Dim textCapt As String = ""
        Dim i = 0
        While i < 5
            Dim index = r.Next(0, huruf.Length - 1)
            textCapt &= huruf(index)
            i += 1
        End While
        lblCapt.Text = textCapt
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Close()

    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        makeCapt()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        cekKoneksi()

        If username.Text = "" Or password.Text = "" Then
            MessageBox.Show(Nothing, "Semua field harus terisi", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            makeCapt()
            txtCapt.Text = ""
            Return
        End If

        If txtCapt.Text = "" Then
            MessageBox.Show(Nothing, "Field captcha harus terisi", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            makeCapt()
            txtCapt.Text = ""
            Return
        End If

        If Not lblCapt.Text = txtCapt.Text Then
            MessageBox.Show(Nothing, "Text captcha tidak sama", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            makeCapt()
            txtCapt.Text = ""
            Return
        End If

        Dim command As New SqlDataAdapter("SELECT level, username, id_user FROM [dbo].[user] WHERE username = '" + username.Text + "' AND password = '" + password.Text + "'", koneksi.connect)
        Dim read As New DataSet
        command.Fill(read)
        If read.Tables(0).Rows.Count > 0 Then
            If read.Tables(0).Rows(0).Item(0) = "admin" Or read.Tables(0).Rows(0).Item(0) = "Petugas" Then
                makeCapt()
                MenuAdmin.username = username.Text
                MenuAdmin.password = password.Text
                MenuAdmin.pbPetugas.Image = Image.FromStream(getFoto(read.Tables(0).Rows(0).Item(2), "petugas"))
                MenuAdmin.Show()
                MenuAdmin.lblusername.Text = read.Tables(0).Rows(0).Item(1)
                Hide()
            ElseIf read.Tables(0).Rows(0).Item(0) = "anggota" Or read.Tables(0).Rows(0).Item(0) = "Anggota" Then
                makeCapt()
                MenuAnggota.pbAnggota.Image = Image.FromStream(getFoto(read.Tables(0).Rows(0).Item(2), "anggota"))
                MenuAnggota.Show()
                MenuAnggota.lblusername.Text = read.Tables(0).Rows(0).Item(1)
                Hide()
            End If
        Else
            MessageBox.Show(Nothing, "Akun Salah", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            makeCapt()
            txtCapt.Text = ""
            Return
        End If
    End Sub
End Class
