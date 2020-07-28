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
        public string product { get; set; }
        [DataMember]
        public Address pickupAddress { get; set; }
        [DataMember]
        public List<Address> deliveryAddresses { get; set; }
        [DataMember]
        public double amountFreighter { get; set; }
        [DataMember]
        public double amountCustomer { get; set; }
        [DataMember]
        public double profit { get; set; }

        public Invoice(int orderNr, DateTime? orderDate, int invoiceNr, DateTime? invoiceDate,
            Customer customer, string referenceNumber, string freighterName, string freightersInvNumber,
            DateTime? freightersInvArrived, DateTime? freighterPaidOn, DateTime? customerPaidOn, DateTime? shipDate,
            string product, Address pickupAddress, List<Address> deliveryAddresses, double amountFreighter, double amountCustomer)
        {
            this.orderNr = orderNr;
            this.orderDate = orderDate;
            this.invoiceNr = invoiceNr;
            this.invoiceDate = invoiceDate;
            this.customer = customer;
            this.referenceNumber = referenceNumber;
            this.freighterName = freighterName;
            this.freightersInvNumber = freightersInvNumber;
            this.freightersInvArrived = freightersInvArrived;
            this.freighterPaidOn = freighterPaidOn;
            this.customerPaidOn = customerPaidOn;
            this.shipDate = shipDate;
            this.product = product;
            this.pickupAddress = pickupAddress;
            this.deliveryAddresses = deliveryAddresses;
            this.amountFreighter = amountFreighter;
            this.amountCustomer = amountCustomer;
            this.profit = amountCustomer - amountFreighter;
        }
        public Invoice(int invoiceId, int orderNr, DateTime? orderDate, int invoiceNr, DateTime? invoiceDate,
            Customer customer, string referenceNumber, string freighterName, string freightersInvNumber,
            DateTime? freightersInvArrived, DateTime? freighterPaidOn, DateTime? customerPaidOn, DateTime? shipDate,
            string product, Address pickupAddress, List<Address> deliveryAddresses, double amountFreighter, double amountCustomer)
        {
            this.invoiceId = invoiceId;
            this.orderNr = orderNr;
            this.orderDate = orderDate;
            this.invoiceNr = invoiceNr;
            this.invoiceDate = invoiceDate;
            this.customer = customer;
            this.referenceNumber = referenceNumber;
            this.freighterName = freighterName;
            this.freightersInvNumber = freightersInvNumber;
            this.freightersInvArrived = freightersInvArrived;
            this.freighterPaidOn = freighterPaidOn;
            this.customerPaidOn = customerPaidOn;
            this.shipDate = shipDate;
            this.product = product;
            this.pickupAddress = pickupAddress;
            this.deliveryAddresses = deliveryAddresses;
            this.amountFreighter = amountFreighter;
            this.amountCustomer = amountCustomer;
            this.profit = amountCustomer - amountFreighter;
        }

        public Invoice(int orderNr, DateTime? orderDate, int invoiceNr, DateTime? invoiceDate, Customer customer, 
            string referenceNumber, string freighterName, string freightersInvNumber, DateTime? freightersInvArrived, DateTime? freighterPaydOn, 
            DateTime? customerPaidOn, DateTime? shipDate, string product, Address pickupAddress, double amountFreighter, double amountCustomer)
        {
            this.orderNr = orderNr;
            this.orderDate = orderDate;
            this.invoiceNr = invoiceNr;
            this.invoiceDate = invoiceDate;
            this.customer = customer;
            this.referenceNumber = referenceNumber;
            this.freighterName = freighterName;
            this.freightersInvNumber = freightersInvNumber;
            this.freightersInvArrived = freightersInvArrived;
            this.freighterPaidOn = freighterPaydOn;
            this.customerPaidOn = customerPaidOn;
            this.shipDate = shipDate;
            this.product = product;
            this.pickupAddress = pickupAddress;
            this.amountFreighter = amountFreighter;
            this.amountCustomer = amountCustomer;
            this.profit = amountCustomer - amountFreighter;
        }
    }
}