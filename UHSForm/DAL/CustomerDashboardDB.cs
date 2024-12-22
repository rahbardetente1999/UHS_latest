using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;
using System.Data.Entity.Core.Objects;

namespace UHSForm.DAL
{
    public class CustomerDashboardDB
    {
        private UHSEntities UhDB;

        public CustomerDashboardDB()
        {
            UhDB = new UHSEntities();
        }

        public List<GetCustomerModelV4> GetCurrentCustomerTimeLines(int? cuID, int? catID, int? catsubID, int? StatusOfWork, string Month, int? vID, string AppartmentName)
        {
            List<GetCustomerModelV4> result = new List<GetCustomerModelV4>();

            var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID 
                                        && x.StatusOfWork == StatusOfWork && x.CustomerOfficalDetail.vID == vID && x.CustomerOfficalDetail.AppartmentNumber == AppartmentName && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                        .Select(p => new
                                        {
                                            PaymentStatus = UhDB.CustomerTransactions.Where(x => x.cuID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                                            (UhDB.CustomerTransactions.Where(x => x.cuID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().PaymentStatus).ToString() : null,
                                            PackageName = p.packID != null ? p.Package.Name : null,
                                            Price = p.parkID != null ? p.Pricing.Price.ToString() : null,
                                            Duration = p.parkID != null ? p.Pricing.Duration : null,
                                            StartTime = p.StartTime,
                                            EndTime = p.EndTime,
                                            TaskNo = p.TaskNo.ToString(),
                                            ServiceDate = p.StartDate,
                                            ServiceDay = p.StartDate != null ? Convert.ToDateTime(p.StartDate).ToString("dddd") : null,
                                            Status = p.CustomerOfficalDetail.ServiceStatus == true ? "Active" : p.CustomerOfficalDetail.ServiceStatus == false ? "InActive" : "Pending",
                                            WorkingStatus = p.StatusOfWork == 1 ? "Open" : p.StatusOfWork == 2 ? "Pending" : "Closed",
                                            teamID = p.teamID,
                                            custODID = p.custODID,
                                            custTDID = p.custTDID,
                                            packID = p.packID,
                                            proprestID = p.CustomerOfficalDetail.proprestID,
                                            TimeMeasurement = p.Pricing.TimeMeasurement,
                                            parkID = p.parkID,
                                        }).ToList();
            foreach (var objCustomerTimeline in objCustomerTimelines)
            {
                if (Convert.ToDateTime(objCustomerTimeline.ServiceDate).ToString("MMMM") == Month)
                {
                    List<string> StaffNames = new List<string>();
                    int? teamID = objCustomerTimeline.teamID;
                    var objStaffs = UhDB.StaffTeams.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).ToList();
                    foreach (var objStaff in objStaffs)
                    {
                        int? stfID = objStaff.stfID;
                        if (stfID != null)
                        {
                            string StaffName = UhDB.Staffs.Where(x => x.IsDelete == false && x.IsActive == true && x.stfID == stfID).FirstOrDefault().Name;
                            StaffNames.Add(StaffName);
                        }

                    }
                    string Staffs = null;
                    if (StaffNames != null)
                    {
                        Staffs = string.Join(", ", StaffNames);
                    }
                    List<GetCustomerServiceRatingModel> objCustomerRating = new List<GetCustomerServiceRatingModel>();
                    if (StatusOfWork == 3)
                    {
                        objCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == objCustomerTimeline.custODID && x.custTDID == objCustomerTimeline.custTDID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                              .Select(p => new GetCustomerServiceRatingModel
                                              {
                                                  CustomerName = p.Customer.Name,
                                                  TeamName = p.CustomerTimeline.teamID != null ? p.CustomerTimeline.Team.Name : null,
                                                  StaffName = p.CustomerOfficalDetail.stfID != null ? p.CustomerOfficalDetail.Staff.Name : null,
                                                  Rating = p.Rating,
                                                  Feedback = p.Feedback,
                                                  cuID = p.cuID,
                                                  custODID = p.custODID,
                                                  custfdbID = p.custfdbID,
                                                  CreatedBy = p.CreatedBy,
                                                  CreatedOn = p.CreatedOn
                                              }).ToList();
                    }


                    result.Add(new GetCustomerModelV4
                    {
                        PaymentStatus = objCustomerTimeline.PaymentStatus,
                        PackageName = objCustomerTimeline.PackageName,
                        Price = objCustomerTimeline.Price,
                        Duration = objCustomerTimeline.Duration,
                        StartTime = objCustomerTimeline.StartTime,
                        EndTime = objCustomerTimeline.EndTime,
                        TaskNo = objCustomerTimeline.TaskNo,
                        ServiceDate = objCustomerTimeline.ServiceDate != null ? Convert.ToDateTime(objCustomerTimeline.ServiceDate).ToString("dd/MM/yyyy") : null,
                        ServiceDay = objCustomerTimeline.ServiceDay,
                        Status = objCustomerTimeline.Status,
                        WorkingStatus = objCustomerTimeline.WorkingStatus,
                        Staffs = Staffs,
                        cuID = cuID,
                        custODID = objCustomerTimeline.custODID,
                        custTDID = objCustomerTimeline.custTDID,
                        Rating = objCustomerRating,
                        TimeMeasurement = objCustomerTimeline.TimeMeasurement,
                        proprestID = objCustomerTimeline.proprestID,
                        packID = objCustomerTimeline.packID,
                        teamID = objCustomerTimeline.teamID,
                        parkID = objCustomerTimeline.parkID
                    });
                }
            }
            return result;
        }

        public List<GetCustomerModelV7> GetCustomerDashboard(int? cuID)
        {
            List<GetCustomerModelV7> result = new List<GetCustomerModelV7>();

            var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                        .Select(p => new
                                        {
                                            PaymentStatus = UhDB.CustomerTransactions.Where(x => x.cuID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                                            (UhDB.CustomerTransactions.Where(x => x.cuID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().PaymentStatus).ToString() : null,
                                            PackageName = p.packID != null ? p.Package.Name : null,
                                            Price = p.parkID != null ? p.Pricing.Price.ToString() : null,
                                            Duration = p.parkID != null ? p.Pricing.Duration : null,
                                            StartTime = p.StartTime,
                                            EndTime = p.EndTime,
                                            TaskNo = p.TaskNo.ToString(),
                                            ServiceDate = p.StartDate,
                                            ServiceDay = p.StartDate != null ? Convert.ToDateTime(p.StartDate).ToString("dddd") : null,
                                            Status = p.CustomerOfficalDetail.ServiceStatus == true ? "Active" : p.CustomerOfficalDetail.ServiceStatus == false ? "InActive" : "Pending",
                                            WorkingStatus = p.StatusOfWork == 1 ? "Open" : p.StatusOfWork == 2 ? "Pending" : "Closed",
                                            teamID = p.teamID,
                                            custODID = p.custODID,
                                            custTDID = p.custTDID,
                                            packID = p.packID,
                                            propaID = p.CustomerOfficalDetail.propaID,
                                            vID = p.CustomerOfficalDetail.vID,
                                            propType = p.CustomerOfficalDetail.propType,
                                            proprestID = p.CustomerOfficalDetail.proprestID,
                                            TimeMeasurement = p.parkID!=null?p.Pricing.TimeMeasurement:"N/A",
                                            parkID = p.parkID,
                                            MainCategory = p.CustomerOfficalDetail.catID!=null? p.CustomerOfficalDetail.MainCategory.Name:"N/A",
                                            SubCategory = p.CustomerOfficalDetail.catsubID != null ? p.CustomerOfficalDetail.SubCategory.Name : "N/A",
                                            Area = p.CustomerOfficalDetail.propaID!=null?p.CustomerOfficalDetail.PropertyArea.Name:"N/A",
                                            PropertyName = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name :
                                                           UhDB.CustomerOtherProperties.Where(x => x.custID == cuID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().TowerName,
                                            PropertyResidencyType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : null,
                                            ApartmentName = p.CustomerOfficalDetail.AppartmentNumber,
                                            GetServices = UhDB.CustomerSpecializedCleanings.Where(x => x.custID == cuID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).Count()!=0? UhDB.CustomerSpecializedCleanings.Where(x => x.custID == cuID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable().
                                                          Select(t => new GetServiceSubCategoryModel
                                                          {
                                                              custSCID = t.custSCID,
                                                              servcatID = t.servcatID,
                                                              servsubcatID = t.servsubcatID,
                                                              ServiceCategoryName = t.ServiceCategory.Name,
                                                              ServiceSubCategoryName = t.ServiceSubCategory.Name,
                                                              Quantity = t.Quantity
                                                          }).ToList():null,
                                            carstID = p.CustomerOfficalDetail.carstID,
                                            cartID = p.CustomerOfficalDetail.cartID,
                                            IsCarWash = p.CustomerOfficalDetail.IsCarWash,
                                            CarType = p.CustomerOfficalDetail.CarType != null ? p.CustomerOfficalDetail.CarType.Name : "N/A",
                                            CarServiceType = p.CustomerOfficalDetail.CarServiceType != null ? p.CustomerOfficalDetail.CarServiceType.Name : "N/A",
                                            CustomCarDetails = p.CustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == cuID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,
                                            OtherLocation = p.CustomerOfficalDetail.propType == 2 ? UhDB.CustomerOtherProperties.Where(x => x.custID == cuID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                            .Select(u => new GetOtherLocationModel
                                                            {
                                                                TowerName = u.TowerName,
                                                                BuildingName = u.BuildingName,
                                                                StreetNumber = u.StreetNumber,
                                                                ZoneNumber = u.ZoneNumber,
                                                                Loacation = u.Loacation,
                                                                LocationLink = u.LocationLink
                                                            }).FirstOrDefault() : null,
                                                            catID=p.CustomerOfficalDetail.catID,
                                                            catsubID=p.CustomerOfficalDetail.catsubID
                                        }).ToList();
            foreach (var objCustomerTimeline in objCustomerTimelines)
            {
                List<string> StaffNames = new List<string>();
                int? teamID = objCustomerTimeline.teamID;
                var objStaffs = UhDB.StaffTeams.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objStaff in objStaffs)
                {
                    int? stfID = objStaff.stfID;
                    if (stfID != null)
                    {
                        string StaffName = UhDB.Staffs.Where(x => x.IsDelete == false && x.IsActive == true && x.stfID == stfID).FirstOrDefault().Name;
                        StaffNames.Add(StaffName);
                    }

                }
                string Staffs = null;
                if (StaffNames != null)
                {
                    Staffs = string.Join(", ", StaffNames);
                }
                List<GetCustomerServiceRatingModel> objCustomerRating = new List<GetCustomerServiceRatingModel>();
                objCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == objCustomerTimeline.custODID && x.custTDID == objCustomerTimeline.custTDID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                      .Select(p => new GetCustomerServiceRatingModel
                                      {
                                          CustomerName = p.Customer.Name,
                                          TeamName = p.CustomerTimeline.teamID != null ? p.CustomerTimeline.Team.Name : null,
                                          StaffName = p.CustomerOfficalDetail.stfID != null ? p.CustomerOfficalDetail.Staff.Name : null,
                                          Rating = p.Rating,
                                          Feedback = p.Feedback,
                                          cuID = p.cuID,
                                          custODID = p.custODID,
                                          custfdbID = p.custfdbID,
                                          CreatedBy = p.CreatedBy,
                                          CreatedOn = p.CreatedOn
                                      }).ToList();
                result.Add(new GetCustomerModelV7
                {
                    PaymentStatus = objCustomerTimeline.PaymentStatus,
                    PackageName = objCustomerTimeline.PackageName,
                    Price = objCustomerTimeline.Price,
                    Duration = objCustomerTimeline.Duration,
                    StartTime = objCustomerTimeline.StartTime,
                    EndTime = objCustomerTimeline.EndTime,
                    TaskNo = objCustomerTimeline.TaskNo,
                    ServiceDate = objCustomerTimeline.ServiceDate != null ? Convert.ToDateTime(objCustomerTimeline.ServiceDate).ToString("dd/MM/yyyy") : null,
                    ServiceDay = objCustomerTimeline.ServiceDay,
                    Status = objCustomerTimeline.Status,
                    WorkingStatus = objCustomerTimeline.WorkingStatus,
                    Staffs = Staffs,
                    cuID = cuID,
                    custODID = objCustomerTimeline.custODID,
                    custTDID = objCustomerTimeline.custTDID,
                    Rating = objCustomerRating,
                    TimeMeasurement = objCustomerTimeline.TimeMeasurement,
                    proprestID = objCustomerTimeline.proprestID,
                    packID = objCustomerTimeline.packID,
                    teamID = objCustomerTimeline.teamID,
                    parkID = objCustomerTimeline.parkID,
                    MainCategory = objCustomerTimeline.MainCategory,
                    SubCategory = objCustomerTimeline.SubCategory,
                    PropertyName = objCustomerTimeline.PropertyName,
                    PropertyResidencyType = objCustomerTimeline.PropertyResidencyType,
                    Area = objCustomerTimeline.Area,
                    ApartmentName = objCustomerTimeline.ApartmentName,
                    CarServiceType = objCustomerTimeline.CarServiceType,
                    CarType = objCustomerTimeline.CarType,
                    IsCarWash = objCustomerTimeline.IsCarWash,
                    carstID = objCustomerTimeline.carstID,
                    cartID = objCustomerTimeline.cartID,
                    CustomCarDetails = objCustomerTimeline.CustomCarDetails,
                    propaID = objCustomerTimeline.propaID,
                    vID = objCustomerTimeline.vID,
                    propType = objCustomerTimeline.propType,
                    GetServices = objCustomerTimeline.GetServices,
                    OtherLocation=objCustomerTimeline.OtherLocation,
                    StartDate=objCustomerTimeline.ServiceDate,
                    catsubID=objCustomerTimeline.catsubID,
                    catID=objCustomerTimeline.catID
                });

            }
            result = result.OrderBy(x => x.StartDate).ToList();
            return result;
        }

        public List<GetCustomerModelV4> GetHistoryCustomerTimeLines(int? cuID, int? catID, int? catsubID)
        {
            List<GetCustomerModelV4> result = new List<GetCustomerModelV4>();
            var objCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID && x.IsActive == true && x.IsDelete == false
                                        && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(DateTime.Now)).AsEnumerable()
                                .Select(p => new
                                {
                                    PaymentStatus = (UhDB.CustomerTransactions.Where(x => x.cuID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().PaymentStatus).ToString(),
                                    PackageName = p.packID != null ? p.Package.Name : null,
                                    Price = p.parkID != null ? p.Pricing.Price.ToString() : null,
                                    Duration = p.parkID != null ? p.Pricing.Duration : null,
                                    StartTime = p.StartTime,
                                    EndTime = p.EndTime,
                                    TaskNo = p.TaskNo.ToString(),
                                    ServiceDate = p.StartDate != null ? Convert.ToDateTime(p.StartDate).ToString("MM/dd/yyyy") : null,
                                    Status = p.CustomerOfficalDetail.ServiceStatus == true ? "Active" : p.CustomerOfficalDetail.ServiceStatus == false ? "InActive" : "Pending",
                                    WorkingStatus = p.StatusOfWork == 1 ? "Open" : p.StatusOfWork == 2 ? "Pending" : "Closed",
                                    teamID = p.CustomerOfficalDetail.teamID,
                                    cuID = cuID,
                                    custODID = p.custODID,
                                }).ToList();
            foreach (var objCustomerTimeLine in objCustomerTimeLines)
            {
                List<string> StaffNames = new List<string>();
                int? teamID = objCustomerTimeLine.teamID;
                var objStaffs = UhDB.StaffTeams.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objStaff in objStaffs)
                {
                    int? stfID = objStaff.stfID;
                    if (stfID != null)
                    {
                        string StaffName = UhDB.Staffs.Where(x => x.IsDelete == false && x.IsActive == true && x.stfID == stfID).FirstOrDefault().Name;
                        StaffNames.Add(StaffName);
                    }

                }
                string Staffs = null;
                if (StaffNames != null)
                {
                    Staffs = string.Join(", ", StaffNames);
                }
                result.Add(new GetCustomerModelV4
                {
                    PaymentStatus = objCustomerTimeLine.PaymentStatus,
                    PackageName = objCustomerTimeLine.PackageName,
                    Price = objCustomerTimeLine.Price,
                    Duration = objCustomerTimeLine.Duration,
                    StartTime = objCustomerTimeLine.StartTime,
                    EndTime = objCustomerTimeLine.EndTime,
                    TaskNo = objCustomerTimeLine.TaskNo,
                    ServiceDate = objCustomerTimeLine.ServiceDate,
                    Status = objCustomerTimeLine.Status,
                    WorkingStatus = objCustomerTimeLine.WorkingStatus,
                    Staffs = Staffs,
                    custODID = objCustomerTimeLine.custODID,
                    cuID = objCustomerTimeLine.cuID
                });
            }

            result = result.OrderBy(x => x.ServiceDate).ToList();


            return result;
        }

        public List<GetCustomerPaymentModel> GetCustomerPayment(int? cuID, int? catID, int? catsubID)
        {
            List<GetCustomerPaymentModel> result = new List<GetCustomerPaymentModel>();

            var objCustomerOfficalDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID && x.catID == catID && x.catsubID == catsubID).ToList();
            foreach (var objCustomerOfficalDetail in objCustomerOfficalDetails)
            {
                int? custODID = objCustomerOfficalDetail.custODID;
                double? Price = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Pricing.Price;
                int? NoOfMonths = objCustomerOfficalDetail.NoOfMonths;
                double? discountAmount = null;
                double? finalPrice = null;
                int? DiscountPercentage = null;
                if (NoOfMonths > 1)
                {
                    if (NoOfMonths == 3)
                    {
                        discountAmount = (3 / 100) * Price;
                        finalPrice = Price - discountAmount;
                        DiscountPercentage = 3;
                    }
                    else if (NoOfMonths == 5)
                    {
                        discountAmount = (5 / 100) * Price;
                        finalPrice = Price - discountAmount;
                        DiscountPercentage = 5;
                    }
                    else if (NoOfMonths == 10)
                    {
                        discountAmount = (10 / 100) * Price;
                        finalPrice = Price - discountAmount;
                        DiscountPercentage = 10;
                    }
                }
                else
                {
                    finalPrice = Price;
                    DiscountPercentage = 0;
                }
                string StartDate = Convert.ToDateTime(UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().StartDate).ToString("dd/MM/yyyy");
                string EndDate = Convert.ToDateTime(UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).OrderByDescending(x => x.custTDID).FirstOrDefault().StartDate).ToString("dd/MM/yyyy");
                String Paid = UhDB.CustomerTransactions.Where(x => x.cuID == cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().PaymentStatus != null ?
                                UhDB.CustomerTransactions.Where(x => x.cuID == cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().CustomerPaymentStatu.Name : "Not Paid";
                String PackageName = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Package.Name;
                result.Add(new GetCustomerPaymentModel { FinalPrice = Price.ToString(), NoOfMonths = NoOfMonths.ToString() + " Months", DiscountPercentage = DiscountPercentage == null ? "0 %" : DiscountPercentage.ToString() + " %", DiscountPrice = discountAmount.ToString(), StartDate = StartDate, EndDate = EndDate, Paid = Paid, PackageName = PackageName });
            }
            return result;
        }

        public List<GetDropDown> GetDashboardPropertyDropdown(int? cuID)
        {
            List<GetDropDown> result = new List<GetDropDown>();
            var objAppartmentDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                    .Select(p => new GetDropDown
                    {
                        ID = p.vID,
                        Value = p.AppartmentNumber + " : " + p.Venture.Name
                    }).ToList();
            foreach (var objAppartmentDetail in objAppartmentDetails)
            {
                if (!result.Any(x => x.Value == objAppartmentDetail.Value))
                {
                    result.Add(new GetDropDown { ID = objAppartmentDetail.ID, Value = objAppartmentDetail.Value });
                }
            }
            return result;
        }

        public List<MonthName> GetDashboardMonthsDropdown(int? cuID, int? catID, int? catsubID, int? vID, string AppartmentName)
        {
            List<MonthName> result = new List<MonthName>();
            if (catsubID == 1)
            {
                int CountNoOfMonths = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID && x.catID == catID && x.catsubID == catsubID && x.vID == vID && x.AppartmentNumber == AppartmentName && x.NoOfMonths != null && x.IsActive == true && x.IsDelete == false).Count();
                if (CountNoOfMonths == 0)
                {
                    //int custODID = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID && x.catID == catID && x.catsubID == catsubID && x.vID == vID && x.AppartmentNumber == AppartmentName && x.IsActive == true && x.IsDelete == false).FirstOrDefault().custODID??0;
                    var customerDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID &&
                                           x.catID == catID && x.catsubID == catsubID && x.vID == vID && x.AppartmentNumber == AppartmentName &&
                                           x.IsActive == true && x.IsDelete == false).FirstOrDefault();

                    int custODID = customerDetails?.custODID ?? default(int);
                    var objStartDate = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == custODID).ToList();
                    DateTime StartDate = Convert.ToDateTime(objStartDate[0].StartDate);
                    DateTime EndDate = Convert.ToDateTime(objStartDate.Last().StartDate);
                    if (EndDate.Month == StartDate.Month)
                    {
                        string Month = Convert.ToDateTime(StartDate).ToString("MMMM");
                        result.Add(new MonthName { Name = Month });
                    }
                    else
                    {
                        for (int i = 0; i <= 1; i++)
                        {
                            StartDate = StartDate.AddMonths(i);
                            string Month = Convert.ToDateTime(StartDate).ToString("MMMM");
                            result.Add(new MonthName { Name = Month });
                        }
                    }
                }
                else
                {
                    //                    int? Count = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID && x.catID == catID && x.catsubID == catsubID && x.vID == vID && x.AppartmentNumber == AppartmentName && x.IsActive == true && x.IsDelete == false).FirstOrDefault().CustomerRenewalMonth.NoOfMonths;

                    var record = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID && x.catID == catID
                         && x.catsubID == catsubID && x.vID == vID && x.AppartmentNumber == AppartmentName
                         && x.IsActive == true && x.IsDelete == false).FirstOrDefault();

                    int? Count = record?.CustomerRenewalMonth?.NoOfMonths;

                    if (Count != null)
                    {

                        int custODID = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID && x.catID == catID && x.catsubID == catsubID && x.vID == vID && x.AppartmentNumber == AppartmentName && x.IsActive == true && x.IsDelete == false).FirstOrDefault().custODID;
                        var objStartDate = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == custODID).OrderBy(x => x.StartDate).ToList();
                        DateTime StartDate = Convert.ToDateTime(objStartDate[0].StartDate);
                        DateTime EndDate = Convert.ToDateTime(objStartDate.Last().StartDate);
                        int monthsDifference = GetMonthsDifference(StartDate, EndDate);
                        if (EndDate.Month != StartDate.Month)
                        {
                            monthsDifference = monthsDifference + 2;
                        }
                        for (var i = 0; i < monthsDifference; i++)
                        {
                            DateTime SelectedStartDate = StartDate.AddMonths(i);
                            string Month = Convert.ToDateTime(SelectedStartDate).ToString("MMMM");
                            result.Add(new MonthName { Name = Month });
                        }

                    }
                }
            }
            else
            {
                var objStartDates = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID && x.CustomerOfficalDetail.vID == vID && x.CustomerOfficalDetail.AppartmentNumber == AppartmentName).ToList();
                foreach (var objStartDate in objStartDates)
                {
                    DateTime StartDate = Convert.ToDateTime(objStartDate.StartDate);
                    string Month = Convert.ToDateTime(StartDate).ToString("MMMM");
                    if (!result.Any(x => x.Name == Month))
                    {
                        result.Add(new MonthName { Name = Month });
                    }
                }
            }


            return result;
        }

        public int GetMonthsDifference(DateTime startDate, DateTime endDate)
        {
            int yearsDifference = endDate.Year - startDate.Year;
            int monthsDifference = endDate.Month - startDate.Month;

            // Total months
            int totalMonths = yearsDifference * 12 + monthsDifference;

            // If the day of the end date is less than the start date, subtract 1 month
            if (endDate.Day < startDate.Day)
            {
                totalMonths--;
            }

            return totalMonths;
        }

    }

    public class MonthName
    {
        public string Name { get; set; }
    }

    public class StartDates
    {
        public DateTime StartDate { get; set; }
    }
}