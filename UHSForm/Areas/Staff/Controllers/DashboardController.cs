using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UHSForm.DAL;
using UHSForm.Models;
using UHSForm.Models.Data;

namespace UHSForm.Areas.Staff.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private static readonly string _awsAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        private static readonly string _awsSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
        private static readonly string _bucketName = "urbanhospitalityserv";

        private CommonStaffDB _objCommonStaffDB;
        private GeneralDB _objGeneralDB;
        private CustomerDB _objCustomerDB;
        private UHSEntities UhDB;
        private StaffRatingDB _objStaffRatingDB;
        private DashboardDB _objDashboardDB;
        private PropertyAreaDB _objPropertyAreaDB;
        private CommonPropertyDB _objCommonPropertyDB;
        private SubAreaDB _objSubAreaDB;

        public DashboardController()
        {
            _objCommonStaffDB = new CommonStaffDB();
            _objGeneralDB = new GeneralDB();
            _objCustomerDB = new CustomerDB();
            _objStaffRatingDB = new StaffRatingDB();
            _objDashboardDB = new DashboardDB();
            _objPropertyAreaDB = new PropertyAreaDB();
            _objCommonPropertyDB = new CommonPropertyDB();
            _objSubAreaDB = new SubAreaDB();
            UhDB = new UHSEntities();
        }

        // GET: Staff/Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetTodaysCustomers()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonStaffDB.GetTodaysCustomers(objUser.stfID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetTodaysCompletedTasksForStaff()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonStaffDB.GetTodaysCompletedTasksForStaff(objUser.stfID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetTodaysPendingTasksForStaff()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonStaffDB.GetTodaysPendingTasksForStaff(objUser.stfID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetTotalTaskForStaff()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonStaffDB.GetTotalTaskForStaff(objUser.stfID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCompletedTasksForStaff()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonStaffDB.GetCompletedTasksForStaff(objUser.stfID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetPedingTasksForStaff(DateTime SearchDate)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonStaffDB.GetPedingTasksForStaff(objUser.stfID,SearchDate);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetDashboardMonthsDropdown()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonStaffDB.GetDashboardMonthsDropdown(objUser.stfID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetDashboardDatesDropdown(string MonthName)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonStaffDB.GetDashboardDatesDropdown(objUser.stfID,MonthName);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetStaffDashboardCount()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objDashboardDB.GetStaffDashboardCount(objUser.uID, objUser.stfID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetStaffDashboardMonthlyCount()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objDashboardDB.GetStaffDashboardMonthlyCount(objUser.uID, objUser.stfID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetTodaysCustomersForDashboard(int? catID, int? catsubID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objDashboardDB.GetTodaysCustomersForDashboard(objUser.uID, catID, catsubID, objUser.stfID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetSpecialServiceTodayCustomersForDashboard(int? catID, int? catsubID, int? servcatID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objDashboardDB.GetSpecialServiceTodayCustomersForDashboard(objUser.uID, catID, catsubID, servcatID, objUser.stfID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetMonthlyCustomersForDashboard(int? catID, int? catsubID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objDashboardDB.GetMonthlyCustomersForDashboard(objUser.uID, catID, catsubID, objUser.stfID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetSpecialServiceMonthlyCustomersForDashboard(int? catID, int? catsubID, int? servcatID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objDashboardDB.GetSpecialServiceMonthlyCustomersForDashboard(objUser.uID, catID, catsubID, servcatID, objUser.stfID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ContentResult CloseTask(ClosedTaskModel task, List<HttpPostedFileBase> files)
        {
            string result = null;
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                    task.UpdatedBy = User.Identity.Name;
                    task.UpdatedOn = DateTime.Now;
                    task.UpdatedRole = objUser.rID;
                    var res = _objCustomerDB.ClosedTask(task);
                    int FilesCount = files.Count();
                    if (FilesCount != 0 && res == "SUCCESS")
                    {
                        foreach (var key in files)
                        {
                            // HttpPostedFileBase postedFile = Request.Files[key];
                            string path = task.cuID.ToString() + "_" + task.catID + "_" +
                                          task.catsubID.ToString() + "_" + task.CustomerID
                                          + task.TaskNo + "_" + key.FileName;
                            string _saveKey = "UHS/Prod/CustomerTaskClosed/";
                            //postedFile.SaveAs(Server.MapPath("~/Images/Properties/" + scopeIdentity.ToString() + "_" + property.PlotNo + "/" + Path.GetFileName(postedFile.FileName)));

                            using (IAmazonS3 s3client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, RegionEndpoint.USEast1))
                            {
                                PutObjectRequest putObjectRequest = new PutObjectRequest
                                {
                                    BucketName = _bucketName,
                                    CannedACL = S3CannedACL.PublicRead,
                                    Key = string.Format(_saveKey + path),
                                    InputStream = key.InputStream
                                };
                                s3client.PutObject(putObjectRequest);
                            }

                            Models.Data.File imageFiles = new Models.Data.File();
                            imageFiles.Filename = key.FileName;
                            imageFiles.FileSize = key.ContentLength.ToString();
                            imageFiles.uID = objUser.uID;
                            imageFiles.cuiD = task.cuID;
                            imageFiles.custODID = task.custODID;
                            imageFiles.FileContentType = key.ContentType;
                            imageFiles.FileFieldName = path;
                            imageFiles.FileUse = 9;
                            imageFiles.IsActive = true;
                            imageFiles.IsDelete = false;
                            imageFiles.CreatedBy = User.Identity.Name;//User.Identity.Name
                            imageFiles.CreatedOn = DateTime.Now;
                            UhDB.Files.Add(imageFiles);
                            UhDB.SaveChanges();

                        }
                        res = "SUCCESS";
                    }
                    if (res == "SUCCESS")
                    {
                        trans.Commit();
                        result = "SUCCESS";
                    }
                    else
                    {
                        trans.Rollback();
                        result = "Exception";
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    result = "Exception";
                }
            }
            return Content(result);
        }

        [HttpPost]
        public ActionResult CreateStaffCustomerRating(StaffCustomerRatingModel staff)
        {
            string result = null;
            using (var trans = UhDB.Database.BeginTransaction()) 
            {
                try
                {
                    var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                    staff.stfID = objUser.stfID;
                    staff.CreatedBy = User.Identity.Name;
                    staff.CreatedOn = DateTime.Now;
                    staff.CreatedRole = objUser.rID;
                    staff.IsActive = true;
                    staff.IsDelete = false;
                    int? custSTFRID = _objStaffRatingDB.CreateStaffCustomerRating(staff);
                    int? FileCount = Request.Files.Count;
                    if (custSTFRID != null && custSTFRID != 0 && custSTFRID != -1 && FileCount != 0)
                    {
                        foreach (string key in Request.Files)
                        {
                            HttpPostedFileBase postedFile = Request.Files[key];
                            string path = staff.cuID.ToString() + "_" + custSTFRID + "_" + postedFile.FileName;
                            string _saveKey = "UHS/Prod/StaffComplaint/";
                            //postedFile.SaveAs(Server.MapPath("~/Images/Properties/" + scopeIdentity.ToString() + "_" + property.PlotNo + "/" + Path.GetFileName(postedFile.FileName)));

                            using (IAmazonS3 s3client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, RegionEndpoint.USEast1))
                            {
                                PutObjectRequest putObjectRequest = new PutObjectRequest
                                {
                                    BucketName = _bucketName,
                                    CannedACL = S3CannedACL.PublicRead,
                                    Key = string.Format(_saveKey + path),
                                    InputStream = postedFile.InputStream
                                };
                                s3client.PutObject(putObjectRequest);
                            }

                            Models.Data.File imageFiles = new Models.Data.File();
                            imageFiles.Filename = Path.GetFileName(postedFile.FileName);
                            imageFiles.FileSize = postedFile.ContentLength.ToString();
                            imageFiles.uID = objUser.uID;
                            imageFiles.cuiD = staff.cuID;
                            imageFiles.FileContentType = postedFile.ContentType;
                            imageFiles.FileFieldName = path;
                            imageFiles.FileUse = 11;
                            imageFiles.IsActive = true;
                            imageFiles.IsDelete = false;
                            imageFiles.CreatedBy = User.Identity.Name;//User.Identity.Name
                            imageFiles.CreatedOn = DateTime.Now;
                            UhDB.Files.Add(imageFiles);
                            UhDB.SaveChanges();

                        }
                    }
                    
                    if (custSTFRID == -1)
                    {
                        trans.Rollback();
                        result = "AlreadyIssue";
                    }
                    else
                    {
                        trans.Commit();
                        result = "SUCCESS";
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    result = "Exception";
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetStaffCustomerRating(int? custID, int? custODID,int? custTDID)
        {

            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objStaffRatingDB.GetStaffCustomerRating(custID, custODID,custTDID,objUser.stfID);
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

        [HttpGet]
        public ActionResult GetStaffAverageRating()
        {

            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonStaffDB.GetStaffAverageRating(objUser.stfID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult GetTotalService()
        {

            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonStaffDB.GetTotalService(objUser.stfID);
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

        [HttpGet]
        public ActionResult GetSubAreaDropdownByPropertyArea(int? propaID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objSubAreaDB.GetSubAreaDropdownByPropertyArea(objUser.uID, propaID);
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
                var result = _objCommonPropertyDB.GetPropertyDropDownByAreasID(objUser.uID, propaID, subAreaID);
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
                var result = _objCommonPropertyDB.GetPropertyResidenceTypeByVIDDropDown(objUser.uID);
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