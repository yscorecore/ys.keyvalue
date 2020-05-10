using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YS.KeyValue
{
    public interface IKeyValueService<T>
    {
        Task<T> GetByKey(string key);
        Task<List<KeyValuePair<string, T>>> ListAll();
    }
}
