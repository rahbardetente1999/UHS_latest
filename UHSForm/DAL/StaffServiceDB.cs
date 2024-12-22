using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models;
using UHSForm.Models.Data;

namespace UHSForm.DAL
{
    public class StaffServiceDB
    {
        private UHSEntities UhDB;

        public StaffServiceDB()
        {
            UhDB = new UHSEntities();
        }

        public string CreateStaffService(StaffServiceModel staffService)
        {
            string result = null;
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    if (staffService.SpecialService == false)
                    {
                        if (staffService.catsubID!= null)
                        {
                            foreach (var item in staffService.catsubID)
                            {
                                StaffService objStaffService = new StaffService();
                                objStaffService.catID = staffService.catID;
                                objStaffService.catsubID = item;
                                if (staffService.IsTeam == true)
                                {
                                    objStaffService.teamID = staffService.teamID;
                                }
                                else
                                {
                                    objStaffService.stfID = staffService.stfID;
                                }
                                objStaffService.propaID = staffService.propaID;
                                objStaffService.IsActive = staffService.IsActive;
                                objStaffService.IsDelete = staffService.IsDelete;
                                objStaffService.CreatedBy = staffService.CreatedBy;
                                objStaffService.CreatedOn = staffService.CreatedOn;
                                UhDB.StaffServices.Add(objStaffService);
                                UhDB.SaveChanges();

                            }
                        }
                        else 
                        {
                            StaffService objStaffService = new StaffService();
                            objStaffService.catID = staffService.catID;
                            if (staffService.IsTeam == true)
                            {
                                objStaffService.teamID = staffService.teamID;
                            }
                            else
                            {
                                objStaffService.stfID = staffService.stfID;
                            }
                            objStaffService.propaID = staffService.propaID;
                            objStaffService.IsActive = staffService.IsActive;
                            objStaffService.IsDelete = staffService.IsDelete;
                            objStaffService.CreatedBy = staffService.CreatedBy;
                            objStaffService.CreatedOn = staffService.CreatedOn;
                            UhDB.StaffServices.Add(objStaffService);
                            UhDB.SaveChanges();
                        }
                    }
                    else
                    {
                        StaffService objStaffService = new StaffService();
                        objStaffService.catID = staffService.catID;
                        objStaffService.catsubID = staffService.catsubID[0];
                        objStaffService.servcatID = staffService.servcatID;
                        objStaffService.servsubcatID = staffService.servsubcatID;
                        if (staffService.IsTeam == true)
                        {
                            objStaffService.teamID = staffService.teamID;
                        }
                        else
                        {
                            objStaffService.stfID = staffService.stfID;
                        }
                        objStaffService.propaID = staffService.propaID;
                        objStaffService.IsActive = staffService.IsActive;
                        objStaffService.IsDelete = staffService.IsDelete;
                        objStaffService.CreatedBy = staffService.CreatedBy;
                        objStaffService.CreatedOn = staffService.CreatedOn;
                        UhDB.StaffServices.Add(objStaffService);
                        UhDB.SaveChanges();
                    }
                    trans.Commit();
                    result = "SUCCESS";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    result = "Exception";
                }
            }

            return result;
        }

        public IEnumerable<GetStaffServiceModel> GetStaffServices(int? uID)
        {
            List<GetStaffServiceModel> result = new List<GetStaffServiceModel>();
            result = UhDB.StaffServices.Where(x => x.MainCategory.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select(p => new GetStaffServiceModel
                     {
                         catID = p.catID,
                         catsubID = p.catsubID,
                         servcatID = p.servcatID,
                         servsubcatID = p.servsubcatID,
                         stfID = p.stfID,
                         stfsID = p.stfsID,
                         teamID = p.teamID,
                         propaID = p.propaID,
                         PropertyName = p.propaID != null ? p.PropertyArea.Name : "N/A",
                         MainCategoryName = p.catID != null ? p.MainCategory.Name : "N/A",
                         SubCategoryName = p.catsubID != null ? p.SubCategory.Name : "N/A",
                         ServiceCategoryName = p.servcatID != null ? p.ServiceCategory.Name : "N/A",
                         SubServiceCategoryName = p.servsubcatID != null ? p.ServiceSubCategory.Name : "N/A",
                         StaffName = p.stfID != null ? p.Staff.Name : "N/A",
                         TeamName = p.teamID != null ? p.Team.Name : "N/A",
                         CreatedBy = p.CreatedBy,
                         CreatedOn = p.CreatedOn
                     }).ToList();
            return result;
        }
    }
}