'*********************************************************************
'*
'* $Id: yocto_led.vb 25275 2016-08-24 13:42:24Z mvuilleu $
'*
'* Implements yFindLed(), the high-level API for Led functions
'*
'* - - - - - - - - - License information: - - - - - - - - - 
'*
'*  Copyright (C) 2011 and beyond by Yoctopuce Sarl, Switzerland.
'*
'*  Yoctopuce Sarl (hereafter Licensor) grants to you a perpetual
'*  non-exclusive license to use, modify, copy and integrate this
'*  file into your software for the sole purpose of interfacing
'*  with Yoctopuce products.
'*
'*  You may reproduce and distribute copies of this file in
'*  source or object form, as long as the sole purpose of this
'*  code is to interface with Yoctopuce products. You must retain
'*  this notice in the distributed source file.
'*
'*  You should refer to Yoctopuce General Terms and Conditions
'*  for additional information regarding your rights and
'*  obligations.
'*
'*  THE SOFTWARE AND DOCUMENTATION ARE PROVIDED 'AS IS' WITHOUT
'*  WARRANTY OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING 
'*  WITHOUT LIMITATION, ANY WARRANTY OF MERCHANTABILITY, FITNESS
'*  FOR A PARTICULAR PURPOSE, TITLE AND NON-INFRINGEMENT. IN NO
'*  EVENT SHALL LICENSOR BE LIABLE FOR ANY INCIDENTAL, SPECIAL,
'*  INDIRECT OR CONSEQUENTIAL DAMAGES, LOST PROFITS OR LOST DATA,
'*  COST OF PROCUREMENT OF SUBSTITUTE GOODS, TECHNOLOGY OR
'*  SERVICES, ANY CLAIMS BY THIRD PARTIES (INCLUDING BUT NOT
'*  LIMITED TO ANY DEFENSE THEREOF), ANY CLAIMS FOR INDEMNITY OR
'*  CONTRIBUTION, OR OTHER SIMILAR COSTS, WHETHER ASSERTED ON THE
'*  BASIS OF CONTRACT, TORT (INCLUDING NEGLIGENCE), BREACH OF
'*  WARRANTY, OR OTHERWISE.
'*
'*********************************************************************/


Imports YDEV_DESCR = System.Int32
Imports YFUN_DESCR = System.Int32
Imports System.Runtime.InteropServices
Imports System.Text

