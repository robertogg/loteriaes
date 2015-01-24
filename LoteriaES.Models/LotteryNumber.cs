using System;
using System.ComponentModel.DataAnnotations;

namespace LoteriaES.Models
{
    public class LotteryNumber
    {
        [Key]
        public Guid LotteryId { get; set; }
        public string Number { get; set; }
        public decimal LotteryPrice { get; set; }
        public int AvailableQuantity { get; set; }
        public string LotteryImage { get; set; }
    }
}
