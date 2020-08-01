using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Ranner_Service.Models
{
    public class Pallet
    {
        [DataMember]
        public int palletId { get; set; }
        [DataMember]
        public int amount { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string place { get; set; }

        public Pallet(int palletId, int amount, string type, string place)
        {
            this.palletId = palletId;
            this.amount = amount;
            this.type = type;
            this.place = place;
        }
    }
}