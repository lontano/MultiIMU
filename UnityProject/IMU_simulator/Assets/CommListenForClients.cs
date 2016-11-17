using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.ComponentModel;
using System;

using System.Collections.Generic;
using System.Diagnostics;

public class SocketListener : MonoBehaviour 
	{


		private BackgroundWorker withEventsField__newClientBackWorker = new BackgroundWorker();

		private BackgroundWorker _newClientBackWorker {
			get { return withEventsField__newClientBackWorker; }
			set {
			
				if (withEventsField__newClientBackWorker != null) {
					withEventsField__newClientBackWorker.DoWork -= _newClientBackWorker_DoWork;
					withEventsField__newClientBackWorker.ProgressChanged -= _newClientBackWorker_ProgressChanged;
					//withEventsField__newClientBackWorker.RunWorkerCompleted -= _newClientBackWorker_RunWorkerCompleted;
				}
				withEventsField__newClientBackWorker = value;

				if (withEventsField__newClientBackWorker != null) {
					withEventsField__newClientBackWorker.DoWork += _newClientBackWorker_DoWork;
					withEventsField__newClientBackWorker.ProgressChanged += _newClientBackWorker_ProgressChanged;
					//withEventsField__newClientBackWorker.RunWorkerCompleted += _newClientBackWorker_RunWorkerCompleted;
				}
			}
		}

		private List<BackgroundWorker> _socketBackworkers = new List<BackgroundWorker>();

		public System.Net.Sockets.TcpListener serverSocket;
		public System.Net.IPAddress ipAddress; // = System.Net.Dns.Resolve(System.Net.Dns.GetHostName()).AddressList(0);


		public System.Net.IPEndPoint ipLocalEndPoint;
		//public event ConnectedEventHandler Connected;
		public delegate void ConnectedEventHandler(ref SocketListener sender, string remoteAddress);
		//public event DisconnectedEventHandler Disconnected;
		public delegate void DisconnectedEventHandler(ref SocketListener sender, string remoteAddress);
		//public event RawDataArrivalEventHandler RawDataArrival;
		public delegate void RawDataArrivalEventHandler(ref SocketListener sender, byte[] message);
		//public event CSVLineDataArriavalEventHandler CSVLineDataArriaval;
		public delegate void CSVLineDataArriavalEventHandler(ref SocketListener sender, string remoteAddress, string line);

		public void Start()
		{
			String strHostName = string.Empty;
		strHostName = System.Net.Dns.GetHostName();
		System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
		System.Net.IPAddress[] addr = ipEntry.AddressList;
			ipAddress = addr [0];

			ipLocalEndPoint = new System.Net.IPEndPoint(ipAddress, 800);
			this.Start (800);
		}

		public void Start(int port)
		{
			System.Net.IPEndPoint ipLocalEndPoint = new System.Net.IPEndPoint(ipAddress, port);

			_newClientBackWorker.WorkerReportsProgress = true;
			_newClientBackWorker.WorkerSupportsCancellation = true;
			_newClientBackWorker.RunWorkerAsync();
		}



		private void _newClientBackWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			try {
				serverSocket = new TcpListener(ipLocalEndPoint);
				serverSocket.Start();
				//blocks until a client has connected to the server
				while (true) {
					TcpClient client = this.serverSocket.AcceptTcpClient();

					_newClientBackWorker.ReportProgress(0, client);

				}
			} catch (Exception ex) {
				Console.WriteLine (ex.ToString ());
			}

		}

		private void _newClientBackWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			try {
				TcpClient client = (TcpClient)e.UserState;
				BackgroundWorker backWorker = new BackgroundWorker();
				backWorker.WorkerReportsProgress = true;
				backWorker.WorkerSupportsCancellation = true;

				backWorker.DoWork += this.backWorker_DoWork;
				backWorker.ProgressChanged += this.backWorker_ProgressChanged;
				backWorker.RunWorkerCompleted += this.backWorker_RunWorkerCompleted;

				backWorker.RunWorkerAsync(client);

				_socketBackworkers.Add(backWorker);

			} catch (Exception ex) {
				Console.WriteLine (ex.ToString ());
			}
		}

		private struct backworker_progress
		{
			public string senderHost;
			public string line;
		}

		private void backWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			TcpClient tcpClient = (TcpClient)e.Argument;

			string clientHost = "";
			try {
			clientHost = ((System.Net.IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();
				NetworkStream clientStream = tcpClient.GetStream();

				byte[] message = new byte[4096];
				int bytesRead = 0;

				while (true) {
					bytesRead = 0;
					bytesRead = clientStream.Read(message, 0, 4096);
					//blocks until a client sends a message

					if (bytesRead == 0) {
						break; // TODO: might not be correct. Was : Exit While
						//the client has disconnected from the server
					}

					//'message has successfully been received
					//Dim encoder As New ASCIIEncoding()
					//Dim serverResponse As String = "Response to send"
					//Dim sendBytes As [Byte]() = Encoding.ASCII.GetBytes(serverResponse)
					//clientStream.Write(sendBytes, 0, sendBytes.Length)
					//Debug.Print(Now.ToString & " data received " & bytesRead.ToString)

					//Dim t As New Threading.Thread(Sub() RaiseEvent RawDataArrival(Me, message))
					//t.Start()

					string aux = Encoding.ASCII.GetString(message);
				string[] stringSeparators = new string[] {"#"};
				string[] asLines = aux.Split(stringSeparators, StringSplitOptions.None);
					foreach (string line in asLines) {
					if (line.Trim().Length > 0 & line.StartsWith(String.Empty) == false) {
							//Dim tLine As New Threading.Thread(Sub() RaiseEvent CSVLineDataArriaval(Me, clientHost, line))
							//tLine.Start()
							backworker_progress res = default(backworker_progress);
							res.senderHost = clientHost;
							res.line = line;
							((BackgroundWorker)sender).ReportProgress(0, res);
						}
					}
					// Debug.Print("   " & aux)
				}

				tcpClient.Close();
			} catch (Exception ex) {
				tcpClient.Close();
				Console.WriteLine (ex.ToString ());
			}

		}

		private void backWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			backworker_progress res = (backworker_progress)e.UserState;
			
		}

		private void backWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			//CType(sender, BackgroundWorker).
		}

	}

