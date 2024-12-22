using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UHSForm.Models
{
    public class PricingModel
    {
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> servcatID { get; set; }
        public Nullable<int> servsubcatID { get; set; }
        public Nullable<int> packID { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<int> proprestID { get; set; }
        public Nullable<int> uID { get; set; }
        public Nullable<int> suID { get; set; }
        public Nullable<int> rID { get; set; }
        public Nullable<double> Price { get; set; }
        public string Duration { get; set; }
        public string TimeMeasurement { get; set; }
        public Nullable<int> cartID { get; set; }
        public Nullable<int> carstID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }

    public class GetPricingModel
    {
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> servcatID { get; set; }
        public Nullable<int> servsubcatID { get; set; }
        public Nullable<int> packID { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<int> proprestID { get; set; }
        public Nullable<int> parkID { get; set; }
        public Nullable<int> carstID { get; set; }
        public Nullable<int> cartID { get; set; }
        public string MainCategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string ServiceCategoryName { get; set; }
        public string SubServiceCategoryName { get; set; }
        public string PackageName { get; set; }
        public string PropertyName { get; set; }
        public string PropertyArea { get; set; }
        public string PropertyResidenceType { get; set; }
        public Nullable<double> Price { get; set; }
        public string TimeMeasurement { get; set; }
        public string Duration { get; set; }
        public string CarType { get; set; }
        public string CarTypeService { get; set; }

        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }

    public class GetPricingByServiceModel
    {
        public Nullable<int> packID { get; set; }
        public Nullable<int> parkID { get; set; }
        public string ResidenceType { get; set; }
        public string PackageName { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<double> TotalPrice { get; set; }
        public string Duration { get; set; }
        public string TimeMeasurement { get; set; }
        public Nullable<int> RecursiveTime { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string ServiceCategoryName { get; set; }
        public string ServiceSubCategoryName { get; set; }
        public string CarType { get; set; }
        public string CarTypeService { get; set; }
        public Nullable<int> cartID { get; set; }
        public Nullable<int> carstID { get; set; }
    }

    public class GetPricingBySubCategoryServiceModel
    {
        public Nullable<int> packID { get; set; }
        public Nullable<int> parkID { get; set; }
        public string Assets { get; set; }
        public string PackageName { get; set; }
        public Nullable<double> Price { get; set; }
        public string Duration { get; set; }
        public Nullable<double> TotalPrice { get; set; }
        public Nullable<double> TotalQauntity { get; set; }
        public string TotalDuration { get; set; }
        public string TimeMeasurement { get; set; }
        public Nullable<int> RecursiveTime { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> servcatID { get; set; }
        public Nullable<int> servsubcatID { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string ServiceCategoryName { get; set; }
        public string ServiceSubCategoryName { get; set; }
    }

    public class PackagesBySubCategoryServicesGetModel
    {
        public Nullable<int> uID { get; set; }
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> servcatID { get; set; }
        public List<SubServiceCategoryGetModel> servsubcatIDs { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<int> vID { get; set; }

        public Nullable<int> proprestID { get; set; }
    }

    public class SubServiceCategoryGetModel
    {
        public int? servsubcatID { get; set; }
        public int? Quantity { get; set; }
    }

    public class UpdatePricingModel
    {
        public Nullable<int> catID { get; set; }
        public Nullable<int> catsubID { get; set; }
        public Nullable<int> servcatID { get; set; }
        public Nullable<int> servsubcatID { get; set; }
        public Nullable<int> packID { get; set; }
        public Nullable<int> propaID { get; set; }
        public Nullable<int> vID { get; set; }
        public Nullable<int> proprestID { get; set; }
        public Nullable<int> parkID { get; set; }
        public Nullable<int> carstID { get; set; }
        public Nullable<int> cartID { get; set; }
        public Nullable<double> Price { get; set; }
        public string Duration { get; set; }
        public string TimeMeasurement { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }

    public class DeletePricingModel
    {

        public Nullable<int> parkID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }

        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}