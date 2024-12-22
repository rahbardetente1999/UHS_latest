using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;
using System.Globalization;
using System.Data.Entity.Core.Objects;
using System.IO;

namespace UHSForm.DAL
{
    public class CustomerRescheduleDB
    {
        private UHSEntities UhDB;
        private GeneralDB objGeneralDB;

        public CustomerRescheduleDB()
        {
            UhDB = new UHSEntities();
            objGeneralDB = new GeneralDB();
        }

        public GetReschedule GetRemaningDateOfCustomer(CustomerBookedStartDates booked)
        {
            GetReschedule result = new GetReschedule();
            DateTime? StartDate = booked.StartDate;
            List<GetTeamsDatesAndTimes> objGetTeamsDatesAndTimes = new List<GetTeamsDatesAndTimes>();
            int? teamID = booked.teamID;
            int? cuID = booked.cuID;
            int? custODID = booked.custODID;
            List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
            if (booked.catID == 1 && booked.catsubID == 1) 
            {
                var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == booked.catID
                           && x.CustomerOfficalDetail.catsubID == booked.catsubID
                           && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                           && x.teamID == teamID && x.custID == cuID && x.custODID == custODID)
                           .Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.CustomerOfficalDetail.catID == booked.catID
                                    && x.CustomerOfficalDetail.catsubID == booked.catsubID
                                     && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                     && x.teamID == teamID && x.custID == cuID && x.custODID == custODID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
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

            }
            else 
            {
                var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == booked.catID
                           && x.CustomerOfficalDetail.catsubID == booked.catsubID
                           && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                           && x.teamID == teamID && x.custID == cuID)
                           .Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                if (objDates != null)
                {
                    if (objDates.Count() != 0)
                    {
                        tempData.AddRange(objDates);
                    }
                }
            }
            DateTime? EndDate = null;
            if (tempData!=null) 
            {
                if (tempData.Count()!=0) 
                {
                    tempData = tempData.OrderBy(x => x.StartDate).ToList();
                    EndDate = tempData.LastOrDefault().StartDate;
                }
            }
            List<GetBookedStartDates> objGetBookedStartDates = new List<GetBookedStartDates>();
            List<GetBookedStartDates> objGetBookedStartDates1 = new List<GetBookedStartDates>();
            List<GetBookedStartDates> objGetBookedStartDates2 = new List<GetBookedStartDates>();
            foreach (var Data in tempData)
            {
                objGetBookedStartDates1.Add(new GetBookedStartDates { StartDate = Convert.ToDateTime(Data.StartDate).ToString("yyyy/MM/dd"), IsDateAvailable = false });
            }
            objGetBookedStartDates.AddRange(objGetBookedStartDates1);
            result.EndDate = EndDate;
            List<TempBookedStartDates> tempData1 = new List<TempBookedStartDates>();
            if (booked.catID == 1 && booked.catsubID == 1)
            {
                var objDateTeam = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == booked.catID
                          && x.CustomerOfficalDetail.catsubID == booked.catsubID
                          && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                          && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(EndDate)
                          && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                var objBlockDatesTeams = UhDB.CustomerDateBlocks.Where(x => x.CustomerOfficalDetail.catID == booked.catID
                                    && x.CustomerOfficalDetail.catsubID == booked.catsubID
                                    && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                    && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(EndDate)
                                    && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                if (objDateTeam != null)
                {
                    if (objDateTeam.Count() != 0)
                    {
                        tempData1.AddRange(objDateTeam);
                    }
                }
                if (objBlockDatesTeams != null)
                {
                    if (objBlockDatesTeams.Count() != 0)
                    {
                        tempData1.AddRange(objBlockDatesTeams);
                    }
                }
            }
            else
            {
                var objDateTeam = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == booked.catID
                                  && x.CustomerOfficalDetail.catsubID == booked.catsubID
                                  && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                  && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                if (objDateTeam != null)
                {
                    if (objDateTeam.Count() != 0)
                    {
                        tempData1.AddRange(objDateTeam);
                    }
                }
            }
            tempData1 = tempData1.OrderBy(x => x.StartDate).ToList();
            if (tempData1 != null)
            {
                if (tempData1.Count() != 0)
                {
                    var groupedDates = tempData1.GroupBy(date => date.StartDate).Select(group => new
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
                        if (booked.catID == 1 && booked.catsubID == 1)
                        {
                            if (TimeMeasurement == "Hours")
                            {
                                Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                            }
                            else { Time = Convert.ToInt32(objTimeRange.Duration); }
                        }
                        else
                        {
                            Time = Convert.ToInt32(booked.Duration);
                        }

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
            if (objGetTeamsDatesAndTimes.Count() == 1)
            {
                foreach (var DatesAndTimes in objGetTeamsDatesAndTimes)
                {
                    var D1 = DatesAndTimes.Dates;
                    foreach (var Dates in D1)
                    {
                        if (!objGetBookedStartDates1.Any(x => x.StartDate == Convert.ToDateTime(Dates.StartDate).ToString("yyyy/MM/dd")))
                        {
                            objGetBookedStartDates2.Add(new GetBookedStartDates { StartDate = Convert.ToDateTime(Dates.StartDate).ToString("yyyy/MM/dd"), IsDateAvailable = Dates.IsAvailableDate });
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
                    if (!objGetBookedStartDates1.Any(x => x.StartDate == Convert.ToDateTime(date).ToString("yyyy/MM/dd")))
                    {
                        objGetBookedStartDates2.Add(new GetBookedStartDates
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
                    if (!objGetBookedStartDates1.Any(x => x.StartDate == Convert.ToDateTime(Date.StartDate).ToString("yyyy/MM/dd")))
                    {
                        objGetBookedStartDates2.Add(new GetBookedStartDates { StartDate = Convert.ToDateTime(Date.StartDate).ToString("yyyy/MM/dd"), IsDateAvailable = true });
                    }
                }
            }
            objGetBookedStartDates2 = objGetBookedStartDates2.OrderBy(x => x.StartDate).ToList();
            objGetBookedStartDates.AddRange(objGetBookedStartDates2);
            objGetBookedStartDates = objGetBookedStartDates.OrderBy(x => x.StartDate).ToList();
            result.GetBookedDates = objGetBookedStartDates;
            return result;
        }

        public string SaveReschedule(SaveRescheduleModel customer)
        {
            string result = null;
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    var objCustomerTimeLine = UhDB.CustomerTimelines.Where(x => x.custID == customer.cuID && x.custODID == customer.custODID && x.IsActive == true && x.IsDelete == false
                                              && x.custTDID == customer.custTDID).FirstOrDefault();
                    objCustomerTimeLine.IsActive = false;
                    objCustomerTimeLine.UpdatedBy = customer.UpdatedBy;
                    objCustomerTimeLine.UpdatedOn = customer.UpdatedOn;
                    objCustomerTimeLine.UpdatedRole = customer.UpdatedRole;
                    UhDB.SaveChanges();
                    int? TaskNo = null;
                    int CountCustomerTaskNo = UhDB.CustomerTimelines.Where(x => x.Customer.uID == 1 && x.TaskNo != null && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountCustomerTaskNo == 0)
                    {
                        TaskNo = 1;
                    }
                    else
                    {
                        TaskNo = UhDB.CustomerTimelines.Where(x => x.Customer.uID == 1 && x.TaskNo != null && x.IsActive == true && x.IsDelete == false).OrderByDescending(y => y.custTDID).FirstOrDefault().TaskNo;
                        TaskNo = TaskNo + 1;
                    }

                    CustomerTimeline objAddCustomerTimeline = new CustomerTimeline();
                    objAddCustomerTimeline.custID = customer.cuID;
                    objAddCustomerTimeline.custODID = customer.custODID;
                    objAddCustomerTimeline.StartDate = customer.RescheduleDate;
                    objAddCustomerTimeline.StartTime = customer.RescheduleStartTime;
                    objAddCustomerTimeline.EndTime = customer.RescheduleEndTime;
                    objAddCustomerTimeline.packID = customer.packID;
                    objAddCustomerTimeline.parkID = customer.parkID;
                    objAddCustomerTimeline.TaskNo = TaskNo;
                    objAddCustomerTimeline.StatusOfWork = 2;
                    objAddCustomerTimeline.teamID = customer.teamID;
                    objAddCustomerTimeline.IsActive = true;
                    objAddCustomerTimeline.IsDelete = false;
                    objAddCustomerTimeline.CreatedBy = customer.UpdatedBy;
                    objAddCustomerTimeline.CreatedOn = customer.UpdatedOn;
                    objAddCustomerTimeline.CreatedRole = customer.UpdatedRole;
                    UhDB.CustomerTimelines.Add(objAddCustomerTimeline);
                    UhDB.SaveChanges();

                    CustomerAlert objCustomerAlert = new CustomerAlert();
                    objCustomerAlert.custID = customer.cuID;
                    objCustomerAlert.custATID = 1;
                    if (objCustomerTimeLine.CustomerOfficalDetail != null)
                    {
                        objCustomerAlert.vID = objCustomerTimeLine.CustomerOfficalDetail.vID;
                        objCustomerAlert.catID = objCustomerTimeLine.CustomerOfficalDetail.catID;
                        objCustomerAlert.catsubID = objCustomerTimeLine.CustomerOfficalDetail.catsubID;
                    }
                    else
                    {
                        objCustomerAlert.vID = 0;
                        objCustomerAlert.catID = 0;
                        objCustomerAlert.catsubID = 0;
                    }


                    objCustomerAlert.Message = "Reschedule Request from :" + objCustomerTimeLine.Customer.Name + " :From " +
                    customer.BeforeDate?.ToString("dd-MM-yyyy") + " To " + customer.RescheduleDate?.ToString("dd-MM-yyyy") + " Time: " + customer.RescheduleStartTime + "-" + customer.RescheduleEndTime + " for " + objCustomerTimeLine.Team.Name;

                    objCustomerAlert.IsActive = true;
                    objCustomerAlert.IsDelete = false;
                    objCustomerAlert.CreatedBy = customer.UpdatedBy;
                    objCustomerAlert.CreatedOn = customer.UpdatedOn;
                    UhDB.CustomerAlerts.Add(objCustomerAlert);
                    UhDB.SaveChanges();
                    if (objCustomerTimeLine.Customer.Email != null)
                    {
                        string CustomerRescheduleBody = EmailBodyCustomerRescheduleBody(objCustomerTimeLine.Customer.Name, Convert.ToDateTime(customer.BeforeDate).ToString("dd/MM/yyyy"), Convert.ToDateTime(customer.RescheduleDate).ToString("dd/MM/yyyy"));
                        objGeneralDB.SentEmailFromAmazon(objCustomerTimeLine.Customer.Email, CustomerRescheduleBody, "You have be reschedule the service", objCustomerTimeLine.Customer.Name);
                    }
                    else
                    {
                        string TextMessage = "You has reschedule your service from " + Convert.ToDateTime(customer.BeforeDate).ToString("dd/MM/yyyy") + " to " + Convert.ToDateTime(customer.RescheduleDate).ToString("dd/MM/yyyy");
                        string MobileNo = objCustomerTimeLine.Customer.PhoneCode + objCustomerTimeLine.Customer.Mobile;
                        MobileNo = MobileNo.Replace(" ", "");
                        objGeneralDB.SendSMS(MobileNo, TextMessage);
                    }
                    string AdminRescheduleBody = EmailBodyAdminRescheduleBody(objCustomerTimeLine.Customer.Name, Convert.ToDateTime(customer.BeforeDate).ToString("dd/MM/yyyy"), Convert.ToDateTime(customer.RescheduleDate).ToString("dd/MM/yyyy"));
                    objGeneralDB.SentEmailFromAmazon(objCustomerTimeLine.Customer.User.Email, AdminRescheduleBody, "Customer has reschedule the service", objCustomerTimeLine.Customer.User.Name);

                    string StaffRescheduleBody = EmailBodyStaffRescheduleBody(objCustomerTimeLine.Customer.Name, Convert.ToDateTime(customer.BeforeDate).ToString("dd/MM/yyyy"), Convert.ToDateTime(customer.RescheduleDate).ToString("dd/MM/yyyy"));
                    var objStaffs = UhDB.StaffTeams.Where(x => x.teamID == objCustomerTimeLine.CustomerOfficalDetail.teamID).ToList();
                    foreach (var objStaff in objStaffs)
                    {
                        if (objStaff.Staff.Email != null)
                        {
                            objGeneralDB.SentEmailFromAmazon(objStaff.Staff.Email, StaffRescheduleBody, "Customer has reschedule the service", objStaff.Staff.Name);
                        }
                        else
                        {
                            string TextMessage = objCustomerTimeLine.Customer.Name + " has reschedule his service from " + Convert.ToDateTime(customer.BeforeDate).ToString("dd/MM/yyyy") + " to " + Convert.ToDateTime(customer.RescheduleDate).ToString("dd/MM/yyyy");
                            string MobileNo = objStaff.Staff.PhoneCode + objStaff.Staff.Mobile;
                            MobileNo = MobileNo.Replace(" ", "");
                            objGeneralDB.SendSMS(MobileNo, TextMessage);
                        }
                    }
                    trans.Commit();
                    result = "SUCCESS";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    result = "Exception";
                }
            }


            return result;
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

        public List<TimeRange> GetSpecDeepAndCarWash(GetSpecDeepAndCarWashModel times)
        {
            List<TimeRange> results = new List<TimeRange>();
            List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
            var objDateTeam = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == times.catID
                                 && x.CustomerOfficalDetail.catsubID == times.catsubID
                                 && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(times.StartDate)
                                 && x.teamID == times.teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
            if (objDateTeam != null)
            {
                if (objDateTeam.Count() != 0)
                {
                    tempData.AddRange(objDateTeam);
                }
            }
            List<TimeRange> expectTimes = new List<TimeRange>();
            foreach (var eventDetail in tempData)
            {
                expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
            }

            TimeRange originalRange = new TimeRange();
            originalRange.Start = ConvertToTimeSpans("8:00 AM");
            originalRange.End = ConvertToTimeSpans("06:00 PM");
            results = RemoveIntervals(originalRange, expectTimes, (int)times.Duration);
            return results;
        }

        public string EmailBodyCustomerRescheduleBody(string CustomerName, string OldDate, string NewDate)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/CustomerReschedule.html")))//using streamreader for reading my htmltemplate  
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{OldDate}", OldDate); //replacing the required things  
            body = body.Replace("{NewDate}", NewDate);
            body = body.Replace("{CustomerName}", CustomerName);
            return body;

        }
        public string EmailBodyAdminRescheduleBody(string CustomerName, string OldDate, string NewDate)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/AdminReschedule.html")))//using streamreader for reading my htmltemplate  
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{OldDate}", OldDate); //replacing the required things  
            body = body.Replace("{NewDate}", NewDate);
            body = body.Replace("{CustomerName}", CustomerName);
            return body;
        }
        public string EmailBodyStaffRescheduleBody(string CustomerName, string OldDate, string NewDate)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/StaffReschedule.html")))//using streamreader for reading my htmltemplate  
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{OldDate}", OldDate); //replacing the required things  
            body = body.Replace("{NewDate}", NewDate);
            body = body.Replace("{CustomerName}", CustomerName);
            return body;
        }

    }
}