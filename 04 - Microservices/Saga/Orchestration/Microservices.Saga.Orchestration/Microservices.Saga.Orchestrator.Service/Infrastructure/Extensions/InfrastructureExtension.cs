using Microservices.Infrastructure.Kafka.Config;
using Microservices.Infrastructure.Kafka.Extensions;
using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Saga.Orchestrator.Service.Infrastructure.Http.Extensions;
using Microservices.Saga.Orchestrator.Service.Infrastructure.Kafka.Events.Handlers;
using Microservices.Saga.Orchestrator.Service.Infrastructure.Kafka.Extensions;
using Microservices.Saga.Orchestrator.Service.Infrastructure.Persistence.Extensions;

namespace Microservices.Saga.Orchestrator.Service.Infrastructure.Extensions
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var kafkaConfig = configuration.GetSection("Kafka").Get<KafkaConfig>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(StartSagaCommandHandler).Assembly));
            services.AddKafkaOrchestrator(kafkaConfig);
            services.AddKafka(kafkaConfig);
            services.AddPersistence(opt => opt.MongoDbSettings = configuration.GetSection("MongoDB").Get<MongoDbSettings>());
            services.AddHttp();

            return services;
        }
    }
}
