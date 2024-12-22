using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class ListOfDays
    {

        public List<ListOfDisplayDays> Days()
        {
            List<ListOfDisplayDays> result = new List<ListOfDisplayDays>();
            result.Add(new ListOfDisplayDays { ID = 1, Day = "Monday" });
            result.Add(new ListOfDisplayDays { ID = 2, Day = "Tuesday" });
            result.Add(new ListOfDisplayDays { ID = 3, Day = "Wednesday" });
            result.Add(new ListOfDisplayDays { ID = 4, Day = "Thursday" });
            result.Add(new ListOfDisplayDays { ID = 5, Day = "Friday" });
            result.Add(new ListOfDisplayDays { ID = 6, Day = "Saturday" });
            result.Add(new ListOfDisplayDays { ID = 0, Day = "Sunday" });

            return result;
        }
    }

    public class ListOfDisplayDays
    {
        public int ID { get; set; }
        public string Day { get; set; }
    }
}