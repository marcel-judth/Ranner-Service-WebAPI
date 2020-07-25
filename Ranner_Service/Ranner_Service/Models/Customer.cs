using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;


namespace Ranner_Service.Models
{
    [DataContract]
    public class Customer
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public int zipCode { get; set; }
        [DataMember]
        public string place { get; set; }
        [DataMember]
        public string uid { get; set; }

        public Customer(int id, string name, string address, int zipCode, string place, string uid)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.zipCode = zipCode;
            this.place = place;
            this.uid = uid;
        }

        public override string ToString()
        {
            return name + ", " + address + ", " + zipCode + " " + place;
        }
    }
}