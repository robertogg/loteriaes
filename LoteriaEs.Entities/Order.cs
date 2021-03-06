﻿using System;
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
            currentOrderLine.Quantity = @event.Sender.Quantity;
            Version = @event.Sender.Version;
        }

        private void Apply(EntityCreatedEvent<Order> @event)
        {
            Id = @event.Sender.Id;
            Version = @event.Sender.Version;
        }
        private void Apply(EntityCreatedEvent<OrderLine> @event)
        {
            Version = @event.Sender.Version;
            var orderLine = new OrderLine
            {
                OrderId = @event.Sender.OrderId,
                Quantity = @event.Sender.Quantity,
                Id = @event.Sender.Id,
                LotteryNumber = @event.Sender.LotteryNumber,
                Version = @event.Sender.Version
            };
            _orderLines.Add(orderLine);
        }
        public void UpdateOrder(Guid idOrderLine, int cantidad)
        {
            var orderLine = new OrderLine
            {
                Quantity = cantidad,
                Id = idOrderLine,
                Version = this.Version
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
