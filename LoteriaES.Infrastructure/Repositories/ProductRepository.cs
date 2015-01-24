using System;
using System.Linq;
using LoteriaES.Core;
using LoteriaES.Infrastructure.Context;
using LoteriaES.Models;


namespace LoteriaES.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly LotteryContext _context = new LotteryContext();

        public Guid GetProduct(string lotteryNumber)
        {
            return _context.LotteryNumbers.FirstOrDefault(q => q.Number == lotteryNumber).LotteryId;
        }
    }
}
