using Microservices.Infrastructure.Kafka.Config;
using Microservices.Messages.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Infrastructure.Kafka.Producer
{
    public class MessageMapper : IMessageMapper
    {
        private List<KeyValuePair<Type, string>> _mappings;
        private KafkaConfig _kafkaConfig;
        public MessageMapper(KafkaConfig kafkaConfig)
        {
            _kafkaConfig = kafkaConfig;
            _mappings = new List<KeyValuePair<Type, string>>();
        }
        public void Add<TMessage>(string topicKey) where TMessage : Message
        {
            _mappings.Add(new KeyValuePair<Type, string>(typeof(TMessage), topicKey));
        }
        private ILookup<Type, string> GetLookup()
        {
            return _mappings.ToLookup(kvp => kvp.Key, kvp => kvp.Value);
        }
        public IEnumerable<string> GetCommandTopicsForMessage(Message message)
        {
            var lookup = GetLookup();
            var messageType = message.GetType();

            if (lookup.Contains(messageType))
            {
                IEnumerable<string> topicKeys = lookup[messageType];
                IEnumerable<string>  topics = _kafkaConfig.Producers.Commands.Topics.Where(t => topicKeys.Contains(t.Keys.FirstOrDefault())).Select(t=>t.Values.FirstOrDefault()).ToList();

                return topics;
            }
            else
            {
                throw new InvalidOperationException($"No topics mapping found for message type {messageType.Name}");
            }
        }
        public IEnumerable<string> GetAllEventTopics()
        {            
            return _kafkaConfig.Producers.Events.Topics.Select(t => t.Values.FirstOrDefault()).ToList();
        }
    }
}
