using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UHSForm.DAL;

namespace UHSForm.Areas.Admin.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private GeneralDB _objGeneralDB;
        private DashboardDB _objDashboardDB;

        public DashboardController()
        {
            _objDashboardDB = new DashboardDB();
            _objGeneralDB = new GeneralDB();
        }
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetDashboardCount()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objDashboardDB.GetDashboardCount(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetDashboardCarWashCount()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objDashboardDB.GetDashboardCarWashCount(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCustomersSpecialServiceForDashboard(int? catID, int? catsubID, int? servcatID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objDashboardDB.GetCustomersSpecialServiceForDashboard(objUser.uID, catID, catsubID, servcatID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCustomersForDashboard(int? catID, int? catsubID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objDashboardDB.GetCustomersForDashboard(objUser.uID, catID, catsubID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult LogOut()
        {
            string result = null;
            try
            {
                FormsAuthentication.SignOut();
                result = "SUCCESS";

            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}