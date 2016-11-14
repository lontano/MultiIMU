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
      End If
    Catch ex As Exception
    End Try
    Return res
  End Function
End Class
