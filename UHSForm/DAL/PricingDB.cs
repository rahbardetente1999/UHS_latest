using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models;
using UHSForm.Models.Data;

namespace UHSForm.DAL
{
    public class PricingDB
    {
        private UHSEntities UhDB;

        public PricingDB()
        {
            UhDB = new UHSEntities();
        }


        public string CreatePricing(PricingModel pricing)
        {
            string result = null;
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    Pricing objPricing = new Pricing();
                    objPricing.catID = pricing.catID;
                    objPricing.catsubID = pricing.catsubID;
                    objPricing.servcatID = pricing.servcatID;
                    objPricing.servsubcatID = pricing.servsubcatID;
                    objPricing.propaID = pricing.propaID;
                    objPricing.vID = pricing.vID;
                    objPricing.proprestID = pricing.proprestID;
                    objPricing.packID = pricing.packID;
                    objPricing.Price = pricing.Price;
                    objPricing.Duration = pricing.Duration;
                    objPricing.TimeMeasurement = pricing.TimeMeasurement;
                    objPricing.carstID = pricing.carstID;
                    objPricing.cartID = pricing.cartID;
                    objPricing.uID = pricing.uID;
                    if (pricing.rID != 10)
                    {
                        objPricing.suID = pricing.suID;
                    }

                    objPricing.IsActive = pricing.IsActive;
                    objPricing.IsDelete = pricing.IsDelete;
                    objPricing.CreatedBy = pricing.CreatedBy;
                    objPricing.CreatedOn = pricing.CreatedOn;
                    UhDB.Pricings.Add(objPricing);
                    Save();

                    var objPackages = UhDB.Packages.Where(x => x.packID == pricing.packID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objPackages.Status = true;
                    objPackages.UpdatedBy = pricing.CreatedBy;
                    objPackages.UpdatedOn = pricing.CreatedOn;
                    Save();

                    trans.Commit();
                    result = "SUCCESS";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    result = "Exception";

                }
            }
            return result;
        }

