using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusReceiver
{
    public class ServiceBusReceiver : IServiceBusReceiver
    {
        public Task<string> ReceiveMessage()
        {
            throw new NotImplementedException();
        }
    }
}
