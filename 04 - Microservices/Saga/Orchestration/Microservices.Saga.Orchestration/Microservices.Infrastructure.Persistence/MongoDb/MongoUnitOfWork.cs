using MongoDB.Driver;
using System.Collections;

namespace Microservices.Infrastructure.Persistence.MongoDb
{
    public abstract class MongoUnitOfWork : IMongoUnitOfWork
    {
        protected readonly IMongoDatabase _database;
        private readonly IClientSessionHandle _session;
        protected readonly MongoDbSettings _mongoDBSettings;


        public MongoUnitOfWork(MongoDbSettings mongoDBSettings)
        {
            _mongoDBSettings = mongoDBSettings;
            var client = new MongoClient(mongoDBSettings.ConnectionString);
            _database = client.GetDatabase(mongoDBSettings.DatabaseName);
            _session = _database.Client.StartSession();        
        }
        public async Task<bool> CommitAsync()
        {
            //try
            //{
            //    _session.StartTransaction();
            //    await _session.CommitTransactionAsync();
            //    return true;
            //}
            //catch (Exception)
            //{
            //    await _session.AbortTransactionAsync();
            //    return false;
            //}
            //finally
            //{
            //    _session.Dispose();
            //}
            return true;
        }        
    }
}
