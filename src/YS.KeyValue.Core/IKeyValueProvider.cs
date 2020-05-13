using System.Collections.Generic;
using System.Threading.Tasks;

namespace YS.KeyValue
{
    public interface IKeyValueProvider
    {
        Task<T> GetByKey<T>(string category, string key);

        Task AddOrUpdate<T>(string category, string key, T value);

        Task<bool> DeleteByKey<T>(string category, string key);

        Task<List<KeyValuePair<string, T>>> ListAll<T>(string category) where T : class;
    }
}
