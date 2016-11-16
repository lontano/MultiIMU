Imports MultiIMU_components

Public Class frmSimulator
  Private WithEvents _socketClient As SocketClient


  Private Sub ButtonConnect_Click(sender As Object, e As EventArgs) Handles ButtonConnect.Click
    Try
      If _socketClient Is Nothing Then
        _socketClient = New SocketClient
        _socketClient.Connect(Me.TextBoxHost.Text, Me.NumericUpDownPort.Value)
        TimerData.Interval = 100
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
        _socketClient.Send(data.Get_csvLine)
      End If
    Catch ex As Exception

    End Try
  End Sub
End Class