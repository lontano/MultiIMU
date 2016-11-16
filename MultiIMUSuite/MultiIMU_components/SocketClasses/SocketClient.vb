Imports System.Net
Imports System.Net.Sockets
Imports System.IO
Imports System.Threading
Imports System.Windows.Forms


Public Class SocketClient
#Region "Private Memebers"
  '--The Actual receive/send
  Private CPiTCPSocket As Socket
  '--Default packet size
  Private nPiPacketSize As Int32 = 16384
  '--LineMode property
  Private nPiLineMode As Boolean = True
  '--Linefeed character denotes end of line
  Private cPieolChar As Char = vbNullChar
  'Per fer la feina, un backgroundworker que s'encarregui de la comunicació
  Private WithEvents CPiBackgroundWorker As System.ComponentModel.BackgroundWorker
  Public ReceivedBytes() As Byte

  Private _connected As Boolean = False
#End Region

#Region "Delegates/Events"
  '--Event definitions
  Public Event Receive(ByVal receiveData As String)
  Public Event ReceiveWithBytes(ByVal receiveData As String, ByVal receivBytes() As Byte)
  Public Event Connected(ByVal connected As Boolean)
  Public Event ConnectedEx(ByVal connected As Boolean, ByVal RemoteEndPoint As EndPoint)
  Public Event Exception(ByVal ex As Exception)
  Public Event ActivityIncoming()
  Public Event ActivityOutgoing()
#End Region

#Region "Public methods"

#Region "Connect overloads"

  Public Sub Connect(ByVal hostNameOrAddress As String, ByVal port As Int32)
    Dim serverAddress As IPAddress
    Try

      serverAddress = Dns.GetHostEntry(hostNameOrAddress).AddressList(0)
      For Each ip As IPAddress In Dns.GetHostEntry(hostNameOrAddress).AddressList
        If ip.AddressFamily = AddressFamily.InterNetwork Then
          serverAddress = ip
          Exit For
        End If
      Next
    Catch ex As Exception
      Throw New Exception("Could not resolve Host name or Address.", ex)
    End Try

    Try
      Me.Connect(serverAddress, port)
    Catch ex As Exception
      Throw ex
    End Try

  End Sub

  Public Sub Connect(ByVal serverAddress As IPAddress, ByVal port As Int32)
    Dim ep As New IPEndPoint(serverAddress, port)
    Try
      Connect(ep)
    Catch ex As Exception
      Throw ex
    End Try
  End Sub

  Private _endPoint As IPEndPoint

  Public Sub Connect(ByVal endPoint As IPEndPoint)
    '--Create a new socket
    _endPoint = endPoint
    CPiTCPSocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)

    Try
      CPiTCPSocket.Connect(_endPoint)
      ' _connected = True
      If IsConnected() = True Then
        RaiseEvent Connected(True)
        RaiseEvent ConnectedEx(True, CPiTCPSocket.RemoteEndPoint)
      End If
    Catch ex As Exception
      Throw New Exception("Count not connect", ex)
    End Try

    Dim bytes(nPiPacketSize - 1) As Byte
    Try
      '-- This tells the socket to wait for received data on a new thread from the system thread pool.
      '  it passes the byte array as the buffer to receive the data and the array adain as a reference that will be passed
      '  back to the ReceiveCallback event
      'CPiTCPSocket.BeginReceive(bytes, 0, bytes.Length, SocketFlags.None, AddressOf ReceiveCallBack, bytes)
      If Me.CPiBackgroundWorker Is Nothing Then
        Me.CPiBackgroundWorker = New System.ComponentModel.BackgroundWorker
        Me.CPiBackgroundWorker.WorkerReportsProgress = True
        Me.CPiBackgroundWorker.WorkerSupportsCancellation = True
      End If
      If Not Me.CPiBackgroundWorker.IsBusy Then
        Me.CPiBackgroundWorker.RunWorkerAsync()
      End If
    Catch ex As Exception
      Throw New Exception("Error receiving data", ex)
      _connected = False
      If IsConnected() = False Then
        RaiseEvent Connected(False)
        RaiseEvent ConnectedEx(False, CPiTCPSocket.RemoteEndPoint)
      End If
    End Try

  End Sub

#End Region

#Region "Disconnect"
  Public Sub Disconnect()
    Try

    Catch ex As Exception

    End Try
  End Sub
