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
            _bus.Send(new UpdateOrderCommand
            {
                Id = 1,
                NumeroLoteria = "00000",
                Cantidad = 10
            });
            //_bus.Send(new CreateOrderCommand
            //{
            //    Id=1,
            //    NumeroLoteria = "00000"
            //});
            return View();
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