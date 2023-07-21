using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AzureServiceBusIntegration
{
    public class ServiceBusSender : IServiceBusSender
    {
        ServiceConfig config;
        ServiceBusClientOptions options;
        public ServiceBusSender()
        {
            config = new ServiceConfig()
            {
                ConnectionString = "Endpoint=sb://os-messageinfra.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=WzbGKM8cTrLcyosNZClIV78uaRyJASl/X+ASbK/jykc=",
                Queue = "email"
            };

            options = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };
        }

        public async Task<bool> SendMessage(string content)
        {
            var client = new ServiceBusClient(config.ConnectionString, options);
            var sender = client.CreateSender(config.Queue);
            try
            {
                
                var message = new ServiceBusMessage(content);
                await sender.SendMessageAsync(message);
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex);
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }
            return true;            
        }

        public async Task<bool> SendMessage<T>(T content)
        {
            var client = new ServiceBusClient(config.ConnectionString, options);
            var sender = client.CreateSender(config.Queue);
            try
            {
                var serializedContent = JsonSerializer.Serialize(content);
                var message = new ServiceBusMessage(serializedContent);
                await sender.SendMessageAsync(message);
                Console.WriteLine($"Message sent to the queue: {config.Queue}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex);
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }
            return true;
        }
    }
}
