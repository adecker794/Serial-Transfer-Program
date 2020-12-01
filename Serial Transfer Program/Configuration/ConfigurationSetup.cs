using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serial_Transfer_Program
{
    class ConfigurationSetup
    {
        public static void startConfigurationSetup()
        {
            Console.WriteLine("What COM Port would you like to select?");
            string[] ports = SerialPort.GetPortNames();
            Console.WriteLine("The following serial ports were found:");
            // Display each port name to the console.
            foreach (string portss in ports)
            {
                Console.WriteLine(portss);
            }
            string comSelection = Console.ReadLine();

            Settings1.Default.ComPort = comSelection;
            Settings1.Default.Save();

            Console.WriteLine("Please input BAUD rate \n" +
            "115200 \n" +
            "230400 \n" +
            "460800 \n" +
            "The higher you go, the higher the failure rate");
            int baudRateSelection = Convert.ToInt32(Console.ReadLine());

            Settings1.Default.ComPort = comSelection;
            Settings1.Default.Save();

            Console.WriteLine("What would you like to set for your destination file?");
            string destinationFileSelection = Console.ReadLine();

            Settings1.Default.DestinationFile = destinationFileSelection;
            Settings1.Default.Save();

            Console.Write("What would you like to set for your source file? \n");
            string sourceFileSelection = Console.ReadLine();

            Settings1.Default.SourceFile = sourceFileSelection;
            Settings1.Default.Save();

            Console.WriteLine("What would you like to do now?");
            ActivitySelection.activitySelection();
        }
    }
}
