using System.Runtime.Serialization;
using LoteriaES.Core.CQRS;

namespace LoteriaES.CQRS.Events
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

        //protected EventBase(
        //    User user,
        //    T sender)
        //{
        //    User = user;
        //    Sender = sender;
        //}

        //[DataMember]
        //public User User { get; set; }

        //[DataMember]
        //public T Sender { get; set; }
    }
}
