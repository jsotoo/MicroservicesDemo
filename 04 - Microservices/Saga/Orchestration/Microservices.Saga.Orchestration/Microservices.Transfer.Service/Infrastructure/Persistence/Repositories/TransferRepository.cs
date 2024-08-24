using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Transfer.Service.Domain.Entities;
using Microservices.Transfer.Service.Domain.Interfaces;
using MongoDB.Driver;

namespace Microservices.Transfer.Service.Infrastructure.Data.Repositories
{
    public class TransferRepository : MongoRepository<Domain.Entities.Transfer>,  ITransferRepository
    {        
        public TransferRepository(IMongoDatabase database, string collectionName):base(database, collectionName)
        {
        }
    }
}
