using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;
using System.IO;

namespace UHSForm.DAL
{
    public class StaffDB
    {
        private UHSEntities UhDB;
        private GeneralDB objGeneralDB;

        public StaffDB()
        {
            UhDB = new UHSEntities();
            objGeneralDB = new GeneralDB();
        }

        public string CreateStaff(StaffModel staff)
        {
            string result = null;
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    int CountUsername = UhDB.Logins.Where(x => x.Username == staff.Mobile).Count();
                    if (CountUsername != 0)
                    {
                        result = "AEUsername";
                    }
                    else
                    {
                        string Password = objGeneralDB.GeneratePassword(7);
                        System.Data.Entity.Core.Objects.ObjectParameter output = new System.Data.Entity.Core.Objects.ObjectParameter("responseMessage", typeof(string));
                        UhDB.SPmyCreateStaff(staff.Mobile, Password, staff.Name, staff.Mobile, staff.Email, null, null, staff.CreatedRole, staff.Role, staff.uID, staff.suID, true, false, staff.CreatedBy, output);
                        if (output.Value.ToString() == "SUCCESS")
                        {
                            int? stfID = UhDB.Staffs.Where(x => x.Mobile == staff.Mobile && x.IsActive == true && x.IsDelete == false).FirstOrDefault().stfID;
                            StaffTeam objStaffTeam = new StaffTeam();
                            objStaffTeam.teamID = staff.teamID;
                            objStaffTeam.stfID = stfID;
                            objStaffTeam.IsDelete = staff.IsDelete;
                            objStaffTeam.IsActive = staff.IsActive;
                            objStaffTeam.CreatedBy = staff.CreatedBy;
                            objStaffTeam.CreatedOn = staff.CreatedOn;
                            objStaffTeam.CreatedRole = staff.CreatedRole;
                            UhDB.StaffTeams.Add(objStaffTeam);
                            Save();

                            var objTeams = UhDB.Teams.Where(x => x.teamID == staff.teamID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                            objTeams.Status = true;
                            objTeams.UpdatedBy = staff.CreatedBy;
                            objTeams.UpdatedOn = staff.CreatedOn;
                            objTeams.UpdatedRole = staff.CreatedRole;
                            Save();

                            string body = EmailBodyForSendPassword(staff.Mobile, Password);
                            string Email = staff.Email;
                            string Subject = "Your Cleaning Account Details";
                            string Name = staff.Name;
                            result = "SUCCESS";
                            if (staff.Email != null)
                            {
                                objGeneralDB.SentEmailFromAmazon(Email, body, Subject, Name);
                            }
                            else
                            {

                            }

                        }
                        else
                        {
                            result = "Exception";
                        }

                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    result = "Exception";
                }
            }
            return result;
        }

        public IEnumerable<GetStaffModel> GetStaffs(int? uID)
        {
            List<GetStaffModel> result = new List<GetStaffModel>();
            result = UhDB.Staffs.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select((p, q) => new GetStaffModel
                     {
                         index = q + 1,
                         Name = p.Name,
                         Email = p.Email,
                         Mobile = p.Mobile,
                         Role = p.Login.Role.Name,
                         stfID = p.suID,
                         rID = p.Login.rID,
                         uID = p.uID,
                         suID = p.suID,
                         CreatedBy = p.CreatedBy,
                         CreatedOn = p.CreatedOn
                     }).ToList();
            return result;
        }

