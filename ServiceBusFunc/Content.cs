using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBusFunc
{
    public class Content
    {
        public Guid CorrelationID { get; set; }
        public string Message { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
