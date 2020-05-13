using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using YS.KeyValue;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKeyValue(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IKeyValueFactory, KeyValueFactory>();
            serviceCollection.TryAddSingleton(typeof(IKeyValueService<>), typeof(KeyValueFactory.TypedKeyValueService<>));
            return serviceCollection;
        }


    }
}
