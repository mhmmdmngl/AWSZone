using AWS.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AWS.Filter;
using System.Data.SqlClient;
using Dapper;
using System.Web;
using AWS.Models;

namespace AWS.Controllers
{
    public class AwsController : ApiController
    {
        //[WebApiAuth]
        //[HttpPost]
        //public dynamic MyService (string wsUser, string wsPassword, string webServisAdi, string FonksiyonAdi, int auto = 1)
        //{
        //    long sorguBaslangici = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        //    wsLog log = new wsLog();
        //    wsLogCTX logCTX = new wsLogCTX();
        //    WebServisCTX wsCtx = new WebServisCTX();
        //    userCTX uctx = new userCTX();
        //    fonksiyonCTX fctx = new fonksiyonCTX();
        //    DBCTX dbctx = new DBCTX();
        //    wsUserCTX wuctx = new wsUserCTX();
        //    //var user = uctx.kullaniciDondur(wsUser);
        //    var user = wuctx.userDogrula(wsUser, wsPassword);
        //    log.userid = user.userId;
        //    log.wsuserid = user.id;
        //    if (user == null)
        //        return "Kullanıcı bulunamadı";
        //    var servis = wsCtx.wsDondurByName(webServisAdi);
        //    var fonksiyon = fctx.fonsiyonDondurByAd(FonksiyonAdi);
        //    log.fonksiyonid = fonksiyon.id;
        //    var db = dbctx.dbDondur(fonksiyon.dbid);
        //    if(db != null)
        //    {
        //        var connection = GetConnection(db.connectionstring);
        //        parametrelerCTX pctx = new parametrelerCTX();
        //        var fonkParameters = pctx.parametreDondurByFid(fonksiyon.id);
        //        using(var a = connection)
        //        {
        //            string sorgu = fonksiyon.sqlSorgu;
        //            var dizi = sorgu.Split(' ');
        //            string sorgumuz = "";
        //            if(auto == 1)
        //            {
        //                if (fonksiyon.islemTuru == "select")
        //                {
        //                    string tabloadi = dizi[3];

        //                    sorgumuz = "select * from " + tabloadi;
        //                    sorgumuz = selectSorgusuOlustur(sorgumuz, fonkParameters);
        //                    long sorguBitisi = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        //                    log.answertime = Convert.ToString((sorguBitisi - sorguBaslangici) / 1000.000);
        //                    log.sorguZamani = DateTime.Now;
        //                    logCTX.wsLogOlustur(log);
        //                    return a.Query<dynamic>(sorgumuz);

        //                }
        //                else if (fonksiyon.islemTuru == "insert")
        //                {
        //                    string tabloadi = dizi[2];

        //                    sorgumuz = "insert into " + tabloadi;
        //                    sorgumuz = insertSorgusuOlustur(sorgumuz, fonkParameters, tabloadi, "");
        //                    long sorguBitisi = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        //                    log.answertime = Convert.ToString((sorguBitisi - sorguBaslangici) / 1000.000);
        //                    log.sorguZamani = DateTime.Now;
        //                    logCTX.wsLogOlustur(log);

        //                    return a.Execute(sorgumuz);
        //                }
        //                else if (fonksiyon.islemTuru == "update")
        //                {
        //                    string tabloadi = dizi[1];

        //                    sorgumuz = "update " + tabloadi + " set ";
        //                    sorgumuz = updateSorgusuOlustur(sorgumuz, fonkParameters, tabloadi, "");
        //                    long sorguBitisi = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        //                    log.answertime = Convert.ToString((sorguBitisi - sorguBaslangici) / 1000.000);
        //                    log.sorguZamani = DateTime.Now;
        //                    logCTX.wsLogOlustur(log);

        //                    return a.Execute(sorgumuz);
        //                }

        //            }
        //            else
        //            {
        //                List<string> binds = new List<string>();
        //                foreach(string deger in dizi)
        //                {
        //                    if(deger != "")
        //                    {
        //                        if (deger[0] == '@')
        //                        {
        //                            string eklenecek = deger.Remove(0, 1);
        //                            binds.Add(eklenecek);
        //                        }
        //                    }

        //                }
        //            }

