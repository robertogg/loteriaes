using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LoteriaES.Core;
using LoteriaES.Infrastructure.Context;
using LoteriaES.Models;


namespace LoteriaES.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly LotteryContext _context = new LotteryContext();

        public IEnumerable<LotteryOrder> GetAll()
        {
            return _context.Orders.ToList();
        }

        public IEnumerable<LotteryOrderLine> GetOrderDetail(Guid orderId)
        {
            var order = _context.Orders.FirstOrDefault(q => q.OrderId == orderId);
            IEnumerable<LotteryOrderLine> dataOrderLines=null;
            if (order != null)
            {
                dataOrderLines = order.OrderLines.ToList();
            }
            return dataOrderLines;
        }

        public void AddNewOrder(LotteryOrder entity)
        {
            _context.Orders.Add(entity);
            _context.SaveChanges();
        }

        public LotteryOrderLine GetOrderLineDetail(Guid orderId,Guid orderLineId)
        {
            var order = _context.Orders.FirstOrDefault(q => q.OrderId == orderId);
            return order.OrderLines.FirstOrDefault(o => o.OrderLineId == orderLineId);
        }

        public void AddNewLineOrder(LotteryOrderLine entity)
        {
            var order = _context.Orders.FirstOrDefault(q => q.OrderId == entity.OrderId);
            if (order != null)
            {
                order.OrderLines.Add(entity);
                _context.SaveChanges();
            }
        }
        public void UpdateQuantity(Guid orderId, Guid orderLineId,int quantity)
        {
            var order = _context.Orders.FirstOrDefault(q => q.OrderId == orderId);
            var orderLine = order.OrderLines.FirstOrDefault(q => q.OrderLineId == orderLineId);
            if (orderLine != null)
            {
                orderLine.Quantity = quantity;
                _context.SaveChanges();
            }
        }

    }
}
