using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using UHSForm.Models;
using UHSForm.Models.Data;
using UHSForm.DAL;
using System.IO;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon;
using System.Web.Security;
using System.Web.Mvc;
using System.Web;

namespace UHSForm.Areas.Operation.Controllers
{
    public class MyProfileController : Controller
    {
        private static readonly string _awsAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        private static readonly string _awsSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
        private static readonly string _bucketName = "urbanhospitalityserv";

        private GeneralDB _objGeneralDB;
        private UHSEntities UhDb;

        public MyProfileController()
        {
            _objGeneralDB = new GeneralDB();
            UhDb = new UHSEntities();
        }
        // GET: Operation/MyProfile
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword password)
        {
            string result = null;
            try
            {
                password.Username = User.Identity.Name;//User.Identity.Name;
                password.CreatedBy = User.Identity.Name;//User.Identity.Name;
                password.CreatedOn = DateTime.Now;
                result = _objGeneralDB.ChangePassword(password);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateUserDetails(UpdateUserModel user)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                user.UpdatedBy = User.Identity.Name;//User.Identity.Name;
                user.UpdatedOn = DateTime.Now;
                user.UpdatedRole = objUser.rID;
                user.rID = objUser.rID;
                result = _objGeneralDB.UpdateUserDetails(user);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetUpdateUserDetails()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objGeneralDB.GetUpdateUserDetails(null, null, null, objUser.rID, objUser.cuID);
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