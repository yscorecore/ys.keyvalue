using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using YS.Knife;

namespace YS.KeyValue.Impl.Dynamo
{
    public class Test : YS.KeyValue.IKeyValueProvider
    {
        public Task AddOrUpdate<T>([CategoryRule, Required] string category, [KeyRule, Required] string key, [Required] T value) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByKey<T>([CategoryRule, Required] string category, [KeyRule, Required] string key) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByKey<T>([CategoryRule, Required] string category, [KeyRule, Required] string key) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<List<KeyValuePair<string, T>>> ListAll<T>([CategoryRule, Required] string category) where T : class, new()
        {
            throw new NotImplementedException();
        }
    }

}
