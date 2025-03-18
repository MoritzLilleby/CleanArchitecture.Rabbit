using Microsoft.Extensions.DependencyInjection;
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

            //services.AddSingleton<IConnectionFactory>(sp => new ConnectionFactory
            //{
            //    HostName = "localhost",
            //    UserName = "guest",
            //    Password = "guest"
            //});

            services.AddHostedService<Receiver>();

            //services.Configure<RabbitOptions>(configuration.GetSection("Rabbit"));
        }

        public static void AddRabbitSender(this IServiceCollection services/*, IConfiguration configuration*/)
        {

            services.AddSingleton<IRabbitSenderProgram, Sender>();

            //services.Configure<RabbitOptions>(configuration.GetSection("Rabbit"));

        }
    }
}
