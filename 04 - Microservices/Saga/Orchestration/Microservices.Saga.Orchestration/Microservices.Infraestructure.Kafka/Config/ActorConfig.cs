using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Infrastructure.Kafka.Config
{
    public class ActorConfig
    {
        public ActorConfig()
        {
            Events = new TopicConfig();
            Commands = new TopicConfig();
        }
        public TopicConfig Events { get; set; }
        public TopicConfig Commands { get; set; }
    }
}
