using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class PropertyModel
    {
        public string Name { get; set; }
        public Nullable<int> OrderBy { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<int> subAreaID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> uID { get; set; }
        public Nullable<int> suID { get; set; }
        public Nullable<int> rID { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Code { get; set; }
    }

    public class GetPropertyModel
    {
        public int index { get; set; }
        public string Name { get; set; }
        public Nullable<int> OrderBy { get; set; }
        public Nullable<int> subAreaID { get; set; }
        public string PropertyArea { get; set; }
        public string SubAreaName { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Code { get; set; }
    }

    public class UpdatePropertyModel
    {
        public string Name { get; set; }
        public Nullable<int> OrderBy { get; set; }
        public string Code { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<int> subAreaID { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class DeletePropertyModel
    {

        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}