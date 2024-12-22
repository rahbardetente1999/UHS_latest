using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models;
using UHSForm.Models.Data;

namespace UHSForm.DAL
{
    public class PropertyResidenceTypeDB
    {
        private UHSEntities UhDB;

        public PropertyResidenceTypeDB()
        {
            UhDB = new UHSEntities();
        }

        public string CreatePropertyResidenceType(PropertyResidenceTypeModel property)
        {
            string result = null;
            PropertyResidenceType objPropertyResidenceType = new PropertyResidenceType();
            objPropertyResidenceType.Name = property.Name;
            objPropertyResidenceType.OrderBy = property.OrderBy;
            objPropertyResidenceType.uID = property.uID;
            if (property.rID != 10)
            {
                objPropertyResidenceType.suID = property.suID;
            }
            objPropertyResidenceType.IsActive = property.IsActive;
            objPropertyResidenceType.IsDelete = property.IsDelete;
            objPropertyResidenceType.CreatedBy = property.CreatedBy;
            objPropertyResidenceType.CreatedOn = property.CreatedOn;
            UhDB.PropertyResidenceTypes.Add(objPropertyResidenceType);
            Save();
            result = "SUCCESS";
            return result;
        }

        public string UpdatePropertyResidenceType(UpdatePropertyResidenceTypeModel property)
        {
            string result = null;
            var objPropertyResidenceTypes = UhDB.PropertyResidenceTypes.Where(x => x.proprestID == property.proprestID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            objPropertyResidenceTypes.Name = property.Name;
            objPropertyResidenceTypes.OrderBy = property.OrderBy;
            objPropertyResidenceTypes.UpdatedBy = property.UpdatedBy;
            objPropertyResidenceTypes.UpdatedOn = property.UpdatedOn;
            Save();
            result = "SUCCESS";
            return result;
        }

        public string DeletePropertyResidenceType(DeletePropertyResidenceTypeModel property)
        {
            string result = null;
            var objPropertyResidenceTypes = UhDB.PropertyResidenceTypes.Where(x => x.proprestID == property.proprestID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            objPropertyResidenceTypes.IsActive = property.IsActive;
            objPropertyResidenceTypes.IsDelete = property.IsDelete;
            objPropertyResidenceTypes.UpdatedBy = property.UpdatedBy;
            objPropertyResidenceTypes.UpdatedOn = property.UpdatedOn;
            Save();
            result = "SUCCESS";
            return result;
        }

        public IEnumerable<GetPropertyResidenceTypeModel> GetPropertyResidenceType(int? uID)
        {
            List<GetPropertyResidenceTypeModel> result = new List<GetPropertyResidenceTypeModel>();
            result = UhDB.PropertyResidenceTypes.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select((p, q) => new GetPropertyResidenceTypeModel
                     {
                         index = q + 1,
                         Name = p.Name,
                         OrderBy = p.OrderBy,
                         proprestID = p.proprestID,
                         CreatedBy = p.CreatedBy,
                         CreatedOn = p.CreatedOn
                     }).ToList();
            return result;

        }

        public IEnumerable<GetPropertyResidenceTypeModel> GetPropertyResidenceTypeByID(int? uID, int? proprestID)
        {
            List<GetPropertyResidenceTypeModel> result = new List<GetPropertyResidenceTypeModel>();
            result = UhDB.PropertyResidenceTypes.Where(x => x.uID == uID && x.proprestID == proprestID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select((p, q) => new GetPropertyResidenceTypeModel
                     {
                         index = q + 1,
                         Name = p.Name,
                         OrderBy = p.OrderBy,
                         proprestID = p.proprestID,
                         CreatedBy = p.CreatedBy,
                         CreatedOn = p.CreatedOn
                     }).ToList();
            return result;

        }

        public IEnumerable<GetDropDowns> GetPropertyResidenceTypeDropDown(int? uID)
        {
            List<GetDropDowns> result = new List<GetDropDowns>();
            result = UhDB.PropertyResidenceTypes.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select(p => new GetDropDowns { ID = p.proprestID, Value = p.Name, ExtraValue = p.OrderBy }).OrderBy(y => y.ExtraValue).ToList();
            return result;

        }




        private void Save()
        {
            UhDB.SaveChanges();
        }
    }
}