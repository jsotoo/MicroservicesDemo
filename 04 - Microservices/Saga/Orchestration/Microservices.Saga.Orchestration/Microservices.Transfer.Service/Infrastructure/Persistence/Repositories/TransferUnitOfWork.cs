using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Transfer.Service.Domain.Entities;
using Microservices.Transfer.Service.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.Collections;

namespace Microservices.Transfer.Service.Infrastructure.Data.Repositories
{
    public class TransferUnitOfWork : MongoUnitOfWork, ITransferUnitOfWork
    {        
        private ITransferRepository _transferRepository;

        public TransferUnitOfWork(MongoDbSettings mongoDBSettings):base(mongoDBSettings)
        {
            _transferRepository = new TransferRepository(_database, mongoDBSettings.CollectionName);
        }
        public ITransferRepository TransferRepository => _transferRepository;
               
    }
}
