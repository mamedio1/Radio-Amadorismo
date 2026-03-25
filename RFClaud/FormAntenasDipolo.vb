Public Class FormAntenasDipolo
    Private txtFrequencia As TextBox
    Private txtVelocidade As TextBox
    Private txtResultado As TextBox
    Private btnCalcular As Button
    Private panelDesenho As Panel
    Private cmbTipoDipolo As ComboBox

    Public Sub New()
        InitializeComponent()
        ConfigurarFormulario()
        AplicarEstiloCyberpunk()
    End Sub

    Private Sub ConfigurarFormulario()
        Me.Text = "Calculadora de Antena Dipolo"
        Me.Size = New Size(750, 650)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False

        ' Título
        Dim lblTitulo As New Label()
        lblTitulo.Text = "ANTENA DIPOLO - Calculadora de Comprimento"
        lblTitulo.Location = New Point(30, 20)
        lblTitulo.Size = New Size(500, 20)
        lblTitulo.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        Me.Controls.Add(lblTitulo)

        ' Tipo de Dipolo
        Dim lblTipoDipolo As New Label()
        lblTipoDipolo.Text = "Tipo de Dipolo:"
        lblTipoDipolo.Location = New Point(30, 60)
        lblTipoDipolo.Size = New Size(130, 20)
        Me.Controls.Add(lblTipoDipolo)

        cmbTipoDipolo = New ComboBox()
        cmbTipoDipolo.Location = New Point(170, 58)
        cmbTipoDipolo.Size = New Size(200, 20)
        cmbTipoDipolo.DropDownStyle = ComboBoxStyle.DropDownList
        cmbTipoDipolo.Items.Add("Meia Onda (λ/2)")
        cmbTipoDipolo.Items.Add("Onda Completa (λ)")
        cmbTipoDipolo.Items.Add("5λ/8 (Extended Double Zepp)")
        cmbTipoDipolo.SelectedIndex = 0
        Me.Controls.Add(cmbTipoDipolo)

        ' Frequência
        Dim lblFrequencia As New Label()
        lblFrequencia.Text = "Frequência (MHz):"
        lblFrequencia.Location = New Point(30, 100)
        lblFrequencia.Size = New Size(130, 20)
        Me.Controls.Add(lblFrequencia)

        txtFrequencia = New TextBox()
        txtFrequencia.Location = New Point(170, 98)
        txtFrequencia.Size = New Size(150, 20)
        txtFrequencia.Text = "146.0"
        AddHandler txtFrequencia.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtFrequencia)

        Dim lblUnidadeFreq As New Label()
        lblUnidadeFreq.Text = "MHz"
        lblUnidadeFreq.Location = New Point(330, 100)
        lblUnidadeFreq.Size = New Size(40, 20)
        Me.Controls.Add(lblUnidadeFreq)

        ' Fator de Velocidade
        Dim lblVelocidade As New Label()
        lblVelocidade.Text = "Fator Velocidade:"
        lblVelocidade.Location = New Point(30, 140)
        lblVelocidade.Size = New Size(130, 20)
        Me.Controls.Add(lblVelocidade)

        txtVelocidade = New TextBox()
        txtVelocidade.Location = New Point(170, 138)
        txtVelocidade.Size = New Size(150, 20)
        txtVelocidade.Text = "0.95"
        AddHandler txtVelocidade.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtVelocidade)

        Dim lblInfoVel As New Label()
        lblInfoVel.Text = "(0.95-0.98 típico)"
        lblInfoVel.Location = New Point(330, 140)
        lblInfoVel.Size = New Size(120, 20)
        lblInfoVel.ForeColor = Color.Gray
        Me.Controls.Add(lblInfoVel)

        ' Botão Calcular
        btnCalcular = New Button()
        btnCalcular.Text = "Calcular e Desenhar"
        btnCalcular.Location = New Point(170, 180)
        btnCalcular.Size = New Size(150, 35)
        AddHandler btnCalcular.Click, AddressOf BtnCalcular_Click
        Me.Controls.Add(btnCalcular)

        ' Painel para o desenho
        panelDesenho = New Panel()
        panelDesenho.Location = New Point(30, 230)
        panelDesenho.Size = New Size(680, 200)
        panelDesenho.BorderStyle = BorderStyle.FixedSingle
        AddHandler panelDesenho.Paint, AddressOf PanelDesenho_Paint
        Me.Controls.Add(panelDesenho)

        ' Resultado
        Dim lblResultado As New Label()
        lblResultado.Text = "Especificações:"
        lblResultado.Location = New Point(30, 445)
        lblResultado.Size = New Size(120, 20)
        Me.Controls.Add(lblResultado)

        txtResultado = New TextBox()
        txtResultado.Location = New Point(30, 470)
        txtResultado.Size = New Size(680, 130)
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
    Private comprimentoTotal As Double = 0
    Private comprimentoCadaLado As Double = 0
    Private frequenciaCalc As Double = 0
    Private velocidadeCalc As Double = 0
    Private comprimentoOnda As Double = 0
    Private impedancia As Double = 0

    Private Sub BtnCalcular_Click(sender As Object, e As EventArgs)
        Try
            If String.IsNullOrWhiteSpace(txtFrequencia.Text) OrElse String.IsNullOrWhiteSpace(txtVelocidade.Text) Then
                MessageBox.Show("Por favor, preencha todos os campos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim freqText As String = txtFrequencia.Text.Replace(",", ".")
            Dim velText As String = txtVelocidade.Text.Replace(",", ".")

            frequenciaCalc = Double.Parse(freqText, System.Globalization.CultureInfo.InvariantCulture)
            velocidadeCalc = Double.Parse(velText, System.Globalization.CultureInfo.InvariantCulture)

            If frequenciaCalc <= 0 OrElse velocidadeCalc <= 0 OrElse velocidadeCalc > 1 Then
                MessageBox.Show("Valores inválidos. Frequência deve ser > 0 e Fator de Velocidade entre 0 e 1.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Calcular comprimento de onda no espaço livre
            Dim velocidadeLuz As Double = 299792458.0 ' m/s
            Dim frequenciaHz As Double = frequenciaCalc * 1000000.0
            comprimentoOnda = velocidadeLuz / frequenciaHz

            ' Aplicar fator de velocidade
            Dim comprimentoOndaReal As Double = comprimentoOnda * velocidadeCalc

            ' Calcular comprimento total baseado no tipo
            Select Case cmbTipoDipolo.SelectedIndex
                Case 0 ' Meia onda (λ/2)
                    comprimentoTotal = comprimentoOndaReal / 2.0
                    impedancia = 73.0 ' Ohms
                Case 1 ' Onda completa (λ)
                    comprimentoTotal = comprimentoOndaReal
                    impedancia = 300.0 ' Ohms
                Case 2 ' 5λ/8
                    comprimentoTotal = comprimentoOndaReal * 5.0 / 8.0
                    impedancia = 50.0 ' Ohms aproximado
            End Select

            comprimentoCadaLado = comprimentoTotal / 2.0

            ' Calcular ganho
            Dim ganho As Double = 2.15 ' dBi para dipolo λ/2
            If cmbTipoDipolo.SelectedIndex = 2 Then
                ganho = 3.5 ' dBi para 5λ/8
            End If

            ' Largura de banda aproximada (2% da frequência central)
            Dim larguraBanda As Double = frequenciaCalc * 0.02

            ' Formatar resultado
            Dim resultado As New System.Text.StringBuilder()
            resultado.AppendLine("=== ANTENA DIPOLO - ESPECIFICAÇÕES ===")
            resultado.AppendLine()
            resultado.AppendLine("Tipo: " & cmbTipoDipolo.Text)
            resultado.AppendLine("Frequência: " & frequenciaCalc.ToString("F3") & " MHz")
            resultado.AppendLine("Fator de Velocidade: " & velocidadeCalc.ToString("F2"))
            resultado.AppendLine()
            resultado.AppendLine("DIMENSÕES:")
            resultado.AppendLine("  • Comprimento TOTAL: " & FormatarComprimento(comprimentoTotal))
            resultado.AppendLine("  • Cada lado (metade): " & FormatarComprimento(comprimentoCadaLado))
            resultado.AppendLine("  • Comprimento de onda (λ): " & FormatarComprimento(comprimentoOnda))
            resultado.AppendLine()
            resultado.AppendLine("CARACTERÍSTICAS ELÉTRICAS:")
            resultado.AppendLine("  • Impedância: " & impedancia.ToString("F1") & " Ω")
            resultado.AppendLine("  • Ganho: " & ganho.ToString("F2") & " dBi")
            resultado.AppendLine("  • Largura de banda (VSWR<2): ±" & larguraBanda.ToString("F2") & " MHz")
            resultado.AppendLine("  • Polarização: Linear (horizontal ou vertical)")
            resultado.AppendLine()
            resultado.AppendLine("DICAS DE CONSTRUÇÃO:")
            resultado.AppendLine("  • Use fio de cobre ou alumínio de 1-4mm")
            resultado.AppendLine("  • Mantenha longe de objetos metálicos")
            resultado.AppendLine("  • Altura ideal: λ/2 acima do solo")
            resultado.AppendLine("  • Use balun 1:1 para conexão coaxial")

            txtResultado.Text = resultado.ToString()

            ' Redesenhar o painel
            panelDesenho.Invalidate()

        Catch ex As FormatException
            MessageBox.Show("Formato de número inválido. Use ponto como separador decimal.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Erro ao calcular: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PanelDesenho_Paint(sender As Object, e As PaintEventArgs)
        If comprimentoTotal = 0 Then
            ' Desenhar texto de instrução
            Dim g As Graphics = e.Graphics
            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

            Dim fonte As New Font("Segoe UI", 12, FontStyle.Italic)
            Dim texto As String = "Clique em 'Calcular e Desenhar' para ver o diagrama da antena"
            Dim tamanhoTexto As SizeF = g.MeasureString(texto, fonte)
            Dim x As Single = (panelDesenho.Width - tamanhoTexto.Width) / 2
            Dim y As Single = (panelDesenho.Height - tamanhoTexto.Height) / 2

            g.DrawString(texto, fonte, New SolidBrush(Color.FromArgb(100, 150, 200)), x, y)
            Return
        End If

        DesenharAntena(e.Graphics)
    End Sub

    Private Sub DesenharAntena(g As Graphics)
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Dim largura As Integer = panelDesenho.Width
        Dim altura As Integer = panelDesenho.Height
        Dim centroX As Integer = largura \ 2
        Dim centroY As Integer = altura \ 2

        ' Calcular escala para desenho
        Dim comprimentoPixels As Integer = 500
        Dim escala As Double = comprimentoPixels / comprimentoTotal
        Dim metadePixels As Integer = CInt(comprimentoCadaLado * escala)

        ' Cores
        Dim corAntena As New Pen(Color.FromArgb(255, 200, 50), 4) ' Dourado
        Dim corCentro As New Pen(Color.FromArgb(255, 100, 100), 6) ' Vermelho
        Dim corTexto As New SolidBrush(Color.FromArgb(150, 255, 150)) ' Verde claro
        Dim corSeta As New Pen(Color.FromArgb(100, 200, 255), 2) ' Azul claro
        corSeta.EndCap = Drawing2D.LineCap.ArrowAnchor

        Dim fonte As New Font("Consolas", 9, FontStyle.Bold)
        Dim fonteGrande As New Font("Consolas", 10, FontStyle.Bold)

        ' Desenhar a antena dipolo (linha horizontal)
        g.DrawLine(corAntena, centroX - metadePixels, centroY, centroX + metadePixels, centroY)

        ' Desenhar ponto de alimentação (centro)
        g.FillEllipse(New SolidBrush(Color.FromArgb(255, 100, 100)), centroX - 5, centroY - 5, 10, 10)

        ' Desenhar linhas de dimensão
        Dim yDimensao As Integer = centroY + 50

        ' Linha de dimensão total
        g.DrawLine(corSeta, centroX - metadePixels, yDimensao, centroX + metadePixels, yDimensao)
        g.DrawLine(corSeta, centroX - metadePixels, yDimensao - 5, centroX - metadePixels, yDimensao + 5)
        g.DrawLine(corSeta, centroX + metadePixels, yDimensao - 5, centroX + metadePixels, yDimensao + 5)

        ' Texto comprimento total
        Dim textoTotal As String = FormatarComprimento(comprimentoTotal)
        Dim tamanhoTexto As SizeF = g.MeasureString(textoTotal, fonteGrande)
        g.DrawString(textoTotal, fonteGrande, corTexto, centroX - tamanhoTexto.Width / 2, yDimensao + 10)

        ' Linhas de dimensão de cada lado
        Dim yLado As Integer = centroY - 40

        ' Lado esquerdo
        g.DrawLine(corSeta, centroX - metadePixels, yLado, centroX, yLado)
        Dim textoLado As String = FormatarComprimento(comprimentoCadaLado)
        g.DrawString(textoLado, fonte, corTexto, centroX - metadePixels + 10, yLado - 20)

        ' Lado direito
        g.DrawLine(corSeta, centroX, yLado, centroX + metadePixels, yLado)
        g.DrawString(textoLado, fonte, corTexto, centroX + metadePixels - 80, yLado - 20)

        ' Legenda do ponto de alimentação
        g.DrawString("Ponto de", fonte, New SolidBrush(Color.FromArgb(255, 150, 150)), centroX + 10, centroY - 30)
        g.DrawString("Alimentação", fonte, New SolidBrush(Color.FromArgb(255, 150, 150)), centroX + 10, centroY - 15)
        g.DrawString(impedancia.ToString("F0") & " Ω", fonte, New SolidBrush(Color.FromArgb(255, 200, 100)), centroX + 10, centroY + 5)

        ' Indicadores de polarização (setas verticais)
        Dim corPolarizacao As New Pen(Color.FromArgb(100, 255, 255), 2)
        corPolarizacao.DashStyle = Drawing2D.DashStyle.Dash

        ' Desenhar padrão de radiação simplificado
        Dim raioRadiacao As Integer = 60
        Dim angulos() As Integer = {-90, -60, -30, 0, 30, 60, 90}

        For Each angulo As Integer In angulos
            Dim radianos As Double = angulo * Math.PI / 180.0
            Dim intensidade As Double = Math.Abs(Math.Cos(radianos)) * raioRadiacao
            Dim xFim As Integer = CInt(centroX + intensidade * Math.Sin(radianos))
            Dim yFim As Integer = CInt(centroY - 80 - intensidade * Math.Cos(radianos))

            Dim corRadiacao As New Pen(Color.FromArgb(150, 200, 100, 255), 1)
            g.DrawLine(corRadiacao, centroX, centroY - 80, xFim, yFim)
        Next

        ' Texto de frequência
        g.DrawString("Freq: " & frequenciaCalc.ToString("F2") & " MHz", fonte,
                    New SolidBrush(Color.FromArgb(200, 200, 255)), 10, 10)

        ' Tipo de dipolo
        g.DrawString(cmbTipoDipolo.Text, fonte,
                    New SolidBrush(Color.FromArgb(255, 200, 100)), 10, 30)
    End Sub

    Private Function FormatarComprimento(comp As Double) As String
        If comp >= 1000 Then
            Return (comp / 1000).ToString("F3") & " km"
        ElseIf comp >= 1 Then
            Return comp.ToString("F3") & " m"
        ElseIf comp >= 0.01 Then
            Return (comp * 100).ToString("F2") & " cm"
        Else
            Return (comp * 1000).ToString("F1") & " mm"
        End If
    End Function
End Class
