using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models;
using UHSForm.Models.Data;

namespace UHSForm.DAL
{
    public class PropertyAreaDB
    {
        private UHSEntities UhDB;

        public PropertyAreaDB()
        {
            UhDB = new UHSEntities();
        }

        public string CreatePropertyArea(PropertyAreaModel propertyArea)
        {
            string result = null;
            PropertyArea objPropertyArea = new PropertyArea();
            objPropertyArea.Name = propertyArea.Name;
            objPropertyArea.OrderBy = propertyArea.OrderBy;
            objPropertyArea.uID = propertyArea.uID;
            if (propertyArea.rID != 10)
            {
                objPropertyArea.suID = propertyArea.suID;
            }
            objPropertyArea.IsActive = propertyArea.IsActive;
            objPropertyArea.IsDelete = propertyArea.IsDelete;
            objPropertyArea.CreatedBy = propertyArea.CreatedBy;
            objPropertyArea.CreatedOn = propertyArea.CreatedOn;
            UhDB.PropertyAreas.Add(objPropertyArea);
            Save();
            result = "SUCCESS";
            return result;
        }

        public string UpdatePropertyArea(UpdatePropertyAreaModel propertyArea)
        {
            string result = null;
            var objPropertyArea = UhDB.PropertyAreas.Where(x => x.propaID == propertyArea.propaID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            objPropertyArea.Name = propertyArea.Name;
            objPropertyArea.OrderBy = propertyArea.OrderBy;
            objPropertyArea.CreatedBy = propertyArea.UpdatedBy;
            objPropertyArea.CreatedOn = propertyArea.UpdatedOn;
            Save();
            result = "SUCCESS";
            return result;
        }

        public string DeletePropertyArea(DeletePropertyAreaModel propertyArea)
        {
            string result = null;
            var objPropertyArea = UhDB.PropertyAreas.Where(x => x.propaID == propertyArea.propaID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            objPropertyArea.IsActive = propertyArea.IsActive;
            objPropertyArea.IsDelete = propertyArea.IsDelete;
            objPropertyArea.CreatedBy = propertyArea.UpdatedBy;
            objPropertyArea.CreatedOn = propertyArea.UpdatedOn;
            Save();
            result = "SUCCESS";
            return result;
        }

        public IEnumerable<GetPropertyAreaModel> GetPropertyAreas(int? uID)
        {
            List<GetPropertyAreaModel> result = new List<GetPropertyAreaModel>();
            result = UhDB.PropertyAreas.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select((p, q) => new GetPropertyAreaModel
                     {
                         index = q + 1,
                         Name = p.Name,
                         OrderBy = p.OrderBy,
                         propaID = p.propaID,
                         CreatedBy = p.CreatedBy,
                         CreatedOn = p.CreatedOn
                     }).ToList();
            return result;

        }

        public IEnumerable<GetPropertyAreaModel> GetPropertyAreaByID(int? uID, int? propaID)
        {
            List<GetPropertyAreaModel> result = new List<GetPropertyAreaModel>();
            result = UhDB.PropertyAreas.Where(x => x.uID == uID && x.propaID == propaID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select((p, q) => new GetPropertyAreaModel
                     {
                         index = q + 1,
                         Name = p.Name,
                         OrderBy = p.OrderBy,
                         propaID = p.propaID,
                         CreatedBy = p.CreatedBy,
                         CreatedOn = p.CreatedOn
                     }).ToList();
            return result;

        }

        public IEnumerable<GetDropDowns> GetPropertyAreaDropDown(int? uID)
        {
            List<GetDropDowns> result = new List<GetDropDowns>();
            result = UhDB.PropertyAreas.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false)
                     .AsEnumerable()
                     .Select(p => new GetDropDowns { ID = p.propaID, Value = p.Name,ExtraValue=p.OrderBy}).OrderBy(x=>x.ExtraValue).ToList();
            return result;

        }

        private void Save()
        {
            UhDB.SaveChanges();
        }
    }
}