using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Saga.Orchestrator.Service.Domain.Entities;

namespace Microservices.Saga.Orchestrator.Service.Domain.Interfaces
{
    public interface ISagaRepository : IMongoRepository<Entities.Saga>
    {
    }
}
