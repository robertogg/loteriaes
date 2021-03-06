﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoteriaES.Core;
using LoteriaES.Core.CQRS;
using LoteriaES.CQRS.Commands;
using LoteriaEs.Entities;
using LoteriaEs.Events.Events;

namespace LoteriaES.CQRS.CommandHandlers
{
    public class CreateOrderCommandHandler: ICommandHandler<CreateOrderCommand>
    {
        private readonly IEventStoreRepository<Order> _orderRepository;

        public CreateOrderCommandHandler(IEventStoreRepository<Order> ordeRepository )
        {
            _orderRepository = ordeRepository;
        }
        public void Execute(CreateOrderCommand command)
        {
            var loteriaOrder = new Order()
            {
                Id = command.Id
            };
            var loteriaOrderItem = new OrderLine()
            {
                Id = Guid.NewGuid(),
                OrderId = command.Id,
                Quantity = command.Cantidad,
                LotteryNumber = command.NumeroLoteria
            };
          
            loteriaOrder.ApplyEvent(new EntityCreatedEvent<Order>(loteriaOrder));
            loteriaOrder.ApplyEvent(new EntityCreatedEvent<OrderLine>(loteriaOrderItem));
            _orderRepository.Add(loteriaOrder);
        }
    }
}
