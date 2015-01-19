namespace LoteriaEs.Events.Events
{
   
    public class EntityUpdatedEvent<T> : EventBase<T> where T : class
    {
        public EntityUpdatedEvent(T sender):base(sender)
        {
        }
    }
}
