using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWS.Models
{
    public class restapiModel
    {
        public int id { get; set; }
        public string apiurl { get; set; }
        public int userId { get; set; }
        public string method { get; set; }
        public string serviceName { get; set; }

    }
}