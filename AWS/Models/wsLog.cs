using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWS.Models
{
    public class wsLog
    {
        public int id { get; set; }
        public int userid { get; set; }
        public int wsuserid { get; set; }

        public int fonksiyonid { get; set; }
        public string answertime { get; set; }
        public DateTime sorguZamani { get; set; }

        public UserModel usermodel { get; set; }
        public wsUser wsuser { get; set; }
        public fonksiyonModel fonksiyon { get; set; }



    }
}