using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoteriaES.CQRS.Events
{
   
    public class EntityUpdatedEvent<T> : EventBase<T> where T : class
    {
        public EntityUpdatedEvent(T sender):base(sender)
        {
        }
    }
}
