using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;
using System.Data.Entity.Core.Objects;

namespace UHSForm.DAL
{
    public class RevenueReportDB
    {
        private UHSEntities UhDB;

        public RevenueReportDB()
        {
            UhDB = new UHSEntities();
        }



        public GetRevenueReportModel GetRevenueReportForToday(int? uID)
        {
            GetRevenueReportModel result = new GetRevenueReportModel();
            List<GetRevenueByTowerReportModel> objTowers = new List<GetRevenueByTowerReportModel>();
            DateTime TodayDate = DateTime.Now;
            var objCustomerTransactions = UhDB.CustomerTransactions.Where(x => x.Customer.uID == uID
                                              && x.IsActive == true && x.IsDelete == false && x.PaymentStatus == 2
                                              && EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(TodayDate)).AsQueryable();
            string pID = "All";
            string tID = "All";

            if (pID == "All")
            {
                if (tID == "All")
                {
                    var objPropertyAreas = UhDB.PropertyAreas.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).ToList();
                    foreach (var objPropertyArea in objPropertyAreas)
                    {
                        int? propaID = objPropertyArea.propaID;
                        string PropertyArea = objPropertyArea.Name;
                        var objVentures = UhDB.Ventures.Where(x => x.uID == uID && x.propaID == propaID && x.IsActive == true && x.IsDelete == false).ToList();
                        foreach (var objVenture in objVentures)
                        {
                            int? vID = objVenture.vID;
                            string VentureName = objVenture.Name;
                            string PropertyCode = objVenture.Code ?? "N/A" ;
                            
                            double? Amount = objCustomerTransactions
                                .Where(x => x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID)
                                .Sum(x => (double?)x.TotalPrice);

                            var objNoOfCustomers = UhDB.CustomerOfficalDetails.Where(x => x.Customer.uID ==uID
                                                        && x.IsActive == true && x.IsDelete == false
                                                        && x.propaID == propaID && x.vID == vID && EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(TodayDate)).Count();

                            string SubArea = UhDB.CustomerOfficalDetails.Where(x => x.Customer.uID == uID && x.IsActive == true && x.IsDelete == false && x.propaID == propaID && x.vID == vID).Select(v => v.SubArea.Name).FirstOrDefault() ?? "N/A";
                            objTowers.Add(new GetRevenueByTowerReportModel
                            {
                                TowerName = VentureName,
                                Area=PropertyArea,
                                SubArea=SubArea,
                                PropertyCode= PropertyCode,
                                Amount = Amount ?? 0,
                                NoOfCustomers = objNoOfCustomers
                            });
                        }
                    }
                }
            }

            result.Towers = objTowers;
            result.RevenuePerTower = objTowers.Sum(x => x.Amount);
            result.RevenuePerArea = objTowers.Sum(x => x.Amount);
            result.TotalRevenue = objTowers.Sum(x => x.Amount);
            return result;
        }

        public GetRevenueReportModel GetRevenueReportByDate(RevenueReportModel report,DateTime? StartDate)
        {
            GetRevenueReportModel result = new GetRevenueReportModel();
            List<GetRevenueByTowerReportModel> objTowers = new List<GetRevenueByTowerReportModel>();
            DateTime TodayDate = DateTime.Now;

            var objCustomerTransactions = UhDB.CustomerTransactions.Where(x => x.Customer.uID == report.uID && x.IsActive == true && x.IsDelete == false && x.PaymentStatus == 2).AsQueryable();
            var objNoOfCustomersQuery = UhDB.CustomerOfficalDetails.Where(x => x.Customer.uID == report.uID && x.IsActive == true && x.IsDelete == false).AsQueryable();

            if (report.StartDate!= null)
            {
                objCustomerTransactions = objCustomerTransactions.Where(x=>EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(StartDate));
                objNoOfCustomersQuery = objNoOfCustomersQuery.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(StartDate));
            }
          
