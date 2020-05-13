using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using YS.Knife;

namespace YS.KeyValue.Impl.Mongo
{
    public class ServiceRegister : IServiceRegister
    {
        public void RegisteServices(IServiceCollection services, IRegisteContext context)
        {
            //services.Configure<Settings>(options =>
            //{
            //    options.ConnectionString = configuration.GetSection("MongoConnection:ConnectionString").Value;
            //    options.Database = configuration.GetSection("MongoConnection:Database").Value;
            //});
        }
    }
}
