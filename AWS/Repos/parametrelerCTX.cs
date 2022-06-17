using AWS.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWS.Repos
{
    public class parametrelerCTX
    {
        public List<parametreModel> parametreDondurByFid(int fonksiyonID)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from parametre where fonksiyonID = @fonksiyonID";
                var DBList = a.Query<parametreModel>(sorgu, new { fonksiyonID = fonksiyonID }).ToList();
                
                return DBList;
            }
        }

        public int parametreOlustur(parametreModel sbl)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "insert into parametre (parametreAdi, fonksiyonID) OUTPUT INSERTED.id values (@parametreAdi, @fonksiyonID)";
                return a.QuerySingle<int>(sorgu, sbl);
            }
        }
    }
}