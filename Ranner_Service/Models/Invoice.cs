using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;


namespace Ranner_Service.Models
{
    [DataContract]
    public class Invoice
    {
        [DataMember]
        public int invoiceId { get; set; }
        [DataMember]
        public int orderNr { get; set; }
        [DataMember]
        public DateTime? orderDate { get; set; }
        [DataMember]
        public int invoiceNr { get; set; }
        [DataMember]
        public DateTime? invoiceDate { get; set; }
        [DataMember]
        public Customer customer { get; set; }
        [DataMember]
        public string referenceNumber { get; set; }
        [DataMember]
        public string refNrCustomer { get; set; }
        [DataMember]
        public string freighterName { get; set; }
        [DataMember]
        public string freightersInvNumber { get; set; }
        [DataMember]
        public DateTime? freightersInvArrived { get; set; }
        [DataMember]
        public DateTime? freighterPaidOn { get; set; }
        [DataMember]
        public DateTime? customerPaidOn { get; set; }
        [DataMember]
        public DateTime? shipDate { get; set; }
        [DataMember]
        public DateTime? deliveryDate { get; set; }
        [DataMember]
        public string product { get; set; }
        [DataMember]
        public List<Address> pickupAddresses { get; set; }
        [DataMember]
        public List<Address> deliveryAddresses { get; set; }
        [DataMember]
        public bool palletChange { get; set; }
        [DataMember]
        public double priceFreighter { get; set; }
        [DataMember]
        public double priceCustomer { get; set; }
        [DataMember]
        public int? amount { get; set; }
        [DataMember]
        public double profit { get; set; }
        [DataMember]
        public List<Pallet> pallets { get; set; }

        [DataMember]
        public string note { get; set; }


        public Invoice(int orderNr, DateTime? orderDate, int invoiceNr, DateTime? invoiceDate,
            Customer customer, string referenceNumber, string refNrCustomer, string freighterName, string freightersInvNumber,
            DateTime? freightersInvArrived, DateTime? freighterPaidOn, DateTime? customerPaidOn, DateTime? shipDate, DateTime? deliveryDate,
            string product, List<Address> pickupAddresses, List<Address> deliveryAddresses, bool palletChange, double priceFreighter, double priceCustomer, int? amount, string note)
        {
            this.orderNr = orderNr;
            this.orderDate = orderDate;
            this.invoiceNr = invoiceNr;
            this.invoiceDate = invoiceDate;
            this.customer = customer;
            this.referenceNumber = referenceNumber;
            this.refNrCustomer = refNrCustomer;
            this.freighterName = freighterName;
            this.freightersInvNumber = freightersInvNumber;
            this.freightersInvArrived = freightersInvArrived;
            this.freighterPaidOn = freighterPaidOn;
            this.customerPaidOn = customerPaidOn;
            this.shipDate = shipDate;
            this.deliveryDate = deliveryDate;
            this.product = product;
            this.pickupAddresses = pickupAddresses;
            this.deliveryAddresses = deliveryAddresses;
            this.priceFreighter = priceFreighter;
            this.priceCustomer = priceCustomer;
            this.profit = priceCustomer - priceFreighter;
            this.palletChange = palletChange;
            this.amount = amount;
            this.pallets = new List<Pallet>();
            this.note = note;
        }
        public Invoice(int invoiceId, int orderNr, DateTime? orderDate, int invoiceNr, DateTime? invoiceDate,
            Customer customer, string referenceNumber, string refNrCustomer, string freighterName, string freightersInvNumber,
            DateTime? freightersInvArrived, DateTime? freighterPaidOn, DateTime? customerPaidOn, DateTime? shipDate, DateTime? deliveryDate,
            string product, List<Address> pickupAddress, List<Address> deliveryAddresses, bool palletChange, double priceFreighter, double priceCustomer, int? amount, List<Pallet> pallets, string note)
        {
            this.invoiceId = invoiceId;
            this.orderNr = orderNr;
            this.orderDate = orderDate;
            this.invoiceNr = invoiceNr;
            this.invoiceDate = invoiceDate;
            this.customer = customer;
            this.referenceNumber = referenceNumber;
            this.refNrCustomer = refNrCustomer;
            this.freighterName = freighterName;
            this.freightersInvNumber = freightersInvNumber;
            this.freightersInvArrived = freightersInvArrived;
            this.freighterPaidOn = freighterPaidOn;
            this.customerPaidOn = customerPaidOn;
            this.shipDate = shipDate;
            this.deliveryDate = deliveryDate;
            this.product = product;
            this.pickupAddresses = pickupAddress;
            this.deliveryAddresses = deliveryAddresses;
            this.priceFreighter = priceFreighter;
            this.priceCustomer = priceCustomer;
            this.profit = priceCustomer - priceFreighter;
            this.palletChange = palletChange;
            this.pallets = pallets;
            this.amount = amount;
            this.note = note;
        }

        public Invoice(int orderNr, DateTime? orderDate, int invoiceNr, DateTime? invoiceDate, Customer customer, 
            string referenceNumber, string refNrCustomer, string freighterName, string freightersInvNumber, DateTime? freightersInvArrived, DateTime? freighterPaydOn, 
            DateTime? customerPaidOn, DateTime? shipDate, DateTime? deliveryDate, string product, List<Address> pickupAddresses, bool palletChange, double priceFreighter, double priceCustomer, int? amount, string note)
        {
            this.orderNr = orderNr;
            this.orderDate = orderDate;
            this.invoiceNr = invoiceNr;
            this.invoiceDate = invoiceDate;
            this.customer = customer;
            this.referenceNumber = referenceNumber;
            this.refNrCustomer = refNrCustomer;
            this.freighterName = freighterName;
            this.freightersInvNumber = freightersInvNumber;
            this.freightersInvArrived = freightersInvArrived;
            this.freighterPaidOn = freighterPaydOn;
            this.customerPaidOn = customerPaidOn;
            this.shipDate = shipDate;
            this.deliveryDate = deliveryDate;
            this.product = product;
            this.pickupAddresses = pickupAddresses;
            this.priceFreighter = priceFreighter;
            this.priceCustomer = priceCustomer;
            this.profit = priceCustomer - priceFreighter;
            this.palletChange = palletChange;
            this.pallets = new List<Pallet>();
            this.amount = amount;
            this.note = note;
        }

        public void addPallet(Pallet newPallet)
        {
            this.pallets.Add(newPallet);
        }
    }
}