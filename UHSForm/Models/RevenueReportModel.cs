using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class RevenueReportModel
    {
        public string propaID { get; set; }
        public string vID { get; set; }
        public Nullable<int> uID { get; set; }
        public string StartDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }

    public class GetRevenueReportModel 
    {
        public List<GetRevenueByTowerReportModel> Towers { get; set; }
        public Nullable<double> TotalRevenue { get; set; }
        public Nullable<double> RevenuePerArea { get; set; }
        public Nullable<double> RevenuePerTower { get; set; }
    }

    public class GetRevenueMonthlyReportModel
    {
        public string Month { get; set; }
        public Nullable<double> Amount { get; set; }
    }

    public class GetRevenueByTowerReportModel
    {
        public string TowerName { get; set; }

        public string  Area { get; set; }
        public string SubArea { get; set; }
        public string PropertyCode { get; set; }
        public Nullable<int> NoOfCustomers { get; set; }
        public Nullable<double> Amount { get; set; }
    }

   
}