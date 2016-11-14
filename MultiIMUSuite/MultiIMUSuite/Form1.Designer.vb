<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
  Inherits System.Windows.Forms.Form

  'Form overrides dispose to clean up the component list.
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

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Me.ButtonConnect = New System.Windows.Forms.Button()
    Me.TextBoxComm = New System.Windows.Forms.TextBox()
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    Me.LabelDevices = New System.Windows.Forms.Label()
    Me.TableLayoutPanel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'ButtonConnect
    '
    Me.ButtonConnect.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ButtonConnect.Location = New System.Drawing.Point(3, 3)
    Me.ButtonConnect.Name = "ButtonConnect"
    Me.ButtonConnect.Size = New System.Drawing.Size(114, 24)
    Me.ButtonConnect.TabIndex = 0
    Me.ButtonConnect.Text = "Listen"
    Me.ButtonConnect.UseVisualStyleBackColor = True
    '
    'TextBoxComm
    '
    Me.TableLayoutPanel1.SetColumnSpan(Me.TextBoxComm, 2)
    Me.TextBoxComm.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TextBoxComm.Location = New System.Drawing.Point(3, 93)
    Me.TextBoxComm.Multiline = True
    Me.TextBoxComm.Name = "TextBoxComm"
    Me.TextBoxComm.Size = New System.Drawing.Size(951, 504)
    Me.TextBoxComm.TabIndex = 1
    Me.TextBoxComm.Text = "fd"
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.ColumnCount = 2
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.ButtonConnect, 0, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.TextBoxComm, 0, 2)
    Me.TableLayoutPanel1.Controls.Add(Me.LabelDevices, 0, 1)
    Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 3
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(957, 600)
    Me.TableLayoutPanel1.TabIndex = 3
    '
    'LabelDevices
    '
    Me.LabelDevices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.TableLayoutPanel1.SetColumnSpan(Me.LabelDevices, 2)
    Me.LabelDevices.Dock = System.Windows.Forms.DockStyle.Fill
    Me.LabelDevices.Location = New System.Drawing.Point(3, 30)
    Me.LabelDevices.Name = "LabelDevices"
    Me.LabelDevices.Size = New System.Drawing.Size(951, 60)
    Me.LabelDevices.TabIndex = 2
    Me.LabelDevices.Text = "Devices"
    '
    'Form1
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(957, 600)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.Name = "Form1"
    Me.Text = "Form1"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.TableLayoutPanel1.PerformLayout()
    Me.ResumeLayout(False)

  End Sub

  Friend WithEvents ButtonConnect As Button
  Friend WithEvents TextBoxComm As TextBox
  Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
  Friend WithEvents LabelDevices As Label
End Class
