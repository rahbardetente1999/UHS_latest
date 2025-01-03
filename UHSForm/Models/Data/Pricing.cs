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
    
    public partial class Pricing
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pricing()
        {
            this.CustomerDateBlocks = new HashSet<CustomerDateBlock>();
            this.CustomerPricings = new HashSet<CustomerPricing>();
            this.CustomerTimelines = new HashSet<CustomerTimeline>();
        }
    
        public int parkID { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> servcatID { get; set; }
        public Nullable<int> servsubcatID { get; set; }
        public Nullable<int> packID { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<int> proprestID { get; set; }
        public Nullable<double> Price { get; set; }
        public string Duration { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> uID { get; set; }
        public Nullable<int> suID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string TimeMeasurement { get; set; }
        public Nullable<int> cartID { get; set; }
        public Nullable<int> carstID { get; set; }
    
        public virtual Admin_Sub Admin_Sub { get; set; }
        public virtual CarServiceType CarServiceType { get; set; }
        public virtual CarType CarType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerDateBlock> CustomerDateBlocks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerPricing> CustomerPricings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerTimeline> CustomerTimelines { get; set; }
        public virtual MainCategory MainCategory { get; set; }
        public virtual Package Package { get; set; }
        public virtual PropertyArea PropertyArea { get; set; }
        public virtual PropertyResidenceType PropertyResidenceType { get; set; }
        public virtual ServiceCategory ServiceCategory { get; set; }
        public virtual ServiceSubCategory ServiceSubCategory { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual User User { get; set; }
        public virtual Venture Venture { get; set; }
    }
}
