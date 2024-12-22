using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class CustomerSupportModel
    {
        public string TicketTitle { get; set; }
        public Nullable<int> custSTTID { get; set; }
        public Nullable<int> custSSTID { get; set; }
        public Nullable<int> custSAID { get; set; }
        public Nullable<int> custSSID { get; set; }
        public Nullable<int> custID { get; set; }
        public DateTime ServiceDate { get; set; }
        public string ActionFor { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }

    public class GetCustomerSupportModel
    {
        public int index { get; set; }
        public string Title { get; set; }
        public string ServiceDate { get; set; }
        public string Remarks { get; set; }
        public string ActionForOther { get; set; }
        public string CustomerSupportTaskStatus { get; set; }
        public string CustomerSupportServiceTicketType { get; set; }
        public string CustomerSupportServiceType { get; set; }
        public string CustomerSupportActionFor { get; set; }
        public string CustomerName { get; set; }

        public Nullable<int> custSID { get; set; }
        public Nullable<int> custSTTID { get; set; }
        public Nullable<int> custSSTID { get; set; }
        public Nullable<int> custSAID { get; set; }
        public Nullable<int> custSSID { get; set; }
        public Nullable<int> custID { get; set; }
        
        public Nullable<DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public int fileID { get; set; }
        public IEnumerable<GetFileDetails> Files { get; set; }
    }

    public class UpdateStatusModel
    {
        public Nullable<int> custSID { get; set; }
        public Nullable<int> custSSID { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }

    }

}