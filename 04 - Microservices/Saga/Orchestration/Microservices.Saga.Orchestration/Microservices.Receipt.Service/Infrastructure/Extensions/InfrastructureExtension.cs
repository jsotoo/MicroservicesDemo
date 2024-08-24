using Microservices.Infrastructure.Kafka.Config;
using Microservices.Receipt.Service.Infrastructure.Persistence;
using Microservices.Infrastructure.Kafka.Extensions;
using Microservices.Receipt.Service.Infrastructure.Persistence.Extensions;
using Microservices.Receipt.Service.Infrastructure.Kafka.Extensions;
using Microservices.Receipt.Service.Infrastructure.Http.Extensions;
using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Receipt.Service.Infrastructure.Kafka.Commands.Handlers;

namespace Microservices.Receipt.Service.Infrastructure.Extensions
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var kafkaConfig = configuration.GetSection("Kafka").Get<KafkaConfig>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IssueReceiptCommandHandler).Assembly));            
            services.AddKafkaReceipt(kafkaConfig);
            services.AddKafka(kafkaConfig);
            services.AddPersistence(opt => opt.MongoDbSettings = configuration.GetSection("MongoDB").Get<MongoDbSettings>());
            services.AddHttp();

            return services;
        }        
    }
}
