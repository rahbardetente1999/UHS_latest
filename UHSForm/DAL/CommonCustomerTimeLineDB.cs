using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Linq;
using System.Web;
using UHSForm.Models;
using UHSForm.Models.Data;

namespace UHSForm.DAL
{
    public class CommonCustomerTimeLineDB
    {
        private UHSEntities UhDB;

        public CommonCustomerTimeLineDB()
        {
            UhDB = new UHSEntities();
        }

        public IEnumerable<GetTimeLineModel> GetTimeLine(TimeLineGetModel timeLine)
        {
            List<GetTimeLineModel> result = new List<GetTimeLineModel>();
            if (timeLine.IsServiceCategory == true)
            {
                foreach (var item in timeLine.ServiceSubCategory)
                {
                    int? servsubcatID = item.servsubcatID;
                    int? packID = item.packID;
                    int? parkID = item.parkID;
                    var objCustomerSpecilized = UhDB.CustomerSpecializedCleanings.Where(x => x.Customer.uID == timeLine.uID
                                                && x.CustomerOfficalDetail.catID == timeLine.catID &&
                                                x.CustomerOfficalDetail.catsubID == timeLine.catsubID &&
                                                x.servcatID == timeLine.servcatID && x.servsubcatID == servsubcatID
                                                ).AsEnumerable()
                                                .Select(p => new GetTempTimeLineModel
                                                {
                                                    TimeLine = UhDB.CustomerTimelines.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.packID == packID && x.parkID == parkID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                         .Select(r => new GetTimeLineModel
                                                         {
                                                             Date = r.StartDate,
                                                             StartTime = r.StartTime,
                                                             EndTime = r.EndTime
                                                         }).ToList()
                                                }).ToList();
                    foreach (var objCustomerSpecilize in objCustomerSpecilized)
                    {
                        result.AddRange(objCustomerSpecilize.TimeLine);
                    }

                }
            }
            else
            {

                var objService = UhDB.CustomerTimelines.Where(x => x.Customer.uID == timeLine.uID
                                && x.CustomerOfficalDetail.catID == timeLine.catID
                                && x.CustomerOfficalDetail.catsubID == timeLine.catsubID
                                && x.packID == timeLine.packID && x.parkID == timeLine.parkID
                                && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                .Select(p => new GetTimeLineModel
                                {
                                    Date = p.StartDate,
                                    StartTime = p.StartTime,
                                    EndTime = p.EndTime
                                }).ToList();
                result.AddRange(objService);
            }
            result = result.OrderBy(x => x.Date).ToList();
            return result;
        }

        public IEnumerable<GetSlotsTimeLineModel> GetSlotsTimeLine(SlotTimeLineGetModel timeLine)
        {
            List<GetSlotsTimeLineModel> result = new List<GetSlotsTimeLineModel>();
            if (timeLine.IsServiceCategory == true)
            {
                foreach (var item in timeLine.ServiceSubCategory)
                {
                    int? servsubcatID = item.servsubcatID;
                    int? packID = item.packID;
                    int? parkID = item.parkID;
                    var objCustomerSpecilized = UhDB.CustomerSpecializedCleanings.Where(x => x.Customer.uID == timeLine.uID
                                                && x.CustomerOfficalDetail.catID == timeLine.catID &&
                                                x.CustomerOfficalDetail.catsubID == timeLine.catsubID &&
                                                x.servcatID == timeLine.servcatID && x.servsubcatID == servsubcatID
                                                ).AsEnumerable()
                                                .Select(p => new GetTempTimeLineSlotModel
                                                {
                                                    TimeLine = UhDB.CustomerTimelines.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.packID == packID && x.parkID == parkID &&
                                                                EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(timeLine.Date) && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                         .Select(r => new GetSlotsTimeLineModel
                                                         {
                                                             StartTime = r.StartTime,
                                                             EndTime = r.EndTime
                                                         }).ToList()
                                                }).ToList();
                    foreach (var objCustomerSpecilize in objCustomerSpecilized)
                    {
                        result.AddRange(objCustomerSpecilize.TimeLine);
                        result = result.OrderBy(x => x.StartTime).ToList();
                    }

                }

            }
            else
            {

                var objService = UhDB.CustomerTimelines.Where(x => x.Customer.uID == timeLine.uID
                                && x.CustomerOfficalDetail.catID == timeLine.catID
                                && x.CustomerOfficalDetail.catsubID == timeLine.catsubID
                                && x.packID == timeLine.packID && x.parkID == timeLine.parkID
                                && x.IsActive == true && x.IsDelete == false
                                && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(timeLine.Date)).AsEnumerable()
                                .Select(p => new GetSlotsTimeLineModel
                                {
                                    StartTime = p.StartTime,
                                    EndTime = p.EndTime
                                }).ToList();
                if (objService != null)
                {
                    result.AddRange(objService);
                    result = result.OrderBy(x => x.StartTime).ToList();
                }
            }
            return result;
        }

        public IEnumerable<GetTimeLineModel> GetTimeLineByStaff(SlotTimeLineForSatffGetModel timeLine)
        {
            List<GetTimeLineModel> result = new List<GetTimeLineModel>();
            if (timeLine.IsTeam == true)
            {
                if (timeLine.IsServiceCategory == true)
                {
                    foreach (var item in timeLine.ServiceSubCategory)
                    {
                        int? servsubcatID = item.servsubcatID;
                        int? packID = item.packID;
                        int? parkID = item.parkID;
                        var objCustomerSpecilized = UhDB.CustomerSpecializedCleanings.Where(x => x.Customer.uID == timeLine.uID
                                                    && x.CustomerOfficalDetail.catID == timeLine.catID &&
                                                    x.CustomerOfficalDetail.catsubID == timeLine.catsubID &&
                                                    x.servcatID == timeLine.servcatID && x.servsubcatID == servsubcatID
                                                    ).AsEnumerable()
                                                    .Select(p => new GetTempTimeLineModel
                                                    {
                                                        TimeLine = UhDB.CustomerTimelines.Where(x => x.custID == p.custID &&
                                                                    x.custODID == p.custODID && x.packID == packID &&
                                                                    x.parkID == parkID && x.IsActive == true && x.IsDelete == false
                                                                    && x.CustomerOfficalDetail.teamID == timeLine.teamID).AsEnumerable()
                                                             .Select(r => new GetTimeLineModel
                                                             {
                                                                 Date = r.StartDate,
                                                                 StartTime = r.StartTime,
                                                                 EndTime = r.EndTime
                                                             }).ToList()
                                                    }).ToList();
                        foreach (var objCustomerSpecilize in objCustomerSpecilized)
                        {
                            result.AddRange(objCustomerSpecilize.TimeLine);
                        }

                    }
                }
                else
                {

                    var objService = UhDB.CustomerTimelines.Where(x => x.Customer.uID == timeLine.uID
                                    && x.CustomerOfficalDetail.catID == timeLine.catID
                                    && x.CustomerOfficalDetail.catsubID == timeLine.catsubID
                                    && x.packID == timeLine.packID && x.parkID == timeLine.parkID
                                    && x.IsActive == true && x.IsDelete == false && x.CustomerOfficalDetail.teamID == timeLine.teamID).AsEnumerable()
                                    .Select(p => new GetTimeLineModel
                                    {
                                        Date = p.StartDate,
                                        StartTime = p.StartTime,
                                        EndTime = p.EndTime
                                    }).ToList();
                    result.AddRange(objService);
                }
            }
            else
            {
                if (timeLine.IsServiceCategory == true)
                {
                    foreach (var item in timeLine.ServiceSubCategory)
                    {
                        int? servsubcatID = item.servsubcatID;
                        int? packID = item.packID;
                        int? parkID = item.parkID;
                        var objCustomerSpecilized = UhDB.CustomerSpecializedCleanings.Where(x => x.Customer.uID == timeLine.uID
                                                    && x.CustomerOfficalDetail.catID == timeLine.catID &&
                                                    x.CustomerOfficalDetail.catsubID == timeLine.catsubID &&
                                                    x.servcatID == timeLine.servcatID && x.servsubcatID == servsubcatID
                                                    ).AsEnumerable()
                                                    .Select(p => new GetTempTimeLineModel
                                                    {
                                                        TimeLine = UhDB.CustomerTimelines.Where(x => x.custID == p.custID &&
                                                                    x.custODID == p.custODID && x.packID == packID &&
                                                                    x.parkID == parkID && x.IsActive == true && x.IsDelete == false
                                                                    && x.CustomerOfficalDetail.stfID == timeLine.stfID).AsEnumerable()
                                                             .Select(r => new GetTimeLineModel
                                                             {
                                                                 Date = r.StartDate,
                                                                 StartTime = r.StartTime,
                                                                 EndTime = r.EndTime
                                                             }).ToList()
                                                    }).ToList();
                        foreach (var objCustomerSpecilize in objCustomerSpecilized)
                        {
                            result.AddRange(objCustomerSpecilize.TimeLine);
                        }

                    }
                }
                else
                {

                    var objService = UhDB.CustomerTimelines.Where(x => x.Customer.uID == timeLine.uID
                                    && x.CustomerOfficalDetail.catID == timeLine.catID
                                    && x.CustomerOfficalDetail.catsubID == timeLine.catsubID
                                    && x.packID == timeLine.packID && x.parkID == timeLine.parkID
                                    && x.IsActive == true && x.IsDelete == false && x.CustomerOfficalDetail.stfID == timeLine.stfID).AsEnumerable()
                                    .Select(p => new GetTimeLineModel
                                    {
                                        Date = p.StartDate,
                                        StartTime = p.StartTime,
                                        EndTime = p.EndTime
                                    }).ToList();
                    result.AddRange(objService);
                }
            }

            result = result.OrderBy(x => x.Date).ToList();
            return result;
        }

        public List<string> GetTimeSlot(int? packID, int? catID, int? catsubID)
        {
            List<string> result = new List<string>();
            DateTime TodayDate = DateTime.Now;
            DateTime LastDate = TodayDate.AddDays(30);
            int? packCount = UhDB.Packages.Where(x => x.packID == packID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().RecursiveTime;
            if (packCount == 0 || packCount == 1)
            {
                string Day = TodayDate.ToString("dddd");
                var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(TodayDate)
                               && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)).Select(p => new { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                if (objDates != null)
                {
                    for (var i = 1; i <= 7; i++)
                    {
                        DateTime checkDate = TodayDate.AddDays(i);
                        string dayOfWeek = checkDate.DayOfWeek.ToString();
                        if (dayOfWeek != "Friday")
                        {
                            var filteredDates = objDates
                                               .Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek).ToList();
                            if (!filteredDates.Any())
                            {
                                result.Add(dayOfWeek);
                            }
                            else
                            {
                                bool isDayAvailable = true;
                                TimeSpan preferredStartTime = new TimeSpan(8, 0, 0); // 8:00 AM
                                TimeSpan preferredEndTime = new TimeSpan(18, 0, 0);  // 6:00 PM

                                foreach (var date in filteredDates)
                                {
                                    DateTime parsedTime1 = DateTime.Parse(date.StartTime);
                                    DateTime parsedTime2 = DateTime.Parse(date.EndTime);

                                    TimeSpan serviceStartTime = parsedTime1.TimeOfDay;
                                    TimeSpan serviceEndTime = parsedTime2.TimeOfDay;

                                    if ((serviceEndTime < preferredStartTime) && (serviceStartTime > preferredEndTime))
                                    {
                                        isDayAvailable = false;
                                        break;
                                    }
                                }

                                if (isDayAvailable)
                                {
                                    result.Add(dayOfWeek);
                                }
                            }
                        }

                    }


                }

            }
            else if (packCount == 2)
            {
                string Day = TodayDate.ToString("dddd");
                var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(TodayDate)
                               && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)).Select(p => new { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                if (objDates != null)
                {
                    // Define preferred start and end times
                    TimeSpan preferredStartTime = new TimeSpan(8, 0, 0); // 8:00 AM
                    TimeSpan preferredEndTime = new TimeSpan(18, 0, 0);  // 6:00 PM

                    int daysToAdd = 0; // Variable to track days to add for scheduling
                    while (result.Count < 8)
                    {
                        DateTime checkDate = TodayDate.AddDays(daysToAdd);
                        string dayOfWeek = checkDate.DayOfWeek.ToString();
                        var filteredDates = objDates
                            .Where(x => x.StartDate.Value.ToString() == dayOfWeek)
                            .ToList();
                        if (!filteredDates.Any()) // If no conflicting appointments on the day
                        {
                            // Check if preferred time slot is available
                            bool isDayAvailable = true;
                            foreach (var date in filteredDates)
                            {
                                DateTime parsedTime1 = DateTime.Parse(date.StartTime);
                                DateTime parsedTime2 = DateTime.Parse(date.EndTime);

                                TimeSpan serviceStartTime = parsedTime1.TimeOfDay;
                                TimeSpan serviceEndTime = parsedTime2.TimeOfDay;

                                if ((preferredStartTime < serviceEndTime) && (preferredEndTime > serviceStartTime))
                                {
                                    isDayAvailable = false;
                                    break;
                                }
                            }

                            if (isDayAvailable)
                            {
                                result.Add(dayOfWeek); // Add the day to the result if available
                                daysToAdd += 3; // Add 3 days for the next service
                            }
                            else
                            {
                                daysToAdd++; // Increment day and check availability again
                            }
                        }
                        else
                        {
                            daysToAdd++; // Increment day and check availability again
                        }
                    }
                }
            }
            else if (packCount == 3)
            {
                string Day = TodayDate.ToString("dddd");
                var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(TodayDate)
                               && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)).Select(p => new { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                if (objDates != null)
                {
                    // Define preferred start and end times
                    TimeSpan preferredStartTime = new TimeSpan(8, 0, 0); // 8:00 AM
                    TimeSpan preferredEndTime = new TimeSpan(18, 0, 0);  // 6:00 PM
                    int servicesScheduled = 0;
                    bool isSaturday = false;
                    int daysToAdd = 0; // Variable to track days to add for scheduling
                    while (servicesScheduled < 12)
                    {
                        DateTime checkDate = TodayDate.AddDays(daysToAdd);
                        string dayOfWeek = checkDate.DayOfWeek.ToString();
                        if (dayOfWeek == "Saturday" || dayOfWeek == "Thursday" || dayOfWeek == "Wednesday" || dayOfWeek == "Sunday" || dayOfWeek == "Monday" || dayOfWeek == "Tuesday")
                        {
                            isSaturday = true;
                        }
                        if ((dayOfWeek != "Friday"))
                        {
                            var filteredDates = objDates
                            .Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek)
                            .ToList();

                            if (!filteredDates.Any()) // If no conflicting appointments on the day
                            {
                                // Check if preferred time slot is available
                                bool isDayAvailable = true;
                                foreach (var date in filteredDates)
                                {
                                    DateTime parsedTime1 = DateTime.Parse(date.StartTime);
                                    DateTime parsedTime2 = DateTime.Parse(date.EndTime);

                                    TimeSpan serviceStartTime = parsedTime1.TimeOfDay;
                                    TimeSpan serviceEndTime = parsedTime2.TimeOfDay;

                                    if ((serviceEndTime < preferredStartTime) && (serviceStartTime > preferredEndTime))
                                    {
                                        isDayAvailable = false;
                                        break;
                                    }
                                }

                                if (isDayAvailable)
                                {
                                    result.Add(dayOfWeek); // Add the day to the result if available
                                    servicesScheduled++; // Increment services scheduled
                                }
                            }
                            daysToAdd++;
                        }
                        else { daysToAdd++; }
                    }
                }
            }
            else if (packCount == 4)
            {
                string Day = TodayDate.ToString("dddd");
                var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(TodayDate)
                               && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)).Select(p => new { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                if (objDates != null)
                {
                    TimeSpan preferredStartTime = new TimeSpan(8, 0, 0); // 8:00 AM
                    TimeSpan preferredEndTime = new TimeSpan(18, 0, 0);  // 6:00 PM
                    int servicesScheduled = 0;
                    int daysToAdd = 0; // Variable to track days to add for scheduling
                    while (servicesScheduled < 16) // Schedule 4 times a week for a month (16 services)
                    {
                        DateTime checkDate = TodayDate.AddDays(daysToAdd);
                        string dayOfWeek = checkDate.DayOfWeek.ToString();

                        // Exclude Fridays, Saturdays, and Thursdays; Exclude Sundays and Wednesdays
                        if (dayOfWeek != "Friday")
                        {
                            var filteredDates = objDates
                                .Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek)
                                .ToList();

                            if (!filteredDates.Any()) // If no conflicting appointments on the day
                            {
                                // Check if preferred time slot is available
                                bool isDayAvailable = true;
                                foreach (var date in filteredDates)
                                {
                                    DateTime parsedTime1 = DateTime.Parse(date.StartTime);
                                    DateTime parsedTime2 = DateTime.Parse(date.EndTime);

                                    TimeSpan serviceStartTime = parsedTime1.TimeOfDay;
                                    TimeSpan serviceEndTime = parsedTime2.TimeOfDay;

                                    if ((serviceEndTime < preferredStartTime) && (serviceStartTime > preferredEndTime))
                                    {
                                        isDayAvailable = false;
                                        break;
                                    }
                                }

                                if (isDayAvailable)
                                {
                                    result.Add(dayOfWeek); // Add the day to the result if available
                                    servicesScheduled++; // Increment services scheduled
                                }
                            }
                        }

                        daysToAdd++; // Increment day and check availability for the next day
                    }

                }
            }
            else if (packCount == 5)
            {
                string Day = TodayDate.ToString("dddd");
                var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(TodayDate)
                               && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)).Select(p => new { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                if (objDates != null)
                {
                    // Define preferred start and end times
                    TimeSpan preferredStartTime = new TimeSpan(8, 0, 0); // 8:00 AM
                    TimeSpan preferredEndTime = new TimeSpan(18, 0, 0);  // 6:00 PM
                    int daysToAdd = 0; // Variable to track days to add for scheduling
                    bool isSaturday = false;
                    while (result.Count < 20) // Schedule 5 times a week for a month (20 services)
                    {

                        DateTime checkDate = TodayDate.AddDays(daysToAdd);
                        string dayOfWeek = checkDate.DayOfWeek.ToString();
                        // Randomly schedule a gap between Saturday and Thursday
                        // Make Saturday and Thursday compulsory
                        if (dayOfWeek == "Saturday" || dayOfWeek == "Thursday" || dayOfWeek == "Sunday" || dayOfWeek == "Wednesday" || dayOfWeek == "Tuesday" || dayOfWeek == "Thursday")
                        {
                            isSaturday = true;
                        }

                        // Randomly schedule a gap between Saturday and Thursday
                        if ((dayOfWeek != "Friday") && isSaturday)
                        {
                            isSaturday = false; // Reset flag for the next cycle
                            var filteredDates = objDates
                                .Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek)
                                .ToList();

                            if (!filteredDates.Any()) // If no conflicting appointments on the day
                            {
                                // Check if preferred time slot is available
                                bool isDayAvailable = true;
                                foreach (var date in filteredDates)
                                {
                                    DateTime parsedTime1 = DateTime.Parse(date.StartTime);
                                    DateTime parsedTime2 = DateTime.Parse(date.EndTime);

                                    TimeSpan serviceStartTime = parsedTime1.TimeOfDay;
                                    TimeSpan serviceEndTime = parsedTime2.TimeOfDay;

                                    if ((serviceEndTime < preferredStartTime) && (serviceStartTime > preferredEndTime))
                                    {
                                        isDayAvailable = false;
                                        break;
                                    }
                                }

                                if (isDayAvailable)
                                {
                                    result.Add(dayOfWeek); // Add the day to the result if available
                                }
                            }

                            // Increment day and check availability for the next day
                        }
                        daysToAdd++;
                    }
                }
            }
            else if (packCount == 6)
            {
                string Day = TodayDate.ToString("dddd");
                var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(TodayDate)
                               && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)).Select(p => new { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                if (objDates != null)
                {
                    TimeSpan preferredStartTime = new TimeSpan(8, 0, 0); // 8:00 AM
                    TimeSpan preferredEndTime = new TimeSpan(18, 0, 0);  // 6:00 PM

                    int daysToAdd = 0; // Variable to track days to add for scheduling
                    while (result.Count < 24) // Schedule 6 times a week for a month (24 services)
                    {
                        DateTime checkDate = TodayDate.AddDays(daysToAdd);
                        string dayOfWeek = checkDate.DayOfWeek.ToString();
                        // Exclude Fridays from scheduling
                        if (dayOfWeek != "Friday")
                        {
                            var filteredDates = objDates
                                .Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek)
                                .ToList();

                            if (!filteredDates.Any()) // If no conflicting appointments on the day
                            {
                                // Check if preferred time slot is available
                                bool isDayAvailable = true;
                                foreach (var date in filteredDates)
                                {
                                    DateTime parsedTime1 = DateTime.Parse(date.StartTime);
                                    DateTime parsedTime2 = DateTime.Parse(date.EndTime);

                                    TimeSpan serviceStartTime = parsedTime1.TimeOfDay;
                                    TimeSpan serviceEndTime = parsedTime2.TimeOfDay;

                                    if ((serviceEndTime < preferredStartTime) && (serviceStartTime > preferredEndTime))
                                    {
                                        isDayAvailable = false;
                                        break;
                                    }
                                }

                                if (isDayAvailable)
                                {
                                    result.Add(dayOfWeek); // Add the day to the result if available
                                }
                            }
                        }

                        daysToAdd++; // Increment day and check availability for the next day
                    }
                }
            }
            return result;
        }

        public int? AssignedTeamAuto(int? Area, int? catID, int? catsubID)
        {
            int? teamID = null;

            var objPropetyAreaTeamAssign = UhDB.StaffServices.Where(x => x.propaID == Area && x.catID == catID && x.catsubID == catsubID).ToList();
            foreach (var item in objPropetyAreaTeamAssign)
            {
                int? TempTeamID = item.teamID;
                int CountTempTeamID = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.teamID == TempTeamID
                                      && x.IsActive == true && x.IsDelete == false).Count();
                if (CountTempTeamID == 0)
                {
                    teamID = TempTeamID;
                }
                else
                {
                    var objOtherPropertyAreaTeams = UhDB.StaffServices.Where(x => x.catID == catID && x.catsubID == catsubID && x.propaID != Area
                                                   && x.IsActive == true && x.IsDelete == false).ToList();
                    foreach (var objOtherPropertyAreaTeam in objOtherPropertyAreaTeams)
                    {
                        TempTeamID = objOtherPropertyAreaTeam.teamID;
                        CountTempTeamID = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.teamID == TempTeamID
                                          && x.IsActive == true && x.IsDelete == false).Count();
                        if (CountTempTeamID == 0)
                        {
                            teamID = TempTeamID;
                        }
                    }

                }
            }

            return teamID;
        }

        public List<GetTimeLineResulsubsettModel> GetTimeForChoosenBookingDaysTwiceInAWeek(int? packID, int? catID, int? catsubID, List<string> Days)
        {
            List<GetTimeLineResulsubsettModel> result = new List<GetTimeLineResulsubsettModel>();
            DateTime TodayDate = DateTime.Now;
            DateTime LastDate = TodayDate.AddMonths(1);
            // Retrieve the list of appointments for the specified category, subcategory, and within the date range
            var objTeams = UhDB.Teams.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objTeam in objTeams)
            {
                int? teamID = objTeam.teamID;
                var objDates = UhDB.CustomerTimelines
                    .Where(x => x.CustomerOfficalDetail.catID == catID &&
                                x.CustomerOfficalDetail.catsubID == catsubID &&
                                EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(TodayDate) &&
                                EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate) && x.packID == packID
                                && x.CustomerOfficalDetail.teamID == teamID)
                    .Select(p => new { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime })
                    .ToList();

                if (objDates != null)
                {
                    foreach (string dayOfWeek in Days)
                    {
                        // Filter dates for the specified day of the week
                        var filteredDates = objDates
                            .Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek)
                            .ToList();

                        if (filteredDates.Any())
                        {
                            foreach (var date in filteredDates)
                            {
                                string serviceStartTime = date.StartTime;
                                string serviceEndTime = date.EndTime;
                                result.Add(new GetTimeLineResulsubsettModel { StartTime = serviceStartTime, EndTime = serviceEndTime });
                            }
                        }
                    }
                }
            }

            return result;
        }

        public IEnumerable<GetTimeLineModel> GetDates(int? packID, int? catID, int? catsubID, List<string> Days)
        {
            DateTime TodayDate = DateTime.Now;
            DateTime LastDate = TodayDate.AddMonths(1);
            List<GetTimeLineModel> result = new List<GetTimeLineModel>();
            var objDates = UhDB.CustomerTimelines
                   .Where(x => x.CustomerOfficalDetail.catID == catID &&
                               x.CustomerOfficalDetail.catsubID == catsubID &&
                               EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(TodayDate) &&
                               EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate))
                   .Select(p => new { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime })
                   .ToList();
            if (objDates != null)
            {
                foreach (string dayOfWeek in Days)
                {
                    // Filter dates for the specified day of the week
                    var filteredDates = objDates
                        .Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek)
                        .ToList();

                    if (filteredDates.Any())
                    {
                        foreach (var date in filteredDates)
                        {
                            DateTime? StartDate = date.StartDate;
                            result.Add(new GetTimeLineModel { Date = StartDate });
                        }
                    }
                }
            }

            return result;
        }

        public List<TimeRange> GetTimeSlotByTeam(int? packID, int? parkID, int? catID, int? catsubID, int? uID, int? Area, string Day)
        {
            List<TimeRange> result = new List<TimeRange>();
            DateTime TodayDate = DateTime.Now;
            DateTime LastDate = TodayDate.AddDays(30);
            List<TeamCount> teamCount = new List<TeamCount>();
            var objPricing = UhDB.Pricings.Where(x => x.packID == packID && x.parkID == parkID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            if (objPricing.TimeMeasurement == "Hour")
            {

                // Convert hours to TimeSpan
                double timeSpan = Convert.ToDouble(TimeSpan.FromMinutes(5));

                // Format TimeSpan to string "HH:mm"
                string timeFormatted = string.Format("{0:D1}", timeSpan);
            }
            var objTeams = UhDB.StaffServices.Where(x => x.propaID == Area && x.catID == catID && x.catsubID == catsubID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objTeam in objTeams)
            {
                int? teamID = objTeam.teamID;
                int? CountTeamTask = UhDB.CustomerOfficalDetails.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).Count();
                teamCount.Add(new TeamCount { teamID = teamID, Count = CountTeamTask });
            }
            if (teamCount.Count() != 0)
            {
                TeamCount selectTeam = teamCount.OrderBy(p => p.Count).FirstOrDefault();
                int? selectTeamID = selectTeam.teamID;
                var objService = UhDB.CustomerTimelines.Where(x => x.Customer.uID == uID
                                 && x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID
                                 && x.packID == packID && x.IsActive == true && x.IsDelete == false && x.CustomerOfficalDetail.teamID == selectTeamID
                                 && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(TodayDate)
                                 && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)).AsEnumerable()
                                 .Select(p => new
                                 {
                                     Date = p.StartDate,
                                     Start = ConvertToTimeSpans(p.StartTime),
                                     End = ConvertToTimeSpans(p.EndTime)
                                 }).ToList();
                if (objService != null)
                {
                    var filteredDates = objService.Where(x => x.Date.Value.DayOfWeek.ToString() == Day).Select(p => new TimeRange { Start = p.Start, End = p.End }).ToList();
                    TimeRange originalRange = new TimeRange();
                    originalRange.Start = ConvertToTimeSpans("8:00 AM");
                    originalRange.End = ConvertToTimeSpans("06:00 PM");
                    if (filteredDates.Count() != 0)
                    {
                        result = RemoveIntervals(originalRange, filteredDates);
                    }
                    else
                    {
                        //result = GetTimeDifference(originalRange);
                    }

                }
            }

            return result;
        }

        public List<TimeRange> RemoveIntervals(TimeRange originalRange, List<TimeRange> intervals)
        {
            List<TimeRange> result = new List<TimeRange>();
            if (intervals == null || intervals.Count == 0)
            {
                result.Add(originalRange);
                return result;
            }

            // Sort intervals by start time
            intervals = intervals.OrderBy(interval => interval.Start).ToList();

            // Merge overlapping intervals
            List<TimeRange> mergedIntervals = new List<TimeRange>();
            TimeRange currentInterval = intervals[0];

            for (int i = 1; i < intervals.Count; i++)
            {
                if (intervals[i].Start <= currentInterval.End)
                {
                    currentInterval.End = new TimeSpan(Math.Max(currentInterval.End.Ticks, intervals[i].End.Ticks));
                }
                else
                {
                    mergedIntervals.Add(currentInterval);
                    currentInterval = intervals[i];
                }
            }
            mergedIntervals.Add(currentInterval);

            // Remove merged intervals from the original range
            TimeSpan currentStart = originalRange.Start;

            foreach (var interval in mergedIntervals)
            {
                if (interval.Start > currentStart)
                {
                    result.Add(new TimeRange { Start = currentStart, End = interval.Start });
                }
                currentStart = interval.End > currentStart ? interval.End : currentStart;
            }

            if (currentStart < originalRange.End)
            {
                result.Add(new TimeRange { Start = currentStart, End = originalRange.End });
            }

            // Ensure a minimum one-hour gap
            List<TimeRange> finalResult = new List<TimeRange>();
            foreach (var range in result)
            {
                if (finalResult.Count == 0)
                {
                    finalResult.Add(range);
                }
                else
                {
                    TimeRange lastRange = finalResult.Last();
                    if (range.Start - lastRange.End >= TimeSpan.FromMinutes(90))
                    {
                        TimeSpan ESP = (range.Start - originalRange.End);
                        int Count = Math.Abs(Convert.ToInt32(ESP.TotalHours));
                        for (int i = 0; i <= Count - 1; i++)
                        {

                            TimeSpan TI1 = range.Start;
                            TimeSpan T2 = new TimeSpan(0, 90, 0);
                            TimeSpan TI2 = TI1.Add(T2);
                            if (range.Start <= originalRange.End)
                            {
                                if (TI2 <= originalRange.End)
                                {
                                    finalResult.Add(new TimeRange { Start = TI1, End = TI2 });
                                    range.Start = TI2;
                                }
                            }
                        }
                    }
                    else
                    {
                        finalResult.Add(range);
                    }
                }
            }

            return finalResult;
        }

        public List<TimeRange> RemoveIntervals(TimeRange originalRange, List<TimeRange> intervals, int Duration)
        {
            List<TimeRange> result = new List<TimeRange>();
            TimeSpan currentStart = originalRange.Start;
            foreach (var interval in intervals.OrderBy(i => i.Start))
            {
                // If the current interval is within the original range, exclude it
                if (interval.Start > currentStart)
                {
                    result.Add(new TimeRange
                    {
                        Start = currentStart,
                        End = interval.Start
                    });
                }
                // Move the current start time after the end of the interval
                currentStart = interval.End;
            }

            // If there's still time left between the last interval and the end of the original range
            if (currentStart < originalRange.End)
            {
                result.Add(new TimeRange
                {
                    Start = currentStart,
                    End = originalRange.End
                });
            }

            // Ensure a minimum one-hour gap
            List<TimeRange> finalResult = new List<TimeRange>();
            foreach (var range in result)
            {
                if (finalResult.Count == 0)
                {
                    if (range.Start == originalRange.Start)
                    {
                        TimeSpan T4 = new TimeSpan(0, 15, 0);
                        range.End = range.End.Subtract(T4);
                        TimeSpan SubtractRange = (range.Start - range.End);
                        int MinutesBeforIteration = Math.Abs(Convert.ToInt32(SubtractRange.TotalMinutes));
                        if (MinutesBeforIteration >= Duration)
                        {
                            TimeRange onlyOne = new TimeRange();
                            onlyOne.Start = range.Start;
                            onlyOne.End = range.End;
                            finalResult = GetTimeDifference1(onlyOne, Duration);
                        }
                    }
                    else
                    {
                        TimeSpan SubtractRange = (range.Start - range.End);
                        int MinutesBeforIteration = Math.Abs(Convert.ToInt32(SubtractRange.TotalMinutes));
                        if (MinutesBeforIteration >= Duration)
                        {
                            TimeRange onlyOne = new TimeRange();
                            TimeSpan T3 = new TimeSpan(0, 15, 0);
                            onlyOne.Start = range.Start.Add(T3);

                            TimeSpan T4 = new TimeSpan(0, 15, 0);
                            onlyOne.End = range.End.Subtract(T4);
                            finalResult = GetTimeDifference1(onlyOne, Duration);
                        }
                    }
                }
                else
                {
                    TimeSpan T3 = new TimeSpan(0, 15, 0);
                    range.Start = range.Start.Add(T3);
                    TimeSpan T4 = new TimeSpan(0, 15, 0);
                    if (range.End != originalRange.End)
                    {
                        range.End = range.End.Subtract(T4);
                    }
                    TimeSpan SubtractRange = (range.Start - range.End);
                    int MinutesBeforIteration = Math.Abs(Convert.ToInt32(SubtractRange.TotalMinutes));
                    if (MinutesBeforIteration >= Duration)
                    {
                        TimeSpan ESP = (range.Start - range.End);
                        int ESPMinutes = Convert.ToInt32(ESP.TotalMinutes);
                        int Count = Math.Abs(ESPMinutes / Convert.ToInt32(Duration));
                        for (int i = 0; i <= Count; i++)
                        {
                            if (i == 0)
                            {
                                TimeSpan TI1 = range.Start;
                                //TimeSpan T3 = new TimeSpan(0, 15, 0);
                                //TI1 = TI1.Add(T3);
                                TimeSpan T2 = new TimeSpan(0, Duration, 0);
                                TimeSpan TI2 = TI1.Add(T2);

                                if (range.Start <= originalRange.End)
                                {
                                    if (TI2 <= range.End)
                                    {
                                        finalResult.Add(new TimeRange { Start = TI1, End = TI2 });
                                        range.Start = TI2;
                                    }
                                }
                            }
                            else
                            {
                                TimeSpan TI1 = range.Start;
                                TimeSpan T5 = new TimeSpan(0, 15, 0);
                                TI1 = TI1.Add(T5);
                                TimeSpan T2 = new TimeSpan(0, Duration, 0);
                                TimeSpan TI2 = TI1.Add(T2);

                                if (range.Start <= originalRange.End)
                                {
                                    if (TI2 <= range.End)
                                    {
                                        finalResult.Add(new TimeRange { Start = TI1, End = TI2 });
                                        range.Start = TI2;
                                    }
                                }
                            }
                        }
                    }
                }

            }
            if (intervals.Count() == 1)
            {

            }
            return finalResult;
        }

        public List<TimeRange> RemoveInterval1(List<TimeRange> originalRange, List<TimeRange> intervals, int Duration)
        {
            List<TimeRange> L3 = new List<TimeRange>();
            foreach (var interval in intervals)
            {
                int index = originalRange.FindIndex(p => p.Start == interval.Start && p.End == interval.End);

                // Skip processing if the index is not found
                if (index < 0)
                    continue;

                if (L3.Count == 0)
                {
                    // Add the range from originalRange up to the found index
                    List<TimeRange> L4 = originalRange.GetRange(0, index);
                    L3.AddRange(L4);
                    originalRange.RemoveAt(index); // Remove the found range

                    // Adjust the subsequent time ranges by 15 minutes
                    TimeSpan EndTime = new TimeSpan(18, 0, 0);
                    for (int i = index; i < originalRange.Count; i++)
                    {
                        TimeRange TimeRange1 = originalRange[i];
                        if (TimeRange1.Start < EndTime)
                        {
                            TimeSpan T1 = new TimeSpan(0, 15, 0);
                            TimeRange1.Start = TimeRange1.Start.Add(T1);
                            TimeSpan T2 = new TimeSpan(0, Duration, 0);
                            TimeRange1.End = TimeRange1.Start.Add(T2);
                            L3.Add(TimeRange1);
                        }
                    }
                }
                else
                {
                    index = L3.FindIndex(p => p.Start == interval.Start && p.End == interval.End);

                    if (index < 0)
                        continue;

                    List<TimeRange> L4 = L3.GetRange(0, index);
                    L3.AddRange(L4);

                    HashSet<TimeRange> unique = new HashSet<TimeRange>(L3);
                    L3 = unique.ToList();
                    L3.RemoveAt(index);

                    // Adjust the subsequent time ranges by 15 minutes
                    TimeSpan EndTime = new TimeSpan(18, 0, 0);
                    for (int i = index; i < L3.Count; i++)
                    {
                        TimeRange TimeRange1 = L3[i];
                        if (TimeRange1.Start < EndTime)
                        {
                            TimeSpan T1 = new TimeSpan(0, 15, 0);
                            TimeRange1.Start = TimeRange1.Start.Add(T1);
                            TimeSpan T2 = new TimeSpan(0, Duration, 0);
                            TimeRange1.End = TimeRange1.Start.Add(T2);
                            L3[i] = TimeRange1;
                        }
                    }
                }

                // Ensure the list is ordered and unique
                L3 = L3.OrderBy(x => x.Start).Distinct().ToList();
            }

            return L3;

        }

        private List<TimeRange> GetTimeDifference(TimeRange originalRange)
        {
            List<TimeRange> finalResult = new List<TimeRange>();
            TimeSpan ESP = (originalRange.Start - originalRange.End);
            int Count = Math.Abs(Convert.ToInt32(ESP.TotalHours));
            for (int i = 0; i <= Count - 1; i++)
            {

                TimeSpan TI1 = originalRange.Start;
                TimeSpan T2 = new TimeSpan(0, 90, 0);
                TimeSpan TI2 = TI1.Add(T2);
                if (originalRange.Start <= originalRange.End)
                {
                    if (TI2 <= originalRange.End)
                    {
                        finalResult.Add(new TimeRange { Start = TI1, End = TI2 });
                        originalRange.Start = TI2;
                    }
                }
            }
            return finalResult;
        }

        private List<TimeRange> GetTimeDifference1(TimeRange originalRange, int Time)
        {
            List<TimeRange> finalResult = new List<TimeRange>();
            TimeSpan ESP = (originalRange.End - originalRange.Start);
            int Count = Math.Abs(Convert.ToInt32(ESP.TotalHours));
            Count = Count + 3;
            for (int i = 0; i <= Count; i++)
            {

                TimeSpan TI1 = originalRange.Start;
                TimeSpan T2 = new TimeSpan(0, Time, 0);
                TimeSpan TI2 = TI1.Add(T2);
                if (originalRange.Start <= originalRange.End)
                {
                    if (TI2 <= originalRange.End)
                    {
                        finalResult.Add(new TimeRange { Start = TI1, End = TI2 });
                        originalRange.Start = TI2;
                    }
                }
            }
            return finalResult;
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


        public List<GetResultsForTimeSlot> GetResultsForTimeSlots(int? packID, int? catID, int? catsubID, int? propresID)
        {
            List<GetResultsForTimeSlot> result = new List<GetResultsForTimeSlot>();
            DateTime TodayDate = DateTime.Now;
            DateTime StartDate = TodayDate.AddHours(24);
            //DateTime LastDate = StartDate.AddDays(30);
            List<GetDayWithTeam> GetDayWithTeam = new List<GetDayWithTeam>();

            int? packCount = UhDB.Packages.Where(x => x.packID == packID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().RecursiveTime;
            if (packCount == 0 || packCount == 1)
            {
                var objTeams = UhDB.StaffServices.Where(x => x.MainCategory.uID == 1 && x.catID == 1 && x.catsubID == 1 && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objTeam in objTeams)
                {
                    int? teamID = objTeam.teamID;
                    int CountTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountTeams == 0)
                    {
                        string Day = StartDate.ToString("dddd");
                        for (var i = 1; i <= 7; i++)
                        {
                            DateTime checkDate = StartDate.AddDays(i);
                            string dayOfWeek = checkDate.DayOfWeek.ToString();
                            GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = dayOfWeek });
                        }
                    }
                    else
                    {
                        string Day = StartDate.ToString("dddd");
                        var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID
                                       && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                       && x.teamID == teamID).Select(p => new { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                        if (objDates != null)
                        {
                            for (int i = 1; i <= 7; i++)
                            {
                                DateTime checkDate = StartDate;
                                string dayOfWeek = checkDate.DayOfWeek.ToString();
                                var filteredDates = objDates.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek).ToList();
                                if (!filteredDates.Any())
                                {
                                    GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = dayOfWeek });
                                    StartDate = StartDate.AddDays(1);
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == packID && x.proprestID == propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);

                                    }
                                    if (ResultimeRange != null)
                                    {
                                        GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = dayOfWeek });
                                        StartDate = StartDate.AddDays(1);
                                    }
                                    else
                                    {
                                        StartDate = StartDate.AddDays(1);
                                    }
                                }
                            }

                        }
                    }

                }
                var result1 = GetDayWithTeam.GroupBy(x => x.Days).Select(group => new
                {
                    Days = group.Key,
                    Teams = group.ToList()
                });
                foreach (var item in result1)
                {
                    string Days = item.Days;
                    List<int?> Teams = item.Teams.Select(p => p.teamID).Distinct().ToList();
                    result.Add(new GetResultsForTimeSlot { Days = Days, Teams = Teams });
                }

            }
            else if (packCount == 2)
            {
                var objTeams = UhDB.StaffServices.Where(x => x.MainCategory.uID == 1 && x.catID == 1 && x.catsubID == 1 && x.IsActive == true && x.IsDelete == false).ToList();
                int length = 8 * objTeams.Count();
                foreach (var objTeam in objTeams)
                {
                    int? teamID = objTeam.teamID;
                    int CountTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountTeams == 0)
                    {
                        List<string> Days = GetBundles4();
                        foreach (var Day in Days)
                        {
                            GetDayWithTeam.Add(new GetDayWithTeam { teamID = teamID, Days = Day });
                        }
                    }
                    else
                    {
                        string Day = StartDate.ToString("dddd");
                        var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID
                                       && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                       && x.teamID == teamID).Select(p => new { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                        if (objDates != null)
                        {
                            int daysToAdd = 0;
                            int CountOFAdd = 0;
                            List<string> Days = GetBundles4();
                            foreach (var InDay in Days)
                            {
                                if (InDay.Contains(","))
                                {
                                    string[] stringArray = InDay.Split(',');
                                    string dayOfWeek1 = stringArray[0];
                                    string dayOfWeek2 = stringArray[1];
                                    string FirstDay = null, SecondDay = null;
                                    var filteredDates1 = objDates.Where(x => x.StartDate.Value.ToString() == dayOfWeek1).ToList();
                                    if (!filteredDates1.Any())
                                    {
                                        FirstDay = dayOfWeek1;
                                    }
                                    else
                                    {
                                        bool isDayAvailable = true;
                                        var groupedDates = filteredDates1.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == packID && x.proprestID == propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            Time = Convert.ToInt32(objTimeRange.Duration);
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        if (ResultimeRange != null)
                                        {
                                            FirstDay = dayOfWeek1;
                                        }
                                    }
                                    var filteredDates2 = objDates.Where(x => x.StartDate.Value.ToString() == dayOfWeek2).ToList();
                                    if (!filteredDates2.Any())
                                    {
                                        SecondDay = dayOfWeek2;
                                    }
                                    else
                                    {
                                        bool isDayAvailable = true;
                                        var groupedDates = filteredDates1.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == packID && x.proprestID == propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        if (ResultimeRange != null)
                                        {
                                            SecondDay = dayOfWeek2;
                                        }
                                    }
                                    if (FirstDay != null && SecondDay != null)
                                    {
                                        GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = FirstDay + "," + SecondDay });
                                        daysToAdd += 4;
                                        CountOFAdd += 1;
                                    }
                                    else
                                    {
                                        daysToAdd += 1;
                                    }


                                }

                            }

                        }

                    }
                }
                var result1 = GetDayWithTeam.GroupBy(x => x.Days).Select(group => new
                {
                    Days = group.Key,
                    Teams = group.ToList()
                });
                foreach (var item in result1)
                {
                    string Days = item.Days;
                    List<int?> Teams = item.Teams.Select(p => p.teamID).Distinct().ToList();
                    result.Add(new GetResultsForTimeSlot { Days = Days, Teams = Teams });
                }
            }
            else if (packCount == 3)
            {
                var objTeams = UhDB.StaffServices.Where(x => x.MainCategory.uID == 1 && x.catID == 1 && x.catsubID == 1 && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objTeam in objTeams)
                {
                    int? teamID = objTeam.teamID;
                    int CountTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountTeams == 0)
                    {
                        List<List<string>> dayBundles = GetDayBundles();
                        foreach (var bundle in dayBundles)
                        {
                            GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = bundle[0] + "," + bundle[1] + "," + bundle[2] });
                        }
                    }
                    else
                    {
                        var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID
                                       && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                       && x.teamID == teamID).Select(p => new { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                        if (objDates != null)
                        {
                            string FirstDay = null, SecondDay = null, ThirdDay = null;
                            List<List<string>> dayBundles = GetDayBundles();
                            foreach (var bundle in dayBundles)
                            {
                                string dayOfWeek1 = bundle[0];
                                string dayOfWeek2 = bundle[1];
                                string dayOfWeek3 = bundle[2];
                                var filteredDates1 = objDates.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek1).ToList();
                                if (!filteredDates1.Any())
                                {
                                    FirstDay = dayOfWeek1;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates1.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == packID && x.proprestID == propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        FirstDay = dayOfWeek1;
                                    }

                                }

                                var filteredDates2 = objDates.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek2).ToList();
                                if (!filteredDates2.Any())
                                {
                                    SecondDay = dayOfWeek2;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates2.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == packID && x.proprestID == propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        SecondDay = dayOfWeek2;
                                    }

                                }

                                var filteredDates3 = objDates.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek3).ToList();
                                if (!filteredDates3.Any())
                                {
                                    ThirdDay = dayOfWeek3;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates3.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == packID && x.proprestID == propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        ThirdDay = dayOfWeek3;
                                    }

                                }
                                if (FirstDay != null && SecondDay != null && ThirdDay != null)
                                {
                                    string[] inputDays = new string[] { FirstDay, SecondDay, ThirdDay };
                                    List<string> resultDays = new List<string>();

                                    for (int i = 0; i < inputDays.Length; i++)
                                    {
                                        bool isRepeated = false;

                                        for (int j = 0; j < inputDays.Length; j++)
                                        {
                                            if (i != j && inputDays[i] == inputDays[j])
                                            {
                                                isRepeated = true;
                                                break;
                                            }
                                        }

                                        if (!isRepeated)
                                        {
                                            resultDays.Add(inputDays[i]);
                                        }
                                    }

                                    if (resultDays != null)
                                    {
                                        if (resultDays.Count() == 3)
                                        {
                                            GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = FirstDay + "," + SecondDay + "," + ThirdDay });
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                var result1 = GetDayWithTeam.GroupBy(x => x.Days).Select(group => new
                {
                    Days = group.Key,
                    Teams = group.ToList()
                });
                foreach (var item in result1)
                {
                    string Days = item.Days;
                    List<int?> Teams = item.Teams.Select(p => p.teamID).Distinct().ToList();
                    result.Add(new GetResultsForTimeSlot { Days = Days, Teams = Teams });
                }
            }
            else if (packCount == 4)
            {
                var objTeams = UhDB.StaffServices.Where(x => x.MainCategory.uID == 1 && x.catID == 1 && x.catsubID == 1 && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objTeam in objTeams)
                {
                    int? teamID = objTeam.teamID;
                    int CountTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountTeams == 0)
                    {
                        List<List<string>> dayBundles = GetDayBundles1();
                        foreach (var bundle in dayBundles)
                        {
                            GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = bundle[0] + "," + bundle[1] + "," + bundle[2] + "," + bundle[3] });
                        }
                    }
                    else
                    {
                        var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID
                                       && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                       && x.teamID == teamID).Select(p => new { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                        if (objDates != null)
                        {
                            string FirstDay = null, SecondDay = null, ThirdDay = null, FourthDay = null;
                            List<List<string>> dayBundles = GetDayBundles1();
                            foreach (var bundle in dayBundles)
                            {
                                string dayOfWeek1 = bundle[0];
                                string dayOfWeek2 = bundle[1];
                                string dayOfWeek3 = bundle[2];
                                string dayOfWeek4 = bundle[3];

                                var filteredDates1 = objDates.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek1).ToList();
                                if (!filteredDates1.Any())
                                {
                                    FirstDay = dayOfWeek1;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates1.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == packID && x.proprestID == propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        FirstDay = dayOfWeek1;
                                    }

                                }

                                var filteredDates2 = objDates.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek2).ToList();
                                if (!filteredDates2.Any())
                                {
                                    SecondDay = dayOfWeek2;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates2.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == packID && x.proprestID == propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        SecondDay = dayOfWeek2;
                                    }

                                }

                                var filteredDates3 = objDates.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek3).ToList();
                                if (!filteredDates3.Any())
                                {
                                    ThirdDay = dayOfWeek3;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates3.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == packID && x.proprestID == propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        ThirdDay = dayOfWeek3;
                                    }

                                }

                                var filteredDates4 = objDates.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek4).ToList();
                                if (!filteredDates4.Any())
                                {
                                    FourthDay = dayOfWeek4;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates4.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == packID && x.proprestID == propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        FourthDay = dayOfWeek4;
                                    }
                                }
                                if (FirstDay != null && SecondDay != null && ThirdDay != null && FourthDay != null)
                                {
                                    string[] inputDays = new string[] { FirstDay, SecondDay, ThirdDay, FourthDay };
                                    List<string> resultDays = new List<string>();

                                    for (int i = 0; i < inputDays.Length; i++)
                                    {
                                        bool isRepeated = false;

                                        for (int j = 0; j < inputDays.Length; j++)
                                        {
                                            if (i != j && inputDays[i] == inputDays[j])
                                            {
                                                isRepeated = true;
                                                break;
                                            }
                                        }

                                        if (!isRepeated)
                                        {
                                            resultDays.Add(inputDays[i]);
                                        }
                                    }
                                    if (resultDays != null)
                                    {
                                        if (resultDays.Count() == 4)
                                        {
                                            GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = FirstDay + "," + SecondDay + "," + ThirdDay + "," + FourthDay });
                                        }
                                    }

                                }

                            }
                        }
                    }
                }
                var result1 = GetDayWithTeam.GroupBy(x => x.Days).Select(group => new
                {
                    Days = group.Key,
                    Teams = group.ToList()
                });
                foreach (var item in result1)
                {
                    string Days = item.Days;
                    List<int?> Teams = item.Teams.Select(p => p.teamID).Distinct().ToList();
                    result.Add(new GetResultsForTimeSlot { Days = Days, Teams = Teams });
                }
            }
            else if (packCount == 5)
            {
                var objTeams = UhDB.StaffServices.Where(x => x.MainCategory.uID == 1 && x.catID == 1 && x.catsubID == 1 && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objTeam in objTeams)
                {
                    int? teamID = objTeam.teamID;
                    int CountTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountTeams == 0)
                    {
                        List<List<string>> dayBundles = GetDayBundles2();
                        foreach (var bundle in dayBundles)
                        {
                            GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = bundle[0] + "," + bundle[1] + "," + bundle[2] + "," + bundle[3] + "," + bundle[4] });
                        }
                    }
                    else
                    {
                        var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                       && x.teamID == teamID).Select(p => new { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                        if (objDates != null)
                        {
                            string FirstDay = null, SecondDay = null, ThirdDay = null, FourthDay = null, FifthDay = null;
                            List<List<string>> dayBundles = GetDayBundles2();
                            foreach (var bundle in dayBundles)
                            {
                                string dayOfWeek1 = bundle[0];
                                string dayOfWeek2 = bundle[1];
                                string dayOfWeek3 = bundle[2];
                                string dayOfWeek4 = bundle[3];
                                string dayOfWeek5 = bundle[4];

                                var filteredDates1 = objDates.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek1).ToList();
                                if (!filteredDates1.Any())
                                {
                                    FirstDay = dayOfWeek1;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates1.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == packID && x.proprestID == propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        FirstDay = dayOfWeek1;
                                    }

                                }

                                var filteredDates2 = objDates.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek2).ToList();
                                if (!filteredDates2.Any())
                                {
                                    SecondDay = dayOfWeek2;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates2.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == packID && x.proprestID == propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        SecondDay = dayOfWeek2;
                                    }

                                }

                                var filteredDates3 = objDates.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek3).ToList();
                                if (!filteredDates3.Any())
                                {
                                    ThirdDay = dayOfWeek3;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates3.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == packID && x.proprestID == propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        ThirdDay = dayOfWeek3;
                                    }

                                }

                                var filteredDates4 = objDates.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek4).ToList();
                                if (!filteredDates4.Any())
                                {
                                    FourthDay = dayOfWeek4;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates4.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == packID && x.proprestID == propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        FourthDay = dayOfWeek4;
                                    }
                                }


                                var filteredDates5 = objDates.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek5).ToList();
                                if (!filteredDates5.Any())
                                {
                                    FifthDay = dayOfWeek5;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates5.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == packID && x.proprestID == propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        FifthDay = dayOfWeek5;
                                    }
                                }

                                if (FirstDay != null && SecondDay != null && ThirdDay != null && FourthDay != null && FifthDay != null)
                                {
                                    string[] inputDays = new string[] { FirstDay, SecondDay, ThirdDay, FourthDay, FifthDay };
                                    List<string> resultDays = new List<string>();

                                    for (int i = 0; i < inputDays.Length; i++)
                                    {
                                        bool isRepeated = false;

                                        for (int j = 0; j < inputDays.Length; j++)
                                        {
                                            if (i != j && inputDays[i] == inputDays[j])
                                            {
                                                isRepeated = true;
                                                break;
                                            }
                                        }

                                        if (!isRepeated)
                                        {
                                            resultDays.Add(inputDays[i]);
                                        }
                                    }
                                    if (resultDays != null)
                                    {
                                        if (resultDays.Count() == 5)
                                        {
                                            GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = FirstDay + "," + SecondDay + "," + ThirdDay + "," + FourthDay + "," + FifthDay });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                var result1 = GetDayWithTeam.GroupBy(x => x.Days).Select(group => new
                {
                    Days = group.Key,
                    Teams = group.ToList()
                });
                foreach (var item in result1)
                {
                    string Days = item.Days;
                    List<int?> Teams = item.Teams.Select(p => p.teamID).Distinct().ToList();
                    result.Add(new GetResultsForTimeSlot { Days = Days, Teams = Teams });
                }
            }
            else if (packCount == 6)
            {
                var objTeams = UhDB.StaffServices.Where(x => x.MainCategory.uID == 1 && x.catID == 1 && x.catsubID == 1 && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objTeam in objTeams)
                {
                    int? teamID = objTeam.teamID;
                    int CountTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountTeams == 0)
                    {
                        List<List<string>> dayBundles = GetDayBundles3();
                        foreach (var bundle in dayBundles)
                        {
                            GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = bundle[0] + "," + bundle[1] + "," + bundle[2] + "," + bundle[3] + "," + bundle[4] + "," + bundle[5] });
                        }
                    }
                    else
                    {
                        var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                       && x.teamID == teamID).Select(p => new { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                        if (objDates != null)
                        {
                            string FirstDay = null, SecondDay = null, ThirdDay = null, FourthDay = null, FifthDay = null, SixthDay = null;
                            List<List<string>> dayBundles = GetDayBundles3();
                            foreach (var bundle in dayBundles)
                            {
                                string dayOfWeek1 = bundle[0];
                                string dayOfWeek2 = bundle[1];
                                string dayOfWeek3 = bundle[2];
                                string dayOfWeek4 = bundle[3];
                                string dayOfWeek5 = bundle[4];
                                string dayOfWeek6 = bundle[5];

                                var filteredDates1 = objDates.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek1).ToList();
                                if (!filteredDates1.Any())
                                {
                                    FirstDay = dayOfWeek1;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates1.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == packID && x.proprestID == propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        FirstDay = dayOfWeek1;
                                    }

                                }

                                var filteredDates2 = objDates.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek2).ToList();
                                if (!filteredDates2.Any())
                                {
                                    SecondDay = dayOfWeek2;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates2.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == packID && x.proprestID == propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        SecondDay = dayOfWeek2;
                                    }

                                }

                                var filteredDates3 = objDates.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek3).ToList();
                                if (!filteredDates3.Any())
                                {
                                    ThirdDay = dayOfWeek3;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates3.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == packID && x.proprestID == propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        ThirdDay = dayOfWeek3;
                                    }

                                }

                                var filteredDates4 = objDates.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek4).ToList();
                                if (!filteredDates4.Any())
                                {
                                    FourthDay = dayOfWeek4;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates4.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == packID && x.proprestID == propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        FourthDay = dayOfWeek4;
                                    }
                                }

                                var filteredDates5 = objDates.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek5).ToList();
                                if (!filteredDates5.Any())
                                {
                                    FifthDay = dayOfWeek5;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates4.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == packID && x.proprestID == propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        FifthDay = dayOfWeek5;
                                    }
                                }

                                var filteredDates6 = objDates.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek6).ToList();
                                if (!filteredDates6.Any())
                                {
                                    SixthDay = dayOfWeek6;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates6.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == packID && x.proprestID == propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        SixthDay = dayOfWeek6;
                                    }
                                }
                                if (FirstDay != null && SecondDay != null && ThirdDay != null && FourthDay != null && FifthDay != null && SixthDay != null)
                                {
                                    string[] inputDays = new string[] { FirstDay, SecondDay, ThirdDay, FourthDay, FifthDay, SixthDay };
                                    List<string> resultDays = new List<string>();

                                    for (int i = 0; i < inputDays.Length; i++)
                                    {
                                        bool isRepeated = false;

                                        for (int j = 0; j < inputDays.Length; j++)
                                        {
                                            if (i != j && inputDays[i] == inputDays[j])
                                            {
                                                isRepeated = true;
                                                break;
                                            }
                                        }

                                        if (!isRepeated)
                                        {
                                            resultDays.Add(inputDays[i]);
                                        }
                                    }
                                    if (resultDays != null)
                                    {
                                        if (resultDays.Count() == 6)
                                        {
                                            GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = FirstDay + "," + SecondDay + "," + ThirdDay + "," + FourthDay + "," + FifthDay + "," + SixthDay });
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
                var result1 = GetDayWithTeam.GroupBy(x => x.Days).Select(group => new
                {
                    Days = group.Key,
                    Teams = group.ToList()
                });
                foreach (var item in result1)
                {
                    string Days = item.Days;
                    List<int?> Teams = item.Teams.Select(p => p.teamID).Distinct().ToList();
                    result.Add(new GetResultsForTimeSlot { Days = Days, Teams = Teams });
                }
            }
            return result;
        }

        public List<GetResultsForTimeSlot> GetResultsForTimeSlots1(ResultsForTimeSlots1 time)
        {
            List<GetResultsForTimeSlot> result = new List<GetResultsForTimeSlot>();
            DateTime TodayDate = DateTime.Now;
            DateTime StartDate = Convert.ToDateTime(time.StartDate);
            string StartDay = StartDate.DayOfWeek.ToString();
            int AddMonth = 0;
            if (time.NoOfMonth == null)
            {
                AddMonth = 1;
            }
            else
            {
                int? NoOfMonthAdd = UhDB.CustomerRenewalMonths.Where(x => x.custrmID == time.NoOfMonth && x.IsActive == true && x.IsDelete == false).FirstOrDefault().NoOfMonths;
                AddMonth = Convert.ToInt32(NoOfMonthAdd);

            }
            DateTime LastDate = StartDate.AddMonths(AddMonth);
            List<GetDayWithTeam> GetDayWithTeam = new List<GetDayWithTeam>();

            int? packCount = UhDB.Packages.Where(x => x.packID == time.packID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().RecursiveTime;
            if (packCount == 0 || packCount == 1)
            {
                var objTeams = UhDB.StaffServices.Where(x => x.MainCategory.uID == 1 && x.catID == 1 && x.catsubID == 1 && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objTeam in objTeams)
                {
                    int? teamID = objTeam.teamID;
                    int CountTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountTeams == 0)
                    {
                        string Day = StartDate.ToString("dddd");
                        for (var i = 1; i <= 7; i++)
                        {
                            DateTime checkDate = StartDate.AddDays(i);
                            string dayOfWeek = checkDate.DayOfWeek.ToString();
                            if (dayOfWeek == StartDay)
                            {
                                GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = dayOfWeek });
                            }
                        }
                    }
                    else
                    {
                        List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
                        string Day = StartDate.ToString("dddd");
                        var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == time.catID && x.CustomerOfficalDetail.catsubID == time.catsubID
                                       && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate) &&
                                       EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                       && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                        var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.CustomerOfficalDetail.catID == time.catID && x.CustomerOfficalDetail.catsubID == time.catsubID
                                            && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate) &&
                                            EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                            && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                        if (objDates != null)
                        {
                            if (objDates.Count()!=0) 
                            {
                                tempData.AddRange(objDates);
                            }
                            
                        }
                        if (objBlockDates != null)
                        {
                            if (objBlockDates.Count()!=0) 
                            {
                                tempData.AddRange(objBlockDates);
                            }
                            
                        }
                        tempData = tempData.OrderBy(x => x.StartDate).ToList();
                        if (tempData != null)
                        {
                            for (int i = 1; i <= 7; i++)
                            {
                                DateTime checkDate = StartDate;
                                string dayOfWeek = checkDate.DayOfWeek.ToString();
                                var filteredDates = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek).ToList();
                                if (!filteredDates.Any())
                                {
                                    if (dayOfWeek == StartDay)
                                    {
                                        GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = dayOfWeek });
                                    }
                                    StartDate = StartDate.AddDays(1);
                                }
                                else
                                {
                                    if (dayOfWeek == StartDay)
                                    {
                                        bool isDayAvailable = true;
                                        var groupedDates = filteredDates.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        List<TimeRange> StartDateTimes = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);

                                        }
                                        if (StartDateTimes.Count != 0)
                                        {
                                            ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                        }
                                        if (time.Time != null)
                                        {
                                            TimeSpan B1 = ConvertToTimeSpans(time.Time);
                                            ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                        }
                                        if (ResultimeRange != null)
                                        {
                                            if (ResultimeRange.Count() != 0)
                                            {
                                                GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = dayOfWeek });
                                            }
                                            StartDate = StartDate.AddDays(1);
                                        }
                                        else
                                        {
                                            StartDate = StartDate.AddDays(1);
                                        }
                                    }
                                    else
                                    {
                                        StartDate = StartDate.AddDays(1);
                                    }
                                }
                            }

                        }
                    }
                }
                var result1 = GetDayWithTeam.GroupBy(x => x.Days).Select(group => new
                {
                    Days = group.Key,
                    Teams = group.ToList()
                });
                foreach (var item in result1)
                {
                    string Days = item.Days;
                    List<int?> Teams = item.Teams.Select(p => p.teamID).Distinct().ToList();
                    result.Add(new GetResultsForTimeSlot { Days = Days, Teams = Teams });
                }
            }
            else if (packCount == 2)
            {
                var objTeams = UhDB.StaffServices.Where(x => x.MainCategory.uID == 1 && x.catID == 1 && x.catsubID == 1 && x.IsActive == true && x.IsDelete == false).ToList();
                int length = 8 * objTeams.Count();
                foreach (var objTeam in objTeams)
                {
                    int? teamID = objTeam.teamID;
                    int CountTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountTeams == 0)
                    {
                        List<string> Days = GetTwoTimesAWeek();
                        foreach (var Day in Days)
                        {
                            if (Day.Contains(StartDay))
                            {
                                string[] stringArray = Day.Split(',');
                                GetDayWithTeam.Add(new GetDayWithTeam { teamID = teamID, Days = stringArray[0] + "," + stringArray[1] });
                            }
                        }
                    }
                    else
                    {
                        List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
                        string Day = StartDate.ToString("dddd");
                        var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == time.catID && x.CustomerOfficalDetail.catsubID == time.catsubID
                                       && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                       && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                       && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                        var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.CustomerOfficalDetail.catID == time.catID && x.CustomerOfficalDetail.catsubID == time.catsubID
                                       && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                       && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                       && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                        if (objDates != null)
                        {
                            if (objDates.Count()!=0) 
                            {
                                tempData.AddRange(objDates);
                            }
                        }
                        if (objBlockDates != null)
                        {
                            if (objBlockDates.Count()!=0) 
                            {
                                tempData.AddRange(objBlockDates);
                            }
                        }
                        tempData = tempData.OrderBy(x => x.StartDate).ToList();

                        if (tempData != null)
                        {
                            int daysToAdd = 0;
                            int CountOFAdd = 0;
                            List<string> Days = GetTwoTimesAWeek();
                            foreach (var InDay in Days)
                            {
                                if (InDay.Contains(StartDay))
                                {
                                    if (InDay.Contains(","))
                                    {
                                        string[] stringArray = InDay.Split(',');
                                        string dayOfWeek1 = stringArray[0];
                                        string dayOfWeek2 = stringArray[1];
                                        string FirstDay = null, SecondDay = null;
                                        var filteredDates1 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek1).ToList();
                                        if (!filteredDates1.Any())
                                        {
                                            FirstDay = dayOfWeek1;
                                        }
                                        else
                                        {
                                            bool isDayAvailable = true;
                                            var groupedDates = filteredDates1.GroupBy(date => date.StartDate).Select(group => new
                                            {
                                                StartDate = group.Key,
                                                Events = group.ToList()
                                            });
                                            List<TimeRange> ResultimeRange = new List<TimeRange>();
                                            List<TimeRange> StartDateTimes = new List<TimeRange>();
                                            foreach (var group in groupedDates)
                                            {
                                                List<TimeRange> expectTimes = new List<TimeRange>();
                                                foreach (var eventDetail in group.Events)
                                                {
                                                    expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                    StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                }
                                                var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                                string TimeMeasurement = objTimeRange.TimeMeasurement;
                                                int Time = 0;
                                                if (TimeMeasurement == "Hours")
                                                {
                                                    Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                                }
                                                Time = Convert.ToInt32(objTimeRange.Duration);
                                                TimeRange originalRange = new TimeRange();
                                                originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                                originalRange.End = ConvertToTimeSpans("06:00 PM");
                                                List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                                ResultimeRange.AddRange(timeRange);
                                            }
                                            if (StartDateTimes.Count != 0)
                                            {
                                                ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                            }
                                            if (time.Time != null)
                                            {
                                                if (dayOfWeek1 == StartDay)
                                                {
                                                    TimeSpan B1 = ConvertToTimeSpans(time.Time);
                                                    ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                                }
                                            }
                                            if (ResultimeRange != null)
                                            {
                                                if (ResultimeRange.Count() != 0)
                                                {
                                                    FirstDay = dayOfWeek1;
                                                }
                                            }
                                        }
                                        var filteredDates2 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek2).ToList();
                                        if (!filteredDates2.Any())
                                        {
                                            SecondDay = dayOfWeek2;
                                        }
                                        else
                                        {
                                            bool isDayAvailable = true;
                                            var groupedDates = filteredDates2.GroupBy(date => date.StartDate).Select(group => new
                                            {
                                                StartDate = group.Key,
                                                Events = group.ToList()
                                            });
                                            List<TimeRange> ResultimeRange = new List<TimeRange>();
                                            List<TimeRange> StartDateTimes = new List<TimeRange>();
                                            foreach (var group in groupedDates)
                                            {
                                                List<TimeRange> expectTimes = new List<TimeRange>();
                                                foreach (var eventDetail in group.Events)
                                                {
                                                    expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                    StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                }
                                                var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                                string TimeMeasurement = objTimeRange.TimeMeasurement;
                                                int Time = 0;
                                                if (TimeMeasurement == "Hours")
                                                {
                                                    Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                                }
                                                else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                                TimeRange originalRange = new TimeRange();
                                                originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                                originalRange.End = ConvertToTimeSpans("06:00 PM");
                                                List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                                ResultimeRange.AddRange(timeRange);
                                            }
                                            if (StartDateTimes.Count != 0)
                                            {
                                                ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                            }
                                            if (time.Time != null)
                                            {
                                                if (dayOfWeek2 == StartDay)
                                                {
                                                    TimeSpan B1 = ConvertToTimeSpans(time.Time);
                                                    ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                                }
                                            }
                                            if (ResultimeRange != null)
                                            {
                                                if (ResultimeRange.Count() != 0)
                                                {
                                                    SecondDay = dayOfWeek2;
                                                }
                                            }
                                        }
                                        if (FirstDay != null && SecondDay != null)
                                        {
                                            GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = FirstDay + "," + SecondDay });
                                            daysToAdd += 4;
                                            CountOFAdd += 1;
                                        }
                                        else
                                        {
                                            daysToAdd += 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                var result1 = GetDayWithTeam.GroupBy(x => x.Days).Select(group => new
                {
                    Days = group.Key,
                    Teams = group.ToList()
                });
                foreach (var item in result1)
                {
                    string Days = item.Days;
                    List<int?> Teams = item.Teams.Select(p => p.teamID).Distinct().ToList();
                    result.Add(new GetResultsForTimeSlot { Days = Days, Teams = Teams });
                }
            }
            else if (packCount == 3)
            {
                var objTeams = UhDB.StaffServices.Where(x => x.MainCategory.uID == 1 && x.catID == 1 && x.catsubID == 1 && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objTeam in objTeams)
                {
                    int? teamID = objTeam.teamID;
                    int CountTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountTeams == 0)
                    {
                        List<string> dayBundles = GetThreeTimesAWeek();
                        foreach (var bundle in dayBundles)
                        {
                            if (bundle.Contains(StartDay))
                            {
                                string[] stringArray = bundle.Split(',');
                                GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = stringArray[0] + "," + stringArray[1] + "," + stringArray[2] });
                            }
                        }
                    }
                    else
                    {
                        List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
                        var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == time.catID && x.CustomerOfficalDetail.catsubID == time.catsubID
                                       && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                       && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                       && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                        var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.CustomerOfficalDetail.catID == time.catID && x.CustomerOfficalDetail.catsubID == time.catsubID
                                            && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                            && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                            && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                        if (objDates != null)
                        {
                            if (objDates.Count()!=0) 
                            {
                                tempData.AddRange(objDates);
                            }
                        }
                        if (objBlockDates != null)
                        {
                            if (objBlockDates.Count()!=0) 
                            {
                                tempData.AddRange(objBlockDates);
                            }
                        }
                        tempData = tempData.OrderBy(x => x.StartDate).ToList();
                        if (tempData != null)
                        {

                            List<string> dayBundles = GetThreeTimesAWeek();
                            foreach (var bundle in dayBundles)
                            {
                                if (bundle.Contains(StartDay))
                                {
                                    string FirstDay = null, SecondDay = null, ThirdDay = null;
                                    string[] stringArray = bundle.Split(',');
                                    string dayOfWeek1 = stringArray[0];
                                    string dayOfWeek2 = stringArray[1];
                                    string dayOfWeek3 = stringArray[2];
                                    var filteredDates1 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek1).ToList();
                                    if (!filteredDates1.Any())
                                    {
                                        FirstDay = dayOfWeek1;
                                    }
                                    else
                                    {
                                        bool isDayAvailable = true;
                                        var groupedDates = filteredDates1.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        List<TimeRange> StartDateTimes = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        if (StartDateTimes.Count != 0)
                                        {
                                            ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                        }
                                        if (time.Time != null)
                                        {
                                            if (dayOfWeek1 == StartDay)
                                            {
                                                TimeSpan B1 = ConvertToTimeSpans(time.Time);
                                                ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                            }
                                        }
                                        if (ResultimeRange != null)
                                        {
                                            if (ResultimeRange.Count() != 0)
                                            {
                                                FirstDay = dayOfWeek1;
                                            }
                                        }
                                    }

                                    var filteredDates2 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek2).ToList();
                                    if (!filteredDates2.Any())
                                    {
                                        SecondDay = dayOfWeek2;
                                    }
                                    else
                                    {
                                        bool isDayAvailable = true;
                                        var groupedDates = filteredDates2.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        List<TimeRange> StartDateTimes = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        if (StartDateTimes.Count != 0)
                                        {
                                            ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                        }
                                        if (time.Time != null)
                                        {
                                            if (dayOfWeek2 == StartDay)
                                            {
                                                TimeSpan B1 = ConvertToTimeSpans(time.Time);
                                                ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                            }
                                        }
                                        if (ResultimeRange != null)
                                        {
                                            if (ResultimeRange.Count() != 0)
                                            {
                                                SecondDay = dayOfWeek2;
                                            }
                                        }

                                    }

                                    var filteredDates3 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek3).ToList();
                                    if (!filteredDates3.Any())
                                    {
                                        ThirdDay = dayOfWeek3;
                                    }
                                    else
                                    {
                                        bool isDayAvailable = true;
                                        var groupedDates = filteredDates3.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        List<TimeRange> StartDateTimes = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        if (StartDateTimes.Count != 0)
                                        {
                                            ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                        }
                                        if (time.Time != null)
                                        {
                                            if (dayOfWeek3 == StartDay)
                                            {
                                                TimeSpan B1 = ConvertToTimeSpans(time.Time);
                                                ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                            }
                                        }
                                        if (ResultimeRange != null)
                                        {
                                            if (ResultimeRange.Count() != 0)
                                            {
                                                ThirdDay = dayOfWeek3;
                                            }
                                        }
                                    }
                                    if (FirstDay != null && SecondDay != null && ThirdDay != null)
                                    {
                                        string[] inputDays = new string[] { FirstDay, SecondDay, ThirdDay };
                                        List<string> resultDays = new List<string>();

                                        for (int i = 0; i < inputDays.Length; i++)
                                        {
                                            bool isRepeated = false;

                                            for (int j = 0; j < inputDays.Length; j++)
                                            {
                                                if (i != j && inputDays[i] == inputDays[j])
                                                {
                                                    isRepeated = true;
                                                    break;
                                                }
                                            }

                                            if (!isRepeated)
                                            {
                                                resultDays.Add(inputDays[i]);
                                            }
                                        }

                                        if (resultDays != null)
                                        {
                                            if (resultDays.Count() == 3)
                                            {
                                                GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = FirstDay + "," + SecondDay + "," + ThirdDay });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                var result1 = GetDayWithTeam.GroupBy(x => x.Days).Select(group => new
                {
                    Days = group.Key,
                    Teams = group.ToList()
                });
                foreach (var item in result1)
                {
                    string Days = item.Days;
                    List<int?> Teams = item.Teams.Select(p => p.teamID).Distinct().ToList();
                    result.Add(new GetResultsForTimeSlot { Days = Days, Teams = Teams });
                }
            }
            else if (packCount == 4)
            {
                var objTeams = UhDB.StaffServices.Where(x => x.MainCategory.uID == 1 && x.catID == 1 && x.catsubID == 1 && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objTeam in objTeams)
                {
                    int? teamID = objTeam.teamID;
                    int CountTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountTeams == 0)
                    {
                        List<string> dayBundles = GetFourTimesAWeek();
                        foreach (var bundle in dayBundles)
                        {
                            if (bundle.Contains(StartDay))
                            {
                                string[] stringArray = bundle.Split(',');
                                GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = stringArray[0] + "," + stringArray[1] + "," + stringArray[2] + "," + stringArray[3] });
                            }

                        }
                    }
                    else
                    {
                        List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
                        var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == time.catID && x.CustomerOfficalDetail.catsubID == time.catsubID
                                       && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                       && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                       && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                        var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.CustomerOfficalDetail.catID == time.catID && x.CustomerOfficalDetail.catsubID == time.catsubID
                                            && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                            && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                            && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                        if (objDates != null)
                        {
                            if (objDates.Count()!=0) 
                            {
                                tempData.AddRange(objDates);
                            }
                        }
                        if (objBlockDates != null)
                        {
                            if (objBlockDates.Count()!=0) 
                            {
                                tempData.AddRange(objBlockDates);
                            }
                        }
                        tempData = tempData.OrderBy(x => x.StartDate).ToList();
                        if (tempData != null)
                        {

                            List<string> dayBundles = GetFourTimesAWeek();
                            foreach (var bundle in dayBundles)
                            {
                                if (bundle.Contains(StartDay))
                                {
                                    string FirstDay = null, SecondDay = null, ThirdDay = null, FourthDay = null;
                                    string[] stringArray = bundle.Split(',');
                                    string dayOfWeek1 = stringArray[0];
                                    string dayOfWeek2 = stringArray[1];
                                    string dayOfWeek3 = stringArray[2];
                                    string dayOfWeek4 = stringArray[3];

                                    var filteredDates1 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek1).ToList();
                                    if (!filteredDates1.Any())
                                    {
                                        FirstDay = dayOfWeek1;
                                    }
                                    else
                                    {
                                        bool isDayAvailable = true;
                                        var groupedDates = filteredDates1.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        List<TimeRange> StartDateTimes = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            }
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        if (StartDateTimes.Count != 0)
                                        {
                                            ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                        }
                                        if (time.Time != null)
                                        {
                                            if (dayOfWeek1 == StartDay)
                                            {
                                                TimeSpan B1 = ConvertToTimeSpans(time.Time);
                                                ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                            }
                                        }
                                        if (ResultimeRange != null)
                                        {
                                            if (ResultimeRange.Count() != 0)
                                            {
                                                FirstDay = dayOfWeek1;
                                            }

                                        }

                                    }

                                    var filteredDates2 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek2).ToList();
                                    if (!filteredDates2.Any())
                                    {
                                        SecondDay = dayOfWeek2;
                                    }
                                    else
                                    {
                                        bool isDayAvailable = true;
                                        var groupedDates = filteredDates2.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        List<TimeRange> StartDateTimes = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        if (StartDateTimes.Count != 0)
                                        {
                                            ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                        }
                                        if (time.Time != null)
                                        {
                                            if (dayOfWeek2 == StartDay)
                                            {
                                                TimeSpan B1 = ConvertToTimeSpans(time.Time);
                                                ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                            }
                                        }
                                        if (ResultimeRange != null)
                                        {
                                            if (ResultimeRange.Count() != 0)
                                            {
                                                SecondDay = dayOfWeek2;
                                            }
                                        }
                                    }

                                    var filteredDates3 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek3).ToList();
                                    if (!filteredDates3.Any())
                                    {
                                        ThirdDay = dayOfWeek3;
                                    }
                                    else
                                    {
                                        bool isDayAvailable = true;
                                        var groupedDates = filteredDates3.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        List<TimeRange> StartDateTimes = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        if (StartDateTimes.Count != 0)
                                        {
                                            ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                        }
                                        if (time.Time != null)
                                        {
                                            if (dayOfWeek3 == StartDay)
                                            {
                                                TimeSpan B1 = ConvertToTimeSpans(time.Time);
                                                ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                            }
                                        }

                                        if (ResultimeRange != null)
                                        {
                                            if (ResultimeRange.Count() != 0)
                                            {
                                                ThirdDay = dayOfWeek3;
                                            }
                                        }

                                    }

                                    var filteredDates4 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek4).ToList();
                                    if (!filteredDates4.Any())
                                    {
                                        FourthDay = dayOfWeek4;
                                    }
                                    else
                                    {
                                        bool isDayAvailable = true;
                                        var groupedDates = filteredDates4.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        List<TimeRange> StartDateTimes = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        if (StartDateTimes.Count != 0)
                                        {
                                            ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                        }
                                        if (time.Time != null)
                                        {
                                            if (dayOfWeek4 == StartDay)
                                            {
                                                TimeSpan B1 = ConvertToTimeSpans(time.Time);
                                                ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                            }
                                        }

                                        if (ResultimeRange != null)
                                        {
                                            if (ResultimeRange.Count() != 0)
                                            {
                                                FourthDay = dayOfWeek4;
                                            }
                                        }
                                    }
                                    if (FirstDay != null && SecondDay != null && ThirdDay != null && FourthDay != null)
                                    {
                                        string[] inputDays = new string[] { FirstDay, SecondDay, ThirdDay, FourthDay };
                                        List<string> resultDays = new List<string>();

                                        for (int i = 0; i < inputDays.Length; i++)
                                        {
                                            bool isRepeated = false;

                                            for (int j = 0; j < inputDays.Length; j++)
                                            {
                                                if (i != j && inputDays[i] == inputDays[j])
                                                {
                                                    isRepeated = true;
                                                    break;
                                                }
                                            }

                                            if (!isRepeated)
                                            {
                                                resultDays.Add(inputDays[i]);
                                            }
                                        }
                                        if (resultDays != null)
                                        {
                                            if (resultDays.Count() == 4)
                                            {
                                                GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = FirstDay + "," + SecondDay + "," + ThirdDay + "," + FourthDay });
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
                var result1 = GetDayWithTeam.GroupBy(x => x.Days).Select(group => new
                {
                    Days = group.Key,
                    Teams = group.ToList()
                });
                foreach (var item in result1)
                {
                    string Days = item.Days;
                    List<int?> Teams = item.Teams.Select(p => p.teamID).Distinct().ToList();
                    result.Add(new GetResultsForTimeSlot { Days = Days, Teams = Teams });
                }
            }
            else if (packCount == 5)
            {
                var objTeams = UhDB.StaffServices.Where(x => x.MainCategory.uID == 1 && x.catID == 1 && x.catsubID == 1 && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objTeam in objTeams)
                {
                    int? teamID = objTeam.teamID;
                    int CountTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountTeams == 0)
                    {
                        List<string> dayBundles = GetFiveTimesAWeek();
                        foreach (var bundle in dayBundles)
                        {
                            if (bundle.Contains(StartDay))
                            {
                                string[] stringArray = bundle.Split(',');
                                GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = stringArray[0] + "," + stringArray[1] + "," + stringArray[2] + "," + stringArray[3] + "," + stringArray[4] });
                            }
                        }
                    }
                    else
                    {
                        List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
                        var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == time.catID && x.CustomerOfficalDetail.catsubID == time.catsubID
                                       && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                       && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                       && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                        var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.CustomerOfficalDetail.catID == time.catID && x.CustomerOfficalDetail.catsubID == time.catsubID
                                            && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                            && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                            && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                        if (objDates != null)
                        {
                            if (objDates.Count()!=0) 
                            {
                                tempData.AddRange(objDates);
                            }
                        }
                        if (objBlockDates != null)
                        {
                            if (objBlockDates.Count()!=0) 
                            {
                                tempData.AddRange(objBlockDates);
                            }
                        }
                        tempData = tempData.OrderBy(x => x.StartDate).ToList();
                        if (tempData != null)
                        {
                            List<string> dayBundles = GetFiveTimesAWeek();
                            foreach (var bundle in dayBundles)
                            {
                                if (bundle.Contains(StartDay))
                                {
                                    string FirstDay = null, SecondDay = null, ThirdDay = null, FourthDay = null, FifthDay = null;
                                    string[] stringArray = bundle.Split(',');
                                    string dayOfWeek1 = stringArray[0];
                                    string dayOfWeek2 = stringArray[1];
                                    string dayOfWeek3 = stringArray[2];
                                    string dayOfWeek4 = stringArray[3];
                                    string dayOfWeek5 = stringArray[4];

                                    var filteredDates1 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek1).ToList();
                                    if (!filteredDates1.Any())
                                    {
                                        FirstDay = dayOfWeek1;
                                    }
                                    else
                                    {
                                        bool isDayAvailable = true;
                                        var groupedDates = filteredDates1.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        List<TimeRange> StartDateTimes = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        if (StartDateTimes.Count != 0)
                                        {
                                            ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                        }
                                        if (time.Time != null)
                                        {
                                            if (dayOfWeek1 == StartDay)
                                            {
                                                TimeSpan B1 = ConvertToTimeSpans(time.Time);
                                                ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                            }
                                        }
                                        if (ResultimeRange != null)
                                        {
                                            if (ResultimeRange.Count() != 0)
                                            {
                                                FirstDay = dayOfWeek1;
                                            }
                                        }

                                    }
                                    var filteredDates2 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek2).ToList();
                                    if (!filteredDates2.Any())
                                    {
                                        SecondDay = dayOfWeek2;
                                    }
                                    else
                                    {
                                        bool isDayAvailable = true;
                                        var groupedDates = filteredDates2.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        List<TimeRange> StartDateTimes = new List<TimeRange>();

                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        if (StartDateTimes.Count != 0)
                                        {
                                            ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                        }
                                        if (time.Time != null)
                                        {
                                            if (dayOfWeek2 == StartDay)
                                            {
                                                TimeSpan B1 = ConvertToTimeSpans(time.Time);
                                                ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                            }
                                        }

                                        if (ResultimeRange != null)
                                        {
                                            if (ResultimeRange.Count() != 0)
                                            {
                                                SecondDay = dayOfWeek2;
                                            }
                                        }

                                    }

                                    var filteredDates3 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek3).ToList();
                                    if (!filteredDates3.Any())
                                    {
                                        ThirdDay = dayOfWeek3;
                                    }
                                    else
                                    {
                                        bool isDayAvailable = true;
                                        var groupedDates = filteredDates3.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        List<TimeRange> StartDateTimes = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        if (StartDateTimes.Count != 0)
                                        {
                                            ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                        }
                                        if (time.Time != null)
                                        {
                                            if (dayOfWeek3 == StartDay)
                                            {
                                                TimeSpan B1 = ConvertToTimeSpans(time.Time);
                                                ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                            }
                                        }

                                        if (ResultimeRange != null)
                                        {
                                            if (ResultimeRange.Count() != 0)
                                            {
                                                ThirdDay = dayOfWeek3;
                                            }
                                        }

                                    }

                                    var filteredDates4 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek4).ToList();
                                    if (!filteredDates4.Any())
                                    {
                                        FourthDay = dayOfWeek4;
                                    }
                                    else
                                    {
                                        bool isDayAvailable = true;
                                        var groupedDates = filteredDates4.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        List<TimeRange> StartDateTimes = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        if (StartDateTimes.Count != 0)
                                        {
                                            ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                        }
                                        if (time.Time != null)
                                        {
                                            if (dayOfWeek4 == StartDay)
                                            {
                                                TimeSpan B1 = ConvertToTimeSpans(time.Time);
                                                ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                            }
                                        }

                                        if (ResultimeRange != null)
                                        {
                                            if (ResultimeRange.Count() != 0)
                                            {
                                                FourthDay = dayOfWeek4;
                                            }
                                        }
                                    }

                                    var filteredDates5 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek5).ToList();
                                    if (!filteredDates5.Any())
                                    {
                                        FifthDay = dayOfWeek5;
                                    }
                                    else
                                    {
                                        bool isDayAvailable = true;
                                        var groupedDates = filteredDates5.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        List<TimeRange> StartDateTimes = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        if (StartDateTimes.Count != 0)
                                        {
                                            ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                        }
                                        if (time.Time != null)
                                        {
                                            if (dayOfWeek5 == StartDay)
                                            {
                                                TimeSpan B1 = ConvertToTimeSpans(time.Time);
                                                ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                            }
                                        }

                                        if (ResultimeRange != null)
                                        {
                                            if (ResultimeRange.Count() != 0)
                                            {
                                                FifthDay = dayOfWeek5;
                                            }
                                        }
                                    }

                                    if (FirstDay != null && SecondDay != null && ThirdDay != null && FourthDay != null && FifthDay != null)
                                    {
                                        string[] inputDays = new string[] { FirstDay, SecondDay, ThirdDay, FourthDay, FifthDay };
                                        List<string> resultDays = new List<string>();

                                        for (int i = 0; i < inputDays.Length; i++)
                                        {
                                            bool isRepeated = false;

                                            for (int j = 0; j < inputDays.Length; j++)
                                            {
                                                if (i != j && inputDays[i] == inputDays[j])
                                                {
                                                    isRepeated = true;
                                                    break;
                                                }
                                            }

                                            if (!isRepeated)
                                            {
                                                resultDays.Add(inputDays[i]);
                                            }
                                        }
                                        if (resultDays != null)
                                        {
                                            if (resultDays.Count() == 5)
                                            {
                                                GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = FirstDay + "," + SecondDay + "," + ThirdDay + "," + FourthDay + "," + FifthDay });
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                var result1 = GetDayWithTeam.GroupBy(x => x.Days).Select(group => new
                {
                    Days = group.Key,
                    Teams = group.ToList()
                });
                foreach (var item in result1)
                {
                    string Days = item.Days;
                    List<int?> Teams = item.Teams.Select(p => p.teamID).Distinct().ToList();
                    result.Add(new GetResultsForTimeSlot { Days = Days, Teams = Teams });
                }
            }
            else if (packCount == 6)
            {
                var objTeams = UhDB.StaffServices.Where(x => x.MainCategory.uID == 1 && x.catID == 1 && x.catsubID == 1 && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objTeam in objTeams)
                {
                    int? teamID = objTeam.teamID;
                    int CountTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountTeams == 0)
                    {
                        List<List<string>> dayBundles = GetDayBundles3();
                        foreach (var bundle in dayBundles)
                        {
                            if (bundle.Contains(StartDay))
                            {
                                GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = bundle[0] + "," + bundle[1] + "," + bundle[2] + "," + bundle[3] + "," + bundle[4] + "," + bundle[5] });
                            }
                        }
                    }
                    else
                    {
                        List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
                        var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == time.catID && x.CustomerOfficalDetail.catsubID == time.catsubID
                                       && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                       && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                       && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                        var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.CustomerOfficalDetail.catID == time.catID && x.CustomerOfficalDetail.catsubID == time.catsubID
                                            && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                            && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                            && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                        if (objDates != null)
                        {
                            if (objDates.Count()!=0) 
                            {
                                tempData.AddRange(objDates);
                            }
                            
                        }
                        if (objBlockDates != null)
                        {
                            if (objBlockDates.Count()!=0) 
                            {
                                tempData.AddRange(objBlockDates);
                            }
                        }
                        tempData = tempData.OrderBy(x => x.StartDate).ToList();
                        if (tempData != null)
                        {
                            List<List<string>> dayBundles = GetDayBundles3();
                            foreach (var bundle in dayBundles)
                            {
                                if (bundle.Contains(StartDay))
                                {
                                    string FirstDay = null, SecondDay = null, ThirdDay = null, FourthDay = null, FifthDay = null, SixthDay = null;
                                    string dayOfWeek1 = bundle[0];
                                    string dayOfWeek2 = bundle[1];
                                    string dayOfWeek3 = bundle[2];
                                    string dayOfWeek4 = bundle[3];
                                    string dayOfWeek5 = bundle[4];
                                    string dayOfWeek6 = bundle[5];

                                    var filteredDates1 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek1).ToList();
                                    if (!filteredDates1.Any())
                                    {
                                        FirstDay = dayOfWeek1;
                                    }
                                    else
                                    {
                                        bool isDayAvailable = true;
                                        var groupedDates = filteredDates1.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        List<TimeRange> StartDateTimes = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        if (StartDateTimes.Count != 0)
                                        {
                                            ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                        }
                                        if (time.Time != null)
                                        {
                                            if (dayOfWeek1 == StartDay)
                                            {
                                                TimeSpan B1 = ConvertToTimeSpans(time.Time);
                                                ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                            }
                                        }
                                        if (ResultimeRange != null)
                                        {
                                            if (ResultimeRange.Count() != 0)
                                            {
                                                FirstDay = dayOfWeek1;
                                            }
                                        }

                                    }

                                    var filteredDates2 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek2).ToList();
                                    if (!filteredDates2.Any())
                                    {
                                        SecondDay = dayOfWeek2;
                                    }
                                    else
                                    {
                                        bool isDayAvailable = true;
                                        var groupedDates = filteredDates2.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        List<TimeRange> StartDateTimes = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        if (StartDateTimes.Count != 0)
                                        {
                                            ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                        }
                                        if (time.Time != null)
                                        {
                                            if (dayOfWeek2 == StartDay)
                                            {
                                                TimeSpan B1 = ConvertToTimeSpans(time.Time);
                                                ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                            }
                                        }

                                        if (ResultimeRange != null)
                                        {
                                            if (ResultimeRange.Count() != 0)
                                            {
                                                SecondDay = dayOfWeek2;
                                            }
                                        }

                                    }

                                    var filteredDates3 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek3).ToList();
                                    if (!filteredDates3.Any())
                                    {
                                        ThirdDay = dayOfWeek3;
                                    }
                                    else
                                    {
                                        bool isDayAvailable = true;
                                        var groupedDates = filteredDates3.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        List<TimeRange> StartDateTimes = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        if (StartDateTimes.Count != 0)
                                        {
                                            ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                        }
                                        if (time.Time != null)
                                        {
                                            if (dayOfWeek3 == StartDay)
                                            {
                                                TimeSpan B1 = ConvertToTimeSpans(time.Time);
                                                ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                            }
                                        }

                                        if (ResultimeRange != null)
                                        {
                                            if (ResultimeRange.Count() != 0)
                                            {
                                                ThirdDay = dayOfWeek3;
                                            }
                                        }

                                    }

                                    var filteredDates4 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek4).ToList();
                                    if (!filteredDates4.Any())
                                    {
                                        FourthDay = dayOfWeek4;
                                    }
                                    else
                                    {
                                        bool isDayAvailable = true;
                                        var groupedDates = filteredDates4.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        List<TimeRange> StartDateTimes = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        if (StartDateTimes.Count != 0)
                                        {
                                            ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                        }
                                        if (time.Time != null)
                                        {
                                            if (dayOfWeek4 == StartDay)
                                            {
                                                TimeSpan B1 = ConvertToTimeSpans(time.Time);
                                                ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                            }
                                        }

                                        if (ResultimeRange != null)
                                        {
                                            if (ResultimeRange.Count() != 0)
                                            {
                                                FourthDay = dayOfWeek4;
                                            }
                                        }
                                    }

                                    var filteredDates5 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek5).ToList();
                                    if (!filteredDates5.Any())
                                    {
                                        FifthDay = dayOfWeek5;
                                    }
                                    else
                                    {
                                        bool isDayAvailable = true;
                                        var groupedDates = filteredDates5.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        List<TimeRange> StartDateTimes = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        if (StartDateTimes.Count != 0)
                                        {
                                            ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                        }
                                        if (time.Time != null)
                                        {
                                            if (dayOfWeek5 == StartDay)
                                            {
                                                TimeSpan B1 = ConvertToTimeSpans(time.Time);
                                                ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                            }
                                        }
                                        if (ResultimeRange != null)
                                        {
                                            if (ResultimeRange.Count() != 0)
                                            {
                                                FifthDay = dayOfWeek5;
                                            }
                                        }
                                    }

                                    var filteredDates6 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek6).ToList();
                                    if (!filteredDates6.Any())
                                    {
                                        SixthDay = dayOfWeek6;
                                    }
                                    else
                                    {
                                        bool isDayAvailable = true;
                                        var groupedDates = filteredDates6.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        List<TimeRange> StartDateTimes = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        if (StartDateTimes.Count != 0)
                                        {
                                            ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                        }
                                        if (time.Time != null)
                                        {
                                            if (dayOfWeek6 == StartDay)
                                            {
                                                TimeSpan B1 = ConvertToTimeSpans(time.Time);
                                                ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                            }
                                        }

                                        if (ResultimeRange != null)
                                        {
                                            if (ResultimeRange.Count() != 0)
                                            {
                                                SixthDay = dayOfWeek6;
                                            }
                                        }
                                    }

                                    if (FirstDay != null && SecondDay != null && ThirdDay != null && FourthDay != null && FifthDay != null && SixthDay != null)
                                    {
                                        string[] inputDays = new string[] { FirstDay, SecondDay, ThirdDay, FourthDay, FifthDay, SixthDay };
                                        List<string> resultDays = new List<string>();

                                        for (int i = 0; i < inputDays.Length; i++)
                                        {
                                            bool isRepeated = false;

                                            for (int j = 0; j < inputDays.Length; j++)
                                            {
                                                if (i != j && inputDays[i] == inputDays[j])
                                                {
                                                    isRepeated = true;
                                                    break;
                                                }
                                            }

                                            if (!isRepeated)
                                            {
                                                resultDays.Add(inputDays[i]);
                                            }
                                        }
                                        if (resultDays != null)
                                        {
                                            if (resultDays.Count() == 6)
                                            {
                                                GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = FirstDay + "," + SecondDay + "," + ThirdDay + "," + FourthDay + "," + FifthDay + "," + SixthDay });
                                            }
                                        }
                                    }
                                }

                            }
                        }

                    }
                }
                var result1 = GetDayWithTeam.GroupBy(x => x.Days).Select(group => new
                {
                    Days = group.Key,
                    Teams = group.ToList()
                });
                foreach (var item in result1)
                {
                    string Days = item.Days;
                    List<int?> Teams = item.Teams.Select(p => p.teamID).Distinct().ToList();
                    result.Add(new GetResultsForTimeSlot { Days = Days, Teams = Teams });
                }
            }
            return result;
        }

        public static List<List<string>> GetDayBundles()
        {
            string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            List<List<string>> sets = new List<List<string>>();
            for (int i = 0; i < daysOfWeek.Length; i++)
            {
                for (int j = i + 2; j < daysOfWeek.Length; j++)
                {
                    for (int k = j + 2; k < daysOfWeek.Length; k++)
                    {
                        List<string> set = new List<string> { daysOfWeek[i], daysOfWeek[j], daysOfWeek[k] };
                        sets.Add(set);
                    }
                }
            }


            return sets;
        }

        public static List<List<string>> GetDayBundles1()
        {
            // Days of the week
            string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            List<List<string>> sets = new List<List<string>>();

            for (int i = 0; i < daysOfWeek.Length; i++)
            {
                for (int j = i + 1; j < daysOfWeek.Length; j++)
                {
                    for (int k = j + 1; k < daysOfWeek.Length; k++)
                    {
                        for (int l = k + 1; l < daysOfWeek.Length; l++)
                        {
                            // Create the set of four days
                            List<string> set = new List<string> { daysOfWeek[i], daysOfWeek[j], daysOfWeek[k], daysOfWeek[l] };

                            // Check if the set meets the criteria
                            if (IsValidSet(i, j, k, l))
                            {
                                sets.Add(set);
                            }
                        }
                    }
                }
            }

            return sets;
        }

        public static bool IsValidSet(int i, int j, int k, int l)
        {
            if (l == k + 1 && k == j + 1 && j == i + 1)
                return false;

            // Check for exactly one pair of consecutive days
            int consecutivePairs = 0;
            if (j == i + 1) consecutivePairs++;
            if (k == j + 1) consecutivePairs++;
            if (l == k + 1) consecutivePairs++;

            // The set is valid if there is exactly one pair of consecutive days
            // and it does not have three consecutive days
            return consecutivePairs == 1;
        }

        public static List<List<string>> GetDayBundles2()
        {
            string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            List<List<string>> sets = new List<List<string>>();
            for (int i = 0; i < daysOfWeek.Length; i++)
            {
                for (int j = i + 1; j < daysOfWeek.Length; j++)
                {
                    for (int k = j + 1; k < daysOfWeek.Length; k++)
                    {
                        for (int l = k + 1; l < daysOfWeek.Length; l++)
                        {
                            for (int m = l + 1; m < daysOfWeek.Length; m++)
                            {
                                // Check if the set meets the criteria
                                if (IsValidSet(new[] { i, j, k, l, m }))
                                {
                                    sets.Add(new List<string> { daysOfWeek[i], daysOfWeek[j], daysOfWeek[k], daysOfWeek[l], daysOfWeek[m] });
                                }
                            }
                        }
                    }
                }
            }

            return sets;
        }

        static bool IsValidSet(int[] indices)
        {
            // Check for at least one day apart between any two non-consecutive days
            for (int i = 1; i < indices.Length; i++)
            {
                if (indices[i] == indices[i - 1] + 1) // Consecutive days
                    continue;
                if (indices[i] > indices[i - 1] + 1) // At least one day apart
                    break;
                return false;
            }

            // Check for no more than three consecutive days in both linear and cyclic forms
            return !HasMoreThanThreeConsecutiveDays(indices);
        }

        static bool HasMoreThanThreeConsecutiveDays(int[] indices)
        {
            // Linear check
            for (int i = 0; i <= indices.Length - 3; i++)
            {
                if (indices[i + 1] == indices[i] + 1 &&
                    indices[i + 2] == indices[i] + 2)
                {
                    if (i + 3 < indices.Length && indices[i + 3] == indices[i] + 3)
                        return true; // More than 3 consecutive days
                }
            }

            // Cyclic check (handle Sunday -> Monday wrap-around)
            int daysInWeek = 7;
            if (indices[0] == 0 && indices[indices.Length - 1] == daysInWeek - 1)
            {
                // Check cyclically connecting the last and first days
                int consecutive = 1;
                for (int i = 1; i < indices.Length; i++)
                {
                    if (indices[i] == indices[i - 1] + 1)
                        consecutive++;
                    else
                        consecutive = 1;

                    if (consecutive > 3)
                        return true;
                }

                // Wrap-around case (e.g., Saturday, Sunday, Monday)
                if (indices[1] == 1 && indices[indices.Length - 2] == daysInWeek - 2)
                    return true;
            }

            return false;
        }

        public static List<List<string>> GetDayBundles3()
        {

            List<List<string>> sets = new List<List<string>>();
            List<string> days = new List<string>
            {
                "Monday, Tuesday, Wednesday, Thursday, Friday, Sunday",
                "Monday, Tuesday, Wednesday, Thursday, Saturday, Sunday",
                "Monday, Tuesday, Wednesday, Friday, Saturday, Sunday",
                "Monday, Tuesday, Thursday, Friday, Saturday, Sunday",
                "Monday, Wednesday, Thursday, Friday, Saturday, Sunday",
                "Tuesday, Wednesday, Thursday, Friday, Saturday, Monday",
                "Wednesday, Thursday, Friday, Saturday, Sunday, Tuesday"
            };

            sets = GetDaysList(days);
            return sets;
        }

        public static List<List<string>> GetDaysList(List<string> days)
        {
            List<List<string>> result = new List<List<string>>();

            foreach (var dayString in days)
            {
                // Split the string by ", " to get individual days
                var dayList = new List<string>(dayString.Split(new[] { ", " }, StringSplitOptions.None));
                result.Add(dayList);
            }

            return result;
        }

        public static List<string> GetBundles4()
        {
            // Days of the week represented as an array
            string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

            // List to store valid combinations as formatted strings
            List<string> validCombinations = new List<string>();

            // Loop through the days to find combinations with at least a 3-day gap
            for (int i = 0; i < daysOfWeek.Length; i++)
            {
                // Use modulo to wrap around the array when the index goes out of bounds
                int secondDayIndex = (i + 4) % daysOfWeek.Length;
                string combination = $"{daysOfWeek[i]},{daysOfWeek[secondDayIndex]}";
                validCombinations.Add(combination);
            }

            return validCombinations;
        }

        static List<List<string>> GenerateCombinations(List<string> days)
        {
            var combinations = new List<List<string>>();
            int totalDays = days.Count;

            // Iterate through each day to be considered as "no service"
            for (int i = 1; i < totalDays - 1; i++)
            {
                var combination = new List<string>();

                // Include all days except the "no service" day
                for (int j = 0; j < totalDays; j++)
                {
                    if (j != i)
                    {
                        combination.Add(days[j]);
                    }
                }

                combinations.Add(combination);
            }

            return combinations;
        }

        public List<TimeResult> GetResultByTeam(GetResultByTeamModel times)
        {
            List<TimeResult> result = new List<TimeResult>();
            DateTime TodayDate = DateTime.Now;
            //            DateTime StartDate = TodayDate.AddHours(24);
            //          DateTime LastDate = StartDate.AddDays(30);

            DateTime StartDate = Convert.ToDateTime(times.StartDate);
            int AddMonth = 0;
            if (times.NoOfMonth == null)
            {
                AddMonth = 1;
            }
            else
            {
                int? NoOfMonthAdd = UhDB.CustomerRenewalMonths.Where(x => x.custrmID == times.NoOfMonth && x.IsActive == true && x.IsDelete == false).FirstOrDefault().NoOfMonths;
                AddMonth = Convert.ToInt32(NoOfMonthAdd);

            }
            DateTime LastDate = StartDate.AddMonths(AddMonth);
            List<TimeRange> StartDateTimes = new List<TimeRange>();
            List<TeamAndTime> objTeamAndTime = new List<TeamAndTime>();
            List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
            List<string> stringListOfDays = new List<string>();
            string Day = null;
            if (times.teams.Days.Contains(","))
            {
                string[] stringArrayOfDays = times.teams.Days.Split(',');
                Day = stringArrayOfDays[0];
                stringListOfDays.Add(Day);
            }
            else
            {
                stringListOfDays.Add(times.teams.Days);
            }
            List<int?> intArrayofTeams = times.teams.Teams;
            foreach (var stringListOfDay in stringListOfDays)
            {
                Day = stringListOfDay;
                foreach (var Teams in intArrayofTeams)
                {

                    int? teamID = Teams;
                    var objCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                               && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                    var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                               && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                    if (objCustomerTimeLines != null)
                    {
                        if (objCustomerTimeLines.Count() != 0)
                        {
                            tempData.AddRange(objCustomerTimeLines);
                        }

                    }
                    if (objBlockDates != null)
                    {
                        if (objBlockDates.Count() != 0)
                        {
                            tempData.AddRange(objBlockDates);
                        }
                    }
                    tempData = tempData.OrderBy(x => x.StartDate).ToList();
                    if (tempData.Count() != 0)
                    {
                        List<TimeRange> ResultimeRange = new List<TimeRange>();

                        var objFilterDatas = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == Day).ToList();
                        if (!objFilterDatas.Any())
                        {
                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == times.packID && x.proprestID == times.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                            int Time = 0;
                            if (TimeMeasurement == "Hours")
                            {
                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                            }
                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                            TimeRange originalRange = new TimeRange();
                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                            ResultimeRange.AddRange(GetTimeDifference1(originalRange, Time));
                            objTeamAndTime.Add(new TeamAndTime { teamID = teamID, Day = Day, Time = ResultimeRange });
                        }
                        else
                        {
                            var groupedDates = objFilterDatas.GroupBy(date => date.StartDate).Select(group => new
                            {
                                StartDate = group.Key,
                                Events = group.ToList()
                            });

                            foreach (var group in groupedDates)
                            {

                                List<TimeRange> expectTimes = new List<TimeRange>();
                                foreach (var eventDetail in group.Events)
                                {
                                    expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                    StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                }
                                var objTimeRange = UhDB.Pricings.Where(x => x.packID == times.packID && x.proprestID == times.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                string TimeMeasurement = objTimeRange.TimeMeasurement;
                                int Time = 0;
                                if (TimeMeasurement == "Hours")
                                {
                                    Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                }
                                else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                TimeRange originalRange = new TimeRange();
                                originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                originalRange.End = ConvertToTimeSpans("06:00 PM");
                                List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                ResultimeRange.AddRange(timeRange);
                            }
                            if (StartDateTimes.Count != 0)
                            {
                                ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                            }
                            objTeamAndTime.Add(new TeamAndTime { teamID = teamID, Day = Day, Time = ResultimeRange });
                        }
                    }
                    else
                    {
                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == times.packID && x.proprestID == times.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                        int Time = 0;
                        if (TimeMeasurement == "Hours")
                        {
                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                        }
                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                        TimeRange originalRange = new TimeRange();
                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                        ResultimeRange.AddRange(GetTimeDifference1(originalRange, Time));
                        objTeamAndTime.Add(new TeamAndTime { teamID = teamID, Day = Day, Time = ResultimeRange });
                    }
                }

            }

            List<string> T1 = new List<string>();
            foreach (var objTime in objTeamAndTime)
            {

                List<TimeRange> objTimeRanges = objTime.Time;
                int? teamID = objTime.teamID;
                objTimeRanges = objTimeRanges.OrderBy(x => x.Start).ToList();

                //objTimeRanges = objTimeRanges.OrderBy(x => x.End).ToList();
                objTimeRanges = objTimeRanges.Distinct().ToList();
                if (result.Count() != 0)
                {
                    result = result.OrderBy(x => x.Time).ToList();
                }
                foreach (var objTimeRange in objTimeRanges)
                {

                    List<int?> Team = new List<int?>();
                    TimeRange FinalTimeRange = new TimeRange();
                    FinalTimeRange.Start = objTimeRange.Start;
                    FinalTimeRange.End = objTimeRange.End;
                    string FinalTime = objTimeRange.Start.ToString() + "_" + objTimeRange.End.ToString();
                    if (result.Any(x => x.Time == FinalTime))
                    {
                        int index = result.FindIndex(p => p.Time == FinalTime);
                        List<int?> newTeam = new List<int?>();
                        TimeResult result1 = result[index];
                        if (!result1.Teams.Contains(teamID))
                        {
                            newTeam.AddRange(result1.Teams);
                            newTeam.Add(teamID);
                            newTeam = newTeam.Distinct().ToList();
                            TimeResult newResult = new TimeResult();
                            newResult.Time = FinalTime;
                            newResult.Teams = newTeam;
                            newResult.Times = FinalTimeRange;
                            result[index] = newResult;

                        }
                    }
                    else
                    {
                        Team.Add(teamID);

                        result.Add(new TimeResult { Time = FinalTime, Teams = Team, Times = FinalTimeRange });
                    }
                }
            }
            result = result.OrderBy(x => x.Times.Start).ToList();
            DateTime TurncatedTime = DateTime.Now;
            int CountCustomerTimeBlock = UhDB.CustomerTimeBlocks.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(TurncatedTime) && x.IsActive == true && x.IsDelete == false).Count();
            if (CountCustomerTimeBlock != 0)
            {
                var objCustomerTimeBlock = UhDB.CustomerTimeBlocks.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(TurncatedTime) && x.IsActive == true && x.IsDelete == false).AsEnumerable().
                                           Select(p => new { teamID = p.teamID, Days = p.Days, Start = ConvertToTimeSpans(p.StartTime), End = ConvertToTimeSpans(p.EndTime) }).ToList();
                if (objCustomerTimeBlock != null)
                {
                    foreach (var item in result)
                    {
                        var selectedTeams = item.Teams;
                        foreach (var selectedTeam in selectedTeams)
                        {
                            int CountData = objCustomerTimeBlock.Where(x => x.Days == Day && x.teamID == selectedTeam).Count();
                            if (CountData != 0)
                            {
                                var filterdates = objCustomerTimeBlock.Where(x => x.Days == Day && x.teamID == selectedTeam).Select(p => new TimeRange { Start = p.Start, End = p.End }).ToList();
                                result = result.Where(o => !filterdates.Any(i => RangesOverlap(o.Times, i))).ToList();
                            }
                            else
                            {
                                var filterdates = objCustomerTimeBlock.Where(x => x.Days == Day).Select(p => new TimeRange { Start = p.Start, End = p.End }).ToList();
                                result = result.Where(o => !filterdates.Any(i => RangesOverlap(o.Times, i))).ToList();
                            }
                        }
                    }
                }
            }
            result = result.OrderBy(x => x.Times.Start).ToList();
            if (times.Time != null)
            {
                TimeSpan B1 = ConvertToTimeSpans(times.Time);
                result = result.Where(x => x.Times.Start >= B1).ToList();
            }

            return result;
        }

        public List<TimeResult1> GetResultForOtherTime(GetResultForOtherTime time)
        {
            List<TimeResult1> result1 = new List<TimeResult1>();
            List<TimeResult1> result = new List<TimeResult1>();
            DateTime TodayDate = DateTime.Now;
            // DateTime StartDate = TodayDate.AddHours(24);
            // DateTime LastDate = StartDate.AddDays(30);
            DateTime StartDate = Convert.ToDateTime(time.StartDate);
            int AddMonth = 0;
            if (time.NoOfMonth == null)
            {
                AddMonth = 1;
            }
            else
            {
                int? NoOfMonthAdd = UhDB.CustomerRenewalMonths.Where(x => x.custrmID == time.NoOfMonth && x.IsActive == true && x.IsDelete == false).FirstOrDefault().NoOfMonths;
                AddMonth = Convert.ToInt32(NoOfMonthAdd);

            }
            DateTime LastDate = StartDate.AddMonths(AddMonth);
            List<Bookings> TeamsScores = new List<Bookings>();
            List<string> stringListOfDays = new List<string>();
            List<int?> AddTeams = new List<int?>();
            List<TeamAndTime> objTeamAndTime = new List<TeamAndTime>();
            if (time.teams.Days.Contains(","))
            {
                string[] stringArrayOfDays = time.teams.Days.Split(',');
                stringListOfDays = stringArrayOfDays.Skip(1).ToList();
            }
            else
            {
                stringListOfDays.Add(time.teams.Days);
            }
            foreach (var team in time.teams.Teams)
            {
                int? teamID = team;
                var objCustomerOfficialDetails = UhDB.CustomerOfficalDetails.Where(x => x.Customer.uID == 1 && x.IsActive == true && x.IsDelete == false).Select(p => new { p.custID, p.custODID }).Distinct().ToList();
                foreach (var objCustomerOfficialDetail in objCustomerOfficialDetails)
                {
                    int? cuID = objCustomerOfficialDetail.custID;
                    int? custODID = objCustomerOfficialDetail.custODID;
                    var objCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.custODID == custODID && x.custID == cuID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    if (objCustomerTimeLines != null)
                    {
                        int? Score = objCustomerTimeLines.CustomerOfficalDetail.SubArea.ScoreID;
                        TeamsScores.Add(new Bookings { TeamID = teamID, Score = Score });
                    }
                    else
                    {
                        TeamsScores.Add(new Bookings { TeamID = teamID, Score = 0 });

                    }
                }

            }
            if (TeamsScores.Count() != 0)
            {
                int ZeroCount = TeamsScores.Where(x => x.Score == 0).Count();
                if (ZeroCount >= 1)
                {
                    List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
                    List<int?> FinalTeams = new List<int?>();
                    FinalTeams = TeamsScores.Where(x => x.Score == 0).Select(x => x.TeamID).ToList();
                    int? FinalTeam = GetRandomTeams(FinalTeams);
                    var objCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.teamID == FinalTeam && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                               && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                    var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.teamID == FinalTeam && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                               && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                    if (objCustomerTimeLines != null)
                    {
                        if (objCustomerTimeLines.Count() != 0)
                        {
                            tempData.AddRange(objCustomerTimeLines);
                        }
                    }
                    if (objBlockDates != null)
                    {
                        if (objBlockDates.Count() != 0)
                        {
                            tempData.AddRange(objBlockDates);
                        }
                    }
                    tempData = tempData.OrderBy(x => x.StartDate).ToList();
                    foreach (var stringListOfDay in stringListOfDays)
                    {
                        if (tempData.Count() != 0)
                        {
                            string Day = stringListOfDay;
                            var filterDates = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == Day).ToList();
                            if (!filterDates.Any())
                            {
                                var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                string TimeMeasurement = objTimeRange.TimeMeasurement;
                                int Time = 0;
                                if (TimeMeasurement == "Hours")
                                {
                                    Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                }
                                else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                TimeRange originalRange = new TimeRange();
                                originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                originalRange.End = ConvertToTimeSpans("06:00 PM");
                                List<TimeRange> timeRange = GetTimeDifference1(originalRange, Time);
                                objTeamAndTime.Add(new TeamAndTime { teamID = FinalTeam, Day = stringListOfDay, Time = timeRange });
                            }
                            else
                            {
                                var groupedDates = filterDates.GroupBy(date => date.StartDate).Select(group => new
                                {
                                    StartDate = group.Key,
                                    Events = group.ToList()
                                });
                                List<TimeRange> ResultimeRange = new List<TimeRange>();
                                List<TimeRange> StartDateTimes = new List<TimeRange>();
                                foreach (var group in groupedDates)
                                {
                                    List<TimeRange> expectTimes = new List<TimeRange>();
                                    foreach (var eventDetail in group.Events)
                                    {
                                        expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                    }
                                    var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                    string TimeMeasurement = objTimeRange.TimeMeasurement;
                                    int Time = 0;
                                    if (TimeMeasurement == "Hours")
                                    {
                                        Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                    }
                                    else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                    TimeRange originalRange = new TimeRange();
                                    originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                    originalRange.End = ConvertToTimeSpans("06:00 PM");
                                    List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                    ResultimeRange.AddRange(timeRange);
                                }
                                ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                objTeamAndTime.Add(new TeamAndTime { teamID = FinalTeam, Day = Day, Time = ResultimeRange });
                            }

                        }
                        else
                        {
                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                            int Time = 0;
                            if (TimeMeasurement == "Hours")
                            {
                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                            }
                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                            TimeRange originalRange = new TimeRange();
                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                            List<TimeRange> timeRange = GetTimeDifference1(originalRange, Time);
                            objTeamAndTime.Add(new TeamAndTime { teamID = FinalTeam, Day = stringListOfDay, Time = timeRange });
                        }
                    }
                }
                else if (ZeroCount == TeamsScores.Count())
                {
                    List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
                    List<int?> FinalTeams = new List<int?>();
                    foreach (var item in TeamsScores)
                    {
                        FinalTeams.Add(item.TeamID);
                    }
                    int? FinalTeam = GetRandomTeams(FinalTeams);
                    var objCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.teamID == FinalTeam && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                               && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                    var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.teamID == FinalTeam && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                        && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                    if (objCustomerTimeLines != null)
                    {
                        if (objCustomerTimeLines.Count() != 0)
                        {
                            tempData.AddRange(objCustomerTimeLines);
                        }
                    }
                    if (objBlockDates != null)
                    {
                        if (objBlockDates.Count() != 0)
                        {
                            tempData.AddRange(objBlockDates);
                        }
                    }
                    tempData = tempData.OrderBy(x => x.StartDate).ToList();

                    foreach (var stringListOfDay in stringListOfDays)
                    {
                        if (tempData.Count() != 0)
                        {
                            string Day = stringListOfDay;
                            var filterDates = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == Day).ToList();
                            if (!filterDates.Any())
                            {
                                var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                string TimeMeasurement = objTimeRange.TimeMeasurement;
                                int Time = 0;
                                if (TimeMeasurement == "Hours")
                                {
                                    Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                }
                                else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                TimeRange originalRange = new TimeRange();
                                originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                originalRange.End = ConvertToTimeSpans("06:00 PM");
                                List<TimeRange> timeRange = GetTimeDifference1(originalRange, Time);
                                objTeamAndTime.Add(new TeamAndTime { teamID = FinalTeam, Day = stringListOfDay, Time = timeRange });
                            }
                            else
                            {
                                var groupedDates = filterDates.GroupBy(date => date.StartDate).Select(group => new
                                {
                                    StartDate = group.Key,
                                    Events = group.ToList()
                                });
                                List<TimeRange> ResultimeRange = new List<TimeRange>();
                                List<TimeRange> StartDateTimes = new List<TimeRange>();
                                foreach (var group in groupedDates)
                                {
                                    List<TimeRange> expectTimes = new List<TimeRange>();
                                    foreach (var eventDetail in group.Events)
                                    {
                                        expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                    }
                                    var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                    string TimeMeasurement = objTimeRange.TimeMeasurement;
                                    int Time = 0;
                                    if (TimeMeasurement == "Hours")
                                    {
                                        Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                    }
                                    else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                    TimeRange originalRange = new TimeRange();
                                    originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                    originalRange.End = ConvertToTimeSpans("06:00 PM");
                                    List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                    ResultimeRange.AddRange(timeRange);
                                }
                                ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                objTeamAndTime.Add(new TeamAndTime { teamID = FinalTeam, Day = Day, Time = ResultimeRange });
                            }

                        }
                        else
                        {
                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                            int Time = 0;
                            if (TimeMeasurement == "Hours")
                            {
                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                            }
                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                            TimeRange originalRange = new TimeRange();
                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                            List<TimeRange> timeRange = GetTimeDifference1(originalRange, Time);
                            objTeamAndTime.Add(new TeamAndTime { teamID = FinalTeam, Day = stringListOfDay, Time = timeRange });
                        }

                    }
                }
                else
                {
                    int? CustomerAreaScore = UhDB.SubAreas.Where(x => x.subAreaID == time.subarea && x.IsActive == true && x.IsDelete == false).FirstOrDefault().ScoreID;
                    var TeamAreaDifference = TeamsScores.Select(person => new
                    {
                        TeamID = person.TeamID,
                        Difference = Math.Abs((int)CustomerAreaScore - (int)person.Score)
                    }).ToList();

                    // Find the minimum difference
                    int minDifference = TeamAreaDifference.Min(x => x.Difference);
                    var NearestTeams = TeamAreaDifference.Where(x => x.Difference == minDifference).OrderBy(x => x.TeamID).ToList();
                    if (NearestTeams.Count() > 1)
                    {
                        List<Bookings> LeastTaskAssigned = new List<Bookings>();
                        foreach (var NearestTeam in NearestTeams)
                        {
                            int? nT = NearestTeam.TeamID;
                            int TeamTaskCount = UhDB.CustomerTimelines.Where(x => x.teamID == nT && x.IsActive == true && x.IsDelete == false).Count();
                            LeastTaskAssigned.Add(new Bookings { TeamID = nT, Score = TeamTaskCount });
                        }
                        int? LeastTask = LeastTaskAssigned.Min(x => x.Score);
                        var LeastTasksTeams = LeastTaskAssigned.Where(x => x.Score == LeastTask).OrderBy(x => x.TeamID).ToList();
                        if (LeastTasksTeams.Count > 1)
                        {
                            List<TeamRating> objTeamRating = new List<TeamRating>();
                            foreach (var LeastTasksTeam in LeastTasksTeams)
                            {
                                int? RatedTeam = LeastTasksTeam.TeamID;
                                objTeamRating.AddRange(GetTeamCustomerRating(RatedTeam));
                            }
                            int? HighRated = objTeamRating.Max(x => x.Rating);
                            var HighRatedTeams = objTeamRating.Where(x => x.Rating == HighRated).OrderBy(x => x.Teams).ToList();
                            int? FinalTeam = null;
                            if (HighRatedTeams.Count() > 1)
                            {
                                List<int?> FinalTeams = new List<int?>();
                                foreach (var HighRatedTeam in HighRatedTeams)
                                {
                                    FinalTeams.Add(HighRatedTeam.Teams);
                                }
                                FinalTeam = GetRandomTeams(FinalTeams);
                            }
                            else
                            {
                                if (HighRatedTeams.Count() == 0)
                                {
                                    List<int?> FinalTeams = new List<int?>();
                                    foreach (var LeastTasksTeam in LeastTasksTeams)
                                    {
                                        FinalTeams.Add(LeastTasksTeam.TeamID);
                                    }
                                    FinalTeam = GetRandomTeams(FinalTeams);
                                }
                                else
                                {
                                    var HighRatedTeam = HighRatedTeams[0];
                                    FinalTeam = HighRatedTeam.Teams;
                                }
                            }
                            List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
                            var objCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.teamID == FinalTeam && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                                       && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                            var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.teamID == FinalTeam && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                                && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                            if (objCustomerTimeLines != null)
                            {
                                if (objCustomerTimeLines.Count() != 0)
                                {
                                    tempData.AddRange(objCustomerTimeLines);
                                }
                            }
                            if (objBlockDates != null)
                            {
                                if (objBlockDates.Count() != 0)
                                {
                                    tempData.AddRange(objBlockDates);
                                }
                            }
                            if (tempData!=null) 
                            {
                                if (tempData.Count()!=0) 
                                {
                                    tempData = tempData.OrderBy(x => x.StartDate).ToList();
                                }
                            }
                            foreach (var stringListOfDay in stringListOfDays)
                            {
                                if (tempData.Count() != 0)
                                {
                                    string Day = stringListOfDay;
                                    var filterDates = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == Day).ToList();
                                    if (!filterDates.Any())
                                    {
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = GetTimeDifference1(originalRange, Time);
                                        objTeamAndTime.Add(new TeamAndTime { teamID = FinalTeam, Day = stringListOfDay, Time = timeRange });
                                    }
                                    else
                                    {
                                        var groupedDates = filterDates.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        List<TimeRange> StartDateTimes = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });

                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                        objTeamAndTime.Add(new TeamAndTime { teamID = FinalTeam, Day = Day, Time = ResultimeRange });
                                    }

                                }
                                else
                                {
                                    var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                    string TimeMeasurement = objTimeRange.TimeMeasurement;
                                    int Time = 0;
                                    if (TimeMeasurement == "Hours")
                                    {
                                        Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                    }
                                    else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                    TimeRange originalRange = new TimeRange();
                                    originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                    originalRange.End = ConvertToTimeSpans("06:00 PM");
                                    List<TimeRange> timeRange = GetTimeDifference1(originalRange, Time);
                                    objTeamAndTime.Add(new TeamAndTime { teamID = FinalTeam, Day = stringListOfDay, Time = timeRange });
                                }
                            }
                        }
                        else
                        {
                            List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
                            int? FinalTeam = null;
                            var LeastTasksTeam = LeastTasksTeams[0];
                            FinalTeam = LeastTasksTeam.TeamID;
                            var objCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.teamID == FinalTeam && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                                       && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                            var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.teamID == FinalTeam && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                                      && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                            if (objCustomerTimeLines != null)
                            {
                                if (objCustomerTimeLines.Count() != 0)
                                {
                                    tempData.AddRange(objCustomerTimeLines);
                                }
                            }
                            if (objBlockDates != null)
                            {
                                if (objBlockDates.Count() != 0)
                                {
                                    tempData.AddRange(objBlockDates);
                                }
                            }
                            if (tempData!=null) 
                            {
                                if (tempData.Count()!=0) 
                                {
                                    tempData = tempData.OrderBy(x => x.StartDate).ToList();
                                }
                            }
                            foreach (var stringListOfDay in stringListOfDays)
                            {
                                if (tempData.Count() != 0)
                                {
                                    string Day = stringListOfDay;
                                    var filterDates = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == Day).ToList();
                                    if (!filterDates.Any())
                                    {
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = GetTimeDifference1(originalRange, Time);
                                        objTeamAndTime.Add(new TeamAndTime { teamID = FinalTeam, Day = stringListOfDay, Time = timeRange });
                                    }
                                    else
                                    {
                                        var groupedDates = filterDates.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        List<TimeRange> StartDateTimes = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });

                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                        objTeamAndTime.Add(new TeamAndTime { teamID = FinalTeam, Day = Day, Time = ResultimeRange });
                                    }

                                }
                                else
                                {
                                    var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                    string TimeMeasurement = objTimeRange.TimeMeasurement;
                                    int Time = 0;
                                    if (TimeMeasurement == "Hours")
                                    {
                                        Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                    }
                                    else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                    TimeRange originalRange = new TimeRange();
                                    originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                    originalRange.End = ConvertToTimeSpans("06:00 PM");
                                    List<TimeRange> timeRange = GetTimeDifference1(originalRange, Time);
                                    objTeamAndTime.Add(new TeamAndTime { teamID = FinalTeam, Day = stringListOfDay, Time = timeRange });
                                }

                            }
                        }
                    }
                    else
                    {
                        List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
                        int? FinalTeam = null;
                        var NearestTeam = NearestTeams[0];
                        FinalTeam = NearestTeam.TeamID;
                        var objCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.teamID == FinalTeam && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                                   && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                        var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.teamID == FinalTeam && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                            && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                        if (objCustomerTimeLines != null)
                        {
                            if (objCustomerTimeLines.Count() != 0)
                            {
                                tempData.AddRange(objCustomerTimeLines);
                            }
                        }
                        if (objBlockDates != null)
                        {
                            if (objBlockDates.Count() != 0)
                            {
                                tempData.AddRange(objBlockDates);
                            }
                        }
                        if (tempData!=null) 
                        {
                            if (tempData.Count()!=0) 
                            {
                                tempData = tempData.OrderBy(x => x.StartDate).ToList();
                            }
                        }
                        foreach (var stringListOfDay in stringListOfDays)
                        {
                            if (tempData.Count() != 0)
                            {
                                string Day = stringListOfDay;
                                var filterDates = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == Day).ToList();
                                if (!filterDates.Any())
                                {
                                    var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                    string TimeMeasurement = objTimeRange.TimeMeasurement;
                                    int Time = 0;
                                    if (TimeMeasurement == "Hours")
                                    {
                                        Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                    }
                                    else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                    TimeRange originalRange = new TimeRange();
                                    originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                    originalRange.End = ConvertToTimeSpans("06:00 PM");
                                    List<TimeRange> timeRange = GetTimeDifference1(originalRange, Time);
                                    objTeamAndTime.Add(new TeamAndTime { teamID = FinalTeam, Day = stringListOfDay, Time = timeRange });
                                }
                                else
                                {
                                    var groupedDates = filterDates.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    List<TimeRange> StartDateTimes = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            //if (group.StartDate == time.StartDate)
                                            //{
                                            //    StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            //}
                                            StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                    objTeamAndTime.Add(new TeamAndTime { teamID = FinalTeam, Day = Day, Time = ResultimeRange });
                                }

                            }
                            else
                            {
                                var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                string TimeMeasurement = objTimeRange.TimeMeasurement;
                                int Time = 0;
                                if (TimeMeasurement == "Hours")
                                {
                                    Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                }
                                else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                TimeRange originalRange = new TimeRange();
                                originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                originalRange.End = ConvertToTimeSpans("06:00 PM");
                                List<TimeRange> timeRange = GetTimeDifference1(originalRange, Time);
                                objTeamAndTime.Add(new TeamAndTime { teamID = FinalTeam, Day = stringListOfDay, Time = timeRange });
                            }
                        }
                    }
                }
            }
            else
            {
                List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
                foreach (var team in time.teams.Teams)
                {
                    AddTeams.Add(team);
                }
                int? FinalTeam = GetRandomTeams(AddTeams);

                var objCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.teamID == FinalTeam && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                           && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.teamID == FinalTeam && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                    && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                if (objCustomerTimeLines != null)
                {
                    if (objCustomerTimeLines.Count()!=0) 
                    {
                        tempData.AddRange(objCustomerTimeLines);
                    }
                }
                if (objBlockDates != null)
                {
                    if (objBlockDates.Count()!=0) 
                    {
                        tempData.AddRange(objBlockDates);
                    }
                    
                }
                if (tempData!=null) 
                {
                    if (tempData.Count()!=0) 
                    {
                        tempData = tempData.OrderBy(x => x.StartDate).ToList();
                    }
                }
                foreach (var stringListOfDay in stringListOfDays)
                {
                    if (tempData.Count() != 0)
                    {
                        string Day = stringListOfDay;
                        var filterDates = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == Day).ToList();
                        if (!filterDates.Any())
                        {
                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                            int Time = 0;
                            if (TimeMeasurement == "Hours")
                            {
                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                            }
                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                            TimeRange originalRange = new TimeRange();
                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                            List<TimeRange> timeRange = GetTimeDifference1(originalRange, Time);
                            objTeamAndTime.Add(new TeamAndTime { teamID = FinalTeam, Day = stringListOfDay, Time = timeRange });
                        }
                        else
                        {
                            var groupedDates = filterDates.GroupBy(date => date.StartDate).Select(group => new
                            {
                                StartDate = group.Key,
                                Events = group.ToList()
                            });
                            List<TimeRange> ResultimeRange = new List<TimeRange>();
                            List<TimeRange> StartDateTimes = new List<TimeRange>();
                            foreach (var group in groupedDates)
                            {
                                List<TimeRange> expectTimes = new List<TimeRange>();
                                foreach (var eventDetail in group.Events)
                                {
                                    expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                    StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                }
                                var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                string TimeMeasurement = objTimeRange.TimeMeasurement;
                                int Time = 0;
                                if (TimeMeasurement == "Hours")
                                {
                                    Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                }
                                else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                TimeRange originalRange = new TimeRange();
                                originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                originalRange.End = ConvertToTimeSpans("06:00 PM");
                                List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                ResultimeRange.AddRange(timeRange);
                            }
                            ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                            objTeamAndTime.Add(new TeamAndTime { teamID = FinalTeam, Day = Day, Time = ResultimeRange });
                        }

                    }
                    else
                    {
                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == time.packID && x.proprestID == time.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                        int Time = 0;
                        if (TimeMeasurement == "Hours")
                        {
                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                        }
                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                        TimeRange originalRange = new TimeRange();
                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                        List<TimeRange> timeRange = GetTimeDifference1(originalRange, Time);
                        objTeamAndTime.Add(new TeamAndTime { teamID = FinalTeam, Day = stringListOfDay, Time = timeRange });
                    }

                }
            }
            List<string> T1 = new List<string>();
            foreach (var objTime in objTeamAndTime)
            {

                List<TimeRange> objTimeRanges = objTime.Time;
                int? teamID = objTime.teamID;
                string Day = objTime.Day;
                objTimeRanges = objTimeRanges.OrderBy(x => x.Start).ToList();
                objTimeRanges = objTimeRanges.OrderBy(x => x.End).ToList();
                objTimeRanges = objTimeRanges.Distinct().ToList();
                List<TimeRange> FinalTimeRange = new List<TimeRange>();
                foreach (var objTimeRange in objTimeRanges)
                {
                    int CountFinalTimeRange = FinalTimeRange.Where(x => x.Start == objTimeRange.Start && x.End == objTimeRange.End).Count();
                    if (CountFinalTimeRange == 0)
                    {
                        FinalTimeRange.Add(new TimeRange { Start = objTimeRange.Start, End = objTimeRange.End });
                    }
                }
                FinalTimeRange = FinalTimeRange.OrderBy(x => x.Start).ToList();
                result.Add(new TimeResult1 { Day = Day, Teams = teamID, Time = FinalTimeRange });
            }
            DateTime TurncatedTime = DateTime.Now;
            int CountCustomerTimeBlock = UhDB.CustomerTimeBlocks.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(TurncatedTime) && x.IsActive == true && x.IsDelete == false).Count();
            if (CountCustomerTimeBlock != 0)
            {
                var objCustomerTimeBlock = UhDB.CustomerTimeBlocks.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(TurncatedTime) && x.IsActive == true && x.IsDelete == false).AsEnumerable().
                                           Select(p => new { teamID = p.teamID, Days = p.Days, Start = ConvertToTimeSpans(p.StartTime), End = ConvertToTimeSpans(p.EndTime) }).ToList();
                foreach (var item in result)
                {
                    List<TimeRange> FinalTime = new List<TimeRange>();
                    int CountFilterData = objCustomerTimeBlock.Where(x => x.Days == item.Day && x.teamID == item.Teams).Count();
                    if (CountFilterData != 0)
                    {
                        var FilterData = objCustomerTimeBlock.Where(x => x.Days == item.Day && x.teamID == item.Teams).Select(p => new TimeRange { Start = p.Start, End = p.End }).ToList();
                        var Times = item.Time;
                        FinalTime = Times.Where(o => !FilterData.Any(i => RangesOverlap(o, i))).ToList();
                        result1.Add(new TimeResult1 { Day = item.Day, Teams = item.Teams, Time = FinalTime });

                    }
                    else
                    {
                        var FilterData = objCustomerTimeBlock.Where(x => x.Days == item.Day).Select(p => new TimeRange { Start = p.Start, End = p.End }).ToList();
                        var Times = item.Time;
                        FinalTime = Times.Where(o => !FilterData.Any(i => RangesOverlap(o, i))).ToList();
                        result1.Add(new TimeResult1 { Day = item.Day, Teams = item.Teams, Time = FinalTime });

                    }
                }
            }
            else
            {
                result1.AddRange(result);
            }
            return result1;
        }

        private static bool RangesOverlap(TimeRange range1, TimeRange range2)
        {
            return range1.Start < range2.End && range2.Start < range1.End;
        }

        private static bool RangesOverlap1(TimeSpan Start, TimeSpan End, TimeRange range2)
        {
            TimeRange range1 = new TimeRange();
            range1.Start = Start;
            range1.End = End;
            return range1.Start < range2.End && range2.Start < range1.End;
        }

        public List<GetBookedStartDates> GetBookedDates(BookedStartDates booked)
        {
            List<GetBookedStartDates> result = new List<GetBookedStartDates>();
            List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
            DateTime TodayDate = DateTime.Now;
            DateTime? StartDate = TodayDate.AddHours(24);
            foreach (var objTeam in booked.Teams)
            {
                int? teamID = objTeam;
                int CountTeams = UhDB.CustomerOfficalDetails.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).Count();
                if (CountTeams != 0)
                {
                    var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == booked.catID && x.CustomerOfficalDetail.catsubID == booked.catsubID && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                   && x.CustomerOfficalDetail.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                    var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.CustomerOfficalDetail.catID == booked.catID && x.CustomerOfficalDetail.catsubID == booked.catsubID && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                     && x.CustomerOfficalDetail.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                    if (objDates != null)
                    {
                        tempData.AddRange(objDates);
                    }
                    if (objBlockDates != null)
                    {
                        tempData.AddRange(objBlockDates);
                    }
                    tempData = tempData.OrderBy(x => x.StartDate).ToList();
                    if (tempData != null)
                    {
                        foreach (var Day in booked.Days)
                        {
                            var objFilterDates = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == Day).ToList();
                            if (objFilterDates != null)
                            {
                                var groupedDates = objFilterDates.GroupBy(date => date.StartDate).Select(group => new
                                {
                                    StartDate = group.Key,
                                    Events = group.ToList()
                                });

                                foreach (var group in groupedDates)
                                {
                                    List<TimeRange> expectTimes = new List<TimeRange>();
                                    foreach (var eventDetail in group.Events)
                                    {
                                        expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                    }
                                    result.Add(new GetBookedStartDates { StartDate = Convert.ToDateTime(group.StartDate).ToString("dd/MM/yyyy"), TimeRange = expectTimes });
                                }
                            }
                        }
                    }
                }


            }
            return result;
        }

        public List<GetBookedStartDates> GetBookedDates1(BookedStartDates1 booked)
        {
            List<GetBookedStartDates> result = new List<GetBookedStartDates>();

            DateTime TodayDate = DateTime.Now;
            DateTime? StartDate = TodayDate.AddHours(24);
            List<GetTeamsDatesAndTimes> objGetTeamsDatesAndTimes = new List<GetTeamsDatesAndTimes>();
            var objTeams = UhDB.StaffServices.Where(x => x.MainCategory.uID == 1 && x.catID == 1 && x.catsubID == 1 && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var teams in objTeams)
            {
                List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
                int? teamID = teams.teamID;
                var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == booked.catID
                               && x.CustomerOfficalDetail.catsubID == booked.catsubID
                               && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                               && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.CustomerOfficalDetail.catID == booked.catID
                                    && x.CustomerOfficalDetail.catsubID == booked.catsubID
                                    && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                    && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                if (objDates != null)
                {
                    if (objDates.Count() != 0)
                    {
                        tempData.AddRange(objDates);
                    }
                }
                if (objBlockDates != null)
                {
                    if (objBlockDates.Count() != 0)
                    {
                        tempData.AddRange(objBlockDates);
                    }
                }
                tempData = tempData.OrderBy(x => x.StartDate).ToList();
                if (tempData != null)
                {
                    if (tempData.Count() != 0)
                    {
                        var groupedDates = tempData.GroupBy(date => date.StartDate).Select(group => new
                        {
                            StartDate = group.Key,
                            Events = group.ToList()
                        });
                        List<GetDatesAndTimes> objGetDatesAndTimes = new List<GetDatesAndTimes>();
                        foreach (var group in groupedDates)
                        {
                            List<TimeRange> ResultimeRange = new List<TimeRange>();
                            List<TimeRange> StartDateTimes = new List<TimeRange>();
                            List<TimeRange> expectTimes = new List<TimeRange>();
                            foreach (var eventDetail in group.Events)
                            {
                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                            }
                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == booked.packID && x.proprestID == booked.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                            int Time = 0;
                            if (TimeMeasurement == "Hours")
                            {
                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                            }
                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                            TimeRange originalRange = new TimeRange();
                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                            if (StartDateTimes.Count != 0)
                            {
                                timeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                            }
                            if (booked.Time != null)
                            {
                                if (Convert.ToDateTime(group.StartDate).ToString("dd/MM/yyyy") == Convert.ToDateTime(StartDate).ToString("dd/MM/yyyy"))
                                {
                                    TimeSpan B1 = ConvertToTimeSpans(booked.Time);
                                    timeRange = timeRange.Where(x => x.Start >= B1).ToList();
                                }
                            }
                            bool? IsAvailableDate = null;
                            if (timeRange != null)
                            {
                                if (timeRange.Count() != 0)
                                {
                                    IsAvailableDate = true;
                                }
                                else
                                {
                                    IsAvailableDate = false;
                                }
                            }
                            else
                            {
                                IsAvailableDate = false;
                            }
                            objGetDatesAndTimes.Add(new GetDatesAndTimes { StartDate = group.StartDate, TimeRange = timeRange, IsAvailableDate = IsAvailableDate });
                        }
                        objGetTeamsDatesAndTimes.Add(new GetTeamsDatesAndTimes { teamID = teamID, Dates = objGetDatesAndTimes });
                    }
                    else
                    {
                        objGetTeamsDatesAndTimes.Add(new GetTeamsDatesAndTimes { teamID = teamID, Dates = null });
                    }
                }
            }
            if (objGetTeamsDatesAndTimes.Count() == 0)
            {
                result = null;
            }
            else if (objGetTeamsDatesAndTimes.Any(x => x.Dates == null))
            {
                result = null;
            }
            else
            {

                if (objGetTeamsDatesAndTimes.Count() == 1)
                {
                    foreach (var DatesAndTimes in objGetTeamsDatesAndTimes)
                    {
                        var D1 = DatesAndTimes.Dates;
                        foreach (var Dates in D1)
                        {
                            if (!result.Any(x => x.StartDate == Convert.ToDateTime(Dates.StartDate).ToString("yyyy/MM/dd")))
                            {
                                result.Add(new GetBookedStartDates { StartDate = Convert.ToDateTime(Dates.StartDate).ToString("yyyy/MM/dd"), IsDateAvailable = Dates.IsAvailableDate });
                            }
                        }
                    }
                }
                else
                {
                    // Find common dates across all teams
                    var commonDates = objGetTeamsDatesAndTimes
                        .Where(t => t.Dates != null)  // Only consider teams with non-null dates
                        .SelectMany(t => t.Dates.Select(d => d.StartDate))
                        .GroupBy(d => d)
                        .Where(g => g.Count() == objGetTeamsDatesAndTimes.Count(t => t.Dates != null))  // Ensure date is common across all teams
                        .Select(g => g.Key)
                        .ToList();

                    // Check availability for each common date
                    foreach (var date in commonDates)
                    {
                        bool isAvailable = objGetTeamsDatesAndTimes
                            .Where(t => t.Dates != null)  // Consider teams with non-null dates
                            .Any(t => t.Dates.Any(d => d.StartDate == date && d.IsAvailableDate == true));
                        if (!result.Any(x => x.StartDate == Convert.ToDateTime(date).ToString("yyyy/MM/dd")))
                        {
                            result.Add(new GetBookedStartDates
                            {
                                StartDate = Convert.ToDateTime(date).ToString("yyyy/MM/dd"),
                                IsDateAvailable = isAvailable
                            });
                        }
                    }

                    // Extract uncommon dates
                    var allDates = objGetTeamsDatesAndTimes
                        .Where(t => t.Dates != null)
                        .SelectMany(t => t.Dates)
                        .GroupBy(d => d.StartDate)
                        .Where(g => g.Count() == 1)
                        .Select(g => g.First())
                        .ToList();
                    foreach (var Date in allDates)
                    {
                        if (!result.Any(x => x.StartDate == Convert.ToDateTime(Date.StartDate).ToString("yyyy/MM/dd")))
                        {
                            result.Add(new GetBookedStartDates { StartDate = Convert.ToDateTime(Date.StartDate).ToString("yyyy/MM/dd"), IsDateAvailable = true });
                        }

                    }
                }

            }
            return result;
        }

        public List<GetBookedStartDates1> GetBookedDates2(BookedStartDates1 booked)
        {
            List<GetBookedStartDates1> result = new List<GetBookedStartDates1>();
            List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
            DateTime TodayDate = DateTime.Now;
            DateTime? StartDate = TodayDate.AddHours(24);
            var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == booked.catID && x.CustomerOfficalDetail.catsubID == booked.catsubID && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
            var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.CustomerOfficalDetail.catID == booked.catID && x.CustomerOfficalDetail.catsubID == booked.catsubID && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
            if (objDates != null)
            {
                tempData.AddRange(objDates);
            }
            if (objBlockDates != null)
            {
                tempData.AddRange(objBlockDates);
            }
            tempData = tempData.OrderBy(x => x.StartDate).ToList();
            if (tempData != null)
            {
                var groupedDates = tempData.GroupBy(date => date.StartDate).Select(group => new
                {
                    StartDate = group.Key,
                    Events = group.ToList()
                });
                foreach (var group in groupedDates)
                {
                    List<StringTimeRange> expectTimes = new List<StringTimeRange>();
                    foreach (var eventDetail in group.Events)
                    {
                        expectTimes.Add(new StringTimeRange { Start = eventDetail.StartTime, End = eventDetail.EndTime });
                    }
                    result.Add(new GetBookedStartDates1 { StartDate = Convert.ToDateTime(group.StartDate).ToString("yyyy/MM/dd"), TimeRange = expectTimes });
                }
            }
            return result;
        }

        public int? GetRandomTeams(List<int?> Teams)
        {
            int? result = null;

            // Create a random number generator
            Random random = new Random();

            // Select a random index from the list
            int randomIndex = random.Next(Teams.Count);

            // Get the random number from the list
            result = (int)Teams[randomIndex];
            return result;
        }

        public List<TeamRating> GetTeamCustomerRating(int? Team)
        {
            List<TeamRating> result = new List<TeamRating>();

            var objRatings = UhDB.CustomerFeedbacks.Where(x => x.CustomerOfficalDetail.teamID == Team && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objRating in objRatings)
            {
                result.Add(new TeamRating { Teams = Team, Rating = objRating.Rating });
            }
            return result;
        }


        public int? GetPerviousTeam(int? catID, int? catsubID, int? cuID)
        {
            int? ID = null;
            int CountTeam = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID && x.custID == cuID && x.IsActive == true && x.IsDelete == false).Count();
            if (CountTeam!=0) 
            {
                ID = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID && x.custID == cuID && x.IsActive == true && x.IsDelete == false).OrderByDescending(x => x.custTDID).FirstOrDefault().teamID;
            }
            return ID;
        }

        public List<GetResultsForTimeSlot> GetResultsForTimeSlotsExisting(GetResultsForTimeSlotsExisting customer)
        {
            List<GetResultsForTimeSlot> result = new List<GetResultsForTimeSlot>();
            DateTime TodayDate = DateTime.Now;
            DateTime StartDate = Convert.ToDateTime(customer.StartDate);
            string StartDay = StartDate.DayOfWeek.ToString();
            int AddMonth = 0;
            if (customer.NoOfMonth == null)
            {
                AddMonth = 1;
            }
            else
            {
                int? NoOfMonthAdd = UhDB.CustomerRenewalMonths.Where(x => x.custrmID == customer.NoOfMonth && x.IsActive == true && x.IsDelete == false).FirstOrDefault().NoOfMonths;
                AddMonth = Convert.ToInt32(NoOfMonthAdd);
            }
            DateTime LastDate = StartDate.AddMonths(AddMonth);
            List<GetDayWithTeam> GetDayWithTeam = new List<GetDayWithTeam>();
            int? packCount = UhDB.Packages.Where(x => x.packID == customer.packID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().RecursiveTime;
            if (packCount == 0 || packCount == 1)
            {
                int? teamID = customer.teamID;
                int CountTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).Count();
                if (CountTeams == 0)
                {
                    string Day = StartDate.ToString("dddd");
                    for (var i = 1; i <= 7; i++)
                    {
                        DateTime checkDate = StartDate.AddDays(i);
                        string dayOfWeek = checkDate.DayOfWeek.ToString();
                        if (dayOfWeek == StartDay)
                        {
                            GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = dayOfWeek });
                        }
                    }
                }
                else
                {
                    List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
                    string Day = StartDate.ToString("dddd");
                    var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == customer.catID && x.CustomerOfficalDetail.catsubID == customer.catsubID
                                   && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate) &&
                                   EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                   && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                    var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.CustomerOfficalDetail.catID == customer.catID && x.CustomerOfficalDetail.catsubID == customer.catsubID
                                        && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate) &&
                                        EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                        && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                    if (objDates != null)
                    {
                        tempData.AddRange(objDates);
                    }
                    if (objBlockDates != null)
                    {
                        tempData.AddRange(objBlockDates);
                    }
                    tempData = tempData.OrderBy(x => x.StartDate).ToList();
                    if (tempData != null)
                    {
                        for (int i = 1; i <= 7; i++)
                        {
                            DateTime checkDate = StartDate;
                            string dayOfWeek = checkDate.DayOfWeek.ToString();
                            var filteredDates = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek).ToList();
                            if (!filteredDates.Any())
                            {
                                if (dayOfWeek == StartDay)
                                {
                                    GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = dayOfWeek });
                                }
                                StartDate = StartDate.AddDays(1);
                            }
                            else
                            {
                                if (dayOfWeek == StartDay)
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    List<TimeRange> StartDateTimes = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == customer.packID && x.proprestID == customer.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);

                                    }
                                    if (StartDateTimes.Count != 0)
                                    {
                                        ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                    }
                                    if (customer.Time != null)
                                    {
                                        TimeSpan B1 = ConvertToTimeSpans(customer.Time);
                                        ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        if (ResultimeRange.Count() != 0)
                                        {
                                            GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = dayOfWeek });
                                        }
                                        StartDate = StartDate.AddDays(1);
                                    }
                                    else
                                    {
                                        StartDate = StartDate.AddDays(1);
                                    }
                                }
                                else
                                {
                                    StartDate = StartDate.AddDays(1);
                                }
                            }
                        }

                    }
                }
                var result1 = GetDayWithTeam.GroupBy(x => x.Days).Select(group => new
                {
                    Days = group.Key,
                    Teams = group.ToList()
                });
                foreach (var item in result1)
                {
                    string Days = item.Days;
                    List<int?> Teams = item.Teams.Select(p => p.teamID).Distinct().ToList();
                    result.Add(new GetResultsForTimeSlot { Days = Days, Teams = Teams });
                }
            }
            else if (packCount == 2)
            {
                int? teamID = customer.teamID;
                int CountTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).Count();
                if (CountTeams == 0)
                {
                    List<string> Days = GetTwoTimesAWeek();
                    foreach (var Day in Days)
                    {
                        if (Day.Contains(StartDay))
                        {
                            string[] stringArray = Day.Split(',');
                            GetDayWithTeam.Add(new GetDayWithTeam { teamID = teamID, Days = stringArray[0] + "," + stringArray[1] });
                        }
                    }
                }
                else
                {
                    List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
                    string Day = StartDate.ToString("dddd");
                    var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == customer.catID && x.CustomerOfficalDetail.catsubID == customer.catsubID
                                   && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                   && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                   && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                    var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.CustomerOfficalDetail.catID == customer.catID && x.CustomerOfficalDetail.catsubID == customer.catsubID
                                   && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                   && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                   && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                    if (objDates != null)
                    {
                        tempData.AddRange(objDates);
                    }
                    if (objBlockDates != null)
                    {
                        tempData.AddRange(objBlockDates);
                    }
                    tempData = tempData.OrderBy(x => x.StartDate).ToList();

                    if (tempData != null)
                    {
                        int daysToAdd = 0;
                        int CountOFAdd = 0;
                        List<string> Days = GetTwoTimesAWeek();
                        foreach (var InDay in Days)
                        {
                            if (InDay.Contains(StartDay))
                            {
                                if (InDay.Contains(","))
                                {
                                    string[] stringArray = InDay.Split(',');
                                    string dayOfWeek1 = stringArray[0];
                                    string dayOfWeek2 = stringArray[1];
                                    string FirstDay = null, SecondDay = null;
                                    var filteredDates1 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek1).ToList();
                                    if (!filteredDates1.Any())
                                    {
                                        FirstDay = dayOfWeek1;
                                    }
                                    else
                                    {
                                        bool isDayAvailable = true;
                                        var groupedDates = filteredDates1.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        List<TimeRange> StartDateTimes = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == customer.packID && x.proprestID == customer.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            Time = Convert.ToInt32(objTimeRange.Duration);
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        if (StartDateTimes.Count != 0)
                                        {
                                            ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                        }
                                        if (customer.Time != null)
                                        {
                                            if (dayOfWeek1 == StartDay)
                                            {
                                                TimeSpan B1 = ConvertToTimeSpans(customer.Time);
                                                ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                            }
                                        }
                                        if (ResultimeRange != null)
                                        {
                                            if (ResultimeRange.Count() != 0)
                                            {
                                                FirstDay = dayOfWeek1;
                                            }
                                        }
                                    }
                                    var filteredDates2 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek2).ToList();
                                    if (!filteredDates2.Any())
                                    {
                                        SecondDay = dayOfWeek2;
                                    }
                                    else
                                    {
                                        bool isDayAvailable = true;
                                        var groupedDates = filteredDates2.GroupBy(date => date.StartDate).Select(group => new
                                        {
                                            StartDate = group.Key,
                                            Events = group.ToList()
                                        });
                                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                                        List<TimeRange> StartDateTimes = new List<TimeRange>();
                                        foreach (var group in groupedDates)
                                        {
                                            List<TimeRange> expectTimes = new List<TimeRange>();
                                            foreach (var eventDetail in group.Events)
                                            {
                                                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                                StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            }
                                            var objTimeRange = UhDB.Pricings.Where(x => x.packID == customer.packID && x.proprestID == customer.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string TimeMeasurement = objTimeRange.TimeMeasurement;
                                            int Time = 0;
                                            if (TimeMeasurement == "Hours")
                                            {
                                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                            }
                                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                            TimeRange originalRange = new TimeRange();
                                            originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                            originalRange.End = ConvertToTimeSpans("06:00 PM");
                                            List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                            ResultimeRange.AddRange(timeRange);
                                        }
                                        if (StartDateTimes.Count != 0)
                                        {
                                            ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                        }
                                        if (customer.Time != null)
                                        {
                                            if (dayOfWeek2 == StartDay)
                                            {
                                                TimeSpan B1 = ConvertToTimeSpans(customer.Time);
                                                ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                            }
                                        }
                                        if (ResultimeRange != null)
                                        {
                                            if (ResultimeRange.Count() != 0)
                                            {
                                                SecondDay = dayOfWeek2;
                                            }
                                        }
                                    }
                                    if (FirstDay != null && SecondDay != null)
                                    {
                                        GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = FirstDay + "," + SecondDay });
                                        daysToAdd += 4;
                                        CountOFAdd += 1;
                                    }
                                    else
                                    {
                                        daysToAdd += 1;
                                    }
                                }
                            }
                        }
                    }
                }
                var result1 = GetDayWithTeam.GroupBy(x => x.Days).Select(group => new
                {
                    Days = group.Key,
                    Teams = group.ToList()
                });
                foreach (var item in result1)
                {
                    string Days = item.Days;
                    List<int?> Teams = item.Teams.Select(p => p.teamID).Distinct().ToList();
                    result.Add(new GetResultsForTimeSlot { Days = Days, Teams = Teams });
                }
            }
            else if (packCount == 3)
            {

                int? teamID = customer.teamID;
                int CountTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).Count();
                if (CountTeams == 0)
                {
                    List<string> dayBundles = GetThreeTimesAWeek();
                    foreach (var bundle in dayBundles)
                    {
                        if (bundle.Contains(StartDay))
                        {
                            string[] stringArray = bundle.Split(',');
                            GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = stringArray[0] + "," + stringArray[1] + "," + stringArray[2] });
                        }
                    }
                }
                else
                {
                    List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
                    var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == customer.catID && x.CustomerOfficalDetail.catsubID == customer.catsubID
                                   && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                   && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                   && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                    var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.CustomerOfficalDetail.catID == customer.catID && x.CustomerOfficalDetail.catsubID == customer.catsubID
                                        && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                        && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                        && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                    if (objDates != null)
                    {
                        tempData.AddRange(objDates);
                    }
                    if (objBlockDates != null)
                    {
                        tempData.AddRange(objBlockDates);
                    }
                    tempData = tempData.OrderBy(x => x.StartDate).ToList();
                    if (tempData != null)
                    {

                        List<string> dayBundles = GetThreeTimesAWeek();
                        foreach (var bundle in dayBundles)
                        {
                            if (bundle.Contains(StartDay))
                            {
                                string[] stringArray = bundle.Split(',');
                                string FirstDay = null, SecondDay = null, ThirdDay = null;
                                string dayOfWeek1 = stringArray[0];
                                string dayOfWeek2 = stringArray[1];
                                string dayOfWeek3 = stringArray[2];
                                var filteredDates1 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek1).ToList();
                                if (!filteredDates1.Any())
                                {
                                    FirstDay = dayOfWeek1;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates1.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    List<TimeRange> StartDateTimes = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == customer.packID && x.proprestID == customer.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (StartDateTimes.Count != 0)
                                    {
                                        ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                    }
                                    if (customer.Time != null)
                                    {
                                        if (dayOfWeek1 == StartDay)
                                        {
                                            TimeSpan B1 = ConvertToTimeSpans(customer.Time);
                                            ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                        }
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        if (ResultimeRange.Count() != 0)
                                        {
                                            FirstDay = dayOfWeek1;
                                        }
                                    }
                                }

                                var filteredDates2 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek2).ToList();
                                if (!filteredDates2.Any())
                                {
                                    SecondDay = dayOfWeek2;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates2.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    List<TimeRange> StartDateTimes = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == customer.packID && x.proprestID == customer.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (StartDateTimes.Count != 0)
                                    {
                                        ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                    }
                                    if (customer.Time != null)
                                    {
                                        if (dayOfWeek2 == StartDay)
                                        {
                                            TimeSpan B1 = ConvertToTimeSpans(customer.Time);
                                            ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                        }
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        if (ResultimeRange.Count() != 0)
                                        {
                                            SecondDay = dayOfWeek2;
                                        }
                                    }

                                }

                                var filteredDates3 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek3).ToList();
                                if (!filteredDates3.Any())
                                {
                                    ThirdDay = dayOfWeek3;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates3.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    List<TimeRange> StartDateTimes = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == customer.packID && x.proprestID == customer.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (StartDateTimes.Count != 0)
                                    {
                                        ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                    }
                                    if (customer.Time != null)
                                    {
                                        if (dayOfWeek3 == StartDay)
                                        {
                                            TimeSpan B1 = ConvertToTimeSpans(customer.Time);
                                            ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                        }
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        if (ResultimeRange.Count() != 0)
                                        {
                                            ThirdDay = dayOfWeek3;
                                        }
                                    }
                                }
                                if (FirstDay != null && SecondDay != null && ThirdDay != null)
                                {
                                    string[] inputDays = new string[] { FirstDay, SecondDay, ThirdDay };
                                    List<string> resultDays = new List<string>();

                                    for (int i = 0; i < inputDays.Length; i++)
                                    {
                                        bool isRepeated = false;

                                        for (int j = 0; j < inputDays.Length; j++)
                                        {
                                            if (i != j && inputDays[i] == inputDays[j])
                                            {
                                                isRepeated = true;
                                                break;
                                            }
                                        }

                                        if (!isRepeated)
                                        {
                                            resultDays.Add(inputDays[i]);
                                        }
                                    }

                                    if (resultDays != null)
                                    {
                                        if (resultDays.Count() == 3)
                                        {
                                            GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = FirstDay + "," + SecondDay + "," + ThirdDay });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                var result1 = GetDayWithTeam.GroupBy(x => x.Days).Select(group => new
                {
                    Days = group.Key,
                    Teams = group.ToList()
                });
                foreach (var item in result1)
                {
                    string Days = item.Days;
                    List<int?> Teams = item.Teams.Select(p => p.teamID).Distinct().ToList();
                    result.Add(new GetResultsForTimeSlot { Days = Days, Teams = Teams });
                }
            }
            else if (packCount == 4)
            {
                int? teamID = customer.teamID;
                int CountTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).Count();
                if (CountTeams == 0)
                {
                    List<string> dayBundles = GetFourTimesAWeek();
                    foreach (var bundle in dayBundles)
                    {
                        if (bundle.Contains(StartDay))
                        {
                            string[] stringArray = bundle.Split(',');
                            GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = stringArray[0] + "," + stringArray[1] + "," + stringArray[2] + "," + stringArray[3] });
                        }

                    }
                }
                else
                {
                    List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
                    var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == customer.catID && x.CustomerOfficalDetail.catsubID == customer.catsubID
                                   && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                   && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                   && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                    var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.CustomerOfficalDetail.catID == customer.catID && x.CustomerOfficalDetail.catsubID == customer.catsubID
                                        && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                        && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                        && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                    if (objDates != null)
                    {
                        tempData.AddRange(objDates);
                    }
                    if (objBlockDates != null)
                    {
                        tempData.AddRange(objBlockDates);
                    }
                    tempData = tempData.OrderBy(x => x.StartDate).ToList();
                    if (tempData != null)
                    {

                        List<string> dayBundles = GetFourTimesAWeek();
                        foreach (var bundle in dayBundles)
                        {
                            if (bundle.Contains(StartDay))
                            {
                                string[] stringArray = bundle.Split(',');
                                string FirstDay = null, SecondDay = null, ThirdDay = null, FourthDay = null;
                                string dayOfWeek1 = stringArray[0];
                                string dayOfWeek2 = stringArray[1];
                                string dayOfWeek3 = stringArray[2];
                                string dayOfWeek4 = stringArray[3];

                                var filteredDates1 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek1).ToList();
                                if (!filteredDates1.Any())
                                {
                                    FirstDay = dayOfWeek1;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates1.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    List<TimeRange> StartDateTimes = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == customer.packID && x.proprestID == customer.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (StartDateTimes.Count != 0)
                                    {
                                        ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                    }
                                    if (customer.Time != null)
                                    {
                                        if (dayOfWeek1 == StartDay)
                                        {
                                            TimeSpan B1 = ConvertToTimeSpans(customer.Time);
                                            ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                        }
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        if (ResultimeRange.Count() != 0)
                                        {
                                            FirstDay = dayOfWeek1;
                                        }

                                    }

                                }

                                var filteredDates2 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek2).ToList();
                                if (!filteredDates2.Any())
                                {
                                    SecondDay = dayOfWeek2;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates2.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    List<TimeRange> StartDateTimes = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == customer.packID && x.proprestID == customer.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (StartDateTimes.Count != 0)
                                    {
                                        ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                    }
                                    if (customer.Time != null)
                                    {
                                        if (dayOfWeek2 == StartDay)
                                        {
                                            TimeSpan B1 = ConvertToTimeSpans(customer.Time);
                                            ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                        }
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        if (ResultimeRange.Count() != 0)
                                        {
                                            SecondDay = dayOfWeek2;
                                        }
                                    }
                                }

                                var filteredDates3 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek3).ToList();
                                if (!filteredDates3.Any())
                                {
                                    ThirdDay = dayOfWeek3;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates3.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    List<TimeRange> StartDateTimes = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == customer.packID && x.proprestID == customer.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (StartDateTimes.Count != 0)
                                    {
                                        ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                    }
                                    if (customer.Time != null)
                                    {
                                        if (dayOfWeek3 == StartDay)
                                        {
                                            TimeSpan B1 = ConvertToTimeSpans(customer.Time);
                                            ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                        }
                                    }

                                    if (ResultimeRange != null)
                                    {
                                        if (ResultimeRange.Count() != 0)
                                        {
                                            ThirdDay = dayOfWeek3;
                                        }
                                    }

                                }

                                var filteredDates4 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek4).ToList();
                                if (!filteredDates4.Any())
                                {
                                    FourthDay = dayOfWeek4;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates4.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    List<TimeRange> StartDateTimes = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == customer.packID && x.proprestID == customer.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (StartDateTimes.Count != 0)
                                    {
                                        ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                    }
                                    if (customer.Time != null)
                                    {
                                        if (dayOfWeek4 == StartDay)
                                        {
                                            TimeSpan B1 = ConvertToTimeSpans(customer.Time);
                                            ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                        }
                                    }

                                    if (ResultimeRange != null)
                                    {
                                        if (ResultimeRange.Count() != 0)
                                        {
                                            FourthDay = dayOfWeek4;
                                        }
                                    }
                                }
                                if (FirstDay != null && SecondDay != null && ThirdDay != null && FourthDay != null)
                                {
                                    string[] inputDays = new string[] { FirstDay, SecondDay, ThirdDay, FourthDay };
                                    List<string> resultDays = new List<string>();

                                    for (int i = 0; i < inputDays.Length; i++)
                                    {
                                        bool isRepeated = false;

                                        for (int j = 0; j < inputDays.Length; j++)
                                        {
                                            if (i != j && inputDays[i] == inputDays[j])
                                            {
                                                isRepeated = true;
                                                break;
                                            }
                                        }

                                        if (!isRepeated)
                                        {
                                            resultDays.Add(inputDays[i]);
                                        }
                                    }
                                    if (resultDays != null)
                                    {
                                        if (resultDays.Count() == 4)
                                        {
                                            GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = FirstDay + "," + SecondDay + "," + ThirdDay + "," + FourthDay });
                                        }
                                    }

                                }
                            }
                        }
                    }
                }

                var result1 = GetDayWithTeam.GroupBy(x => x.Days).Select(group => new
                {
                    Days = group.Key,
                    Teams = group.ToList()
                });
                foreach (var item in result1)
                {
                    string Days = item.Days;
                    List<int?> Teams = item.Teams.Select(p => p.teamID).Distinct().ToList();
                    result.Add(new GetResultsForTimeSlot { Days = Days, Teams = Teams });
                }
            }
            else if (packCount == 5)
            {
                int? teamID = customer.teamID;
                int CountTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).Count();
                if (CountTeams == 0)
                {
                    List<string> dayBundles = GetFiveTimesAWeek();
                    foreach (var bundle in dayBundles)
                    {
                        if (bundle.Contains(StartDay))
                        {
                            string[] stringArray = bundle.Split(',');
                            GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = stringArray[0] + "," + stringArray[1] + "," + stringArray[2] + "," + stringArray[3] + "," + stringArray[4] });
                        }
                    }
                }
                else
                {
                    List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
                    var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == customer.catID && x.CustomerOfficalDetail.catsubID == customer.catsubID
                                   && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                   && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                   && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                    var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.CustomerOfficalDetail.catID == customer.catID && x.CustomerOfficalDetail.catsubID == customer.catsubID
                                        && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                        && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                        && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                    if (objDates != null)
                    {
                        tempData.AddRange(objDates);
                    }
                    if (objBlockDates != null)
                    {
                        tempData.AddRange(objBlockDates);
                    }
                    tempData = tempData.OrderBy(x => x.StartDate).ToList();
                    if (tempData != null)
                    {
                        List<string> dayBundles = GetFiveTimesAWeek();
                        foreach (var bundle in dayBundles)
                        {
                            if (bundle.Contains(StartDay))
                            {
                                string FirstDay = null, SecondDay = null, ThirdDay = null, FourthDay = null, FifthDay = null;
                                string[] stringArray = bundle.Split(',');
                                string dayOfWeek1 = stringArray[0];
                                string dayOfWeek2 = stringArray[1];
                                string dayOfWeek3 = stringArray[2];
                                string dayOfWeek4 = stringArray[3];
                                string dayOfWeek5 = stringArray[4];

                                var filteredDates1 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek1).ToList();
                                if (!filteredDates1.Any())
                                {
                                    FirstDay = dayOfWeek1;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates1.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    List<TimeRange> StartDateTimes = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == customer.packID && x.proprestID == customer.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (StartDateTimes.Count != 0)
                                    {
                                        ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                    }
                                    if (customer.Time != null)
                                    {
                                        if (dayOfWeek1 == StartDay)
                                        {
                                            TimeSpan B1 = ConvertToTimeSpans(customer.Time);
                                            ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                        }
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        if (ResultimeRange.Count() != 0)
                                        {
                                            FirstDay = dayOfWeek1;
                                        }
                                    }

                                }
                                var filteredDates2 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek2).ToList();
                                if (!filteredDates2.Any())
                                {
                                    SecondDay = dayOfWeek2;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates2.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    List<TimeRange> StartDateTimes = new List<TimeRange>();

                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == customer.packID && x.proprestID == customer.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (StartDateTimes.Count != 0)
                                    {
                                        ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                    }
                                    if (customer.Time != null)
                                    {
                                        if (dayOfWeek2 == StartDay)
                                        {
                                            TimeSpan B1 = ConvertToTimeSpans(customer.Time);
                                            ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                        }
                                    }

                                    if (ResultimeRange != null)
                                    {
                                        if (ResultimeRange.Count() != 0)
                                        {
                                            SecondDay = dayOfWeek2;
                                        }
                                    }

                                }

                                var filteredDates3 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek3).ToList();
                                if (!filteredDates3.Any())
                                {
                                    ThirdDay = dayOfWeek3;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates3.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    List<TimeRange> StartDateTimes = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == customer.packID && x.proprestID == customer.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (StartDateTimes.Count != 0)
                                    {
                                        ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                    }
                                    if (customer.Time != null)
                                    {
                                        if (dayOfWeek3 == StartDay)
                                        {
                                            TimeSpan B1 = ConvertToTimeSpans(customer.Time);
                                            ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                        }
                                    }

                                    if (ResultimeRange != null)
                                    {
                                        if (ResultimeRange.Count() != 0)
                                        {
                                            ThirdDay = dayOfWeek3;
                                        }
                                    }

                                }

                                var filteredDates4 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek4).ToList();
                                if (!filteredDates4.Any())
                                {
                                    FourthDay = dayOfWeek4;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates4.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    List<TimeRange> StartDateTimes = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == customer.packID && x.proprestID == customer.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (StartDateTimes.Count != 0)
                                    {
                                        ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                    }
                                    if (customer.Time != null)
                                    {
                                        if (dayOfWeek4 == StartDay)
                                        {
                                            TimeSpan B1 = ConvertToTimeSpans(customer.Time);
                                            ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                        }
                                    }

                                    if (ResultimeRange != null)
                                    {
                                        if (ResultimeRange.Count() != 0)
                                        {
                                            FourthDay = dayOfWeek4;
                                        }
                                    }
                                }

                                var filteredDates5 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek5).ToList();
                                if (!filteredDates5.Any())
                                {
                                    FifthDay = dayOfWeek5;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates5.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    List<TimeRange> StartDateTimes = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == customer.packID && x.proprestID == customer.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (StartDateTimes.Count != 0)
                                    {
                                        ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                    }
                                    if (customer.Time != null)
                                    {
                                        if (dayOfWeek5 == StartDay)
                                        {
                                            TimeSpan B1 = ConvertToTimeSpans(customer.Time);
                                            ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                        }
                                    }

                                    if (ResultimeRange != null)
                                    {
                                        if (ResultimeRange.Count() != 0)
                                        {
                                            FifthDay = dayOfWeek5;
                                        }
                                    }
                                }

                                if (FirstDay != null && SecondDay != null && ThirdDay != null && FourthDay != null && FifthDay != null)
                                {
                                    string[] inputDays = new string[] { FirstDay, SecondDay, ThirdDay, FourthDay, FifthDay };
                                    List<string> resultDays = new List<string>();

                                    for (int i = 0; i < inputDays.Length; i++)
                                    {
                                        bool isRepeated = false;

                                        for (int j = 0; j < inputDays.Length; j++)
                                        {
                                            if (i != j && inputDays[i] == inputDays[j])
                                            {
                                                isRepeated = true;
                                                break;
                                            }
                                        }

                                        if (!isRepeated)
                                        {
                                            resultDays.Add(inputDays[i]);
                                        }
                                    }
                                    if (resultDays != null)
                                    {
                                        if (resultDays.Count() == 5)
                                        {
                                            GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = FirstDay + "," + SecondDay + "," + ThirdDay + "," + FourthDay + "," + FifthDay });
                                        }
                                    }
                                }
                            }

                        }
                    }
                }

                var result1 = GetDayWithTeam.GroupBy(x => x.Days).Select(group => new
                {
                    Days = group.Key,
                    Teams = group.ToList()
                });
                foreach (var item in result1)
                {
                    string Days = item.Days;
                    List<int?> Teams = item.Teams.Select(p => p.teamID).Distinct().ToList();
                    result.Add(new GetResultsForTimeSlot { Days = Days, Teams = Teams });
                }
            }
            else if (packCount == 6)
            {
                int? teamID = customer.teamID;
                int CountTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).Count();
                if (CountTeams == 0)
                {
                    List<List<string>> dayBundles = GetDayBundles3();
                    foreach (var bundle in dayBundles)
                    {
                        if (bundle.Contains(StartDay))
                        {
                            GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = bundle[0] + "," + bundle[1] + "," + bundle[2] + "," + bundle[3] + "," + bundle[4] + "," + bundle[5] });
                        }
                    }
                }
                else
                {
                    List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
                    var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == customer.catID && x.CustomerOfficalDetail.catsubID == customer.catsubID
                                   && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                   && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                   && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                    var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.CustomerOfficalDetail.catID == customer.catID && x.CustomerOfficalDetail.catsubID == customer.catsubID
                                        && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                        && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)
                                        && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                    if (objDates != null)
                    {
                        tempData.AddRange(objDates);
                    }
                    if (objBlockDates != null)
                    {
                        tempData.AddRange(objBlockDates);
                    }
                    tempData = tempData.OrderBy(x => x.StartDate).ToList();
                    if (tempData != null)
                    {
                        List<List<string>> dayBundles = GetDayBundles3();
                        foreach (var bundle in dayBundles)
                        {
                            if (bundle.Contains(StartDay))
                            {
                                string FirstDay = null, SecondDay = null, ThirdDay = null, FourthDay = null, FifthDay = null, SixthDay = null;
                                string dayOfWeek1 = bundle[0];
                                string dayOfWeek2 = bundle[1];
                                string dayOfWeek3 = bundle[2];
                                string dayOfWeek4 = bundle[3];
                                string dayOfWeek5 = bundle[4];
                                string dayOfWeek6 = bundle[5];

                                var filteredDates1 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek1).ToList();
                                if (!filteredDates1.Any())
                                {
                                    FirstDay = dayOfWeek1;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates1.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    List<TimeRange> StartDateTimes = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == customer.packID && x.proprestID == customer.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (StartDateTimes.Count != 0)
                                    {
                                        ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                    }
                                    if (customer.Time != null)
                                    {
                                        if (dayOfWeek1 == StartDay)
                                        {
                                            TimeSpan B1 = ConvertToTimeSpans(customer.Time);
                                            ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                        }
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        if (ResultimeRange.Count() != 0)
                                        {
                                            FirstDay = dayOfWeek1;
                                        }
                                    }

                                }

                                var filteredDates2 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek2).ToList();
                                if (!filteredDates2.Any())
                                {
                                    SecondDay = dayOfWeek2;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates2.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    List<TimeRange> StartDateTimes = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == customer.packID && x.proprestID == customer.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (StartDateTimes.Count != 0)
                                    {
                                        ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                    }
                                    if (customer.Time != null)
                                    {
                                        if (dayOfWeek2 == StartDay)
                                        {
                                            TimeSpan B1 = ConvertToTimeSpans(customer.Time);
                                            ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                        }
                                    }

                                    if (ResultimeRange != null)
                                    {
                                        if (ResultimeRange.Count() != 0)
                                        {
                                            SecondDay = dayOfWeek2;
                                        }
                                    }

                                }

                                var filteredDates3 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek3).ToList();
                                if (!filteredDates3.Any())
                                {
                                    ThirdDay = dayOfWeek3;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates3.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    List<TimeRange> StartDateTimes = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == customer.packID && x.proprestID == customer.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (StartDateTimes.Count != 0)
                                    {
                                        ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                    }
                                    if (customer.Time != null)
                                    {
                                        if (dayOfWeek3 == StartDay)
                                        {
                                            TimeSpan B1 = ConvertToTimeSpans(customer.Time);
                                            ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                        }
                                    }

                                    if (ResultimeRange != null)
                                    {
                                        if (ResultimeRange.Count() != 0)
                                        {
                                            ThirdDay = dayOfWeek3;
                                        }
                                    }

                                }

                                var filteredDates4 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek4).ToList();
                                if (!filteredDates4.Any())
                                {
                                    FourthDay = dayOfWeek4;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates4.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    List<TimeRange> StartDateTimes = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == customer.packID && x.proprestID == customer.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (StartDateTimes.Count != 0)
                                    {
                                        ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                    }
                                    if (customer.Time != null)
                                    {
                                        if (dayOfWeek4 == StartDay)
                                        {
                                            TimeSpan B1 = ConvertToTimeSpans(customer.Time);
                                            ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                        }
                                    }

                                    if (ResultimeRange != null)
                                    {
                                        if (ResultimeRange.Count() != 0)
                                        {
                                            FourthDay = dayOfWeek4;
                                        }
                                    }
                                }

                                var filteredDates5 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek5).ToList();
                                if (!filteredDates5.Any())
                                {
                                    FifthDay = dayOfWeek5;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates5.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    List<TimeRange> StartDateTimes = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == customer.packID && x.proprestID == customer.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (StartDateTimes.Count != 0)
                                    {
                                        ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                    }
                                    if (customer.Time != null)
                                    {
                                        if (dayOfWeek5 == StartDay)
                                        {
                                            TimeSpan B1 = ConvertToTimeSpans(customer.Time);
                                            ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                        }
                                    }
                                    if (ResultimeRange != null)
                                    {
                                        if (ResultimeRange.Count() != 0)
                                        {
                                            FifthDay = dayOfWeek5;
                                        }
                                    }
                                }

                                var filteredDates6 = tempData.Where(x => x.StartDate.Value.DayOfWeek.ToString() == dayOfWeek6).ToList();
                                if (!filteredDates6.Any())
                                {
                                    SixthDay = dayOfWeek6;
                                }
                                else
                                {
                                    bool isDayAvailable = true;
                                    var groupedDates = filteredDates6.GroupBy(date => date.StartDate).Select(group => new
                                    {
                                        StartDate = group.Key,
                                        Events = group.ToList()
                                    });
                                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                                    List<TimeRange> StartDateTimes = new List<TimeRange>();
                                    foreach (var group in groupedDates)
                                    {
                                        List<TimeRange> expectTimes = new List<TimeRange>();
                                        foreach (var eventDetail in group.Events)
                                        {
                                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                            StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                        }
                                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == customer.packID && x.proprestID == customer.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                                        int Time = 0;
                                        if (TimeMeasurement == "Hours")
                                        {
                                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                                        }
                                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                                        TimeRange originalRange = new TimeRange();
                                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                                        ResultimeRange.AddRange(timeRange);
                                    }
                                    if (StartDateTimes.Count != 0)
                                    {
                                        ResultimeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                                    }
                                    if (customer.Time != null)
                                    {
                                        if (dayOfWeek6 == StartDay)
                                        {
                                            TimeSpan B1 = ConvertToTimeSpans(customer.Time);
                                            ResultimeRange = ResultimeRange.Where(x => x.Start >= B1).ToList();
                                        }
                                    }

                                    if (ResultimeRange != null)
                                    {
                                        if (ResultimeRange.Count() != 0)
                                        {
                                            SixthDay = dayOfWeek6;
                                        }
                                    }
                                }

                                if (FirstDay != null && SecondDay != null && ThirdDay != null && FourthDay != null && FifthDay != null && SixthDay != null)
                                {
                                    string[] inputDays = new string[] { FirstDay, SecondDay, ThirdDay, FourthDay, FifthDay, SixthDay };
                                    List<string> resultDays = new List<string>();

                                    for (int i = 0; i < inputDays.Length; i++)
                                    {
                                        bool isRepeated = false;

                                        for (int j = 0; j < inputDays.Length; j++)
                                        {
                                            if (i != j && inputDays[i] == inputDays[j])
                                            {
                                                isRepeated = true;
                                                break;
                                            }
                                        }

                                        if (!isRepeated)
                                        {
                                            resultDays.Add(inputDays[i]);
                                        }
                                    }
                                    if (resultDays != null)
                                    {
                                        if (resultDays.Count() == 6)
                                        {
                                            GetDayWithTeam.Add(new DAL.GetDayWithTeam { teamID = teamID, Days = FirstDay + "," + SecondDay + "," + ThirdDay + "," + FourthDay + "," + FifthDay + "," + SixthDay });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                var result1 = GetDayWithTeam.GroupBy(x => x.Days).Select(group => new
                {
                    Days = group.Key,
                    Teams = group.ToList()
                });
                foreach (var item in result1)
                {
                    string Days = item.Days;
                    List<int?> Teams = item.Teams.Select(p => p.teamID).Distinct().ToList();
                    result.Add(new GetResultsForTimeSlot { Days = Days, Teams = Teams });
                }
            }
            return result;
        }

        public int? GetTimeBlock(GetResultCheck time)
        {
            int? result = null;
            DateTime StartDate = Convert.ToDateTime(time.StartDate);
            int AddMonth = 0;
            if (time.NoOfMonth == null)
            {
                AddMonth = 1;
            }
            else
            {
                int? NoOfMonthAdd = UhDB.CustomerRenewalMonths.Where(x => x.custrmID == time.NoOfMonth && x.IsActive == true && x.IsDelete == false).FirstOrDefault().NoOfMonths;
                AddMonth = Convert.ToInt32(NoOfMonthAdd);

            }
            DateTime LastDate = StartDate.AddMonths(AddMonth);
            List<Bookings> TeamsScores = new List<Bookings>();
            List<string> stringListOfDays = new List<string>();
            List<int?> AddTeams = new List<int?>();
            List<TeamAndTime> objTeamAndTime = new List<TeamAndTime>();
            List<int> IsDateAvailable = new List<int>();
            int? teamID = null;
            foreach (var teams in time.teams.Teams)
            {
                teamID = teams;
                var objCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.teamID == teamID && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                            && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(LastDate)).ToList();

                foreach (var times in time.teams.Time)
                {
                    if (objCustomerTimeLines.Count() != 0)
                    {
                        string Day = times.Days;
                        var filterDates = objCustomerTimeLines.Where(x => x.StartDate.Value.DayOfWeek.ToString() == Day).ToList();
                        if (!filterDates.Any())
                        {
                            int? selectedTeamID = time.teams.Teams[0];
                            DateTime TurncatedDate = DateTime.Now;
                            int CountCustomerTimeLock = UhDB.CustomerTimeBlocks.Where(x => x.IsActive == true && x.IsDelete == false && x.teamID == selectedTeamID
                                                        && x.Days == times.Days && EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(TurncatedDate)
                                                        && x.StartTime == times.Times.Start && x.EndTime == times.Times.End).Count();
                            if (CountCustomerTimeLock == 0)
                            {

                                IsDateAvailable.Add(1);
                            }
                            else
                            {
                                IsDateAvailable.Add(0);
                            }
                        }
                        else
                        {
                            var groupedDates = filterDates.GroupBy(date => date.StartDate).Select(group => new
                            {
                                StartDate = group.Key,
                                Events = group.ToList()
                            });
                            List<TimeRange> ResultimeRange = new List<TimeRange>();
                            foreach (var group in groupedDates)
                            {
                                List<TimeRange> expectTimes = new List<TimeRange>();
                                foreach (var eventDetail in group.Events)
                                {
                                    expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                                }
                                if (expectTimes.Any(x => x.Start == ConvertToTimeSpans(times.Times.Start) && x.End == ConvertToTimeSpans(times.Times.End)))
                                {
                                    IsDateAvailable.Add(0);
                                }
                                else
                                {
                                    int? selectedTeamID = time.teams.Teams[0];
                                    DateTime TurncatedDate = DateTime.Now;
                                    int CountCustomerTimeLock = UhDB.CustomerTimeBlocks.Where(x => x.IsActive == true && x.IsDelete == false && x.teamID == selectedTeamID
                                                                && EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(TurncatedDate) && x.Days == times.Days
                                                                && x.StartTime == times.Times.Start.ToString() && x.EndTime == times.Times.End.ToString()).Count();
                                    if (CountCustomerTimeLock == 0)
                                    {

                                        IsDateAvailable.Add(1);
                                    }
                                    else
                                    {
                                        IsDateAvailable.Add(0);
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        int? selectedTeamID = time.teams.Teams[0];
                        DateTime TurncatedDate = DateTime.Now;
                        int CountCustomerTimeLock = UhDB.CustomerTimeBlocks.Where(x => x.IsActive == true && x.IsDelete == false && x.teamID == selectedTeamID
                                                    && EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(TurncatedDate) && x.Days == times.Days
                                                    && x.StartTime == times.Times.Start.ToString() && x.EndTime == times.Times.End.ToString()).Count();
                        if (CountCustomerTimeLock == 0)
                        {

                            IsDateAvailable.Add(1);
                        }
                        else
                        {
                            IsDateAvailable.Add(0);
                        }

                    }
                }

            }
            if (IsDateAvailable.Contains(0))
            {
                result = 0;
            }
            else
            {
                DateTime TurncatedDate = DateTime.Now;
                int CountCustomerTimeLock = UhDB.CustomerTimeBlocks.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(TurncatedDate)).Count();
                if (CountCustomerTimeLock == 0)
                {
                    result = 1;
                    foreach (var item in time.teams.Time)
                    {
                        CustomerTimeBlock objCustomerTimeBlocks = new CustomerTimeBlock();
                        objCustomerTimeBlocks.TempID = result;
                        objCustomerTimeBlocks.Days = item.Days;
                        objCustomerTimeBlocks.teamID = teamID;
                        objCustomerTimeBlocks.MobileNo = time.MobileNo;
                        objCustomerTimeBlocks.StartTime = item.Times.Start.ToString();
                        objCustomerTimeBlocks.EndTime = item.Times.End.ToString();
                        objCustomerTimeBlocks.IsActive = true;
                        objCustomerTimeBlocks.IsDelete = false;
                        objCustomerTimeBlocks.CreatedOn = DateTime.Now;
                        objCustomerTimeBlocks.CreatedBy = result.ToString();
                        UhDB.CustomerTimeBlocks.Add(objCustomerTimeBlocks);
                        UhDB.SaveChanges();
                    }
                }
                else
                {
                    int? TempID = UhDB.CustomerTimeBlocks.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(TurncatedDate)).OrderByDescending(x => x.TempID).FirstOrDefault().TempID;
                    result = Convert.ToInt32(TempID) + 1;
                    foreach (var item in time.teams.Time)
                    {
                        CustomerTimeBlock objCustomerTimeBlocks = new CustomerTimeBlock();
                        objCustomerTimeBlocks.TempID = result;
                        objCustomerTimeBlocks.Days = item.Days;
                        objCustomerTimeBlocks.teamID = teamID;
                        objCustomerTimeBlocks.MobileNo = time.MobileNo;
                        objCustomerTimeBlocks.StartTime = item.Times.Start.ToString();
                        objCustomerTimeBlocks.EndTime = item.Times.End.ToString();
                        objCustomerTimeBlocks.IsActive = true;
                        objCustomerTimeBlocks.IsDelete = false;
                        objCustomerTimeBlocks.CreatedOn = DateTime.Now;
                        objCustomerTimeBlocks.CreatedBy = result.ToString();
                        UhDB.CustomerTimeBlocks.Add(objCustomerTimeBlocks);
                        UhDB.SaveChanges();
                    }
                }


            }
            return result;
        }

        public string GetReleaseTimeBlock(string MobileNo)
        {
            string result = null;
            int CountTempID = UhDB.CustomerTimeBlocks.Where(x => x.MobileNo == MobileNo && EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(DateTime.Now) && x.IsActive == true && x.IsDelete == false).Count();
            if (CountTempID == 0)
            {
                result = "ID not Found";
            }
            else
            {
                var objCustomerTimeBlocks = UhDB.CustomerTimeBlocks.Where(x => x.MobileNo == MobileNo && EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(DateTime.Now) && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objCustomerTimeBlock in objCustomerTimeBlocks)
                {
                    var objUpdateCustomerTimeBlock = UhDB.CustomerTimeBlocks.Where(x => x.custTBID == objCustomerTimeBlock.custTBID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objUpdateCustomerTimeBlock.IsActive = false;
                    objUpdateCustomerTimeBlock.IsDelete = true;
                    objUpdateCustomerTimeBlock.UpdatedBy = MobileNo;
                    objUpdateCustomerTimeBlock.UpdatedOn = DateTime.Now;
                    UhDB.SaveChanges();
                }
                result = "SUCCESS";
            }
            return result;
        }

        public List<string> GetTwoTimesAWeek()
        {
            List<string> objTwoTimes = new List<string>();
            objTwoTimes.Add("Monday,Friday");
            objTwoTimes.Add("Tuesday,Saturday");
            objTwoTimes.Add("Wednesday,Sunday");
            objTwoTimes.Add("Thursday,Monday");
            objTwoTimes.Add("Friday,Tuesday");
            objTwoTimes.Add("Saturday,Wednesday");
            objTwoTimes.Add("Sunday,Thursday");
            objTwoTimes.Add("Monday,Saturday");
            objTwoTimes.Add("Tuesday,Sunday");
            objTwoTimes.Add("Thursday,Tuesday");
            objTwoTimes.Add("Saturday,Thursday");
            objTwoTimes.Add("Monday,Wednesday");
            objTwoTimes.Add("Wednesday,Friday");
            objTwoTimes.Add("Friday,Sunday");


            return objTwoTimes;
        }

        public List<string> GetThreeTimesAWeek()
        {
            List<string> days = new List<string>
            {
                "Saturday,Wednesday,Friday",
                "Sunday,Tuesday,Thursday",
                "Monday,Wednesday,Friday",
                "Monday,Wednesday,Saturday",
                "Monday,Thursday,Saturday",
                "Wednesday,Friday,Sunday",
                "Thursday,Saturday,Sunday",
                "Friday,Sunday,Tuesday"
            };
            return days;
        }

        public List<string> GetFourTimesAWeek()
        {
            List<string> days = new List<string>
            {
                "Monday,Wednesday,Friday,Sunday",
                "Monday,Wednesday,Thursday,Saturday",
                "Wednesday,Friday,Saturday,Monday",
                "Tuesday,Thursday,Friday,Sunday",
                "Tuesday,Wednesday,Friday,Sunday",
                "Tuesday,Thursday,Saturday,Sunday",
                "Sunday,Tuesday,Thursday,Saturday",
                "Wednesday,Thursday,Saturday,Sunday",
                "Wednesday,Friday,Saturday,Sunday",
                "Sunday,Wednesday,Friday,Saturday",
                "Sunday,Thursday,Friday,Saturday",
            };
            return days;
        }

        public List<string> GetFiveTimesAWeek()
        {
            List<string> days = new List<string>
            {
                "Monday,Tuesday,Wednesday,Friday,Saturday",
                "Monday,Tuesday,Thursday,Friday,Sunday",
                "Monday,Wednesday,Thursday,Friday,Saturday",
                "Monday,Tuesday,Thursday,Saturday,Sunday",
                "Monday,Wednesday,Thursday,Saturday,Sunday",
                "Monday,Tuesday,Wednesday,Thursday,Saturday",
                "Wednesday,Thursday,Friday,Saturday,Monday",
                "Wednesday,Friday,Saturday,Sunday,Monday",
                "Wednesday,Thursday,Friday,Sunday,Monday",
                "Tuesday,Wednesday,Thursday,Friday,Sunday",
                "Thursday,Friday,Saturday,Sunday,Tuesday",
                "Tuesday,Wednesday,Thursday,Saturday,Sunday",
            };
            return days;
        }



    }


    public class TimeResult
    {
        public TimeRange Times { get; set; }
        public string Time { get; set; }
        public List<int?> Teams { get; set; }
    }

    public class TeamAvailability
    {
        public List<TimeRange> Times { get; set; }
        public string TeamName { get; set; }
    }

    public class TimeResult1
    {
        public List<TimeRange> Time { get; set; }
        public int? Teams { get; set; }
        public string Day { get; set; }
    }

    public class TeamRating
    {
        public Nullable<int> Rating { get; set; }
        public Nullable<int> Teams { get; set; }
    }
    public class TeamsTime
    {
        public List<int?> teamIDs { get; set; }
        public List<TimeRange> Time { get; set; }
    }

    public class TeamAndTime
    {
        public Nullable<int> teamID { get; set; }
        public string Day { get; set; }
        public List<TimeRange> Time { get; set; }
    }

    public class PostTeam
    {
        public string Days { get; set; }
        public List<int?> Teams { get; set; }
    }

    public class TeamCount
    {
        public Nullable<int> teamID { get; set; }
        public Nullable<int> Count { get; set; }
    }

    public class TimeRange
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

        public bool OverlapsWith(TimeRange other)
        {
            return this.Start < other.End && this.End > other.Start;
        }


    }

    public class TimeRange1
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is TimeRange other)
            {
                return this.Start == other.Start && this.End == other.End;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Start.GetHashCode();
                hash = hash * 23 + End.GetHashCode();
                return hash;
            }
        }
    }

    public class AdTimeRange
    {
        public Nullable<int> teamID { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<int> subAreaID { get; set; }
        public Nullable<int> Rank { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
    }

    public class GetResultsForTimeSlot
    {

        public string Days { get; set; }
        public List<int?> Teams { get; set; }

    }

    public class ResultsForTimeSlots1
    {
        public Nullable<int> packID { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> propresID { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public Nullable<int> NoOfMonth { get; set; }
        public string Time { get; set; }
    }

    public class teamIDs
    {
        public List<int?> ID { get; set; }
    }

    public class GetDayWithTeam
    {
        public Nullable<int> teamID { get; set; }
        public string Days { get; set; }
    }

    public class GetDatesAndTimes
    {
        public Nullable<DateTime> StartDate { get; set; }
        public List<TimeRange> TimeRange { get; set; }
        public Nullable<bool> IsAvailableDate { get; set; }
    }

    public class GetTeamsDatesAndTimes
    {
        public List<GetDatesAndTimes> Dates { get; set; }
        public Nullable<int> teamID { get; set; }
    }

}