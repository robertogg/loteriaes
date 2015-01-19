using LoteriaES.Core;
using LoteriaES.CQRS.Events;

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
        }

        private void Apply(EntityCreatedEvent<Order> @event)
        {
            Id = @event.Sender.Id;
            NumeroLoteria = @event.Sender.NumeroLoteria;
            Cantidad = @event.Sender.Cantidad;
        }
        public void UpdateOrder(string numeroLoteria, int cantidad)
        {
            ApplyChange(new EntityUpdatedEvent<Order>(new Order{Cantidad = cantidad,Id=this.Id,NumeroLoteria = numeroLoteria}));
        }
    }
}
