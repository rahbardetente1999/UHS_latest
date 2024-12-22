using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;

namespace UHSForm.DAL
{
    public class CustomerInVoiceDB
    {
        private UHSEntities UhDB;

        public CustomerInVoiceDB()
        {
            UhDB = new UHSEntities();
        }

        public string GetCustomerLastInvoice(int? uID)
        {
            string result = null;
            int? CountInvoice = UhDB.CustomerInovices.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).Count();
            if (CountInvoice == 0)
            {
                result = "1";
            }
            else
            {
                var objCustomerInvoice = UhDB.CustomerInovices.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).OrderByDescending(s => s.custInID).FirstOrDefault();
                int Invoice = Convert.ToInt32(objCustomerInvoice.InvoiceNumber);
                Invoice = Invoice + 1;
                result = Invoice.ToString();

            }
            return result;
        }
    }
}