Public Class FormBobinaToroidal
    Private txtNumeroEspiras As TextBox
    Private txtDiametroExterno As TextBox
    Private txtDiametroInterno As TextBox
    Private txtAltura As TextBox
    Private txtPermeabilidade As TextBox
    Private txtResultado As TextBox
    Private btnCalcular As Button
    Private panelDesenho As Panel
    Private cmbMaterial As ComboBox
    Private cmbTipoCalculo As ComboBox

    Public Sub New()
        InitializeComponent()
        ConfigurarFormulario()
        AplicarEstiloCyberpunk()
    End Sub

    Private Sub ConfigurarFormulario()
        Me.Text = "Calculadora de Bobina Toroidal"
        Me.Size = New Size(750, 750)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False

        ' Título
        Dim lblTitulo As New Label()
        lblTitulo.Text = "CALCULADORA DE BOBINA TOROIDAL"
        lblTitulo.Location = New Point(30, 20)
        lblTitulo.Size = New Size(500, 20)
        lblTitulo.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        Me.Controls.Add(lblTitulo)

        ' Tipo de Cálculo
        Dim lblTipoCalculo As New Label()
        lblTipoCalculo.Text = "Tipo de Cálculo:"
        lblTipoCalculo.Location = New Point(30, 55)
        lblTipoCalculo.Size = New Size(140, 20)
        Me.Controls.Add(lblTipoCalculo)

        cmbTipoCalculo = New ComboBox()
        cmbTipoCalculo.Location = New Point(180, 53)
        cmbTipoCalculo.Size = New Size(250, 20)
        cmbTipoCalculo.DropDownStyle = ComboBoxStyle.DropDownList
        cmbTipoCalculo.Items.Add("Calcular Indutância (L)")
        cmbTipoCalculo.Items.Add("Calcular Espiras (N)")
        cmbTipoCalculo.SelectedIndex = 0
        AddHandler cmbTipoCalculo.SelectedIndexChanged, AddressOf CmbTipoCalculo_Changed
        Me.Controls.Add(cmbTipoCalculo)

        ' Material do Núcleo
        Dim lblMaterial As New Label()
        lblMaterial.Text = "Material Núcleo:"
        lblMaterial.Location = New Point(30, 90)
        lblMaterial.Size = New Size(140, 20)
        Me.Controls.Add(lblMaterial)

        cmbMaterial = New ComboBox()
        cmbMaterial.Location = New Point(180, 88)
        cmbMaterial.Size = New Size(250, 20)
        cmbMaterial.DropDownStyle = ComboBoxStyle.DropDownList
        cmbMaterial.Items.Add("Ar (µr = 1)")
        cmbMaterial.Items.Add("Ferrite MnZn (µr = 2000-3000)")
        cmbMaterial.Items.Add("Ferrite NiZn (µr = 100-500)")
        cmbMaterial.Items.Add("Ferro em Pó (µr = 10-100)")
        cmbMaterial.Items.Add("Ferro Silício (µr = 1500)")
        cmbMaterial.Items.Add("Personalizado")
        cmbMaterial.SelectedIndex = 1
        AddHandler cmbMaterial.SelectedIndexChanged, AddressOf CmbMaterial_Changed
        Me.Controls.Add(cmbMaterial)

        ' Número de Espiras
        Dim lblNumeroEspiras As New Label()
        lblNumeroEspiras.Text = "Número Espiras:"
        lblNumeroEspiras.Location = New Point(30, 130)
        lblNumeroEspiras.Size = New Size(140, 20)
        Me.Controls.Add(lblNumeroEspiras)

        txtNumeroEspiras = New TextBox()
        txtNumeroEspiras.Location = New Point(180, 128)
        txtNumeroEspiras.Size = New Size(150, 20)
        txtNumeroEspiras.Text = "20"
        AddHandler txtNumeroEspiras.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtNumeroEspiras)

        Dim lblUnidadeN As New Label()
        lblUnidadeN.Text = "voltas"
        lblUnidadeN.Location = New Point(340, 130)
        lblUnidadeN.Size = New Size(50, 20)
        Me.Controls.Add(lblUnidadeN)

        ' Diâmetro Externo
        Dim lblDiametroExterno As New Label()
        lblDiametroExterno.Text = "Diâmetro Externo:"
        lblDiametroExterno.Location = New Point(30, 170)
        lblDiametroExterno.Size = New Size(140, 20)
        Me.Controls.Add(lblDiametroExterno)

        txtDiametroExterno = New TextBox()
        txtDiametroExterno.Location = New Point(180, 168)
        txtDiametroExterno.Size = New Size(150, 20)
        txtDiametroExterno.Text = "40"
        AddHandler txtDiametroExterno.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtDiametroExterno)

        Dim lblUnidadeDE As New Label()
        lblUnidadeDE.Text = "mm"
        lblUnidadeDE.Location = New Point(340, 170)
        lblUnidadeDE.Size = New Size(30, 20)
        Me.Controls.Add(lblUnidadeDE)

        ' Diâmetro Interno
        Dim lblDiametroInterno As New Label()
        lblDiametroInterno.Text = "Diâmetro Interno:"
        lblDiametroInterno.Location = New Point(30, 210)
        lblDiametroInterno.Size = New Size(140, 20)
        Me.Controls.Add(lblDiametroInterno)

        txtDiametroInterno = New TextBox()
        txtDiametroInterno.Location = New Point(180, 208)
        txtDiametroInterno.Size = New Size(150, 20)
        txtDiametroInterno.Text = "25"
        AddHandler txtDiametroInterno.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtDiametroInterno)

        Dim lblUnidadeDI As New Label()
        lblUnidadeDI.Text = "mm"
        lblUnidadeDI.Location = New Point(340, 210)
        lblUnidadeDI.Size = New Size(30, 20)
        Me.Controls.Add(lblUnidadeDI)

        ' Altura
        Dim lblAltura As New Label()
        lblAltura.Text = "Altura (h):"
        lblAltura.Location = New Point(30, 250)
        lblAltura.Size = New Size(140, 20)
        Me.Controls.Add(lblAltura)

        txtAltura = New TextBox()
        txtAltura.Location = New Point(180, 248)
        txtAltura.Size = New Size(150, 20)
        txtAltura.Text = "15"
        AddHandler txtAltura.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtAltura)

        Dim lblUnidadeH As New Label()
        lblUnidadeH.Text = "mm"
        lblUnidadeH.Location = New Point(340, 250)
        lblUnidadeH.Size = New Size(30, 20)
        Me.Controls.Add(lblUnidadeH)

        ' Permeabilidade Relativa
        Dim lblPermeabilidade As New Label()
        lblPermeabilidade.Text = "Permeab. Relativa:"
        lblPermeabilidade.Location = New Point(30, 290)
        lblPermeabilidade.Size = New Size(140, 20)
        Me.Controls.Add(lblPermeabilidade)

        txtPermeabilidade = New TextBox()
        txtPermeabilidade.Location = New Point(180, 288)
        txtPermeabilidade.Size = New Size(150, 20)
        txtPermeabilidade.Text = "2500"
        txtPermeabilidade.Enabled = False
        AddHandler txtPermeabilidade.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtPermeabilidade)

        Dim lblUnidadePerm As New Label()
        lblUnidadePerm.Text = "µr"
        lblUnidadePerm.Location = New Point(340, 290)
        lblUnidadePerm.Size = New Size(30, 20)
        Me.Controls.Add(lblUnidadePerm)

        ' Botão Calcular
        btnCalcular = New Button()
        btnCalcular.Text = "Calcular e Desenhar"
        btnCalcular.Location = New Point(200, 330)
        btnCalcular.Size = New Size(150, 35)
        AddHandler btnCalcular.Click, AddressOf BtnCalcular_Click
        Me.Controls.Add(btnCalcular)

        ' Painel para o desenho
        panelDesenho = New Panel()
        panelDesenho.Location = New Point(30, 380)
        panelDesenho.Size = New Size(680, 220)
        panelDesenho.BorderStyle = BorderStyle.FixedSingle
        AddHandler panelDesenho.Paint, AddressOf PanelDesenho_Paint
        Me.Controls.Add(panelDesenho)

        ' Resultado
        Dim lblResultado As New Label()
        lblResultado.Text = "Resultado:"
        lblResultado.Location = New Point(30, 615)
        lblResultado.Size = New Size(120, 20)
        Me.Controls.Add(lblResultado)

        txtResultado = New TextBox()
        txtResultado.Location = New Point(30, 640)
        txtResultado.Size = New Size(680, 70)
        txtResultado.Multiline = True
        txtResultado.ReadOnly = True
        txtResultado.ScrollBars = ScrollBars.Vertical
        Me.Controls.Add(txtResultado)
    End Sub

    Private Sub AplicarEstiloCyberpunk()
        Me.BackColor = Color.FromArgb(20, 20, 40)

        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is Label Then
                ctrl.ForeColor = Color.FromArgb(100, 200, 255)
                ctrl.Font = New Font("Segoe UI", 9, FontStyle.Bold)
            ElseIf TypeOf ctrl Is TextBox Then
                Dim txt As TextBox = DirectCast(ctrl, TextBox)
                If Not txt.ReadOnly Then
                    txt.BackColor = Color.FromArgb(30, 30, 60)
                    txt.ForeColor = Color.FromArgb(150, 255, 150)
                Else
                    txt.BackColor = Color.FromArgb(10, 10, 25)
                    txt.ForeColor = Color.FromArgb(120, 220, 255)
                End If
                txt.Font = New Font("Consolas", 9)
                txt.BorderStyle = BorderStyle.FixedSingle
            ElseIf TypeOf ctrl Is Button Then
                Dim btn As Button = DirectCast(ctrl, Button)
                btn.BackColor = Color.FromArgb(200, 100, 255)
                btn.ForeColor = Color.White
                btn.FlatStyle = FlatStyle.Flat
                btn.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                btn.FlatAppearance.BorderColor = Color.FromArgb(150, 150, 255)
                btn.FlatAppearance.BorderSize = 2
            ElseIf TypeOf ctrl Is ComboBox Then
                Dim cmb As ComboBox = DirectCast(ctrl, ComboBox)
                cmb.BackColor = Color.FromArgb(30, 30, 60)
                cmb.ForeColor = Color.FromArgb(150, 255, 150)
                cmb.Font = New Font("Consolas", 9)
            ElseIf TypeOf ctrl Is Panel Then
                ctrl.BackColor = Color.FromArgb(15, 15, 30)
            End If
        Next
    End Sub

    Private Sub CmbMaterial_Changed(sender As Object, e As EventArgs)
        Select Case cmbMaterial.SelectedIndex
            Case 0 ' Ar
                txtPermeabilidade.Text = "1"
                txtPermeabilidade.Enabled = False
            Case 1 ' Ferrite MnZn
                txtPermeabilidade.Text = "2500"
                txtPermeabilidade.Enabled = False
            Case 2 ' Ferrite NiZn
                txtPermeabilidade.Text = "300"
                txtPermeabilidade.Enabled = False
            Case 3 ' Ferro em Pó
                txtPermeabilidade.Text = "50"
                txtPermeabilidade.Enabled = False
            Case 4 ' Ferro Silício
                txtPermeabilidade.Text = "1500"
                txtPermeabilidade.Enabled = False
            Case 5 ' Personalizado
                txtPermeabilidade.Enabled = True
        End Select
    End Sub

    Private Sub CmbTipoCalculo_Changed(sender As Object, e As EventArgs)
        ' Ajustar interface baseado no tipo de cálculo
        If cmbTipoCalculo.SelectedIndex = 1 Then
            ' Calcular Espiras - precisa da indutância desejada
            ' Aqui você pode adicionar um campo extra se quiser
        End If
    End Sub

    Private Sub ValidarEntrada(sender As Object, e As KeyPressEventArgs)
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c AndAlso e.KeyChar <> ","c AndAlso e.KeyChar <> ChrW(Keys.Back) Then
            e.Handled = True
            Return
        End If

        If e.KeyChar = ","c Then
            e.Handled = True
            Dim txt As TextBox = DirectCast(sender, TextBox)
            If Not txt.Text.Contains(".") Then
                txt.Text = txt.Text.Insert(txt.SelectionStart, ".")
                txt.SelectionStart = txt.Text.Length
            End If
            Return
        End If

        Dim txtBox As TextBox = DirectCast(sender, TextBox)
        If e.KeyChar = "."c AndAlso txtBox.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    ' Variáveis para armazenar dados do desenho
    Private numeroEspiras As Integer = 0
    Private diametroExterno As Double = 0
    Private diametroInterno As Double = 0
    Private altura As Double = 0
    Private permeabilidade As Double = 0
    Private indutanciaCalculada As Double = 0
    Private AL As Double = 0

    Private Sub BtnCalcular_Click(sender As Object, e As EventArgs)
        Try
            If String.IsNullOrWhiteSpace(txtNumeroEspiras.Text) OrElse
               String.IsNullOrWhiteSpace(txtDiametroExterno.Text) OrElse
               String.IsNullOrWhiteSpace(txtDiametroInterno.Text) OrElse
               String.IsNullOrWhiteSpace(txtAltura.Text) OrElse
               String.IsNullOrWhiteSpace(txtPermeabilidade.Text) Then
                MessageBox.Show("Por favor, preencha todos os campos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim nText As String = txtNumeroEspiras.Text.Replace(",", ".")
            Dim deText As String = txtDiametroExterno.Text.Replace(",", ".")
            Dim diText As String = txtDiametroInterno.Text.Replace(",", ".")
            Dim hText As String = txtAltura.Text.Replace(",", ".")
            Dim permText As String = txtPermeabilidade.Text.Replace(",", ".")

            numeroEspiras = CInt(Double.Parse(nText, System.Globalization.CultureInfo.InvariantCulture))
            diametroExterno = Double.Parse(deText, System.Globalization.CultureInfo.InvariantCulture) / 1000.0 ' mm para m
            diametroInterno = Double.Parse(diText, System.Globalization.CultureInfo.InvariantCulture) / 1000.0
            altura = Double.Parse(hText, System.Globalization.CultureInfo.InvariantCulture) / 1000.0
            permeabilidade = Double.Parse(permText, System.Globalization.CultureInfo.InvariantCulture)

            If numeroEspiras <= 0 OrElse diametroExterno <= diametroInterno OrElse altura <= 0 OrElse permeabilidade <= 0 Then
                MessageBox.Show("Valores inválidos. Verifique: N>0, DE>DI, h>0, µr>0", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Calcular parâmetros do toróide
            Dim raioExterno As Double = diametroExterno / 2.0
            Dim raioInterno As Double = diametroInterno / 2.0

            ' Raio médio do caminho magnético
            Dim raioMedio As Double = (raioExterno + raioInterno) / 2.0

            ' Comprimento do caminho magnético
            Dim comprimentoCaminho As Double = 2.0 * Math.PI * raioMedio

            ' Área da seção transversal
            Dim areaSecao As Double = ((raioExterno - raioInterno) * altura)

            ' Permeabilidade do vácuo
            Dim mu0 As Double = 4.0 * Math.PI * 0.0000001 ' H/m

            ' Fórmula da indutância para toróide:
            ' L = (µ₀ × µr × N² × A) / l
            ' Onde: N = número de espiras, A = área da seção, l = comprimento do caminho

            indutanciaCalculada = (mu0 * permeabilidade * numeroEspiras * numeroEspiras * areaSecao) / comprimentoCaminho

            ' Fator AL (indutância por espira ao quadrado) em nH/N²
            AL = (indutanciaCalculada / (numeroEspiras * numeroEspiras)) * 1000000000.0

            ' Volume do núcleo
            Dim volumeNucleo As Double = Math.PI * altura * (raioExterno * raioExterno - raioInterno * raioInterno)

            ' Comprimento do fio necessário
            Dim circunferenciaMedia As Double = 2.0 * Math.PI * raioMedio
            Dim comprimentoFio As Double = circunferenciaMedia * numeroEspiras

            ' Formatar resultado
            Dim resultado As New System.Text.StringBuilder()
            resultado.AppendLine("=== BOBINA TOROIDAL - RESULTADOS ===")
            resultado.AppendLine()
            resultado.AppendLine("Material: " & cmbMaterial.Text)
            resultado.AppendLine("Permeabilidade Relativa (µr): " & permeabilidade.ToString("F0"))
            resultado.AppendLine()
            resultado.AppendLine("DIMENSÕES:")
            resultado.AppendLine("  • Diâmetro Externo: " & (diametroExterno * 1000).ToString("F2") & " mm")
            resultado.AppendLine("  • Diâmetro Interno: " & (diametroInterno * 1000).ToString("F2") & " mm")
            resultado.AppendLine("  • Altura: " & (altura * 1000).ToString("F2") & " mm")
            resultado.AppendLine("  • Número de Espiras: " & numeroEspiras.ToString())
            resultado.AppendLine()
            resultado.AppendLine("RESULTADOS:")
            resultado.AppendLine("  • INDUTÂNCIA: " & FormatarIndutancia(indutanciaCalculada))
            resultado.AppendLine("  • Fator AL: " & AL.ToString("F1") & " nH/N²")
            resultado.AppendLine("  • Comprimento do fio: " & comprimentoFio.ToString("F3") & " m")
            resultado.AppendLine("  • Volume do núcleo: " & (volumeNucleo * 1000000000).ToString("F2") & " cm³")
            resultado.AppendLine("  • Caminho magnético: " & (comprimentoCaminho * 1000).ToString("F2") & " mm")
            resultado.AppendLine()
            resultado.AppendLine("NOTA: Para calcular espiras necessárias para L desejada:")
            resultado.AppendLine("      N = √(L / AL), onde L está em nH")

            txtResultado.Text = resultado.ToString()

            ' Redesenhar
            panelDesenho.Invalidate()

        Catch ex As FormatException
            MessageBox.Show("Formato de número inválido. Use ponto como separador decimal.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Erro ao calcular: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PanelDesenho_Paint(sender As Object, e As PaintEventArgs)
        If diametroExterno = 0 Then
            Dim g As Graphics = e.Graphics
            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

            Dim fonte As New Font("Segoe UI", 12, FontStyle.Italic)
            Dim texto As String = "Clique em 'Calcular e Desenhar' para ver o diagrama do toróide"
            Dim tamanhoTexto As SizeF = g.MeasureString(texto, fonte)
            Dim x As Single = (panelDesenho.Width - tamanhoTexto.Width) / 2
            Dim y As Single = (panelDesenho.Height - tamanhoTexto.Height) / 2

            g.DrawString(texto, fonte, New SolidBrush(Color.FromArgb(100, 150, 200)), x, y)
            Return
        End If

        DesenharToroide(e.Graphics)
    End Sub

    Private Sub DesenharToroide(g As Graphics)
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Dim largura As Integer = panelDesenho.Width
        Dim altura As Integer = panelDesenho.Height
        Dim centroX As Single = largura / 2.0F
        Dim centroY As Single = altura / 2.0F

        ' Calcular escala
        Dim escala As Single = 150.0F / CSng(diametroExterno * 1000)
        Dim raioExternoPixels As Single = CSng(diametroExterno * 1000 * escala / 2)
        Dim raioInternoPixels As Single = CSng(diametroInterno * 1000 * escala / 2)

        ' Cores
        Dim corNucleo As New SolidBrush(Color.FromArgb(100, 80, 60, 40))
        Dim corBordaNucleo As New Pen(Color.FromArgb(150, 120, 100), 2)
        Dim corFio As New Pen(Color.FromArgb(255, 200, 50), 3)
        Dim corTexto As New SolidBrush(Color.FromArgb(150, 255, 150))
        Dim corDimensao As New Pen(Color.FromArgb(100, 200, 255), 2)
        corDimensao.EndCap = Drawing2D.LineCap.ArrowAnchor

        Dim fonte As New Font("Consolas", 8, FontStyle.Bold)
        Dim fonteGrande As New Font("Consolas", 9, FontStyle.Bold)

        ' Desenhar núcleo toroidal (anel)
        Using path As New Drawing2D.GraphicsPath()
            path.AddEllipse(centroX - raioExternoPixels, centroY - raioExternoPixels,
                           raioExternoPixels * 2, raioExternoPixels * 2)
            path.AddEllipse(centroX - raioInternoPixels, centroY - raioInternoPixels,
                           raioInternoPixels * 2, raioInternoPixels * 2)

            g.FillPath(corNucleo, path)
            g.DrawEllipse(corBordaNucleo, centroX - raioExternoPixels, centroY - raioExternoPixels,
                         raioExternoPixels * 2, raioExternoPixels * 2)
            g.DrawEllipse(corBordaNucleo, centroX - raioInternoPixels, centroY - raioInternoPixels,
                         raioInternoPixels * 2, raioInternoPixels * 2)
        End Using

        ' Desenhar espiras (simplificado - algumas linhas representativas)
        Dim numLinhasVisiveis As Integer = Math.Min(numeroEspiras, 12)
        Dim anguloIncremento As Single = 360.0F / numLinhasVisiveis

        For i As Integer = 0 To numLinhasVisiveis - 1
            Dim angulo As Single = i * anguloIncremento
            Dim anguloRad As Double = angulo * Math.PI / 180.0

            Dim raioMedio As Single = (raioExternoPixels + raioInternoPixels) / 2
            Dim x1 As Single = CSng(centroX + raioInternoPixels * Math.Cos(anguloRad))
            Dim y1 As Single = CSng(centroY + raioInternoPixels * Math.Sin(anguloRad))
            Dim x2 As Single = CSng(centroX + raioExternoPixels * Math.Cos(anguloRad))
            Dim y2 As Single = CSng(centroY + raioExternoPixels * Math.Sin(anguloRad))

            g.DrawLine(corFio, x1, y1, x2, y2)
        Next

        ' Dimensões - Diâmetro Externo
        Dim yDimensao As Single = centroY + raioExternoPixels + 30
        g.DrawLine(corDimensao, centroX - raioExternoPixels, yDimensao,
                  centroX + raioExternoPixels, yDimensao)

        Dim textoDe As String = (diametroExterno * 1000).ToString("F1") & " mm"
        g.DrawString(textoDe, fonteGrande, corTexto, centroX - 30, yDimensao + 5)
        g.DrawString("D.Externo", fonte, corTexto, centroX - 30, yDimensao + 20)

        ' Dimensão - Diâmetro Interno
        Dim xDimensao As Single = centroX + raioExternoPixels + 30
        g.DrawLine(corDimensao, xDimensao, centroY - raioInternoPixels,
                  xDimensao, centroY + raioInternoPixels)

        Dim textoDi As String = (diametroInterno * 1000).ToString("F1") & " mm"
        g.DrawString(textoDi, fonte, corTexto, xDimensao + 5, centroY - 20)
        g.DrawString("D.Interno", fonte, corTexto, xDimensao + 5, centroY - 5)

        ' Informações
        g.DrawString("Material: " & cmbMaterial.Text, fonte,
                    New SolidBrush(Color.FromArgb(200, 200, 255)), 10.0F, 10.0F)
        g.DrawString("µr = " & permeabilidade.ToString("F0"), fonte,
                    New SolidBrush(Color.FromArgb(255, 200, 100)), 10.0F, 25.0F)
        g.DrawString("N = " & numeroEspiras.ToString() & " espiras", fonte,
        New SolidBrush(Color.FromArgb(150, 255, 150)), 10.0F, 40.0F)
        g.DrawString("L = " & FormatarIndutancia(indutanciaCalculada), fonte,
        New SolidBrush(Color.FromArgb(255, 150, 255)), 10.0F, 55.0F)
        g.DrawString("AL = " & AL.ToString("F1") & " nH/N²", fonte,
        New SolidBrush(Color.FromArgb(150, 200, 255)), 10.0F, 70.0F)
        ' Vista lateral (esquemática)
        Dim xVista As Single = CSng(largura - 120)
        Dim yVista As Single = 80.0F
        Dim larguraVista As Single = 60.0F
        Dim alturaVista As Single = CSng(altura * 1000 * escala)

        g.DrawString("Vista", fonte, New SolidBrush(Color.FromArgb(150, 200, 255)), xVista - 20, yVista - 30)
        g.DrawString("Lateral", fonte, New SolidBrush(Color.FromArgb(150, 200, 255)), xVista - 25, yVista - 15)

        ' Desenhar retângulo representando a seção
        g.FillRectangle(corNucleo, xVista - larguraVista / 2, yVista - alturaVista / 2,
                   larguraVista, alturaVista)
        g.DrawRectangle(corBordaNucleo, xVista - larguraVista / 2, yVista - alturaVista / 2,
                   larguraVista, alturaVista)

        ' Dimensão altura
        g.DrawLine(corDimensao, xVista + larguraVista / 2 + 10, yVista - alturaVista / 2,
              xVista + larguraVista / 2 + 10, yVista + alturaVista / 2)
        g.DrawString((Me.altura * 1000).ToString("F1") & " mm", fonte, corTexto,
                xVista + larguraVista / 2 + 15, yVista - 10)
    End Sub

    Private Function FormatarIndutancia(indutanciaH As Double) As String
        If indutanciaH >= 0.001 Then
            Return (indutanciaH * 1000).ToString("F3") & " mH"
        ElseIf indutanciaH >= 0.000001 Then
            Return (indutanciaH * 1000000).ToString("F2") & " µH"
        Else
            Return (indutanciaH * 1000000000).ToString("F1") & " nH"
        End If
    End Function
End Class
