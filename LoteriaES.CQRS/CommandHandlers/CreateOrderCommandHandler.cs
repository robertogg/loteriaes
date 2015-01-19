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
    public class CreateOrderCommandHandler: ICommandHandler<CreateOrderCommand>
    {
        private readonly IRepository<Order> _orderRepository;

        public CreateOrderCommandHandler(IRepository<Order> ordeRepository )
        {
            _orderRepository = ordeRepository;
        }
        public void Execute(CreateOrderCommand command)
        {
            //En este caso se crea el pedido en otros aqui se debería llamar a GetById
            var loteriaOrder = new Order()
            {
                Id = command.Id,
                NumeroLoteria = command.NumeroLoteria,
                Cantidad = command.Cantidad
            };
            loteriaOrder.ApplyEvent(new EntityCreatedEvent<Order>(loteriaOrder));
            _orderRepository.Add(loteriaOrder);
        }
    }
}
