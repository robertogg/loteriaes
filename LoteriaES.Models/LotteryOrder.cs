using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoteriaES.Models
{
    [Table("Orders")]
    public class LotteryOrder
    {
        [Key]
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual ICollection<LotteryOrderLine> OrderLines { get; set; } 
    }
}
