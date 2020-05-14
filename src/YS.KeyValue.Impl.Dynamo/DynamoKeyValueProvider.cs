using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using YS.Knife;

namespace YS.KeyValue.Impl.Dynamo
{
    [ServiceClass(Lifetime = ServiceLifetime.Singleton)]
    public class DynamoKeyValueProvider : IKeyValueProvider
    {
        public DynamoKeyValueProvider(IOptions<DynamoOptions> options)
        {
            var dynamoOptions = options.Value;
            var clientConfig = new AmazonDynamoDBConfig()
            {
                ServiceURL = dynamoOptions.ServiceUrl
            };
            this.client = new AmazonDynamoDBClient(clientConfig);
            this.dbContext = new DynamoDBContext(client);

        }
        private readonly DynamoDBContext dbContext;
        private readonly AmazonDynamoDBClient client;
        public async Task AddOrUpdate<T>(string category, string key, T value)
              where T : class,new()
        {
            var table = await EntryTable(category);
            var document = dbContext.ToDocument(value);
            document["__Id"] = key;

            await table.PutItemAsync(document);
        }

        public Task<bool> DeleteByKey<T>(string category, string key)
            where T : class, new()
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetByKey<T>(string category, string key)
               where T : class, new()
        {
            var table = await EntryTable(category);
            var item = await table.GetItemAsync(key);
            if (item == null) return default;
            item.Remove("__Id");
            return dbContext.FromDocument<T>(item);
        }

        public Task<List<KeyValuePair<string, T>>> ListAll<T>(string category) where T :  class, new()
        {
            throw new NotImplementedException();
        }

        private async Task<Table> EntryTable(string tableName)
        {
            if (Table.TryLoadTable(client, tableName, out var table))
            {
                return table;
            }
            var createTableRequest = new CreateTableRequest(tableName,
                new List<KeySchemaElement>
                {
                     new KeySchemaElement
                    {
                        AttributeName = "__Id",
                        KeyType = "HASH"
                    },
                });
            createTableRequest.AttributeDefinitions = new List<AttributeDefinition>
                {
                 new AttributeDefinition
                    {
                        AttributeName = "__Id",
                        AttributeType = ScalarAttributeType.S,
                    }
                };
            createTableRequest.ProvisionedThroughput = new ProvisionedThroughput(200, 200);
            var response = await client.CreateTableAsync(createTableRequest);
            if (response.TableDescription != null && response.TableDescription.TableName == tableName)
            {
                return Table.LoadTable(client, tableName);
            }
            else
            {
                throw new Exception("Create dynamodb table error.");
            }
        }
    }
}
