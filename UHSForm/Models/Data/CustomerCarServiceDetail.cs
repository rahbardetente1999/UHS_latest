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
    
    public partial class CustomerCarServiceDetail
    {
        public int custCarsDID { get; set; }
        public Nullable<int> custID { get; set; }
        public Nullable<int> custODID { get; set; }
        public string ParkingLevel { get; set; }
        public string ParkingNumber { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleColor { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string VehicleNumber { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual CustomerOfficalDetail CustomerOfficalDetail { get; set; }
    }
}
