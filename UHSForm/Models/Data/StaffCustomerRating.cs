//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UHSForm.Models.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class StaffCustomerRating
    {
        public int custSTFRID { get; set; }
        public Nullable<int> cuID { get; set; }
        public Nullable<int> custODID { get; set; }
        public Nullable<int> custTDID { get; set; }
        public Nullable<int> stfID { get; set; }
        public Nullable<int> Rating { get; set; }
        public string Review { get; set; }
        public string OtherIssue { get; set; }
        public Nullable<int> custCTID { get; set; }
        public Nullable<int> custISID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> CreatedRole { get; set; }
        public Nullable<int> UpdatedRole { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual CustomerConditionalType CustomerConditionalType { get; set; }
        public virtual CustomerIssueType CustomerIssueType { get; set; }
        public virtual CustomerOfficalDetail CustomerOfficalDetail { get; set; }
        public virtual CustomerTimeline CustomerTimeline { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
