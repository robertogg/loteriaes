using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoteriaES.Core;
using LoteriaES.Core.CQRS;
using LoteriaES.CQRS.Events;
using LoteriaEs.Entities;

namespace LoteriaES.Infrastructure.EventStore
{
    public class EventStore:IEventStore
    {
        private struct EventDescriptor
        {

            public readonly IEvent EventData;
            public readonly int Id;
            public readonly int Version;

            public EventDescriptor(int id, IEvent eventData, int version)
            {
                EventData = eventData;
                Version = version;
                Id = id;
            }
        }

        public void SaveEvents(int aggregateId, IEnumerable<IEvent> events, int expectedVersion)
        {
            
        }

        public List<IEvent> GetEventsForAggregate(int aggregateId)
        {
            List<EventDescriptor> eventDescriptors;
            //Mock
            eventDescriptors = new List<EventDescriptor>()
            {
                new EventDescriptor(aggregateId,new EntityCreatedEvent<Order>(new Order {Id = 1,Cantidad = 1,NumeroLoteria = "00000"}),0),
                new EventDescriptor(aggregateId,new EntityUpdatedEvent<Order>(new Order {Id = 1,Cantidad = 5,NumeroLoteria = "00000"}),1)
            };
            return eventDescriptors.Select(desc => desc.EventData).ToList();
        }
    }
}
