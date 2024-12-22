using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class CustomerAlertsModel
    {
        public string Message { get; set; }
        public string CustomerName { get; set; }
        public string AlertType { get; set; }
        public string MainCategroy { get; set; }
        public string SubCategory { get; set; }
        public string PropertyName { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> custAID { get; set; }
        public Nullable<int> custATID { get; set; }
        public Nullable<int> custID { get; set; }
    }
}