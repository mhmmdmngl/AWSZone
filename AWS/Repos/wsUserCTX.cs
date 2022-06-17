using AWS.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWS.Repos
{
    public class wsUserCTX
    {
        public List<wsUser> tumUserlariDondur()
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from wsUser";
                var DBList = a.Query<wsUser>(sorgu).ToList();
                foreach (var b in DBList)
                {
                    userCTX wsctx = new userCTX();
                    b.um = wsctx.kullaniciDondurById(b.userId);
                }
                return DBList;
            }
        }
        public wsUser userDogrula(string wsKullanici, string wsPassword)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from wsUser where wsKullanici = @wsKullanici and wsPassword = @wsPassword";
                var DBList = a.Query<wsUser>(sorgu, new { wsKullanici = wsKullanici, wsPassword = wsPassword }).FirstOrDefault();

                return DBList;
            }
        }
        public List<wsUser> tumUserlariDondurByUserId(int userId)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from wsUser where userId = @userId";
                var DBList = a.Query<wsUser>(sorgu, new { userId = userId }).ToList();
                
                return DBList;
            }
        }
        public wsUser userId(int id)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from wsUser where id = @id";
                var sablonList = a.Query<wsUser>(sorgu, new { id = id }).FirstOrDefault();

                return sablonList;

            }
        }


        public int wsUserOlustur(wsUser fm)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "insert into WSUser (wsKullanici, wsPassword, userId) OUTPUT INSERTED.id values (@wsKullanici, @wsPassword, @userId)";
                return a.QuerySingle<int>(sorgu, fm);
            }
        }
    }
}