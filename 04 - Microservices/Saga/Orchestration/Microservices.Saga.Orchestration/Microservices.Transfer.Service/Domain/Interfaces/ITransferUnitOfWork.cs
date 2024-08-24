using Microservices.Infrastructure.Persistence.MongoDb;

namespace Microservices.Transfer.Service.Domain.Interfaces
{
    public interface ITransferUnitOfWork : IMongoUnitOfWork
    {
        ITransferRepository TransferRepository { get; }        
    }
}
