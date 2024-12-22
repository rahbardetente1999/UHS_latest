using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UHSForm.Models;
using UHSForm.DAL;

namespace UHSForm.Areas.Admin.Controllers
{
    [Authorize]
    public class PackagesController : Controller
    {
        private PricingDB _objPricingDB;
        private PackagesDB _objPackagesDB;
        private GeneralDB _objGeneralDB;
        private CarServicesTypeDB _objCarServicesTypeDB;
        private CommonPackagesDB _objCommonPackagesDB;
        private CarTypesDB _objCarTypesDB;
        public PackagesController()
        {
            _objPricingDB = new PricingDB();
            _objPackagesDB = new PackagesDB();
            _objGeneralDB = new GeneralDB();
            _objCarServicesTypeDB = new CarServicesTypeDB();
            _objCarTypesDB = new CarTypesDB();
            _objCommonPackagesDB = new CommonPackagesDB();

        }

        // GET: Admin/Packages
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pricing()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePackages(PackagesModel package)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                package.uID = objUser.uID;
                package.suID = objUser.userID;
                package.rID = objUser.rID;
                package.IsActive = true;
                package.IsDelete = false;
                package.CreatedBy = User.Identity.Name;
                package.CreatedOn = DateTime.Now;
                result = _objPackagesDB.CreatePackages(package);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdatePackages(UpdatePackagesModel package)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                package.UpdatedBy = User.Identity.Name;
                package.UpdatedOn = DateTime.Now;
                result = _objPackagesDB.UpdatePackages(package);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeletePackages(DeletePackagesModel package)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                package.IsActive = false;
                package.IsDelete = true;
                package.UpdatedBy = User.Identity.Name;
                package.UpdatedOn = DateTime.Now;
                result = _objPackagesDB.DeletePackages(package);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPackages()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objPackagesDB.GetPackages(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPackagesByID(int? packID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objPackagesDB.GetPackagesByID(objUser.uID, packID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPackagesDropDown()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objPackagesDB.GetPackagesDropDown(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult CreatePricing(PricingModel pricing)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                pricing.uID = objUser.uID;
                pricing.suID = objUser.userID;
                pricing.rID = objUser.rID;
                pricing.IsActive = true;
                pricing.IsDelete = false;
                pricing.CreatedBy = User.Identity.Name;
                pricing.CreatedOn = DateTime.Now;
                result = _objPricingDB.CreatePricing(pricing);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdatePricingModel(UpdatePricingModel pricing)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                pricing.UpdatedBy = User.Identity.Name;
                pricing.UpdatedOn = DateTime.Now;
                result = _objPricingDB.UpdatePricing(pricing);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeletePricing(DeletePricingModel pricing)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                pricing.IsActive = false;
                pricing.IsDelete = true;
                pricing.UpdatedBy = User.Identity.Name;
                pricing.UpdatedOn = DateTime.Now;
                result = _objPricingDB.DeletePricing(pricing);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPricing()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objPricingDB.GetPricing(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPricingByID(int? parkID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objPricingDB.GetPricingByID(objUser.uID, parkID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPricingDropDown()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objPricingDB.GetPricingDropDown(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCarTypesDropdown()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCarTypesDB.GetCarTypesDropdown(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCarServicesTypeDropdown()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCarServicesTypeDB.GetCarServicesTypeDropdown(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPackagesByServices(int? catID, int? catsubID, int? propaID, int? vID, int? proprestID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonPackagesDB.GetPackagesByServices(objUser.uID, catID, catsubID, propaID, vID, proprestID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPackagesByServicesWithOutProperty(int? catID, int? catsubID, int? proprestID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonPackagesDB.GetPackagesByServicesWithOutProperty(objUser.uID, catID, catsubID, proprestID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

    }
}