        public IEnumerable<GetPricingModel> GetPricing(int? uID)
        {
            List<GetPricingModel> result = new List<GetPricingModel>();

            result = UhDB.Pricings.Where(x => x.MainCategory.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                      .Select(p => new Models.GetPricingModel
                      {
                          SubServiceCategoryName = p.servsubcatID != null ? p.ServiceSubCategory.Name : "N/A",
                          MainCategoryName = p.catID != null ? p.MainCategory.Name : "N/A",
                          SubCategoryName = p.catsubID != null ? p.SubCategory.Name : "N/A",
                          ServiceCategoryName = p.servcatID != null ? p.ServiceCategory.Name : "N/A",
                          PackageName = p.packID != null ? p.Package.Name : "N/A",
                          PropertyArea = p.propaID != null ? p.PropertyArea.Name : "N/A",
                          PropertyName = p.vID != null ? p.Venture.Name : "N/A",
                          PropertyResidenceType = p.proprestID != null ? p.PropertyResidenceType.Name : "N/A",
                          CarType = p.cartID != null ? p.CarType.Name : "N/A",
                          CarTypeService = p.carstID != null ? p.CarServiceType.Name : "N/A",
                          CreatedBy = p.CreatedBy,
                          CreatedOn = p.CreatedOn,
                          catID = p.catID,
                          catsubID = p.catsubID,
                          servcatID = p.servcatID,
                          servsubcatID = p.servsubcatID,
                          packID = p.packID,
                          propaID = p.propaID,
                          proprestID = p.proprestID,
                          vID = p.vID,
                          parkID = p.parkID,
                          carstID = p.carstID,
                          cartID = p.cartID,
                          Duration = p.Duration,
                          Price = p.Price,
                          TimeMeasurement = p.TimeMeasurement
                      }).ToList();

            return result;
        }

        public GetPricingModel GetPricingByID(int? uID, int? parkID)
        {
            GetPricingModel result = new GetPricingModel();

            result = UhDB.Pricings.Where(x => x.MainCategory.uID == uID && x.parkID == parkID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                      .Select(p => new Models.GetPricingModel
                      {
                          SubServiceCategoryName = p.servsubcatID != null ? p.ServiceSubCategory.Name : "N/A",
                          MainCategoryName = p.catID != null ? p.MainCategory.Name : "N/A",
                          SubCategoryName = p.catsubID != null ? p.SubCategory.Name : "N/A",
                          ServiceCategoryName = p.servcatID != null ? p.ServiceCategory.Name : "N/A",
                          PackageName = p.packID != null ? p.Package.Name : "N/A",
                          PropertyArea = p.propaID != null ? p.PropertyArea.Name : "N/A",
                          PropertyName = p.vID != null ? p.Venture.Name : "N/A",
                          PropertyResidenceType = p.proprestID != null ? p.PropertyResidenceType.Name : "N/A",
                          CarType = p.cartID != null ? p.CarType.Name : "N/A",
                          CarTypeService = p.carstID != null ? p.CarServiceType.Name : "N/A",
                          CreatedBy = p.CreatedBy,
                          CreatedOn = p.CreatedOn,
                          catID = p.catID,
                          catsubID = p.catsubID,
                          servcatID = p.servcatID,
                          servsubcatID = p.servsubcatID,
                          packID = p.packID,
                          propaID = p.propaID,
                          proprestID = p.proprestID,
                          vID = p.vID,
                          parkID = p.parkID,
                          Duration = p.Duration,
                          Price = p.Price,
                          TimeMeasurement = p.TimeMeasurement,
                          carstID = p.carstID,
                          cartID = p.cartID
                      }).FirstOrDefault();

            return result;
        }

        public IEnumerable<GetPricingDropDown> GetPricingDropDown(int? uID)
        {
            List<GetPricingDropDown> result = new List<GetPricingDropDown>();
            result = UhDB.Pricings.Where(x => x.MainCategory.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select(p => new GetPricingDropDown { ID = p.parkID, Price = p.Price.ToString(), Duration = Convert.ToDateTime(p.Duration).ToString("MM/dd/yyyy") }).ToList();
            return result;
        }

        public string UpdatePricing(UpdatePricingModel pricing)
        {
            string result = null;
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    var objPricings = UhDB.Pricings.Where(x => x.parkID == pricing.parkID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objPricings.catID = pricing.catID;
                    objPricings.catsubID = pricing.catsubID;
                    objPricings.servcatID = pricing.servcatID;
                    objPricings.servsubcatID = pricing.servsubcatID;
                    objPricings.propaID = pricing.propaID;
                    objPricings.vID = pricing.vID;
                    objPricings.proprestID = pricing.proprestID;
                    objPricings.packID = pricing.packID;
                    objPricings.Price = pricing.Price;
                    objPricings.Duration = pricing.Duration;
                    objPricings.TimeMeasurement = pricing.TimeMeasurement;
                    objPricings.carstID = pricing.carstID;
                    objPricings.cartID = pricing.cartID;
                    objPricings.UpdatedBy = pricing.UpdatedBy;
                    objPricings.UpdatedOn = pricing.UpdatedOn;
                    Save();

                    var objPackages = UhDB.Packages.Where(x => x.packID == pricing.packID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objPackages.Status = true;
                    objPackages.UpdatedBy = pricing.UpdatedBy;
                    objPackages.UpdatedOn = pricing.UpdatedOn;
                    Save();

                    trans.Commit();
                    result = "SUCCESS";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    result = "Exception";
                }
            }
            return result;

        }

        public string DeletePricing(DeletePricingModel pricing)
        {
            string result = null;

            var objPricings = UhDB.Pricings.Where(x => x.parkID == pricing.parkID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            if (objPricings.Status == true)
            {
                result = "Can't";
            }
            else
            {
                var objDeletePricings = UhDB.Pricings.Where(x => x.parkID == pricing.parkID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                objDeletePricings.IsActive = pricing.IsActive;
                objDeletePricings.IsDelete = pricing.IsDelete;
                objDeletePricings.UpdatedBy = pricing.UpdatedBy;
                objDeletePricings.UpdatedOn = pricing.UpdatedOn;
                Save();
                result = "SUCCESS";
            }

            return result;

        }


        private void Save()
        {
            UhDB.SaveChanges();
        }

    }
}