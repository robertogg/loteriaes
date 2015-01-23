using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoteriaES.Core;

namespace LoteriaEs.Entities
{
    public class OrderLine: Entity
    {
        public string NumeroLoteria { get; set; }
        public int Cantidad { get; set; }
        public Guid OrderId { get; set; }
    }
}
