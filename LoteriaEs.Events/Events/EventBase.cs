using System.Runtime.Serialization;
using LoteriaES.Core.CQRS;

namespace LoteriaEs.Events.Events
{
    [DataContract]
    public abstract class EventBase<T> : IEvent where T : class
    {
        protected EventBase(T sender)
        {
            Sender = sender;
        }
        [DataMember]
        public T Sender { get; set; }
    }
}
