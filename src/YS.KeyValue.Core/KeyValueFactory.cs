using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace YS.KeyValue
{
    public class KeyValueFactory : IKeyValueFactory
    {
        public KeyValueFactory(IOptions<KeyValueOptions> options, IKeyValueProvider keyValueProvider)
        {
            this.keyValueOptions = options;
            this.keyValueProvider = keyValueProvider;
        }
        private readonly IOptions<KeyValueOptions> keyValueOptions;
        private readonly IKeyValueProvider keyValueProvider;
        public IKeyValueService<T> CreateKeyValue<T>(string name)
            where T : class
        {
            return new KeyValueService<T>(keyValueOptions, name, keyValueProvider);
        }

        internal class KeyValueService<T> : IKeyValueService<T>
            where T : class
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
                return this.keyValueProvider.GetByKey<T>(this.categoryName, key);
            }

            public Task<List<KeyValuePair<string, T>>> ListAll()

            {
                return this.keyValueProvider.ListAll<T>(this.categoryName);
            }
        }

        internal class TypedKeyValueService<T> : KeyValueService<T>
            where T : class
        {
            public TypedKeyValueService(IOptions<KeyValueOptions> options, IKeyValueProvider keyValueProvider) :
                base(options, typeof(T).FullName, keyValueProvider)
            {
            }
        }

    }




}
