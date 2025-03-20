using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Rabbit
{
    public class AnotherMyMessageHandler : MessageHandlerBase
    {
        [ReceivedMessageHandler("description", "hello")]
        public override async Task HandleMessageAsync(string message)
        {
            Console.WriteLine($" [x] Received {message} on queue hello");
            // Process the message here
            await Task.CompletedTask;
        }
    }
}
