Public Class FormAntenasLoopMagnetica
    Private txtFrequencia As TextBox
    Private txtDiametro As TextBox
    Private txtDiametroCondutor As TextBox
    Private txtResultado As TextBox
    Private btnCalcular As Button
    Private panelDesenho As Panel
    Private cmbTipoLoop As ComboBox

    Public Sub New()
        InitializeComponent()
        ConfigurarFormulario()
        AplicarEstiloCyberpunk()
    End Sub

    Private Sub ConfigurarFormulario()
        Me.Text = "Calculadora de Antena Loop Magnética"
        Me.Size = New Size(750, 750)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False

        ' Título
        Dim lblTitulo As New Label()
        lblTitulo.Text = "ANTENA LOOP MAGNÉTICA - Calculadora"
        lblTitulo.Location = New Point(30, 20)
        lblTitulo.Size = New Size(500, 20)
        lblTitulo.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        Me.Controls.Add(lblTitulo)

        ' Tipo de Loop
        Dim lblTipoLoop As New Label()
        lblTipoLoop.Text = "Tipo de Loop:"
        lblTipoLoop.Location = New Point(30, 60)
        lblTipoLoop.Size = New Size(130, 20)
        Me.Controls.Add(lblTipoLoop)

        cmbTipoLoop = New ComboBox()
        cmbTipoLoop.Location = New Point(170, 58)
        cmbTipoLoop.Size = New Size(250, 20)
        cmbTipoLoop.DropDownStyle = ComboBoxStyle.DropDownList
        cmbTipoLoop.Items.Add("Small Loop (perímetro < λ/10)")
        cmbTipoLoop.Items.Add("Medium Loop (λ/10 a λ/3)")
        cmbTipoLoop.Items.Add("Full Wave Loop (≈ λ)")
        cmbTipoLoop.SelectedIndex = 0
        Me.Controls.Add(cmbTipoLoop)

        ' Frequência
        Dim lblFrequencia As New Label()
        lblFrequencia.Text = "Frequência (MHz):"
        lblFrequencia.Location = New Point(30, 100)
        lblFrequencia.Size = New Size(130, 20)
        Me.Controls.Add(lblFrequencia)

        txtFrequencia = New TextBox()
        txtFrequencia.Location = New Point(170, 98)
        txtFrequencia.Size = New Size(150, 20)
        txtFrequencia.Text = "7.1"
        AddHandler txtFrequencia.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtFrequencia)

        Dim lblUnidadeFreq As New Label()
        lblUnidadeFreq.Text = "MHz"
        lblUnidadeFreq.Location = New Point(330, 100)
        lblUnidadeFreq.Size = New Size(40, 20)
        Me.Controls.Add(lblUnidadeFreq)

        ' Diâmetro da Loop
        Dim lblDiametro As New Label()
        lblDiametro.Text = "Diâmetro Loop (m):"
        lblDiametro.Location = New Point(30, 140)
        lblDiametro.Size = New Size(130, 20)
        Me.Controls.Add(lblDiametro)

        txtDiametro = New TextBox()
        txtDiametro.Location = New Point(170, 138)
        txtDiametro.Size = New Size(150, 20)
        txtDiametro.Text = "1.0"
        AddHandler txtDiametro.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtDiametro)

        Dim lblUnidadeDiam As New Label()
        lblUnidadeDiam.Text = "metros"
        lblUnidadeDiam.Location = New Point(330, 140)
        lblUnidadeDiam.Size = New Size(60, 20)
        Me.Controls.Add(lblUnidadeDiam)

        ' Diâmetro do Condutor
        Dim lblDiametroCondutor As New Label()
        lblDiametroCondutor.Text = "Diâmetro Tubo (mm):"
        lblDiametroCondutor.Location = New Point(30, 180)
        lblDiametroCondutor.Size = New Size(130, 20)
        Me.Controls.Add(lblDiametroCondutor)

        txtDiametroCondutor = New TextBox()
        txtDiametroCondutor.Location = New Point(170, 178)
        txtDiametroCondutor.Size = New Size(150, 20)
        txtDiametroCondutor.Text = "12"
        AddHandler txtDiametroCondutor.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtDiametroCondutor)

        Dim lblUnidadeCond As New Label()
        lblUnidadeCond.Text = "mm"
        lblUnidadeCond.Location = New Point(330, 180)
        lblUnidadeCond.Size = New Size(30, 20)
        Me.Controls.Add(lblUnidadeCond)

        ' Botão Calcular
        btnCalcular = New Button()
        btnCalcular.Text = "Calcular e Desenhar"
        btnCalcular.Location = New Point(170, 220)
        btnCalcular.Size = New Size(150, 35)
        AddHandler btnCalcular.Click, AddressOf BtnCalcular_Click
        Me.Controls.Add(btnCalcular)

        Dim btnExportarPDF As New Button()
        btnExportarPDF.Text = "Exportar PDF"
        btnExportarPDF.Location = New Point(330, 220)  ' ← Mesma altura (Y=220), ao lado (X=330)
        btnExportarPDF.Size = New Size(120, 35)
        AddHandler btnExportarPDF.Click, AddressOf BtnExportarPDF_Click
        Me.Controls.Add(btnExportarPDF)

        ' Painel para o desenho
        panelDesenho = New Panel()
        panelDesenho.Location = New Point(30, 270)
        panelDesenho.Size = New Size(680, 300)
        panelDesenho.BorderStyle = BorderStyle.FixedSingle
        AddHandler panelDesenho.Paint, AddressOf PanelDesenho_Paint
        Me.Controls.Add(panelDesenho)

        ' Resultado
        Dim lblResultado As New Label()
        lblResultado.Text = "Especificações:"
        lblResultado.Location = New Point(30, 585)
        lblResultado.Size = New Size(120, 20)
        Me.Controls.Add(lblResultado)

        txtResultado = New TextBox()
        txtResultado.Location = New Point(30, 610)
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
    Private diametroLoop As Double = 0
    Private diametroCond As Double = 0
    Private frequenciaCalc As Double = 0
    Private comprimentoOnda As Double = 0
    Private impedancia As Double = 0
    Private indutancia As Double = 0
    Private capacitancia As Double = 0
    Private fatorQ As Double = 0
    Private ganho As Double = 0
    Private larguraBanda As Double = 0
    Private perimetro As Double = 0
    Private tipoSelecionado As Integer = 0

    Private Sub BtnCalcular_Click(sender As Object, e As EventArgs)
        Try
            If String.IsNullOrWhiteSpace(txtFrequencia.Text) OrElse
               String.IsNullOrWhiteSpace(txtDiametro.Text) OrElse
               String.IsNullOrWhiteSpace(txtDiametroCondutor.Text) Then
                MessageBox.Show("Por favor, preencha todos os campos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim freqText As String = txtFrequencia.Text.Replace(",", ".")
            Dim diamText As String = txtDiametro.Text.Replace(",", ".")
            Dim condText As String = txtDiametroCondutor.Text.Replace(",", ".")

            frequenciaCalc = Double.Parse(freqText, System.Globalization.CultureInfo.InvariantCulture)
            diametroLoop = Double.Parse(diamText, System.Globalization.CultureInfo.InvariantCulture)
            diametroCond = Double.Parse(condText, System.Globalization.CultureInfo.InvariantCulture) / 1000.0 ' mm para metros

            If frequenciaCalc <= 0 OrElse diametroLoop <= 0 OrElse diametroCond <= 0 Then
                MessageBox.Show("Todos os valores devem ser maiores que zero.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Calcular comprimento de onda
            Dim velocidadeLuz As Double = 299792458.0
            Dim frequenciaHz As Double = frequenciaCalc * 1000000.0
            comprimentoOnda = velocidadeLuz / frequenciaHz

            ' Calcular perímetro da loop
            Dim raioLoop As Double = diametroLoop / 2.0
            perimetro = 2.0 * Math.PI * raioLoop

            tipoSelecionado = cmbTipoLoop.SelectedIndex

            ' Calcular parâmetros baseado no tipo
            Select Case tipoSelecionado
                Case 0 ' Small Loop (< λ/10)
                    ' Indutância de uma loop circular: L = μ₀ × r × [ln(8r/a) - 2]
                    Dim mu0 As Double = 4.0 * Math.PI * 0.0000001 ' Permeabilidade do vácuo
                    indutancia = mu0 * raioLoop * (Math.Log(8.0 * raioLoop / (diametroCond / 2.0)) - 2.0)

                    ' Capacitância de sintonia (ressonância): C = 1 / (ω² × L)
                    Dim omega As Double = 2.0 * Math.PI * frequenciaHz
                    capacitancia = 1.0 / (omega * omega * indutancia)

                    ' Resistência de radiação (muito baixa para small loops)
                    Dim areaLoop As Double = Math.PI * raioLoop * raioLoop
                    Dim Rrad As Double = 31171.0 * Math.Pow((areaLoop / (comprimentoOnda * comprimentoOnda)), 2)

                    ' Resistência ôhmica aproximada
                    Dim Rohm As Double = perimetro / (Math.PI * (diametroCond / 2.0) * (diametroCond / 2.0) * 58000000.0)

                    impedancia = Rrad + Rohm
                    fatorQ = (omega * indutancia) / (Rrad + Rohm)
                    larguraBanda = frequenciaCalc / fatorQ
                    ganho = -20.0 ' dBi aproximado para small loop

                Case 1 ' Medium Loop
                    indutancia = 0.0001 * raioLoop ' Aproximação
                    capacitancia = 50.0 / 1000000000000.0 ' pF
                    impedancia = 50.0
                    fatorQ = 50.0
                    larguraBanda = frequenciaCalc / fatorQ
                    ganho = -10.0

                Case 2 ' Full Wave Loop
                    indutancia = 0.0002 * raioLoop
                    capacitancia = 0
                    impedancia = 100.0
                    fatorQ = 20.0
                    larguraBanda = frequenciaCalc / fatorQ
                    ganho = 2.1 ' dBi
            End Select

            ' Formatar resultado
            Dim resultado As New System.Text.StringBuilder()
            resultado.AppendLine("=== ANTENA LOOP MAGNÉTICA - ESPECIFICAÇÕES ===")
            resultado.AppendLine()
            resultado.AppendLine("Tipo: " & cmbTipoLoop.Text)
            resultado.AppendLine("Frequência: " & frequenciaCalc.ToString("F3") & " MHz")
            resultado.AppendLine()
            resultado.AppendLine("DIMENSÕES:")
            resultado.AppendLine("  • Diâmetro da loop: " & diametroLoop.ToString("F3") & " m")
            resultado.AppendLine("  • Perímetro: " & perimetro.ToString("F3") & " m (" & (perimetro / comprimentoOnda).ToString("F3") & "λ)")
            resultado.AppendLine("  • Diâmetro do condutor: " & (diametroCond * 1000).ToString("F1") & " mm")
            resultado.AppendLine("  • Comprimento de onda: " & comprimentoOnda.ToString("F3") & " m")
            resultado.AppendLine()
            resultado.AppendLine("CARACTERÍSTICAS ELÉTRICAS:")
            resultado.AppendLine("  • Indutância: " & (indutancia * 1000000).ToString("F2") & " µH")

            If capacitancia > 0 Then
                resultado.AppendLine("  • Capacitor de sintonia: " & (capacitancia * 1000000000000.0).ToString("F1") & " pF")
            End If

            resultado.AppendLine("  • Impedância: " & impedancia.ToString("F1") & " Ω")
            resultado.AppendLine("  • Fator Q: " & fatorQ.ToString("F0"))
            resultado.AppendLine("  • Largura de banda: " & larguraBanda.ToString("F3") & " MHz")
            resultado.AppendLine("  • Ganho: " & ganho.ToString("F1") & " dBi")
            resultado.AppendLine("  • Eficiência: " & If(tipoSelecionado = 0, "Baixa (<30%)", "Média a Alta"))
            resultado.AppendLine()
            resultado.AppendLine("DICAS DE CONSTRUÇÃO:")
            resultado.AppendLine("  • Use tubo de cobre ou alumínio grosso")
            resultado.AppendLine("  • Capacitor variável de alta tensão para sintonia")
            resultado.AppendLine("  • Loop de acoplamento (1/5 do diâmetro principal)")
            resultado.AppendLine("  • Instale longe de objetos metálicos")
            resultado.AppendLine("  • Aterramento adequado é essencial")
            resultado.AppendLine("  • Use balun se necessário")

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
        If diametroLoop = 0 Then
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

        DesenharLoopMagnetica(e.Graphics)
    End Sub

    Private Sub DesenharLoopMagnetica(g As Graphics)
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Dim largura As Integer = panelDesenho.Width
        Dim altura As Integer = panelDesenho.Height
        Dim centroX As Single = largura / 2.0F
        Dim centroY As Single = altura / 2.0F

        ' Calcular escala para desenho
        Dim escala As Single = 120.0F / CSng(diametroLoop)
        Dim raioLoopPixels As Single = CSng(diametroLoop / 2.0 * escala)
        Dim espessuraCondutor As Single = Math.Max(CSng(diametroCond * escala * 100), 4.0F)

        ' Cores
        Dim corLoop As New Pen(Color.FromArgb(255, 200, 50), espessuraCondutor)
        Dim corLoopAcoplamento As New Pen(Color.FromArgb(100, 200, 255), 3)
        Dim corCapacitor As New Pen(Color.FromArgb(255, 100, 100), 3)
        Dim corTexto As New SolidBrush(Color.FromArgb(150, 255, 150))
        Dim corDimensao As New Pen(Color.FromArgb(100, 200, 255), 2)
        corDimensao.EndCap = Drawing2D.LineCap.ArrowAnchor

        Dim fonte As New Font("Consolas", 8, FontStyle.Bold)
        Dim fonteGrande As New Font("Consolas", 9, FontStyle.Bold)

        ' Desenhar loop principal
        Dim rectLoop As New RectangleF(centroX - raioLoopPixels, centroY - raioLoopPixels,
                                       raioLoopPixels * 2, raioLoopPixels * 2)
        g.DrawEllipse(corLoop, rectLoop)

        ' Desenhar capacitor de sintonia (no topo)
        Dim capWidth As Single = 30.0F
        Dim capHeight As Single = 15.0F
        Dim capX As Single = centroX - capWidth / 2
        Dim capY As Single = centroY - raioLoopPixels - 20

        ' Desenhar símbolo do capacitor
        g.DrawLine(corCapacitor, capX, capY, capX + capWidth, capY)
        g.DrawLine(corCapacitor, capX, capY + capHeight, capX + capWidth, capY + capHeight)
        g.DrawLine(corCapacitor, capX + capWidth / 2, capY, capX + capWidth / 2, centroY - raioLoopPixels)
        g.DrawLine(corCapacitor, capX + capWidth / 2, capY + capHeight, capX + capWidth / 2, capY + capHeight + 10)

        ' Label do capacitor
        g.DrawString("C", fonte, New SolidBrush(Color.FromArgb(255, 150, 150)), capX + capWidth / 2 - 5, capY - 15)

        ' Desenhar loop de acoplamento (menor, embaixo)
        Dim raioAcoplamento As Single = raioLoopPixels * 0.2F
        Dim rectAcoplamento As New RectangleF(centroX - raioAcoplamento,
                                              centroY + raioLoopPixels - raioAcoplamento - 10,
                                              raioAcoplamento * 2, raioAcoplamento * 2)
        g.DrawEllipse(corLoopAcoplamento, rectAcoplamento)

        ' Desenhar cabo de alimentação
        g.DrawLine(corLoopAcoplamento, centroX, centroY + raioLoopPixels + raioAcoplamento - 10,
                  centroX, centroY + raioLoopPixels + 40)

        ' Label da loop de acoplamento
        g.DrawString("Loop Acoplamento", fonte, New SolidBrush(Color.FromArgb(100, 200, 255)),
                    centroX - 55, centroY + raioLoopPixels + 45)
        g.DrawString("50Ω Coax", fonte, New SolidBrush(Color.FromArgb(100, 200, 255)),
                    centroX - 30, centroY + raioLoopPixels + 60)

        ' Dimensões - Diâmetro
        Dim yDimensao As Single = centroY + raioLoopPixels + 80
        g.DrawLine(corDimensao, centroX - raioLoopPixels, yDimensao, centroX + raioLoopPixels, yDimensao)
        g.DrawLine(New Pen(Color.FromArgb(100, 200, 255), 1), centroX - raioLoopPixels, centroY,
                  centroX - raioLoopPixels, yDimensao)
        g.DrawLine(New Pen(Color.FromArgb(100, 200, 255), 1), centroX + raioLoopPixels, centroY,
                  centroX + raioLoopPixels, yDimensao)

        Dim textoDiametro As String = diametroLoop.ToString("F2") & " m"
        g.DrawString(textoDiametro, fonteGrande, corTexto, centroX - 30, yDimensao + 5)

        ' Informações no canto
        g.DrawString("Freq: " & frequenciaCalc.ToString("F2") & " MHz", fonte,
                    New SolidBrush(Color.FromArgb(200, 200, 255)), 10.0F, 10.0F)
        g.DrawString(cmbTipoLoop.Text, fonte,
                    New SolidBrush(Color.FromArgb(255, 200, 100)), 10.0F, 25.0F)
        g.DrawString("Q: " & fatorQ.ToString("F0"), fonte,
                    New SolidBrush(Color.FromArgb(150, 255, 150)), 10.0F, 40.0F)
        g.DrawString("Perímetro: " & (perimetro / comprimentoOnda).ToString("F3") & "λ", fonte,
                    New SolidBrush(Color.FromArgb(255, 150, 255)), 10.0F, 55.0F)

        ' Desenhar campo magnético (linhas de campo)
        Dim corCampo As New Pen(Color.FromArgb(80, 150, 100, 255), 1)
        corCampo.DashStyle = Drawing2D.DashStyle.Dash

        For r As Single = raioLoopPixels + 20 To raioLoopPixels + 60 Step 20
            g.DrawEllipse(corCampo, centroX - r, centroY - r, r * 2, r * 2)
        Next

        ' Label campo magnético
        g.DrawString("Campo", fonte, New SolidBrush(Color.FromArgb(150, 200, 150)),
                    CSng(largura - 80), centroY - 20)
        g.DrawString("Magnético", fonte, New SolidBrush(Color.FromArgb(150, 200, 150)),
                    CSng(largura - 85), centroY - 5)
    End Sub
    Private Sub BtnExportarPDF_Click(sender As Object, e As EventArgs)
        Try
            If String.IsNullOrWhiteSpace(txtResultado.Text) Then
                MessageBox.Show("Nenhum resultado para exportar. Calcule primeiro!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Abrir diálogo para salvar arquivo
            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "PDF Files (*.pdf)|*.pdf"
            saveDialog.Title = "Salvar Resultados em PDF"
            saveDialog.FileName = "Antane Loop Magnetica_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".pdf"

            If saveDialog.ShowDialog() = DialogResult.OK Then
                ' Gerar PDF
                GeradorPDF.ExportarParaPDF("Antane Loop Magnetica", txtResultado.Text, saveDialog.FileName)

                MessageBox.Show("PDF gerado com sucesso!" & vbCrLf & vbCrLf &
                              "Arquivo salvo em:" & vbCrLf & saveDialog.FileName,
                              "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Perguntar se quer abrir o PDF
                If MessageBox.Show("Deseja abrir o PDF agora?", "Abrir PDF", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Process.Start(saveDialog.FileName)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Erro ao exportar PDF: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class