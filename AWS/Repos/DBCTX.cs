using AWS.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWS.Repos
{
    public class DBCTX
    {
        public List<DBModel> tumSablonlariDondur()
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from DB";
                var DBList = a.Query<DBModel>(sorgu).ToList();
               
                return DBList;
            }
        }

        public List<DBModel> tumdbDondurByUserId(int userid)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from DB where userid = @userid";
                var DBList = a.Query<DBModel>(sorgu, new { userid = userid }).ToList();

                return DBList;
            }
        }
        public DBModel dbDondur(int dbId)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from DB where id = @dbId";
                var sablonList = a.Query<DBModel>(sorgu, new { dbId = dbId }).FirstOrDefault();
                return sablonList;

            }
        }
        // (C) reate : Oluşturmak demek. Yeni bir tane kayıt eklemek. (Insert)
        public int dbOlustur(DBModel sbl)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "insert into DB (vtId, adi, aciklama, connectionstring, username, password, hostname, dbAdi, userid) OUTPUT INSERTED.id values (@vtId, @adi, @aciklama, @connectionstring, @username, @password, @hostname, @dbAdi, @userid)";
                return a.QuerySingle<int>(sorgu, sbl);
            }
        }
        
    }
}