using Hamburger.Core.Configuration;
using Hamburger.Core.PersistentStorage;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hamburger.MongoStorage
{
    public class MongoDbStorage : IDbStorage
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _db;

        public MongoDbStorage(IConfiguration config)
        {
            _client = new MongoClient(config.MongoConnectionString);
            _db = _client.GetDatabase("Hamburger");
        }

        public async Task<T> StoreOne<T>(T item, string path)
        {
            var collection = _db.GetCollection<T>(path);

            await collection.InsertOneAsync(item);

            return item;
        }

        public async Task<IEnumerable<T>> StoreMany<T>(IEnumerable<T> items, string path)
        {
            var collection = _db.GetCollection<T>(path);

            await collection.InsertManyAsync(items);

            return items;
        }

        public async Task<T> UpdateOne<T>(T item, Expression<Func<T, bool>> predicate, string path)
        {
            var collection = _db.GetCollection<T>(path);

            await collection.ReplaceOneAsync(predicate, item);

            return item;
        }

        public async void DeleteOne<T>(Expression<Func<T, bool>> predicate, string path)
        {
            var collection = _db.GetCollection<T>(path);

            await collection.DeleteOneAsync(predicate);
        }

        public async void DeleteMany<T>(Expression<Func<T, bool>> predicate, string path)
        {
            var collection = _db.GetCollection<T>(path);

            await collection.DeleteManyAsync(predicate);
        }

        public async Task<T> FindOne<T>(Expression<Func<T, bool>> predicate, string path)
        {
            var collection = _db.GetCollection<T>(path);

            var result = await collection.FindAsync(predicate);

            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<T>> FindMany<T>(Expression<Func<T, bool>> predicate, string path)
        {
            var collection = _db.GetCollection<T>(path);

            var results = await collection.FindAsync(predicate);

            return results.ToEnumerable();
        }

        public bool Exists<T>(Expression<Func<T, bool>> predicate, string path)
        {
            var collection = _db.GetCollection<T>(path);

            var result = collection.FindAsync(predicate);
            var isNull = result.Result.FirstOrDefault().Equals(null);

            if (isNull) return false;
            return true;
        }

        public async Task<bool> CollectionExistsAsync(string path)
        {
            var filter = new BsonDocument("name", path);
            var collections = await _db.ListCollectionsAsync(new ListCollectionsOptions { Filter = filter });
            return await collections.AnyAsync();
        }

        public async Task DeleteCollectionAsync(string path)
        {
            await _db.DropCollectionAsync(path);
        }
    }
}
