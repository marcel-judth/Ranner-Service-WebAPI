using Microsoft.Ajax.Utilities;
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

        #region invoices
        public static List<Invoice> GetInvoices()
        {
            List<Invoice> result = new List<Invoice>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // This code uses an SqlCommand based on the SqlConnection.
                //
                using (SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Invoices]", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //always check if the value is null -> otherwise it raises an exception
                        int invoiceId = reader.IsDBNull(reader.GetOrdinal("invoiceId")) ? 0 : reader.GetInt32(reader.GetOrdinal("invoiceId"));
                        int orderNr = reader.IsDBNull(reader.GetOrdinal("orderNr")) ? 0 : reader.GetInt32(reader.GetOrdinal("orderNr"));
                        DateTime? orderdate = reader.IsDBNull(reader.GetOrdinal("orderDate")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("orderDate"));
                        int invoiceNr = reader.IsDBNull(reader.GetOrdinal("invoiceNr")) ? 0 : reader.GetInt32(reader.GetOrdinal("invoiceNr"));
                        DateTime? invoicedate = reader.IsDBNull(reader.GetOrdinal("invoiceDate")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("invoiceDate"));
                        int customerId = reader.IsDBNull(reader.GetOrdinal("customerId")) ? 0 : reader.GetInt32(reader.GetOrdinal("customerId"));
                        Customer customer = SqlDataAccess.GetCustomerById(customerId);
                        string referenceNr = reader.IsDBNull(reader.GetOrdinal("referenceNumber")) ? null : reader.GetString(reader.GetOrdinal("referenceNumber"));
                        string freighterName = reader.IsDBNull(reader.GetOrdinal("freighterName")) ? null : reader.GetString(reader.GetOrdinal("freighterName"));
                        string freighterInvNr = reader.IsDBNull(reader.GetOrdinal("freighterInvNr")) ? null : reader.GetString(reader.GetOrdinal("freighterInvNr"));
                        DateTime? freightersInvArrived = reader.IsDBNull(reader.GetOrdinal("freighterInvArrived")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("freighterInvArrived"));
                        DateTime? freighterPaidOn = reader.IsDBNull(reader.GetOrdinal("freighterPaidOn")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("freighterPaidOn"));
                        DateTime? customerPaidOn = reader.IsDBNull(reader.GetOrdinal("customerPaidOn")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("customerPaidOn"));
                        DateTime? shipDate = reader.IsDBNull(reader.GetOrdinal("shipDate")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("shipDate"));
                        int pickupAddressId = reader.IsDBNull(reader.GetOrdinal("pickupAddressId")) ? 0 : reader.GetInt32(reader.GetOrdinal("pickupAddressId"));
                        Address shipAddress = SqlDataAccess.GetAddresseById(pickupAddressId);
                        List<Address> deliveryAddresses = SqlDataAccess.GetDeliveryAddressesByInvId(invoiceId);
                        string product = reader.IsDBNull(reader.GetOrdinal("product")) ? null : reader.GetString(reader.GetOrdinal("product"));
                        double amountfreighter = reader.IsDBNull(reader.GetOrdinal("amountFreighter")) ? 0 : reader.GetDouble(reader.GetOrdinal("amountFreighter"));
                        double amountCustomer = reader.IsDBNull(reader.GetOrdinal("amountCustomer")) ? 0 : reader.GetDouble(reader.GetOrdinal("amountCustomer"));
                        result.Add(new Invoice(invoiceId, orderNr, orderdate, invoiceNr, invoicedate, customer, referenceNr, freighterName, freighterInvNr, freightersInvArrived,
                            freighterPaidOn, customerPaidOn, shipDate, product, shipAddress, deliveryAddresses, amountfreighter, amountCustomer));
                    }
                }
                con.Close();
            }
            return result;
        }
        private static int GetCurrentInvoiceId()
        {
            int result = -1;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // This code uses an SqlCommand based on the SqlConnection.
                //
                using (SqlCommand command = new SqlCommand("SELECT IDENT_CURRENT('Invoices'); ", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = (int)reader.GetDecimal(0);
                    }
                }
                con.Close();
            }
            return result;
        }

        public static void InsertInvoice(Invoice invoice)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Invoices(orderNr,orderDate, invoiceNr, invoiceDate, customerId, referenceNumber, freighterName, freighterInvNr, " +
                    "freighterInvArrived, freighterPaidOn, customerPaidOn, shipDate, pickupAddressId, product, amountFreighter, amountCustomer)" +
                    " VALUES(@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9, @param10, @param11, @param12, @param13, @param14, @param15, @param16)";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@param1", (invoice.orderNr <= 0) ? DBNull.Value : (object)invoice.orderNr);
                    cmd.Parameters.AddWithValue("@param2", ((object)invoice.orderDate) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param3", (invoice.invoiceNr <= 0) ? DBNull.Value : (object)invoice.invoiceNr);
                    cmd.Parameters.AddWithValue("@param4", ((object)invoice.invoiceDate) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param5", ((object)invoice.customer.id) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param6", ((object)invoice.referenceNumber) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param7", ((object)invoice.freighterName) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param8", ((object)invoice.freightersInvNumber) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param9", ((object)invoice.freightersInvArrived) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param10", ((object)invoice.freighterPaidOn) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param11", ((object)invoice.customerPaidOn) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param12", ((object)invoice.shipDate) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param13", ((object)invoice.pickupAddress.id) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param14", ((object)invoice.product) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param15", ((object)invoice.amountFreighter) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param16", ((object)invoice.amountCustomer) ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }
                int currentInvoiceId = SqlDataAccess.GetCurrentInvoiceId();

                if(currentInvoiceId > -1)
                    foreach(Address deliveryAddress in invoice.deliveryAddresses)
                        SqlDataAccess.InsertDeliverTo(currentInvoiceId, deliveryAddress.id, deliveryAddress.deliveryTime);
                    
                connection.Close();
            }
        }

        public static void UpdateInvoice(Invoice invoice)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE Invoices SET orderNr = @param1, orderDate = @param2, invoiceNr = @param3, invoiceDate = @param4, customerId = @param5, " +
                    " referenceNumber = @param6, freighterName = @param7, freighterInvNr = @param8, freighterInvArrived = @param9, freighterPaidOn = @param10, customerPaidOn = @param11," +
                    "shipDate = @param12, pickupAddressId = @param13, product = @param14, amountFreighter = @param15, amountCustomer = @param16 WHERE invoiceId = " + invoice.invoiceId;

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@param1", (invoice.orderNr <= 0) ? DBNull.Value : (object)invoice.orderNr);
                    cmd.Parameters.AddWithValue("@param2", ((object)invoice.orderDate) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param3", (invoice.invoiceNr <= 0) ? DBNull.Value : (object)invoice.invoiceNr);
                    cmd.Parameters.AddWithValue("@param4", ((object)invoice.invoiceDate) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param5", ((object)invoice.customer.id) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param6", ((object)invoice.referenceNumber) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param7", ((object)invoice.freighterName) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param8", ((object)invoice.freightersInvNumber) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param9", ((object)invoice.freightersInvArrived) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param10", ((object)invoice.freighterPaidOn) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param11", ((object)invoice.customerPaidOn) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param12", ((object)invoice.shipDate) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param13", ((object)invoice.pickupAddress.id) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param14", ((object)invoice.product) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param15", ((object)invoice.amountFreighter) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@param16", ((object)invoice.amountCustomer) ?? DBNull.Value);

                    cmd.ExecuteNonQuery();
                }

                SqlDataAccess.DeleteDeliverToFromInvoice(invoice.invoiceId);
                foreach (Address deliveryAddress in invoice.deliveryAddresses)
                    SqlDataAccess.InsertDeliverTo(invoice.invoiceId, deliveryAddress.id, deliveryAddress.deliveryTime);

                connection.Close();
            }
        }

        public static void DeleteInvoice(int invoiceId)
        {
            SqlDataAccess.DeleteDeliverToFromInvoice(invoiceId);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM invoices WHERE invoiceId = " + invoiceId;
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        #endregion

        #region deliverTo
        public static void InsertDeliverTo(int invoiceId, int addressId, DateTime? deliveryTime)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO deliverTo(invoiceId, deliveryAddressId, deliveryTime) VALUES(@param1,@param2,@param3)";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@param1", invoiceId);
                    cmd.Parameters.AddWithValue("@param2", addressId);
                    cmd.Parameters.AddWithValue("@param3", deliveryTime);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        private static void DeleteDeliverToFromInvoice(int invoiceId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM deliverTo WHERE invoiceId = " + invoiceId;
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        #endregion

        #region addresses
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
                con.Close();
            }
            return result;
        }
        private static List<Address> GetDeliveryAddressesByInvId(int invoiceId)
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
                using (SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[deliverTo] WHERE [invoiceId] = " + invoiceId, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(GetAddresseById(reader.GetInt32(1)));
                    }
                }
                con.Close();
            }
            return result;
        }
        private static Address GetAddresseById(int shipAddressId)
        {
            Address result = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // This code uses an SqlCommand based on the SqlConnection.
                //
                using (SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Addresses] WHERE [addressID] = " + shipAddressId, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = new Address(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                            reader.GetString(3), reader.GetString(4), reader.GetString(5));
                    }
                }
                con.Close();
            }
            return result;
        }

        public static void InsertAddress(Address address)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Addresses(shipName,address, zipcode, city, country) VALUES(@param1,@param2,@param3,@param4,@param5)";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@param1", address.name);
                    cmd.Parameters.AddWithValue("@param2", address.address);
                    cmd.Parameters.AddWithValue("@param3", address.zipCode);
                    cmd.Parameters.AddWithValue("@param4", address.city);
                    cmd.Parameters.AddWithValue("@param5", address.country);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public static void UpdateAddress(Address address)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE Addresses SET shipName = @param1, address = @param2, zipcode = @param3, city = @param4, country = @param5 " +
                    "WHERE addressID = " + address.id;
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@param1", address.name);
                    cmd.Parameters.AddWithValue("@param2", address.address);
                    cmd.Parameters.AddWithValue("@param3", address.zipCode);
                    cmd.Parameters.AddWithValue("@param4", address.city);
                    cmd.Parameters.AddWithValue("@param5", address.country);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public static void DeleteAddressById(int addressId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Addresses WHERE addressID = " + addressId;
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        #endregion

        #region customers
        public static List<Customer> GetCustomers()
        {
            List<Customer> result = new List<Customer>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // This code uses an SqlCommand based on the SqlConnection.
                //
                using (SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Customers]", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Customer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                            reader.GetString(3), reader.GetString(4), reader.GetString(5)));
                    }
                }
                con.Close();
            }
            return result;
        }
        private static Customer GetCustomerById(int customerId)
        {
            Customer result = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // This code uses an SqlCommand based on the SqlConnection.
                //
                using (SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[Customers] WHERE [customerID] = " + customerId, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = new Customer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                            reader.GetString(3), reader.GetString(4), reader.GetString(5));
                    }
                }
                con.Close();
            }
            return result;
        }

        public static void InsertCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Customers(customerName,address, zipcode, city, customerUID) VALUES(@param1,@param2,@param3,@param4,@param5)";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@param1", customer.name);
                    cmd.Parameters.AddWithValue("@param2", customer.address);
                    cmd.Parameters.AddWithValue("@param3", customer.zipCode);
                    cmd.Parameters.AddWithValue("@param4", customer.city);
                    cmd.Parameters.AddWithValue("@param5", customer.uid);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public static void UpdateCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "UPDATE Customers SET customerName = @param1, address = @param2, zipcode = @param3, city = @param4, customerUID = @param5 " +
                    "WHERE customerID = " + customer.id;
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@param1", customer.name);
                    cmd.Parameters.AddWithValue("@param2", customer.address);
                    cmd.Parameters.AddWithValue("@param3", customer.zipCode);
                    cmd.Parameters.AddWithValue("@param4", customer.city);
                    cmd.Parameters.AddWithValue("@param5", customer.uid);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public static void DeleteCustomerById(int customerId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Customers WHERE customerID = " + customerId;
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        #endregion
    }
}