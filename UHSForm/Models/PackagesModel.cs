using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class PackagesModel
    {
        public string Name { get; set; }
        public Nullable<int> RecursiveTime { get; set; }
        public Nullable<int> uID { get; set; }
        public Nullable<int> suID { get; set; }
        public Nullable<int> rID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
    }

    public class GetPackagesModel
    {
        public int index { get; set; }
        public Nullable<int> packID { get; set; }
        public Nullable<int> RecursiveTime { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
    }

    public class UpdatePackagesModel
    {
        public int packID { get; set; }
        public Nullable<int> RecursiveTime { get; set; }
        public string Name { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
    }

    public class DeletePackagesModel
    {
        public int packID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
    }
}