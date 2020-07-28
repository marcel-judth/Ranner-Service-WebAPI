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
    public class AddressesController : ApiController
    {
        // GET api/<controller>
        public List<Address> Get()
        {
            return SqlDataAccess.GetAddresses();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] Address value)
        {
            SqlDataAccess.InsertAddress(value);
        }

        // PUT api/<controller>/5
        public void Put([FromBody] Address value)
        {
            SqlDataAccess.UpdateAddress(value);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            SqlDataAccess.DeleteAddressById(id);
        }
    }
}