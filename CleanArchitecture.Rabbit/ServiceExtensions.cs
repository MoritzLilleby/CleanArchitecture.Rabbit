using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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

            services.AddSingleton<Sender>();
        }
    }
}
