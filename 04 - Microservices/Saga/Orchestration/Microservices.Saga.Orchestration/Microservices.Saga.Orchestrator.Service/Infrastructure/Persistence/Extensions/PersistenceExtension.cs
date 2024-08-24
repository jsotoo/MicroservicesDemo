using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Saga.Orchestrator.Service.Domain.Interfaces;
using Microservices.Saga.Orchestrator.Service.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;


namespace Microservices.Saga.Orchestrator.Service.Infrastructure.Persistence.Extensions
{
    public class PersistenceOptions
    {
        public MongoDbSettings MongoDbSettings;
    }
    public static class PersistenceExtension
    {
        public static void AddPersistence(this IServiceCollection services, Action<PersistenceOptions> configure)
        {
            var options = new PersistenceOptions();
            configure(options);

            services.AddTransient<IOrchestratorUnitOfWork>(provider =>
            {
                return new OrchestratorUnitOfWork(options.MongoDbSettings);
            });
        }
    }
}
