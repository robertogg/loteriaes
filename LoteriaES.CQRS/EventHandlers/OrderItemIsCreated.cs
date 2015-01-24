using System;
using LoteriaES.Core.CQRS;
using LoteriaEs.Entities;
using LoteriaEs.Events.Events;
using LoteriaES.Infrastructure.Repositories;
using LoteriaES.Models;

namespace LoteriaES.CQRS.EventHandlers
{
    public class OrderItemIsCreated:IEventHandler<EntityCreatedEvent<Order>>
    {
        private readonly OrderRepository _lotteryOrdeRepository;

        public OrderItemIsCreated(OrderRepository lotteryOrdeRepository)
        {
            _lotteryOrdeRepository = lotteryOrdeRepository;
        }

        public void Handle(EntityCreatedEvent<Order> domainEvent)
        {
            var lotteryOrder = new LotteryOrder
            {
                OrderId = domainEvent.Sender.Id,
                OrderDate = DateTime.Now
            };
            _lotteryOrdeRepository.AddNewOrder(lotteryOrder);
        }
    }
}
