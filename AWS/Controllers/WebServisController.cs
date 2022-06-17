
using AWS.Models;
using AWS.Repos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using Newtonsoft.Json;

namespace AWS.Controllers
{
    public class WebServisController : Controller
    {
        // GET: WebServis
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WebServisListele()
        {
            bool bagliMi = sessionKontrol();
            if (bagliMi == false)
                return RedirectToAction("Login", "Login");
            WebServisCTX wctx = new WebServisCTX();
            var wsListesi = wctx.wsListesiDondurByUserID(Convert.ToInt32(Session["id"].ToString()));
            return View(wsListesi);
        }


        public ActionResult WebServisEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WebServisEkle(string kull)
        {
            WebServisModel wsm = new WebServisModel();
            try { wsm.wsAdi = Request.Form["txtWSAdi"].ToString(); } catch { wsm.wsAdi = ""; }
            try { wsm.wsAciklama = Request.Form["txtWsAciklama"].ToString(); } catch { wsm.wsAciklama = ""; }
            wsm.userid = Convert.ToInt32(Session["id"].ToString());
            WebServisCTX wsctx = new WebServisCTX();
            wsctx.wsOlustur(wsm);
            return RedirectToAction("WebServisListele");
        }

        public ActionResult WebServisYonet(int wsMappingId =1)
        {
            //test için...
            int wsId = 1;
            int dbid = 1;

            WebServisCTX wsCTX = new WebServisCTX();
            DBCTX dbCTX = new DBCTX();

            var ws = wsCTX.wsDondur(wsId);
            var db = dbCTX.dbDondur(dbid);
            var conS = db.connectionstring;
            SqlConnection baglanti = GetConnection(conS);
            return View();
        }
        public ActionResult FonksiyonListesi(int wsid)
        {
            fonksiyonCTX fctx = new fonksiyonCTX();
            var liste = fctx.tumFonksiyonlariDondur(wsid);
            ViewBag.wsid = wsid;
            return View(liste);
        }

        public ActionResult FonksiyonEkle(int wsid)
        {
            WebServisCTX wsCTX = new WebServisCTX();
            var ws = wsCTX.wsDondur(wsid);
            fonksiyonModel fm = new fonksiyonModel();
            fm.wsid = wsid;
            fm.wsm = ws;
            ViewBag.dbListesi = new SelectList(dbListListeHazirla(), "id", "ad");

            return View(fm);
        }
        public List<string> kolonBul(string sqlSorgu)
        {
            var dizi = sqlSorgu.Split(' ');
            List<string> kolonlar = new List<string>();
            foreach (var a in dizi)
            {
                if(a != "")
                {
                    if(a[0] == '@')
                    {
                        string eklenecek = a.Remove(0, 1);
                        kolonlar.Add(eklenecek);
                    }
                }
            }
            return kolonlar;
        }
        [HttpPost]
        public ActionResult FonksiyonEkle(fonksiyonModel fm)
        { 
            //var gelenKolonlar = Request.Form["kolons"].ToString().Split(',');
            var sqlSorgu = Request.Form["txtSql"].ToString();
            var kolonlar = kolonBul(sqlSorgu);
            var dbSecimi = Convert.ToInt32(Request.Form["dbListesi"].ToString());
            var islemTuru = Request.Form["metot"].ToString();
            fm.dbid = dbSecimi;
            fm.sqlSorgu = sqlSorgu;
            fm.islemTuru = islemTuru;
            fonksiyonCTX fctx = new fonksiyonCTX();
            int eklenenId = fctx.fonksiyonOlustur(fm);
            foreach(var a in kolonlar)
            {
                parametreModel pm = new parametreModel();
                pm.fonksiyonID = eklenenId;
                pm.parametreAdi = a;
                parametrelerCTX pctx = new parametrelerCTX();
                pctx.parametreOlustur(pm);
            }
            return RedirectToAction("FonksiyonListesi", new { wsid = fm.wsid });
        }
        public SqlConnection GetConnection(string connectionString)
        {
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = connectionString;
            sc.Open();
            return sc;
        }
        public ActionResult WebServisler()
        {
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

        public List<selectFor> dbListListeHazirla()
        {
            DBCTX ikctx = new DBCTX();
            var liste = ikctx.tumdbDondurByUserId(Convert.ToInt32(Session["id"].ToString()));
            List<selectFor> birimListesi = new List<selectFor>();
            foreach (var x in liste)
            {
                selectFor bl = new selectFor();
                bl.id = x.id;
                bl.ad = x.adi;
                birimListesi.Add(bl);

            }
            return birimListesi;
        }
        public class selectFor
        {
            public int id { set; get; }
            public string ad { set; get; }
        }

        public JsonResult DbPost(int dbID)
        {
            DBCTX dbc = new DBCTX();
            var db = dbc.dbDondur(dbID);
            var con = GetConnection(db.connectionstring);
            using(var a  = con)
            {
                string sorgu = "SELECT * FROM sys.Tables";
                dynamic tabloIsimleri = a.Query<dynamic>(sorgu);
                string json = JsonConvert.SerializeObject(tabloIsimleri);

                return Json(json);


            }
        }

        public JsonResult kritik(string tabloAdi)
        {
            DBCTX dbc = new DBCTX();
            var db = dbc.dbDondur(1);

            var con = GetConnection(db.connectionstring);

            using (var a = con)
            {
                string sorgu = $"SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('{tabloAdi}')";
                dynamic tabloIsimleri = a.Query<dynamic>(sorgu);
                string json = JsonConvert.SerializeObject(tabloIsimleri);

                return Json(json);

               
            }
        }
    }
}