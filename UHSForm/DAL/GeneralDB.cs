using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;
using Amazon.SimpleEmail.Model;
using Amazon.SimpleEmail;
using Amazon.Runtime;
using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Net.Http;
using System.Threading.Tasks;
using UHSForm.MessengerService;

namespace UHSForm.DAL
{
    public class GeneralDB
    {
        private UHSEntities UhDB;
        private static readonly string apiUrl = "https://messaging.ooredoo.qa/bms/soap/Messenger.asmx/HTTP_SendSms";
        private static readonly string apiBaseUrl = "https://messaging.ooredoo.qa/bms/soap/Messenger.asmx/";
        private static readonly string customerID = "6264";
        private static readonly string userName = "UHS";
        private static readonly string userPassword = "UHSms$007";
        private MessengerSoapClient messenger = new MessengerSoapClient("MessengerSoap");
        public GeneralDB()
        {
            UhDB = new UHSEntities();
        }

        public GetUserDetails GetUserLoginDetails(string Username)
        {
            GetUserDetails objGetUserDetails = new GetUserDetails();
            var objLogin = UhDB.Logins.Where(x => x.IsActive == true && x.IsDelete == false && x.Username == Username).FirstOrDefault();
            if (objLogin.rID == 10)
            {
                objGetUserDetails = UhDB.Users.Where(x => x.IsActive == true && x.IsDelete == false && x.logID == objLogin.logID).AsEnumerable().
                    Select(p => new GetUserDetails
                    {
                        uID = p.uID,
                        userID = p.uID,
                        Name = p.Name,
                        MobileNo = p.MobileNo,
                        AlternativeMobileNo = p.AlternativeMobileNo,
                        Email = p.Email,
                        PhoneCode = p.PhoneCode,
                        Country = p.Country,
                        rID = objLogin.rID,
                        logID = objLogin.logID
                    }).FirstOrDefault();
            }
            else if (objLogin.rID == 15)
            {
                objGetUserDetails = UhDB.Staffs.Where(x => x.IsActive == true && x.IsDelete == false && x.logID == objLogin.logID).AsEnumerable().
                    Select(p => new GetUserDetails
                    {
                        uID = p.uID,
                        userID = p.suID,
                        stfID = p.stfID,
                        Username = p.Email,
                        Name = p.Name,
                        MobileNo = p.Mobile,
                        Email = p.Email,
                        PhoneCode = p.PhoneCode,
                        Country = p.Country,
                        rID = objLogin.rID,
                    }).FirstOrDefault();
            }
            else if (objLogin.rID == 14)
            {
                objGetUserDetails = UhDB.Customers.Where(x => x.logID == objLogin.logID && x.IsActive == true && x.IsDelete == false).AsEnumerable().
                                  Select(p => new GetUserDetails
                                  {
                                      uID = p.uID,
                                      userID = p.suID,
                                      cuID = p.cuID,
                                      Username = objLogin.Username,
                                      Name = p.Name,
                                      MobileNo = p.Mobile,
                                      AlternativeMobileNo = p.AlternativeNo,
                                      Email = p.Email,
                                      PhoneCode = p.PhoneCode,
                                      Country = p.Country,
                                      rID = objLogin.rID,
                                  }).FirstOrDefault();
            }
            else
            {
                objGetUserDetails = UhDB.Admin_Sub.Where(x => x.IsActive == true && x.IsDelete == false && x.logID == objLogin.logID).AsEnumerable().
                    Select(p => new GetUserDetails
                    {
                        uID = p.uID,
                        userID = p.suID,
                        Username = objLogin.Username,
                        Name = p.Name,
                        MobileNo = p.MobileNo,
                        AlternativeMobileNo = p.AlternativeMobileNo,
                        Email = p.Email,
                        PhoneCode = p.PhoneCode,
                        Country = p.Country,
                        rID = objLogin.rID,
                    }).FirstOrDefault();
            }
            return objGetUserDetails;
        }

        public GetUserDetails GetUserLoginDetailsById(int? uid)
        {
            var user = UhDB.Users.Where(x => x.IsActive == true && x.IsDelete == false && x.uID == uid)
                .Select(p => new GetUserDetails
                {
                    uID = p.uID,
                    Username = p.Login.Username,//by dawood
                    Name = p.Name,
                    MobileNo = p.MobileNo,
                    AlternativeMobileNo = p.AlternativeMobileNo,
                    Email = p.Email,
                    PhoneCode = p.PhoneCode,
                    Country = p.Country,
                    rID = p.Login.rID,
                })
                .FirstOrDefault();
            return user;
        }

        public string GenerateOTP1(int OTPlength)
        {
            string numbers = "1234567890";

            string characters = numbers;
            characters += numbers;
            int length = OTPlength;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }

        public string GeneratePassword(int Passwordlength)
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;
            characters += alphabets + small_alphabets + numbers;
            int length = Passwordlength;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }

