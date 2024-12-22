using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.DAL;

namespace UHSForm.Models
{
    public class GetResultsForTimeSlotsExisting
    {
        public Nullable<int> packID { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> propresID { get; set; }
        public Nullable<int> teamID { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public Nullable<int> NoOfMonth { get; set; }
        public string Time { get; set; }
    }

    public class GetSpecDeepAndCarWashModel 
    {
        public Nullable<int> Duration { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> teamID { get; set; }
        public Nullable<DateTime> StartDate { get; set; }

    }

    public class GetResultByTeamModel
    {
        public Nullable<int> packID { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> propresID { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public Nullable<int> NoOfMonth { get; set; }
        public PostTeam teams { get; set; }
        public String Time { get; set; }
    }

    public class GetResultForOtherTime
    {
        public Nullable<int> packID { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> propresID { get; set; }
        public Nullable<int> area { get; set; }
        public Nullable<int> subarea { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public Nullable<int> NoOfMonth { get; set; }
        public PostTeam teams { get; set; }
    }

    public class GetResultCheck
    {
        public Nullable<int> packID { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> propresID { get; set; }
        public Nullable<int> area { get; set; }
        public Nullable<int> subarea { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public Nullable<int> NoOfMonth { get; set; }
        public string MobileNo { get; set; }
        public PostTeam1 teams { get; set; }
    }

    public class GetBookedStartDates
    {
        public List<TimeRange> TimeRange { get; set; }
        public string StartDate { get; set; }
        public Nullable<bool> IsDateAvailable { get; set; }
    }

    public class GetReschedule
    {
        public List<GetBookedStartDates> GetBookedDates { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
    }

    public class SaveRescheduleModel 
    {
        public Nullable<int> cuID { get; set; }
        public Nullable<int> custODID { get; set; }
        public Nullable<int> custTDID { get; set; }
        public Nullable<int> packID { get; set; }
        public Nullable<int> parkID { get; set; }
        public Nullable<int> teamID { get; set; }
        public Nullable<DateTime> BeforeDate { get; set; }
        public string BeforeStartTime { get; set; }
        public string BeforeEndTime { get; set; }
        public Nullable<DateTime> RescheduleDate { get; set; }
        public string RescheduleStartTime { get; set; }
        public string RescheduleEndTime { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<int> UpdatedRole { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class GetBookedStartDates1
    {
        public List<StringTimeRange> TimeRange { get; set; }

        public string StartDate { get; set; }
    }

    public class StringTimeRange 
    {
        public string Start { get; set; }
        public string End { get; set; }
    }

    public class TempBookedStartDates
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
    }

    public class BookedStartDates
    {
        public List<string> Days { get; set; }
        public List<int> Teams { get; set; }
        public int? catID { get; set; }
        public int? catsubID { get; set; }
        public int? packID { get; set; }
        public int? propresID { get; set; }
    }

    public class ExistingBookedStartDates
    {
        public List<string> Days { get; set; }
        public Nullable<int> Teams { get; set; }
        public string Time { get; set; }
        public int? catID { get; set; }
        public int? catsubID { get; set; }
        public int? packID { get; set; }
        public int? propresID { get; set; }
    }

    public class BookedStartDates1
    {
        public int? catID { get; set; }
        public int? catsubID { get; set; }
        public int? packID { get; set; }
        public int? propresID { get; set; }
        public string Time { get; set; }
    }

    public class CustomerBookedStartDates
    {
        public int? catID { get; set; }
        public int? catsubID { get; set; }
        public int? packID { get; set; }
        public int? propresID { get; set; }
        public string Time { get; set; }
        public int? cuID { get; set; }
        public int? custODID { get; set; }
        public int? Duration { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public int? teamID { get; set; }
    }

    public class PostTeam
    {
        public string Days { get; set; }
        public List<int?> Teams { get; set; }
    }

    public class Bookings
    {
        public int? TeamID { get; set; }
        public int? Score { get; set; }
    }

    public class PostTeam1
    {
        public List<ConfirmTime> Time { get; set; }
        public List<int?> Teams { get; set; }
    }

    public class ConfirmTime
    {
        public TimeRange2 Times { get; set; }
        public string Days { get; set; }
    }

    public class TimeRange2
    {
        public string Start { get; set; }
        public string End { get; set; }
    }
}