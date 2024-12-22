using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class GeneralModel
    {
    }

    public class CustomerVerification 
    {
        public Nullable<bool> IsEmail { get; set; }
        public Nullable<bool> IsMobile { get; set; }
        public Nullable<int> cuID { get; set; }
        public Nullable<int> UpdatedRole { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class Block
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<int> UpdatedRole { get; set; }
        public bool IsBlock { get; set; }
    }

    public class GetBlock
    {
        public Nullable<bool> Block { get; set; }
    }

    public class ChangePassword
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
    }

    public class GetDropDown
    {
        public Nullable<int> ID { get; set; }
        public string Value { get; set; }
    }

    public class GetPropertyDropDown
    {
        public Nullable<int> ID { get; set; }
        public string Value { get; set; }
        public string PropertyType { get; set; }
    }

    public class GetCustomerOtherPropertyDetails
    {
        public string TowerName { get; set; }
        public string BuildingName { get; set; }
        public string StreetNumber { get; set; }
        public string ZoneNumber { get; set; }
        public string Loacation { get; set; }
        public string LocationLink { get; set; }
        public string AppartmentName { get; set; }
    }


    public class GetDropDownWithImages
    {
        public Nullable<int> ID { get; set; }
        public string Value { get; set; }
        public Nullable<int> NextServices { get; set; }
        public IEnumerable<GetFileDetails> Images { get; set; }
    }

    public class GetPricingDropDown
    {
        public Nullable<int> ID { get; set; }
        public string Price { get; set; }
        public string Duration { get; set; }
    }

    public class GetUserDetails
    {
        public Nullable<int> userID { get; set; }
        public Nullable<int> uID { get; set; }
        public Nullable<int> stfID { get; set; }

        public Nullable<int> cuID { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string AlternativeMobileNo { get; set; }
        public string Email { get; set; }
        public Nullable<int> PhoneCode { get; set; }
        public string Country { get; set; }
        public Nullable<int> rID { get; set; }
        public Nullable<int> parentID { get; set; }
        public Nullable<int> logID { get; set; }
    }

    public class GetFileDetails
    {
     
        public string Name { get; set; }
        public string ContentType { get; set; }
        public string Size { get; set; }
        public string Value { get; set; }
    }
    public class GetDropDowns
    {
        public Nullable<int> ID { get; set; }
        public string Value { get; set; }
        public Nullable<int> ExtraValue { get; set; }
    }

    public class GetPropertyAreaDropDowns
    {
        public Nullable<int> ID { get; set; }
        public string Value { get; set; }
        public Nullable<int> ExtraValue { get; set; }
        public Nullable<int> subArea { get; set; }
    }
}