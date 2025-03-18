using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Rabbit
{
    internal class ConnectionFactoryWrapper
    {
        private readonly Lazy<ConnectionFactory> _factory;

        public ConnectionFactoryWrapper()
        {
            _factory = new Lazy<ConnectionFactory>(() => new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                Port = 5672
            });
        }

        public ConnectionFactory Factory => _factory.Value;
    }

}
