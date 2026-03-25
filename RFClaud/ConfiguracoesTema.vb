Imports System.IO

Public Class ConfiguracoesTema
    ' Singleton
    Private Shared _instancia As ConfiguracoesTema

    Public Shared ReadOnly Property Instancia() As ConfiguracoesTema
        Get
            If _instancia Is Nothing Then
                _instancia = New ConfiguracoesTema()
                _instancia.CarregarTema()  ' Carregar tema salvo ao iniciar
            End If
            Return _instancia
        End Get
    End Property

    ' Cores do tema
    Public Property CorFundo As Color = Color.FromArgb(20, 20, 40)
    Public Property CorTexto As Color = Color.FromArgb(100, 200, 255)
    Public Property CorTextoVerde As Color = Color.FromArgb(150, 255, 150)
    Public Property CorCaixaTexto As Color = Color.FromArgb(30, 30, 60)
    Public Property CorCaixaTextoTexto As Color = Color.FromArgb(150, 255, 150)
    Public Property CorBotao As Color = Color.FromArgb(200, 100, 255)
    Public Property CorBotaoBorda As Color = Color.FromArgb(150, 150, 255)
    Public Property CorPanel As Color = Color.FromArgb(15, 15, 30)
    Public Property NomeTema As String = "Cyberpunk"

    ' Caminho do arquivo de configuração
    Private ReadOnly Property ArquivoConfig As String
        Get
            Dim pastaConfig As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RFCalculator")
            If Not Directory.Exists(pastaConfig) Then
                Directory.CreateDirectory(pastaConfig)
            End If
            Return Path.Combine(pastaConfig, "tema.config")
        End Get
    End Property

    ' Aplicar tema
    Public Sub AplicarTema(nomeTema As String)
        Select Case nomeTema
            Case "Cyberpunk"
                CorFundo = Color.FromArgb(20, 20, 40)
                CorTexto = Color.FromArgb(100, 200, 255)
                CorTextoVerde = Color.FromArgb(150, 255, 150)
                CorCaixaTexto = Color.FromArgb(30, 30, 60)
                CorCaixaTextoTexto = Color.FromArgb(150, 255, 150)
                CorBotao = Color.FromArgb(200, 100, 255)
                CorBotaoBorda = Color.FromArgb(150, 150, 255)
                CorPanel = Color.FromArgb(15, 15, 30)

            Case "Azul Escuro"
                CorFundo = Color.FromArgb(10, 20, 40)
                CorTexto = Color.FromArgb(100, 150, 255)
                CorTextoVerde = Color.FromArgb(100, 200, 255)
                CorCaixaTexto = Color.FromArgb(20, 30, 60)
                CorCaixaTextoTexto = Color.FromArgb(200, 220, 255)
                CorBotao = Color.FromArgb(50, 100, 200)
                CorBotaoBorda = Color.FromArgb(100, 150, 255)
                CorPanel = Color.FromArgb(15, 25, 50)

            Case "Verde Matrix"
                CorFundo = Color.FromArgb(0, 20, 0)
                CorTexto = Color.FromArgb(0, 255, 0)
                CorTextoVerde = Color.FromArgb(100, 255, 100)
                CorCaixaTexto = Color.FromArgb(0, 40, 0)
                CorCaixaTextoTexto = Color.FromArgb(150, 255, 150)
                CorBotao = Color.FromArgb(0, 150, 0)
                CorBotaoBorda = Color.FromArgb(0, 255, 0)
                CorPanel = Color.FromArgb(0, 30, 0)

            Case "Laranja Quente"
                CorFundo = Color.FromArgb(40, 20, 10)
                CorTexto = Color.FromArgb(255, 180, 100)
                CorTextoVerde = Color.FromArgb(255, 200, 150)
                CorCaixaTexto = Color.FromArgb(60, 30, 20)
                CorCaixaTextoTexto = Color.FromArgb(255, 220, 180)
                CorBotao = Color.FromArgb(200, 100, 50)
                CorBotaoBorda = Color.FromArgb(255, 150, 100)
                CorPanel = Color.FromArgb(50, 25, 15)

            Case "Rosa Neon"
                CorFundo = Color.FromArgb(30, 10, 30)
                CorTexto = Color.FromArgb(255, 100, 255)
                CorTextoVerde = Color.FromArgb(255, 150, 255)
                CorCaixaTexto = Color.FromArgb(50, 20, 50)
                CorCaixaTextoTexto = Color.FromArgb(255, 200, 255)
                CorBotao = Color.FromArgb(200, 50, 200)
                CorBotaoBorda = Color.FromArgb(255, 100, 255)
                CorPanel = Color.FromArgb(40, 15, 40)

            Case "Cinza Profissional"
                CorFundo = Color.FromArgb(30, 30, 30)
                CorTexto = Color.FromArgb(200, 200, 200)
                CorTextoVerde = Color.FromArgb(150, 220, 150)
                CorCaixaTexto = Color.FromArgb(50, 50, 50)
                CorCaixaTextoTexto = Color.FromArgb(220, 220, 220)
                CorBotao = Color.FromArgb(80, 80, 80)
                CorBotaoBorda = Color.FromArgb(150, 150, 150)
                CorPanel = Color.FromArgb(40, 40, 40)
        End Select

        Me.NomeTema = nomeTema
        SalvarTema() ' Salvar automaticamente ao aplicar
    End Sub

    ' Salvar tema no arquivo
    Public Sub SalvarTema()
        Try
            File.WriteAllText(ArquivoConfig, NomeTema)
        Catch ex As Exception
            ' Silenciosamente falhar se não conseguir salvar
            Debug.WriteLine("Erro ao salvar tema: " & ex.Message)
        End Try
    End Sub

    ' Carregar tema do arquivo
    Public Sub CarregarTema()
        Try
            If File.Exists(ArquivoConfig) Then
                Dim temaSalvo As String = File.ReadAllText(ArquivoConfig).Trim()
                If Not String.IsNullOrEmpty(temaSalvo) Then
                    AplicarTema(temaSalvo)
                End If
            Else
                ' Se não existe arquivo, usar tema padrão (Cyberpunk)
                AplicarTema("Cyberpunk")
            End If
        Catch ex As Exception
            ' Se der erro ao carregar, usar tema padrão
            AplicarTema("Cyberpunk")
            Debug.WriteLine("Erro ao carregar tema: " & ex.Message)
        End Try
    End Sub
End Class