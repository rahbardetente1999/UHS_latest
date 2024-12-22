using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;

namespace UHSForm.DAL
{
    public class CustomerAlertsDB
    {
        private UHSEntities UhDB;

        public CustomerAlertsDB()
        {
            UhDB = new UHSEntities();
        }

        public IEnumerable<CustomerAlertsModel> GetCustomerAlerts(int uID)
        {
            List<CustomerAlertsModel> result = new List<CustomerAlertsModel>();
            result = UhDB.CustomerAlerts.Where(x => x.Customer.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select((p, q) => new CustomerAlertsModel
                     {
                         Message = p.Message,
                         CustomerName = p.custID != null ? p.Customer.Name : "N/A",
                         AlertType = p.custATID != null ? p.CustomerAltersType.Name : "N/A",
                         MainCategroy=p.catID!=null?p.MainCategory.Name:"N/A",
                         SubCategory=p.catsubID!=null?p.SubCategory.Name:"N/A",
                         PropertyName=p.vID!=null?p.Venture.Name:"N/A",
                         catID=p.catID,
                         catsubID=p.catsubID,
                         custATID = p.custATID,
                         custID = p.custID,
                         custAID = p.custAID,
                         vID=p.vID
                     }).ToList();


            return result;
        }

        public IEnumerable<CustomerAlertsModel> GetCustomerAlertsForCustomer(int cuID)
        {
            List<CustomerAlertsModel> result = new List<CustomerAlertsModel>();
            result = UhDB.CustomerAlerts.Where(x => x.custID == cuID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select((p, q) => new CustomerAlertsModel
                     {
                         Message = p.Message,
                         CustomerName = p.custID != null ? p.Customer.Name : "N/A",
                         AlertType = p.custATID != null ? p.CustomerAltersType.Name : "N/A",
                         MainCategroy = p.catID != null ? p.MainCategory.Name : "N/A",
                         SubCategory = p.catsubID != null ? p.SubCategory.Name : "N/A",
                         PropertyName = p.vID != null ? p.Venture.Name : "N/A",
                         catID = p.catID,
                         catsubID = p.catsubID,
                         custATID = p.custATID,
                         custID = p.custID,
                         custAID = p.custAID,
                         vID = p.vID
                     }).ToList();


            return result;
        }

        public IEnumerable<CustomerAlertsModel> GetCustomerAlertsByStatus(int uID,int? Status)
        {
            List<CustomerAlertsModel> result = new List<CustomerAlertsModel>();
            result = UhDB.CustomerAlerts.Where(x => x.Customer.uID == uID && x.custATID==Status && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select((p, q) => new CustomerAlertsModel
                     {
                         Message = p.Message,
                         CustomerName = p.custID != null ? p.Customer.Name : "N/A",
                         AlertType = p.custATID != null ? p.CustomerAltersType.Name : "N/A",
                         MainCategroy = p.catID != null ? p.MainCategory.Name : "N/A",
                         SubCategory = p.catsubID != null ? p.SubCategory.Name : "N/A",
                         PropertyName = p.vID != null ? p.Venture.Name : "N/A",
                         catID = p.catID,
                         catsubID = p.catsubID,
                         custATID = p.custATID,
                         custID = p.custID,
                         custAID = p.custAID,
                         vID = p.vID
                     }).ToList();


            return result;
        }

        public IEnumerable<CustomerAlertsModel> GetCustomerAlertsByStatusForCustomer(int cuID, int? Status)
        {
            List<CustomerAlertsModel> result = new List<CustomerAlertsModel>();
            result = UhDB.CustomerAlerts.Where(x => x.custID == cuID && x.custATID == Status && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select((p, q) => new CustomerAlertsModel
                     {
                         Message = p.Message,
                         CustomerName = p.custID != null ? p.Customer.Name : "N/A",
                         AlertType = p.custATID != null ? p.CustomerAltersType.Name : "N/A",
                         MainCategroy = p.catID != null ? p.MainCategory.Name : "N/A",
                         SubCategory = p.catsubID != null ? p.SubCategory.Name : "N/A",
                         PropertyName = p.vID != null ? p.Venture.Name : "N/A",
                         catID = p.catID,
                         catsubID = p.catsubID,
                         custATID = p.custATID,
                         custID = p.custID,
                         custAID = p.custAID,
                         vID = p.vID
                     }).ToList();


            return result;
        }
    }
}