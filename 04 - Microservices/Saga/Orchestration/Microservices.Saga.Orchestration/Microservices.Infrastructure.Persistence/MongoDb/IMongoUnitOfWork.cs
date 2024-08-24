namespace Microservices.Infrastructure.Persistence.MongoDb
{
    public interface IMongoUnitOfWork
    {        
        Task<bool> CommitAsync();
    }
}
