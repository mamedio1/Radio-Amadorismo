Imports System.Drawing
Imports System.Threading
Imports MathNet.Numerics
Imports MathNet.Numerics.IntegralTransforms
Imports NAudio.Wave
Imports NAudio.Wave.SampleProviders
Imports System

Public Class FormGeradorSinais
    Inherits Form

    ' ========== COMPONENTES ==========
    Private WithEvents trackBarFrequencia As TrackBar
    Private WithEvents trackBarVolume As TrackBar
    Private WithEvents rbSenoidal As RadioButton
    Private WithEvents rbTriangular As RadioButton
    Private WithEvents rbDenteSerra As RadioButton
    Private WithEvents rbQuadrada As RadioButton
    Private WithEvents rbRuidoBranco As RadioButton
    Private chartOnda As System.Windows.Forms.DataVisualization.Charting.Chart
    Private chartEspectro As System.Windows.Forms.DataVisualization.Charting.Chart
    Private WithEvents btnPlayStop As Button
    Private WithEvents btnAdicionarCanal As Button
    Private WithEvents timerAtualizacao As System.Windows.Forms.Timer
    Private lblFrequencia As Label
    Private lblVolume As Label
    Private panelControles As Panel
    Private groupBoxTipoOnda As GroupBox
    Private panelMixer As Panel
    Private flowLayoutMixer As FlowLayoutPanel

    ' ========== VARIÁVEIS ==========
    Private frequenciaAtual As Double = 440.0
    Private tipoOnda As String = "Senoidal"
    Private isPlaying As Boolean = False
    Private volumeAtual As Double = 0.3
    Private waveOut As WaveOutEvent
    Private signalGenerator As SignalGenerator
    Private mixerProvider As MixingSampleProvider
    Private mixerChannels As New List(Of MixerChannel)
    Private bufferFFT(8191) As MathNet.Numerics.Complex32
    Private bufferPosition As Integer = 0

    Public Sub New()
        MyBase.New()

        ' Configurações básicas ANTES de criar componentes
        Me.Text = "Gerador de Sinais"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.FromArgb(30, 30, 30)

        ' Chamar métodos de configuração
        Try
            ConfigurarInterface()
            ConfigurarGraficos()

            ' Ajustar tamanho DEPOIS de criar tudo
            Me.ClientSize = New Size(1180, 750)

        Catch ex As Exception
            MessageBox.Show("Erro ao inicializar: " & ex.Message & vbCrLf & ex.StackTrace, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ConfigurarInterface()
        panelControles = New Panel()
        panelControles.Location = New Point(20, 20)
        panelControles.Size = New Size(300, 650)
        panelControles.BackColor = Color.FromArgb(45, 45, 48)
        panelControles.BorderStyle = BorderStyle.FixedSingle
        panelControles.AutoScroll = True
        Me.Controls.Add(panelControles)

        Dim yPos As Integer = 10

        Dim lblTitulo As New Label()
        lblTitulo.Text = "CONTROLES"
        lblTitulo.Location = New Point(10, yPos)
        lblTitulo.Size = New Size(280, 30)
        lblTitulo.Font = New Font("Segoe UI", 14, FontStyle.Bold)
        lblTitulo.ForeColor = Color.FromArgb(0, 255, 136)
        lblTitulo.TextAlign = ContentAlignment.MiddleCenter
        panelControles.Controls.Add(lblTitulo)

        yPos += 40

        lblFrequencia = New Label()
        lblFrequencia.Text = "Frequência: 440 Hz"
        lblFrequencia.Location = New Point(10, yPos)
        lblFrequencia.Size = New Size(280, 25)
        lblFrequencia.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        lblFrequencia.ForeColor = Color.FromArgb(0, 200, 255)
        panelControles.Controls.Add(lblFrequencia)

        yPos += 30

        trackBarFrequencia = New TrackBar()
        trackBarFrequencia.Location = New Point(10, yPos)
        trackBarFrequencia.Size = New Size(280, 45)
        trackBarFrequencia.Minimum = 20
        trackBarFrequencia.Maximum = 2000
        trackBarFrequencia.Value = 440
        trackBarFrequencia.TickFrequency = 100
        trackBarFrequencia.BackColor = Color.FromArgb(45, 45, 48)
        panelControles.Controls.Add(trackBarFrequencia)

        yPos += 50

        Dim lblMinFreq As New Label()
        lblMinFreq.Text = "20 Hz"
        lblMinFreq.Location = New Point(10, yPos)
        lblMinFreq.Size = New Size(50, 20)
        lblMinFreq.ForeColor = Color.Gray
        lblMinFreq.Font = New Font("Segoe UI", 8)
        panelControles.Controls.Add(lblMinFreq)

        Dim lblMaxFreq As New Label()
        lblMaxFreq.Text = "2000 Hz"
        lblMaxFreq.Location = New Point(230, yPos)
        lblMaxFreq.Size = New Size(60, 20)
        lblMaxFreq.ForeColor = Color.Gray
        lblMaxFreq.Font = New Font("Segoe UI", 8)
        panelControles.Controls.Add(lblMaxFreq)

        yPos += 30

        lblVolume = New Label()
        lblVolume.Text = "Volume: 30%"
        lblVolume.Location = New Point(10, yPos)
        lblVolume.Size = New Size(280, 25)
        lblVolume.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        lblVolume.ForeColor = Color.FromArgb(255, 180, 0)
        panelControles.Controls.Add(lblVolume)

        yPos += 30

        trackBarVolume = New TrackBar()
        trackBarVolume.Location = New Point(10, yPos)
        trackBarVolume.Size = New Size(280, 45)
        trackBarVolume.Minimum = 0
        trackBarVolume.Maximum = 100
        trackBarVolume.Value = 30
        trackBarVolume.TickFrequency = 10
        trackBarVolume.BackColor = Color.FromArgb(45, 45, 48)
        panelControles.Controls.Add(trackBarVolume)

        yPos += 50

        Dim lblMinVol As New Label()
        lblMinVol.Text = "0%"
        lblMinVol.Location = New Point(10, yPos)
        lblMinVol.Size = New Size(50, 20)
        lblMinVol.ForeColor = Color.Gray
        lblMinVol.Font = New Font("Segoe UI", 8)
        panelControles.Controls.Add(lblMinVol)

        Dim lblMaxVol As New Label()
        lblMaxVol.Text = "100%"
        lblMaxVol.Location = New Point(245, yPos)
        lblMaxVol.Size = New Size(60, 20)
        lblMaxVol.ForeColor = Color.Gray
        lblMaxVol.Font = New Font("Segoe UI", 8)
        panelControles.Controls.Add(lblMaxVol)

        yPos += 30

        groupBoxTipoOnda = New GroupBox()
        groupBoxTipoOnda.Text = "Tipo de Onda"
        groupBoxTipoOnda.Location = New Point(10, yPos)
        groupBoxTipoOnda.Size = New Size(280, 180)
        groupBoxTipoOnda.ForeColor = Color.FromArgb(0, 255, 136)
        groupBoxTipoOnda.Font = New Font("Segoe UI", 11, FontStyle.Bold)
        panelControles.Controls.Add(groupBoxTipoOnda)

        rbSenoidal = New RadioButton()
        rbSenoidal.Text = "Senoidal"
        rbSenoidal.Location = New Point(15, 30)
        rbSenoidal.Size = New Size(250, 25)
        rbSenoidal.Checked = True
        rbSenoidal.ForeColor = Color.White
        rbSenoidal.Font = New Font("Segoe UI", 10)
        groupBoxTipoOnda.Controls.Add(rbSenoidal)

        rbTriangular = New RadioButton()
        rbTriangular.Text = "Triangular"
        rbTriangular.Location = New Point(15, 60)
        rbTriangular.Size = New Size(250, 25)
        rbTriangular.ForeColor = Color.White
        rbTriangular.Font = New Font("Segoe UI", 10)
        groupBoxTipoOnda.Controls.Add(rbTriangular)

        rbDenteSerra = New RadioButton()
        rbDenteSerra.Text = "Dente de Serra"
        rbDenteSerra.Location = New Point(15, 90)
        rbDenteSerra.Size = New Size(250, 25)
        rbDenteSerra.ForeColor = Color.White
        rbDenteSerra.Font = New Font("Segoe UI", 10)
        groupBoxTipoOnda.Controls.Add(rbDenteSerra)

        rbQuadrada = New RadioButton()
        rbQuadrada.Text = "Quadrada"
        rbQuadrada.Location = New Point(15, 120)
        rbQuadrada.Size = New Size(250, 25)
        rbQuadrada.ForeColor = Color.White
        rbQuadrada.Font = New Font("Segoe UI", 10)
        groupBoxTipoOnda.Controls.Add(rbQuadrada)

        rbRuidoBranco = New RadioButton()
        rbRuidoBranco.Text = "Ruído Branco"
        rbRuidoBranco.Location = New Point(15, 150)
        rbRuidoBranco.Size = New Size(250, 25)
        rbRuidoBranco.ForeColor = Color.White
        rbRuidoBranco.Font = New Font("Segoe UI", 10)
        groupBoxTipoOnda.Controls.Add(rbRuidoBranco)

        yPos += 190

        btnPlayStop = New Button()
        btnPlayStop.Text = "▶ INICIAR"
        btnPlayStop.Location = New Point(10, yPos)
        btnPlayStop.Size = New Size(280, 50)
        btnPlayStop.Font = New Font("Segoe UI", 14, FontStyle.Bold)
        btnPlayStop.BackColor = Color.FromArgb(0, 120, 215)
        btnPlayStop.ForeColor = Color.White
        btnPlayStop.FlatStyle = FlatStyle.Flat
        btnPlayStop.FlatAppearance.BorderSize = 0
        btnPlayStop.Cursor = Cursors.Hand
        panelControles.Controls.Add(btnPlayStop)

        yPos += 60

        btnAdicionarCanal = New Button()
        btnAdicionarCanal.Text = "➕ ADICIONAR CANAL"
        btnAdicionarCanal.Location = New Point(10, yPos)
        btnAdicionarCanal.Size = New Size(280, 40)
        btnAdicionarCanal.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        btnAdicionarCanal.BackColor = Color.FromArgb(100, 65, 165)
        btnAdicionarCanal.ForeColor = Color.White
        btnAdicionarCanal.FlatStyle = FlatStyle.Flat
        btnAdicionarCanal.FlatAppearance.BorderSize = 0
        btnAdicionarCanal.Cursor = Cursors.Hand
        panelControles.Controls.Add(btnAdicionarCanal)

        timerAtualizacao = New System.Windows.Forms.Timer()
        timerAtualizacao.Interval = 150
    End Sub

    Private Sub ConfigurarGraficos()
        Try
            ' ========== CHART ONDA ==========
            chartOnda = New System.Windows.Forms.DataVisualization.Charting.Chart()
            chartOnda.Location = New Point(340, 20)
            chartOnda.Size = New Size(820, 280)
            chartOnda.BackColor = Color.FromArgb(30, 30, 30)

            Dim chartAreaOnda As New System.Windows.Forms.DataVisualization.Charting.ChartArea("AreaOnda")
            chartAreaOnda.BackColor = Color.FromArgb(26, 26, 46)
            chartAreaOnda.AxisX.MajorGrid.LineColor = Color.FromArgb(51, 51, 51)
            chartAreaOnda.AxisY.MajorGrid.LineColor = Color.FromArgb(51, 51, 51)
            chartAreaOnda.AxisX.LabelStyle.ForeColor = Color.Gray
            chartAreaOnda.AxisY.LabelStyle.ForeColor = Color.Gray
            chartAreaOnda.AxisX.LineColor = Color.Gray
            chartAreaOnda.AxisY.LineColor = Color.Gray
            chartOnda.ChartAreas.Add(chartAreaOnda)

            Dim seriesOnda As New System.Windows.Forms.DataVisualization.Charting.Series("Onda")
            seriesOnda.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
            seriesOnda.Color = Color.FromArgb(0, 255, 136)
            seriesOnda.BorderWidth = 2
            chartOnda.Series.Add(seriesOnda)

            Dim titleOnda As New System.Windows.Forms.DataVisualization.Charting.Title("FORMA DE ONDA")
            titleOnda.Font = New Font("Segoe UI", 12, FontStyle.Bold)
            titleOnda.ForeColor = Color.FromArgb(0, 255, 136)
            chartOnda.Titles.Add(titleOnda)

            Me.Controls.Add(chartOnda)

            ' ========== CHART ESPECTRO ==========
            chartEspectro = New System.Windows.Forms.DataVisualization.Charting.Chart()
            chartEspectro.Location = New Point(340, 320)
            chartEspectro.Size = New Size(820, 300)
            chartEspectro.BackColor = Color.FromArgb(30, 30, 30)

            Dim chartAreaEspectro As New System.Windows.Forms.DataVisualization.Charting.ChartArea("AreaEspectro")
            chartAreaEspectro.BackColor = Color.FromArgb(26, 26, 46)
            chartAreaEspectro.AxisX.MajorGrid.LineColor = Color.FromArgb(51, 51, 51)
            chartAreaEspectro.AxisY.MajorGrid.LineColor = Color.FromArgb(51, 51, 51)
            chartAreaEspectro.AxisX.LabelStyle.ForeColor = Color.Gray
            chartAreaEspectro.AxisY.LabelStyle.ForeColor = Color.Gray
            chartAreaEspectro.AxisX.LineColor = Color.Gray
            chartAreaEspectro.AxisY.LineColor = Color.Gray
            chartAreaEspectro.AxisX.Title = "Frequência (Hz)"
            chartAreaEspectro.AxisY.Title = "Amplitude"
            chartAreaEspectro.AxisX.TitleForeColor = Color.Gray
            chartAreaEspectro.AxisY.TitleForeColor = Color.Gray
            chartEspectro.ChartAreas.Add(chartAreaEspectro)

            Dim seriesEspectro As New System.Windows.Forms.DataVisualization.Charting.Series("Espectro")
            seriesEspectro.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column
            seriesEspectro.Color = Color.FromArgb(0, 200, 255)
            chartEspectro.Series.Add(seriesEspectro)

            Dim titleEspectro As New System.Windows.Forms.DataVisualization.Charting.Title("ANALISADOR DE ESPECTRO")
            titleEspectro.Font = New Font("Segoe UI", 12, FontStyle.Bold)
            titleEspectro.ForeColor = Color.FromArgb(0, 200, 255)
            chartEspectro.Titles.Add(titleEspectro)

            Me.Controls.Add(chartEspectro)

            ' ========== ATUALIZAR E CONFIGURAR MIXER ==========
            AtualizarGraficoOnda()
            ConfigurarMixer()

        Catch ex As Exception
            MessageBox.Show("Erro ao configurar gráficos: " & ex.Message & vbCrLf & ex.StackTrace, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ConfigurarMixer()
        panelMixer = New Panel()
        panelMixer.Location = New Point(340, 640)
        panelMixer.Size = New Size(820, 80)
        panelMixer.BackColor = Color.FromArgb(45, 45, 48)
        panelMixer.BorderStyle = BorderStyle.FixedSingle
        panelMixer.AutoScroll = True
        Me.Controls.Add(panelMixer)

        Dim lblMixerTitulo As New Label()
        lblMixerTitulo.Text = "MIXER - CANAIS ADICIONAIS"
        lblMixerTitulo.Location = New Point(10, 10)
        lblMixerTitulo.Size = New Size(800, 25)
        lblMixerTitulo.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        lblMixerTitulo.ForeColor = Color.FromArgb(100, 200, 255)
        panelMixer.Controls.Add(lblMixerTitulo)

        flowLayoutMixer = New FlowLayoutPanel()
        flowLayoutMixer.Location = New Point(10, 40)
        flowLayoutMixer.Size = New Size(790, 30)
        flowLayoutMixer.AutoScroll = True
        flowLayoutMixer.FlowDirection = FlowDirection.LeftToRight
        panelMixer.Controls.Add(flowLayoutMixer)
    End Sub

    Private Sub trackBarFrequencia_Scroll(sender As Object, e As EventArgs) Handles trackBarFrequencia.Scroll
        frequenciaAtual = trackBarFrequencia.Value
        lblFrequencia.Text = "Frequência: " & frequenciaAtual.ToString() & " Hz"

        If signalGenerator IsNot Nothing Then
            signalGenerator.Frequency = frequenciaAtual
        End If

        AtualizarGraficoOnda()
        If isPlaying Then
            AtualizarGraficoEspectro()
        End If
    End Sub

    Private Sub trackBarVolume_Scroll(sender As Object, e As EventArgs) Handles trackBarVolume.Scroll
        volumeAtual = trackBarVolume.Value / 100.0
        lblVolume.Text = "Volume: " & trackBarVolume.Value.ToString() & "%"

        If signalGenerator IsNot Nothing Then
            signalGenerator.Volume = volumeAtual
        End If
    End Sub

    Private Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles rbSenoidal.CheckedChanged, rbTriangular.CheckedChanged, rbDenteSerra.CheckedChanged, rbQuadrada.CheckedChanged, rbRuidoBranco.CheckedChanged
        If rbSenoidal.Checked Then
            tipoOnda = "Senoidal"
        ElseIf rbTriangular.Checked Then
            tipoOnda = "Triangular"
        ElseIf rbDenteSerra.Checked Then
            tipoOnda = "Dente de Serra"
        ElseIf rbQuadrada.Checked Then
            tipoOnda = "Quadrada"
        ElseIf rbRuidoBranco.Checked Then
            tipoOnda = "Ruído Branco"
        End If

        If signalGenerator IsNot Nothing Then
            signalGenerator.WaveType = tipoOnda
        End If

        AtualizarGraficoOnda()
    End Sub

    Private Sub AtualizarGraficoOnda()
        If chartOnda Is Nothing Then Return
        If chartOnda.Series.Count = 0 Then Return

        Try
            chartOnda.Series("Onda").Points.Clear()

            Dim pontos As Integer = 500
            Dim ciclos As Integer = 3
            Dim rnd As Random = New Random()

            For i As Integer = 0 To pontos - 1
                Dim t As Double = (i / pontos) * ciclos * 2 * Math.PI
                Dim y As Double

                Select Case tipoOnda
                    Case "Senoidal"
                        y = Math.Sin(t)
                    Case "Triangular"
                        y = (2 / Math.PI) * Math.Asin(Math.Sin(t))
                    Case "Dente de Serra"
                        y = 2 * ((t / (2 * Math.PI)) Mod 1) - 1
                    Case "Quadrada"
                        y = If(Math.Sin(t) >= 0, 1.0, -1.0)
                    Case "Ruído Branco"
                        y = (rnd.NextDouble() * 2.0) - 1.0
                    Case Else
                        y = Math.Sin(t)
                End Select

                chartOnda.Series("Onda").Points.AddXY(i, y)
            Next

            If chartOnda.ChartAreas.Count > 0 Then
                chartOnda.ChartAreas(0).AxisX.Minimum = 0
                chartOnda.ChartAreas(0).AxisX.Maximum = pontos
                chartOnda.ChartAreas(0).AxisY.Minimum = -1.2
                chartOnda.ChartAreas(0).AxisY.Maximum = 1.2
            End If

        Catch ex As Exception
            MessageBox.Show("Erro ao atualizar gráfico de onda: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub AtualizarGraficoEspectro()
        If chartEspectro Is Nothing OrElse chartEspectro.Series.Count = 0 Then Return
        If bufferPosition < bufferFFT.Length Then Return

        Try
            chartEspectro.Series("Espectro").Points.Clear()

            ' Aplicar janela de Hanning
            For i As Integer = 0 To bufferFFT.Length - 1
                Dim window As Double = 0.5 * (1.0 - Math.Cos(2.0 * Math.PI * i / (bufferFFT.Length - 1)))
                bufferFFT(i) = New MathNet.Numerics.Complex32(CSng(bufferFFT(i).Real * window), 0)
            Next

            ' Executar FFT
            Fourier.Forward(bufferFFT, FourierOptions.Matlab)

            ' Configurações
            Dim sampleRate As Integer = 44100
            Dim maxFreqDisplay As Double = 2000 ' Frequência máxima a exibir
            Dim numBars As Integer = 100 ' Número de barras a plotar
            Dim freqPerBin As Double = sampleRate / bufferFFT.Length

            ' Calcular até qual bin precisamos ir para chegar a 2000 Hz
            Dim maxBin As Integer = Math.Min(CInt(maxFreqDisplay / freqPerBin), bufferFFT.Length \ 2)

            ' Intervalo entre barras para distribuir uniformemente
            Dim binStep As Double = maxBin / numBars

            For i As Integer = 0 To numBars - 1
                Dim binIndex As Integer = CInt(i * binStep)
                If binIndex >= bufferFFT.Length Then Exit For

                Dim freq As Double = binIndex * freqPerBin
                Dim magnitude As Double = bufferFFT(binIndex).Magnitude * 2.0 / bufferFFT.Length

                ' Converter para dB
                magnitude = 20 * Math.Log10(magnitude + 0.0001)
                magnitude = Math.Max(0, magnitude + 100) ' Normalizar

                Dim ponto = chartEspectro.Series("Espectro").Points.AddXY(freq, magnitude)

                ' Destacar frequência fundamental e harmônicos
                If Math.Abs(freq - frequenciaAtual) < 30 Then
                    ' Frequência fundamental em rosa
                    chartEspectro.Series("Espectro").Points(ponto).Color = Color.FromArgb(255, 0, 102)
                ElseIf tipoOnda <> "Senoidal" AndAlso tipoOnda <> "Ruído Branco" Then
                    ' Destacar harmônicos (múltiplos da fundamental)
                    For h As Integer = 2 To 10
                        If Math.Abs(freq - (frequenciaAtual * h)) < 30 Then
                            chartEspectro.Series("Espectro").Points(ponto).Color = Color.FromArgb(255, 150, 0)
                            Exit For
                        End If
                    Next
                End If
            Next

            ' Ajustar eixos
            If chartEspectro.ChartAreas.Count > 0 Then
                chartEspectro.ChartAreas(0).AxisX.Minimum = 0
                chartEspectro.ChartAreas(0).AxisX.Maximum = maxFreqDisplay
                chartEspectro.ChartAreas(0).AxisY.Minimum = 0
                chartEspectro.ChartAreas(0).AxisY.Maximum = 100
            End If

            bufferPosition = 0

        Catch ex As Exception
            bufferPosition = 0
            Debug.WriteLine("Erro espectro: " & ex.Message)
        End Try
    End Sub

    Private Sub btnPlayStop_Click(sender As Object, e As EventArgs) Handles btnPlayStop.Click
        If chartEspectro Is Nothing Then
            MessageBox.Show("ERRO: chartEspectro está Nothing!", "Debug")
            Return
        End If

        If Not isPlaying Then
            Try
                isPlaying = True
                btnPlayStop.Text = "■ PARAR"
                btnPlayStop.BackColor = Color.FromArgb(200, 0, 0)

                bufferPosition = 0
                Array.Clear(bufferFFT, 0, bufferFFT.Length)

                signalGenerator = New SignalGenerator(44100)
                signalGenerator.Frequency = frequenciaAtual
                signalGenerator.WaveType = tipoOnda
                signalGenerator.Volume = volumeAtual

                AddHandler signalGenerator.SampleGenerated, AddressOf CapturarSampleParaFFT

                waveOut = New WaveOutEvent()
                waveOut.Init(signalGenerator)
                waveOut.Play()

                timerAtualizacao.Start()

            Catch ex As Exception
                MessageBox.Show("Erro ao iniciar áudio: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                isPlaying = False
                btnPlayStop.Text = "▶ INICIAR"
                btnPlayStop.BackColor = Color.FromArgb(0, 120, 215)
            End Try
        Else
            isPlaying = False
            btnPlayStop.Text = "▶ INICIAR"
            btnPlayStop.BackColor = Color.FromArgb(0, 120, 215)

            timerAtualizacao.Stop()

            If waveOut IsNot Nothing Then
                waveOut.Stop()
                waveOut.Dispose()
                waveOut = Nothing
            End If

            If signalGenerator IsNot Nothing Then
                RemoveHandler signalGenerator.SampleGenerated, AddressOf CapturarSampleParaFFT
            End If

            signalGenerator = Nothing
            chartEspectro.Series("Espectro").Points.Clear()
        End If


    End Sub

    Private Sub timerAtualizacao_Tick(sender As Object, e As EventArgs) Handles timerAtualizacao.Tick
        If isPlaying Then
            AtualizarGraficoEspectro()
        End If
    End Sub

    Private Sub CapturarSampleParaFFT(sample As Single)
        If bufferPosition < bufferFFT.Length Then
            bufferFFT(bufferPosition) = New MathNet.Numerics.Complex32(sample, 0)
            bufferPosition += 1
        End If
    End Sub

    Private Sub btnAdicionarCanal_Click(sender As Object, e As EventArgs) Handles btnAdicionarCanal.Click
        If mixerChannels.Count >= 5 Then
            MessageBox.Show("Máximo de 5 canais adicionais atingido!", "Limite", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim canalId As Integer = mixerChannels.Count + 1
        Dim novoCanal As New MixerChannel() With {
            .Id = canalId,
            .Frequency = 440.0 + (canalId * 100),
            .Volume = 0.2,
            .WaveType = "Senoidal"
        }

        Dim panelCanal As New Panel()
        panelCanal.Size = New Size(150, 30)
        panelCanal.BackColor = Color.FromArgb(60, 60, 70)
        panelCanal.BorderStyle = BorderStyle.FixedSingle

        Dim lblCanal As New Label()
        lblCanal.Text = "Canal " & canalId & ": " & novoCanal.Frequency & " Hz"
        lblCanal.Location = New Point(5, 7)
        lblCanal.Size = New Size(140, 15)
        lblCanal.ForeColor = Color.White
        lblCanal.Font = New Font("Segoe UI", 8)
        panelCanal.Controls.Add(lblCanal)

        novoCanal.PanelControl = panelCanal
        mixerChannels.Add(novoCanal)
        flowLayoutMixer.Controls.Add(panelCanal)

        MessageBox.Show("Canal " & canalId & " adicionado!", "Mixer", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub FormGeradorSinais_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If waveOut IsNot Nothing Then
            waveOut.Stop()
            waveOut.Dispose()
        End If

        If signalGenerator IsNot Nothing Then
            RemoveHandler signalGenerator.SampleGenerated, AddressOf CapturarSampleParaFFT
        End If
    End Sub

End Class

' ========== CLASSE SIGNAL GENERATOR ==========
Public Class SignalGenerator
    Implements ISampleProvider

    Private _sampleRate As Integer = 44100
    Private _frequency As Double = 440.0
    Private _waveType As String = "Senoidal"
    Private _phase As Double = 0
    Private _volume As Double = 0.3
    Private _random As Random = New Random()

    Public Event SampleGenerated(sample As Single)

    Public Sub New(sampleRate As Integer)
        Me._sampleRate = sampleRate
    End Sub

    Public Property Frequency As Double
        Get
            Return _frequency
        End Get
        Set(value As Double)
            _frequency = value
        End Set
    End Property

    Public Property WaveType As String
        Get
            Return _waveType
        End Get
        Set(value As String)
            _waveType = value
        End Set
    End Property

    Public Property Volume As Double
        Get
            Return _volume
        End Get
        Set(value As Double)
            _volume = Math.Max(0, Math.Min(1, value))
        End Set
    End Property
    Public ReadOnly Property WaveFormat As WaveFormat Implements ISampleProvider.WaveFormat
        Get
            Return WaveFormat.CreateIeeeFloatWaveFormat(_sampleRate, 1)
        End Get
    End Property

    Public Function Read(buffer() As Single, offset As Integer, count As Integer) As Integer Implements ISampleProvider.Read
        For n As Integer = 0 To count - 1
            Dim sample As Single = CSng(GenerateSample())
            buffer(offset + n) = sample
            RaiseEvent SampleGenerated(sample)
        Next
        Return count
    End Function

    Private Function GenerateSample() As Double
        Dim sample As Double

        Select Case _waveType
            Case "Senoidal"
                sample = Math.Sin(2 * Math.PI * _phase)
            Case "Triangular"
                sample = 2.0 * Math.Abs(2.0 * (_phase - Math.Floor(_phase + 0.5))) - 1.0
            Case "Dente de Serra"
                sample = 2.0 * (_phase - Math.Floor(_phase)) - 1.0
            Case "Quadrada"
                sample = If(_phase < 0.5, 1.0, -1.0)
            Case "Ruído Branco"
                sample = (_random.NextDouble() * 2.0) - 1.0
                _phase = 0
            Case Else
                sample = Math.Sin(2 * Math.PI * _phase)
        End Select

        sample *= _volume

        If _waveType <> "Ruído Branco" Then
            _phase += _frequency / _sampleRate
            If _phase >= 1.0 Then
                _phase -= 1.0
            End If
        End If

        Return sample
    End Function
End Class
' ========== CLASSE MIXER CHANNEL ==========
Public Class MixerChannel
    Public Id As Integer
    Public Generator As SignalGenerator
    Public Frequency As Double
    Public Volume As Double
    Public WaveType As String
    Public PanelControl As Panel
End Class