#End Region

  '--It will return true only if the socket is connected, it will also check for nothing. Though the socket object
  '  has a connected property, it does bot accurately reflect the state of the socket connection.
  Public Function IsConnected() As Boolean
    If CPiTCPSocket Is Nothing Then
      Return False
    Else
      Return True
    End If
    Exit Function
    'Dim result As Boolean = False
    'result = CPiTCPSocket.Poll(1, SelectMode.SelectRead)
    'result = CPiTCPSocket.Poll(1, SelectMode.SelectWrite)
    ''--Still can be nothing
    'If CPiTCPSocket Is Nothing Then
    '  Return False
    'End If
    'Dim temp As Int32
    'temp = CPiTCPSocket.Available

    ''--In order to successfully test to see if the socket is connected, we AND the results of the Poll methos
    ''  and the avialable property.
    'If result = True Then
    '  Return True
    'ElseIf result = False Then
    '  Return False
    'Else

    '  Return True
    'End If

  End Function

  Public Sub Send(ByVal data As String)
    Dim lTicks As Long = Now.Ticks
    Try
      If IsConnected() = False Then
        RaiseEvent Connected(False)
        RaiseEvent ConnectedEx(False, CPiTCPSocket.RemoteEndPoint)

      Else
        If CPiTCPSocket.Connected Then
          'lTicks = Now.Ticks - lTicks : Debug.Print("IsConnected " & CInt(lTicks / TimeSpan.TicksPerMillisecond)) : lTicks = Now.Ticks
          '--Send the string data and block the thread until all data is sent
          Dim bytes() As Byte = System.Text.Encoding.UTF8.GetBytes(data) ' _ascii.GetBytes(data)
          'CPiTCPSocket.Send(bytes, bytes.Length, SocketFlags.None)
          'lTicks = Now.Ticks - lTicks : Debug.Print("Encoding " & CInt(lTicks / TimeSpan.TicksPerMillisecond)) : lTicks = Now.Ticks

          Dim nDataSent As Integer = 0
          Dim nSize As Integer

          While nDataSent < bytes.Length
            nSize = Math.Min(Me.nPiPacketSize, bytes.Length - nDataSent)
            nDataSent = nDataSent + CPiTCPSocket.Send(bytes, nDataSent, nSize, SocketFlags.None)
            'lTicks = Now.Ticks - lTicks : Debug.Print("send loop step " & CInt(lTicks / TimeSpan.TicksPerMillisecond)) : lTicks = Now.Ticks
            RaiseEvent ActivityOutgoing()
            'Application.DoEvents()
          End While
          'lTicks = Now.Ticks - lTicks : Debug.Print("DataSend " & data & " " & CInt(lTicks / TimeSpan.TicksPerMillisecond)) : lTicks = Now.Ticks
        Else
          Try
            RaiseEvent Connected(False)
            RaiseEvent ConnectedEx(False, CPiTCPSocket.RemoteEndPoint)
            CPiTCPSocket.Disconnect(True)
          Catch ex As Exception
          End Try
          CPiTCPSocket = Nothing
        End If
      End If
    Catch ex As Exception

    End Try

  End Sub

  Public Sub Close()
    If IsConnected() = True Then
      CPiTCPSocket.Shutdown(SocketShutdown.Both)
      CPiTCPSocket.Close()
      CPiTCPSocket = Nothing
      If IsConnected() = False Then
        RaiseEvent Connected(False)
        RaiseEvent ConnectedEx(False, CPiTCPSocket.RemoteEndPoint)
      End If

    End If
  End Sub

#End Region

#Region "Properties"

  Public Property EOLChar() As Char
    Get
      Dim c As Char
      c = cPieolChar

      Return c
    End Get
    Set(ByVal Value As Char)
      cPieolChar = Value

    End Set
  End Property

  Public Property LineMode() As Boolean
    Get
      Dim l As Boolean

      l = nPiLineMode

      Return l
    End Get
    Set(ByVal Value As Boolean)

      nPiLineMode = Value
    End Set
  End Property

  Public Property PacketSize() As Int32
    Get
      Dim pk As Int32
      pk = nPiPacketSize

      Return pk
    End Get
    Set(ByVal Value As Int32)

      nPiPacketSize = Value

    End Set
  End Property

  Public ReadOnly Property SocketHandle() As Long
    Get
      Dim res As Long
      Try
        If Me.CPiTCPSocket Is Nothing Then
          res = 0
        Else
          res = Me.CPiTCPSocket.Handle
        End If
      Catch ex As Exception

      End Try
      Return res
    End Get
  End Property

#End Region