            if (report.Month != null && report.Year != null)
            {
                int? Month = Convert.ToInt32(report.Month);
                int? Year = Convert.ToInt32(report.Year);
                objCustomerTransactions = objCustomerTransactions.Where(x => EntityFunctions.TruncateTime(x.CreatedOn).Value.Month == Month
                                                                    && EntityFunctions.TruncateTime(x.CreatedOn).Value.Year == Year);

                objNoOfCustomersQuery = objNoOfCustomersQuery.Where(x=>EntityFunctions.TruncateTime(x.CreatedOn).Value.Month == Month
                                                                    && EntityFunctions.TruncateTime(x.CreatedOn).Value.Year == Year);
            }
            if(report.Month=="All" && report.Year!=null)
            {
          
                int? Month = Convert.ToInt32(report.Month);
                int? Year = Convert.ToInt32(report.Year);
                objCustomerTransactions = objCustomerTransactions.Where(x => EntityFunctions.TruncateTime(x.CreatedOn).Value.Year == Year);
                objNoOfCustomersQuery = objNoOfCustomersQuery.Where(x => EntityFunctions.TruncateTime(x.CreatedOn).Value.Year == Year);
            }
            string pID = "All";
            string tID = "All";

            if (pID == "All")
            {
                if (tID == "All")
                {
                    var objPropertyAreas = UhDB.PropertyAreas.Where(x => x.uID == report.uID && x.IsActive == true && x.IsDelete == false).ToList();
                    foreach (var objPropertyArea in objPropertyAreas)
                    {
                        int? propaID = objPropertyArea.propaID;
                        string PropertyArea = objPropertyArea.Name;
                        var objVentures = UhDB.Ventures.Where(x => x.uID == report.uID && x.propaID == propaID && x.IsActive == true && x.IsDelete == false).ToList();
                        foreach (var objVenture in objVentures)
                        {
                            int? vID = objVenture.vID;
                            string VentureName = objVenture.Name;
                            string PropertyCode = objVenture.Code ?? "N/A";

                            double? Amount = objCustomerTransactions
                                .Where(x => x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID)
                                .Sum(x => (double?)x.TotalPrice);

                            int? objNoOfCustomers = objNoOfCustomersQuery.Where(x=>x.propaID == propaID && x.vID == vID).Count();

                            string SubArea = UhDB.CustomerOfficalDetails.Where(x => x.Customer.uID == report.uID && x.IsActive == true && x.IsDelete == false && x.propaID == propaID && x.vID == vID).Select(v => v.SubArea.Name).FirstOrDefault() ?? "N/A";
                            objTowers.Add(new GetRevenueByTowerReportModel
                            {
                                TowerName = VentureName,
                                Area = PropertyArea,
                                SubArea = SubArea,
                                PropertyCode = PropertyCode,
                                Amount = Amount ?? 0,
                                NoOfCustomers = objNoOfCustomers
                            });
                        }
                    }
                }
            }

