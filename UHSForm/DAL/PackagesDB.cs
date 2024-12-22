using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models;
using UHSForm.Models.Data;

namespace UHSForm.DAL
{
    public class PackagesDB
    {
        private UHSEntities UhDB;

        public PackagesDB()
        {
            UhDB = new UHSEntities();
        }

        public string CreatePackages(PackagesModel package)
        {
            string result = null;
            Package objPackage = new Package();
            objPackage.Name = package.Name;
            objPackage.RecursiveTime = package.RecursiveTime;
            objPackage.uID = package.uID;
            if (package.rID != 10)
            {
                objPackage.suID = package.suID;
            }
            objPackage.IsActive = package.IsActive;
            objPackage.IsDelete = package.IsDelete;
            objPackage.CreatedBy = package.CreatedBy;
            objPackage.CreatedOn = package.CreatedOn;
            UhDB.Packages.Add(objPackage);
            Save();
            result = "SUCCESS";
            return result;

        }

        public string UpdatePackages(UpdatePackagesModel package)
        {
            string result = null;
            var objPackages = UhDB.Packages.Where(x => x.packID == package.packID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            objPackages.Name = package.Name;
            objPackages.RecursiveTime = package.RecursiveTime;
            objPackages.UpdatedBy = package.UpdatedBy;
            objPackages.UpdatedOn = package.UpdatedOn;
            Save();
            result = "SUCCESS";
            return result;

        }

        public string DeletePackages(DeletePackagesModel package)
        {
            string result = null;

            var objPackages = UhDB.Packages.Where(x => x.packID == package.packID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            if (objPackages.Status == true)
            {
                result = "Can't";
            }
            else
            {
                var objDeleteMainCategory = UhDB.Packages.Where(x => x.packID == package.packID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                objDeleteMainCategory.IsActive = package.IsActive;
                objDeleteMainCategory.IsDelete = package.IsDelete;
                objDeleteMainCategory.UpdatedBy = package.UpdatedBy;
                objDeleteMainCategory.UpdatedOn = package.UpdatedOn;
                Save();
                result = "SUCCESS";
            }

            return result;

        }

        public IEnumerable<GetPackagesModel> GetPackages(int? uID)
        {
            List<GetPackagesModel> result = new List<GetPackagesModel>();

            result = UhDB.Packages.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                      .Select(p => new Models.GetPackagesModel
                      {
                          Name = p.Name,
                          RecursiveTime = p.RecursiveTime,
                          CreatedBy = p.CreatedBy,
                          CreatedOn = p.CreatedOn,
                          packID = p.packID,
                      }).ToList();

            return result;
        }

        public GetPackagesModel GetPackagesByID(int? uID, int? packID)
        {
            GetPackagesModel result = new GetPackagesModel();

            result = UhDB.Packages.Where(x => x.uID == uID && x.packID == packID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                      .Select(p => new Models.GetPackagesModel
                      {
                          Name = p.Name,
                          RecursiveTime = p.RecursiveTime,
                          CreatedBy = p.CreatedBy,
                          CreatedOn = p.CreatedOn,
                          packID = p.packID,
                      }).FirstOrDefault();

            return result;
        }

        public IEnumerable<GetDropDown> GetPackagesDropDown(int? uID)
        {
            List<GetDropDown> result = new List<GetDropDown>();

            result = UhDB.Packages.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                      .Select(p => new Models.GetDropDown
                      {
                          Value = p.Name,
                          ID = p.packID
                      }).ToList();

            return result;
        }

        public void Save()
        {
            UhDB.SaveChanges();
        }
    }
}