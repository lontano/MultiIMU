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
    Me.LabelOrientation = New System.Windows.Forms.Label()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.NumericUpDownOrientatin_X = New System.Windows.Forms.NumericUpDown()
    Me.NumericUpDownOrientatin_Y = New System.Windows.Forms.NumericUpDown()
    Me.NumericUpDownOrientatin_Z = New System.Windows.Forms.NumericUpDown()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
    Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown()
    Me.NumericUpDown3 = New System.Windows.Forms.NumericUpDown()
    Me.ButtonYocto = New System.Windows.Forms.Button()
    Me.TimerData = New System.Windows.Forms.Timer(Me.components)
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.NumericUpDownPort, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NumericUpDownOrientatin_X, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NumericUpDownOrientatin_Y, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NumericUpDownOrientatin_Z, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).BeginInit()
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
    Me.TableLayoutPanel1.Controls.Add(Me.LabelOrientation, 1, 5)
    Me.TableLayoutPanel1.Controls.Add(Me.Label4, 1, 10)
    Me.TableLayoutPanel1.Controls.Add(Me.Label5, 0, 6)
    Me.TableLayoutPanel1.Controls.Add(Me.Label6, 0, 7)
    Me.TableLayoutPanel1.Controls.Add(Me.Label7, 0, 8)
    Me.TableLayoutPanel1.Controls.Add(Me.NumericUpDownOrientatin_X, 1, 6)
    Me.TableLayoutPanel1.Controls.Add(Me.NumericUpDownOrientatin_Y, 1, 7)
    Me.TableLayoutPanel1.Controls.Add(Me.NumericUpDownOrientatin_Z, 1, 8)
    Me.TableLayoutPanel1.Controls.Add(Me.Label8, 0, 11)
    Me.TableLayoutPanel1.Controls.Add(Me.Label9, 0, 12)
    Me.TableLayoutPanel1.Controls.Add(Me.Label10, 0, 13)
    Me.TableLayoutPanel1.Controls.Add(Me.NumericUpDown1, 1, 11)
    Me.TableLayoutPanel1.Controls.Add(Me.NumericUpDown2, 1, 12)
    Me.TableLayoutPanel1.Controls.Add(Me.NumericUpDown3, 1, 13)
    Me.TableLayoutPanel1.Controls.Add(Me.ButtonYocto, 2, 3)
    Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 15
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(371, 308)
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
    Me.TextBoxHost.Size = New System.Drawing.Size(125, 20)
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
    Me.NumericUpDownPort.Value = New Decimal(New Integer() {800, 0, 0, 0})
    '
    'ButtonConnect
    '
    Me.ButtonConnect.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ButtonConnect.Location = New System.Drawing.Point(254, 3)
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
    Me.TextBoxName.Size = New System.Drawing.Size(125, 20)
    Me.TextBoxName.TabIndex = 6
    Me.TextBoxName.Text = "800"
    '
    'LabelOrientation
    '
    Me.LabelOrientation.AutoSize = True
    Me.LabelOrientation.Dock = System.Windows.Forms.DockStyle.Fill
    Me.LabelOrientation.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.LabelOrientation.Location = New System.Drawing.Point(123, 95)
    Me.LabelOrientation.Name = "LabelOrientation"
    Me.LabelOrientation.Size = New System.Drawing.Size(125, 25)
    Me.LabelOrientation.TabIndex = 7
    Me.LabelOrientation.Text = "Orientation"
    Me.LabelOrientation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Label4.Location = New System.Drawing.Point(123, 205)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(125, 25)
    Me.Label4.TabIndex = 7
    Me.Label4.Text = "GPS"
    Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label5.Location = New System.Drawing.Point(3, 120)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(114, 25)
    Me.Label5.TabIndex = 8
    Me.Label5.Text = "X"
    Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label6.Location = New System.Drawing.Point(3, 145)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(114, 25)
    Me.Label6.TabIndex = 8
    Me.Label6.Text = "Y"
    Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label7.Location = New System.Drawing.Point(3, 170)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(114, 25)
    Me.Label7.TabIndex = 8
    Me.Label7.Text = "Z"
    Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'NumericUpDownOrientatin_X
    '
    Me.NumericUpDownOrientatin_X.Dock = System.Windows.Forms.DockStyle.Fill
    Me.NumericUpDownOrientatin_X.Location = New System.Drawing.Point(123, 123)
    Me.NumericUpDownOrientatin_X.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
    Me.NumericUpDownOrientatin_X.Minimum = New Decimal(New Integer() {3600, 0, 0, -2147483648})
    Me.NumericUpDownOrientatin_X.Name = "NumericUpDownOrientatin_X"
    Me.NumericUpDownOrientatin_X.Size = New System.Drawing.Size(125, 20)
    Me.NumericUpDownOrientatin_X.TabIndex = 9
    '
    'NumericUpDownOrientatin_Y
    '
    Me.NumericUpDownOrientatin_Y.Dock = System.Windows.Forms.DockStyle.Fill
    Me.NumericUpDownOrientatin_Y.Location = New System.Drawing.Point(123, 148)
    Me.NumericUpDownOrientatin_Y.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
    Me.NumericUpDownOrientatin_Y.Minimum = New Decimal(New Integer() {3600, 0, 0, -2147483648})
    Me.NumericUpDownOrientatin_Y.Name = "NumericUpDownOrientatin_Y"
    Me.NumericUpDownOrientatin_Y.Size = New System.Drawing.Size(125, 20)
    Me.NumericUpDownOrientatin_Y.TabIndex = 9
    '
    'NumericUpDownOrientatin_Z
    '
    Me.NumericUpDownOrientatin_Z.Dock = System.Windows.Forms.DockStyle.Fill
    Me.NumericUpDownOrientatin_Z.Location = New System.Drawing.Point(123, 173)
    Me.NumericUpDownOrientatin_Z.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
    Me.NumericUpDownOrientatin_Z.Minimum = New Decimal(New Integer() {3600, 0, 0, -2147483648})
    Me.NumericUpDownOrientatin_Z.Name = "NumericUpDownOrientatin_Z"
    Me.NumericUpDownOrientatin_Z.Size = New System.Drawing.Size(125, 20)
    Me.NumericUpDownOrientatin_Z.TabIndex = 9
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label8.Location = New System.Drawing.Point(3, 230)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(114, 25)
    Me.Label8.TabIndex = 10
    Me.Label8.Text = "Longitude"
    Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label9
    '
    Me.Label9.AutoSize = True
    Me.Label9.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label9.Location = New System.Drawing.Point(3, 255)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(114, 25)
    Me.Label9.TabIndex = 10
    Me.Label9.Text = "Lattitude"
    Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Label10
    '
    Me.Label10.AutoSize = True
    Me.Label10.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label10.Location = New System.Drawing.Point(3, 280)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(114, 25)
    Me.Label10.TabIndex = 10
    Me.Label10.Text = "Altitude"
    Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'NumericUpDown1
    '
    Me.NumericUpDown1.DecimalPlaces = 8
    Me.NumericUpDown1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.NumericUpDown1.Location = New System.Drawing.Point(123, 233)
    Me.NumericUpDown1.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
    Me.NumericUpDown1.Minimum = New Decimal(New Integer() {3600, 0, 0, -2147483648})
    Me.NumericUpDown1.Name = "NumericUpDown1"
    Me.NumericUpDown1.Size = New System.Drawing.Size(125, 20)
    Me.NumericUpDown1.TabIndex = 11
    Me.NumericUpDown1.Value = New Decimal(New Integer() {41373993, 0, 0, 393216})
    '
    'NumericUpDown2
    '
    Me.NumericUpDown2.DecimalPlaces = 8
    Me.NumericUpDown2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.NumericUpDown2.Location = New System.Drawing.Point(123, 258)
    Me.NumericUpDown2.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
    Me.NumericUpDown2.Minimum = New Decimal(New Integer() {3600, 0, 0, -2147483648})
    Me.NumericUpDown2.Name = "NumericUpDown2"
    Me.NumericUpDown2.Size = New System.Drawing.Size(125, 20)
    Me.NumericUpDown2.TabIndex = 11
    Me.NumericUpDown2.Value = New Decimal(New Integer() {2069474, 0, 0, 393216})
    '
    'NumericUpDown3
    '
    Me.NumericUpDown3.DecimalPlaces = 2
    Me.NumericUpDown3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.NumericUpDown3.Location = New System.Drawing.Point(123, 283)
    Me.NumericUpDown3.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
    Me.NumericUpDown3.Minimum = New Decimal(New Integer() {3600, 0, 0, -2147483648})
    Me.NumericUpDown3.Name = "NumericUpDown3"
    Me.NumericUpDown3.Size = New System.Drawing.Size(125, 20)
    Me.NumericUpDown3.TabIndex = 11
    Me.NumericUpDown3.Value = New Decimal(New Integer() {150, 0, 0, 0})
    '
    'ButtonYocto
    '
    Me.ButtonYocto.Location = New System.Drawing.Point(254, 63)
    Me.ButtonYocto.Name = "ButtonYocto"
    Me.ButtonYocto.Size = New System.Drawing.Size(105, 19)
    Me.ButtonYocto.TabIndex = 12
    Me.ButtonYocto.Text = "Yocto"
    Me.ButtonYocto.UseVisualStyleBackColor = True
    '
    'TimerData
    '
    '
    'frmSimulator
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(371, 308)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.Name = "frmSimulator"
    Me.Text = "Simulator"
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.TableLayoutPanel1.PerformLayout()
    CType(Me.NumericUpDownPort, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NumericUpDownOrientatin_X, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NumericUpDownOrientatin_Y, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NumericUpDownOrientatin_Z, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).EndInit()
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
  Friend WithEvents LabelOrientation As Label
  Friend WithEvents Label4 As Label
  Friend WithEvents Label5 As Label
  Friend WithEvents Label6 As Label
  Friend WithEvents Label7 As Label
  Friend WithEvents NumericUpDownOrientatin_X As NumericUpDown
  Friend WithEvents NumericUpDownOrientatin_Y As NumericUpDown
  Friend WithEvents NumericUpDownOrientatin_Z As NumericUpDown
  Friend WithEvents Label8 As Label
  Friend WithEvents Label9 As Label
  Friend WithEvents Label10 As Label
  Friend WithEvents NumericUpDown1 As NumericUpDown
  Friend WithEvents NumericUpDown2 As NumericUpDown
  Friend WithEvents NumericUpDown3 As NumericUpDown
  Friend WithEvents ButtonYocto As Button
End Class
