using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;

namespace UHSForm.DAL
{
    public class ServiceCategoryDB
    {
        private UHSEntities UhDB;

        public ServiceCategoryDB()
        {
            UhDB = new UHSEntities();
        }

        public int? CreateServiceCategory(ServiceCategoryModel category)
        {
            int? result = null;
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    int? catID = UhDB.SubCategories.Where(x => x.catsubID == category.catsubID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().catID;

                    ServiceCategory objServiceCategory = new ServiceCategory();
                    objServiceCategory.Name = category.Name;
                    objServiceCategory.catID = catID;
                    objServiceCategory.catsubID = category.catsubID;
                    objServiceCategory.IsActive = category.IsActive;
                    objServiceCategory.IsDelete = category.IsDelete;
                    objServiceCategory.CreatedBy = category.CreatedBy;
                    objServiceCategory.CreatedOn = category.CreatedOn;
                    UhDB.ServiceCategories.Add(objServiceCategory);
                    Save();

                    var objSubCategory = UhDB.SubCategories.Where(x => x.catsubID == category.catsubID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objSubCategory.Status = true;
                    objSubCategory.UpdatedBy = category.CreatedBy;
                    objSubCategory.UpdatedOn = category.CreatedOn;
                    Save();

                    trans.Commit();
                    result = objServiceCategory.servcatID;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    result = 0;

                }
            }
            return result;
        }

        public string UpdateServiceCategory(UpdateServiceCategoryModel category)
        {
            string result = null;
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    var objServiceCategory = UhDB.ServiceCategories.Where(x => x.servcatID == category.servcatID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objServiceCategory.Name = category.Name;
                    objServiceCategory.catsubID = category.catsubID;
                    objServiceCategory.UpdatedBy = category.UpdatedBy;
                    objServiceCategory.UpdatedOn = category.UpdatedOn;
                    Save();

                    var objSubCategory = UhDB.SubCategories.Where(x => x.catsubID == category.catsubID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objSubCategory.Status = true;
                    objSubCategory.UpdatedBy = category.UpdatedBy;
                    objSubCategory.UpdatedOn = category.UpdatedOn;

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

        public string DeleteServiceCategory(DeleteServiceCategoryModel category)
        {
            string result = null;

            var objServiceCategory = UhDB.ServiceCategories.Where(x => x.servcatID == category.servcatID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            if (objServiceCategory.Status == true)
            {
                result = "Can't";
            }
            else
            {
                var objDeleteServiceCategory = UhDB.ServiceCategories.Where(x => x.servcatID == category.servcatID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                objDeleteServiceCategory.IsActive = category.IsActive;
                objDeleteServiceCategory.IsDelete = category.IsDelete;
                objDeleteServiceCategory.UpdatedBy = category.UpdatedBy;
                objDeleteServiceCategory.UpdatedOn = category.UpdatedOn;
                Save();
                result = "SUCCESS";
            }

            return result;

        }

        public IEnumerable<GetServiceCategoryModel> GetServiceCategories(int? uID)
        {
            List<GetServiceCategoryModel> result = new List<GetServiceCategoryModel>();

            result = UhDB.ServiceCategories.Where(x => x.MainCategory.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                      .Select(p => new Models.GetServiceCategoryModel
                      {
                          Name = p.Name,
                          MainCategoryName = p.MainCategory.Name,
                          SubCategoryName = p.SubCategory.Name,
                          CreatedBy = p.CreatedBy,
                          CreatedOn = p.CreatedOn,
                          catID = p.catID,
                          catsubID = p.catsubID,
                          servcatID = p.servcatID,
                          Images = UhDB.Files.Where(x => x.uID == uID && x.servcatID == p.servcatID && x.FileUse == 3 && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                    UhDB.Files.Where(x => x.uID == uID && x.servcatID == p.servcatID && x.FileUse == 3 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                    .Select(r => new GetFileDetails
                                    {
                                        Name = r.Filename,
                                        Size = r.FileSize,
                                        ContentType = r.FileContentType,
                                        Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/ServiceCategory/" + r.FileFieldName
                                    }).ToList() : null
                      }).ToList();

            return result;
        }

        public GetServiceCategoryModel GetServiceCategoryByID(int? uID, int? servcatID)
        {
            GetServiceCategoryModel result = new GetServiceCategoryModel();

            result = UhDB.ServiceCategories.Where(x => x.MainCategory.uID == uID && x.servcatID == servcatID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                      .Select(p => new Models.GetServiceCategoryModel
                      {
                          Name = p.Name,
                          MainCategoryName = p.MainCategory.Name,
                          SubCategoryName = p.SubCategory.Name,
                          CreatedBy = p.CreatedBy,
                          CreatedOn = p.CreatedOn,
                          catID = p.catID,
                          catsubID = p.catsubID,
                          Images = UhDB.Files.Where(x => x.uID == uID && x.servcatID == p.servcatID && x.FileUse == 3 && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                    UhDB.Files.Where(x => x.uID == uID && x.servcatID == p.servcatID && x.FileUse == 3 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                    .Select(r => new GetFileDetails
                                    {
                                        Name = r.Filename,
                                        Size = r.FileSize,
                                        ContentType = r.FileContentType,
                                        Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/ServiceCategory/" + r.FileFieldName
                                    }).ToList() : null
                      }).FirstOrDefault();

            return result;
        }

        public IEnumerable<GetDropDown> GetGetServiceCategoryDroDown(int? uID)
        {
            List<GetDropDown> result = new List<GetDropDown>();
            result = UhDB.ServiceCategories.Where(x => x.MainCategory.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select(p => new GetDropDown { ID = p.servcatID, Value = p.Name }).ToList();
            return result;
        }

        private void Save()
        {
            UhDB.SaveChanges();
        }
    }
}