using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoteriaES.Models
{
    public class LotteryNewOrder
    {
        public Guid OrderId { get; set; }
        public string Number { get; set; }
        public int Quantity { get; set; }
    }
}
