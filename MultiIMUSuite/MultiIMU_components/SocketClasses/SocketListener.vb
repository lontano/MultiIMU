
Imports System.Net.Sockets
Imports System.Threading
Imports System.Text
Imports System.ComponentModel

Public Class SocketListener
  Private WithEvents _newClientBackWorker As New BackgroundWorker
  Private _socketBackworkers As New List(Of BackgroundWorker)


  Public serverSocket As System.Net.Sockets.TcpListener
  Public ipAddress As System.Net.IPAddress = System.Net.Dns.Resolve(System.Net.Dns.GetHostName()).AddressList(0)
  Public ipLocalEndPoint As New System.Net.IPEndPoint(ipAddress, 800)

  Public Event Connected(ByRef sender As SocketListener, remoteAddress As String)
  Public Event Disconnected(ByRef sender As SocketListener, remoteAddress As String)
  Public Event RawDataArrival(ByRef sender As SocketListener, message As Byte())
  Public Event CSVLineDataArriaval(ByRef sender As SocketListener, remoteAddress As String, line As String)

  Public Sub Start()
    Dim listenThread As New Thread(New ThreadStart(AddressOf ListenForClients))
    listenThread.Start()
  End Sub

  Public Sub Start(port As Integer)
    Dim ipLocalEndPoint As New System.Net.IPEndPoint(ipAddress, port)
    'Dim listenThread As New Thread(New ThreadStart(AddressOf ListenForClients))
    'listenThread.Start()

    _newClientBackWorker.WorkerReportsProgress = True
    _newClientBackWorker.WorkerSupportsCancellation = True
    _newClientBackWorker.RunWorkerAsync()
  End Sub

#Region "Threads"

  Private Sub ListenForClients()
    serverSocket = New TcpListener(ipLocalEndPoint)
    serverSocket.Start()
    While True  'blocks until a client has connected to the server
      Dim client As TcpClient = Me.serverSocket.AcceptTcpClient()
      Dim clientThread As New Thread(New ParameterizedThreadStart(AddressOf HandleClientComm))

      'Dim tConnected As New Threading.Thread(Sub() RaiseEvent Connected(Me, CType(client.Client.RemoteEndPoint, Net.IPEndPoint).Address.ToString))
      'tConnected.Start()
      'clientThread.Start(client)

      Dim backWorker As New BackgroundWorker
      backWorker.WorkerReportsProgress = True
      backWorker.WorkerSupportsCancellation = True
      backWorker.RunWorkerAsync(client)
    End While
  End Sub

  Private Sub HandleClientComm(ByVal client As Object)
    Dim tcpClient As TcpClient = DirectCast(client, TcpClient)

    Dim clientHost As String = ""
    Try
      clientHost = CType(tcpClient.Client.RemoteEndPoint, Net.IPEndPoint).Address.ToString
      Dim clientStream As NetworkStream = tcpClient.GetStream()

      Dim message As Byte() = New Byte(4095) {}
      Dim bytesRead As Integer

      While True
        bytesRead = 0
        bytesRead = clientStream.Read(message, 0, 4096) 'blocks until a client sends a message

        If bytesRead = 0 Then
          Exit While 'the client has disconnected from the server
        End If

        ''message has successfully been received
        'Dim encoder As New ASCIIEncoding()
        'Dim serverResponse As String = "Response to send"
        'Dim sendBytes As [Byte]() = Encoding.ASCII.GetBytes(serverResponse)
        'clientStream.Write(sendBytes, 0, sendBytes.Length)
        'Debug.Print(Now.ToString & " data received " & bytesRead.ToString)

        Dim t As New Threading.Thread(Sub() RaiseEvent RawDataArrival(Me, message))
        t.Start()

        Dim aux As String = Encoding.ASCII.GetString(message)
        Dim asLines() As String = aux.Split("#")
        For Each line As String In asLines
          If line.Trim.Length > 0 And line.StartsWith(vbNullChar) = False Then
            Dim tLine As New Threading.Thread(Sub() RaiseEvent CSVLineDataArriaval(Me, clientHost, line))
            tLine.Start()
          End If
        Next
        Debug.Print("   " & aux)
      End While

      tcpClient.Close()
    Catch ex As Exception
      tcpClient.Close()
    End Try

    Dim tDisconnected As New Threading.Thread(Sub() RaiseEvent Disconnected(Me, clientHost))
    tDisconnected.Start()
  End Sub

