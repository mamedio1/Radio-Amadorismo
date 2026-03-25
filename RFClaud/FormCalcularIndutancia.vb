Public Class FormCalcularIndutancia
    Private txtFrequencia As TextBox
    Private txtCapacitor As TextBox
    Private txtResultado As TextBox
    Private btnCalcular As Button

    Public Sub New()
        InitializeComponent()
        ConfigurarFormulario()
        AplicarEstiloCyberpunk()  ' ← LINHA ADICIONADA
    End Sub

    Private Sub ConfigurarFormulario()
        Me.Text = "Calcular Indutância - Tanque LC"
        Me.Size = New Size(500, 350)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False

        ' Label Frequência
        Dim lblFrequencia As New Label()
        lblFrequencia.Text = "Frequência (MHz):"
        lblFrequencia.Location = New Point(30, 30)
        lblFrequencia.Size = New Size(120, 20)
        Me.Controls.Add(lblFrequencia)

        ' TextBox Frequência
        txtFrequencia = New TextBox()
        txtFrequencia.Location = New Point(160, 28)
        txtFrequencia.Size = New Size(150, 20)
        AddHandler txtFrequencia.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtFrequencia)

        ' Label unidade MHz
        Dim lblUnidadeMHz As New Label()
        lblUnidadeMHz.Text = "MHz"
        lblUnidadeMHz.Location = New Point(320, 30)
        lblUnidadeMHz.Size = New Size(40, 20)
        Me.Controls.Add(lblUnidadeMHz)

        ' Label Capacitor
        Dim lblCapacitor As New Label()
        lblCapacitor.Text = "Capacitor (pF):"
        lblCapacitor.Location = New Point(30, 70)
        lblCapacitor.Size = New Size(120, 20)
        Me.Controls.Add(lblCapacitor)

        ' TextBox Capacitor
        txtCapacitor = New TextBox()
        txtCapacitor.Location = New Point(160, 68)
        txtCapacitor.Size = New Size(150, 20)
        AddHandler txtCapacitor.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtCapacitor)

        ' Label unidade pF
        Dim lblUnidadepF As New Label()
        lblUnidadepF.Text = "pF"
        lblUnidadepF.Location = New Point(320, 70)
        lblUnidadepF.Size = New Size(30, 20)
        Me.Controls.Add(lblUnidadepF)

        ' Botão Calcular
        btnCalcular = New Button()
        btnCalcular.Text = "Calcular"
        btnCalcular.Location = New Point(160, 110)
        btnCalcular.Size = New Size(100, 30)
        AddHandler btnCalcular.Click, AddressOf BtnCalcular_Click
        Me.Controls.Add(btnCalcular)

        ' Label Resultado
        Dim lblResultado As New Label()
        lblResultado.Text = "Resultado:"
        lblResultado.Location = New Point(30, 160)
        lblResultado.Size = New Size(120, 20)
        Me.Controls.Add(lblResultado)

        ' TextBox Resultado
        txtResultado = New TextBox()
        txtResultado.Location = New Point(30, 185)
        txtResultado.Size = New Size(430, 100)
        txtResultado.Multiline = True
        txtResultado.ReadOnly = True
        txtResultado.ScrollBars = ScrollBars.Vertical
        Me.Controls.Add(txtResultado)
    End Sub

    ' ↓↓↓ MÉTODO NOVO ADICIONADO ↓↓↓
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
            End If
        Next
    End Sub
    ' ↑↑↑ ATÉ AQUI ↑↑↑

    Private Sub ValidarEntrada(sender As Object, e As KeyPressEventArgs)
        ' Aceita números, ponto, vírgula e backspace
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c AndAlso e.KeyChar <> ","c AndAlso e.KeyChar <> ChrW(Keys.Back) Then
            e.Handled = True
            Return
        End If

        ' Substitui vírgula por ponto automaticamente
        If e.KeyChar = ","c Then
            e.Handled = True
            Dim txt As TextBox = DirectCast(sender, TextBox)
            If Not txt.Text.Contains(".") Then
                txt.Text = txt.Text.Insert(txt.SelectionStart, ".")
                txt.SelectionStart = txt.Text.Length
            End If
            Return
        End If

        ' Permite apenas um ponto
        Dim txtBox As TextBox = DirectCast(sender, TextBox)
        If e.KeyChar = "."c AndAlso txtBox.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub BtnCalcular_Click(sender As Object, e As EventArgs)
        Try
            If String.IsNullOrWhiteSpace(txtFrequencia.Text) OrElse String.IsNullOrWhiteSpace(txtCapacitor.Text) Then
                MessageBox.Show("Por favor, preencha todos os campos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Substitui vírgula por ponto antes de converter
            Dim freqText As String = txtFrequencia.Text.Replace(",", ".")
            Dim capText As String = txtCapacitor.Text.Replace(",", ".")

            Dim frequenciaMHz As Double = Double.Parse(freqText, System.Globalization.CultureInfo.InvariantCulture)
            Dim capacitorPF As Double = Double.Parse(capText, System.Globalization.CultureInfo.InvariantCulture)

            If frequenciaMHz <= 0 OrElse capacitorPF <= 0 Then
                MessageBox.Show("Os valores devem ser maiores que zero.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Converter MHz para Hz
            Dim frequenciaHz As Double = frequenciaMHz * 1000000.0

            ' Converter pF para Farad
            Dim CFarad As Double = capacitorPF / 1000000000000.0

            ' Calcular indutância: L = 1 / ((2 * π * f)² * C)
            Dim omega As Double = 2.0 * Math.PI * frequenciaHz
            Dim LHenry As Double = 1.0 / (omega * omega * CFarad)

            ' Converter Henry para µH
            Dim indutanciaUH As Double = LHenry * 1000000.0

            ' Calcular comprimento de onda
            Dim velocidadeLuz As Double = 299792458.0
            Dim comprimentoOnda As Double = velocidadeLuz / frequenciaHz

            ' Formatar resultado
            Dim resultado As New System.Text.StringBuilder()
            resultado.AppendLine("=== RESULTADOS DO CÁLCULO ===")
            resultado.AppendLine()
            resultado.AppendLine("Frequência: " & frequenciaMHz.ToString() & " MHz")
            resultado.AppendLine("Capacitor: " & capacitorPF.ToString() & " pF")
            resultado.AppendLine()
            resultado.AppendLine("Indutância Necessária: " & FormatarIndutancia(indutanciaUH))
            resultado.AppendLine()
            resultado.AppendLine("Comprimento de Onda: " & FormatarComprimento(comprimentoOnda))

            txtResultado.Text = resultado.ToString()

        Catch ex As FormatException
            MessageBox.Show("Formato de número inválido. Use ponto como separador decimal (ex: 7.1)", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Function FormatarComprimento(comp As Double) As String
        If comp >= 1000 Then
            Return (comp / 1000).ToString("F3") & " km"
        ElseIf comp >= 1 Then
            Return comp.ToString("F3") & " m"
        ElseIf comp >= 0.01 Then
            Return (comp * 100).ToString("F3") & " cm"
        Else
            Return (comp * 1000).ToString("F3") & " mm"
        End If
    End Function
End Class