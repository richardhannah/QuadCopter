using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using WiringPi;
using System.Threading;



namespace QuadCopter
{
	class MainClass
	{
		public static void Main (string[] args)
		{

			Init.WiringPiSetup ();

			GPIO.pinMode (0, 1);


			while (true) {

				GPIO.digitalWrite(0,1);
				Thread.Sleep(500);
				GPIO.digitalWrite(0,0);
				Thread.Sleep(500);


			}

			/*
			GPIOPinDriver led1;
 
            // Create the object.
            led1 = new GPIOPinDriver(GPIOPinDriver.Pin.GPIO18);
 
            // Set it as an output pin.
			led1.Direction = GPIOPinDriver.GPIODirection.Out;
 
            // Give it power.
            led1.State = GPIOPinDriver.GPIOState.High;








			GPIOMem led =  new GPIOMem(GPIOPins.V2_Pin_P1_12);

			while (true) {

				led.Write(PinState.High);
				Console.WriteLine("blink on");
				System.Threading.Thread.Sleep(500);
				led.Write(PinState.Low);
				Console.WriteLine("blink off");
				System.Threading.Thread.Sleep(500);
			}
			*/








			TcpListener server=null;   
    try
    {
      // Set the TcpListener on port 13000.
      Int32 port = 13000;
      IPAddress localAddr = IPAddress.Parse("192.168.0.7");

      // TcpListener server = new TcpListener(port);
      server = new TcpListener(localAddr, port);

      // Start listening for client requests.
      server.Start();

      // Buffer for reading data
      Byte[] bytes = new Byte[256];
      String data = null;

      // Enter the listening loop. 
      while(true) 
      {
        Console.Write("Waiting for a connection... ");

        // Perform a blocking call to accept requests. 
        // You could also user server.AcceptSocket() here.
        TcpClient client = server.AcceptTcpClient();            
        Console.WriteLine("Connected!");

        data = null;

        // Get a stream object for reading and writing
        NetworkStream stream = client.GetStream();

        int i;

        // Loop to receive all the data sent by the client. 
        while((i = stream.Read(bytes, 0, bytes.Length))!=0) 
        {   
          // Translate data bytes to a ASCII string.
          data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
          Console.WriteLine("Received: {0}", data);

          // Process the data sent by the client.
          data = data.ToUpper();

          byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

          // Send back a response.
          stream.Write(msg, 0, msg.Length);
          Console.WriteLine("Sent: {0}", data);            
        }

        // Shutdown and end connection
        client.Close();
      }
    }
    catch(SocketException e)
    {
      Console.WriteLine("SocketException: {0}", e);
    }
    finally
    {
       // Stop listening for new clients.
       server.Stop();
    }


    Console.WriteLine("\nHit enter to continue...");
    Console.Read();
		}
	}
}
