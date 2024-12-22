using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class PropertyAreaModel
    {
        public string Name { get; set; }
        public Nullable<int> OrderBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> uID { get; set; }
        public Nullable<int> suID { get; set; }
        public Nullable<int> rID { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }

    public class GetPropertyAreaModel
    {
        public int index { get; set; }
        public Nullable<int> OrderBy { get; set; }
        public Nullable<int> propaID { get; set; }
        public string Name { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }

    public class UpdatePropertyAreaModel
    {
        public string Name { get; set; }
        public Nullable<int> OrderBy { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class DeletePropertyAreaModel
    {
        public Nullable<int> propaID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}