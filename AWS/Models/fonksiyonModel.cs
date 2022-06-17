using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWS.Models
{
    public class fonksiyonModel
    {
        public int id { get; set; }
        public string fonksiyonAdi { get; set; }
        public string method { get; set; }
        public int wsid { get; set; }
        public int dbid { get; set; }
        public string sqlSorgu { get; set; }
        public string islemTuru { set; get; }
        public WebServisModel wsm { get; set; }

    }
}