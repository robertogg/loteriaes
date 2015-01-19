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
        Task SaveEvents(Guid aggregateId, IEnumerable<IEvent> events, int expectedVersion);
        Task<List<IEvent>> GetEventsForAggregate(Guid aggregateId);
    }
}
