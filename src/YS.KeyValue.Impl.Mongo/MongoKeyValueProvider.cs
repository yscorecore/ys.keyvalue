using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using MongoDB.Bson.Serialization;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using YS.Knife;

namespace YS.KeyValue.Impl.Mongo
{
    [ServiceClass(Lifetime = ServiceLifetime.Singleton)]
    public class MongoKeyValueProvider : IKeyValueProvider
    {
        public MongoKeyValueProvider(IOptions<MongoOptions> mongoOptions)
        {
            this.mongoOptions = mongoOptions;
            this.database = new Lazy<IMongoDatabase>(() =>
            {
                var mongoClient = new MongoClient(this.mongoOptions.Value.ConnectionString);
                return mongoClient.GetDatabase(this.mongoOptions.Value.DataBase);
            }, true);


        }
        private IOptions<MongoOptions> mongoOptions;
        private Lazy<IMongoDatabase> database;

        public async Task<T> GetByKey<T>(string category, string key)
        {
            var collectionName = NormalCategoryName(category);
            var collection = await EntryCollection<BsonDocument>(collectionName);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", key);
            var cursor = await collection.FindAsync(filter);
            var bson = cursor.FirstOrDefault();
            if (bson == null)
            {
                return default;
            }
            else
            {
                bson.Remove("_id");
                return BsonSerializer.Deserialize<T>(bson);
            }
        }

        public async Task AddOrUpdate<T>(string category, string key, T value)
        {
            var collectionName = NormalCategoryName(category);
            var collection = await EntryCollection<BsonDocument>(collectionName);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", key);
            var document = value.ToBsonDocument();
            document["_id"] = key;
            await collection.ReplaceOneAsync(filter, document, new ReplaceOptions { IsUpsert = true });
        }

        public async Task<bool> DeleteByKey<T>(string category, string key)
        {
            var collectionName = NormalCategoryName(category);
            var collection = await EntryCollection<BsonDocument>(collectionName);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", key);
            var deleteResult = await collection.DeleteOneAsync(filter);
            return deleteResult.DeletedCount > 0;

        }

        public async Task<List<KeyValuePair<string, T>>> ListAll<T>(string category) where T : class
        {
            var collectionName = NormalCategoryName(category);
            var collection = await EntryCollection<BsonDocument>(collectionName);
            var cursor = await collection.FindAsync(_ => true);
            var res = cursor.ToEnumerable().Select(bson =>
                new KeyValuePair<string, T>(bson["_id"].AsString, BsonSerializer.Deserialize<T>(bson)));
            return res.ToList();
        }
        private async Task<IMongoCollection<T>> EntryCollection<T>(string collectionName)
        {
            //TODO add to cache
            var nameInDatabase = database.Value.ListCollectionNames(new ListCollectionNamesOptions
            {
                Filter = new BsonDocument { { "name", collectionName } }
            });
            if (!nameInDatabase.Any())
            {
                await database.Value.CreateCollectionAsync(collectionName);
            }
            return database.Value.GetCollection<T>(collectionName);
        }

        private string NormalCategoryName(string category)
        {
            return category.Replace('.', '_');
        }


    }
}
