using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoteriaES.CQRS.Events
{
   
    public class EntityCreatedEvent<T> : EventBase<T> where T : class
    {
        public EntityCreatedEvent(T sender):base(sender)
        {
        }
    }
}
