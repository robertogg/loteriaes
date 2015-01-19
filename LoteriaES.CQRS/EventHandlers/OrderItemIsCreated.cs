using LoteriaES.Core.CQRS;
using LoteriaEs.Entities;
using LoteriaEs.Events.Events;

namespace LoteriaES.CQRS.EventHandlers
{
    public class OrderItemIsCreated:IEventHandler<EntityCreatedEvent<Order>>
    {
        public void Handle(EntityCreatedEvent<Order> domainEvent)
        {
           
        }
    }
}
