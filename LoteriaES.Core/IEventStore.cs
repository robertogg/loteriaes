using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoteriaES.Core.CQRS;

namespace LoteriaES.Core
{
    public interface IEventStore
    {
        void SaveEvents(int aggregateId, IEnumerable<IEvent> events, int expectedVersion);
        List<IEvent> GetEventsForAggregate(int aggregateId);
    }
}
