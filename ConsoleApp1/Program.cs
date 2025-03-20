// See https://aka.ms/new-console-template for more information
using CleanArchitecture.Rabbit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
         .ConfigureServices((context, services) =>
         {
             services.AddRabbitReceiver();
             services.AddRabbitSender();
         })
         .Build();

var receiver = host.Services.GetRequiredService<Receiver>();
var sender = host.Services.GetRequiredService<Sender>();

//Subscribe to the queue
await receiver.Receive("hello");
await receiver.Receive("hi");

//Send a message
for (int i = 0; i < 10; i++)
{
    await sender.Send($"Message {i}", "hi");
    await Task.Delay(3000);
    await sender.Send($"Message {i}", "hello");
}

await host.RunAsync();
