using System.Collections.Generic;
using System.Threading.Tasks;

namespace YS.KeyValue
{
    public interface IKeyValueProvider
    {
        Task<T> GetByKey<T>(string key, string category);
        Task<List<KeyValuePair<string, T>>> ListAll<T>(string category);
    }
}
