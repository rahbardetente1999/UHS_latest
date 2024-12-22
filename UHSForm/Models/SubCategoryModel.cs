using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class SubCategoryModel
    {
        public string Name { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> uID { get; set; }
        public Nullable<int> suID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public Nullable<int> CreatedRole { get; set; }

    }

    public class GetSubCategoryModel
    {
        public string Name { get; set; }
        public string MainCategoryName { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public List<GetFileDetails> Images{ get; set; }

    }

    public class UpdateSubCategoryModel
    {
        public string Name { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<int> UpdatedRole { get; set; }

    }

    public class DeleteSubCategoryModel
    {
        public Nullable<int> catsubID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<int> UpdatedRole { get; set; }

    }
}