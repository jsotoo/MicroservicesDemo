using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Validator.Service.Domain.Interfaces;
using Microservices.Validator.Service.Infrastructure.Data;
using Microservices.Validator.Service.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Microservices.Validator.Service.Infrastructure.Persistence.Extensions
{
    public class PersistenceOptions
    {
        public MongoDbSettings MongoDbSettings; 
    }
    public static class PersistenceExtension
    {
        public static void UseSeedData(this IHost app)
        {
            var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopedFactory.CreateScope())
            {
                FinancialUnitOfWork unitOfWork = (FinancialUnitOfWork)scope.ServiceProvider.GetRequiredService<IFinancialUnitOfWork>();
                unitOfWork.EnsureSeedDataForUnitOfWorkAsync();
            }
        }
        public static void AddPersistence(this IServiceCollection services, Action<PersistenceOptions> configure)
        {
            var options = new PersistenceOptions();
            configure(options);                      
            
            services.AddTransient<IFinancialUnitOfWork>(provider =>
            {                
                return new FinancialUnitOfWork(options.MongoDbSettings);
            });
        }
    }
}
