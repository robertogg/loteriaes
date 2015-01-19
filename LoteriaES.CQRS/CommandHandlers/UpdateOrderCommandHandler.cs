using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoteriaES.Core;
using LoteriaES.Core.CQRS;
using LoteriaES.CQRS.Commands;
using LoteriaES.CQRS.Events;
using LoteriaEs.Entities;

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
            var loteriaOrder = _orderRepository.Get(command.Id);
            loteriaOrder.UpdateOrder(command.NumeroLoteria,command.Cantidad);
            loteriaOrder.ApplyEvent(new EntityUpdatedEvent<Order>(loteriaOrder));
            _orderRepository.Add(loteriaOrder);
        }

    }
}
