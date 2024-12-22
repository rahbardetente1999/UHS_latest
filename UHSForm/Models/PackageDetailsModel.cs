using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class PackageDetailsModel
    {
        public string SubCategoryName { get; set; }
        public string PackageName { get; set; }
        public string InVoice { get; set; }
        public string AreaName { get; set; }
        public string PropName { get; set; }
        public string resdName { get; set; }
        public string NoOfMonths { get; set; }
        public string TotalPrice { get; set; }
        public string Price { get; set; }
    }

    public class PackageDetailsModelV2
    {
        public string SubCategoryName { get; set; }
        public string InVoice { get; set; }
        public string AreaName { get; set; }
        public string PropName { get; set; }
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public List<SubServicesInvoiceDetails> ServiceSubCategory { get; set; }
    }

    public class SubServicesInvoiceDetails
    {
        public string ServiceOption { get; set; }
        public string ServiceSubCategory { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string TotalPrice { get; set; }
    }
}