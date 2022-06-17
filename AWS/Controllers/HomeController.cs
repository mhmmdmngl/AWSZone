using AWS.Filter;
using AWS.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AWS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            bool bagliMi = sessionKontrol();
            if(bagliMi == false)
                return RedirectToAction("Login", "Login");
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

        public bool sessionKontrol()
        {
            if (Session["user"] != null && Session["email"] != null)
            {
                return true;
            }
            return false;
        }
    }
}