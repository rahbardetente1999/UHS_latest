using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.DAL;
using UHSForm.Models.Data;
using UHSForm.Models;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Globalization;

namespace UHSForm.DAL
{
    public class TeamReportDB
    {
        private UHSEntities UhDB;
        private GeneralDB objGeneralDB;
        private CommonCustomerTimeLineDB objCommonCustomerTimeLineDB;

        public TeamReportDB()
        {
            UhDB = new UHSEntities();
            objCommonCustomerTimeLineDB = new CommonCustomerTimeLineDB();
            objGeneralDB = new GeneralDB();
        }

        public List<GetReportByServiceAndRating> GetReportForAverageRatingAndServiceCountForTeams(int? uID)
        {
            List<GetReportByServiceAndRating> result = new List<GetReportByServiceAndRating>();
            double? RegularCleaningRating = 0, DeepCleaningRating = 0, SpecializeCleaningRating = 0, CarWashCleaningRating = 0;
            int RegularCleaingCustomerCount = 0, DeepCleaningCustomerCount = 0, SpecializeCleaningCustomerCount = 0, CarWashCustomerCount = 0;
            DateTime TodayDate = DateTime.Now;
            var objTeams = UhDB.Teams.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objTeam in objTeams)
            {
                GetReportByServiceAndRating teamReport = new GetReportByServiceAndRating { TeamName = objTeam.Name, RegularCleaning = new ServiceDetails(), DeepCleaning = new ServiceDetails(), SpecializeCleaning = new ServiceDetails(), CarWashCleaning = new ServiceDetails() };
                int? teamID = objTeam.teamID;

                //RegularCleaningServiceCount
                var objRegularCleaningCustomersForTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 1 && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(TodayDate)).ToList();
                int CountRegularCleaning = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 1
                            && x.IsActive == true && x.IsDelete == false && x.teamID == teamID && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(TodayDate)).Count();

