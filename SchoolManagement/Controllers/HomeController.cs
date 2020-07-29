using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "School Management System";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "feel free to reach us";

            return View();
        }
    }
}