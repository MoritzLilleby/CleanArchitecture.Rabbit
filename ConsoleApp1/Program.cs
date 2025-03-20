// See https://aka.ms/new-console-template for more information
using CleanArchitecture.Rabbit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
         .ConfigureServices((context, services) =>
         {
             services.AddSingleton<IMessageHandlerBase, AnotherMyMessageHandler>();
             services.AddSingleton<IMessageHandlerBase, MyMessageHandler>();

             services.AddRabbitReceiver();
             services.AddRabbitSender();


         })
         .Build();

var receiver = host.Services.GetRequiredService<Receiver>();
var sender = host.Services.GetRequiredService<Sender>();

//Subscribe to the queue
await receiver.Receive("hello");

//Send a message
for (int i = 0; i < 10; i++)
{
    await sender.Send($"Message {i}", "hello");
}

await receiver.Receive("hi");

for (int i = 0; i < 10; i++)
{
    await sender.Send($"Message {i}", "hi");
}


await host.RunAsync();

