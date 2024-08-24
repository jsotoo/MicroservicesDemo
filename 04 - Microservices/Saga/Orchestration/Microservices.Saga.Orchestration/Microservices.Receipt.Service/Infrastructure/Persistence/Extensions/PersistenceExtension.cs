using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Receipt.Service.Domain.Interfaces;
using Microservices.Receipt.Service.Infrastructure.Persistence;
using Microservices.Receipt.Service.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Microservices.Receipt.Service.Infrastructure.Persistence.Extensions
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
                ReceiptUnitOfWork unitOfWork = (ReceiptUnitOfWork)scope.ServiceProvider.GetRequiredService<IReceiptUnitOfWork>();             
            }
        }
        public static void AddPersistence(this IServiceCollection services, Action<PersistenceOptions> configure)
        {
            var options = new PersistenceOptions();
            configure(options);                      
            
            services.AddTransient<IReceiptUnitOfWork>(provider =>
            {                
                return new ReceiptUnitOfWork(options.MongoDbSettings);
            });
        }
    }
}
