Public Class FormCalcularLPorDimensoes
    Private txtNumeroEspiras As TextBox
    Private txtDiametroNucleo As TextBox
    Private txtDiametroFio As TextBox
    Private txtComprimento As TextBox
    Private txtResultado As TextBox
    Private btnCalcular As Button
    Private cmbTipoBobina As ComboBox
    Private cmbTipoNucleo As ComboBox

    Public Sub New()
        InitializeComponent()
        ConfigurarFormulario()
        AplicarEstiloCyberpunk()
    End Sub

    Private Sub ConfigurarFormulario()
        Me.Text = "Calcular Indutância por Dimensões Físicas"
        Me.Size = New Size(580, 600)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False

        ' Título
        Dim lblTitulo As New Label()
        lblTitulo.Text = "Cálculo de Indutância (L) - Dimensões Físicas"
        lblTitulo.Location = New Point(30, 20)
        lblTitulo.Size = New Size(500, 20)
        lblTitulo.Font = New Font(lblTitulo.Font, FontStyle.Bold)
        Me.Controls.Add(lblTitulo)

        ' Tipo de Bobina
        Dim lblTipoBobina As New Label()
        lblTipoBobina.Text = "Tipo de Bobina:"
        lblTipoBobina.Location = New Point(30, 55)
        lblTipoBobina.Size = New Size(140, 20)
        Me.Controls.Add(lblTipoBobina)

        cmbTipoBobina = New ComboBox()
        cmbTipoBobina.Location = New Point(180, 53)
        cmbTipoBobina.Size = New Size(220, 20)
        cmbTipoBobina.DropDownStyle = ComboBoxStyle.DropDownList
        cmbTipoBobina.Items.Add("Monocamada (Camada Única)")
        cmbTipoBobina.Items.Add("Multicamadas")
        cmbTipoBobina.Items.Add("Solenoide Compacto")
        cmbTipoBobina.SelectedIndex = 0
        Me.Controls.Add(cmbTipoBobina)

        ' Tipo de Núcleo
        Dim lblTipoNucleo As New Label()
        lblTipoNucleo.Text = "Tipo de Núcleo:"
        lblTipoNucleo.Location = New Point(30, 90)
        lblTipoNucleo.Size = New Size(140, 20)
        Me.Controls.Add(lblTipoNucleo)

        cmbTipoNucleo = New ComboBox()
        cmbTipoNucleo.Location = New Point(180, 88)
        cmbTipoNucleo.Size = New Size(220, 20)
        cmbTipoNucleo.DropDownStyle = ComboBoxStyle.DropDownList
        cmbTipoNucleo.Items.Add("Ar (µr = 1.0)")
        cmbTipoNucleo.Items.Add("Ferrite (µr ≈ 100-300)")
        cmbTipoNucleo.Items.Add("Ferro em Pó (µr ≈ 10-100)")
        cmbTipoNucleo.SelectedIndex = 0
        Me.Controls.Add(cmbTipoNucleo)

        ' Número de Espiras
        Dim lblNumeroEspiras As New Label()
        lblNumeroEspiras.Text = "Número de Espiras:"
        lblNumeroEspiras.Location = New Point(30, 130)
        lblNumeroEspiras.Size = New Size(140, 20)
        Me.Controls.Add(lblNumeroEspiras)

        txtNumeroEspiras = New TextBox()
        txtNumeroEspiras.Location = New Point(180, 128)
        txtNumeroEspiras.Size = New Size(150, 20)
        AddHandler txtNumeroEspiras.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtNumeroEspiras)

        Dim lblUnidadeN As New Label()
        lblUnidadeN.Text = "voltas"
        lblUnidadeN.Location = New Point(340, 130)
        lblUnidadeN.Size = New Size(50, 20)
        Me.Controls.Add(lblUnidadeN)

        ' Diâmetro do Núcleo
        Dim lblDiametroNucleo As New Label()
        lblDiametroNucleo.Text = "Diâmetro Núcleo:"
        lblDiametroNucleo.Location = New Point(30, 170)
        lblDiametroNucleo.Size = New Size(140, 20)
        Me.Controls.Add(lblDiametroNucleo)

        txtDiametroNucleo = New TextBox()
        txtDiametroNucleo.Location = New Point(180, 168)
        txtDiametroNucleo.Size = New Size(150, 20)
        AddHandler txtDiametroNucleo.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtDiametroNucleo)

        Dim lblUnidadeD As New Label()
        lblUnidadeD.Text = "mm"
        lblUnidadeD.Location = New Point(340, 170)
        lblUnidadeD.Size = New Size(30, 20)
        Me.Controls.Add(lblUnidadeD)

        ' Diâmetro do Fio
        Dim lblDiametroFio As New Label()
        lblDiametroFio.Text = "Diâmetro do Fio:"
        lblDiametroFio.Location = New Point(30, 210)
        lblDiametroFio.Size = New Size(140, 20)
        Me.Controls.Add(lblDiametroFio)

        txtDiametroFio = New TextBox()
        txtDiametroFio.Location = New Point(180, 208)
        txtDiametroFio.Size = New Size(150, 20)
        AddHandler txtDiametroFio.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtDiametroFio)

        Dim lblUnidadeFio As New Label()
        lblUnidadeFio.Text = "mm"
        lblUnidadeFio.Location = New Point(340, 210)
        lblUnidadeFio.Size = New Size(30, 20)
        Me.Controls.Add(lblUnidadeFio)

        ' Comprimento da Bobina
        Dim lblComprimento As New Label()
        lblComprimento.Text = "Comprimento:"
        lblComprimento.Location = New Point(30, 250)
        lblComprimento.Size = New Size(140, 20)
        Me.Controls.Add(lblComprimento)

        txtComprimento = New TextBox()
        txtComprimento.Location = New Point(180, 248)
        txtComprimento.Size = New Size(150, 20)
        AddHandler txtComprimento.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtComprimento)

        Dim lblUnidadeComp As New Label()
        lblUnidadeComp.Text = "mm"
        lblUnidadeComp.Location = New Point(340, 250)
        lblUnidadeComp.Size = New Size(30, 20)
        Me.Controls.Add(lblUnidadeComp)

        ' Botão Calcular
        btnCalcular = New Button()
        btnCalcular.Text = "Calcular Indutância"
        btnCalcular.Location = New Point(200, 295)
        btnCalcular.Size = New Size(150, 35)
        AddHandler btnCalcular.Click, AddressOf BtnCalcular_Click
        Me.Controls.Add(btnCalcular)

        ' Resultado
        Dim lblResultado As New Label()
        lblResultado.Text = "Resultado:"
        lblResultado.Location = New Point(30, 350)
        lblResultado.Size = New Size(120, 20)
        Me.Controls.Add(lblResultado)

        txtResultado = New TextBox()
        txtResultado.Location = New Point(30, 375)
        txtResultado.Size = New Size(510, 180)
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

    Private Sub BtnCalcular_Click(sender As Object, e As EventArgs)
        Try
            If String.IsNullOrWhiteSpace(txtNumeroEspiras.Text) OrElse
               String.IsNullOrWhiteSpace(txtDiametroNucleo.Text) OrElse
               String.IsNullOrWhiteSpace(txtDiametroFio.Text) OrElse
               String.IsNullOrWhiteSpace(txtComprimento.Text) Then
                MessageBox.Show("Por favor, preencha todos os campos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim nText As String = txtNumeroEspiras.Text.Replace(",", ".")
            Dim dNucleoText As String = txtDiametroNucleo.Text.Replace(",", ".")
            Dim dFioText As String = txtDiametroFio.Text.Replace(",", ".")
            Dim compText As String = txtComprimento.Text.Replace(",", ".")

            Dim numeroEspiras As Double = Double.Parse(nText, System.Globalization.CultureInfo.InvariantCulture)
            Dim diametroNucleoMM As Double = Double.Parse(dNucleoText, System.Globalization.CultureInfo.InvariantCulture)
            Dim diametroFioMM As Double = Double.Parse(dFioText, System.Globalization.CultureInfo.InvariantCulture)
            Dim comprimentoMM As Double = Double.Parse(compText, System.Globalization.CultureInfo.InvariantCulture)

            If numeroEspiras <= 0 OrElse diametroNucleoMM <= 0 OrElse diametroFioMM <= 0 OrElse comprimentoMM <= 0 Then
                MessageBox.Show("Todos os valores devem ser maiores que zero.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Permeabilidade relativa baseada no núcleo
            Dim permeabilidade As Double = 1.0
            Select Case cmbTipoNucleo.SelectedIndex
                Case 0 ' Ar
                    permeabilidade = 1.0
                Case 1 ' Ferrite
                    permeabilidade = 200.0 ' Valor médio
                Case 2 ' Ferro em Pó
                    permeabilidade = 50.0 ' Valor médio
            End Select

            ' Converter para metros
            Dim diametroNucleoM As Double = diametroNucleoMM / 1000.0
            Dim raioNucleoM As Double = diametroNucleoM / 2.0
            Dim comprimentoM As Double = comprimentoMM / 1000.0

            ' Diâmetro médio da bobina (núcleo + fio)
            Dim diametroMedioMM As Double = diametroNucleoMM + diametroFioMM
            Dim raioMedioM As Double = (diametroMedioMM / 1000.0) / 2.0

            ' Fórmula de Wheeler para bobina monocamada
            ' L (µH) = (N² × r² × µr) / (9r + 10l)
            ' Onde: N = espiras, r = raio em polegadas, l = comprimento em polegadas, µr = permeabilidade relativa

            ' Converter para polegadas
            Dim raioPolegadas As Double = (diametroMedioMM / 2.0) / 25.4
            Dim comprimentoPolegadas As Double = comprimentoMM / 25.4

            ' Calcular indutância em µH
            Dim indutanciaUH As Double = (numeroEspiras * numeroEspiras * raioPolegadas * raioPolegadas * permeabilidade) /
                                         (9.0 * raioPolegadas + 10.0 * comprimentoPolegadas)

            ' Calcular área da seção transversal do núcleo
            Dim areaM2 As Double = Math.PI * raioNucleoM * raioNucleoM

            ' Calcular comprimento total do fio
            Dim circunferencia As Double = Math.PI * diametroMedioMM
            Dim comprimentoFioTotal As Double = circunferencia * numeroEspiras

            ' Calcular densidade de espiras
            Dim densidadeEspiras As Double = numeroEspiras / comprimentoMM

            ' Calcular frequência de auto-ressonância aproximada (simplificada)
            ' f_res ≈ 1 / (2π√(LC)) onde C é a capacitância parasita (aproximada)
            Dim capacitanciaParasita As Double = 0.5 * numeroEspiras ' pF (aproximação)
            Dim indutanciaH As Double = indutanciaUH / 1000000.0
            Dim frequenciaRessonancia As Double = 1.0 / (2.0 * Math.PI * Math.Sqrt(indutanciaH * capacitanciaParasita / 1000000000000.0))

            ' Formatar resultado
            Dim resultado As New System.Text.StringBuilder()
            resultado.AppendLine("=== CÁLCULO DE INDUTÂNCIA ===")
            resultado.AppendLine()
            resultado.AppendLine("Configuração:")
            resultado.AppendLine("  • Tipo: " & cmbTipoBobina.Text)
            resultado.AppendLine("  • Núcleo: " & cmbTipoNucleo.Text)
            resultado.AppendLine("  • Permeabilidade relativa (µr): " & permeabilidade.ToString("F1"))
            resultado.AppendLine()
            resultado.AppendLine("Dimensões:")
            resultado.AppendLine("  • Número de espiras: " & numeroEspiras.ToString("F0") & " voltas")
            resultado.AppendLine("  • Diâmetro do núcleo: " & diametroNucleoMM.ToString("F2") & " mm")
            resultado.AppendLine("  • Diâmetro do fio: " & diametroFioMM.ToString("F2") & " mm")
            resultado.AppendLine("  • Diâmetro médio: " & diametroMedioMM.ToString("F2") & " mm")
            resultado.AppendLine("  • Comprimento: " & comprimentoMM.ToString("F2") & " mm")
            resultado.AppendLine()
            resultado.AppendLine("RESULTADO:")
            resultado.AppendLine("  • INDUTÂNCIA: " & FormatarIndutancia(indutanciaUH))
            resultado.AppendLine()
            resultado.AppendLine("Informações Adicionais:")
            resultado.AppendLine("  • Comprimento do fio: " & (comprimentoFioTotal / 1000.0).ToString("F3") & " m")
            resultado.AppendLine("  • Densidade: " & densidadeEspiras.ToString("F2") & " espiras/mm")
            resultado.AppendLine("  • Área seção transversal: " & (areaM2 * 1000000.0).ToString("F2") & " mm²")
            resultado.AppendLine("  • Freq. auto-ressonância aprox.: " & FormatarFrequencia(frequenciaRessonancia))
            resultado.AppendLine()
            resultado.AppendLine("Nota: Cálculo baseado na fórmula de Wheeler.")
            resultado.AppendLine("Valores reais podem variar devido a tolerâncias")
            resultado.AppendLine("de fabricação e efeitos parasitas.")

            txtResultado.Text = resultado.ToString()

        Catch ex As FormatException
            MessageBox.Show("Formato de número inválido. Use ponto como separador decimal.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Erro ao calcular: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function FormatarIndutancia(indutanciaUH As Double) As String
        If indutanciaUH >= 1000000 Then
            Return (indutanciaUH / 1000000).ToString("F3") & " H"
        ElseIf indutanciaUH >= 1000 Then
            Return (indutanciaUH / 1000).ToString("F3") & " mH"
        Else
            Return indutanciaUH.ToString("F3") & " µH"
        End If
    End Function

    Private Function FormatarFrequencia(freq As Double) As String
        If freq >= 1000000000 Then
            Return (freq / 1000000000).ToString("F3") & " GHz"
        ElseIf freq >= 1000000 Then
            Return (freq / 1000000).ToString("F3") & " MHz"
        ElseIf freq >= 1000 Then
            Return (freq / 1000).ToString("F3") & " kHz"
        Else
            Return freq.ToString("F3") & " Hz"
        End If
    End Function
End Class