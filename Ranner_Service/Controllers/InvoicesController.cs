using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Ranner_Service.DataAccess;
using Ranner_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ranner_Service.Controllers
{
    public class InvoicesController : ApiController
    {
        // GET api/<controller>
        public List<Invoice> Get()
        {
            return SqlDataAccess.GetInvoices();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        [ActionName("Complex")]
        public void Post([FromBody] JObject jsonResult)
        {
            int orderNr = (int)jsonResult["orderNr"];
            DateTime? orderDate = (jsonResult["orderDate"] != null) ? (DateTime?)jsonResult["orderDate"] : null;
            int invoiceNr = (int)jsonResult["invoiceNr"];
            DateTime? invoiceDate = (jsonResult["invoiceDate"] != null) ? (DateTime?)jsonResult["invoiceDate"] : null;
            JToken jsonCustomer = jsonResult["customer"];
            int customerId = (int)jsonCustomer["id"];
            string referenceNumber = (string)jsonResult["referenceNumber"];
            string freighterName = (string)jsonResult["freighterName"];
            string freightersInvNumber = (string)jsonResult["freightersInvNumber"];
            DateTime? freightersInvArrived = (jsonResult["freightersInvArrived"] != null) ? (DateTime?)jsonResult["freightersInvArrived"] : null;
            DateTime? freighterPaidOn = (jsonResult["freighterPaidOn"] != null) ? (DateTime?)jsonResult["freighterPaidOn"] : null;
            DateTime? customerPaidOn = (jsonResult["customerPaidOn"] != null) ? (DateTime?)jsonResult["customerPaidOn"] : null;
            DateTime? shipDate = (jsonResult["shipDate"] != null) ? (DateTime?)jsonResult["shipDate"] : null;
            JToken jsonAddress = jsonResult["pickupAddress"];
            int shipAddressId = (int)jsonAddress["id"];
            JToken[] jsondeliveryAddresses = jsonResult["deliveryAddresses"].ToArray();
            List<Address> deliveryAddresses = new List<Address>();
            foreach (JToken jsonDelAddress in jsondeliveryAddresses)
            {
                int deliveryaddressId = (int)jsonDelAddress["id"];
                DateTime? deliveryTime = (DateTime?)jsonDelAddress["deliveryTime"];
                deliveryAddresses.Add(new Address(deliveryaddressId, null, null, null, null, null, deliveryTime));
            }
            
            string product = (string)jsonResult["product"];
            double amountFreighter = (double)jsonResult["amountFreighter"];
            double amountCustomer = (double)jsonResult["amountCustomer"];


            Invoice newInvoice = new Invoice(orderNr, orderDate, invoiceNr, invoiceDate, new Customer(customerId, null, null, null, null, null), referenceNumber,
                freighterName, freightersInvNumber, freightersInvArrived, freighterPaidOn, customerPaidOn, shipDate, product, new Address(shipAddressId, null, null, null, null, null),
                deliveryAddresses, amountFreighter, amountCustomer);

            SqlDataAccess.InsertInvoice(newInvoice);
        }

        // PUT api/<controller>/5
        public void Put([FromBody] JObject jsonResult)
        {
            int invoiceId = (int)jsonResult["invoiceId"];
            int orderNr = (int)jsonResult["orderNr"];
            DateTime? orderDate = (jsonResult["orderDate"] != null) ? (DateTime?)jsonResult["orderDate"] : null;
            int invoiceNr = (int)jsonResult["invoiceNr"];
            DateTime? invoiceDate = (jsonResult["invoiceDate"] != null) ? (DateTime?)jsonResult["invoiceDate"] : null;
            JToken jsonCustomer = jsonResult["customer"];
            int customerId = (int)jsonCustomer["id"];
            string referenceNumber = (string)jsonResult["referenceNumber"];
            string freighterName = (string)jsonResult["freighterName"];
            string freightersInvNumber = (string)jsonResult["freightersInvNumber"];
            DateTime? freightersInvArrived = (jsonResult["freightersInvArrived"] != null) ? (DateTime?)jsonResult["freightersInvArrived"] : null;
            DateTime? freighterPaidOn = (jsonResult["freighterPaidOn"] != null) ? (DateTime?)jsonResult["freighterPaidOn"] : null;
            DateTime? customerPaidOn = (jsonResult["customerPaidOn"] != null) ? (DateTime?)jsonResult["customerPaidOn"] : null;
            DateTime? shipDate = (jsonResult["shipDate"] != null) ? (DateTime?)jsonResult["shipDate"] : null;
            JToken jsonAddress = jsonResult["pickupAddress"];
            int shipAddressId = (int)jsonAddress["id"];
            JToken[] jsondeliveryAddresses = jsonResult["deliveryAddresses"].ToArray();
            List<Address> deliveryAddresses = new List<Address>();
            foreach (JToken jsonDelAddress in jsondeliveryAddresses)
            {
                int deliveryaddressId = (int)jsonDelAddress["id"];
                DateTime? deliveryTime = (DateTime?)jsonDelAddress["deliveryTime"];
                deliveryAddresses.Add(new Address(deliveryaddressId, null, null, null, null, null, deliveryTime));
            }

            string product = (string)jsonResult["product"];
            double amountFreighter = (double)jsonResult["amountFreighter"];
            double amountCustomer = (double)jsonResult["amountCustomer"];

            Invoice invoice = new Invoice(invoiceId, orderNr, orderDate, invoiceNr, invoiceDate, new Customer(customerId, null, null, null, null, null), referenceNumber,
                freighterName, freightersInvNumber, freightersInvArrived, freighterPaidOn, customerPaidOn, shipDate, product, new Address(shipAddressId, null, null, null, null, null),
                deliveryAddresses, amountFreighter, amountCustomer);

            SqlDataAccess.UpdateInvoice(invoice);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            SqlDataAccess.DeleteInvoice(id);
        }
    }
}