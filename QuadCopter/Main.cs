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
			//Initialise
			Init.WiringPiSetup ();

			Engine engine = new Engine(0);
			engine.engineTest();

			UserInputHandler input = new UserInputHandler(13000);

			while(input.ClientConnected == false) 
		      {
				input.AwaitConnection();
		        
			}
			Console.WriteLine("Connected!");

			Thread listenerThread = new Thread(new ThreadStart(input.StartListening));
			listenerThread.Start();


			//Sensors sensors = new Sensors();
			//AttitudeController attControl = new AttitudeController();

			//Engine engine1 = new Engine(0);
			//Engine engine2 = new Engine(1);

			//Start Main Loop

			while (true) {
				Console.WriteLine("main loop");
				//get userInput

				Console.WriteLine("Current Command:{0}",input.CurrentCommand);

				//get input from sensors


				//adjust attitude
				Thread.Sleep(1000);

			}

		}
	}
}

/*
			GPIO.pinMode (0, 1);
			GPIO.pinMode (1, 1);

			while (true) {

				GPIO.digitalWrite(1,1);
				GPIO.digitalWrite(0,0);
				Thread.Sleep(500);
				GPIO.digitalWrite(1,0);
				GPIO.digitalWrite(0,1);
				Thread.Sleep(500);


			}


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






			
					





