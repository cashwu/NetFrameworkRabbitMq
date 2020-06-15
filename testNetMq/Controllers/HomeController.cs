using System.Web.Mvc;
using testNetMq.Models;
using testNetMq.Services;

namespace testNetMq.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQueueSender _queueSender;

        public HomeController(IQueueSender queueSender)
        {
            _queueSender = queueSender;
        }

        public ActionResult Index()
        {
            return Json(new { OK = "OK" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Send()
        {
            _queueSender.Send("test", new QueueDto(1, "cash"));

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
    }
}