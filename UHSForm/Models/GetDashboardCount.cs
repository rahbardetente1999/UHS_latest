using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class GetDashboardCount
    {
        public GetDashboardCountDetails RegularCleaning { get; set; }
        public GetDashboardCountDetails DeepCleaning { get; set; }
        public GetDashboardCountDetails SofaCleaning { get; set; }
        public GetDashboardCountDetails MattressCleaning { get; set; }
        public GetDashboardCountDetails CarpetCleaning { get; set; }
        public GetDashboardCountDetails CurtainsCleaning { get; set; }
        public GetDashboardCountDetails CarWashCleaning { get; set; }
    }

    public class GetCustomerDashboardCount
    {
        public GetCustomerDashboardCountDetails RegularCleaning { get; set; }
        public GetCustomerDashboardCountDetails DeepCleaning { get; set; }
        public GetCustomerDashboardCountDetails SpecializeCleaning { get; set; }
        public GetCustomerDashboardCountDetails CarWash { get; set; }
       
    }

    public class GetDashboardCountDetails
    {
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> servcatID { get; set; }
        public string Name { get; set; }
        public Nullable<int> Count { get; set; }
    }

    public class GetCustomerDashboardCountDetails
    {
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> servcatID { get; set; }
        public string Name { get; set; }
        public Nullable<int> TotalCount { get; set; }
        public Nullable<int> CompletedCount { get; set; }
        public Nullable<int> PendingCount { get; set; }
    }
}