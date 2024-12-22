using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;

namespace UHSForm.DAL
{
    public class SubCategoryDB
    {
        private UHSEntities UhDB;

        public SubCategoryDB()
        {
            UhDB = new UHSEntities();
        }

        public int? CreateSubCategory(SubCategoryModel category)
        {
            int? result = null;
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    SubCategory objSubCategory = new SubCategory();
                    objSubCategory.Name = category.Name;
                    objSubCategory.catID = category.catID;
                    objSubCategory.IsActive = category.IsActive;
                    objSubCategory.IsDelete = category.IsDelete;
                    objSubCategory.CreatedBy = category.CreatedBy;
                    objSubCategory.CreatedOn = category.CreatedOn;
                    UhDB.SubCategories.Add(objSubCategory);
                    Save();

                    var objMainCategory = UhDB.MainCategories.Where(x => x.catID == category.catID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objMainCategory.Status = true;
                    objMainCategory.UpdatedBy = category.CreatedBy;
                    objMainCategory.UpdatedOn = category.CreatedOn;
                    Save();

                    trans.Commit();
                    result = objSubCategory.catsubID;

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    result = 0;

                }
            }

            return result;

        }

        public string UpdateSubCategory(UpdateSubCategoryModel category)
        {
            string result = null;
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    var objSubCategory = UhDB.SubCategories.Where(x => x.catsubID == category.catsubID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objSubCategory.Name = category.Name;
                    objSubCategory.catID = category.catID;
                    objSubCategory.UpdatedBy = category.UpdatedBy;
                    objSubCategory.UpdatedOn = category.UpdatedOn;
                    Save();

                    var objMainCategory = UhDB.MainCategories.Where(x => x.catID == category.catID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objMainCategory.Status = true;
                    objMainCategory.UpdatedBy = category.UpdatedBy;
                    objMainCategory.UpdatedOn = category.UpdatedOn;
                    Save();

                    trans.Commit();
                    result = "SUCCESS";
                }
                catch (Exception ex)
                {
                    trans.Commit();
                    result = "Exception";
                }
            }

            return result;

        }

        public string DeleteSubCategory(DeleteSubCategoryModel category)
        {
            string result = null;

            var objSubCategory = UhDB.SubCategories.Where(x => x.catsubID == category.catsubID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            if (objSubCategory.Status == true)
            {
                result = "Can't";
            }
            else
            {
                var objDeleteMainCategory = UhDB.SubCategories.Where(x => x.catsubID == category.catsubID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                objDeleteMainCategory.IsActive = category.IsActive;
                objDeleteMainCategory.IsDelete = category.IsDelete;
                objDeleteMainCategory.UpdatedBy = category.UpdatedBy;
                objDeleteMainCategory.UpdatedOn = category.UpdatedOn;
                Save();
                result = "SUCCESS";
            }

            return result;

        }

        public IEnumerable<GetSubCategoryModel> GetSubCategories(int? uID)
        {
            List<GetSubCategoryModel> result = new List<GetSubCategoryModel>();

            result = UhDB.SubCategories.Where(x => x.MainCategory.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                      .Select(p => new Models.GetSubCategoryModel
                      {
                          Name = p.Name,
                          MainCategoryName = p.MainCategory.Name,
                          CreatedBy = p.CreatedBy,
                          CreatedOn = p.CreatedOn,
                          catID = p.catID,
                          catsubID = p.catsubID,
                          Images = UhDB.Files.Where(x => x.uID == uID && x.catsubID == p.catsubID && x.FileUse == 4 && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                    UhDB.Files.Where(x => x.uID == uID && x.catsubID == p.catsubID && x.FileUse == 4 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                    .Select(r => new GetFileDetails
                                    {
                                        Name = r.Filename,
                                        Size = r.FileSize,
                                        ContentType = r.FileContentType,
                                        Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/SubCategory/" + r.FileFieldName
                                    }).ToList() : null
                      }).ToList();

            return result;
        }

        public GetSubCategoryModel GetSubCategoryByID(int? uID, int? catsubID)
        {
            GetSubCategoryModel result = new GetSubCategoryModel();

            result = UhDB.SubCategories.Where(x => x.MainCategory.uID == uID && x.catsubID == catsubID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                      .Select(p => new Models.GetSubCategoryModel
                      {
                          Name = p.Name,
                          MainCategoryName = p.MainCategory.Name,
                          CreatedBy = p.CreatedBy,
                          CreatedOn = p.CreatedOn,
                          catID = p.catID,
                          catsubID = p.catsubID,
                          Images = UhDB.Files.Where(x => x.uID == uID && x.catsubID == p.catsubID && x.FileUse == 4 && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                    UhDB.Files.Where(x => x.uID == uID && x.catsubID == p.catsubID && x.FileUse == 4 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                    .Select(r => new GetFileDetails
                                    {
                                        Name = r.Filename,
                                        Size = r.FileSize,
                                        ContentType = r.FileContentType,
                                        Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/SubCategory/" + r.FileFieldName
                                    }).ToList() : null
                      }).FirstOrDefault();

            return result;
        }

        public List<GetSubCategoryModel> GetSubCategoriesByCatID(int? uID, int? catID)
        {
            List<GetSubCategoryModel> result = new List<GetSubCategoryModel>();

            result = UhDB.SubCategories.Where(x => x.MainCategory.uID == uID && x.catID == catID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                      .Select(p => new Models.GetSubCategoryModel
                      {
                          Name = p.Name,
                          MainCategoryName = p.MainCategory.Name,
                          CreatedBy = p.CreatedBy,
                          CreatedOn = p.CreatedOn,
                          catID = p.catID,
                          catsubID = p.catsubID
                      }).ToList();

            return result;
        }

        public IEnumerable<GetDropDown> GetSubCategoryDropDown(int? uID)
        {
            List<GetDropDown> result = new List<GetDropDown>();

            result = UhDB.SubCategories.Where(x => x.MainCategory.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                      .Select(p => new Models.GetDropDown
                      {
                          Value = p.Name,
                          ID = p.catsubID
                      }).ToList();

            return result;
        }

        public void Save()
        {
            UhDB.SaveChanges();
        }
    }
}