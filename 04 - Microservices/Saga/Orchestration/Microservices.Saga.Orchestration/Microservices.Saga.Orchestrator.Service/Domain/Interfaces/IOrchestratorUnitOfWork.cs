using Microservices.Infrastructure.Persistence.MongoDb;

namespace Microservices.Saga.Orchestrator.Service.Domain.Interfaces
{
    public interface IOrchestratorUnitOfWork : IMongoUnitOfWork
    {
        ISagaRepository SagaRepository { get; }
    }
}
