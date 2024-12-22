using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class CustomerComplaintModel
    {
        public string Remarks { get; set; }
        public Nullable<int> cuID { get; set; }
        public Nullable<int> custODID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> TaskNo { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> CreatedRole { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
    }

    public class GetCustomerComplaintModel
    {
        public Nullable<int> custComID { get; set; }
        public string Remarks { get; set; }
        public IEnumerable<GetFileDetails> Files { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
    }
}