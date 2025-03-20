using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Rabbit
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ReceivedMessageHandlerAttribute : Attribute
    {
        public string Description { get; }
        public string Queue { get; }

        public ReceivedMessageHandlerAttribute(string description, string queue)
        {
            Description = description;
            Queue=queue;
        }
    }
}
