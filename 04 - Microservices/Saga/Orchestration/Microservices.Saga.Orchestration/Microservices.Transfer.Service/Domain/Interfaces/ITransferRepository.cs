using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Transfer.Service.Domain.Entities;

namespace Microservices.Transfer.Service.Domain.Interfaces
{
    public interface ITransferRepository : IMongoRepository<Entities.Transfer>
    {        
    }
}
