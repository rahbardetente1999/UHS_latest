using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UHSForm.DAL;
using UHSForm.Models;

namespace UHSForm.Areas.Operation.Controllers
{
    
    public class DashboardController : Controller
    {
        private GeneralDB _objGeneralDB;
        private CustomerReportDB _objCustomerReportDB;
        private DriverAssignDB _objDriverAssignDB;
        private PropertyDB _objPropertyDB;

        public DashboardController()
        {
            _objGeneralDB = new GeneralDB();
            _objCustomerReportDB = new CustomerReportDB();
            _objDriverAssignDB = new DriverAssignDB();
            _objPropertyDB = new PropertyDB();
        }

        // GET: Operation/Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TeamRoaster()
        {
            return View();
        }

        public ActionResult CustomerReport()
        {
            return View();
        }

        public ActionResult GanttChart()
        {
            return View();
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
        public async Task<ActionResult> GetCustomerDataForGraph(CustomerReportModel customer)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
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
    }
}