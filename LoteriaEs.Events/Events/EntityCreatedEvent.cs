namespace LoteriaEs.Events.Events
{
   
    public class EntityCreatedEvent<T> : EventBase<T> where T : class
    {
        public EntityCreatedEvent(T sender):base(sender)
        {
        }
    }
}
