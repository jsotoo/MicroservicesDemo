using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Saga.Orchestrator.Service.Domain.Interfaces;
using MongoDB.Driver;
using System.Collections;

namespace Microservices.Saga.Orchestrator.Service.Infrastructure.Persistence.Repositories
{
    public class OrchestratorUnitOfWork : MongoUnitOfWork, IOrchestratorUnitOfWork
    {
        private ISagaRepository _sagaRepository;

        public OrchestratorUnitOfWork(MongoDbSettings mongoDBSettings) : base(mongoDBSettings)
        {
            _sagaRepository = new SagaRepository(_database, mongoDBSettings.CollectionName);
        }
        public ISagaRepository SagaRepository => _sagaRepository;        
    }
}
