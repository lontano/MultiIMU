'*********************************************************************
'*
'* $Id: yocto_digitalio.vb 25871 2016-11-15 14:32:56Z seb $
'*
'* Implements yFindDigitalIO(), the high-level API for DigitalIO functions
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

Module yocto_digitalio

    REM --- (YDigitalIO return codes)
    REM --- (end of YDigitalIO return codes)
    REM --- (YDigitalIO dlldef)
    REM --- (end of YDigitalIO dlldef)
  REM --- (YDigitalIO globals)

  Public Const Y_PORTSTATE_INVALID As Integer = YAPI.INVALID_UINT
  Public Const Y_PORTDIRECTION_INVALID As Integer = YAPI.INVALID_UINT
  Public Const Y_PORTOPENDRAIN_INVALID As Integer = YAPI.INVALID_UINT
  Public Const Y_PORTPOLARITY_INVALID As Integer = YAPI.INVALID_UINT
  Public Const Y_PORTSIZE_INVALID As Integer = YAPI.INVALID_UINT
  Public Const Y_OUTPUTVOLTAGE_USB_5V As Integer = 0
  Public Const Y_OUTPUTVOLTAGE_USB_3V As Integer = 1
  Public Const Y_OUTPUTVOLTAGE_EXT_V As Integer = 2
  Public Const Y_OUTPUTVOLTAGE_INVALID As Integer = -1
  Public Const Y_COMMAND_INVALID As String = YAPI.INVALID_STRING
  Public Delegate Sub YDigitalIOValueCallback(ByVal func As YDigitalIO, ByVal value As String)
  Public Delegate Sub YDigitalIOTimedReportCallback(ByVal func As YDigitalIO, ByVal measure As YMeasure)
  REM --- (end of YDigitalIO globals)

  REM --- (YDigitalIO class start)

  '''*
  ''' <summary>
  '''   The Yoctopuce application programming interface allows you to switch the state of each
  '''   bit of the I/O port.
  ''' <para>
  '''   You can switch all bits at once, or one by one. The library
  '''   can also automatically generate short pulses of a determined duration. Electrical behavior
  '''   of each I/O can be modified (open drain and reverse polarity).
  ''' </para>
  ''' </summary>
  '''/
  Public Class YDigitalIO
    Inherits YFunction
    REM --- (end of YDigitalIO class start)

    REM --- (YDigitalIO definitions)
    Public Const PORTSTATE_INVALID As Integer = YAPI.INVALID_UINT
    Public Const PORTDIRECTION_INVALID As Integer = YAPI.INVALID_UINT
    Public Const PORTOPENDRAIN_INVALID As Integer = YAPI.INVALID_UINT
    Public Const PORTPOLARITY_INVALID As Integer = YAPI.INVALID_UINT
    Public Const PORTSIZE_INVALID As Integer = YAPI.INVALID_UINT
    Public Const OUTPUTVOLTAGE_USB_5V As Integer = 0
    Public Const OUTPUTVOLTAGE_USB_3V As Integer = 1
    Public Const OUTPUTVOLTAGE_EXT_V As Integer = 2
    Public Const OUTPUTVOLTAGE_INVALID As Integer = -1
    Public Const COMMAND_INVALID As String = YAPI.INVALID_STRING
    REM --- (end of YDigitalIO definitions)

    REM --- (YDigitalIO attributes declaration)
    Protected _portState As Integer
    Protected _portDirection As Integer
    Protected _portOpenDrain As Integer
    Protected _portPolarity As Integer
    Protected _portSize As Integer
    Protected _outputVoltage As Integer
    Protected _command As String
    Protected _valueCallbackDigitalIO As YDigitalIOValueCallback
    REM --- (end of YDigitalIO attributes declaration)

    Public Sub New(ByVal func As String)
      MyBase.New(func)
      _classname = "DigitalIO"
      REM --- (YDigitalIO attributes initialization)
      _portState = PORTSTATE_INVALID
      _portDirection = PORTDIRECTION_INVALID
      _portOpenDrain = PORTOPENDRAIN_INVALID
      _portPolarity = PORTPOLARITY_INVALID
      _portSize = PORTSIZE_INVALID
      _outputVoltage = OUTPUTVOLTAGE_INVALID
      _command = COMMAND_INVALID
      _valueCallbackDigitalIO = Nothing
      REM --- (end of YDigitalIO attributes initialization)
    End Sub

    REM --- (YDigitalIO private methods declaration)

    Protected Overrides Function _parseAttr(ByRef member As TJSONRECORD) As Integer
      If (member.name = "portState") Then
        _portState = CInt(member.ivalue)
        Return 1
      End If
      If (member.name = "portDirection") Then
        _portDirection = CInt(member.ivalue)
        Return 1
      End If
      If (member.name = "portOpenDrain") Then
        _portOpenDrain = CInt(member.ivalue)
        Return 1
      End If
      If (member.name = "portPolarity") Then
        _portPolarity = CInt(member.ivalue)
        Return 1
      End If
      If (member.name = "portSize") Then
        _portSize = CInt(member.ivalue)
        Return 1
      End If
      If (member.name = "outputVoltage") Then
        _outputVoltage = CInt(member.ivalue)
        Return 1
      End If
      If (member.name = "command") Then
        _command = member.svalue
        Return 1
      End If
      Return MyBase._parseAttr(member)
    End Function

    REM --- (end of YDigitalIO private methods declaration)

    REM --- (YDigitalIO public methods declaration)
    '''*
    ''' <summary>
    '''   Returns the digital IO port state: bit 0 represents input 0, and so on.
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   an integer corresponding to the digital IO port state: bit 0 represents input 0, and so on
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns <c>Y_PORTSTATE_INVALID</c>.
    ''' </para>
    '''/
    Public Function get_portState() As Integer
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return PORTSTATE_INVALID
        End If
      End If
      Return Me._portState
    End Function


    '''*
    ''' <summary>
    '''   Changes the digital IO port state: bit 0 represents input 0, and so on.
    ''' <para>
    '''   This function has no effect
    '''   on bits configured as input in <c>portDirection</c>.
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="newval">
    '''   an integer corresponding to the digital IO port state: bit 0 represents input 0, and so on
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
    Public Function set_portState(ByVal newval As Integer) As Integer
      Dim rest_val As String
      rest_val = Ltrim(Str(newval))
      Return _setAttr("portState", rest_val)
    End Function
    '''*
    ''' <summary>
    '''   Returns the IO direction of all bits of the port: 0 makes a bit an input, 1 makes it an output.
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   an integer corresponding to the IO direction of all bits of the port: 0 makes a bit an input, 1
    '''   makes it an output
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns <c>Y_PORTDIRECTION_INVALID</c>.
    ''' </para>
    '''/
    Public Function get_portDirection() As Integer
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return PORTDIRECTION_INVALID
        End If
      End If
      Return Me._portDirection
    End Function


    '''*
    ''' <summary>
    '''   Changes the IO direction of all bits of the port: 0 makes a bit an input, 1 makes it an output.
    ''' <para>
    '''   Remember to call the <c>saveToFlash()</c> method  to make sure the setting is kept after a reboot.
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="newval">
    '''   an integer corresponding to the IO direction of all bits of the port: 0 makes a bit an input, 1
    '''   makes it an output
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
    Public Function set_portDirection(ByVal newval As Integer) As Integer
      Dim rest_val As String
      rest_val = Ltrim(Str(newval))
      Return _setAttr("portDirection", rest_val)
    End Function
    '''*
    ''' <summary>
    '''   Returns the electrical interface for each bit of the port.
    ''' <para>
    '''   For each bit set to 0  the matching I/O works in the regular,
    '''   intuitive way, for each bit set to 1, the I/O works in reverse mode.
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   an integer corresponding to the electrical interface for each bit of the port
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns <c>Y_PORTOPENDRAIN_INVALID</c>.
    ''' </para>
    '''/
    Public Function get_portOpenDrain() As Integer
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return PORTOPENDRAIN_INVALID
        End If
      End If
      Return Me._portOpenDrain
    End Function


    '''*
    ''' <summary>
    '''   Changes the electrical interface for each bit of the port.
    ''' <para>
    '''   0 makes a bit a regular input/output, 1 makes
    '''   it an open-drain (open-collector) input/output. Remember to call the
    '''   <c>saveToFlash()</c> method  to make sure the setting is kept after a reboot.
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="newval">
    '''   an integer corresponding to the electrical interface for each bit of the port
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
    Public Function set_portOpenDrain(ByVal newval As Integer) As Integer
      Dim rest_val As String
      rest_val = Ltrim(Str(newval))
      Return _setAttr("portOpenDrain", rest_val)
    End Function
    '''*
    ''' <summary>
    '''   Returns the polarity of all the bits of the port.
    ''' <para>
    '''   For each bit set to 0, the matching I/O works the regular,
    '''   intuitive way; for each bit set to 1, the I/O works in reverse mode.
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   an integer corresponding to the polarity of all the bits of the port
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns <c>Y_PORTPOLARITY_INVALID</c>.
    ''' </para>
    '''/
    Public Function get_portPolarity() As Integer
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return PORTPOLARITY_INVALID
        End If
      End If
      Return Me._portPolarity
    End Function


    '''*
    ''' <summary>
    '''   Changes the polarity of all the bits of the port: For each bit set to 0, the matching I/O works the regular,
    '''   intuitive way; for each bit set to 1, the I/O works in reverse mode.
    ''' <para>
    '''   Remember to call the <c>saveToFlash()</c> method  to make sure the setting will be kept after a reboot.
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="newval">
    '''   an integer corresponding to the polarity of all the bits of the port: For each bit set to 0, the
    '''   matching I/O works the regular,
    '''   intuitive way; for each bit set to 1, the I/O works in reverse mode
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
    Public Function set_portPolarity(ByVal newval As Integer) As Integer
      Dim rest_val As String
      rest_val = Ltrim(Str(newval))
      Return _setAttr("portPolarity", rest_val)
    End Function
    '''*
    ''' <summary>
    '''   Returns the number of bits implemented in the I/O port.
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   an integer corresponding to the number of bits implemented in the I/O port
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns <c>Y_PORTSIZE_INVALID</c>.
    ''' </para>
    '''/
    Public Function get_portSize() As Integer
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return PORTSIZE_INVALID
        End If
      End If
      Return Me._portSize
    End Function

    '''*
    ''' <summary>
    '''   Returns the voltage source used to drive output bits.
    ''' <para>
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   a value among <c>Y_OUTPUTVOLTAGE_USB_5V</c>, <c>Y_OUTPUTVOLTAGE_USB_3V</c> and
    '''   <c>Y_OUTPUTVOLTAGE_EXT_V</c> corresponding to the voltage source used to drive output bits
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns <c>Y_OUTPUTVOLTAGE_INVALID</c>.
    ''' </para>
    '''/
    Public Function get_outputVoltage() As Integer
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return OUTPUTVOLTAGE_INVALID
        End If
      End If
      Return Me._outputVoltage
    End Function


    '''*
    ''' <summary>
    '''   Changes the voltage source used to drive output bits.
    ''' <para>
    '''   Remember to call the <c>saveToFlash()</c> method  to make sure the setting is kept after a reboot.
    ''' </para>
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="newval">
    '''   a value among <c>Y_OUTPUTVOLTAGE_USB_5V</c>, <c>Y_OUTPUTVOLTAGE_USB_3V</c> and
    '''   <c>Y_OUTPUTVOLTAGE_EXT_V</c> corresponding to the voltage source used to drive output bits
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
    Public Function set_outputVoltage(ByVal newval As Integer) As Integer
      Dim rest_val As String
      rest_val = Ltrim(Str(newval))
      Return _setAttr("outputVoltage", rest_val)
    End Function
    Public Function get_command() As String
      If (Me._cacheExpiration <= YAPI.GetTickCount()) Then
        If (Me.load(YAPI.DefaultCacheValidity) <> YAPI.SUCCESS) Then
          Return COMMAND_INVALID
        End If
      End If
      Return Me._command
    End Function


    Public Function set_command(ByVal newval As String) As Integer
      Dim rest_val As String
      rest_val = newval
      Return _setAttr("command", rest_val)
    End Function
    '''*
    ''' <summary>
    '''   Retrieves a digital IO port for a given identifier.
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
    '''   This function does not require that the digital IO port is online at the time
    '''   it is invoked. The returned object is nevertheless valid.
    '''   Use the method <c>YDigitalIO.isOnline()</c> to test if the digital IO port is
    '''   indeed online at a given time. In case of ambiguity when looking for
    '''   a digital IO port by logical name, no error is notified: the first instance
    '''   found is returned. The search is performed first by hardware name,
    '''   then by logical name.
    ''' </para>
    ''' </summary>
    ''' <param name="func">
    '''   a string that uniquely characterizes the digital IO port
    ''' </param>
    ''' <returns>
    '''   a <c>YDigitalIO</c> object allowing you to drive the digital IO port.
    ''' </returns>
    '''/
    Public Shared Function FindDigitalIO(func As String) As YDigitalIO
      Dim obj As YDigitalIO
      obj = CType(YFunction._FindFromCache("DigitalIO", func), YDigitalIO)
      If ((obj Is Nothing)) Then
        obj = New YDigitalIO(func)
        YFunction._AddToCache("DigitalIO", func, obj)
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
    Public Overloads Function registerValueCallback(callback As YDigitalIOValueCallback) As Integer
      Dim val As String
      If (Not (callback Is Nothing)) Then
        YFunction._UpdateValueCallbackList(Me, True)
      Else
        YFunction._UpdateValueCallbackList(Me, False)
      End If
      Me._valueCallbackDigitalIO = callback
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
      If (Not (Me._valueCallbackDigitalIO Is Nothing)) Then
        Me._valueCallbackDigitalIO(Me, value)
      Else
        MyBase._invokeValueCallback(value)
      End If
      Return 0
    End Function

    '''*
    ''' <summary>
    '''   Sets a single bit of the I/O port.
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="bitno">
    '''   the bit number; lowest bit has index 0
    ''' </param>
    ''' <param name="bitstate">
    '''   the state of the bit (1 or 0)
    ''' </param>
    ''' <returns>
    '''   <c>YAPI_SUCCESS</c> if the call succeeds.
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns a negative error code.
    ''' </para>
    '''/
    Public Overridable Function set_bitState(bitno As Integer, bitstate As Integer) As Integer
      If Not(bitstate >= 0) Then
        me._throw( YAPI.INVALID_ARGUMENT,  "invalid bitstate")
        return YAPI.INVALID_ARGUMENT
      end if
      If Not(bitstate <= 1) Then
        me._throw( YAPI.INVALID_ARGUMENT,  "invalid bitstate")
        return YAPI.INVALID_ARGUMENT
      end if
      Return Me.set_command("" + Chr(82+bitstate) + "" + Convert.ToString(bitno))
    End Function

    '''*
    ''' <summary>
    '''   Returns the state of a single bit of the I/O port.
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="bitno">
    '''   the bit number; lowest bit has index 0
    ''' </param>
    ''' <returns>
    '''   the bit state (0 or 1)
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns a negative error code.
    ''' </para>
    '''/
    Public Overridable Function get_bitState(bitno As Integer) As Integer
      Dim portVal As Integer = 0
      portVal = Me.get_portState()
      Return ((((portVal) >> (bitno))) And (1))
    End Function

    '''*
    ''' <summary>
    '''   Reverts a single bit of the I/O port.
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="bitno">
    '''   the bit number; lowest bit has index 0
    ''' </param>
    ''' <returns>
    '''   <c>YAPI_SUCCESS</c> if the call succeeds.
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns a negative error code.
    ''' </para>
    '''/
    Public Overridable Function toggle_bitState(bitno As Integer) As Integer
      Return Me.set_command("T" + Convert.ToString(bitno))
    End Function

    '''*
    ''' <summary>
    '''   Changes  the direction of a single bit from the I/O port.
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="bitno">
    '''   the bit number; lowest bit has index 0
    ''' </param>
    ''' <param name="bitdirection">
    '''   direction to set, 0 makes the bit an input, 1 makes it an output.
    '''   Remember to call the   <c>saveToFlash()</c> method to make sure the setting is kept after a reboot.
    ''' </param>
    ''' <returns>
    '''   <c>YAPI_SUCCESS</c> if the call succeeds.
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns a negative error code.
    ''' </para>
    '''/
    Public Overridable Function set_bitDirection(bitno As Integer, bitdirection As Integer) As Integer
      If Not(bitdirection >= 0) Then
        me._throw( YAPI.INVALID_ARGUMENT,  "invalid direction")
        return YAPI.INVALID_ARGUMENT
      end if
      If Not(bitdirection <= 1) Then
        me._throw( YAPI.INVALID_ARGUMENT,  "invalid direction")
        return YAPI.INVALID_ARGUMENT
      end if
      Return Me.set_command("" + Chr(73+6*bitdirection) + "" + Convert.ToString(bitno))
    End Function

    '''*
    ''' <summary>
    '''   Returns the direction of a single bit from the I/O port (0 means the bit is an input, 1  an output).
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="bitno">
    '''   the bit number; lowest bit has index 0
    ''' </param>
    ''' <returns>
    '''   <c>YAPI_SUCCESS</c> if the call succeeds.
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns a negative error code.
    ''' </para>
    '''/
    Public Overridable Function get_bitDirection(bitno As Integer) As Integer
      Dim portDir As Integer = 0
      portDir = Me.get_portDirection()
      Return ((((portDir) >> (bitno))) And (1))
    End Function

    '''*
    ''' <summary>
    '''   Changes the polarity of a single bit from the I/O port.
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="bitno">
    '''   the bit number; lowest bit has index 0.
    ''' </param>
    ''' <param name="bitpolarity">
    '''   polarity to set, 0 makes the I/O work in regular mode, 1 makes the I/O  works in reverse mode.
    '''   Remember to call the   <c>saveToFlash()</c> method to make sure the setting is kept after a reboot.
    ''' </param>
    ''' <returns>
    '''   <c>YAPI_SUCCESS</c> if the call succeeds.
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns a negative error code.
    ''' </para>
    '''/
    Public Overridable Function set_bitPolarity(bitno As Integer, bitpolarity As Integer) As Integer
      If Not(bitpolarity >= 0) Then
        me._throw( YAPI.INVALID_ARGUMENT,  "invalid bitpolarity")
        return YAPI.INVALID_ARGUMENT
      end if
      If Not(bitpolarity <= 1) Then
        me._throw( YAPI.INVALID_ARGUMENT,  "invalid bitpolarity")
        return YAPI.INVALID_ARGUMENT
      end if
      Return Me.set_command("" + Chr(110+4*bitpolarity) + "" + Convert.ToString(bitno))
    End Function

    '''*
    ''' <summary>
    '''   Returns the polarity of a single bit from the I/O port (0 means the I/O works in regular mode, 1 means the I/O  works in reverse mode).
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="bitno">
    '''   the bit number; lowest bit has index 0
    ''' </param>
    ''' <returns>
    '''   <c>YAPI_SUCCESS</c> if the call succeeds.
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns a negative error code.
    ''' </para>
    '''/
    Public Overridable Function get_bitPolarity(bitno As Integer) As Integer
      Dim portPol As Integer = 0
      portPol = Me.get_portPolarity()
      Return ((((portPol) >> (bitno))) And (1))
    End Function

    '''*
    ''' <summary>
    '''   Changes  the electrical interface of a single bit from the I/O port.
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <param name="bitno">
    '''   the bit number; lowest bit has index 0
    ''' </param>
    ''' <param name="opendrain">
    '''   0 makes a bit a regular input/output, 1 makes
    '''   it an open-drain (open-collector) input/output. Remember to call the
    '''   <c>saveToFlash()</c> method to make sure the setting is kept after a reboot.
    ''' </param>
    ''' <returns>
    '''   <c>YAPI_SUCCESS</c> if the call succeeds.
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns a negative error code.
    ''' </para>
    '''/
    Public Overridable Function set_bitOpenDrain(bitno As Integer, opendrain As Integer) As Integer
      If Not(opendrain >= 0) Then
        me._throw( YAPI.INVALID_ARGUMENT,  "invalid state")
        return YAPI.INVALID_ARGUMENT
      end if
      If Not(opendrain <= 1) Then
        me._throw( YAPI.INVALID_ARGUMENT,  "invalid state")
        return YAPI.INVALID_ARGUMENT
      end if
      Return Me.set_command("" + Chr(100-32*opendrain) + "" + Convert.ToString(bitno))
    End Function

    '''*
    ''' <summary>
    '''   Returns the type of electrical interface of a single bit from the I/O port.
    ''' <para>
    '''   (0 means the bit is an input, 1  an output).
    ''' </para>
    ''' </summary>
    ''' <param name="bitno">
    '''   the bit number; lowest bit has index 0
    ''' </param>
    ''' <returns>
    '''   0 means the a bit is a regular input/output, 1 means the bit is an open-drain
    '''   (open-collector) input/output.
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns a negative error code.
    ''' </para>
    '''/
    Public Overridable Function get_bitOpenDrain(bitno As Integer) As Integer
      Dim portOpenDrain As Integer = 0
      portOpenDrain = Me.get_portOpenDrain()
      Return ((((portOpenDrain) >> (bitno))) And (1))
    End Function

    '''*
    ''' <summary>
    '''   Triggers a pulse on a single bit for a specified duration.
    ''' <para>
    '''   The specified bit
    '''   will be turned to 1, and then back to 0 after the given duration.
    ''' </para>
    ''' </summary>
    ''' <param name="bitno">
    '''   the bit number; lowest bit has index 0
    ''' </param>
    ''' <param name="ms_duration">
    '''   desired pulse duration in milliseconds. Be aware that the device time
    '''   resolution is not guaranteed up to the millisecond.
    ''' </param>
    ''' <returns>
    '''   <c>YAPI_SUCCESS</c> if the call succeeds.
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns a negative error code.
    ''' </para>
    '''/
    Public Overridable Function pulse(bitno As Integer, ms_duration As Integer) As Integer
      Return Me.set_command("Z" + Convert.ToString( bitno) + ",0," + Convert.ToString(ms_duration))
    End Function

    '''*
    ''' <summary>
    '''   Schedules a pulse on a single bit for a specified duration.
    ''' <para>
    '''   The specified bit
    '''   will be turned to 1, and then back to 0 after the given duration.
    ''' </para>
    ''' </summary>
    ''' <param name="bitno">
    '''   the bit number; lowest bit has index 0
    ''' </param>
    ''' <param name="ms_delay">
    '''   waiting time before the pulse, in milliseconds
    ''' </param>
    ''' <param name="ms_duration">
    '''   desired pulse duration in milliseconds. Be aware that the device time
    '''   resolution is not guaranteed up to the millisecond.
    ''' </param>
    ''' <returns>
    '''   <c>YAPI_SUCCESS</c> if the call succeeds.
    ''' </returns>
    ''' <para>
    '''   On failure, throws an exception or returns a negative error code.
    ''' </para>
    '''/
    Public Overridable Function delayedPulse(bitno As Integer, ms_delay As Integer, ms_duration As Integer) As Integer
      Return Me.set_command("Z" + Convert.ToString(bitno) + "," + Convert.ToString(ms_delay) + "," + Convert.ToString(ms_duration))
    End Function


    '''*
    ''' <summary>
    '''   Continues the enumeration of digital IO ports started using <c>yFirstDigitalIO()</c>.
    ''' <para>
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   a pointer to a <c>YDigitalIO</c> object, corresponding to
    '''   a digital IO port currently online, or a <c>Nothing</c> pointer
    '''   if there are no more digital IO ports to enumerate.
    ''' </returns>
    '''/
    Public Function nextDigitalIO() As YDigitalIO
      Dim hwid As String = ""
      If (YISERR(_nextFunction(hwid))) Then
        Return Nothing
      End If
      If (hwid = "") Then
        Return Nothing
      End If
      Return YDigitalIO.FindDigitalIO(hwid)
    End Function

    '''*
    ''' <summary>
    '''   Starts the enumeration of digital IO ports currently accessible.
    ''' <para>
    '''   Use the method <c>YDigitalIO.nextDigitalIO()</c> to iterate on
    '''   next digital IO ports.
    ''' </para>
    ''' </summary>
    ''' <returns>
    '''   a pointer to a <c>YDigitalIO</c> object, corresponding to
    '''   the first digital IO port currently online, or a <c>Nothing</c> pointer
    '''   if there are none.
    ''' </returns>
    '''/
    Public Shared Function FirstDigitalIO() As YDigitalIO
      Dim v_fundescr(1) As YFUN_DESCR
      Dim dev As YDEV_DESCR
      Dim neededsize, err As Integer
      Dim serial, funcId, funcName, funcVal As String
      Dim errmsg As String = ""
      Dim size As Integer = Marshal.SizeOf(v_fundescr(0))
      Dim p As IntPtr = Marshal.AllocHGlobal(Marshal.SizeOf(v_fundescr(0)))

      err = yapiGetFunctionsByClass("DigitalIO", 0, p, size, neededsize, errmsg)
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
      Return YDigitalIO.FindDigitalIO(serial + "." + funcId)
    End Function

    REM --- (end of YDigitalIO public methods declaration)

  End Class

  REM --- (DigitalIO functions)

  '''*
  ''' <summary>
  '''   Retrieves a digital IO port for a given identifier.
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
  '''   This function does not require that the digital IO port is online at the time
  '''   it is invoked. The returned object is nevertheless valid.
  '''   Use the method <c>YDigitalIO.isOnline()</c> to test if the digital IO port is
  '''   indeed online at a given time. In case of ambiguity when looking for
  '''   a digital IO port by logical name, no error is notified: the first instance
  '''   found is returned. The search is performed first by hardware name,
  '''   then by logical name.
  ''' </para>
  ''' </summary>
  ''' <param name="func">
  '''   a string that uniquely characterizes the digital IO port
  ''' </param>
  ''' <returns>
  '''   a <c>YDigitalIO</c> object allowing you to drive the digital IO port.
  ''' </returns>
  '''/
  Public Function yFindDigitalIO(ByVal func As String) As YDigitalIO
    Return YDigitalIO.FindDigitalIO(func)
  End Function

  '''*
  ''' <summary>
  '''   Starts the enumeration of digital IO ports currently accessible.
  ''' <para>
  '''   Use the method <c>YDigitalIO.nextDigitalIO()</c> to iterate on
  '''   next digital IO ports.
  ''' </para>
  ''' </summary>
  ''' <returns>
  '''   a pointer to a <c>YDigitalIO</c> object, corresponding to
  '''   the first digital IO port currently online, or a <c>Nothing</c> pointer
  '''   if there are none.
  ''' </returns>
  '''/
  Public Function yFirstDigitalIO() As YDigitalIO
    Return YDigitalIO.FirstDigitalIO()
  End Function


  REM --- (end of DigitalIO functions)

End Module
