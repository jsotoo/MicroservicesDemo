using Confluent.Kafka;
using Microservices.Infrastructure.Kafka.Config;
using Microservices.Infrastructure.Kafka.Consumer;
using Microservices.Infrastructure.Kafka.Producer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Microservices.Infrastructure.Kafka.Extensions
{
    public static class KafkaExtensions
    {
        public static IServiceCollection AddKafka(
            this IServiceCollection services, 
            KafkaConfig kafkaConfig
        )
        {
            services.AddKafkaConsumers(kafkaConfig);
            services.AddKafkaProducer(kafkaConfig);

            return services;
        }
        private static IServiceCollection AddKafkaProducer(
            this IServiceCollection services,
            KafkaConfig kafkaConfig)
        {
            services.AddSingleton<IMessageProducer>(provider => 
            {
                IMessageMapper messageMapper = provider.GetRequiredService<IMessageMapper>();
                return new MessageProducer(kafkaConfig, messageMapper);
            });

            return services;
        }
        private static IServiceCollection AddKafkaConsumers(
            this IServiceCollection services,
            KafkaConfig kafkaConfig)
        {

            services.AddTransient<IMessageProcessor, MessageProcessor>();

            if (kafkaConfig.Consumers.Events.Topics.Count() > 0)
            {
                services.AddConsumerWorkerWithProcessor(kafkaConfig, TypeWorker.Event);
            }

            if (kafkaConfig.Consumers.Commands.Topics.Count() > 0)
            {
                services.AddConsumerWorkerWithProcessor(kafkaConfig,TypeWorker.Command);
            }

            return services;
        }
        private static IServiceCollection AddConsumerWorkerWithProcessor(
            this IServiceCollection services,
            KafkaConfig kafkaConfig,
            TypeWorker typeWorker
        )
        {
            if (typeWorker == TypeWorker.Event)
            {
                services.AddHostedService(provider =>
                {
                    IMessageProcessor messageProcessor = provider.GetRequiredService<IMessageProcessor>();
                    ILogger<EventWorker> logger = provider.GetRequiredService<ILogger<EventWorker>>();
                    return new EventWorker(logger, messageProcessor, kafkaConfig.Consumers.Events, kafkaConfig.BootstrapServers);
                });
            }
            if (typeWorker == TypeWorker.Command)
            {
                services.AddHostedService(provider =>
                {
                    IMessageProcessor messageProcessor = provider.GetRequiredService<IMessageProcessor>();
                    ILogger<CommandWorker> logger = provider.GetRequiredService<ILogger<CommandWorker>>();
                    return new CommandWorker(logger, messageProcessor, kafkaConfig.Consumers.Commands, kafkaConfig.BootstrapServers);
                });
            }            

            return services;
        }


    }
}
