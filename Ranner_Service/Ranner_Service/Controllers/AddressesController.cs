﻿using Ranner_Service.Models;
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
            return new List<Address>( new Address[] { new Address(1, "Ranner-Service", "Villacherstrasse 73a", 9300, "St.Veit", "AT"), new Address(2, "Bautstelle", "Wiener-Strasse", 0100, "Wien", "AT") });
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