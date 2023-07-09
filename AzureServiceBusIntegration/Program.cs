using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzureServiceBusIntegration
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await SendObjectMessage();
        }

        private static async Task SendStringMessage()
        {
            IServiceBusSender sender = new ServiceBusSender();
            var result = await sender.SendMessage("Hello this is Saurabh");
            Console.WriteLine(result);
            Console.ReadKey();
        }

        private static async Task SendObjectMessage()
        {
            IServiceBusSender sender = new ServiceBusSender();
            Random random = new Random();
            for (int i = 0; i < 15; i++)
            {
                int randomNum = random.Next(1, 100000);
                Content content = new Content()
                {
                    CorrelationID = Guid.NewGuid(),
                    CreatedBy = "saurabh.singh@onscreen.us",
                    CreatedDate = DateTimeOffset.Now,
                    Message = $"This is my test Message : {randomNum}"
                };
                var result = await sender.SendMessage<Content>(content);
                
                Console.WriteLine(result);
            }
            
            Console.ReadKey();
        }

        private static async Task SendParallelMessages()
        {
            IServiceBusSender sender = new ServiceBusSender();
            Random random = new Random();
            Parallel.For(0, 15, async (i) =>
            {
                int randomNum = random.Next(1, 100000);
                Content content = new Content()
                {
                    CorrelationID = Guid.NewGuid(),
                    CreatedBy = "saurabh.singh@dummy.us",
                    CreatedDate = DateTimeOffset.Now,
                    Message = $"This is my test Message : {randomNum}"
                };

                var result = await sender.SendMessage<Content>(content);
                Console.WriteLine(result);
            });
        }
    }
}
