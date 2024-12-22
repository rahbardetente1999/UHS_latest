using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UHSForm.DAL;
using UHSForm.Models;

namespace UHSForm.Areas.Admin.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private PropertyAreaDB _objPropertyAreaDB;
        private GeneralDB _objGeneralDB;
        private PropertyDB _objPropertyDB;
        private PropertyResidenceTypeDB _objPropertyResidenceTypeDB;
        private CommonPropertyDB _objCommonPropertyDB;
        private SubAreaDB _objSubAreaDB;

        public ProjectController()
        {
            _objPropertyAreaDB = new PropertyAreaDB();
            _objGeneralDB = new GeneralDB();
            _objPropertyDB = new PropertyDB();
            _objPropertyResidenceTypeDB = new PropertyResidenceTypeDB();
            _objCommonPropertyDB = new CommonPropertyDB();
            _objSubAreaDB = new SubAreaDB();
        }

        // GET: Admin/Project
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddProperty()
        {
            return View();
        }

        public ActionResult SubArea()
        {
            return View();
        }

        public ActionResult ResidenceType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePropertyArea(PropertyAreaModel propertyArea)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                propertyArea.IsActive = true;
                propertyArea.IsDelete = false;
                propertyArea.CreatedBy = User.Identity.Name;
                propertyArea.CreatedOn = DateTime.Now;
                propertyArea.uID = objUser.uID;
                propertyArea.suID = objUser.userID;
                propertyArea.rID = objUser.rID;
                result = _objPropertyAreaDB.CreatePropertyArea(propertyArea);

            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdatePropertyArea(UpdatePropertyAreaModel propertyArea)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                propertyArea.UpdatedBy = User.Identity.Name;
                propertyArea.UpdatedOn = DateTime.Now;
                result = _objPropertyAreaDB.UpdatePropertyArea(propertyArea);

            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeletePropertyArea(DeletePropertyAreaModel propertyArea)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                propertyArea.IsActive = false;
                propertyArea.IsDelete = true;
                propertyArea.UpdatedBy = User.Identity.Name;
                propertyArea.UpdatedOn = DateTime.Now;
                result = _objPropertyAreaDB.DeletePropertyArea(propertyArea);

            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPropertyAreas()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objPropertyAreaDB.GetPropertyAreas(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult CreateSubArea(SubAreaModel area)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                area.IsActive = true;
                area.IsDelete = false;
                area.CreatedBy = User.Identity.Name;
                area.CreatedOn = DateTime.Now;
                area.uID = objUser.uID;
                area.suID = objUser.userID;
                area.rID = objUser.rID;
                result = _objSubAreaDB.CreateSubArea(area);

            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateSubArea(UpdateSubAreaModel area)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                area.UpdatedBy = User.Identity.Name;
                area.UpdatedOn = DateTime.Now;
                result = _objSubAreaDB.UpdateSubArea(area);

            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteSubArea(DeleteSubAreaModel area)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                area.IsActive = false;
                area.IsDelete = true;
                area.UpdatedBy = User.Identity.Name;
                area.UpdatedOn = DateTime.Now;
                result = _objSubAreaDB.DeleteSubArea(area);

            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSubAreas()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objSubAreaDB.GetSubAreas(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetSubAreaDropdown()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objSubAreaDB.GetSubAreaDropdown(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetSubAreaDropdownByPropertyArea(int? propaID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objSubAreaDB.GetSubAreaDropdownByPropertyArea(objUser.uID,propaID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult GetPropertyAreaByID(int? propaID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objPropertyAreaDB.GetPropertyAreaByID(objUser.uID, propaID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPropertyAreaDropDown()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objPropertyAreaDB.GetPropertyAreaDropDown(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult CreateProperty(PropertyModel property)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                property.IsActive = true;
                property.IsDelete = false;
                property.CreatedBy = User.Identity.Name;
                property.CreatedOn = DateTime.Now;
                property.uID = objUser.uID;
                property.suID = objUser.userID;
                property.rID = objUser.rID;
                result = _objPropertyDB.CreateProperty(property);

            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateProperty(UpdatePropertyModel property)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                property.UpdatedBy = User.Identity.Name;
                property.UpdatedOn = DateTime.Now;
                result = _objPropertyDB.UpdateProperty(property);

            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteProperty(DeletePropertyModel property)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                property.IsActive = false;
                property.IsDelete = true;
                property.UpdatedBy = User.Identity.Name;
                property.UpdatedOn = DateTime.Now;
                result = _objPropertyDB.DeleteProperty(property);

            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetProperty()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objPropertyDB.GetProperty(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPropertyByID(int? vID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objPropertyDB.GetPropertyByID(objUser.uID, vID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPropertyDropDown()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objPropertyDB.GetPropertyDropDown(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult CreatePropertyResidenceType(PropertyResidenceTypeModel property)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                property.IsActive = true;
                property.IsDelete = false;
                property.CreatedBy = User.Identity.Name;
                property.CreatedOn = DateTime.Now;
                property.uID = objUser.uID;
                property.suID = objUser.userID;
                property.rID = objUser.rID;
                result = _objPropertyResidenceTypeDB.CreatePropertyResidenceType(property);

            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdatePropertyResidenceType(UpdatePropertyResidenceTypeModel property)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                property.UpdatedBy = User.Identity.Name;
                property.UpdatedOn = DateTime.Now;
                result = _objPropertyResidenceTypeDB.UpdatePropertyResidenceType(property);

            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeletePropertyResidenceType(DeletePropertyResidenceTypeModel property)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                property.IsActive = false;
                property.IsDelete = true;
                property.UpdatedBy = User.Identity.Name;
                property.UpdatedOn = DateTime.Now;
                result = _objPropertyResidenceTypeDB.DeletePropertyResidenceType(property);

            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPropertyResidenceType()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objPropertyResidenceTypeDB.GetPropertyResidenceType(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPropertyResidenceTypeByID(int? proprestID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objPropertyResidenceTypeDB.GetPropertyResidenceTypeByID(objUser.uID, proprestID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPropertyResidenceTypeDropDown()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objPropertyResidenceTypeDB.GetPropertyResidenceTypeDropDown(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult GetPropertyResidenceTypeByVIDDropDown()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonPropertyDB.GetPropertyResidenceTypeByVIDDropDown(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPropertyByAreaDropDown(int? propaID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonPropertyDB.GetPropertyByAreaDropDown(objUser.uID, propaID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPropertyDropDownByAreasID(int? propaID, int? subAreaID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonPropertyDB.GetPropertyDropDownByAreasID(objUser.uID, propaID,subAreaID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

    }
}