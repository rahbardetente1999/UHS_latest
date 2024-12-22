using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models;
using UHSForm.Models.Data;

namespace UHSForm.DAL
{
    public class CommonServiceDB
    {
        private UHSEntities UhDB;

        public CommonServiceDB()
        {
            UhDB = new UHSEntities();
        }

        public IEnumerable<GetDropDown> GetSubCategoryByCatIDDropDown(int? catID, int? uID)
        {
            List<GetDropDown> result = new List<GetDropDown>();
            result = UhDB.SubCategories.Where(x => x.catID == catID && x.MainCategory.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                   .Select(p => new GetDropDown { ID = p.catsubID, Value = p.Name }).ToList();
            return result;
        }

        public IEnumerable<GetDropDown> GetServiceCategoryByCatSubIDDropDown(int? catsubID, int? uID)
        {
            List<GetDropDown> result = new List<GetDropDown>();
            result = UhDB.ServiceCategories.Where(x => x.catsubID == catsubID && x.MainCategory.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                   .Select(p => new GetDropDown { ID = p.servcatID, Value = p.Name }).ToList();
            return result;
        }

        public IEnumerable<GetDropDown> GetSubServiceCategoryByServCatIDDropDown(int? servcatID, int? uID)
        {
            List<GetDropDown> result = new List<GetDropDown>();
            result = UhDB.ServiceSubCategories.Where(x => x.servcatID == servcatID && x.MainCategory.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                   .Select(p => new GetDropDown { ID = p.servsubcatID, Value = p.Name }).ToList();
            return result;
        }

        public IEnumerable<GetDropDownWithImages> GetSubCategoryByCatIDDropDownWithImages(int? catID, int? uID)
        {
            List<GetDropDownWithImages> result = new List<GetDropDownWithImages>();
            result = UhDB.SubCategories.Where(x => x.catID == catID && x.MainCategory.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                   .Select(p => new GetDropDownWithImages
                   {
                       ID = p.catsubID,
                       Value = p.Name,
                       Images = UhDB.Files.Where(x => x.uID == uID && x.catsubID == p.catsubID && x.FileUse == 4 && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                    UhDB.Files.Where(x => x.uID == uID && x.catsubID == p.catsubID && x.FileUse == 4 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                    .Select(r => new GetFileDetails
                                    {
                                        Name = r.Filename,
                                        Size = r.FileSize,
                                        ContentType = r.FileContentType,
                                        Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/SubCategory/" + r.FileFieldName
                                    }).ToList() : null,
                       NextServices = UhDB.ServiceCategories.Where(x => x.catsubID == p.catsubID && x.IsActive == true && x.IsDelete == false).Count()
                   }).ToList();
            return result;
        }

        public IEnumerable<GetDropDownWithImages> GetServiceCategoryByCatSubIDDropDownWithImages(int? catsubID, int? uID)
        {
            List<GetDropDownWithImages> result = new List<GetDropDownWithImages>();
            result = UhDB.ServiceCategories.Where(x => x.catsubID == catsubID && x.MainCategory.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                   .Select(p => new GetDropDownWithImages
                   {
                       ID = p.servcatID,
                       Value = p.Name,
                       Images = UhDB.Files.Where(x => x.uID == uID && x.servcatID == p.servcatID && x.FileUse == 3 && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                    UhDB.Files.Where(x => x.uID == uID && x.servcatID == p.servcatID && x.FileUse == 3 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                    .Select(r => new GetFileDetails
                                    {
                                        Name = r.Filename,
                                        Size = r.FileSize,
                                        ContentType = r.FileContentType,
                                        Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/ServiceCategory/" + r.FileFieldName
                                    }).ToList() : null,
                       NextServices = UhDB.ServiceSubCategories.Where(x => x.servcatID == p.servcatID && x.IsActive == true && x.IsDelete == false).Count()
                   }).ToList();
            return result;
        }

        public IEnumerable<GetDropDownWithImages> GetSubServiceCategoryByServCatIDDropDownWithImages(int? servcatID, int? uID)
        {
            List<GetDropDownWithImages> result = new List<GetDropDownWithImages>();
            result = UhDB.ServiceSubCategories.Where(x => x.servcatID == servcatID && x.MainCategory.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                   .Select(p => new GetDropDownWithImages
                   {
                       ID = p.servsubcatID,
                       Value = p.Name,
                       Images = UhDB.Files.Where(x => x.uID == uID && x.servsubcatID == p.servsubcatID && x.FileUse == 5 && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                   UhDB.Files.Where(x => x.uID == uID && x.servsubcatID == p.servsubcatID && x.FileUse == 5 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                    .Select(r => new GetFileDetails
                                    {
                                        Name = r.Filename,
                                        Size = r.FileSize,
                                        ContentType = r.FileContentType,
                                        Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/SubServiceCategory/" + r.FileFieldName
                                    }).ToList() : null,

                   }).ToList();
            return result;
        }
    }
}