using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class ResultObj
    {
        public string id { get; set; }
        public string statusId { get; set; }
        public string created { get; set; }
        public string payUrl { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string transactionId { get; set; }
        public string custom1 { get; set; }
        public string visaId { get; set; }
        public string refundId { get; set; }
        public string refundStatusId { get; set; }
        public string status { get; set; }

    }
}