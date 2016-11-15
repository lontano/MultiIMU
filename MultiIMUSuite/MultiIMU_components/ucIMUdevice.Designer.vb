<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucIMUdevice
  Inherits System.Windows.Forms.UserControl

  'UserControl overrides dispose to clean up the component list.
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
    Me.TableLayoutPanelAll = New System.Windows.Forms.TableLayoutPanel()
    Me.LabelType = New System.Windows.Forms.Label()
    Me.ComboBoxType = New System.Windows.Forms.ComboBox()
    Me.LabelName = New System.Windows.Forms.Label()
    Me.LabelVisibleDevices = New System.Windows.Forms.Label()
    Me.TableLayoutPanelAll.SuspendLayout()
    Me.SuspendLayout()
    '
    'TableLayoutPanelAll
    '
    Me.TableLayoutPanelAll.ColumnCount = 5
    Me.TableLayoutPanelAll.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
    Me.TableLayoutPanelAll.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
    Me.TableLayoutPanelAll.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
    Me.TableLayoutPanelAll.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
    Me.TableLayoutPanelAll.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    Me.TableLayoutPanelAll.Controls.Add(Me.ComboBoxType, 1, 1)
    Me.TableLayoutPanelAll.Controls.Add(Me.LabelType, 0, 1)
    Me.TableLayoutPanelAll.Controls.Add(Me.LabelName, 0, 0)
    Me.TableLayoutPanelAll.Controls.Add(Me.LabelVisibleDevices, 2, 0)
    Me.TableLayoutPanelAll.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TableLayoutPanelAll.Location = New System.Drawing.Point(0, 0)
    Me.TableLayoutPanelAll.Name = "TableLayoutPanelAll"
    Me.TableLayoutPanelAll.RowCount = 3
    Me.TableLayoutPanelAll.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
    Me.TableLayoutPanelAll.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
    Me.TableLayoutPanelAll.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    Me.TableLayoutPanelAll.Size = New System.Drawing.Size(769, 76)
    Me.TableLayoutPanelAll.TabIndex = 0
    '
    'LabelType
    '
    Me.LabelType.AutoSize = True
    Me.LabelType.Dock = System.Windows.Forms.DockStyle.Fill
    Me.LabelType.Location = New System.Drawing.Point(3, 28)
    Me.LabelType.Name = "LabelType"
    Me.LabelType.Size = New System.Drawing.Size(94, 28)
    Me.LabelType.TabIndex = 0
    Me.LabelType.Text = "Type"
    Me.LabelType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'ComboBoxType
    '
    Me.ComboBoxType.Dock = System.Windows.Forms.DockStyle.Fill
    Me.ComboBoxType.FormattingEnabled = True
    Me.ComboBoxType.Location = New System.Drawing.Point(103, 31)
    Me.ComboBoxType.Name = "ComboBoxType"
    Me.ComboBoxType.Size = New System.Drawing.Size(94, 21)
    Me.ComboBoxType.TabIndex = 1
    '
    'LabelName
    '
    Me.LabelName.AutoSize = True
    Me.LabelName.Dock = System.Windows.Forms.DockStyle.Fill
    Me.LabelName.Location = New System.Drawing.Point(3, 0)
    Me.LabelName.Name = "LabelName"
    Me.LabelName.Size = New System.Drawing.Size(94, 28)
    Me.LabelName.TabIndex = 2
    Me.LabelName.Text = "Name"
    Me.LabelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'LabelVisibleDevices
    '
    Me.LabelVisibleDevices.AutoSize = True
    Me.TableLayoutPanelAll.SetColumnSpan(Me.LabelVisibleDevices, 3)
    Me.LabelVisibleDevices.Dock = System.Windows.Forms.DockStyle.Fill
    Me.LabelVisibleDevices.Location = New System.Drawing.Point(203, 0)
    Me.LabelVisibleDevices.Name = "LabelVisibleDevices"
    Me.LabelVisibleDevices.Size = New System.Drawing.Size(563, 28)
    Me.LabelVisibleDevices.TabIndex = 3
    Me.LabelVisibleDevices.Text = "Visible devices"
    '
    'ucIMUdevice
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.TableLayoutPanelAll)
    Me.Name = "ucIMUdevice"
    Me.Size = New System.Drawing.Size(769, 76)
    Me.TableLayoutPanelAll.ResumeLayout(False)
    Me.TableLayoutPanelAll.PerformLayout()
    Me.ResumeLayout(False)

  End Sub

  Friend WithEvents TableLayoutPanelAll As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents ComboBoxType As System.Windows.Forms.ComboBox
  Friend WithEvents LabelType As System.Windows.Forms.Label
  Friend WithEvents LabelName As System.Windows.Forms.Label
  Friend WithEvents LabelVisibleDevices As System.Windows.Forms.Label
End Class
