using Microservices.Infrastructure.Kafka.Config;
using Microservices.Transfer.Service.Infrastructure.Data;
using Microservices.Infrastructure.Kafka.Extensions;
using Microservices.Transfer.Service.Infrastructure.Persistence.Extensions;
using Microservices.Transfer.Service.Infrastructure.Kafka.Extensions;
using Microservices.Transfer.Service.Infrastructure.Http.Extensions;
using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Transfer.Service.Infrastructure.Kafka.Commands.Handlers;

namespace Microservices.Transfer.Service.Infrastructure.Extensions
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var kafkaConfig = configuration.GetSection("Kafka").Get<KafkaConfig>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(PerformTransferCommandHandler).Assembly));            
            services.AddKafkaTransfer(kafkaConfig);
            services.AddKafka(kafkaConfig);
            services.AddPersistence(opt => opt.MongoDbSettings = configuration.GetSection("MongoDB").Get<MongoDbSettings>());
            services.AddHttp();

            return services;
        }        
    }
}
