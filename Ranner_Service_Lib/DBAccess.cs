using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Ranner_Service_Lib
{
    public class DBAccess
    {
        private static readonly string connectionString = "Data Source=ranner-service-server.database.windows.net;User ID=ranner-service-admin;Password=Database1!";

        public DBAccess(string connectionString)
        {
            
        }

        public static List<Object> GetInvoices()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return null;
        }
    }
}
