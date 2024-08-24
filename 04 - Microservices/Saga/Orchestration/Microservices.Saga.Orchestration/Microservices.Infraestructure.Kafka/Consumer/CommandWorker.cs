using Microservices.Infrastructure.Kafka.Config;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Microservices.Infrastructure.Kafka.Consumer
{
    public class CommandWorker : Worker
    {
        public CommandWorker(ILogger<Worker> logger, IMessageProcessor processor, TopicConfig topicConfig, string bootstrapServers) : base(logger, processor, topicConfig, bootstrapServers)
        {
        }
    }
}
