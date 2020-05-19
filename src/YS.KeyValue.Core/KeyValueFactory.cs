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
            where T : class, new()
        {
            return new KeyValueService<T>(keyValueOptions, name, keyValueProvider);
        }

        internal class KeyValueService<T> : IKeyValueService<T>
            where T : class, new()
        {
            private readonly IKeyValueProvider keyValueProvider;
            private readonly string categoryName;
            public KeyValueService(IOptions<KeyValueOptions> options, string categoryName, IKeyValueProvider keyValueProvider)
            {
                this.categoryName = CombinCategoryName(options, categoryName);
                this.keyValueProvider = keyValueProvider;
            }

            public Task AddOrUpdate(string key, T value)
            {
                return keyValueProvider.AddOrUpdate(this.categoryName,key,value);
            }

            public Task<bool> DeleteByKey(string key)
            {
                return keyValueProvider.DeleteByKey<T>(this.categoryName,key);
            }

            public Task<T> GetByKey(string key)
            {
                return this.keyValueProvider.GetByKey<T>(this.categoryName, key);
            }

            public Task<List<KeyValuePair<string, T>>> ListAll()
            {
                return keyValueProvider.ListAll<T>(this.categoryName);
            }
            private string CombinCategoryName(IOptions<KeyValueOptions> options, string categoryName)
            {
                var categoryPrefix = options.Value.CategoryPrefix;
                return string.IsNullOrEmpty(categoryPrefix) ?
                    categoryName :
                    $"{categoryPrefix}.{categoryName}";
            }
        }

        internal class TypedKeyValueService<T> : KeyValueService<T>
            where T : class, new()
        {
            public TypedKeyValueService(IOptions<KeyValueOptions> options, IKeyValueProvider keyValueProvider) :
                base(options, GetTypeCategoryName(options,typeof(T)), keyValueProvider)
            {
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1308:将字符串规范化为大写", Justification = "<挂起>")]
            public static string GetTypeCategoryName(IOptions<KeyValueOptions> options,Type type)
            {
                var nameStyle = options.Value.TypedCategoryNameStyle;
                if (type.IsNested)
                {
                    throw new NotSupportedException("The entity type can not be a nested type.");
                }
                if (type.IsGenericType)
                {
                    throw new NotSupportedException("The entity type can not be a generic type.");
                }
                var catetoryName = type.FullName;
                switch (nameStyle)
                {
                    case NameStyle.Lower:
                        return catetoryName.ToLowerInvariant();
                    case NameStyle.Upper:
                        return type.FullName.ToUpperInvariant();
                    default:
                        return catetoryName;
                }
               
            }
        }



    }




}
