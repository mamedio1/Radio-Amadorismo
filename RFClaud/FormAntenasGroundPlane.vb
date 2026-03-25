Public Class FormAntenasGroundPlane
    Private txtFrequencia As TextBox
    Private txtVelocidade As TextBox
    Private txtNumRadiais As TextBox
    Private txtResultado As TextBox
    Private btnCalcular As Button
    Private panelDesenho As Panel
    Private cmbTipoGP As ComboBox

    Public Sub New()
        InitializeComponent()
        ConfigurarFormulario()
        AplicarEstiloCyberpunk()
    End Sub

    Private Sub ConfigurarFormulario()
        Me.Text = "Calculadora de Antena Ground Plane"
        Me.Size = New Size(750, 700)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False

        ' Título
        Dim lblTitulo As New Label()
        lblTitulo.Text = "ANTENA GROUND PLANE - Calculadora Vertical"
        lblTitulo.Location = New Point(30, 20)
        lblTitulo.Size = New Size(500, 20)
        lblTitulo.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        Me.Controls.Add(lblTitulo)

        ' Tipo de Ground Plane
        Dim lblTipoGP As New Label()
        lblTipoGP.Text = "Tipo:"
        lblTipoGP.Location = New Point(30, 60)
        lblTipoGP.Size = New Size(130, 20)
        Me.Controls.Add(lblTipoGP)

        cmbTipoGP = New ComboBox()
        cmbTipoGP.Location = New Point(170, 58)
        cmbTipoGP.Size = New Size(250, 20)
        cmbTipoGP.DropDownStyle = ComboBoxStyle.DropDownList
        cmbTipoGP.Items.Add("λ/4 - Radiais Horizontais (0°)")
        cmbTipoGP.Items.Add("λ/4 - Radiais Inclinados 45°")
        cmbTipoGP.Items.Add("5λ/8 - Ganho Extra")
        cmbTipoGP.Items.Add("λ/2 - J-Pole Style")
        cmbTipoGP.SelectedIndex = 0
        Me.Controls.Add(cmbTipoGP)

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

        ' Número de Radiais
        Dim lblNumRadiais As New Label()
        lblNumRadiais.Text = "Número Radiais:"
        lblNumRadiais.Location = New Point(30, 180)
        lblNumRadiais.Size = New Size(130, 20)
        Me.Controls.Add(lblNumRadiais)

        txtNumRadiais = New TextBox()
        txtNumRadiais.Location = New Point(170, 178)
        txtNumRadiais.Size = New Size(150, 20)
        txtNumRadiais.Text = "4"
        AddHandler txtNumRadiais.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtNumRadiais)

        Dim lblInfoRadiais As New Label()
        lblInfoRadiais.Text = "(mín. 3, ideal 4-8)"
        lblInfoRadiais.Location = New Point(330, 180)
        lblInfoRadiais.Size = New Size(150, 20)
        lblInfoRadiais.ForeColor = Color.Gray
        Me.Controls.Add(lblInfoRadiais)

        ' Botão Calcular
        btnCalcular = New Button()
        btnCalcular.Text = "Calcular e Desenhar"
        btnCalcular.Location = New Point(170, 220)
        btnCalcular.Size = New Size(150, 35)
        AddHandler btnCalcular.Click, AddressOf BtnCalcular_Click
        Me.Controls.Add(btnCalcular)

        ' Painel para o desenho
        panelDesenho = New Panel()
        panelDesenho.Location = New Point(30, 270)
        panelDesenho.Size = New Size(680, 250)
        panelDesenho.BorderStyle = BorderStyle.FixedSingle
        AddHandler panelDesenho.Paint, AddressOf PanelDesenho_Paint
        Me.Controls.Add(panelDesenho)

        ' Resultado
        Dim lblResultado As New Label()
        lblResultado.Text = "Especificações:"
        lblResultado.Location = New Point(30, 535)
        lblResultado.Size = New Size(120, 20)
        Me.Controls.Add(lblResultado)

        txtResultado = New TextBox()
        txtResultado.Location = New Point(30, 560)
        txtResultado.Size = New Size(680, 100)
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
    Private comprimentoVertical As Double = 0
    Private comprimentoRadial As Double = 0
    Private frequenciaCalc As Double = 0
    Private velocidadeCalc As Double = 0
    Private comprimentoOnda As Double = 0
    Private impedancia As Double = 0
    Private numRadiais As Integer = 4
    Private tipoSelecionado As Integer = 0
    Private ganho As Double = 0
    Private angulo As Integer = 0

    Private Sub BtnCalcular_Click(sender As Object, e As EventArgs)
        Try
            If String.IsNullOrWhiteSpace(txtFrequencia.Text) OrElse
               String.IsNullOrWhiteSpace(txtVelocidade.Text) OrElse
               String.IsNullOrWhiteSpace(txtNumRadiais.Text) Then
                MessageBox.Show("Por favor, preencha todos os campos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim freqText As String = txtFrequencia.Text.Replace(",", ".")
            Dim velText As String = txtVelocidade.Text.Replace(",", ".")
            Dim radText As String = txtNumRadiais.Text.Replace(",", ".")

            frequenciaCalc = Double.Parse(freqText, System.Globalization.CultureInfo.InvariantCulture)
            velocidadeCalc = Double.Parse(velText, System.Globalization.CultureInfo.InvariantCulture)
            numRadiais = CInt(Double.Parse(radText, System.Globalization.CultureInfo.InvariantCulture))

            If frequenciaCalc <= 0 OrElse velocidadeCalc <= 0 OrElse velocidadeCalc > 1 OrElse numRadiais < 2 Then
                MessageBox.Show("Valores inválidos. Verifique frequência, fator de velocidade e número de radiais (mínimo 2).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Calcular comprimento de onda
            Dim velocidadeLuz As Double = 299792458.0
            Dim frequenciaHz As Double = frequenciaCalc * 1000000.0
            comprimentoOnda = velocidadeLuz / frequenciaHz
            Dim comprimentoOndaReal As Double = comprimentoOnda * velocidadeCalc

            tipoSelecionado = cmbTipoGP.SelectedIndex

            ' Calcular dimensões baseado no tipo
            Select Case tipoSelecionado
                Case 0 ' λ/4 horizontal (0°)
                    comprimentoVertical = comprimentoOndaReal / 4.0
                    comprimentoRadial = comprimentoOndaReal / 4.0
                    impedancia = 36.0
                    ganho = 0.0
                    angulo = 0

                Case 1 ' λ/4 radiais 45°
                    comprimentoVertical = comprimentoOndaReal / 4.0
                    comprimentoRadial = comprimentoOndaReal / 4.0
                    impedancia = 50.0
                    ganho = 0.5
                    angulo = 45

                Case 2 ' 5λ/8
                    comprimentoVertical = comprimentoOndaReal * 5.0 / 8.0
                    comprimentoRadial = comprimentoOndaReal / 4.0
                    impedancia = 50.0
                    ganho = 3.0
                    angulo = 45

                Case 3 ' λ/2 J-Pole
                    comprimentoVertical = comprimentoOndaReal / 2.0
                    comprimentoRadial = comprimentoOndaReal / 4.0
                    impedancia = 50.0
                    ganho = 1.5
                    angulo = 0
            End Select

            ' Largura de banda
            Dim larguraBanda As Double = frequenciaCalc * 0.03

            ' Ângulo de radiação
            Dim anguloRadiacao As Double = 25.0
            If tipoSelecionado = 2 Then anguloRadiacao = 18.0

            ' Formatar resultado
            Dim resultado As New System.Text.StringBuilder()
            resultado.AppendLine("=== ANTENA GROUND PLANE - ESPECIFICAÇÕES ===")
            resultado.AppendLine()
            resultado.AppendLine("Tipo: " & cmbTipoGP.Text)
            resultado.AppendLine("Frequência: " & frequenciaCalc.ToString("F3") & " MHz")
            resultado.AppendLine("Fator de Velocidade: " & velocidadeCalc.ToString("F2"))
            resultado.AppendLine()
            resultado.AppendLine("DIMENSÕES:")
            resultado.AppendLine("  • Elemento VERTICAL: " & FormatarComprimento(comprimentoVertical))
            resultado.AppendLine("  • Cada RADIAL: " & FormatarComprimento(comprimentoRadial))
            resultado.AppendLine("  • Número de radiais: " & numRadiais.ToString())
            If angulo > 0 Then
                resultado.AppendLine("  • Ângulo dos radiais: " & angulo.ToString() & "° (abaixo da horizontal)")
            End If
            resultado.AppendLine("  • Comprimento de onda (λ): " & FormatarComprimento(comprimentoOnda))
            resultado.AppendLine()
            resultado.AppendLine("CARACTERÍSTICAS ELÉTRICAS:")
            resultado.AppendLine("  • Impedância: " & impedancia.ToString("F0") & " Ω")
            resultado.AppendLine("  • Ganho: " & ganho.ToString("F1") & " dBi")
            resultado.AppendLine("  • Ângulo de radiação: " & anguloRadiacao.ToString("F0") & "° (acima do horizonte)")
            resultado.AppendLine("  • Largura de banda (VSWR<2): ±" & larguraBanda.ToString("F2") & " MHz")
            resultado.AppendLine("  • Polarização: Vertical")
            resultado.AppendLine()
            resultado.AppendLine("DICAS DE CONSTRUÇÃO:")
            resultado.AppendLine("  • Use tubo de alumínio ou fio grosso para elemento vertical")
            resultado.AppendLine("  • Radiais podem ser fio de cobre #12 ou similar")
            resultado.AppendLine("  • Ponto de alimentação: base do elemento vertical")
            resultado.AppendLine("  • Instale o mais alto possível, longe de obstruções")
            resultado.AppendLine("  • Use balun 1:1 se impedância for 50Ω")

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
        If comprimentoVertical = 0 Then
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

        DesenharGroundPlane(e.Graphics)
    End Sub

    Private Sub DesenharGroundPlane(g As Graphics)
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Dim largura As Integer = panelDesenho.Width
        Dim altura As Integer = panelDesenho.Height
        Dim centroX As Integer = largura \ 2
        Dim baseY As Integer = altura - 50

        ' Calcular escala
        Dim escalaVertical As Double = 150.0 / comprimentoVertical
        Dim escalaRadial As Double = 120.0 / comprimentoRadial

        Dim alturaVerticalPixels As Integer = CInt(comprimentoVertical * escalaVertical)
        Dim comprimentoRadialPixels As Integer = CInt(comprimentoRadial * escalaRadial)

        ' Cores
        Dim corVertical As New Pen(Color.FromArgb(255, 200, 50), 5)
        Dim corRadial As New Pen(Color.FromArgb(100, 200, 255), 3)
        Dim corBase As New Pen(Color.FromArgb(255, 100, 100), 4)
        Dim corTexto As New SolidBrush(Color.FromArgb(150, 255, 150))
        Dim corDimensao As New Pen(Color.FromArgb(100, 200, 255), 2)
        corDimensao.EndCap = Drawing2D.LineCap.ArrowAnchor

        Dim fonte As New Font("Consolas", 8, FontStyle.Bold)
        Dim fonteGrande As New Font("Consolas", 9, FontStyle.Bold)

        ' Desenhar plano de terra (linha horizontal)
        g.DrawLine(New Pen(Color.FromArgb(80, 80, 120), 2), 50, baseY, largura - 50, baseY)
        g.DrawString("Plano de Terra", fonte, New SolidBrush(Color.FromArgb(150, 150, 200)), 60.0F, CSng(baseY + 5))

        ' Desenhar elemento vertical
        g.DrawLine(corVertical, centroX, baseY, centroX, baseY - alturaVerticalPixels)

        ' Desenhar ponto de alimentação
        g.FillEllipse(New SolidBrush(Color.FromArgb(255, 100, 100)), centroX - 6, baseY - 6, 12, 12)

        ' Desenhar radiais
        Dim anguloIncremento As Double = 360.0 / numRadiais

        For i As Integer = 0 To numRadiais - 1
            Dim anguloGraus As Double = i * anguloIncremento
            Dim anguloRad As Double = anguloGraus * Math.PI / 180.0

            Dim xFim As Integer
            Dim yFim As Integer

            If angulo = 0 Then
                ' Radiais horizontais
                xFim = CInt(centroX + comprimentoRadialPixels * Math.Cos(anguloRad))
                yFim = baseY
            Else
                ' Radiais inclinados
                Dim anguloInclinacao As Double = angulo * Math.PI / 180.0
                xFim = CInt(centroX + comprimentoRadialPixels * Math.Cos(anguloRad) * Math.Cos(anguloInclinacao))
                yFim = CInt(baseY + comprimentoRadialPixels * Math.Sin(anguloInclinacao))
            End If

            g.DrawLine(corRadial, centroX, baseY, xFim, yFim)
        Next

        ' Dimensões - Vertical
        Dim xDimensaoV As Integer = centroX + 70
        g.DrawLine(corDimensao, xDimensaoV, baseY, xDimensaoV, baseY - alturaVerticalPixels)
        g.DrawLine(New Pen(Color.FromArgb(100, 200, 255), 1), centroX, baseY - alturaVerticalPixels, xDimensaoV, baseY - alturaVerticalPixels)
        g.DrawLine(New Pen(Color.FromArgb(100, 200, 255), 1), centroX, baseY, xDimensaoV, baseY)

        Dim textoVertical As String = FormatarComprimento(comprimentoVertical)
        Dim yPosTextoV As Single = CSng(baseY - alturaVerticalPixels / 2)
        g.DrawString(textoVertical, fonteGrande, corTexto, CSng(xDimensaoV + 5), yPosTextoV - 10.0F)
        g.DrawString("VERTICAL", fonte, corTexto, CSng(xDimensaoV + 5), yPosTextoV + 5.0F)

        ' Dimensões - Radial (primeiro radial à direita)
        If numRadiais > 0 Then
            Dim anguloRad As Double = 0
            Dim xRadial As Integer
            Dim yRadial As Integer

            If angulo = 0 Then
                xRadial = centroX + comprimentoRadialPixels
                yRadial = baseY
            Else
                Dim anguloInclinacao As Double = angulo * Math.PI / 180.0
                xRadial = CInt(centroX + comprimentoRadialPixels * Math.Cos(anguloInclinacao))
                yRadial = CInt(baseY + comprimentoRadialPixels * Math.Sin(anguloInclinacao))
            End If

            Dim textoRadial As String = FormatarComprimento(comprimentoRadial)
            Dim xPosRadial As Single = CSng((centroX + xRadial) / 2)
            g.DrawString(textoRadial, fonteGrande, corTexto, xPosRadial - 40.0F, CSng(yRadial - 30))
            g.DrawString("RADIAL", fonte, corTexto, xPosRadial - 30.0F, CSng(yRadial - 15))
        End If

        ' Informações
        g.DrawString("Freq: " & frequenciaCalc.ToString("F2") & " MHz", fonte,
                    New SolidBrush(Color.FromArgb(200, 200, 255)), 10.0F, 10.0F)
        g.DrawString(cmbTipoGP.Text, fonte,
                    New SolidBrush(Color.FromArgb(255, 200, 100)), 10.0F, 25.0F)
        g.DrawString("Radiais: " & numRadiais.ToString(), fonte,
                    New SolidBrush(Color.FromArgb(150, 255, 150)), 10.0F, 40.0F)
        g.DrawString("Impedância: " & impedancia.ToString("F0") & " Ω", fonte,
                    New SolidBrush(Color.FromArgb(255, 150, 150)), 10.0F, 55.0F)

        ' Legenda de alimentação
        g.DrawString("Ponto de", fonte, New SolidBrush(Color.FromArgb(255, 150, 150)), CSng(centroX + 15), CSng(baseY - 15))
        g.DrawString("Alimentação", fonte, New SolidBrush(Color.FromArgb(255, 150, 150)), CSng(centroX + 15), CSng(baseY))

        ' Desenhar padrão de radiação simplificado (vista lateral)
        Dim xRadiacao As Single = CSng(largura - 120)
        Dim yRadiacao As Single = 80.0F
        Dim raioMax As Single = 50.0F

        g.DrawString("Padrão", fonte, New SolidBrush(Color.FromArgb(150, 200, 255)), xRadiacao - 20.0F, yRadiacao - 50.0F)
        g.DrawString("Radiação", fonte, New SolidBrush(Color.FromArgb(150, 200, 255)), xRadiacao - 25.0F, yRadiacao - 35.0F)

        ' Desenhar padrão omnidirecional
        For angGraus As Integer = -90 To 90 Step 10
            Dim angRad As Double = angGraus * Math.PI / 180.0
            Dim intensidade As Single = CSng(Math.Abs(Math.Cos(angRad)) * raioMax * 0.8)
            Dim xFim As Single = CSng(xRadiacao + intensidade * Math.Sin(angRad))
            Dim yFim As Single = CSng(yRadiacao - intensidade * Math.Cos(angRad))

            Dim corRadiacao As New Pen(Color.FromArgb(100, 150, 100, 255), 1)
            g.DrawLine(corRadiacao, xRadiacao, yRadiacao, xFim, yFim)
        Next

        ' Linha de horizonte no padrão
        g.DrawLine(New Pen(Color.FromArgb(150, 150, 150), 1) With {.DashStyle = Drawing2D.DashStyle.Dash},
                  xRadiacao - raioMax, yRadiacao, xRadiacao + raioMax, yRadiacao)
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