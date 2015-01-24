using System;
using LoteriaES.Models;

namespace LoteriaES.Core
{
    public interface IProductRepository
    {
        Guid GetProduct(string lotteryNumber);
    }
}