            result.Towers = objTowers;
            result.RevenuePerTower = objTowers.Sum(x => x.Amount);
            result.RevenuePerArea = objTowers.Sum(x => x.Amount);
            result.TotalRevenue = objTowers.Sum(x => x.Amount);
            return result;
        }

        //public GetRevenueReportModel GetRevenueReport(RevenueReportModel report)
        //{ 
        //    GetRevenueReportModel result = new GetRevenueReportModel();
        //    List<GetRevenueByTowerReportModel> objTowers = new List<GetRevenueByTowerReportModel>();
            
        //    var objCustomerTransactions = UhDB.CustomerTransactions.Where(x => x.Customer.uID == report.uID
        //                                  && x.IsActive == true && x.IsDelete == false && x.PaymentStatus == 2).AsQueryable();
        //    if (report.EndDate != null)
        //    {
        //        objCustomerTransactions = objCustomerTransactions.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) >= EntityFunctions.TruncateTime(report.StartDate) && EntityFunctions.TruncateTime(x.CreatedOn) <= EntityFunctions.TruncateTime(report.EndDate));
        //    }
        //    else
        //    {
        //        objCustomerTransactions = objCustomerTransactions.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(report.StartDate));
        //    }
        //    if (report.propaID == "All")
        //    {
        //        if (report.vID == "All")
        //        {
        //            var objPropertyAreas = UhDB.PropertyAreas.Where(x => x.uID == report.uID && x.IsActive == true && x.IsDelete == false).ToList();
        //            foreach (var objPropertyArea in objPropertyAreas)
        //            {
        //                int? propaID = objPropertyArea.propaID;
        //                string PropertyArea = objPropertyArea.Name;
        //                var objVentures = UhDB.Ventures.Where(x => x.uID == report.uID && x.propaID == propaID && x.IsActive == true && x.IsDelete == false).ToList();
        //                foreach (var objVenture in objVentures)
        //                {
        //                    double? Amount = null;
        //                    int? vID = objVenture.vID;
        //                    string VentureName = objVenture.Name;
                            
        //                    int CountTransaction = objCustomerTransactions.Where(x => x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Count();
        //                    if (CountTransaction != 0)
        //                    {
        //                        Amount = objCustomerTransactions.Where(x => x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Sum(x => x.TotalPrice);
        //                    }
        //                    var objNoOfCustomers = UhDB.CustomerOfficalDetails.Where(x => x.Customer.uID == report.uID
        //                                           && x.IsActive == true && x.IsDelete == false && x.propaID == propaID && x.vID == vID).AsQueryable();
        //                    if (report.EndDate != null)
        //                    {
        //                        objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) >= EntityFunctions.TruncateTime(report.StartDate) && EntityFunctions.TruncateTime(x.CreatedOn) <= EntityFunctions.TruncateTime(report.EndDate));
        //                    }
        //                    else
        //                    {
        //                        objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(report.StartDate));
        //                    }

        //                    int? NoOfCustomers = objNoOfCustomers.ToList().Count();
        //                    objTowers.Add(new GetRevenueByTowerReportModel { TowerName = VentureName, Amount = Amount, NoOfCustomers =NoOfCustomers}); 

        //                }
        //            }
        //            result.TotalRevenue = objTowers.Sum(x => x.Amount);
        //            result.RevenuePerTower = objTowers.Sum(x => x.Amount);
        //            result.RevenuePerArea = objTowers.Sum(x => x.Amount);
        //        }
                
        //        else
        //        {
        //            int? vID = Convert.ToInt32(report.vID);
        //            double? Amount = null;
        //            var objVentures = UhDB.Ventures.Where(x => x.uID == report.uID && x.vID == vID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
        //            string VentureName = objVentures.Name;
        //            int CountTransaction = objCustomerTransactions.Where(x => x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Count();
        //            if (CountTransaction != 0)
        //            {
        //                Amount = objCustomerTransactions.Where(x => x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Sum(x => x.TotalPrice);
        //            }
        //            var objNoOfCustomers = UhDB.CustomerOfficalDetails.Where(x => x.Customer.uID == report.uID
        //                                   && x.IsActive == true && x.IsDelete == false && x.vID == vID).AsQueryable();
        //            if (report.EndDate != null)
        //            {
        //                objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) >= EntityFunctions.TruncateTime(report.StartDate) && EntityFunctions.TruncateTime(x.CreatedOn) <= EntityFunctions.TruncateTime(report.EndDate));
        //            }
        //            else
        //            {
        //                objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(report.StartDate));
        //            }
                    
        //            int? NoOfCustomers = objNoOfCustomers.ToList().Count();
        //            objTowers.Add(new GetRevenueByTowerReportModel { TowerName = VentureName, Amount = Amount, NoOfCustomers = NoOfCustomers });
        //            result.RevenuePerTower = objTowers.Sum(x => x.Amount);
                    
        //            var objPropertyAreas = UhDB.PropertyAreas.Where(x => x.uID == report.uID && x.IsActive == true && x.IsDelete == false).ToList();
        //            double TotalAreaAmount = 0;
        //            foreach (var objPropertyArea in objPropertyAreas)
        //            {
        //                int? propaID = objPropertyArea.propaID;
        //                string PropertyArea = objPropertyArea.Name;
        //                var objALLVentures = UhDB.Ventures.Where(x => x.uID == report.uID && x.propaID == propaID && x.IsActive == true && x.IsDelete == false).ToList();
        //                foreach (var objVenture in objALLVentures)
        //                {
        //                    double? AreaAmount = null;
        //                    int? AreaVID = objVenture.vID;
        //                    int CountAreaTransaction = objCustomerTransactions.Where(x => x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Count();
        //                    if (CountAreaTransaction != 0)
        //                    {
        //                        AreaAmount = objCustomerTransactions.Where(x => x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Sum(x => x.TotalPrice);
        //                    }
        //                    TotalAreaAmount = TotalAreaAmount + Convert.ToDouble(AreaAmount);
        //                }
        //            } 
        //            result.RevenuePerArea = TotalAreaAmount;
        //            result.TotalRevenue = TotalAreaAmount + objTowers.Sum(x => x.Amount);
        //        }
        //    }
        //    else
        //    {
        //        if (report.vID == "All")
        //        {

        //            int? propaID = Convert.ToInt32(report.propaID);
        //            var objVentures = UhDB.Ventures.Where(x => x.uID == report.uID && x.propaID == propaID && x.IsActive == true && x.IsDelete == false).ToList();
        //            foreach (var objVenture in objVentures)
        //            {
        //                double? Amount = null;
        //                int? vID = objVenture.vID;
        //                string VentureName = objVenture.Name;
        //                int CountTransaction = objCustomerTransactions.Where(x => x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Count();
        //                if (CountTransaction != 0)
        //                {
        //                    Amount = objCustomerTransactions.Where(x => x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Sum(x => x.TotalPrice);
        //                }
        //                var objNoOfCustomers = UhDB.CustomerOfficalDetails.Where(x => x.Customer.uID == report.uID
        //                                       && x.IsActive == true && x.IsDelete == false && x.propaID==propaID && x.vID == vID).AsQueryable();
        //                if (report.EndDate != null)
        //                {
        //                    objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) >= EntityFunctions.TruncateTime(report.StartDate) && EntityFunctions.TruncateTime(x.CreatedOn) <= EntityFunctions.TruncateTime(report.EndDate));
        //                }
        //                else
        //                {
        //                    objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(report.StartDate));
        //                }
        //                int? NoOfCustomers = objNoOfCustomers.ToList().Count();
        //                objTowers.Add(new GetRevenueByTowerReportModel { TowerName = VentureName, Amount = Amount, NoOfCustomers = NoOfCustomers });
        //            }
        //            result.RevenuePerTower = objTowers.Sum(x=>x.Amount);
        //            result.RevenuePerArea = objTowers.Sum(x => x.Amount);
        //        }
        //        else
        //        {
        //            int? vID = Convert.ToInt32(report.vID);
        //            int? propaID = Convert.ToInt32(report.propaID);
        //            double? Amount = null;
        //            var objVentures = UhDB.Ventures.Where(x => x.uID == report.uID && x.vID == vID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
        //            string VentureName = objVentures.Name;
        //            int CountTransaction = objCustomerTransactions.Where(x =>x.CustomerOfficalDetail.propaID==propaID &&  x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Count();
        //            if (CountTransaction != 0)
        //            {
        //                Amount = objCustomerTransactions.Where(x => x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Sum(x => x.TotalPrice);
        //            }
        //            var objNoOfCustomers = UhDB.CustomerOfficalDetails.Where(x => x.Customer.uID == report.uID
        //                                                       && x.IsActive == true && x.IsDelete == false && x.propaID == propaID && x.vID == vID).AsQueryable();
        //            if (report.EndDate != null)
        //            {
        //                objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) >= EntityFunctions.TruncateTime(report.StartDate) && EntityFunctions.TruncateTime(x.CreatedOn) <= EntityFunctions.TruncateTime(report.EndDate));
        //            }
        //            else
        //            {
        //                objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(report.StartDate));
        //            }


        //            int? NoOfCustomers = objNoOfCustomers.ToList().Count();
        //            objTowers.Add(new GetRevenueByTowerReportModel { TowerName = VentureName, Amount = Amount, NoOfCustomers = NoOfCustomers });
        //            result.RevenuePerTower = objTowers.Sum(x=>x.Amount);
        //            var objAreaVentures = UhDB.Ventures.Where(x => x.uID == report.uID && x.propaID == propaID && x.IsActive == true && x.IsDelete == false).ToList();
        //            double TotalAreaAmount = 0;
        //            foreach (var objAreaVenture in objAreaVentures)
        //            {
        //                double? AreaAmount = null;
        //                int? AreaVentureID = objAreaVenture.vID;
        //                int CountAreaTransaction = objCustomerTransactions.Where(x => x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Count();
        //                if (CountAreaTransaction != 0)
        //                {
        //                    AreaAmount = objCustomerTransactions.Where(x => x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Sum(x => x.TotalPrice);
        //                }
        //                TotalAreaAmount = TotalAreaAmount + Convert.ToDouble(AreaAmount);

        //            }
        //            result.RevenuePerArea = TotalAreaAmount;
        //        }
        //    }

        //    result.Towers = objTowers;
        //    return result;
        //}

        ////public GetRevenueReportModel GetRevenueReportByFilters(RevenueReportModel report)
        //{
        //    GetRevenueReportModel result = new GetRevenueReportModel();
        //    List<GetRevenueByTowerReportModel> objTowers = new List<GetRevenueByTowerReportModel>();
        //    var objCustomerTransactions = UhDB.CustomerTransactions.Where(x => x.Customer.uID == report.uID
        //                                  && x.IsActive == true && x.IsDelete == false && x.PaymentStatus == 2).AsQueryable();
        //    if (report.EndDate != null)
        //    {
        //        objCustomerTransactions = objCustomerTransactions.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) >= EntityFunctions.TruncateTime(report.StartDate) && EntityFunctions.TruncateTime(x.CreatedOn) <= EntityFunctions.TruncateTime(report.EndDate));
        //    }
        //    else
        //    {
        //        objCustomerTransactions = objCustomerTransactions.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(report.StartDate));
        //    }

        //    if (report.Month != null && report.Year != null)
        //    {
        //        int? Month = Convert.ToInt32(report.Month);
        //        int? Year = Convert.ToInt32(report.Year);
        //        objCustomerTransactions = objCustomerTransactions.Where(x => EntityFunctions.TruncateTime(x.CreatedOn).Value.Month == Month
        //                                                            && EntityFunctions.TruncateTime(x.CreatedOn).Value.Year == Year);
        //    }
        //    else if (report.Month != null)
        //    {
        //        int? Month = Convert.ToInt32(report.Month);
        //        objCustomerTransactions = objCustomerTransactions.Where(x => EntityFunctions.TruncateTime(x.CreatedOn).Value.Month == Month);
        //    }
        //    else if (report.Year != null)
        //    {
        //        int? Year = Convert.ToInt32(report.Year);
        //        objCustomerTransactions = objCustomerTransactions.Where(x => EntityFunctions.TruncateTime(x.CreatedOn).Value.Year == Year);
        //    }

        //    if (report.propaID == "All")
        //    {

        //        if (report.vID == "All")
        //        {
        //            var objPropertyAreas = UhDB.PropertyAreas.Where(x => x.uID == report.uID && x.IsActive == true && x.IsDelete == false).ToList();
        //            foreach (var objPropertyArea in objPropertyAreas)
        //            {
        //                int? propaID = objPropertyArea.propaID;
        //                string PropertyArea = objPropertyArea.Name;
        //                var objVentures = UhDB.Ventures.Where(x => x.uID == report.uID && x.propaID == propaID && x.IsActive == true && x.IsDelete == false).ToList();
        //                foreach (var objVenture in objVentures)
        //                {
        //                    double? Amount = null;
        //                    int? vID = objVenture.vID;
        //                    string VentureName = objVenture.Name;

        //                    int CountTransaction = objCustomerTransactions.Where(x => x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Count();
        //                    if (CountTransaction != 0)
        //                    {
        //                        Amount = objCustomerTransactions.Where(x => x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Sum(x => x.TotalPrice);
        //                    }
        //                    var objNoOfCustomers = UhDB.CustomerOfficalDetails.Where(x => x.Customer.uID == report.uID
        //                                           && x.IsActive == true && x.IsDelete == false && x.propaID == propaID && x.vID == vID).AsQueryable();

        //                    if (report.StartDate == null && report.EndDate == null)
        //                    {
        //                        if (report.Month != null && report.Year != null)
        //                        {
        //                            int? Month = Convert.ToInt32(report.Month);
        //                            int? Year = Convert.ToInt32(report.Year);
        //                            objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn).Value.Month == Month && EntityFunctions.TruncateTime(x.CreatedOn).Value.Year == Year);
        //                        }
        //                        else if (report.Month != null)
        //                        {
        //                            int? Month = Convert.ToInt32(report.Month);
        //                            objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn).Value.Month == Month);
        //                        }
        //                        else if (report.Year != null)
        //                        {
        //                            int? Year = Convert.ToInt32(report.Year);
        //                            objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn).Value.Year == Year);
        //                        }
        //                    }
        //                    if (report.EndDate != null)
        //                    {
        //                        objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) >= EntityFunctions.TruncateTime(report.StartDate) && EntityFunctions.TruncateTime(x.CreatedOn) <= EntityFunctions.TruncateTime(report.EndDate));
        //                    }
        //                    else
        //                    {
        //                        objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(report.StartDate));
        //                    }
        //                    int? NoOfCustomers = objNoOfCustomers.ToList().Count();
        //                    objTowers.Add(new GetRevenueByTowerReportModel { TowerName = VentureName, Amount = Amount, NoOfCustomers = NoOfCustomers });

        //                }
        //            }
        //            result.TotalRevenue = objTowers.Sum(x => x.Amount);
        //            result.RevenuePerTower = objTowers.Sum(x => x.Amount);
        //            result.RevenuePerArea = objTowers.Sum(x => x.Amount);
        //        }
        //        else
        //        {
        //            int? vID = Convert.ToInt32(report.vID);
        //            double? Amount = null;
        //            var objVentures = UhDB.Ventures.Where(x => x.uID == report.uID && x.vID == vID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
        //            string VentureName = objVentures.Name;
        //            int CountTransaction = objCustomerTransactions.Where(x => x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Count();
        //            if (CountTransaction != 0)
        //            {
        //                Amount = objCustomerTransactions.Where(x => x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Sum(x => x.TotalPrice);
        //            }
        //            var objNoOfCustomers = UhDB.CustomerOfficalDetails.Where(x => x.Customer.uID == report.uID
        //                                   && x.IsActive == true && x.IsDelete == false && x.vID == vID).AsQueryable();


        //            if (report.StartDate == null && report.EndDate == null)
        //            {
        //                if (report.Month != null && report.Year != null)
        //                {
        //                    int? Month = Convert.ToInt32(report.Month);
        //                    int? Year = Convert.ToInt32(report.Year);
        //                    objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn).Value.Month == Month && EntityFunctions.TruncateTime(x.CreatedOn).Value.Year == Year);

        //                }
        //                else if (report.Month != null)
        //                {
        //                    int? Month = Convert.ToInt32(report.Month);
        //                    objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn).Value.Month == Month);

        //                }
        //                else if (report.Year != null)
        //                {
        //                    int? Year = Convert.ToInt32(report.Year);
        //                    objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn).Value.Year == Year);
        //                }
        //            }
        //            if (report.EndDate != null)
        //            {
        //                objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) >= EntityFunctions.TruncateTime(report.StartDate) && EntityFunctions.TruncateTime(x.CreatedOn) <= EntityFunctions.TruncateTime(report.EndDate));
        //            }
        //            else
        //            {
        //                objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(report.StartDate));
        //            }

        //            int? NoOfCustomers = objNoOfCustomers.ToList().Count();
        //            objTowers.Add(new GetRevenueByTowerReportModel { TowerName = VentureName, Amount = Amount, NoOfCustomers = NoOfCustomers });
        //            result.RevenuePerTower = objTowers.Sum(x => x.Amount);
        //            var objPropertyAreas = UhDB.PropertyAreas.Where(x => x.uID == report.uID && x.IsActive == true && x.IsDelete == false).ToList();
        //            double TotalAreaAmount = 0;
        //            foreach (var objPropertyArea in objPropertyAreas)
        //            {
        //                int? propaID = objPropertyArea.propaID;
        //                string PropertyArea = objPropertyArea.Name;
        //                var objALLVentures = UhDB.Ventures.Where(x => x.uID == report.uID && x.propaID == propaID && x.IsActive == true && x.IsDelete == false).ToList();
        //                foreach (var objVenture in objALLVentures)
        //                {
        //                    double? AreaAmount = null;
        //                    int? AreaVID = objVenture.vID;
        //                    int CountAreaTransaction = objCustomerTransactions.Where(x => x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Count();
        //                    if (CountAreaTransaction != 0)
        //                    {
        //                        AreaAmount = objCustomerTransactions.Where(x => x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Sum(x => x.TotalPrice);
        //                    }
        //                    TotalAreaAmount = TotalAreaAmount + Convert.ToDouble(AreaAmount);
        //                }
        //            }
        //            result.RevenuePerArea = TotalAreaAmount;
        //            result.TotalRevenue = TotalAreaAmount + objTowers.Sum(x => x.Amount);
        //        }
        //    }
        //    else
        //    {
        //        if (report.vID == "All")
        //        {

        //            int? propaID = Convert.ToInt32(report.propaID);
        //            var objVentures = UhDB.Ventures.Where(x => x.uID == report.uID && x.propaID == propaID && x.IsActive == true && x.IsDelete == false).ToList();
        //            foreach (var objVenture in objVentures)
        //            {
        //                double? Amount = null;
        //                int? vID = objVenture.vID;
        //                string VentureName = objVenture.Name;
        //                int CountTransaction = objCustomerTransactions.Where(x => x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Count();
        //                if (CountTransaction != 0)
        //                {
        //                    Amount = objCustomerTransactions.Where(x => x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Sum(x => x.TotalPrice);
        //                }
        //                var objNoOfCustomers = UhDB.CustomerOfficalDetails.Where(x => x.Customer.uID == report.uID
        //                                       && x.IsActive == true && x.IsDelete == false && x.propaID == propaID && x.vID == vID).AsQueryable();
                        
        //                if(report.StartDate==null && report.EndDate==null)
        //                {
        //                    if (report.Month != null && report.Year != null)
        //                    {
        //                        int? Month = Convert.ToInt32(report.Month);
        //                        int? Year = Convert.ToInt32(report.Year);
        //                        objNoOfCustomers=objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn).Value.Month == Month && EntityFunctions.TruncateTime(x.CreatedOn).Value.Year == Year );
        
        //                    }
        //                    else if (report.Month != null)
        //                    {
        //                        int? Month = Convert.ToInt32(report.Month);
        //                        objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn).Value.Month == Month);

        //                    }
        //                    else if (report.Year != null)
        //                    {
        //                        int? Year = Convert.ToInt32(report.Year);
        //                        objNoOfCustomers = objNoOfCustomers.Where(x =>EntityFunctions.TruncateTime(x.CreatedOn).Value.Year == Year);

        //                    }

        //                }
        //                if (report.EndDate != null)
        //                {
        //                    objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) >= EntityFunctions.TruncateTime(report.StartDate) && EntityFunctions.TruncateTime(x.CreatedOn) <= EntityFunctions.TruncateTime(report.EndDate));
        //                }
        //                else
        //                {
        //                    objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(report.StartDate));
        //                }

        //                int? NoOfCustomers = objNoOfCustomers.ToList().Count();
        //                objTowers.Add(new GetRevenueByTowerReportModel { TowerName = VentureName, Amount = Amount, NoOfCustomers = NoOfCustomers });
        //            }
        //            result.RevenuePerTower = objTowers.Sum(x => x.Amount);
        //            result.RevenuePerArea = objTowers.Sum(x => x.Amount);
        //        }
        //        else
        //        {
        //            int? vID = Convert.ToInt32(report.vID);
        //            int? propaID = Convert.ToInt32(report.propaID);
        //            double? Amount = null;
        //            var objVentures = UhDB.Ventures.Where(x => x.uID == report.uID && x.vID == vID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
        //            string VentureName = objVentures.Name;
        //            int CountTransaction = objCustomerTransactions.Where(x => x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Count();
        //            if (CountTransaction != 0)
        //            {
        //                Amount = objCustomerTransactions.Where(x => x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Sum(x => x.TotalPrice);
        //            }
        //            var objNoOfCustomers = UhDB.CustomerOfficalDetails.Where(x => x.Customer.uID == report.uID
        //                                                       && x.IsActive == true && x.IsDelete == false && x.propaID == propaID && x.vID == vID).AsQueryable();

        //            if (report.StartDate == null && report.EndDate == null)
        //            {
        //                if (report.Month != null && report.Year != null)
        //                {
        //                    int? Month = Convert.ToInt32(report.Month);
        //                    int? Year = Convert.ToInt32(report.Year);
        //                    objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn).Value.Month == Month && EntityFunctions.TruncateTime(x.CreatedOn).Value.Year == Year);

        //                }
        //                else if (report.Month != null)
        //                {
        //                    int? Month = Convert.ToInt32(report.Month);
        //                    objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn).Value.Month == Month);

        //                }
        //                else if (report.Year != null)
        //                {
        //                    int? Year = Convert.ToInt32(report.Year);
        //                    objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn).Value.Year == Year);
        //                }

        //            }
        //            if (report.EndDate != null)
        //            {
        //                objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) >= EntityFunctions.TruncateTime(report.StartDate) && EntityFunctions.TruncateTime(x.CreatedOn) <= EntityFunctions.TruncateTime(report.EndDate));
        //            }
        //            else
        //            {
        //                objNoOfCustomers = objNoOfCustomers.Where(x => EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(report.StartDate));
        //            }

        //            int? NoOfCustomers = objNoOfCustomers.ToList().Count();
        //            objTowers.Add(new GetRevenueByTowerReportModel { TowerName = VentureName, Amount = Amount, NoOfCustomers = NoOfCustomers });
        //            result.RevenuePerTower = objTowers.Sum(x => x.Amount);
        //            var objAreaVentures = UhDB.Ventures.Where(x => x.uID == report.uID && x.propaID == propaID && x.IsActive == true && x.IsDelete == false).ToList();
        //            double TotalAreaAmount = 0;
        //            foreach (var objAreaVenture in objAreaVentures)
        //            {
        //                double? AreaAmount = null;
        //                int? AreaVentureID = objAreaVenture.vID;
        //                int CountAreaTransaction = objCustomerTransactions.Where(x => x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Count();
        //                if (CountAreaTransaction != 0)
        //                {
        //                    AreaAmount = objCustomerTransactions.Where(x => x.CustomerOfficalDetail.propaID == propaID && x.CustomerOfficalDetail.vID == vID && x.IsActive == true && x.IsDelete == false).Sum(x => x.TotalPrice);
        //                }
        //                TotalAreaAmount = TotalAreaAmount + Convert.ToDouble(AreaAmount);
        //            }
        //            result.RevenuePerArea = TotalAreaAmount;
        //        }
        //    }

        //    result.Towers = objTowers;
        //    return result;
        //}





        public string TotalRevenue(int? uID) 
        {
            string result = null;
            double? totalRevenue = UhDB.CustomerTransactions.Where(x => x.Customer.uID == uID && x.IsActive == true && x.IsDelete == false).Sum(x => x.TotalPrice);
            result = totalRevenue.ToString();
            return result;
        }

        public string TotalRetriveRevenue(int? uID)
        {
            string result = null;
            double? totalRevenue = UhDB.CustomerTransactions.Where(x => x.Customer.uID == uID && x.IsActive == true && x.IsDelete == false && x.PaymentStatus==2).Sum(x => x.TotalPrice);
            result = totalRevenue.ToString();
            return result;
        }
    }
}