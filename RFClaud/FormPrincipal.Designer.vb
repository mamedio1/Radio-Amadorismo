<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormPrincipal
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        picLogoRF = New PictureBox()
        CType(picLogoRF, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' picLogoRF
        ' 
        picLogoRF.Image = My.Resources.Resources.RFCalculator
        picLogoRF.Location = New Point(256, 56)
        picLogoRF.Name = "picLogoRF"
        picLogoRF.Size = New Size(361, 438)
        picLogoRF.SizeMode = PictureBoxSizeMode.Zoom
        picLogoRF.TabIndex = 0
        picLogoRF.TabStop = False
        ' 
        ' FormPrincipal
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 539)
        Controls.Add(picLogoRF)
        Name = "FormPrincipal"
        Text = "Form1"
        CType(picLogoRF, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents picLogoRF As PictureBox

End Class
