using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models;
using UHSForm.Models.Data;

namespace UHSForm.DAL
{
    public class CustomerComplaintDB
    {
        private UHSEntities UhDB;

        public CustomerComplaintDB()
        {
            UhDB = new UHSEntities();
        }

        public int? CreateCustomerComplaint(CustomerComplaintModel customer)
        {
            int? result = null;
            CustomerComplaint objCustomerComplaint = new CustomerComplaint();
            objCustomerComplaint.cuID = customer.cuID;
            objCustomerComplaint.custODID = customer.custODID;
            objCustomerComplaint.Remarks = customer.Remarks;
            objCustomerComplaint.IsActive = customer.IsActive;
            objCustomerComplaint.IsDelete = customer.IsDelete;
            objCustomerComplaint.CreatedBy = customer.CreatedBy;
            objCustomerComplaint.CreatedOn = customer.CreatedOn;
            objCustomerComplaint.CreatedRole = customer.CreatedRole;
            UhDB.CustomerComplaints.Add(objCustomerComplaint);
            UhDB.SaveChanges();
            result = objCustomerComplaint.custComID;
            return result;
        }

        public GetCustomerComplaintModel GetCustomerComplaints(int? cuID, int custODID)
        {
            GetCustomerComplaintModel result = new GetCustomerComplaintModel();
            result = UhDB.CustomerComplaints.Where(x => x.cuID == cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                    .Select(p => new GetCustomerComplaintModel
                    {
                        custComID = p.custComID,
                        Remarks = p.Remarks,
                        CreatedBy = p.CreatedBy,
                        CreatedOn = p.CreatedOn,
                        Files = UhDB.Files.Where(x => x.cuiD == cuID && x.custODID == custODID && x.FileUse == 8 && x.IsActive == true && x.IsDelete == false).Count() != 0 ? UhDB.Files.Where(x => x.cuiD == cuID && x.custODID == custODID && x.FileUse == 8 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                              .Select(r => new GetFileDetails
                              {
                                  Name = r.Filename,
                                  Size = r.FileSize,
                                  ContentType = r.FileContentType,
                                  Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + r.FileFieldName
                              }).ToList() : null
                    }).FirstOrDefault();
            return result;
        }
    }
}