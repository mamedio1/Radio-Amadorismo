<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSobre
    Inherits System.Windows.Forms.Form

    'Descartar substituições de formulário para limpar a lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Exigido pelo Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'OBSERVAÇÃO: o procedimento a seguir é exigido pelo Windows Form Designer
    'Pode ser modificado usando o Windows Form Designer.  
    'Não o modifique usando o editor de códigos.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        btnFechar = New Button()
        SuspendLayout()
        ' 
        ' btnFechar
        ' 
        btnFechar.Location = New Point(697, 416)
        btnFechar.Name = "btnFechar"
        btnFechar.Size = New Size(81, 22)
        btnFechar.TabIndex = 6
        btnFechar.Text = "Fechar"
        btnFechar.UseVisualStyleBackColor = True
        ' 
        ' FormSobre
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(btnFechar)
        Name = "FormSobre"
        Text = "Form1"
        ResumeLayout(False)
    End Sub
    Friend WithEvents btnFechar As Button
End Class
