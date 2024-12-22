using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class CustomerRenewalModel
    {
        public Nullable<int> propaID { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<int> proprestID { get; set; }
        public Nullable<int> proTypeID { get; set; }
        public Nullable<int> cuID { get; set; }
        public string InVoice { get; set; }
        public Nullable<bool> IsAdmin { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> CreatedRole { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }

    public class CheckTeamAvailable 
    {
        public Nullable<int> teamID { get; set; }
        public Nullable<int> cuID { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<int> proprestID { get; set; }
    }

    public class GetLastDatesOfRegularCleaning 
    {
        public Nullable<DateTime> LastDate { get; set; }
        public Nullable<DateTime> SecodnLastDate { get; set; }
    }

    public class CustomerRenewalInfo
    {
        public string Salution { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string ApartmentNo { get; set; }
        public string SubCategory { get; set; }
        public string Duration { get; set; }
        public string Price { get; set; }
        public string TotalPrice { get; set; }
        public string TotalNoOfService { get; set; }
        public string PackageName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string NoOfMonths { get; set; }
        public string Days { get; set; }
        public string TimeMeasurement { get; set; }
        public List<CustomTimes> Times { get; set; }

    }

    public class CustomerRenewalPropertyInfo 
    {
        public Nullable<int> propaID { get; set; }
        public Nullable<int> subareaID { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<int> proprestID { get; set; }
        public Nullable<int> proTypeID { get; set; }
        public string PropertyArea { get; set; }
        public string PropertyName { get; set; }
        public string PropertyResidency { get; set; }
        public string PropertType { get; set; }
        public string ApartmentName { get; set; }
    }

    public class GetCustomerRenewalProperty 
    {
        public Nullable<int> ID { get; set; }
        public string Value { get; set; }
        public Nullable<int> propTypeID { get; set; }
    }

    public class GetCustomerRenewalPropertyResidencyType 
    {
        public Nullable<int> ID { get; set; }
        public string Value { get; set; }
        public string AppartmentName { get; set; }
    }

    public class GetDecryptValues
    {
        public string CustomerID { get; set; }
        public string PropertyAreaID { get; set; }
        public string PropertyID { get; set; }
        public string PropertyResidencyID { get; set; }
        public string PropertyTypeID { get; set; }
        public string AppartmentNo { get; set; }
    }

    public class GetCustomerRenewalFromAdmin 
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string WhatsAppNo { get; set; }
        public string Saluation { get; set; }
        public string Area { get; set; }
        public string PropertyName { get; set; }
        public string PropertyResidencyType { get; set; }
        public string ApartmentName { get; set; }
        public IEnumerable<GetFileDetails> Files { get; set; }
        public GetCustomerPaymentStatus PaymentStatus { get; set; }
        public string TeamName { get; set; }
        public string Status { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> CompletdTaskCount { get; set; }
        public Nullable<int> UnCompletdTaskCount { get; set; }
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
}