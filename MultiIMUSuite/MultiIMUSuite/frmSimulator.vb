Imports MultiIMU_components
Imports MultiIMUSuite

Public Class frmSimulator
  Private WithEvents _socketClient As TCPSocketClient


  Private Sub ButtonConnect_Click(sender As Object, e As EventArgs) Handles ButtonConnect.Click
    Try
      If _socketClient Is Nothing Then
        _socketClient = New TCPSocketClient
        _socketClient.Connect(Me.TextBoxHost.Text, Me.NumericUpDownPort.Value)
        TimerData.Interval = 20
        TimerData.Enabled = True
        Me.ButtonConnect.Text = "Disconnect"
      Else
        TimerData.Enabled = False
        _socketClient.Disconnect()
        _socketClient = Nothing
        Me.ButtonConnect.Text = "Connect"
      End If
      Me.TextBoxHost.Enabled = _socketClient Is Nothing
      Me.NumericUpDownPort.Enabled = _socketClient Is Nothing

    Catch ex As Exception

    End Try
  End Sub

  Private Sub TimerData_Tick(sender As Object, e As EventArgs) Handles TimerData.Tick
    Try
      If _socketClient.IsConnected Then
        Dim data As New IMUdata(Me.TextBoxName.Text)
        data.timeStamp = (DateTime.Now - New DateTime(1970, 1, 1)).TotalMilliseconds
        data.magneticOrientation.X = Me.NumericUpDownOrientatin_X.Value
        data.magneticOrientation.Y = Me.NumericUpDownOrientatin_Y.Value
        data.magneticOrientation.Z = Me.NumericUpDownOrientatin_Z.Value

        data.gpsPosition.X = Me.NumericUpDown1.Value
        data.gpsPosition.Y = Me.NumericUpDown2.Value
        data.gpsPosition.Z = Me.NumericUpDown3.Value
        _socketClient.Send(data.Get_csvLine)
      End If
    Catch ex As Exception

    End Try
  End Sub

  Private Sub frmSimulator_Load(sender As Object, e As EventArgs) Handles MyBase.Load

  End Sub

  Private WithEvents _yoctogps As YoctoGPSHelper
  Private Sub ButtonYocto_Click(sender As Object, e As EventArgs) Handles ButtonYocto.Click
    _yoctogps = New YoctoGPSHelper()
    _yoctogps.InitHelper()
  End Sub

  Private Sub _yoctogps_PositionUpdated(ByRef sender As YoctoGPSHelper) Handles _yoctogps.PositionUpdated
    'Me.NumericUpDown1.Value = _yoctogps.Longitude
    'Me.NumericUpDown1.Value = _yoctogps.Latitude
    'Me.NumericUpDown1.Value = _yoctogps.Altitude
  End Sub
End Class