        //        }
        //    }
        //    return null;
        //}
        [WebApiAuth]
        [HttpPost]
        public dynamic MyService(string wsUser, string wsPassword, string webServisAdi, string FonksiyonAdi, int auto = 1)
        {
            long sorguBaslangici = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            wsLog log = new wsLog();
            wsLogCTX logCTX = new wsLogCTX();
            WebServisCTX wsCtx = new WebServisCTX();
            userCTX uctx = new userCTX();
            fonksiyonCTX fctx = new fonksiyonCTX();
            DBCTX dbctx = new DBCTX();
            wsUserCTX wuctx = new wsUserCTX();
            //var user = uctx.kullaniciDondur(wsUser);
            var user = wuctx.userDogrula(wsUser, wsPassword);
            log.userid = user.userId;
            log.wsuserid = user.id;
            if (user == null)
                return "Kullanıcı bulunamadı";
            var servis = wsCtx.wsDondurByName(webServisAdi);
            var fonksiyon = fctx.fonsiyonDondurByAd(FonksiyonAdi);
            log.fonksiyonid = fonksiyon.id;
            var db = dbctx.dbDondur(fonksiyon.dbid);
            if (db != null)
            {
                var connection = GetConnection(db.connectionstring);
                parametrelerCTX pctx = new parametrelerCTX();
                var fonkParameters = pctx.parametreDondurByFid(fonksiyon.id);
                using (var a = connection)
                {
                    string sorgu = fonksiyon.sqlSorgu;
                    var dizi = sorgu.Split(' ');
                    string sorgumuz = "";
                    if (auto == 1)
                    {
                        if (fonksiyon.islemTuru == "select")
                        {
                            string tabloadi = dizi[3];

                            sorgumuz = sorgu;
                            if(fonkParameters.Count > 0)
                                sorgumuz = selectSorgusuOlusturYeni(sorgu, fonkParameters);
                            long sorguBitisi = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                            log.answertime = Convert.ToString((sorguBitisi - sorguBaslangici) / 1000.000);
                            log.sorguZamani = DateTime.Now;
                            logCTX.wsLogOlustur(log);
                            return a.Query<dynamic>(sorgumuz);

                        }
                        else if (fonksiyon.islemTuru == "insert")
                        {
                            string tabloadi = dizi[2];

                            sorgumuz = "insert into " + tabloadi;
                            sorgumuz = insertSorgusuOlustur(sorgumuz, fonkParameters, tabloadi, "");
                            long sorguBitisi = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                            log.answertime = Convert.ToString((sorguBitisi - sorguBaslangici) / 1000.000);
                            log.sorguZamani = DateTime.Now;
                            logCTX.wsLogOlustur(log);

                            return a.Execute(sorgumuz);
                        }
                        else if (fonksiyon.islemTuru == "update")
                        {
                            string tabloadi = dizi[1];

                            sorgumuz = "update " + tabloadi + " set ";
                            sorgumuz = updateSorgusuOlustur(sorgumuz, fonkParameters, tabloadi, "");
                            long sorguBitisi = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                            log.answertime = Convert.ToString((sorguBitisi - sorguBaslangici) / 1000.000);
                            log.sorguZamani = DateTime.Now;
                            logCTX.wsLogOlustur(log);

                            return a.Execute(sorgumuz);
                        }

                    }
                    else
                    {
                        sorgumuz = selectSorgusuOlustur(sorgumuz, fonkParameters);
                        long sorguBitisi = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                        log.answertime = Convert.ToString((sorguBitisi - sorguBaslangici) / 1000.000);
                        log.sorguZamani = DateTime.Now;
                        return a.Query<dynamic>(sorgumuz, new { 
                            
                        });
                    }

                }
            }
            return null;
        }

        
        public List<string> kolonBul(string sqlSorgu)
        {
            var dizi = sqlSorgu.Split(' ');
            List<string> kolonlar = new List<string>();
            foreach (var a in dizi)
            {
                if (a != "")
                {
                    if (a[0] == '@')
                    {
                        string eklenecek = a.Remove(0, 1);
                        kolonlar.Add(eklenecek);
                    }
                }
            }
            return kolonlar;
        }
        public string selectSorgusuOlusturYeni(string sorgumuz, List<parametreModel> fonkParameters)
        {
            var kolonlar = kolonBul(sorgumuz);
            for (int i = 0; i < fonkParameters.Count; i++)
            {
                string degisecekDeger = '@' + fonkParameters[i].parametreAdi;
                string gelenDeger = HttpContext.Current.Request.Params[fonkParameters[i].parametreAdi.ToString()];
                if (fonkParameters[i].parametreAdi.ToString()[0] < 58)
                    sorgumuz = sorgumuz.Replace(degisecekDeger, gelenDeger);

                else
                    sorgumuz = sorgumuz.Replace(degisecekDeger, gelenDeger);


            }
            return sorgumuz;

        }

