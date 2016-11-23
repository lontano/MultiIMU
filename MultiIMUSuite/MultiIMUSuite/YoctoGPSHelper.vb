Public Class YoctoGPSHelper
  Public Property TargetUID As String = "any"

  Private _yGPS As YGps
  Private _yLongitude As YLongitude
  Private _yLatitude As YLatitude
  Private _yAltitude As YAltitude

  Private _longitude As Double
  Private _latitude As Double
  Private _altitude As Double

  Public ReadOnly Property Longitude As Double
    Get
      Return _longitude
    End Get
  End Property

  Public ReadOnly Property Latitude As Double
    Get
      Return _latitude
    End Get
  End Property

  Public ReadOnly Property Altitude As Double
    Get
      Return _altitude
    End Get
  End Property


  Public Event PositionUpdated(ByRef sender As YoctoGPSHelper)

  Public Enum eYoctoGPSHelperState
    Idle = 0
    Fixing
    Fixed
  End Enum

  Private _yoctoGPSHelperState As eYoctoGPSHelperState = eYoctoGPSHelperState.Idle
  Public Property YoctoGPSHelperState As eYoctoGPSHelperState
    Get
      Return _yoctoGPSHelperState
    End Get
    Set(value As eYoctoGPSHelperState)
      If _yoctoGPSHelperState <> value Then
        value = _yoctoGPSHelperState
      End If
    End Set
  End Property

  Private Sub FunctionValueChanged(func As yocto_api.YFunction, name As String)
    Debug.Print(name)

    If (_yGPS.get_isFixed() <> YGps.ISFIXED_TRUE) Then
      Console.WriteLine("Fixing...")
    Else
      _longitude = Double.Parse(_yGPS.get_longitude().Replace(".", ","))
      _latitude = Double.Parse(_yGPS.get_latitude().Replace(".", ","))
      _altitude = Double.Parse(_yGPS.get_altitude())
      Console.WriteLine(_longitude.ToString() & "  " & _latitude.ToString() & "    " & _altitude.ToString())
    End If
    RaiseEvent PositionUpdated (Me)

  End Sub

  Public Sub InitHelper()
    Dim errmsg As String = ""

    REM Setup the API to use local USB devices
    If (yRegisterHub("usb", errmsg) <> YAPI_SUCCESS) Then
      Console.WriteLine("RegisterHub error: " + errmsg)
      End
    End If

    If TargetUID = "any" Then
      _yGPS = yFirstGps()
      _yLongitude = yFirstLongitude()
      _yLatitude = yFirstLatitude()
      _yAltitude = yFirstAltitude()

      If _yGPS Is Nothing Then
        Console.WriteLine("No module connected (check USB cable) ")
        End
      End If
    Else
      _yGPS = yFindGps(TargetUID + ".gps")
      _yLongitude = yFindLongitude(TargetUID + ".longitude")
      _yLatitude = yFindLatitude(TargetUID + ".latitude")
      _yAltitude = yFindAltitude(TargetUID + ".altitude")
    End If


    Dim lat As New YFunctionValueCallback(AddressOf FunctionValueChanged)
    _yGPS.registerValueCallback(lat)
    _yLongitude.registerValueCallback(New YLongitudeValueCallback(AddressOf FunctionValueChanged))
    _yLatitude.registerValueCallback(New YLatitudeValueCallback(AddressOf FunctionValueChanged))
    _yAltitude.registerValueCallback(New YAltitudeValueCallback(AddressOf FunctionValueChanged))

    While (True)
      If Not (_yGPS.isOnline()) Then
        Console.WriteLine("Module not connected (check identification and USB cable)")
        End
      End If
      If (_yGPS.get_isFixed() <> YGps.ISFIXED_TRUE) Then
        Console.WriteLine("Fixing...")
      Else
        ' Console.WriteLine(_yGPS.get_latitude() + "  " + _yGPS.get_longitude())
      End If
      'Console.WriteLine("  (press Ctrl-C to exit)")
      ySleep(40, errmsg)
      yUpdateDeviceList(errmsg)
    End While
    yFreeAPI()
  End Sub
End Class
