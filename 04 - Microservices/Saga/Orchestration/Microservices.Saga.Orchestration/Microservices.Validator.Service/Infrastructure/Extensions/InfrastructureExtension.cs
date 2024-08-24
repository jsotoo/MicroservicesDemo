using Microservices.Infrastructure.Kafka.Config;
using Microservices.Validator.Service.Infrastructure.Data;
using Microservices.Infrastructure.Kafka.Extensions;
using Microservices.Validator.Service.Infrastructure.Persistence.Extensions;
using Microservices.Validator.Service.Infrastructure.Kafka.Extensions;
using Microservices.Validator.Service.Infrastructure.Http.Extensions;
using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Validator.Service.Infrastructure.Kafka.Commands.Handlers;

namespace Microservices.Validator.Service.Infrastructure.Extensions
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var kafkaConfig = configuration.GetSection("Kafka").Get<KafkaConfig>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ValidateAccountCommandHandler).Assembly));            
            services.AddKafkaValidator(kafkaConfig);
            services.AddKafka(kafkaConfig);
            services.AddPersistence(opt => opt.MongoDbSettings = configuration.GetSection("MongoDB").Get<MongoDbSettings>());
            services.AddHttp();

            return services;
        }        
    }
}