        public string BlockUser(Block user)
        {
            string result = null;
            try
            {
                var objUser = UhDB.Logins.Where(x => x.IsActive == true && x.IsDelete == false && x.Username == user.Username).FirstOrDefault();
                objUser.IsBlocked = user.IsBlock;
                objUser.UpdatedBy = user.UpdatedBy;
                objUser.UpdatedOn = user.UpdatedOn;
                objUser.UpdateRole = user.UpdatedRole;
                Save();
                result = "SUCCESS";
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return result;
        }

        public GetBlock GetBlock(string Username)
        {
            return UhDB.Logins.Where(x => x.IsActive == true && x.IsDelete == false && x.Username == Username).AsEnumerable()
                .Select(p => new GetBlock
                {
                    Block = p.IsBlocked
                })
                .FirstOrDefault();
        }

        public string GetRID(string Username)
        {
            try
            {
                string Role = null;
                Role = UhDB.Logins.Where(x => x.IsActive == true && x.IsDelete == false && x.Username == Username).FirstOrDefault().rID.ToString();
                return Role;
                SqlConnection.ClearAllPools();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public string ForgotPassword(ChangePassword password)
        {
            string result = "";
            System.Data.Entity.Core.Objects.ObjectParameter output = new System.Data.Entity.Core.Objects.ObjectParameter("responseMessage", typeof(string));
            UhDB.SPmyLogin("REQPWD", password.Username, password.Password, null, output);
            if (output.Value.ToString() == "User Name Not Registered")
            {
                result = "User Name is not correct";
            }
            else
            {
                result = "Success";
                var res = UhDB.Logins.Where(x => x.Username == password.Username).FirstOrDefault();
                res.UpdatedBy = password.CreatedBy;
                res.UpdatedOn = password.CreatedOn;
                Save();
            }
            Save();
            return result;
        }

        public string ChangePassword(ChangePassword password)
        {
            string result = null;
            System.Data.Entity.Core.Objects.ObjectParameter output = new System.Data.Entity.Core.Objects.ObjectParameter("responseMessage", typeof(string));
            UhDB.SPmyLogin("CHPASSWORD", password.Username, password.Password, password.OldPassword, output);
            if (output.Value.ToString() == "INCORRECT OLD PASSWORD")
            {
                result = "INCORRECT OLD PASSWORD";
            }
            else if (output.Value.ToString() == "NUMBER NOT REGISTERED")
            {
                result = "NUMBER NOT REGISTERED";
            }
            else
            {
                result = "SUCCESS";
            }
            return result;
        }

        public string UpdateUserDetails(UpdateUserModel user)
        {
            string result = "";
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    if (user.rID == 10)
                    {
                        var objUser = UhDB.Users.Where(x => x.IsActive == true && x.IsDelete == false && x.uID == user.ID).FirstOrDefault();
                        objUser.Name = user.Name;
                        objUser.MobileNo = user.MobileNo;
                        objUser.AlternativeMobileNo = user.AlternativeMobileNo;
                        objUser.Email = user.Email;
                        objUser.Gender = user.Gender;
                        objUser.LandMark = user.LandMark;
                        objUser.Address = user.Address;
                        objUser.City = user.City;
                        objUser.State = user.State;
                        objUser.Country = user.Country;
                        objUser.Pincode = user.Pincode;
                        objUser.PhoneCode = user.PhoneCode;
                        objUser.UpdatedRole = user.UpdatedRole;
                        objUser.UpdatedOn = user.UpdatedOn;
                        objUser.UpdatedBy = user.UpdatedBy;
                        Save();

                    }
                    else if (user.rID == 11)
                    {
                        var objAdminSub = UhDB.Admin_Sub.Where(x => x.IsActive == true && x.IsDelete == false && x.suID == user.ID).FirstOrDefault();
                        if (objAdminSub.Email != user.Email)
                        {
                            var objLogin = UhDB.Logins.Where(x => x.logID == objAdminSub.logID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                            objLogin.Username = user.Email;
                            objLogin.UpdatedBy = user.UpdatedBy;
                            objLogin.UpdatedOn = user.UpdatedOn;
                            objLogin.UpdateRole = user.UpdatedRole;
                            Save();
                        }
                        var objUpdateAdminSub = UhDB.Admin_Sub.Where(x => x.IsActive == true && x.IsDelete == false && x.suID == user.ID).FirstOrDefault();
                        objUpdateAdminSub.Name = user.Name;
                        objUpdateAdminSub.MobileNo = user.MobileNo;
                        objUpdateAdminSub.AlternativeMobileNo = user.AlternativeMobileNo;
                        objUpdateAdminSub.Email = user.Email;
                        objUpdateAdminSub.Gender = user.Gender;
                        objUpdateAdminSub.LandMark = user.LandMark;
                        objUpdateAdminSub.Address = user.Address;
                        objUpdateAdminSub.City = user.City;
                        objUpdateAdminSub.State = user.State;
                        objUpdateAdminSub.Country = user.Country;
                        objUpdateAdminSub.Pincode = user.Pincode;
                        objUpdateAdminSub.PhoneCode = user.PhoneCode;
                        objUpdateAdminSub.UpdatedRole = user.UpdatedRole;
                        objUpdateAdminSub.UpdatedOn = user.UpdatedOn;
                        objUpdateAdminSub.UpdatedBy = user.UpdatedBy;
                        Save();
                    }
                    else if (user.rID == 15)
                    {
                        var objStaffs = UhDB.Staffs.Where(x => x.IsActive == true && x.IsDelete == false && x.stfID == user.ID).FirstOrDefault();
                        if (objStaffs.Mobile != user.MobileNo)
                        {
                            var objLogin = UhDB.Logins.Where(x => x.logID == objStaffs.logID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                            objLogin.Username = user.MobileNo;
                            objLogin.UpdatedBy = user.UpdatedBy;
                            objLogin.UpdatedOn = user.UpdatedOn;
                            objLogin.UpdateRole = user.UpdatedRole;
                            Save();
                        }

                        var objUpdateStaffs = UhDB.Staffs.Where(x => x.IsActive == true && x.IsDelete == false && x.stfID == user.ID).FirstOrDefault();
                        objUpdateStaffs.Name = user.Name;
                        objUpdateStaffs.Mobile = user.MobileNo;
                        objUpdateStaffs.Email = user.Email;
                        objUpdateStaffs.Country = user.Country;
                        objUpdateStaffs.PhoneCode = user.PhoneCode;
                        objUpdateStaffs.UpdatedRole = user.UpdatedRole;
                        objUpdateStaffs.UpdatedOn = user.UpdatedOn;
                        objUpdateStaffs.UpdatedBy = user.UpdatedBy;
                        Save();
                    }
                    else if (user.rID == 14)
                    {
                        var objCustomers = UhDB.Customers.Where(x => x.IsActive == true && x.IsDelete == false && x.cuID == user.ID).FirstOrDefault();
                        if (objCustomers.Email != user.Email)
                        {
                            var objLogin = UhDB.Logins.Where(x => x.logID == objCustomers.logID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                            objLogin.Username = user.Email;
                            objLogin.UpdatedBy = user.UpdatedBy;
                            objLogin.UpdatedOn = user.UpdatedOn;
                            objLogin.UpdateRole = user.UpdatedRole;
                            Save();
                        }

                        var objUpdateCustomers = UhDB.Customers.Where(x => x.IsActive == true && x.IsDelete == false && x.cuID == user.ID).FirstOrDefault();
                        objUpdateCustomers.Name = user.Name;
                        objUpdateCustomers.Mobile = user.MobileNo;
                        objUpdateCustomers.Email = user.Email;
                        objUpdateCustomers.AlternativeNo = user.AlternativeMobileNo;
                        objUpdateCustomers.WhatsAppNo = user.WhatsAppNo;
                        objUpdateCustomers.Country = user.Country;
                        objUpdateCustomers.PhoneCode = user.PhoneCode;
                        objUpdateCustomers.UpdatedRole = user.UpdatedRole;
                        objUpdateCustomers.UpdatedOn = user.UpdatedOn;
                        objUpdateCustomers.UpdatedBy = user.UpdatedBy;
                        Save();

                        //var objUpdateCustomerOfficalDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == user.ID && x.IsActive == true && x.IsDelete == false).OrderByDescending(x => x.custODID).FirstOrDefault();
                        //objUpdateCustomerOfficalDetails.AppartmentNumber = user.Address;
                        //objUpdateCustomerOfficalDetails.UpdatedBy = user.UpdatedBy;
                        //objUpdateCustomerOfficalDetails.UpdatedOn = user.UpdatedOn;
                        //objUpdateCustomerOfficalDetails.UpdatedRole = user.UpdatedRole;
                        //Save();
                    }
                    else 
                    {
                        var objAdmin_SubEmail = UhDB.Admin_Sub.Where(x => x.IsActive == true && x.IsDelete == false && x.suID == user.ID).FirstOrDefault();
                        if (objAdmin_SubEmail.Email != user.Email)
                        {
                            var objLogin = UhDB.Logins.Where(x => x.logID == objAdmin_SubEmail.logID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                            objLogin.Username = user.Email;
                            objLogin.UpdatedBy = user.UpdatedBy;
                            objLogin.UpdatedOn = user.UpdatedOn;
                            objLogin.UpdateRole = user.UpdatedRole;
                            Save();
                        }

                        var objAdmin_SubUser = UhDB.Admin_Sub.Where(x => x.IsActive == true && x.IsDelete == false && x.suID == user.ID).FirstOrDefault();
                        objAdmin_SubUser.Name = user.Name;
                        objAdmin_SubUser.MobileNo = user.MobileNo;
                        objAdmin_SubUser.AlternativeMobileNo = user.AlternativeMobileNo;
                        objAdmin_SubUser.Email = user.Email;
                        objAdmin_SubUser.Gender = user.Gender;
                        objAdmin_SubUser.LandMark = user.LandMark;
                        objAdmin_SubUser.Address = user.Address;
                        objAdmin_SubUser.City = user.City;
                        objAdmin_SubUser.State = user.State;
                        objAdmin_SubUser.Country = user.Country;
                        objAdmin_SubUser.Pincode = user.Pincode;
                        objAdmin_SubUser.PhoneCode = user.PhoneCode;
                        objAdmin_SubUser.UpdatedRole = user.UpdatedRole;
                        objAdmin_SubUser.UpdatedOn = user.UpdatedOn;
                        objAdmin_SubUser.UpdatedBy = user.UpdatedBy;
                        Save();

                    }
                    trans.Commit();
                    result = "SUCCESS";
                }
                catch (Exception ex)
                {
                    result = "Exception";
                }
            }
            return result;
        }

        public GetUpdateUserModel GetUpdateUserDetails(int? uID, int? suID, int? stfID, int? rID, int? cuID)
        {
            GetUpdateUserModel result = new GetUpdateUserModel();
            if (rID == 10)
            {
                result = UhDB.Users.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                       .Select(p => new GetUpdateUserModel
                       {
                           ID = p.uID,
                           Username = p.Login.Username,
                           Name = p.Name,
                           Email = p.Email,
                           MobileNo = p.MobileNo,
                           AlternativeMobileNo = p.AlternativeMobileNo,
                           Gender = p.Gender,
                           City = p.City,
                           State = p.State,
                           LandMark = p.LandMark,
                           Address = p.Address,
                           Country = p.Country,
                           PhoneCode = p.PhoneCode,
                           Pincode = p.Pincode,
                       }).FirstOrDefault();
            }
            else if (rID == 11)
            {
                result = UhDB.Admin_Sub.Where(x => x.suID == suID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                        .Select(p => new GetUpdateUserModel
                        {
                            ID = p.suID,
                            Username = p.Login.Username,
                            Name = p.Name,
                            Email = p.Email,
                            MobileNo = p.MobileNo,
                            AlternativeMobileNo = p.AlternativeMobileNo,
                            Gender = p.Gender,
                            City = p.City,
                            State = p.State,
                            LandMark = p.LandMark,
                            Address = p.Address,
                            Country = p.Country,
                            PhoneCode = p.PhoneCode,
                            Pincode = p.Pincode,
                        }).FirstOrDefault();
            }
            else if (rID == 15)
            {
                result = UhDB.Staffs.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                         .Select(p => new GetUpdateUserModel
                         {
                             ID = p.stfID,
                             Username = p.Login.Username,
                             Name = p.Name,
                             Email = p.Email,
                             MobileNo = p.Mobile,
                             Country = p.Country,
                             PhoneCode = p.PhoneCode,
                         }).FirstOrDefault();
            }
            else if (rID == 14)
            {
                result = UhDB.Customers.Where(x => x.cuID == cuID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                         .Select(p => new GetUpdateUserModel
                         {
                             ID = p.cuID,
                             Username = p.Login.Username,
                             Name = p.Name,
                             Email = p.Email,
                             MobileNo = p.Mobile,
                             AlternativeMobileNo = p.AlternativeNo,
                             WhatsAppNo = p.WhatsAppNo,
                             Country = p.Country,
                             PhoneCode = p.PhoneCode,
                             CustomerID = p.CustomerID.ToString(),
                             IsEmail = p.IsEmail,
                             IsMobile = p.IsMobile,
                             Address = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID && x.IsActive == true && x.IsDelete == false).OrderByDescending(x => x.custODID).FirstOrDefault().AppartmentNumber
                         }).FirstOrDefault();
            }
            else if (rID == 12)
            {
                result = UhDB.Staffs.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                         .Select(p => new GetUpdateUserModel
                         {
                             ID = p.stfID,
                             Username = p.Login.Username,
                             Name = p.Name,
                             Email = p.Email,
                             MobileNo = p.Mobile,
                             //AlternativeMobileNo = p.AlternativeNo,
                             //WhatsAppNo = p.WhatsAppNo,
                             Country = p.Country,
                             PhoneCode = p.PhoneCode,
                             //CustomerID = p.CustomerID.ToString(),
                             //IsEmail = p.IsEmail,
                             //IsMobile = p.IsMobile,
                             Address = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID && x.IsActive == true && x.IsDelete == false).OrderByDescending(x => x.custODID).FirstOrDefault().AppartmentNumber
                         }).FirstOrDefault();
            }
            else
            {
                result = UhDB.Admin_Sub.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false && x.suID == suID).AsEnumerable()
                         .Select(p => new GetUpdateUserModel
                         {
                             ID = (int)p.suID,
                             Username = p.Login.Username,
                             Name = p.Name,
                             Email = p.Email,
                             MobileNo = p.MobileNo,
                             AlternativeMobileNo = p.AlternativeMobileNo,
                             Gender = p.Gender,
                             City = p.City,
                             State = p.State,
                             LandMark = p.LandMark,
                             Address = p.Address,
                             Country = p.Country,
                             PhoneCode = p.PhoneCode,
                             Pincode = p.Pincode,
                         }).FirstOrDefault();
            }
            return result;
        }

        public List<GetCustomerPropertyModel> GetUpdateCustomerDetails(int? cuID)
        {
            List<GetCustomerPropertyModel> result = new List<GetCustomerPropertyModel>();

            var objCustomerOfficalDetails = UhDB.CustomerOfficalDetails.Where(x => x.Customer.cuID == cuID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objCustomerOfficalDetail in objCustomerOfficalDetails)
            {
                int? propType = objCustomerOfficalDetail.propType;
                int? custODID = objCustomerOfficalDetail.custODID;
                if (propType == 2)
                {
                    var objCustomerOtherProperties = UhDB.CustomerOtherProperties.Where(x => x.custODID == custODID && x.custID == cuID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    if (!result.Any(x => x.PropertyArea == objCustomerOfficalDetail.PropertyArea.Name
                    && x.PropertyName == objCustomerOtherProperties.TowerName && x.PropertyResidencyType == objCustomerOfficalDetail.PropertyResidenceType.Name
                    && x.AppartmentNumber == objCustomerOfficalDetail.AppartmentNumber && x.BuildingName == objCustomerOtherProperties.BuildingName
                    && x.StreetNumber == objCustomerOtherProperties.StreetNumber && x.ZoneNumber == objCustomerOtherProperties.ZoneNumber &&
                    x.Loacation == objCustomerOtherProperties.Loacation))
                    {
                        result.Add(new GetCustomerPropertyModel
                        {
                            PropertyName = objCustomerOtherProperties.TowerName,
                            PropertyArea = objCustomerOfficalDetail.PropertyArea.Name,
                            PropertyResidencyType = objCustomerOfficalDetail.proprestID != null ? objCustomerOfficalDetail.PropertyResidenceType.Name : "N/A",
                            AppartmentNumber = objCustomerOfficalDetail.AppartmentNumber,
                            BuildingName = objCustomerOtherProperties.BuildingName,
                            StreetNumber = objCustomerOtherProperties.StreetNumber,
                            ZoneNumber = objCustomerOtherProperties.ZoneNumber,
                            Loacation = objCustomerOtherProperties.Loacation,
                            LocationLink = objCustomerOtherProperties.LocationLink,
                            propType = objCustomerOfficalDetail.propType,
                            custOPID = objCustomerOtherProperties.custOPID,
                            propaID = objCustomerOfficalDetail.propaID,
                            subArea = objCustomerOfficalDetail.subAreaID,
                            vID = objCustomerOfficalDetail.vID,
                            proprestID = objCustomerOfficalDetail.proprestID,
                            custODID = custODID,
                            ID = cuID,
                            SubAreaName = objCustomerOfficalDetail.subAreaID != null ? objCustomerOfficalDetail.SubArea.Name : "N/A"
                        });
                    }
                }
                else
                {
                    if (!result.Any(x => x.propaID == objCustomerOfficalDetail.propaID && x.vID == objCustomerOfficalDetail.vID
                    && x.proprestID == objCustomerOfficalDetail.proprestID))
                    {
                        result.Add(new GetCustomerPropertyModel
                        {
                            PropertyName = objCustomerOfficalDetail.Venture.Name,
                            PropertyArea = objCustomerOfficalDetail.PropertyArea.Name,
                            PropertyResidencyType = objCustomerOfficalDetail.proprestID != null ? objCustomerOfficalDetail.PropertyResidenceType.Name : "N/A",
                            AppartmentNumber = objCustomerOfficalDetail.AppartmentNumber,
                            propType = objCustomerOfficalDetail.propType,
                            propaID = objCustomerOfficalDetail.propaID,
                            vID = objCustomerOfficalDetail.vID,
                            subArea = objCustomerOfficalDetail.subAreaID,
                            proprestID = objCustomerOfficalDetail.proprestID,
                            custODID = custODID,
                            ID = cuID,
                            SubAreaName = objCustomerOfficalDetail.subAreaID != null ? objCustomerOfficalDetail.SubArea.Name : "N/A"
                        });
                    }

                }
            }
            return result;
        }

        public List<GetCustomerCarModel> GetCustomerCarModel(int? cuID)
        {
            List<GetCustomerCarModel> result = new List<GetCustomerCarModel>();

            result = UhDB.CustomerCarServiceDetails.Where(x => x.Customer.cuID == cuID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select(p => new GetCustomerCarModel
                     {
                         ID = p.custID,
                         custODID = p.custODID,
                         custCarsDID = p.custCarsDID,
                         ParkingLevel = p.ParkingLevel,
                         ParkingNo = p.ParkingNumber,
                         VehicleNo = p.VehicleNumber,
                         VehilcleBrand = p.VehicleBrand,
                         VehilcleColor = p.VehicleColor,
                         CarTypeName = p.CustomerOfficalDetail.cartID != null ? p.CustomerOfficalDetail.CarType.Name : null,
                         carType = p.CustomerOfficalDetail.cartID,
                         propaID = p.CustomerOfficalDetail.propaID,
                         vID = p.CustomerOfficalDetail.vID,
                         PropertyArea = p.CustomerOfficalDetail.PropertyArea.Name,
                         PropertyName = p.CustomerOfficalDetail.Venture.Name

                     }).ToList();



            return result;
        }

        public string UpdateCustomerDetails(UpdateCustomerPropertyModel customerUpdate)
        {
            string result = null;
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    var objCustomerOfficalDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == customerUpdate.ID && x.custODID == customerUpdate.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objCustomerOfficalDetails.AppartmentNumber = customerUpdate.AppartmentNumber;
                    objCustomerOfficalDetails.propaID = customerUpdate.propaID;
                    objCustomerOfficalDetails.proprestID = customerUpdate.proprestID;
                    objCustomerOfficalDetails.propType = customerUpdate.propType;
                    objCustomerOfficalDetails.vID = customerUpdate.vID;
                    objCustomerOfficalDetails.UpdatedBy = customerUpdate.UpdatedBy;
                    objCustomerOfficalDetails.UpdatedOn = customerUpdate.UpdatedOn;
                    objCustomerOfficalDetails.UpdatedRole = customerUpdate.UpdatedRole;
                    UhDB.SaveChanges();

                    if (customerUpdate.propType == 2)
                    {
                        var objCustomerOtherProperties = UhDB.CustomerOtherProperties.Where(x => x.custOPID == customerUpdate.custOPID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                        objCustomerOtherProperties.TowerName = customerUpdate.TowerNumber;
                        objCustomerOtherProperties.BuildingName = customerUpdate.BuildingName;
                        objCustomerOtherProperties.StreetNumber = customerUpdate.StreetNumber;
                        objCustomerOtherProperties.ZoneNumber = customerUpdate.ZoneNumber;
                        objCustomerOtherProperties.Loacation = customerUpdate.Loacation;
                        objCustomerOtherProperties.LocationLink = customerUpdate.LocationLink;
                        objCustomerOtherProperties.UpdatedRole = customerUpdate.UpdatedRole;
                        objCustomerOtherProperties.UpdatedOn = customerUpdate.UpdatedOn;
                        objCustomerOtherProperties.UpdatedBy = customerUpdate.UpdatedBy;
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

        public string UpdateCustomerCarDetails(UpdateCustomerCarModel customerUpdate)
        {
            string result = null;
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    var objCustomerOfficalDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == customerUpdate.ID && x.custODID == customerUpdate.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objCustomerOfficalDetails.propaID = customerUpdate.propaID;
                    objCustomerOfficalDetails.cartID = customerUpdate.cartID;
                    objCustomerOfficalDetails.vID = customerUpdate.vID;
                    objCustomerOfficalDetails.UpdatedBy = customerUpdate.UpdatedBy;
                    objCustomerOfficalDetails.UpdatedOn = customerUpdate.UpdatedOn;
                    objCustomerOfficalDetails.UpdatedRole = customerUpdate.UpdatedRole;
                    UhDB.SaveChanges();

                    var objCustomerCarServiceDetails = UhDB.CustomerCarServiceDetails.Where(x => x.custCarsDID == customerUpdate.custCarsDID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objCustomerCarServiceDetails.ParkingLevel = customerUpdate.ParkingLevel;
                    objCustomerCarServiceDetails.ParkingNumber = customerUpdate.ParkingNo;
                    objCustomerCarServiceDetails.VehicleBrand = customerUpdate.VehilcleBrand;
                    objCustomerCarServiceDetails.VehicleColor = customerUpdate.VehilcleColor;
                    objCustomerCarServiceDetails.VehicleNumber = customerUpdate.VehicleNo;
                    objCustomerCarServiceDetails.UpdatedBy = customerUpdate.UpdatedBy;
                    objCustomerCarServiceDetails.UpdatedOn = customerUpdate.UpdatedOn;
                    UhDB.SaveChanges();

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

        public string CreateCustomerCarDetails(CreateCustomerCarModel customerUpdate)
        {
            string result = null;
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    var objCustomerOfficalDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == customerUpdate.ID && x.custODID == customerUpdate.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objCustomerOfficalDetails.propaID = customerUpdate.propaID;
                    objCustomerOfficalDetails.cartID = customerUpdate.cartID;
                    objCustomerOfficalDetails.vID = customerUpdate.vID;
                    objCustomerOfficalDetails.UpdatedBy = customerUpdate.CreatedBy;
                    objCustomerOfficalDetails.UpdatedOn = customerUpdate.CreatedOn;
                    objCustomerOfficalDetails.UpdatedRole = customerUpdate.CreatedRole;
                    UhDB.SaveChanges();

                    CustomerCarServiceDetail objCustomerCarServiceDetails = new CustomerCarServiceDetail();
                    objCustomerCarServiceDetails.custID = customerUpdate.ID;
                    objCustomerCarServiceDetails.custODID = customerUpdate.custODID;
                    objCustomerCarServiceDetails.ParkingLevel = customerUpdate.ParkingLevel;
                    objCustomerCarServiceDetails.ParkingNumber = customerUpdate.ParkingNo;
                    objCustomerCarServiceDetails.VehicleBrand = customerUpdate.VehilcleBrand;
                    objCustomerCarServiceDetails.VehicleColor = customerUpdate.VehilcleColor;
                    objCustomerCarServiceDetails.VehicleNumber = customerUpdate.VehicleNo;
                    objCustomerCarServiceDetails.IsActive = customerUpdate.IsActive;
                    objCustomerCarServiceDetails.IsDelete = customerUpdate.IsDelete;
                    objCustomerCarServiceDetails.CreatedBy = customerUpdate.CreatedBy;
                    objCustomerCarServiceDetails.CreatedOn = customerUpdate.CreatedOn;
                    UhDB.CustomerCarServiceDetails.Add(objCustomerCarServiceDetails);
                    UhDB.SaveChanges();

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



        public GetFileDetails GetProfilePic(int? uID, int? suID, int? stfID, int? rID, int? cuID)
        {
            GetFileDetails result = new GetFileDetails();
            if (rID == 10)
            {
                result = UhDB.Files.Where(x => x.uID == uID && x.FileUse == 2 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                       .Select(p => new GetFileDetails
                       {
                           Name = p.Filename,
                           Size = p.FileSize,
                           ContentType = p.FileContentType,
                           Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/Profile/" + p.FileFieldName
                       }).FirstOrDefault();
            }
            else if (rID == 11)
            {
                result = UhDB.Files.Where(x => x.suID == suID && x.FileUse == 2 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                         .Select(p => new GetFileDetails
                         {
                             Name = p.Filename,
                             Size = p.FileSize,
                             ContentType = p.FileContentType,
                             Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/Profile/" + p.FileFieldName
                         }).FirstOrDefault();
            }
            else if (rID == 12)
            {
                result = UhDB.Files.Where(x => x.stfID == stfID && x.FileUse == 2 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                         .Select(p => new GetFileDetails
                         {
                             Name = p.Filename,
                             Size = p.FileSize,
                             ContentType = p.FileContentType,
                             Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/Profile/" + p.FileFieldName
                         }).FirstOrDefault();
            }
            else if (rID == 14)
            {
                result = UhDB.Files.Where(x => x.cuiD == cuID && x.FileUse == 2 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                         .Select(p => new GetFileDetails
                         {
                             Name = p.Filename,
                             Size = p.FileSize,
                             ContentType = p.FileContentType,
                             Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/Profile/" + p.FileFieldName
                         }).FirstOrDefault();
            }
            else 
            {
                result = UhDB.Files.Where(x => x.suID == suID && x.FileUse == 2 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                         .Select(p => new GetFileDetails
                         {
                             Name = p.Filename,
                             Size = p.FileSize,
                             ContentType = p.FileContentType,
                             Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/Profile/" + p.FileFieldName
                         }).FirstOrDefault();
            }
            return result;
        }

        public int GetUserIsActive(string Username)
        {
            int UserActiveCount = 0;
            UserActiveCount = UhDB.Logins.Where(x => x.Username == Username && x.IsActive == true).Count();
            return UserActiveCount;
        }

        public int GetUserCount(string Username)
        {
            return UhDB.Logins.Where(x => x.Username == Username && x.IsActive == true && x.IsDelete == false).Count();
        }

        public string CustomerVerification(CustomerVerification customer)
        {
            string result = null;
            if (customer.IsEmail == true)
            {
                var objUpdateCustomer = UhDB.Customers.Where(x => x.cuID == customer.cuID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                objUpdateCustomer.IsEmail = true;
                objUpdateCustomer.UpdatedBy = customer.UpdatedBy;
                objUpdateCustomer.UpdatedOn = customer.UpdatedOn;
                objUpdateCustomer.UpdatedRole = customer.UpdatedRole;
                Save();
                result = "SUCCESS";
            }
            else
            {
                var objUpdateCustomer = UhDB.Customers.Where(x => x.cuID == customer.cuID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                objUpdateCustomer.IsMobile = true;
                objUpdateCustomer.UpdatedBy = customer.UpdatedBy;
                objUpdateCustomer.UpdatedOn = customer.UpdatedOn;
                objUpdateCustomer.UpdatedRole = customer.UpdatedRole;
                Save();
                result = "SUCCESS";
            }
            return result;
        }

        private void Save()
        {
            UhDB.SaveChanges();
        }

        public string SendSMSFromAmazon(string message, string number)
        {
            string result = null;
            try
            {
                var credentals = new Amazon.Runtime.BasicAWSCredentials("AKIA6ODUZ3JIXO5DNYNF", "ehg4JK9Ii8ComVCSdhMtak8nNMT7+s+KRCRRISgq");
                var client = new AmazonSimpleNotificationServiceClient(credentals, region: Amazon.RegionEndpoint.EUNorth1);
                var request = new PublishRequest
                {
                    Message = message,
                    PhoneNumber = number
                };
                var response = client.Publish(request);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    result = "SUCCESS";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public string SentEmailFromAmazon(string to, string body, string subject, string name)
        {
            string result = null;
            string senderAddress = "Bookings@urbanhospitalityservices.com";
            string htmlBody = body;
            var credentals = new BasicAWSCredentials("AKIA6ODUZ3JIXO5DNYNF", "ehg4JK9Ii8ComVCSdhMtak8nNMT7+s+KRCRRISgq");
            using (var client = new AmazonSimpleEmailServiceClient(credentals, RegionEndpoint.EUNorth1))
            {
                var sendRequest = new SendEmailRequest
                {
                    Source = senderAddress,
                    Destination = new Destination
                    {
                        ToAddresses =
                        new List<string> { to }
                    },
                    Message = new Message
                    {
                        Subject = new Amazon.SimpleEmail.Model.Content(subject),
                        Body = new Body
                        {
                            Html = new Amazon.SimpleEmail.Model.Content
                            {
                                Charset = "UTF-8",
                                Data = htmlBody//body
                            }
                        },

                    },
                };
                try
                {
                    var response = client.SendEmail(sendRequest);
                    if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        result = "SUCCESS";
                    }
                    else
                    {
                        result = "Not Send";
                    }
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            }
            return result;
        }

        public string SentMultipleEmailFromAmazon(string body, string subject)
        {
            string result = null;
            string senderAddress = "Bookings@urbanhospitalityservices.com";
            string htmlBody = body;
            List<string> MultipleEmailIDs = new List<string>();
            //MultipleEmailIDs.Add("s.sharif@detentech.com");
            MultipleEmailIDs.Add("a.khayyum@detentech.com");
            MultipleEmailIDs.Add("n.uddin@detentech.com");
            var credentals = new BasicAWSCredentials("AKIA6ODUZ3JIXO5DNYNF", "ehg4JK9Ii8ComVCSdhMtak8nNMT7+s+KRCRRISgq");
            using (var client = new AmazonSimpleEmailServiceClient(credentals, RegionEndpoint.EUNorth1))
            {
                var sendRequest = new SendEmailRequest
                {
                    Source = senderAddress,
                    Destination = new Destination
                    {
                        ToAddresses = MultipleEmailIDs

                    },
                    Message = new Message
                    {
                        Subject = new Amazon.SimpleEmail.Model.Content(subject),
                        Body = new Body
                        {
                            Html = new Amazon.SimpleEmail.Model.Content
                            {
                                Charset = "UTF-8",
                                Data = htmlBody//body
                            }
                        },

                    },
                };
                try
                {
                    var response = client.SendEmail(sendRequest);
                    result = response.ToString();
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            }
            return result;
        }

        public string SendEmailWithAttachmentForBodyAttachment(string pdfBase64, string toEmail, string subject, string PaymentLink)
        {
            string result = "";

            byte[] pdfBytes = Convert.FromBase64String(pdfBase64);
            MemoryStream pdfStream = new MemoryStream(pdfBytes);

            try
            {
                var credentals =
                    new BasicAWSCredentials("AKIA6ODUZ3JIXO5DNYNF", "ehg4JK9Ii8ComVCSdhMtak8nNMT7+s+KRCRRISgq");
                using (var client = new AmazonSimpleEmailServiceClient(credentals, RegionEndpoint.EUNorth1))
                {
                    var sendRequest = new SendRawEmailRequest
                    {
                        RawMessage = new RawMessage
                        {
                            Data = new MemoryStream(Encoding.UTF8.GetBytes(
                                  "From: Bookings@urbanhospitalityservices.com\r\n" +
                                "To: " + toEmail + "\r\n" +
                               "Subject: " + subject + "\r\n" +
                                "MIME-Version: 1.0\r\n" +
                                "Content-Type: multipart/mixed; boundary=\"NextPart\"\r\n\r\n" +
                                "--NextPart\r\n" +
                                "Content-Type: text/html\r\n\r\n" +
                                 "<h3 style='font-size:16px;'>Hi,</h3> \r\n\r\n" +
                                  "<p style='color:#000;font-size:16px;'>Please find the attached Invoice requested for your reference.</p>\r\n" +
                                  "<p style = 'color:#000;font-size:16px;'> To proceed with payment, please click the button below. If you have already paid, kindly ignore this message.</ p ><br/><br/>" +
                                  "<a href='" + PaymentLink + "' style='display:inline-block;background-color:#007bff;color:#fff;padding:05px 20px;text-decoration:none;border-radius:5px;margin-top: 20px;'>Pay Now</a>" +
                                 "<p style='color:#000;font-size:16px;'>Best Regards,</p><p style='color:#000;font-size:16px;margin-top: -12px;'>Urban Hospitality Service</p><br/> \r\n" +
                                "--NextPart\r\n" +
                                "Content-Type: application/pdf; name=Invoice.pdf\r\n" +
                                "Content-Disposition: attachment; filename=attachment.pdf\r\n" +
                                "Content-Transfer-Encoding: base64\r\n\r\n" +
                                Convert.ToBase64String(pdfBytes) + "\r\n\r\n" +
                                "--NextPart\r\n" +
                                "Content-Type: text/plain\r\n\r\n" +
                                "Disclaimer: This email and any files transmitted with it are confidential and intended solely for the use of the recipient(s). If you have received this email in error please delete it and notify the sender. The content of this email and its attachements are maintained and managed by the client,  holds no responsibility on its accuracy.\r\n\r\n" +
                                "--NextPart--"
                            ))
                        }
                    };
                    var response = client.SendRawEmail(sendRequest);

                }
                result = "SUCCESS";
            }
            catch (Exception ex)
            {
                result = "Exception";
            }

            return result;
        }

        public string SendSMS(string MobileNo, string body)
        {
            string result = null;
            try
            {
                // Step 1: Authenticate the user
                MessengerService.SoapUser user = new MessengerService.SoapUser
                {
                    CustomerID = 6264, // Provide your customer ID
                    Name = "UHS",   // Replace with your username
                    Language = "en", // Language
                    Password = "Doha@123#" // Replace with your password
                };

                // Call the Authenticate function
                AuthResult authResult = messenger.Authenticate(user);

                if (authResult.Result != "OK")
                {
                    result = "Authentication Failed: " + authResult.Result;
                }

                // Step 2: Send SMS
                MobileNo = MobileNo.Replace(" ", "");
                MobileNo = MobileNo.Replace("+", "");
                string originator = authResult.Originators[0]; // Use the first originator
                string smsData = body;
                string phoneNumbers = MobileNo; // Replace with recipient phone numbers
                string defDate = ""; // Deferred delivery time (optional)

                SendResult sendResult = messenger.SendSms(user, originator, smsData, phoneNumbers,
                                                           MessageType.Latin, defDate, false, false, false);

                if (sendResult.Result == "OK")
                {
                    return result = "OK";

                }
                else
                {
                    return result = "Not Send";
                    //return Content("SMS Failed: " + sendResult.Result);
                }
            }
            catch (Exception ex)
            {
                return result = "Exception";
            }
        }

        public string Encrypt(string clearText, string enKey)
        {
            string EncryptionKey = enKey;
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Padding = PaddingMode.Zeros;
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public string Decrypt(string cipherText, string deKey)
        {
            string EncryptionKey = deKey;
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Padding = PaddingMode.Zeros;
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

    }
}