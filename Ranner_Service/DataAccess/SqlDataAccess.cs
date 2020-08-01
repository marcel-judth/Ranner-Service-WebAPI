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
                        int palletChange = reader.IsDBNull(reader.GetOrdinal("palletChange")) ? 0 : reader.GetInt32(reader.GetOrdinal("palletChange"));
                        List<Pallet> allPallets = new List<Pallet>();
                        if (palletChange == 1)
                            allPallets = SqlDataAccess.GetPalletsByInvId(invoiceId);
                        result.Add(new Invoice(invoiceId, orderNr, orderdate, invoiceNr, invoicedate, customer, referenceNr, freighterName, freighterInvNr, freightersInvArrived,
                            freighterPaidOn, customerPaidOn, shipDate, product, shipAddress, deliveryAddresses, (palletChange == 1), amountfreighter, amountCustomer, allPallets));
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

        private static int GetCurrentOrderNr()
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
                using (SqlCommand command = new SqlCommand("SELECT Max(orderNr) from Invoices;", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = (int)reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            return result;
        }

        private static int GetCurrentInvoiceNr()
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
                using (SqlCommand command = new SqlCommand("SELECT Max(invoiceNr) from Invoices; ", con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = (int)reader.GetInt32(0);
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
                string sql = "INSERT INTO Invoices(orderNr, orderDate, customerId, referenceNumber, freighterName, freighterInvNr, " +
                    "freighterInvArrived, freighterPaidOn, customerPaidOn, shipDate, pickupAddressId, product, amountFreighter, amountCustomer, palletChange)" +
                    " VALUES(@orderNr, @orderDate, @customerId, @referenceNumber, @freighterName, @freightersInvNumber, @freightersInvArrived, @freighterPaidOn, " +
                    "@customerPaidOn, @shipDate, @pickupAddressId, @product, @amountFreighter, @amountCustomer, @palletChange)";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@orderNr", (SqlDataAccess.GetCurrentOrderNr() + 1));
                    cmd.Parameters.AddWithValue("@orderDate", ((object)invoice.orderDate) ?? DBNull.Value);
                    //cmd.Parameters.AddWithValue("@invoiceNr", (invoice.invoiceNr <= 0) ? DBNull.Value : (object)invoice.invoiceNr);
                    //cmd.Parameters.AddWithValue("@invoiceDate", ((object)invoice.invoiceDate) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@customerId", ((object)invoice.customer.id) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@referenceNumber", ((object)invoice.referenceNumber) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@freighterName", ((object)invoice.freighterName) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@freightersInvNumber", ((object)invoice.freightersInvNumber) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@freightersInvArrived", ((object)invoice.freightersInvArrived) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@freighterPaidOn", ((object)invoice.freighterPaidOn) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@customerPaidOn", ((object)invoice.customerPaidOn) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@shipDate", ((object)invoice.shipDate) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@pickupAddressId", ((object)invoice.pickupAddress.id) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@product", ((object)invoice.product) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@amountFreighter", ((object)invoice.amountFreighter) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@amountCustomer", ((object)invoice.amountCustomer) ?? DBNull.Value);
                    if (invoice.palletChange)
                        cmd.Parameters.AddWithValue("@palletChange", 1);
                    else
                        cmd.Parameters.AddWithValue("@palletChange", 0);
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
                string sql = "UPDATE Invoices SET orderDate = @orderDate, invoiceNr = @invoiceNr, invoiceDate = @invoiceDate, customerId = @customerId, " +
                    " referenceNumber = @referenceNumber, freighterName = @freighterName, freighterInvNr = @freighterInvNr, freighterInvArrived = @freighterInvArrived," +
                    " freighterPaidOn = @freighterPaidOn, customerPaidOn = @customerPaidOn, shipDate = @shipDate, pickupAddressId = @pickupAddressId, product = @product, " +
                    "amountFreighter = @amountFreighter, amountCustomer = @amountCustomer, palletChange = @palletChange WHERE invoiceId = " + invoice.invoiceId;

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@orderDate", ((object)invoice.orderDate) ?? DBNull.Value);
                    if (invoice.invoiceNr > 0)
                        cmd.Parameters.AddWithValue("@invoiceNr", SqlDataAccess.GetCurrentInvoiceNr() + 1);
                    else
                        cmd.Parameters.AddWithValue("@invoiceNr", DBNull.Value);
                    cmd.Parameters.AddWithValue("@invoiceDate", ((object)invoice.invoiceDate) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@customerId", ((object)invoice.customer.id) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@referenceNumber", ((object)invoice.referenceNumber) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@freighterName", ((object)invoice.freighterName) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@freighterInvNr", ((object)invoice.freightersInvNumber) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@freighterInvArrived", ((object)invoice.freightersInvArrived) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@freighterPaidOn", ((object)invoice.freighterPaidOn) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@customerPaidOn", ((object)invoice.customerPaidOn) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@shipDate", ((object)invoice.shipDate) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@pickupAddressId", ((object)invoice.pickupAddress.id) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@product", ((object)invoice.product) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@amountFreighter", ((object)invoice.amountFreighter) ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@amountCustomer", ((object)invoice.amountCustomer) ?? DBNull.Value);
                    if (invoice.palletChange)
                    {
                        cmd.Parameters.AddWithValue("@palletChange", 1);
                        SqlDataAccess.DeletePalletsFromInvoice(invoice.invoiceId);
                        foreach (Pallet pallet in invoice.pallets)
                            SqlDataAccess.InsertPallet(pallet, invoice.invoiceId);
                    }
                    else
                        cmd.Parameters.AddWithValue("@palletChange", 0);
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

        #region pallets
        private static List<Pallet> GetPalletsByInvId(int invoiceId)
        {
            List<Pallet> result = new List<Pallet>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                //
                // Open the SqlConnection.
                //
                con.Open();
                //
                // This code uses an SqlCommand based on the SqlConnection.
                //
                using (SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[pallets] WHERE [invoiceId] = " + invoiceId, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int palletId = reader.IsDBNull(reader.GetOrdinal("palletId")) ? 0 : reader.GetInt32(reader.GetOrdinal("palletId"));
                        int amount = reader.IsDBNull(reader.GetOrdinal("amount")) ? 0 : reader.GetInt32(reader.GetOrdinal("amount"));
                        string type = reader.IsDBNull(reader.GetOrdinal("type")) ? null : reader.GetString(reader.GetOrdinal("type"));
                        string place = reader.IsDBNull(reader.GetOrdinal("place")) ? null : reader.GetString(reader.GetOrdinal("place"));

                        result.Add(new Pallet(palletId, amount, type, place));
                    }
                }
                con.Close();
            }
            return result;
        }
        private static void InsertPallet(Pallet pallet, int invoiceId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO pallets(invoiceId, amount, type, place) VALUES(@invoiceId,@amount,@type,@place)";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@invoiceId", invoiceId);
                    cmd.Parameters.AddWithValue("@amount", pallet.amount);
                    cmd.Parameters.AddWithValue("@type", pallet.type);
                    cmd.Parameters.AddWithValue("@place", pallet.place);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }


        private static void DeletePalletsFromInvoice(int invoiceId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM pallets WHERE invoiceId = " + invoiceId;
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