using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;

namespace UHSForm.DAL
{
    public class CustomerSupportDB
    {
        private UHSEntities UhDB;

        public CustomerSupportDB()
        {
            UhDB = new UHSEntities();
        }

        public IEnumerable<GetCustomerSupportModel> GetCustomerSupport(int? uID)
        {
            List<GetCustomerSupportModel> result = new List<GetCustomerSupportModel>();
            result = UhDB.CustomerSupports.Where(x => x.Customer.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select((p, q) => new GetCustomerSupportModel
                     {
                         Title = p.Title,
                         ServiceDate = Convert.ToDateTime(p.ServiceDate).ToString("MM/dd/yyyy"),
                         Remarks = p.Remarks,
                         ActionForOther = p.ActionForOther,
                         CustomerName = p.Customer.Name,
                         CustomerSupportActionFor = p.custSAID != null ? p.CustomerSupportActionFor.Name : "N/A",
                         CustomerSupportServiceTicketType = p.custSTTID != null ? p.CustomerSupportTicketType.Name : "N/A",
                         CustomerSupportServiceType = p.custSSTID != null ? p.CustomerSupportServiceType.Name : "N/A",
                         CustomerSupportTaskStatus = p.custSSID != null ? p.ApprovalStatu.Name : "N/A",
                         custID = p.custID,
                         custSSID = p.custSSID,
                         custSSTID = p.custSSTID,
                         custSAID = p.custSAID,
                         custSID = p.custSID,
                         custSTTID = p.custSTTID,
                         CreatedBy = p.CreatedBy,
                         CreatedOn = p.CreatedOn,
                         index = q + 1,
                         Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.FileUse == 12 && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                  UhDB.Files.Where(x => x.cuiD == p.custID && x.FileUse == 12 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                  .Select(r => new GetFileDetails
                                  {
                                      Name = r.Filename,
                                      Size = r.FileSize,
                                      ContentType = r.FileContentType,
                                      Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerSupport/" + r.FileFieldName
                                  }).ToList() : null
                     }).ToList();
            return result;
        }

        public IEnumerable<GetCustomerSupportModel> GetCustomerSupportForCustomer(int? cuID) 
        {
            List<GetCustomerSupportModel> result = new List<GetCustomerSupportModel>();
            result = UhDB.CustomerSupports.Where(x => x.custID == cuID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                    .Select((p, q) => new GetCustomerSupportModel
                    {
                        Title = p.Title,
                        ServiceDate = Convert.ToDateTime(p.ServiceDate).ToString("MM/dd/yyyy"),
                        Remarks = p.Remarks,
                        ActionForOther = p.ActionForOther,
                        CustomerName = p.Customer.Name,
                        CustomerSupportActionFor = p.custSAID != null ? p.CustomerSupportActionFor.Name : "N/A",
                        CustomerSupportServiceTicketType = p.custSTTID != null ? p.CustomerSupportTicketType.Name : "N/A",
                        CustomerSupportServiceType = p.custSSTID != null ? p.CustomerSupportServiceType.Name : "N/A",
                        CustomerSupportTaskStatus = p.custSSID != null ? p.ApprovalStatu.Name : "N/A",
                        custID = p.custID,
                        custSSID = p.custSSID,
                        custSSTID = p.custSSTID,
                        custSAID = p.custSAID,
                        custSID = p.custSID,
                        custSTTID = p.custSTTID,
                        CreatedBy = p.CreatedBy,
                        CreatedOn = p.CreatedOn,
                        index = q + 1,
                        Files = UhDB.Files.Where(x => x.cuiD == cuID && x.FileUse == 12 && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                  UhDB.Files.Where(x => x.cuiD == cuID && x.FileUse == 12 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                  .Select(r => new GetFileDetails
                                  {
                                      Name = r.Filename,
                                      Size = r.FileSize,
                                      ContentType = r.FileContentType,
                                      Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerSupport/" + r.FileFieldName
                                  }).ToList() : null
                    }).ToList();
            return result;
        }

        public int CreateCustomerSupportTicket(CustomerSupportModel customersupport)
        {
            int result = 0;
            CustomerSupport objcustomersupport = new CustomerSupport();
            objcustomersupport.Title = customersupport.TicketTitle;
            objcustomersupport.custSTTID = customersupport.custSTTID;
            objcustomersupport.Remarks = customersupport.Remarks;

            if (customersupport.custSTTID == 2)
            {
                objcustomersupport.ServiceDate = customersupport.ServiceDate;
                objcustomersupport.custSAID = customersupport.custSAID;
                objcustomersupport.custSSTID = customersupport.custSSTID;
                objcustomersupport.ActionForOther = customersupport.ActionFor;
            }

            objcustomersupport.custSSID = 1;
            objcustomersupport.custID = customersupport.custID;
            objcustomersupport.IsActive = customersupport.IsActive;
            objcustomersupport.IsDelete = customersupport.IsDelete;
            objcustomersupport.CreatedBy = customersupport.CreatedBy;
            objcustomersupport.CreatedOn = customersupport.CreatedOn;
            UhDB.CustomerSupports.Add(objcustomersupport);
            Save();
            result = objcustomersupport.custSID;
            return result;
        }

        public string UpdateStatus(UpdateStatusModel status)
        {
            var objCustomerSupport = UhDB.CustomerSupports.FirstOrDefault(x => x.custSID == status.custSID);
            objCustomerSupport.custSSID = status.custSSID;
            objCustomerSupport.UpdatedBy = status.UpdatedBy;
            objCustomerSupport.UpdatedOn = status.UpdatedOn;
            UhDB.SaveChanges();

            return "SUCCESS";
        }

        private void Save()
        {
            UhDB.SaveChanges();
        }
    }
}