using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;
using System.Data.Entity.Core.Objects;

namespace UHSForm.DAL
{
    public class DriverAssignDB
    {
        private UHSEntities UhDB;
        private CommonCustomerTimeLineDB objCommonCustomerTimeLineDB;
        public DriverAssignDB()
        {
            UhDB = new UHSEntities();
            objCommonCustomerTimeLineDB = new CommonCustomerTimeLineDB();
        }

        public List<GrantChartReportModel> GetGrantChartForDriver(int? uID) 
        {
            List<GrantChartReportModel> result = new List<GrantChartReportModel>();
            var objTeams = UhDB.Teams.Where(x => x.IsActive == true && x.IsDelete == false && x.uID == uID).ToList();
            foreach (var objTeam in objTeams)
            {
                string TeamName = objTeam.Name;
                int? teamID = objTeam.teamID;
                List<AreaBased> objAreaBased = new List<AreaBased>();
                var objStaffServices = UhDB.StaffServices.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objStaffService in objStaffServices)
                {
                    int? catID = objStaffService.catID;
                    int? catsubID = objStaffService.catsubID;
                    string ServiceName = null;
                    if (catsubID == null)
                    {
                        ServiceName = objStaffService.MainCategory.Name;
                    }
                    else 
                    {
                        ServiceName = objStaffService.SubCategory.Name;
                    }
                    var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.CustomerOfficalDetail.catID == catID
                                               && x.CustomerOfficalDetail.catsubID == catsubID && x.IsActive == true && x.IsDelete == false
                                               && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).ToList();
                    
                    foreach (var objCustomerTimeline in objCustomerTimelines)
                    {
                        Times objTimes = new Times();
                        TimeSpan Start = objCommonCustomerTimeLineDB.ConvertToTimeSpans(objCustomerTimeline.StartTime);
                        TimeSpan End = objCommonCustomerTimeLineDB.ConvertToTimeSpans(objCustomerTimeline.EndTime);
                        objTimes.Start = Start;
                        objTimes.End = End;
                        string Area = objCustomerTimeline.CustomerOfficalDetail.PropertyArea.Name;
                        string SubArea = objCustomerTimeline.CustomerOfficalDetail.SubArea.Name;
                        string TowerName = objCustomerTimeline.CustomerOfficalDetail.Venture.Name;
                        string PropertyCode = objCustomerTimeline.CustomerOfficalDetail.Venture.Code;
                        objAreaBased.Add(new AreaBased { Time=objTimes,Area=Area, SubArea = SubArea, Service =ServiceName, TowerName= TowerName, PropertyCode = PropertyCode});
                    }

                }
                result.Add(new GrantChartReportModel {Team=TeamName,AreaBased=objAreaBased });

            }
            return result;
        }

        public List<GrantChartReportModel> GetGrantChartForDriverWithDate(int? uID,DateTime? Date)
        {
            List<GrantChartReportModel> result = new List<GrantChartReportModel>();
            var objTeams = UhDB.Teams.Where(x => x.IsActive == true && x.IsDelete == false && x.uID == uID).ToList();
            foreach (var objTeam in objTeams)
            {
                string TeamName = objTeam.Name;
                int? teamID = objTeam.teamID;
                List<AreaBased> objAreaBased = new List<AreaBased>();
                var objStaffServices = UhDB.StaffServices.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objStaffService in objStaffServices)
                {
                    int? catID = objStaffService.catID;
                    int? catsubID = objStaffService.catsubID;
                    string ServiceName = null;
                    if (catsubID == null)
                    {
                        ServiceName = objStaffService.MainCategory.Name;
                    }
                    else
                    {
                        ServiceName = objStaffService.SubCategory.Name;
                    }
                    DateTime OnDate = Convert.ToDateTime(Date);
                    var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.CustomerOfficalDetail.catID == catID
                                               && x.CustomerOfficalDetail.catsubID == catsubID && x.IsActive == true && x.IsDelete == false
                                               && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(OnDate)).ToList();

                    foreach (var objCustomerTimeline in objCustomerTimelines)
                    {
                        Times objTimes = new Times();
                        TimeSpan Start = objCommonCustomerTimeLineDB.ConvertToTimeSpans(objCustomerTimeline.StartTime);
                        TimeSpan End = objCommonCustomerTimeLineDB.ConvertToTimeSpans(objCustomerTimeline.EndTime);
                        objTimes.Start = Start;
                        objTimes.End = End;
                        string Area = objCustomerTimeline.CustomerOfficalDetail.PropertyArea.Name;
                        string SubArea = objCustomerTimeline.CustomerOfficalDetail.SubArea.Name;
                        string TowerName = objCustomerTimeline.CustomerOfficalDetail.Venture.Name;
                        string PropertyCode = objCustomerTimeline.CustomerOfficalDetail.Venture.Code;
                        objAreaBased.Add(new AreaBased { Time = objTimes, Area = Area,SubArea = SubArea, Service = ServiceName, TowerName = TowerName, PropertyCode = PropertyCode });
                    }

                }
                result.Add(new GrantChartReportModel { Team = TeamName, AreaBased = objAreaBased });

            }
            return result;
        }
    }
}