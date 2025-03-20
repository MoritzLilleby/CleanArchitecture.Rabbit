using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Rabbit
{
    public class MyMessageHandler : IMessageHandlerBase
    {
        private readonly ILogger<MyMessageHandler> logger;

        public MyMessageHandler(ILogger<MyMessageHandler> logger)
        {
            this.logger=logger;
        }

        [ReceivedMessageHandler("description", "hi")]
        public async Task HandleMessageAsync(string message)
        {
            this.logger.LogInformation($" [x] Received {message} from queue hi");
            // Process the message here
            await Task.CompletedTask;
        }

        [ReceivedMessageHandler("description", "hello")]
        public async Task HandleMessageAsync2(string message)
        {
            this.logger.LogInformation($" [x] Received {message} from queue hello");
            // Process the message here
            await Task.CompletedTask;
        }
    }
}
