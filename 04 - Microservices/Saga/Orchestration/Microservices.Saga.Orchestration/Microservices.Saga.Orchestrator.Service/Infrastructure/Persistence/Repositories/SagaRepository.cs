using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Saga.Orchestrator.Service.Domain.Entities;
using Microservices.Saga.Orchestrator.Service.Domain.Interfaces;
using MongoDB.Driver;

namespace Microservices.Saga.Orchestrator.Service.Infrastructure.Persistence.Repositories
{
    public class SagaRepository : MongoRepository<Domain.Entities.Saga>, ISagaRepository
    {
        public SagaRepository(IMongoDatabase database, string collectionName) : base(database, collectionName)
        {
        }
    }
}
