using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Transfer.Service.Domain.Interfaces;
using Microservices.Transfer.Service.Infrastructure.Data;
using Microservices.Transfer.Service.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Microservices.Transfer.Service.Infrastructure.Persistence.Extensions
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
                TransferUnitOfWork unitOfWork = (TransferUnitOfWork)scope.ServiceProvider.GetRequiredService<ITransferUnitOfWork>();             
            }
        }
        public static void AddPersistence(this IServiceCollection services, Action<PersistenceOptions> configure)
        {
            var options = new PersistenceOptions();
            configure(options);                      
            
            services.AddTransient<ITransferUnitOfWork>(provider =>
            {                
                return new TransferUnitOfWork(options.MongoDbSettings);
            });
        }
    }
}
