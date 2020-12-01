using System.Configuration;
using System;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;

namespace Serial_Transfer_Program
{
    class ReceivingData
    {
        public static string comport = Settings1.Default.ComPort;
        public static int baudrateInt = Settings1.Default.BaudRate;
        public static string destinationFile = Settings1.Default.DestinationFile;
        [STAThread]
        public static void receivingDataStart()
        {
            Console.WriteLine("You have picked {0} as the ComPort and {1} as the BaudRate ", comport, baudrateInt);
            Console.WriteLine("Your destination file is {0}", destinationFile);

            // Instantiate the communications
            // port with some basic settings
            SerialPort port = new SerialPort(comport, baudrateInt, Parity.None, 8, StopBits.One);
            Console.WriteLine("Incoming Data:");

            // Attach a method to be called when there
            // is data waiting in the port's buffer
            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);

            // Begin communications
            port.Open();

            //Keeps the application open and awaiting data
            Application.Run();
        }
        public static void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //Brings in the serialport connection
            SerialPort sp = (SerialPort)sender;

            //Reads the lines as they come into the connection
            string line = sp.ReadLine();
            Console.WriteLine(line);
            // WriteAllLines creates a file, writes a collection of strings to the file,
            // It then converts the file from Base64 to a "readable" string and sends it to a specified file
            System.IO.File.WriteAllText(@"tempwritefile.txt", line, System.Text.Encoding.Default);
            string linesRead = File.ReadAllText(@"tempwritefile.txt");
            Byte[] bytes1 = Convert.FromBase64String(linesRead);
            File.WriteAllBytes(destinationFile, bytes1);

            //Deletes the old file originially used to write the data to
            File.Delete(@"tempwritefile.txt");

            //Closes the connection and starts a new one so the loop continues
            Console.WriteLine("File transfer has completed, starting to receive again.");
            sp.Close();
            ReceivingData.receivingDataStart();
        }
    }
}
