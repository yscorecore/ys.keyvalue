using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using YS.Knife;

namespace YS.KeyValue
{
    public class ServiceRegister : IServiceRegister
    {
        public void RegisteServices(IServiceCollection services, IRegisteContext context)
        {
            services.AddKeyValue();
        }
    }
}
