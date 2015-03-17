using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace QuadCopter
{
	public class UserInputHandler
	{

		int port;
		IPAddress localAddr;
		TcpListener server;
		TcpClient client;
		string currentCommand;


		//Buffer for reading data
		Byte[] bytes = new Byte[256];
		String data = null;

		public bool ClientConnected {

			get;
			set;
		}

		public string CurrentCommand {
			get {

				if (currentCommand != null) {

					return currentCommand;
				} else {
					return "no command specified";
				}
				;
			}
			set {
				if (value != null) {
					currentCommand = value;
				} else {
					currentCommand = "no key pressed";
				}
				;
			}



		}

		public UserInputHandler (int cPort)
		{

			port = cPort;
			localAddr = IPAddress.Parse ("192.168.0.8");
			ClientConnected = false;

			// TcpListener server = new TcpListener(port);
			server = new TcpListener (localAddr, port);

			// Start listening for client requests.
			server.Start ();

	      
		}

		public void AwaitConnection ()
		{
			Console.Write ("Waiting for a connection... ");
			client = server.AcceptTcpClient ();
			ClientConnected = true;
		}

		public void StartListening ()
		{
			while (true) {


					
				try {


					// Get a stream object for reading and writing
					NetworkStream stream = client.GetStream ();

					int i;

					// Loop to receive all the data sent by the client. 
					while ((i = stream.Read(bytes, 0, bytes.Length))!=0) {   
						// Translate data bytes to a ASCII string.
						data = System.Text.Encoding.ASCII.GetString (bytes, 0, i);
						Console.WriteLine("data length: {0}",data.Length);
						CurrentCommand = data;
						Console.WriteLine ("Received: {0}", data);
						// Process the data sent by the client.
						//data = data.ToUpper ();

						//byte[] msg = System.Text.Encoding.ASCII.GetBytes (data);

						// Send back a response.
						//stream.Write (msg, 0, msg.Length);
						//Console.WriteLine ("Sent: {0}", data);            
					}



						
						






					// Shutdown and end connection
					//client.Close ();
      
				} catch (SocketException e) {
					Console.WriteLine ("SocketException: {0}", e);
				} finally {
					// Stop listening for new clients.
					server.Stop ();
				}



				data = null;

				Thread.Sleep (2000);
			}
		}






	}
}


