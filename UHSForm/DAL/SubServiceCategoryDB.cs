using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;

namespace UHSForm.DAL
{
    public class SubServiceCategoryDB
    {
        private UHSEntities UhDB;

        public SubServiceCategoryDB()
        {
            UhDB = new UHSEntities();
        }

        public int? CreateServiceSubCategory(SubServiceCategoryModel category)
        {
            int? result = null;
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    var objServiceCategories = UhDB.ServiceCategories.Where(x => x.servcatID == category.servcatID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    int? catID = objServiceCategories.catID;
                    int? catsubID = objServiceCategories.catsubID;

                    ServiceSubCategory objServiceSubCategory = new ServiceSubCategory();
                    objServiceSubCategory.Name = category.Name;
                    objServiceSubCategory.catID = catID;
                    objServiceSubCategory.catsubID = catsubID;
                    objServiceSubCategory.servcatID = category.servcatID;
                    objServiceSubCategory.IsActive = category.IsActive;
                    objServiceSubCategory.IsDelete = category.IsDelete;
                    objServiceSubCategory.CreatedBy = category.CreatedBy;
                    objServiceSubCategory.CreatedOn = category.CreatedOn;
                    UhDB.ServiceSubCategories.Add(objServiceSubCategory);
                    Save();

                    var objServiceCategory = UhDB.ServiceCategories.Where(x => x.servcatID == category.servcatID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objServiceCategory.Status = true;
                    objServiceCategory.UpdatedBy = category.CreatedBy;
                    objServiceCategory.UpdatedOn = category.CreatedOn;
                    Save();

                    trans.Commit();
                    result = objServiceSubCategory.servsubcatID;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    result = 0;

                }
            }
            return result;
        }

        public IEnumerable<GetSubServiceCategoryModel> GetSubServiceCategories(int? uID)
        {
            List<GetSubServiceCategoryModel> result = new List<GetSubServiceCategoryModel>();

            result = UhDB.ServiceSubCategories.Where(x => x.MainCategory.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                      .Select(p => new Models.GetSubServiceCategoryModel
                      {
                          Name = p.Name,
                          MainCategoryName = p.MainCategory.Name,
                          SubCategoryName = p.SubCategory.Name,
                          ServiceCategoryName = p.ServiceCategory.Name,
                          CreatedBy = p.CreatedBy,
                          CreatedOn = p.CreatedOn,
                          catID = p.catID,
                          catsubID = p.catsubID,
                          servcatID = p.servcatID,
                          servsubcatID = p.servsubcatID,
                          Images = UhDB.Files.Where(x => x.uID == uID && x.servsubcatID == p.servsubcatID && x.FileUse == 5 && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                   UhDB.Files.Where(x => x.uID == uID && x.servsubcatID == p.servsubcatID && x.FileUse == 5 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                    .Select(r => new GetFileDetails
                                    {
                                        Name = r.Filename,
                                        Size = r.FileSize,
                                        ContentType = r.FileContentType,
                                        Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/SubServiceCategory/" + r.FileFieldName
                                    }).ToList() : null

                      }).ToList();

            return result;
        }

        public GetSubServiceCategoryModel GetSubServiceCategoryByID(int? uID, int? servsubcatID)
        {
            GetSubServiceCategoryModel result = new GetSubServiceCategoryModel();

            result = UhDB.ServiceSubCategories.Where(x => x.MainCategory.uID == uID && x.servsubcatID == servsubcatID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                      .Select(p => new Models.GetSubServiceCategoryModel
                      {
                          Name = p.Name,
                          MainCategoryName = p.MainCategory.Name,
                          SubCategoryName = p.SubCategory.Name,
                          ServiceCategoryName = p.ServiceCategory.Name,
                          CreatedBy = p.CreatedBy,
                          CreatedOn = p.CreatedOn,
                          catID = p.catID,
                          catsubID = p.catsubID,
                          servcatID = p.servcatID,
                          Images = UhDB.Files.Where(x => x.uID == uID && x.servsubcatID == p.servsubcatID && x.FileUse == 5 && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                   UhDB.Files.Where(x => x.uID == uID && x.servsubcatID == p.servsubcatID && x.FileUse == 5 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                    .Select(r => new GetFileDetails
                                    {
                                        Name = r.Filename,
                                        Size = r.FileSize,
                                        ContentType = r.FileContentType,
                                        Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/SubServiceCategory/" + r.FileFieldName
                                    }).ToList() : null
                      }).FirstOrDefault();

            return result;
        }

        public IEnumerable<GetDropDown> GetSubServiceCategoryDroDown(int? uID)
        {
            List<GetDropDown> result = new List<GetDropDown>();
            result = UhDB.ServiceSubCategories.Where(x => x.MainCategory.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select(p => new GetDropDown { ID = p.servsubcatID, Value = p.Name }).ToList();
            return result;
        }

        public string UpdateSubServiceCategory(UpdateSubServiceCategoryModel category)
        {
            string result = null;
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    var objServiceSubCategory = UhDB.ServiceSubCategories.Where(x => x.servsubcatID == category.servsubcatID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objServiceSubCategory.Name = category.Name;
                    objServiceSubCategory.servcatID = category.servcatID;
                    objServiceSubCategory.UpdatedBy = category.UpdatedBy;
                    objServiceSubCategory.UpdatedOn = category.UpdatedOn;
                    Save();

                    var objServiceCategory = UhDB.ServiceCategories.Where(x => x.servcatID == category.servcatID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objServiceCategory.Status = true;
                    objServiceCategory.UpdatedBy = category.UpdatedBy;
                    objServiceCategory.UpdatedOn = category.UpdatedOn;

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

        public string DeleteServiceCategory(DeleteSubServiceCategoryModel category)
        {
            string result = null;

            var objServiceSubCategory = UhDB.ServiceSubCategories.Where(x => x.servsubcatID == category.servsubcatID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            if (objServiceSubCategory.Status == true)
            {
                result = "Can't";
            }
            else
            {
                var objDeleteServiceSubCategory = UhDB.ServiceSubCategories.Where(x => x.servsubcatID == category.servsubcatID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                objDeleteServiceSubCategory.IsActive = category.IsActive;
                objDeleteServiceSubCategory.IsDelete = category.IsDelete;
                objDeleteServiceSubCategory.UpdatedBy = category.UpdatedBy;
                objDeleteServiceSubCategory.UpdatedOn = category.UpdatedOn;
                Save();
                result = "SUCCESS";
            }

            return result;

        }


        private void Save()
        {
            UhDB.SaveChanges();
        }
    }
}