Public Class FormComprimentoOnda
    Private txtCapacitor As TextBox
    Private txtIndutancia As TextBox
    Private txtResultado As TextBox
    Private btnCalcular As Button

    Public Sub New()
        InitializeComponent()
        ConfigurarFormulario()
        AplicarEstiloCyberpunk()  ' ADICIONAR ESTA LINHA
    End Sub

    Private Sub ConfigurarFormulario()
        Me.Text = "Cálculo de Comprimento de Onda - Tanque LC"
        Me.Size = New Size(500, 350)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False

        ' Label Capacitor
        Dim lblCapacitor As New Label()
        lblCapacitor.Text = "Capacitor (pF):"
        lblCapacitor.Location = New Point(30, 30)
        lblCapacitor.Size = New Size(120, 20)
        Me.Controls.Add(lblCapacitor)

        ' TextBox Capacitor
        txtCapacitor = New TextBox()
        txtCapacitor.Location = New Point(160, 28)
        txtCapacitor.Size = New Size(150, 20)
        AddHandler txtCapacitor.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtCapacitor)

        ' Label unidade pF
        Dim lblUnidadepF As New Label()
        lblUnidadepF.Text = "pF"
        lblUnidadepF.Location = New Point(320, 30)
        lblUnidadepF.Size = New Size(30, 20)
        Me.Controls.Add(lblUnidadepF)

        ' Label Indutância
        Dim lblIndutancia As New Label()
        lblIndutancia.Text = "Indutância (µH):"
        lblIndutancia.Location = New Point(30, 70)
        lblIndutancia.Size = New Size(120, 20)
        Me.Controls.Add(lblIndutancia)

        ' TextBox Indutância
        txtIndutancia = New TextBox()
        txtIndutancia.Location = New Point(160, 68)
        txtIndutancia.Size = New Size(150, 20)
        AddHandler txtIndutancia.KeyPress, AddressOf ValidarEntrada
        Me.Controls.Add(txtIndutancia)

        ' Label unidade µH
        Dim lblUnidadeuH As New Label()
        lblUnidadeuH.Text = "µH"
        lblUnidadeuH.Location = New Point(320, 70)
        lblUnidadeuH.Size = New Size(30, 20)
        Me.Controls.Add(lblUnidadeuH)

        ' Botão Calcular
        btnCalcular = New Button()
        btnCalcular.Text = "Calcular"
        btnCalcular.Location = New Point(160, 110)
        btnCalcular.Size = New Size(100, 30)
        AddHandler btnCalcular.Click, AddressOf BtnCalcular_Click
        Me.Controls.Add(btnCalcular)

        Dim btnExportarPDF As New Button()
        btnExportarPDF.Text = "Exportar PDF"
        btnExportarPDF.Location = New Point(270, 110)
        btnExportarPDF.Size = New Size(100, 30)
        AddHandler btnExportarPDF.Click, AddressOf BtnExportarPDF_Click
        Me.Controls.Add(btnExportarPDF)

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

    Private Sub AplicarEstiloCyberpunk()
        ' Fundo do formulário
        Me.BackColor = Color.FromArgb(20, 20, 40)

        ' Estilizar todos os Labels
        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is Label Then
                ctrl.ForeColor = Color.FromArgb(0, 255, 255) ' Ciano
                ctrl.Font = New Font("Segoe UI", 9, FontStyle.Bold)
            ElseIf TypeOf ctrl Is TextBox Then
                Dim txt As TextBox = DirectCast(ctrl, TextBox)
                txt.BackColor = Color.FromArgb(30, 30, 60)
                txt.ForeColor = Color.FromArgb(100, 255, 100) ' Verde neon
                txt.Font = New Font("Consolas", 10)
                txt.BorderStyle = BorderStyle.FixedSingle
            ElseIf TypeOf ctrl Is Button Then
                Dim btn As Button = DirectCast(ctrl, Button)
                btn.BackColor = Color.FromArgb(255, 0, 150) ' Rosa neon
                btn.ForeColor = Color.White
                btn.FlatStyle = FlatStyle.Flat
                btn.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                btn.FlatAppearance.BorderColor = Color.FromArgb(255, 100, 255)
                btn.FlatAppearance.BorderSize = 2
            End If
        Next

        ' Estilizar resultado especificamente
        txtResultado.BackColor = Color.FromArgb(10, 10, 25)
        txtResultado.ForeColor = Color.FromArgb(0, 255, 255)
        txtResultado.Font = New Font("Courier New", 9)
    End Sub

    ' ... resto do código permanece igual ...

    Private Sub ValidarEntrada(sender As Object, e As KeyPressEventArgs)
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> "."c AndAlso e.KeyChar <> ChrW(Keys.Back) Then
            e.Handled = True
        End If

        Dim txt As TextBox = DirectCast(sender, TextBox)
        If e.KeyChar = "."c AndAlso txt.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub BtnCalcular_Click(sender As Object, e As EventArgs)
        Try
            If String.IsNullOrWhiteSpace(txtCapacitor.Text) OrElse String.IsNullOrWhiteSpace(txtIndutancia.Text) Then
                MessageBox.Show("Por favor, preencha todos os campos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim capacitor As Double = Convert.ToDouble(txtCapacitor.Text)
            Dim indutancia As Double = Convert.ToDouble(txtIndutancia.Text)

            If capacitor <= 0 OrElse indutancia <= 0 Then
                MessageBox.Show("Os valores devem ser maiores que zero.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim C As Double = capacitor * 0.000000000001
            Dim L As Double = indutancia * 0.000001

            Dim frequencia As Double = 1 / (2 * Math.PI * Math.Sqrt(L * C))

            Dim velocidadeLuz As Double = 299792458
            Dim comprimentoOnda As Double = velocidadeLuz / frequencia

            Dim resultado As New System.Text.StringBuilder()
            resultado.AppendLine("=== RESULTADOS DO TANQUE LC ===")
            resultado.AppendLine()
            resultado.AppendLine($"Capacitor: {capacitor} pF")
            resultado.AppendLine($"Indutância: {indutancia} µH")
            resultado.AppendLine()
            resultado.AppendLine($"Frequência de Ressonância: {FormatarFrequencia(frequencia)}")
            resultado.AppendLine()
            resultado.AppendLine($"Comprimento de Onda: {FormatarComprimento(comprimentoOnda)}")

            txtResultado.Text = resultado.ToString()

        Catch ex As FormatException
            MessageBox.Show("Formato de número inválido. Use ponto como separador decimal.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show($"Erro ao calcular: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function FormatarFrequencia(freq As Double) As String
        If freq >= 1000000000 Then
            Return $"{(freq / 1000000000):F3} GHz"
        ElseIf freq >= 1000000 Then
            Return $"{(freq / 1000000):F3} MHz"
        ElseIf freq >= 1000 Then
            Return $"{(freq / 1000):F3} kHz"
        Else
            Return $"{freq:F3} Hz"
        End If
    End Function

    Private Function FormatarComprimento(comp As Double) As String
        If comp >= 1000 Then
            Return $"{(comp / 1000):F3} km"
        ElseIf comp >= 1 Then
            Return $"{comp:F3} m"
        ElseIf comp >= 0.01 Then
            Return $"{(comp * 100):F3} cm"
        Else
            Return $"{(comp * 1000):F3} mm"
        End If
    End Function
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
            saveDialog.FileName = "ComprimentoOnda_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".pdf"

            If saveDialog.ShowDialog() = DialogResult.OK Then
                ' Gerar PDF
                GeradorPDF.ExportarParaPDF("Cálculo de Comprimento de Onda - Tanque LC", txtResultado.Text, saveDialog.FileName)

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