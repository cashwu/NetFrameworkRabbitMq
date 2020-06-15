using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac.Core.Activators.Reflection;
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
            return View();
        }

        public ActionResult Send()
        {
            _queueSender.Send("test", new QueueDto(1, "cash"));

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
    }

    public class QueueDto
    {
        public QueueDto()
        {
        }

        public QueueDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}