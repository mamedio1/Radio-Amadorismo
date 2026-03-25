Public Class FormConversaoPotencia
    Private txtDBm As TextBox
    Private txtWatts As TextBox
    Private txtmW As TextBox
    Private txtResultado As TextBox
    Private btnDBmParaWatts As Button
    Private btnWattsParaDBm As Button
    Private btnLimpar As Button

    Public Sub New()
        InitializeComponent()
        ConfigurarFormulario()
        AplicarEstiloCyberpunk()  ' ← LINHA ADICIONADA
    End Sub

    Private Sub ConfigurarFormulario()
        Me.Text = "Conversão de Potência - dBm ↔ Watts"
        Me.Size = New Size(550, 450)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False

        ' ===== SEÇÃO dBm =====
        Dim lblTituloDB As New Label()
        lblTituloDB.Text = "dBm para Watts"
        lblTituloDB.Location = New Point(30, 20)
        lblTituloDB.Size = New Size(200, 20)
        lblTituloDB.Font = New Font(lblTituloDB.Font, FontStyle.Bold)
        Me.Controls.Add(lblTituloDB)

        Dim lblDBm As New Label()
        lblDBm.Text = "Potência (dBm):"
        lblDBm.Location = New Point(30, 50)
        lblDBm.Size = New Size(120, 20)
        Me.Controls.Add(lblDBm)

        txtDBm = New TextBox()
        txtDBm.Location = New Point(160, 48)
        txtDBm.Size = New Size(150, 20)
        AddHandler txtDBm.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtDBm)

        Dim lblUnidadeDBm As New Label()
        lblUnidadeDBm.Text = "dBm"
        lblUnidadeDBm.Location = New Point(320, 50)
        lblUnidadeDBm.Size = New Size(40, 20)
        Me.Controls.Add(lblUnidadeDBm)

        btnDBmParaWatts = New Button()
        btnDBmParaWatts.Text = "Converter →"
        btnDBmParaWatts.Location = New Point(370, 45)
        btnDBmParaWatts.Size = New Size(120, 30)
        AddHandler btnDBmParaWatts.Click, AddressOf BtnDBmParaWatts_Click
        Me.Controls.Add(btnDBmParaWatts)

        ' ===== SEÇÃO Watts =====
        Dim lblTituloWatts As New Label()
        lblTituloWatts.Text = "Watts para dBm"
        lblTituloWatts.Location = New Point(30, 100)
        lblTituloWatts.Size = New Size(200, 20)
        lblTituloWatts.Font = New Font(lblTituloWatts.Font, FontStyle.Bold)
        Me.Controls.Add(lblTituloWatts)

        Dim lblWatts As New Label()
        lblWatts.Text = "Potência (Watts):"
        lblWatts.Location = New Point(30, 130)
        lblWatts.Size = New Size(120, 20)
        Me.Controls.Add(lblWatts)

        txtWatts = New TextBox()
        txtWatts.Location = New Point(160, 128)
        txtWatts.Size = New Size(150, 20)
        AddHandler txtWatts.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtWatts)

        Dim lblUnidadeWatts As New Label()
        lblUnidadeWatts.Text = "W"
        lblUnidadeWatts.Location = New Point(320, 130)
        lblUnidadeWatts.Size = New Size(40, 20)
        Me.Controls.Add(lblUnidadeWatts)

        ' ===== OU mW =====
        Dim lblOu As New Label()
        lblOu.Text = "ou"
        lblOu.Location = New Point(140, 155)
        lblOu.Size = New Size(20, 20)
        Me.Controls.Add(lblOu)

        Dim lblmW As New Label()
        lblmW.Text = "Potência (mW):"
        lblmW.Location = New Point(30, 160)
        lblmW.Size = New Size(120, 20)
        Me.Controls.Add(lblmW)

        txtmW = New TextBox()
        txtmW.Location = New Point(160, 158)
        txtmW.Size = New Size(150, 20)
        AddHandler txtmW.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtmW)

        Dim lblUnidademW As New Label()
        lblUnidademW.Text = "mW"
        lblUnidademW.Location = New Point(320, 160)
        lblUnidademW.Size = New Size(40, 20)
        Me.Controls.Add(lblUnidademW)

        btnWattsParaDBm = New Button()
        btnWattsParaDBm.Text = "Converter →"
        btnWattsParaDBm.Location = New Point(370, 140)
        btnWattsParaDBm.Size = New Size(120, 30)
        AddHandler btnWattsParaDBm.Click, AddressOf BtnWattsParaDBm_Click
        Me.Controls.Add(btnWattsParaDBm)

        ' ===== BOTÃO LIMPAR =====
        btnLimpar = New Button()
        btnLimpar.Text = "Limpar Tudo"
        btnLimpar.Location = New Point(200, 200)
        btnLimpar.Size = New Size(100, 30)
        AddHandler btnLimpar.Click, AddressOf BtnLimpar_Click
        Me.Controls.Add(btnLimpar)

        ' ===== RESULTADO =====
        Dim lblResultado As New Label()
        lblResultado.Text = "Resultado:"
        lblResultado.Location = New Point(30, 250)
        lblResultado.Size = New Size(120, 20)
        Me.Controls.Add(lblResultado)

        txtResultado = New TextBox()
        txtResultado.Location = New Point(30, 275)
        txtResultado.Size = New Size(480, 120)
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
        ' Aceita números, ponto, vírgula, sinal de menos e backspace
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c AndAlso e.KeyChar <> ","c AndAlso e.KeyChar <> "-"c AndAlso e.KeyChar <> ChrW(Keys.Back) Then
            e.Handled = True
            Return
        End If

        ' Substitui vírgula por ponto
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

        ' Permite apenas um sinal de menos no início
        If e.KeyChar = "-"c AndAlso (txtBox.Text.Contains("-") OrElse txtBox.SelectionStart <> 0) Then
            e.Handled = True
        End If
    End Sub

    Private Sub BtnDBmParaWatts_Click(sender As Object, e As EventArgs)
        Try
            If String.IsNullOrWhiteSpace(txtDBm.Text) Then
                MessageBox.Show("Por favor, preencha o campo dBm.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim dbmText As String = txtDBm.Text.Replace(",", ".")
            Dim potenciaDBm As Double = Double.Parse(dbmText, System.Globalization.CultureInfo.InvariantCulture)

            ' Converter dBm para Watts: P(W) = 10^((P(dBm) - 30) / 10)
            Dim potenciaWatts As Double = Math.Pow(10, (potenciaDBm - 30.0) / 10.0)
            Dim potenciaEmMiliWatts As Double = potenciaWatts * 1000.0

            ' Formatar resultado
            Dim resultado As New System.Text.StringBuilder()
            resultado.AppendLine("=== CONVERSÃO dBm → Watts ===")
            resultado.AppendLine()
            resultado.AppendLine("Entrada: " & potenciaDBm.ToString() & " dBm")
            resultado.AppendLine()
            resultado.AppendLine("Resultado:")
            resultado.AppendLine("  • " & FormatarPotencia(potenciaWatts))
            resultado.AppendLine("  • " & potenciaEmMiliWatts.ToString("F6") & " mW")
            resultado.AppendLine("  • " & (potenciaEmMiliWatts * 1000.0).ToString("F3") & " µW")

            txtResultado.Text = resultado.ToString()

        Catch ex As FormatException
            MessageBox.Show("Formato de número inválido. Use ponto como separador decimal.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Erro ao calcular: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnWattsParaDBm_Click(sender As Object, e As EventArgs)
        Try
            Dim potenciaWatts As Double = 0

            ' Verifica qual campo foi preenchido
            If Not String.IsNullOrWhiteSpace(txtWatts.Text) Then
                Dim wattsText As String = txtWatts.Text.Replace(",", ".")
                potenciaWatts = Double.Parse(wattsText, System.Globalization.CultureInfo.InvariantCulture)
            ElseIf Not String.IsNullOrWhiteSpace(txtmW.Text) Then
                Dim mWText As String = txtmW.Text.Replace(",", ".")
                Dim valorEmMiliWatts As Double = Double.Parse(mWText, System.Globalization.CultureInfo.InvariantCulture)
                potenciaWatts = valorEmMiliWatts / 1000.0
            Else
                MessageBox.Show("Por favor, preencha o campo Watts ou mW.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If potenciaWatts <= 0 Then
                MessageBox.Show("A potência deve ser maior que zero.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Converter Watts para dBm: P(dBm) = 10 * log10(P(W)) + 30
            Dim potenciaDBm As Double = 10.0 * Math.Log10(potenciaWatts) + 30.0
            Dim valorEmMW As Double = potenciaWatts * 1000.0

            ' Formatar resultado
            Dim resultado As New System.Text.StringBuilder()
            resultado.AppendLine("=== CONVERSÃO Watts → dBm ===")
            resultado.AppendLine()
            resultado.AppendLine("Entrada: " & FormatarPotencia(potenciaWatts))
            resultado.AppendLine("         (" & valorEmMW.ToString("F6") & " mW)")
            resultado.AppendLine()
            resultado.AppendLine("Resultado: " & potenciaDBm.ToString("F3") & " dBm")

            txtResultado.Text = resultado.ToString()

        Catch ex As FormatException
            MessageBox.Show("Formato de número inválido. Use ponto como separador decimal.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Erro ao calcular: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnLimpar_Click(sender As Object, e As EventArgs)
        txtDBm.Clear()
        txtWatts.Clear()
        txtmW.Clear()
        txtResultado.Clear()
    End Sub

    Private Function FormatarPotencia(watts As Double) As String
        If watts >= 1000 Then
            Return (watts / 1000).ToString("F3") & " kW"
        ElseIf watts >= 1 Then
            Return watts.ToString("F6") & " W"
        ElseIf watts >= 0.001 Then
            Return (watts * 1000).ToString("F6") & " mW"
        ElseIf watts >= 0.000001 Then
            Return (watts * 1000000).ToString("F3") & " µW"
        ElseIf watts >= 0.000000001 Then
            Return (watts * 1000000000).ToString("F3") & " nW"
        Else
            Return (watts * 1000000000000.0).ToString("F3") & " pW"
        End If
    End Function
End Class