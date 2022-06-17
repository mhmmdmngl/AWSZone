using AWS.Models;
using AWS.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AWS.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string kullaniciAdi)
        {
            var kull = Request.Form["txtKullanici"].ToString();
            var pass = Request.Form["txtParola"].ToString();

            userCTX uctx = new userCTX();
            var kullaniciVarMi = uctx.kullaniciGiris(kull, pass);
            if (kullaniciVarMi != null)
            {
                initSession(kullaniciVarMi);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public ActionResult Index(string kullaniciAdi)
        {
            var kull = Request.Form["txtKullanici"].ToString();
            var pass = Request.Form["txtParola"].ToString();

            userCTX uctx = new userCTX();
            var kullaniciVarMi = uctx.kullaniciGiris(kull, pass);
            if (kullaniciVarMi != null)
            {
                Session["user"] = kull;
                Session["email"] = kullaniciVarMi.email;
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Login");
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }

        public void initSession(UserModel kulllanici)
        {
            Session["user"] = kulllanici.userName;
            Session["email"] = kulllanici.email;
            Session["id"] = kulllanici.id;
        }
    }
}