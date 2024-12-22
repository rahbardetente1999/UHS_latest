using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class StaffServiceModel
    {
        public Nullable<int> catID { get; set; }
        public List<int> catsubID { get; set; }
        public Nullable<bool> SpecialService { get; set; }
        public Nullable<int> servcatID { get; set; }
        public Nullable<int> servsubcatID { get; set; }
        public Nullable<int> stfID { get; set; }
        public Nullable<int> teamID { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<bool> IsTeam { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }

    }

    public class GetStaffServiceModel
    {
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> servcatID { get; set; }
        public Nullable<int> servsubcatID { get; set; }
        public Nullable<int> stfID { get; set; }
        public Nullable<int> stfsID { get; set; }
        public Nullable<int> teamID { get; set; }
        public Nullable<int> propaID { get; set; }
        public string StaffName { get; set; }
        public string TeamName { get; set; }
        public string MainCategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string ServiceCategoryName { get; set; }
        public string SubServiceCategoryName { get; set; }
        public string PropertyName { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }

    }
}