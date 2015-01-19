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
            _bus.Send(new CreateOrderCommand
            {
                Id = new Guid("59B8A5FD-9046-431E-B90B-6DCD8EC48524"),
                NumeroLoteria = "00000",
                Cantidad = 1
            });
            return RedirectToAction("Index");
        }

        public ActionResult UpdateOrder()
        {
            _bus.Send(new UpdateOrderCommand
            {
                Id = new Guid("59B8A5FD-9046-431E-B90B-6DCD8EC48524"),
                NumeroLoteria = "00000",
                Cantidad = 10
            });
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