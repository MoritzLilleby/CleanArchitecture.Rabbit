using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Rabbit
{
    public class MyMessageHandler : MessageHandlerBase
    {
        [ReceivedMessageHandler("description", "hi")]
        public override async Task HandleMessageAsync(string message)
        {
            Console.WriteLine($" [x] Received {message} from queue hi");
            // Process the message here
            await Task.CompletedTask;
        }
    }
}
