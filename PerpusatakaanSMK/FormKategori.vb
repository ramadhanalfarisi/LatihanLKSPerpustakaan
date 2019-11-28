Imports System.Data.SqlClient
Public Class FormKategori
    Dim koneksi As New Connection
    Dim dtKat As New DataTable
    Dim id, namaKategori As String
    Dim angka() As Char = "1234567890".ToCharArray

    Sub cekKoneksi()
        If koneksi.connect.State = ConnectionState.Closed Then
            koneksi.connect.Open()
        End If
    End Sub

    Public Sub kosong()
        nameKat.Text = ""
        cariKat.Text = ""
        id = Nothing
        namaKategori = Nothing
    End Sub

    Sub makeTableKategori()
        dtKat.Columns.Add("ID KATEGORI")
        dtKat.Columns.Add("KATEGORI")
        dgKategori.DataSource = dtKat
    End Sub

    Public Sub readKategori(ByVal where As String)
        cekKoneksi()
        dtKat.Clear()

        Dim command As New SqlDataAdapter("SELECT * FROM [dbo].[kategori] " + where, koneksi.connect)
        Dim dsKat As New DataSet
        command.Fill(dsKat)
        If dsKat.Tables(0).Rows.Count > 0 Then
            Dim i = 0
            While i < dsKat.Tables(0).Rows.Count
                dtKat.Rows.Add(dsKat.Tables(0).Rows(i).Item(0), dsKat.Tables(0).Rows(i).Item(1))
                i += 1
            End While
        End If
    End Sub

    Private Sub FormKategori_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        makeTableKategori()
        readKategori("")
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        MenuAdmin.Show()
        Hide()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        cekKoneksi()

        id = dgKategori.CurrentRow.Cells(0).Value
        If id = Nothing Then
            MessageBox.Show(Nothing, "Harus memilih kategori dahulu", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If nameKat.Text = "" Then
            MessageBox.Show(Nothing, "Semua Field harus terisi", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim i = 0
        While i < angka.Length
            If nameKat.Text.Contains(angka(i)) Then
                MessageBox.Show(Nothing, "Field kategori harus terdiri dari huruf", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            i += 1
        End While

        Dim command As New SqlCommand("UPDATE [dbo].[kategori] SET nama_kat = @namakat WHERE id_kat = @id", koneksi.connect)
        command.Parameters.AddWithValue("@namakat", nameKat.Text)
        command.Parameters.AddWithValue("@id", id)
        If command.ExecuteNonQuery() > 0 Then
            MessageBox.Show(Nothing, "Berhasil mengubah kategori", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            readKategori("")
            kosong()

        Else
            MessageBox.Show(Nothing, "Gagal mengubah kategori", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            readKategori("")
            kosong()

        End If
    End Sub

    Private Sub dgKategori_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgKategori.CellContentClick
        nameKat.Text = dgKategori.CurrentRow.Cells(1).Value
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        cekKoneksi()

        id = dgKategori.CurrentRow.Cells(0).Value
        If id = Nothing Then
            MessageBox.Show(Nothing, "Harus memilih kategori dahulu", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim command As New SqlCommand("DELETE FROM [dbo].[kategori] WHERE id_kat = @id", koneksi.connect)
        command.Parameters.AddWithValue("@id", id)
        If command.ExecuteNonQuery() > 0 Then
            MessageBox.Show(Nothing, "Berhasil Menghapus kategori", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            readKategori("")
            kosong()

        Else
            MessageBox.Show(Nothing, "Gagal menghapus kategori", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            readKategori("")
            kosong()

        End If
    End Sub

    Private Sub cariKat_TextChanged(sender As Object, e As EventArgs) Handles cariKat.TextChanged
        readKategori("WHERE nama_kat LIKE '%" + cariKat.Text + "%'")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        cekKoneksi()

        If nameKat.Text = "" Then
            MessageBox.Show(Nothing, "Semua Field harus terisi", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim i = 0
        While i < angka.Length
            If nameKat.Text.Contains(angka(i)) Then
                MessageBox.Show(Nothing, "Field kategori harus terdiri dari huruf", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            i += 1
        End While

        Dim command As New SqlCommand("INSERT INTO [dbo].[kategori](nama_kat) VALUES(@namakat)", koneksi.connect)
        command.Parameters.AddWithValue("@namakat", nameKat.Text)
        If command.ExecuteNonQuery() > 0 Then
            MessageBox.Show(Nothing, "Berhasil menambah kategori", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            readKategori("")
            kosong()

        Else
            MessageBox.Show(Nothing, "Gagal menambah kategori", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            readKategori("")
            kosong()

        End If
    End Sub

End Class