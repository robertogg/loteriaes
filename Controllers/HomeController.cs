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
            var orders = _orderRepository.GetAll();
            return View(orders);
        }

        public ActionResult Details(string id)
        {
            var orders = _orderRepository.GetOrderDetail(Guid.Parse(id));
            return View(orders);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("");
        }

        public ActionResult Create(LotteryNewOrder lotteryNewOrder)
        {
            var command = new CreateOrderCommand(
                Guid.NewGuid(),
                lotteryNewOrder.Number,
                lotteryNewOrder.Quantity);

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