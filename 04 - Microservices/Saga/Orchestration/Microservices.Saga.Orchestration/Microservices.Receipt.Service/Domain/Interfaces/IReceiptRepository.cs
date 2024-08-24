using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Receipt.Service.Domain.Entities;

namespace Microservices.Receipt.Service.Domain.Interfaces
{
    public interface IReceiptRepository : IMongoRepository<Entities.Receipt>
    {        
    }
}
