using Confluent.Kafka;
using Microservices.Infrastructure.Kafka.Config;
using Microservices.Infrastructure.Kafka.Util;
using Microservices.Messages.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Infrastructure.Kafka.Producer
{
    public class MessageProducer : IMessageProducer
    {
        private readonly IProducer<Null, Message> _producer;
        private readonly IMessageMapper _messageMapper;

        public MessageProducer(
            KafkaConfig kafkaConfig,
            IMessageMapper messageMapper
        )
        {
            var producerConfig = new ProducerConfig { BootstrapServers = kafkaConfig.BootstrapServers };
            _producer = new ProducerBuilder<Null, Message>(producerConfig)
                .SetValueSerializer(new JsonMessageSerializer<Message>())
                .Build();
            _messageMapper = messageMapper;
        }
        public async Task SendMessageAsync(Message message)
        {
            IEnumerable<string> topics;

            if (message is Command)
            {
                topics = _messageMapper.GetCommandTopicsForMessage(message);
            }
            else if (message is Event)
            {                
                topics = _messageMapper.GetAllEventTopics();
            }
            else
            {             
                throw new InvalidOperationException("Unrecognized message type");
            }
                        
            foreach (var topic in topics)
            {
                try
                {
                    var result = await _producer.ProduceAsync(topic, new Message<Null, Message> { Value = message });
                    Console.WriteLine($"Message sent to {result.TopicPartitionOffset}");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Failed to deliver message to {topic}: {e.Message} [{e.Error.Code}]");
                }
            }
        }
        public void Dispose()
        {
            _producer.Flush();
            _producer.Dispose();
        }
    }
}
