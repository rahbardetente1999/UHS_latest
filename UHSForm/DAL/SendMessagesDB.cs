using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;
using System.IO;

namespace UHSForm.DAL
{
    public class SendMessagesDB
    {
        private UHSEntities UhDB;
        private GeneralDB objGeneralDB;

        public SendMessagesDB()
        {
            UhDB = new UHSEntities();
            objGeneralDB = new GeneralDB();
        }

        public string SendNotificationToCustomer(SendNotificationToCustomerModel customer)
        {
            string result = null;
            var objCustomer = UhDB.Customers.Where(x => x.cuID == customer.custID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            
            if (customer.IsEmail == true)
            {
                string body = EmailBody(customer.Message, objCustomer.Name);
                result = objGeneralDB.SentEmailFromAmazon(objCustomer.Email, body, customer.Subject, objCustomer.Name);
            }
            else
            {
                string Mobile = objCustomer.PhoneCode + objCustomer.Mobile;
                string res = objGeneralDB.SendSMS(Mobile, customer.Message);
                if (res == "OK")
                {
                    result = "SUCCESS";
                }
                else
                {
                    result = res;
                }
            }
            return result;
        }

        public string SendNotificationToCleaner(SendNotificationToCleanerModel staff)
        {
            string result = null;
            if (staff.teamID == null)
            {
                var objStaff = UhDB.Staffs.Where(x => x.stfID == staff.stfID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                if (staff.IsEmail == true)
                {
                    string body = EmailBody(staff.Message, objStaff.Name);
                    result = objGeneralDB.SentEmailFromAmazon(objStaff.Email, body, staff.Subject, objStaff.Name);
                }
                else
                {
                    string Mobile = objStaff.PhoneCode + objStaff.Mobile;
                    string res = objGeneralDB.SendSMS(Mobile, staff.Message);
                    if (res == "OK")
                    {
                        result = "SUCCESS";
                    }
                    else
                    {
                        result = res;
                    }
                }
            }
            else 
            {
                var objStaffs = UhDB.StaffTeams.Where(x => x.teamID == staff.teamID && x.IsActive == true && x.IsDelete == false).ToList();
                foreach (var objStaff in objStaffs)
                {
                    if (staff.IsEmail == true)
                    {
                        string body = EmailBody(staff.Message, objStaff.Staff.Name);
                        result = objGeneralDB.SentEmailFromAmazon(objStaff.Staff.Email, body, staff.Subject, objStaff.Staff.Name);
                    }
                    else
                    {
                        string Mobile = objStaff.Staff.PhoneCode + objStaff.Staff.Mobile;
                        string res = objGeneralDB.SendSMS(Mobile, staff.Message);
                        if (res == "OK")
                        {
                            result = "SUCCESS";
                        }
                        else
                        {
                            result = res;
                        }
                    }
                }
            }

            return result;
        }


        private string EmailBody(string message, string name)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/CustomerNotification.html")))//using streamreader for reading my htmltemplate  
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{message}", message); //replacing the required things  
            body = body.Replace("{name}", name);
            return body;

        }
    }
}