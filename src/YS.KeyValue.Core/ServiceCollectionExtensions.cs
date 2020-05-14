using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using YS.KeyValue;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKeyValue(this IServiceCollection serviceCollection,Action<KeyValueOptions> configOptions=null)
        {
            serviceCollection.TryAddSingleton<IKeyValueFactory, KeyValueFactory>();
            serviceCollection.TryAddSingleton(typeof(IKeyValueService<>), typeof(KeyValueFactory.TypedKeyValueService<>));
            if (configOptions != null)
            {
                serviceCollection.Configure(configOptions);
            }
            return serviceCollection;
        }
    }
}
