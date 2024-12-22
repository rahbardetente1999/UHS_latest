using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public Nullable<int> Role { get; set; }
        public Nullable<int> uID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> CreatedRole { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
    }

    public class GetUserModel
    {
        public int index { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Role { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public Nullable<int> rID { get; set; }
        public Nullable<int> suID { get; set; }
    }

    public class GetCommonUserModel
    {
        public int index { get; set; }
        public string Name { get; set; }
        public string TeamName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Role { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public Nullable<int> rID { get; set; }
        public Nullable<int> suID { get; set; }
        public Nullable<int> stfID { get; set; }
        public Nullable<int> teamID { get; set; }
        public Nullable<int> uID { get; set; }
    }

    public class UpdatePersonalUserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<int> UpdatedRole { get; set; }
        public Nullable<int> uID { get; set; }
        public Nullable<int> suID { get; set; }
    }

    public class UpdateRoleUserModel
    {
        public Nullable<int> uID { get; set; }
        public Nullable<int> suID { get; set; }
        public Nullable<int> rID { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<int> UpdatedRole { get; set; }
 
    }

    public class DeleteUserModel
    {
        public Nullable<int> uID { get; set; }
        public Nullable<int> suID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<int> UpdatedRole { get; set; }

    }
}