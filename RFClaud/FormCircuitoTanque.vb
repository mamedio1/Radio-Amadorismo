Public Class FormCircuitoTanque
    Private TabControl1 As TabControl

    ' Controles da aba Air Core
    Private txtFreq As TextBox
    Private txtCap As TextBox
    Private txtDiam As TextBox
    Private txtLen As TextBox
    Private txtWire As TextBox
    Private lblResultFreq As Label
    Private lblResultCap As Label
    Private lblResultInd As Label
    Private lblResultTurns As Label
    Private lblResultWireLen As Label
    Private lblResultQ As Label
    Private lblResultZ As Label
    Private lblResultBW As Label

    Private Sub FormCircuitoTanque_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Calculadora - Circuito Tanque LC"
        Me.Size = New Size(950, 750)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.FromArgb(15, 15, 35) ' Tema dark para combinar

        TabControl1 = New TabControl()
        TabControl1.Dock = DockStyle.Fill
        TabControl1.Font = New Font("Segoe UI", 10)

        ' Aba Circuito Tanque (completa)
        TabControl1.TabPages.Add(CreateAirCoreTab())

        Me.Controls.Add(TabControl1)
    End Sub

    Private Function CreateAirCoreTab() As TabPage
        Dim tab As New TabPage("Circuito Tanque - Núcleo de Ar")
        tab.BackColor = Color.FromArgb(240, 248, 255)

        Dim panel As New Panel()
        panel.Dock = DockStyle.Fill
        panel.AutoScroll = True
        panel.Padding = New Padding(20)

        ' TÍTULO
        Dim lblTitle As New Label()
        lblTitle.Text = "CIRCUITO TANQUE LC - NÚCLEO DE AR"
        lblTitle.Location = New Point(20, 10)
        lblTitle.Size = New Size(600, 35)
        lblTitle.Font = New Font("Segoe UI", 18, FontStyle.Bold)
        lblTitle.ForeColor = Color.DarkBlue
        panel.Controls.Add(lblTitle)

        Dim lblSubtitle As New Label()
        lblSubtitle.Text = "Calcule o indutor ideal para sua frequência de operação"
        lblSubtitle.Location = New Point(20, 45)
        lblSubtitle.Size = New Size(600, 25)
        lblSubtitle.Font = New Font("Segoe UI", 10, FontStyle.Italic)
        lblSubtitle.ForeColor = Color.DarkGreen
        panel.Controls.Add(lblSubtitle)

        Dim yPos As Integer = 90

        ' ========== GRUPO ENTRADA DE DADOS ==========
        Dim grpInput As New GroupBox()
        grpInput.Text = "PARÂMETROS DE ENTRADA"
        grpInput.Font = New Font("Segoe UI", 11, FontStyle.Bold)
        grpInput.Location = New Point(20, yPos)
        grpInput.Size = New Size(450, 320)
        grpInput.BackColor = Color.LightCyan

        Dim yInput As Integer = 35

        ' Frequência
        Dim lblFreq As New Label()
        lblFreq.Text = "Frequência (MHz):"
        lblFreq.Location = New Point(15, yInput)
        lblFreq.Size = New Size(150, 25)
        grpInput.Controls.Add(lblFreq)

        txtFreq = New TextBox()
        txtFreq.Text = "7.150"
        txtFreq.Location = New Point(170, yInput)
        txtFreq.Size = New Size(120, 25)
        txtFreq.Font = New Font("Consolas", 10, FontStyle.Bold)
        grpInput.Controls.Add(txtFreq)

        Dim lblFreqEx As New Label()
        lblFreqEx.Text = "Ex: 3.5, 7, 14, 28, 50, 144"
        lblFreqEx.Font = New Font("Segoe UI", 8, FontStyle.Italic)
        lblFreqEx.ForeColor = Color.Gray
        lblFreqEx.Location = New Point(300, yInput + 3)
        lblFreqEx.Size = New Size(150, 20)
        grpInput.Controls.Add(lblFreqEx)
        yInput += 45

        ' Capacitor
        Dim lblCap As New Label()
        lblCap.Text = "Capacitor (pF):"
        lblCap.Location = New Point(15, yInput)
        lblCap.Size = New Size(150, 25)
        grpInput.Controls.Add(lblCap)

        txtCap = New TextBox()
        txtCap.Text = "100"
        txtCap.Location = New Point(170, yInput)
        txtCap.Size = New Size(120, 25)
        txtCap.Font = New Font("Consolas", 10, FontStyle.Bold)
        grpInput.Controls.Add(txtCap)

        Dim lblCapEx As New Label()
        lblCapEx.Text = "Ex: 10, 47, 100, 220, 470"
        lblCapEx.Font = New Font("Segoe UI", 8, FontStyle.Italic)
        lblCapEx.ForeColor = Color.Gray
        lblCapEx.Location = New Point(300, yInput + 3)
        lblCapEx.Size = New Size(150, 20)
        grpInput.Controls.Add(lblCapEx)
        yInput += 45

        ' Diâmetro
        Dim lblDiam As New Label()
        lblDiam.Text = "Diâmetro bobina (mm):"
        lblDiam.Location = New Point(15, yInput)
        lblDiam.Size = New Size(150, 25)
        grpInput.Controls.Add(lblDiam)

        txtDiam = New TextBox()
        txtDiam.Text = "25"
        txtDiam.Location = New Point(170, yInput)
        txtDiam.Size = New Size(120, 25)
        txtDiam.Font = New Font("Consolas", 10, FontStyle.Bold)
        grpInput.Controls.Add(txtDiam)

        Dim lblDiamEx As New Label()
        lblDiamEx.Text = "Diâmetro interno"
        lblDiamEx.Font = New Font("Segoe UI", 8, FontStyle.Italic)
        lblDiamEx.ForeColor = Color.Gray
        lblDiamEx.Location = New Point(300, yInput + 3)
        lblDiamEx.Size = New Size(150, 20)
        grpInput.Controls.Add(lblDiamEx)
        yInput += 45

        ' Comprimento
        Dim lblLen As New Label()
        lblLen.Text = "Comprimento (mm):"
        lblLen.Location = New Point(15, yInput)
        lblLen.Size = New Size(150, 25)
        grpInput.Controls.Add(lblLen)

        txtLen = New TextBox()
        txtLen.Text = "30"
        txtLen.Location = New Point(170, yInput)
        txtLen.Size = New Size(120, 25)
        txtLen.Font = New Font("Consolas", 10, FontStyle.Bold)
        grpInput.Controls.Add(txtLen)

        Dim lblLenEx As New Label()
        lblLenEx.Text = "Comprimento do enrolamento"
        lblLenEx.Font = New Font("Segoe UI", 8, FontStyle.Italic)
        lblLenEx.ForeColor = Color.Gray
        lblLenEx.Location = New Point(300, yInput + 3)
        lblLenEx.Size = New Size(180, 20)
        grpInput.Controls.Add(lblLenEx)
        yInput += 45

        ' Fio
        Dim lblWire As New Label()
        lblWire.Text = "Fio (AWG):"
        lblWire.Location = New Point(15, yInput)
        lblWire.Size = New Size(150, 25)
        grpInput.Controls.Add(lblWire)

        txtWire = New TextBox()
        txtWire.Text = "18"
        txtWire.Location = New Point(170, yInput)
        txtWire.Size = New Size(120, 25)
        txtWire.Font = New Font("Consolas", 10, FontStyle.Bold)
        grpInput.Controls.Add(txtWire)

        Dim lblWireEx As New Label()
        lblWireEx.Text = "16=grosso  20=médio  24=fino"
        lblWireEx.Font = New Font("Segoe UI", 8, FontStyle.Italic)
        lblWireEx.ForeColor = Color.Gray
        lblWireEx.Location = New Point(300, yInput + 3)
        lblWireEx.Size = New Size(180, 20)
        grpInput.Controls.Add(lblWireEx)
        yInput += 55

        ' Botões
        Dim btnCalc As New Button()
        btnCalc.Text = "CALCULAR"
        btnCalc.Location = New Point(15, yInput)
        btnCalc.Size = New Size(130, 40)
        btnCalc.BackColor = Color.DarkGreen
        btnCalc.ForeColor = Color.White
        btnCalc.Font = New Font("Segoe UI", 11, FontStyle.Bold)
        AddHandler btnCalc.Click, AddressOf btnCalculate_Click
        grpInput.Controls.Add(btnCalc)

        Dim btnOpt As New Button()
        btnOpt.Text = "OTIMIZAR"
        btnOpt.Location = New Point(155, yInput)
        btnOpt.Size = New Size(100, 40)
        btnOpt.BackColor = Color.DarkBlue
        btnOpt.ForeColor = Color.White
        btnOpt.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        AddHandler btnOpt.Click, AddressOf btnOptimize_Click
        grpInput.Controls.Add(btnOpt)

        Dim btnClear As New Button()
        btnClear.Text = "LIMPAR"
        btnClear.Location = New Point(265, yInput)
        btnClear.Size = New Size(80, 40)
        btnClear.BackColor = Color.DarkRed
        btnClear.ForeColor = Color.White
        btnClear.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        AddHandler btnClear.Click, AddressOf btnClear_Click
        grpInput.Controls.Add(btnClear)

        panel.Controls.Add(grpInput)

        ' ========== GRUPO RESULTADOS ==========
        Dim grpResult As New GroupBox()
        grpResult.Text = "RESULTADOS DO CÁLCULO"
        grpResult.Font = New Font("Segoe UI", 11, FontStyle.Bold)
        grpResult.Location = New Point(490, yPos)
        grpResult.Size = New Size(420, 320)
        grpResult.BackColor = Color.LightYellow

        Dim yRes As Integer = 35

        lblResultFreq = CreateResultField(grpResult, "Frequência:", yRes)
        yRes += 40
        lblResultCap = CreateResultField(grpResult, "Capacitor:", yRes)
        yRes += 40
        lblResultInd = CreateResultField(grpResult, "Indutância:", yRes, True)
        yRes += 40
        lblResultTurns = CreateResultField(grpResult, "Nº de Espiras:", yRes, True)
        yRes += 40
        lblResultWireLen = CreateResultField(grpResult, "Fio necessário:", yRes)
        yRes += 40
        lblResultQ = CreateResultField(grpResult, "Fator Q:", yRes)
        yRes += 40
        lblResultZ = CreateResultField(grpResult, "Impedância Z:", yRes)
        yRes += 40
        lblResultBW = CreateResultField(grpResult, "Largura de Banda:", yRes)

        panel.Controls.Add(grpResult)

        ' ========== GRUPO DICAS ==========
        yPos += 340
        Dim grpTips As New GroupBox()
        grpTips.Text = "DICAS E FÓRMULAS"
        grpTips.Font = New Font("Segoe UI", 11, FontStyle.Bold)
        grpTips.Location = New Point(20, yPos)
        grpTips.Size = New Size(890, 120)
        grpTips.BackColor = Color.Honeydew

        Dim lblTips As New Label()
        lblTips.Text = "FÓRMULAS UTILIZADAS:" & vbCrLf &
                      "• Frequência de ressonância: Fr = 1 / (2π√(LC))" & vbCrLf &
                      "• Indutância do solenoide: L = (μ₀ × N² × π × r²) / comprimento" & vbCrLf &
                      "• Fator de qualidade: Q = (2πfL) / R    |    Impedância: Z = √(L/C)" & vbCrLf &
                      "• Bobina de núcleo de ar tem Q alto (>100) mas maior tamanho físico"
        lblTips.Font = New Font("Consolas", 9)
        lblTips.Location = New Point(15, 25)
        lblTips.Size = New Size(860, 90)
        grpTips.Controls.Add(lblTips)

        panel.Controls.Add(grpTips)

        ' ========== TABELA DE REFERÊNCIA ==========
        yPos += 140
        Dim grpRef As New GroupBox()
        grpRef.Text = "TABELA DE REFERÊNCIA RÁPIDA"
        grpRef.Font = New Font("Segoe UI", 11, FontStyle.Bold)
        grpRef.Location = New Point(20, yPos)
        grpRef.Size = New Size(890, 150)
        grpRef.BackColor = Color.Lavender

        Dim lblRef As New Label()
        lblRef.Text = "BANDA        FREQUÊNCIA      CAPACITOR      INDUTÂNCIA      DIAM×COMP      FIO" & vbCrLf &
                     "160m         1.8-2.0 MHz     100-1000 pF    100-500 µH      40×50 mm       AWG 16" & vbCrLf &
                     "80m          3.5-4.0 MHz     100-500 pF     50-200 µH       35×45 mm       AWG 16-18" & vbCrLf &
                     "40m          7.0-7.3 MHz     50-500 pF      20-100 µH       25×30 mm       AWG 18-20" & vbCrLf &
                     "20m          14.0-14.35 MHz  50-250 pF      5-30 µH         15×20 mm       AWG 20-22" & vbCrLf &
                     "10m          28.0-29.7 MHz   20-100 pF      2-8 µH          10×12 mm       AWG 22-24" & vbCrLf &
                     "6m           50-54 MHz       10-50 pF       0.5-3 µH        6×8 mm         AWG 24-26"
        lblRef.Font = New Font("Consolas", 9, FontStyle.Regular)
        lblRef.Location = New Point(15, 25)
        lblRef.Size = New Size(860, 120)
        grpRef.Controls.Add(lblRef)

        panel.Controls.Add(grpRef)

        tab.Controls.Add(panel)
        Return tab
    End Function

    Private Function CreateResultField(parent As Control, label As String, y As Integer, Optional big As Boolean = False) As Label
        Dim lblHeader As New Label()
        lblHeader.Text = label
        lblHeader.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        lblHeader.Location = New Point(15, y)
        lblHeader.Size = New Size(140, 25)
        parent.Controls.Add(lblHeader)

        Dim lbl As New Label()
        lbl.Text = "---"
        If big Then
            lbl.Font = New Font("Consolas", 14, FontStyle.Bold)
            lbl.ForeColor = Color.DarkRed
        Else
            lbl.Font = New Font("Consolas", 11, FontStyle.Bold)
            lbl.ForeColor = Color.DarkBlue
        End If
        lbl.Location = New Point(160, y - 2)
        lbl.Size = New Size(250, 30)
        lbl.BackColor = Color.White
        lbl.BorderStyle = BorderStyle.FixedSingle
        parent.Controls.Add(lbl)
        Return lbl
    End Function

    ' ========== EVENTOS DOS BOTÕES ==========

    Private Sub btnCalculate_Click(sender As Object, e As EventArgs)
        Try
            Dim freqMHz As Double = Double.Parse(txtFreq.Text.Replace(".", ","))
            Dim freq As Double = freqMHz * 1000000.0
            Dim capPF As Double = Double.Parse(txtCap.Text.Replace(".", ","))
            Dim cap As Double = capPF * 0.000000000001
            Dim diamMM As Double = Double.Parse(txtDiam.Text.Replace(".", ","))
            Dim diam As Double = diamMM / 1000.0
            Dim lenMM As Double = Double.Parse(txtLen.Text.Replace(".", ","))
            Dim length As Double = lenMM / 1000.0
            Dim awg As Integer = Integer.Parse(txtWire.Text)

            If freq <= 0 Or cap <= 0 Or diam <= 0 Or length <= 0 Then
                MessageBox.Show("Todos os valores devem ser maiores que zero!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim u0 As Double = 0.0000012566370614
            Dim rho As Double = 0.0000000000168

            Dim L As Double = 1.0 / ((2 * Math.PI * freq) ^ 2 * cap)
            Dim L_uH As Double = L * 1000000.0

            Dim coilRadius As Double = diam / 2.0
            Dim N As Double = Math.Sqrt(L * length / (u0 * Math.PI * coilRadius * coilRadius))
            Dim N_int As Integer = CInt(Math.Round(N))

            Dim L_actual As Double = (u0 * N_int * N_int * Math.PI * coilRadius * coilRadius) / length
            Dim L_actual_uH As Double = L_actual * 1000000.0

            Dim wireLenM As Double = N_int * 2 * Math.PI * coilRadius
            Dim wireLenCM As Double = wireLenM * 100

            Dim wireMm As Double = 0.127 * 92 ^ ((36 - awg) / 39.0)
            Dim wireRadius As Double = wireMm / 2000.0

            Dim skinDepth As Double = Math.Sqrt((2 * rho) / (2 * Math.PI * freq * u0))
            Dim area As Double = Math.PI * wireRadius * wireRadius
            If skinDepth < wireRadius Then
                area = Math.PI * (wireRadius * wireRadius - (wireRadius - skinDepth) * (wireRadius - skinDepth))
            End If
            Dim R_wire As Double = (rho * wireLenM) / area

            Dim XL As Double = 2 * Math.PI * freq * L_actual
            Dim Q As Double = XL / R_wire

            Dim Z As Double = Math.Sqrt(L_actual / cap)

            Dim BW As Double = freq / Q

            lblResultFreq.Text = String.Format("{0:F3} MHz", freqMHz)
            lblResultCap.Text = String.Format("{0} pF", capPF)
            lblResultInd.Text = String.Format("{0:F3} µH", L_actual_uH)
            lblResultTurns.Text = String.Format("{0} espiras", N_int)

            If wireLenCM >= 100 Then
                lblResultWireLen.Text = String.Format("{0:F2} m ({1:F0} cm)", wireLenM, wireLenCM)
            Else
                lblResultWireLen.Text = String.Format("{0:F1} cm", wireLenCM)
            End If

            lblResultQ.Text = String.Format("{0:F0} (alta qualidade)", Q)
            lblResultZ.Text = String.Format("{0:F1} Ω", Z)

            If BW >= 1000 Then
                lblResultBW.Text = String.Format("{0:F2} kHz", BW / 1000)
            Else
                lblResultBW.Text = String.Format("{0:F0} Hz", BW)
            End If

            If Q > 100 Then
                lblResultQ.ForeColor = Color.DarkGreen
            ElseIf Q > 50 Then
                lblResultQ.ForeColor = Color.DarkOrange
            Else
                lblResultQ.ForeColor = Color.Red
            End If

        Catch ex As Exception
            MessageBox.Show("Erro no cálculo: " & ex.Message & vbCrLf & "Verifique se todos os campos estão preenchidos corretamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnOptimize_Click(sender As Object, e As EventArgs)
        Try
            Dim freq As Double = Double.Parse(txtFreq.Text.Replace(".", ","))

            If freq < 1 Or freq > 1000 Then
                MessageBox.Show("Frequência fora da faixa esperada (1-1000 MHz)", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If freq < 5 Then
                txtCap.Text = "470"
                txtDiam.Text = "40"
                txtLen.Text = "50"
                txtWire.Text = "16"
            ElseIf freq < 10 Then
                txtCap.Text = "220"
                txtDiam.Text = "30"
                txtLen.Text = "35"
                txtWire.Text = "18"
            ElseIf freq < 20 Then
                txtCap.Text = "100"
                txtDiam.Text = "20"
                txtLen.Text = "25"
                txtWire.Text = "20"
            ElseIf freq < 35 Then
                txtCap.Text = "47"
                txtDiam.Text = "15"
                txtLen.Text = "18"
                txtWire.Text = "22"
            ElseIf freq < 60 Then
                txtCap.Text = "22"
                txtDiam.Text = "10"
                txtLen.Text = "12"
                txtWire.Text = "24"
            Else
                txtCap.Text = "10"
                txtDiam.Text = "6"
                txtLen.Text = "8"
                txtWire.Text = "26"
            End If

            MessageBox.Show(String.Format(
                "Valores otimizados para {0:F2} MHz:" & vbCrLf & vbCrLf &
                "Capacitor: {1} pF" & vbCrLf &
                "Diâmetro: {2} mm" & vbCrLf &
                "Comprimento: {3} mm" & vbCrLf &
                "Fio AWG: {4}" & vbCrLf & vbCrLf &
                "Clique em CALCULAR para ver os resultados!",
                freq, txtCap.Text, txtDiam.Text, txtLen.Text, txtWire.Text),
                "Otimização Concluída", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch
            MessageBox.Show("Informe uma frequência válida primeiro!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs)
        txtFreq.Text = "7.150"
        txtCap.Text = "100"
        txtDiam.Text = "25"
        txtLen.Text = "30"
        txtWire.Text = "18"

        lblResultFreq.Text = "---"
        lblResultCap.Text = "---"
        lblResultInd.Text = "---"
        lblResultTurns.Text = "---"
        lblResultWireLen.Text = "---"
        lblResultQ.Text = "---"
        lblResultZ.Text = "---"
        lblResultBW.Text = "---"

        lblResultQ.ForeColor = Color.DarkBlue
    End Sub
End Class