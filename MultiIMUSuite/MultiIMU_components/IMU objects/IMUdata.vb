Public Class Point3D
  Public Property X As Double = 0
  Public Property Y As Double = 0
  Public Property Z As Double = 0
End Class


Public Class IMUdata
  Public source As String
  Public timeStamp As Double = 0
  Public magneticOrientation As New Point3D
  Public gpsPosition As New Point3D

#Region "Constructors"
  Public Sub New(source As String)
    Me.source = source
  End Sub

  Public Sub New(source As String, csv_line As String)
    Me.source = source
    Try
      Dim aux() As String = csv_line.Split(",")
      If aux.Length > 6 Then
        Me.timeStamp = CDbl(aux(0))
        Me.magneticOrientation.X = CDbl(aux(1).Replace(".", ","))
        Me.magneticOrientation.Y = CDbl(aux(2).Replace(".", ","))
        Me.magneticOrientation.Z = CDbl(aux(3).Replace(".", ","))
        Me.gpsPosition.X = CDbl(aux(4).Replace(".", ","))
        Me.gpsPosition.Y = CDbl(aux(5).Replace(".", ","))
        Me.gpsPosition.Z = CDbl(aux(6).Replace(".", ","))
      Else
        Me.timeStamp = 0
      End If
    Catch ex As Exception
    End Try
  End Sub

  Public Function Get_csvLine() As String
    Dim res As String = ""

    res = res & Strings.Format(Me.timeStamp).Replace(",", ".") & ","
    res = res & "" & Strings.Format(Me.magneticOrientation.X, "0.00").Replace(",", ".") & ","
    res = res & "" & Strings.Format(Me.magneticOrientation.Y).Replace(",", ".") & ","
    res = res & "" & Strings.Format(Me.magneticOrientation.Z).Replace(",", ".") & ","
    res = res & "" & Strings.Format(Me.gpsPosition.X).Replace(",", ".") & ","
    res = res & "" & Strings.Format(Me.gpsPosition.Y).Replace(",", ".") & ","
    res = res & "" & Strings.Format(Me.gpsPosition.Z).Replace(",", ".") & ",#"

    Return res
  End Function
#End Region

  Public ReadOnly Property gps_XYZ As Point3D
    Get
      Dim res As New Point3D
      Dim cosLat As Double = Math.Cos(Me.gpsPosition.X * Math.PI / 180.0)
      Dim sinLat As Double = Math.Sin(Me.gpsPosition.X * Math.PI / 180.0)
      Dim cosLon As Double = Math.Cos(Me.gpsPosition.Y * Math.PI / 180.0)
      Dim sinLon As Double = Math.Sin(Me.gpsPosition.Y * Math.PI / 180.0)
      Dim rad As Double = 6378137.0
      Dim f As Double = 1.0 / 298.257224
      Dim C As Double = 1.0 / Math.Sqrt(cosLat * cosLat + (1 - f) * (1 - f) * sinLat * sinLat)
      Dim S As Double = (1.0 - f) * (1.0 - f) * C
      Dim h As Double = 0.0
      res.X = (rad * C + h) * cosLat * cosLon
      res.Y = (rad * C + h) * cosLat * sinLon
      res.Z = (rad * S + h) * sinLat
      Return res
    End Get
  End Property


  Public Overrides Function ToString() As String
    Return Me.Get_csvLine
  End Function


End Class
