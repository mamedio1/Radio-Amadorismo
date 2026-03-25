Imports System.Drawing
Imports System.Math
Imports System.Windows.Forms

Public Class CalculadoraBalun
    Inherits Form

    Private btnCalc As Button
    Private btnClear As Button
    Private txtFMin As TextBox
    Private txtFMax As TextBox
    Private txtPow As TextBox
    Private txtResultado As TextBox  ' MUDOU DE lblRes PARA txtResultado
    Private picBox As PictureBox

    Public Sub New()
        MyBase.New()

        Me.Text = "Calculadora de Baluns"
        Me.ClientSize = New Size(900, 800)
        Me.BackColor = Color.FromArgb(30, 30, 30)
        Me.StartPosition = FormStartPosition.CenterScreen

        CriarControles()
    End Sub

    Private Sub CriarControles()
        Dim titulo As New Label With {
            .Text = "CALCULADORA DE BALUNS - RF",
            .Location = New Point(20, 20),
            .Size = New Size(860, 40),
            .Font = New Font("Segoe UI", 18, FontStyle.Bold),
            .ForeColor = Color.Cyan,
            .TextAlign = ContentAlignment.MiddleCenter
        }
        Me.Controls.Add(titulo)

        Dim y As Integer = 80

        Me.Controls.Add(New Label With {.Text = "Frequência Mínima (MHz):", .Location = New Point(20, y), .Size = New Size(200, 20), .ForeColor = Color.White})
        txtFMin = New TextBox With {.Location = New Point(230, y), .Size = New Size(100, 25), .Text = "3.5"}
        Me.Controls.Add(txtFMin)

        y += 35
        Me.Controls.Add(New Label With {.Text = "Frequência Máxima (MHz):", .Location = New Point(20, y), .Size = New Size(200, 20), .ForeColor = Color.White})
        txtFMax = New TextBox With {.Location = New Point(230, y), .Size = New Size(100, 25), .Text = "30"}
        Me.Controls.Add(txtFMax)

        y += 35
        Me.Controls.Add(New Label With {.Text = "Potência (Watts):", .Location = New Point(20, y), .Size = New Size(200, 20), .ForeColor = Color.White})
        txtPow = New TextBox With {.Location = New Point(230, y), .Size = New Size(100, 25), .Text = "100"}
        Me.Controls.Add(txtPow)

        y += 50
        btnCalc = New Button With {.Text = "🔧 CALCULAR", .Location = New Point(20, y), .Size = New Size(150, 40), .BackColor = Color.FromArgb(0, 120, 215), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat, .Font = New Font("Segoe UI", 11, FontStyle.Bold)}
        btnCalc.FlatAppearance.BorderSize = 0
        AddHandler btnCalc.Click, AddressOf BtnCalc_Click
        Me.Controls.Add(btnCalc)

        btnClear = New Button With {.Text = "🗑️ LIMPAR", .Location = New Point(180, y), .Size = New Size(150, 40), .BackColor = Color.FromArgb(180, 60, 60), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat, .Font = New Font("Segoe UI", 11, FontStyle.Bold)}
        btnClear.FlatAppearance.BorderSize = 0
        AddHandler btnClear.Click, AddressOf BtnClear_Click
        Me.Controls.Add(btnClear)

        y += 60

        ' CRIAR TABCONTROL PARA RESULTADOS, DIAGRAMA E GRÁFICO
        Dim tabControl As New TabControl With {
    .Location = New Point(20, y),
    .Size = New Size(860, 580),
    .Font = New Font("Segoe UI", 10, FontStyle.Bold)
}
        Me.Controls.Add(tabControl)

        ' Aba 1: Resultados
        Dim tabResultados As New TabPage("📋 Resultados")
        tabResultados.BackColor = Color.FromArgb(45, 45, 48)
        tabControl.TabPages.Add(tabResultados)

        ' Aba 2: Diagrama
        Dim tabDiagrama As New TabPage("🔧 Diagrama")
        tabDiagrama.BackColor = Color.FromArgb(45, 45, 48)
        tabControl.TabPages.Add(tabDiagrama)

        ' Aba 3: Gráfico
        Dim tabGrafico As New TabPage("📊 Gráfico Impedância")
        tabGrafico.BackColor = Color.FromArgb(45, 45, 48)
        tabControl.TabPages.Add(tabGrafico)

        ' TEXTBOX DE RESULTADOS DENTRO DA ABA 1
        txtResultado = New TextBox With {
    .Location = New Point(5, 5),
    .Size = New Size(845, 540),
    .BackColor = Color.FromArgb(45, 45, 48),
    .ForeColor = Color.White,
    .Font = New Font("Consolas", 9),
    .Multiline = True,
    .ScrollBars = ScrollBars.Vertical,
    .ReadOnly = True,
    .BorderStyle = BorderStyle.FixedSingle,
    .Text = "Clique em CALCULAR para ver os resultados..."
}
        tabResultados.Controls.Add(txtResultado)

        ' PICTUREBOX PARA DIAGRAMA DENTRO DA ABA 2
        picBox = New PictureBox With {
    .Location = New Point(5, 5),
    .Size = New Size(845, 540),
    .BackColor = Color.FromArgb(35, 35, 40),
    .BorderStyle = BorderStyle.FixedSingle,
    .SizeMode = PictureBoxSizeMode.CenterImage
}
        tabDiagrama.Controls.Add(picBox)

        ' Label inicial no diagrama
        Dim lblDiagramaInicial As New Label With {
    .Text = "Clique em CALCULAR para ver o diagrama",
    .Location = New Point(200, 250),
    .Size = New Size(450, 40),
    .ForeColor = Color.Gray,
    .Font = New Font("Segoe UI", 14, FontStyle.Italic),
    .TextAlign = ContentAlignment.MiddleCenter,
    .Name = "lblDiagramaInicial"
}
        picBox.Controls.Add(lblDiagramaInicial)

        ' CRIAR CHART PARA GRÁFICO DENTRO DA ABA 3
        Dim chartImpedancia As New System.Windows.Forms.DataVisualization.Charting.Chart With {
    .Location = New Point(5, 5),
    .Size = New Size(845, 540),
    .BackColor = Color.FromArgb(35, 35, 40)
}
        tabGrafico.Controls.Add(chartImpedancia)

        ' Configurar ChartArea
        Dim chartArea As New System.Windows.Forms.DataVisualization.Charting.ChartArea With {
    .BackColor = Color.FromArgb(35, 35, 40)
}
        chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(60, 60, 70)
        chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(60, 60, 70)
        chartArea.AxisX.LabelStyle.ForeColor = Color.White
        chartArea.AxisY.LabelStyle.ForeColor = Color.White
        chartArea.AxisX.LineColor = Color.White
        chartArea.AxisY.LineColor = Color.White
        chartArea.AxisX.Title = "Frequência (MHz)"
        chartArea.AxisY.Title = "Impedância (Ω) / SWR"
        chartArea.AxisX.TitleForeColor = Color.White
        chartArea.AxisY.TitleForeColor = Color.White
        chartArea.AxisX.TitleFont = New Font("Segoe UI", 10, FontStyle.Bold)
        chartArea.AxisY.TitleFont = New Font("Segoe UI", 10, FontStyle.Bold)
        chartImpedancia.ChartAreas.Add(chartArea)

        ' Série de Impedância
        Dim seriesZ As New System.Windows.Forms.DataVisualization.Charting.Series("Impedância (Z)") With {
    .ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline,
    .Color = Color.FromArgb(0, 200, 255),
    .BorderWidth = 3
}
        chartImpedancia.Series.Add(seriesZ)

        ' Série de SWR
        Dim seriesSWR As New System.Windows.Forms.DataVisualization.Charting.Series("SWR") With {
    .ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline,
    .Color = Color.FromArgb(255, 100, 100),
    .BorderWidth = 2,
    .BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash
}
        chartImpedancia.Series.Add(seriesSWR)

        ' Título do gráfico
        Dim tituloGrafico As New System.Windows.Forms.DataVisualization.Charting.Title("IMPEDÂNCIA E SWR vs FREQUÊNCIA") With {
    .Font = New Font("Segoe UI", 12, FontStyle.Bold),
    .ForeColor = Color.Cyan
}
        chartImpedancia.Titles.Add(tituloGrafico)

        ' Legenda
        Dim legenda As New System.Windows.Forms.DataVisualization.Charting.Legend With {
    .BackColor = Color.FromArgb(45, 45, 48),
    .ForeColor = Color.White,
    .Font = New Font("Segoe UI", 9)
}
        chartImpedancia.Legends.Add(legenda)

        ' Mensagem inicial
        Dim anotacaoInicial As New System.Windows.Forms.DataVisualization.Charting.TextAnnotation With {
    .Text = "Clique em CALCULAR para gerar o gráfico",
    .ForeColor = Color.Gray,
    .Font = New Font("Segoe UI", 14, FontStyle.Italic),
    .Alignment = ContentAlignment.MiddleCenter,
    .X = 50,
    .Y = 50
}
        chartImpedancia.Annotations.Add(anotacaoInicial)
    End Sub


    ' NÃO ADICIONAR MAIS O picBox AQUI FORA - JÁ ESTÁ DENTRO DA ABA

    Private Sub BtnCalc_Click(sender As Object, e As EventArgs)
        Try
            Dim fMin As Double = Convert.ToDouble(txtFMin.Text)
            Dim fMax As Double = Convert.ToDouble(txtFMax.Text)
            Dim pot As Double = Convert.ToDouble(txtPow.Text)

            If fMin <= 0 Or fMax <= 0 Or pot <= 0 Then
                MessageBox.Show("Todos os valores devem ser maiores que zero!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            If fMin >= fMax Then
                MessageBox.Show("A frequência mínima deve ser menor que a máxima!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim nucleo As String
            Dim dExt As Double
            Dim dInt As Double
            Dim altura As Double
            Dim alValue As Double
            Dim permeabilidade As Integer

            If pot <= 100 Then
                nucleo = "FT50-43"
                dExt = 12.7
                dInt = 7.5
                altura = 6.35
                alValue = 523
                permeabilidade = 850
            ElseIf pot <= 200 Then
                nucleo = "FT82-43"
                dExt = 20.8
                dInt = 13.2
                altura = 6.35
                alValue = 523
                permeabilidade = 850
            ElseIf pot <= 500 Then
                nucleo = "FT140-43"
                dExt = 35.6
                dInt = 22.9
                altura = 12.7
                alValue = 885
                permeabilidade = 850
            Else
                nucleo = "FT240-43"
                dExt = 61.0
                dInt = 35.8
                altura = 12.7
                alValue = 885
                permeabilidade = 850
            End If

            If fMax > 50 Then
                If pot <= 50 Then
                    nucleo = "FT50-61"
                    dExt = 12.7
                    dInt = 7.5
                    altura = 6.35
                    alValue = 178
                    permeabilidade = 125
                Else
                    nucleo = "FT240-61"
                    dExt = 61.0
                    dInt = 35.8
                    altura = 12.7
                    alValue = 178
                    permeabilidade = 125
                End If
            End If

            Dim impedanciaMinima As Double = 300
            Dim indutanciaMinima As Double = impedanciaMinima / (2 * PI * fMin * 1000000)
            indutanciaMinima *= 1000000
            Dim esp As Integer = Math.Max(6, CInt(Sqrt((indutanciaMinima * 1000) / alValue)))

            If fMin < 5 Then
                esp = CInt(esp * 1.3)
            End If

            Dim corrente As Double = Sqrt(pot / 50)
            Dim areaMinimaFio As Double = corrente / 3
            Dim diametroMinimoFio As Double = 2 * Sqrt(areaMinimaFio / PI)

            Dim profundidadePele As Double = 503 / Sqrt(fMax * 1000)
            If fMax > 10 Then
                diametroMinimoFio = Math.Max(diametroMinimoFio, profundidadePele * 4)
            End If

            Dim awgNum As Integer
            Dim dFio As Double

            If diametroMinimoFio >= 2.0 Then
                awgNum = 12
                dFio = 2.053
            ElseIf diametroMinimoFio >= 1.6 Then
                awgNum = 14
                dFio = 1.628
            ElseIf diametroMinimoFio >= 1.3 Then
                awgNum = 16
                dFio = 1.291
            ElseIf diametroMinimoFio >= 1.0 Then
                awgNum = 18
                dFio = 1.024
            ElseIf diametroMinimoFio >= 0.8 Then
                awgNum = 20
                dFio = 0.812
            Else
                awgNum = 22
                dFio = 0.644
            End If

            Dim comp As Double = PI * (dExt + dInt) / 2 * esp / 10
            Dim compTotal As Double = comp * 1.2

            Dim res As New System.Text.StringBuilder()
            res.AppendLine("══════════════════════════════════════════════════════════════")
            res.AppendLine("            RESULTADOS DO CÁLCULO DE BALUN 1:1")
            res.AppendLine("══════════════════════════════════════════════════════════════")
            res.AppendLine("")
            res.AppendLine("  NÚCLEO RECOMENDADO:")
            res.AppendLine("    • Modelo: " & nucleo)
            res.AppendLine("    • Dimensões: Ø" & dExt & "mm (ext) x Ø" & dInt & "mm (int) x " & altura & "mm (alt)")
            res.AppendLine("    • Material: Ferrite (μi = " & permeabilidade & ")")
            res.AppendLine("    • Valor AL: " & alValue & " nH/N²")
            res.AppendLine("")
            res.AppendLine("  ENROLAMENTO:")
            res.AppendLine("    • Número de Espiras: " & esp & " voltas")
            res.AppendLine("    • Fio Recomendado: AWG #" & awgNum & " (" & dFio.ToString("F2") & "mm)")
            res.AppendLine("    • Comprimento Necessário: " & comp.ToString("F1") & " cm")
            res.AppendLine("    • Comprimento Total (c/ margem): " & compTotal.ToString("F1") & " cm")
            res.AppendLine("")
            res.AppendLine("  ESPECIFICAÇÕES ELÉTRICAS:")
            res.AppendLine("    • Faixa de Frequência: " & fMin & " - " & fMax & " MHz")
            res.AppendLine("    • Potência Máxima: " & pot & " W")
            res.AppendLine("    • Impedância: 50 Ω : 50 Ω")
            res.AppendLine("    • SWR Esperado: < 1.5:1")
            res.AppendLine("    • Perda de Inserção: < 0.3 dB")
            res.AppendLine("")
            res.AppendLine("══════════════════════════════════════════════════════════════")

            txtResultado.Text = res.ToString()
            DesenharDiag(dExt, dInt, esp, nucleo)
            GerarGraficoImpedancia(fMin, fMax, nucleo, dExt, dInt, alValue, esp)

        Catch ex As Exception
            MessageBox.Show("Erro: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DesenharDiag(dE As Double, dI As Double, esp As Integer, nom As String)
        Dim lblInicial = picBox.Controls("lblDiagramaInicial")
        If lblInicial IsNot Nothing Then
            picBox.Controls.Remove(lblInicial)
        End If

        Dim bmp As New Bitmap(845, 540)  ' AJUSTADO PARA O NOVO TAMANHO
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        g.Clear(Color.FromArgb(35, 35, 40))

        g.DrawString("DIAGRAMA DE ENROLAMENTO", New Font("Segoe UI", 14, FontStyle.Bold), Brushes.Cyan, 300, 10)

        Dim cx As Integer = 430, cy As Integer = 140, esc As Single = 2.8F
        Dim dExtPx As Integer = CInt(dE * esc)
        Dim dIntPx As Integer = CInt(dI * esc)

        g.FillEllipse(New SolidBrush(Color.FromArgb(80, 80, 90)), cx - dExtPx \ 2, cy - dExtPx \ 2, dExtPx, dExtPx)
        g.DrawEllipse(New Pen(Color.Gray, 3), cx - dExtPx \ 2, cy - dExtPx \ 2, dExtPx, dExtPx)
        g.FillEllipse(New SolidBrush(Color.FromArgb(35, 35, 40)), cx - dIntPx \ 2, cy - dIntPx \ 2, dIntPx, dIntPx)
        g.DrawEllipse(New Pen(Color.Gray, 3), cx - dIntPx \ 2, cy - dIntPx \ 2, dIntPx, dIntPx)

        Dim rMed As Double = (dExtPx + dIntPx) / 4.0
        For i As Integer = 0 To esp - 1
            Dim ang As Double = (i / esp) * 2 * PI
            Dim x1 As Integer = cx + CInt(rMed * Cos(ang))
            Dim y1 As Integer = cy + CInt(rMed * Sin(ang))
            Dim x2 As Integer = cx + CInt((rMed + 12) * Cos(ang))
            Dim y2 As Integer = cy + CInt((rMed + 12) * Sin(ang))
            g.DrawLine(New Pen(Color.Orange, 2), x1, y1, x2, y2)
            g.FillEllipse(Brushes.Orange, x2 - 2, y2 - 2, 4, 4)
        Next

        g.DrawString("Núcleo: " & nom, New Font("Segoe UI", 10, FontStyle.Bold), Brushes.White, 30, 250)
        g.DrawString("Espiras: " & esp, New Font("Segoe UI", 10, FontStyle.Bold), Brushes.Orange, 750, 250)

        g.Dispose()
        picBox.Image = bmp
    End Sub
    Private Sub GerarGraficoImpedancia(fMin As Double, fMax As Double, nucleo As String, dExt As Double, dInt As Double, alValue As Double, esp As Integer)
        Try
            ' Buscar o TabControl e o Chart
            Dim chart As System.Windows.Forms.DataVisualization.Charting.Chart = Nothing

            For Each ctrl As Control In Me.Controls
                If TypeOf ctrl Is TabControl Then
                    Dim tabCtrl As TabControl = CType(ctrl, TabControl)

                    ' Procurar na TERCEIRA aba (índice 2)
                    If tabCtrl.TabPages.Count > 2 Then
                        For Each c As Control In tabCtrl.TabPages(2).Controls
                            If TypeOf c Is System.Windows.Forms.DataVisualization.Charting.Chart Then
                                chart = CType(c, System.Windows.Forms.DataVisualization.Charting.Chart)
                                Exit For
                            End If
                        Next
                    End If
                End If
            Next

            If chart Is Nothing Then
                MessageBox.Show("Chart não encontrado!", "Debug")
                Return
            End If

            ' Remover mensagem inicial
            chart.Annotations.Clear()

            ' Limpar séries
            chart.Series(0).Points.Clear()
            chart.Series(1).Points.Clear()

            Dim numPontos As Integer = 100
            Dim freqStep As Double = (fMax - fMin) / numPontos

            For i As Integer = 0 To numPontos
                Dim freq As Double = fMin + (i * freqStep)

                Dim indutancia As Double = (alValue * esp * esp) / 1000000000.0
                Dim reatanciaIndutiva As Double = 2 * PI * freq * 1000000 * indutancia
                Dim comprimentoFio As Double = PI * (dExt + dInt) / 2 * esp / 1000
                Dim resistenciaFio As Double = 0.017 * comprimentoFio / 0.0005
                Dim impedancia As Double = Sqrt(resistenciaFio * resistenciaFio + reatanciaIndutiva * reatanciaIndutiva)

                Dim Z0 As Double = 50.0
                Dim gamma As Double = Abs((impedancia - Z0) / (impedancia + Z0))
                Dim swr As Double = If(gamma < 1, (1 + gamma) / (1 - gamma), 10)
                swr = Math.Min(swr, 10)

                chart.Series(0).Points.AddXY(freq, impedancia)
                chart.Series(1).Points.AddXY(freq, swr)
            Next

            chart.ChartAreas(0).AxisX.Minimum = fMin
            chart.ChartAreas(0).AxisX.Maximum = fMax
            chart.ChartAreas(0).AxisY.Minimum = 0
            chart.ChartAreas(0).RecalculateAxesScale()

            Dim annotation As New System.Windows.Forms.DataVisualization.Charting.TextAnnotation With {
            .Text = "Núcleo: " & nucleo & " | Espiras: " & esp,
            .ForeColor = Color.Yellow,
            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
            .X = 5,
            .Y = 5
        }
            chart.Annotations.Add(annotation)

        Catch ex As Exception
            MessageBox.Show("Erro ao gerar gráfico: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub BtnClear_Click(sender As Object, e As EventArgs)
        txtFMin.Text = "3.5"
        txtFMax.Text = "30"
        txtPow.Text = "100"
        txtResultado.Text = "Clique em CALCULAR para ver os resultados..."
        picBox.Image = Nothing

        ' Limpar gráfico
        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is TabControl Then
                Dim tab As TabControl = CType(ctrl, TabControl)
                For Each tabPage As TabPage In tab.TabPages
                    For Each c As Control In tabPage.Controls
                        If TypeOf c Is System.Windows.Forms.DataVisualization.Charting.Chart Then
                            Dim chart = CType(c, System.Windows.Forms.DataVisualization.Charting.Chart)
                            chart.Series("Impedância (Z)").Points.Clear()
                            chart.Series("SWR").Points.Clear()
                        End If
                    Next
                Next
            End If
        Next
    End Sub

End Class