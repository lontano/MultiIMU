using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.ComponentModel;
using System;
using System.Net;
using UnityEngine.UI;

using System.Collections.Generic;
using System.Diagnostics;

public class MySocket : MonoBehaviour {
	public int Port = 800;
	public int maxConnections = 10;
	public Text text;

	private String error_text = "";

	private List<BackgroundWorker> _socketBackworkers = new List<BackgroundWorker>();

	System.Net.Sockets.TcpListener _serverSocket;
	public System.Net.IPEndPoint _ipLocalEndPoint;
	private System.Net.IPAddress _ipAddress; 
	private System.Net.IPAddress[] _addresses;

	private struct backworker_progress
	{
		public string senderHost;
		public string line;
	}

	// Use this for initialization
	void Start () {
		String strHostName = string.Empty;
		strHostName = System.Net.Dns.GetHostName();
		System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
		_addresses = ipEntry.AddressList;
		_ipAddress = _addresses [2];

		_ipLocalEndPoint = new System.Net.IPEndPoint(_ipAddress, 800);
		this.Start (800);
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "COMM\n\r" + _ipAddress.ToString() + "\n\r\n\r"; 

		text.text = text.text + error_text;
	}

	void Start (int port) {
		try {
			
			System.Net.IPEndPoint ipLocalEndPoint = new System.Net.IPEndPoint(_ipAddress, port);

			//_newClientBackWorker.WorkerReportsProgress = true;
			//_newClientBackWorker.WorkerSupportsCancellation = true;
			//_newClientBackWorker.RunWorkerAsync();

			_serverSocket = new TcpListener(ipLocalEndPoint);

			_serverSocket.Start();
			_serverSocket.BeginAcceptTcpClient(Accept, _serverSocket);
		} catch (Exception ex) {
			Console.WriteLine (ex.ToString ());
			error_text = ex.ToString ();
		}
	}


	private void Accept(IAsyncResult ar)
	{
		try {
			
			System.Net.Sockets.TcpListener server = (TcpListener)ar.AsyncState;
			TcpClient client = server.EndAcceptTcpClient(ar);
			Receive (client.Client);
			/*
			BackgroundWorker backWorker = new BackgroundWorker();
			backWorker.WorkerReportsProgress = true;
			backWorker.WorkerSupportsCancellation = true;

			backWorker.DoWork += this.backWorker_DoWork;
			backWorker.ProgressChanged += this.backWorker_ProgressChanged;
			backWorker.RunWorkerCompleted += this.backWorker_RunWorkerCompleted;

			backWorker.RunWorkerAsync(client);
			_socketBackworkers.Add(backWorker);*/
		} catch (Exception ex) {
			Console.WriteLine (ex.ToString ());
			error_text = ex.ToString ();
		}
	}


	private static void Receive(Socket client) {
	/*
		try {
			// Create the state object.
			StateObject state = new StateObject();
			state.workSocket = client;

			// Begin receiving the data from the remote device.
			client.BeginReceive( state.buffer, 0, StateObject.BufferSize, 0,
				new AsyncCallback(ReceiveCallback), state);
		} catch (Exception e) {
			Console.WriteLine(e.ToString());
		}*/
	}

	private static void ReceiveCallback( IAsyncResult ar ) {
		try {/*
			// Retrieve the state object and the client socket 
			// from the asynchronous state object.
			StateObject state = (StateObject) ar.AsyncState;
			Socket client = state.workSocket;
			// Read data from the remote device.
			int bytesRead = client.EndReceive(ar);
			if (bytesRead > 0) {
				// There might be more data, so store the data received so far.
				state.sb.Append(Encoding.ASCII.GetString(state.buffer,0,bytesRead));
				//  Get the rest of the data.
				client.BeginReceive(state.buffer,0,StateObject.BufferSize,0,
					new AsyncCallback(ReceiveCallback), state);
			} else {
				// All the data has arrived; put it in response.
				if (state.sb.Length > 1) {
					response = state.sb.ToString();
				}
				// Signal that all bytes have been received.
				receiveDone.Set();
			}*/
		} catch (Exception e) {
			Console.WriteLine(e.ToString());
		}
	}


	private void _newClientBackWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
		try {
			Console.WriteLine ("Done");
		} catch (Exception ex) {
			Console.WriteLine (ex.ToString ());
		}	
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
				string aux = Encoding.ASCII.GetString(message);
				string[] stringSeparators = new string[] {"#"};
				string[] asLines = aux.Split(stringSeparators, StringSplitOptions.None);
				foreach (string line in asLines) {
					if (line.Trim().Length > 0) {
						backworker_progress res = default(backworker_progress);
						res.senderHost = clientHost;
						res.line = line;
						((BackgroundWorker)sender).ReportProgress(0, res);
					}
				}
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
		IMUdata data = new IMUdata (res.senderHost, res.line);
		//error_text = data.Get_csvLine ();

	}

	private void backWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		//CType(sender, BackgroundWorker).
	}
}