#Region "Background worker"

  Private Sub CPiBackgroundWorker_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles CPiBackgroundWorker.Disposed

  End Sub

  Private Structure tyDataArrival
    Public stringData As String
    Public byteData() As Byte
  End Structure

  Private Sub CPiBackgroundWorker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles CPiBackgroundWorker.DoWork
    Dim totalBytes() As Byte = Nothing
    Dim nIndex As Integer
    Dim tData As New tyDataArrival

    Try
      While Not Me.CPiBackgroundWorker.CancellationPending
        '--Retreive array of bytes
        Try
          If CPiTCPSocket Is Nothing Then
            _connected = False
            Thread.Sleep(100)
            Me.CPiBackgroundWorker.CancelAsync()
          ElseIf CPiTCPSocket.Connected = True Then
            Dim bytes(nPiPacketSize) As Byte
            '--Get number of bytes received and also clean up resources that was used from beginReceive
            Dim numBytes As Int32 = CPiTCPSocket.Receive(bytes, SocketFlags.None)
            '--Did we receive anything?
            If numBytes > 0 Then
              '--Resize the array to match the number of bytes received. Also keep the current data
              ReDim Preserve bytes(numBytes - 1)

              'a saco paco
              If numBytes - 1 = Me.nPiPacketSize Then
                'packet complet!
                If totalBytes Is Nothing Then
                  totalBytes = bytes
                Else
                  nIndex = totalBytes.Length - 1
                  ReDim Preserve totalBytes(nIndex + numBytes)
                  bytes.CopyTo(totalBytes, nIndex)
                End If
              Else
                If totalBytes Is Nothing Then
                  totalBytes = bytes
                Else
                  nIndex = totalBytes.Length - 1
                  ReDim Preserve totalBytes(nIndex + numBytes)
                  bytes.CopyTo(totalBytes, nIndex)
                End If

                Dim received As String = System.Text.Encoding.UTF8.GetString(totalBytes)

                '--Now we need to raise the received event. 
                '  args() is used to pass an argument from this thread to the synchronized container's ui thread.
                Dim args(0) As Object
                tData.byteData = totalBytes

                If nPiLineMode = True Then
                  '--Yes, split the string into an array based on the EOL character
                  Dim sep() As Char = {EOLChar}
                  Dim lines() As String = received.Split(sep)
                  Dim i As Int32
                  '--Raise the received event once for every line of text!
                  For i = 0 To lines.Length - 1
                    If i = lines.Length - 1 Then
                      args(0) = lines(i)
                    Else
                      args(0) = lines(i) & EOLChar
                    End If
                    '--Invoke the private delegate from the thread. 
                    '            _syncObject.Invoke(d, args)
                    'RaiseEvent Receive(args(0))
                    tData.stringData = CStr(args(0))

                    Me.CPiBackgroundWorker.ReportProgress(eSocketProgressState.DataArrial, tData)
                  Next
                Else
                  '--Not line mode. Pass the entire string at once with only one event
                  args(0) = received
                  '--Invoke the private delegate from the thread. 
                  '_syncObject.Invoke(d, args)
                  'RaiseEvent Receive(args(0))
                  tData.stringData = CStr(args(0))

                  Me.CPiBackgroundWorker.ReportProgress(eSocketProgressState.DataArrial, tData)
                End If
                totalBytes = Nothing
              End If
            Else
              'we didn't receve anything... ain't that suspicious?
              If CPiTCPSocket.Connected = False Then
                CPiTCPSocket = Nothing
              End If
              CPiTCPSocket = Nothing
              Dim args() As Object = {False}
              Me.CPiBackgroundWorker.ReportProgress(eSocketProgressState.Disconnected, CStr(args(0)))
              Me.CPiBackgroundWorker.CancelAsync()
            End If
          Else
            'the socket admits it's not connected!
            CPiTCPSocket = Nothing
            Dim args() As Object = {False}
            Me.CPiBackgroundWorker.ReportProgress(eSocketProgressState.Disconnected, CStr(args(0)))
            Me.CPiBackgroundWorker.CancelAsync()
          End If

          '--Are we stil conncted?
          If IsConnected() = False Then
            '--Raise the connect event
            Dim args() As Object = {False}
            'Me.CPiBackgroundWorker.ReportProgress(eSocketProgressState.Disconnected, CStr(args(0)))
          End If
        Catch ex As Exception
          Debug.Print("Viz socket exception: " & Hex(ex.HResult))
          Select Case ex.HResult
            Case 0
            Case Else
              _connected = False
          End Select
        End Try
        Thread.Sleep(1)
      End While
    Catch ex As Exception

    End Try
  End Sub

  Private Enum eSocketProgressState
    DataArrial = 0
    Connected = 1
    Disconnected = 2
  End Enum

  Private Sub CPiBackgroundWorker_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles CPiBackgroundWorker.ProgressChanged
    Try
      'Ha arribat data!
      Select Case e.ProgressPercentage
        Case eSocketProgressState.DataArrial

          Dim tAux As tyDataArrival = CType(e.UserState, tyDataArrival)
          RaiseEvent ActivityIncoming()
          RaiseEvent Receive(tAux.stringData)
          RaiseEvent ReceiveWithBytes(tAux.stringData, tAux.byteData)
        Case eSocketProgressState.Disconnected
          _connected = False
          RaiseEvent Connected(False)
          RaiseEvent ConnectedEx(False, _endPoint)
        Case eSocketProgressState.Connected
          _connected = True
          RaiseEvent Connected(True)
          RaiseEvent ConnectedEx(True, _endPoint)
      End Select
    Catch ex As Exception

    End Try
  End Sub

  Private Sub CPiBackgroundWorker_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles CPiBackgroundWorker.RunWorkerCompleted

  End Sub

  Public Property NoDelay() As Boolean
    Get
      Dim bRes As Boolean = False
      Try
        If Not Me.CPiTCPSocket Is Nothing Then bRes = Me.CPiTCPSocket.NoDelay
      Catch ex As Exception
      End Try
      Return bRes
    End Get
    Set(ByVal value As Boolean)
      Try
        If Not Me.CPiTCPSocket Is Nothing Then Me.CPiTCPSocket.NoDelay = value
      Catch ex As Exception
      End Try
    End Set
  End Property
#End Region
End Class

