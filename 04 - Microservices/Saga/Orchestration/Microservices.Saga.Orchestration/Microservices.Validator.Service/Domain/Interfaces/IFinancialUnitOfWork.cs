using Microservices.Infrastructure.Persistence.MongoDb;

namespace Microservices.Validator.Service.Domain.Interfaces
{
    public interface IFinancialUnitOfWork:IMongoUnitOfWork
    {
        IAccountRepository AccountRepository { get; }        
    }
}
