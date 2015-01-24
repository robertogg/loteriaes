using System;
using System.Collections.Generic;
using LoteriaES.Models;

namespace LoteriaES.Core
{
    public interface IOrderRepository
    {
        IEnumerable<LotteryOrder> GetAll();
        void AddNewOrder(LotteryOrder entity);
        IEnumerable<LotteryOrderLine> GetOrderDetail(Guid orderId);
        void AddNewLineOrder(LotteryOrderLine entity);
        void UpdateQuantity(Guid orderId, Guid orderLineId,int quantity);
        LotteryOrderLine GetOrderLineDetail(Guid orderId, Guid orderLineId);
    }
}