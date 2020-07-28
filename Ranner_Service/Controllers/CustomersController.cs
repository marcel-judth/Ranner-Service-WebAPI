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
    public class CustomersController : ApiController
    {
        // GET api/<controller>
        public List<Customer> Get()
        {
            return SqlDataAccess.GetCustomers();
        }

        // POST api/<controller>
        public void Post([FromBody] Customer value)
        {
            SqlDataAccess.InsertCustomer(value);
        }

        // PUT api/<controller>/5
        public void Put([FromBody] Customer value)
        {
            SqlDataAccess.UpdateCustomer(value);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            SqlDataAccess.DeleteCustomerById(id);
        }
    }
}