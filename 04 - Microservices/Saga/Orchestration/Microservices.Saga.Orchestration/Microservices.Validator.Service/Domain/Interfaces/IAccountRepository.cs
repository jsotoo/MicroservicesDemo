using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Validator.Service.Domain.Entities;

namespace Microservices.Validator.Service.Domain.Interfaces
{
    public interface IAccountRepository: IMongoRepository<Account>
    {        
    }
}
