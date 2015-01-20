using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoteriaES.Core.Bus;
using LoteriaES.CQRS.Commands;

namespace LoteriaES.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMessageBus _bus;

        public HomeController(IMessageBus bus)
        {
            _bus = bus;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewOrder()
        {
            var command = new CreateOrderCommand(
                new Guid("59B8A5FD-9046-431E-B90B-6DCD8EC48524"),
                "00000",
                1);

            _bus.Send(command);

            return RedirectToAction("Index");
        }

        public ActionResult UpdateOrder()
        {
            var command = new UpdateOrderCommand(
                new Guid("59B8A5FD-9046-431E-B90B-6DCD8EC48524"),
                "00000",
                10);

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