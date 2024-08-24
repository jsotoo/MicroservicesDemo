using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Infrastructure.Persistence.MongoDb
{
    public class MongoRepository<T> : IMongoRepository<T> where T : IEntity
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _collection.Find(entity => entity.Id == id).FirstOrDefaultAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var result = await _collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
            if (result.IsAcknowledged && result.ModifiedCount > 0)
            {
                return await _collection.Find(e => e.Id == entity.Id).FirstOrDefaultAsync();
            }
            else
            {
                throw new InvalidOperationException("Update operation failed or no document was updated.");
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _collection.DeleteOneAsync(entity => entity.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