Module yocto_led

    REM --- (YLed return codes)
    REM --- (end of YLed return codes)
    REM --- (YLed dlldef)
    REM --- (end of YLed dlldef)
  REM --- (YLed globals)

  Public Const Y_POWER_OFF As Integer = 0
  Public Const Y_POWER_ON As Integer = 1
  Public Const Y_POWER_INVALID As Integer = -1
  Public Const Y_LUMINOSITY_INVALID As Integer = YAPI.INVALID_UINT
  Public Const Y_BLINKING_STILL As Integer = 0
  Public Const Y_BLINKING_RELAX As Integer = 1
  Public Const Y_BLINKING_AWARE As Integer = 2
  Public Const Y_BLINKING_RUN As Integer = 3
  Public Const Y_BLINKING_CALL As Integer = 4
  Public Const Y_BLINKING_PANIC As Integer = 5
  Public Const Y_BLINKING_INVALID As Integer = -1
  Public Delegate Sub YLedValueCallback(ByVal func As YLed, ByVal value As String)
  Public Delegate Sub YLedTimedReportCallback(ByVal func As YLed, ByVal measure As YMeasure)
  REM --- (end of YLed globals)

  REM --- (YLed class start)

  '''*
  ''' <summary>
  '''   The Yoctopuce application programming interface
  '''   allows you not only to drive the intensity of the LED, but also to
  '''   have it blink at various preset frequencies.
  ''' <para>
  ''' </para>
  ''' </summary>
  '''/
  Public Class YLed
    Inherits YFunction
    REM --- (end of YLed class start)

    REM --- (YLed definitions)
    Public Const POWER_OFF As Integer = 0
    Public Const POWER_ON As Integer = 1
    Public Const POWER_INVALID As Integer = -1
    Public Const LUMINOSITY_INVALID As Integer = YAPI.INVALID_UINT
    Public Const BLINKING_STILL As Integer = 0
    Public Const BLINKING_RELAX As Integer = 1
    Public Const BLINKING_AWARE As Integer = 2
    Public Const BLINKING_RUN As Integer = 3
    Public Const BLINKING_CALL As Integer = 4
    Public Const BLINKING_PANIC As Integer = 5
    Public Const BLINKING_INVALID As Integer = -1
    REM --- (end of YLed definitions)

    REM --- (YLed attributes declaration)
    Protected _power As Integer
    Protected _luminosity As Integer
    Protected _blinking As Integer
    Protected _valueCallbackLed As YLedValueCallback
    REM --- (end of YLed attributes declaration)

    Public Sub New(ByVal func As String)
      MyBase.New(func)
      _classname = "Led"
      REM --- (YLed attributes initialization)
      _power = POWER_INVALID
      _luminosity = LUMINOSITY_INVALID
      _blinking = BLINKING_INVALID
      _valueCallbackLed = Nothing
      REM --- (end of YLed attributes initialization)
    End Sub

    REM --- (YLed private methods declaration)

    Protected Overrides Function _parseAttr(ByRef member As TJSONRECORD) As Integer
      If (member.name = "power") Then
        If (member.ivalue > 0) Then _power = 1 Else _power = 0
        Return 1
      End If
      If (member.name = "luminosity") Then
        _luminosity = CInt(member.ivalue)
        Return 1
      End If
      If (member.name = "blinking") Then
        _blinking = CInt(member.ivalue)
        Return 1
      End If
      Return MyBase._parseAttr(member)
    End Function

    REM --- (end of YLed private methods declaration)

    REM --- (YLed public methods declaration)
    '''*
    ''' <summary>
    '''   Returns the current LED state.
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   either <c>Y_POWER_OFF</c> or <c>Y_POWER_ON</c>, according to the current LED state
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns <c>Y_POWER_INVALID</c>.
    ''' </para>
    '''/
    Public Function get_power() As Integer
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return POWER_INVALID
        End If
      End If
      Return Me._power
    End Function


    '''*
    ''' <summary>
    '''   Changes the state of the LED.
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="newval">
    '''   either <c>Y_POWER_OFF</c> or <c>Y_POWER_ON</c>, according to the state of the LED
    ''' </param>
    ''' <para>
    ''' </para>
    ''' <returns>
    '''   <c>YAPI_SUCCESS</c> if the call succeeds.
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns a negative error code.
    ''' </para>
    '''/
    Public Function set_power(ByVal newval As Integer) As Integer
      Dim rest_val As String
      If (newval > 0) Then rest_val = "1" Else rest_val = "0"
      Return _setAttr("power", rest_val)
    End Function
    '''*
    ''' <summary>
    '''   Returns the current LED intensity (in per cent).
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   an integer corresponding to the current LED intensity (in per cent)
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns <c>Y_LUMINOSITY_INVALID</c>.
    ''' </para>
    '''/
    Public Function get_luminosity() As Integer
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return LUMINOSITY_INVALID
        End If
      End If
      Return Me._luminosity
    End Function


    '''*
    ''' <summary>
    '''   Changes the current LED intensity (in per cent).
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="newval">
    '''   an integer corresponding to the current LED intensity (in per cent)
    ''' </param>
    ''' <para>
    ''' </para>
    ''' <returns>
    '''   <c>YAPI_SUCCESS</c> if the call succeeds.
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns a negative error code.
    ''' </para>
    '''/
    Public Function set_luminosity(ByVal newval As Integer) As Integer
      Dim rest_val As String
      rest_val = Ltrim(Str(newval))
      Return _setAttr("luminosity", rest_val)
    End Function
    '''*
    ''' <summary>
    '''   Returns the current LED signaling mode.
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   a value among <c>Y_BLINKING_STILL</c>, <c>Y_BLINKING_RELAX</c>, <c>Y_BLINKING_AWARE</c>,
    '''   <c>Y_BLINKING_RUN</c>, <c>Y_BLINKING_CALL</c> and <c>Y_BLINKING_PANIC</c> corresponding to the
    '''   current LED signaling mode
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns <c>Y_BLINKING_INVALID</c>.
    ''' </para>
    '''/
    Public Function get_blinking() As Integer
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return BLINKING_INVALID
        End If
      End If
      Return Me._blinking
    End Function


    '''*
    ''' <summary>
    '''   Changes the current LED signaling mode.
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="newval">
    '''   a value among <c>Y_BLINKING_STILL</c>, <c>Y_BLINKING_RELAX</c>, <c>Y_BLINKING_AWARE</c>,
    '''   <c>Y_BLINKING_RUN</c>, <c>Y_BLINKING_CALL</c> and <c>Y_BLINKING_PANIC</c> corresponding to the
    '''   current LED signaling mode
    ''' </param>
    ''' <para>
    ''' </para>
    ''' <returns>
    '''   <c>YAPI_SUCCESS</c> if the call succeeds.
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns a negative error code.
    ''' </para>
    '''/
    Public Function set_blinking(ByVal newval As Integer) As Integer
      Dim rest_val As String
      rest_val = Ltrim(Str(newval))
      Return _setAttr("blinking", rest_val)
    End Function
    '''*
    ''' <summary>
    '''   Retrieves a LED for a given identifier.
    ''' <para>
    '''   The identifier can be specified using several formats:
    ''' </para>
    ''' <para>
    ''' </para>
    ''' <para>
    '''   - FunctionLogicalName
    ''' </para>
    ''' <para>
    '''   - ModuleSerialNumber.FunctionIdentifier
    ''' </para>
    ''' <para>
    '''   - ModuleSerialNumber.FunctionLogicalName
    ''' </para>
    ''' <para>
    '''   - ModuleLogicalName.FunctionIdentifier
    ''' </para>
    ''' <para>
    '''   - ModuleLogicalName.FunctionLogicalName
    ''' </para>
    ''' <para>
    ''' </para>
    ''' <para>
    '''   This function does not require that the LED is online at the time
    '''   it is invoked. The returned object is nevertheless valid.
    '''   Use the method <c>YLed.isOnline()</c> to test if the LED is
    '''   indeed online at a given time. In case of ambiguity when looking for
    '''   a LED by logical name, no error is notified: the first instance
    '''   found is returned. The search is performed first by hardware name,
    '''   then by logical name.
    ''' </para>
    ''' </summary>
    ''' <param name="func">
    '''   a string that uniquely characterizes the LED
    ''' </param>
    ''' <returns>
    '''   a <c>YLed</c> object allowing you to drive the LED.
    ''' </returns>
    '''/
    Public Shared Function FindLed(func As String) As YLed
      Dim obj As YLed
      obj = CType(YFunction._FindFromCache("Led", func), YLed)
      If ((obj Is Nothing)) Then
        obj = New YLed(func)
        YFunction._AddToCache("Led", func, obj)
      End If
      Return obj
    End Function

    '''*
    ''' <summary>
    '''   Registers the callback function that is invoked on every change of advertised value.
    ''' <para>
    '''   The callback is invoked only during the execution of <c>ySleep</c> or <c>yHandleEvents</c>.
    '''   This provides control over the time when the callback is triggered. For good responsiveness, remember to call
    '''   one of these two functions periodically. To unregister a callback, pass a Nothing pointer as argument.
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="callback">
    '''   the callback function to call, or a Nothing pointer. The callback function should take two
    '''   arguments: the function object of which the value has changed, and the character string describing
    '''   the new advertised value.
    ''' @noreturn
    ''' </param>
    '''/
    Public Overloads Function registerValueCallback(callback As YLedValueCallback) As Integer
      Dim val As String
      If (Not (callback Is Nothing)) Then
        YFunction._UpdateValueCallbackList(Me, True)
      Else
        YFunction._UpdateValueCallbackList(Me, False)
      End If
      Me._valueCallbackLed = callback
      REM // Immediately invoke value callback with current value
      If (Not (callback Is Nothing) And Me.isOnline()) Then
        val = Me._advertisedValue
        If (Not (val = "")) Then
          Me._invokeValueCallback(val)
        End If
      End If
      Return 0
    End Function

    Public Overrides Function _invokeValueCallback(value As String) As Integer
      If (Not (Me._valueCallbackLed Is Nothing)) Then
        Me._valueCallbackLed(Me, value)
      Else
        MyBase._invokeValueCallback(value)
      End If
      Return 0
    End Function


    '''*
    ''' <summary>
    '''   Continues the enumeration of LEDs started using <c>yFirstLed()</c>.
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   a pointer to a <c>YLed</c> object, corresponding to
    '''   a LED currently online, or a <c>Nothing</c> pointer
    '''   if there are no more LEDs to enumerate.
    ''' </returns>
    '''/
    Public Function nextLed() As YLed
      Dim hwid As String = ""
      If (YISERR(_nextFunction(hwid))) Then
        Return Nothing
      End If
      If (hwid = "") Then
        Return Nothing
      End If
      Return YLed.FindLed(hwid)
    End Function

    '''*
    ''' <summary>
    '''   Starts the enumeration of LEDs currently accessible.
    ''' <para>
    '''   Use the method <c>YLed.nextLed()</c> to iterate on
    '''   next LEDs.
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   a pointer to a <c>YLed</c> object, corresponding to
    '''   the first LED currently online, or a <c>Nothing</c> pointer
    '''   if there are none.
    ''' </returns>
    '''/
    Public Shared Function FirstLed() As YLed
      Dim v_fundescr(1) As YFUN_DESCR
      Dim dev As YDEV_DESCR
      Dim neededsize, err As Integer
      Dim serial, funcId, funcName, funcVal As String
      Dim errmsg As String = ""
      Dim size As Integer = Marshal.SizeOf(v_fundescr(0))
      Dim p As IntPtr = Marshal.AllocHGlobal(Marshal.SizeOf(v_fundescr(0)))

      err = yapiGetFunctionsByClass("Led", 0, p, size, neededsize, errmsg)
      Marshal.Copy(p, v_fundescr, 0, 1)
      Marshal.FreeHGlobal(p)

      If (YISERR(err) Or (neededsize = 0)) Then
        Return Nothing
      End If
      serial = ""
      funcId = ""
      funcName = ""
      funcVal = ""
      errmsg = ""
      If (YISERR(yapiGetFunctionInfo(v_fundescr(0), dev, serial, funcId, funcName, funcVal, errmsg))) Then
        Return Nothing
      End If
      Return YLed.FindLed(serial + "." + funcId)
    End Function

    REM --- (end of YLed public methods declaration)

  End Class

  REM --- (Led functions)

  '''*
  ''' <summary>
  '''   Retrieves a LED for a given identifier.
  ''' <para>
  '''   The identifier can be specified using several formats:
  ''' </para>
  ''' <para>
  ''' </para>
  ''' <para>
  '''   - FunctionLogicalName
  ''' </para>
  ''' <para>
  '''   - ModuleSerialNumber.FunctionIdentifier
  ''' </para>
  ''' <para>
  '''   - ModuleSerialNumber.FunctionLogicalName
  ''' </para>
  ''' <para>
  '''   - ModuleLogicalName.FunctionIdentifier
  ''' </para>
  ''' <para>
  '''   - ModuleLogicalName.FunctionLogicalName
  ''' </para>
  ''' <para>
  ''' </para>
  ''' <para>
  '''   This function does not require that the LED is online at the time
  '''   it is invoked. The returned object is nevertheless valid.
  '''   Use the method <c>YLed.isOnline()</c> to test if the LED is
  '''   indeed online at a given time. In case of ambiguity when looking for
  '''   a LED by logical name, no error is notified: the first instance
  '''   found is returned. The search is performed first by hardware name,
  '''   then by logical name.
  ''' </para>
  ''' </summary>
  ''' <param name="func">
  '''   a string that uniquely characterizes the LED
  ''' </param>
  ''' <returns>
  '''   a <c>YLed</c> object allowing you to drive the LED.
  ''' </returns>
  '''/
  Public Function yFindLed(ByVal func As String) As YLed
    Return YLed.FindLed(func)
  End Function

  '''*
  ''' <summary>
  '''   Starts the enumeration of LEDs currently accessible.
  ''' <para>
  '''   Use the method <c>YLed.nextLed()</c> to iterate on
  '''   next LEDs.
  ''' </para>
  ''' </summary>
  ''' <returns>
  '''   a pointer to a <c>YLed</c> object, corresponding to
  '''   the first LED currently online, or a <c>Nothing</c> pointer
  '''   if there are none.
  ''' </returns>
  '''/
  Public Function yFirstLed() As YLed
    Return YLed.FirstLed()
  End Function


  REM --- (end of Led functions)

End Module
