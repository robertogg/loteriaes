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
        public int Id { get; set; }
        public string NumeroLoteria { get; set; }
        public int Cantidad { get; set; }
    }
}
