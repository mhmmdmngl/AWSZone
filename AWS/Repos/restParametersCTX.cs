using AWS.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWS.Repos
{
    public class restParametersCTX
    {
        public List<restParemetersModel> restParamDondurDondur()
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from restParameters";
                var DBList = a.Query<restParemetersModel>(sorgu).ToList();

                return DBList;
            }
        }

        public List<restParemetersModel> restParamDondurDondurbyRestId(int restId)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from restParameters where restId = @restId";
                var DBList = a.Query<restParemetersModel>(sorgu, new { restId = restId }).ToList();

                return DBList;
            }
        }

       

        public restParemetersModel rpDondur(int rpId)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from restParameters where id = @rpId";
                var sablonList = a.Query<restParemetersModel>(sorgu, new { rpId = rpId }).FirstOrDefault();
                return sablonList;

            }
        }
        // (C) reate : Oluşturmak demek. Yeni bir tane kayıt eklemek. (Insert)
        public int rpEkle(restParemetersModel rp)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "insert into restParameters (valueName, restId) OUTPUT INSERTED.id values (@valueName, @restId)";
                return a.QuerySingle<int>(sorgu, rp);
            }
        }
    }
}