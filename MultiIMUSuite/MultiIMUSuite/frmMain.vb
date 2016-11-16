Imports MultiIMU_components

Public Class frmMain
  Private WithEvents _socketListener As MultiIMU_components.SocketListener

  Private _imu_devices As New List(Of IMUdevice)

  Private Sub _socketListener_Connected(ByRef sender As SocketListener, senderAdress As String) Handles _socketListener.Connected
    SetText(" connected " & senderAdress)
    Dim device As IMUdevice = IMUdevices.Instance.GetDevice(senderAdress)
    UpdateDevices()
  End Sub

  Private Sub _socketListener_CSVLineDataArriaval(ByRef sender As SocketListener, senderAdress As String, line As String) Handles _socketListener.CSVLineDataArriaval

    Dim device As IMUdevice = IMUdevices.Instance.GetDevice(senderAdress)
    If Not device Is Nothing Then
      device.SetIMUData(line)
      'SetText(device.ToString)
      UpdateDevices()
    End If
  End Sub

  Private Sub _socketListener_Disconnected(ByRef sender As SocketListener, senderAdress As String) Handles _socketListener.Disconnected
    SetText(" Disconnected " & senderAdress)
    UpdateDevices()
  End Sub

  Delegate Sub SetTextCallback([text] As String)

  Private Sub SetText(ByVal myText As String)
    Try
      ' InvokeRequired required compares the thread ID of the
      ' calling thread to the thread ID of the creating thread.
      ' If these threads are different, it returns true.
      If Me.InvokeRequired Then
        Dim d As New SetTextCallback(AddressOf SetText)
        Me.Invoke(d, New Object() {myText})
      Else
        'Me.TextBox1.Text = Now.ToString & " " & [text] & vbCrLf & Me.TextBox1.Text
        ' Me.Text = myText
      End If
    Catch ex As Exception

    End Try
  End Sub

  Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ButtonConnect.Click
    Try
      _socketListener = New MultiIMU_components.SocketListener

      Me.Text = _socketListener.ipAddress.ToString & ":" & _socketListener.ipLocalEndPoint.Port
      _socketListener.Start(800)

    Catch ex As Exception
      MsgBox(ex.ToString)
    End Try
  End Sub

  Private Enum devicesCols
    name = 0
    type
    time
    orientation_x
    orientation_y
    orientation_z
    gps_long
    gps_lat
    gps_att
    gps_x
    gps_y
    gps_z
    Total
  End Enum

  Delegate Sub UpdateDevicesCallback()
  Private lastUpdateMs As Double = 0
  Private Sub UpdateDevices()
    Static busy As Boolean = False

    Dim nextUpdatems As Double = (DateTime.Now - New DateTime(1970, 1, 1)).TotalMilliseconds

    If nextUpdatems - lastUpdateMs < 1000 Then Exit Sub
    lastUpdateMs = nextUpdatems

    If busy Then Exit Sub
    busy = True
    Try

      If Me.InvokeRequired Then
        Dim d As New UpdateDevicesCallback(AddressOf UpdateDevices)
        Me.Invoke(d, New Object() {})
      Else
        Me.LabelDevices.Text = ""
        For Each device As IMUdevice In IMUdevices.Instance.Devices
          Me.LabelDevices.Text = Me.LabelDevices.Text & device.ToString & vbCrLf
        Next

        If IMUdevices.Instance.Devices.Count <> Me.ListViewDevices.Items.Count Then
          Me.ListViewDevices.Items.Clear()
          For i As Integer = 0 To IMUdevices.Instance.Devices.Count - 1
            Dim itm As ListViewItem = Me.ListViewDevices.Items.Add(IMUdevices.Instance.Devices(i).name)
            For j As Integer = 0 To devicesCols.Total
              itm.SubItems.Add(" ")
            Next
          Next
        End If

        For i As Integer = 0 To IMUdevices.Instance.Devices.Count - 1
          With Me.ListViewDevices.Items(i)
            .SubItems(devicesCols.type).Text = IMUdevices.Instance.Devices(i).type.ToString
            If Not IMUdevices.Instance.Devices(i).LastIMUData Is Nothing Then
              .SubItems(devicesCols.time).Text = IMUdevices.Instance.Devices(i).LastIMUData.timeStamp
              .SubItems(devicesCols.orientation_x).Text = IMUdevices.Instance.Devices(i).LastIMUData.magneticOrientation.X
              .SubItems(devicesCols.orientation_y).Text = IMUdevices.Instance.Devices(i).LastIMUData.magneticOrientation.Y
              .SubItems(devicesCols.orientation_z).Text = IMUdevices.Instance.Devices(i).LastIMUData.magneticOrientation.Z
              .SubItems(devicesCols.gps_long).Text = IMUdevices.Instance.Devices(i).LastIMUData.gpsPosition.X
              .SubItems(devicesCols.gps_lat).Text = IMUdevices.Instance.Devices(i).LastIMUData.gpsPosition.Y
              .SubItems(devicesCols.gps_att).Text = IMUdevices.Instance.Devices(i).LastIMUData.gpsPosition.Z
              .SubItems(devicesCols.gps_x).Text = IMUdevices.Instance.Devices(i).LastIMUData.gps_XYZ.X
              .SubItems(devicesCols.gps_y).Text = IMUdevices.Instance.Devices(i).LastIMUData.gps_XYZ.Y
              .SubItems(devicesCols.gps_z).Text = IMUdevices.Instance.Devices(i).LastIMUData.gps_XYZ.Z
            End If
          End With
        Next

      End If

      IMUdevices.Instance.UpdateVisibilities()
      Me.UcIMUdeviceSelected.UpdateVisibleDevices()

    Catch ex As Exception

    End Try
    busy = False
  End Sub

  Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

  End Sub

  Private _selectedIMUDevice As IMUdevice
  Private Sub ListViewDevices_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListViewDevices.SelectedIndexChanged
    Try
      If Me.ListViewDevices.SelectedIndices.Count = 0 Then Exit Sub

      _selectedIMUDevice = IMUdevices.Instance.Devices(ListViewDevices.SelectedIndices(0))
      Me.UcIMUdeviceSelected.SetIMUDevice(_selectedIMUDevice)
      IMUdevices.Instance.UpdateVisibilities()
      Me.UcIMUdeviceSelected.UpdateVisibleDevices()
      Me.UpdateDevices()
    Catch ex As Exception

    End Try
  End Sub

  Private Sub UcIMUdeviceSelected_Load(sender As Object, e As EventArgs) Handles UcIMUdeviceSelected.Load

  End Sub

  Private Sub ButtonSimulator_Click(sender As Object, e As EventArgs) Handles ButtonSimulator.Click
    Dim dlg As New frmSimulator()
    dlg.Show(Me)
  End Sub
End Class
