using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace YS.KeyValue.Impl.Dynamo
{
    public class DynamoKeyValueProvider : IKeyValueProvider
    {
        public Task<T> GetByKey<T>(string id, string category)
        {
            throw new NotImplementedException();
        }

        public Task<List<KeyValuePair<string, T>>> ListAll<T>(string category)
        {
            throw new NotImplementedException();
        }
    }
}
