using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UHSForm.DAL;
using UHSForm.Models;
using UHSForm.Areas.Admin.Data;

namespace UHSForm.Areas.Admin.Controllers
{
    [Authorize]
    public class UserManagementController : Controller
    {
        private UserDB _objUserDB;
        private StaffDB _objStaffDB;
        private GeneralDB _objGeneralDB;
        private CommonUsersDB _objCommonUsersDB;
        private TeamsDB _objTeamsDB;
        private CommonStaffDB _objCommonStaffDB;

        public UserManagementController()
        {
            _objUserDB = new UserDB();
            _objStaffDB = new StaffDB();
            _objGeneralDB = new GeneralDB();
            _objTeamsDB = new TeamsDB();
            _objCommonUsersDB = new CommonUsersDB();
            _objCommonStaffDB = new CommonStaffDB();
        }

        // GET: Admin/UserManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Teams()
        {
            return View();
        }

        public ActionResult CleanerTask()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(CreateUser user)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                if (user.Role == 15)
                {
                    StaffModel objStaffDetails = new StaffModel();
                    objStaffDetails.Name = user.Name;
                    objStaffDetails.Email = user.Email;
                    objStaffDetails.Mobile = user.Mobile;
                    objStaffDetails.Role = user.Role;
                    objStaffDetails.teamID = user.teamID;
                    objStaffDetails.uID = objUser.uID;
                    objStaffDetails.IsActive = true;
                    objStaffDetails.IsDelete = false;
                    objStaffDetails.CreatedBy = User.Identity.Name;
                    objStaffDetails.CreatedOn = DateTime.Now;
                    objStaffDetails.CreatedRole = objUser.rID;
                    result = _objStaffDB.CreateStaff(objStaffDetails);
                }
                else
                {
                    UserModel objUserDetails = new UserModel();
                    objUserDetails.Name = user.Name;
                    objUserDetails.Email = user.Email;
                    objUserDetails.Mobile = user.Mobile;
                    objUserDetails.Role = user.Role;
                    objUserDetails.uID = objUser.uID;
                    objUserDetails.IsActive = true;
                    objUserDetails.IsDelete = false;
                    objUserDetails.CreatedBy = User.Identity.Name;
                    objUserDetails.CreatedOn = DateTime.Now;
                    objUserDetails.CreatedRole = objUser.rID;
                    result = _objUserDB.CreateUser(objUserDetails);
                }
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCommonUsers()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonUsersDB.GetCommonUsers(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpGet]
        public ActionResult GetCommonUsersByrID(int? rID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonUsersDB.GetCommonUsersByrID(objUser.uID, rID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpGet]
        public ActionResult GetGetUserDropDown(int? rID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objCommonUsersDB.GetCommonUsersDropDownByRoleID(objUser.uID, rID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        public ActionResult UpdateUserPersonal(UpdateUserPersonalDetails user)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                if (user.rID == 11)
                {
                    UpdatePersonalUserModel objUpdatePersonalUserModel = new UpdatePersonalUserModel();
                    objUpdatePersonalUserModel.Name = user.Name;
                    objUpdatePersonalUserModel.Email = user.Email;
                    objUpdatePersonalUserModel.Mobile = user.Mobile;
                    objUpdatePersonalUserModel.suID = user.suID;
                    objUpdatePersonalUserModel.UpdatedBy = User.Identity.Name;
                    objUpdatePersonalUserModel.UpdatedOn = DateTime.Now;
                    objUpdatePersonalUserModel.UpdatedRole = objUser.rID;
                    result = _objUserDB.UpdateUserInfo(objUpdatePersonalUserModel);
                }
                else
                {
                    UpdatePersonalStaffModel objUpdatePersonalStaffModel = new UpdatePersonalStaffModel();
                    objUpdatePersonalStaffModel.Name = user.Name;
                    objUpdatePersonalStaffModel.Email = user.Email;
                    objUpdatePersonalStaffModel.Mobile = user.Mobile;
                    objUpdatePersonalStaffModel.stfID = user.stfID;
                    objUpdatePersonalStaffModel.UpdatedBy = User.Identity.Name;
                    objUpdatePersonalStaffModel.UpdatedOn = DateTime.Now;
                    objUpdatePersonalStaffModel.UpdatedRole = objUser.rID;
                    result = _objStaffDB.UpdateStaffInfo(objUpdatePersonalStaffModel);
                }
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateUserRole(UpdateUserRole user)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                if (user.rID == 11)
                {
                    UpdateRoleUserModel objUpdateRoleUserModel = new UpdateRoleUserModel();
                    objUpdateRoleUserModel.rID = user.rID;
                    objUpdateRoleUserModel.suID = user.suID;
                    objUpdateRoleUserModel.UpdatedBy = User.Identity.Name;
                    objUpdateRoleUserModel.UpdatedOn = DateTime.Now;
                    objUpdateRoleUserModel.UpdatedRole = objUser.rID;
                    result = _objUserDB.UpdateManageUser(objUpdateRoleUserModel);
                }
                else
                {
                    UpdateRoleStaffModel objUpdateRoleStaffModel = new UpdateRoleStaffModel();
                    objUpdateRoleStaffModel.rID = user.rID;
                    objUpdateRoleStaffModel.stfID = user.stfID;
                    objUpdateRoleStaffModel.UpdatedBy = User.Identity.Name;
                    objUpdateRoleStaffModel.UpdatedOn = DateTime.Now;
                    objUpdateRoleStaffModel.UpdatedRole = objUser.rID;
                    result = _objStaffDB.UpdateRoleStaff(objUpdateRoleStaffModel);
                }
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateTeamStaff(UpdateTeamStaffModel team)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                team.CurrentIsActive = false;
                team.CurrentIsDelete = true;
                team.UpdateIsActive = true;
                team.UpdateIsDelete = false;
                team.UpdatedBy = User.Identity.Name;
                team.UpdatedOn = DateTime.Now;
                team.UpdatedRole = objUser.rID;
                result = _objStaffDB.UpdateTeamStaff(team);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteStaff(DeleteStaffModel staff)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                staff.IsActive = false;
                staff.IsDelete = true;
                staff.UpdatedBy = User.Identity.Name;
                staff.UpdatedOn = DateTime.Now;
                staff.UpdatedRole = objUser.rID;
                result = _objStaffDB.DeleteStaff(staff);
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateTeams(TeamsModel teams)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                teams.IsActive = true;
                teams.IsDelete = false;
                teams.uID = objUser.uID;
                teams.suID = objUser.userID;
                teams.CreatedBy = User.Identity.Name;
                teams.CreatedOn = DateTime.Now;
                teams.CreatedRole = objUser.rID;
                result = _objTeamsDB.CreateTeams(teams);

            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateTeams(UpdateTeamsModel teams)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                teams.UpdatedBy = User.Identity.Name;
                teams.UpdatedOn = DateTime.Now;
                teams.UpdatedRole = objUser.rID;
                result = _objTeamsDB.UpdateTeams(teams);

            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteTeams(DeleteTeamsModel teams)
        {
            string result = null;
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                teams.IsActive = false;
                teams.IsDelete = true;
                teams.UpdatedBy = User.Identity.Name;
                teams.UpdatedOn = DateTime.Now;
                teams.UpdatedRole = objUser.rID;
                result = _objTeamsDB.DeleteTeams(teams);

            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetTeams()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objTeamsDB.GetTeams(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpGet]
        public ActionResult GetTeamsByTeamID(int? teamID)
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objTeamsDB.GetTeamsByTeamID(objUser.uID, teamID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpGet]
        public ActionResult GetTeamsDropDown()
        {
            try
            {
                var objUser = _objGeneralDB.GetUserLoginDetails(User.Identity.Name);
                var result = _objTeamsDB.GetTeamsDropDown(objUser.uID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpGet]
        public ActionResult GetStaffAverageRating(int? stfID)
        {

            try
            {
                var result = _objCommonStaffDB.GetStaffAverageRating(stfID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult GetTotalService(int? stfID)
        {

            try
            {
                var result = _objCommonStaffDB.GetTotalService(stfID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = "Exception";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult ResetCleanerPassword(ChangePassword password)
        {
            string result = "";
            try
            {
                int usercount = _objGeneralDB.GetUserCount(password.Username);
                if (usercount == 0)
                {
                    result = "NUser";
                }
                else
                {
                    password.Username = password.Username;
                    password.Password = password.Password;
                    password.CreatedBy = User.Identity.Name;
                    password.CreatedOn = DateTime.Now;
                    result = _objGeneralDB.ForgotPassword(password);
                }
            }
            catch (Exception ex)
            {
                result = "Exception";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}