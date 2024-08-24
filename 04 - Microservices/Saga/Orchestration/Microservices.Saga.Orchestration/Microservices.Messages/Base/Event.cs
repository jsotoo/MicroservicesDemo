using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Messages.Base
{
    public class Event : Message
    {
        public DateTime Timestamp { get; set; }
    }
}
