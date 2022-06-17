using AWS.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWS.Repos
{
    public class wsLogCTX
    {
        public List<wsLog> tumLoglariDondur()
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from wsLog";
                var DBList = a.Query<wsLog>(sorgu).ToList();
                return DBList;
            }
        }
        public List<wsLog> tumUserlariDondurByUserId(int userid)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from wsLog where userid = @userid";
                var DBList = a.Query<wsLog>(sorgu, new { userid = userid }).ToList();

                return DBList;
            }
        }

        public List<wsLog> tumUserlariDondurBywsUserId(int wsuserid)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from wsLog where wsuserid = @wsuserid";
                var DBList = a.Query<wsLog>(sorgu, new { wsuserid = wsuserid }).ToList();
                fonksiyonCTX fctx = new fonksiyonCTX();
                wsUserCTX wctx = new wsUserCTX();
                userCTX uctx = new userCTX();
                foreach(var b in DBList)
                {
                    b.usermodel = uctx.kullaniciDondurById(b.userid);
                    b.fonksiyon = fctx.dbDondur(b.fonksiyonid);
                    b.wsuser = wctx.userId(b.wsuserid);
                }
                return DBList;
            }
        }


        public int wsLogOlustur(wsLog fm)
        {
            if (fm == null)
                return -1;
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "insert into wsLog (userid, wsuserid, fonksiyonid,answertime,sorguzamani) OUTPUT INSERTED.id values (@userid, @wsuserid, @fonksiyonid,@answertime,@sorguzamani)";
                return a.QuerySingle<int>(sorgu, fm);
            }
        }
    }
}