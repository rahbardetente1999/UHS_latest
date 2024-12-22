using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models;
using UHSForm.Models.Data;

namespace UHSForm.DAL
{
    public class CustomerRatingDB
    {

        private UHSEntities UhDB;

        public CustomerRatingDB()
        {
            UhDB = new UHSEntities();
        }

        public int? CustomerServiceRating(CustomerServiceRatingModel customer)
        {
            int? result = null;
            CustomerFeedback objCustomerFeedback = new CustomerFeedback();
            objCustomerFeedback.cuID = customer.cuID;
            objCustomerFeedback.custODID = customer.custODID;
            objCustomerFeedback.custTDID = customer.custTDID;
            objCustomerFeedback.Rating = customer.Rating;
            objCustomerFeedback.Feedback = customer.Feedback;
            objCustomerFeedback.IsActive = customer.IsActive;
            objCustomerFeedback.IsDelete = customer.IsDelete;
            objCustomerFeedback.CreatedBy = customer.CreatedBy;
            objCustomerFeedback.CreatedOn = customer.CreatedOn;
            objCustomerFeedback.CreatedRole = customer.CreatedRole;
            UhDB.CustomerFeedbacks.Add(objCustomerFeedback);
            UhDB.SaveChanges();
            result = objCustomerFeedback.custfdbID;
            return result;
        }

        public GetCustomerServiceRatingModel GetCustomerServiceRatings(int? cuID, int? custODID)
        {
            GetCustomerServiceRatingModel result = new GetCustomerServiceRatingModel();

            result = UhDB.CustomerFeedbacks.Where(x => x.cuID == cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select(p => new GetCustomerServiceRatingModel
                     {
                         CustomerName = p.Customer.Name,
                         TeamName = p.CustomerOfficalDetail.teamID != null ? p.CustomerOfficalDetail.Team.Name : null,
                         StaffName = p.CustomerOfficalDetail.stfID != null ? p.CustomerOfficalDetail.Staff.Name : null,
                         Rating = p.Rating,
                         Feedback = p.Feedback,
                         cuID = p.cuID,
                         custTDID=p.custTDID,
                         custODID = p.custODID,
                         custfdbID = p.custfdbID,
                         CreatedBy = p.CreatedBy,
                         CreatedOn = p.CreatedOn
                     }).FirstOrDefault();
            return result;
        }
    }
}