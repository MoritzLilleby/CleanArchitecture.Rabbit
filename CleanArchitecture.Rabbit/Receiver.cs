using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

namespace CleanArchitecture.Rabbit
{
    public sealed class Receiver : IDisposable
    {

        private readonly ConnectionFactory _factory;
        private readonly IEnumerable<IMessageHandlerBase> _messageHandlers;

        private IConnection connection;
        private Dictionary<string, IChannel> channels = new();

        public Receiver(
            ConnectionFactoryWrapper connectionFactoryWrapper,
            IEnumerable<IMessageHandlerBase> messageHandlers
            )
        {
            _factory = connectionFactoryWrapper.Factory;
            _messageHandlers=messageHandlers;
        }

        public void Dispose()
        {
            connection.Dispose();
            foreach (var item in channels.Values)
            {
                item.Dispose();
            }
        }

        public async Task Receive(string queue)
        {
            try
            {

                connection = await _factory.CreateConnectionAsync();
                var found = channels.TryGetValue(queue, out IChannel? channel);

                if (!found)
                {
                    channel = await connection.CreateChannelAsync();
                    await channel!.QueueDeclareAsync(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    channels.Add(queue, channel);   
                }

                var consumer = new AsyncEventingBasicConsumer(channel!);

                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    foreach (var handler in _messageHandlers)
                    {

                        var methodInfos = handler.GetType().GetMethods().Where(m => m.GetCustomAttributes(typeof(ReceivedMessageHandlerAttribute), false).Length > 0);

                        foreach (var methodInfo in methodInfos)
                        {
                            var attribute = (ReceivedMessageHandlerAttribute)methodInfo.GetCustomAttributes(typeof(ReceivedMessageHandlerAttribute), false).FirstOrDefault();
                            if (attribute != null && attribute.Queue == queue)
                            {
                                await (Task)methodInfo.Invoke(handler, new object[] { message });
                            }
                        }
                    }

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
