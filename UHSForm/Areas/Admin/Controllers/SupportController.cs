using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UHSForm.Models;
using UHSForm.Models.Data;
using UHSForm.DAL;
using System.Configuration;
using Amazon.S3;
using Amazon.S3.Model;

namespace UHSForm.Areas.Admin.Controllers
{
    [Authorize(Roles = "10,11")]
    public class SupportController : Controller
    {
        private static readonly string _awsAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        private static readonly string _awsSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
        private static readonly string _bucketName = "urbanhospitalityserv";

        private GeneralDB _objGeneralDB;
        private SupportDB _objSupportDB;
        private UHSEntities UhDb;
        public SupportController()
        {
            _objGeneralDB = new GeneralDB();
            _objSupportDB = new SupportDB();
            UhDb = new UHSEntities();
        }



        // GET: Admin/Support
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SupportRequest(SupportDetailsModel Support)
        {
            string result = null;
            string link = null;
            try
            {
                using (var transaction = UhDb.Database.BeginTransaction())
                {
                    try
                    {
                        int fileCount = Request.Files.Count;
                        string TicketNo = null;
                        var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                        int CountSupport = UhDb.Supports.Where(x => x.IsActive == true && x.IsDelete == false && x.uID == objUser.uID && x.TicketID != null).Count();
                        if (CountSupport == 0)
                        {
                            DateTime TodayDate = DateTime.Now;
                            int? Date = TodayDate.Day;
                            int? Month = TodayDate.Month;
                            int? Year = TodayDate.Year;
                            TicketNo = Date.ToString() + Month.ToString() + Year.ToString() + 1;
                        }
                        else
                        {
                            TicketNo = UhDb.Supports.Where(x => x.IsActive == true && x.IsDelete == false && x.uID == objUser.uID).OrderByDescending(x => x.stID).FirstOrDefault().TicketID;
                            int T1 = Convert.ToInt32(TicketNo);
                            T1 = T1 + 1;
                            TicketNo = T1.ToString();

                        }
                        Support.IsActive = true;
                        Support.IsDelete = false;
                        Support.CreatedBy = User.Identity.Name;
                        Support.CreatedOn = DateTime.Now;
                        Support.uID = objUser.uID;
                        Support.suID = objUser.userID;
                        Support.rID = objUser.rID;
                        Support.Status = "Open";
                        Support.TicketNo = TicketNo;
                        int scopeIdentity = _objSupportDB.CreateSupport(Support);
                        string[] ArrayOfLink = new string[2];
                        int CountLink = 1;
                        if (scopeIdentity != 0 && fileCount > 0)
                        {
                            foreach (string key in Request.Files)
                            {
                                HttpPostedFileBase postedFile = Request.Files[key];
                                string path = scopeIdentity.ToString() + "_" + "_" + postedFile.FileName;
                                using (IAmazonS3 s3client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, Amazon.RegionEndpoint.USEast1))
                                {
                                    PutObjectRequest putObjectRequest = new PutObjectRequest
                                    {
                                        BucketName = _bucketName,
                                        CannedACL = S3CannedACL.PublicRead,
                                        Key = string.Format("UHS/Prod/Support/" + path),
                                        InputStream = postedFile.InputStream
                                    };
                                    s3client.PutObject(putObjectRequest);
                                    if (CountLink == 1)
                                    {
                                        ArrayOfLink[0] = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/Support/" + path;
                                    }
                                    else
                                    {
                                        ArrayOfLink[1] = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/Support/" + path;
                                    }
                                    CountLink = CountLink + 1;
                                }

                                Models.Data.File objFiles = new Models.Data.File();
                                objFiles.Filename = Path.GetFileName(postedFile.FileName);
                                objFiles.FileSize = postedFile.ContentLength.ToString();
                                objFiles.uID = Support.uID;
                                if (objUser.rID == 11)
                                {
                                    objFiles.suID = objUser.userID;
                                }
                                objFiles.stID = (int)scopeIdentity;
                                objFiles.FileContentType = postedFile.ContentType;
                                objFiles.FileFieldName = path;
                                objFiles.FileUse = 6;
                                objFiles.IsActive = true;
                                objFiles.IsDelete = false;
                                objFiles.CreatedBy = User.Identity.Name;//User.Identity.Name
                                objFiles.CreatedOn = Support.CreatedOn;
                                UhDb.Files.Add(objFiles);
                                UhDb.SaveChanges();
                            }
                            transaction.Commit();
                        }
                        string Serverity = null;
                        if (Support.Serverity == 1)
                        {
                            Serverity = "General";
                        }
                        else
                        {
                            Serverity = "System";
                        }
                        string body = EmailBodyForSupport(objUser.Name, Serverity, Support.subject, Support.Description, ArrayOfLink[0], ArrayOfLink[1], TicketNo);
                        _objGeneralDB.SentMultipleEmailFromAmazon(body, "Support Request");
                        if (Support.EmailAddress != null)
                        {
                            string body1 = EmailBodyForSupportClient(objUser.Name, Support.subject, Serverity, TicketNo);
                            foreach (var email in Support.EmailAddress)
                            {
                                _objGeneralDB.SentEmailFromAmazon(email, body1, "Support Request Received", objUser.Name);
                            }
                        }
                        result = "Success";
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
        public ActionResult GetSupports()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objSupportDB.GetSupports(objUser.uID, objUser.rID, objUser.userID, objUser.stfID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }


        }

        private string EmailBodyForSupport(string Username, string Soverity, string Subject, string Description, string Link1, string Link2, string TicketNo)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Server.MapPath("~/EmailTemplates/Support.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{user}", Username);
            body = body.Replace("{Soverity}", Soverity);
            body = body.Replace("{Subject}", Subject);
            body = body.Replace("{Description}", Description);
            // Add File View Button for Link1 if not empty or null
            if (!string.IsNullOrEmpty(Link1))
            {
                body = body.Replace("{Link1}", $@"
            <p>
                <a href=""{Link1}"" style=""background-color: #007BFF; color: #ffffff; padding: 6px 10px; text-decoration: none; display: inline-block; border-radius: 5px;"">
                    View File 1
                </a>
            </p>");
            }
            else
            {
                // If Link1 is empty or null, remove the placeholder
                body = body.Replace("{Link1}", string.Empty);
            }

            // Add File View Button for Link2 if not empty or null
            if (!string.IsNullOrEmpty(Link2))
            {
                body = body.Replace("{Link2}", $@"
            <p>
                <a href=""{Link2}"" style=""background-color: #007BFF; color: #ffffff; padding: 6px 10px; text-decoration: none; display: inline-block; border-radius: 5px;"">
                    View File 2
                </a>
            </p>");
            }
            else
            {
                // If Link2 is empty or null, remove the placeholder
                body = body.Replace("{Link2}", string.Empty);
            }
            body = body.Replace("{TicketNo}", TicketNo);
            return body;
        }

        private string EmailBodyForSupportClient(string Username, string Subject, string Soverity, string TicketNo)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Server.MapPath("~/EmailTemplates/SupportReceived.html")))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{Soverity}", Soverity);
            body = body.Replace("{Subject}", Subject);
            body = body.Replace("{Username}", Username);
            body = body.Replace("{TicketNo}", TicketNo);
            return body;
        }
    }
}