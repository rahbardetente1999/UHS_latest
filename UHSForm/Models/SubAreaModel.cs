using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class SubAreaModel
    {
        public Nullable<int> ScoreID { get; set; }
        public Nullable<int> propaID { get; set; }
        public string SubAreaName { get; set; }
        public Nullable<int> rID { get; set; }
        public Nullable<int> uID { get; set; }
        public Nullable<int> suID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }

    public class GetSubAreaModel
    {
        public Nullable<int> subAreaID { get; set; }
        public Nullable<int> ScoreID { get; set; }
        public Nullable<int> propaID { get; set; }
        public string SubAreaName { get; set; }

        public string AreaName { get; set; }
        public Nullable<DateTime> AddedOn { get; set; }
        public string AddedBy { get; set; }
    }

    public class UpdateSubAreaModel
    {
        public Nullable<int> subAreaID { get; set; }
        public Nullable<int> ScoreID { get; set; }
        public Nullable<int> propaID { get; set; }
        public string SubAreaName { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class DeleteSubAreaModel
    {
        public Nullable<int> subAreaID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}