using CleanArchitecture.Rabbit;


Sender sender = new Sender(new ConnectionFactoryWrapper());

while (true)
{
    Console.WriteLine("Enter message to send:");
    string message = Console.ReadLine();
    Console.WriteLine("Enter queue to send to:");
    string queue = Console.ReadLine();
    await sender.Send(message, queue);
    Console.WriteLine("Message sent.");
}