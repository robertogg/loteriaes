using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoteriaES.Core;
using LoteriaES.Core.Bus;
using LoteriaES.Core.CQRS;

namespace LoteriaES.Infrastructure.Repositories
{
    public class Repository<T>:IRepository<T> where T:Entity, new()
    {
        private readonly IMessageBus _messageBus;
        private readonly IEventStore _eventStore;
        public Repository(IMessageBus messageBus,IEventStore eventStore)
        {
            _messageBus = messageBus;
            _eventStore = eventStore;
        }
        
        public IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
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
    }
}
