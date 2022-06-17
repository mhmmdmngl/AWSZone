using AWS.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWS.Repos
{
    public class fonksiyonDbCTX
    {
        public List<fonksiyondbMapping> fonksiyonListesiDondur()
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from fonksiyondbMapping";
                var fonksiyonlist = a.Query<fonksiyondbMapping>(sorgu).ToList();
                fonksiyonCTX fctx = new fonksiyonCTX();
                DBCTX dctx = new DBCTX();
                foreach(var f in fonksiyonlist)
                {
                    f.dbmodel = dctx.dbDondur(f.dbid);
                    f.fonksiyon = fctx.dbDondur(f.fonksiyonid);
                }

                return fonksiyonlist;
            }
        }
        public List<fonksiyondbMapping> fonksiyonListesiDondurByUserfonksiyonid(int fid)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from fonksiyondbMapping where fonksiyonsid = @fonksiyonsid";
                var parameters = new { fonksiyonsid = fid };
                var DBList = a.Query<fonksiyondbMapping>(sorgu, parameters).ToList();
                fonksiyonCTX fctx = new fonksiyonCTX();
                DBCTX dctx = new DBCTX();
                foreach (var f in DBList)
                {
                    f.dbmodel = dctx.dbDondur(f.dbid);
                    f.fonksiyon = fctx.dbDondur(f.fonksiyonid);
                }
                return DBList;
            }
        }

        public List<fonksiyondbMapping> fonksiyonListesiDondurByUserdbid(int dbid)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from fonksiyondbMapping where dbid = @dbid";
                var parameters = new { dbid = dbid };
                var DBList = a.Query<fonksiyondbMapping>(sorgu, parameters).ToList();
                fonksiyonCTX fctx = new fonksiyonCTX();
                DBCTX dctx = new DBCTX();
                foreach (var f in DBList)
                {
                    f.dbmodel = dctx.dbDondur(f.dbid);
                    f.fonksiyon = fctx.dbDondur(f.fonksiyonid);
                }
                return DBList;
            }
        }
        public WebServisModel wsDondur(int wsid)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from fonksiyondbMapping where id = @wsid";
                var sablonList = a.Query<WebServisModel>(sorgu, new { wsid = wsid }).FirstOrDefault();
                return sablonList;
            }
        }
        // (C) reate : Oluşturmak demek. Yeni bir tane kayıt eklemek. (Insert)
        public int wsOlustur(WebServisModel sbl)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "insert into fonksiyondbMapping (wsAdi, wsAciklama) OUTPUT INSERTED.id values (@wsAdi, @wsAciklama)";
                return a.QuerySingle<int>(sorgu, sbl);
            }
        }
    }
}