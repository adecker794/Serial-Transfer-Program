using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serial_Transfer_Program
{
    class ActivitySelection
    {
        public static void activitySelection()
        {
            Console.WriteLine("Pick an option. \n" +
                "Option 1: Receiving \n" +
                "Option 2: Sending \n" +
                "Option 3: Configuration \n" +
                "Enter an option.");
            string startstr = Console.ReadLine();
            if (startstr.Equals("1"))
            {
                ReceivingData.receivingDataStart();
            }
            else if (startstr.Equals("2"))
            {
                SendingData.sendingDataStart();
            }
            else if (startstr.Equals("3"))
            {
                ConfigurationSetup.startConfigurationSetup();
            }
            else if (startstr.Equals(""))
            {
                Console.WriteLine("You have to actually input an option for this to work properly....");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("You really entered the wrong thing, close me");
                Console.ReadLine();
            }
            //Console.ReadLine();
        }
    }
}
