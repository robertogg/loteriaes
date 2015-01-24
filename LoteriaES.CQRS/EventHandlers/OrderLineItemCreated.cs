using System;
using LoteriaES.Core.CQRS;
using LoteriaEs.Entities;
using LoteriaEs.Events.Events;
using LoteriaES.Infrastructure.Repositories;
using LoteriaES.Models;

namespace LoteriaES.CQRS.EventHandlers
{
    public class OrderLineItemIsCreated:IEventHandler<EntityCreatedEvent<OrderLine>>
    {
        private readonly OrderRepository _lotteryOrdeRepository;
        private readonly ProductRepository _productRepository;

        public OrderLineItemIsCreated(OrderRepository lotteryOrdeRepository,ProductRepository productRepository)
        {
            _lotteryOrdeRepository = lotteryOrdeRepository;
            _productRepository = productRepository;
        }

        public void Handle(EntityCreatedEvent<OrderLine> domainEvent)
        {
            var lotteryOrder = new LotteryOrderLine()
            {
                OrderId = domainEvent.Sender.OrderId,
                LotteryId = _productRepository.GetProduct(domainEvent.Sender.LotteryNumber),
                OrderLineId = domainEvent.Sender.Id,
                Quantity = domainEvent.Sender.Quantity
            };
            _lotteryOrdeRepository.AddNewLineOrder(lotteryOrder);
        }
    }
}
