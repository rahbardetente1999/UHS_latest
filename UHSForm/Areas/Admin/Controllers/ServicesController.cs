using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UHSForm.Models.Data;
using UHSForm.Models;
using UHSForm.DAL;
using Amazon.S3;
using Amazon.S3.Model;
using System.Configuration;
using System.IO;
using Amazon;

namespace UHSForm.Areas.Admin.Controllers
{
    [Authorize]
    public class ServicesController : Controller
    {
        private static readonly string _awsAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        private static readonly string _awsSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
        private static readonly string _bucketName = "urbanhospitalityserv";


        private GeneralDB _objGeneralDB;
        private IncExcluDB _objIncExcluDB;
        private MainCategoryDB _objMainCategoryDB;
        private SubCategoryDB _objSubCategory;
        private ServiceCategoryDB _objServiceCategoryDB;
        private SubServiceCategoryDB _objSubServiceCategoryDB;
        private CommonServiceDB _objCommonServiceDB;
        private StaffServiceDB _objStaffServiceDB;
        private UHSEntities UhDb;

        public ServicesController()
        {
            _objGeneralDB = new GeneralDB();
            _objIncExcluDB = new IncExcluDB();
            _objMainCategoryDB = new MainCategoryDB();
            _objSubCategory = new SubCategoryDB();
            _objServiceCategoryDB = new ServiceCategoryDB();
            _objSubServiceCategoryDB = new SubServiceCategoryDB();
            _objCommonServiceDB = new CommonServiceDB();
            _objStaffServiceDB = new StaffServiceDB();
            UhDb = new UHSEntities();
        }

        // GET: Admin/Services
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SubCategory()
        {
            return View();
        }

        public ActionResult ServiceCategory()
        {
            return View();
        }

        public ActionResult ServiceCategoryOption()
        {
            return View();
        }

        public ActionResult ServiceAssign()
        {
            return View();
        }

        public ActionResult InclExclusion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatIncExc(IncExcluModel incExclu)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                incExclu.IsActive = true;
                incExclu.IsDelete = false;
                incExclu.uID = objUser.uID;
                incExclu.suID = objUser.userID;
                incExclu.rID = objUser.rID;
                incExclu.CreatedBy = User.Identity.Name;
                incExclu.CreatedOn = DateTime.Now;
                result = _objIncExcluDB.CreatIncExc(incExclu);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateRefIncExc(CreateIncExcluTypeModel incExcluType)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                incExcluType.IsActive = true;
                incExcluType.IsDelete = false;
                incExcluType.CreatedBy = User.Identity.Name;
                incExcluType.CreatedOn = DateTime.Now;
                result = _objIncExcluDB.CreateRefIncExc(incExcluType);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateRefIncExc(UpdateIncExcluTypeModel incExcluType)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                incExcluType.UpdatedBy = User.Identity.Name;
                incExcluType.UpdatedOn = DateTime.Now;
                result = _objIncExcluDB.UpdateRefIncExc(incExcluType);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteRefIncExc(DeleteIncExcluTypeModel incExcluType)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                incExcluType.IsActive = false;
                incExcluType.IsDelete = true;
                incExcluType.UpdatedBy = User.Identity.Name;
                incExcluType.UpdatedOn = DateTime.Now;
                result = _objIncExcluDB.DeleteRefIncExc(incExcluType);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetIncExclus()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objIncExcluDB.GetIncExclus(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetIncExcluType(int? incexID, int? Type)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objIncExcluDB.GetIncExcluType(incexID, Type);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ContentResult CreateMainCategory(MainCategoryModel category)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                category.IsActive = true;
                category.IsDelete = false;
                category.uID = objUser.uID;
                category.suID = objUser.userID;
                category.CreatedRole = objUser.rID;
                category.CreatedBy = User.Identity.Name;
                category.CreatedOn = DateTime.Now;
                int catID = (int)_objMainCategoryDB.CreateMainCategory(category);
                int FilesCount = Request.Files.Count;
                if (FilesCount != 0 && catID != 0)
                {
                    foreach (string key in Request.Files)
                    {
                        HttpPostedFileBase postedFile = Request.Files[key];
                        string path = catID.ToString() + "_" + category.Name + "_" + postedFile.FileName;
                        string _saveKey = "UHS/Prod/MainCategory/";
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
                        imageFiles.catID = catID;
                        imageFiles.FileContentType = postedFile.ContentType;
                        imageFiles.FileFieldName = path;
                        imageFiles.FileUse = 1;
                        imageFiles.IsActive = true;
                        imageFiles.IsDelete = false;
                        imageFiles.CreatedBy = User.Identity.Name;//User.Identity.Name
                        imageFiles.CreatedOn = DateTime.Now;
                        UhDb.Files.Add(imageFiles);
                        UhDb.SaveChanges();

                    }
                }
                result = "SUCCESS";
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Content(result);
        }

