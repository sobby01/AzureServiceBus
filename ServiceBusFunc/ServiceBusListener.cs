using System;
using System.Text;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ServiceBusFunc
{
    public static class ServiceBusListener
    {
        [FunctionName("ServiceBusListener")]
        public static void Run([ServiceBusTrigger("email", Connection = "connectionstring")] Message message, MessageReceiver messageReceiver, ILogger log)
        {
            try
            {
                string payload = Encoding.UTF8.GetString(message.Body);
                log.LogInformation($"C# ServiceBus queue  trigger function processed message: {payload}");

                Content model = JsonConvert.DeserializeObject<Content>(payload);

                log.LogInformation($"C# ServiceBus queue trigger function processed message: {model.Message}");
                log.LogInformation($"CorrelationID of processed message: {model.CorrelationID}");
                messageReceiver.CompleteAsync(message.SystemProperties.LockToken);

            }
            catch (Exception)
            {
                // Send message to DeadLetter Queue 
                messageReceiver.DeadLetterAsync(message.SystemProperties.LockToken);
            }
        }
    }
}
