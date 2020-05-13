using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace YS.KeyValue.Impl.Dynamo
{
    public class DynamoKeyValueProvider : IKeyValueProvider
    {
        public Task AddOrUpdate<T>(string category, string key, T value)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByKey<T>(string category, string key)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByKey<T>(string category, string key)
        {
            throw new NotImplementedException();
        }

        public Task<List<KeyValuePair<string, T>>> ListAll<T>(string category) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
