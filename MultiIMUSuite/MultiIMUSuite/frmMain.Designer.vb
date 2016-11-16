<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
    Me.LabelDevices = New System.Windows.Forms.Label()
    Me.PictureBoxCanvas = New System.Windows.Forms.PictureBox()
    Me.ListViewDevices = New System.Windows.Forms.ListView()
    Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.ColumnHeader10 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.ColumnHeader11 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.ColumnHeader12 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.UcIMUdeviceSelected = New MultiIMU_components.ucIMUdevice()
    Me.ButtonSimulator = New System.Windows.Forms.Button()
    Me.TableLayoutPanel1.SuspendLayout()
    CType(Me.PictureBoxCanvas, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'ButtonConnect
    '
    Me.ButtonConnect.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ButtonConnect.Location = New System.Drawing.Point(3, 3)
    Me.ButtonConnect.Name = "ButtonConnect"
    Me.ButtonConnect.Size = New System.Drawing.Size(194, 24)
    Me.ButtonConnect.TabIndex = 0
    Me.ButtonConnect.Text = "Listen"
    Me.ButtonConnect.UseVisualStyleBackColor = True
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.ColumnCount = 3
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.ButtonConnect, 0, 0)
    Me.TableLayoutPanel1.Controls.Add(Me.LabelDevices, 0, 1)
    Me.TableLayoutPanel1.Controls.Add(Me.PictureBoxCanvas, 1, 4)
    Me.TableLayoutPanel1.Controls.Add(Me.ListViewDevices, 0, 2)
    Me.TableLayoutPanel1.Controls.Add(Me.UcIMUdeviceSelected, 0, 3)
    Me.TableLayoutPanel1.Controls.Add(Me.ButtonSimulator, 2, 0)
    Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 5
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(957, 600)
    Me.TableLayoutPanel1.TabIndex = 3
    '
    'LabelDevices
    '
    Me.LabelDevices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.TableLayoutPanel1.SetColumnSpan(Me.LabelDevices, 3)
    Me.LabelDevices.Dock = System.Windows.Forms.DockStyle.Fill
    Me.LabelDevices.Location = New System.Drawing.Point(3, 30)
    Me.LabelDevices.Name = "LabelDevices"
    Me.LabelDevices.Size = New System.Drawing.Size(951, 60)
    Me.LabelDevices.TabIndex = 2
    Me.LabelDevices.Text = "Devices"
    '
    'PictureBoxCanvas
    '
    Me.PictureBoxCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.TableLayoutPanel1.SetColumnSpan(Me.PictureBoxCanvas, 2)
    Me.PictureBoxCanvas.Dock = System.Windows.Forms.DockStyle.Fill
    Me.PictureBoxCanvas.Location = New System.Drawing.Point(203, 263)
    Me.PictureBoxCanvas.Name = "PictureBoxCanvas"
    Me.PictureBoxCanvas.Size = New System.Drawing.Size(751, 334)
    Me.PictureBoxCanvas.TabIndex = 3
    Me.PictureBoxCanvas.TabStop = False
    '
    'ListViewDevices
    '
    Me.ListViewDevices.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6, Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9, Me.ColumnHeader10, Me.ColumnHeader11, Me.ColumnHeader12})
    Me.TableLayoutPanel1.SetColumnSpan(Me.ListViewDevices, 3)
    Me.ListViewDevices.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ListViewDevices.Location = New System.Drawing.Point(3, 93)
    Me.ListViewDevices.Name = "ListViewDevices"
    Me.ListViewDevices.Size = New System.Drawing.Size(951, 94)
    Me.ListViewDevices.TabIndex = 4
    Me.ListViewDevices.UseCompatibleStateImageBehavior = False
    Me.ListViewDevices.View = System.Windows.Forms.View.Details
    '
    'UcIMUdeviceSelected
    '
    Me.UcIMUdeviceSelected.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.TableLayoutPanel1.SetColumnSpan(Me.UcIMUdeviceSelected, 3)
    Me.UcIMUdeviceSelected.Dock = System.Windows.Forms.DockStyle.Fill
    Me.UcIMUdeviceSelected.Location = New System.Drawing.Point(3, 193)
    Me.UcIMUdeviceSelected.Name = "UcIMUdeviceSelected"
    Me.UcIMUdeviceSelected.Size = New System.Drawing.Size(951, 64)
    Me.UcIMUdeviceSelected.TabIndex = 6
    '
    'ButtonSimulator
    '
    Me.ButtonSimulator.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ButtonSimulator.Location = New System.Drawing.Point(760, 3)
    Me.ButtonSimulator.Name = "ButtonSimulator"
    Me.ButtonSimulator.Size = New System.Drawing.Size(194, 24)
    Me.ButtonSimulator.TabIndex = 7
    Me.ButtonSimulator.Text = "Simulator"
    Me.ButtonSimulator.UseVisualStyleBackColor = True
    '
    'frmMain
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(957, 600)
    Me.Controls.Add(Me.TableLayoutPanel1)
    Me.Name = "frmMain"
    Me.Text = "Form1"
    Me.TableLayoutPanel1.ResumeLayout(False)
    CType(Me.PictureBoxCanvas, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Friend WithEvents ButtonConnect As Button
  Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
  Friend WithEvents LabelDevices As Label
  Friend WithEvents PictureBoxCanvas As PictureBox
  Friend WithEvents ListViewDevices As ListView
  Friend WithEvents ColumnHeader1 As ColumnHeader
  Friend WithEvents ColumnHeader2 As ColumnHeader
  Friend WithEvents ColumnHeader3 As ColumnHeader
  Friend WithEvents ColumnHeader4 As ColumnHeader
  Friend WithEvents ColumnHeader5 As ColumnHeader
  Friend WithEvents ColumnHeader6 As ColumnHeader
  Friend WithEvents ColumnHeader7 As ColumnHeader
  Friend WithEvents ColumnHeader8 As ColumnHeader
  Friend WithEvents ColumnHeader9 As ColumnHeader
  Friend WithEvents ColumnHeader10 As ColumnHeader
  Friend WithEvents ColumnHeader11 As ColumnHeader
  Friend WithEvents ColumnHeader12 As ColumnHeader
  Friend WithEvents UcIMUdeviceSelected As MultiIMU_components.ucIMUdevice
  Friend WithEvents ButtonSimulator As Button
End Class
