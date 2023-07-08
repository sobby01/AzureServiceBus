using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusReceiver
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            ServiceConfig config = new ServiceConfig()
            {
                ConnectionString = "<ConnectionString>",
                Queue = "email"
            };

            ServiceBusClientOptions options = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };

            var client = new ServiceBusClient(config.ConnectionString, options);
            //ServiceBusSender sender = client.CreateSender(config.Queue);
            // the processor that reads and processes messages from the queue
            ServiceBusProcessor processor;
            processor = client.CreateProcessor(config.Queue, new ServiceBusProcessorOptions());

            try
            {
                // add handler to process messages
                processor.ProcessMessageAsync += MessageHandler;

                // add handler to process any errors
                processor.ProcessErrorAsync += ErrorHandler;

                // start processing 
                await processor.StartProcessingAsync();

                Console.WriteLine("Wait for a minute and then press any key to end the processing");
                Console.ReadKey();

                // stop processing 
                Console.WriteLine("\nStopping the receiver...");
                await processor.StopProcessingAsync();
                Console.WriteLine("Stopped receiving messages");
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await processor.DisposeAsync();
                await client.DisposeAsync();
            }
        }

        static async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string content = args.Message.Body.ToString();
            Console.WriteLine(content);
            // complete the message. message is deleted from the queue. 
            await args.CompleteMessageAsync(args.Message);
        }

        static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.ErrorSource);
            Console.WriteLine(args.FullyQualifiedNamespace);
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
