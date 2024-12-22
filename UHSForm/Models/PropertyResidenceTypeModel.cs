using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class PropertyResidenceTypeModel
    {
        public string Name { get; set; }
        public Nullable<int> OrderBy { get; set; }
        public Nullable<int> uID { get; set; }
        public Nullable<int> suID { get; set; }
        public Nullable<int> rID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }

    }

    public class GetPropertyResidenceTypeModel
    {
        public int index { get; set; }
        public string Name { get; set; }
        public Nullable<int> OrderBy { get; set; }
        public Nullable<int> proprestID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }

    }

    public class UpdatePropertyResidenceTypeModel
    {
        public string Name { get; set; }
        public Nullable<int> OrderBy { get; set; }
        public Nullable<int> proprestID { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }

    }

    public class DeletePropertyResidenceTypeModel
    {
        public Nullable<int> proprestID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }

    }
}