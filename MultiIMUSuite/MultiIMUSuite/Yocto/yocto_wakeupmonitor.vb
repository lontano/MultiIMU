'*********************************************************************
'*
'* $Id: yocto_wakeupmonitor.vb 25275 2016-08-24 13:42:24Z mvuilleu $
'*
'* Implements yFindWakeUpMonitor(), the high-level API for WakeUpMonitor functions
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

Module yocto_wakeupmonitor

    REM --- (YWakeUpMonitor return codes)
    REM --- (end of YWakeUpMonitor return codes)
    REM --- (YWakeUpMonitor dlldef)
    REM --- (end of YWakeUpMonitor dlldef)
  REM --- (YWakeUpMonitor globals)

  Public Const Y_POWERDURATION_INVALID As Integer = YAPI.INVALID_INT
  Public Const Y_SLEEPCOUNTDOWN_INVALID As Integer = YAPI.INVALID_INT
  Public Const Y_NEXTWAKEUP_INVALID As Long = YAPI.INVALID_LONG
  Public Const Y_WAKEUPREASON_USBPOWER As Integer = 0
  Public Const Y_WAKEUPREASON_EXTPOWER As Integer = 1
  Public Const Y_WAKEUPREASON_ENDOFSLEEP As Integer = 2
  Public Const Y_WAKEUPREASON_EXTSIG1 As Integer = 3
  Public Const Y_WAKEUPREASON_SCHEDULE1 As Integer = 4
  Public Const Y_WAKEUPREASON_SCHEDULE2 As Integer = 5
  Public Const Y_WAKEUPREASON_INVALID As Integer = -1
  Public Const Y_WAKEUPSTATE_SLEEPING As Integer = 0
  Public Const Y_WAKEUPSTATE_AWAKE As Integer = 1
  Public Const Y_WAKEUPSTATE_INVALID As Integer = -1
  Public Const Y_RTCTIME_INVALID As Long = YAPI.INVALID_LONG
  Public Delegate Sub YWakeUpMonitorValueCallback(ByVal func As YWakeUpMonitor, ByVal value As String)
  Public Delegate Sub YWakeUpMonitorTimedReportCallback(ByVal func As YWakeUpMonitor, ByVal measure As YMeasure)
  REM --- (end of YWakeUpMonitor globals)

  REM --- (YWakeUpMonitor class start)

  '''*
  ''' <summary>
  '''   The WakeUpMonitor function handles globally all wake-up sources, as well
  '''   as automated sleep mode.
  ''' <para>
  ''' </para>
  ''' </summary>
  '''/
  Public Class YWakeUpMonitor
    Inherits YFunction
    REM --- (end of YWakeUpMonitor class start)

    REM --- (YWakeUpMonitor definitions)
    Public Const POWERDURATION_INVALID As Integer = YAPI.INVALID_INT
    Public Const SLEEPCOUNTDOWN_INVALID As Integer = YAPI.INVALID_INT
    Public Const NEXTWAKEUP_INVALID As Long = YAPI.INVALID_LONG
    Public Const WAKEUPREASON_USBPOWER As Integer = 0
    Public Const WAKEUPREASON_EXTPOWER As Integer = 1
    Public Const WAKEUPREASON_ENDOFSLEEP As Integer = 2
    Public Const WAKEUPREASON_EXTSIG1 As Integer = 3
    Public Const WAKEUPREASON_SCHEDULE1 As Integer = 4
    Public Const WAKEUPREASON_SCHEDULE2 As Integer = 5
    Public Const WAKEUPREASON_INVALID As Integer = -1
    Public Const WAKEUPSTATE_SLEEPING As Integer = 0
    Public Const WAKEUPSTATE_AWAKE As Integer = 1
    Public Const WAKEUPSTATE_INVALID As Integer = -1
    Public Const RTCTIME_INVALID As Long = YAPI.INVALID_LONG
    REM --- (end of YWakeUpMonitor definitions)

    REM --- (YWakeUpMonitor attributes declaration)
    Protected _powerDuration As Integer
    Protected _sleepCountdown As Integer
    Protected _nextWakeUp As Long
    Protected _wakeUpReason As Integer
    Protected _wakeUpState As Integer
    Protected _rtcTime As Long
    Protected _endOfTime As Integer
    Protected _valueCallbackWakeUpMonitor As YWakeUpMonitorValueCallback
    REM --- (end of YWakeUpMonitor attributes declaration)

    Public Sub New(ByVal func As String)
      MyBase.New(func)
      _classname = "WakeUpMonitor"
      REM --- (YWakeUpMonitor attributes initialization)
      _powerDuration = POWERDURATION_INVALID
      _sleepCountdown = SLEEPCOUNTDOWN_INVALID
      _nextWakeUp = NEXTWAKEUP_INVALID
      _wakeUpReason = WAKEUPREASON_INVALID
      _wakeUpState = WAKEUPSTATE_INVALID
      _rtcTime = RTCTIME_INVALID
      _endOfTime = 2145960000
      _valueCallbackWakeUpMonitor = Nothing
      REM --- (end of YWakeUpMonitor attributes initialization)
    End Sub

    REM --- (YWakeUpMonitor private methods declaration)

    Protected Overrides Function _parseAttr(ByRef member As TJSONRECORD) As Integer
      If (member.name = "powerDuration") Then
        _powerDuration = CInt(member.ivalue)
        Return 1
      End If
      If (member.name = "sleepCountdown") Then
        _sleepCountdown = CInt(member.ivalue)
        Return 1
      End If
      If (member.name = "nextWakeUp") Then
        _nextWakeUp = member.ivalue
        Return 1
      End If
      If (member.name = "wakeUpReason") Then
        _wakeUpReason = CInt(member.ivalue)
        Return 1
      End If
      If (member.name = "wakeUpState") Then
        _wakeUpState = CInt(member.ivalue)
        Return 1
      End If
      If (member.name = "rtcTime") Then
        _rtcTime = member.ivalue
        Return 1
      End If
      Return MyBase._parseAttr(member)
    End Function

    REM --- (end of YWakeUpMonitor private methods declaration)

    REM --- (YWakeUpMonitor public methods declaration)
    '''*
    ''' <summary>
    '''   Returns the maximal wake up time (in seconds) before automatically going to sleep.
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   an integer corresponding to the maximal wake up time (in seconds) before automatically going to sleep
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns <c>Y_POWERDURATION_INVALID</c>.
    ''' </para>
    '''/
    Public Function get_powerDuration() As Integer
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return POWERDURATION_INVALID
        End If
      End If
      Return Me._powerDuration
    End Function


    '''*
    ''' <summary>
    '''   Changes the maximal wake up time (seconds) before automatically going to sleep.
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="newval">
    '''   an integer corresponding to the maximal wake up time (seconds) before automatically going to sleep
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
    Public Function set_powerDuration(ByVal newval As Integer) As Integer
      Dim rest_val As String
      rest_val = Ltrim(Str(newval))
      Return _setAttr("powerDuration", rest_val)
    End Function
    '''*
    ''' <summary>
    '''   Returns the delay before the  next sleep period.
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   an integer corresponding to the delay before the  next sleep period
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns <c>Y_SLEEPCOUNTDOWN_INVALID</c>.
    ''' </para>
    '''/
    Public Function get_sleepCountdown() As Integer
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return SLEEPCOUNTDOWN_INVALID
        End If
      End If
      Return Me._sleepCountdown
    End Function


    '''*
    ''' <summary>
    '''   Changes the delay before the next sleep period.
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="newval">
    '''   an integer corresponding to the delay before the next sleep period
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
    Public Function set_sleepCountdown(ByVal newval As Integer) As Integer
      Dim rest_val As String
      rest_val = Ltrim(Str(newval))
      Return _setAttr("sleepCountdown", rest_val)
    End Function
    '''*
    ''' <summary>
    '''   Returns the next scheduled wake up date/time (UNIX format).
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   an integer corresponding to the next scheduled wake up date/time (UNIX format)
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns <c>Y_NEXTWAKEUP_INVALID</c>.
    ''' </para>
    '''/
    Public Function get_nextWakeUp() As Long
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return NEXTWAKEUP_INVALID
        End If
      End If
      Return Me._nextWakeUp
    End Function


    '''*
    ''' <summary>
    '''   Changes the days of the week when a wake up must take place.
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="newval">
    '''   an integer corresponding to the days of the week when a wake up must take place
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
    Public Function set_nextWakeUp(ByVal newval As Long) As Integer
      Dim rest_val As String
      rest_val = Ltrim(Str(newval))
      Return _setAttr("nextWakeUp", rest_val)
    End Function
    '''*
    ''' <summary>
    '''   Returns the latest wake up reason.
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   a value among <c>Y_WAKEUPREASON_USBPOWER</c>, <c>Y_WAKEUPREASON_EXTPOWER</c>,
    '''   <c>Y_WAKEUPREASON_ENDOFSLEEP</c>, <c>Y_WAKEUPREASON_EXTSIG1</c>, <c>Y_WAKEUPREASON_SCHEDULE1</c>
    '''   and <c>Y_WAKEUPREASON_SCHEDULE2</c> corresponding to the latest wake up reason
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns <c>Y_WAKEUPREASON_INVALID</c>.
    ''' </para>
    '''/
    Public Function get_wakeUpReason() As Integer
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return WAKEUPREASON_INVALID
        End If
      End If
      Return Me._wakeUpReason
    End Function

    '''*
    ''' <summary>
    '''   Returns  the current state of the monitor.
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   either <c>Y_WAKEUPSTATE_SLEEPING</c> or <c>Y_WAKEUPSTATE_AWAKE</c>, according to  the current state
    '''   of the monitor
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns <c>Y_WAKEUPSTATE_INVALID</c>.
    ''' </para>
    '''/
    Public Function get_wakeUpState() As Integer
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return WAKEUPSTATE_INVALID
        End If
      End If
      Return Me._wakeUpState
    End Function


    Public Function set_wakeUpState(ByVal newval As Integer) As Integer
      Dim rest_val As String
      rest_val = Ltrim(Str(newval))
      Return _setAttr("wakeUpState", rest_val)
    End Function
    Public Function get_rtcTime() As Long
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return RTCTIME_INVALID
        End If
      End If
      Return Me._rtcTime
    End Function

    '''*
    ''' <summary>
    '''   Retrieves a monitor for a given identifier.
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
    '''   This function does not require that the monitor is online at the time
    '''   it is invoked. The returned object is nevertheless valid.
    '''   Use the method <c>YWakeUpMonitor.isOnline()</c> to test if the monitor is
    '''   indeed online at a given time. In case of ambiguity when looking for
    '''   a monitor by logical name, no error is notified: the first instance
    '''   found is returned. The search is performed first by hardware name,
    '''   then by logical name.
    ''' </para>
    ''' </summary>
    ''' <param name="func">
    '''   a string that uniquely characterizes the monitor
    ''' </param>
    ''' <returns>
    '''   a <c>YWakeUpMonitor</c> object allowing you to drive the monitor.
    ''' </returns>
    '''/
    Public Shared Function FindWakeUpMonitor(func As String) As YWakeUpMonitor
      Dim obj As YWakeUpMonitor
      obj = CType(YFunction._FindFromCache("WakeUpMonitor", func), YWakeUpMonitor)
      If ((obj Is Nothing)) Then
        obj = New YWakeUpMonitor(func)
        YFunction._AddToCache("WakeUpMonitor", func, obj)
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
    Public Overloads Function registerValueCallback(callback As YWakeUpMonitorValueCallback) As Integer
      Dim val As String
      If (Not (callback Is Nothing)) Then
        YFunction._UpdateValueCallbackList(Me, True)
      Else
        YFunction._UpdateValueCallbackList(Me, False)
      End If
      Me._valueCallbackWakeUpMonitor = callback
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
      If (Not (Me._valueCallbackWakeUpMonitor Is Nothing)) Then
        Me._valueCallbackWakeUpMonitor(Me, value)
      Else
        MyBase._invokeValueCallback(value)
      End If
      Return 0
    End Function

    '''*
    ''' <summary>
    '''   Forces a wake up.
    ''' <para>
    ''' </para>
    ''' </summary>
    '''/
    Public Overridable Function wakeUp() As Integer
      REM // may throw an exception
      Return Me.set_wakeUpState(WAKEUPSTATE_AWAKE)
    End Function

    '''*
    ''' <summary>
    '''   Goes to sleep until the next wake up condition is met,  the
    '''   RTC time must have been set before calling this function.
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="secBeforeSleep">
    '''   number of seconds before going into sleep mode,
    ''' </param>
    ''' <returns>
    '''   <c>YAPI_SUCCESS</c> if the call succeeds.
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns a negative error code.
    ''' </para>
    '''/
    Public Overridable Function sleep(secBeforeSleep As Integer) As Integer
      Dim currTime As Integer = 0
      currTime = CInt(Me.get_rtcTime())
      If Not(currTime <> 0) Then
        me._throw( YAPI.RTC_NOT_READY,  "RTC time not set")
        return YAPI.RTC_NOT_READY
      end if
      Me.set_nextWakeUp(Me._endOfTime)
      Me.set_sleepCountdown(secBeforeSleep)
      Return YAPI.SUCCESS
    End Function

    '''*
    ''' <summary>
    '''   Goes to sleep for a specific duration or until the next wake up condition is met, the
    '''   RTC time must have been set before calling this function.
    ''' <para>
    '''   The count down before sleep
    '''   can be canceled with resetSleepCountDown.
    ''' </para>
    ''' </summary>
    ''' <param name="secUntilWakeUp">
    '''   number of seconds before next wake up
    ''' </param>
    ''' <param name="secBeforeSleep">
    '''   number of seconds before going into sleep mode
    ''' </param>
    ''' <returns>
    '''   <c>YAPI_SUCCESS</c> if the call succeeds.
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns a negative error code.
    ''' </para>
    '''/
    Public Overridable Function sleepFor(secUntilWakeUp As Integer, secBeforeSleep As Integer) As Integer
      Dim currTime As Integer = 0
      currTime = CInt(Me.get_rtcTime())
      If Not(currTime <> 0) Then
        me._throw( YAPI.RTC_NOT_READY,  "RTC time not set")
        return YAPI.RTC_NOT_READY
      end if
      Me.set_nextWakeUp(currTime+secUntilWakeUp)
      Me.set_sleepCountdown(secBeforeSleep)
      Return YAPI.SUCCESS
    End Function

    '''*
    ''' <summary>
    '''   Go to sleep until a specific date is reached or until the next wake up condition is met, the
    '''   RTC time must have been set before calling this function.
    ''' <para>
    '''   The count down before sleep
    '''   can be canceled with resetSleepCountDown.
    ''' </para>
    ''' </summary>
    ''' <param name="wakeUpTime">
    '''   wake-up datetime (UNIX format)
    ''' </param>
    ''' <param name="secBeforeSleep">
    '''   number of seconds before going into sleep mode
    ''' </param>
    ''' <returns>
    '''   <c>YAPI_SUCCESS</c> if the call succeeds.
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns a negative error code.
    ''' </para>
    '''/
    Public Overridable Function sleepUntil(wakeUpTime As Integer, secBeforeSleep As Integer) As Integer
      Dim currTime As Integer = 0
      currTime = CInt(Me.get_rtcTime())
      If Not(currTime <> 0) Then
        me._throw( YAPI.RTC_NOT_READY,  "RTC time not set")
        return YAPI.RTC_NOT_READY
      end if
      Me.set_nextWakeUp(wakeUpTime)
      Me.set_sleepCountdown(secBeforeSleep)
      Return YAPI.SUCCESS
    End Function

    '''*
    ''' <summary>
    '''   Resets the sleep countdown.
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   <c>YAPI_SUCCESS</c> if the call succeeds.
    '''   On failure, throws an exception or returns a negative error code.
    ''' </returns>
    '''/
    Public Overridable Function resetSleepCountDown() As Integer
      Me.set_sleepCountdown(0)
      Me.set_nextWakeUp(0)
      Return YAPI.SUCCESS
    End Function


    '''*
    ''' <summary>
    '''   Continues the enumeration of monitors started using <c>yFirstWakeUpMonitor()</c>.
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   a pointer to a <c>YWakeUpMonitor</c> object, corresponding to
    '''   a monitor currently online, or a <c>Nothing</c> pointer
    '''   if there are no more monitors to enumerate.
    ''' </returns>
    '''/
    Public Function nextWakeUpMonitor() As YWakeUpMonitor
      Dim hwid As String = ""
      If (YISERR(_nextFunction(hwid))) Then
        Return Nothing
      End If
      If (hwid = "") Then
        Return Nothing
      End If
      Return YWakeUpMonitor.FindWakeUpMonitor(hwid)
    End Function

    '''*
    ''' <summary>
    '''   Starts the enumeration of monitors currently accessible.
    ''' <para>
    '''   Use the method <c>YWakeUpMonitor.nextWakeUpMonitor()</c> to iterate on
    '''   next monitors.
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   a pointer to a <c>YWakeUpMonitor</c> object, corresponding to
    '''   the first monitor currently online, or a <c>Nothing</c> pointer
    '''   if there are none.
    ''' </returns>
    '''/
    Public Shared Function FirstWakeUpMonitor() As YWakeUpMonitor
      Dim v_fundescr(1) As YFUN_DESCR
      Dim dev As YDEV_DESCR
      Dim neededsize, err As Integer
      Dim serial, funcId, funcName, funcVal As String
      Dim errmsg As String = ""
      Dim size As Integer = Marshal.SizeOf(v_fundescr(0))
      Dim p As IntPtr = Marshal.AllocHGlobal(Marshal.SizeOf(v_fundescr(0)))

      err = yapiGetFunctionsByClass("WakeUpMonitor", 0, p, size, neededsize, errmsg)
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
      Return YWakeUpMonitor.FindWakeUpMonitor(serial + "." + funcId)
    End Function

    REM --- (end of YWakeUpMonitor public methods declaration)

  End Class

  REM --- (WakeUpMonitor functions)

  '''*
  ''' <summary>
  '''   Retrieves a monitor for a given identifier.
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
  '''   This function does not require that the monitor is online at the time
  '''   it is invoked. The returned object is nevertheless valid.
  '''   Use the method <c>YWakeUpMonitor.isOnline()</c> to test if the monitor is
  '''   indeed online at a given time. In case of ambiguity when looking for
  '''   a monitor by logical name, no error is notified: the first instance
  '''   found is returned. The search is performed first by hardware name,
  '''   then by logical name.
  ''' </para>
  ''' </summary>
  ''' <param name="func">
  '''   a string that uniquely characterizes the monitor
  ''' </param>
  ''' <returns>
  '''   a <c>YWakeUpMonitor</c> object allowing you to drive the monitor.
  ''' </returns>
  '''/
  Public Function yFindWakeUpMonitor(ByVal func As String) As YWakeUpMonitor
    Return YWakeUpMonitor.FindWakeUpMonitor(func)
  End Function

  '''*
  ''' <summary>
  '''   Starts the enumeration of monitors currently accessible.
  ''' <para>
  '''   Use the method <c>YWakeUpMonitor.nextWakeUpMonitor()</c> to iterate on
  '''   next monitors.
  ''' </para>
  ''' </summary>
  ''' <returns>
  '''   a pointer to a <c>YWakeUpMonitor</c> object, corresponding to
  '''   the first monitor currently online, or a <c>Nothing</c> pointer
  '''   if there are none.
  ''' </returns>
  '''/
  Public Function yFirstWakeUpMonitor() As YWakeUpMonitor
    Return YWakeUpMonitor.FirstWakeUpMonitor()
  End Function


  REM --- (end of WakeUpMonitor functions)

End Module
