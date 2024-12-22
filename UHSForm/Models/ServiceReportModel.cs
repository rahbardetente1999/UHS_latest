using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class ServiceReportModel
    {

    }

    public class ServiceCount 
    {
        public Nullable<int> uID { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
    }

    public class GetServiceData
    {
        public Nullable<int> Completed { get; set; }
        public Nullable<int> Cancelled { get; set; }
        public Nullable<int> Reschdule { get; set; }
        public string Month { get; set; }

    }
}