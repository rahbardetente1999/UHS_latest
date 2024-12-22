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

namespace UHSForm.Areas.Customer.Controllers
{
    [Authorize]

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


        // GET: Customer/MyProfile
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

        [HttpPost]
        public ActionResult UpdateCustomerDetails(UpdateCustomerPropertyModel customerUpdate)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                customerUpdate.UpdatedBy = User.Identity.Name;//User.Identity.Name;
                customerUpdate.UpdatedOn = DateTime.Now;
                customerUpdate.UpdatedRole = objUser.rID;
                customerUpdate.ID = objUser.cuID;
                result = _objGeneralDB.UpdateCustomerDetails(customerUpdate);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateCustomerCarDetails(UpdateCustomerCarModel customerUpdate)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                customerUpdate.UpdatedBy = User.Identity.Name;//User.Identity.Name;
                customerUpdate.UpdatedOn = DateTime.Now;
                customerUpdate.UpdatedRole = objUser.rID;
                customerUpdate.ID = objUser.cuID;
                result = _objGeneralDB.UpdateCustomerCarDetails(customerUpdate);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateCustomerCarDetails(CreateCustomerCarModel customerUpdate)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                customerUpdate.CreatedBy = User.Identity.Name;//User.Identity.Name;
                customerUpdate.CreatedOn = DateTime.Now;
                customerUpdate.CreatedRole = objUser.rID;
                customerUpdate.IsActive = true;
                customerUpdate.IsDelete = false;
                customerUpdate.ID = objUser.cuID;
                result = _objGeneralDB.CreateCustomerCarDetails(customerUpdate);
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

        [HttpGet]
        public ActionResult GetUpdateCustomerDetails()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objGeneralDB.GetUpdateCustomerDetails(objUser.cuID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult GetCustomerCarModel()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objGeneralDB.GetCustomerCarModel(objUser.cuID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult UploadProfilePic()
        {
            string result = null;
            try
            {
                int fileCount = Request.Files.Count;
                if (fileCount > 0)
                {
                    HttpPostedFileBase postedFile = Request.Files[0];
                    var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                    Nullable<DateTime> cuurentDtTime = DateTime.Now.Date;
                    int? ID = objUser.cuID;
                    int? uID = objUser.uID;
                    string path = ID + "_" + objUser.Name + "_" + Path.GetFileName(postedFile.FileName);
                    using (IAmazonS3 s3client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, RegionEndpoint.USEast1))
                    {
                        PutObjectRequest putObjectRequest = new PutObjectRequest
                        {
                            BucketName = _bucketName,
                            CannedACL = S3CannedACL.PublicRead,
                            Key = string.Format("UHS/Prod/Profile/" + path),
                            InputStream = postedFile.InputStream
                        };
                        s3client.PutObject(putObjectRequest);
                    }

                    int CountProfileFile = UhDb.Files.Where(x => x.cuiD == ID && x.FileUse == 2 && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountProfileFile == 0)
                    {
                        Models.Data.File objFiles = new Models.Data.File();

                        objFiles.Filename = Path.GetFileName(postedFile.FileName);
                        objFiles.FileSize = postedFile.ContentLength.ToString();
                        objFiles.cuiD = ID;
                        objFiles.FileContentType = "Image";
                        objFiles.FileFieldName = path;
                        objFiles.FileUse = 2;
                        objFiles.IsActive = true;
                        objFiles.IsDelete = false;
                        objFiles.CreatedBy = User.Identity.Name;
                        objFiles.CreatedOn = DateTime.Now;
                        UhDb.Files.Add(objFiles);
                        UhDb.SaveChanges();
                        result = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/Profile/" + path;
                    }
                }
            }
            catch (Exception ex)
            {
                result = "Exception";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetProfilePic()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objGeneralDB.GetProfilePic(objUser.uID, objUser.userID, objUser.stfID, objUser.rID, objUser.cuID);
                //result.CName = objUser.Name;
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