using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class SupportDetailsModel
    {
        public Nullable<int> uID { get; set; }
        public Nullable<int> suID { get; set; }
        public Nullable<int> rID { get; set; }
        public int Serverity { get; set; }
        public string Status { get; set; }
        public string subject { get; set; }
        public string Description { get; set; }
        public string[] EmailAddress { get; set; }
        public string Emails { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> CreatedRole { get; set; }
        public Nullable<int> UpdatedRole { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string TicketNo { get; set; }
    }

    public class GetSupports
    {
        public int index { get; set; }
        public int ID { get; set; }
        public Nullable<int> uID { get; set; }
        public Nullable<int> suID { get; set; }
        public Nullable<int> rID { get; set; }
        public string Usersname { get; set; }
        public string AgentName { get; set; }
        public string RoleName { get; set; }
        public string ServerityName { get; set; }
        public Nullable<int> Serverity { get; set; }
        public string Status { get; set; }
        public string subject { get; set; }
        public string Description { get; set; }
        public string Emails { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string TicketNo { get; set; }
        public IEnumerable<GetFileDetails> Files { get; set; }
    }
}