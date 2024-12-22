using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class TeamsModel
    {
        public string Name { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> teamTyID { get; set; }
        public Nullable<int> uID { get; set; }
        public Nullable<int> suID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> CreatedRole { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }

    }


    public class GetTeamsModel
    {
        public Nullable<int> teamID { get; set; }
        public string Name { get; set; }
        public string TeamName { get; set; }
        public Nullable<int> teamTyID { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }

    public class UpdateTeamsModel
    {
        public Nullable<int> teamID { get; set; }
        public Nullable<int> teamTyID { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> UpdatedRole { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }

    public class DeleteTeamsModel
    {
        public Nullable<int> teamID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> UpdatedRole { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}