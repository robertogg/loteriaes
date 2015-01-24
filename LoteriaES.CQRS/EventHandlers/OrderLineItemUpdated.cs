using System;
using LoteriaES.Core.CQRS;
using LoteriaEs.Entities;
using LoteriaEs.Events.Events;
using LoteriaES.Infrastructure.Repositories;
using LoteriaES.Models;

namespace LoteriaES.CQRS.EventHandlers
{
    public class OrderLineItemIsUpdated:IEventHandler<EntityUpdatedEvent<OrderLine>>
    {
        private readonly OrderRepository _lotteryOrdeRepository;
        private readonly ProductRepository _productRepository;

        public OrderLineItemIsUpdated(OrderRepository lotteryOrdeRepository, ProductRepository productRepository)
        {
            _lotteryOrdeRepository = lotteryOrdeRepository;
            _productRepository = productRepository;
        }

        public void Handle(EntityUpdatedEvent<OrderLine> domainEvent)
        {
            _lotteryOrdeRepository.UpdateQuantity(domainEvent.Sender.OrderId, domainEvent.Sender.Id,domainEvent.Sender.Quantity);
        }
    }
}
