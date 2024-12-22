using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class StaffCustomerRatingModel
    {
        public Nullable<int> cuID { get; set; }
        public Nullable<int> custODID { get; set; }
        public Nullable<int> stfID { get; set; }
        public Nullable<int> custCTID { get; set; }
        public List<int?> custISID { get; set; }
        public Nullable<int> custTDID { get; set; }
        public Nullable<int> Rating { get; set; }
        public string Review { get; set; }
        public string OtherIssues { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> CreatedRole { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
    }

    public class GetStaffCustomerRatingModel
    {
        public string CustomerName { get; set; }
        public string CustomerIssue { get; set; }
        public string CustomerConditionalType { get; set; }
        public Nullable<int> custCTID { get; set; }
        public Nullable<int> custISID { get; set; }
        public Nullable<int> Rating { get; set; }
        public string Review { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
    }

    public class GetStaffCustomerRatingForAdminModel
    {
        public string CustomerName { get; set; }
        public string StaffName { get; set; }
        public string ServiceDate { get; set; }
        public string TaskNo { get; set; }
        public string Review { get; set; }
        public string CreatedBy { get; set; }
        public string MainCategory { get; set; }
        public string SubCategory { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public Nullable<int> custID { get; set; }
        public Nullable<int> custODID { get; set; }
        public Nullable<int> custTDID { get; set; }
        public Nullable<int> stfID { get; set; }
        public List<GetFileDetails> Files { get; set; }

    }

    public class GetStaffCustomerRatingForAdminDetailsModel
    {
        public string CustomerIssue { get; set; }
        public string CustomerConditionalType { get; set; }
        public string OtherCustomerIssue { get; set; }
        public Nullable<int> custCTID { get; set; }
        public Nullable<int> custISID { get; set; }
    }



    public class GetStaffAverageRating
    {
        public Nullable<double> RegularCleaning { get; set; }
        public Nullable<double> DeepCleaning { get; set; }
        public Nullable<double> SpecializedClaeaning { get; set; }
    }
}