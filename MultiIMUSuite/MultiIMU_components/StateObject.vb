
Imports System.Net.Sockets
Imports System.Text


' State object for receiving data from remote device / reading client data asynchronously

Public Class StateObject
  ' Client socket.
  Public workSocket As Socket = Nothing
  ' Size of receive buffer.
  Public Const BufferSize As Integer = 256
  ' Receive buffer.
  Public buffer(BufferSize) As Byte
  ' Received data string.
  Public sb As New StringBuilder
End Class 'StateObject