using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;

namespace UHSForm.DAL
{
    public class CommonUsersDB
    {
        private UHSEntities UhDB;

        public CommonUsersDB()
        {
            UhDB = new UHSEntities();
        }
        public IEnumerable<GetCommonUserModel> GetCommonUsers(int? uID)
        {
            List<GetCommonUserModel> result = new List<GetCommonUserModel>();

            var objAdminSub = UhDB.Admin_Sub.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select((p, q) => new GetCommonUserModel
                     {
                         index = q + 1,
                         Name = p.Name,
                         Email = p.Email,
                         Mobile = p.MobileNo,
                         Role = p.Login.Role.Name,
                         suID = p.suID,
                         rID = p.Login.rID,
                         CreatedBy = p.CreatedBy,
                         CreatedOn = p.CreatedOn
                     }).ToList();
            result.AddRange(objAdminSub);
            var objStaff = UhDB.StaffTeams.Where(x => x.Staff.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                            .Select((p, q) => new GetCommonUserModel
                            {
                                index = q + 1,
                                Name = p.Staff.Name,
                                Email = p.Staff.Email,
                                Mobile = p.Staff.Mobile,
                                Role = p.Staff.Login.Role.Name,
                                stfID = p.stfID,
                                rID = p.Staff.Login.rID,
                                uID = p.Staff.uID,
                                suID = p.Staff.suID,
                                teamID=p.teamID,
                                TeamName=p.Team.Name,
                                CreatedBy = p.CreatedBy,
                                CreatedOn = p.CreatedOn
                            }).ToList();
            result.AddRange(objStaff);
            result = result.OrderByDescending(x => x.CreatedOn).ToList();
            
            return result;
        }
        

        public IEnumerable<GetCommonUserModel> GetCommonUsersByrID(int? uID,int? rID)
        {
            List<GetCommonUserModel> result = new List<GetCommonUserModel>();
   
            
            var objAdminSub = UhDB.Admin_Sub.Where(x => x.Login.rID == rID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select((p, q) => new GetCommonUserModel
                     {
                         index = q + 1,
                         Name = p.Name,
                         Email = p.Email,
                         Mobile = p.MobileNo,
                         Role = p.Login.Role.Name,
                         suID = p.suID,
                         rID = p.Login.rID,
                         CreatedBy = p.CreatedBy,
                         CreatedOn = p.CreatedOn
                     }).ToList();
            result.AddRange(objAdminSub);
            
            var objStaff=UhDB.StaffTeams.Where(x=>x.Staff.Login.rID==rID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select((p, q) => new GetCommonUserModel
                     {
                         index = q + 1,
                         Name = p.Staff.Name,
                         Email = p.Staff.Email,
                         Mobile = p.Staff.Mobile,
                         Role = p.Staff.Login.Role.Name,
                         stfID = p.stfID,
                         rID = p.Staff.Login.rID,
                         uID = p.Staff.uID,
                         suID = p.Staff.suID,
                         teamID = p.teamID,
                         TeamName = p.Team.Name,
                         CreatedBy = p.CreatedBy,
                         CreatedOn = p.CreatedOn
                     }).ToList();
            result.AddRange(objStaff);
            return result;
        }


        public IEnumerable<GetDropDown> GetCommonUsersDropDownByRoleID(int? uID, int? rID)
        {
            List<GetDropDown> result = new List<GetDropDown>();
            if (rID == 12)
            {
                result = UhDB.Staffs.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                         .Select(p => new GetDropDown
                         {
                             ID = p.stfID,
                             Value = p.Name
                         }).ToList();
            }
            else if (rID == 11)
            {
                result = UhDB.Admin_Sub.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                         .Select(p => new GetDropDown
                         {
                             ID = p.suID,
                             Value = p.Name
                         }).ToList();
            }
            return result;
        }
    }
}