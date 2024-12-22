using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models;
using UHSForm.Models.Data;

namespace UHSForm.DAL
{
    public class CustomerTransactionsDB
    {
        private UHSEntities UhDB;

        public CustomerTransactionsDB()
        {
            UhDB = new UHSEntities();
        }

        public string UpdatePaymentStatus(CustomerPaymentModel customer)
        {
            string result = null;
            var objCustomerPayment = UhDB.CustomerTransactions.Where(x => x.PayementID == customer.PaymentID && x.TransactionID == customer.TransactionID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            objCustomerPayment.PaymentStatus = customer.PaymentStatus;
            objCustomerPayment.UpdatedBy = objCustomerPayment.Customer.Name;
            objCustomerPayment.UpdatedOn = customer.UpdatedOn;
            UhDB.SaveChanges();
            result = UhDB.CustomerPaymentStatus.Where(x => x.custPSID == customer.PaymentStatus && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
            return result;
        }
    }
}