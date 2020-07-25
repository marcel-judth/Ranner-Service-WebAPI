using Ranner_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ranner_Service.Controllers
{
    public class CustomersController : ApiController
    {
        // GET api/<controller>
        public List<Customer> Get()
        {
            return new List<Customer>(new Customer[] { new Customer(1, "Customer1", "Hauptstrasse 1", 9500, "Villach", "AT59 871979"), new Customer(2, "Kunde", "Nebenstrasse 1", 9220, "Velden", "AT59 871979") });
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