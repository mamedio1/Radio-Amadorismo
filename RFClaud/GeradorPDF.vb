Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.draw
Imports System.Diagnostics


Public Class GeradorPDF

    Public Shared Sub ExportarParaPDF(titulo As String, conteudo As String, nomeArquivo As String)
        Try
            ' Criar documento PDF
            Dim documento As New Document(PageSize.A4, 50, 50, 50, 50)

            ' Criar o arquivo
            Dim writer As PdfWriter = PdfWriter.GetInstance(documento, New FileStream(nomeArquivo, FileMode.Create))

            ' Abrir documento
            documento.Open()

            ' Definir fontes
            Dim fonteTitulo As New Font(Font.FontFamily.HELVETICA, 18, Font.BOLD, BaseColor.DARK_GRAY)
            Dim fonteSubtitulo As New Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK)
            Dim fonteTexto As New Font(Font.FontFamily.COURIER, 10, Font.NORMAL, BaseColor.BLACK)
            Dim fonteRodape As New Font(Font.FontFamily.HELVETICA, 8, Font.ITALIC, BaseColor.GRAY)

            ' Adicionar título principal
            Dim paragrafoTitulo As New Paragraph("RF CALCULATOR", fonteTitulo)
            paragrafoTitulo.Alignment = Element.ALIGN_CENTER
            paragrafoTitulo.SpacingAfter = 10
            documento.Add(paragrafoTitulo)

            ' Adicionar subtítulo (nome do cálculo)
            Dim paragrafoSubtitulo As New Paragraph(titulo, fonteSubtitulo)
            paragrafoSubtitulo.Alignment = Element.ALIGN_CENTER
            paragrafoSubtitulo.SpacingAfter = 20
            documento.Add(paragrafoSubtitulo)

            ' Adicionar linha separadora
            Dim linhaSeparadora As New LineSeparator(1.0F, 100.0F, BaseColor.LIGHT_GRAY, Element.ALIGN_CENTER, -1)
            documento.Add(New Chunk(linhaSeparadora))
            documento.Add(New Paragraph(" "))

            ' Adicionar data e hora
            Dim dataHora As New Paragraph("Data: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), fonteRodape)
            dataHora.Alignment = Element.ALIGN_RIGHT
            dataHora.SpacingAfter = 15
            documento.Add(dataHora)

            ' Adicionar conteúdo (resultados)
            Dim linhas() As String = conteudo.Split(New String() {vbCrLf, vbLf}, StringSplitOptions.None)

            For Each linha As String In linhas
                Dim paragrafo As New Paragraph(linha, fonteTexto)
                paragrafo.SpacingAfter = 3
                documento.Add(paragrafo)
            Next

            ' Adicionar rodapé
            documento.Add(New Paragraph(" "))
            documento.Add(New Chunk(New LineSeparator(1.0F, 100.0F, BaseColor.LIGHT_GRAY, Element.ALIGN_CENTER, -1)))

            Dim rodape As New Paragraph("Gerado por RF Calculator - Sistema de Cálculos Avançados", fonteRodape)
            rodape.Alignment = Element.ALIGN_CENTER
            rodape.SpacingBefore = 10
            documento.Add(rodape)

            ' Fechar documento
            documento.Close()

        Catch ex As Exception
            Throw New Exception("Erro ao gerar PDF: " & ex.Message)
        End Try
    End Sub

End Class