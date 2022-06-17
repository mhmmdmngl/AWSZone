using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWS.Models
{
    public class UserModel
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public int isActive { get; set; }
        public DateTime created { get; set; }

    }
}