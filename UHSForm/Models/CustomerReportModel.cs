using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class CustomerReportModel
    {

        public Nullable<int> uID { get; set; }
        public string vID { get; set; }
        public string propaID { get; set; }
        public Nullable<int> CustomerType { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }

    }

    public class GetCustomerCount
    {
        public string ventureName { get; set; }
        public Nullable<int> NewCustomer { get; set; }
        public Nullable<int> ExistingCustomer { get; set; }
        public Nullable<int> SuspendCustomer { get; set; }
    }


    public class GetCustomerDataForTable
    {
        public string Month { get; set; }
        public GetCustomerTableCount TableData  { get; set; }

    }

    public class GetCustomerTableCount
    {
        public string Towers { get; set; }
        public Nullable<int> NewCustomer { get; set; }
        public Nullable<int> ExistingCustomer { get; set; }
        public Nullable<int> SuspendCustomer { get; set; }
    }

    public class GetCustomerDataForGraph
    {
        public string Month { get; set; }
        public GetCustomerCount GetCustomerCount { get; set; }
    }
}
