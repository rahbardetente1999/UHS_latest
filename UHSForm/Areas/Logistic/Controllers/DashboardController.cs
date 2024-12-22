using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UHSForm.DAL;
using UHSForm.Models;

namespace UHSForm.Areas.Logistic.Controllers
{
    //[Authorize(Roles = "18")]
    [Authorize]
    public class DashboardController : Controller
    {
        private CustomerDB _objCustomerDB;
        private GeneralDB _objGeneralDB;
        private CustomerRescheduleDB _objCustomerRescheduleDB;
        private CustomerTeamAssignDB _objCustomerTeamAssignDB;
        private DriverAssignDB _objDriverAssignDB;
        private TeamReportDB _objTeamReportDB;
        private ServiceReportDB _objServiceReportDB;
        private PropertyAreaDB _objPropertyAreaDB;
        private CommonPropertyDB _objCommonPropertyDB;
        private SubAreaDB _objSubAreaDB;
        private TeamsDB _objTeamsDB;
        private PropertyDB _objPropertyDB;
        private SendMessagesDB _objSendMessagesDB;
        private CommonCustomerTimeLineDB _objCommonCustomerTimeLineDB;
        public DashboardController()
        {
            _objCustomerDB = new CustomerDB();
            _objGeneralDB = new GeneralDB();
            _objDriverAssignDB = new DriverAssignDB();
            _objPropertyDB = new PropertyDB();
            _objCustomerRescheduleDB = new CustomerRescheduleDB();
            _objCustomerTeamAssignDB = new CustomerTeamAssignDB();
            _objTeamReportDB = new TeamReportDB();
            _objServiceReportDB = new ServiceReportDB();
            _objPropertyAreaDB = new PropertyAreaDB();
            _objCommonPropertyDB = new CommonPropertyDB();
            _objSubAreaDB = new SubAreaDB();
            _objTeamsDB = new TeamsDB();
            _objSendMessagesDB = new SendMessagesDB();
            _objCommonCustomerTimeLineDB = new CommonCustomerTimeLineDB();
        }
        // GET: Logistic/Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ServiceReport()
        {
            return View();
        }

        public ActionResult TeamDetails()
        {
            return View();
        }

        public ActionResult TeamRoaster()
        {
            return View();
        }

        public ActionResult TeamAvailability()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetReportForAverageRatingAndServiceCountForTeams()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objTeamReportDB.GetReportForAverageRatingAndServiceCountForTeams(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }




        [HttpGet] 
        public ActionResult GetTeamsByStaffDetails()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objTeamReportDB.GetTeamsByStaffDetails(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult GetRescheduleingList()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objTeamReportDB.GetRescheduleingList(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetCancelledReschedulesLists()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objTeamReportDB.GetCancelledReschedulesLists(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetTeamRoasterForTable()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objTeamReportDB.GetTeamRoasterForTable(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetTeamRoasterByDate(string Date)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                DateTime StartDate = DateTime.ParseExact(Date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                var result = _objTeamReportDB.GetTeamRoasterForTableByDate(objUser.uID, StartDate);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetTeamRoasters()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objTeamReportDB.GetTeamRoasters(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetTeamRoastersByDate(string Date)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                DateTime StartDate = DateTime.ParseExact(Date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                var result = _objTeamReportDB.GetTeamRoastersByDate(objUser.uID, StartDate);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        //[HttpGet]
        //public ActionResult GetTeamRoasterForTableByFilters(GetTeamReportModel team)
        //{
        //    try
        //    {
        //        var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
        //        team.uID = (int)objUser.uID;
        //        var result = _objTeamReportDB.GetTeamRoasterForTableByFilters(team);
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        string result = "Exception";
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //}

        [HttpPost]
        public ActionResult GetTeamAvailableByDate(GetTeamAvailableByDate times)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                times.uID = (int)objUser.uID;
                var result = _objTeamReportDB.GetTeamAvailableByDate(times);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
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
        public ActionResult GetCustomersByDateForAdmin(DateTime Date)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCustomerDB.GetCustomersByDateForAdmin(objUser.uID, Date);
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
        public ActionResult GetGrantChartForDriver()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objDriverAssignDB.GetGrantChartForDriver(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetGrantChartForDriverWithDate(DateTime? Date)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objDriverAssignDB.GetGrantChartForDriverWithDate(objUser.uID, Date);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetAverageRatingForTeams()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objTeamReportDB.GetAverageRatingForTeams(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetTeamsServiceCount()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objTeamReportDB.GetTeamsServiceCount(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetTeamsServiceIndividualCount()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objTeamReportDB.GetTeamsServiceIndividualCount(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetTeamsServiceIndividualCountByDate(string Date)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                DateTime StartDate = DateTime.ParseExact(Date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                var result = _objTeamReportDB.GetTeamsServiceIndividualCountByDate(objUser.uID, StartDate);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetTeamsCountForTower(string pID,string tID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objTeamReportDB.GetTeamsCountForTower(objUser.uID,pID,tID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }



        //[HttpGet]
        //public ActionResult GetTeamsCountForTowerByFilter(string pID, string vID)
        //{
        //    try
        //    {
        //        var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
        //        var result = _objTeamReportDB.GetTeamsCountForTowerByFilter(objUser.uID, pID, vID);
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        string result = "Exception";
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //}

        [HttpGet]
        public ActionResult GetCountTotalService()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objServiceReportDB.GetCountTotalService(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetCountService(ServiceCount service)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                service.uID = objUser.uID;
                var result = _objServiceReportDB.GetCountService(service);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetServiceData(ServiceCount service)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                service.uID = objUser.uID;
                var result = _objServiceReportDB.GetServiceData(service);
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
        public ActionResult GetSubAreaDropdown()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objSubAreaDB.GetSubAreaDropdown(objUser.uID);
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
        public ActionResult GetPropertyDropDownForReports()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objPropertyDB.GetPropertyDropDownForReports(objUser.uID);
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

        [HttpGet]
        public ActionResult GetTeamsDropDown()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objTeamsDB.GetTeamsDropDown(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);

            }
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

        [HttpGet]
        public ActionResult GetTeamsCountByToday()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objTeamReportDB.GetTeamsCountByToday(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetTeamsCountForTowerByDate(string Date)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                DateTime StartDate = DateTime.ParseExact(Date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                var result = _objTeamReportDB.GetTeamsCountForTowerByDate(objUser.uID, StartDate);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult RoasterTeams(int? uID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objTeamReportDB.RoasterTeams(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult RoasterTeamsByDate(string Date)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objTeamReportDB.RoasterTeamsByDate(objUser.uID, Date);
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