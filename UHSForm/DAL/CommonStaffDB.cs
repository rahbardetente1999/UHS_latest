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
    public class CommonStaffDB
    {
        private UHSEntities UhDB;

        public CommonStaffDB()
        {
            UhDB = new UHSEntities();
        }

        public IEnumerable<GetCustomerStaffModel> GetCustomers(int? stfID)
        {

            List<GetCustomerStaffModel> result = new List<GetCustomerStaffModel>();
            result = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.stfID == stfID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select(p => new GetCustomerStaffModel
                     {
                         CustomerName = p.Customer.Name,
                         CustomerAddress = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name + "_" + p.CustomerOfficalDetail.AppartmentNumber :
                                                                  UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                  .FirstOrDefault().TowerName + "_" + p.CustomerOfficalDetail.AppartmentNumber,
                         AppartmentType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : "N/A",
                         ServiceTime = p.StartTime + " - " + p.EndTime,
                         ServiceType = p.CustomerOfficalDetail.SubCategory.Name,
                         StarTime = ConvertToTimeSpans(p.StartTime),
                         catID = p.CustomerOfficalDetail.catID,
                         catsubID = p.CustomerOfficalDetail.catsubID,
                         cuID = p.custID,
                         cuODID = p.custODID,
                         teamID = p.CustomerOfficalDetail.teamID,
                         packID = p.packID,
                         propType = p.CustomerOfficalDetail.propType,
                         parkID = p.parkID,
                         vID = p.CustomerOfficalDetail.vID,
                         propaID = p.CustomerOfficalDetail.propaID,
                         proprestID = p.CustomerOfficalDetail.proprestID,
                         stfID = stfID,
                         SpecialInstruction=p.CustomerOfficalDetail.Remarks,
                         Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                                                            UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                                                            .Select(s => new GetFileDetails
                                                            {
                                                                Name = s.Filename,
                                                                ContentType = s.FileContentType,
                                                                Size = s.FileSize,
                                                                Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

                                                            }).ToList() : null,
                         GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                    .Select(r => new GetCustomerAvailabilityModel
                                                    {
                                                        custSCID = r.custSCID,
                                                        Availability = r.Availability,
                                                        KeyCollection = r.KeyCollection,
                                                        AccessProperty = r.AccessProperty,
                                                        ReceptionDate = r.ReceptionDate
                                                    }).FirstOrDefault()
                     }).ToList();

            var objTeams = UhDB.StaffTeams.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                          .Select(p => new { teamID = p.teamID }).ToList();
            foreach (var item in objTeams)
            {
                int? teamID = item.teamID;
                var objCustomerOfficalDetails = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.teamID == teamID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                .Select(p => new GetCustomerStaffModel
                                                {
                                                    CustomerName = p.Customer.Name,
                                                    CustomerAddress = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name + "_" + p.CustomerOfficalDetail.AppartmentNumber :
                                                                  UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                  .FirstOrDefault().TowerName + "_" + p.CustomerOfficalDetail.AppartmentNumber,
                                                    AppartmentType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : "N/A",
                                                    ServiceTime = p.StartTime + " - " + p.EndTime,
                                                    ServiceType = p.CustomerOfficalDetail.SubCategory.Name,
                                                    StarTime = ConvertToTimeSpans(p.StartTime),
                                                    catID = p.CustomerOfficalDetail.catID,
                                                    catsubID = p.CustomerOfficalDetail.catsubID,
                                                    cuID = p.custID,
                                                    cuODID = p.custODID,
                                                    teamID = p.CustomerOfficalDetail.teamID,
                                                    packID = p.packID,
                                                    propType = p.CustomerOfficalDetail.propType,
                                                    parkID = p.parkID,
                                                    vID = p.CustomerOfficalDetail.vID,
                                                    propaID = p.CustomerOfficalDetail.propaID,
                                                    proprestID = p.CustomerOfficalDetail.proprestID,
                                                    stfID = stfID,
                                                    SpecialInstruction = p.CustomerOfficalDetail.Remarks,
                                                    Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                                                            UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                                                            .Select(s => new GetFileDetails
                                                            {
                                                                Name = s.Filename,
                                                                ContentType = s.FileContentType,
                                                                Size = s.FileSize,
                                                                Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

                                                            }).ToList() : null,
                                                    GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                    .Select(r => new GetCustomerAvailabilityModel
                                                    {
                                                        custSCID = r.custSCID,
                                                        Availability = r.Availability,
                                                        KeyCollection = r.KeyCollection,
                                                        AccessProperty = r.AccessProperty,
                                                        ReceptionDate = r.ReceptionDate
                                                    }).FirstOrDefault()


                                                }).ToList();
                result.AddRange(objCustomerOfficalDetails);
            }
            result = result.OrderByDescending(x => x.cuODID).ToList();
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

        public IEnumerable<GetCustomerStaffModel> GetTodaysCustomers(int? stfID)
        {
            List<GetCustomerStaffModel> result = new List<GetCustomerStaffModel>();
            var objCustomerOfficalDetails = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.stfID == stfID
                                            && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)
                                            && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                            .Select(p => new GetCustomerStaffModel
                                            {
                                                CustomerName = p.Customer.Name,
                                                CustomerAddress = p.CustomerOfficalDetail.propType==1?p.CustomerOfficalDetail.Venture.Name + " _ " + p.CustomerOfficalDetail.AppartmentNumber:
                                                                  UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                  .FirstOrDefault().TowerName+" _ "+ p.CustomerOfficalDetail.AppartmentNumber,
                                                AppartmentType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : "N/A",
                                                ServiceTime = p.StartTime + " - " + p.EndTime,
                                                ServiceType = p.CustomerOfficalDetail.IsCarWash==false ?p.CustomerOfficalDetail.SubCategory.Name:p.CustomerOfficalDetail.MainCategory.Name,
                                                StarTime=ConvertToTimeSpans(p.StartTime),
                                                StatusOfWork = p.StatusOfWork,
                                                catID =p.CustomerOfficalDetail.catID,
                                                catsubID=p.CustomerOfficalDetail.catsubID,
                                                cuID=p.custID,
                                                cuODID=p.custODID,
                                                custTDID=p.custTDID,
                                                teamID=p.teamID,
                                                packID=p.packID,
                                                propType=p.CustomerOfficalDetail.propType,
                                                parkID=p.parkID,
                                                vID=p.CustomerOfficalDetail.vID,
                                                propaID=p.CustomerOfficalDetail.propaID,
                                                proprestID=p.CustomerOfficalDetail.proprestID,
                                                stfID=stfID,
                                                SpecialInstruction = p.CustomerOfficalDetail.Remarks,
                                                Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                                                            UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                                                            .Select(s => new GetFileDetails
                                                            {
                                                                Name = s.Filename,
                                                                ContentType = s.FileContentType,
                                                                Size = s.FileSize,
                                                                Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

                                                            }).ToList() : null,
                                                GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                    .Select(r => new GetCustomerAvailabilityModel
                                                    {
                                                        custSCID = r.custSCID,
                                                        Availability = r.Availability,
                                                        KeyCollection = r.KeyCollection,
                                                        AccessProperty = r.AccessProperty,
                                                        ReceptionDate = r.ReceptionDate
                                                    }).FirstOrDefault(),
                                                carstID = p.CustomerOfficalDetail.carstID,
                                                cartID = p.CustomerOfficalDetail.cartID,
                                                IsCarWash = p.CustomerOfficalDetail.IsCarWash,
                                                carTRID = p.CustomerOfficalDetail.carTRID,
                                                CarWashTimes = p.CustomerOfficalDetail.carTRID != null ? p.CustomerOfficalDetail.CarWashTimeRange.Name + " " + p.CustomerOfficalDetail.CarWashTimeRange.Timing : null,
                                                CarType = p.CustomerOfficalDetail.CarType != null ? p.CustomerOfficalDetail.CarType.Name : "N/A",
                                                CarServiceType = p.CustomerOfficalDetail.CarServiceType != null ? p.CustomerOfficalDetail.CarServiceType.Name : "N/A",
                                                CustomCarDetails = p.CustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                   .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,
                                                SubArea = p.CustomerOfficalDetail.subAreaID != null ? p.CustomerOfficalDetail.SubArea.Name : "N/A",
                                                Area = p.CustomerOfficalDetail.propaID != null ? p.CustomerOfficalDetail.PropertyArea.Name : "N/A",
                                                Code = p.CustomerOfficalDetail.vID != null ? p.CustomerOfficalDetail.Venture.Code : "N/A",
                                                subAreaID = p.CustomerOfficalDetail.subAreaID
                                            }).ToList();
            if (objCustomerOfficalDetails != null)
            {
                result.AddRange(objCustomerOfficalDetails);
            }
            var objTeams = UhDB.StaffTeams.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                          .Select(p => new { teamID = p.teamID }).ToList();
            foreach (var item in objTeams)
            {
                int? teamID = item.teamID;
                var objCustomerOfficalDetail = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)).AsEnumerable()
                                                .Select(p => new GetCustomerStaffModel
                                                {
                                                    CustomerName = p.Customer.Name,
                                                    CustomerAddress = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name + " _ " + p.CustomerOfficalDetail.AppartmentNumber :
                                                                  UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                  .FirstOrDefault().TowerName + " _ " + p.CustomerOfficalDetail.AppartmentNumber,
                                                    AppartmentType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : "N/A",
                                                    ServiceTime = p.StartTime + " - " + p.EndTime,
                                                    ServiceType = p.CustomerOfficalDetail.IsCarWash == false ? p.CustomerOfficalDetail.SubCategory.Name : p.CustomerOfficalDetail.MainCategory.Name,
                                                    StatusOfWork = p.StatusOfWork,
                                                    StarTime = ConvertToTimeSpans(p.StartTime),
                                                    catID = p.CustomerOfficalDetail.catID,
                                                    catsubID = p.CustomerOfficalDetail.catsubID,
                                                    cuID = p.custID,
                                                    cuODID = p.custODID,
                                                    custTDID = p.custTDID,
                                                    teamID = p.teamID,
                                                    packID = p.packID,
                                                    propType = p.CustomerOfficalDetail.propType,
                                                    parkID = p.parkID,
                                                    vID = p.CustomerOfficalDetail.vID,
                                                    propaID = p.CustomerOfficalDetail.propaID,
                                                    proprestID = p.CustomerOfficalDetail.proprestID,
                                                    stfID = stfID,
                                                    SpecialInstruction = p.CustomerOfficalDetail.Remarks,
                                                    Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                                                            UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                                                            .Select(s => new GetFileDetails
                                                            {
                                                                Name = s.Filename,
                                                                ContentType = s.FileContentType,
                                                                Size = s.FileSize,
                                                                Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

                                                            }).ToList() : null,
                                                    GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                    .Select(r => new GetCustomerAvailabilityModel
                                                    {
                                                        custSCID = r.custSCID,
                                                        Availability = r.Availability,
                                                        KeyCollection = r.KeyCollection,
                                                        AccessProperty = r.AccessProperty,
                                                        ReceptionDate = r.ReceptionDate
                                                    }).FirstOrDefault(),
                                                    carstID = p.CustomerOfficalDetail.carstID,
                                                    cartID = p.CustomerOfficalDetail.cartID,
                                                    IsCarWash = p.CustomerOfficalDetail.IsCarWash,
                                                    carTRID = p.CustomerOfficalDetail.carTRID,
                                                    CarWashTimes = p.CustomerOfficalDetail.carTRID != null ? p.CustomerOfficalDetail.CarWashTimeRange.Name + " " + p.CustomerOfficalDetail.CarWashTimeRange.Timing : null,
                                                    CarType = p.CustomerOfficalDetail.CarType != null ? p.CustomerOfficalDetail.CarType.Name : "N/A",
                                                    CarServiceType = p.CustomerOfficalDetail.CarServiceType != null ? p.CustomerOfficalDetail.CarServiceType.Name : "N/A",
                                                    CustomCarDetails = p.CustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                   .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,
                                                    SubArea = p.CustomerOfficalDetail.subAreaID != null ? p.CustomerOfficalDetail.SubArea.Name : "N/A",
                                                    Area = p.CustomerOfficalDetail.propaID != null ? p.CustomerOfficalDetail.PropertyArea.Name : "N/A",
                                                    Code = p.CustomerOfficalDetail.vID != null ? p.CustomerOfficalDetail.Venture.Code : "N/A",
                                                    subAreaID = p.CustomerOfficalDetail.subAreaID
                                                }).ToList();
                result.AddRange(objCustomerOfficalDetail);
            }
            result = result.OrderBy(x => x.StarTime).ToList();
            return result;

        }

        public IEnumerable<GetCustomerStaffModel> GetTodaysCompletedTasksForStaff(int? stfID)
        {
            List<GetCustomerStaffModel> result = new List<GetCustomerStaffModel>();
            var objCustomerOfficalDetails = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.stfID == stfID
                                            && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)
                                            && x.IsActive == true && x.IsDelete == false && x.StatusOfWork==3).AsEnumerable()
                                            .Select(p => new GetCustomerStaffModel
                                            {
                                                CustomerName = p.Customer.Name,
                                                CustomerAddress = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name + "_" + p.CustomerOfficalDetail.AppartmentNumber :
                                                                  UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                  .FirstOrDefault().TowerName + "_" + p.CustomerOfficalDetail.AppartmentNumber,
                                                AppartmentType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : "N/A",
                                                ServiceTime = p.StartTime + " - " + p.EndTime,
                                                ServiceType = p.CustomerOfficalDetail.IsCarWash == false ? p.CustomerOfficalDetail.SubCategory.Name : p.CustomerOfficalDetail.MainCategory.Name,
                                                StarTime = ConvertToTimeSpans(p.StartTime),
                                                catID = p.CustomerOfficalDetail.catID,
                                                catsubID = p.CustomerOfficalDetail.catsubID,
                                                cuID = p.custID,
                                                cuODID = p.custODID,
                                                custTDID = p.custTDID,
                                                teamID = p.teamID,
                                                packID = p.packID,
                                                propType = p.CustomerOfficalDetail.propType,
                                                parkID = p.parkID,
                                                vID = p.CustomerOfficalDetail.vID,
                                                propaID = p.CustomerOfficalDetail.propaID,
                                                proprestID = p.CustomerOfficalDetail.proprestID,
                                                stfID = stfID,
                                                SpecialInstruction = p.CustomerOfficalDetail.Remarks,
                                                Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                                                            UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                                                            .Select(s => new GetFileDetails
                                                            {
                                                                Name = s.Filename,
                                                                ContentType = s.FileContentType,
                                                                Size = s.FileSize,
                                                                Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

                                                            }).ToList() : null,
                                                GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                    .Select(r => new GetCustomerAvailabilityModel
                                                    {
                                                        custSCID = r.custSCID,
                                                        Availability = r.Availability,
                                                        KeyCollection = r.KeyCollection,
                                                        AccessProperty = r.AccessProperty,
                                                        ReceptionDate = r.ReceptionDate
                                                    }).FirstOrDefault(),
                                                carstID = p.CustomerOfficalDetail.carstID,
                                                cartID = p.CustomerOfficalDetail.cartID,
                                                IsCarWash = p.CustomerOfficalDetail.IsCarWash,
                                                carTRID = p.CustomerOfficalDetail.carTRID,
                                                CarWashTimes = p.CustomerOfficalDetail.carTRID != null ? p.CustomerOfficalDetail.CarWashTimeRange.Name + " " + p.CustomerOfficalDetail.CarWashTimeRange.Timing : null,
                                                CarType = p.CustomerOfficalDetail.CarType != null ? p.CustomerOfficalDetail.CarType.Name : "N/A",
                                                CarServiceType = p.CustomerOfficalDetail.CarServiceType != null ? p.CustomerOfficalDetail.CarServiceType.Name : "N/A",
                                                CustomCarDetails = p.CustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                   .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,
                                                SubArea = p.CustomerOfficalDetail.subAreaID != null ? p.CustomerOfficalDetail.SubArea.Name : "N/A",
                                                Area = p.CustomerOfficalDetail.propaID != null ? p.CustomerOfficalDetail.PropertyArea.Name : "N/A",
                                                Code = p.CustomerOfficalDetail.vID != null ? p.CustomerOfficalDetail.Venture.Code : "N/A",
                                                subAreaID = p.CustomerOfficalDetail.subAreaID
                                            }).ToList();
            if (objCustomerOfficalDetails != null)
            {
                result.AddRange(objCustomerOfficalDetails);
            }
            var objTeams = UhDB.StaffTeams.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                          .Select(p => new { teamID = p.teamID }).ToList();
            foreach (var item in objTeams)
            {
                int? teamID = item.teamID;
                var objCustomerOfficalDetail = UhDB.CustomerTimelines.Where(x => x.teamID == teamID 
                                               && x.IsActive == true && x.IsDelete == false 
                                               && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)
                                               && x.StatusOfWork==3).AsEnumerable()
                                                .Select(p => new GetCustomerStaffModel
                                                {
                                                    CustomerName = p.Customer.Name,
                                                    CustomerAddress = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name + " _ " + p.CustomerOfficalDetail.AppartmentNumber :
                                                                  UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                  .FirstOrDefault().TowerName + " _ " + p.CustomerOfficalDetail.AppartmentNumber,
                                                    AppartmentType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : "N/A",
                                                    ServiceTime = p.StartTime + " - " + p.EndTime,
                                                    ServiceType = p.CustomerOfficalDetail.IsCarWash == false ? p.CustomerOfficalDetail.SubCategory.Name : p.CustomerOfficalDetail.MainCategory.Name,
                                                    StarTime = ConvertToTimeSpans(p.StartTime),
                                                    catID = p.CustomerOfficalDetail.catID,
                                                    catsubID = p.CustomerOfficalDetail.catsubID,
                                                    cuID = p.custID,
                                                    cuODID = p.custODID,
                                                    custTDID = p.custTDID,
                                                    teamID = p.CustomerOfficalDetail.teamID,
                                                    packID = p.packID,
                                                    propType = p.CustomerOfficalDetail.propType,
                                                    parkID = p.parkID,
                                                    vID = p.CustomerOfficalDetail.vID,
                                                    propaID = p.CustomerOfficalDetail.propaID,
                                                    proprestID = p.CustomerOfficalDetail.proprestID,
                                                    stfID = stfID,
                                                    SpecialInstruction = p.CustomerOfficalDetail.Remarks,
                                                    Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                                                            UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                                                            .Select(s => new GetFileDetails
                                                            {
                                                                Name = s.Filename,
                                                                ContentType = s.FileContentType,
                                                                Size = s.FileSize,
                                                                Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

                                                            }).ToList() : null,
                                                    GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                    .Select(r => new GetCustomerAvailabilityModel
                                                    {
                                                        custSCID = r.custSCID,
                                                        Availability = r.Availability,
                                                        KeyCollection = r.KeyCollection,
                                                        AccessProperty = r.AccessProperty,
                                                        ReceptionDate = r.ReceptionDate
                                                    }).FirstOrDefault(),
                                                    carstID = p.CustomerOfficalDetail.carstID,
                                                    cartID = p.CustomerOfficalDetail.cartID,
                                                    IsCarWash = p.CustomerOfficalDetail.IsCarWash,
                                                    carTRID = p.CustomerOfficalDetail.carTRID,
                                                    CarWashTimes = p.CustomerOfficalDetail.carTRID != null ? p.CustomerOfficalDetail.CarWashTimeRange.Name + " " + p.CustomerOfficalDetail.CarWashTimeRange.Timing : null,
                                                    CarType = p.CustomerOfficalDetail.CarType != null ? p.CustomerOfficalDetail.CarType.Name : "N/A",
                                                    CarServiceType = p.CustomerOfficalDetail.CarServiceType != null ? p.CustomerOfficalDetail.CarServiceType.Name : "N/A",
                                                    CustomCarDetails = p.CustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                   .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,
                                                    SubArea = p.CustomerOfficalDetail.subAreaID != null ? p.CustomerOfficalDetail.SubArea.Name : "N/A",
                                                    Area = p.CustomerOfficalDetail.propaID != null ? p.CustomerOfficalDetail.PropertyArea.Name : "N/A",
                                                    Code = p.CustomerOfficalDetail.vID != null ? p.CustomerOfficalDetail.Venture.Code : "N/A",
                                                    subAreaID = p.CustomerOfficalDetail.subAreaID
                                                }).ToList();
                result.AddRange(objCustomerOfficalDetail);
            }
            result = result.OrderBy(x => x.StarTime).ToList();
            return result;

        }

        public IEnumerable<GetCustomerStaffModel> GetTodaysPendingTasksForStaff(int? stfID)
        {
            List<GetCustomerStaffModel> result = new List<GetCustomerStaffModel>();
            var objCustomerOfficalDetails = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.stfID == stfID
                                            && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)
                                            && x.IsActive == true && x.IsDelete == false && x.StatusOfWork == 2).AsEnumerable()
                                            .Select(p => new GetCustomerStaffModel
                                            {
                                                CustomerName = p.Customer.Name,
                                                CustomerAddress = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name + "_" + p.CustomerOfficalDetail.AppartmentNumber :
                                                                  UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                  .FirstOrDefault().TowerName + "_" + p.CustomerOfficalDetail.AppartmentNumber,
                                                AppartmentType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : "N/A",
                                                ServiceTime = p.StartTime + " - " + p.EndTime,
                                                ServiceType = p.CustomerOfficalDetail.IsCarWash == false ? p.CustomerOfficalDetail.SubCategory.Name : p.CustomerOfficalDetail.MainCategory.Name,
                                                StarTime = ConvertToTimeSpans(p.StartTime),
                                                catID = p.CustomerOfficalDetail.catID,
                                                catsubID = p.CustomerOfficalDetail.catsubID,
                                                cuID = p.custID,
                                                cuODID = p.custODID,
                                                custTDID = p.custTDID,
                                                teamID = p.teamID,
                                                packID = p.packID,
                                                propType = p.CustomerOfficalDetail.propType,
                                                parkID = p.parkID,
                                                vID = p.CustomerOfficalDetail.vID,
                                                propaID = p.CustomerOfficalDetail.propaID,
                                                proprestID = p.CustomerOfficalDetail.proprestID,
                                                stfID = stfID,
                                                SpecialInstruction = p.CustomerOfficalDetail.Remarks,
                                                Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                                                            UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                                                            .Select(s => new GetFileDetails
                                                            {
                                                                Name = s.Filename,
                                                                ContentType = s.FileContentType,
                                                                Size = s.FileSize,
                                                                Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

                                                            }).ToList() : null,
                                                GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                    .Select(r => new GetCustomerAvailabilityModel
                                                    {
                                                        custSCID = r.custSCID,
                                                        Availability = r.Availability,
                                                        KeyCollection = r.KeyCollection,
                                                        AccessProperty = r.AccessProperty,
                                                        ReceptionDate = r.ReceptionDate
                                                    }).FirstOrDefault(),
                                                carstID = p.CustomerOfficalDetail.carstID,
                                                cartID = p.CustomerOfficalDetail.cartID,
                                                IsCarWash = p.CustomerOfficalDetail.IsCarWash,
                                                carTRID = p.CustomerOfficalDetail.carTRID,
                                                CarWashTimes = p.CustomerOfficalDetail.carTRID != null ? p.CustomerOfficalDetail.CarWashTimeRange.Name + " " + p.CustomerOfficalDetail.CarWashTimeRange.Timing : null,
                                                CarType = p.CustomerOfficalDetail.CarType != null ? p.CustomerOfficalDetail.CarType.Name : "N/A",
                                                CarServiceType = p.CustomerOfficalDetail.CarServiceType != null ? p.CustomerOfficalDetail.CarServiceType.Name : "N/A",
                                                CustomCarDetails = p.CustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                   .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,
                                                SubArea = p.CustomerOfficalDetail.subAreaID != null ? p.CustomerOfficalDetail.SubArea.Name : "N/A",
                                                Area = p.CustomerOfficalDetail.propaID != null ? p.CustomerOfficalDetail.PropertyArea.Name : "N/A",
                                                Code = p.CustomerOfficalDetail.vID != null ? p.CustomerOfficalDetail.Venture.Code : "N/A",
                                                subAreaID = p.CustomerOfficalDetail.subAreaID
                                            }).ToList();
            if (objCustomerOfficalDetails != null)
            {
                result.AddRange(objCustomerOfficalDetails);
            }
            var objTeams = UhDB.StaffTeams.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                          .Select(p => new { teamID = p.teamID }).ToList();
            foreach (var item in objTeams)
            {
                int? teamID = item.teamID;
                var objCustomerOfficalDetail = UhDB.CustomerTimelines.Where(x => x.teamID == teamID
                                               && x.IsActive == true && x.IsDelete == false
                                               && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(DateTime.Now)
                                               && x.StatusOfWork == 2).AsEnumerable()
                                                .Select(p => new GetCustomerStaffModel
                                                {
                                                    CustomerName = p.Customer.Name,
                                                    CustomerAddress = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name + " _ " + p.CustomerOfficalDetail.AppartmentNumber :
                                                                  UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                  .FirstOrDefault().TowerName + " _ " + p.CustomerOfficalDetail.AppartmentNumber,
                                                    AppartmentType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : "N/A",
                                                    ServiceTime = p.StartTime + " - " + p.EndTime,
                                                    ServiceType = p.CustomerOfficalDetail.IsCarWash == false ? p.CustomerOfficalDetail.SubCategory.Name : p.CustomerOfficalDetail.MainCategory.Name,
                                                    StarTime = ConvertToTimeSpans(p.StartTime),
                                                    catID = p.CustomerOfficalDetail.catID,
                                                    catsubID = p.CustomerOfficalDetail.catsubID,
                                                    cuID = p.custID,
                                                    cuODID = p.custODID,
                                                    custTDID = p.custTDID,
                                                    teamID = p.teamID,
                                                    packID = p.packID,
                                                    propType = p.CustomerOfficalDetail.propType,
                                                    parkID = p.parkID,
                                                    vID = p.CustomerOfficalDetail.vID,
                                                    propaID = p.CustomerOfficalDetail.propaID,
                                                    proprestID = p.CustomerOfficalDetail.proprestID,
                                                    stfID = stfID,
                                                    SpecialInstruction = p.CustomerOfficalDetail.Remarks,
                                                    Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                                                            UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                                                            .Select(s => new GetFileDetails
                                                            {
                                                                Name = s.Filename,
                                                                ContentType = s.FileContentType,
                                                                Size = s.FileSize,
                                                                Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

                                                            }).ToList() : null,
                                                    GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                    .Select(r => new GetCustomerAvailabilityModel
                                                    {
                                                        custSCID = r.custSCID,
                                                        Availability = r.Availability,
                                                        KeyCollection = r.KeyCollection,
                                                        AccessProperty = r.AccessProperty,
                                                        ReceptionDate = r.ReceptionDate
                                                    }).FirstOrDefault(),
                                                    carstID = p.CustomerOfficalDetail.carstID,
                                                    cartID = p.CustomerOfficalDetail.cartID,
                                                    IsCarWash = p.CustomerOfficalDetail.IsCarWash,
                                                    carTRID = p.CustomerOfficalDetail.carTRID,
                                                    CarWashTimes = p.CustomerOfficalDetail.carTRID != null ? p.CustomerOfficalDetail.CarWashTimeRange.Name + " " + p.CustomerOfficalDetail.CarWashTimeRange.Timing : null,
                                                    CarType = p.CustomerOfficalDetail.CarType != null ? p.CustomerOfficalDetail.CarType.Name : "N/A",
                                                    CarServiceType = p.CustomerOfficalDetail.CarServiceType != null ? p.CustomerOfficalDetail.CarServiceType.Name : "N/A",
                                                    CustomCarDetails = p.CustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                       .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,
                                                    SubArea = p.CustomerOfficalDetail.subAreaID != null ? p.CustomerOfficalDetail.SubArea.Name : "N/A",
                                                    Area = p.CustomerOfficalDetail.propaID != null ? p.CustomerOfficalDetail.PropertyArea.Name : "N/A",
                                                    Code = p.CustomerOfficalDetail.vID != null ? p.CustomerOfficalDetail.Venture.Code : "N/A",
                                                    subAreaID = p.CustomerOfficalDetail.subAreaID
                                                }).ToList();
                result.AddRange(objCustomerOfficalDetail);
            }
            result = result.OrderBy(x => x.StarTime).ToList();
            return result;

        }

        public IEnumerable<GetCustomerStaffModel> GetTotalTaskForStaff(int? stfID)
        {
            List<GetCustomerStaffModel> result = new List<GetCustomerStaffModel>();
            var objCustomerOfficalDetails = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.stfID == stfID
                                            && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                            .Select(p => new GetCustomerStaffModel
                                            {
                                                CustomerName = p.Customer.Name,
                                                CustomerAddress = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name + "_" + p.CustomerOfficalDetail.AppartmentNumber :
                                                                  UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                  .FirstOrDefault().TowerName + "_" + p.CustomerOfficalDetail.AppartmentNumber,
                                                AppartmentType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : "N/A",
                                                ServiceTime = p.StartTime + " - " + p.EndTime,
                                                ServiceType = p.CustomerOfficalDetail.IsCarWash == false ? p.CustomerOfficalDetail.SubCategory.Name : p.CustomerOfficalDetail.MainCategory.Name,
                                                StatusOfWork =p.StatusOfWork,
                                                StartDate=p.StartDate,
                                                StarTime = ConvertToTimeSpans(p.StartTime),
                                                catID = p.CustomerOfficalDetail.catID,
                                                catsubID = p.CustomerOfficalDetail.catsubID,
                                                cuID = p.custID,
                                                cuODID = p.custODID,
                                                custTDID = p.custTDID,
                                                teamID = p.teamID,
                                                packID = p.packID,
                                                propType = p.CustomerOfficalDetail.propType,
                                                parkID = p.parkID,
                                                vID = p.CustomerOfficalDetail.vID,
                                                propaID = p.CustomerOfficalDetail.propaID,
                                                proprestID = p.CustomerOfficalDetail.proprestID,
                                                stfID = stfID,
                                                SpecialInstruction = p.CustomerOfficalDetail.Remarks,
                                                Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                                                            UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                                                            .Select(s => new GetFileDetails
                                                            {
                                                                Name = s.Filename,
                                                                ContentType = s.FileContentType,
                                                                Size = s.FileSize,
                                                                Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

                                                            }).ToList() : null,
                                                GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                    .Select(r => new GetCustomerAvailabilityModel
                                                    {
                                                        custSCID = r.custSCID,
                                                        Availability = r.Availability,
                                                        KeyCollection = r.KeyCollection,
                                                        AccessProperty = r.AccessProperty,
                                                        ReceptionDate = r.ReceptionDate
                                                    }).FirstOrDefault(),
                                                carstID = p.CustomerOfficalDetail.carstID,
                                                cartID = p.CustomerOfficalDetail.cartID,
                                                IsCarWash = p.CustomerOfficalDetail.IsCarWash,
                                                carTRID = p.CustomerOfficalDetail.carTRID,
                                                CarWashTimes = p.CustomerOfficalDetail.carTRID != null ? p.CustomerOfficalDetail.CarWashTimeRange.Name + " " + p.CustomerOfficalDetail.CarWashTimeRange.Timing : null,
                                                CarType = p.CustomerOfficalDetail.CarType != null ? p.CustomerOfficalDetail.CarType.Name : "N/A",
                                                CarServiceType = p.CustomerOfficalDetail.CarServiceType != null ? p.CustomerOfficalDetail.CarServiceType.Name : "N/A",
                                                CustomCarDetails = p.CustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                   .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,
                                                SubArea=p.CustomerOfficalDetail.subAreaID!=null? p.CustomerOfficalDetail.SubArea.Name:"N/A",
                                                Area= p.CustomerOfficalDetail.propaID != null ? p.CustomerOfficalDetail.PropertyArea.Name : "N/A",
                                                Code= p.CustomerOfficalDetail.vID != null ? p.CustomerOfficalDetail.Venture.Code : "N/A",
                                                subAreaID=p.CustomerOfficalDetail.subAreaID
                                            }).ToList();
            if (objCustomerOfficalDetails != null)
            {
                result.AddRange(objCustomerOfficalDetails);
            }
            var objTeams = UhDB.StaffTeams.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                          .Select(p => new { teamID = p.teamID }).ToList();
            foreach (var item in objTeams)
            {
                int? teamID = item.teamID;
                var objCustomerOfficalDetail = UhDB.CustomerTimelines.Where(x => x.teamID == teamID 
                                                && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                .Select(p => new GetCustomerStaffModel
                                                {
                                                    CustomerName = p.Customer.Name,
                                                    CustomerAddress = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name + " _ " + p.CustomerOfficalDetail.AppartmentNumber :
                                                                  UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                  .FirstOrDefault().TowerName + " _ " + p.CustomerOfficalDetail.AppartmentNumber,
                                                    AppartmentType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : "N/A",
                                                    ServiceTime = p.StartTime + " - " + p.EndTime,
                                                    ServiceType = p.CustomerOfficalDetail.IsCarWash == false ? p.CustomerOfficalDetail.SubCategory.Name : p.CustomerOfficalDetail.MainCategory.Name,
                                                    StatusOfWork = p.StatusOfWork,
                                                    StartDate =p.StartDate,
                                                    StarTime = ConvertToTimeSpans(p.StartTime),
                                                    catID = p.CustomerOfficalDetail.catID,
                                                    catsubID = p.CustomerOfficalDetail.catsubID,
                                                    cuID = p.custID,
                                                    cuODID = p.custODID,
                                                    custTDID = p.custTDID,
                                                    teamID = p.teamID,
                                                    packID = p.packID,
                                                    propType = p.CustomerOfficalDetail.propType,
                                                    parkID = p.parkID,
                                                    vID = p.CustomerOfficalDetail.vID,
                                                    propaID = p.CustomerOfficalDetail.propaID,
                                                    proprestID = p.CustomerOfficalDetail.proprestID,
                                                    stfID = stfID,
                                                    SpecialInstruction = p.CustomerOfficalDetail.Remarks,
                                                    Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                                                            UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                                                            .Select(s => new GetFileDetails
                                                            {
                                                                Name = s.Filename,
                                                                ContentType = s.FileContentType,
                                                                Size = s.FileSize,
                                                                Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

                                                            }).ToList() : null,
                                                    GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                    .Select(r => new GetCustomerAvailabilityModel
                                                    {
                                                        custSCID = r.custSCID,
                                                        Availability = r.Availability,
                                                        KeyCollection = r.KeyCollection,
                                                        AccessProperty = r.AccessProperty,
                                                        ReceptionDate = r.ReceptionDate
                                                    }).FirstOrDefault(),
                                                    carstID = p.CustomerOfficalDetail.carstID,
                                                    cartID = p.CustomerOfficalDetail.cartID,
                                                    IsCarWash = p.CustomerOfficalDetail.IsCarWash,
                                                    carTRID = p.CustomerOfficalDetail.carTRID,
                                                    CarWashTimes = p.CustomerOfficalDetail.carTRID != null ? p.CustomerOfficalDetail.CarWashTimeRange.Name + " " + p.CustomerOfficalDetail.CarWashTimeRange.Timing : null,
                                                    CarType = p.CustomerOfficalDetail.CarType != null ? p.CustomerOfficalDetail.CarType.Name : "N/A",
                                                    CarServiceType = p.CustomerOfficalDetail.CarServiceType != null ? p.CustomerOfficalDetail.CarServiceType.Name : "N/A",
                                                    CustomCarDetails = p.CustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                       .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,
                                                    SubArea = p.CustomerOfficalDetail.subAreaID != null ? p.CustomerOfficalDetail.SubArea.Name : "N/A",
                                                    Area = p.CustomerOfficalDetail.propaID != null ? p.CustomerOfficalDetail.PropertyArea.Name : "N/A",
                                                    Code = p.CustomerOfficalDetail.vID != null ? p.CustomerOfficalDetail.Venture.Code : "N/A",
                                                    subAreaID = p.CustomerOfficalDetail.subAreaID
                                                }).ToList();
                result.AddRange(objCustomerOfficalDetail);
            }
            result = result.OrderBy(x => x.StartDate).ToList();
            result = result.OrderBy(x => x.StarTime).ToList();
            return result;

        }

        public IEnumerable<GetCustomerStaffModel> GetCompletedTasksForStaff(int? stfID)
        {
            List<GetCustomerStaffModel> result = new List<GetCustomerStaffModel>();
            var objCustomerOfficalDetails = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.stfID == stfID
                                            && x.IsActive == true && x.IsDelete == false && x.StatusOfWork == 3).AsEnumerable()
                                            .Select(p => new GetCustomerStaffModel
                                            {
                                                CustomerName = p.Customer.Name,
                                                CustomerAddress = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name + "_" + p.CustomerOfficalDetail.AppartmentNumber :
                                                                  UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                  .FirstOrDefault().TowerName + "_" + p.CustomerOfficalDetail.AppartmentNumber,
                                                AppartmentType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : "N/A",
                                                ServiceTime = p.StartTime + " - " + p.EndTime,
                                                ServiceType = p.CustomerOfficalDetail.IsCarWash == false ? p.CustomerOfficalDetail.SubCategory.Name : p.CustomerOfficalDetail.MainCategory.Name,
                                                StartDate =p.StartDate,
                                                StarTime = ConvertToTimeSpans(p.StartTime),
                                                catID = p.CustomerOfficalDetail.catID,
                                                catsubID = p.CustomerOfficalDetail.catsubID,
                                                cuID = p.custID,
                                                cuODID = p.custODID,
                                                custTDID = p.custTDID,
                                                teamID = p.teamID,
                                                packID = p.packID,
                                                propType = p.CustomerOfficalDetail.propType,
                                                parkID = p.parkID,
                                                vID = p.CustomerOfficalDetail.vID,
                                                propaID = p.CustomerOfficalDetail.propaID,
                                                proprestID = p.CustomerOfficalDetail.proprestID,
                                                stfID = stfID,
                                                SpecialInstruction = p.CustomerOfficalDetail.Remarks,
                                                Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                                                            UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                                                            .Select(s => new GetFileDetails
                                                            {
                                                                Name = s.Filename,
                                                                ContentType = s.FileContentType,
                                                                Size = s.FileSize,
                                                                Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

                                                            }).ToList() : null,
                                                GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                    .Select(r => new GetCustomerAvailabilityModel
                                                    {
                                                        custSCID = r.custSCID,
                                                        Availability = r.Availability,
                                                        KeyCollection = r.KeyCollection,
                                                        AccessProperty = r.AccessProperty,
                                                        ReceptionDate = r.ReceptionDate
                                                    }).FirstOrDefault(),
                                                CustomerRating=UhDB.CustomerFeedbacks.Where(x=>x.custTDID==p.custTDID && x.IsActive==true && x.IsDelete==false).Count()!=0?
                                                               UhDB.CustomerFeedbacks.Where(x => x.custTDID == p.custTDID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                               .Select(s=>new GetCustomerServiceRatingModel {Rating=s.Rating,Feedback=s.Feedback}).FirstOrDefault():null,
                                                carstID = p.CustomerOfficalDetail.carstID,
                                                cartID = p.CustomerOfficalDetail.cartID,
                                                IsCarWash = p.CustomerOfficalDetail.IsCarWash,
                                                carTRID = p.CustomerOfficalDetail.carTRID,
                                                CarWashTimes = p.CustomerOfficalDetail.carTRID != null ? p.CustomerOfficalDetail.CarWashTimeRange.Name + " " + p.CustomerOfficalDetail.CarWashTimeRange.Timing : null,
                                                CarType = p.CustomerOfficalDetail.CarType != null ? p.CustomerOfficalDetail.CarType.Name : "N/A",
                                                CarServiceType = p.CustomerOfficalDetail.CarServiceType != null ? p.CustomerOfficalDetail.CarServiceType.Name : "N/A",
                                                CustomCarDetails = p.CustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                   .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,
                                                SubArea = p.CustomerOfficalDetail.subAreaID != null ? p.CustomerOfficalDetail.SubArea.Name : "N/A",
                                                Area = p.CustomerOfficalDetail.propaID != null ? p.CustomerOfficalDetail.PropertyArea.Name : "N/A",
                                                Code = p.CustomerOfficalDetail.vID != null ? p.CustomerOfficalDetail.Venture.Code : "N/A",
                                                subAreaID = p.CustomerOfficalDetail.subAreaID
                                            }).ToList();
            if (objCustomerOfficalDetails != null)
            {
                result.AddRange(objCustomerOfficalDetails);
            }
            var objTeams = UhDB.StaffTeams.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                          .Select(p => new { teamID = p.teamID }).ToList();
            foreach (var item in objTeams)
            {
                int? teamID = item.teamID;
                var objCustomerOfficalDetail = UhDB.CustomerTimelines.Where(x => x.teamID == teamID
                                               && x.IsActive == true && x.IsDelete == false
                                               && x.StatusOfWork == 3).AsEnumerable()
                                                .Select(p => new GetCustomerStaffModel
                                                {
                                                    CustomerName = p.Customer.Name,
                                                    CustomerAddress = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name + " _ " + p.CustomerOfficalDetail.AppartmentNumber :
                                                                  UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                  .FirstOrDefault().TowerName + " _ " + p.CustomerOfficalDetail.AppartmentNumber,
                                                    AppartmentType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : "N/A",
                                                    ServiceTime = p.StartTime + " - " + p.EndTime,
                                                    ServiceType = p.CustomerOfficalDetail.IsCarWash == false ? p.CustomerOfficalDetail.SubCategory.Name : p.CustomerOfficalDetail.MainCategory.Name,
                                                    StartDate = p.StartDate,
                                                    StarTime = ConvertToTimeSpans(p.StartTime),
                                                    catID = p.CustomerOfficalDetail.catID,
                                                    catsubID = p.CustomerOfficalDetail.catsubID,
                                                    cuID = p.custID,
                                                    cuODID = p.custODID,
                                                    custTDID = p.custTDID,
                                                    teamID = p.teamID,
                                                    packID = p.packID,
                                                    propType = p.CustomerOfficalDetail.propType,
                                                    parkID = p.parkID,
                                                    vID = p.CustomerOfficalDetail.vID,
                                                    propaID = p.CustomerOfficalDetail.propaID,
                                                    proprestID = p.CustomerOfficalDetail.proprestID,
                                                    stfID = stfID,
                                                    SpecialInstruction = p.CustomerOfficalDetail.Remarks,
                                                    Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                                                            UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                                                            .Select(s => new GetFileDetails
                                                            {
                                                                Name = s.Filename,
                                                                ContentType = s.FileContentType,
                                                                Size = s.FileSize,
                                                                Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

                                                            }).ToList() : null,
                                                    GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                    .Select(r => new GetCustomerAvailabilityModel
                                                    {
                                                        custSCID = r.custSCID,
                                                        Availability = r.Availability,
                                                        KeyCollection = r.KeyCollection,
                                                        AccessProperty = r.AccessProperty,
                                                        ReceptionDate = r.ReceptionDate
                                                    }).FirstOrDefault(),
                                                    CustomerRating = UhDB.CustomerFeedbacks.Where(x => x.custTDID == p.custTDID && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                                               UhDB.CustomerFeedbacks.Where(x => x.custTDID == p.custTDID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                               .Select(s => new GetCustomerServiceRatingModel { Rating = s.Rating, Feedback = s.Feedback }).FirstOrDefault() : null,
                                                    carstID = p.CustomerOfficalDetail.carstID,
                                                    cartID = p.CustomerOfficalDetail.cartID,
                                                    IsCarWash = p.CustomerOfficalDetail.IsCarWash,
                                                    carTRID = p.CustomerOfficalDetail.carTRID,
                                                    CarWashTimes = p.CustomerOfficalDetail.carTRID != null ? p.CustomerOfficalDetail.CarWashTimeRange.Name + " " + p.CustomerOfficalDetail.CarWashTimeRange.Timing : null,
                                                    CarType = p.CustomerOfficalDetail.CarType != null ? p.CustomerOfficalDetail.CarType.Name : "N/A",
                                                    CarServiceType = p.CustomerOfficalDetail.CarServiceType != null ? p.CustomerOfficalDetail.CarServiceType.Name : "N/A",
                                                    CustomCarDetails = p.CustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                       .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,
                                                    SubArea = p.CustomerOfficalDetail.subAreaID != null ? p.CustomerOfficalDetail.SubArea.Name : "N/A",
                                                    Area = p.CustomerOfficalDetail.propaID != null ? p.CustomerOfficalDetail.PropertyArea.Name : "N/A",
                                                    Code = p.CustomerOfficalDetail.vID != null ? p.CustomerOfficalDetail.Venture.Code : "N/A",
                                                    subAreaID = p.CustomerOfficalDetail.subAreaID

                                                }).ToList();
                result.AddRange(objCustomerOfficalDetail);
            }
            result = result.OrderBy(x => x.StartDate).ToList();
            result = result.OrderBy(x => x.StarTime).ToList();
            return result;

        }

        public IEnumerable<GetCustomerStaffModel> GetPedingTasksForStaff(int? stfID,DateTime SearchDate)
        {
            List<GetCustomerStaffModel> result = new List<GetCustomerStaffModel>();
            var objCustomerOfficalDetails = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.stfID == stfID
                                            && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(SearchDate)
                                            && x.IsActive == true && x.IsDelete == false && x.StatusOfWork == 2).AsEnumerable()
                                            .Select(p => new GetCustomerStaffModel
                                            {
                                                CustomerName = p.Customer.Name,
                                                CustomerAddress = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name + "_" + p.CustomerOfficalDetail.AppartmentNumber :
                                                                  UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                  .FirstOrDefault().TowerName + "_" + p.CustomerOfficalDetail.AppartmentNumber,
                                                AppartmentType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : "N/A",
                                                ServiceTime = p.StartTime + " - " + p.EndTime,
                                                ServiceType = p.CustomerOfficalDetail.IsCarWash == false ? p.CustomerOfficalDetail.SubCategory.Name : p.CustomerOfficalDetail.MainCategory.Name,
                                                StartDate =p.StartDate,
                                                StarTime = ConvertToTimeSpans(p.StartTime),
                                                catID = p.CustomerOfficalDetail.catID,
                                                catsubID = p.CustomerOfficalDetail.catsubID,
                                                cuID = p.custID,
                                                cuODID = p.custODID,
                                                custTDID = p.custTDID,
                                                teamID = p.teamID,
                                                packID = p.packID,
                                                propType = p.CustomerOfficalDetail.propType,
                                                parkID = p.parkID,
                                                vID = p.CustomerOfficalDetail.vID,
                                                propaID = p.CustomerOfficalDetail.propaID,
                                                proprestID = p.CustomerOfficalDetail.proprestID,
                                                stfID = stfID,
                                                SpecialInstruction = p.CustomerOfficalDetail.Remarks,
                                                Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                                                            UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                                                            .Select(s => new GetFileDetails
                                                            {
                                                                Name = s.Filename,
                                                                ContentType = s.FileContentType,
                                                                Size = s.FileSize,
                                                                Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

                                                            }).ToList() : null,
                                                GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                    .Select(r => new GetCustomerAvailabilityModel
                                                    {
                                                        custSCID = r.custSCID,
                                                        Availability = r.Availability,
                                                        KeyCollection = r.KeyCollection,
                                                        AccessProperty = r.AccessProperty,
                                                        ReceptionDate = r.ReceptionDate
                                                    }).FirstOrDefault(),
                                                carstID = p.CustomerOfficalDetail.carstID,
                                                cartID = p.CustomerOfficalDetail.cartID,
                                                IsCarWash = p.CustomerOfficalDetail.IsCarWash,
                                                carTRID = p.CustomerOfficalDetail.carTRID,
                                                CarWashTimes = p.CustomerOfficalDetail.carTRID != null ? p.CustomerOfficalDetail.CarWashTimeRange.Name + " " + p.CustomerOfficalDetail.CarWashTimeRange.Timing : null,
                                                CarType = p.CustomerOfficalDetail.CarType != null ? p.CustomerOfficalDetail.CarType.Name : "N/A",
                                                CarServiceType = p.CustomerOfficalDetail.CarServiceType != null ? p.CustomerOfficalDetail.CarServiceType.Name : "N/A",
                                                CustomCarDetails = p.CustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                  .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,
                                                SubArea = p.CustomerOfficalDetail.subAreaID != null ? p.CustomerOfficalDetail.SubArea.Name : "N/A",
                                                Area = p.CustomerOfficalDetail.propaID != null ? p.CustomerOfficalDetail.PropertyArea.Name : "N/A",
                                                Code = p.CustomerOfficalDetail.vID != null ? p.CustomerOfficalDetail.Venture.Code : "N/A",
                                                subAreaID = p.CustomerOfficalDetail.subAreaID
                                            }).ToList();
            if (objCustomerOfficalDetails != null)
            {
                result.AddRange(objCustomerOfficalDetails);
            }
            var objTeams = UhDB.StaffTeams.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                          .Select(p => new { teamID = p.teamID }).ToList();
            foreach (var item in objTeams)
            {
                int? teamID = item.teamID;
                var objCustomerOfficalDetail = UhDB.CustomerTimelines.Where(x => x.teamID == teamID
                                               && x.IsActive == true && x.IsDelete == false
                                               && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(SearchDate)
                                               && x.StatusOfWork == 2).AsEnumerable()
                                                .Select(p => new GetCustomerStaffModel
                                                {
                                                    CustomerName = p.Customer.Name,
                                                    CustomerAddress = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.Venture.Name + " _ " + p.CustomerOfficalDetail.AppartmentNumber :
                                                                  UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                  .FirstOrDefault().TowerName + " _ " + p.CustomerOfficalDetail.AppartmentNumber,
                                                    AppartmentType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : "N/A",
                                                    ServiceTime = p.StartTime + " - " + p.EndTime,
                                                    ServiceType = p.CustomerOfficalDetail.IsCarWash == false ? p.CustomerOfficalDetail.SubCategory.Name : p.CustomerOfficalDetail.MainCategory.Name,
                                                    StartDate =p.StartDate,
                                                    StarTime = ConvertToTimeSpans(p.StartTime),
                                                    catID = p.CustomerOfficalDetail.catID,
                                                    catsubID = p.CustomerOfficalDetail.catsubID,
                                                    cuID = p.custID,
                                                    cuODID = p.custODID,
                                                    custTDID = p.custTDID,
                                                    teamID = p.teamID,
                                                    packID = p.packID,
                                                    propType = p.CustomerOfficalDetail.propType,
                                                    parkID = p.parkID,
                                                    vID = p.CustomerOfficalDetail.vID,
                                                    propaID = p.CustomerOfficalDetail.propaID,
                                                    proprestID = p.CustomerOfficalDetail.proprestID,
                                                    stfID = stfID,
                                                    SpecialInstruction = p.CustomerOfficalDetail.Remarks,
                                                    Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                                                            UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                                                            .Select(s => new GetFileDetails
                                                            {
                                                                Name = s.Filename,
                                                                ContentType = s.FileContentType,
                                                                Size = s.FileSize,
                                                                Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

                                                            }).ToList() : null,
                                                    GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                    .Select(r => new GetCustomerAvailabilityModel
                                                    {
                                                        custSCID = r.custSCID,
                                                        Availability = r.Availability,
                                                        KeyCollection = r.KeyCollection,
                                                        AccessProperty = r.AccessProperty,
                                                        ReceptionDate = r.ReceptionDate
                                                    }).FirstOrDefault(),
                                                    carstID = p.CustomerOfficalDetail.carstID,
                                                    cartID = p.CustomerOfficalDetail.cartID,
                                                    IsCarWash = p.CustomerOfficalDetail.IsCarWash,
                                                    carTRID = p.CustomerOfficalDetail.carTRID,
                                                    CarWashTimes = p.CustomerOfficalDetail.carTRID != null ? p.CustomerOfficalDetail.CarWashTimeRange.Name + " " + p.CustomerOfficalDetail.CarWashTimeRange.Timing : null,
                                                    CarType = p.CustomerOfficalDetail.CarType != null ? p.CustomerOfficalDetail.CarType.Name : "N/A",
                                                    CarServiceType = p.CustomerOfficalDetail.CarServiceType != null ? p.CustomerOfficalDetail.CarServiceType.Name : "N/A",
                                                    CustomCarDetails = p.CustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                       .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,
                                                    SubArea = p.CustomerOfficalDetail.subAreaID != null ? p.CustomerOfficalDetail.SubArea.Name : "N/A",
                                                    Area = p.CustomerOfficalDetail.propaID != null ? p.CustomerOfficalDetail.PropertyArea.Name : "N/A",
                                                    Code = p.CustomerOfficalDetail.vID != null ? p.CustomerOfficalDetail.Venture.Code : "N/A",
                                                    subAreaID = p.CustomerOfficalDetail.subAreaID
                                                }).ToList();
                result.AddRange(objCustomerOfficalDetail);
            }
            result = result.OrderBy(x => x.StartDate).ToList();
            result = result.OrderBy(x => x.StarTime).ToList();
            return result;

        }


        public GetStaffAverageRating GetStaffAverageRating(int? stfID)
        {
            GetStaffAverageRating result = new GetStaffAverageRating();
            double? RegularCleaningRating = 0, DeepCleaningRating = 0, SpecializeCleaningRating = 0;
            int RegularCleaingCustomerCount = 0, DeepCleaningCustomerCount = 0, SpecializeCleaningCustomerCount = 0;
            var objRegularCleaningCustomers = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.stfID == stfID && x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 1 && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objRegularCleaningCustomer in objRegularCleaningCustomers)
            {
                int? cuID = objRegularCleaningCustomer.custID;
                int? custODID = objRegularCleaningCustomer.custODID;
                int? custTdID = objRegularCleaningCustomer.custTDID;
                int CountCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID==custTdID && x.IsActive == true && x.IsDelete == false).Count();
                if (CountCustomerRating != 0)
                {
                    var objCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID == custTdID && x.IsActive == true && x.IsDelete == false).OrderByDescending(y => y.custfdbID).FirstOrDefault();
                    RegularCleaningRating = objCustomerRating.Rating + RegularCleaningRating;
                    RegularCleaingCustomerCount = RegularCleaingCustomerCount + 1;
                }

            }

            var objDeepCleaningRatingCustomers = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.stfID == stfID && x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 2 && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objDeepCleaningRatingCustomer in objDeepCleaningRatingCustomers)
            {
                int? cuID = objDeepCleaningRatingCustomer.custID;
                int? custODID = objDeepCleaningRatingCustomer.custODID;
                int? custTdID = objDeepCleaningRatingCustomer.custTDID;
                int CountCustomRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID==custTdID && x.IsActive == true && x.IsDelete == false).Count();
                if (CountCustomRating != 0)
                {
                    var objCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID == custTdID && x.IsActive == true && x.IsDelete == false).OrderByDescending(y => y.custfdbID).FirstOrDefault();
                    DeepCleaningRating = objCustomerRating.Rating + DeepCleaningRating;
                    DeepCleaningCustomerCount = DeepCleaningCustomerCount + 1;
                }
            }

            var objSpecializeCleaningRatingCustomers = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.stfID == stfID && x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 3 && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objSpecializeCleaningRatingCustomer in objSpecializeCleaningRatingCustomers)
            {
                int? cuID = objSpecializeCleaningRatingCustomer.custID;
                int? custODID = objSpecializeCleaningRatingCustomer.custODID;
                int? custTdID = objSpecializeCleaningRatingCustomer.custTDID;
                int CountCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID==custTdID && x.IsActive == true && x.IsDelete == false).Count();
                if (CountCustomerRating != 0)
                {
                    var objCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID == custTdID && x.IsActive == true && x.IsDelete == false).OrderByDescending(y => y.custfdbID).FirstOrDefault();
                    SpecializeCleaningRating = objCustomerRating.Rating + SpecializeCleaningRating;
                    SpecializeCleaningCustomerCount = SpecializeCleaningCustomerCount + 1;
                }

            }
            var objTeams = UhDB.StaffTeams.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objTeam in objTeams)
            {
                int? teamID = objTeam.teamID;
                var objRegularCleaningCustomersForTeams = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 1 && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objRegularCleaningCustomersForTeam in objRegularCleaningCustomersForTeams)
                {
                    int? cuID = objRegularCleaningCustomersForTeam.custID;
                    int? custODID = objRegularCleaningCustomersForTeam.custODID;
                    int? custTdID = objRegularCleaningCustomersForTeam.custTDID;
                    int CountCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID==custTdID && x.IsActive == true && x.IsDelete == false).Count();
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
                    int CountCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID==custTdID && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountCustomerRating != 0)
                    {
                        var objCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID==custTdID && x.IsActive == true && x.IsDelete == false).OrderByDescending(y => y.custfdbID).FirstOrDefault();
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
                    int CountCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID==custTdID && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountCustomerRating != 0)
                    {
                        var objCustomerRating = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.custTDID==custTdID && x.IsActive == true && x.IsDelete == false).OrderByDescending(y => y.custfdbID).FirstOrDefault();
                        SpecializeCleaningRating = objCustomerRating.Rating + SpecializeCleaningRating;
                        SpecializeCleaningCustomerCount = SpecializeCleaningCustomerCount + 1;
                    }
                }
            }
            if (RegularCleaingCustomerCount != 0)
            {
                result.RegularCleaning = RegularCleaningRating / RegularCleaingCustomerCount;
            }
            if (DeepCleaningCustomerCount != 0)
            {
                result.DeepCleaning = DeepCleaningRating / DeepCleaningCustomerCount;
            }
            if (SpecializeCleaningCustomerCount != 0)
            {
                result.SpecializedClaeaning = SpecializeCleaningRating / SpecializeCleaningCustomerCount;
            }
            return result;
        }

        public GetStaffAverageRating GetTotalService(int? stfID)
        {
            GetStaffAverageRating result = new GetStaffAverageRating();
            int RegularCleaingCustomerCount = 0, DeepCleaningCustomerCount = 0, SpecializeCleaningCustomerCount = 0;
            RegularCleaingCustomerCount = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.stfID == stfID && x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 1 && x.IsActive == true && x.IsDelete == false).Count();
            DeepCleaningCustomerCount = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.stfID == stfID && x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 2 && x.IsActive == true && x.IsDelete == false).Count();
            SpecializeCleaningCustomerCount = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.stfID == stfID && x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 3 && x.IsActive == true && x.IsDelete == false).Count();
            var objTeams = UhDB.StaffTeams.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objTeam in objTeams)
            {
                int? teamID = objTeam.teamID;
                int RegularCleaingCustomerForTeamsCount = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.teamID == teamID && x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 1 && x.IsActive == true && x.IsDelete == false).Count();
                RegularCleaingCustomerCount = RegularCleaingCustomerCount + RegularCleaingCustomerForTeamsCount;
                int DeepCleaningCustomerForTeamsCount = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.teamID == teamID && x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 2 && x.IsActive == true && x.IsDelete == false).Count();
                DeepCleaningCustomerCount = DeepCleaningCustomerCount + DeepCleaningCustomerForTeamsCount;
                int SpecializeCleaningRatingCustomersForTeams = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.teamID == teamID && x.CustomerOfficalDetail.catID == 1 && x.CustomerOfficalDetail.catsubID == 3 && x.IsActive == true && x.IsDelete == false).Count();
                SpecializeCleaningCustomerCount = SpecializeCleaningCustomerCount + SpecializeCleaningRatingCustomersForTeams;
            }

            return result;
        }

        public List<MonthName> GetDashboardMonthsDropdown(int? stfID)
        {
            List<MonthName> result = new List<MonthName>();
            int? StaffCount = UhDB.CustomerOfficalDetails.Where(x => x.stfID==stfID && x.IsActive == true && x.IsDelete == false).Count();
            if (StaffCount!=0) 
            {
                var objStartDate = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.stfID==stfID && x.IsActive == true && x.IsDelete == false).ToList();
                objStartDate = objStartDate.OrderBy(x => x.StartDate).ToList();
                DateTime StartDate = Convert.ToDateTime(objStartDate[0].StartDate);
                DateTime EndDate = Convert.ToDateTime(objStartDate.Last().StartDate);
                if (StaffCount == 1)
                {
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
                    for (var i = 0; i <= StaffCount; i++)
                    {
                        StartDate = StartDate.AddMonths(i);
                        string Month = Convert.ToDateTime(StartDate).ToString("MMMM");
                        result.Add(new MonthName { Name = Month });
                    }
                }

            }
            var objTeams = UhDB.StaffTeams.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objTeam in objTeams)
            {
                int? teamID = objTeam.teamID;
                int? TeamCount= UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).Count();
                if (TeamCount != 0)
                {
                    var objStartDate = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).ToList();
                    objStartDate = objStartDate.OrderBy(x => x.StartDate).ToList();
                    DateTime StartDate = Convert.ToDateTime(objStartDate[0].StartDate);
                    DateTime EndDate = Convert.ToDateTime(objStartDate.Last().StartDate);
                    if (TeamCount == 1)
                    {
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
                        int monthsDifference = GetMonthsDifference(StartDate, EndDate);
                        if (EndDate.Month != StartDate.Month) 
                        {
                            monthsDifference = monthsDifference + 1;
                        }
                        for (var i = 0; i <= monthsDifference; i++)
                        {
                            StartDate = StartDate.AddMonths(i);
                            string Month = Convert.ToDateTime(StartDate).ToString("MMMM");
                            result.Add(new MonthName { Name = Month });
                        }
                    }

                }
            }
            result = result.Distinct().ToList();
            return result;
        }

        public List<StartDates> GetDashboardDatesDropdown(int? stfID,string MonthName)
        {
            List<StartDates> result = new List<StartDates>();
            int? StaffCount = UhDB.CustomerOfficalDetails.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false).Count();
            if (StaffCount != 0)
            {
                var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.stfID == stfID && 
                                   x.IsActive == true && x.IsDelete == false).ToList();

                if (objCustomerTimelines != null)
                {
                    foreach (var objCustomerTimeline in objCustomerTimelines)
                    {
                        if (Convert.ToDateTime(objCustomerTimeline.StartDate).ToString("MMMM") == MonthName)
                        {
                            result.Add(new StartDates { StartDate = Convert.ToDateTime(objCustomerTimeline.StartDate) });
                        }
                    }
                }

            }
            var objTeams = UhDB.StaffTeams.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objTeam in objTeams)
            {
                int? teamID = objTeam.teamID;
                int? TeamCount = UhDB.CustomerTimelines.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).Count();
                if (TeamCount != 0)
                {
                    var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.teamID == teamID 
                                       && x.IsActive == true && x.IsDelete == false).ToList();
                    if (objCustomerTimelines != null) 
                    {
                        foreach (var objCustomerTimeline in objCustomerTimelines)
                        {
                            if (Convert.ToDateTime(objCustomerTimeline.StartDate).ToString("MMMM") == MonthName) 
                            {
                                result.Add(new StartDates { StartDate = Convert.ToDateTime(objCustomerTimeline.StartDate) });
                            }
                        }
                    }
                    
                }
            }
            result = result.OrderByDescending(x=>x.StartDate).ToList();
            foreach (var item in result)
            {
                DateTime StartDate = item.StartDate;
                if (!result.Any(x=>x.StartDate== StartDate)) 
                {
                    result.Add(new StartDates {StartDate=StartDate});
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
}