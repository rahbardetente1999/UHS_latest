using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class WebHookRequest
    {
        public string Uid { get; set; }

        public string KeyId { get; set; }

        public string PaymentId { get; set; }

        public string StatusId { get; set; }

        public string TransactionId { get; set; }

        public string Custom1 { get; set; }

        public string VisaId { get; set; }
        public string Amount { get; set; }
    }
}