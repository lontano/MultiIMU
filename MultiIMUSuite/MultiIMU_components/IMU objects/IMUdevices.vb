Public Class IMUdevices
#Region "Singleton"
  Public Shared ReadOnly Property Instance() As IMUdevices
    Get
      Return Nested.instance
    End Get
  End Property

  Private Class Nested
    ' Explicit static constructor to tell compiler
    ' not to mark type as beforefieldinit
    Shared Sub New()
    End Sub

    Friend Shared ReadOnly instance As New IMUdevices()
  End Class
#End Region

  Public Property Devices As New List(Of IMUdevice)
  Public ReadOnly Property Transponders As New List(Of IMUdevice)
  Public ReadOnly Property Cameras As New List(Of IMUdevice)

  Public Function GetDevice(name As String) As IMUdevice
    Dim res As IMUdevice = Nothing
    Try
      For Each aux As IMUdevice In Me.Devices
        If aux.name = name Then
          res = aux
          Exit For
        End If
      Next
      If res Is Nothing Then
        res = New IMUdevice(name)
        Me.Devices.Add(res)
        UpdateCollections()
      End If
    Catch ex As Exception
    End Try
    Return res
  End Function

  Public Sub UpdateCollections()
    Try
      Me.Transponders.Clear()
      Me.Cameras.Clear()
      For Each device As IMUdevice In Me.Devices
        Select Case device.type
          Case IMUdevice.deviceType.camera
            Me.Cameras.Add(device)
          Case IMUdevice.deviceType.transponder
            Me.Transponders.Add(device)
        End Select
      Next
    Catch ex As Exception

    End Try
  End Sub

  Public Sub UpdateVisibilities()
    Try
      For Each device As IMUdevice In Me.Devices
        device.VisibleFromCameraIDs.Clear()
        device.VisibleTransponderIDs.Clear()
      Next
      For Each cam As IMUdevice In Me.Cameras
        For Each transponder As IMUdevice In Me.Transponders
          Me.UpdateVisibility(cam, transponder)
        Next
      Next
    Catch ex As Exception

    End Try
  End Sub

  Public Sub UpdateVisibility(cam As IMUdevice, transponder As IMUdevice)
    Try
      Dim p1 As New Drawing.PointF(cam.LastIMUData.gps_XYZ.X, cam.LastIMUData.gps_XYZ.Y)
      Dim p2 As New Drawing.PointF(transponder.LastIMUData.gps_XYZ.X, transponder.LastIMUData.gps_XYZ.Y)
      Dim o1 As New Drawing.PointF(cam.LastIMUData.magneticOrientation.X, cam.LastIMUData.magneticOrientation.Y)

      Dim v21 As New Drawing.PointF(p2.X - p1.X, p2.Y - p1.Y)

      Dim distance As Double = Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2))

      Debug.Print("updateVisibility between cam " & cam.name & " and transponder " & transponder.name)
      Debug.Print("    distance " & distance & "  " & v21.X & " " & v21.Y & "  ")
      Debug.Print("    orientation " & o1.X & " " & o1.Y & "  ")
    Catch ex As Exception

    End Try
  End Sub
End Class
