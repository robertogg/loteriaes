using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoteriaES.Core.CQRS;
using LoteriaES.CQRS.Events;
using LoteriaEs.Entities;

namespace LoteriaES.CQRS.EventHandlers
{
    public class OrderItemIsCreated:IEventHandler<EntityCreatedEvent<Order>>
    {
        public void Handle(EntityCreatedEvent<Order> domainEvent)
        {
           
        }
    }
}
