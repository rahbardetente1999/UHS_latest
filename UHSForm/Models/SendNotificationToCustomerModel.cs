using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class SendNotificationToCustomerModel
    {
        public Nullable<int> custID { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public Nullable<bool> IsEmail { get; set; }

    }

    public class SendNotificationToCleanerModel
    {
        public Nullable<int> stfID { get; set; }
        public Nullable<int> teamID { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public Nullable<bool> IsEmail { get; set; }

    }
}