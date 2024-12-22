using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class GrantChartReportModel
    {
        public string Team { get; set; }
        public List<AreaBased> AreaBased { get; set; }
    }

    public class AreaBased
    {
        public Times Time { get; set; }
        public string Area { get; set; }

        public string SubArea { get; set; }
        public string PropertyCode { get; set; }

        public string TowerName { get; set; }
        public string Service { get; set; }
    }

    public class Times
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

    }
}