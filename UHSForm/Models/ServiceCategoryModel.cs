using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class ServiceCategoryModel
    {
        public string Name { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> uID { get; set; }
        public Nullable<int> suID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public Nullable<int> CreatedRole { get; set; }

    }

    public class GetServiceCategoryModel
    {
        public string Name { get; set; }
        public string MainCategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> servcatID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public List<GetFileDetails> Images { get; set; }

    }

    public class UpdateServiceCategoryModel
    {
        public string Name { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> servcatID { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<int> UpdatedRole { get; set; }

    }

    public class DeleteServiceCategoryModel
    {
        public Nullable<int> servcatID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<int> UpdatedRole { get; set; }

    }
}