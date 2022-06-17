using AWS.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWS.Repos
{
    public class restApiCTX
    {
        public List<restapiModel> restApiListesiDondur()
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from restapi";
                var fonksiyonlist = a.Query<restapiModel>(sorgu).ToList();
                return fonksiyonlist;
            }
        }

        public restapiModel restApiListesiDondur(int id)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from restapi where id = @id";
                var fonksiyonlist = a.Query<restapiModel>(sorgu, new { id = id }).FirstOrDefault();
                return fonksiyonlist;
            }
        }

        public restapiModel restApiDondurByKullanici(int userId, string servicename)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from restapi where userId = @userId and servicename = @servicename";
                var fonksiyonlist = a.Query<restapiModel>(sorgu, new { userId = userId , servicename = servicename }).FirstOrDefault();
                return fonksiyonlist;
            }
        }

        public List<restapiModel> restApiListesiDondurbyUserId(int userId)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from restapi where userId = @userId";
                var fonksiyonlist = a.Query<restapiModel>(sorgu, new { userId = userId }).ToList();
                return fonksiyonlist;
            }
        }

      
        // (C) reate : Oluşturmak demek. Yeni bir tane kayıt eklemek. (Insert)
        public int restApiOlustur(restapiModel ram)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "insert into restapi (apiurl, userId, method,servicename) OUTPUT INSERTED.id values (@apiurl, @userId, @method, @servicename)";
                return a.QuerySingle<int>(sorgu, ram);
            }
        }
    }
}