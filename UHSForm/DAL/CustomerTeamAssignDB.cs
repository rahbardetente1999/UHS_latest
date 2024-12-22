using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;
using System.Data.Entity.Core.Objects;
using System.Globalization;

namespace UHSForm.DAL
{
    public class CustomerTeamAssignDB
    {
        private UHSEntities UhDB;

        public CustomerTeamAssignDB()
        {
            UhDB = new UHSEntities();
        }

        public List<GetDropDown> GetCustomerDeepAndSpecializeTeamAssign(CustomerDeepAndSpecializeTeamAssignModel customer) 
        {
            List<GetDropDown> result = new List<GetDropDown>();
            var objTeams = UhDB.StaffServices.Where(x => x.catID == customer.catID && x.catsubID == customer.catsubID && x.Team.uID == 1 && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objTeam in objTeams)
            {
                int? teamID = objTeam.teamID;
                var objCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false
                                           && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(customer.StartDate)).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                if (objCustomerTimeLines!=null) 
                {
                    if (objCustomerTimeLines.Count() == 0)
                    {
                        result.Add(new GetDropDown { ID = teamID, Value = objTeam.Team.Name });
                    }
                    else 
                    {
                        List<TimeRange> intervalRanges = new List<TimeRange>();
                        foreach (var objCustomerTimeLine in objCustomerTimeLines)
                        {
                            intervalRanges.Add(new TimeRange { Start = ConvertToTimeSpans(objCustomerTimeLine.StartTime), End = ConvertToTimeSpans(objCustomerTimeLine.EndTime) });
                        }
                        TimeRange OrginalTimeRange = new TimeRange();
                        OrginalTimeRange.Start = new TimeSpan(8, 0, 0);
                        OrginalTimeRange.End = new TimeSpan(18, 0, 0);

                        TimeRange GivenTimeRange = new TimeRange();
                        GivenTimeRange.Start = ConvertToTimeSpans(customer.StartTime);
                        GivenTimeRange.End = ConvertToTimeSpans(customer.EndTime);
                        int Duration = 0;
                        if (customer.catsubID == 1)
                        {
                            Duration = 15;
                        }
                        else
                        {
                            Duration = 30;
                        }
                        bool available = IsAvailable(OrginalTimeRange, GivenTimeRange, intervalRanges, Duration);
                        if (available == true)
                        {
                            result.Add(new GetDropDown { ID = teamID, Value = objTeam.Team.Name });
                        }
                    }
                }
                else 
                {
                    List<TimeRange> intervalRanges = new List<TimeRange>();
                    foreach (var objCustomerTimeLine in objCustomerTimeLines)
                    {
                        intervalRanges.Add(new TimeRange { Start = ConvertToTimeSpans(objCustomerTimeLine.StartTime), End = ConvertToTimeSpans(objCustomerTimeLine.EndTime) });
                    }
                    TimeRange OrginalTimeRange = new TimeRange();
                    OrginalTimeRange.Start = new TimeSpan(8, 0, 0);
                    OrginalTimeRange.End = new TimeSpan(18, 0, 0);

                    TimeRange GivenTimeRange = new TimeRange();
                    GivenTimeRange.Start = ConvertToTimeSpans(customer.StartTime);
                    GivenTimeRange.End = ConvertToTimeSpans(customer.EndTime);
                    int Duration = 0;
                    if (customer.catsubID==1) 
                    {
                        Duration = 15;
                    }
                    else
                    {
                        Duration = 30;
                    }
                    bool available = IsAvailable(OrginalTimeRange,GivenTimeRange,intervalRanges,Duration);
                    if (available==true) 
                    {
                        result.Add(new GetDropDown { ID = teamID, Value = objTeam.Team.Name });
                    }

                }

            }
            return result;
        }

        public bool IsAvailable(TimeRange originalRange, TimeRange givenRange, List<TimeRange> intervalRanges,int Duration)
        {
            // Define the 30 minutes before start and 30 minutes after end
            TimeSpan startBuffer = givenRange.Start - TimeSpan.FromMinutes(Duration);
            TimeSpan endBuffer = givenRange.End + TimeSpan.FromMinutes(Duration);

            // Check if the buffered range is within the original range
            if (startBuffer < originalRange.Start || endBuffer > originalRange.End)
            {
                return false; // Not within the original time range
            }

            // Create the buffered time range
            TimeRange bufferedRange = new TimeRange { Start = startBuffer, End = endBuffer };

            // Check if the buffered range overlaps with any intervals
            foreach (var interval in intervalRanges)
            {
                if (bufferedRange.OverlapsWith(interval))
                {
                    return false; // Overlaps with one of the intervals
                }
            }

            return true; // No overlaps, available
        }

        public TimeSpan ConvertToTimeSpans(string timeStrings)
        {
            TimeSpan timeSpans = new TimeSpan();
            string format = "h:mm tt";
            if (DateTime.TryParseExact(timeStrings, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
            {
                timeSpans = dateTime.TimeOfDay;
            }
            return timeSpans;
        }
    }
}