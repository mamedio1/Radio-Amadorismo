Public Class FormSobre
    Public Sub New()
        InitializeComponent()
        CriarControles()
        AplicarEstiloCyberpunk()
    End Sub

    Private Sub CriarControles()
        Me.Text = "Sobre o Autor"
        Me.Size = New Size(600, 550)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.BackColor = Color.FromArgb(20, 20, 40)

        ' Título
        Dim lblTitulo As New Label()
        lblTitulo.Text = "RF CALCULATOR"
        lblTitulo.Location = New Point(30, 20)
        lblTitulo.Size = New Size(540, 35)
        lblTitulo.Font = New Font("Courier New", 20, FontStyle.Bold)
        lblTitulo.TextAlign = ContentAlignment.MiddleCenter
        lblTitulo.ForeColor = Color.FromArgb(150, 255, 150)
        Me.Controls.Add(lblTitulo)

        ' Subtítulo
        Dim lblSubtitulo As New Label()
        lblSubtitulo.Text = "Sistema de Cálculos Avançados para RF"
        lblSubtitulo.Location = New Point(30, 60)
        lblSubtitulo.Size = New Size(540, 20)
        lblSubtitulo.Font = New Font("Segoe UI", 11, FontStyle.Italic)
        lblSubtitulo.TextAlign = ContentAlignment.MiddleCenter
        lblSubtitulo.ForeColor = Color.FromArgb(100, 200, 255)
        Me.Controls.Add(lblSubtitulo)

        ' PictureBox para a foto
        Dim pictureBoxFoto As New PictureBox()
        pictureBoxFoto.Location = New Point(200, 100)
        pictureBoxFoto.Size = New Size(200, 200)
        pictureBoxFoto.BorderStyle = BorderStyle.FixedSingle
        pictureBoxFoto.SizeMode = PictureBoxSizeMode.Zoom
        pictureBoxFoto.BackColor = Color.FromArgb(30, 30, 60)
        pictureBoxFoto.Name = "pictureBoxFoto"

        ' Criar uma imagem placeholder
        ' Tentar carregar foto do disco
        Try
            pictureBoxFoto.Image = Image.FromFile("c:\Robson.JPG")
        Catch ex As Exception
            ' Se não encontrar a foto, cria um placeholder
            Dim bmp As New Bitmap(200, 200)
            Dim g As Graphics = Graphics.FromImage(bmp)
            g.Clear(Color.FromArgb(30, 30, 60))
            g.DrawString("Foto não" & vbCrLf & "encontrada" & vbCrLf & vbCrLf & "Coloque em:" & vbCrLf & "C:\foto.jpg",
                New Font("Segoe UI", 10, FontStyle.Italic),
                New SolidBrush(Color.FromArgb(200, 100, 100)),
                New RectangleF(20, 40, 160, 120))
            pictureBoxFoto.Image = bmp
        End Try

        Me.Controls.Add(pictureBoxFoto)

        ' Versão
        Dim lblVersao As New Label()
        lblVersao.Text = "Versão 1.0 - 2025"
        lblVersao.Location = New Point(30, 310)
        lblVersao.Size = New Size(540, 20)
        lblVersao.Font = New Font("Segoe UI", 9)
        lblVersao.TextAlign = ContentAlignment.MiddleCenter
        lblVersao.ForeColor = Color.Gray
        Me.Controls.Add(lblVersao)

        ' Separador
        Dim separator As New Label()
        separator.Location = New Point(100, 340)
        separator.Size = New Size(400, 2)
        separator.BorderStyle = BorderStyle.FixedSingle
        separator.BackColor = Color.FromArgb(100, 100, 150)
        Me.Controls.Add(separator)

        ' Informações do Autor
        Dim lblAutor As New Label()
        lblAutor.Text = "Desenvolvido por: Robson Mamedio Araujo"
        lblAutor.Location = New Point(30, 355)
        lblAutor.Size = New Size(540, 25)
        lblAutor.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        lblAutor.TextAlign = ContentAlignment.MiddleCenter
        lblAutor.ForeColor = Color.FromArgb(150, 255, 150)
        Me.Controls.Add(lblAutor)

        ' Informações adicionais
        Dim lblInfo As New Label()
        lblInfo.Text = "Tecnólogo em Telecomunicações e Eletrônica" & vbCrLf &
                      "Especialista em Antenas e Propagação" & vbCrLf & vbCrLf &
                      "📧 Email: mamedio1@outlook.com" & vbCrLf &
                      "💼 LinkedIn: https://www.linkedin.com/in/robson-mamedio-de-araujo-33433931/"
        lblInfo.Location = New Point(30, 390)
        lblInfo.Size = New Size(540, 90)
        lblInfo.Font = New Font("Segoe UI", 9)
        lblInfo.TextAlign = ContentAlignment.MiddleCenter
        lblInfo.ForeColor = Color.FromArgb(100, 200, 255)
        Me.Controls.Add(lblInfo)

        ' Botão Fechar
        Dim btnFechar As New Button()
        btnFechar.Text = "Fechar"
        btnFechar.Location = New Point(250, 485)
        btnFechar.Size = New Size(100, 30)
        btnFechar.BackColor = Color.FromArgb(200, 100, 255)
        btnFechar.ForeColor = Color.White
        btnFechar.FlatStyle = FlatStyle.Flat
        btnFechar.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        btnFechar.FlatAppearance.BorderColor = Color.FromArgb(150, 150, 255)
        btnFechar.FlatAppearance.BorderSize = 2
        AddHandler btnFechar.Click, AddressOf BtnFechar_Click
        Me.Controls.Add(btnFechar)
    End Sub

    Private Sub AplicarEstiloCyberpunk()
        ' Já aplicado em CriarControles
    End Sub

    Private Sub BtnFechar_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub
End Class