Imports System.Data.SqlClient
Public Class FormLokasi

    Dim koneksi As New Connection
    Dim dtLokasi As New DataTable
    Dim id As String

    Sub cekKoneksi()
        If koneksi.connect.State = ConnectionState.Closed Then
            koneksi.connect.Open()
        End If
    End Sub

    Sub makeTable()
        dtLokasi.Columns.Add("ID LOKASI")
        dtLokasi.Columns.Add("LABEL")
        dtLokasi.Columns.Add("LANTAI")
        dtLokasi.Columns.Add("RAK")
        dgLokasi.DataSource = dtLokasi
    End Sub

    Public Sub kosong()
        cariLok.Text = ""
        IDLok.Text = ""
        LabelLok.Text = ""
        LantaiLok.Text = ""
        RakLok.Text = ""
        id = Nothing
    End Sub

    Public Sub readLokasi(ByVal where As String)
        cekKoneksi()
        dtLokasi.Clear()

        Dim command As New SqlDataAdapter("SELECT * FROM [dbo].[lokasi] " + where, koneksi.connect)
        Dim dsLokasi As New DataSet
        command.Fill(dsLokasi)
        If dsLokasi.Tables(0).Rows.Count > 0 Then
            Dim i = 0
            While i < dsLokasi.Tables(0).Rows.Count
                dtLokasi.Rows.Add(dsLokasi.Tables(0).Rows(i).Item(0), dsLokasi.Tables(0).Rows(i).Item(1), dsLokasi.Tables(0).Rows(i).Item(2), dsLokasi.Tables(0).Rows(i).Item(3))
                i += 1
            End While
        End If
    End Sub
    Private Sub FormLokasi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        makeTable()
        readLokasi("")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        cekKoneksi()
        If IDLok.Text = "" Or LabelLok.Text = "" Or LantaiLok.Text = "" Or RakLok.Text = "" Then
            MessageBox.Show(Nothing, "Semua field harus terisi", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Try
            Integer.Parse(LantaiLok.Text)
        Catch ex As Exception
            MessageBox.Show(Nothing, "Field Lantai harus tersiri dari angka", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try
        Dim command As New SqlCommand("INSERT INTO [dbo].[lokasi](kode_lokasi,label,lantai,rak) VALUES(@kode_lokasi,@label,@lantai,@rak)", koneksi.connect)
        command.Parameters.AddWithValue("@kode_lokasi", IDLok.Text)
        command.Parameters.AddWithValue("@label", LabelLok.Text)
        command.Parameters.AddWithValue("@lantai", LantaiLok.Text)
        command.Parameters.AddWithValue("@rak", RakLok.Text)
        Try
            If command.ExecuteNonQuery() > 0 Then
                MessageBox.Show(Nothing, "Berhasil menambah lokasi", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                readLokasi("")
                kosong()

            Else
                MessageBox.Show(Nothing, "Gagal menambah lokasi", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                readLokasi("")
                kosong()

            End If
        Catch ex As Exception
            MessageBox.Show(Nothing, "ID Lokasi sudah terpakai", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        cekKoneksi()

        If id = Nothing Then
            MessageBox.Show(Nothing, "Harus memilih lokasi dahulu", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If IDLok.Text = "" Or LabelLok.Text = "" Or LantaiLok.Text = "" Or RakLok.Text = "" Then
            MessageBox.Show(Nothing, "Semua field harus terisi", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Try
            Integer.Parse(LantaiLok.Text)
        Catch ex As Exception
            MessageBox.Show(Nothing, "Field Lantai harus tersiri dari angka", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try
        Dim command As New SqlCommand("UPDATE [dbo].[lokasi] SET kode_lokasi = @kodebaru,label = @label,lantai = @lantai,rak = @rak WHERE kode_lokasi = @kode_lokasi", koneksi.connect)
        command.Parameters.AddWithValue("@kode_lokasi", id)
        command.Parameters.AddWithValue("@kodebaru", IDLok.Text)
        command.Parameters.AddWithValue("@label", LabelLok.Text)
        command.Parameters.AddWithValue("@lantai", LantaiLok.Text)
        command.Parameters.AddWithValue("@rak", RakLok.Text)
        Try
            If command.ExecuteNonQuery() > 0 Then
                MessageBox.Show(Nothing, "Berhasil mengubah lokasi", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                readLokasi("")
                kosong()

            Else
                MessageBox.Show(Nothing, "Gagal mengubah lokasi", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                readLokasi("")
                kosong()

            End If
        Catch ex As Exception
            MessageBox.Show(Nothing, "ID Lokasi sudah terpakai", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

    End Sub

    Private Sub dgLokasi_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgLokasi.CellContentClick
        id = dgLokasi.CurrentRow.Cells(0).Value
        IDLok.Text = dgLokasi.CurrentRow.Cells(0).Value
        LabelLok.Text = dgLokasi.CurrentRow.Cells(1).Value
        LantaiLok.Text = dgLokasi.CurrentRow.Cells(2).Value
        RakLok.Text = dgLokasi.CurrentRow.Cells(3).Value
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        cekKoneksi()

        If id = Nothing Then
            MessageBox.Show(Nothing, "Harus memilih lokasi dahulu", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim command As New SqlCommand("DELETE FROM [dbo].[lokasi] WHERE kode_lokasi = @kode_lokasi", koneksi.connect)
        command.Parameters.AddWithValue("@kode_lokasi", id)

        If command.ExecuteNonQuery() > 0 Then
            MessageBox.Show(Nothing, "Berhasil menghapus lokasi", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            readLokasi("")
            kosong()

        Else
            MessageBox.Show(Nothing, "Gagal menghapus lokasi", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            readLokasi("")
            kosong()

        End If
    End Sub

    Private Sub cariLok_TextChanged(sender As Object, e As EventArgs) Handles cariLok.TextChanged
        readLokasi("WHERE kode_lokasi LIKE '%" + cariLok.Text + "%' OR label LIKE '%" + cariLok.Text + "%' OR kode_lokasi LIKE '%" + cariLok.Text + "%' OR kode_lokasi LIKE '%" + cariLok.Text + "%'")
    End Sub

    Private Sub Label2_Click_1(sender As Object, e As EventArgs) Handles Label2.Click
        MenuAdmin.Show()
        Hide()
    End Sub
End Class