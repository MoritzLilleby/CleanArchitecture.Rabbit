using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Rabbit
{
    public class AnotherMyMessageHandler : IMessageHandlerBase
    {
        private readonly ILogger<AnotherMyMessageHandler> logger;
        public AnotherMyMessageHandler(ILogger<AnotherMyMessageHandler> logger)
        {
            this.logger=logger;
        }

        [ReceivedMessageHandler("description", "hello")]
        public async Task HandleMessageAsync(string message)
        {
            logger.LogInformation($" [x] Received {message} on queue hello");
            // Process the message here
            await Task.CompletedTask;
        }
    
    }
}
