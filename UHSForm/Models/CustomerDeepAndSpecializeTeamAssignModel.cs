using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class CustomerDeepAndSpecializeTeamAssignModel
    {
        public Nullable<int> cuID { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}