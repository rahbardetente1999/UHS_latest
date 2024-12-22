using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models;
using UHSForm.Models.Data;

namespace UHSForm.DAL
{
    public class PropertyDB
    {
        private UHSEntities UhDB;

        public PropertyDB()
        {
            UhDB = new UHSEntities();
        }

        public string CreateProperty(PropertyModel property)
        {
            string result = null;
            Venture objVenture = new Venture();
            objVenture.Name = property.Name;
            objVenture.OrderBy = property.OrderBy;
            objVenture.uID = property.uID;
            if (property.rID != 10)
            {
                objVenture.suID = property.suID;
            }
            objVenture.subAreaID = property.subAreaID;
            objVenture.Code = property.Code;
            objVenture.propaID = property.propaID;
            objVenture.IsActive = property.IsActive;
            objVenture.IsDelete = property.IsDelete;
            objVenture.CreatedBy = property.CreatedBy;
            objVenture.CreatedOn = property.CreatedOn;
            UhDB.Ventures.Add(objVenture);
            Save();
            result = "SUCCESS";
            return result;
        }

        public string UpdateProperty(UpdatePropertyModel property)
        {
            string result = null;
            var objVentures = UhDB.Ventures.Where(x => x.vID == property.vID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            objVentures.Name = property.Name;
            objVentures.Code = property.Code;
            objVentures.OrderBy = property.OrderBy;
            objVentures.subAreaID = property.subAreaID;
            objVentures.propaID = property.propaID;
            objVentures.UpdatedBy = property.UpdatedBy;
            objVentures.UpdatedOn = property.UpdatedOn;
            Save();
            result = "SUCCESS";
            return result;
        }

        public string DeleteProperty(DeletePropertyModel property)
        {
            string result = null;
            var objVentures = UhDB.Ventures.Where(x => x.vID == property.vID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            objVentures.IsActive = property.IsActive;
            objVentures.IsDelete = property.IsDelete;
            objVentures.UpdatedBy = property.UpdatedBy;
            objVentures.UpdatedOn = property.UpdatedOn;
            Save();
            result = "SUCCESS";
            return result;
        }

        public IEnumerable<GetPropertyModel> GetProperty(int? uID)
        {
            List<GetPropertyModel> result = new List<GetPropertyModel>();
            result = UhDB.Ventures.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select((p, q) => new GetPropertyModel
                     {
                         index = q + 1,
                         Name = p.Name,
                         OrderBy = p.OrderBy,
                         PropertyArea = p.PropertyArea.Name,
                         propaID = p.propaID,
                         subAreaID=p.subAreaID,
                         SubAreaName=p.subAreaID!=null?p.SubArea.Name:"N/A",
                         Code=p.Code,
                         vID = p.vID,
                         CreatedBy = p.CreatedBy,
                         CreatedOn = p.CreatedOn
                     }).ToList();
            return result;

        }

        public IEnumerable<GetPropertyModel> GetPropertyByID(int? uID, int? vID)
        {
            List<GetPropertyModel> result = new List<GetPropertyModel>();
            result = UhDB.Ventures.Where(x => x.uID == uID && x.vID == vID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select((p, q) => new GetPropertyModel
                     {
                         index = q + 1,
                         Name = p.Name,
                         PropertyArea = p.PropertyArea.Name,
                         subAreaID = p.subAreaID,
                         SubAreaName = p.subAreaID != null ? p.SubArea.Name : "N/A",
                         Code = p.Code,
                         OrderBy = p.OrderBy,
                         propaID = p.propaID,
                         CreatedBy = p.CreatedBy,
                         CreatedOn = p.CreatedOn
                     }).ToList();
            return result;

        }

        public IEnumerable<GetDropDowns> GetPropertyDropDown(int? uID)
        {
            List<GetDropDowns> result = new List<GetDropDowns>();
            result = UhDB.Ventures.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select(p => new GetDropDowns { ID = p.propaID, Value = p.Name, ExtraValue = p.OrderBy }).OrderBy(y => y.ExtraValue).ToList();
            return result;

        }

        public IEnumerable<GetDropDowns> GetPropertyDropDownForReports(int? uID)
        {
            List<GetDropDowns> result = new List<GetDropDowns>();
            result = UhDB.Ventures.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select(p => new GetDropDowns { ID = p.vID, Value = p.Name}).ToList();
            return result;

        }




        private void Save()
        {
            UhDB.SaveChanges();
        }
    }
}