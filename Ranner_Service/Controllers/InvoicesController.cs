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
        public Invoice Get(int id)
        {
            return SqlDataAccess.GetInvoiceById(id);
        }

        // POST api/<controller>
        [HttpPost]
        [ActionName("Complex")]
        public void Post([FromBody] JObject jsonResult)
        {
            DateTime? orderDate = (jsonResult["orderDate"] != null) ? (DateTime?)jsonResult["orderDate"] : null;
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
            DateTime? deliveryDate = (jsonResult["deliveryDate"] != null) ? (DateTime?)jsonResult["deliveryDate"] : null;

            JToken[] jsondeliveryAddresses = jsonResult["deliveryAddresses"].ToArray();
            List<Address> deliveryAddresses = new List<Address>();
            foreach (JToken jsonDelAddress in jsondeliveryAddresses)
            {
                int deliveryaddressId = (int)jsonDelAddress["id"];
                DateTime? deliveryTime = (DateTime?)jsonDelAddress["deliveryTime"];
                deliveryAddresses.Add(new Address(deliveryaddressId, null, null, null, null, null, deliveryTime));
            }
            JToken[] jsonpickupAddresses = jsonResult["pickupAddresses"].ToArray();
            List<Address> pickupAddresses = new List<Address>();
            foreach (JToken jsonPickupAddress in jsonpickupAddresses)
            {
                int pickupaddressId = (int)jsonPickupAddress["id"];
                DateTime? deliveryTime = (DateTime?)jsonPickupAddress["deliveryTime"];
                pickupAddresses.Add(new Address(pickupaddressId, null, null, null, null, null, deliveryTime));
            }
            string product = (string)jsonResult["product"];
            double priceFreighter = (double)jsonResult["priceFreighter"];
            double priceCustomer = (double)jsonResult["priceCustomer"];
            int? amount = (int?)jsonResult["amount"];
            bool palletChange = (bool)jsonResult["palletChange"];
            string note = (string)jsonResult["note"];

            Invoice newInvoice = new Invoice(0, orderDate, -1, invoiceDate, new Customer(customerId, null, null, null, null, null), referenceNumber,
                freighterName, freightersInvNumber, freightersInvArrived, freighterPaidOn, customerPaidOn, shipDate, deliveryDate, product, pickupAddresses,
                deliveryAddresses, palletChange, priceFreighter, priceCustomer, amount, note);

            SqlDataAccess.InsertInvoice(newInvoice);
        }

        // PUT api/<controller>/5
        public void Put([FromBody] JObject jsonResult)
        {
            int invoiceId = (int)jsonResult["invoiceId"];
            DateTime? orderDate = (jsonResult["orderDate"] != null) ? (DateTime?)jsonResult["orderDate"] : null;
            DateTime? invoiceDate = (jsonResult["invoiceDate"] != null) ? (DateTime?)jsonResult["invoiceDate"] : null;
            int? invoiceNr = jsonResult["invoiceNr"] != null ? (int?)jsonResult["invoiceNr"] : null;
            JToken jsonCustomer = jsonResult["customer"];
            int customerId = (int)jsonCustomer["id"];
            string referenceNumber = (string)jsonResult["referenceNumber"];
            string freighterName = (string)jsonResult["freighterName"];
            string freightersInvNumber = (string)jsonResult["freightersInvNumber"];
            DateTime? freightersInvArrived = (jsonResult["freightersInvArrived"] != null) ? (DateTime?)jsonResult["freightersInvArrived"] : null;
            DateTime? freighterPaidOn = (jsonResult["freighterPaidOn"] != null) ? (DateTime?)jsonResult["freighterPaidOn"] : null;
            DateTime? customerPaidOn = (jsonResult["customerPaidOn"] != null) ? (DateTime?)jsonResult["customerPaidOn"] : null;
            DateTime? shipDate = (jsonResult["shipDate"] != null) ? (DateTime?)jsonResult["shipDate"] : null;
            DateTime? deliveryDate = (jsonResult["deliveryDate"] != null) ? (DateTime?)jsonResult["deliveryDate"] : null;
            JToken[] jsondeliveryAddresses = jsonResult["deliveryAddresses"].ToArray();
            List<Address> deliveryAddresses = new List<Address>();
            foreach (JToken jsonDelAddress in jsondeliveryAddresses)
            {
                int deliveryaddressId = (int)jsonDelAddress["id"];
                DateTime? deliveryTime = (DateTime?)jsonDelAddress["deliveryTime"];
                deliveryAddresses.Add(new Address(deliveryaddressId, null, null, null, null, null, deliveryTime));
            }

            JToken[] jsonpickupAddresses = jsonResult["pickupAddresses"].ToArray();
            List<Address> pickupAddresses = new List<Address>();
            foreach (JToken jsonPickupAddress in jsonpickupAddresses)
            {
                int pickupaddressId = (int)jsonPickupAddress["id"];
                DateTime? deliveryTime = (DateTime?)jsonPickupAddress["deliveryTime"];
                pickupAddresses.Add(new Address(pickupaddressId, null, null, null, null, null, deliveryTime));
            }

            string product = (string)jsonResult["product"];
            double priceFreighter = (double)jsonResult["priceFreighter"];
            double priceCustomer = (double)jsonResult["priceCustomer"];
            bool palletChange = (bool)jsonResult["palletChange"];
            int? amount = (int?)jsonResult["amount"];
            string note = (string)jsonResult["note"];

            JToken[] jsonPallets = jsonResult["pallets"].ToArray();
            List<Pallet> pallets = new List<Pallet>();
            foreach (JToken jTokenPallets in jsonPallets)
            {
                int palletId = (int)jTokenPallets["palletId"];
                int pamount = (int)jTokenPallets["amount"];
                string type = jTokenPallets["type"].ToString();
                string place = jTokenPallets["place"].ToString();

                pallets.Add(new Pallet(palletId, pamount, type, place));
            }

            Invoice invoice = new Invoice(invoiceId, 0, orderDate, invoiceNr, invoiceDate, new Customer(customerId, null, null, null, null, null), referenceNumber,
                freighterName, freightersInvNumber, freightersInvArrived, freighterPaidOn, customerPaidOn, shipDate, deliveryDate, product, pickupAddresses,
                deliveryAddresses, palletChange, priceFreighter, priceCustomer, amount, pallets, note);

            SqlDataAccess.UpdateInvoice(invoice);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            SqlDataAccess.DeleteInvoice(id);
        }
    }
}