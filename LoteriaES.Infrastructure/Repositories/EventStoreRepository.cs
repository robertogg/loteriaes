using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LoteriaES.Core;
using LoteriaES.Core.Bus;
using LoteriaES.Core.CQRS;

namespace LoteriaES.Infrastructure.Repositories
{
    public class EventStoreRepository<T>:IEventStoreRepository<T> where T:Entity, new()
    {
        private readonly IMessageBus _messageBus;
        private readonly IEventStore _eventStore;
        public EventStoreRepository(IMessageBus messageBus,IEventStore eventStore)
        {
            _messageBus = messageBus;
            _eventStore = eventStore;
        }

        public T Get(Guid id)
        {
            var data = new T();
            var events = _eventStore.GetEventsForAggregate(id).Result;
            data.LoadsFromHistory(events);
            return data;
        }

        public T Add(T entity)
        {
            _eventStore.SaveEvents(entity.Id, entity.GetUnpublishedEvents(), entity.Version).Wait();
            PublishEvents(entity);
            return entity;
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void PublishEvents(T entity)
        {
            foreach (var @event in entity.GetUnpublishedEvents())
            {
                _messageBus.Publish(@event);
            }
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
