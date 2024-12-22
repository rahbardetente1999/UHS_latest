using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class CustomerModel
    {
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public IEnumerable<PostServiceSubCategoryModel> ServiceSubCategory { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public Nullable<int> packID { get; set; }
        public Nullable<int> parkID { get; set; }
        public Nullable<int> monthlyCount { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<int> tempID { get; set; }
        public Nullable<int> subAreaID { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<int> proprestID { get; set; }
        public Nullable<int> carTRID { get; set; }
        public Nullable<int> propType { get; set; }
        public Nullable<bool> SpecialService { get; set; }
        public string Remarks { get; set; }
        public PostPackagesModelV2 Packages { get; set; }
        public string TowerName { get; set; }
        public string BuildingName { get; set; }
        public string StreetNumber { get; set; }
        public string ZoneNumber { get; set; }
        public string Location { get; set; }
        public string LocationLink { get; set; }
        public Nullable<bool> Availability { get; set; }
        public Nullable<bool> KeyCollection { get; set; }
        public string AccessProperty { get; set; }
        public Nullable<DateTime> ReceptionDate { get; set; }
        public string Salutaion { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public Nullable<int> PhoneCode { get; set; }
        public string WhatsAppNo { get; set; }
        public string AlternativeNo { get; set; }
        public string Email { get; set; }
        public string AppartmentNumber { get; set; }
        public string pdfBase64 { get; set; }
        public string Amount { get; set; }
        public string InVoice { get; set; }
        public string NoOfMonths { get; set; }
        public string DiscountPercentage { get; set; }
        public string DiscountPrice { get; set; }
        public string Price { get; set; }
        public string ParkingLevel { get; set; }
        public string ParkingNumber { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleColor { get; set; }
        public string VehicleNumber { get; set; }
        public Nullable<int> TotalNoOfService { get; set; }
        public Nullable<int> carstID { get; set; }
        public Nullable<int> cartID { get; set; }
        public Nullable<bool> IsCarWash { get; set; }
        public Nullable<int> teamID { get; set; }
        public List<BundleOfDays> BundleOfDays { get; set; }
        public Nullable<int> WorkStatus { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<int> CreatedRole { get; set; }
    }

    public class BundleOfDays
    {
        public string Days { get; set; }
        public CustomTimes Times { get; set; }
    }

    public class CustomTimes
    {
        public string Start { get; set; }
        public string End { get; set; }
    }

    public class CreateAppointmentModel
    {
        public Nullable<int> cuID { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> monthlyCount { get; set; }
        public Nullable<int> parkID { get; set; }
        public Nullable<int> packID { get; set; }
        public List<PostServiceSubCategoryModel> ServiceSubCategory { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<int> proprestID { get; set; }
        public Nullable<int> carTRID { get; set; }
        public Nullable<int> tempID { get; set; }
        public Nullable<int> subAreaID { get; set; }
        public Nullable<int> propType { get; set; }
        public Nullable<bool> SpecialService { get; set; }
        public string Remarks { get; set; }
        public PostPackagesModelV2 Packages { get; set; }
        public string TowerName { get; set; }
        public string BuildingName { get; set; }
        public string StreetNumber { get; set; }
        public string ZoneNumber { get; set; }
        public string Location { get; set; }
        public string LocationLink { get; set; }
        public Nullable<bool> Availability { get; set; }
        public Nullable<bool> KeyCollection { get; set; }
        public string AccessProperty { get; set; }
        public Nullable<DateTime> ReceptionDate { get; set; }
        public string AppartmentNumber { get; set; }
        public string pdfBase64 { get; set; }
        public string Amount { get; set; }
        public string InVoice { get; set; }
        public string NoOfMonths { get; set; }
        public string DiscountPercentage { get; set; }
        public string DiscountPrice { get; set; }
        public string Price { get; set; }
        public string ParkingLevel { get; set; }
        public string ParkingNumber { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleColor { get; set; }
        public string VehicleNumber { get; set; }
        public Nullable<int> TotalNoOfService { get; set; }
        public Nullable<int> teamID { get; set; }
        public List<BundleOfDays> BundleOfDays { get; set; }
        public Nullable<int> WorkStatus { get; set; }
        public Nullable<int> carstID { get; set; }
        public Nullable<int> cartID { get; set; }
        public Nullable<bool> IsCarWash { get; set; }
        public Nullable<bool> IsSameTeam { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<int> CreatedRole { get; set; }
    }

    public class ClosedTaskModel
    {
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> cuID { get; set; }
        public Nullable<int> custODID { get; set; }
        public Nullable<int> custTDID { get; set; }
        public Nullable<int> TaskNo { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<bool> IsSpecialService { get; set; }
        public string EndTime { get; set; }
        public string Remarks { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<int> UpdatedRole { get; set; }
    }

    public class CountSameTeamModel
    {
        public Nullable<int> cuID { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
    }

    public class GetCustomerModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string WhatsAppNo { get; set; }
        public string AlternativeNo { get; set; }
        public string Saluation { get; set; }
        public string MainCategory { get; set; }
        public string SubCategory { get; set; }
        public string Area { get; set; }
        public string PropertyName { get; set; }
        public string PropertyResidencyType { get; set; }
        public string Remarks { get; set; }
        public string ApartmentName { get; set; }
        public Nullable<int> WorkStatus { get; set; }
        public IEnumerable<GetFileDetails> Files { get; set; }
        public GetCustomerAvailabilityModel GetCustomerAvailability { get; set; }
        public IEnumerable<GetPackagesForSubCategoryModel> Packages { get; set; }
        public IEnumerable<GetServiceSubCategoryModel> GetServices { get; set; }
        public GetOtherLocationModel OtherLocation { get; set; }
        public string TeamName { get; set; }
        public string staffName { get; set; }
        public string ServiceStatus { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> TaskNo { get; set; }
        public Nullable<int> teamID { get; set; }
        public Nullable<int> stfID { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<int> proprestID { get; set; }
        public Nullable<int> propType { get; set; }
        public Nullable<int> custOPID { get; set; }
        public Nullable<int> cuID { get; set; }
        public Nullable<int> cuODID { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }

    }

    public class GetCustomerModelV2
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string WhatsAppNo { get; set; }
        public string AlternativeNo { get; set; }
        public string Saluation { get; set; }
        public string MainCategory { get; set; }
        public string SubCategory { get; set; }
        public string Area { get; set; }
        public string PropertyName { get; set; }
        public string PropertyResidencyType { get; set; }
        public string Remarks { get; set; }
        public string ApartmentName { get; set; }
        public Nullable<int> WorkStatus { get; set; }
        public IEnumerable<GetFileDetails> Files { get; set; }
        public GetCustomerAvailabilityModel GetCustomerAvailability { get; set; }
        public IEnumerable<GetServiceSubCategoryModel> GetServices { get; set; }
        public GetOtherLocationModel OtherLocation { get; set; }
        public GetCustomerPaymentStatus PaymentStatus { get; set; }
        public GetCustomerCustomeDate CustomDates { get; set; }
        public GetCustomerCarDetails CustomCarDetails { get; set; }
        public string PackageName { get; set; }
        public string Duration { get; set; }
        public string Measurement { get; set; }
        public string Price { get; set; }
        public string WeeklyCounts { get; set; }
        public string EndDate { get; set; }
        public string CustomerType { get; set; }
        public string ServiceDays { get; set; }
        public string NoOfMonths { get; set; }
        public string CarType { get; set; }
        public string CarServiceType { get; set; }
        public string CarWashTimes { get; set; }
        public Nullable<int> carstID { get; set; }
        public Nullable<int> cartID { get; set; }
        public Nullable<int> carTRID { get; set; }
        public Nullable<bool> IsCarWash { get; set; }
        public Nullable<int> packID { get; set; }
        public Nullable<int> parkID { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string TeamName { get; set; }
        public string staffName { get; set; }
        public string ServiceStatus { get; set; }
        public string CreatedOn { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> TaskNo { get; set; }
        public Nullable<int> teamID { get; set; }
        public Nullable<int> stfID { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<int> proprestID { get; set; }
        public Nullable<int> propType { get; set; }
        public Nullable<int> custOPID { get; set; }
        public Nullable<int> cuID { get; set; }
        public Nullable<int> cuODID { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }

    }

    public class GetCustomerModelV8
    {
        public string ServiceStatus { get; set; }
        public string Measurement { get; set; }
        public string PropertyResidencyType { get; set; }
        public string ApartmentName { get; set; }
        public string PaymentStatus { get; set; }
        public string Duration { get; set; }
        public string CustomerType { get; set; }
        public string ServiceDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string TeamName { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string ServiceType { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> teamID { get; set; }
        public Nullable<int> cuID { get; set; }
        public Nullable<int> cuODID { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<int> subAreaID { get; set; }
        public Nullable<int> proprestID { get; set; }
        public Nullable<int> propType { get; set; }
        public Nullable<int> packID { get; set; }
        public Nullable<int> parkID { get; set; }

    }

    public class GetCustomerModelV6
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string WhatsAppNo { get; set; }
        public string AlternativeNo { get; set; }
        public string Saluation { get; set; }
        public string Area { get; set; }
        public string PropertyName { get; set; }
        public string PropertyResidencyType { get; set; }
        public string ApartmentName { get; set; }
        public Nullable<int> PropertyType { get; set; }
        public GetOtherLocationModel OtherLocation { get; set; }
    }

    public class GetCustomerCarDetails
    {
        public string ParkingLevel { get; set; }
        public string ParkingNumber { get; set; }
        public string VehicleBrand { get; set; }
        public string VehicleColor { get; set; }
        public string VehicleNumber { get; set; }
    }

    public class GetCustomerPaymentStatus
    {
        public int custTrasID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> TotalPrice { get; set; }
        public string PayementID { get; set; }
        public string TransactionID { get; set; }
        public Nullable<int> PaymentStatus { get; set; }

    }

    public class GetCustomerCustomeDate
    {
        public Nullable<int> custDTID { get; set; }
        public string CustomStartDate { get; set; }
        public string CustomStartTime { get; set; }
        public string CustomEndTime { get; set; }
        public string CustomDays { get; set; }
    }

    public class SuspendCustomerServiceModel
    {
        public Nullable<int> cuID { get; set; }
        public Nullable<int> custODID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<int> UpdatedRole { get; set; }
        public string UpdatedBy { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class GetCustomerStaffModel
    {
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string AppartmentType { get; set; }
        public Nullable<int> StatusOfWork { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public string ServiceType { get; set; }
        public string ServiceTime { get; set; }
        public string Area { get; set; }
        public string SubArea { get; set; }
        public string Code { get; set; }
        public TimeSpan StarTime { get; set; }
        public Nullable<int> teamID { get; set; }
        public Nullable<int> stfID { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<int> proprestID { get; set; }
        public Nullable<int> propType { get; set; }
        public Nullable<int> cuID { get; set; }
        public Nullable<int> cuODID { get; set; }
        public Nullable<int> custTDID { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> packID { get; set; }
        public Nullable<int> parkID { get; set; }
        public Nullable<int> carstID { get; set; }
        public Nullable<int> cartID { get; set; }
        public Nullable<int> carTRID { get; set; }
        public Nullable<int> subAreaID { get; set; }
        public Nullable<bool> IsCarWash { get; set; }
        public string SpecialInstruction { get; set; }
        public string CarWashTimes { get; set; }
        public string CarType { get; set; }
        public string CarServiceType { get; set; }
        public GetCustomerCarDetails CustomCarDetails { get; set; }
        public GetCustomerAvailabilityModel GetCustomerAvailability { get; set; }
        public List<GetFileDetails> Files { get; set; }
        public GetCustomerServiceRatingModel CustomerRating { get; set; }

    }

    public class GetCustomerForSubCategoryModel
    {
        public string SubCategory { get; set; }
        public string Area { get; set; }
        public string PropertyName { get; set; }
        public string PropertyResidencyType { get; set; }
        public GetCustomerAvailabilityModel GetCustomerAvailability { get; set; }
        public IEnumerable<GetPackagesForSubCategoryModel> Packages { get; set; }
        public IEnumerable<GetServiceSubCategoryModel> GetServices { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<int> proprestID { get; set; }
        public Nullable<int> propType { get; set; }
        public Nullable<int> custOPID { get; set; }

    }

    public class GetCustomerForServiceCategoryModel
    {
        public string SubCategory { get; set; }
        public string Area { get; set; }
        public string PropertyName { get; set; }
        public GetCustomerAvailabilityModel GetCustomerAvailability { get; set; }
        public IEnumerable<GetPackagesForSubCategoryModel> Packages { get; set; }
        public IEnumerable<GetServiceSubCategoryModel> GetServices { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<int> propType { get; set; }
        public Nullable<int> custOPID { get; set; }

    }

    public class GetTimeLineModel
    {
        public Nullable<DateTime> Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }

    public class GetTimeLineResultModel
    {
        public List<GetTimeLineResulsubsettModel> Time { get; set; }
    }

    public class GetTimeLineResulsubsettModel
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }


    public class GetTimeLineNewModel
    {
        public Nullable<DateTime> Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }

    public class GetSlotsTimeLineModel
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }

    public class GetTempTimeLineModel
    {
        public IEnumerable<GetTimeLineModel> TimeLine { get; set; }

    }

    public class GetTempTimeLineSlotModel
    {
        public IEnumerable<GetSlotsTimeLineModel> TimeLine { get; set; }

    }

    public class GetOtherLocationModel
    {
        public string TowerName { get; set; }
        public string BuildingName { get; set; }
        public string StreetNumber { get; set; }
        public string ZoneNumber { get; set; }
        public string Loacation { get; set; }
        public string LocationLink { get; set; }

    }


    public class TimeLineGetModel
    {
        public Nullable<int> uID { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<bool> IsServiceCategory { get; set; }
        public Nullable<int> servcatID { get; set; }
        public List<TimeLineSubCategoryGetModel> ServiceSubCategory { get; set; }
        public Nullable<int> packID { get; set; }
        public Nullable<int> parkID { get; set; }
    }

    public class SlotTimeLineGetModel
    {
        public Nullable<int> uID { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<bool> IsServiceCategory { get; set; }
        public Nullable<int> servcatID { get; set; }
        public List<TimeLineSubCategoryGetModel> ServiceSubCategory { get; set; }
        public Nullable<int> packID { get; set; }
        public Nullable<int> parkID { get; set; }
        public Nullable<DateTime> Date { get; set; }
    }

    public class SlotTimeLineForSatffGetModel
    {
        public Nullable<int> uID { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<bool> IsServiceCategory { get; set; }
        public Nullable<int> servcatID { get; set; }
        public List<TimeLineSubCategoryGetModel> ServiceSubCategory { get; set; }
        public Nullable<int> packID { get; set; }
        public Nullable<int> parkID { get; set; }
        public Nullable<int> stfID { get; set; }
        public Nullable<int> teamID { get; set; }
        public Nullable<bool> IsTeam { get; set; }
    }

    public class TimeLineSubCategoryGetModel
    {
        public Nullable<int> servsubcatID { get; set; }
        public Nullable<int> packID { get; set; }
        public Nullable<int> parkID { get; set; }
    }

    public class PostServiceSubCategoryModel
    {
        public Nullable<int> servcatID { get; set; }
        public Nullable<int> servsubcatID { get; set; }
        public Nullable<int> parkID { get; set; }
        public Nullable<int> packID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<double> EachServiceprice { get; set; }
        public Nullable<double> TotalPrice { get; set; }
    }

    public class GetServiceSubCategoryModel
    {
        public Nullable<int> custSCID { get; set; }
        public Nullable<int> servcatID { get; set; }
        public Nullable<int> servsubcatID { get; set; }
        public string ServiceCategoryName { get; set; }
        public string ServiceSubCategoryName { get; set; }
        public Nullable<int> Quantity { get; set; }
    }

    public class GetCustomerAvailabilityModel
    {
        public int custSCID { get; set; }
        public Nullable<bool> Availability { get; set; }
        public Nullable<bool> KeyCollection { get; set; }
        public string AccessProperty { get; set; }
        public Nullable<DateTime> ReceptionDate { get; set; }

    }

    public class PostPackagesModel
    {
        public Nullable<int> packID { get; set; }
        public Nullable<int> parkID { get; set; }
        public Nullable<DateTime> Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }

    public class PostPackagesModelV2
    {
        public Nullable<int> packID { get; set; }
        public Nullable<int> parkID { get; set; }
        public string[] BundleDays { get; set; }
        public Nullable<double> EachServiceprice { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<double> TotalPriceForEachQuantity { get; set; }
        public Nullable<double> TotalPrice { get; set; }
        public List<string> Time { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public Nullable<bool> IsCustomDays { get; set; }
        public string[] CustomDays { get; set; }
        public Nullable<bool> IsCustomTime { get; set; }
        public List<string> CustomTime { get; set; }
        public Nullable<bool> IsCustomSelectDate { get; set; }
        public Nullable<DateTime> CustomSelectDate { get; set; }
    }

    public class GetPackagesForSubCategoryModel
    {
        public string PackageName { get; set; }
        public string Duration { get; set; }
        public string Measurement { get; set; }
        public string Price { get; set; }
        public Nullable<int> packID { get; set; }
        public Nullable<int> parkID { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }

    public class AssignTeamCustomer
    {
        public Nullable<int> cuID { get; set; }
        public Nullable<int> custODID { get; set; }
        public Nullable<int> custTDID { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> stfID { get; set; }
        public Nullable<int> teamID { get; set; }
        public Nullable<bool> IsTeam { get; set; }
        public Nullable<bool> IsTeamPermanent { get; set; }
        public Nullable<bool> IsTeamReAssign { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<int> UpdatedRole { get; set; }
    }

    public class GetCustomerModelV3
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string WhatsAppNo { get; set; }
        public string AlternativeNo { get; set; }
        public string Saluation { get; set; }
        public string MainCategory { get; set; }
        public string SubCategory { get; set; }
        public string Area { get; set; }
        public string PropertyName { get; set; }
        public string PropertyResidencyType { get; set; }
        public string Remarks { get; set; }
        public string ApartmentName { get; set; }
        public IEnumerable<GetFileDetails> Files { get; set; }
        public GetCustomerAvailabilityModel GetCustomerAvailability { get; set; }
        public IEnumerable<GetServiceSubCategoryModel> GetServices { get; set; }
        public IEnumerable<GetPackagesForSubCategoryForParticularCustomerModel> Packages { get; set; }
        public GetOtherLocationModel OtherLocation { get; set; }
        public GetCustomerPaymentStatus PaymentStatus { get; set; }
        public GetCustomerCustomeDate CustomDates { get; set; }
        public GetCustomerCarDetails CustomCarDetails { get; set; }
        public string TeamName { get; set; }
        public string staffName { get; set; }
        public string ServiceStatus { get; set; }
        public string NoOfMonths { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string CarType { get; set; }
        public string CarServiceType { get; set; }
        public Nullable<int> carstID { get; set; }
        public Nullable<int> cartID { get; set; }
        public Nullable<bool> IsCarWash { get; set; }
        public Nullable<int> teamID { get; set; }
        public Nullable<int> stfID { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<int> proprestID { get; set; }
        public Nullable<int> propType { get; set; }
        public Nullable<int> custOPID { get; set; }
        public Nullable<int> cuID { get; set; }
        public Nullable<int> cuODID { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }

    }

    public class GetCustomerModelV4
    {
        public string TimeMeasurement { get; set; }
        public string PackageName { get; set; }
        public string TaskNo { get; set; }
        public string ServiceDate { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public string ServiceDay { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }
        public string Price { get; set; }
        public string PaymentStatus { get; set; }
        public string Status { get; set; }
        public string WorkingStatus { get; set; }
        public string Staffs { get; set; }
        public string TeamName { get; set; }
        public Nullable<int> cuID { get; set; }
        public Nullable<int> custTDID { get; set; }
        public Nullable<int> custODID { get; set; }
        public Nullable<int> packID { get; set; }
        public Nullable<int> proprestID { get; set; }
        public Nullable<int> teamID { get; set; }
        public Nullable<int> parkID { get; set; }
        public List<GetCustomerServiceRatingModel> Rating { get; set; }
    }

    public class GetCustomerModelV5
    {

        public string PackageName { get; set; }
        public string TaskNo { get; set; }
        public string ServiceDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }
        public string Price { get; set; }
        public string PaymentStatus { get; set; }
        public string Status { get; set; }
        public string WorkingStatus { get; set; }
        public Nullable<int> cuID { get; set; }
        public Nullable<int> custTDID { get; set; }
        public Nullable<int> custODID { get; set; }
        public Nullable<int> parkID { get; set; }
        public Nullable<int> packID { get; set; }
        public Nullable<int> proprestID { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> teamID { get; set; }
        public string TimeMeasurement { get; set; }

    }

    public class GetCustomerModelV7
    {
        public string TimeMeasurement { get; set; }
        public string PackageName { get; set; }
        public string TaskNo { get; set; }
        public string ServiceDate { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public string ServiceDay { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }
        public string Price { get; set; }
        public string PaymentStatus { get; set; }
        public string Status { get; set; }
        public string WorkingStatus { get; set; }
        public string Staffs { get; set; }
        public string TeamName { get; set; }
        public Nullable<int> cuID { get; set; }
        public Nullable<int> custTDID { get; set; }
        public Nullable<int> custODID { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<int> proprestID { get; set; }
        public Nullable<int> propType { get; set; }
        public Nullable<int> teamID { get; set; }
        public Nullable<int> parkID { get; set; }
        public Nullable<int> packID { get; set; }
        public IEnumerable<GetServiceSubCategoryModel> GetServices { get; set; }
        public List<GetCustomerServiceRatingModel> Rating { get; set; }
        public GetCustomerCarDetails CustomCarDetails { get; set; }
        public GetOtherLocationModel OtherLocation { get; set; }
        public string MainCategory { get; set; }
        public string SubCategory { get; set; }
        public string Area { get; set; }
        public string PropertyName { get; set; }
        public string PropertyResidencyType { get; set; }
        public string ApartmentName { get; set; }
        public string CarType { get; set; }
        public string CarServiceType { get; set; }
        public Nullable<int> carstID { get; set; }
        public Nullable<int> cartID { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<bool> IsCarWash { get; set; }
    }


    public class GetCustomerPaymentModel
    {
        public string PackageName { get; set; }
        public string FinalPrice { get; set; }
        public string DiscountPrice { get; set; }
        public string NoOfMonths { get; set; }
        public string DiscountPercentage { get; set; }
        public string Paid { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }



    }

    public class GetPackagesForSubCategoryForParticularCustomerModel
    {
        public string PackageName { get; set; }
        public string Duration { get; set; }
        public string Measurement { get; set; }
        public string Price { get; set; }
        public Nullable<int> packID { get; set; }
        public Nullable<int> parkID { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string TaskNo { get; set; }
        public string WorkStatus { get; set; }
    }


}