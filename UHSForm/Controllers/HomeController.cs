using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UHSForm.Models;
using UHSForm.Models.Data;
using UHSForm.DAL;
using System.Web.Security;
using System.IO;
using System.Configuration;
using Amazon.S3;
using Amazon;
using Amazon.S3.Model;
using log4net;
using System.Threading.Tasks;
using UHSForm.MessengerService;

namespace UHSForm.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        // Initialize the SOAP client using the endpoint from the Web Reference
        //private MessengerSoapClient messenger = new MessengerSoapClient();
        // Initialize the SOAP client
        private MessengerSoapClient messenger = new MessengerSoapClient("MessengerSoap");
        //private static readonly string apiUrl = "https://messaging.ooredoo.qa/bms/soap/Messenger.asmx/HTTP_SendSms";
        private static readonly string apiBaseUrl = "https://messaging.ooredoo.qa/bms/soap/Messenger.asmx/";
        private static readonly string customerID = "6264";
        private static readonly string userName = "UHS";
        private static readonly string userPassword = "UHSms$007";
        private static readonly string _awsAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        private static readonly string _awsSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
        private static readonly string _bucketName = "urbanhospitalityserv";
        private GeneralDB _objGeneralDB;
        private MainCategoryDB _objMainCategoryDB;
        private CommonServiceDB _objCommonServiceDB;
        private PropertyAreaDB _objPropertyAreaDB;
        private CommonPropertyDB _objCommonPropertyDB;
        private CommonPackagesDB _objCommonPackagesDB;
        private CommonIncExcDB _objCommonIncExcDB;
        private CommonCustomerTimeLineDB _objCommonCustomerTimeLineDB;
        private CustomerDB _objCustomerDB;
        private CustomerInVoiceDB _objCustomerInVoiceDB;
        private SubAreaDB _objSubAreaDB;
        private UHSEntities UhDB;
        private static log4net.ILog Log { get; set; }
        ILog lognet = log4net.LogManager.GetLogger(typeof(HomeController));
        public HomeController()
        {
            _objGeneralDB = new GeneralDB();
            _objCommonServiceDB = new CommonServiceDB();
            _objPropertyAreaDB = new PropertyAreaDB();
            _objCommonPropertyDB = new CommonPropertyDB();
            _objMainCategoryDB = new MainCategoryDB();
            _objCommonPackagesDB = new CommonPackagesDB();
            _objCommonIncExcDB = new CommonIncExcDB();
            _objCommonCustomerTimeLineDB = new CommonCustomerTimeLineDB();
            _objCustomerDB = new CustomerDB();
            _objCustomerInVoiceDB = new CustomerInVoiceDB();
            _objSubAreaDB = new SubAreaDB();
            UhDB = new UHSEntities();
        }
        // GET: Home
        public ActionResult Index()
        {
          
            return View();
        }

        public ActionResult Test()
        {

            return View();
        }
        public ActionResult Welcome()
        {

            return View();
        }

        [HttpPost]
        public ActionResult SingIn(Models.Login log)
        {

            string result = null;

            try
            {
                if (Membership.ValidateUser(log.Username, log.Password))
                {
                    FormsAuthentication.SetAuthCookie(log.Username, false);
                    string role = _objGeneralDB.GetRID(log.Username);
                    if (role == "10")
                    {
                        result = "SuperAdmin";
                    }
                    else if (role == "11")
                    {
                        result = "Admin";
                    }
                    else if (role == "15")
                    {
                        result = "Staff";
                    }
                    else if (role == "14")
                    {
                        result = "Customer";
                    }
                    else if (role == "16")
                    {
                        result = "Operation Manager";
                    }
                    else if (role == "17")
                    {
                        result = "Customer Support";
                    }
                    else if (role == "18")
                    {
                        result = "Logistic";
                    }
                    else
                    {
                        result = "NotAuthorized";
                    }
                }
                else
                {
                    int UserActiveCount = _objGeneralDB.GetUserIsActive(log.Username);
                    if (UserActiveCount == 0)
                    {
                        result = "UserNotActivated";
                    }
                    else
                    {
                        result = "NotAuthenticate";
                    }
                }
            }
            catch (Exception ex)
            {
                result = "LoginFailed";

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetBlock(string Username)
        {
            try
            {

                var res = _objGeneralDB.GetBlock(Username);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = ex.InnerException.Message;
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Block(string Username)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(Username);
                Block objBlock = new Block();
                objBlock.IsBlock = true;
                objBlock.Username = Username;
                objBlock.UpdatedBy = User.Identity.Name;//User.Identity.Name
                objBlock.UpdatedOn = DateTime.Now;
                objBlock.UpdatedRole = objUser.rID;
                var result = _objGeneralDB.BlockUser(objBlock);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult GetPassword(string Username)
        {
            //log.Info("Home/GetPassword");
            string result = "";
            try
            {
                int usercount = _objGeneralDB.GetUserCount(Username);
                if (usercount == 0)
                {
                    result = "NUser";
                }
                else
                {
                    var user = _objGeneralDB.GetUserLoginDetails(Username);//User.Identity.Name or username
                    ChangePassword password = new ChangePassword();
                    password.Username = Username;
                    password.Password = _objGeneralDB.GeneratePassword(7);
                    password.CreatedBy = Username;
                    password.CreatedOn = DateTime.Now;
                    result = _objGeneralDB.ForgotPassword(password);
                    if (result == "Success")
                    {
                        string body = EmailBody(Username, password.Password);
                        _objGeneralDB.SentEmailFromAmazon(user.Email, body, "UHS New Password", user.Name);
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
        public ActionResult GetMainCategoryDropdown(int? uID)
        {
            try
            {

                var result = _objMainCategoryDB.GetMainCategoryDropdown(uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetSubCategoryByCatIDDropDown(int? catID, int? uID)
        {
            try
            {

                var result = _objCommonServiceDB.GetSubCategoryByCatIDDropDownWithImages(catID, uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetServiceCategoryByCatSubIDDropDown(int? catsubID, int? uID)
        {
            try
            {

                var result = _objCommonServiceDB.GetServiceCategoryByCatSubIDDropDownWithImages(catsubID, uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetSubServiceCategoryByServCatIDDropDown(int? servcatID, int? uID)
        {
            try
            {

                var result = _objCommonServiceDB.GetSubServiceCategoryByServCatIDDropDownWithImages(servcatID, uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPropertyAreaDropDown(int? uID)
        {
            try
            {

                var result = _objPropertyAreaDB.GetPropertyAreaDropDown(uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPropertyByAreaDropDown(int? uID, int? propaID)
        {
            try
            {

                var result = _objCommonPropertyDB.GetPropertyByAreaDropDown(uID, propaID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPropertyDropDownByAreasID(int? uID, int? propaID, int? subAreaID)
        {
            try
            {
               
                var result = _objCommonPropertyDB.GetPropertyDropDownByAreasID(uID, propaID, subAreaID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetPropertyResidenceTypeByVIDDropDown(int? uID)
        {
            try
            {

                var result = _objCommonPropertyDB.GetPropertyResidenceTypeByVIDDropDown(uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult GetPackagesByServices(int? uID, int? catID, int? catsubID, int? propaID, int? vID, int? proprestID)
        {
            try
            {

                var result = _objCommonPackagesDB.GetPackagesByServices(uID, catID, catsubID, propaID, vID, proprestID);
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
        public ActionResult GetIncExclusByService(int? uID, int? catID, int? catsubID)
        {
            try
            {

                var result = _objCommonIncExcDB.GetIncExclusByService(uID, catID, catsubID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetIncExclusBySubService(int? uID, int? catID, int? catsubID, int? servcatID, int? servsubcatID)
        {
            try
            {

                var result = _objCommonIncExcDB.GetIncExclusBySubService(uID, catID, catsubID, servcatID, servsubcatID);
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
       
        [HttpPost]
        public ContentResult CreateFirtTimeCustomer(CustomerModel customer, List<HttpPostedFileBase> files)
        {
            string result = null;
            try
            {
                customer.CreatedRole = 14;
                customer.CreatedBy = customer.Email;
                customer.CreatedOn = DateTime.Now;
                customer.IsActive = true;
                customer.IsDelete = false;
                List<string> saveResult = _objCustomerDB.CreateCustomerFirstTime(customer);
                int? cuID = null, custODID = null, CustomerID = null, TaskNo = null;
                string PaymentLink = null;
                for (int i = 0; i < saveResult.Count(); i++)
                {
                    if (i==0) {cuID= Convert.ToInt32(saveResult[0]); }
                    else if (i == 1) { custODID = Convert.ToInt32(saveResult[1]); }
                    else if (i == 2) { CustomerID = Convert.ToInt32(saveResult[2]); }
                    else if (i == 3) { TaskNo = Convert.ToInt32(saveResult[3]); }
                    else if (i == 4) { PaymentLink = saveResult[4]; }
                }

                if (cuID != -1 && cuID != -2 && cuID != 0 && cuID!=null && files != null && saveResult != null)
                {
                    foreach (var key in files)
                    {
                        //HttpPostedFileBase postedFile = Request.Files[key];
                        string path = cuID.ToString() + "_" + custODID.ToString() + "_" +
                                         CustomerID + "_" + TaskNo + "_" + customer.Name + "_" + key.FileName;
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

        [HttpGet]
        public ActionResult GetCustomerLastInvoice(int? uID)
        {
            try
            {

                var result = _objCustomerInVoiceDB.GetCustomerLastInvoice(uID);
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
        public ActionResult GetPackagesByServicesWithOutProperty(int? uID, int? catID, int? catsubID, int? proprestID)
        {
            try
            {

                var result = _objCommonPackagesDB.GetPackagesByServicesWithOutProperty(uID, catID, catsubID, proprestID);
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

        [HttpPost]
        public ActionResult GetPackagesBySubCategoryServicesWithOutProperty(PackagesBySubCategoryServicesGetModel packagesBySub)
        {
            try
            {

                var result = _objCommonPackagesDB.GetPackagesBySubCategoryServicesWithOutProperty(packagesBySub);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string res = "Exception";
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }




        private string EmailBody(string user, string password)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Server.MapPath("~/EmailTemplates/ForgotPassword.html")))//using streamreader for reading my htmltemplate  
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{user}", user); //replacing the required things  
            body = body.Replace("{Password}", password);
            return body;
        }


        [HttpGet]
        public ActionResult TestCode(int? packID, int? catID, int? catsubID, int? propresID)
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

        [HttpPost]
        public ActionResult GetBookedDates2(BookedStartDates1 booked)
        {
            try
            {
                var result = _objCommonCustomerTimeLineDB.GetBookedDates2(booked);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        //// Authenticate API Method
        //[HttpGet]
        //public async Task<ActionResult> Authenticate()
        //{
        //    try
        //    {
        //        string authUrl = $"{apiBaseUrl}HTTP_Authenticate?customerID={customerID}&userName={userName}&userPassword={userPassword}";

        //        using (HttpClient client = new HttpClient())
        //        {
        //            HttpResponseMessage response = await client.GetAsync(authUrl);
        //            string responseContent = await response.Content.ReadAsStringAsync();

        //            if (response.IsSuccessStatusCode)
        //            {
        //                return Json(new { success = true, message = "Authentication Successful", data = responseContent }, JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                return Json(new { success = false, message = "Authentication Failed", data = responseContent }, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        
        // Send SMS Method
        [HttpGet]
        public ActionResult SendSms()
        {
            try
            {
                // Step 1: Authenticate the user
                MessengerService.SoapUser user = new MessengerService.SoapUser
                {
                    CustomerID = 6264, // Provide your customer ID
                    Name = "UHS",   // Replace with your username
                    Language = "en", // Language
                    Password = "Doha@123#" // Replace with your password
                };

                // Call the Authenticate function
                AuthResult authResult =  messenger.Authenticate(user);

                if (authResult.Result != "OK")
                {
                    return Content("Authentication Failed: " + authResult.Result);
                }

                // Step 2: Send SMS
                string originator = authResult.Originators[0]; // Use the first originator
                string smsData = "34345 is your OTP to verify your mobile";
                string phoneNumbers = "97472119012"; // Replace with recipient phone numbers
                string defDate = ""; // Deferred delivery time (optional)

                SendResult sendResult = messenger.SendSms(user, originator, smsData, phoneNumbers,
                                                           MessageType.Latin, defDate, false, false, false);

                if (sendResult.Result == "OK")
                {
                    return Json(new { success = true, message = "SMS Sent Successfully", data = sendResult.Result }, JsonRequestBehavior.AllowGet);
                   
                }
                else
                {
                    return Json(new { success = false, message = "Failed to Send SMS", data = sendResult.Result }, JsonRequestBehavior.AllowGet);
                    //return Content("SMS Failed: " + sendResult.Result);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
           
        }
        //[HttpGet]
        //public async Task<ActionResult> SendSms()
        //{
        //    try
        //    {
        //        // Build the request URL with query parameters
        //        string customerID = "6264"; // Replace with your customer ID
        //        string userName = HttpUtility.UrlEncode("UHSAdmin"); // Replace with your username and encode it
        //        string userPassword = HttpUtility.UrlEncode("UHSms$007"); // Replace with your password and encode it
        //        string originator = HttpUtility.UrlEncode("UHS"); // Replace with the originator and encode it
        //        string smsText = HttpUtility.UrlEncode("Assalamualikum");
        //        string recipientPhone = "97472119012,97433212115"; // Replace with recipient phone numbers
        //        string defDate = DateTime.UtcNow.ToString("yyyyMMddhhmmss"); // For immediate delivery, use empty string

        //        string requestUrl = $"{apiUrl}?customerID={customerID}&userName={userName}&userPassword={userPassword}" +
        //                            $"&originator={originator}&smsText={smsText}&recipientPhone={recipientPhone}" +
        //                            $"&messageType=Latin&defDate={defDate}&blink=false&flash=false&Private=false";

        //        // Send the GET request
        //        using (HttpClient client = new HttpClient())
        //        {
        //            HttpResponseMessage response = await client.GetAsync(requestUrl);
        //            string responseContent = await response.Content.ReadAsStringAsync();
        //            return Content(responseContent);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content("Error: " + ex.Message);
        //    }
        //}

        [HttpGet]
        public ActionResult GetSubAreaDropdownByPropertyArea(int? uID, int? propaID)
        {
            try
            {
                //var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objSubAreaDB.GetSubAreaDropdownByPropertyArea(uID, propaID);
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
