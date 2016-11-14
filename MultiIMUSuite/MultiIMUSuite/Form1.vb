Imports MultiIMU_components

Public Class Form1
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
      SetText(device.ToString)
      UpdateDevices()
    End If
  End Sub

  Private Sub _socketListener_Disconnected(ByRef sender As SocketListener, senderAdress As String) Handles _socketListener.Disconnected
    SetText(" Disconnected " & senderAdress)
    UpdateDevices()
  End Sub

  Delegate Sub SetTextCallback([text] As String)

  Private Sub SetText(ByVal myText As String)
    ' InvokeRequired required compares the thread ID of the
    ' calling thread to the thread ID of the creating thread.
    ' If these threads are different, it returns true.
    If Me.InvokeRequired Then
      Dim d As New SetTextCallback(AddressOf SetText)
      Me.Invoke(d, New Object() {myText})
    Else
      'Me.TextBox1.Text = Now.ToString & " " & [text] & vbCrLf & Me.TextBox1.Text
      Me.Text = myText
    End If
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

  Delegate Sub UpdateDevicesCallback()
  Private Sub UpdateDevices()
    If Me.InvokeRequired Then
      Dim d As New UpdateDevicesCallback(AddressOf UpdateDevices)
      Me.Invoke(d, New Object() {})
    Else
      Me.LabelDevices.Text = ""
      For Each device As IMUdevice In IMUdevices.Instance.Devices
        Me.LabelDevices.Text = Me.LabelDevices.Text & device.ToString & vbCrLf
      Next

    End If
  End Sub

  Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

  End Sub
End Class
