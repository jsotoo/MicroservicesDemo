using Microservices.Messages.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Infrastructure.Kafka.Consumer
{
    public interface IMessageProcessor
    {
        Task ProcessCommand(Message message);
        Task ProcessEvent(Message message);
    }
}
