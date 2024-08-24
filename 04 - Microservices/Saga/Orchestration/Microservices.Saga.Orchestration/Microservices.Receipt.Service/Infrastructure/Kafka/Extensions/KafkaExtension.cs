using Microservices.Infrastructure.Kafka.Config;
using Microservices.Infrastructure.Kafka.Consumer;
using Microservices.Infrastructure.Kafka.Producer;
using Microservices.Messages.Service.Orchestrator;
using Microservices.Messages.Service.Receipt.Events;
using Microservices.Messages.Service.Transfer.Commands;
using Microservices.Messages.Service.Transfer.Events;
using Microservices.Messages.Service.Validator.Commands;
using Microservices.Messages.Service.Validator.Events;
using Microservices.Receipt.Service.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Microservices.Receipt.Service.Infrastructure.Kafka.Extensions
{
    public static class KafkaExtension
    {
        public static IServiceCollection AddKafkaReceipt(this IServiceCollection services,KafkaConfig kafkaConfig)
        {   
            services.AddProducerMessageMapping(kafkaConfig);

            return services;
        }
        private static IServiceCollection AddProducerMessageMapping(this IServiceCollection services, KafkaConfig kafkaConfig)
        {
            var messageMapper = new MessageMapper(kafkaConfig);
            messageMapper.Add<ReceiptIssuedEvent>("Receipt");

            services.AddSingleton<IMessageMapper>(messageMapper);            

            return services;
        }
    }
}
