using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class CustomerServiceRatingModel
    {
        public Nullable<int> cuID { get; set; }
        public Nullable<int> custODID { get; set; }
        public Nullable<int> custTDID { get; set; }
        public Nullable<int> Rating { get; set; }
        public string Feedback { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> CreatedRole { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }

    }

    public class GetCustomerServiceRatingModel
    {
        public string CustomerName { get; set; }
        public string StaffName { get; set; }
        public string TeamName { get; set; }
        public Nullable<int> Rating { get; set; }
        public string Feedback { get; set; }
        public Nullable<int> cuID { get; set; }
        public Nullable<int> custTDID { get; set; }
        public Nullable<int> custODID { get; set; }
        public Nullable<int> custfdbID { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }

    }
}