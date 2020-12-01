using System;
using System.Configuration;
using System.IO;
using System.Timers;
using System.IO.Ports;

namespace Serial_Transfer_Program
{
    class SendingData
    {
        public static string comport = Settings1.Default.ComPort;
        public static int baudrateInt = Settings1.Default.BaudRate;
        public static string sourceFile = Settings1.Default.SourceFile;
        private static System.Timers.Timer aTimer;
        [STAThread]

        public static void sendingDataStart()
        {
            Console.WriteLine("You have picked {0} as the ComPort and {1} as the BaudRate ", comport, baudrateInt);
            Console.WriteLine("Your source file is {0}", sourceFile);
            // Instantiate the communications
            // port with some basic settings
            SerialPort port = new SerialPort(
              comport, baudrateInt, Parity.None, 8, StopBits.One);

            // Open the port for communications
            port.Open();

            //Grabs the bytes from a file, converts it to Base64 and sends it
            Byte[] bytes = File.ReadAllBytes(sourceFile);
            String file = Convert.ToBase64String(bytes);
            Console.WriteLine(file);
            port.Write(file);

            // Write a set of bytes
            port.Write(new byte[] { 0x0A, 0xE2, 0xFF }, 0, 3);

            // Close the port
            port.Close();
            Console.WriteLine("Would you like to send again? Y\\N");
            string runSendDataAgain = Console.ReadLine();
            if (runSendDataAgain.ToUpper() == "Y")
            {
                Console.WriteLine("Sending data again");
                sendingDataStart();
            }
            else if (runSendDataAgain.ToUpper() == "N")
            {
                Console.WriteLine("Not running send data again, closing in 5 seconds");
                // Create a timer with a five second interval.
                aTimer = new System.Timers.Timer(5000);
                // Hook up the Elapsed event for the timer. 
                aTimer.Elapsed += OnTimedEvent;
                aTimer.AutoReset = true;
                aTimer.Enabled = true;
            }
        }
        public static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
            System.Environment.Exit(1);
        }
    }
}
