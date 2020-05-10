using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace YS.KeyValue
{
    public class KeyValueService<T> : IKeyValueService<T>
    {
        private readonly IKeyValueProvider keyValueProvider;
        private readonly string categoryName;
        public KeyValueService(IOptions<KeyValueOptions> options, string categoryName, IKeyValueProvider keyValueProvider)
        {
            this.categoryName = categoryName;
            this.keyValueProvider = keyValueProvider;
        }
        public Task<T> GetByKey(string key)
        {
            return this.keyValueProvider.GetByKey<T>(key, this.categoryName);
        }

        public Task<List<KeyValuePair<string, T>>> ListAll()
        {
            return this.keyValueProvider.ListAll<T>(this.categoryName);
        }
    }
}
