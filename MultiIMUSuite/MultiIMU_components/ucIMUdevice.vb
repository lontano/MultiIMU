Public Class ucIMUdevice
  Private WithEvents _imuDevice As IMUdevice
  Private _initializing As Boolean = False

  Public Sub SetIMUDevice(ByRef device As IMUdevice)
    Try
      _imuDevice = device
      ShowDevice()
    Catch ex As Exception
    End Try
  End Sub

  Private Sub ShowDevice()
    Try
      _initializing = True
      If _imuDevice Is Nothing Then
        Me.LabelName.Text = ""
        Me.ComboBoxType.Text = ""
      Else
        Me.LabelName.Text = _imuDevice.name
        Me.ComboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxType.Items.Clear()
        Me.ComboBoxType.Items.Add(IMUdevice.deviceType.camera.ToString)
        Me.ComboBoxType.Items.Add(IMUdevice.deviceType.transponder.ToString)
        Me.ComboBoxType.Text = _imuDevice.type.ToString
      End If
    Catch ex As Exception

    End Try
    _initializing = False
  End Sub

  Private Sub ComboBoxType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxType.SelectedIndexChanged
    Try
      If _imuDevice Is Nothing Then Exit Sub
      If _initializing Then Exit Sub

      Select Case Me.ComboBoxType.Text
        Case IMUdevice.deviceType.camera.ToString
          _imuDevice.type = IMUdevice.deviceType.camera
        Case IMUdevice.deviceType.transponder.ToString
          _imuDevice.type = IMUdevice.deviceType.transponder
      End Select
      IMUdevices.Instance.UpdateCollections()
    Catch ex As Exception

    End Try
  End Sub

  Public Sub UpdateVisibleDevices()
    Try
      Dim text As String = ""
      Select Case _imuDevice.type
        Case IMUdevice.deviceType.camera
          For Each deviceID As String In _imuDevice.VisibleTransponderIDs
            If text <> "" Then text = text & ", "
            text = text & deviceID
          Next
          text = "Visible transponders from this camera" & vbCrLf & text
        Case IMUdevice.deviceType.transponder
          For Each deviceID As String In _imuDevice.VisibleFromCameraIDs
            If text <> "" Then text = text & ", "
            text = text & deviceID
          Next
          text = "This transponder is visible from these cameras" & vbCrLf & text
      End Select
      Me.LabelVisibleDevices.Text = text
    Catch ex As Exception

    End Try
  End Sub
End Class
