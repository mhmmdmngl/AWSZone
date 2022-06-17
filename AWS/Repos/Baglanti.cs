using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AWS.Repos
{
    public class Baglanti
    {
        public SqlConnection GetConnection()
        {
            SqlConnection sc = new SqlConnection();
            sc.ConnectionString = System.Configuration.ConfigurationManager.
    ConnectionStrings["awsDB"].ConnectionString;
            sc.Open();
            return sc;
        }
    }
}