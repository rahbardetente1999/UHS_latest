using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models;
using UHSForm.Models.Data;

namespace UHSForm.DAL
{
    public class CommonPackagesDB
    {
        public UHSEntities UhDB;

        public CommonPackagesDB()
        {
            UhDB = new UHSEntities();
        }

        public IEnumerable<GetPricingByServiceModel> GetPackagesByServices(int? uID, int? catID, int? catsubID, int? propaID, int? vID, int? proprestID)
        {
            List<GetPricingByServiceModel> result = new List<GetPricingByServiceModel>();
            result = UhDB.Pricings.Where(x => x.catID == catID && x.catsubID == catsubID && x.uID == uID && x.propaID == propaID && x.vID == vID
                     && x.proprestID == proprestID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                    .Select(p => new GetPricingByServiceModel
                    {
                        packID = p.packID,
                        parkID = p.parkID,
                        PackageName = p.Package.Name,
                        Duration = p.Duration,
                        TimeMeasurement = p.TimeMeasurement,
                        RecursiveTime = p.Package.RecursiveTime,
                        Price = p.Price,
                        TotalPrice = p.Price,
                        ResidenceType = p.PropertyResidenceType.Name,
                        catID = catID,
                        catsubID = catsubID,
                        CategoryName = p.catID != null ? p.MainCategory.Name : null,
                        ServiceCategoryName = p.servcatID != null ? p.ServiceCategory.Name : null,
                        ServiceSubCategoryName = p.servsubcatID != null ? p.ServiceSubCategory.Name : null,
                        SubCategoryName = p.catsubID != null ? p.SubCategory.Name : null
                    }).ToList();
            return result;
        }

        public IEnumerable<GetPricingBySubCategoryServiceModel> GetPackagesBySubCategoryServices(PackagesBySubCategoryServicesGetModel packagesBySub)
        {
            List<GetPricingBySubCategoryServiceModel> result = new List<GetPricingBySubCategoryServiceModel>();
            foreach (var item in packagesBySub.servsubcatIDs)
            {
                int? servsubcatID = item.servsubcatID;
                var objPricings = UhDB.Pricings.Where(x => x.uID == packagesBySub.uID
                                 && x.catID == packagesBySub.catID && x.catsubID == packagesBySub.catsubID
                                 && x.servcatID == packagesBySub.servcatID && x.servsubcatID == servsubcatID
                                 && x.propaID == packagesBySub.propaID && x.vID == packagesBySub.vID && x.IsActive == true
                                 && x.IsDelete == false).AsEnumerable()
                                .Select(p => new GetPricingBySubCategoryServiceModel
                                {
                                    packID = p.packID,
                                    parkID = p.parkID,
                                    PackageName = p.Package.Name,
                                    Duration = p.Duration,
                                    Price = p.Price,
                                    TimeMeasurement = p.TimeMeasurement,
                                    RecursiveTime = p.Package.RecursiveTime,
                                    TotalQauntity = item.Quantity,
                                    TotalDuration = (Convert.ToInt32(p.Duration) * item.Quantity).ToString(),
                                    TotalPrice = p.Price * item.Quantity,
                                    Assets = p.ServiceSubCategory.Name,
                                    catID = packagesBySub.catID,
                                    catsubID = packagesBySub.catsubID,
                                    servcatID = packagesBySub.servcatID,
                                    servsubcatID = servsubcatID,
                                    CategoryName = p.catID != null ? p.MainCategory.Name : null,
                                    ServiceCategoryName = p.servcatID != null ? p.ServiceCategory.Name : null,
                                    ServiceSubCategoryName = p.servsubcatID != null ? p.ServiceSubCategory.Name : null,
                                    SubCategoryName = p.catsubID != null ? p.SubCategory.Name : null
                                }).ToList();
                result.AddRange(objPricings);
            }
            return result;
        }

        public IEnumerable<GetPricingByServiceModel> GetPackagesByServicesWithOutProperty(int? uID, int? catID, int? catsubID, int? proprestID)
        {
            List<GetPricingByServiceModel> result = new List<GetPricingByServiceModel>();
            result = UhDB.Pricings.Where(x => x.catID == catID && x.catsubID == catsubID && x.uID == uID
                     && x.proprestID == proprestID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                    .Select(p => new GetPricingByServiceModel
                    {
                        packID = p.packID,
                        parkID = p.parkID,
                        PackageName = p.Package.Name,
                        Duration = p.Duration,
                        TimeMeasurement = p.TimeMeasurement,
                        RecursiveTime = p.Package.RecursiveTime,
                        Price = p.Price,
                        TotalPrice = p.Price,
                        ResidenceType = p.PropertyResidenceType.Name,
                        catID = catID,
                        catsubID = catsubID,
                        CategoryName = p.catID != null ? p.MainCategory.Name : null,
                        ServiceCategoryName = p.servcatID != null ? p.ServiceCategory.Name : null,
                        ServiceSubCategoryName = p.servsubcatID != null ? p.ServiceSubCategory.Name : null,
                        SubCategoryName = p.catsubID != null ? p.SubCategory.Name : null
                    }).ToList();
            return result;
        }

        public IEnumerable<GetPricingBySubCategoryServiceModel> GetPackagesBySubCategoryServicesWithOutProperty(PackagesBySubCategoryServicesGetModel packagesBySub)
        {
            List<GetPricingBySubCategoryServiceModel> result = new List<GetPricingBySubCategoryServiceModel>();
            foreach (var item in packagesBySub.servsubcatIDs)
            {
                int? servsubcatID = item.servsubcatID;
                var objPricings = UhDB.Pricings.Where(x => x.uID == packagesBySub.uID
                                 && x.catID == packagesBySub.catID && x.catsubID == packagesBySub.catsubID
                                 && x.servcatID == packagesBySub.servcatID && x.servsubcatID == servsubcatID
                                 && x.proprestID == packagesBySub.proprestID
                                 && x.IsActive == true
                                 && x.IsDelete == false).AsEnumerable()
                                .Select(p => new GetPricingBySubCategoryServiceModel
                                {
                                    packID = p.packID,
                                    parkID = p.parkID,
                                    PackageName = p.Package.Name,
                                    Duration = p.Duration,
                                    Price = p.Price,
                                    TimeMeasurement = p.TimeMeasurement,
                                    RecursiveTime = p.Package.RecursiveTime,
                                    TotalQauntity = item.Quantity,
                                    TotalDuration = (Convert.ToInt32(p.Duration) * item.Quantity).ToString(),
                                    TotalPrice = p.Price * item.Quantity,
                                    Assets = p.ServiceSubCategory.Name,
                                    catID = packagesBySub.catID,
                                    catsubID = packagesBySub.catsubID,
                                    servcatID = packagesBySub.servcatID,
                                    servsubcatID = servsubcatID,
                                    CategoryName = p.catID != null ? p.MainCategory.Name : null,
                                    ServiceCategoryName = p.servcatID != null ? p.ServiceCategory.Name : null,
                                    ServiceSubCategoryName = p.servsubcatID != null ? p.ServiceSubCategory.Name : null,
                                    SubCategoryName = p.catsubID != null ? p.SubCategory.Name : null
                                }).ToList();
                result.AddRange(objPricings);
            }
            return result;
        }

        public IEnumerable<GetPricingByServiceModel> GetPackagesByServicesForCarWash(int? uID, int? catID, int? cartID, int? cartsID)
        {
            List<GetPricingByServiceModel> result = new List<GetPricingByServiceModel>();
            result = UhDB.Pricings.Where(x => x.catID == catID && x.cartID == cartID && x.uID == uID
                     && x.carstID == cartsID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                    .Select(p => new GetPricingByServiceModel
                    {
                        packID = p.packID,
                        parkID = p.parkID,
                        PackageName = p.Package.Name,
                        Duration = p.Duration,
                        TimeMeasurement = p.TimeMeasurement,
                        RecursiveTime = p.Package.RecursiveTime,
                        Price = p.Price,
                        TotalPrice = p.Price,
                        ResidenceType = p.proprestID != null ? p.PropertyResidenceType.Name : null,
                        catID = catID,
                        cartID = cartID,
                        carstID = cartsID,
                        CarType = p.CarType.Name,
                        CarTypeService = p.CarServiceType.Name,
                        CategoryName = p.catID != null ? p.MainCategory.Name : null,
                        ServiceCategoryName = p.servcatID != null ? p.ServiceCategory.Name : null,
                        ServiceSubCategoryName = p.servsubcatID != null ? p.ServiceSubCategory.Name : null,
                        SubCategoryName = p.catsubID != null ? p.SubCategory.Name : null
                    }).ToList();
            return result;
        }
    }
}