        public GetStaffModel GetStaffsByID(int? uID, int? stfID)
        {
            GetStaffModel result = new GetStaffModel();
            result = UhDB.Staffs.Where(x => x.uID == uID && x.stfID == stfID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select((p, q) => new GetStaffModel
                     {
                         index = q + 1,
                         Name = p.Name,
                         Email = p.Email,
                         Mobile = p.Mobile,
                         Role = p.Login.Role.Name,
                         stfID = p.suID,
                         rID = p.Login.rID,
                         uID = p.uID,
                         suID = p.suID,
                         CreatedBy = p.CreatedBy,
                         CreatedOn = p.CreatedOn
                     }).FirstOrDefault();
            return result;
        }

        public string UpdateRoleStaff(UpdateRoleStaffModel staff)
        {
            string result = null;
            var objStaffs = UhDB.Staffs.Where(x => x.suID == staff.stfID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            int? logID = objStaffs.logID;
            var objLogin = UhDB.Logins.Where(x => x.logID == logID).FirstOrDefault();
            objLogin.rID = staff.rID;
            objLogin.UpdatedBy = staff.UpdatedBy;
            objLogin.UpdatedOn = staff.UpdatedOn;
            objLogin.UpdateRole = staff.UpdatedRole;
            Save();
            result = "SUCCESS";



            return result;
        }

        public string UpdateStaffInfo(UpdatePersonalStaffModel staff)
        {
            string result = null;
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    var objStaffs = UhDB.Staffs.Where(x => x.stfID == staff.stfID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    if (objStaffs.Mobile != staff.Mobile)
                    {
                        var objLogin = UhDB.Logins.Where(x => x.logID == objStaffs.logID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                        objLogin.Username = staff.Mobile;
                        objLogin.UpdatedBy = staff.UpdatedBy;
                        objLogin.UpdatedOn = staff.UpdatedOn;
                        objLogin.UpdateRole = staff.UpdatedRole;
                        Save();
                    }

                    var objUpdateStaffs = UhDB.Staffs.Where(x => x.stfID == staff.stfID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objUpdateStaffs.Name = staff.Name;
                    objUpdateStaffs.Email = staff.Email;
                    objUpdateStaffs.Mobile = staff.Mobile;
                    objUpdateStaffs.UpdatedBy = staff.UpdatedBy;
                    objUpdateStaffs.UpdatedOn = staff.UpdatedOn;
                    objUpdateStaffs.UpdatedRole = staff.UpdatedRole;
                    Save();
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

        public string UpdateTeamStaff(UpdateTeamStaffModel staff)
        {
            string result = null;
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    var objCurrentStaffs = UhDB.StaffTeams.Where(x => x.stfID == staff.stfID && x.teamID==staff.CurrentteamID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objCurrentStaffs.IsActive = staff.CurrentIsActive;
                    objCurrentStaffs.IsDelete = staff.CurrentIsDelete;
                    objCurrentStaffs.UpdatedBy = staff.UpdatedBy;
                    objCurrentStaffs.UpdatedOn = staff.UpdatedOn;
                    objCurrentStaffs.UpdatedRole = staff.UpdatedRole;
                    Save();

                    StaffTeam objUpdateTeam = new StaffTeam();
                    objUpdateTeam.teamID = staff.UpdateteamID;
                    objUpdateTeam.stfID = staff.stfID;
                    objUpdateTeam.IsDelete = staff.UpdateIsDelete;
                    objUpdateTeam.IsActive = staff.UpdateIsActive;
                    objUpdateTeam.CreatedBy = staff.UpdatedBy;
                    objUpdateTeam.CreatedOn = staff.UpdatedOn;
                    objUpdateTeam.CreatedRole = staff.UpdatedRole;
                    UhDB.StaffTeams.Add(objUpdateTeam);
                    Save();

                    var objTeams = UhDB.Teams.Where(x => x.teamID == staff.UpdateteamID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objTeams.Status = true;
                    objTeams.UpdatedBy = staff.UpdatedBy;
                    objTeams.UpdatedOn = staff.UpdatedOn;
                    objTeams.UpdatedRole = staff.UpdatedRole;
                    Save();
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

        public string DeleteStaff(DeleteStaffModel staff)
        {
            string result = null;
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    var objStaffTeams = UhDB.StaffTeams.Where(x => x.stfID == staff.stfID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objStaffTeams.IsActive = staff.IsActive;
                    objStaffTeams.IsDelete = staff.IsDelete;
                    objStaffTeams.UpdatedBy = staff.UpdatedBy;
                    objStaffTeams.UpdatedOn = staff.UpdatedOn;
                    objStaffTeams.UpdatedRole = staff.UpdatedRole;
                    Save();

                    var objStaffs = UhDB.Staffs.Where(x => x.stfID == staff.stfID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objStaffs.IsActive = staff.IsActive;
                    objStaffs.IsDelete = staff.IsDelete;
                    objStaffs.UpdatedBy = staff.UpdatedBy;
                    objStaffs.UpdatedOn = staff.UpdatedOn;
                    objStaffs.UpdatedRole = staff.UpdatedRole;
                    Save();

                    Save();
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

        public IEnumerable<GetDropDown> GetStaffDropDown(int uID)
        {
            var result = UhDB.Staffs.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                               .Select(p => new GetDropDown
                               {
                                   ID = p.stfID,
                                   Value = p.Name
                               }).ToList();
            return result;
        }

        private void Save()
        {
            UhDB.SaveChanges();
        }

        private string EmailBodyForSendPassword(string Username, string Password)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/SetPassword.html")))//using streamreader for reading my htmltemplate  
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Username}", Username); //replacing the required things  
            body = body.Replace("{Password}", Password);
            return body;
        }
    }
}