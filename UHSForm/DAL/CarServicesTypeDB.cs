using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;

namespace UHSForm.DAL
{
    public class CarServicesTypeDB
    {
        private UHSEntities UhDB;

        public CarServicesTypeDB()
        {
            UhDB = new UHSEntities();
        }

        public string CrerateCarServiceType(CrerateCarServiceTypeModel carServiceType)
        {
            string result = null;
            CarServiceType objCarServiceType = new CarServiceType();
            objCarServiceType.Name = carServiceType.Name;
            objCarServiceType.uID = carServiceType.uID;
            if (carServiceType.rID == 10)
            {
                objCarServiceType.suID = carServiceType.suID;
            }
            objCarServiceType.IsActive = carServiceType.IsActive;
            objCarServiceType.IsDelete = carServiceType.IsDelete;
            objCarServiceType.CreatedBy = carServiceType.CreatedBy;
            objCarServiceType.CreatedOn = carServiceType.CreatedOn;
            UhDB.CarServiceTypes.Add(objCarServiceType);
            UhDB.SaveChanges();
            result = "SUCCESS";
            return result;
        }

        public string UpdateCarServiceType(UpdateCarServiceTypeModel carServiceType)
        {
            string result = null;
            var objCarType = UhDB.CarServiceTypes.Where(x => x.carstID == carServiceType.ID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            objCarType.Name = carServiceType.Name;
            objCarType.UpdatedBy = carServiceType.UpdatedBy;
            objCarType.UpdatedOn = carServiceType.UpdatedOn;
            UhDB.SaveChanges();
            result = "SUCCESS";
            return result;
        }

        public string DeleteCarServiceType(DeleteCarServiceTypeModel carServiceType)
        {
            string result = null;
            int CountCarTypeStatus = UhDB.CarServiceTypes.Where(x => x.carstID == carServiceType.ID && x.IsActive == true && x.IsDelete == false && x.Status == true).Count();
            if (CountCarTypeStatus != 0)
            {
                result = "Can't";
            }
            else
            {
                var objCarType = UhDB.CarTypes.Where(x => x.cartID == carServiceType.ID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                objCarType.IsActive = carServiceType.IsActive;
                objCarType.IsDelete = carServiceType.IsDelete;
                objCarType.UpdatedBy = carServiceType.UpdatedBy;
                objCarType.UpdatedOn = carServiceType.UpdatedOn;
                UhDB.SaveChanges();
                result = "SUCCESS";
            }
            return result;
        }

        public List<GetCarServiceTypeModel> GetCarTypes(int? uID)
        {
            List<GetCarServiceTypeModel> result = new List<GetCarServiceTypeModel>();
            result = UhDB.CarServiceTypes.Where(x => x.uID == uID && x.IsDelete == false && x.IsActive == true).AsEnumerable()
                     .Select(p => new GetCarServiceTypeModel { Name = p.Name, ID = p.carstID, AddedBy = p.CreatedBy, AddedOn = p.CreatedOn }).ToList();
            return result;
        }

        public List<GetDropDown> GetCarServicesTypeDropdown(int? uID)
        {
            List<GetDropDown> result = new List<GetDropDown>();
            result = UhDB.CarServiceTypes.Where(x => x.uID == uID && x.IsDelete == false && x.IsActive == true).AsEnumerable()
                     .Select(p => new GetDropDown { Value = p.Name, ID = p.carstID }).ToList();
            return result;
        }
    }
}