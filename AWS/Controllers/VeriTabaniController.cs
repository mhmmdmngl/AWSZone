using AWS.Models;
using AWS.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AWS.Controllers
{
    public class VeriTabaniController : Controller
    {
        // GET: VeriTabani
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult VTListele()
        {
            int userId = Convert.ToInt32(Session["id"].ToString());
            DBCTX dctx = new DBCTX();
            return View(dctx.tumdbDondurByUserId(userId));
        }
        public ActionResult VTEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult VTEkle(string a)
        {
            DBModel db = new DBModel();
            try { db.adi = Request.Form["txtAdi"].ToString(); } catch { db.adi = ""; }
            try { db.aciklama = Request.Form["txtAciklama"].ToString(); } catch { db.aciklama = ""; }
            try { db.connectionstring = Request.Form["txtBaglanti"].ToString(); } catch { db.connectionstring = ""; }
            try { db.DBAdi = Request.Form["txtDBAdi"].ToString(); } catch { db.DBAdi = ""; }

            try { db.username = Request.Form["txtUserName"].ToString(); } catch { db.username = ""; }
            try { db.password = Request.Form["txtPassword"].ToString(); } catch { db.password = ""; }
            try { db.hostname = Request.Form["txtHostname"].ToString(); } catch { db.hostname = ""; }
            var t = Convert.ToInt32(Session["id"].ToString());
            try { db.userid = Convert.ToInt32(Session["id"].ToString()); } catch { int x = 3; }

            DBCTX dctx = new DBCTX();
            dctx.dbOlustur(db);

            return View("");
        }
    }
}