Public Class FormConfiguracoesTemas
    Private lstTemas As ListBox
    Private panelPreview As Panel
    Private btnAplicar As Button
    Private btnCancelar As Button
    Private lblPreview As Label

    Private Sub FormConfiguracoesTemas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigurarFormulario()
        AplicarEstiloAtual()
    End Sub

    Private Sub ConfigurarFormulario()
        Me.Text = "Configurações de Temas"
        Me.Size = New Size(600, 500)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False

        ' Título
        Dim lblTitulo As New Label()
        lblTitulo.Text = "ESCOLHA SEU TEMA DE CORES"
        lblTitulo.Location = New Point(30, 20)
        lblTitulo.Size = New Size(540, 30)
        lblTitulo.Font = New Font("Segoe UI", 14, FontStyle.Bold)
        lblTitulo.TextAlign = ContentAlignment.MiddleCenter
        Me.Controls.Add(lblTitulo)

        ' Label Lista
        Dim lblLista As New Label()
        lblLista.Text = "Temas Disponíveis:"
        lblLista.Location = New Point(30, 70)
        lblLista.Size = New Size(150, 20)
        lblLista.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        Me.Controls.Add(lblLista)

        ' ListBox com temas
        lstTemas = New ListBox()
        lstTemas.Location = New Point(30, 95)
        lstTemas.Size = New Size(250, 250)
        lstTemas.Font = New Font("Consolas", 10)
        lstTemas.Items.Add("Cyberpunk")
        lstTemas.Items.Add("Azul Escuro")
        lstTemas.Items.Add("Verde Matrix")
        lstTemas.Items.Add("Laranja Quente")
        lstTemas.Items.Add("Rosa Neon")
        lstTemas.Items.Add("Cinza Profissional")
        ' Selecionar o tema atual
        Dim temaAtual As String = ConfiguracoesTema.Instancia.NomeTema
        Dim index As Integer = lstTemas.Items.IndexOf(temaAtual)
        If index >= 0 Then
            lstTemas.SelectedIndex = index
        Else
            lstTemas.SelectedIndex = 0
        End If


        AddHandler lstTemas.SelectedIndexChanged, AddressOf LstTemas_SelectedIndexChanged
        Me.Controls.Add(lstTemas)

        ' Label Preview
        lblPreview = New Label()
        lblPreview.Text = "Preview:"
        lblPreview.Location = New Point(310, 70)
        lblPreview.Size = New Size(100, 20)
        lblPreview.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        Me.Controls.Add(lblPreview)

        ' Panel de Preview
        panelPreview = New Panel()
        panelPreview.Location = New Point(310, 95)
        panelPreview.Size = New Size(250, 250)
        panelPreview.BorderStyle = BorderStyle.FixedSingle
        Me.Controls.Add(panelPreview)

        ' Criar preview inicial
        CriarPreview()

        ' Descrição
        Dim lblDescricao As New Label()
        lblDescricao.Text = "As cores serão aplicadas em todas as janelas do software." & vbCrLf &
                           "Selecione um tema e clique em 'Aplicar'."
        lblDescricao.Location = New Point(30, 360)
        lblDescricao.Size = New Size(540, 40)
        lblDescricao.Font = New Font("Segoe UI", 9)
        lblDescricao.TextAlign = ContentAlignment.MiddleCenter
        Me.Controls.Add(lblDescricao)

        ' Botão Aplicar
        btnAplicar = New Button()
        btnAplicar.Text = "Aplicar Tema"
        btnAplicar.Location = New Point(200, 420)
        btnAplicar.Size = New Size(120, 35)
        AddHandler btnAplicar.Click, AddressOf BtnAplicar_Click
        Me.Controls.Add(btnAplicar)

        ' Botão Cancelar
        btnCancelar = New Button()
        btnCancelar.Text = "Cancelar"
        btnCancelar.Location = New Point(340, 420)
        btnCancelar.Size = New Size(120, 35)
        AddHandler btnCancelar.Click, AddressOf BtnCancelar_Click
        Me.Controls.Add(btnCancelar)
    End Sub

    Private Sub AplicarEstiloAtual()
        Dim tema As ConfiguracoesTema = ConfiguracoesTema.Instancia

        Me.BackColor = tema.CorFundo

        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is Label Then
                Dim lbl As Label = DirectCast(ctrl, Label)
                If lbl.Font.Size >= 10 Then
                    lbl.ForeColor = tema.CorTextoVerde
                Else
                    lbl.ForeColor = tema.CorTexto
                End If
            ElseIf TypeOf ctrl Is Button Then
                Dim btn As Button = DirectCast(ctrl, Button)
                btn.BackColor = tema.CorBotao
                btn.ForeColor = Color.White
                btn.FlatStyle = FlatStyle.Flat
                btn.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                btn.FlatAppearance.BorderColor = tema.CorBotaoBorda
                btn.FlatAppearance.BorderSize = 2
            ElseIf TypeOf ctrl Is ListBox Then
                Dim lst As ListBox = DirectCast(ctrl, ListBox)
                lst.BackColor = tema.CorCaixaTexto
                lst.ForeColor = tema.CorCaixaTextoTexto
            ElseIf TypeOf ctrl Is Panel Then
                ctrl.BackColor = tema.CorPanel
            End If
        Next
    End Sub

    Private Sub LstTemas_SelectedIndexChanged(sender As Object, e As EventArgs)
        CriarPreview()
    End Sub

    Private Sub CriarPreview()
        panelPreview.Controls.Clear()

        Dim temaTeste As New ConfiguracoesTema()
        temaTeste.AplicarTema(lstTemas.SelectedItem.ToString())

        panelPreview.BackColor = temaTeste.CorFundo

        Dim lblTituloPrev As New Label()
        lblTituloPrev.Text = "Exemplo de Título"
        lblTituloPrev.Location = New Point(10, 10)
        lblTituloPrev.Size = New Size(230, 25)
        lblTituloPrev.Font = New Font("Segoe UI", 11, FontStyle.Bold)
        lblTituloPrev.ForeColor = temaTeste.CorTextoVerde
        panelPreview.Controls.Add(lblTituloPrev)

        Dim lblTextoPrev As New Label()
        lblTextoPrev.Text = "Texto normal:"
        lblTextoPrev.Location = New Point(10, 45)
        lblTextoPrev.Size = New Size(100, 20)
        lblTextoPrev.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblTextoPrev.ForeColor = temaTeste.CorTexto
        panelPreview.Controls.Add(lblTextoPrev)

        Dim txtPrev As New TextBox()
        txtPrev.Location = New Point(10, 70)
        txtPrev.Size = New Size(230, 20)
        txtPrev.Text = "Caixa de texto"
        txtPrev.BackColor = temaTeste.CorCaixaTexto
        txtPrev.ForeColor = temaTeste.CorCaixaTextoTexto
        txtPrev.Font = New Font("Consolas", 9)
        txtPrev.BorderStyle = BorderStyle.FixedSingle
        panelPreview.Controls.Add(txtPrev)

        Dim btnPrev As New Button()
        btnPrev.Text = "Botão de Exemplo"
        btnPrev.Location = New Point(50, 110)
        btnPrev.Size = New Size(150, 30)
        btnPrev.BackColor = temaTeste.CorBotao
        btnPrev.ForeColor = Color.White
        btnPrev.FlatStyle = FlatStyle.Flat
        btnPrev.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        btnPrev.FlatAppearance.BorderColor = temaTeste.CorBotaoBorda
        btnPrev.FlatAppearance.BorderSize = 2
        panelPreview.Controls.Add(btnPrev)

        Dim panelPrev As New Panel()
        panelPrev.Location = New Point(10, 160)
        panelPrev.Size = New Size(230, 80)
        panelPrev.BackColor = temaTeste.CorPanel
        panelPrev.BorderStyle = BorderStyle.FixedSingle

        Dim lblPanelPrev As New Label()
        lblPanelPrev.Text = "Área de Desenho" & vbCrLf & "ou Painel"
        lblPanelPrev.Location = New Point(50, 25)
        lblPanelPrev.Size = New Size(130, 35)
        lblPanelPrev.Font = New Font("Consolas", 9)
        lblPanelPrev.ForeColor = Color.FromArgb(150, 150, 200)
        lblPanelPrev.TextAlign = ContentAlignment.MiddleCenter
        panelPrev.Controls.Add(lblPanelPrev)

        panelPreview.Controls.Add(panelPrev)
    End Sub

    Private Sub BtnAplicar_Click(sender As Object, e As EventArgs)
        Dim temaEscolhido As String = lstTemas.SelectedItem.ToString()
        ConfiguracoesTema.Instancia.AplicarTema(temaEscolhido)

        MessageBox.Show("Tema '" & temaEscolhido & "' aplicado e salvo com sucesso!" & vbCrLf & vbCrLf &
                   "O tema será carregado automaticamente na próxima vez que abrir o programa." & vbCrLf & vbCrLf &
                   "Feche e reabra as janelas para ver as mudanças.",
                   "Tema Aplicado e Salvo", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs)
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class