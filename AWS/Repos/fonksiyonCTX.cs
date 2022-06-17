using AWS.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWS.Repos
{
    public class fonksiyonCTX
    {
        public List<fonksiyonModel> tumSablonlariDondur()
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from fonksiyon";
                var DBList = a.Query<fonksiyonModel>(sorgu).ToList();
                foreach(var b in DBList)
                {
                    WebServisCTX wsctx = new WebServisCTX();
                    b.wsm = wsctx.wsDondur(b.wsid);
                }
                return DBList;
            }
        }
        public List<fonksiyonModel> tumFonksiyonlariDondur(int wsid)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from fonksiyon where wsid = @wsid";
                var DBList = a.Query<fonksiyonModel>(sorgu, new { wsid = wsid}).ToList();
                foreach (var b in DBList)
                {
                    WebServisCTX wsctx = new WebServisCTX();
                    b.wsm = wsctx.wsDondur(b.wsid);
                }
                return DBList;
            }
        }
        public fonksiyonModel dbDondur(int dbId)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from fonksiyon where id = @id";
                var sablonList = a.Query<fonksiyonModel>(sorgu, new { id = dbId }).FirstOrDefault();
                WebServisCTX wsctx = new WebServisCTX();
                sablonList.wsm = wsctx.wsDondur(sablonList.wsid);
                return sablonList;

            }
        }

        public fonksiyonModel fonsiyonDondurByAd(string dbId)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from fonksiyon where fonksiyonAdi = @fonksiyonAdi";
                var sablonList = a.Query<fonksiyonModel>(sorgu, new { fonksiyonAdi = dbId }).FirstOrDefault();
                WebServisCTX wsctx = new WebServisCTX();
                sablonList.wsm = wsctx.wsDondur(sablonList.wsid);
                return sablonList;

            }
        }

        // (C) reate : Oluşturmak demek. Yeni bir tane kayıt eklemek. (Insert)
        public int fonksiyonOlustur(fonksiyonModel fm)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "insert into fonksiyon (fonksiyonAdi, method, wsid, sqlSorgu, dbid, islemTuru) OUTPUT INSERTED.id values (@fonksiyonAdi, @method, @wsid, @sqlSorgu, @dbid, @islemTuru)";
                return a.QuerySingle<int>(sorgu, fm);
            }
        }

    }
}