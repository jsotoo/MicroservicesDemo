using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Infrastructure.Kafka.Config
{
    public class TopicConfig
    {
        public TopicConfig()
        {
            Topics = new List<Dictionary<string, string>>();
        }
        public string GroupId { get; set; }
        public List<Dictionary<string, string>> Topics { get; set; }
    }
}
