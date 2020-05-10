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
        {
            return new KeyValueService<T>(keyValueOptions, name, keyValueProvider);
        }



    }




}
