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
    
    public partial class CustomerFeedback
    {
        public int custfdbID { get; set; }
        public string Feedback { get; set; }
        public Nullable<int> cuID { get; set; }
        public Nullable<int> custODID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> CreatedRole { get; set; }
        public Nullable<int> UpdatedRole { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdaatedOn { get; set; }
        public Nullable<int> Rating { get; set; }
        public Nullable<int> custTDID { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual CustomerOfficalDetail CustomerOfficalDetail { get; set; }
        public virtual CustomerTimeline CustomerTimeline { get; set; }
    }
}
