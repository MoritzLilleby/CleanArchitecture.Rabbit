using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;

namespace CleanArchitecture.Rabbit
{
    public class Receiver : IHostedService
    {
        //private readonly IConnectionFactory _connectionFactory;

        //public RabbitProgram(IConnectionFactory connectionFactory)
        //{
        //    _connectionFactory = connectionFactory;
        //}

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Receive();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Clean up resources if needed
            return Task.CompletedTask;
        }


        public async Task Receive()
        {
            try
            {
                var factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest" };
                var connection = await factory.CreateConnectionAsync();
                var channel = await connection.CreateChannelAsync();

                //using var connection = await _connectionFactory.CreateConnectionAsync();
                //using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

                Console.WriteLine(" [*] Waiting for messages.");

                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.ReceivedAsync += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($" [x] Received {message}");
                    return Task.CompletedTask;
                };

                await channel.BasicConsumeAsync("hello", autoAck: true, consumer: consumer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Implement retry logic or other error handling as needed
            }
        }
    }
}
