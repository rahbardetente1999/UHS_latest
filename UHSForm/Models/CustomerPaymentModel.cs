using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class CustomerPaymentModel
    {
        public string PaymentID { get; set; }
        public string TransactionID { get; set; }
        public Nullable<int> PaymentStatus { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
    }
}