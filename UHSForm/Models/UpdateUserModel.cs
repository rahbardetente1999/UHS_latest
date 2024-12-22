using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class UpdateUserModel
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string AlternativeMobileNo { get; set; }
        public string WhatsAppNo { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string LandMark { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Nullable<int> Pincode { get; set; }
        public string Country { get; set; }
        public Nullable<int> PhoneCode { get; set; }
        public Nullable<int> rID { get; set; }
        public Nullable<int> UpdatedRole { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }

    }

    public class GetUpdateUserModel
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string AlternativeMobileNo { get; set; }
        public string WhatsAppNo { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string LandMark { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Nullable<int> Pincode { get; set; }
        public string Country { get; set; }
        public Nullable<int> PhoneCode { get; set; }
        public string CustomerID { get; set; }
        public Nullable<bool> IsEmail { get; set; }
        public Nullable<bool> IsMobile { get; set; }


    }

    public class GetCustomerPropertyModel
    {

        public Nullable<int> ID { get; set; }
        public Nullable<int> custODID { get; set; }
        public int custOPID { get; set; }
        public Nullable<int> propType { get; set; }
        public string PropertyArea { get; set; }
        public string PropertyName { get; set; }
        public string PropertyResidencyType { get; set; }
        public string AppartmentNumber { get; set; }
        public string BuildingName { get; set; }
        public string StreetNumber { get; set; }
        public string ZoneNumber { get; set; }
        public string Loacation { get; set; }
        public string LocationLink { get; set; }
        public string SubAreaName { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<int> subArea { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<int> proprestID { get; set; }
    }

    public class UpdateCustomerPropertyModel
    {

        public Nullable<int> ID { get; set; }
        public Nullable<int> custODID { get; set; }
        public int custOPID { get; set; }
        public Nullable<int> propType { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<int> proprestID { get; set; }
        public string TowerNumber { get; set; }
        public string AppartmentNumber { get; set; }
        public string BuildingName { get; set; }
        public string StreetNumber { get; set; }
        public string ZoneNumber { get; set; }
        public string Loacation { get; set; }
        public string LocationLink { get; set; }
        public Nullable<int> UpdatedRole { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class GetCustomerCarModel
    {

        public Nullable<int> ID { get; set; }
        public Nullable<int> custODID { get; set; }
        public Nullable<int> custCarsDID { get; set; }
        public string ParkingLevel { get; set; }
        public string ParkingNo { get; set; }
        public string VehicleNo { get; set; }
        public string VehilcleBrand { get; set; }
        public string VehilcleColor { get; set; }
        public string CarTypeName { get; set; }
        public string PropertyArea { get; set; }
        public string PropertyName { get; set; }
        public Nullable<int> carType { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<int> propaID { get; set; }
    }

    public class UpdateCustomerCarModel
    {

        public Nullable<int> ID { get; set; }
        public Nullable<int> custODID { get; set; }
        public Nullable<int> custCarsDID { get; set; }
        public string ParkingLevel { get; set; }
        public string ParkingNo { get; set; }
        public string VehicleNo { get; set; }
        public string VehilcleBrand { get; set; }
        public string VehilcleColor { get; set; }
         public Nullable<int> cartID { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<int> UpdatedRole { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class CreateCustomerCarModel
    {

        public Nullable<int> ID { get; set; }
        public Nullable<int> custODID { get; set; }
        public Nullable<int> custCarsDID { get; set; }
        public string ParkingLevel { get; set; }
        public string ParkingNo { get; set; }
        public string VehicleNo { get; set; }
        public string VehilcleBrand { get; set; }
        public string VehilcleColor { get; set; }
        public Nullable<int> cartID { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> CreatedRole { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}