using Microservices.Infrastructure.Kafka.Config;
using Microservices.Infrastructure.Kafka.Consumer;
using Microservices.Infrastructure.Kafka.Producer;
using Microservices.Messages.Service.Orchestrator.Commands;
using Microservices.Messages.Service.Orchestrator.Events;
using Microservices.Messages.Service.Receipt.Commands;
using Microservices.Messages.Service.Transfer.Commands;
using Microservices.Messages.Service.Validator.Commands;
using MongoDB.Driver;

namespace Microservices.Saga.Orchestrator.Service.Infrastructure.Kafka.Extensions
{
    public static class KafkaExtension
    {
        public static IServiceCollection AddKafkaOrchestrator(this IServiceCollection services, KafkaConfig kafkaConfig)
        {            
            services.AddProducerMessageMapping(kafkaConfig);

            return services;
        }
        private static IServiceCollection AddProducerMessageMapping(this IServiceCollection services, KafkaConfig kafkaConfig)
        {
            var messageMapper = new MessageMapper(kafkaConfig);
            messageMapper.Add<StartSagaCommand>("Orchestrator");
            messageMapper.Add<SagaStartedEvent>("Orchestrator");
            messageMapper.Add<ValidateAccountCommand>("Validator");
            messageMapper.Add<PerformTransferCommand>("Transfer");
            messageMapper.Add<IssueReceiptCommand>("Receipt");
            messageMapper.Add<SagaCompletedEvent>("Orchestrator");
            services.AddSingleton<IMessageMapper>(messageMapper);            

            return services;
        }
    }
}
