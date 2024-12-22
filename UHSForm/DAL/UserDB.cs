using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;
using System.IO;

namespace UHSForm.DAL
{
    public class UserDB
    {
        private UHSEntities UhDB;
        private GeneralDB objGeneralDB;

        public UserDB()
        {
            UhDB = new UHSEntities();
            objGeneralDB = new GeneralDB();
        }

        public string CreateUser(UserModel user)
        {
            string result = null;
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    int CountUsername = UhDB.Logins.Where(x => x.Username == user.Email).Count();
                    if (CountUsername != 0)
                    {
                        result = "AEUsername";
                    }
                    else
                    {
                        int CountMobile = UhDB.Admin_Sub.Where(x => x.IsActive == true && x.IsDelete == false && x.MobileNo == user.Mobile).Count();
                        if (CountMobile != 0)
                        {
                            result = "AEMobile";
                        }
                        else
                        {

                            string Password = objGeneralDB.GeneratePassword(7);
                            System.Data.Entity.Core.Objects.ObjectParameter output = new System.Data.Entity.Core.Objects.ObjectParameter("responseMessage", typeof(string));
                            UhDB.SPmyCreateSubUser(user.Email, Password, user.Name, user.Email, user.Mobile, null, null, user.CreatedRole, user.Role, null, user.uID, output);
                            if (output.Value.ToString() == "SUCCESS")
                            {
                                string body = EmailBodyForSendPassword(user.Email, Password);
                                string Email = user.Email;
                                string Subject = "Login Credentials";
                                string Name = user.Name;
                                result = "SUCCESS";
                                result = objGeneralDB.SentEmailFromAmazon(Email, body, Subject, Name);
                            }

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

        public IEnumerable<GetUserModel> GetMangeUser(int? uID)
        {
            List<GetUserModel> result = new List<GetUserModel>();
            result = UhDB.Admin_Sub.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select((p, q) => new GetUserModel
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
            return result;
        }

        public GetUserModel GetMangeUserByID(int? uID, int? suID)
        {
            GetUserModel result = new GetUserModel();
            result = UhDB.Admin_Sub.Where(x => x.uID == uID && x.suID==suID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select((p, q) => new GetUserModel
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
                     }).FirstOrDefault();
            return result;
        }

        public string UpdateManageUser(UpdateRoleUserModel user)
        {
            string result = null;
            var objUser = UhDB.Admin_Sub.Where(x => x.suID == user.suID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            int? logID = objUser.logID;
            var objLogin = UhDB.Logins.Where(x => x.logID == logID).FirstOrDefault();
            objLogin.rID = user.rID;
            objLogin.UpdatedBy = user.UpdatedBy;
            objLogin.UpdatedOn = user.UpdatedOn;
            objLogin.UpdateRole = user.UpdatedRole;
            Save();
            result = "SUCCESS";



            return result;
        }

        public string UpdateUserInfo(UpdatePersonalUserModel user)
        {
            string result = null;
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    var objAdminSub = UhDB.Admin_Sub.Where(x => x.suID == user.suID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    if (objAdminSub.Email != user.Email)
                    {
                        var objLogin = UhDB.Logins.Where(x => x.logID == objAdminSub.logID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                        objLogin.Username = user.Email;
                        objLogin.UpdatedBy = user.UpdatedBy;
                        objLogin.UpdatedOn = user.UpdatedOn;
                        objLogin.UpdateRole = user.UpdatedRole;
                        Save();
                    }

                    var objUpdateAdminSub = UhDB.Admin_Sub.Where(x => x.suID == user.suID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objUpdateAdminSub.Name = user.Name;
                    objUpdateAdminSub.Email = user.Email;
                    objUpdateAdminSub.MobileNo = user.Mobile;
                    objUpdateAdminSub.UpdatedBy = user.UpdatedBy;
                    objUpdateAdminSub.UpdatedOn = user.UpdatedOn;
                    objUpdateAdminSub.UpdatedRole = user.UpdatedRole;
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

        public IEnumerable<GetDropDown> GetUserDropDown(int uID)
        {
            var result = UhDB.Admin_Sub.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                               .Select(p => new GetDropDown
                               {
                                   ID = p.suID,
                                   Value = p.Login.Username
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