using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoteriaES.Core.CQRS;

namespace LoteriaES.Core
{
    public abstract class Entity
    {
        private readonly List<IEvent> _events = new List<IEvent>();

        public int Id { get; set; }

        public IEnumerable<IEvent> GetUnpublishedEvents()
        {
            return _events;
        }

        public void MarkEventsAsPublished()
        {
            _events.Clear();
        }

        public void ApplyEvent<T>(T @event) where T : IEvent
        {
            _events.Add(@event);
        }
        public void LoadsFromHistory(IEnumerable<IEvent> eventsHistory)
        {
            foreach (var e in eventsHistory)
            {
                ApplyChange(e);
            }
        }
        protected void ApplyChange(IEvent @event)
        {
            this.AsDynamic().Apply(@event);
        }
        //private void ApplyChange(IEvent @event, bool isNew)
        //{
            
           
        //}
    }
}
