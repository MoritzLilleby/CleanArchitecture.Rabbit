using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Rabbit
{
    public abstract class MessageHandlerBase
    {
        [ReceivedMessageHandler("default", "Handles received messages")]
        public abstract Task HandleMessageAsync(string message);
    }
}
