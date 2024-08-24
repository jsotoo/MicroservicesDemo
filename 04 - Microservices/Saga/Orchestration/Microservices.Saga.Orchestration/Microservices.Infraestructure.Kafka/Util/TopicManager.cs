using Confluent.Kafka.Admin;
using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Infrastructure.Kafka.Util
{
    public class TopicManager
    {
        private readonly string _bootstrapServers;
        private readonly IList<string> _topics;
        private readonly int _numPartitions;
        private readonly short _replicationFactor;
        public TopicManager(
            string bootstrapServers,
            IList<string> topics,
            int numPartition = 1,
            short replicationFactor = 1)
        {
            _bootstrapServers = bootstrapServers;
            _topics = topics;
            _numPartitions = numPartition;
            _replicationFactor = replicationFactor;
        }
        public async Task EnsureTopicsExistsAsync()
        {
            foreach (var topic in _topics)
            {
                await EnsureTopicsExistsAsync(_bootstrapServers, topic, _numPartitions, _replicationFactor);
            }
        }
        private async Task EnsureTopicsExistsAsync(string bootstrapServers, string topicName, int numPartitions = 1, short replicationFactor = 1)
        {
            using var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = bootstrapServers }).Build();

            try
            {
                var metadata = adminClient.GetMetadata(topicName, TimeSpan.FromSeconds(10));
                if (metadata.Topics.Count > 0 && metadata.Topics[0].Error.Code == ErrorCode.NoError)
                {
                    Console.WriteLine($"Topic '{topicName}' already exists.");
                    return;
                }
            }
            catch (KafkaException e) when (e.Error.Reason.Contains("Unknown Topic"))
            {
                Console.WriteLine($"Topic '{topicName}' does not exist, creating it...");

                var topicSpecification = new TopicSpecification
                {
                    Name = topicName,
                    NumPartitions = numPartitions,
                    ReplicationFactor = replicationFactor
                };

                try
                {
                    await adminClient.CreateTopicsAsync(new[] { topicSpecification });
                    Console.WriteLine($"Topic '{topicName}' successfully created.");
                }
                catch (CreateTopicsException topicEx)
                {
                    Console.WriteLine($"Failed to create topic: {topicEx.Results[0].Error.Reason}");
                }
            }
        }
    }
}
