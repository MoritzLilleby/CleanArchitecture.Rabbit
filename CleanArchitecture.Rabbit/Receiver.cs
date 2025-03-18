using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace CleanArchitecture.Rabbit
{
    public class Receiver
    {
        private readonly ConnectionFactory _factory;

        internal Receiver(ConnectionFactoryWrapper connectionFactoryWrapper) 
        {
            _factory = connectionFactoryWrapper.Factory;
        }

        public async Task Receive(string queue)
        {
            try
            {
              
                var connection = await _factory.CreateConnectionAsync();

                var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    Console.WriteLine($" [x] Received {message}");

                    return Task.CompletedTask;
                };

                await channel.BasicConsumeAsync(queue, autoAck: true, consumer: consumer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
