using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;
using System.Data.Entity.Core.Objects;

namespace UHSForm.DAL
{
    public class DashboardDB
    {
        private UHSEntities UhDB;
        public DashboardDB()
        {
            UhDB = new UHSEntities();
        }

        public GetDashboardCount GetDashboardCount(int? uID)
        {
            GetDashboardCount result = new GetDashboardCount();
            GetDashboardCountDetails objRegularCleaning = new GetDashboardCountDetails();
            GetDashboardCountDetails objDeepCleaning = new GetDashboardCountDetails();
            GetDashboardCountDetails objSofaCleaning = new GetDashboardCountDetails();
            GetDashboardCountDetails objMattressCleaning = new GetDashboardCountDetails();
            GetDashboardCountDetails objCarpetCleaning = new GetDashboardCountDetails();
            GetDashboardCountDetails objCurtainsCleaning = new GetDashboardCountDetails();
            int CountRegularCleaning = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 1
                                       && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID
                                       && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).Count();
            if (CountRegularCleaning != 0)
            {
                var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 1
                                       && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID
                                       && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).ToList();
                objRegularCleaning.catID = 1;
                objRegularCleaning.catsubID = 1;
                objRegularCleaning.Count = objCustomerTimelines.Count();
                objRegularCleaning.Name = objCustomerTimelines.FirstOrDefault().CustomerOfficalDetail.SubCategory.Name;
            }
            int CountDeepCleaning = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 2
                                    && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID
                                    && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).Count();
            if (CountDeepCleaning != 0)
            {
                var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 2
                                       && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID
                                       && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).ToList();
                objDeepCleaning.catID = 1;
                objDeepCleaning.catsubID = 2;
                objDeepCleaning.Count = objCustomerTimelines.Count();
                objDeepCleaning.Name = objCustomerTimelines.FirstOrDefault().CustomerOfficalDetail.SubCategory.Name;
            }
            int CountSofa = 0, CountMattress = 0, CountCarpet = 0, CountCurtains = 0;
            int SofaCount = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 1 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID).Count();
            if (SofaCount != 0)
            {
                var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 1 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID).ToList();
                foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
                {
                    int? cuID = objCustomerSpecializedCleaning.custID;
                    int? cuODID = objCustomerSpecializedCleaning.custODID;
                    int CountCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true
                                                 && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).Count();
                    CountSofa = CountSofa + CountCustomerTimelines;
                }
                objSofaCleaning.catID = 1;
                objSofaCleaning.catsubID = 3;
                objSofaCleaning.servcatID = 1;
                objSofaCleaning.Count = CountSofa;
                objSofaCleaning.Name = objCustomerSpecializedCleanings.FirstOrDefault().ServiceCategory.Name;

            }
            int MattressCount = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 2 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID).Count();
            if (MattressCount != 0)
            {
                var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 2 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID).ToList();
                foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
                {
                    int? cuID = objCustomerSpecializedCleaning.custID;
                    int? cuODID = objCustomerSpecializedCleaning.custODID;
                    int CountCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true
                                                 && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).Count();
                    CountMattress = CountMattress + CountCustomerTimelines;
                }
                objMattressCleaning.catID = 1;
                objMattressCleaning.catsubID = 3;
                objMattressCleaning.servcatID = 2;
                objMattressCleaning.Count = CountMattress;
                objMattressCleaning.Name = objCustomerSpecializedCleanings.FirstOrDefault().ServiceCategory.Name;
            }
            int CarpetCount = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 3 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID).Count();
            if (CarpetCount != 0)
            {
                var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 3 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID).ToList();
                foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
                {
                    int? cuID = objCustomerSpecializedCleaning.custID;
                    int? cuODID = objCustomerSpecializedCleaning.custODID;
                    int CountCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true
                                                 && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).Count();
                    CountCarpet = CountCarpet + CountCustomerTimelines;
                }
                objCarpetCleaning.catID = 1;
                objCarpetCleaning.catsubID = 3;
                objCarpetCleaning.servcatID = 3;
                objCarpetCleaning.Count = CountCarpet;
                objCarpetCleaning.Name = objCustomerSpecializedCleanings.FirstOrDefault().ServiceCategory.Name;
            }
            int CurtainsCount = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 4 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID).Count();
            if (CurtainsCount != 0)
            {
                var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 4 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID).ToList();
                foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
                {
                    int? cuID = objCustomerSpecializedCleaning.custID;
                    int? cuODID = objCustomerSpecializedCleaning.custODID;
                    int CountCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true
                                                 && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).Count();
                    CountCurtains = CountCurtains + CountCustomerTimelines;
                }
                objCurtainsCleaning.catID = 1;
                objCurtainsCleaning.catsubID = 3;
                objCurtainsCleaning.servcatID = 4;
                objCurtainsCleaning.Count = CountCurtains;
                objCurtainsCleaning.Name = objCustomerSpecializedCleanings.FirstOrDefault().ServiceCategory.Name;
            }
            result.RegularCleaning = objRegularCleaning;
            result.DeepCleaning = objDeepCleaning;
            result.SofaCleaning = objSofaCleaning;
            result.MattressCleaning = objMattressCleaning;
            result.CarpetCleaning = objCarpetCleaning;
            result.CurtainsCleaning = objCurtainsCleaning;
            return result;
        }

        public GetDashboardCount GetDashboardCarWashCount(int? uID)
        {
            GetDashboardCount result = new GetDashboardCount();
            GetDashboardCountDetails objCarWashingCleaning = new GetDashboardCountDetails();

            int CountCarWashCleaning = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 2
                                    && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID
                                    && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).Count();
            if (CountCarWashCleaning != 0)
            {
                var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 2
                                       && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID
                                       && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).ToList();
                objCarWashingCleaning.catID = 2;
                objCarWashingCleaning.Count = objCustomerTimelines.Count();
                objCarWashingCleaning.Name = objCustomerTimelines.FirstOrDefault().CustomerOfficalDetail.MainCategory.Name;
            }
            result.CarWashCleaning = objCarWashingCleaning;
            return result;
        }

        public IEnumerable<GetCustomerModelV2> GetCustomersForDashboard(int? uID, int? catID, int? catsubID)
        {

            List<GetCustomerModelV2> result = new List<GetCustomerModelV2>();
            result = UhDB.CustomerTimelines.Where(x => x.Customer.uID == uID && x.IsActive == true && x.IsDelete == false
                     && x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).AsEnumerable()
                     .Select(p => new GetCustomerModelV2
                     {
                         Name = p.Customer.Name,
                         Email = p.Customer.Email,
                         Mobile = p.Customer.Mobile,
                         WhatsAppNo = p.Customer.WhatsAppNo,
                         AlternativeNo = p.Customer.AlternativeNo,
                         Saluation = p.Customer.Salutaion,
                         WorkStatus = p.StatusOfWork,
                         Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                               UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                               .Select(s => new GetFileDetails
                               {
                                   Name = s.Filename,
                                   ContentType = s.FileContentType,
                                   Size = s.FileSize,
                                   Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

                               }).ToList() : null,
                         MainCategory = p.CustomerOfficalDetail.MainCategory.Name,
                         SubCategory = p.CustomerOfficalDetail.catsubID != null ? p.CustomerOfficalDetail.SubCategory.Name : "N/A",
                         Area = p.CustomerOfficalDetail.PropertyArea.Name,
                         PropertyName = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name :
                                        UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().TowerName,
                         PropertyResidencyType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : null,
                         Remarks = p.CustomerOfficalDetail.Remarks,
                         ApartmentName = p.CustomerOfficalDetail.AppartmentNumber,
                         OtherLocation = p.CustomerOfficalDetail.propType == 2 ? UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                       .Select(u => new GetOtherLocationModel
                                       {
                                           TowerName = u.TowerName,
                                           BuildingName = u.BuildingName,
                                           StreetNumber = u.StreetNumber,
                                           ZoneNumber = u.ZoneNumber,
                                           Loacation = u.Loacation,
                                           LocationLink = u.LocationLink
                                       }).FirstOrDefault() : null,
                         GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                    .Select(r => new GetCustomerAvailabilityModel
                                                    {
                                                        custSCID = r.custSCID,
                                                        Availability = r.Availability,
                                                        KeyCollection = r.KeyCollection,
                                                        AccessProperty = r.AccessProperty,
                                                        ReceptionDate = r.ReceptionDate
                                                    }).FirstOrDefault(),
                         PaymentStatus = UhDB.CustomerTransactions.Where(x => x.cuID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                      .Select(c => new GetCustomerPaymentStatus { PayementID = c.PayementID, PaymentStatus = c.PaymentStatus, Price = c.Price, TotalPrice = c.TotalPrice, TransactionID = c.TransactionID, Quantity = c.Quantity, custTrasID = c.custTrasID }).FirstOrDefault(),
                         CustomDates = UhDB.CustomerCustomDateTimes.Where(x => x.cuID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                    .Select(b => new GetCustomerCustomeDate { CustomDays = b.CustomDays, CustomEndTime = b.CustomEndTime, CustomStartDate = b.CustomStartDate != null ? Convert.ToDateTime(b.CustomStartDate).ToString("dd/MM/yyyy") : null, CustomStartTime = b.CustomStartTime, custDTID = b.custDTID }).FirstOrDefault(),
                         GetServices = UhDB.CustomerSpecializedCleanings.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable().
                                       Select(t => new GetServiceSubCategoryModel
                                       {
                                           custSCID = t.custSCID,
                                           servcatID = t.servcatID,
                                           servsubcatID = t.servsubcatID,
                                           ServiceCategoryName = t.ServiceCategory.Name,
                                           ServiceSubCategoryName = t.ServiceSubCategory.Name,
                                           Quantity = t.Quantity
                                       }).ToList(),
                         carstID = p.CustomerOfficalDetail.carstID,
                         cartID = p.CustomerOfficalDetail.cartID,
                         IsCarWash = p.CustomerOfficalDetail.IsCarWash,
                         carTRID=p.CustomerOfficalDetail.carTRID,
                         CarWashTimes=p.CustomerOfficalDetail.carTRID!=null?p.CustomerOfficalDetail.CarWashTimeRange.Name+" "+ p.CustomerOfficalDetail.CarWashTimeRange.Timing:null,
                         CarType = p.CustomerOfficalDetail.CarType != null ? p.CustomerOfficalDetail.CarType.Name : "N/A",
                         CarServiceType = p.CustomerOfficalDetail.CarServiceType != null ? p.CustomerOfficalDetail.CarServiceType.Name : "N/A",
                         CustomCarDetails = p.CustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                          .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,
                         PackageName = p.packID != null ? p.Package.Name : null,
                         Price = p.parkID != null ? p.Pricing.Price.ToString() : null,
                         Duration = p.parkID != null ? p.Pricing.Duration : null,
                         Measurement = p.parkID != null ? p.Pricing.TimeMeasurement : null,
                         packID = p.packID,
                         parkID = p.parkID,
                         Date = Convert.ToDateTime(p.StartDate).ToString("MM/dd/yyyy"),
                         WeeklyCounts = p.packID != null ? p.Package.RecursiveTime.ToString() : null,
                         ServiceDays = p.CustomerOfficalDetail.BuldleDays,
                         EndDate = Convert.ToDateTime(UhDB.CustomerTimelines.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).OrderByDescending(j => j.custTDID).FirstOrDefault().StartDate).ToString("dd/MM/yyyy"),
                         CustomerType = p.Customer.CustomerType != null ? p.Customer.CustomerType1.Name : null,
                         StartTime = p.StartTime,
                         EndTime = p.EndTime,
                         TeamName = p.teamID != null ? p.Team.Name : null,
                         staffName = p.CustomerOfficalDetail.stfID != null ? p.CustomerOfficalDetail.Staff.Name : null,
                         TaskNo = p.TaskNo,
                         NoOfMonths = p.CustomerOfficalDetail.NoOfMonths != null ? p.CustomerOfficalDetail.CustomerRenewalMonth.Name : "N/A",
                         CreatedOn = Convert.ToDateTime(p.CreatedOn).ToString("MM/dd/yyyy"),
                         CustomerID = p.Customer.CustomerID,
                         ServiceStatus = p.CustomerOfficalDetail.ServiceStatus == true ? "Active" : p.CustomerOfficalDetail.ServiceStatus == false ? "InActive" : "Pending",
                         stfID = p.CustomerOfficalDetail.stfID,
                         teamID = p.teamID,
                         propaID = p.CustomerOfficalDetail.propaID,
                         vID = p.CustomerOfficalDetail.vID,
                         proprestID = p.CustomerOfficalDetail.proprestID,
                         propType = p.CustomerOfficalDetail.propType,
                         custOPID = p.CustomerOfficalDetail.propType == 2 ? (UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().custODID) : null,
                         cuID = p.custID,
                         cuODID = p.custODID,
                         catID = p.CustomerOfficalDetail.catID,
                         catsubID = p.CustomerOfficalDetail.catsubID
                     }).ToList();


            return result;
        }

        public IEnumerable<GetCustomerModelV2> GetCustomersSpecialServiceForDashboard(int? uID, int? catID, int? catsubID, int? servcatID)
        {

            List<GetCustomerModelV2> result = new List<GetCustomerModelV2>();
            var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.CustomerOfficalDetail.catID == catID
                                                  && x.CustomerOfficalDetail.catsubID == catsubID && x.servcatID == servcatID
                                                  && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID).ToList();
            foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
            {
                int? cuID = objCustomerSpecializedCleaning.custID;
                int? cuODID = objCustomerSpecializedCleaning.custODID;
                var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true
                                             && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now))
                                             .ToList();
                if (objCustomerTimelines != null)
                {

                    foreach (var p in objCustomerTimelines)
                    {
                        result.Add(new GetCustomerModelV2
                        {
                            Name = p.Customer.Name,
                            Email = p.Customer.Email,
                            Mobile = p.Customer.Mobile,
                            WhatsAppNo = p.Customer.WhatsAppNo,
                            AlternativeNo = p.Customer.AlternativeNo,
                            Saluation = p.Customer.Salutaion,
                            WorkStatus = p.StatusOfWork,
                            Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                               UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                               .Select(s => new GetFileDetails
                               {
                                   Name = s.Filename,
                                   ContentType = s.FileContentType,
                                   Size = s.FileSize,
                                   Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Test/CustomerUploads/" + s.FileFieldName

                               }).ToList() : null,
                            MainCategory = p.CustomerOfficalDetail.MainCategory.Name,
                            SubCategory = p.CustomerOfficalDetail.SubCategory.Name,
                            Area = p.CustomerOfficalDetail.PropertyArea.Name,
                            PropertyName = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name :
                                        UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().TowerName,
                            PropertyResidencyType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : null,
                            Remarks = p.CustomerOfficalDetail.Remarks,
                            ApartmentName = p.CustomerOfficalDetail.AppartmentNumber,
                            OtherLocation = p.CustomerOfficalDetail.propType == 2 ? UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                       .Select(u => new GetOtherLocationModel
                                       {
                                           TowerName = u.TowerName,
                                           BuildingName = u.BuildingName,
                                           StreetNumber = u.StreetNumber,
                                           ZoneNumber = u.ZoneNumber,
                                           Loacation = u.Loacation,
                                           LocationLink = u.LocationLink
                                       }).FirstOrDefault() : null,
                            GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                    .Select(r => new GetCustomerAvailabilityModel
                                                    {
                                                        custSCID = r.custSCID,
                                                        Availability = r.Availability,
                                                        KeyCollection = r.KeyCollection,
                                                        AccessProperty = r.AccessProperty,
                                                        ReceptionDate = r.ReceptionDate
                                                    }).FirstOrDefault(),
                            PaymentStatus = UhDB.CustomerTransactions.Where(x => x.cuID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                      .Select(c => new GetCustomerPaymentStatus { PayementID = c.PayementID, PaymentStatus = c.PaymentStatus, Price = c.Price, TotalPrice = c.TotalPrice, TransactionID = c.TransactionID, Quantity = c.Quantity, custTrasID = c.custTrasID }).FirstOrDefault(),
                            CustomDates = UhDB.CustomerCustomDateTimes.Where(x => x.cuID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                    .Select(b => new GetCustomerCustomeDate { CustomDays = b.CustomDays, CustomEndTime = b.CustomEndTime, CustomStartDate = b.CustomStartDate != null ? Convert.ToDateTime(b.CustomStartDate).ToString("dd/MM/yyyy") : null, CustomStartTime = b.CustomStartTime, custDTID = b.custDTID }).FirstOrDefault(),
                            GetServices = UhDB.CustomerSpecializedCleanings.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable().
                                       Select(t => new GetServiceSubCategoryModel
                                       {
                                           custSCID = t.custSCID,
                                           servcatID = t.servcatID,
                                           servsubcatID = t.servsubcatID,
                                           ServiceCategoryName = t.ServiceCategory.Name,
                                           ServiceSubCategoryName = t.ServiceSubCategory.Name,
                                           Quantity = t.Quantity
                                       }).ToList(),
                            PackageName = p.packID != null ? p.Package.Name : null,
                            Price = p.parkID != null ? p.Pricing.Price.ToString() : null,
                            Duration = p.parkID != null ? p.Pricing.Duration : null,
                            Measurement = p.parkID != null ? p.Pricing.TimeMeasurement : null,
                            packID = p.packID,
                            parkID = p.parkID,
                            Date = Convert.ToDateTime(p.StartDate).ToString("MM/dd/yyyy"),
                            WeeklyCounts = p.packID != null ? p.Package.RecursiveTime.ToString() : null,
                            ServiceDays = p.CustomerOfficalDetail.BuldleDays,
                            EndDate = Convert.ToDateTime(UhDB.CustomerTimelines.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).OrderByDescending(j => j.custTDID).FirstOrDefault().StartDate).ToString("dd/MM/yyyy"),
                            CustomerType = p.Customer.CustomerType != null ? p.Customer.CustomerType1.Name : null,
                            StartTime = p.StartTime,
                            EndTime = p.EndTime,
                            TeamName = p.CustomerOfficalDetail.teamID != null ? p.CustomerOfficalDetail.Team.Name : null,
                            staffName = p.CustomerOfficalDetail.stfID != null ? p.CustomerOfficalDetail.Staff.Name : null,
                            TaskNo = p.TaskNo,
                            CreatedOn = Convert.ToDateTime(p.CreatedOn).ToString("dd/MM/yyyy"),
                            CustomerID = p.Customer.CustomerID,
                            ServiceStatus = p.CustomerOfficalDetail.ServiceStatus == true ? "Active" : p.CustomerOfficalDetail.ServiceStatus == false ? "InActive" : "Pending",
                            stfID = p.CustomerOfficalDetail.stfID,
                            teamID = p.CustomerOfficalDetail.teamID,
                            propaID = p.CustomerOfficalDetail.propaID,
                            vID = p.CustomerOfficalDetail.vID,
                            proprestID = p.CustomerOfficalDetail.proprestID,
                            propType = p.CustomerOfficalDetail.propType,
                            custOPID = p.CustomerOfficalDetail.propType == 2 ? (UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().custODID) : null,
                            cuID = p.custID,
                            cuODID = p.custODID,
                            catID = p.CustomerOfficalDetail.catID,
                            catsubID = p.CustomerOfficalDetail.catsubID,
                            NoOfMonths = p.CustomerOfficalDetail.NoOfMonths.ToString()

                        });
                    }

                }
            }

            return result;
        }

        public GetDashboardCount GetStaffDashboardCount(int? uID, int? stfID)
        {
            GetDashboardCount result = new GetDashboardCount();
            GetDashboardCountDetails objRegularCleaning = new GetDashboardCountDetails();
            GetDashboardCountDetails objDeepCleaning = new GetDashboardCountDetails();
            GetDashboardCountDetails objSofaCleaning = new GetDashboardCountDetails();
            GetDashboardCountDetails objMattressCleaning = new GetDashboardCountDetails();
            GetDashboardCountDetails objCarpetCleaning = new GetDashboardCountDetails();
            GetDashboardCountDetails objCurtainsCleaning = new GetDashboardCountDetails();
            GetDashboardCountDetails objCarWashCleaning = new GetDashboardCountDetails();
            int RegularCleaningCount = 0, DeepCleaningCount = 0, CarWashCleaningCount = 0, CountSofa = 0, CountMattress = 0, CountCarpet = 0, CountCurtains = 0;
            var objTeams = UhDB.StaffTeams.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objTeam in objTeams)
            {
                int? teamID = objTeam.teamID;
                int CountRegularCleaning = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 1
                           && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == teamID
                           && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).Count();
                RegularCleaningCount = RegularCleaningCount + CountRegularCleaning;
                int CountDeepCleaning = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 2
                                    && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == teamID
                                    && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).Count();
                DeepCleaningCount = DeepCleaningCount + CountDeepCleaning;
                int CountCarWashCleaning = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 2
                                   && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == teamID
                                   && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).Count();
                CarWashCleaningCount = CarWashCleaningCount + CountCarWashCleaning;
                int SofaCount = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 1 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == teamID).Count();
                if (SofaCount != 0)
                {
                    var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 1 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == teamID).ToList();
                    foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
                    {
                        int? cuID = objCustomerSpecializedCleaning.custID;
                        int? cuODID = objCustomerSpecializedCleaning.custODID;
                        int CountCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true
                                                     && x.IsDelete == false && x.CustomerOfficalDetail.teamID == teamID &&
                                                     EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).Count();
                        CountSofa = CountSofa + CountCustomerTimelines;
                    }
                }
                int MattressCount = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 2 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == teamID).Count();
                if (MattressCount != 0)
                {
                    var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 2 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == teamID).ToList();
                    foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
                    {
                        int? cuID = objCustomerSpecializedCleaning.custID;
                        int? cuODID = objCustomerSpecializedCleaning.custODID;
                        int CountCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true && x.CustomerOfficalDetail.teamID == teamID
                                                     && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).Count();
                        CountMattress = CountMattress + CountCustomerTimelines;
                    }
                }

                int CarpetCount = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 3 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == teamID).Count();
                if (CarpetCount != 0)
                {
                    var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 3 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == teamID).ToList();
                    foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
                    {
                        int? cuID = objCustomerSpecializedCleaning.custID;
                        int? cuODID = objCustomerSpecializedCleaning.custODID;
                        int CountCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true && x.CustomerOfficalDetail.teamID == teamID
                                                     && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).Count();
                        CountCarpet = CountCarpet + CountCustomerTimelines;
                    }
                }
                int CurtainsCount = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 4 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == teamID).Count();
                if (CurtainsCount != 0)
                {
                    var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 4 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == teamID).ToList();
                    foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
                    {
                        int? cuID = objCustomerSpecializedCleaning.custID;
                        int? cuODID = objCustomerSpecializedCleaning.custODID;
                        int CountCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true && x.CustomerOfficalDetail.teamID == teamID
                                                     && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).Count();
                        CountCurtains = CountCurtains + CountCustomerTimelines;
                    }
                }
            }
            int CountRegularCleaning1 = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 1
                       && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == stfID
                       && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).Count();
            RegularCleaningCount = RegularCleaningCount + CountRegularCleaning1;
            int CountDeepCleaning1 = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 2
                                   && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == stfID
                                   && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).Count();
            DeepCleaningCount = DeepCleaningCount + CountDeepCleaning1;
            int CountCarWashCleaning1 = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 2
                   && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == stfID
                   && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).Count();
            CarWashCleaningCount = CarWashCleaningCount + CountCarWashCleaning1;
            int SofaCount1 = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 1 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == stfID).Count();
            if (SofaCount1 != 0)
            {
                var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 1 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == stfID).ToList();
                foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
                {
                    int? cuID = objCustomerSpecializedCleaning.custID;
                    int? cuODID = objCustomerSpecializedCleaning.custODID;
                    int CountCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true
                                                 && x.IsDelete == false && x.CustomerOfficalDetail.teamID == stfID &&
                                                 EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).Count();
                    CountSofa = CountSofa + CountCustomerTimelines;
                }
            }
            int MattressCount1 = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 2 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == stfID).Count();
            if (MattressCount1 != 0)
            {
                var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 2 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == stfID).ToList();
                foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
                {
                    int? cuID = objCustomerSpecializedCleaning.custID;
                    int? cuODID = objCustomerSpecializedCleaning.custODID;
                    int CountCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true && x.CustomerOfficalDetail.teamID == stfID
                                                 && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).Count();
                    CountMattress = CountMattress + CountCustomerTimelines;
                }
            }
            int CarpetCount1 = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 3 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == stfID).Count();
            if (CarpetCount1 != 0)
            {
                var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 3 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == stfID).ToList();
                foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
                {
                    int? cuID = objCustomerSpecializedCleaning.custID;
                    int? cuODID = objCustomerSpecializedCleaning.custODID;
                    int CountCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true && x.CustomerOfficalDetail.teamID == stfID
                                                 && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).Count();
                    CountCarpet = CountCarpet + CountCustomerTimelines;
                }
            }
            int CurtainsCount1 = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 4 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == stfID).Count();
            if (CurtainsCount1 != 0)
            {
                var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 4 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == stfID).ToList();
                foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
                {
                    int? cuID = objCustomerSpecializedCleaning.custID;
                    int? cuODID = objCustomerSpecializedCleaning.custODID;
                    int CountCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true && x.CustomerOfficalDetail.teamID == stfID
                                                 && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).Count();
                    CountCurtains = CountCurtains + CountCustomerTimelines;
                }
            }
            objRegularCleaning.catID = 1;
            objRegularCleaning.catsubID = 1;
            objRegularCleaning.Count = RegularCleaningCount;
            objRegularCleaning.Name = UhDB.SubCategories.Where(x => x.catsubID == 1 && x.catID == 1 && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
            objDeepCleaning.catID = 1;
            objDeepCleaning.catsubID = 2;
            objDeepCleaning.Count = DeepCleaningCount;
            objDeepCleaning.Name = UhDB.SubCategories.Where(x => x.catsubID == 2 && x.catID == 1 && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
            objCarWashCleaning.catID = 2;
            objCarWashCleaning.Name = UhDB.MainCategories.Where(x => x.catID == 1 && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
            objSofaCleaning.catID = 1;
            objSofaCleaning.catsubID = 3;
            objSofaCleaning.servcatID = 1;
            objSofaCleaning.Count = CountSofa;
            objSofaCleaning.Name = UhDB.ServiceCategories.Where(x => x.servcatID == 1 && x.catID == 1 && x.catsubID == 3 && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
            objMattressCleaning.catID = 1;
            objMattressCleaning.catsubID = 3;
            objMattressCleaning.servcatID = 2;
            objMattressCleaning.Count = CountMattress;
            objMattressCleaning.Name = UhDB.ServiceCategories.Where(x => x.servcatID == 2 && x.catID == 1 && x.catsubID == 3 && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
            objCarpetCleaning.catID = 1;
            objCarpetCleaning.catsubID = 3;
            objCarpetCleaning.servcatID = 3;
            objCarpetCleaning.Count = CountCarpet;
            objCarpetCleaning.Name = UhDB.ServiceCategories.Where(x => x.servcatID == 3 && x.catID == 1 && x.catsubID == 3 && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                     UhDB.ServiceCategories.Where(x => x.servcatID == 3 && x.catID == 1 && x.catsubID == 3 && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name : null;
            objCurtainsCleaning.catID = 1;
            objCurtainsCleaning.catsubID = 3;
            objCurtainsCleaning.servcatID = 4;
            objCurtainsCleaning.Count = CountCurtains;
            objCurtainsCleaning.Name = UhDB.ServiceCategories.Where(x => x.servcatID == 4 && x.catID == 1 && x.catsubID == 3 && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                       UhDB.ServiceCategories.Where(x => x.servcatID == 4 && x.catID == 1 && x.catsubID == 3 && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name : null;
            result.RegularCleaning = objRegularCleaning;
            result.DeepCleaning = objDeepCleaning;
            result.SofaCleaning = objSofaCleaning;
            result.MattressCleaning = objMattressCleaning;
            result.CarpetCleaning = objCarpetCleaning;
            result.CurtainsCleaning = objCurtainsCleaning;
            result.CarWashCleaning = objCarWashCleaning;
            return result;
        }

        public GetDashboardCount GetStaffDashboardMonthlyCount(int? uID, int? stfID)
        {
            GetDashboardCount result = new GetDashboardCount();
            GetDashboardCountDetails objRegularCleaning = new GetDashboardCountDetails();
            GetDashboardCountDetails objDeepCleaning = new GetDashboardCountDetails();
            GetDashboardCountDetails objSofaCleaning = new GetDashboardCountDetails();
            GetDashboardCountDetails objMattressCleaning = new GetDashboardCountDetails();
            GetDashboardCountDetails objCarpetCleaning = new GetDashboardCountDetails();
            GetDashboardCountDetails objCurtainsCleaning = new GetDashboardCountDetails();
            GetDashboardCountDetails objCarWashCleaning = new GetDashboardCountDetails();
            DateTime TodaysDate = DateTime.Now;
            int Year = TodaysDate.Year;
            int Month = TodaysDate.Month;
            int AddDates = DateTime.DaysInMonth(Year, Month);
            DateTime StartDate = new DateTime(Year, Month, 1);
            DateTime EndDate = StartDate.AddDays(AddDates);


            int RegularCleaningCount = 0, DeepCleaningCount = 0, CarWashCleaningCount = 0, CountSofa = 0, CountMattress = 0, CountCarpet = 0, CountCurtains = 0;
            var objTeams = UhDB.StaffTeams.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objTeam in objTeams)
            {
                int? teamID = objTeam.teamID;
                int CountRegularCleaning = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 1
                           && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == teamID
                           && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(EndDate)).Count();
                RegularCleaningCount = RegularCleaningCount + CountRegularCleaning;
                int CountDeepCleaning = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 2
                                    && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == teamID
                                    && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(EndDate)).Count();
                DeepCleaningCount = DeepCleaningCount + CountDeepCleaning;
                int CountCarWash = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 2
                                      && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == teamID
                                      && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(EndDate)).Count();
                CarWashCleaningCount = CarWashCleaningCount + CountCarWash;
                int SofaCount = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 1 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == teamID).Count();
                if (SofaCount != 0)
                {
                    var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 1 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == teamID).ToList();
                    foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
                    {
                        int? cuID = objCustomerSpecializedCleaning.custID;
                        int? cuODID = objCustomerSpecializedCleaning.custODID;
                        int CountCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true
                                                     && x.IsDelete == false && x.CustomerOfficalDetail.teamID == teamID &&
                                                     EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(EndDate)).Count();
                        CountSofa = CountSofa + CountCustomerTimelines;
                    }
                }
                int MattressCount = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 2 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == teamID).Count();
                if (MattressCount != 0)
                {
                    var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 2 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == teamID).ToList();
                    foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
                    {
                        int? cuID = objCustomerSpecializedCleaning.custID;
                        int? cuODID = objCustomerSpecializedCleaning.custODID;
                        int CountCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true && x.CustomerOfficalDetail.teamID == teamID
                                                     && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(EndDate)).Count();
                        CountMattress = CountMattress + CountCustomerTimelines;
                    }
                }

                int CarpetCount = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 3 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == teamID).Count();
                if (CarpetCount != 0)
                {
                    var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 3 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == teamID).ToList();
                    foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
                    {
                        int? cuID = objCustomerSpecializedCleaning.custID;
                        int? cuODID = objCustomerSpecializedCleaning.custODID;
                        int CountCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true && x.CustomerOfficalDetail.teamID == teamID
                                                     && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(EndDate)).Count();
                        CountCarpet = CountCarpet + CountCustomerTimelines;
                    }
                }
                int CurtainsCount = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 4 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == teamID).Count();
                if (CurtainsCount != 0)
                {
                    var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 4 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == teamID).ToList();
                    foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
                    {
                        int? cuID = objCustomerSpecializedCleaning.custID;
                        int? cuODID = objCustomerSpecializedCleaning.custODID;
                        int CountCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true && x.CustomerOfficalDetail.teamID == teamID
                                                     && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(EndDate)).Count();
                        CountCurtains = CountCurtains + CountCustomerTimelines;
                    }
                }
            }
            int CountRegularCleaning1 = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 1
                       && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == stfID
                       && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(EndDate)).Count();
            RegularCleaningCount = RegularCleaningCount + CountRegularCleaning1;
            int CountDeepCleaning1 = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 2
                                   && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.stfID == stfID
                                   && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(EndDate)).Count();
            DeepCleaningCount = DeepCleaningCount + CountDeepCleaning1;
            int CountCarWash1 = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 2
                      && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == stfID
                      && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(EndDate)).Count();
            CarWashCleaningCount = CarWashCleaningCount + CountCarWash1;
            int SofaCount1 = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 1 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == stfID).Count();
            if (SofaCount1 != 0)
            {
                var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 1 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == stfID).ToList();
                foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
                {
                    int? cuID = objCustomerSpecializedCleaning.custID;
                    int? cuODID = objCustomerSpecializedCleaning.custODID;
                    int CountCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true
                                                 && x.IsDelete == false && x.CustomerOfficalDetail.teamID == stfID &&
                                                 EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(EndDate)).Count();
                    CountSofa = CountSofa + CountCustomerTimelines;
                }
            }
            int MattressCount1 = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 2 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == stfID).Count();
            if (MattressCount1 != 0)
            {
                var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 2 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == stfID).ToList();
                foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
                {
                    int? cuID = objCustomerSpecializedCleaning.custID;
                    int? cuODID = objCustomerSpecializedCleaning.custODID;
                    int CountCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true && x.CustomerOfficalDetail.teamID == stfID
                                                 && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(EndDate)).Count();
                    CountMattress = CountMattress + CountCustomerTimelines;
                }
            }
            int CarpetCount1 = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 3 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == stfID).Count();
            if (CarpetCount1 != 0)
            {
                var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 3 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == stfID).ToList();
                foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
                {
                    int? cuID = objCustomerSpecializedCleaning.custID;
                    int? cuODID = objCustomerSpecializedCleaning.custODID;
                    int CountCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true && x.CustomerOfficalDetail.teamID == stfID
                                                 && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(EndDate)).Count();
                    CountCarpet = CountCarpet + CountCustomerTimelines;
                }
            }
            int CurtainsCount1 = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 4 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == stfID).Count();
            if (CurtainsCount1 != 0)
            {
                var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.servcatID == 4 && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID && x.CustomerOfficalDetail.teamID == stfID).ToList();
                foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
                {
                    int? cuID = objCustomerSpecializedCleaning.custID;
                    int? cuODID = objCustomerSpecializedCleaning.custODID;
                    int CountCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true && x.CustomerOfficalDetail.teamID == stfID
                                                 && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(EndDate)).Count();
                    CountCurtains = CountCurtains + CountCustomerTimelines;
                }
            }
            objRegularCleaning.catID = 1;
            objRegularCleaning.catsubID = 1;
            objRegularCleaning.Count = RegularCleaningCount;
            objRegularCleaning.Name = UhDB.SubCategories.Where(x => x.catsubID == 1 && x.catID == 1 && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
            objDeepCleaning.catID = 1;
            objDeepCleaning.catsubID = 2;
            objDeepCleaning.Count = DeepCleaningCount;
            objCarWashCleaning.catID = 2;
            objCarWashCleaning.Name = UhDB.MainCategories.Where(x => x.catID == 2 && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
            objDeepCleaning.Name = UhDB.SubCategories.Where(x => x.catsubID == 2 && x.catID == 1 && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
            objSofaCleaning.catID = 1;
            objSofaCleaning.catsubID = 3;
            objSofaCleaning.servcatID = 1;
            objSofaCleaning.Count = CountSofa;
            objSofaCleaning.Name = UhDB.ServiceCategories.Where(x => x.servcatID == 1 && x.catID == 1 && x.catsubID == 3 && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
            objMattressCleaning.catID = 1;
            objMattressCleaning.catsubID = 3;
            objMattressCleaning.servcatID = 2;
            objMattressCleaning.Count = CountMattress;
            objMattressCleaning.Name = UhDB.ServiceCategories.Where(x => x.servcatID == 2 && x.catID == 1 && x.catsubID == 3 && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
            objCarpetCleaning.catID = 1;
            objCarpetCleaning.catsubID = 3;
            objCarpetCleaning.servcatID = 3;
            objCarpetCleaning.Count = CountCarpet;
            objCarpetCleaning.Name = UhDB.ServiceCategories.Where(x => x.servcatID == 3 && x.catID == 1 && x.catsubID == 3 && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                     UhDB.ServiceCategories.Where(x => x.servcatID == 3 && x.catID == 1 && x.catsubID == 3 && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name : null;
            objCurtainsCleaning.catID = 1;
            objCurtainsCleaning.catsubID = 3;
            objCurtainsCleaning.servcatID = 4;
            objCurtainsCleaning.Count = CountCurtains;
            objCurtainsCleaning.Name = UhDB.ServiceCategories.Where(x => x.servcatID == 4 && x.catID == 1 && x.catsubID == 3 && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                       UhDB.ServiceCategories.Where(x => x.servcatID == 4 && x.catID == 1 && x.catsubID == 3 && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name : null;
            result.RegularCleaning = objRegularCleaning;
            result.DeepCleaning = objDeepCleaning;
            result.SofaCleaning = objSofaCleaning;
            result.MattressCleaning = objMattressCleaning;
            result.CarpetCleaning = objCarpetCleaning;
            result.CurtainsCleaning = objCurtainsCleaning;
            result.CarWashCleaning = objCarWashCleaning;
            return result;
        }

        public IEnumerable<GetCustomerStaffModel> GetTodaysCustomersForDashboard(int? uID, int? catID, int? catsubID, int? stfID)
        {

            List<GetCustomerStaffModel> result = new List<GetCustomerStaffModel>();
            //result = UhDB.CustomerTimelines.Where(x => x.Customer.uID == uID && x.IsActive == true && x.IsDelete == false
            //         && x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID && x.CustomerOfficalDetail.stfID == stfID
            //         && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).AsEnumerable()
            //         .Select(p => new GetCustomerStaffModel
            //         {
            //             Name = p.Customer.Name,
            //             Email = p.Customer.Email,
            //             Mobile = p.Customer.Mobile,
            //             WhatsAppNo = p.Customer.WhatsAppNo,
            //             AlternativeNo = p.Customer.AlternativeNo,
            //             Saluation = p.Customer.Salutaion,
            //             WorkStatus = p.StatusOfWork,
            //             Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
            //                   UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
            //                   .Select(s => new GetFileDetails
            //                   {
            //                       Name = s.Filename,
            //                       ContentType = s.FileContentType,
            //                       Size = s.FileSize,
            //                       Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

            //                   }).ToList() : null,
            //             MainCategory = p.CustomerOfficalDetail.MainCategory.Name,
            //             SubCategory = p.CustomerOfficalDetail.catsubID != null ? p.CustomerOfficalDetail.SubCategory.Name : "N/A",
            //             Area = p.CustomerOfficalDetail.PropertyArea.Name,
            //             PropertyName = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name :
            //                            UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().TowerName,
            //             PropertyResidencyType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : null,
            //             Remarks = p.CustomerOfficalDetail.Remarks,
            //             ApartmentName = p.CustomerOfficalDetail.AppartmentNumber,
            //             OtherLocation = p.CustomerOfficalDetail.propType == 2 ? UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
            //                           .Select(u => new GetOtherLocationModel
            //                           {
            //                               TowerName = u.TowerName,
            //                               BuildingName = u.BuildingName,
            //                               StreetNumber = u.StreetNumber,
            //                               ZoneNumber = u.ZoneNumber,
            //                               Loacation = u.Loacation,
            //                               LocationLink = u.LocationLink
            //                           }).FirstOrDefault() : null,
            //             GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
            //                                        .Select(r => new GetCustomerAvailabilityModel
            //                                        {
            //                                            custSCID = r.custSCID,
            //                                            Availability = r.Availability,
            //                                            KeyCollection = r.KeyCollection,
            //                                            AccessProperty = r.AccessProperty,
            //                                            ReceptionDate = r.ReceptionDate
            //                                        }).FirstOrDefault(),
            //             GetServices = UhDB.CustomerSpecializedCleanings.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable().
            //                           Select(t => new GetServiceSubCategoryModel
            //                           {
            //                               custSCID = t.custSCID,
            //                               servcatID = t.servcatID,
            //                               servsubcatID = t.servsubcatID,
            //                               ServiceCategoryName = t.ServiceCategory.Name,
            //                               ServiceSubCategoryName = t.ServiceSubCategory.Name,
            //                               Quantity = t.Quantity
            //                           }).ToList(),
            //             carstID = p.CustomerOfficalDetail.carstID,
            //             cartID = p.CustomerOfficalDetail.cartID,
            //             IsCarWash = p.CustomerOfficalDetail.IsCarWash,
            //             CarType = p.CustomerOfficalDetail.CarType != null ? p.CustomerOfficalDetail.CarType.Name : "N/A",
            //             CarServiceType = p.CustomerOfficalDetail.CarServiceType != null ? p.CustomerOfficalDetail.CarServiceType.Name : "N/A",
            //             CustomCarDetails = p.CustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
            //                              .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,

            //             PackageName = p.packID != null ? p.Package.Name : null,
            //             Price = p.packID != null ? p.Pricing.Price.ToString() : null,
            //             Duration = p.packID != null ? p.Pricing.Duration : null,
            //             Measurement = p.packID != null ? p.Pricing.TimeMeasurement : null,
            //             packID = p.packID,
            //             parkID = p.parkID,
            //             Date = Convert.ToDateTime(p.StartDate).ToString("MM/dd/yyyy"),
            //             WeeklyCounts = p.packID != null ? p.Package.RecursiveTime.ToString() : null,
            //             ServiceDays = p.CustomerOfficalDetail.BuldleDays,
            //             EndDate = Convert.ToDateTime(UhDB.CustomerTimelines.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).OrderByDescending(j => j.custTDID).FirstOrDefault().StartDate).ToString("dd/MM/yyyy"),
            //             CustomerType = p.Customer.CustomerType != null ? p.Customer.CustomerType1.Name : null,
            //             StartTime = p.StartTime,
            //             EndTime = p.EndTime,
            //             TeamName = p.CustomerOfficalDetail.teamID != null ? p.CustomerOfficalDetail.Team.Name : null,
            //             staffName = p.CustomerOfficalDetail.stfID != null ? p.CustomerOfficalDetail.Staff.Name : null,
            //             TaskNo = p.TaskNo,
            //             AddedOn = p.CreatedOn,
            //             IsCustomerFeedback = UhDB.CustomerFeedbacks.Where(x => x.cuID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).Count() != 0 ? true : false,
            //             CustomerID = p.Customer.CustomerID,
            //             ServiceStatus = p.CustomerOfficalDetail.ServiceStatus == true ? "Active" : p.CustomerOfficalDetail.ServiceStatus == false ? "InActive" : "Pending",
            //             stfID = p.CustomerOfficalDetail.stfID,
            //             teamID = p.CustomerOfficalDetail.teamID,
            //             propaID = p.CustomerOfficalDetail.propaID,
            //             vID = p.CustomerOfficalDetail.vID,
            //             proprestID = p.CustomerOfficalDetail.proprestID,
            //             propType = p.CustomerOfficalDetail.propType,
            //             custOPID = p.CustomerOfficalDetail.propType == 2 ? (UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().custODID) : null,
            //             cuID = p.custID,
            //             cuODID = p.custODID,
            //             catID = p.CustomerOfficalDetail.catID,
            //             catsubID = p.CustomerOfficalDetail.catsubID,
            //             NoOfMonths = p.CustomerOfficalDetail.NoOfMonths != null ? p.CustomerOfficalDetail.CustomerRenewalMonth.Name : "N/A",
            //         }).ToList();
            var objTeams = UhDB.StaffTeams.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objTeam in objTeams)
            {
                //int? teamID = objTeam.teamID;
                //var objCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.Customer.uID == uID && x.IsActive == true && x.IsDelete == false
                //                           && x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID && x.CustomerOfficalDetail.teamID == teamID
                //                           && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).AsEnumerable()
                //                          .Select(p => new GetCustomerStaffModel
                //                          {
                //                              Name = p.Customer.Name,
                //                              Email = p.Customer.Email,
                //                              Mobile = p.Customer.Mobile,
                //                              WhatsAppNo = p.Customer.WhatsAppNo,
                //                              AlternativeNo = p.Customer.AlternativeNo,
                //                              Saluation = p.Customer.Salutaion,
                //                              WorkStatus = p.StatusOfWork,
                //                              Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                //                                      UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                //                                      .Select(s => new GetFileDetails
                //                                      {
                //                                          Name = s.Filename,
                //                                          ContentType = s.FileContentType,
                //                                          Size = s.FileSize,
                //                                          Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

                //                                      }).ToList() : null,
                //                              MainCategory = p.CustomerOfficalDetail.MainCategory.Name,
                //                              SubCategory = p.CustomerOfficalDetail.SubCategory.Name,
                //                              Area = p.CustomerOfficalDetail.PropertyArea.Name,
                //                              PropertyName = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name :
                //                                             UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().TowerName,
                //                              PropertyResidencyType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : null,
                //                              Remarks = p.CustomerOfficalDetail.Remarks,
                //                              ApartmentName = p.CustomerOfficalDetail.AppartmentNumber,
                //                              OtherLocation = p.CustomerOfficalDetail.propType == 2 ? UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                //                                              .Select(u => new GetOtherLocationModel
                //                                              {
                //                                                  TowerName = u.TowerName,
                //                                                  BuildingName = u.BuildingName,
                //                                                  StreetNumber = u.StreetNumber,
                //                                                  ZoneNumber = u.ZoneNumber,
                //                                                  Loacation = u.Loacation,
                //                                                  LocationLink = u.LocationLink
                //                                              }).FirstOrDefault() : null,
                //                              GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                //                                                        .Select(r => new GetCustomerAvailabilityModel
                //                                                        {
                //                                                            custSCID = r.custSCID,
                //                                                            Availability = r.Availability,
                //                                                            KeyCollection = r.KeyCollection,
                //                                                            AccessProperty = r.AccessProperty,
                //                                                            ReceptionDate = r.ReceptionDate
                //                                                        }).FirstOrDefault(),
                //                              GetServices = UhDB.CustomerSpecializedCleanings.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable().
                //                                            Select(t => new GetServiceSubCategoryModel
                //                                            {
                //                                                custSCID = t.custSCID,
                //                                                servcatID = t.servcatID,
                //                                                servsubcatID = t.servsubcatID,
                //                                                ServiceCategoryName = t.ServiceCategory.Name,
                //                                                ServiceSubCategoryName = t.ServiceSubCategory.Name,
                //                                                Quantity = t.Quantity
                //                                            }).ToList(),
                //                              PackageName = p.packID != null ? p.Package.Name : null,
                //                              Price = p.packID != null ? p.Pricing.Price.ToString() : null,
                //                              Duration = p.packID != null ? p.Pricing.Duration : null,
                //                              Measurement = p.packID != null ? p.Pricing.TimeMeasurement : null,
                //                              packID = p.packID,
                //                              parkID = p.parkID,
                //                              Date = Convert.ToDateTime(p.StartDate).ToString("MM/dd/yyyy"),
                //                              WeeklyCounts = p.packID != null ? p.Package.RecursiveTime.ToString() : null,
                //                              ServiceDays = p.CustomerOfficalDetail.BuldleDays,
                //                              EndDate = Convert.ToDateTime(UhDB.CustomerTimelines.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).OrderByDescending(j => j.custTDID).FirstOrDefault().StartDate).ToString("dd/MM/yyyy"),
                //                              CustomerType = p.Customer.CustomerType != null ? p.Customer.CustomerType1.Name : null,
                //                              StartTime = p.StartTime,
                //                              EndTime = p.EndTime,
                //                              TeamName = p.CustomerOfficalDetail.teamID != null ? p.CustomerOfficalDetail.Team.Name : null,
                //                              staffName = p.CustomerOfficalDetail.stfID != null ? p.CustomerOfficalDetail.Staff.Name : null,
                //                              TaskNo = p.TaskNo,
                //                              NoOfMonths = p.CustomerOfficalDetail.NoOfMonths != null ? p.CustomerOfficalDetail.CustomerRenewalMonth.Name : "N/A",
                //                              AddedOn = p.CreatedOn,
                //                              IsCustomerFeedback = UhDB.CustomerFeedbacks.Where(x => x.cuID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).Count() != 0 ? true : false,
                //                              CustomerID = p.Customer.CustomerID,
                //                              ServiceStatus = p.CustomerOfficalDetail.ServiceStatus == true ? "Active" : p.CustomerOfficalDetail.ServiceStatus == false ? "InActive" : "Pending",
                //                              stfID = p.CustomerOfficalDetail.stfID,
                //                              teamID = p.CustomerOfficalDetail.teamID,
                //                              propaID = p.CustomerOfficalDetail.propaID,
                //                              vID = p.CustomerOfficalDetail.vID,
                //                              proprestID = p.CustomerOfficalDetail.proprestID,
                //                              propType = p.CustomerOfficalDetail.propType,
                //                              custOPID = p.CustomerOfficalDetail.propType == 2 ? (UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().custODID) : null,
                //                              cuID = p.custID,
                //                              cuODID = p.custODID,
                //                              catID = p.CustomerOfficalDetail.catID,
                //                              catsubID = p.CustomerOfficalDetail.catsubID
                //                          }).ToList();
                //if (objCustomerTimeLines != null)
                //{
                //    result.AddRange(objCustomerTimeLines);
                //}
            }

            return result;
        }

        public IEnumerable<GetCustomerStaffModel> GetSpecialServiceTodayCustomersForDashboard(int? uID, int? catID, int? catsubID, int? servcatID, int? stfID)
        {

            List<GetCustomerStaffModel> result = new List<GetCustomerStaffModel>();
            //var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.CustomerOfficalDetail.catID == catID
            //                                      && x.CustomerOfficalDetail.catsubID == catsubID && x.servcatID == servcatID && x.CustomerOfficalDetail.stfID == stfID
            //                                      && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID).ToList();
            //foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
            //{
            //    int? cuID = objCustomerSpecializedCleaning.custID;
            //    int? cuODID = objCustomerSpecializedCleaning.custODID;
            //    var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true && x.CustomerOfficalDetail.stfID == stfID
            //                               && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now))
            //                               .Select(p => new GetCustomerStaffModel
            //                               {
            //                                   Name = p.Customer.Name,
            //                                   Email = p.Customer.Email,
            //                                   Mobile = p.Customer.Mobile,
            //                                   WhatsAppNo = p.Customer.WhatsAppNo,
            //                                   AlternativeNo = p.Customer.AlternativeNo,
            //                                   Saluation = p.Customer.Salutaion,
            //                                   WorkStatus = p.StatusOfWork,
            //                                   Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
            //                                             UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
            //                                             .Select(s => new GetFileDetails
            //                                             {
            //                                                 Name = s.Filename,
            //                                                 ContentType = s.FileContentType,
            //                                                 Size = s.FileSize,
            //                                                 Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

            //                                             }).ToList() : null,
            //                                   MainCategory = p.CustomerOfficalDetail.MainCategory.Name,
            //                                   SubCategory = p.CustomerOfficalDetail.catsubID != null ? p.CustomerOfficalDetail.SubCategory.Name : "N/A",
            //                                   Area = p.CustomerOfficalDetail.PropertyArea.Name,
            //                                   PropertyName = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name :
            //                                                    UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().TowerName,
            //                                   PropertyResidencyType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : null,
            //                                   Remarks = p.CustomerOfficalDetail.Remarks,
            //                                   ApartmentName = p.CustomerOfficalDetail.AppartmentNumber,
            //                                   OtherLocation = p.CustomerOfficalDetail.propType == 2 ? UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
            //                                                     .Select(u => new GetOtherLocationModel
            //                                                     {
            //                                                         TowerName = u.TowerName,
            //                                                         BuildingName = u.BuildingName,
            //                                                         StreetNumber = u.StreetNumber,
            //                                                         ZoneNumber = u.ZoneNumber,
            //                                                         Loacation = u.Loacation,
            //                                                         LocationLink = u.LocationLink
            //                                                     }).FirstOrDefault() : null,
            //                                   GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
            //                                                               .Select(r => new GetCustomerAvailabilityModel
            //                                                               {
            //                                                                   custSCID = r.custSCID,
            //                                                                   Availability = r.Availability,
            //                                                                   KeyCollection = r.KeyCollection,
            //                                                                   AccessProperty = r.AccessProperty,
            //                                                                   ReceptionDate = r.ReceptionDate
            //                                                               }).FirstOrDefault(),
            //                                   GetServices = UhDB.CustomerSpecializedCleanings.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable().
            //                                                   Select(t => new GetServiceSubCategoryModel
            //                                                   {
            //                                                       custSCID = t.custSCID,
            //                                                       servcatID = t.servcatID,
            //                                                       servsubcatID = t.servsubcatID,
            //                                                       ServiceCategoryName = t.ServiceCategory.Name,
            //                                                       ServiceSubCategoryName = t.ServiceSubCategory.Name,
            //                                                       Quantity = t.Quantity
            //                                                   }).ToList(),
            //                                   carstID = p.CustomerOfficalDetail.carstID,
            //                                   cartID = p.CustomerOfficalDetail.cartID,
            //                                   IsCarWash = p.CustomerOfficalDetail.IsCarWash,
            //                                   CarType = p.CustomerOfficalDetail.CarType != null ? p.CustomerOfficalDetail.CarType.Name : "N/A",
            //                                   CarServiceType = p.CustomerOfficalDetail.CarServiceType != null ? p.CustomerOfficalDetail.CarServiceType.Name : "N/A",
            //                                   CustomCarDetails = p.CustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
            //                                                      .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,

            //                                   PackageName = p.packID != null ? p.Package.Name : null,
            //                                   Price = p.packID != null ? p.Pricing.Price.ToString() : null,
            //                                   Duration = p.packID != null ? p.Pricing.Duration : null,
            //                                   Measurement = p.packID != null ? p.Pricing.TimeMeasurement : null,
            //                                   packID = p.packID,
            //                                   parkID = p.parkID,
            //                                   Date = Convert.ToDateTime(p.StartDate).ToString("MM/dd/yyyy"),
            //                                   WeeklyCounts = p.packID != null ? p.Package.RecursiveTime.ToString() : null,
            //                                   ServiceDays = p.CustomerOfficalDetail.BuldleDays,
            //                                   EndDate = Convert.ToDateTime(UhDB.CustomerTimelines.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).OrderByDescending(j => j.custTDID).FirstOrDefault().StartDate).ToString("dd/MM/yyyy"),
            //                                   CustomerType = p.Customer.CustomerType != null ? p.Customer.CustomerType1.Name : null,
            //                                   StartTime = p.StartTime,
            //                                   EndTime = p.EndTime,
            //                                   TeamName = p.CustomerOfficalDetail.teamID != null ? p.CustomerOfficalDetail.Team.Name : null,
            //                                   staffName = p.CustomerOfficalDetail.stfID != null ? p.CustomerOfficalDetail.Staff.Name : null,
            //                                   TaskNo = p.TaskNo,
            //                                   NoOfMonths = p.CustomerOfficalDetail.NoOfMonths != null ? p.CustomerOfficalDetail.CustomerRenewalMonth.Name : "N/A",
            //                                   AddedOn = p.CreatedOn,
            //                                   CustomerID = p.Customer.CustomerID,
            //                                   ServiceStatus = p.CustomerOfficalDetail.ServiceStatus == true ? "Active" : p.CustomerOfficalDetail.ServiceStatus == false ? "InActive" : "Pending",
            //                                   stfID = p.CustomerOfficalDetail.stfID,
            //                                   teamID = p.CustomerOfficalDetail.teamID,
            //                                   propaID = p.CustomerOfficalDetail.propaID,
            //                                   vID = p.CustomerOfficalDetail.vID,
            //                                   proprestID = p.CustomerOfficalDetail.proprestID,
            //                                   propType = p.CustomerOfficalDetail.propType,
            //                                   custOPID = p.CustomerOfficalDetail.propType == 2 ? (UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().custODID) : null,
            //                                   cuID = p.custID,
            //                                   cuODID = p.custODID,
            //                                   catID = p.CustomerOfficalDetail.catID,
            //                                   catsubID = p.CustomerOfficalDetail.catsubID
            //                               }).ToList();
            //    if (objCustomerTimelines != null)
            //    {
            //        result.AddRange(objCustomerTimelines);
            //    }
            //}

            //var objTeams = UhDB.StaffTeams.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false).ToList();
            //foreach (var objTeam in objTeams)
            //{
            //    int? teamID = objTeam.teamID;
            //    var objCustomerSpecializedCleanings1 = UhDB.CustomerSpecializedCleanings.Where(x => x.CustomerOfficalDetail.catID == catID
            //                                          && x.CustomerOfficalDetail.catsubID == catsubID && x.servcatID == servcatID && x.CustomerOfficalDetail.teamID == teamID
            //                                          && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID).ToList();
            //    foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings1)
            //    {
            //        int? cuID = objCustomerSpecializedCleaning.custID;
            //        int? cuODID = objCustomerSpecializedCleaning.custODID;
            //        var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true && x.CustomerOfficalDetail.stfID == stfID
            //                                   && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now))
            //                                   .Select(p => new GetCustomerStaffModel
            //                                   {
            //                                       Name = p.Customer.Name,
            //                                       Email = p.Customer.Email,
            //                                       Mobile = p.Customer.Mobile,
            //                                       WhatsAppNo = p.Customer.WhatsAppNo,
            //                                       AlternativeNo = p.Customer.AlternativeNo,
            //                                       Saluation = p.Customer.Salutaion,
            //                                       WorkStatus = p.StatusOfWork,
            //                                       Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
            //                                                 UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
            //                                                 .Select(s => new GetFileDetails
            //                                                 {
            //                                                     Name = s.Filename,
            //                                                     ContentType = s.FileContentType,
            //                                                     Size = s.FileSize,
            //                                                     Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

            //                                                 }).ToList() : null,
            //                                       MainCategory = p.CustomerOfficalDetail.MainCategory.Name,
            //                                       SubCategory = p.CustomerOfficalDetail.catsubID != null ? p.CustomerOfficalDetail.SubCategory.Name : "N/A",
            //                                       Area = p.CustomerOfficalDetail.PropertyArea.Name,
            //                                       PropertyName = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name :
            //                                                        UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().TowerName,
            //                                       PropertyResidencyType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : null,
            //                                       Remarks = p.CustomerOfficalDetail.Remarks,
            //                                       ApartmentName = p.CustomerOfficalDetail.AppartmentNumber,
            //                                       OtherLocation = p.CustomerOfficalDetail.propType == 2 ? UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
            //                                                         .Select(u => new GetOtherLocationModel
            //                                                         {
            //                                                             TowerName = u.TowerName,
            //                                                             BuildingName = u.BuildingName,
            //                                                             StreetNumber = u.StreetNumber,
            //                                                             ZoneNumber = u.ZoneNumber,
            //                                                             Loacation = u.Loacation,
            //                                                             LocationLink = u.LocationLink
            //                                                         }).FirstOrDefault() : null,
            //                                       GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
            //                                                                   .Select(r => new GetCustomerAvailabilityModel
            //                                                                   {
            //                                                                       custSCID = r.custSCID,
            //                                                                       Availability = r.Availability,
            //                                                                       KeyCollection = r.KeyCollection,
            //                                                                       AccessProperty = r.AccessProperty,
            //                                                                       ReceptionDate = r.ReceptionDate
            //                                                                   }).FirstOrDefault(),
            //                                       GetServices = UhDB.CustomerSpecializedCleanings.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable().
            //                                                       Select(t => new GetServiceSubCategoryModel
            //                                                       {
            //                                                           custSCID = t.custSCID,
            //                                                           servcatID = t.servcatID,
            //                                                           servsubcatID = t.servsubcatID,
            //                                                           ServiceCategoryName = t.ServiceCategory.Name,
            //                                                           ServiceSubCategoryName = t.ServiceSubCategory.Name,
            //                                                           Quantity = t.Quantity
            //                                                       }).ToList(),
            //                                       carstID = p.CustomerOfficalDetail.carstID,
            //                                       cartID = p.CustomerOfficalDetail.cartID,
            //                                       IsCarWash = p.CustomerOfficalDetail.IsCarWash,
            //                                       CarType = p.CustomerOfficalDetail.CarType != null ? p.CustomerOfficalDetail.CarType.Name : "N/A",
            //                                       CarServiceType = p.CustomerOfficalDetail.CarServiceType != null ? p.CustomerOfficalDetail.CarServiceType.Name : "N/A",
            //                                       CustomCarDetails = p.CustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
            //                                                      .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,

            //                                       PackageName = p.packID != null ? p.Package.Name : null,
            //                                       Price = p.packID != null ? p.Pricing.Price.ToString() : null,
            //                                       Duration = p.packID != null ? p.Pricing.Duration : null,
            //                                       Measurement = p.packID != null ? p.Pricing.TimeMeasurement : null,
            //                                       packID = p.packID,
            //                                       parkID = p.parkID,
            //                                       Date = Convert.ToDateTime(p.StartDate).ToString("MM/dd/yyyy"),
            //                                       WeeklyCounts = p.packID != null ? p.Package.RecursiveTime.ToString() : null,
            //                                       ServiceDays = p.CustomerOfficalDetail.BuldleDays,
            //                                       EndDate = Convert.ToDateTime(UhDB.CustomerTimelines.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).OrderByDescending(j => j.custTDID).FirstOrDefault().StartDate).ToString("dd/MM/yyyy"),
            //                                       CustomerType = p.Customer.CustomerType != null ? p.Customer.CustomerType1.Name : null,
            //                                       StartTime = p.StartTime,
            //                                       EndTime = p.EndTime,
            //                                       TeamName = p.CustomerOfficalDetail.teamID != null ? p.CustomerOfficalDetail.Team.Name : null,
            //                                       staffName = p.CustomerOfficalDetail.stfID != null ? p.CustomerOfficalDetail.Staff.Name : null,
            //                                       TaskNo = p.TaskNo,
            //                                       NoOfMonths = p.CustomerOfficalDetail.NoOfMonths != null ? p.CustomerOfficalDetail.CustomerRenewalMonth.Name : "N/A",
            //                                       AddedOn = p.CreatedOn,
            //                                       CustomerID = p.Customer.CustomerID,
            //                                       ServiceStatus = p.CustomerOfficalDetail.ServiceStatus == true ? "Active" : p.CustomerOfficalDetail.ServiceStatus == false ? "InActive" : "Pending",
            //                                       stfID = p.CustomerOfficalDetail.stfID,
            //                                       teamID = p.CustomerOfficalDetail.teamID,
            //                                       propaID = p.CustomerOfficalDetail.propaID,
            //                                       vID = p.CustomerOfficalDetail.vID,
            //                                       proprestID = p.CustomerOfficalDetail.proprestID,
            //                                       propType = p.CustomerOfficalDetail.propType,
            //                                       custOPID = p.CustomerOfficalDetail.propType == 2 ? (UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().custODID) : null,
            //                                       cuID = p.custID,
            //                                       cuODID = p.custODID,
            //                                       catID = p.CustomerOfficalDetail.catID,
            //                                       catsubID = p.CustomerOfficalDetail.catsubID
            //                                   }).ToList();
            //        if (objCustomerTimelines != null)
            //        {
            //            result.AddRange(objCustomerTimelines);
            //        }
            //    }
            //}

            return result;
        }

        public IEnumerable<GetCustomerStaffModel> GetMonthlyCustomersForDashboard(int? uID, int? catID, int? catsubID, int? stfID)
        {

            DateTime TodaysDate = DateTime.Now;
            int Year = TodaysDate.Year;
            int Month = TodaysDate.Month;
            int AddDates = DateTime.DaysInMonth(Year, Month);
            DateTime StartDate = new DateTime(Year, Month, 1);
            DateTime EndDate = StartDate.AddDays(AddDates);

            List<GetCustomerStaffModel> result = new List<GetCustomerStaffModel>();
            //result = UhDB.CustomerTimelines.Where(x => x.Customer.uID == uID && x.IsActive == true && x.IsDelete == false
            //         && x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID && x.CustomerOfficalDetail.stfID == stfID
            //         && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(EndDate)).AsEnumerable()
            //         .Select(p => new GetCustomerStaffModel
            //         {
            //             Name = p.Customer.Name,
            //             Email = p.Customer.Email,
            //             Mobile = p.Customer.Mobile,
            //             WhatsAppNo = p.Customer.WhatsAppNo,
            //             AlternativeNo = p.Customer.AlternativeNo,
            //             Saluation = p.Customer.Salutaion,
            //             WorkStatus = p.StatusOfWork,
            //             Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
            //                   UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
            //                   .Select(s => new GetFileDetails
            //                   {
            //                       Name = s.Filename,
            //                       ContentType = s.FileContentType,
            //                       Size = s.FileSize,
            //                       Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

            //                   }).ToList() : null,
            //             MainCategory = p.CustomerOfficalDetail.MainCategory.Name,
            //             SubCategory = p.CustomerOfficalDetail.catsubID != null ? p.CustomerOfficalDetail.SubCategory.Name : "N/A",
            //             Area = p.CustomerOfficalDetail.PropertyArea.Name,
            //             PropertyName = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name :
            //                            UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().TowerName,
            //             PropertyResidencyType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : null,
            //             Remarks = p.CustomerOfficalDetail.Remarks,
            //             ApartmentName = p.CustomerOfficalDetail.AppartmentNumber,
            //             OtherLocation = p.CustomerOfficalDetail.propType == 2 ? UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
            //                           .Select(u => new GetOtherLocationModel
            //                           {
            //                               TowerName = u.TowerName,
            //                               BuildingName = u.BuildingName,
            //                               StreetNumber = u.StreetNumber,
            //                               ZoneNumber = u.ZoneNumber,
            //                               Loacation = u.Loacation,
            //                               LocationLink = u.LocationLink
            //                           }).FirstOrDefault() : null,
            //             GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
            //                                        .Select(r => new GetCustomerAvailabilityModel
            //                                        {
            //                                            custSCID = r.custSCID,
            //                                            Availability = r.Availability,
            //                                            KeyCollection = r.KeyCollection,
            //                                            AccessProperty = r.AccessProperty,
            //                                            ReceptionDate = r.ReceptionDate
            //                                        }).FirstOrDefault(),
            //             GetServices = UhDB.CustomerSpecializedCleanings.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable().
            //                           Select(t => new GetServiceSubCategoryModel
            //                           {
            //                               custSCID = t.custSCID,
            //                               servcatID = t.servcatID,
            //                               servsubcatID = t.servsubcatID,
            //                               ServiceCategoryName = t.ServiceCategory.Name,
            //                               ServiceSubCategoryName = t.ServiceSubCategory.Name,
            //                               Quantity = t.Quantity
            //                           }).ToList(),
            //             carstID = p.CustomerOfficalDetail.carstID,
            //             cartID = p.CustomerOfficalDetail.cartID,
            //             IsCarWash = p.CustomerOfficalDetail.IsCarWash,
            //             CarType = p.CustomerOfficalDetail.CarType != null ? p.CustomerOfficalDetail.CarType.Name : "N/A",
            //             CarServiceType = p.CustomerOfficalDetail.CarServiceType != null ? p.CustomerOfficalDetail.CarServiceType.Name : "N/A",
            //             CustomCarDetails = p.CustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
            //                                                      .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,

            //             PackageName = p.packID != null ? p.Package.Name : null,
            //             Price = p.packID != null ? p.Pricing.Price.ToString() : null,
            //             Duration = p.packID != null ? p.Pricing.Duration : null,
            //             Measurement = p.packID != null ? p.Pricing.TimeMeasurement : null,
            //             packID = p.packID,
            //             parkID = p.parkID,
            //             Date = Convert.ToDateTime(p.StartDate).ToString("MM/dd/yyyy"),
            //             WeeklyCounts = p.packID != null ? p.Package.RecursiveTime.ToString() : null,
            //             ServiceDays = p.CustomerOfficalDetail.BuldleDays,
            //             EndDate = Convert.ToDateTime(UhDB.CustomerTimelines.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).OrderByDescending(j => j.custTDID).FirstOrDefault().StartDate).ToString("dd/MM/yyyy"),
            //             CustomerType = p.Customer.CustomerType != null ? p.Customer.CustomerType1.Name : null,
            //             StartTime = p.StartTime,
            //             EndTime = p.EndTime,
            //             TeamName = p.CustomerOfficalDetail.teamID != null ? p.CustomerOfficalDetail.Team.Name : null,
            //             staffName = p.CustomerOfficalDetail.stfID != null ? p.CustomerOfficalDetail.Staff.Name : null,
            //             TaskNo = p.TaskNo,
            //             NoOfMonths = p.CustomerOfficalDetail.NoOfMonths != null ? p.CustomerOfficalDetail.CustomerRenewalMonth.Name : "N/A",
            //             AddedOn = p.CreatedOn,
            //             IsCustomerFeedback = UhDB.CustomerFeedbacks.Where(x => x.cuID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).Count() != 0 ? true : false,
            //             CustomerID = p.Customer.CustomerID,
            //             ServiceStatus = p.CustomerOfficalDetail.ServiceStatus == true ? "Active" : p.CustomerOfficalDetail.ServiceStatus == false ? "InActive" : "Pending",
            //             stfID = p.CustomerOfficalDetail.stfID,
            //             teamID = p.CustomerOfficalDetail.teamID,
            //             propaID = p.CustomerOfficalDetail.propaID,
            //             vID = p.CustomerOfficalDetail.vID,
            //             proprestID = p.CustomerOfficalDetail.proprestID,
            //             propType = p.CustomerOfficalDetail.propType,
            //             custOPID = p.CustomerOfficalDetail.propType == 2 ? (UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().custODID) : null,
            //             cuID = p.custID,
            //             cuODID = p.custODID,
            //             catID = p.CustomerOfficalDetail.catID,
            //             catsubID = p.CustomerOfficalDetail.catsubID
            //         }).ToList();
            var objTeams = UhDB.StaffTeams.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objTeam in objTeams)
            {
                //int? teamID = objTeam.teamID;
                //var objCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.Customer.uID == uID && x.IsActive == true && x.IsDelete == false
                //                           && x.CustomerOfficalDetail.catID == catID && x.CustomerOfficalDetail.catsubID == catsubID && x.CustomerOfficalDetail.teamID == teamID
                //                           && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(EndDate)).AsEnumerable()
                //                          .Select(p => new GetCustomerStaffModel
                //                          {
                //                              Name = p.Customer.Name,
                //                              Email = p.Customer.Email,
                //                              Mobile = p.Customer.Mobile,
                //                              WhatsAppNo = p.Customer.WhatsAppNo,
                //                              AlternativeNo = p.Customer.AlternativeNo,
                //                              Saluation = p.Customer.Salutaion,
                //                              WorkStatus = p.StatusOfWork,
                //                              Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                //                                      UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                //                                      .Select(s => new GetFileDetails
                //                                      {
                //                                          Name = s.Filename,
                //                                          ContentType = s.FileContentType,
                //                                          Size = s.FileSize,
                //                                          Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

                //                                      }).ToList() : null,
                //                              MainCategory = p.CustomerOfficalDetail.MainCategory.Name,
                //                              SubCategory = p.CustomerOfficalDetail.catsubID != null ? p.CustomerOfficalDetail.SubCategory.Name : "N/A",
                //                              Area = p.CustomerOfficalDetail.PropertyArea.Name,
                //                              PropertyName = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name :
                //                                             UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().TowerName,
                //                              PropertyResidencyType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : null,
                //                              Remarks = p.CustomerOfficalDetail.Remarks,
                //                              ApartmentName = p.CustomerOfficalDetail.AppartmentNumber,
                //                              OtherLocation = p.CustomerOfficalDetail.propType == 2 ? UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                //                                              .Select(u => new GetOtherLocationModel
                //                                              {
                //                                                  TowerName = u.TowerName,
                //                                                  BuildingName = u.BuildingName,
                //                                                  StreetNumber = u.StreetNumber,
                //                                                  ZoneNumber = u.ZoneNumber,
                //                                                  Loacation = u.Loacation,
                //                                                  LocationLink = u.LocationLink
                //                                              }).FirstOrDefault() : null,
                //                              GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                //                                                        .Select(r => new GetCustomerAvailabilityModel
                //                                                        {
                //                                                            custSCID = r.custSCID,
                //                                                            Availability = r.Availability,
                //                                                            KeyCollection = r.KeyCollection,
                //                                                            AccessProperty = r.AccessProperty,
                //                                                            ReceptionDate = r.ReceptionDate
                //                                                        }).FirstOrDefault(),
                //                              GetServices = UhDB.CustomerSpecializedCleanings.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable().
                //                                            Select(t => new GetServiceSubCategoryModel
                //                                            {
                //                                                custSCID = t.custSCID,
                //                                                servcatID = t.servcatID,
                //                                                servsubcatID = t.servsubcatID,
                //                                                ServiceCategoryName = t.ServiceCategory.Name,
                //                                                ServiceSubCategoryName = t.ServiceSubCategory.Name,
                //                                                Quantity = t.Quantity
                //                                            }).ToList(),
                //                              carstID = p.CustomerOfficalDetail.carstID,
                //                              cartID = p.CustomerOfficalDetail.cartID,
                //                              IsCarWash = p.CustomerOfficalDetail.IsCarWash,
                //                              CarType = p.CustomerOfficalDetail.CarType != null ? p.CustomerOfficalDetail.CarType.Name : "N/A",
                //                              CarServiceType = p.CustomerOfficalDetail.CarServiceType != null ? p.CustomerOfficalDetail.CarServiceType.Name : "N/A",
                //                              CustomCarDetails = p.CustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                //                                                  .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,

                //                              PackageName = p.packID != null ? p.Package.Name : null,
                //                              Price = p.packID != null ? p.Pricing.Price.ToString() : null,
                //                              Duration = p.packID != null ? p.Pricing.Duration : null,
                //                              Measurement = p.packID != null ? p.Pricing.TimeMeasurement : null,
                //                              packID = p.packID,
                //                              parkID = p.parkID,
                //                              Date = Convert.ToDateTime(p.StartDate).ToString("MM/dd/yyyy"),
                //                              WeeklyCounts = p.packID != null ? p.Package.RecursiveTime.ToString() : null,
                //                              ServiceDays = p.CustomerOfficalDetail.BuldleDays,
                //                              EndDate = Convert.ToDateTime(UhDB.CustomerTimelines.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).OrderByDescending(j => j.custTDID).FirstOrDefault().StartDate).ToString("dd/MM/yyyy"),
                //                              CustomerType = p.Customer.CustomerType != null ? p.Customer.CustomerType1.Name : null,
                //                              StartTime = p.StartTime,
                //                              EndTime = p.EndTime,
                //                              TeamName = p.CustomerOfficalDetail.teamID != null ? p.CustomerOfficalDetail.Team.Name : null,
                //                              staffName = p.CustomerOfficalDetail.stfID != null ? p.CustomerOfficalDetail.Staff.Name : null,
                //                              TaskNo = p.TaskNo,
                //                              NoOfMonths = p.CustomerOfficalDetail.NoOfMonths != null ? p.CustomerOfficalDetail.CustomerRenewalMonth.Name : "N/A",
                //                              AddedOn = p.CreatedOn,
                //                              IsCustomerFeedback = UhDB.CustomerFeedbacks.Where(x => x.cuID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).Count() != 0 ? true : false,
                //                              CustomerID = p.Customer.CustomerID,
                //                              ServiceStatus = p.CustomerOfficalDetail.ServiceStatus == true ? "Active" : p.CustomerOfficalDetail.ServiceStatus == false ? "InActive" : "Pending",
                //                              stfID = p.CustomerOfficalDetail.stfID,
                //                              teamID = p.CustomerOfficalDetail.teamID,
                //                              propaID = p.CustomerOfficalDetail.propaID,
                //                              vID = p.CustomerOfficalDetail.vID,
                //                              proprestID = p.CustomerOfficalDetail.proprestID,
                //                              propType = p.CustomerOfficalDetail.propType,
                //                              custOPID = p.CustomerOfficalDetail.propType == 2 ? (UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().custODID) : null,
                //                              cuID = p.custID,
                //                              cuODID = p.custODID,
                //                              catID = p.CustomerOfficalDetail.catID,
                //                              catsubID = p.CustomerOfficalDetail.catsubID
                //                          }).ToList();
                //if (objCustomerTimeLines != null)
                //{
                //    result.AddRange(objCustomerTimeLines);
                //}
            }

            return result;
        }

        public IEnumerable<GetCustomerStaffModel> GetSpecialServiceMonthlyCustomersForDashboard(int? uID, int? catID, int? catsubID, int? servcatID, int? stfID)
        {

            DateTime TodaysDate = DateTime.Now;
            int Year = TodaysDate.Year;
            int Month = TodaysDate.Month;
            int AddDates = DateTime.DaysInMonth(Year, Month);
            DateTime StartDate = new DateTime(Year, Month, 1);
            DateTime EndDate = StartDate.AddDays(AddDates);

            List<GetCustomerStaffModel> result = new List<GetCustomerStaffModel>();
            var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.CustomerOfficalDetail.catID == catID
                                                  && x.CustomerOfficalDetail.catsubID == catsubID && x.servcatID == servcatID && x.CustomerOfficalDetail.stfID == stfID
                                                  && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID).ToList();
            foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
            {
                //int? cuID = objCustomerSpecializedCleaning.custID;
                //int? cuODID = objCustomerSpecializedCleaning.custODID;
                //var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true && x.CustomerOfficalDetail.stfID == stfID
                //                           && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(EndDate))
                //                           .Select(p => new GetCustomerStaffModel
                //                           {
                //                               Name = p.Customer.Name,
                //                               Email = p.Customer.Email,
                //                               Mobile = p.Customer.Mobile,
                //                               WhatsAppNo = p.Customer.WhatsAppNo,
                //                               AlternativeNo = p.Customer.AlternativeNo,
                //                               Saluation = p.Customer.Salutaion,
                //                               WorkStatus = p.StatusOfWork,
                //                               Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                //                                         UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                //                                         .Select(s => new GetFileDetails
                //                                         {
                //                                             Name = s.Filename,
                //                                             ContentType = s.FileContentType,
                //                                             Size = s.FileSize,
                //                                             Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

                //                                         }).ToList() : null,
                //                               MainCategory = p.CustomerOfficalDetail.MainCategory.Name,
                //                               SubCategory = p.CustomerOfficalDetail.catsubID != null ? p.CustomerOfficalDetail.SubCategory.Name : "N/A",
                //                               Area = p.CustomerOfficalDetail.PropertyArea.Name,
                //                               PropertyName = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name :
                //                                                UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().TowerName,
                //                               PropertyResidencyType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : null,
                //                               Remarks = p.CustomerOfficalDetail.Remarks,
                //                               ApartmentName = p.CustomerOfficalDetail.AppartmentNumber,
                //                               OtherLocation = p.CustomerOfficalDetail.propType == 2 ? UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                //                                                 .Select(u => new GetOtherLocationModel
                //                                                 {
                //                                                     TowerName = u.TowerName,
                //                                                     BuildingName = u.BuildingName,
                //                                                     StreetNumber = u.StreetNumber,
                //                                                     ZoneNumber = u.ZoneNumber,
                //                                                     Loacation = u.Loacation,
                //                                                     LocationLink = u.LocationLink
                //                                                 }).FirstOrDefault() : null,
                //                               GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                //                                                           .Select(r => new GetCustomerAvailabilityModel
                //                                                           {
                //                                                               custSCID = r.custSCID,
                //                                                               Availability = r.Availability,
                //                                                               KeyCollection = r.KeyCollection,
                //                                                               AccessProperty = r.AccessProperty,
                //                                                               ReceptionDate = r.ReceptionDate
                //                                                           }).FirstOrDefault(),
                //                               GetServices = UhDB.CustomerSpecializedCleanings.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable().
                //                                               Select(t => new GetServiceSubCategoryModel
                //                                               {
                //                                                   custSCID = t.custSCID,
                //                                                   servcatID = t.servcatID,
                //                                                   servsubcatID = t.servsubcatID,
                //                                                   ServiceCategoryName = t.ServiceCategory.Name,
                //                                                   ServiceSubCategoryName = t.ServiceSubCategory.Name,
                //                                                   Quantity = t.Quantity
                //                                               }).ToList(),
                //                               carstID = p.CustomerOfficalDetail.carstID,
                //                               cartID = p.CustomerOfficalDetail.cartID,
                //                               IsCarWash = p.CustomerOfficalDetail.IsCarWash,
                //                               CarType = p.CustomerOfficalDetail.CarType != null ? p.CustomerOfficalDetail.CarType.Name : "N/A",
                //                               CarServiceType = p.CustomerOfficalDetail.CarServiceType != null ? p.CustomerOfficalDetail.CarServiceType.Name : "N/A",
                //                               CustomCarDetails = p.CustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                //                                                  .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,

                //                               PackageName = p.packID != null ? p.Package.Name : null,
                //                               Price = p.packID != null ? p.Pricing.Price.ToString() : null,
                //                               Duration = p.packID != null ? p.Pricing.Duration : null,
                //                               Measurement = p.packID != null ? p.Pricing.TimeMeasurement : null,
                //                               packID = p.packID,
                //                               parkID = p.parkID,
                //                               Date = Convert.ToDateTime(p.StartDate).ToString("MM/dd/yyyy"),
                //                               WeeklyCounts = p.packID != null ? p.Package.RecursiveTime.ToString() : null,
                //                               ServiceDays = p.CustomerOfficalDetail.BuldleDays,
                //                               EndDate = Convert.ToDateTime(UhDB.CustomerTimelines.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).OrderByDescending(j => j.custTDID).FirstOrDefault().StartDate).ToString("dd/MM/yyyy"),
                //                               CustomerType = p.Customer.CustomerType != null ? p.Customer.CustomerType1.Name : null,
                //                               StartTime = p.StartTime,
                //                               EndTime = p.EndTime,
                //                               TeamName = p.CustomerOfficalDetail.teamID != null ? p.CustomerOfficalDetail.Team.Name : null,
                //                               staffName = p.CustomerOfficalDetail.stfID != null ? p.CustomerOfficalDetail.Staff.Name : null,
                //                               TaskNo = p.TaskNo,
                //                               NoOfMonths = p.CustomerOfficalDetail.NoOfMonths != null ? p.CustomerOfficalDetail.CustomerRenewalMonth.Name : "N/A",
                //                               AddedOn = p.CreatedOn,
                //                               CustomerID = p.Customer.CustomerID,
                //                               ServiceStatus = p.CustomerOfficalDetail.ServiceStatus == true ? "Active" : p.CustomerOfficalDetail.ServiceStatus == false ? "InActive" : "Pending",
                //                               stfID = p.CustomerOfficalDetail.stfID,
                //                               teamID = p.CustomerOfficalDetail.teamID,
                //                               propaID = p.CustomerOfficalDetail.propaID,
                //                               vID = p.CustomerOfficalDetail.vID,
                //                               proprestID = p.CustomerOfficalDetail.proprestID,
                //                               propType = p.CustomerOfficalDetail.propType,
                //                               custOPID = p.CustomerOfficalDetail.propType == 2 ? (UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().custODID) : null,
                //                               cuID = p.custID,
                //                               cuODID = p.custODID,
                //                               catID = p.CustomerOfficalDetail.catID,
                //                               catsubID = p.CustomerOfficalDetail.catsubID
                //                           }).ToList();
                //if (objCustomerTimelines != null)
                //{
                //    result.AddRange(objCustomerTimelines);
                //}
            }

            var objTeams = UhDB.StaffTeams.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objTeam in objTeams)
            {
                int? teamID = objTeam.teamID;
                var objCustomerSpecializedCleanings1 = UhDB.CustomerSpecializedCleanings.Where(x => x.CustomerOfficalDetail.catID == catID
                                                      && x.CustomerOfficalDetail.catsubID == catsubID && x.servcatID == servcatID && x.CustomerOfficalDetail.teamID == teamID
                                                      && x.IsActive == true && x.IsDelete == false && x.Customer.uID == uID).ToList();
                //foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings1)
                //{
                //    int? cuID = objCustomerSpecializedCleaning.custID;
                //    int? cuODID = objCustomerSpecializedCleaning.custODID;
                //    var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true && x.CustomerOfficalDetail.stfID == stfID
                //                               && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate) && EntityFunctions.TruncateTime(x.StartDate) <= EntityFunctions.TruncateTime(EndDate))
                //                               .Select(p => new GetCustomerStaffModel
                //                               {
                //                                   Name = p.Customer.Name,
                //                                   Email = p.Customer.Email,
                //                                   Mobile = p.Customer.Mobile,
                //                                   WhatsAppNo = p.Customer.WhatsAppNo,
                //                                   AlternativeNo = p.Customer.AlternativeNo,
                //                                   Saluation = p.Customer.Salutaion,
                //                                   WorkStatus = p.StatusOfWork,
                //                                   Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                //                                             UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                //                                             .Select(s => new GetFileDetails
                //                                             {
                //                                                 Name = s.Filename,
                //                                                 ContentType = s.FileContentType,
                //                                                 Size = s.FileSize,
                //                                                 Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

                //                                             }).ToList() : null,
                //                                   MainCategory = p.CustomerOfficalDetail.MainCategory.Name,
                //                                   SubCategory = p.CustomerOfficalDetail.catsubID != null ? p.CustomerOfficalDetail.SubCategory.Name : "N/A",
                //                                   Area = p.CustomerOfficalDetail.PropertyArea.Name,
                //                                   PropertyName = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name :
                //                                                    UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().TowerName,
                //                                   PropertyResidencyType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : null,
                //                                   Remarks = p.CustomerOfficalDetail.Remarks,
                //                                   ApartmentName = p.CustomerOfficalDetail.AppartmentNumber,
                //                                   OtherLocation = p.CustomerOfficalDetail.propType == 2 ? UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                //                                                     .Select(u => new GetOtherLocationModel
                //                                                     {
                //                                                         TowerName = u.TowerName,
                //                                                         BuildingName = u.BuildingName,
                //                                                         StreetNumber = u.StreetNumber,
                //                                                         ZoneNumber = u.ZoneNumber,
                //                                                         Loacation = u.Loacation,
                //                                                         LocationLink = u.LocationLink
                //                                                     }).FirstOrDefault() : null,
                //                                   GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                //                                                               .Select(r => new GetCustomerAvailabilityModel
                //                                                               {
                //                                                                   custSCID = r.custSCID,
                //                                                                   Availability = r.Availability,
                //                                                                   KeyCollection = r.KeyCollection,
                //                                                                   AccessProperty = r.AccessProperty,
                //                                                                   ReceptionDate = r.ReceptionDate
                //                                                               }).FirstOrDefault(),
                //                                   GetServices = UhDB.CustomerSpecializedCleanings.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable().
                //                                                   Select(t => new GetServiceSubCategoryModel
                //                                                   {
                //                                                       custSCID = t.custSCID,
                //                                                       servcatID = t.servcatID,
                //                                                       servsubcatID = t.servsubcatID,
                //                                                       ServiceCategoryName = t.ServiceCategory.Name,
                //                                                       ServiceSubCategoryName = t.ServiceSubCategory.Name,
                //                                                       Quantity = t.Quantity
                //                                                   }).ToList(),
                //                                   carstID = p.CustomerOfficalDetail.carstID,
                //                                   cartID = p.CustomerOfficalDetail.cartID,
                //                                   IsCarWash = p.CustomerOfficalDetail.IsCarWash,
                //                                   CarType = p.CustomerOfficalDetail.CarType != null ? p.CustomerOfficalDetail.CarType.Name : "N/A",
                //                                   CarServiceType = p.CustomerOfficalDetail.CarServiceType != null ? p.CustomerOfficalDetail.CarServiceType.Name : "N/A",
                //                                   CustomCarDetails = p.CustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                //                                                  .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,

                //                                   PackageName = p.packID != null ? p.Package.Name : null,
                //                                   Price = p.packID != null ? p.Pricing.Price.ToString() : null,
                //                                   Duration = p.packID != null ? p.Pricing.Duration : null,
                //                                   Measurement = p.packID != null ? p.Pricing.TimeMeasurement : null,
                //                                   packID = p.packID,
                //                                   parkID = p.parkID,
                //                                   Date = Convert.ToDateTime(p.StartDate).ToString("MM/dd/yyyy"),
                //                                   WeeklyCounts = p.packID != null ? p.Package.RecursiveTime.ToString() : null,
                //                                   ServiceDays = p.CustomerOfficalDetail.BuldleDays,
                //                                   EndDate = Convert.ToDateTime(UhDB.CustomerTimelines.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).OrderByDescending(j => j.custTDID).FirstOrDefault().StartDate).ToString("dd/MM/yyyy"),
                //                                   CustomerType = p.Customer.CustomerType != null ? p.Customer.CustomerType1.Name : null,
                //                                   StartTime = p.StartTime,
                //                                   EndTime = p.EndTime,
                //                                   TeamName = p.CustomerOfficalDetail.teamID != null ? p.CustomerOfficalDetail.Team.Name : null,
                //                                   staffName = p.CustomerOfficalDetail.stfID != null ? p.CustomerOfficalDetail.Staff.Name : null,
                //                                   TaskNo = p.TaskNo,
                //                                   NoOfMonths = p.CustomerOfficalDetail.NoOfMonths != null ? p.CustomerOfficalDetail.CustomerRenewalMonth.Name : "N/A",
                //                                   AddedOn = p.CreatedOn,
                //                                   CustomerID = p.Customer.CustomerID,
                //                                   ServiceStatus = p.CustomerOfficalDetail.ServiceStatus == true ? "Active" : p.CustomerOfficalDetail.ServiceStatus == false ? "InActive" : "Pending",
                //                                   stfID = p.CustomerOfficalDetail.stfID,
                //                                   teamID = p.CustomerOfficalDetail.teamID,
                //                                   propaID = p.CustomerOfficalDetail.propaID,
                //                                   vID = p.CustomerOfficalDetail.vID,
                //                                   proprestID = p.CustomerOfficalDetail.proprestID,
                //                                   propType = p.CustomerOfficalDetail.propType,
                //                                   custOPID = p.CustomerOfficalDetail.propType == 2 ? (UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().custODID) : null,
                //                                   cuID = p.custID,
                //                                   cuODID = p.custODID,
                //                                   catID = p.CustomerOfficalDetail.catID,
                //                                   catsubID = p.CustomerOfficalDetail.catsubID
                //                               }).ToList();
                //    if (objCustomerTimelines != null)
                //    {
                //        result.AddRange(objCustomerTimelines);
                //    }
                //}
            }

            return result;
        }


        public GetCustomerDashboardCount GetCustomerDashboardCount(int? cuID)
        {
            GetCustomerDashboardCount result = new GetCustomerDashboardCount();
            GetCustomerDashboardCountDetails objRegularCleaning = new GetCustomerDashboardCountDetails();
            GetCustomerDashboardCountDetails objDeepCleaning = new GetCustomerDashboardCountDetails();
            GetCustomerDashboardCountDetails objSpecializeCleaning = new GetCustomerDashboardCountDetails();
            GetCustomerDashboardCountDetails objCarWashing = new GetCustomerDashboardCountDetails();
            int CountRegularCleaning = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 1
                                       && x.Customer.cuID == cuID && x.IsActive==true && x.IsDelete==false).Count();
            if (CountRegularCleaning != 0)
            {
                var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 1
                                           && x.Customer.cuID == cuID && x.IsActive == true && x.IsDelete == false).ToList();
                objRegularCleaning.catID = 1;
                objRegularCleaning.catsubID = 1;
                objRegularCleaning.TotalCount = objCustomerTimelines.Count();
                objRegularCleaning.PendingCount = objCustomerTimelines.Where(x => x.StatusOfWork == 2).Count();
                objRegularCleaning.CompletedCount = objCustomerTimelines.Where(x => x.StatusOfWork == 3).Count();
                objRegularCleaning.Name = objCustomerTimelines.FirstOrDefault().CustomerOfficalDetail.SubCategory.Name;
            }
            else 
            {
                objRegularCleaning.catID = 1;
                objRegularCleaning.catsubID = 1;
                objRegularCleaning.Name =UhDB.SubCategories.Where(x=>x.catID==1 && x.catsubID==1 && x.IsActive==true && x.IsDelete==false).FirstOrDefault().Name;
            }
            int CountDeepCleaning = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 2
                                    && x.Customer.cuID == cuID && x.IsActive == true && x.IsDelete == false).Count();
            if (CountDeepCleaning != 0)
            {
                var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 2
                                            && x.Customer.cuID == cuID && x.IsActive == true && x.IsDelete == false).ToList();
                objDeepCleaning.catID = 1;
                objDeepCleaning.catsubID = 2;
                objDeepCleaning.TotalCount = objCustomerTimelines.Count();
                objDeepCleaning.PendingCount = objCustomerTimelines.Where(x => x.StatusOfWork == 2).Count();
                objDeepCleaning.CompletedCount = objCustomerTimelines.Where(x => x.StatusOfWork == 3).Count();
                objDeepCleaning.Name = objCustomerTimelines.FirstOrDefault().CustomerOfficalDetail.SubCategory.Name;
            }
            else 
            {
                objDeepCleaning.catID = 1;
                objDeepCleaning.catsubID = 2;
                objDeepCleaning.Name = UhDB.SubCategories.Where(x => x.catID == 1 && x.catsubID == 2 && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
            }

            int SpecializeCount = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 3
                                  && x.Customer.cuID == cuID && x.IsActive == true && x.IsDelete == false).Count();
            if (SpecializeCount != 0)
            {
                var objCustomerSpecializedCleanings = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 3
                                                      && x.Customer.cuID == cuID && x.IsActive == true && x.IsDelete == false).ToList();

                objSpecializeCleaning.catID = 1;
                objSpecializeCleaning.catsubID = 3;
                objSpecializeCleaning.TotalCount = objCustomerSpecializedCleanings.Count();
                objSpecializeCleaning.PendingCount = objCustomerSpecializedCleanings.Where(x => x.StatusOfWork == 2).Count();
                objSpecializeCleaning.CompletedCount = objCustomerSpecializedCleanings.Where(x => x.StatusOfWork == 3).Count();
                objSpecializeCleaning.Name = objCustomerSpecializedCleanings.FirstOrDefault().CustomerOfficalDetail.SubCategory.Name;

            }
            else 
            {
                objSpecializeCleaning.catID = 1;
                objSpecializeCleaning.catsubID = 3;
                objSpecializeCleaning.Name = UhDB.SubCategories.Where(x => x.catID == 1 && x.catsubID == 3 && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
            }

            int CarWashCount = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 2 
                               && x.Customer.cuID == cuID && x.IsActive == true && x.IsDelete == false).Count();
            if (CarWashCount != 0)
            {
                var objCustomerSpecializedCleanings = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == 2 
                                                      && x.Customer.cuID == cuID && x.IsActive == true && x.IsDelete == false).ToList();

                objCarWashing.catID = 2;
                objCarWashing.TotalCount = objCustomerSpecializedCleanings.Count();
                objCarWashing.PendingCount = objCustomerSpecializedCleanings.Where(x => x.StatusOfWork == 2).Count();
                objCarWashing.CompletedCount = objCustomerSpecializedCleanings.Where(x => x.StatusOfWork == 3).Count();
                objCarWashing.Name = objCustomerSpecializedCleanings.FirstOrDefault().CustomerOfficalDetail.MainCategory.Name;

            }
            else
            {
                objCarWashing.catID = 2;
                objCarWashing.Name = UhDB.MainCategories.Where(x => x.catID == 2 && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
            }

            result.RegularCleaning = objRegularCleaning;
            result.DeepCleaning = objDeepCleaning;
            result.SpecializeCleaning = objSpecializeCleaning;
            result.CarWash = objCarWashing;
            return result;
        }

    }
}