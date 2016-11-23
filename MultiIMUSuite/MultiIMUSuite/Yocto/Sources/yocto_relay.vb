'*********************************************************************
'*
'* $Id: yocto_relay.vb 25275 2016-08-24 13:42:24Z mvuilleu $
'*
'* Implements yFindRelay(), the high-level API for Relay functions
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

Module yocto_relay

    REM --- (YRelay return codes)
    REM --- (end of YRelay return codes)
    REM --- (YRelay dlldef)
    REM --- (end of YRelay dlldef)
  REM --- (YRelay globals)

Public Class YRelayDelayedPulse
  Public target As Integer = YAPI.INVALID_INT
  Public ms As Integer = YAPI.INVALID_INT
  Public moving As Integer = YAPI.INVALID_UINT
End Class

  REM Y_STATE is defined in yocto_api.vb
  Public Const Y_STATEATPOWERON_UNCHANGED As Integer = 0
  Public Const Y_STATEATPOWERON_A As Integer = 1
  Public Const Y_STATEATPOWERON_B As Integer = 2
  Public Const Y_STATEATPOWERON_INVALID As Integer = -1
  Public Const Y_MAXTIMEONSTATEA_INVALID As Long = YAPI.INVALID_LONG
  Public Const Y_MAXTIMEONSTATEB_INVALID As Long = YAPI.INVALID_LONG
  REM Y_OUTPUT is defined in yocto_api.vb
  Public Const Y_PULSETIMER_INVALID As Long = YAPI.INVALID_LONG
  Public Const Y_COUNTDOWN_INVALID As Long = YAPI.INVALID_LONG
  Public ReadOnly Y_DELAYEDPULSETIMER_INVALID As YRelayDelayedPulse = Nothing
  Public Delegate Sub YRelayValueCallback(ByVal func As YRelay, ByVal value As String)
  Public Delegate Sub YRelayTimedReportCallback(ByVal func As YRelay, ByVal measure As YMeasure)
  REM --- (end of YRelay globals)

  REM --- (YRelay class start)

  '''*
  ''' <summary>
  '''   The Yoctopuce application programming interface allows you to switch the relay state.
  ''' <para>
  '''   This change is not persistent: the relay will automatically return to its idle position
  '''   whenever power is lost or if the module is restarted.
  '''   The library can also generate automatically short pulses of determined duration.
  '''   On devices with two output for each relay (double throw), the two outputs are named A and B,
  '''   with output A corresponding to the idle position (at power off) and the output B corresponding to the
  '''   active state. If you prefer the alternate default state, simply switch your cables on the board.
  ''' </para>
  ''' </summary>
  '''/
  Public Class YRelay
    Inherits YFunction
    REM --- (end of YRelay class start)

    REM --- (YRelay definitions)
    Public Const STATE_A As Integer = 0
    Public Const STATE_B As Integer = 1
    Public Const STATE_INVALID As Integer = -1
    Public Const STATEATPOWERON_UNCHANGED As Integer = 0
    Public Const STATEATPOWERON_A As Integer = 1
    Public Const STATEATPOWERON_B As Integer = 2
    Public Const STATEATPOWERON_INVALID As Integer = -1
    Public Const MAXTIMEONSTATEA_INVALID As Long = YAPI.INVALID_LONG
    Public Const MAXTIMEONSTATEB_INVALID As Long = YAPI.INVALID_LONG
    Public Const OUTPUT_OFF As Integer = 0
    Public Const OUTPUT_ON As Integer = 1
    Public Const OUTPUT_INVALID As Integer = -1
    Public Const PULSETIMER_INVALID As Long = YAPI.INVALID_LONG
    Public ReadOnly DELAYEDPULSETIMER_INVALID As YRelayDelayedPulse = Nothing
    Public Const COUNTDOWN_INVALID As Long = YAPI.INVALID_LONG
    REM --- (end of YRelay definitions)

    REM --- (YRelay attributes declaration)
    Protected _state As Integer
    Protected _stateAtPowerOn As Integer
    Protected _maxTimeOnStateA As Long
    Protected _maxTimeOnStateB As Long
    Protected _output As Integer
    Protected _pulseTimer As Long
    Protected _delayedPulseTimer As YRelayDelayedPulse
    Protected _countdown As Long
    Protected _valueCallbackRelay As YRelayValueCallback
    REM --- (end of YRelay attributes declaration)

    Public Sub New(ByVal func As String)
      MyBase.New(func)
      _classname = "Relay"
      REM --- (YRelay attributes initialization)
      _state = STATE_INVALID
      _stateAtPowerOn = STATEATPOWERON_INVALID
      _maxTimeOnStateA = MAXTIMEONSTATEA_INVALID
      _maxTimeOnStateB = MAXTIMEONSTATEB_INVALID
      _output = OUTPUT_INVALID
      _pulseTimer = PULSETIMER_INVALID
      _delayedPulseTimer = New YRelayDelayedPulse()
      _countdown = COUNTDOWN_INVALID
      _valueCallbackRelay = Nothing
      REM --- (end of YRelay attributes initialization)
    End Sub

    REM --- (YRelay private methods declaration)

    Protected Overrides Function _parseAttr(ByRef member As TJSONRECORD) As Integer
      If (member.name = "state") Then
        If (member.ivalue > 0) Then _state = 1 Else _state = 0
        Return 1
      End If
      If (member.name = "stateAtPowerOn") Then
        _stateAtPowerOn = CInt(member.ivalue)
        Return 1
      End If
      If (member.name = "maxTimeOnStateA") Then
        _maxTimeOnStateA = member.ivalue
        Return 1
      End If
      If (member.name = "maxTimeOnStateB") Then
        _maxTimeOnStateB = member.ivalue
        Return 1
      End If
      If (member.name = "output") Then
        If (member.ivalue > 0) Then _output = 1 Else _output = 0
        Return 1
      End If
      If (member.name = "pulseTimer") Then
        _pulseTimer = member.ivalue
        Return 1
      End If
      If (member.name = "delayedPulseTimer") Then
        If (member.recordtype = TJSONRECORDTYPE.JSON_STRUCT) Then
          Dim submemb As TJSONRECORD
          Dim l As Integer
          For l = 0 To member.membercount - 1
            submemb = member.members(l)
            If (submemb.name = "moving") Then
              _delayedPulseTimer.moving = CInt(submemb.ivalue)
            ElseIf (submemb.name = "target") Then
              _delayedPulseTimer.target = CInt(submemb.ivalue)
            ElseIf (submemb.name = "ms") Then
              _delayedPulseTimer.ms = CInt(submemb.ivalue)
            End If
          Next l
        End If
        Return 1
      End If
      If (member.name = "countdown") Then
        _countdown = member.ivalue
        Return 1
      End If
      Return MyBase._parseAttr(member)
    End Function

    REM --- (end of YRelay private methods declaration)

    REM --- (YRelay public methods declaration)
    '''*
    ''' <summary>
    '''   Returns the state of the relays (A for the idle position, B for the active position).
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   either <c>Y_STATE_A</c> or <c>Y_STATE_B</c>, according to the state of the relays (A for the idle
    '''   position, B for the active position)
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns <c>Y_STATE_INVALID</c>.
    ''' </para>
    '''/
    Public Function get_state() As Integer
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return STATE_INVALID
        End If
      End If
      Return Me._state
    End Function


    '''*
    ''' <summary>
    '''   Changes the state of the relays (A for the idle position, B for the active position).
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="newval">
    '''   either <c>Y_STATE_A</c> or <c>Y_STATE_B</c>, according to the state of the relays (A for the idle
    '''   position, B for the active position)
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
    Public Function set_state(ByVal newval As Integer) As Integer
      Dim rest_val As String
      If (newval > 0) Then rest_val = "1" Else rest_val = "0"
      Return _setAttr("state", rest_val)
    End Function
    '''*
    ''' <summary>
    '''   Returns the state of the relays at device startup (A for the idle position, B for the active position, UNCHANGED for no change).
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   a value among <c>Y_STATEATPOWERON_UNCHANGED</c>, <c>Y_STATEATPOWERON_A</c> and
    '''   <c>Y_STATEATPOWERON_B</c> corresponding to the state of the relays at device startup (A for the
    '''   idle position, B for the active position, UNCHANGED for no change)
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns <c>Y_STATEATPOWERON_INVALID</c>.
    ''' </para>
    '''/
    Public Function get_stateAtPowerOn() As Integer
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return STATEATPOWERON_INVALID
        End If
      End If
      Return Me._stateAtPowerOn
    End Function


    '''*
    ''' <summary>
    '''   Preset the state of the relays at device startup (A for the idle position,
    '''   B for the active position, UNCHANGED for no modification).
    ''' <para>
    '''   Remember to call the matching module <c>saveToFlash()</c>
    '''   method, otherwise this call will have no effect.
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="newval">
    '''   a value among <c>Y_STATEATPOWERON_UNCHANGED</c>, <c>Y_STATEATPOWERON_A</c> and <c>Y_STATEATPOWERON_B</c>
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
    Public Function set_stateAtPowerOn(ByVal newval As Integer) As Integer
      Dim rest_val As String
      rest_val = Ltrim(Str(newval))
      Return _setAttr("stateAtPowerOn", rest_val)
    End Function
    '''*
    ''' <summary>
    '''   Retourne the maximum time (ms) allowed for $THEFUNCTIONS$ to stay in state A before automatically switching back in to B state.
    ''' <para>
    '''   Zero means no maximum time.
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   an integer
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns <c>Y_MAXTIMEONSTATEA_INVALID</c>.
    ''' </para>
    '''/
    Public Function get_maxTimeOnStateA() As Long
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return MAXTIMEONSTATEA_INVALID
        End If
      End If
      Return Me._maxTimeOnStateA
    End Function


    '''*
    ''' <summary>
    '''   Sets the maximum time (ms) allowed for $THEFUNCTIONS$ to stay in state A before automatically switching back in to B state.
    ''' <para>
    '''   Use zero for no maximum time.
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="newval">
    '''   an integer
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
    Public Function set_maxTimeOnStateA(ByVal newval As Long) As Integer
      Dim rest_val As String
      rest_val = Ltrim(Str(newval))
      Return _setAttr("maxTimeOnStateA", rest_val)
    End Function
    '''*
    ''' <summary>
    '''   Retourne the maximum time (ms) allowed for $THEFUNCTIONS$ to stay in state B before automatically switching back in to A state.
    ''' <para>
    '''   Zero means no maximum time.
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   an integer
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns <c>Y_MAXTIMEONSTATEB_INVALID</c>.
    ''' </para>
    '''/
    Public Function get_maxTimeOnStateB() As Long
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return MAXTIMEONSTATEB_INVALID
        End If
      End If
      Return Me._maxTimeOnStateB
    End Function


    '''*
    ''' <summary>
    '''   Sets the maximum time (ms) allowed for $THEFUNCTIONS$ to stay in state B before automatically switching back in to A state.
    ''' <para>
    '''   Use zero for no maximum time.
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="newval">
    '''   an integer
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
    Public Function set_maxTimeOnStateB(ByVal newval As Long) As Integer
      Dim rest_val As String
      rest_val = Ltrim(Str(newval))
      Return _setAttr("maxTimeOnStateB", rest_val)
    End Function
    '''*
    ''' <summary>
    '''   Returns the output state of the relays, when used as a simple switch (single throw).
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   either <c>Y_OUTPUT_OFF</c> or <c>Y_OUTPUT_ON</c>, according to the output state of the relays, when
    '''   used as a simple switch (single throw)
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns <c>Y_OUTPUT_INVALID</c>.
    ''' </para>
    '''/
    Public Function get_output() As Integer
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return OUTPUT_INVALID
        End If
      End If
      Return Me._output
    End Function


    '''*
    ''' <summary>
    '''   Changes the output state of the relays, when used as a simple switch (single throw).
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="newval">
    '''   either <c>Y_OUTPUT_OFF</c> or <c>Y_OUTPUT_ON</c>, according to the output state of the relays, when
    '''   used as a simple switch (single throw)
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
    Public Function set_output(ByVal newval As Integer) As Integer
      Dim rest_val As String
      If (newval > 0) Then rest_val = "1" Else rest_val = "0"
      Return _setAttr("output", rest_val)
    End Function
    '''*
    ''' <summary>
    '''   Returns the number of milliseconds remaining before the relays is returned to idle position
    '''   (state A), during a measured pulse generation.
    ''' <para>
    '''   When there is no ongoing pulse, returns zero.
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   an integer corresponding to the number of milliseconds remaining before the relays is returned to idle position
    '''   (state A), during a measured pulse generation
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns <c>Y_PULSETIMER_INVALID</c>.
    ''' </para>
    '''/
    Public Function get_pulseTimer() As Long
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return PULSETIMER_INVALID
        End If
      End If
      Return Me._pulseTimer
    End Function


    Public Function set_pulseTimer(ByVal newval As Long) As Integer
      Dim rest_val As String
      rest_val = Ltrim(Str(newval))
      Return _setAttr("pulseTimer", rest_val)
    End Function

    '''*
    ''' <summary>
    '''   Sets the relay to output B (active) for a specified duration, then brings it
    '''   automatically back to output A (idle state).
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="ms_duration">
    '''   pulse duration, in millisecondes
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
    Public Function pulse(ByVal ms_duration As Integer) As Integer
      Dim rest_val As String
      rest_val = Ltrim(Str(ms_duration))
      Return _setAttr("pulseTimer", rest_val)
    End Function
    Public Function get_delayedPulseTimer() As YRelayDelayedPulse
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return DELAYEDPULSETIMER_INVALID
        End If
      End If
      Return Me._delayedPulseTimer
    End Function


    Public Function set_delayedPulseTimer(ByVal newval As YRelayDelayedPulse) As Integer
      Dim rest_val As String
      rest_val = Ltrim(Str(newval.target)) + ":" + Ltrim(Str(newval.ms))
      Return _setAttr("delayedPulseTimer", rest_val)
    End Function

    '''*
    ''' <summary>
    '''   Schedules a pulse.
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="ms_delay">
    '''   waiting time before the pulse, in millisecondes
    ''' </param>
    ''' <param name="ms_duration">
    '''   pulse duration, in millisecondes
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
    Public Function delayedPulse(ByVal ms_delay As Integer, ByVal ms_duration As Integer) As Integer
      Dim rest_val As String
      rest_val = Ltrim(Str(ms_delay)) + ":" + Ltrim(Str(ms_duration))
      Return _setAttr("delayedPulseTimer", rest_val)
    End Function
    '''*
    ''' <summary>
    '''   Returns the number of milliseconds remaining before a pulse (delayedPulse() call)
    '''   When there is no scheduled pulse, returns zero.
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   an integer corresponding to the number of milliseconds remaining before a pulse (delayedPulse() call)
    '''   When there is no scheduled pulse, returns zero
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns <c>Y_COUNTDOWN_INVALID</c>.
    ''' </para>
    '''/
    Public Function get_countdown() As Long
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return COUNTDOWN_INVALID
        End If
      End If
      Return Me._countdown
    End Function

    '''*
    ''' <summary>
    '''   Retrieves a relay for a given identifier.
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
    '''   This function does not require that the relay is online at the time
    '''   it is invoked. The returned object is nevertheless valid.
    '''   Use the method <c>YRelay.isOnline()</c> to test if the relay is
    '''   indeed online at a given time. In case of ambiguity when looking for
    '''   a relay by logical name, no error is notified: the first instance
    '''   found is returned. The search is performed first by hardware name,
    '''   then by logical name.
    ''' </para>
    ''' </summary>
    ''' <param name="func">
    '''   a string that uniquely characterizes the relay
    ''' </param>
    ''' <returns>
    '''   a <c>YRelay</c> object allowing you to drive the relay.
    ''' </returns>
    '''/
    Public Shared Function FindRelay(func As String) As YRelay
      Dim obj As YRelay
      obj = CType(YFunction._FindFromCache("Relay", func), YRelay)
      If ((obj Is Nothing)) Then
        obj = New YRelay(func)
        YFunction._AddToCache("Relay", func, obj)
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
    Public Overloads Function registerValueCallback(callback As YRelayValueCallback) As Integer
      Dim val As String
      If (Not (callback Is Nothing)) Then
        YFunction._UpdateValueCallbackList(Me, True)
      Else
        YFunction._UpdateValueCallbackList(Me, False)
      End If
      Me._valueCallbackRelay = callback
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
      If (Not (Me._valueCallbackRelay Is Nothing)) Then
        Me._valueCallbackRelay(Me, value)
      Else
        MyBase._invokeValueCallback(value)
      End If
      Return 0
    End Function


    '''*
    ''' <summary>
    '''   Continues the enumeration of relays started using <c>yFirstRelay()</c>.
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   a pointer to a <c>YRelay</c> object, corresponding to
    '''   a relay currently online, or a <c>Nothing</c> pointer
    '''   if there are no more relays to enumerate.
    ''' </returns>
    '''/
    Public Function nextRelay() As YRelay
      Dim hwid As String = ""
      If (YISERR(_nextFunction(hwid))) Then
        Return Nothing
      End If
      If (hwid = "") Then
        Return Nothing
      End If
      Return YRelay.FindRelay(hwid)
    End Function

    '''*
    ''' <summary>
    '''   Starts the enumeration of relays currently accessible.
    ''' <para>
    '''   Use the method <c>YRelay.nextRelay()</c> to iterate on
    '''   next relays.
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   a pointer to a <c>YRelay</c> object, corresponding to
    '''   the first relay currently online, or a <c>Nothing</c> pointer
    '''   if there are none.
    ''' </returns>
    '''/
    Public Shared Function FirstRelay() As YRelay
      Dim v_fundescr(1) As YFUN_DESCR
      Dim dev As YDEV_DESCR
      Dim neededsize, err As Integer
      Dim serial, funcId, funcName, funcVal As String
      Dim errmsg As String = ""
      Dim size As Integer = Marshal.SizeOf(v_fundescr(0))
      Dim p As IntPtr = Marshal.AllocHGlobal(Marshal.SizeOf(v_fundescr(0)))

      err = yapiGetFunctionsByClass("Relay", 0, p, size, neededsize, errmsg)
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
      Return YRelay.FindRelay(serial + "." + funcId)
    End Function

    REM --- (end of YRelay public methods declaration)

  End Class

  REM --- (Relay functions)

  '''*
  ''' <summary>
  '''   Retrieves a relay for a given identifier.
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
  '''   This function does not require that the relay is online at the time
  '''   it is invoked. The returned object is nevertheless valid.
  '''   Use the method <c>YRelay.isOnline()</c> to test if the relay is
  '''   indeed online at a given time. In case of ambiguity when looking for
  '''   a relay by logical name, no error is notified: the first instance
  '''   found is returned. The search is performed first by hardware name,
  '''   then by logical name.
  ''' </para>
  ''' </summary>
  ''' <param name="func">
  '''   a string that uniquely characterizes the relay
  ''' </param>
  ''' <returns>
  '''   a <c>YRelay</c> object allowing you to drive the relay.
  ''' </returns>
  '''/
  Public Function yFindRelay(ByVal func As String) As YRelay
    Return YRelay.FindRelay(func)
  End Function

  '''*
  ''' <summary>
  '''   Starts the enumeration of relays currently accessible.
  ''' <para>
  '''   Use the method <c>YRelay.nextRelay()</c> to iterate on
  '''   next relays.
  ''' </para>
  ''' </summary>
  ''' <returns>
  '''   a pointer to a <c>YRelay</c> object, corresponding to
  '''   the first relay currently online, or a <c>Nothing</c> pointer
  '''   if there are none.
  ''' </returns>
  '''/
  Public Function yFirstRelay() As YRelay
    Return YRelay.FirstRelay()
  End Function


  REM --- (end of Relay functions)

End Module