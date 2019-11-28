<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormBuku
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dgBuku = New System.Windows.Forms.DataGridView()
        Me.cariBuku = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.penerbitBuku = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.penulisBuku = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.judulBuku = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.kodeBuku = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.deskripsiBuku = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.hargaBuku = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.stokBuku = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cbKat = New System.Windows.Forms.ComboBox()
        Me.cbLok = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.dgBuku, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.DarkSlateGray
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(-1, -1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1365, 93)
        Me.Panel1.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial Rounded MT Bold", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label3.Location = New System.Drawing.Point(572, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(198, 34)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "DATA BUKU"
        '
        'dgBuku
        '
        Me.dgBuku.AllowUserToAddRows = False
        Me.dgBuku.AllowUserToDeleteRows = False
        Me.dgBuku.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgBuku.Location = New System.Drawing.Point(12, 165)
        Me.dgBuku.Name = "dgBuku"
        Me.dgBuku.ReadOnly = True
        Me.dgBuku.RowTemplate.Height = 24
        Me.dgBuku.Size = New System.Drawing.Size(756, 491)
        Me.dgBuku.TabIndex = 5
        '
        'cariBuku
        '
        Me.cariBuku.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cariBuku.Location = New System.Drawing.Point(162, 122)
        Me.cariBuku.Name = "cariBuku"
        Me.cariBuku.Size = New System.Drawing.Size(606, 31)
        Me.cariBuku.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 125)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(121, 23)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Cari Buku : "
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.DarkSlateGray
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button4.Font = New System.Drawing.Font("Arial Rounded MT Bold", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Button4.Location = New System.Drawing.Point(774, 685)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(166, 48)
        Me.Button4.TabIndex = 29
        Me.Button4.Text = "Tambah"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.Maroon
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button3.Font = New System.Drawing.Font("Arial Rounded MT Bold", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Button3.Location = New System.Drawing.Point(1181, 685)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(172, 48)
        Me.Button3.TabIndex = 28
        Me.Button3.Text = "Hapus"
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.DarkCyan
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.Font = New System.Drawing.Font("Arial Rounded MT Bold", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Button1.Location = New System.Drawing.Point(979, 685)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(163, 48)
        Me.Button1.TabIndex = 27
        Me.Button1.Text = "Ubah"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'penerbitBuku
        '
        Me.penerbitBuku.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.penerbitBuku.Location = New System.Drawing.Point(938, 298)
        Me.penerbitBuku.Name = "penerbitBuku"
        Me.penerbitBuku.Size = New System.Drawing.Size(396, 31)
        Me.penerbitBuku.TabIndex = 25
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(797, 301)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(93, 23)
        Me.Label7.TabIndex = 26
        Me.Label7.Text = "Penerbit"
        '
        'penulisBuku
        '
        Me.penulisBuku.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.penulisBuku.Location = New System.Drawing.Point(938, 237)
        Me.penulisBuku.Name = "penulisBuku"
        Me.penulisBuku.Size = New System.Drawing.Size(396, 31)
        Me.penulisBuku.TabIndex = 23
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(797, 240)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 23)
        Me.Label6.TabIndex = 24
        Me.Label6.Text = "Penulis"
        '
        'judulBuku
        '
        Me.judulBuku.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.judulBuku.Location = New System.Drawing.Point(938, 175)
        Me.judulBuku.Name = "judulBuku"
        Me.judulBuku.Size = New System.Drawing.Size(396, 31)
        Me.judulBuku.TabIndex = 21
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(797, 178)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 23)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "Judul"
        '
        'kodeBuku
        '
        Me.kodeBuku.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.kodeBuku.Location = New System.Drawing.Point(938, 112)
        Me.kodeBuku.Name = "kodeBuku"
        Me.kodeBuku.Size = New System.Drawing.Size(396, 31)
        Me.kodeBuku.TabIndex = 19
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(797, 115)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(116, 23)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Kode Buku"
        '
        'deskripsiBuku
        '
        Me.deskripsiBuku.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.deskripsiBuku.Location = New System.Drawing.Point(938, 359)
        Me.deskripsiBuku.Multiline = True
        Me.deskripsiBuku.Name = "deskripsiBuku"
        Me.deskripsiBuku.Size = New System.Drawing.Size(396, 87)
        Me.deskripsiBuku.TabIndex = 30
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(797, 362)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(102, 23)
        Me.Label8.TabIndex = 31
        Me.Label8.Text = "Deskripsi"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(797, 601)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(73, 23)
        Me.Label9.TabIndex = 37
        Me.Label9.Text = "Lokasi"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(797, 540)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(95, 23)
        Me.Label10.TabIndex = 35
        Me.Label10.Text = "Kategori"
        '
        'hargaBuku
        '
        Me.hargaBuku.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.hargaBuku.Location = New System.Drawing.Point(938, 475)
        Me.hargaBuku.Name = "hargaBuku"
        Me.hargaBuku.Size = New System.Drawing.Size(237, 31)
        Me.hargaBuku.TabIndex = 32
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(797, 478)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(82, 23)
        Me.Label11.TabIndex = 33
        Me.Label11.Text = "Harga :"
        '
        'stokBuku
        '
        Me.stokBuku.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.stokBuku.Location = New System.Drawing.Point(1242, 475)
        Me.stokBuku.Name = "stokBuku"
        Me.stokBuku.Size = New System.Drawing.Size(92, 31)
        Me.stokBuku.TabIndex = 38
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(1183, 478)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(53, 23)
        Me.Label12.TabIndex = 39
        Me.Label12.Text = "Stok"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(885, 478)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(43, 23)
        Me.Label13.TabIndex = 40
        Me.Label13.Text = "Rp."
        '
        'cbKat
        '
        Me.cbKat.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbKat.FormattingEnabled = True
        Me.cbKat.Location = New System.Drawing.Point(938, 537)
        Me.cbKat.Name = "cbKat"
        Me.cbKat.Size = New System.Drawing.Size(396, 31)
        Me.cbKat.TabIndex = 41
        '
        'cbLok
        '
        Me.cbLok.Font = New System.Drawing.Font("Arial Rounded MT Bold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbLok.FormattingEnabled = True
        Me.cbLok.Location = New System.Drawing.Point(938, 598)
        Me.cbLok.Name = "cbLok"
        Me.cbLok.Size = New System.Drawing.Size(396, 31)
        Me.cbLok.TabIndex = 42
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial Rounded MT Bold", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.DarkCyan
        Me.Label2.Location = New System.Drawing.Point(13, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 20)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "< BACK"
        '
        'FormBuku
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1364, 752)
        Me.Controls.Add(Me.cbLok)
        Me.Controls.Add(Me.cbKat)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.stokBuku)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.hargaBuku)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.deskripsiBuku)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.penerbitBuku)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.penulisBuku)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.judulBuku)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.kodeBuku)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cariBuku)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgBuku)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FormBuku"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FormBuku"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgBuku, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents dgBuku As DataGridView
    Friend WithEvents cariBuku As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button4 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents penerbitBuku As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents penulisBuku As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents judulBuku As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents kodeBuku As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents deskripsiBuku As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents hargaBuku As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents stokBuku As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents cbKat As ComboBox
    Friend WithEvents cbLok As ComboBox
    Friend WithEvents Label2 As Label
End Class
