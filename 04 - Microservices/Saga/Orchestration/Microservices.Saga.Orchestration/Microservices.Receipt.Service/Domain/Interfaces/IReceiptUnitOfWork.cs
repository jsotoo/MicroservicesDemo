using Microservices.Infrastructure.Persistence.MongoDb;

namespace Microservices.Receipt.Service.Domain.Interfaces
{
    public interface IReceiptUnitOfWork : IMongoUnitOfWork
    {
        IReceiptRepository ReceiptRepository { get; }        
    }
}
