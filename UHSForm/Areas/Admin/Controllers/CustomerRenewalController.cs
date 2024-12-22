using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UHSForm.DAL;
using UHSForm.Models;

namespace UHSForm.Areas.Admin.Controllers
{
    public class CustomerRenewalController : Controller
    {
        private CustomerRenewalDB _objCustomerRenewalDB;
        private GeneralDB _objGeneralDB;
        private CustomerDB _objCustomerDB;

        public CustomerRenewalController()
        {
            _objCustomerRenewalDB = new CustomerRenewalDB();
            _objGeneralDB = new GeneralDB();
            _objCustomerDB = new CustomerDB();
        }

        // GET: Admin/CustomerRenewal
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetCustomerRenewalFromAdmin()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerRenewalDB.GetCustomerRenewalFromAdmin(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCustomersForCompletedTask(int? cuID)
        {
            try
            {
                var result = _objCustomerRenewalDB.GetCustomersForCompletedTask(cuID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCustomersForUnCompletedTask(int? cuID)
        {
            try
            {
                var result = _objCustomerRenewalDB.GetCustomersForUnCompletedTask(cuID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult SendConfirmationLink(int? cuID, int? propaID, int? vID, int? proprestID, int? propTypeID, string AppartmentNumber)
        {
            try
            {
                string CustomerID = HttpUtility.HtmlEncode(_objGeneralDB.Encrypt((cuID.ToString()), "Lets1Make2It3Happen4"));
                string PropertyAreaID = HttpUtility.HtmlEncode(_objGeneralDB.Encrypt((propaID.ToString()), "Lets1Make2It3Happen4"));
                string PropertyID = HttpUtility.HtmlEncode(_objGeneralDB.Encrypt((vID.ToString()), "Lets1Make2It3Happen4"));
                string PropertyResidencyID = HttpUtility.HtmlEncode(_objGeneralDB.Encrypt((proprestID.ToString()), "Lets1Make2It3Happen4"));
                string AppartmentNo = HttpUtility.HtmlEncode(_objGeneralDB.Encrypt((AppartmentNumber.ToString()), "Lets1Make2It3Happen4"));
                string PropertyTypeID = HttpUtility.HtmlEncode(_objGeneralDB.Encrypt((propTypeID.ToString()), "Lets1Make2It3Happen4"));
                string Link = "https://booking.urbanhospitalityservices.com/CustomerRenewal/Index?A=" + CustomerID + "&B=" + PropertyAreaID + "&C=" + PropertyID + "&D=" + PropertyResidencyID + "&E=" + PropertyTypeID + "&F=" + AppartmentNo;
                var objCustomer = _objCustomerDB.GetCustomersByCustomerID(cuID);
                string CustomerName = objCustomer.FirstOrDefault().Name;
                string CustomerEmail = objCustomer.FirstOrDefault().Email;
                string EmailBody = EmailForNotification(Link, CustomerName);
                _objGeneralDB.SentEmailFromAmazon(CustomerEmail, EmailBody, "Your Subscription Renewal", CustomerName);
                return Json("SUCCESS", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCustomerRenewalInfo(int? cuID,int? propaID, int? vID, int? proprestID, int? propTypeID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerRenewalDB.GetCustomerRenewalInfo(cuID, propaID, vID, proprestID, propTypeID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        private string EmailForNotification(string link, string customerName)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Server.MapPath("~/EmailTemplates/RenewalNotificationFromAdmin.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{link}", link);
            body = body.Replace("{customerName}", customerName);
            return body;
        }
    }
}