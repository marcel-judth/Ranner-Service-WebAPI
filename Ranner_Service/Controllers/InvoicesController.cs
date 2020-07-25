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
            List<Invoice> result = new List<Invoice>();
            Customer cust1 = new Customer(1, "Customer1", "Hauptstrasse 1", 9500, "Villach", "AT59 871979");
            Customer cust2 = new Customer(2, "Kunde", "Nebenstrasse 1", 9220, "Velden", "AT59 871979");

            Address add1 = new Address(1, "Ranner-Service", "Villacherstrasse 73a", "9300", "St.Veit", "AT");
            Address add2 = new Address(2, "Bautstelle", "Wiener-Strasse", "0100", "Wien", "AT");
            // string orderNr, DateTime? orderDate, string invoiceNr, DateTime? invoiceDate, Customer customer, string referenceNumber, string freighterName, string freightersInvNumber, DateTime? freightersInvArrived, DateTime? freighterPaydOn,
            //    DateTime? customerPaidOn, DateTime shipDate, DateTime unshipDate, string product, Address pickupAddress, Address deliveryAddress, double amountFreighter, double amountCustomer
            Invoice invoice1 = new Invoice("2020303", DateTime.Now, null, null, cust1, "12345", "Slemnik", null, null, null, null, DateTime.Now, DateTime.Now, "Ware", add1, add2, 200, 240);
            Invoice invoice2 = new Invoice("2020303", DateTime.Now, null, null, cust1, "12345", "Temmel", null, null, null, null, DateTime.Now, DateTime.Now, "Ware", add1, add2, 200, 240);
            Invoice invoice3 = new Invoice("2020303", DateTime.Now, null, null, cust1, "12345", "Temmel", null, null, null, null, DateTime.Now, DateTime.Now, "Ware", add1, add2, 200, 240);
            Invoice invoice4 = new Invoice("2020303", DateTime.Now, "123", DateTime.Now, cust1, "12345", "Temmel", "1245", DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, "Ware", add1, add2, 200, 240);
            Invoice invoice5 = new Invoice("2020303", DateTime.Now, null, null, cust1, "12345", "Temmel", null, null, null, null, DateTime.Now, DateTime.Now, "Ware", add1, add2, 200, 240);
            Invoice invoice6 = new Invoice("2020303", DateTime.Now, null, null, cust1, "12345", "Temmel", null, null, null, null, DateTime.Now, DateTime.Now, "Ware", add1, add2, 200, 240);
            Invoice invoice7 = new Invoice("2020303", DateTime.Now, null, null, cust1, "12345", "Temmel", null, null, null, null, DateTime.Now, DateTime.Now, "Ware", add1, add2, 200, 240);
            Invoice invoice8 = new Invoice("2020303", DateTime.Now, null, null, cust1, "12345", "Temmel", null, null, null, null, DateTime.Now, DateTime.Now, "Ware", add1, add2, 200, 240);
            Invoice invoice9 = new Invoice("2020303", DateTime.Now, null, null, cust1, "12345", "Temmel", null, null, null, null, DateTime.Now, DateTime.Now, "Ware", add1, add2, 200, 240);
            Invoice invoice10 = new Invoice("2020303", DateTime.Now, null, null, cust1, "12345", "Temmel", null, null, null, null, DateTime.Now, DateTime.Now, "Ware", add1, add2, 200, 240);
            Invoice invoice11 = new Invoice("2020303", DateTime.Now, null, null, cust1, "12345", "Temmel", null, null, null, null, DateTime.Now, DateTime.Now, "Ware", add1, add2, 200, 240);

            result.Add(invoice1);
            result.Add(invoice2);
            result.Add(invoice3);
            result.Add(invoice4);
            result.Add(invoice5);
            result.Add(invoice6);
            result.Add(invoice7);
            result.Add(invoice8);
            result.Add(invoice9);
            result.Add(invoice10);
            result.Add(invoice11);

            return result;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}