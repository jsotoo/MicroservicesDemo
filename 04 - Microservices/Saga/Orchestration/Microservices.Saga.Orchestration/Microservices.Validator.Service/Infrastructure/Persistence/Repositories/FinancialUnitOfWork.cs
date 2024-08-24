using Microservices.Infrastructure.Persistence.MongoDb;
using Microservices.Validator.Service.Domain.Entities;
using Microservices.Validator.Service.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.Collections;

namespace Microservices.Validator.Service.Infrastructure.Data.Repositories
{
    public class FinancialUnitOfWork : MongoUnitOfWork, IFinancialUnitOfWork
    {        
        private IAccountRepository _accountRepository;

        public FinancialUnitOfWork(MongoDbSettings mongoDBSettings):base(mongoDBSettings)
        {
            _accountRepository = new AccountRepository(_database, mongoDBSettings.CollectionName);
        }
        public IAccountRepository AccountRepository => _accountRepository;
       
        public async Task EnsureSeedDataForUnitOfWorkAsync()
        {
            _database.DropCollection(_mongoDBSettings.CollectionName);
            var accounts = new List<Account>
            {
                new Account(Guid.Parse("55dd72a4-bafb-4da3-bd21-7f6f11ffdf30"), "Propietario 1", 1000, true),
                new Account(Guid.Parse("f8b09e51-2959-4c88-ba6a-76dcbbb01aca"), "Propietario 2", 2000, true),
                new Account(Guid.Parse("c49dc503-522f-455a-b712-f1c06035c033"), "Propietario 3", 3000, true)
            };

            foreach (var account in accounts)
            {
                await _accountRepository.CreateAsync(account);                
            }

            await CommitAsync();
        }
    }
}