                if (CountRegularCleaning != 0)
                {
                    var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 1
                                           && x.IsActive == true && x.IsDelete == false && x.teamID == teamID && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(TodayDate)).ToList();
                    teamReport.RegularCleaning.ServiceCount = objCustomerTimelines.Count();
                }
                else
                {
                    teamReport.RegularCleaning.ServiceCount = 0;
                }

                //RegularCleaningRating
                foreach (var objRegularCleaningCustomersForTeam in objRegularCleaningCustomersForTeams)
                {
                    int? cuID = objRegularCleaningCustomersForTeam.custID;
                    int? custODID = objRegularCleaningCustomersForTeam.custODID;
                    int? custTdID = objRegularCleaningCustomersForTeam.custTDID;
                    int CountCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID == custTdID && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountCustomerRating != 0)
                    {
                        var objCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID == custTdID && x.IsActive == true && x.IsDelete == false).OrderByDescending(y => y.custfdbID).FirstOrDefault();
                        RegularCleaningRating = objCustomerRating.Rating + RegularCleaningRating;
                        RegularCleaingCustomerCount = RegularCleaingCustomerCount + 1;
                    }
                }

                //DeepCleaningService
                int CountDeepCleaning = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 2
                                        && x.IsActive == true && x.IsDelete == false && x.teamID == teamID && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(TodayDate)).Count();
                if (CountDeepCleaning != 0)
                {
                    var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 2
                                            && x.IsActive == true && x.IsDelete == false && x.teamID == teamID && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(TodayDate)).ToList();
                    teamReport.DeepCleaning.ServiceCount = objCustomerTimelines.Count();
                }
                else
                {
                    teamReport.DeepCleaning.ServiceCount = 0;
                }

                //DeepCleaningRating
                var objDeepCleaningRatingCustomersForTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 2 && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(TodayDate)).ToList();
                foreach (var objDeepCleaningRatingCustomersForTeam in objDeepCleaningRatingCustomersForTeams)
                {
                    int? cuID = objDeepCleaningRatingCustomersForTeam.custID;
                    int? custODID = objDeepCleaningRatingCustomersForTeam.custODID;
                    int? custTdID = objDeepCleaningRatingCustomersForTeam.custTDID;
                    int CountCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID == custTdID && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountCustomerRating != 0)
                    {
                        var objCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID == custTdID && x.IsActive == true && x.IsDelete == false).OrderByDescending(y => y.custfdbID).FirstOrDefault();
                        DeepCleaningRating = objCustomerRating.Rating + DeepCleaningRating;
                        DeepCleaningCustomerCount = DeepCleaningCustomerCount + 1;
                    }
                }

                //SpecializedService
                int SpecializeCount = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 3
                                      && x.IsActive == true && x.IsDelete == false && x.teamID == teamID && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(TodayDate)).Count();
                if (SpecializeCount != 0)
                {
                    var objCustomerSpecializedCleanings = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 3
                                                          && x.IsActive == true && x.IsDelete == false && x.teamID == teamID && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(TodayDate)).ToList();
                    teamReport.SpecializeCleaning.ServiceCount = objCustomerSpecializedCleanings.Count();

                }
                else
                {
                    teamReport.SpecializeCleaning.ServiceCount = 0;
                }

                //SpecializeCleaningRating
                var objSpecializeCleaningRatingCustomersForTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 3 && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(TodayDate)).ToList();
                foreach (var objSpecializeCleaningRatingCustomersForTeam in objSpecializeCleaningRatingCustomersForTeams)
                {
                    int? cuID = objSpecializeCleaningRatingCustomersForTeam.custID;
                    int? custODID = objSpecializeCleaningRatingCustomersForTeam.custODID;
                    int? custTdID = objSpecializeCleaningRatingCustomersForTeam.custTDID;
                    int CountCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID == custTdID && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountCustomerRating != 0)
                    {
                        var objCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID == custTdID && x.IsActive == true && x.IsDelete == false).OrderByDescending(y => y.custfdbID).FirstOrDefault();
                        SpecializeCleaningRating = objCustomerRating.Rating + SpecializeCleaningRating;
                        SpecializeCleaningCustomerCount = SpecializeCleaningCustomerCount + 1;
                    }
                }

                int CarWashCount = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 2
                          && x.IsActive == true && x.IsDelete == false && x.teamID == teamID && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(TodayDate)).Count();
                if (CarWashCount != 0)
                {
                    var objCustomerCarWashCleanings = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 2
                                                          && x.IsActive == true && x.IsDelete == false && x.teamID == teamID && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(TodayDate)).ToList();
                    teamReport.CarWashCleaning.ServiceCount = objCustomerCarWashCleanings.Count();
                }
                else
                {
                    teamReport.CarWashCleaning.ServiceCount = 0;
                }

                //CarWashCleaningRating
                var objCarWashRatingCustomersForTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.CustomerOfficalDetail.catID == 2 && x.CustomerOfficalDetail.catsubID == null && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(TodayDate)).ToList();
                foreach (var objCarWashRatingCustomersForTeam in objCarWashRatingCustomersForTeams)
                {
                    int? cuID = objCarWashRatingCustomersForTeam.custID;
                    int? custODID = objCarWashRatingCustomersForTeam.custODID;
                    int? custTdID = objCarWashRatingCustomersForTeam.custTDID;
                    int CountCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID == custTdID && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountCustomerRating != 0)
                    {
                        var objCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID == custTdID && x.IsActive == true && x.IsDelete == false).OrderByDescending(y => y.custfdbID).FirstOrDefault();
                        CarWashCleaningRating = objCustomerRating.Rating + CarWashCleaningRating;
                        CarWashCustomerCount = CarWashCustomerCount + 1;
                    }
                }
                if (RegularCleaingCustomerCount != 0)
                {
                    teamReport.RegularCleaning.RatingCount = RegularCleaningRating / RegularCleaingCustomerCount;
                }
                if (DeepCleaningCustomerCount != 0)
                {
                    teamReport.DeepCleaning.RatingCount = DeepCleaningRating / DeepCleaningCustomerCount;
                }
                if (SpecializeCleaningCustomerCount != 0)
                {
                    teamReport.SpecializeCleaning.RatingCount = SpecializeCleaningRating / SpecializeCleaningCustomerCount;
                }
                if (CarWashCustomerCount != 0)
                {
                    teamReport.CarWashCleaning.RatingCount = CarWashCleaningRating / CarWashCustomerCount;
                }
                result.Add(teamReport);
            }
            return result;
        }

        public List<GetTeamsAverageRatingForReport> GetAverageRatingForTeams(int? uID)
        {
            List<GetTeamsAverageRatingForReport> result = new List<GetTeamsAverageRatingForReport>();

            double? RegularCleaningRating = 0, DeepCleaningRating = 0, SpecializeCleaningRating = 0, CarWashCleaningRating = 0;
            int RegularCleaingCustomerCount = 0, DeepCleaningCustomerCount = 0, SpecializeCleaningCustomerCount = 0, CarWashCustomerCount = 0;
            var objTeams = UhDB.Teams.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objTeam in objTeams)
            {
                GetTeamsAverageRatingForReport objGetTeamsAverageRatingForReport = new GetTeamsAverageRatingForReport();
                int? teamID = objTeam.teamID;
                var objRegularCleaningCustomersForTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 1 && x.IsActive == true && x.IsDelete == false).ToList();

                foreach (var objRegularCleaningCustomersForTeam in objRegularCleaningCustomersForTeams)
                {
                    int? cuID = objRegularCleaningCustomersForTeam.custID;
                    int? custODID = objRegularCleaningCustomersForTeam.custODID;
                    int? custTdID = objRegularCleaningCustomersForTeam.custTDID;
                    int CountCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID == custTdID && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountCustomerRating != 0)
                    {
                        var objCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID == custTdID && x.IsActive == true && x.IsDelete == false).OrderByDescending(y => y.custfdbID).FirstOrDefault();
                        RegularCleaningRating = objCustomerRating.Rating + RegularCleaningRating;
                        RegularCleaingCustomerCount = RegularCleaingCustomerCount + 1;
                    }
                }


                var objDeepCleaningRatingCustomersForTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 2 && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objDeepCleaningRatingCustomersForTeam in objDeepCleaningRatingCustomersForTeams)
                {
                    int? cuID = objDeepCleaningRatingCustomersForTeam.custID;
                    int? custODID = objDeepCleaningRatingCustomersForTeam.custODID;
                    int? custTdID = objDeepCleaningRatingCustomersForTeam.custTDID;
                    int CountCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID == custTdID && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountCustomerRating != 0)
                    {
                        var objCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID == custTdID && x.IsActive == true && x.IsDelete == false).OrderByDescending(y => y.custfdbID).FirstOrDefault();
                        DeepCleaningRating = objCustomerRating.Rating + DeepCleaningRating;
                        DeepCleaningCustomerCount = DeepCleaningCustomerCount + 1;
                    }
                }
                var objSpecializeCleaningRatingCustomersForTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 3 && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objSpecializeCleaningRatingCustomersForTeam in objSpecializeCleaningRatingCustomersForTeams)
                {
                    int? cuID = objSpecializeCleaningRatingCustomersForTeam.custID;
                    int? custODID = objSpecializeCleaningRatingCustomersForTeam.custODID;
                    int? custTdID = objSpecializeCleaningRatingCustomersForTeam.custTDID;
                    int CountCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID == custTdID && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountCustomerRating != 0)
                    {
                        var objCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID == custTdID && x.IsActive == true && x.IsDelete == false).OrderByDescending(y => y.custfdbID).FirstOrDefault();
                        SpecializeCleaningRating = objCustomerRating.Rating + SpecializeCleaningRating;
                        SpecializeCleaningCustomerCount = SpecializeCleaningCustomerCount + 1;
                    }
                }
                var objCarWashRatingCustomersForTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.CustomerOfficalDetail.catID == 2 && x.CustomerOfficalDetail.catsubID == null && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objCarWashRatingCustomersForTeam in objCarWashRatingCustomersForTeams)
                {
                    int? cuID = objCarWashRatingCustomersForTeam.custID;
                    int? custODID = objCarWashRatingCustomersForTeam.custODID;
                    int? custTdID = objCarWashRatingCustomersForTeam.custTDID;
                    int CountCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID == custTdID && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountCustomerRating != 0)
                    {
                        var objCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID == custTdID && x.IsActive == true && x.IsDelete == false).OrderByDescending(y => y.custfdbID).FirstOrDefault();
                        CarWashCleaningRating = objCustomerRating.Rating + CarWashCleaningRating;
                        CarWashCustomerCount = CarWashCustomerCount + 1;
                    }
                }
                objGetTeamsAverageRatingForReport.TeamName = objTeam.Name;
                if (RegularCleaingCustomerCount != 0)
                {
                    objGetTeamsAverageRatingForReport.RegularCleaning = RegularCleaningRating / RegularCleaingCustomerCount;
                }
                if (DeepCleaningCustomerCount != 0)
                {
                    objGetTeamsAverageRatingForReport.DeepCleaning = DeepCleaningRating / DeepCleaningCustomerCount;
                }
                if (SpecializeCleaningCustomerCount != 0)
                {
                    objGetTeamsAverageRatingForReport.SpecializedCleaning = SpecializeCleaningRating / SpecializeCleaningCustomerCount;
                }
                if (CarWashCustomerCount != 0)
                {
                    objGetTeamsAverageRatingForReport.CarWashCleaning = CarWashCleaningRating / CarWashCustomerCount;
                }
                result.Add(objGetTeamsAverageRatingForReport);
            }
            return result;
        }

        public List<GetTeamsServiceCount> GetTeamsServiceCount(int? uID)
        {
            List<GetTeamsServiceCount> result = new List<GetTeamsServiceCount>();
            var objTeams = UhDB.Teams.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objTeam in objTeams)
            {
                GetTeamsServiceCount objGetTeamsServiceCount = new GetTeamsServiceCount();
                int? teamID = objTeam.teamID;
                objGetTeamsServiceCount.TeamName = objTeam.Name;
                int CountRegularCleaning = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 1
                            && x.IsActive == true && x.IsDelete == false && x.teamID == teamID).Count();
                if (CountRegularCleaning != 0)
                {
                    var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 1
                                           && x.IsActive == true && x.IsDelete == false && x.teamID == teamID).ToList();
                    objGetTeamsServiceCount.RegularCleaning = objCustomerTimelines.Count();
                }

                else
                {
                    objGetTeamsServiceCount.RegularCleaning = 0;
                }

                int CountDeepCleaning = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 2
                                        && x.IsActive == true && x.IsDelete == false && x.teamID == teamID).Count();
                if (CountDeepCleaning != 0)
                {
                    var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 2
                                           && x.IsActive == true && x.IsDelete == false && x.teamID == teamID).ToList();
                    objGetTeamsServiceCount.DeepCleaning = objCustomerTimelines.Count();
                }
                else
                {
                    objGetTeamsServiceCount.DeepCleaning = 0;
                }

                int SpecializeCount = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 3
                                      && x.IsActive == true && x.IsDelete == false && x.teamID == teamID).Count();
                if (SpecializeCount != 0)
                {
                    var objCustomerSpecializedCleanings = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 3
                                                          && x.IsActive == true && x.IsDelete == false && x.teamID == teamID).ToList();
                    objGetTeamsServiceCount.SpecializeCleaning = objCustomerSpecializedCleanings.Count();

                }
                else
                {
                    objGetTeamsServiceCount.SpecializeCleaning = 0;
                }

                int CarWashCount = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 2
                          && x.IsActive == true && x.IsDelete == false && x.teamID == teamID).Count();
                if (CarWashCount != 0)
                {
                    var objCustomerCarWashCleanings = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 2
                                                          && x.IsActive == true && x.IsDelete == false && x.teamID == teamID).ToList();
                    objGetTeamsServiceCount.CarWash = objCustomerCarWashCleanings.Count();

                }
                else
                {
                    objGetTeamsServiceCount.CarWash = 0;
                }
                result.Add(objGetTeamsServiceCount);
            }
            return result;
        }

        public List<GetTeamsServiceIndividualCount> GetTeamsServiceIndividualCount(int? uID)
        {
            List<GetTeamsServiceIndividualCount> result = new List<GetTeamsServiceIndividualCount>();
            var objTeams = UhDB.Teams.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).ToList();
            var objVentures = UhDB.Ventures.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).ToList();

            foreach (var objTeam in objTeams)
            {
                GetTeamsServiceIndividualCount objGetTeamsServiceIndividualCount = new GetTeamsServiceIndividualCount();
                int? teamID = objTeam.teamID;
                var objTeamServices = UhDB.StaffServices.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objTeamService in objTeamServices)
                {
                    foreach (var objVenture in objVentures)
                    {
                        int? catID = objTeamService.catID;
                        int? catsubID = objTeamService.catsubID;
                        int? vID = objVenture.vID;
                        DateTime TodaysDate = DateTime.Now;
                        List<GetTeamsServiceIndividualTiming> objCustomerTimeTineLines = new List<GetTeamsServiceIndividualTiming>();
                        int CountCustomerTimeTineLines = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID
                                                         && x.CustomerOfficalDetail.vID == vID && x.teamID == teamID && x.IsActive == true
                                                         && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(TodaysDate)).Count();
                        if (CountCustomerTimeTineLines != 0)
                        {
                            objCustomerTimeTineLines = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID
                                                       && x.CustomerOfficalDetail.vID == vID && x.teamID == teamID && x.IsActive == true
                                                       && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(TodaysDate)).Select(p => new GetTeamsServiceIndividualTiming { StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                        }
                        result.Add(new Models.GetTeamsServiceIndividualCount
                        {
                            Area = objVenture.PropertyArea.Name,
                            SubArea = objVenture.SubArea.Name,
                            TowerName = objVenture.Name,
                            TeamName = objTeam.Name,
                            Count = objCustomerTimeTineLines.Count(),
                            Timings = objCustomerTimeTineLines
                        });

                    }
                }


            }

            return result;
        }

        public List<GetTeamsServiceIndividualCount> GetTeamsServiceIndividualCountByDate(int? uID, DateTime Date)
        {
            List<GetTeamsServiceIndividualCount> result = new List<GetTeamsServiceIndividualCount>();
            var objTeams = UhDB.Teams.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).ToList();
            var objVentures = UhDB.Ventures.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).ToList();

            foreach (var objTeam in objTeams)
            {
                GetTeamsServiceIndividualCount objGetTeamsServiceIndividualCount = new GetTeamsServiceIndividualCount();
                int? teamID = objTeam.teamID;
                var objTeamServices = UhDB.StaffServices.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objTeamService in objTeamServices)
                {
                    foreach (var objVenture in objVentures)
                    {
                        int? catID = objTeamService.catID;
                        int? catsubID = objTeamService.catsubID;
                        int? vID = objVenture.vID;
                        DateTime ToDate = Convert.ToDateTime(Date);
                        var objCustomerTimeTineLines = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID
                                                       && x.CustomerOfficalDetail.vID == vID && x.teamID == teamID && x.IsActive == true
                                                       && x.IsDelete == false).Select(p => new GetTeamsServiceIndividualTiming { StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                        result.Add(new Models.GetTeamsServiceIndividualCount
                        {
                            Area = objVenture.PropertyArea.Name,
                            SubArea = objVenture.SubArea.Name,
                            TowerName = objVenture.Name,
                            TeamName = objTeam.Name,
                            Count = objCustomerTimeTineLines.Count(),
                            Timings = objCustomerTimeTineLines
                        });

                    }
                }


            }

            return result;
        }

        public List<GetTeamsByTower> GetTeamsCountByToday(int? uID)
        {
            List<GetTeamsByTower> result = new List<GetTeamsByTower>();
            var objVentures = UhDB.Ventures.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objVenture in objVentures)
            {
                int? vID = objVenture.vID;
                DateTime TodayDate = DateTime.Now;
                List<string> TeamNames = new List<string>();
                int CountCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.IsActive == true && x.IsDelete == false && x.CustomerOfficalDetail.vID == vID && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(TodayDate)).Count();
                if (CountCustomerTimeLines != 0)
                {
                    TeamNames = UhDB.CustomerTimelines.Where(x => x.IsActive == true && x.IsDelete == false && x.CustomerOfficalDetail.vID == vID && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(TodayDate)).Select(p => p.Team.Name).ToList();

                }
                result.Add(new GetTeamsByTower { TeamName = TeamNames.Distinct().ToArray(), TowerName = objVenture.Name, Area = objVenture.PropertyArea.Name, SubArea = objVenture.SubArea.Name });
            }
            return result;
        }

        public List<GetTeamsByTower> GetTeamsCountForTowerByDate(int? uID, DateTime Date)
        {
            List<GetTeamsByTower> result = new List<GetTeamsByTower>();
            var objVentures = UhDB.Ventures.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objVenture in objVentures)
            {
                int? vID = objVenture.vID;
                List<string> TeamNames = new List<string>();
                int CountCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.IsActive == true && x.IsDelete == false && x.CustomerOfficalDetail.vID == vID && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(Date)).Count();
                if (CountCustomerTimeLines != 0)
                {
                    TeamNames = UhDB.CustomerTimelines.Where(x => x.IsActive == true && x.IsDelete == false && x.CustomerOfficalDetail.vID == vID && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(Date)).Select(p => p.Team.Name).ToList();

                }
                result.Add(new GetTeamsByTower { TeamName = TeamNames.Distinct().ToArray(), TowerName = objVenture.Name, Area = objVenture.PropertyArea.Name, SubArea = objVenture.SubArea.Name });
            }
            return result;
        }

        public List<GetTeamsCountForTower> GetTeamsCountForTower(int? uID, string pID, string tID)
        {
            List<GetTeamsCountForTower> result = new List<GetTeamsCountForTower>();
            var objVentures = UhDB.Ventures.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).ToList();


            if (pID == "All")
            {
                var objPropertyAreas = UhDB.PropertyAreas.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objPropertyArea in objPropertyAreas)
                {
                    int? propaID = objPropertyArea.propaID;
                    string PropertyArea = objPropertyArea.Name;
                    if (tID == "All")
                    {
                        foreach (var objVenture in objVentures)
                        {
                            GetTeamsCountForTower objGetTeamsCountForTower = new GetTeamsCountForTower();
                            int? vID = objVenture.vID;
                            var objCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.IsActive == true && x.IsDelete == false && x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID).ToList();
                            List<GetDropDown> objTeams = new List<GetDropDown>();
                            foreach (var objCustomerTimeLine in objCustomerTimeLines)
                            {
                                if (objCustomerTimeLine.teamID != null)
                                {
                                    if (!objTeams.Any(x => x.ID == objCustomerTimeLine.teamID))
                                    {
                                        objTeams.Add(new GetDropDown { ID = objCustomerTimeLine.teamID, Value = objCustomerTimeLine.Team.Name });
                                    }
                                }
                            }
                            objGetTeamsCountForTower.TeamName = objTeams.Select(x => x.Value).ToArray();
                            objGetTeamsCountForTower.TowerName = objVenture.Name;
                            objGetTeamsCountForTower.Propertyname = PropertyArea;
                            result.Add(objGetTeamsCountForTower);
                        }
                    }
                    else
                    {
                        GetTeamsCountForTower objGetTeamsCountForTower = new GetTeamsCountForTower();
                        int? vID = Convert.ToInt32(tID);
                        string TName = UhDB.Ventures.Where(x => x.vID == vID && x.IsActive == true && x.IsDelete == false).Select(p => p.Name).SingleOrDefault();
                        var objCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.IsActive == true && x.IsDelete == false && x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID).ToList();
                        List<GetDropDown> objTeams = new List<GetDropDown>();
                        foreach (var objCustomerTimeLine in objCustomerTimeLines)
                        {
                            if (objCustomerTimeLine.teamID != null)
                            {
                                if (!objTeams.Any(x => x.ID == objCustomerTimeLine.teamID))
                                {
                                    objTeams.Add(new GetDropDown { ID = objCustomerTimeLine.teamID, Value = objCustomerTimeLine.Team.Name });
                                }
                            }
                        }
                        objGetTeamsCountForTower.TeamName = objTeams.Select(x => x.Value).ToArray();
                        objGetTeamsCountForTower.Propertyname = PropertyArea;
                        objGetTeamsCountForTower.TowerName = TName;
                        result.Add(objGetTeamsCountForTower);
                    }
                }

            }
            else
            {
                int? propaID = Convert.ToInt32(pID);
                if (tID == "All")
                {
                    foreach (var objVenture in objVentures)
                    {
                        GetTeamsCountForTower objGetTeamsCountForTower = new GetTeamsCountForTower();
                        int? vID = objVenture.vID;
                        var objCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.IsActive == true && x.IsDelete == false && x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID).ToList();
                        List<GetDropDown> objTeams = new List<GetDropDown>();
                        foreach (var objCustomerTimeLine in objCustomerTimeLines)
                        {
                            if (objCustomerTimeLine.teamID != null)
                            {
                                if (!objTeams.Any(x => x.ID == objCustomerTimeLine.teamID))
                                {
                                    objTeams.Add(new GetDropDown { ID = objCustomerTimeLine.teamID, Value = objCustomerTimeLine.Team.Name });
                                }
                            }
                        }
                        objGetTeamsCountForTower.TeamName = objTeams.Select(x => x.Value).ToArray();
                        objGetTeamsCountForTower.TowerName = objVenture.Name;
                        result.Add(objGetTeamsCountForTower);
                    }
                }
                else
                {
                    GetTeamsCountForTower objGetTeamsCountForTower = new GetTeamsCountForTower();
                    int? vID = Convert.ToInt32(tID);
                    string TName = UhDB.Ventures.Where(x => x.vID == vID && x.IsActive == true && x.IsDelete == false).Select(p => p.Name).SingleOrDefault();
                    var objCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.IsActive == true && x.IsDelete == false && x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID).ToList();
                    List<GetDropDown> objTeams = new List<GetDropDown>();
                    foreach (var objCustomerTimeLine in objCustomerTimeLines)
                    {
                        if (objCustomerTimeLine.teamID != null)
                        {
                            if (!objTeams.Any(x => x.ID == objCustomerTimeLine.teamID))
                            {
                                objTeams.Add(new GetDropDown { ID = objCustomerTimeLine.teamID, Value = objCustomerTimeLine.Team.Name });
                            }
                        }
                    }
                    objGetTeamsCountForTower.TeamName = objTeams.Select(x => x.Value).ToArray();
                    objGetTeamsCountForTower.TowerName = TName;
                    result.Add(objGetTeamsCountForTower);
                }

            }
            return result;
        }

        public List<TeamRoasterDetails> GetTeamRoasterForTable(int? uID)
        {
            List<TeamRoasterDetails> objTeamRoasterDetails = new List<TeamRoasterDetails>();


            var objTeams = UhDB.Teams.Where(x => x.IsActive == true && x.IsDelete == false && x.uID == uID).ToList();
            foreach (var objTeam in objTeams)
            {
                List<TeamRoasterDetails> objTeamRoaster = new List<TeamRoasterDetails>();
                string TeamName = objTeam.Name;
                int? teamID = objTeam.teamID;
                DateTime TodayDate = DateTime.Now;
                var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(TodayDate)).ToList();
                foreach (var item in objCustomerTimelines)
                {
                    string TowerName = item.CustomerOfficalDetail.propType == 1 ? item.CustomerOfficalDetail.Venture.Name :
                                     UhDB.CustomerOtherProperties.Where(x => x.custID == item.custID && x.custODID == item.custODID).FirstOrDefault().TowerName;
                    objTeamRoaster.Add(new TeamRoasterDetails { Teams = TeamName, ServiceTimings = item.StartTime + " - " + item.EndTime, Area = item.CustomerOfficalDetail.PropertyArea.Name, SubArea = item.CustomerOfficalDetail.SubArea.Name, Tower = TowerName, StartTime = ConvertToTimeSpans(item.StartTime),AppartmentName=item.CustomerOfficalDetail.AppartmentNumber,teamID=item.teamID });
                }

                objTeamRoaster = objTeamRoaster.OrderBy(x => x.StartTime).ToList();
                for (int i = 0; i < objTeamRoaster.Count; i++)
                {
                    if (i <= objTeamRoaster.Count - 1)
                    {
                        if (i == 0)
                        {
                            objTeamRoaster[i].LocationStatus = "Same";
                            objTeamRoasterDetails.Add(objTeamRoaster[i]);
                        }
                        else if (objTeamRoaster[i - 1].Area != objTeamRoaster[i].Area
                            || objTeamRoaster[i - 1].SubArea != objTeamRoaster[i].SubArea ||
                            objTeamRoaster[i - 1].Tower != objTeamRoaster[i].Tower || 
                            objTeamRoaster[i - 1].AppartmentName != objTeamRoaster[i].AppartmentName)
                        {
                            objTeamRoasterDetails.Add(new TeamRoasterDetails
                            {
                                Teams = objTeamRoaster[i].Teams,
                                ServiceTimings = objTeamRoaster[i].ServiceTimings,
                                Tower = objTeamRoaster[i].Tower,
                                Area = objTeamRoaster[i].Area,
                                SubArea = objTeamRoaster[i].SubArea,
                                AppartmentName=objTeamRoaster[i].AppartmentName,
                                LocationStatus = "Different",
                                teamID = objTeamRoaster[i].teamID
                            });
                            objTeamRoasterDetails.Add(new TeamRoasterDetails
                            {
                                Teams = objTeamRoaster[i - 1].Teams,
                                ServiceTimings = objTeamRoaster[i - 1].ServiceTimings,
                                Tower = objTeamRoaster[i - 1].Tower,
                                Area = objTeamRoaster[i - 1].Area,
                                SubArea = objTeamRoaster[i - 1].SubArea,
                                AppartmentName = objTeamRoaster[i].AppartmentName,
                                LocationStatus = "Different",
                                ServiceStatus = "Pervious",
                                teamID = objTeamRoaster[i].teamID
                            });
                        }
                        else
                        {
                            objTeamRoaster[i].LocationStatus = "Same";
                            objTeamRoasterDetails.Add(objTeamRoaster[i]);
                        }
                    }
                }
            }
            return objTeamRoasterDetails;
        }

        public List<TeamRoasterDetails> GetTeamRoasterForTableByDate(int? uID, DateTime? Date)
        {
            List<TeamRoasterDetails> objTeamRoasterDetails = new List<TeamRoasterDetails>();


            var objTeams = UhDB.Teams.Where(x => x.IsActive == true && x.IsDelete == false && x.uID == uID).ToList();
            foreach (var objTeam in objTeams)
            {
                List<TeamRoasterDetails> objTeamRoaster = new List<TeamRoasterDetails>();
                string TeamName = objTeam.Name;
                int? teamID = objTeam.teamID;
                DateTime StartDate = Convert.ToDateTime(Date);
                var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(StartDate)).ToList();
                foreach (var item in objCustomerTimelines)
                {
                    string TowerName = item.CustomerOfficalDetail.propType == 1 ? item.CustomerOfficalDetail.Venture.Name :
                                     UhDB.CustomerOtherProperties.Where(x => x.custID == item.custID && x.custODID == item.custODID).FirstOrDefault().TowerName;
                    objTeamRoaster.Add(new TeamRoasterDetails { Teams = TeamName, ServiceTimings = item.StartTime + " - " + item.EndTime, Area = item.CustomerOfficalDetail.PropertyArea.Name, SubArea = item.CustomerOfficalDetail.SubArea.Name, Tower = TowerName, StartTime = ConvertToTimeSpans(item.StartTime),AppartmentName=item.CustomerOfficalDetail.AppartmentNumber,teamID=item.teamID });
                }
                objTeamRoaster = objTeamRoaster.OrderBy(x => x.StartTime).ToList();
                for (int i = 0; i < objTeamRoaster.Count; i++)
                {
                    if (i <= objTeamRoaster.Count - 1)
                    {
                        if (i == 0)
                        {
                            objTeamRoaster[i].LocationStatus = "Same";
                            objTeamRoasterDetails.Add(objTeamRoaster[i]);
                        }
                        else if (objTeamRoaster[i - 1].Area != objTeamRoaster[i].Area
                            || objTeamRoaster[i - 1].SubArea != objTeamRoaster[i].SubArea ||
                            objTeamRoaster[i - 1].Tower != objTeamRoaster[i].Tower
                            || objTeamRoaster[i - 1].AppartmentName != objTeamRoaster[i].AppartmentName)
                        {
                            objTeamRoasterDetails.Add(new TeamRoasterDetails
                            {
                                Teams = objTeamRoaster[i].Teams,
                                ServiceTimings = objTeamRoaster[i].ServiceTimings,
                                Tower = objTeamRoaster[i].Tower,
                                Area = objTeamRoaster[i].Area,
                                SubArea = objTeamRoaster[i].SubArea,
                                AppartmentName=objTeamRoaster[i].AppartmentName,
                                LocationStatus = "Different",
                                teamID = objTeamRoaster[i].teamID
                            });
                            objTeamRoasterDetails.Add(new TeamRoasterDetails
                            {
                                Teams = objTeamRoaster[i - 1].Teams,
                                ServiceTimings = objTeamRoaster[i - 1].ServiceTimings,
                                Tower = objTeamRoaster[i - 1].Tower,
                                Area = objTeamRoaster[i - 1].Area,
                                SubArea = objTeamRoaster[i - 1].SubArea,
                                AppartmentName = objTeamRoaster[i].AppartmentName,
                                LocationStatus = "Different",
                                ServiceStatus = "Pervious",
                                teamID = objTeamRoaster[i].teamID
                            });
                        }
                        else
                        {
                            objTeamRoaster[i].LocationStatus = "Same";
                            objTeamRoasterDetails.Add(objTeamRoaster[i]);
                        }
                    }
                }
            }
            return objTeamRoasterDetails;
        }

        public List<TeamRoaster> GetTeamRoasters(int? uID)
        {
            List<TeamRoaster> result = new List<TeamRoaster>();

            var objGetTeamRoasterForTableBy = GetTeamRoasterForTable(uID);
            TimeSpan? SafeParseTime(string time)
            {
                return TimeSpan.TryParse(time, out var result1) ? result1 : (TimeSpan?)null;
            }

            objGetTeamRoasterForTableBy = objGetTeamRoasterForTableBy.OrderBy(x => x.teamID).ThenBy(x=>x.StartTime).ToList();
            
            // Filter and group previous start times
            var previousStartTimesGrouped = objGetTeamRoasterForTableBy
           .Where(x => x.LocationStatus == "Different" && x.ServiceStatus == "Pervious")
           .Select(x => new
           {
               Team = x.Teams,
               FromArea = x.Area,
               FromSubArea = x.SubArea,
               FromTower = x.Tower,
               FromAppartmentName=x.AppartmentName,
               FromStartTime = ConvertToTimeSpans1(x.ServiceTimings.Split('-')[0].Trim()),
               FromEndTime= ConvertToTimeSpans1(x.ServiceTimings.Split('-')[1].Trim()),
               TeamID=x.teamID
           }).OrderBy(x=>x.TeamID)
           .ToList();

            var differentEndTimesGrouped = objGetTeamRoasterForTableBy
                .Where(x => x.LocationStatus == "Different" && string.IsNullOrEmpty(x.ServiceStatus))
                .Select(x => new
                {
                    Team = x.Teams,
                    ToArea = x.Area,
                    ToSubArea = x.SubArea,
                    ToTower = x.Tower,
                    ToAppartmentName=x.AppartmentName,
                    ToForm= ConvertToTimeSpans1(x.ServiceTimings.Split('-')[0].Trim()),
                    ToEndTime = ConvertToTimeSpans1(x.ServiceTimings.Split('-')[1].Trim()),
                    TeamID = x.teamID
                }).OrderBy(x=>x.TeamID)
                .ToList();

            // Combine "From" fields into a single "FromAddress"
            var fromAddresses = previousStartTimesGrouped
                .Select(x => new
                {
                    Team = x.Team,
                    FromArea = x.FromArea,
                    FromSubArea=x.FromSubArea,
                    FromTower=x.FromTower,
                    FromAppartmentName=x.FromAppartmentName,
                    FromStartTime = x.FromStartTime,
                    FromEndTime=x.FromEndTime
                })
                .ToList();

            // Combine "To" fields into a single "ToAddress"
            var toAddresses = differentEndTimesGrouped
                .Select(x => new
                {
                    Team = x.Team,
                    ToArea = x.ToArea,
                    ToSubArea= x.ToSubArea,
                    ToTower=x.ToTower,
                    ToAppartmentName=x.ToAppartmentName,
                    ToFormTime=x.ToForm,
                    ToEndTime = x.ToEndTime
                })
                .ToList();
            for (int i = 0; i < fromAddresses.Count; i++)
            {
                var from = fromAddresses[i];
                var to = toAddresses[i];
                result.Add(new TeamRoaster
                {
                    Teams = from.Team,
                    FromArea = from.FromArea,
                    FromSubArea = from.FromSubArea,
                    FromTower = from.FromTower,
                    FromAppartmentName = from.FromAppartmentName,
                    ToArea = to.ToArea,
                    ToSubArea = to.ToSubArea,
                    ToTower = to.ToTower,
                    ToAppartmentName = to.ToAppartmentName,
                    FromServiceTime = from.FromStartTime+"-"+from.FromEndTime,
                    ToServiceTime = to.ToFormTime+"-"+to.ToEndTime

                }); 
            }


            return result;

        }

        public List<TeamRoaster> GetTeamRoastersByDate(int? uID, DateTime? Date)
        {
            List<TeamRoaster> result = new List<TeamRoaster>();

            var objGetTeamRoasterForTableBy = GetTeamRoasterForTableByDate(uID, Date);
            TimeSpan? SafeParseTime(string time)
            {
                return TimeSpan.TryParse(time, out var result1) ? result1 : (TimeSpan?)null;
            }

            objGetTeamRoasterForTableBy = objGetTeamRoasterForTableBy.OrderBy(x => x.teamID).ThenBy(x => x.StartTime).ToList();
            // Filter and group previous start times
            var previousStartTimesGrouped = objGetTeamRoasterForTableBy
           .Where(x => x.LocationStatus == "Different" && x.ServiceStatus == "Pervious")
           .Select(x => new
           {
               Team = x.Teams,
               FromArea = x.Area,
               FromSubArea = x.SubArea,
               FromTower = x.Tower,
               FromAppartmentName = x.AppartmentName,
               FromStartTime = ConvertToTimeSpans1(x.ServiceTimings.Split('-')[0].Trim()),
               FromEndTime = ConvertToTimeSpans1(x.ServiceTimings.Split('-')[1].Trim()),
               TeamID = x.teamID
           }).OrderBy(x => x.TeamID)
           .ToList();

            var differentEndTimesGrouped = objGetTeamRoasterForTableBy
                .Where(x => x.LocationStatus == "Different" && string.IsNullOrEmpty(x.ServiceStatus))
                .Select(x => new
                {
                    Team = x.Teams,
                    ToArea = x.Area,
                    ToSubArea = x.SubArea,
                    ToTower = x.Tower,
                    ToAppartmentName = x.AppartmentName,
                    ToForm = ConvertToTimeSpans1(x.ServiceTimings.Split('-')[0].Trim()),
                    ToEndTime = ConvertToTimeSpans1(x.ServiceTimings.Split('-')[1].Trim()),
                    TeamID = x.teamID
                }).OrderBy(x => x.TeamID)
                .ToList();

            // Combine "From" fields into a single "FromAddress"
            var fromAddresses = previousStartTimesGrouped
                .Select(x => new
                {
                    Team = x.Team,
                    FromArea = x.FromArea,
                    FromSubArea = x.FromSubArea,
                    FromTower = x.FromTower,
                    FromAppartmentName = x.FromAppartmentName,
                    FromStartTime = x.FromStartTime,
                    FromEndTime = x.FromEndTime
                })
                .ToList();

            // Combine "To" fields into a single "ToAddress"
            var toAddresses = differentEndTimesGrouped
                .Select(x => new
                {
                    Team = x.Team,
                    ToArea = x.ToArea,
                    ToSubArea = x.ToSubArea,
                    ToTower = x.ToTower,
                    ToAppartmentName = x.ToAppartmentName,
                    ToFormTime = x.ToForm,
                    ToEndTime = x.ToEndTime
                })
                .ToList();
            for (int i = 0; i < fromAddresses.Count; i++)
            {
                var from = fromAddresses[i];
                var to = toAddresses[i];
                result.Add(new TeamRoaster
                {
                    Teams = from.Team,
                    FromArea = from.FromArea,
                    FromSubArea = from.FromSubArea,
                    FromTower = from.FromTower,
                    FromAppartmentName = from.FromAppartmentName,
                    ToArea = to.ToArea,
                    ToSubArea = to.ToSubArea,
                    ToTower = to.ToTower,
                    ToAppartmentName = to.ToAppartmentName,
                    FromServiceTime = from.FromStartTime + "-" + from.FromEndTime,
                    ToServiceTime = to.ToFormTime + "-" + to.ToEndTime

                });
            }


            return result;

        }

        public List<TeamAvailability> GetTeamAvailableByDate(GetTeamAvailableByDate times)
        {
            List<TeamAvailability> result = new List<TeamAvailability>();
            DateTime StartDate = DateTime.ParseExact(times.Date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            List<TimeRange> StartDateTimes = new List<TimeRange>();
            List<TeamAndTime> objTeamAndTime = new List<TeamAndTime>();
            List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();

            var objTeams = UhDB.Teams.Where(x => x.IsActive == true && x.IsDelete == false && x.uID == times.uID).ToList();
            foreach (var Teams in objTeams)
            {
                int? teamID = Teams.teamID;
                var objCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(StartDate))
                                           .Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
                var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(StartDate))
                                    .Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
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
                if (tempData != null)
                {
                    if (tempData.Count() != 0)
                    {
                        tempData = tempData.OrderBy(x => x.StartDate).ToList();
                    }
                }
                if (tempData.Count() != 0)
                {
                    List<TimeRange> ResultimeRange = new List<TimeRange>();
                    var groupedDates = tempData.GroupBy(date => date.StartDate).Select(group => new
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
                        expectTimes = expectTimes.OrderBy(x => x.Start).ToList();
                        TimeRange originalRange = new TimeRange();
                        originalRange.Start = ConvertToTimeSpans("8:00AM");
                        originalRange.End = ConvertToTimeSpans("06:00PM");
                        List<TimeRange> timeRange = objCommonCustomerTimeLineDB.RemoveIntervals(originalRange, expectTimes, (int)times.Time);
                        ResultimeRange.AddRange(timeRange);
                    }
                    result.Add(new TeamAvailability { TeamName = Teams.Name, Times = ResultimeRange });
                }
            }
            return result;
        }

        public List<TeamStaffDetails> GetTeamsByStaffDetails(int? uID)
        {
            List<TeamStaffDetails> result = new List<TeamStaffDetails>();
            var objTeams = UhDB.Teams.Where(x => x.IsActive == true && x.IsDelete == false && x.uID == uID).ToList();
            foreach (var objTeam in objTeams)
            {
                string TeamName = objTeam.Name;
                int? teamID = objTeam.teamID;
                List<StaffDetails> staffDetails = new List<StaffDetails>();
                var objStaffTeam = UhDB.StaffTeams.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objStaffRecord in objStaffTeam)
                {
                    var staffId = objStaffRecord.stfID;
                    var objStaff = UhDB.Staffs.Where(x => x.stfID == staffId && x.IsActive == true && x.IsDelete == false).SingleOrDefault();
                    string StaffName = objStaff.Name;
                    string emailId = objStaff.Email;
                    string mobileNo = objStaff.Mobile;
                    staffDetails.Add(new StaffDetails { stfID = (int)staffId, StaffName = StaffName, Email = emailId, MobileNo = mobileNo });
                }
                result.Add(new TeamStaffDetails { teamID = (int)teamID, TeamName = TeamName, StaffDetails = staffDetails });
            }
            return result;
        }

        public IEnumerable<GetRescheduleStatusList> GetRescheduleingList(int? uID)
        {
            List<GetRescheduleStatusList> result = new List<GetRescheduleStatusList>();
            result = UhDB.CustomerAlerts.Where(x => x.Customer.uID == uID && x.custATID == 1 && x.IsActive == true && x.IsDelete == false).OrderByDescending(x => x.CreatedOn).AsEnumerable()
                     .Select((p, q) => new GetRescheduleStatusList
                     {
                         Message = p.Message,
                         CustomerName = p.custID != null ? p.Customer.Name : "N/A",
                     }).ToList();
            return result;
        }

        public IEnumerable<GetRescheduleStatusList> GetCancelledReschedulesLists(int? uID)
        {
            List<GetRescheduleStatusList> result = new List<GetRescheduleStatusList>();
            result = UhDB.CustomerTimelines.Where(x => x.Customer.uID == uID && x.CustomerOfficalDetail.ServiceStatus == false && x.IsActive == true && x.IsDelete == false).OrderByDescending(x => x.UpdatedOn).AsEnumerable()
                     .Select((p, q) => new GetRescheduleStatusList
                     {

                         CustomerName = p.custID != null ? p.Customer.Name : "N/A",
                         StartDate = p.StartDate?.ToString("dd-mm-yyyy"),
                         StartTime = p.StartTime,
                         EndTime = p.EndTime

                     }).ToList();
            return result;
        }

        //public string SendNotificationToTeams(SendNotificationToTeamsModel teams)
        //{
        //    string result = null;
        //    var objCustomer = UhDB.Staffs.Where(x => x.stfID == teams.stfID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
        //    if (teams.IsEmail == true)
        //    {
        //        string body = EmailBody(teams.Message, objCustomer.Name);
        //        result = objGeneralDB.SentEmailFromAmazon(objCustomer.Email, body, teams.Subject, objCustomer.Name);
        //    }
        //    else
        //    {
        //        string Mobile = objCustomer.PhoneCode + objCustomer.Mobile;
        //        string res = objGeneralDB.SendSMS(Mobile, teams.Message);
        //        if (res == "OK")
        //        {
        //            result = "SUCCESS";
        //        }
        //        else
        //        {
        //            result = res;
        //        }
        //    }
        //    return result;
        //}
        //private string EmailBody(string message, string name)
        //{
        //    string body = string.Empty;
        //    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/CustomerNotification.html")))//using streamreader for reading my htmltemplate  
        //    {
        //        body = reader.ReadToEnd();
        //    }
        //    body = body.Replace("{message}", message); 
        //    body = body.Replace("{name}", name);
        //    return body;

        //}

        public TimeSpan ConvertToTimeSpans(string timeStrings)
        {
            TimeSpan timeSpans = new TimeSpan();
            string format = "h:mmtt";
            if (DateTime.TryParseExact(timeStrings, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
            {
                timeSpans = dateTime.TimeOfDay;
            }
            return timeSpans;
        }

        public TimeSpan ConvertToTimeSpans1(string timeStrings)
        {
            TimeSpan timeSpans = new TimeSpan();
            string format = "h:mm tt";
            if (DateTime.TryParseExact(timeStrings, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
            {
                timeSpans = dateTime.TimeOfDay;
            }
            return timeSpans;
        }

        public IEnumerable<RoasterTeam> RoasterTeams(int? uID)
        {
            List<RoasterTeam> result = new List<RoasterTeam>();
         
            var objTeams = UhDB.Teams.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objTeam in objTeams)
            {
                DateTime TodayDate = DateTime.Now;
                var objCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.Customer.uID == uID && x.teamID == objTeam.teamID && x.IsActive == true && x.IsDelete == false
                                            && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(TodayDate)).ToList();
                List<RoasterTeam> objRoasterTeam = new List<RoasterTeam>();
                foreach (var objCustomerTimeLine in objCustomerTimeLines)
                {
         
                    string ServiceName = null;
                    if (objCustomerTimeLine.CustomerOfficalDetail.catsubID == 3)
                    {
                        var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.custID == objCustomerTimeLine.custID && x.custODID == objCustomerTimeLine.custODID && x.IsActive == true && x.IsDelete == false).Select(p => new { Name = p.ServiceCategory.Name }).ToString();
                        if (objCustomerSpecializedCleanings.Count() == 1)
                        {
                            ServiceName = objCustomerSpecializedCleanings.FirstOrDefault().ToString();
                        }
                        else
                        {
                            ServiceName = string.Join(",", objCustomerSpecializedCleanings);
                        }
                    }
                    else
                    {
                        ServiceName = objCustomerTimeLine.CustomerOfficalDetail.SubCategory.Name;
                    }

                    string TowerName = objCustomerTimeLine.CustomerOfficalDetail.propType == 1 ? objCustomerTimeLine.CustomerOfficalDetail.Venture.Name :
                                      UhDB.CustomerOtherProperties.Where(x => x.custID == objCustomerTimeLine.custID && x.custODID == objCustomerTimeLine.custODID).FirstOrDefault().TowerName;
                    objRoasterTeam.Add(new RoasterTeam
                    {
                        Teams = objCustomerTimeLine.Team.Name,
                        StartTime = ConvertToTimeSpans1(objCustomerTimeLine.StartTime),
                        ServiceTimings = objCustomerTimeLine.StartTime + "-" + objCustomerTimeLine.EndTime,
                        Area = objCustomerTimeLine.CustomerOfficalDetail.propaID != null ? objCustomerTimeLine.CustomerOfficalDetail.PropertyArea.Name : "N/A",
                        SubArea = objCustomerTimeLine.CustomerOfficalDetail.subAreaID != null ? objCustomerTimeLine.CustomerOfficalDetail.SubArea.Name : "N/A",
                        Tower = TowerName,
                        AppartmentName = objCustomerTimeLine.CustomerOfficalDetail.AppartmentNumber,
                        Service = ServiceName,
                        AppartmentType = objCustomerTimeLine.CustomerOfficalDetail.proprestID != null ? objCustomerTimeLine.CustomerOfficalDetail.PropertyResidenceType.Name : "N/A",
                        PropertyCode = objCustomerTimeLine.CustomerOfficalDetail.vID != null ? objCustomerTimeLine.CustomerOfficalDetail.Venture.Code : "N/A",
                        ServiceStatus = objCustomerTimeLine.CustomerOfficalDetail.ServiceStatus == true ? "Active" : "InActive"
                    });
                }
                objRoasterTeam= objRoasterTeam.OrderBy(x => x.StartTime).ToList();
                result.AddRange(objRoasterTeam); 
            }
            return result;
        }

        public IEnumerable<RoasterTeam> RoasterTeamsByDate(int? uID, string Date)
        {
            List<RoasterTeam> result = new List<RoasterTeam>();

            var objTeams = UhDB.Teams.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objTeam in objTeams)
            {
                List<RoasterTeam> objRoasterTeam = new List<RoasterTeam>();
                DateTime StartDate = DateTime.ParseExact(Date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                var objCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.Customer.uID == uID &&
                                           x.teamID == objTeam.teamID && x.IsActive == true
                                           && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(StartDate)).ToList();
                foreach (var objCustomerTimeLine in objCustomerTimeLines)
                {
                    string ServiceName = null;
                    if (objCustomerTimeLine.CustomerOfficalDetail.catsubID == 3)
                    {
                        var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.custID == objCustomerTimeLine.custID && x.custODID == objCustomerTimeLine.custODID && x.IsActive == true && x.IsDelete == false).Select(p => new { Name = p.ServiceCategory.Name }).ToString();
                        if (objCustomerSpecializedCleanings.Count() == 1)
                        {
                            ServiceName = objCustomerSpecializedCleanings.FirstOrDefault().ToString();
                        }
                        else
                        {
                            ServiceName = string.Join(",", objCustomerSpecializedCleanings);
                        }
                    }
                    else
                    {
                        ServiceName = objCustomerTimeLine.CustomerOfficalDetail.SubCategory.Name;
                    }

                    string TowerName = objCustomerTimeLine.CustomerOfficalDetail.propType == 1 ? objCustomerTimeLine.CustomerOfficalDetail.Venture.Name :
                                      UhDB.CustomerOtherProperties.Where(x => x.custID == objCustomerTimeLine.custID && x.custODID == objCustomerTimeLine.custODID).FirstOrDefault().TowerName;
                    objRoasterTeam.Add(new RoasterTeam
                    {
                        Teams = objCustomerTimeLine.Team.Name,
                        StartTime = ConvertToTimeSpans1(objCustomerTimeLine.StartTime),
                        ServiceTimings = objCustomerTimeLine.StartTime + "-" + objCustomerTimeLine.EndTime,
                        Area = objCustomerTimeLine.CustomerOfficalDetail.propaID != null ? objCustomerTimeLine.CustomerOfficalDetail.PropertyArea.Name : "N/A",
                        SubArea = objCustomerTimeLine.CustomerOfficalDetail.subAreaID != null ? objCustomerTimeLine.CustomerOfficalDetail.SubArea.Name : "N/A",
                        Tower = TowerName,
                        AppartmentName = objCustomerTimeLine.CustomerOfficalDetail.AppartmentNumber,
                        Service = ServiceName,
                        AppartmentType = objCustomerTimeLine.CustomerOfficalDetail.proprestID != null ? objCustomerTimeLine.CustomerOfficalDetail.PropertyResidenceType.Name : "N/A",
                        PropertyCode = objCustomerTimeLine.CustomerOfficalDetail.vID != null ? objCustomerTimeLine.CustomerOfficalDetail.Venture.Code : "N/A",
                        ServiceStatus = objCustomerTimeLine.CustomerOfficalDetail.ServiceStatus == true ? "Active" : "InActive"
                    });
                }
                objRoasterTeam = objRoasterTeam.OrderBy(x => x.StartTime).ToList();
                result.AddRange(objRoasterTeam);
            }
            return result;
        }
    }
}