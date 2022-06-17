using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWS.Models
{
    public class wsUser
    {
        public int id { get; set; }
        public string wsKullanici { get; set; }
        public string wsPassword { get; set; }
        public int userId { get; set; }
        public UserModel um { set; get; }


    }
}