﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
    Me.components = New System.ComponentModel.Container()
    Me.ComboBox1 = New System.Windows.Forms.ComboBox()
    Me.InventoryTimer = New System.Windows.Forms.Timer(Me.components)
    Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
    Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
    Me.PictureBox1 = New System.Windows.Forms.PictureBox()
    Me.RefreshTimer = New System.Windows.Forms.Timer(Me.components)
    Me.bt_200mA = New System.Windows.Forms.Button()
    Me.bt_2A = New System.Windows.Forms.Button()
    Me.bt_20A = New System.Windows.Forms.Button()
    Me.ACDCcheckBox = New System.Windows.Forms.CheckBox()
    Me.StatusStrip1.SuspendLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'ComboBox1
    '
    Me.ComboBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.ComboBox1.Enabled = False
    Me.ComboBox1.FormattingEnabled = True
    Me.ComboBox1.Location = New System.Drawing.Point(2, 12)
    Me.ComboBox1.Name = "ComboBox1"
    Me.ComboBox1.Size = New System.Drawing.Size(298, 21)
    Me.ComboBox1.TabIndex = 0
    '
    'InventoryTimer
    '
    '
    'StatusStrip1
    '
    Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
    Me.StatusStrip1.Location = New System.Drawing.Point(0, 231)
    Me.StatusStrip1.Name = "StatusStrip1"
    Me.StatusStrip1.Size = New System.Drawing.Size(305, 22)
    Me.StatusStrip1.SizingGrip = False
    Me.StatusStrip1.TabIndex = 1
    Me.StatusStrip1.Text = "StatusStrip1"
    '
    'ToolStripStatusLabel1
    '
    Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
    Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(121, 17)
    Me.ToolStripStatusLabel1.Text = "ToolStripStatusLabel1"
    '
    'PictureBox1
    '
    Me.PictureBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.PictureBox1.Location = New System.Drawing.Point(2, 39)
    Me.PictureBox1.Name = "PictureBox1"
    Me.PictureBox1.Size = New System.Drawing.Size(300, 150)
    Me.PictureBox1.TabIndex = 2
    Me.PictureBox1.TabStop = False
    '
    'RefreshTimer
    '
    Me.RefreshTimer.Interval = 200
    '
    'bt_200mA
    '
    Me.bt_200mA.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.bt_200mA.Location = New System.Drawing.Point(2, 195)
    Me.bt_200mA.Name = "bt_200mA"
    Me.bt_200mA.Size = New System.Drawing.Size(75, 23)
    Me.bt_200mA.TabIndex = 3
    Me.bt_200mA.Text = "200 mA"
    Me.bt_200mA.UseVisualStyleBackColor = True
    '
    'bt_2A
    '
    Me.bt_2A.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.bt_2A.Location = New System.Drawing.Point(83, 195)
    Me.bt_2A.Name = "bt_2A"
    Me.bt_2A.Size = New System.Drawing.Size(75, 23)
    Me.bt_2A.TabIndex = 4
    Me.bt_2A.Text = "2A"
    Me.bt_2A.UseVisualStyleBackColor = True
    '
    'bt_20A
    '
    Me.bt_20A.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.bt_20A.Location = New System.Drawing.Point(164, 195)
    Me.bt_20A.Name = "bt_20A"
    Me.bt_20A.Size = New System.Drawing.Size(75, 23)
    Me.bt_20A.TabIndex = 5
    Me.bt_20A.Text = "20A"
    Me.bt_20A.UseVisualStyleBackColor = True
    '
    'ACDCcheckBox
    '
    Me.ACDCcheckBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.ACDCcheckBox.AutoSize = True
    Me.ACDCcheckBox.Location = New System.Drawing.Point(255, 199)
    Me.ACDCcheckBox.Name = "ACDCcheckBox"
    Me.ACDCcheckBox.Size = New System.Drawing.Size(40, 17)
    Me.ACDCcheckBox.TabIndex = 6
    Me.ACDCcheckBox.Text = "AC"
    Me.ACDCcheckBox.UseVisualStyleBackColor = True
    '
    'Form1
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(305, 253)
    Me.Controls.Add(Me.ACDCcheckBox)
    Me.Controls.Add(Me.bt_20A)
    Me.Controls.Add(Me.bt_2A)
    Me.Controls.Add(Me.bt_200mA)
    Me.Controls.Add(Me.PictureBox1)
    Me.Controls.Add(Me.StatusStrip1)
    Me.Controls.Add(Me.ComboBox1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.Name = "Form1"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Yocto-Amp demo"
    Me.StatusStrip1.ResumeLayout(False)
    Me.StatusStrip1.PerformLayout()
    CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents InventoryTimer As System.Windows.Forms.Timer
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents RefreshTimer As System.Windows.Forms.Timer
    Friend WithEvents bt_200mA As System.Windows.Forms.Button
    Friend WithEvents bt_2A As System.Windows.Forms.Button
    Friend WithEvents bt_20A As System.Windows.Forms.Button
  Friend WithEvents ACDCcheckBox As System.Windows.Forms.CheckBox

End Class
