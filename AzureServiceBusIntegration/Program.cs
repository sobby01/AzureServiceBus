using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureServiceBusIntegration
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            IServiceBusSender  sender =  new ServiceBusSender();
            var result = await sender.SendMessage("Hello this is Saurabh");
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
