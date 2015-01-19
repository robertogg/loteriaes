using LoteriaES.Core;
using LoteriaEs.Events.Events;

namespace LoteriaEs.Entities
{
    public class Order:Entity
    {
        public string NumeroLoteria { get; set; }
        public int Cantidad { get; set; }

        private void Apply(EntityUpdatedEvent<Order> @event)
        {
            NumeroLoteria = @event.Sender.NumeroLoteria;
            Cantidad = @event.Sender.Cantidad;
            Version = @event.Sender.Version;
        }

        private void Apply(EntityCreatedEvent<Order> @event)
        {
            Id = @event.Sender.Id;
            NumeroLoteria = @event.Sender.NumeroLoteria;
            Cantidad = @event.Sender.Cantidad;
            Version = @event.Sender.Version;
        }
        public void UpdateOrder(string numeroLoteria, int cantidad,int version)
        {
            ApplyChange(new EntityUpdatedEvent<Order>(
                new Order
                {
                    Cantidad = cantidad,
                    Id=this.Id,
                    NumeroLoteria = numeroLoteria,
                    Version=version
                }));
        }
    }
}
