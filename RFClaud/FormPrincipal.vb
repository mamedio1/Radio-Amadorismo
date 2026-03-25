Public Class FormPrincipal
    Private Sub FormPrincipal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Cálculos RF - Sistema Avançado"
        Me.Size = New Size(800, 600)
        Me.StartPosition = FormStartPosition.CenterScreen

        ' Estilo Cyberpunk - Fundo escuro
        Me.BackColor = Color.FromArgb(15, 15, 35)

        ' Criar MenuStrip
        Dim menuStrip As New MenuStrip()
        menuStrip.BackColor = Color.FromArgb(25, 25, 50)
        menuStrip.ForeColor = Color.FromArgb(100, 200, 255)
        menuStrip.Font = New Font("Segoe UI", 10, FontStyle.Bold)

        ' ========== Menu Arquivo ==========
        Dim menuArquivo As New ToolStripMenuItem("Arquivo")
        menuArquivo.ForeColor = Color.FromArgb(100, 200, 255)
        Dim menuSair As New ToolStripMenuItem("Sair")
        menuSair.ForeColor = Color.FromArgb(255, 150, 200)
        AddHandler menuSair.Click, AddressOf MenuSair_Click
        menuArquivo.DropDownItems.Add(menuSair)

        ' ========== Menu Ferramentas ==========
        Dim menuFerramentas As New ToolStripMenuItem("Ferramentas")
        menuFerramentas.ForeColor = Color.FromArgb(100, 200, 255)

        Dim menuGeradorSinais As New ToolStripMenuItem("Gerador de Sinais")
        menuGeradorSinais.ForeColor = Color.FromArgb(100, 200, 255)
        AddHandler menuGeradorSinais.Click, AddressOf AbrirGeradorSinais
        menuFerramentas.DropDownItems.Add(menuGeradorSinais)

        Dim menuComprimentoOnda As New ToolStripMenuItem("Comprimento de Onda")
        menuComprimentoOnda.ForeColor = Color.FromArgb(150, 255, 150)
        AddHandler menuComprimentoOnda.Click, AddressOf MenuComprimentoOnda_Click
        menuFerramentas.DropDownItems.Add(menuComprimentoOnda)

        Dim menuCalcularCapacitor As New ToolStripMenuItem("Calcular Capacitor")
        menuCalcularCapacitor.ForeColor = Color.FromArgb(150, 255, 150)
        AddHandler menuCalcularCapacitor.Click, AddressOf MenuCalcularCapacitor_Click
        menuFerramentas.DropDownItems.Add(menuCalcularCapacitor)

        Dim menuCalcularIndutancia As New ToolStripMenuItem("Calcular Indutância")
        menuCalcularIndutancia.ForeColor = Color.FromArgb(150, 255, 150)
        AddHandler menuCalcularIndutancia.Click, AddressOf MenuCalcularIndutancia_Click
        menuFerramentas.DropDownItems.Add(menuCalcularIndutancia)

        Dim menuConversaoPotencia As New ToolStripMenuItem("Conversão dBm ↔ Watts")
        menuConversaoPotencia.ForeColor = Color.FromArgb(150, 255, 150)
        AddHandler menuConversaoPotencia.Click, AddressOf MenuConversaoPotencia_Click
        menuFerramentas.DropDownItems.Add(menuConversaoPotencia)

        Dim menuCalcularEspiras As New ToolStripMenuItem("Calcular Espiras de Bobina")
        menuCalcularEspiras.ForeColor = Color.FromArgb(150, 255, 150)
        AddHandler menuCalcularEspiras.Click, AddressOf MenuCalcularEspiras_Click
        menuFerramentas.DropDownItems.Add(menuCalcularEspiras)

        Dim menuCalcularLPorDimensoes As New ToolStripMenuItem("Calcular L por Dimensões")
        menuCalcularLPorDimensoes.ForeColor = Color.FromArgb(150, 255, 150)
        AddHandler menuCalcularLPorDimensoes.Click, AddressOf MenuCalcularLPorDimensoes_Click
        menuFerramentas.DropDownItems.Add(menuCalcularLPorDimensoes)

        ' No método onde você configura o MenuStrip
        Dim menuCalculadoraBalun As New ToolStripMenuItem("Calculadora de Baluns")
        menuCalculadoraBalun.ForeColor = Color.FromArgb(100, 200, 255)
        AddHandler menuCalculadoraBalun.Click, AddressOf AbrirCalculadoraBalun
        menuFerramentas.DropDownItems.Add(menuCalculadoraBalun)

        ' Adicionar ANTES de fechar o menu Ferramentas
        Dim menuCircuitoTanque As New ToolStripMenuItem("Circuito Tanque LC")
        menuCircuitoTanque.ForeColor = Color.FromArgb(150, 255, 150)
        AddHandler menuCircuitoTanque.Click, AddressOf MenuCircuitoTanque_Click
        menuFerramentas.DropDownItems.Add(menuCircuitoTanque)

        ' ========== Menu Antenas ==========
        Dim menuAntenas As New ToolStripMenuItem("Antenas")
        menuAntenas.ForeColor = Color.FromArgb(255, 200, 100)

        Dim menuDipolo As New ToolStripMenuItem("Dipolo - Comprimento Ideal")
        menuDipolo.ForeColor = Color.FromArgb(255, 220, 150)
        AddHandler menuDipolo.Click, AddressOf MenuDipolo_Click
        menuAntenas.DropDownItems.Add(menuDipolo)

        Dim menuGroundPlane As New ToolStripMenuItem("Ground Plane - Vertical")
        menuGroundPlane.ForeColor = Color.FromArgb(255, 220, 150)
        AddHandler menuGroundPlane.Click, AddressOf MenuGroundPlane_Click
        menuAntenas.DropDownItems.Add(menuGroundPlane)

        Dim menuLoopMagnetica As New ToolStripMenuItem("Loop Magnética")
        menuLoopMagnetica.ForeColor = Color.FromArgb(255, 220, 150)
        AddHandler menuLoopMagnetica.Click, AddressOf MenuLoopMagnetica_Click
        menuAntenas.DropDownItems.Add(menuLoopMagnetica)

        Dim menuBobinaToroidal As New ToolStripMenuItem("Bobina Toroidal")
        menuBobinaToroidal.ForeColor = Color.FromArgb(150, 255, 150)
        AddHandler menuBobinaToroidal.Click, AddressOf MenuBobinaToroidal_Click
        menuFerramentas.DropDownItems.Add(menuBobinaToroidal)


        ' ========== Adicionar TODOS os menus ao MenuStrip ==========
        menuStrip.Items.Add(menuArquivo)
        menuStrip.Items.Add(menuFerramentas)
        menuStrip.Items.Add(menuAntenas)  ' ← MUITO IMPORTANTE!
       

        Dim menuSobre As New ToolStripMenuItem("Sobre")
        menuSobre.ForeColor = Color.FromArgb(255, 200, 100)

        Dim menuSobreAutor As New ToolStripMenuItem("Sobre o Autor")
        menuSobreAutor.ForeColor = Color.FromArgb(255, 220, 150)
        AddHandler menuSobreAutor.Click, AddressOf MenuSobreAutor_Click
        menuSobre.DropDownItems.Add(menuSobreAutor)

        ' ========== Menu Configurações (ADICIONAR ISTO) ==========
        Dim menuConfig As New ToolStripMenuItem("Configurações")
        menuConfig.ForeColor = Color.FromArgb(255, 200, 100)

        Dim menuTemas As New ToolStripMenuItem("Temas de Cores")
        menuTemas.ForeColor = Color.FromArgb(255, 220, 150)
        AddHandler menuTemas.Click, AddressOf MenuTemas_Click

        menuConfig.DropDownItems.Add(menuTemas)
        menuStrip.Items.Add(menuConfig)
        menuStrip.Items.Add(menuSobre)

        Me.Controls.Add(menuStrip)
        Me.MainMenuStrip = menuStrip

        ' Adicionar label de título no centro
        'Dim lblTitulo As New Label()
        'lblTitulo.Text = "RF CALCULATOR" & vbCrLf & "DE CÁLCULOS AVANÇADOS"
        'lblTitulo.Font = New Font("Courier New", 24, FontStyle.Bold)
        'lblTitulo.ForeColor = Color.FromArgb(0, 217, 255)  ' Cyan brilhante
        'lblTitulo.BackColor = Color.Transparent
        'lblTitulo.TextAlign = ContentAlignment.MiddleCenter
        'lblTitulo.Location = New Point(250, 100)  ' Ajustado para baixo da imagem
        'lblTitulo.Size = New Size(500, 80)
        'Me.Controls.Add(lblTitulo)

        ' Adicionar subtítulo
        Dim lblSubtitulo As New Label()
        lblSubtitulo.Text = "Selecione uma ferramenta no menu acima"
        lblSubtitulo.Font = New Font("Segoe UI", 14, FontStyle.Italic)
        lblSubtitulo.ForeColor = Color.FromArgb(125, 211, 252)  ' Cyan claro
        lblSubtitulo.BackColor = Color.Transparent
        lblSubtitulo.TextAlign = ContentAlignment.MiddleCenter
        lblSubtitulo.Location = New Point(140, 500)  ' Mais abaixo
        lblSubtitulo.Size = New Size(600, 40)
        Me.Controls.Add(lblSubtitulo)
    End Sub

    ' ========== MÉTODOS DOS MENUS ==========

    Private Sub MenuSair_Click(sender As Object, e As EventArgs)
        Application.Exit()
    End Sub

    Private Sub MenuComprimentoOnda_Click(sender As Object, e As EventArgs)
        Dim formComprimento As New FormComprimentoOnda()
        formComprimento.Owner = Me
        formComprimento.Show()
        formComprimento.BringToFront()
    End Sub

    Private Sub MenuCalcularCapacitor_Click(sender As Object, e As EventArgs)
        Dim formCapacitor As New FormCalcularCapacitor()
        formCapacitor.Owner = Me
        formCapacitor.Show()
        formCapacitor.BringToFront()
    End Sub

    Private Sub MenuCalcularIndutancia_Click(sender As Object, e As EventArgs)
        Dim formIndutancia As New FormCalcularIndutancia()
        formIndutancia.Owner = Me
        formIndutancia.Show()
        formIndutancia.BringToFront()
    End Sub

    Private Sub MenuConversaoPotencia_Click(sender As Object, e As EventArgs)
        Dim formPotencia As New FormConversaoPotencia()
        formPotencia.Owner = Me
        formPotencia.Show()
        formPotencia.BringToFront()
    End Sub

    Private Sub MenuCalcularEspiras_Click(sender As Object, e As EventArgs)
        Dim formEspiras As New FormCalcularEspiras()
        formEspiras.Owner = Me
        formEspiras.Show()
        formEspiras.BringToFront()
    End Sub

    Private Sub MenuCalcularLPorDimensoes_Click(sender As Object, e As EventArgs)
        Dim formLDimensoes As New FormCalcularLPorDimensoes()
        formLDimensoes.Owner = Me
        formLDimensoes.Show()
        formLDimensoes.BringToFront()
    End Sub

    ' ← ESTE MÉTODO NOVO PARA O DIPOLO
    Private Sub MenuDipolo_Click(sender As Object, e As EventArgs)
        Dim formDipolo As New FormAntenasDipolo()
        formDipolo.Owner = Me
        formDipolo.Show()
        formDipolo.BringToFront()
    End Sub
    Private Sub MenuGroundPlane_Click(sender As Object, e As EventArgs)
        Dim formGroundPlane As New FormAntenasGroundPlane()
        formGroundPlane.Owner = Me
        formGroundPlane.Show()
        formGroundPlane.BringToFront()
    End Sub

    Private Sub MenuSobreAutor_Click(sender As Object, e As EventArgs)
        Dim formSobre As New FormSobre()
        formSobre.Owner = Me
        formSobre.ShowDialog()  ' ShowDialog para ser modal (bloqueia até fechar)
    End Sub

    Private Sub MenuLoopMagnetica_Click(sender As Object, e As EventArgs)
        Dim formLoop As New FormAntenasLoopMagnetica()
        formLoop.Owner = Me
        formLoop.Show()
        formLoop.BringToFront()
    End Sub

    Private Sub MenuBobinaToroidal_Click(sender As Object, e As EventArgs)
        Dim formToroidal As New FormBobinaToroidal()
        formToroidal.Owner = Me
        formToroidal.Show()
        formToroidal.BringToFront()
    End Sub
    Private Sub AbrirGeradorSinais(sender As Object, e As EventArgs)
        Dim formGerador As New FormGeradorSinais()
        formGerador.Show()
    End Sub
    ' Método para abrir
    Private Sub AbrirCalculadoraBalun(sender As Object, e As EventArgs)
        Dim formBalun As New CalculadoraBalun()  ' MUDOU O NOME!
        formBalun.Show()
    End Sub

    Private Sub MenuCircuitoTanque_Click(sender As Object, e As EventArgs)
        Dim formTanque As New FormCircuitoTanque()
        formTanque.ShowDialog()
    End Sub


    Private Sub MenuTemas_Click(sender As Object, e As EventArgs)
        Dim formTemas As New FormConfiguracoesTemas()
        formTemas.Owner = Me
        If formTemas.ShowDialog() = DialogResult.OK Then
            AplicarEstiloPrincipal()

            MessageBox.Show("Tema aplicado! As janelas já abertas manterão o tema antigo." & vbCrLf &
                           "Feche e reabra-as para ver o novo tema.",
                           "Tema Alterado", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    Private Sub AplicarEstiloPrincipal()
        Dim tema As ConfiguracoesTema = ConfiguracoesTema.Instancia

        Me.BackColor = tema.CorFundo

        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is Label Then
                Dim lbl As Label = DirectCast(ctrl, Label)
                If lbl.Font.Size >= 12 Then
                    lbl.ForeColor = tema.CorTextoVerde
                Else
                    lbl.ForeColor = tema.CorTexto
                End If
            ElseIf TypeOf ctrl Is MenuStrip Then
                Dim menu As MenuStrip = DirectCast(ctrl, MenuStrip)
                menu.BackColor = Color.FromArgb(25, 25, 50)
                menu.ForeColor = tema.CorTexto

                For Each item As ToolStripMenuItem In menu.Items
                    item.ForeColor = tema.CorTexto
                    For Each subItem As ToolStripMenuItem In item.DropDownItems
                        subItem.ForeColor = tema.CorTextoVerde
                    Next
                Next
            End If
        Next
    End Sub


End Class