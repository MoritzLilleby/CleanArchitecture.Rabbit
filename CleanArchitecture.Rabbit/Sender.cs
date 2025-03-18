using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RabbitMQ.Client;

namespace CleanArchitecture.Rabbit
{
    public interface IRabbitSenderProgram
    {
        Task Send(string message);
    }
    public class Sender : IRabbitSenderProgram
    {
        public async Task Send(string message)
        {

            var factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

            //const string message = "Hello World!";
            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "hello", body: body);

        }

    }
}
