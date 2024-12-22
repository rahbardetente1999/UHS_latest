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
using UHSForm.DAL;
using UHSForm.Models;
using UHSForm.Models.Data;

namespace UHSForm.Areas.Customer.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private static readonly string _awsAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        private static readonly string _awsSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
        private static readonly string _bucketName = "urbanhospitalityserv";

        private CustomerDB _objCustomerDB;
        private GeneralDB _objGeneralDB;
        private MainCategoryDB _objMainCategoryDB;
        private CommonServiceDB _objCommonServiceDB;
        private PropertyAreaDB _objPropertyAreaDB;
        private CommonPropertyDB _objCommonPropertyDB;
        private CommonPackagesDB _objCommonPackagesDB;
        private CommonIncExcDB _objCommonIncExcDB;
        private CommonCustomerTimeLineDB _objCommonCustomerTimeLineDB;
        private CustomerRatingDB _objCustomerRatingDB;
        private CustomerComplaintDB _objCustomerComplaintDB;
        private CustomerInVoiceDB _objCustomerInVoiceDB;
        private CustomerServiceDB _objCustomerServiceDB;
        private CustomerDashboardDB _objCustomerDashboardDB;
        private DashboardDB _objDashboardDB;
        private CustomerRenewalDB _objCustomerRenewalDB;
        private CustomerRescheduleDB _objCustomerRescheduleDB;
        private SubAreaDB _objSubAreaDB;
        private CustomerSupportDB _objcustomerSupportDB;
        private CustomerAlertsDB _objcustomerAlertsDB;
        private UHSEntities UhDB;

        public BookingController()
        {
            _objCustomerDashboardDB = new CustomerDashboardDB();
            _objCustomerDB = new CustomerDB();
            _objGeneralDB = new GeneralDB();
            _objCommonServiceDB = new CommonServiceDB();
            _objPropertyAreaDB = new PropertyAreaDB();
            _objCommonPropertyDB = new CommonPropertyDB();
            _objMainCategoryDB = new MainCategoryDB();
            _objCommonPackagesDB = new CommonPackagesDB();
            _objCommonIncExcDB = new CommonIncExcDB();
            _objCommonCustomerTimeLineDB = new CommonCustomerTimeLineDB();
            _objCustomerRatingDB = new CustomerRatingDB();
            _objCustomerComplaintDB = new CustomerComplaintDB();
            _objCustomerInVoiceDB = new CustomerInVoiceDB();
            _objCustomerServiceDB = new CustomerServiceDB();
            _objcustomerSupportDB = new CustomerSupportDB();
            _objDashboardDB = new DashboardDB();
            _objCustomerRenewalDB = new CustomerRenewalDB();
            _objCustomerRescheduleDB = new CustomerRescheduleDB();
            _objcustomerAlertsDB = new CustomerAlertsDB();
             UhDB = new UHSEntities();
            _objSubAreaDB = new SubAreaDB();
        }

        // GET: Customer/Booking
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Renew()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Existing()
        {
            return View();
        }

        public ActionResult Reschedule()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult TermsCondition()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }


        public ActionResult InclusionExclusion()
        {
            return View();
        }

        public ActionResult TestForm()
        {
            return View();
        }

        public ActionResult NewBooking()
        {
            return View();
        }

        public ActionResult CustomerSupportForm()
        {
            return View();
            //return View();
        }

        [HttpPost]
        public ActionResult CreateCustomerSupportRequest(CustomerSupportModel customersupport, List<HttpPostedFileBase> files)
        {
            string result = null;
            try
            {
                using (var transaction = UhDB.Database.BeginTransaction())
                {
                    try
                    {
                      
                        var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                        
                        customersupport.IsActive = true;
                        customersupport.IsDelete = false;
                        customersupport.CreatedBy = User.Identity.Name;
                        customersupport.CreatedOn = DateTime.Now;
                        customersupport.custID = objUser.cuID;

                        int scopeIdentity = _objcustomerSupportDB.CreateCustomerSupportTicket(customersupport);
                        if (files != null && scopeIdentity != null && scopeIdentity != 0)
                        {
                            foreach (var key in files)
                            {
                                //HttpPostedFileBase postedFile = Request.Files[key];
                                string path = scopeIdentity.ToString()  + "_" + key.FileName;
                                using (IAmazonS3 s3client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, Amazon.RegionEndpoint.USEast1))
                                {
                                    PutObjectRequest putObjectRequest = new PutObjectRequest
                                    {
                                        BucketName = _bucketName,
                                        CannedACL = S3CannedACL.PublicRead,
                                        Key = string.Format("UHS/Prod/CustomerSupport/" + path),
                                        InputStream = key.InputStream
                                    };
                                    s3client.PutObject(putObjectRequest);
                                 }

                                Models.Data.File objFiles = new Models.Data.File();
                                objFiles.Filename = Path.GetFileName(key.FileName);
                                objFiles.FileSize = key.ContentLength.ToString();
                                objFiles.cuiD = customersupport.custID;
                                objFiles.custSID = scopeIdentity;
                                //objFiles.stID = (int)scopeIdentity;
                                objFiles.FileContentType = key.ContentType;
                                objFiles.FileFieldName = path;
                                objFiles.FileUse = 12;
                                objFiles.IsActive = true;
                                objFiles.IsDelete = false;
                                objFiles.CreatedBy = User.Identity.Name;
                                objFiles.CreatedOn = customersupport.CreatedOn;
                                UhDB.Files.Add(objFiles);
                                UhDB.SaveChanges();
                            }
                            transaction.Commit();
                        }
                        result = "SUCCESS";
                    }
                    catch (Exception ex)
                    {
                        
                        result = "Exception";
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
        public ActionResult GetCustomerSupport()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objcustomerSupportDB.GetCustomerSupportForCustomer(objUser.cuID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }


        [HttpGet]
        public ActionResult GetCustomerAlertsByStatusForCustomer(int? Status)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objcustomerAlertsDB.GetCustomerAlertsByStatusForCustomer((int)objUser.cuID,Status);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult GetCustomersByCustomerID()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerDB.GetCustomersByCustomerID(objUser.cuID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCustomersBySubCategory(int? catID, int? catsubID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerDB.GetCustomersBySubCategory(objUser.cuID, catID, catsubID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCustomersByServiceSubCategory(int? catID, int? catsubID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerDB.GetCustomersByServiceSubCategory(objUser.cuID, catID, catsubID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ContentResult CreateAppointment(CreateAppointmentModel customer, List<HttpPostedFileBase> files)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                customer.CreatedRole = objUser.rID;
                customer.CreatedBy = User.Identity.Name;
                customer.CreatedOn = DateTime.Now;
                customer.cuID = objUser.cuID;
                customer.IsActive = true;
                customer.IsDelete = false;
                List<string> saveResult = _objCustomerDB.CreateAppointment(customer);
                int? cuID = null, custODID = null, CustomerID = null, TaskNo = null;
                string PaymentLink = null;
                for (int i = 0; i < saveResult.Count(); i++)
                {
                    if (i == 0) { cuID = Convert.ToInt32(saveResult[0]); }
                    else if (i == 1) { custODID = Convert.ToInt32(saveResult[1]); }
                    else if (i == 2) { CustomerID = Convert.ToInt32(saveResult[2]); }
                    else if (i == 3) { TaskNo = Convert.ToInt32(saveResult[3]); }
                    else if (i == 4) { PaymentLink = saveResult[4]; }
                }
                if (cuID != -1 && cuID != -2 && cuID != 0 && files != null)
                {
                    foreach (var key in files)
                    {
                        string Name = UhDB.Customers.Where(x => x.cuID == cuID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                        //HttpPostedFileBase postedFile = Request.Files[key];
                        string path = cuID.ToString() + "_" + custODID.ToString() + "_" +
                                        CustomerID + "_" + TaskNo + "_" + Name + "_" + key.FileName;
                        string _saveKey = "UHS/Prod/CustomerUploads/";

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

                        Models.Data.File objFiles = new Models.Data.File();
                        objFiles.Filename = key.FileName;
                        objFiles.FileSize = key.ContentLength.ToString();
                        objFiles.cuiD = cuID;
                        objFiles.custODID = custODID;
                        objFiles.uID = 1;
                        objFiles.FileContentType = key.ContentType;
                        objFiles.FileFieldName = path;
                        objFiles.FileUse = 7;
                        objFiles.IsActive = true;
                        objFiles.IsDelete = false;
                        objFiles.CreatedBy = User.Identity.Name;//User.Identity.Name
                        objFiles.CreatedOn = DateTime.Now;
                        UhDB.Files.Add(objFiles);
                        UhDB.SaveChanges();

                    }
                    result = "SUCCESS";
                }
                else if (cuID == -1)
                {
                    result = "AExEmail";
                }
                else if (cuID == -2)
                {
                    result = "Exception";
                }
                else if (cuID == 0)
                {
                    result = "Not Save";
                }
                else
                {
                    result = PaymentLink;
                }
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Content(result);
        }

        [HttpPost]
        public ContentResult CustomerServiceRating(CustomerServiceRatingModel customer)
        {
            using (var trans = UhDB.Database.BeginTransaction()) 
            {
                try
                {
                    var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                    customer.IsActive = true;
                    customer.IsDelete = false;
                    customer.CreatedBy = User.Identity.Name;
                    customer.CreatedOn = DateTime.Now;
                    customer.CreatedRole = objUser.rID;
                    int? CustomerFeedbackID = _objCustomerRatingDB.CustomerServiceRating(customer);
                    int FileCount = Request.Files.Count;
                    if (CustomerFeedbackID != null && CustomerFeedbackID != 0 && FileCount != 0)
                    {
                        foreach (string key in Request.Files)
                        {
                            HttpPostedFileBase postedFile = Request.Files[key];
                            string path = objUser.cuID.ToString() + "_" + CustomerFeedbackID + "_" + postedFile.FileName;
                            string _saveKey = "UHS/Prod/CustomerRating/";
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
                            imageFiles.cuiD = objUser.cuID;
                            imageFiles.FileContentType = postedFile.ContentType;
                            imageFiles.FileFieldName = path;
                            imageFiles.FileUse = 10;
                            imageFiles.IsActive = true;
                            imageFiles.IsDelete = false;
                            imageFiles.CreatedBy = User.Identity.Name;//User.Identity.Name
                            imageFiles.CreatedOn = DateTime.Now;
                            UhDB.Files.Add(imageFiles);
                            UhDB.SaveChanges();


                        }
                    }
                    trans.Commit();
                    string result = "SUCCESS";
                    return Content(result);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    string result = "Exception";
                    return Content(result);
                }
            }


        }

        [HttpGet]
        public ActionResult GetCustomerServiceRatings(int? cuID, int? custODID)
        {
            try
            {
                var result = _objCustomerRatingDB.GetCustomerServiceRatings(cuID, custODID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ContentResult CustomerComplaint(CustomerComplaintModel customer, List<HttpPostedFileBase> files)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                customer.IsActive = true;
                customer.IsDelete = false;
                customer.CreatedBy = User.Identity.Name;
                customer.CreatedOn = DateTime.Now;
                customer.CreatedRole = objUser.rID;
                int? custComID = _objCustomerComplaintDB.CreateCustomerComplaint(customer);

                if (files != null && custComID != null && custComID != 0)
                {

                    foreach (var key in files)
                    {
                        //HttpPostedFileBase postedFile = Request.Files[key];
                        string Name = UhDB.Customers.Where(x => x.cuID == customer.cuID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                        //HttpPostedFileBase postedFile = Request.Files[key];
                        string path = customer.cuID.ToString() + "_" + customer.custODID.ToString() + "_" + Name + "_" + key.FileName;
                        string _saveKey = "UHS/Prod/CustomerComplaints/";

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

                        Models.Data.File objFiles = new Models.Data.File();
                        objFiles.Filename = key.FileName;
                        objFiles.FileSize = key.ContentLength.ToString();
                        objFiles.cuiD = customer.cuID;
                        objFiles.custODID = customer.custODID;
                        objFiles.uID = 1;
                        objFiles.FileContentType = key.ContentType;
                        objFiles.FileFieldName = path;
                        objFiles.FileUse = 8;
                        objFiles.IsActive = true;
                        objFiles.IsDelete = false;
                        objFiles.CreatedBy = User.Identity.Name;//User.Identity.Name
                        objFiles.CreatedOn = DateTime.Now;
                        UhDB.Files.Add(objFiles);
                        UhDB.SaveChanges();

                    }
                }
                if (custComID == 0 || custComID == null)
                {
                    result = "Exception";
                }
                else
                {
                    result = "SUCCESS";
                }
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Content(result);
        }

        [HttpGet]
        public ActionResult GetCustomerComplaints(int? cuID, int custODID)
        {
            try
            {
                var result = _objCustomerComplaintDB.GetCustomerComplaints(cuID, custODID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpGet]
        public ActionResult GetMainCategoryDropdown()
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

        [HttpGet]
        public ActionResult GetSubCategoryByCatIDDropDown(int? catID)
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

        [HttpGet]
        public ActionResult GetServiceCategoryByCatSubIDDropDown(int? catsubID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonServiceDB.GetServiceCategoryByCatSubIDDropDownWithImages(catsubID, objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetSubServiceCategoryByServCatIDDropDown(int? servcatID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonServiceDB.GetSubServiceCategoryByServCatIDDropDownWithImages(servcatID, objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
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
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
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
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
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
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public ActionResult GetPackagesBySubCategoryServices(PackagesBySubCategoryServicesGetModel packagesBySub)
        {
            try
            {

                var result = _objCommonPackagesDB.GetPackagesBySubCategoryServices(packagesBySub);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetIncExclusByService(int? catID, int? catsubID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonIncExcDB.GetIncExclusByService(objUser.uID, catID, catsubID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetIncExclusBySubService(int? catID, int? catsubID, int? servcatID, int? servsubcatID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonIncExcDB.GetIncExclusBySubService(objUser.uID, catID, catsubID, servcatID, servsubcatID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetTimeLine(TimeLineGetModel timeLine)
        {
            try
            {

                var result = _objCommonCustomerTimeLineDB.GetTimeLine(timeLine);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCustomerLastInvoice()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerInVoiceDB.GetCustomerLastInvoice(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult CountSameTeam(CountSameTeamModel team)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                team.cuID = objUser.cuID;
                var result = _objCustomerDB.CountSameTeam(team);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetSlotsTimeLine(SlotTimeLineGetModel timeLine)
        {
            try
            {

                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                timeLine.uID = objUser.uID;
                var result = _objCommonCustomerTimeLineDB.GetSlotsTimeLine(timeLine);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetTimeLineByStaff(SlotTimeLineForSatffGetModel timeLine)
        {
            try
            {

                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                timeLine.uID = objUser.uID;
                var result = _objCommonCustomerTimeLineDB.GetTimeLineByStaff(timeLine);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetTimeSlot(int? packID, int? catID, int? catsubID)
        {
            try
            {
                var result = _objCommonCustomerTimeLineDB.GetTimeSlot(packID, catID, catsubID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetTimeForChoosenBookingDaysTwiceInAWeek(int? packID, int? catID, int? catsubID, List<string> Days)
        {
            try
            {
                var result = _objCommonCustomerTimeLineDB.GetTimeForChoosenBookingDaysTwiceInAWeek(packID, catID, catsubID, Days);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetDates(int? packID, int? catID, int? catsubID, List<string> Days)
        {
            try
            {
                var result = _objCommonCustomerTimeLineDB.GetDates(packID, catID, catsubID, Days);
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

        [HttpPost]
        public ActionResult GetPackagesBySubCategoryServicesWithOutProperty(PackagesBySubCategoryServicesGetModel packagesBySub)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                packagesBySub.uID = objUser.uID;
                var result = _objCommonPackagesDB.GetPackagesBySubCategoryServicesWithOutProperty(packagesBySub);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCustomerShortDetails()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerDB.GetCustomerShortDetails((int)objUser.cuID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCurrentCustomerTimeLines(int? catID, int? catsubID, int? StatusOfWork, string Month, int? vID, string AppartmentName)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerDashboardDB.GetCurrentCustomerTimeLines((int)objUser.cuID, catID, catsubID, StatusOfWork, Month, vID, AppartmentName);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpGet]
        public ActionResult GetCustomerDashboard() 
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerDashboardDB.GetCustomerDashboard((int)objUser.cuID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetHistoryCustomerTimeLines(int? catID, int? catsubID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerDashboardDB.GetHistoryCustomerTimeLines((int)objUser.cuID, catID, catsubID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCustomerPayment(int? catID, int? catsubID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerDashboardDB.GetCustomerPayment((int)objUser.cuID, catID, catsubID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpGet]
        public ActionResult GetCustomersByIDs(int? cuID, int? cuODID)
        {
            try
            {
                var result = _objCustomerDB.GetCustomersByIDs(cuID, cuODID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPropertyAreaByCustomer()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerServiceDB.GetPropertyAreaByCustomer((int)objUser.cuID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPropertyByCustomerPropertyArea(int? propaID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerServiceDB.GetPropertyByCustomerPropertyArea((int)objUser.cuID, propaID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPropertyByCustomerPropertyResidenceType(int? propaID, int? vID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerServiceDB.GetPropertyByCustomerPropertyResidenceType((int)objUser.cuID, propaID, vID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPropertyByCustomerOtherProperty(int? propaID, int? vID, int? propType)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerServiceDB.GetPropertyByCustomerOtherProperty((int)objUser.cuID, propaID, vID, propType);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPackagesByServicesForCarWash(int? uID, int? catID, int? cartID, int? cartsID)
        {
            try
            {

                var result = _objCommonPackagesDB.GetPackagesByServicesForCarWash(uID, catID, cartID, cartsID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCustomersForTimeLineCustomerID(int? custID, int? custODID)
        {
            try
            {
                var result = _objCustomerDB.GetCustomersForTimeLineCustomerID(custID, custODID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetResultByTeam(GetResultByTeamModel teams)
        {
            try
            {
                var result = _objCommonCustomerTimeLineDB.GetResultByTeam(teams);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult GetResultForOtherTime(GetResultForOtherTime time)
        {
            try
            {
                var result = _objCommonCustomerTimeLineDB.GetResultForOtherTime(time);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult GetResultsForTimeSlotsExisting(GetResultsForTimeSlotsExisting customer)
        {
            try
            {

                var result = _objCommonCustomerTimeLineDB.GetResultsForTimeSlotsExisting(customer);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult GetPerviousTeam(int? catID, int? catsubID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonCustomerTimeLineDB.GetPerviousTeam(catID, catsubID, objUser.cuID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetResultsForTimeSlots(int? packID, int? catID, int? catsubID, int? propresID)
        {
            try
            {

                var result = _objCommonCustomerTimeLineDB.GetResultsForTimeSlots(packID, catID, catsubID, propresID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult GetCustomerDashboardCount()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objDashboardDB.GetCustomerDashboardCount(objUser.cuID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult GetDashboardPropertyDropdown()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerDashboardDB.GetDashboardPropertyDropdown(objUser.cuID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult GetDashboardMonthsDropdown(int? catID, int? catsubID, int? vID, string AppartmentName)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerDashboardDB.GetDashboardMonthsDropdown(objUser.cuID, catID, catsubID, vID, AppartmentName);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult GetTimeBlock(GetResultCheck time)
        {
            try
            {
                var result = _objCommonCustomerTimeLineDB.GetTimeBlock(time);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetReleaseTimeBlock(string MobileNo)
        {
            try
            {
                var result = _objCommonCustomerTimeLineDB.GetReleaseTimeBlock(MobileNo);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetBookedDates(BookedStartDates booked)
        {
            try
            {
                var result = _objCommonCustomerTimeLineDB.GetBookedDates(booked);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetBookedDates1(BookedStartDates1 booked)
        {
            try
            {
                var result = _objCommonCustomerTimeLineDB.GetBookedDates1(booked);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpPost]
        public ActionResult GetResultsForTimeSlots1(ResultsForTimeSlots1 time)
        {
            try
            {

                var result = _objCommonCustomerTimeLineDB.GetResultsForTimeSlots1(time);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult CheckRenewal()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerRenewalDB.CheckRenewal(objUser.cuID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult UpdateCustomerRenewal(CustomerRenewalModel customer)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                customer.cuID = objUser.cuID;
                customer.IsActive = true;
                customer.IsDelete = false;
                customer.CreatedBy = User.Identity.Name;
                customer.CreatedOn = DateTime.Now;
                customer.CreatedRole = objUser.rID;
                var result = _objCustomerRenewalDB.UpdateCustomerRenewal(customer);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult GetCustomerRenewalInfo(int? propaID, int? vID, int? proprestID, int? propTypeID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerRenewalDB.GetCustomerRenewalInfo(objUser.cuID,propaID,vID,proprestID,propTypeID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult GetCustomerRenewalPropertyInfo() 
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerRenewalDB.GetCustomerRenewalPropertyInfo(objUser.cuID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCustomerRenewalPropertyArea()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerRenewalDB.GetCustomerRenewalPropertyArea(objUser.cuID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCustomerRenewalProperty(int? propaID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerRenewalDB.GetCustomerRenewalProperty(objUser.cuID,propaID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCustomerRenewalPropertyResidencyType(int? propaID,int? vID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerRenewalDB.GetCustomerRenewalPropertyResidencyType(objUser.cuID, propaID,vID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCustomersForUnCompletedTask()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerRenewalDB.GetCustomersForUnCompletedTask(objUser.cuID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SendCustomerEmailVerification() 
        {
            string result = null;
            try 
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                string OTP = _objGeneralDB.GenerateOTP1(5);
                string body = EmailBodyCustomerEmailVerification(objUser.Name,OTP);
                result = _objGeneralDB.SentEmailFromAmazon(objUser.Email,body,"Your email verification",objUser.Name);
                if (result == "SUCCESS")
                {
                    result = OTP;
                }
            }
            catch (Exception ex) 
            {
                result = "Exception";
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CustomerEmailVerification(bool? IsVerified)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                CustomerVerification objCustomerVerification = new CustomerVerification();
                objCustomerVerification.IsEmail = true;
                objCustomerVerification.cuID = objUser.cuID;
                objCustomerVerification.UpdatedBy = User.Identity.Name;
                objCustomerVerification.UpdatedOn = DateTime.Now;
                objCustomerVerification.UpdatedRole = objUser.rID;
                result = _objGeneralDB.CustomerVerification(objCustomerVerification);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SendCustomerMobileVerification()
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                string OTP = _objGeneralDB.GenerateOTP1(5);
                string body = OTP+" is your OTP to verify your mobile.";
                result = _objGeneralDB.SendSMS(objUser.PhoneCode+objUser.MobileNo,body);
                if (result == "OK")
                {
                   
                    result = OTP;
                  
                }
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CustomerMobileVerification(bool? IsVerified)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                CustomerVerification objCustomerVerification = new CustomerVerification();
                objCustomerVerification.IsMobile = true;
                objCustomerVerification.cuID = objUser.cuID;
                objCustomerVerification.UpdatedBy = User.Identity.Name;
                objCustomerVerification.UpdatedOn = DateTime.Now;
                objCustomerVerification.UpdatedRole = objUser.rID;
                result = _objGeneralDB.CustomerVerification(objCustomerVerification);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetRemaningDateOfCustomer(CustomerBookedStartDates booked) 
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                booked.cuID = objUser.cuID;
                var result = _objCustomerRescheduleDB.GetRemaningDateOfCustomer(booked);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SaveReschedule(SaveRescheduleModel customer)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                customer.UpdatedBy = objUser.Email;
                customer.UpdatedOn = DateTime.Now;
                var result = _objCustomerRescheduleDB.SaveReschedule(customer);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult IsTeamAvaialble(CheckTeamAvailable customer) 
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                customer.cuID = objUser.cuID;
                var result = _objCustomerRenewalDB.IsTeamAvaialble(customer);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult GetRenewalBookedDates(ExistingBookedStartDates booked)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerRenewalDB.GetRenewalBookedDates(booked);
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

        [HttpPost]
        public ActionResult GetSpecDeepAndCarWash(GetSpecDeepAndCarWashModel times)
        {
            try
            {
                var result = _objCustomerRescheduleDB.GetSpecDeepAndCarWash(times);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        private string EmailBodyCustomerEmailVerification(string CustomerName, string OTP)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Server.MapPath("~/EmailTemplates/SendOTPVerification.html")))//using streamreader for reading my htmltemplate  
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{CustomerName}", CustomerName); //replacing the required things  
            body = body.Replace("{OTP}", OTP);
            return body;
        }
    }
}