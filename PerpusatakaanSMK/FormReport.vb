Imports System.Data.SqlClient
Public Class FormReport

    Dim koneksi As New Connection

    Sub cekKoneksi()
        If koneksi.connect.State = ConnectionState.Closed Then
            koneksi.connect.Open()

        End If
    End Sub

    Public Sub readReport()
        cekKoneksi()

        Dim command As New SqlDataAdapter("SELECT COUNT(a.kode_buku) as count, b.judul FROM [dbo].[peminjaman_buku] a INNER JOIN [dbo].[buku] b ON a.kode_buku = b.kode_buku GROUP BY b.judul", koneksi.connect)
        Dim ds As New DataSet
        command.Fill(ds)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim i = 0
            While i < ds.Tables(0).Rows.Count
                chartBuku.Series("Buku").Points.AddXY(ds.Tables(0).Rows(i).Item(1), ds.Tables(0).Rows(i).Item(0))
                i += 1
            End While
        End If

        Dim command2 As New SqlDataAdapter("SELECT COUNT(b.id_anggota) as count, c.nama_lengkap FROM [dbo].[peminjaman_buku] a INNER JOIN [dbo].[peminjaman] b ON a.id_pinjam = b.id_pinjam INNER JOIN [dbo].[anggota] c ON b.id_anggota = c.id_anggota GROUP BY c.nama_lengkap", koneksi.connect)
        Dim ds2 As New DataSet
        command2.Fill(ds2)
        If ds2.Tables(0).Rows.Count > 0 Then
            Dim i = 0
            While i < ds2.Tables(0).Rows.Count
                chartPengunjung.Series("Pengunjung").Points.AddXY(ds2.Tables(0).Rows(i).Item(1), ds2.Tables(0).Rows(i).Item(0))
                i += 1
            End While
        End If
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        MenuAdmin.Show()
        Hide()

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        chartBuku.Visible = True
        chartPengunjung.Visible = False
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        chartBuku.Visible = False
        chartPengunjung.Visible = True
    End Sub

    Private Sub FormReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        chartBuku.Visible = True
        chartPengunjung.Visible = False
    End Sub
End Class