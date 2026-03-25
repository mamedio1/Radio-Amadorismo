Public Class FormCalcularEspiras
    Private txtIndutancia As TextBox
    Private txtDiametro As TextBox
    Private txtComprimento As TextBox
    Private txtDiametroFio As TextBox
    Private txtResultado As TextBox
    Private btnCalcular As Button
    Private cmbTipoBobina As ComboBox

    Public Sub New()
        InitializeComponent()
        ConfigurarFormulario()
        AplicarEstiloCyberpunk()
    End Sub

    Private Sub ConfigurarFormulario()
        Me.Text = "Calcular Número de Espiras - Bobina"
        Me.Size = New Size(550, 500)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False

        ' Título
        Dim lblTitulo As New Label()
        lblTitulo.Text = "Cálculo de Bobina (Indutor)"
        lblTitulo.Location = New Point(30, 20)
        lblTitulo.Size = New Size(400, 20)
        lblTitulo.Font = New Font(lblTitulo.Font, FontStyle.Bold)
        Me.Controls.Add(lblTitulo)

        ' Tipo de Bobina
        Dim lblTipo As New Label()
        lblTipo.Text = "Tipo de Bobina:"
        lblTipo.Location = New Point(30, 50)
        lblTipo.Size = New Size(120, 20)
        Me.Controls.Add(lblTipo)

        cmbTipoBobina = New ComboBox()
        cmbTipoBobina.Location = New Point(160, 48)
        cmbTipoBobina.Size = New Size(200, 20)
        cmbTipoBobina.DropDownStyle = ComboBoxStyle.DropDownList
        cmbTipoBobina.Items.Add("Monocamada (camada única)")
        cmbTipoBobina.Items.Add("Solenoide (espaçamento mínimo)")
        cmbTipoBobina.SelectedIndex = 0
        Me.Controls.Add(cmbTipoBobina)

        ' Indutância Desejada
        Dim lblIndutancia As New Label()
        lblIndutancia.Text = "Indutância (µH):"
        lblIndutancia.Location = New Point(30, 90)
        lblIndutancia.Size = New Size(120, 20)
        Me.Controls.Add(lblIndutancia)

        txtIndutancia = New TextBox()
        txtIndutancia.Location = New Point(160, 88)
        txtIndutancia.Size = New Size(150, 20)
        AddHandler txtIndutancia.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtIndutancia)

        Dim lblUnidadeInd As New Label()
        lblUnidadeInd.Text = "µH"
        lblUnidadeInd.Location = New Point(320, 90)
        lblUnidadeInd.Size = New Size(30, 20)
        Me.Controls.Add(lblUnidadeInd)

        ' Diâmetro da Bobina
        Dim lblDiametro As New Label()
        lblDiametro.Text = "Diâmetro (mm):"
        lblDiametro.Location = New Point(30, 130)
        lblDiametro.Size = New Size(120, 20)
        Me.Controls.Add(lblDiametro)

        txtDiametro = New TextBox()
        txtDiametro.Location = New Point(160, 128)
        txtDiametro.Size = New Size(150, 20)
        AddHandler txtDiametro.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtDiametro)

        Dim lblUnidadeDiam As New Label()
        lblUnidadeDiam.Text = "mm"
        lblUnidadeDiam.Location = New Point(320, 130)
        lblUnidadeDiam.Size = New Size(30, 20)
        Me.Controls.Add(lblUnidadeDiam)

        ' Comprimento da Bobina
        Dim lblComprimento As New Label()
        lblComprimento.Text = "Comprimento (mm):"
        lblComprimento.Location = New Point(30, 170)
        lblComprimento.Size = New Size(120, 20)
        Me.Controls.Add(lblComprimento)

        txtComprimento = New TextBox()
        txtComprimento.Location = New Point(160, 168)
        txtComprimento.Size = New Size(150, 20)
        AddHandler txtComprimento.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtComprimento)

        Dim lblUnidadeComp As New Label()
        lblUnidadeComp.Text = "mm"
        lblUnidadeComp.Location = New Point(320, 170)
        lblUnidadeComp.Size = New Size(30, 20)
        Me.Controls.Add(lblUnidadeComp)

        ' Diâmetro do Fio (opcional)
        Dim lblDiametroFio As New Label()
        lblDiametroFio.Text = "Diâmetro Fio (mm):"
        lblDiametroFio.Location = New Point(30, 210)
        lblDiametroFio.Size = New Size(120, 20)
        Me.Controls.Add(lblDiametroFio)

        txtDiametroFio = New TextBox()
        txtDiametroFio.Location = New Point(160, 208)
        txtDiametroFio.Size = New Size(150, 20)
        AddHandler txtDiametroFio.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtDiametroFio)

        Dim lblOpcional As New Label()
        lblOpcional.Text = "(opcional)"
        lblOpcional.Location = New Point(320, 210)
        lblOpcional.Size = New Size(70, 20)
        lblOpcional.ForeColor = Color.Gray
        Me.Controls.Add(lblOpcional)

        ' Botão Calcular
        btnCalcular = New Button()
        btnCalcular.Text = "Calcular"
        btnCalcular.Location = New Point(200, 250)
        btnCalcular.Size = New Size(100, 35)
        AddHandler btnCalcular.Click, AddressOf BtnCalcular_Click
        Me.Controls.Add(btnCalcular)

        ' Resultado
        Dim lblResultado As New Label()
        lblResultado.Text = "Resultado:"
        lblResultado.Location = New Point(30, 300)
        lblResultado.Size = New Size(120, 20)
        Me.Controls.Add(lblResultado)

        txtResultado = New TextBox()
        txtResultado.Location = New Point(30, 325)
        txtResultado.Size = New Size(480, 130)
        txtResultado.Multiline = True
        txtResultado.ReadOnly = True
        txtResultado.ScrollBars = ScrollBars.Vertical
        Me.Controls.Add(txtResultado)
    End Sub
    Private Sub AplicarEstiloCyberpunk()
        Me.BackColor = Color.FromArgb(20, 20, 40)

        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is Label Then
                ctrl.ForeColor = Color.FromArgb(0, 255, 255)
                ctrl.Font = New Font("Segoe UI", 9, FontStyle.Bold)
            ElseIf TypeOf ctrl Is TextBox Then
                Dim txt As TextBox = DirectCast(ctrl, TextBox)
                If Not txt.ReadOnly Then
                    txt.BackColor = Color.FromArgb(30, 30, 60)
                    txt.ForeColor = Color.FromArgb(100, 255, 100)
                Else
                    txt.BackColor = Color.FromArgb(10, 10, 25)
                    txt.ForeColor = Color.FromArgb(0, 255, 255)
                End If
                txt.Font = New Font("Consolas", 9)
                txt.BorderStyle = BorderStyle.FixedSingle
            ElseIf TypeOf ctrl Is Button Then
                Dim btn As Button = DirectCast(ctrl, Button)
                btn.BackColor = Color.FromArgb(255, 0, 150)
                btn.ForeColor = Color.White
                btn.FlatStyle = FlatStyle.Flat
                btn.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                btn.FlatAppearance.BorderColor = Color.FromArgb(255, 100, 255)
                btn.FlatAppearance.BorderSize = 2
            ElseIf TypeOf ctrl Is ComboBox Then
                Dim cmb As ComboBox = DirectCast(ctrl, ComboBox)
                cmb.BackColor = Color.FromArgb(30, 30, 60)
                cmb.ForeColor = Color.FromArgb(100, 255, 100)
                cmb.Font = New Font("Consolas", 9)
            End If
        Next
    End Sub

    'End Sub

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
            If String.IsNullOrWhiteSpace(txtIndutancia.Text) OrElse
               String.IsNullOrWhiteSpace(txtDiametro.Text) OrElse
               String.IsNullOrWhiteSpace(txtComprimento.Text) Then
                MessageBox.Show("Por favor, preencha os campos: Indutância, Diâmetro e Comprimento.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim indText As String = txtIndutancia.Text.Replace(",", ".")
            Dim diamText As String = txtDiametro.Text.Replace(",", ".")
            Dim compText As String = txtComprimento.Text.Replace(",", ".")

            Dim indutanciaUH As Double = Double.Parse(indText, System.Globalization.CultureInfo.InvariantCulture)
            Dim diametroMM As Double = Double.Parse(diamText, System.Globalization.CultureInfo.InvariantCulture)
            Dim comprimentoMM As Double = Double.Parse(compText, System.Globalization.CultureInfo.InvariantCulture)

            If indutanciaUH <= 0 OrElse diametroMM <= 0 OrElse comprimentoMM <= 0 Then
                MessageBox.Show("Todos os valores devem ser maiores que zero.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Converter para metros
            Dim diametroM As Double = diametroMM / 1000.0
            Dim comprimentoM As Double = comprimentoMM / 1000.0
            Dim raioM As Double = diametroM / 2.0

            ' Converter indutância para Henry
            Dim indutanciaH As Double = indutanciaUH / 1000000.0

            ' Fórmula de Wheeler para bobina monocamada
            ' L = (d² × N²) / (18d + 40l)
            ' Onde: L em µH, d em polegadas, l em polegadas, N = número de espiras

            ' Converter para polegadas (1 polegada = 25.4 mm)
            Dim diametroPolegadas As Double = diametroMM / 25.4
            Dim comprimentoPolegadas As Double = comprimentoMM / 25.4

            ' Resolver para N: N = sqrt(L × (18d + 40l) / d²)
            Dim numeroEspiras As Double = Math.Sqrt(indutanciaUH * (18 * diametroPolegadas + 40 * comprimentoPolegadas) / (diametroPolegadas * diametroPolegadas))

            ' Calcular passo (espaçamento entre espiras)
            Dim passo As Double = comprimentoMM / numeroEspiras

            ' Calcular comprimento do fio necessário
            Dim circunferencia As Double = Math.PI * diametroMM
            Dim comprimentoFio As Double = circunferencia * numeroEspiras

            ' Verificar diâmetro do fio se fornecido
            Dim infoFio As String = ""
            If Not String.IsNullOrWhiteSpace(txtDiametroFio.Text) Then
                Dim fioText As String = txtDiametroFio.Text.Replace(",", ".")
                Dim diametroFioMM As Double = Double.Parse(fioText, System.Globalization.CultureInfo.InvariantCulture)

                If diametroFioMM > 0 Then
                    Dim espirasMaximas As Double = comprimentoMM / diametroFioMM
                    infoFio = vbCrLf & "Com fio de " & diametroFioMM.ToString("F2") & " mm:" & vbCrLf
                    infoFio &= "  • Espiras máximas possíveis: " & Math.Floor(espirasMaximas).ToString() & vbCrLf

                    If numeroEspiras > espirasMaximas Then
                        infoFio &= "  • ATENÇÃO: Número de espiras excede o espaço disponível!" & vbCrLf
                        infoFio &= "  • Sugestão: Aumente o comprimento ou use fio mais fino" & vbCrLf
                    Else
                        infoFio &= "  • Status: OK - Espiras cabem no comprimento" & vbCrLf
                    End If
                End If
            End If

            ' Formatar resultado
            Dim resultado As New System.Text.StringBuilder()
            resultado.AppendLine("=== CÁLCULO DE BOBINA ===")
            resultado.AppendLine()
            resultado.AppendLine("Tipo: " & cmbTipoBobina.Text)
            resultado.AppendLine()
            resultado.AppendLine("Parâmetros:")
            resultado.AppendLine("  • Indutância: " & indutanciaUH.ToString("F3") & " µH")
            resultado.AppendLine("  • Diâmetro: " & diametroMM.ToString("F2") & " mm")
            resultado.AppendLine("  • Comprimento: " & comprimentoMM.ToString("F2") & " mm")
            resultado.AppendLine()
            resultado.AppendLine("Resultado:")
            resultado.AppendLine("  • Número de Espiras: " & Math.Ceiling(numeroEspiras).ToString() & " voltas")
            resultado.AppendLine("  • Passo entre espiras: " & passo.ToString("F3") & " mm")
            resultado.AppendLine("  • Comprimento do fio: " & (comprimentoFio / 1000).ToString("F3") & " m")
            resultado.AppendLine("  • Circunferência: " & circunferencia.ToString("F2") & " mm")

            If infoFio <> "" Then
                resultado.AppendLine(infoFio)
            End If

            resultado.AppendLine()
            resultado.AppendLine("Nota: Cálculo baseado na fórmula de Wheeler")
            resultado.AppendLine("para bobinas monocamada com núcleo de ar.")

            txtResultado.Text = resultado.ToString()

        Catch ex As FormatException
            MessageBox.Show("Formato de número inválido. Use ponto como separador decimal.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Erro ao calcular: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class