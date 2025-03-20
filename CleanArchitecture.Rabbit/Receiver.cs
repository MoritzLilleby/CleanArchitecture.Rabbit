using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace CleanArchitecture.Rabbit
{
    public sealed class Receiver : IDisposable
    {
        
        private readonly ConnectionFactory _factory;
        private IConnection connection;
        private IChannel channel;

        public Receiver(ConnectionFactoryWrapper connectionFactoryWrapper)
        {
            _factory = connectionFactoryWrapper.Factory;
        }

        public void Dispose()
        {
            connection.Dispose();
            channel.Dispose();
        }

        public async Task Receive(string queue)
        {
            try
            {

                connection = await _factory.CreateConnectionAsync();

                channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var handlerTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => typeof(MessageHandlerBase)
                    .IsAssignableFrom(p) && !p.IsAbstract);

                    foreach (var handlerType in handlerTypes)
                    {
                        var handlerInstance = Activator.CreateInstance(handlerType);
                        var methodInfos = handlerType.GetMethods().Where(m => m.GetCustomAttributes(typeof(ReceivedMessageHandlerAttribute), false).Length > 0);

                        foreach (var methodInfo in methodInfos)
                        {
                            var attribute = (ReceivedMessageHandlerAttribute)methodInfo.GetCustomAttributes(typeof(ReceivedMessageHandlerAttribute), false).FirstOrDefault();
                            if (attribute != null && attribute.Queue == queue)
                            {
                                await (Task)methodInfo.Invoke(handlerInstance, new object[] { message });
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
