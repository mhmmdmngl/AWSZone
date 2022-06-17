using AWS.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWS.Repos
{
    public class WebServisCTX
    {
        public List<WebServisModel> wsListesiDondur()
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from WebServis";
                var DBList = a.Query<WebServisModel>(sorgu).ToList();

                return DBList;
            }
        }
        public List<WebServisModel> wsListesiDondurByUserID(int uid)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from WebServis where userid = @uid";
                var parameters = new { uid = uid };
                var DBList = a.Query<WebServisModel>(sorgu, parameters).ToList();

                return DBList;
            }
        }
        public WebServisModel wsDondur(int wsid)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from WebServis where id = @wsid";
                var sablonList = a.Query<WebServisModel>(sorgu, new { wsid = wsid }).FirstOrDefault();
                return sablonList;
            }
        }
        public WebServisModel wsDondurByName(string name)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from WebServis where wsAdi = @name";
                var sablonList = a.Query<WebServisModel>(sorgu, new { name = name }).FirstOrDefault();
                return sablonList;
            }
        }
        // (C) reate : Oluşturmak demek. Yeni bir tane kayıt eklemek. (Insert)
        public int wsOlustur(WebServisModel sbl)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "insert into WebServis (wsAdi, wsAciklama, userid) OUTPUT INSERTED.id values (@wsAdi, @wsAciklama, @userid)";
                return a.QuerySingle<int>(sorgu, sbl);
            }
        }
    }
}