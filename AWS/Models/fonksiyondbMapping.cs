using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWS.Models
{
    public class fonksiyondbMapping
    {
        public int id { get; set; }
        public int fonksiyonid { get; set; }
        public int dbid { get; set; }
        public DBModel dbmodel { get; set; }
        public fonksiyonModel fonksiyon { get; set; }

    }
}