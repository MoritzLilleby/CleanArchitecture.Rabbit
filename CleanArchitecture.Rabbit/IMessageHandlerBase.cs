using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Rabbit
{
    public interface IMessageHandlerBase
    {
        [ReceivedMessageHandler("default", "Handles received messages")]
        public Task HandleMessageAsync(string message);
    }
}
