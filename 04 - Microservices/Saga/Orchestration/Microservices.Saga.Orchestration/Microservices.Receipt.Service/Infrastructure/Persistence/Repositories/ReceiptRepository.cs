using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Receipt.Service.Domain.Entities;
using Microservices.Receipt.Service.Domain.Interfaces;
using MongoDB.Driver;

namespace Microservices.Receipt.Service.Infrastructure.Persistence.Repositories
{
    public class ReceiptRepository : MongoRepository<Domain.Entities.Receipt>,  IReceiptRepository
    {        
        public ReceiptRepository(IMongoDatabase database, string collectionName):base(database, collectionName)
        {
        }
    }
}
