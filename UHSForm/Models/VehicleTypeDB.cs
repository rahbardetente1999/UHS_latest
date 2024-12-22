using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;

namespace UHSForm.Models
{
    public class VehicleTypeDB
    {
        private UHSEntities UhDB;

        public VehicleTypeDB()
        {
            UhDB = new UHSEntities();
        }

        public string CreateCarType(CreateCarTypeModel carType)
        {
            string result = null;
            CarType objCarType = new CarType();
            objCarType.Name = carType.Name;
            objCarType.uID = carType.uID;
            if (carType.rID == 10)
            {
                objCarType.suID = carType.suID;
            }
            objCarType.IsActive = carType.IsActive;
            objCarType.IsDelete = carType.IsDelete;
            objCarType.CreatedBy = carType.CreatedBy;
            objCarType.CreatedOn = carType.CreatedOn;
            UhDB.CarTypes.Add(objCarType);
            UhDB.SaveChanges();
            result = "SUCCESS";
            return result;
        }

        public string UpdateCarType(UpdateCarTypeModel carType)
        {
            string result = null;
            var objCarType = UhDB.CarTypes.Where(x => x.cartID == carType.ID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            objCarType.Name = carType.Name;
            objCarType.UpdatedBy = carType.UpdatedBy;
            objCarType.UpdatedOn = carType.UpdatedOn;
            UhDB.SaveChanges();
            result = "SUCCESS";
            return result;
        }

        public string DeleteCarType(DeleteCarTypeModel carType)
        {
            string result = null;
            int CountCarTypeStatus = UhDB.CarTypes.Where(x => x.cartID == carType.ID && x.IsActive == true && x.IsDelete == false && x.Status == true).Count();
            if (CountCarTypeStatus != 0)
            {
                result = "Can't";
            }
            else
            {
                var objCarType = UhDB.CarTypes.Where(x => x.cartID == carType.ID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                objCarType.IsActive = carType.IsActive;
                objCarType.IsDelete = carType.IsDelete;
                objCarType.UpdatedBy = carType.UpdatedBy;
                objCarType.UpdatedOn = carType.UpdatedOn;
                UhDB.SaveChanges();
                result = "SUCCESS";
            }
            return result;
        }

        public List<GetCarTypeModel> GetCarTypes(int? uID)
        {
            List<GetCarTypeModel> result = new List<GetCarTypeModel>();
            result = UhDB.CarTypes.Where(x => x.uID == uID && x.IsDelete == false && x.IsActive == true).AsEnumerable()
                     .Select(p => new GetCarTypeModel { Name = p.Name, ID = p.cartID, AddedBy = p.CreatedBy, AddedOn = p.CreatedOn }).ToList();
            return result;
        }

        public List<GetDropDown> GetCarTypesDropdown(int? uID)
        {
            List<GetDropDown> result = new List<GetDropDown>();
            result = UhDB.CarTypes.Where(x => x.uID == uID && x.IsDelete == false && x.IsActive == true).AsEnumerable()
                     .Select(p => new GetDropDown { Value = p.Name, ID = p.cartID }).ToList();
            return result;
        }
    }
}