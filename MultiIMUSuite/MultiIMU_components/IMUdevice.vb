Public Class IMUdevice
  Public Enum deviceType
    transponder
    camera
  End Enum

  Public Property name As String
  Public Property type As deviceType = deviceType.transponder
  Public Property LastIMUData As IMUdata
  Public Property IMUData As New List(Of IMUdata)

  Public Sub New(name As String)
    Me.name = name
  End Sub

  Public Sub New(name As String, type As deviceType)
    Me.name = name
    Me.type = type
  End Sub

  Public Sub SetIMUData(new_data As IMUdata)
    If Me.LastIMUData Is Nothing Then
      Me.LastIMUData = new_data
    ElseIf Me.LastIMUData.timeStamp < new_data.timeStamp Then
      Me.LastIMUData = new_data
    End If

    'Me.IMUData.Add(Me.LastIMUData)
  End Sub

  Public Sub SetIMUData(csv_line As String)
    Dim new_data As New IMUdata(Me.name, csv_line)
    If new_data.timeStamp > 0 Then
      Me.SetIMUData(new_data)
    End If
    'Me.IMUData.Add(Me.LastIMUData)
  End Sub

  Public Overrides Function ToString() As String
    If Me.LastIMUData Is Nothing Then
      Return Me.name & "<" & type.ToString & "> "
    Else
      Return Me.name & "<" & type.ToString & "> " & Me.LastIMUData.ToString
    End If
  End Function
End Class
