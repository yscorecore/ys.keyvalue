using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace YS.KeyValue
{
    public interface IKeyValueProvider
    {
        Task<T> GetByKey<T>([Required][CategoryRule]string category, [Required][KeyRule]string key) where T : class, new();

        Task AddOrUpdate<T>([Required][CategoryRule]string category, [Required][KeyRule]string key, [Required]T value) where T : class, new();

        Task<bool> DeleteByKey<T>([Required][CategoryRule]string category, [Required][KeyRule]string key) where T : class, new();

        Task<List<KeyValuePair<string, T>>> ListAll<T>([Required][CategoryRule]string category) where T : class, new();
    }
    
}