        public string selectSorgusuOlustur(string sorgumuz, List<parametreModel> fonkParameters)
        {
            for (int i = 0; i < fonkParameters.Count; i++)
            {
                if (fonkParameters[i].parametreAdi.ToString()[0] < 58)
                {
                    if (i == 0)
                        sorgumuz = sorgumuz + " Where " + fonkParameters[i].parametreAdi + " = " + HttpContext.Current.Request.Params[fonkParameters[i].parametreAdi.ToString()];
                    else
                        sorgumuz = sorgumuz + " and " + fonkParameters[i].parametreAdi + " = " + HttpContext.Current.Request.Params[fonkParameters[i].parametreAdi.ToString()];
                }
                else
                {
                    if (i == 0)
                        sorgumuz = sorgumuz + " Where " + fonkParameters[i].parametreAdi + " = " + "'" + HttpContext.Current.Request.Params[fonkParameters[i].parametreAdi.ToString()] + "'";
                    else
                        sorgumuz = sorgumuz + " and " + fonkParameters[i].parametreAdi + " = " + "'" + HttpContext.Current.Request.Params[fonkParameters[i].parametreAdi.ToString()] + "'";
                }

            }
            return sorgumuz;
        }
        public string insertSorgusuOlustur(string sorgumuz, List<parametreModel> fonkParameters, string tabloAdi, string kolonAdi)
        {
            string kolonlar = "";
            string degerler = "";
            int flag = 0;
            for (int i = 0; i < fonkParameters.Count; i++)
            {
                string degiskenTuru = kritik(tabloAdi, fonkParameters[i].parametreAdi);
                dynamic param;
                if (degiskenTuru != "int")
                    param = "'"+ HttpContext.Current.Request.Params[fonkParameters[i].parametreAdi.ToString()]+"'";
                else
                    param = Convert.ToInt32(HttpContext.Current.Request.Params[fonkParameters[i].parametreAdi.ToString()]);
                if (fonkParameters[i].parametreAdi != "id")
                {
                    if (i == 0 || flag == 1)
                    {
                        kolonlar = kolonlar + fonkParameters[i].parametreAdi;
                        degerler = degerler + param;
                        flag = 0;
                    }
                    else
                    {
                        kolonlar = kolonlar + "," + fonkParameters[i].parametreAdi;
                        degerler = degerler + ", " + param;
                    }

                }
                else
                {
                    if(i == 0)
                        flag = 1;
                }
              

            }
            sorgumuz = sorgumuz + " (" + kolonlar + ") values (" + degerler + ")";
            return sorgumuz;
        }

        public string updateSorgusuOlustur(string sorgumuz, List<parametreModel> fonkParameters, string tabloAdi, string kolonAdi)
        {
            int id = -1;
            string kolonlar = "";
            string degerler = "";
            int flag = 0;
            for (int i = 0; i < fonkParameters.Count; i++)
            {
                string degiskenTuru = kritik(tabloAdi, fonkParameters[i].parametreAdi);
                dynamic param;
                if (degiskenTuru != "int")
                    param = "'" + HttpContext.Current.Request.Params[fonkParameters[i].parametreAdi.ToString()] + "'";
                else
                    param = Convert.ToInt32(HttpContext.Current.Request.Params[fonkParameters[i].parametreAdi.ToString()]);
                if (fonkParameters[i].parametreAdi != "id")
                {
                    if (i == 0 || flag == 1)
                    {
                        sorgumuz = sorgumuz + " " + fonkParameters[i].parametreAdi + " = " + param;
                        flag = 0;
                    }
                    else
                    {
                        sorgumuz = sorgumuz + ", " + fonkParameters[i].parametreAdi + " = " + param;
                    }

                }
                else
                {
                    if (i == 0)
                        flag = 1;
                    id = param;
                }


            }
            sorgumuz = sorgumuz + " where id = " + id;
            return sorgumuz;
        }

        public void LogEkle(wsLog log)
        {
            wsLogCTX wlctx = new wsLogCTX();
            wlctx.wsLogOlustur(log);
        }

        public string kritik(string tabloAdi, string kolonAdi)
        {
            DBCTX dbc = new DBCTX();
            var db = dbc.dbDondur(1);

            var con = GetConnection(db.connectionstring);

            using (var a = con)
            {
                string sorgu = $"SELECT DATA_TYPE  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = ('{tabloAdi}') and COLUMN_NAME = ('{kolonAdi}')";
                dynamic tabloIsimleri = a.Query<dynamic>(sorgu);
                return tabloIsimleri[0].DATA_TYPE;


            }
        }

        public SqlConnection GetConnection(string connectionString)
        {
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = connectionString;
            sc.Open();
            return sc;
        }

    }
}
