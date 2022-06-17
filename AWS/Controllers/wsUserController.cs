using AWS.Models;
using AWS.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AWS.Controllers
{
    public class wsUserController : Controller
    {
        // GET: wsUser
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult wsUserListele()
        {
            wsUserCTX wuctx = new wsUserCTX();
            var wus = wuctx.tumUserlariDondurByUserId(Convert.ToInt32(Session["id"].ToString()));
            return View(wus);
        }

        public ActionResult wsUserEkle()
        {
            wsUser wu = new wsUser();
            return View(wu);
        }
        [HttpPost]
        public ActionResult wsUserEkle(wsUser wu)
        {
            wsUserCTX wuctx = new wsUserCTX();
            wu.userId = Convert.ToInt32(Session["id"].ToString());
            wuctx.wsUserOlustur(wu);
            return RedirectToAction("wsUserListele");
        }

        public ActionResult logGoruntule(int wsUserId)
        {
            wsLogCTX wuCTX = new wsLogCTX();
            var liste = wuCTX.tumUserlariDondurBywsUserId(wsUserId);
            return View(liste);
        }
    }
}