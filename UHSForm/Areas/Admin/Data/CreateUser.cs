using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Areas.Admin.Data
{
    public class CreateUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public Nullable<int> Role { get; set; }
        public Nullable<int> teamID { get; set; }
    }

    public class UpdateUserPersonalDetails
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public Nullable<int> rID { get; set; }
        public Nullable<int> suID { get; set; }
        public Nullable<int> stfID { get; set; }
    }

    public class UpdateUserRole
    {
        public Nullable<int> rID { get; set; }
        public Nullable<int> suID { get; set; }
        public Nullable<int> stfID { get; set; }
    }

    public class DeleteUser
    {
        public Nullable<int> rID { get; set; }
        public Nullable<int> suID { get; set; }
        public Nullable<int> stfID { get; set; }
    }
}