using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class GetCustomerV3
    {
        public string MainCategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string RecursiveTime { get; set; }
        public string NextDate { get; set; }
        public string PerviousDate { get; set; }
        public string AssignedCleaner { get; set; }
        public string NoOfMonths { get; set; }
        public Nullable<int> cuID { get; set; }
        public Nullable<int> cuODID { get; set; }
    }
}