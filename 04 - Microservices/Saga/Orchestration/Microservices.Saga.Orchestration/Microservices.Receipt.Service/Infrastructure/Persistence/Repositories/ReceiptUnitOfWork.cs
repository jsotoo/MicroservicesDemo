using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Receipt.Service.Domain.Entities;
using Microservices.Receipt.Service.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.Collections;

namespace Microservices.Receipt.Service.Infrastructure.Persistence.Repositories
{
    public class ReceiptUnitOfWork : MongoUnitOfWork, IReceiptUnitOfWork
    {        
        private IReceiptRepository _transferRepository;

        public ReceiptUnitOfWork(MongoDbSettings mongoDBSettings):base(mongoDBSettings)
        {
            _transferRepository = new ReceiptRepository(_database, mongoDBSettings.CollectionName);
        }
        public IReceiptRepository ReceiptRepository => _transferRepository;
               
    }
}
