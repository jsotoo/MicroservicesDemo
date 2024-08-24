using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Validator.Service.Domain.Entities;
using Microservices.Validator.Service.Domain.Interfaces;
using MongoDB.Driver;

namespace Microservices.Validator.Service.Infrastructure.Data.Repositories
{
    public class AccountRepository : MongoRepository<Account>,  IAccountRepository
    {        
        public AccountRepository(IMongoDatabase database, string collectionName):base(database, collectionName)
        {
        }
    }
}
