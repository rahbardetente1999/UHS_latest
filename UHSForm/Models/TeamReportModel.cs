using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class TeamReportModel
    {

    }

    public class ServiceDetails
    {
        public Nullable<int> ServiceCount { get; set; }
        public Nullable<double> RatingCount { get; set; }
    }

    public class GetReportByServiceAndRating
    {
        public string TeamName { get; set; }
        public ServiceDetails RegularCleaning { get; set; }
        public ServiceDetails DeepCleaning { get; set; }
        public ServiceDetails SpecializeCleaning { get; set; }
        public ServiceDetails CarWashCleaning { get; set; }
    }
    public class GetTeamsAverageRatingForReport
    {
        public Nullable<double> RegularCleaning { get; set; }
        public Nullable<double> DeepCleaning { get; set; }
        public Nullable<double> SpecializedCleaning { get; set; }
        public Nullable<double> CarWashCleaning { get; set; }
        public string TeamName { get; set; }
    }
    public class GetTeamsServiceCount
    {
        public Nullable<int> RegularCleaning { get; set; }
        public Nullable<int> DeepCleaning { get; set; }
        public Nullable<int> SpecializeCleaning { get; set; }
        public Nullable<int> CarWash { get; set; }
        public string TeamName { get; set; }

    }

    public class GetTeamsServiceIndividualCount
    {
        public Nullable<int> Count { get; set; }
        public string TeamName { get; set; }
        public string Area { get; set; }
        public string SubArea { get; set; }
        public string TowerName { get; set; }
        public List<GetTeamsServiceIndividualTiming> Timings { get; set; }
    }

    public class GetTeamsServiceIndividualTiming
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }

    }

    public class GetTeamsByTower
    {
        public string[] TeamName { get; set; }
        public string Area { get; set; }
        public string SubArea { get; set; }
        public string TowerName { get; set; }

    }

    public class GetTeamsCountForTower
    {
        public string[] TeamName { get; set; }
        public string TowerName { get; set; }
        public string Propertyname { get; set; }
    }

    public class GetTeamAvailableByDate
    {
        public Nullable<int> Time { get; set; }
        public Nullable<int> uID { get; set; }
        public string Date { get; set; }
    }



    public class GetTeamReportModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string TeamID { get; set; }
        public string PropaID { get; set; }
        public string VentureID { get; set; }
        public int uID { get; set; }
    }


    public class GetRescheduleStatusList
    {
        public string CustomerName { get; set; }
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Message { get; set; }
    }


    public class GetTeamRoasterForTable
    {
        public string Teams { get; set; }
        public List<TeamRoasterDetails> TeamRoasterDetails { get; set; }
    }

    public class TeamRoasterDetails
    {

        public string Teams { get; set; }
        public Nullable<int> teamID { get; set; }
        public TimeSpan StartTime { get; set; }
        public string ServiceTimings { get; set; }
        public string Area { get; set; }
        public string Tower { get; set; }
        public string SubArea { get; set; }
        public string AppartmentName { get; set; }
        public string PropertyCode { get; set; }
        public string Service { get; set; }
        public string ServiceStatus { get; set; }
        public string LocationStatus { get; set; }
    }

    public class RoasterTeam
    {
        public string Teams { get; set; }
        public TimeSpan StartTime { get; set; }
        public string ServiceTimings { get; set; }
        public string Area { get; set; }
        public string Tower { get; set; }
        public string SubArea { get; set; }
        public string AppartmentName { get; set; }
        public string AppartmentType { get; set; }
        public string PropertyCode { get; set; }
        public string Service { get; set; }
        public string ServiceStatus { get; set; }
    }

    public class TeamRoaster
    {

        public string Teams { get; set; }
        public string FromArea { get; set; }
        public string FromSubArea { get; set; }
        public string FromTower { get; set; }
        public string FromAppartmentName { get; set; }
        public string FromServiceTime { get; set; }
        public string ToServiceTime { get; set; }
        public string ToArea { get; set; }
        public string ToSubArea { get; set; }
        public string ToTower { get; set; }
        public string ToAppartmentName { get; set; }

        public string ServiceStatus { get; set; }
        public string LocationStatus { get; set; }
    }


    public class TeamStaffDetails
    {
        public int teamID { get; set; }
        public string TeamName { get; set; }
        public List<StaffDetails> StaffDetails { get; set; }
    }
    public class StaffDetails
    {


        public int stfID { get; set; }
        public string StaffName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
    }



}