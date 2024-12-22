using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UHSForm.DAL;
using UHSForm.Models;

namespace UHSForm.Areas.Admin.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private CustomerDB _objCustomerDB;
        private GeneralDB _objGeneralDB;
        private StaffRatingDB _objStaffRatingDB;
        private CustomerTeamAssignDB _objCustomerTeamAssignDB;
        private CustomerRescheduleDB _objCustomerRescheduleDB;
        private CommonCustomerTimeLineDB _objCommonCustomerTimeLineDB;
        private CustomerSupportDB _objCustomerSupportDB;
        private CustomerAlertsDB _objCustomerAlertDB;
        private SendMessagesDB _objSendMessagesDB;

        public AppointmentController()
        {
            _objCustomerDB = new CustomerDB();
            _objGeneralDB = new GeneralDB();
            _objStaffRatingDB = new StaffRatingDB();
            _objCustomerRescheduleDB = new CustomerRescheduleDB();
            _objCustomerTeamAssignDB = new CustomerTeamAssignDB();
            _objCommonCustomerTimeLineDB = new CommonCustomerTimeLineDB();
            _objCustomerSupportDB = new CustomerSupportDB();
            _objCustomerAlertDB = new CustomerAlertsDB();
            _objSendMessagesDB = new SendMessagesDB();

        }

        // GET: Admin/Appointment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Slots()
        {
            return View();
        }

        public ActionResult Customer()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Notifications()
        {
            return View();
        }

        public ActionResult CustomerSupport()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetCustomerSupportDetails()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerSupportDB.GetCustomerSupport(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpPost]
        public ActionResult SetApprovalStatus(UpdateStatusModel status)
        {
            try
            {
                status.UpdatedOn = DateTime.Now;
                status.UpdatedBy = User.Identity.Name;
                var result = _objCustomerSupportDB.UpdateStatus(status);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        


        [HttpGet]
        public ActionResult GetCustomers()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerDB.GetCustomers(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCustomersTodaysForAdmin()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerDB.GetCustomersTodaysForAdmin(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCustomersByDateForAdmin(string Date)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                DateTime StartDate = DateTime.ParseExact(Date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                var result = _objCustomerDB.GetCustomersByDateForAdmin(objUser.uID, StartDate);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCustomerDetail(int? cuID, int? custODID)
        {
            try
            {
                var result = _objCustomerDB.GetCustomerDetail(cuID, custODID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult GetCustomerAlertsByStatus(int? Status)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerAlertDB.GetCustomerAlertsByStatus((int)objUser.uID,Status);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetCustomersByCustomerID(int? cuID)
        {
            try
            {
                var result = _objCustomerDB.GetCustomersByCustomerID(cuID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCustomersForTimeLineCustomerID(int? custID, int? custODID)
        {
            try
            {
                var result = _objCustomerDB.GetCustomersForTimeLineCustomerID(custID,custODID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCustomersBySubCategory(int? cuID, int? catID, int? catsubID)
        {
            try
            {
                var result = _objCustomerDB.GetCustomersBySubCategory(cuID, catID, catsubID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCustomersByServiceSubCategory(int? cuID, int? catID, int? catsubID)
        {
            try
            {
                var result = _objCustomerDB.GetCustomersByServiceSubCategory(cuID, catID, catsubID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AssignTeamCustomer(AssignTeamCustomer customer)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                customer.UpdatedBy = User.Identity.Name;
                customer.UpdatedOn = DateTime.Now;
                customer.UpdatedRole = objUser.rID;
                result = _objCustomerDB.AssignTeamCustomer(customer);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDropdownForCustomerIDs() 
        {
            try 
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerDB.GetDropdownForCustomerIDs(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex) 
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetStaffCustomerRatingForAdmin()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objStaffRatingDB.GetStaffCustomerRatingForAdmin(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetStaffCustomerRatingForAdminDetails(int? custID, int? custODID, int? custTDID)
        {
            try
            {
                var result = _objStaffRatingDB.GetStaffCustomerRatingForAdminDetails(custID,custODID,custTDID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCustomerDetailsForComplain(int? custID, int? custODID)
        {
            try
            {
                var result = _objCustomerDB.GetCustomerDetailsForComplain(custID, custODID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetCustomerDeepAndSpecializeTeamAssign(CustomerDeepAndSpecializeTeamAssignModel customer)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerTeamAssignDB.GetCustomerDeepAndSpecializeTeamAssign(customer);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult SuspendCustomerService(SuspendCustomerServiceModel customer)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                customer.IsActive = false;
                customer.IsDelete = true;
                customer.UpdatedBy = User.Identity.Name;
                customer.UpdatedOn = DateTime.Now;
                customer.UpdatedRole = objUser.rID;
                var result = _objCustomerDB.SuspendCustomerService(customer);
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
                var result = _objCustomerRescheduleDB.SaveReschedule(customer);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                string result = ex.Message;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult GetRemaningDateOfCustomer(CustomerBookedStartDates booked)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
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

        [HttpPost]
        public ActionResult SendNotificationToCustomer(SendNotificationToCustomerModel customer)
        {
            try
            {
                var result = _objSendMessagesDB.SendNotificationToCustomer(customer);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult SendNotificationToCleaner(SendNotificationToCleanerModel staff)
        {
            try
            {
                var result = _objSendMessagesDB.SendNotificationToCleaner(staff);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }
    }
}