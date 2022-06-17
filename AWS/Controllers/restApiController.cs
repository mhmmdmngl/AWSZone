using AWS.Models;
using AWS.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AWS.Controllers
{
    public class restApiController : Controller
    {
        public ActionResult RestApiEkle()
        {
            restapiModel ram = new restapiModel();
            return View();

        }

        [HttpPost]
        public ActionResult restApiEkle()
        {
            restapiModel ram = new restapiModel();
            int userId = Convert.ToInt32(Session["id"].ToString());
            restApiCTX ractx = new restApiCTX();
            try { ram.apiurl = Request.Form["txtrest"].ToString(); } catch { ram.apiurl = ""; }
            try { ram.method = Request.Form["txtmetot"].ToString(); } catch { ram.method = ""; }
            
            try { ram.serviceName = Request.Form["txtRestApi"].ToString(); } catch { ram.method = ""; }
            ram.userId = userId;
            ractx.restApiOlustur(ram);
            return View();

        }

        public ActionResult restApiListesi()
        {
            int userId = Convert.ToInt32(Session["id"].ToString());
            restApiCTX ractx = new restApiCTX();
            var restApiListesi = ractx.restApiListesiDondurbyUserId(userId);
            return View(restApiListesi);

        }

        public ActionResult restParamEkle(int restId)
        {
            restParemetersModel ram = new restParemetersModel();
            restApiCTX ractx = new restApiCTX();
            var rap = ractx.restApiListesiDondur(restId);
            ram.restId = rap.id;
            return View(ram);
        }
        [HttpPost]
        public ActionResult restParamEkle()
        {
            restParemetersModel ram = new restParemetersModel();
            int userId = Convert.ToInt32(Session["id"].ToString());
            restParametersCTX ractx = new restParametersCTX();
            try { ram.valueName = Request.Form["txtparamname"].ToString(); } catch { ram.valueName = ""; }
            try { ram.restId = Convert.ToInt32(Request.Form["txtid"].ToString()); } catch {  }
            ractx.rpEkle(ram);
            return Redirect("restApiListesi");
        }
    }
}