#End Region

#Region "New client backworker"
  Private Sub _newClientBackWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles _newClientBackWorker.DoWork
    Try
      serverSocket = New TcpListener(ipLocalEndPoint)
      serverSocket.Start()
      While True  'blocks until a client has connected to the server
        Dim client As TcpClient = Me.serverSocket.AcceptTcpClient()
        'Dim clientThread As New Thread(New ParameterizedThreadStart(AddressOf HandleClientComm))

        'Dim tConnected As New Threading.Thread(Sub() RaiseEvent Connected(Me, CType(client.Client.RemoteEndPoint, Net.IPEndPoint).Address.ToString))
        'tConnected.Start()
        'clientThread.Start(client)

        _newClientBackWorker.ReportProgress(0, client)

      End While
    Catch ex As Exception
    End Try

  End Sub

  Private Sub _newClientBackWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles _newClientBackWorker.ProgressChanged
    Try
      Dim client As TcpClient = CType(e.UserState, TcpClient)
      Dim backWorker As New BackgroundWorker
      backWorker.WorkerReportsProgress = True
      backWorker.WorkerSupportsCancellation = True

      AddHandler backWorker.DoWork, AddressOf Me.backWorker_DoWork
      AddHandler backWorker.ProgressChanged, AddressOf Me.backWorker_ProgressChanged
      AddHandler backWorker.RunWorkerCompleted, AddressOf Me.backWorker_RunWorkerCompleted

      backWorker.RunWorkerAsync(client)

      _socketBackworkers.Add(backWorker)
      RaiseEvent Connected(Me, CType(client.Client.RemoteEndPoint, Net.IPEndPoint).Address.ToString)
    Catch ex As Exception

    End Try
  End Sub

  Private Sub _newClientBackWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles _newClientBackWorker.RunWorkerCompleted

  End Sub
#End Region

#Region "Client Backworker"
  Private Structure backworker_progress
    Dim senderHost As String
    Dim line As String
  End Structure
  Private Sub backWorker_DoWork(sender As Object, e As DoWorkEventArgs)
    Dim tcpClient As TcpClient = DirectCast(e.Argument, TcpClient)

    Dim clientHost As String = ""
    Try
      clientHost = CType(tcpClient.Client.RemoteEndPoint, Net.IPEndPoint).Address.ToString
      Dim clientStream As NetworkStream = tcpClient.GetStream()

      Dim message As Byte() = New Byte(4095) {}
      Dim bytesRead As Integer

      While True
        bytesRead = 0
        bytesRead = clientStream.Read(message, 0, 4096) 'blocks until a client sends a message

        If bytesRead = 0 Then
          Exit While 'the client has disconnected from the server
        End If

        ''message has successfully been received
        'Dim encoder As New ASCIIEncoding()
        'Dim serverResponse As String = "Response to send"
        'Dim sendBytes As [Byte]() = Encoding.ASCII.GetBytes(serverResponse)
        'clientStream.Write(sendBytes, 0, sendBytes.Length)
        'Debug.Print(Now.ToString & " data received " & bytesRead.ToString)

        'Dim t As New Threading.Thread(Sub() RaiseEvent RawDataArrival(Me, message))
        't.Start()

        Dim aux As String = Encoding.ASCII.GetString(message)
        Dim asLines() As String = aux.Split("#")
        For Each line As String In asLines
          If line.Trim.Length > 0 And line.StartsWith(vbNullChar) = False Then
            'Dim tLine As New Threading.Thread(Sub() RaiseEvent CSVLineDataArriaval(Me, clientHost, line))
            'tLine.Start()
            Dim res As backworker_progress
            res.senderHost = clientHost
            res.line = line
            CType(sender, BackgroundWorker).ReportProgress(0, res)
          End If
        Next
        ' Debug.Print("   " & aux)
      End While

      tcpClient.Close()
    Catch ex As Exception
      tcpClient.Close()
    End Try

    Dim tDisconnected As New Threading.Thread(Sub() RaiseEvent Disconnected(Me, clientHost))
    tDisconnected.Start()
  End Sub

  Private Sub backWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs)
    Dim res As backworker_progress = CType(e.UserState, backworker_progress)
    RaiseEvent CSVLineDataArriaval(Me, res.senderHost, res.line)
  End Sub

  Private Sub backWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
    'CType(sender, BackgroundWorker).
  End Sub

#End Region
End Class
