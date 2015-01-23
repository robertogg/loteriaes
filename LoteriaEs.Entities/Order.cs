using System;
using System.Collections.Generic;
using System.Linq;
using LoteriaES.Core;
using LoteriaEs.Events.Events;

namespace LoteriaEs.Entities
{
    public class Order:Entity
    {
        private readonly List<OrderLine> _orderLines= new List<OrderLine>();

        private void Apply(EntityUpdatedEvent<OrderLine> @event)
        {
            var currentOrderLine = _orderLines.Find(data => data.Id == @event.Sender.Id);
            currentOrderLine.Cantidad = @event.Sender.Cantidad;
            Version = @event.Sender.Version;
        }

        private void Apply(EntityCreatedEvent<Order> @event)
        {
            Id = @event.Sender.Id;
            Version = @event.Sender.Version;
        }
        private void Apply(EntityCreatedEvent<OrderLine> @event)
        {
            var orderLine = new OrderLine
            {
                OrderId = @event.Sender.OrderId,
                Cantidad = @event.Sender.Cantidad,
                Id = @event.Sender.Id,
                NumeroLoteria = @event.Sender.NumeroLoteria,
                Version = @event.Sender.Version
            };
            _orderLines.Add(orderLine);
        }
        public void UpdateOrder(Guid idOrderLine, int cantidad)
        {
            var orderLine = new OrderLine
            {
                Cantidad = cantidad,
                Id = idOrderLine,
                Version = _orderLines.Find(data=>data.Id==idOrderLine).Version
            };
            ApplyChange(new EntityUpdatedEvent<OrderLine>(orderLine));
        }

        public OrderLine GetOrderDetail(Guid orderLineId)
        {
            var indexItem = _orderLines.FindIndex(data => data.Id == orderLineId);
            return _orderLines[indexItem];
        }
    }
}
