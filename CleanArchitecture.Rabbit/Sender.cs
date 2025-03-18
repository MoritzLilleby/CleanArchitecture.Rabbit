using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RabbitMQ.Client;

namespace CleanArchitecture.Rabbit
{
    public class Sender
    {
        private readonly ConnectionFactory _factory;

        internal Sender(ConnectionFactoryWrapper connectionFactoryWrapper)
        {
            _factory = connectionFactoryWrapper.Factory;
        }

        public async Task Send(string message, string queue)
        {

            var connection = await _factory.CreateConnectionAsync();

            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queue, body: body);

        }

    }
}
