using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWS.Models
{
    public class DBModel
    {
        public int id { get; set; }
        public int vtId { get; set; }
        public string adi { get; set; }
        public string aciklama { get; set; }
        public string connectionstring { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string hostname { get; set; }
        public DateTime created { set; get; }
        public string DBAdi { get; set; }
        public int userid { get; set; }


    }
}