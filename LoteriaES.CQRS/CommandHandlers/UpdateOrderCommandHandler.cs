using System;
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
    public class UpdateOrderCommandHandler: ICommandHandler<UpdateOrderCommand>
    {
        private readonly IRepository<Order> _orderRepository;

        public UpdateOrderCommandHandler(IRepository<Order> ordeRepository )
        {
            _orderRepository = ordeRepository;
        }
        public void Execute(UpdateOrderCommand command)
        {
            var loteriaOrder = _orderRepository.Get(command.OrderId);

            loteriaOrder.UpdateOrder(command.OrderLineId, command.Cantidad);
            var loteriaOrderItem = loteriaOrder.GetOrderDetail(command.OrderLineId);
            loteriaOrder.ApplyEvent(new EntityUpdatedEvent<OrderLine>(loteriaOrderItem));
            _orderRepository.Add(loteriaOrder);
        }

    }
}
