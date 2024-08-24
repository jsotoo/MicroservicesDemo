using Confluent.Kafka;
using Microservices.Infrastructure.Kafka.Config;
using Microservices.Infrastructure.Kafka.Util;
using Microservices.Messages.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Infrastructure.Kafka.Consumer
{
    public abstract class Worker : BackgroundService
    {
        private readonly IConsumer<Ignore, Message> _consumer;
        private readonly ILogger<Worker> _logger;
        private readonly TopicManager _topicManager;
        private readonly IMessageProcessor _processor;
        private readonly TopicConfig _topicConfig;
        private readonly string _bootstrapServers;        

        public Worker(
            ILogger<Worker> logger,
            IMessageProcessor processor,
            TopicConfig topicConfig,
            string bootstrapServers)
        {
            _logger = logger;
            _topicConfig = topicConfig;
            _bootstrapServers = bootstrapServers;
            _topicManager = new TopicManager(_bootstrapServers, _topicConfig.Topics.Select(t => t.Values.FirstOrDefault()).ToList());

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                GroupId = _topicConfig.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, Message>(consumerConfig)
                        .SetValueDeserializer(new JsonMessageDeserializer())
                        .Build();

            _processor = processor;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(async () =>
            {
                await _topicManager.EnsureTopicsExistsAsync();

                _consumer.Subscribe(_topicConfig.Topics.Select(t => t.Values.FirstOrDefault()).ToList());

                while (!stoppingToken.IsCancellationRequested)
                {
                    await ProcessKafkaMessageAsync(stoppingToken);
                }

                _consumer.Close();
            }, stoppingToken);
        }
        public async Task ProcessKafkaMessageAsync(CancellationToken stoppingToken)
        {
            try
            {
                var consumeResult = _consumer.Consume(stoppingToken);
                if (consumeResult != null)
                {
                    var message = consumeResult.Message.Value;
                    
                    switch (message)
                    {
                        case Command commandMessage:
                            await _processor.ProcessCommand(commandMessage);
                            _logger.LogInformation("Processed Command: {0}", commandMessage);
                            break;
                        case Event eventMessage:
                            await _processor.ProcessEvent(eventMessage);
                            _logger.LogInformation("Processed Event: {0}", eventMessage);
                            break;
                        default:
                            _logger.LogWarning("Received unsupported message type: {0}", message.GetType());
                            break;
                    }

                    _logger.LogInformation("Received message: {0}", message);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Consumption was canceled by the stopping token.");
            }
            catch (ConsumeException e)
            {
                _logger.LogError($"Kafka consume error: {e.Error.Reason}");
               // throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error processing Kafka message: {ex.Message}");
                //throw;
            }
        }
    }
}
