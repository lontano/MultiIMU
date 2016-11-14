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

    res = res & Me.timeStamp & "   "
    res = res & " orientation_X = " & CInt(Me.magneticOrientation.X) & ","
    res = res & " orientation_Y = " & CInt(Me.magneticOrientation.Y) & ","
    res = res & " orientation_Z = " & CInt(Me.magneticOrientation.Z) & ","
    res = res & " Long = " & CInt(Me.gpsPosition.X) & ","
    res = res & " Lat = " & CInt(Me.gpsPosition.Y) & ","
    res = res & " Alt = " & CInt(Me.gpsPosition.Z) & ","

    Return res
  End Function
#End Region


  Public Overrides Function ToString() As String
    Return Me.Get_csvLine
  End Function


End Class
