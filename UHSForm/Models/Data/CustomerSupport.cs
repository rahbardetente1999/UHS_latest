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
    
    public partial class CustomerSupport
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerSupport()
        {
            this.Files = new HashSet<File>();
        }
    
        public int custSID { get; set; }
        public string Title { get; set; }
        public Nullable<int> custSTTID { get; set; }
        public Nullable<int> custSSTID { get; set; }
        public Nullable<int> custSAID { get; set; }
        public Nullable<int> custSSID { get; set; }
        public Nullable<int> custID { get; set; }
        public Nullable<System.DateTime> ServiceDate { get; set; }
        public string ActionForOther { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    
        public virtual ApprovalStatu ApprovalStatu { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual CustomerSupportActionFor CustomerSupportActionFor { get; set; }
        public virtual CustomerSupportServiceType CustomerSupportServiceType { get; set; }
        public virtual CustomerSupportTicketType CustomerSupportTicketType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<File> Files { get; set; }
    }
}