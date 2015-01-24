using System.Data.Entity;
using LoteriaES.Models;

namespace LoteriaES.Infrastructure.Context
{
    public class LotteryContext : DbContext
    {
        public LotteryContext()
            : base("LotteryContext")
        {
        }

        public DbSet<LotteryOrder> Orders { get; set; }
        //public DbSet<LotteryOrderLine> OrderLines { get; set; }
        public DbSet<LotteryNumber> LotteryNumbers { get; set; }

    }
}
