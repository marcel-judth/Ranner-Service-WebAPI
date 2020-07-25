using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;


namespace Ranner_Service.Models
{
    [DataContract]
    public class Address
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public string zipCode { get; set; }
        [DataMember]
        public string place { get; set; }
        [DataMember]
        public string country { get; set; }

        public Address(int id, string name, string address, string zipCode, string place, string country)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.zipCode = zipCode;
            this.place = place;
            this.country = country;
        }

        public override string ToString()
        {
            return name + ", " + address + ", " + zipCode + " " + place + " (" + country + ")";
        }
    }
}