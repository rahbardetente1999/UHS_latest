using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;

namespace UHSForm.DAL
{
    public class MainCategoryDB
    {
        private UHSEntities UhDB;

        public MainCategoryDB()
        {
            UhDB = new UHSEntities();
        }

        public int? CreateMainCategory(MainCategoryModel category)
        {
            int? result = null;
            MainCategory objMainCategory = new MainCategory();
            objMainCategory.Name = category.Name;
            objMainCategory.IsFlag = category.IsFlag;
            objMainCategory.uID = category.uID;
            if (category.CreatedRole != 10)
            {
                objMainCategory.suID = category.suID;
            }
            objMainCategory.IsActive = category.IsActive;
            objMainCategory.IsDelete = category.IsDelete;
            objMainCategory.CreatedBy = category.CreatedBy;
            objMainCategory.CreatedOn = category.CreatedOn;
            UhDB.MainCategories.Add(objMainCategory);
            Save();
            result = objMainCategory.catID;
            return result;

        }

        public string UpdateMainCategory(UpdateMainCategoryModel category)
        {
            string result = null;
            var objMainCategory = UhDB.MainCategories.Where(x => x.catID == category.catID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            objMainCategory.Name = category.Name;
            objMainCategory.UpdatedBy = category.UpdatedBy;
            objMainCategory.UpdatedOn = category.UpdatedOn;
            Save();
            result = "SUCCESS";
            return result;

        }

        public string UpdateMainCategoryFlag(UpdateMainCategoryFlagModel category)
        {
            string result = null;
            var objMainCategory = UhDB.MainCategories.Where(x => x.catID == category.catID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            objMainCategory.IsFlag = category.IsFlag;
            objMainCategory.UpdatedBy = category.UpdatedBy;
            objMainCategory.UpdatedOn = category.UpdatedOn;
            Save();
            result = "SUCCESS";
            return result;

        }

        public string DeleteMainCategory(DeleteMainCategoryModel category)
        {
            string result = null;

            var objMainCategory = UhDB.MainCategories.Where(x => x.catID == category.catID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            if (objMainCategory.Status == true)
            {
                result = "Can't";
            }
            else
            {
                var objDeleteMainCategory = UhDB.MainCategories.Where(x => x.catID == category.catID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                objDeleteMainCategory.IsActive = category.IsActive;
                objDeleteMainCategory.IsDelete = category.IsDelete;
                objDeleteMainCategory.UpdatedBy = category.UpdatedBy;
                objDeleteMainCategory.UpdatedOn = category.UpdatedOn;
                Save();
                result = "SUCCESS";
            }

            return result;

        }

        public IEnumerable<GetMainCategoryModel> GetMainCategories(int? uID)
        {
            List<GetMainCategoryModel> result = new List<GetMainCategoryModel>();

            result = UhDB.MainCategories.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                      .Select(p => new Models.GetMainCategoryModel
                      {
                          Name = p.Name,
                          IsFlag = p.IsFlag,
                          Flag = p.IsFlag == true ? "Active" : "InActive",
                          CreatedBy = p.CreatedBy,
                          CreatedOn = p.CreatedOn,
                          catID = p.catID,
                          Images = UhDB.Files.Where(x => x.uID == uID && x.catID == p.catID && x.FileUse == 1 && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                    UhDB.Files.Where(x => x.uID == uID && x.catID == p.catID && x.FileUse == 1 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                    .Select(r => new GetFileDetails
                                    {
                                        Name = r.Filename,
                                        Size = r.FileSize,
                                        ContentType = r.FileContentType,
                                        Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/MainCategory/" + r.FileFieldName
                                    }).ToList() : null


                      }).ToList();

            return result;
        }

        public GetMainCategoryModel GetMainCategoryByID(int? uID, int? catID)
        {
            GetMainCategoryModel result = new GetMainCategoryModel();

            result = UhDB.MainCategories.Where(x => x.uID == uID && x.catID == catID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                      .Select(p => new Models.GetMainCategoryModel
                      {
                          Name = p.Name,
                          IsFlag = p.IsFlag,
                          Flag = p.IsFlag == true ? "Active" : "InActive",
                          CreatedBy = p.CreatedBy,
                          CreatedOn = p.CreatedOn,
                          catID = p.catID,
                          Images = UhDB.Files.Where(x => x.uID == uID && x.catID == p.catID && x.FileUse == 1 && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                    UhDB.Files.Where(x => x.uID == uID && x.catID == p.catID && x.FileUse == 1 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                    .Select(r => new GetFileDetails
                                    {
                                        Name = r.Filename,
                                        Size = r.FileSize,
                                        ContentType = r.FileContentType,
                                        Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/MainCategory/" + r.FileFieldName
                                    }).ToList() : null
                      }).FirstOrDefault();

            return result;
        }

        public IEnumerable<GetDropDown> GetMainCategoryDropDown(int? uID)
        {
            List<GetDropDown> result = new List<GetDropDown>();

            result = UhDB.MainCategories.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                      .Select(p => new Models.GetDropDown
                      {
                          Value = p.Name,
                          ID = p.catID
                      }).ToList();

            return result;
        }

        public IEnumerable<GetMainCategoryDropdownModel> GetMainCategoryDropdown(int? uID)
        {
            List<GetMainCategoryDropdownModel> result = new List<GetMainCategoryDropdownModel>();

            result = UhDB.MainCategories.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                      .Select(p => new Models.GetMainCategoryDropdownModel
                      {
                          Value = p.Name,
                          ID = p.catID,
                          Images = UhDB.Files.Where(x => x.uID == uID && x.catID == p.catID && x.FileUse == 1 && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                    UhDB.Files.Where(x => x.uID == uID && x.catID == p.catID && x.FileUse == 1 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                    .Select(r => new GetFileDetails
                                    {
                                        Name = r.Filename,
                                        Size = r.FileSize,
                                        ContentType = r.FileContentType,
                                        Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/MainCategory/" + r.FileFieldName
                                    }).ToList() : null,
                          IsFlag = p.IsFlag

                      }).ToList();

            return result;
        }

        public void Save()
        {
            UhDB.SaveChanges();
        }
    }
}