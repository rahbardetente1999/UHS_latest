using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models;
using UHSForm.Models.Data;

namespace UHSForm.DAL
{
    public class StaffRatingDB
    {
        private UHSEntities UhDB;

        public StaffRatingDB()
        {
            UhDB = new UHSEntities();
        }

        public int? CreateStaffCustomerRating(StaffCustomerRatingModel staff)
        {
            int? result = null;
            int CountStaffCustomerRating = UhDB.StaffCustomerRatings.Where(x => x.custTDID == staff.custTDID && x.IsActive == true && x.IsDelete == false).Count();
            if (CountStaffCustomerRating == 0)
            {
                if (staff.custISID != null)
                {
                    foreach (var item in staff.custISID)
                    {
                        StaffCustomerRating objStaffCustomerRating = new StaffCustomerRating();
                        objStaffCustomerRating.cuID = staff.cuID;
                        objStaffCustomerRating.custODID = staff.custODID;
                        objStaffCustomerRating.custISID = item;
                        objStaffCustomerRating.custCTID = staff.custCTID;
                        objStaffCustomerRating.custTDID = staff.custTDID;
                        objStaffCustomerRating.Review = staff.Review;
                        objStaffCustomerRating.Rating = staff.Rating;
                        if (item==4) 
                        {
                            objStaffCustomerRating.OtherIssue = staff.OtherIssues;
                        }
                        objStaffCustomerRating.stfID = staff.stfID;
                        objStaffCustomerRating.IsActive = staff.IsActive;
                        objStaffCustomerRating.IsDelete = staff.IsDelete;
                        objStaffCustomerRating.CreatedBy = staff.CreatedBy;
                        objStaffCustomerRating.CreatedOn = staff.CreatedOn;
                        objStaffCustomerRating.CreatedRole = staff.CreatedRole;
                        UhDB.StaffCustomerRatings.Add(objStaffCustomerRating);
                        UhDB.SaveChanges();
                    }
                    result = UhDB.CustomerTimelines.Where(x => x.custTDID == staff.custTDID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().TaskNo;
                }
            }
            else 
            {
                result = -1;
            }

            return result;
        }

        public GetStaffCustomerRatingModel GetStaffCustomerRating(int? custID, int? custODID, int? custTDID, int? stfID)
        {
            GetStaffCustomerRatingModel result = new GetStaffCustomerRatingModel();
            result = UhDB.StaffCustomerRatings.Where(x => x.cuID == custID && x.custODID == custODID && x.custTDID == custTDID && x.stfID == stfID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                    .Select(p => new GetStaffCustomerRatingModel
                    {
                        CustomerName = p.Customer.Name,
                        CreatedBy = p.CreatedBy,
                        CreatedOn = p.CreatedOn,
                        Rating = p.Rating,
                        Review = p.Review,
                        CustomerConditionalType = p.custCTID != null ? p.CustomerConditionalType.Name : "N/A",
                        CustomerIssue = p.custCTID != null ? p.CustomerIssueType.Name : "N/A",
                        custCTID = p.custCTID,
                        custISID = p.custISID
                    }).FirstOrDefault();
            var objTeams = UhDB.StaffTeams.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objTeam in objTeams)
            {
                int? teamID = objTeam.teamID;
                UhDB.StaffCustomerRatings.Where(x => x.cuID == custID && x.custODID == custODID && x.custTDID == custTDID && x.stfID == stfID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                    .Select(p => new GetStaffCustomerRatingModel
                    {
                        CustomerName = p.Customer.Name,
                        CreatedBy = p.CreatedBy,
                        CreatedOn = p.CreatedOn,
                        Rating = p.Rating,
                        Review = p.Review,
                        CustomerConditionalType = p.custCTID != null ? p.CustomerConditionalType.Name : "N/A",
                        CustomerIssue = p.custCTID != null ? p.CustomerIssueType.Name : "N/A",
                        custCTID = p.custCTID,
                        custISID = p.custISID
                    }).FirstOrDefault();
            }
            return result;
        }

        public List<GetStaffCustomerRatingForAdminModel> GetStaffCustomerRatingForAdmin(int? uID)
        {
            List<GetStaffCustomerRatingForAdminModel> result = new List<GetStaffCustomerRatingForAdminModel>();
            var objStaffCustomerRatings = UhDB.StaffCustomerRatings.Where(x => x.Customer.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                    .Select(p => new GetStaffCustomerRatingForAdminModel
                    {
                        CustomerName = p.Customer.Name,
                        MainCategory=p.CustomerOfficalDetail.MainCategory.Name,
                        SubCategory=p.CustomerOfficalDetail.SubCategory.Name,
                        CreatedBy = p.CreatedBy,
                        CreatedOn = p.CreatedOn,
                        Review = p.Review,
                        stfID = p.stfID,
                        StaffName = p.stfID != null ? p.Staff.Name : "N/A",
                        custID = p.cuID,
                        custODID = p.custODID,
                        custTDID = p.custTDID,
                        ServiceDate = p.custTDID != null ? Convert.ToDateTime(p.CustomerTimeline.StartDate).ToString("MM/dd/yyyy") : "N/A",
                        TaskNo = p.custTDID != null ? p.CustomerTimeline.TaskNo.ToString() : "N/A",
                        Files = UhDB.Files.Where(x => x.cuiD == p.cuID && x.uID == uID && x.FileUse == 11 && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                              UhDB.Files.Where(x => x.cuiD == p.cuID && x.uID == uID && x.FileUse == 11 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                              .Select(s => new GetFileDetails {Name=s.Filename,ContentType=s.FileContentType,Size=s.FileSize,Value= "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/StaffComplaint/"+s.FileFieldName }).ToList():null

                    }).ToList();
            foreach (var objStaffCustomerRating in objStaffCustomerRatings)
            {
                if (!result.Any(x=>x.custID==objStaffCustomerRating.custID && x.custODID==objStaffCustomerRating.custODID && x.custTDID==objStaffCustomerRating.custTDID)) 
                {
                    result.Add(new GetStaffCustomerRatingForAdminModel
                    {
                        CustomerName = objStaffCustomerRating.CustomerName,
                        MainCategory = objStaffCustomerRating.MainCategory,
                        SubCategory = objStaffCustomerRating.SubCategory,
                        CreatedBy = objStaffCustomerRating.CreatedBy,
                        CreatedOn = objStaffCustomerRating.CreatedOn,
                        Review = objStaffCustomerRating.Review,
                        stfID = objStaffCustomerRating.stfID,
                        StaffName = objStaffCustomerRating.StaffName,
                        custID = objStaffCustomerRating.custID,
                        custODID = objStaffCustomerRating.custODID,
                        custTDID = objStaffCustomerRating.custTDID,
                        ServiceDate = objStaffCustomerRating.ServiceDate,
                        TaskNo = objStaffCustomerRating.TaskNo,
                        Files = objStaffCustomerRating.Files
                    });
                }
            }
            return result;
        }

        public List<GetStaffCustomerRatingForAdminDetailsModel> GetStaffCustomerRatingForAdminDetails(int? custID,int? custODID,int? custTDID)
        {
            List<GetStaffCustomerRatingForAdminDetailsModel> result = new List<GetStaffCustomerRatingForAdminDetailsModel>();
            result = UhDB.StaffCustomerRatings.Where(x => x.cuID == custID && x.custODID==custODID && x.custTDID==custTDID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                    .Select(p => new GetStaffCustomerRatingForAdminDetailsModel
                    {
                        CustomerConditionalType=p.custCTID!=null?p.CustomerConditionalType.Name:"N/A",
                        CustomerIssue=p.custISID!=null?p.CustomerIssueType.Name:"N/A",
                        OtherCustomerIssue=p.custISID==4?p.OtherIssue:"N/A",
                        custISID=p.custISID,
                        custCTID=p.custCTID
                    }).ToList();
            return result;
        }

    }
}