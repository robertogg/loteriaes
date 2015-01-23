using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoteriaES.Core.CQRS;

namespace LoteriaES.CQRS.Commands
{
    public class CreateOrderCommand : ICommand
    {
        public Guid Id { get; private set; }

        public string NumeroLoteria { get; private set; }

        public int Cantidad { get; private set; }

        public CreateOrderCommand(
            Guid id, 
            string numeroLoteria, 
            int cantidad)
        {
            Id = id;
            NumeroLoteria = numeroLoteria;
            Cantidad = cantidad;
        }
    }
}
