Imports System.Reflection
Imports System.IO

Module Module1

  Private Sub Usage()
    Dim errmsg As String = ""
    Dim exe As String = Path.GetFileName(Assembly.GetExecutingAssembly().Location)
    Console.WriteLine("Bad command line arguments")
    Console.WriteLine(exe + " <serial_number>  [ color | rgb ]")
    Console.WriteLine(exe + " <logical_name> [ color | rgb ]")
    Console.WriteLine(exe + " any  [ color | rgb ] ")
    Console.WriteLine("Eg.")
    Console.WriteLine(exe + " any FF1493 ")
    Console.WriteLine(exe + " YRGBHI01-123456 red")
    System.Threading.Thread.Sleep(2500)
    End
  End Sub

  Sub Main()
    Dim argv() As String = System.Environment.GetCommandLineArgs()
    Dim errmsg As String = ""
    Dim target As String
    Dim led1 As YColorLed

    Dim color_str As String
    Dim color As Integer

    REM Setup the API to use local USB devices
    If (yRegisterHub("usb", errmsg) <> YAPI_SUCCESS) Then
      Console.WriteLine("RegisterHub error: " + errmsg)
      End
    End If

    If argv.Length < 3 Then Usage()

    target = argv(1)
    color_str = argv(2).ToUpper()

    If (color_str = "RED") Then
      color = &HFF0000
    ElseIf (color_str = "GREEN") Then
      color = &HFF00
    ElseIf (color_str = "BLUE") Then
      color = &HFF
    Else
      color = CInt(Val("&H" + color_str))
    End If

    If target = "any" Then
      led1 = yFirstColorLed()
      If led1 Is Nothing Then
        Console.WriteLine("No module connected (check USB cable) ")
        End
      End If
    Else
      led1 = yFindColorLed(target + ".colorLed1")
    End If

    If (led1.isOnline()) Then
      led1.rgbMove(color, 1000) REM smooth transition
    Else
      Console.WriteLine("Module not connected (check identification and USB cable)")
    End If
    yFreeAPI()
  End Sub
End Module
