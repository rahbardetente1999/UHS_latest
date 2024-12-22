using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UHSForm.DAL;
using UHSForm.Models;
namespace UHSForm.Controllers
{
    [AllowAnonymous]
    public class CustomerRenewalController : Controller
    {
        private GeneralDB _objGeneralDB;
        private CustomerRenewalDB _objCustomerRenewalDB;
        public CustomerRenewalController()
        {
            _objGeneralDB = new GeneralDB();
            _objCustomerRenewalDB = new CustomerRenewalDB();
        }

        // GET: CustomerRenewal
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetDecryptValues(GetDecryptValues values)
        {
            try
            {
               
                if (values.CustomerID.Contains("%2"))
                {
                    values.CustomerID = values.CustomerID.Replace("%2", "");
                    values.CustomerID = HttpUtility.HtmlDecode(_objGeneralDB.Decrypt((values.CustomerID), "Lets1Make2It3Happen4"));
                    values.CustomerID = values.CustomerID.Replace("\0", "");
                }
                else
                {
                    values.CustomerID = HttpUtility.HtmlDecode(_objGeneralDB.Decrypt((values.CustomerID), "Lets1Make2It3Happen4"));
                    values.CustomerID = values.CustomerID.Replace("\0", "");
                }
                if (values.PropertyAreaID.Contains("%2"))
                {
                    values.PropertyAreaID = values.PropertyAreaID.Replace("%2", "");
                    values.PropertyAreaID = HttpUtility.HtmlDecode(_objGeneralDB.Decrypt((values.PropertyAreaID), "Lets1Make2It3Happen4"));
                    values.PropertyAreaID = values.PropertyAreaID.Replace("\0", "");
                }
                else
                {
                    values.PropertyAreaID = HttpUtility.HtmlDecode(_objGeneralDB.Decrypt((values.PropertyAreaID), "Lets1Make2It3Happen4"));
                    values.PropertyAreaID = values.PropertyAreaID.Replace("\0", "");
                }
                if (values.PropertyID.Contains("%2"))
                {
                    values.PropertyID = values.PropertyID.Replace("%2", "");
                    values.PropertyID = HttpUtility.HtmlDecode(_objGeneralDB.Decrypt((values.PropertyID), "Lets1Make2It3Happen4"));
                    values.PropertyID = values.PropertyID.Replace("\0", "");
                }
                else
                {
                    values.PropertyID = HttpUtility.HtmlDecode(_objGeneralDB.Decrypt((values.PropertyID), "Lets1Make2It3Happen4"));
                    values.PropertyID = values.PropertyID.Replace("\0", "");
                }
                if (values.PropertyResidencyID.Contains("%2"))
                {
                    values.PropertyResidencyID = values.PropertyResidencyID.Replace("%2", "");
                    values.PropertyResidencyID = HttpUtility.HtmlDecode(_objGeneralDB.Decrypt((values.PropertyResidencyID), "Lets1Make2It3Happen4"));
                    values.PropertyResidencyID = values.PropertyResidencyID.Replace("\0", "");
                }
                else
                {
                    values.PropertyResidencyID = HttpUtility.HtmlDecode(_objGeneralDB.Decrypt((values.PropertyResidencyID), "Lets1Make2It3Happen4"));
                    values.PropertyResidencyID = values.PropertyResidencyID.Replace("\0", "");
                }
                if (values.AppartmentNo.Contains("%2"))
                {
                    values.AppartmentNo = values.AppartmentNo.Replace("%2", "");
                    values.AppartmentNo = HttpUtility.HtmlDecode(_objGeneralDB.Decrypt((values.AppartmentNo), "Lets1Make2It3Happen4"));
                    values.AppartmentNo = values.AppartmentNo.Replace("\0", "");
                }
                else
                {
                    values.AppartmentNo = HttpUtility.HtmlDecode(_objGeneralDB.Decrypt((values.AppartmentNo), "Lets1Make2It3Happen4"));
                    values.AppartmentNo = values.AppartmentNo.Replace("\0", "");
                }
                if (values.PropertyTypeID.Contains("%2"))
                {
                    values.PropertyTypeID = values.PropertyTypeID.Replace("%2", "");
                    values.PropertyTypeID = HttpUtility.HtmlDecode(_objGeneralDB.Decrypt((values.PropertyTypeID), "Lets1Make2It3Happen4"));
                    values.PropertyTypeID = values.PropertyTypeID.Replace("\0", "");
                }
                else
                {
                    values.PropertyTypeID = HttpUtility.HtmlDecode(_objGeneralDB.Decrypt((values.PropertyTypeID), "Lets1Make2It3Happen4"));
                    values.PropertyTypeID = values.PropertyTypeID.Replace("\0", "");
                }
                return Json(values, JsonRequestBehavior.AllowGet);
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
                var result = _objCustomerRenewalDB.GetCustomerRenewalInfo(cuID, propaID, vID, proprestID, propTypeID);
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
                customer.IsActive = true;
                customer.IsDelete = false;
                customer.CreatedBy = User.Identity.Name;
                customer.CreatedOn = DateTime.Now;
                customer.CreatedRole = 14;
                customer.IsAdmin = true;
                var result = _objCustomerRenewalDB.UpdateCustomerRenewal(customer);
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