        [HttpPost]
        public ActionResult UpdateMainCategory(UpdateMainCategoryModel category)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                category.UpdatedBy = User.Identity.Name;
                category.UpdatedOn = DateTime.Now;
                category.UpdatedRole = objUser.rID;
                result = _objMainCategoryDB.UpdateMainCategory(category);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMainCategory(DeleteMainCategoryModel category)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                category.IsActive = false;
                category.IsDelete = true;
                category.UpdatedBy = User.Identity.Name;
                category.UpdatedOn = DateTime.Now;
                category.UpdatedRole = objUser.rID;
                result = _objMainCategoryDB.DeleteMainCategory(category);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateMainCategoryFlag(UpdateMainCategoryFlagModel category)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                category.UpdatedBy = User.Identity.Name;
                category.UpdatedOn = DateTime.Now;
                category.UpdatedRole = objUser.rID;
                result = _objMainCategoryDB.UpdateMainCategoryFlag(category);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetMainCategories()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objMainCategoryDB.GetMainCategories(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetMainCategoryByID(int? catID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objMainCategoryDB.GetMainCategoryByID(objUser.uID, catID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetMainCategoryDropDown()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objMainCategoryDB.GetMainCategoryDropDown(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetMainCategoryImgDropdown()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objMainCategoryDB.GetMainCategoryDropdown(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ContentResult CreateSubCategory(SubCategoryModel category)
        {
            string result = null;
            using (var trans = UhDb.Database.BeginTransaction()) 
            {
                try
                {
                    var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                    category.IsActive = true;
                    category.IsDelete = false;
                    category.uID = objUser.uID;
                    category.suID = objUser.userID;
                    category.CreatedRole = objUser.rID;
                    category.CreatedBy = User.Identity.Name;
                    category.CreatedOn = DateTime.Now;
                    int catsubID = (int)_objSubCategory.CreateSubCategory(category);
                    int FilesCount = Request.Files.Count;
                    if (FilesCount != 0 && catsubID != 0)
                    {
                        foreach (string key in Request.Files)
                        {
                            HttpPostedFileBase postedFile = Request.Files[key];
                            string path = category.catID + "_" + catsubID.ToString() + "_" + category.Name + "_" + postedFile.FileName;
                            string _saveKey = "UHS/Prod/SubCategory/";
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
                            imageFiles.catsubID = catsubID;
                            imageFiles.FileContentType = postedFile.ContentType;
                            imageFiles.FileFieldName = path;
                            imageFiles.FileUse = 4;
                            imageFiles.IsActive = true;
                            imageFiles.IsDelete = false;
                            imageFiles.CreatedBy = User.Identity.Name;//User.Identity.Name
                            imageFiles.CreatedOn = DateTime.Now;
                            UhDb.Files.Add(imageFiles);
                            UhDb.SaveChanges();

                        }
                    }
                    trans.Commit();
                    result = "SUCCESS";
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
        public ActionResult UpdateSubCategory(UpdateSubCategoryModel category)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                category.UpdatedBy = User.Identity.Name;
                category.UpdatedOn = DateTime.Now;
                category.UpdatedRole = objUser.rID;
                result = _objSubCategory.UpdateSubCategory(category);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteSubCategory(DeleteSubCategoryModel category)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                category.IsActive = false;
                category.IsDelete = true;
                category.UpdatedBy = User.Identity.Name;
                category.UpdatedOn = DateTime.Now;
                category.UpdatedRole = objUser.rID;
                result = _objSubCategory.DeleteSubCategory(category);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSubCategories()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objSubCategory.GetSubCategories(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetSubCategoryByID(int? catsubID) 
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objSubCategory.GetSubCategoryByID(objUser.uID,catsubID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetSubCategoryDropDown() 
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objSubCategory.GetSubCategoryDropDown(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ContentResult CreateServiceCategory(ServiceCategoryModel category)
        {
            string result = null;
            using (var trans = UhDb.Database.BeginTransaction()) 
            {
                try
                {
                    var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                    category.IsActive = true;
                    category.IsDelete = false;
                    category.uID = objUser.uID;
                    category.suID = objUser.userID;
                    category.CreatedRole = objUser.rID;
                    category.CreatedBy = User.Identity.Name;
                    category.CreatedOn = DateTime.Now;
                    int servcatID = (int)_objServiceCategoryDB.CreateServiceCategory(category);
                    int FilesCount = Request.Files.Count;
                    if (FilesCount != 0 && servcatID != 0)
                    {
                        foreach (string key in Request.Files)
                        {
                            HttpPostedFileBase postedFile = Request.Files[key];
                            var objServiceCategory = UhDb.ServiceCategories.Where(x => x.servcatID == servcatID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                            int? catID = objServiceCategory.catID;
                            int? catsubID = objServiceCategory.catsubID;
                            string path = catID + "_" + catsubID.ToString() + "_" + servcatID + "_" + category.Name + "_" + postedFile.FileName;
                            string _saveKey = "UHS/Prod/ServiceCategory/";
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
                            imageFiles.servcatID = servcatID;
                            imageFiles.FileContentType = postedFile.ContentType;
                            imageFiles.FileFieldName = path;
                            imageFiles.FileUse = 3;
                            imageFiles.IsActive = true;
                            imageFiles.IsDelete = false;
                            imageFiles.CreatedBy = User.Identity.Name;//User.Identity.Name
                            imageFiles.CreatedOn = DateTime.Now;
                            UhDb.Files.Add(imageFiles);
                            UhDb.SaveChanges();

                        }
                    }
                    trans.Commit();
                    result = "SUCCESS";
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
        public ActionResult UpdateServiceCategory(UpdateServiceCategoryModel category)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                category.UpdatedBy = User.Identity.Name;
                category.UpdatedOn = DateTime.Now;
                category.UpdatedRole = objUser.rID;
                result = _objServiceCategoryDB.UpdateServiceCategory(category);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteServiceCategory(DeleteServiceCategoryModel category)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                category.IsActive = false;
                category.IsDelete = true;
                category.UpdatedBy = User.Identity.Name;
                category.UpdatedOn = DateTime.Now;
                category.UpdatedRole = objUser.rID;
                result = _objServiceCategoryDB.DeleteServiceCategory(category);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetServiceCategories()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objServiceCategoryDB.GetServiceCategories(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetServiceCategoryByID(int? servcatID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objServiceCategoryDB.GetServiceCategoryByID(objUser.uID,servcatID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetGetServiceCategoryDroDown()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objServiceCategoryDB.GetGetServiceCategoryDroDown(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ContentResult CreateServiceSubCategory(SubServiceCategoryModel category)
        {
            string result = null;
            using (var trans = UhDb.Database.BeginTransaction()) 
            {
                try
                {
                    var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                    category.IsActive = true;
                    category.IsDelete = false;
                    category.uID = objUser.uID;
                    category.suID = objUser.userID;
                    category.CreatedRole = objUser.rID;
                    category.CreatedBy = User.Identity.Name;
                    category.CreatedOn = DateTime.Now;
                    int servsubcatID = (int)_objSubServiceCategoryDB.CreateServiceSubCategory(category);
                    int FilesCount = Request.Files.Count;
                    if (FilesCount != 0 && servsubcatID != 0)
                    {
                        foreach (string key in Request.Files)
                        {
                            HttpPostedFileBase postedFile = Request.Files[key];
                            var objServiceCategory = UhDb.ServiceCategories.Where(x => x.servcatID == category.servcatID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                            int? catID = objServiceCategory.catID;
                            int? catsubID = objServiceCategory.catsubID;
                            string path = catID + "_" + catsubID.ToString() + "_" + category.servcatID + "_" + servsubcatID + "_" + category.Name + "_" + postedFile.FileName;
                            string _saveKey = "UHS/Prod/SubServiceCategory/";
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
                            imageFiles.servsubcatID = servsubcatID;
                            imageFiles.FileContentType = postedFile.ContentType;
                            imageFiles.FileFieldName = path;
                            imageFiles.FileUse = 5;
                            imageFiles.IsActive = true;
                            imageFiles.IsDelete = false;
                            imageFiles.CreatedBy = User.Identity.Name;//User.Identity.Name
                            imageFiles.CreatedOn = DateTime.Now;
                            UhDb.Files.Add(imageFiles);
                            UhDb.SaveChanges();

                        }
                    }
                    trans.Commit();
                    result = "SUCCESS";
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
        public ActionResult UpdateSubServiceCategory(UpdateSubServiceCategoryModel category)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                category.UpdatedBy = User.Identity.Name;
                category.UpdatedOn = DateTime.Now;
                category.UpdatedRole = objUser.rID;
                result = _objSubServiceCategoryDB.UpdateSubServiceCategory(category);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
       
        public ActionResult DeleteSubServiceCategory(DeleteSubServiceCategoryModel category)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                category.IsActive = false;
                category.IsDelete = true;
                category.UpdatedBy = User.Identity.Name;
                category.UpdatedOn = DateTime.Now;
                category.UpdatedRole = objUser.rID;
                result = _objSubServiceCategoryDB.DeleteServiceCategory(category);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSubServiceCategories()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objSubServiceCategoryDB.GetSubServiceCategories(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetSubServiceCategoryByID(int? servsubcatID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objSubServiceCategoryDB.GetSubServiceCategoryByID(objUser.uID,servsubcatID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetSubServiceCategoryDroDown()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objSubServiceCategoryDB.GetSubServiceCategoryDroDown(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetSubCategoryByCatIDDropDown(int? catID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonServiceDB.GetSubCategoryByCatIDDropDown(catID,objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetServiceCategoryByCatSubIDDropDown(int? catsubID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonServiceDB.GetServiceCategoryByCatSubIDDropDown(catsubID, objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetSubServiceCategoryByServCatIDDropDown(int? servcatID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonServiceDB.GetSubServiceCategoryByServCatIDDropDown(servcatID, objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult CreateStaffService(StaffServiceModel staffService)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                staffService.IsActive = true;
                staffService.IsDelete = false;
                staffService.CreatedBy = User.Identity.Name;
                staffService.CreatedOn = DateTime.Now;
                result = _objStaffServiceDB.CreateStaffService(staffService);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetStaffServices()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objStaffServiceDB.GetStaffServices(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetSubCategoryByCatIDDropDownWithImages(int? catID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonServiceDB.GetSubCategoryByCatIDDropDownWithImages(catID, objUser.uID);
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