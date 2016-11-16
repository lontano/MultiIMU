<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSimulator
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
    Me.components = New System.ComponentModel.Container()
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.TextBoxHost = New System.Windows.Forms.TextBox()
    Me.NumericUpDownPort = New System.Windows.Forms.NumericUpDown()
    Me.ButtonConnect = New System.Windows.Forms.Button()
    Me.TextBoxName = New System.Windows.Forms.TextBox()
    Me.TimerData = New System.Windows.Forms.Timer(Me.components)
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.NumericUpDownPort, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.ColumnCount = 3
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 1)
    Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 3)
    Me.TableLayoutPanel1.Controls.Add(Me.TextBoxHost, 1, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.NumericUpDownPort, 1, 1)
    Me.TableLayoutPanel1.Controls.Add(Me.ButtonConnect, 2, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.TextBoxName, 1, 3)
    Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 6
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(482, 139)
    Me.TableLayoutPanel1.TabIndex = 0
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label1.Location = New System.Drawing.Point(3, 0)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(114, 25)
    Me.Label1.TabIndex = 0
    Me.Label1.Text = "Host"
    Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label2.Location = New System.Drawing.Point(3, 25)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(114, 25)
    Me.Label2.TabIndex = 1
    Me.Label2.Text = "Port"
    Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label3.Location = New System.Drawing.Point(3, 60)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(114, 25)
    Me.Label3.TabIndex = 2
    Me.Label3.Text = "Name"
    Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'TextBoxHost
    '
    Me.TextBoxHost.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TextBoxHost.Location = New System.Drawing.Point(123, 3)
    Me.TextBoxHost.Name = "TextBoxHost"
    Me.TextBoxHost.Size = New System.Drawing.Size(236, 20)
    Me.TextBoxHost.TabIndex = 3
    Me.TextBoxHost.Text = "127.0.0.1"
    '
    'NumericUpDownPort
    '
    Me.NumericUpDownPort.Location = New System.Drawing.Point(123, 28)
    Me.NumericUpDownPort.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
    Me.NumericUpDownPort.Name = "NumericUpDownPort"
    Me.NumericUpDownPort.Size = New System.Drawing.Size(65, 20)
    Me.NumericUpDownPort.TabIndex = 4
    '
    'ButtonConnect
    '
    Me.ButtonConnect.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ButtonConnect.Location = New System.Drawing.Point(365, 3)
    Me.ButtonConnect.Name = "ButtonConnect"
    Me.TableLayoutPanel1.SetRowSpan(Me.ButtonConnect, 2)
    Me.ButtonConnect.Size = New System.Drawing.Size(114, 44)
    Me.ButtonConnect.TabIndex = 5
    Me.ButtonConnect.Text = "Connect"
    Me.ButtonConnect.UseVisualStyleBackColor = True
    '
    'TextBoxName
    '
    Me.TextBoxName.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TextBoxName.Location = New System.Drawing.Point(123, 63)
    Me.TextBoxName.Name = "TextBoxName"
    Me.TextBoxName.Size = New System.Drawing.Size(236, 20)
    Me.TextBoxName.TabIndex = 6
    '
    'TimerData
    '
    '
    'frmSimulator
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(482, 139)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.Name = "frmSimulator"
    Me.Text = "Simulator"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.TableLayoutPanel1.PerformLayout()
    CType(Me.NumericUpDownPort, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
  Friend WithEvents Label1 As Label
  Friend WithEvents Label2 As Label
  Friend WithEvents Label3 As Label
  Friend WithEvents TextBoxHost As TextBox
  Friend WithEvents NumericUpDownPort As NumericUpDown
  Friend WithEvents ButtonConnect As Button
  Friend WithEvents TextBoxName As TextBox
  Friend WithEvents TimerData As Timer
End Class
