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
    
    public partial class CustomerTransaction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerTransaction()
        {
            this.CustomerInovices = new HashSet<CustomerInovice>();
        }
    
        public int custTrasID { get; set; }
        public Nullable<int> cuID { get; set; }
        public Nullable<int> custODID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> TotalPrice { get; set; }
        public string PayementID { get; set; }
        public string TransactionID { get; set; }
        public Nullable<int> PaymentStatus { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    
        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerInovice> CustomerInovices { get; set; }
        public virtual CustomerOfficalDetail CustomerOfficalDetail { get; set; }
        public virtual CustomerPaymentStatu CustomerPaymentStatu { get; set; }
    }
}