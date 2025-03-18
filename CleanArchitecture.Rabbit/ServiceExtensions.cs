using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Rabbit
{
    public static class ServiceExtensions
    {
        public static void AddRabbitReceiver(this IServiceCollection services /*, IConfiguration configuration*/)
        {
            services.TryAddSingleton<ConnectionFactoryWrapper>();

            services.AddSingleton<Receiver>();
        }

        public static void AddRabbitSender(this IServiceCollection services/*, IConfiguration configuration*/)
        {
            services.TryAddSingleton<ConnectionFactoryWrapper>();

            services.AddSingleton<IRabbitSender, Sender>();
        }
    }
}
