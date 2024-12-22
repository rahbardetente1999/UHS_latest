using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class PaymentRequest
    {
        // Payment properties
        public string Phone { get; set; }
        public string Street { get; set; }
        public string Email { get; set; }
        public string Amount { get; set; }

        // Billing address properties (if needed)
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Uid { get; set; }
        public string KeyId { get; set; }
        public string TransactionId { get; set; }

        public string Custom1 { get; set; }
    }
}