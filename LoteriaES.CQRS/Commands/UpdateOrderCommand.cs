using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoteriaES.Core.CQRS;

namespace LoteriaES.CQRS.Commands
{
    public class UpdateOrderCommand : ICommand
    {
        public Guid OrderId { get; private set; }

        public Guid OrderLineId { get; private set; }

        public string NumeroLoteria { get; private set; }

        public int Cantidad { get; private set; }

        public UpdateOrderCommand(
            Guid orderId,
            Guid orderLineId,
            string numeroLoteria,
            int cantidad)
        {
            OrderId = orderId;
            OrderLineId = orderLineId;
            NumeroLoteria = numeroLoteria;
            Cantidad = cantidad;
        }
    }
}
