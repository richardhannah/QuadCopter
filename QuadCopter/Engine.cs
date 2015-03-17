using System;
using WiringPi;
using System.Threading;

namespace QuadCopter
{
	public class Engine
	{
		int outPutPin;



		public Engine (int coutputPin)
		{
			outPutPin = coutputPin;
			GPIO.pinMode(coutputPin,1);
		}

		public void engineTest ()
		{
			int write = 0;
			for(int i = 0; i < 10; i++){

				write = i%2;
				GPIO.digitalWrite(outPutPin,write);
				Thread.Sleep(500);
			}
		}


		public void IncreaseThrottle ()
		{

		}

		public void DecreaseThrottle ()
		{
		}

		public void CutEngine ()
		{
		}
	}
}

