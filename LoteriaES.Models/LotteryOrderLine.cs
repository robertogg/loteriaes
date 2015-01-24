using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoteriaES.Models
{
    [Table("OrderLines")]
    public class LotteryOrderLine
    {
        [Key]
        public Guid OrderLineId { get; set; }
        public Guid OrderId { get; set; }
        public Guid LotteryId { get; set; }
        public int Quantity { get; set; }

        public virtual LotteryNumber Lottery { get; set; }
    }
}
