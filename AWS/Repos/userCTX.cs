using AWS.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWS.Repos
{
    public class userCTX
    {
        public List<UserModel> tumUserlariDondur()
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from kullanici";
                var userList = a.Query<UserModel>(sorgu).ToList();
                
                return userList;
            }
        }

        public UserModel kullaniciGiris(string username, string password)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from kullanici where username = @username and password = @password";
                var parameters = new { username = username, password = password };
                var userList = a.Query<UserModel>(sorgu,parameters).FirstOrDefault();

                return userList;
            }
        }

        public UserModel kullaniciDondur(string username)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from kullanici where username = @username";
                var parameters = new { username = username};
                var userList = a.Query<UserModel>(sorgu, parameters).FirstOrDefault();

                return userList;
            }
        }

        public UserModel kullaniciDondurById(int id)
        {
            var bag = new Baglanti();
            using (var a = bag.GetConnection())
            {
                string sorgu = "select * from kullanici where id = @id";
                var parameters = new { id = id };
                var userList = a.Query<UserModel>(sorgu, parameters).FirstOrDefault();

                return userList;
            }
        }
    }
}