using Ranner_Service.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace Ranner_Service.DataAccess
{
    public class SqlDataAccess
    {
        private static readonly string connectionString = "Data Source=ranner-service-server.database.windows.net;Initial Catalog=Ranner-Service-Database;User ID=ranner-service-admin;Password=Database1!";


        public static List<Object> GetInvoices()
        {
            connect();
            return null;
        }

        public static List<Address> GetAddresses()
        {
            List<Address> result = new List<Address>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // This code uses an SqlCommand based on the SqlConnection.
                //
                using (SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Addresses]", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Address(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), 
                            reader.GetString(3), reader.GetString(4), reader.GetString(5)));
                    }
                }
            }
            return result;
        }

        #region private-methods
        private static void connect()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
        }
        #endregion
    }
}