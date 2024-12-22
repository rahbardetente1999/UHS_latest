using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class IncExcluModel
    {
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> servcatID { get; set; }
        public Nullable<int> servsubcatID { get; set; }
        public List<IncExcluType> incExcluTypes { get; set; }
        public Nullable<int> rID { get; set; }
        public Nullable<int> uID { get; set; }
        public Nullable<int> suID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
    }

    public class GetIncExcluModel
    {
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> servcatID { get; set; }
        public Nullable<int> servsubcatID { get; set; }
        public Nullable<int> incexID { get; set; }
        public Nullable<int> CountInc { get; set; }
        public Nullable<int> CountExc { get; set; }
        public string MainCategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string ServiceCategoryName { get; set; }
        public string SubServiceCategoryName { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
    }


    public class IncExcluType
    {
        public Nullable<int> Type { get; set; }
        public string Name { get; set; }
    }

    public class CreateIncExcluTypeModel
    {
        public Nullable<int> Type { get; set; }
        public Nullable<int> incexID { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
    }

    public class UpdateIncExcluTypeModel
    {
        public Nullable<int> Type { get; set; }
        public Nullable<int> incexID { get; set; }
        public Nullable<int> incexRefID { get; set; }
        public string Name { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
    }

    public class DeleteIncExcluTypeModel
    {
        public Nullable<int> incexID { get; set; }
        public Nullable<int> incexRefID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
    }

    public class GetIncExcluTypeModel
    {
        public int index { get; set; }
        public Nullable<int> Type { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public Nullable<int> incexRefID { get; set; }
        public Nullable<int> incexID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
    }
}