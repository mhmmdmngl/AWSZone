using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWS.Models
{
    public class WebServisModel
    {
        public int id { get; set; }
        public string wsAdi { get; set; }
        public string wsAciklama { get; set; }
        public DateTime created { get; set; }
        public int userid { get; set; }
    }
}