using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UHSForm.DAL;
using UHSForm.Models;

namespace UHSForm.Areas.Admin.Controllers
{
    public class ReportsController : Controller
    {
        private GeneralDB _objGeneralDB;
        private RevenueReportDB _objRevenueReportDB;
        private DriverAssignDB _objDriverAssignDB;
        private CustomerReportDB _objCustomerReportDB;
        private PropertyDB _objPropertyDB;
        private TeamReportDB _objTeamReportDB;
        private ServiceReportDB _objServiceReportDB;

        public ReportsController()
        {
            _objGeneralDB = new GeneralDB();
            _objRevenueReportDB = new RevenueReportDB();
            _objDriverAssignDB = new DriverAssignDB();
            _objCustomerReportDB = new CustomerReportDB();
            _objPropertyDB = new PropertyDB();
            _objTeamReportDB = new TeamReportDB();
            _objServiceReportDB = new ServiceReportDB();
        }



        // GET: Admin/Reports
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

        public ActionResult Logistics()
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
        public ActionResult TotalRevenue() 
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objRevenueReportDB.TotalRevenue(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult TotalRetriveRevenue()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objRevenueReportDB.TotalRetriveRevenue(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CustomerReport()
        {
            return View();
        }



        [HttpGet]
        public ActionResult GetRevenueReportForToday()
        {

            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objRevenueReportDB.GetRevenueReportForToday((int)objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetRevenueReportByDate(RevenueReportModel report)
        {

            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                report.uID = objUser.uID;
                DateTime? StartDate=null;
                if (report.StartDate!=null) {
                   StartDate = DateTime.ParseExact(report.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                }
                
                var result = _objRevenueReportDB.GetRevenueReportByDate(report,StartDate);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        //[HttpPost]
        //public ActionResult GetRevenueReport(RevenueReportModel report)
        //{
        //    try
        //    {
        //        var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
        //        report.uID = objUser.uID;
        //        var result = _objRevenueReportDB.GetRevenueReport(report);
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        string result = "Exception";
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //}



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
                var result = _objDriverAssignDB.GetGrantChartForDriverWithDate(objUser.uID,Date);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetCustomerCount(CustomerReportModel customer)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                customer.uID = objUser.uID;
                var result = await _objCustomerReportDB.GetCustomerCount(customer);
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
        public async Task<ActionResult> GetCustomerDataForGraph(CustomerReportModel customer)
        {
            try
            {
                var objUser =  _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                customer.uID = objUser.uID;
                var result = await _objCustomerReportDB.GetCustomerDataForGraph(customer);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpGet]
        public async Task<ActionResult> GetCustomerDataForTable(CustomerReportModel customer)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                customer.uID = objUser.uID;
                var result = await _objCustomerReportDB.GetCustomerCountForTable(customer);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpGet]
        public ActionResult GetTotalCustomerCount()
        {
            try
            {
                var result = _objCustomerReportDB.TotalCustomersCount();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                string result = "0";
                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }

        //[HttpGet]
        //public ActionResult GetAverageRatingForTeams()
        //{
        //    try
        //    {
        //        var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
        //        var result = _objTeamReportDB.GetAverageRatingForTeams(objUser.uID);
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        string result = "Exception";
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //}

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
                var result = _objTeamReportDB.GetTeamsServiceIndividualCountByDate(objUser.uID,StartDate);
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
        //public ActionResult GetTeamsCountForTowerByFilter(string pID,string vID)
        //{
        //    try
        //    {
        //        var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
        //        var result = _objTeamReportDB.GetTeamsCountForTowerByFilter(objUser.uID,pID,vID);
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
                DateTime StartDate= DateTime.ParseExact(Date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
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
                var result = _objTeamReportDB.RoasterTeamsByDate(objUser.uID,Date);
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