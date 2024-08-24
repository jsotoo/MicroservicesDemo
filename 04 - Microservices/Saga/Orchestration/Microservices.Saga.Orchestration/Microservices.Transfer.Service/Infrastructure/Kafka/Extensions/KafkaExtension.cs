using Microservices.Infrastructure.Kafka.Config;
using Microservices.Infrastructure.Kafka.Consumer;
using Microservices.Infrastructure.Kafka.Producer;
using Microservices.Messages.Service.Orchestrator;
using Microservices.Messages.Service.Transfer.Commands;
using Microservices.Messages.Service.Transfer.Events;
using Microservices.Messages.Service.Validator.Commands;
using Microservices.Messages.Service.Validator.Events;
using Microservices.Transfer.Service.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Microservices.Transfer.Service.Infrastructure.Kafka.Extensions
{
    public static class KafkaExtension
    {
        public static IServiceCollection AddKafkaTransfer(this IServiceCollection services,KafkaConfig kafkaConfig)
        {   
            services.AddProducerMessageMapping(kafkaConfig);

            return services;
        }
        private static IServiceCollection AddProducerMessageMapping(this IServiceCollection services, KafkaConfig kafkaConfig)
        {
            var messageMapper = new MessageMapper(kafkaConfig);
            messageMapper.Add<TransferredEvent>("Transfer");            

            services.AddSingleton<IMessageMapper>(messageMapper);            

            return services;
        }
    }
}
