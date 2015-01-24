using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoteriaES.Core;
using LoteriaES.Core.Bus;
using LoteriaES.CQRS.Commands;
using LoteriaES.Models;

namespace LoteriaES.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMessageBus _bus;
        private readonly IOrderRepository _orderRepository;

        public HomeController(IMessageBus bus,IOrderRepository orderRepository)
        {
            _bus = bus;
            _orderRepository = orderRepository;
        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult OrderList()
        {
            var orders= _orderRepository.GetAll();
            return View(orders);
        }

        public ActionResult Details(string id)
        {
            var orders = _orderRepository.GetOrderDetail(Guid.Parse(id));
            return View(orders);
        }

        public ActionResult NewOrder()
        {
            var command = new CreateOrderCommand(
                new Guid("59B8A5FD-9046-431E-B90B-6DCD8EC48524"),
                "25003",
                1);

            _bus.Send(command);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateOrderLine(string orderId,string orderLineId)
        {
            var orderLine = _orderRepository.GetOrderLineDetail(Guid.Parse(orderId), Guid.Parse(orderLineId));
            return View(orderLine);
        }

        public ActionResult UpdateOrderLine(LotteryOrderLine orderLine)
        {
            //var command = new UpdateOrderCommand(
            //    Guid.Parse("59B8A5FD-9046-431E-B90B-6DCD8EC48524"),
            //    Guid.Parse("82EF2FDF-177C-4760-969D-DE7EC442126C"),
            //    "25003",
            //    5);

            var command = new UpdateOrderCommand(orderLine.OrderId, 
                                                orderLine.OrderLineId, 
                                                orderLine.LotteryId.ToString(),
                                                orderLine.Quantity);
            _bus.Send(command);

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}