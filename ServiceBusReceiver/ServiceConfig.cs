using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusReceiver
{
    public class ServiceConfig
    {
        public string ConnectionString { get; set; }

        public string Queue { get; set; }
    }
}
