using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AVS.NerdStore.Core.Messages
{
    public abstract class Message
    {
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }
        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}