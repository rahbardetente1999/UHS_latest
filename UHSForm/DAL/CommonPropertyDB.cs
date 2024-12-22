using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models;
using UHSForm.Models.Data;
namespace UHSForm.DAL
{
    public class CommonPropertyDB
    {
        private UHSEntities UhDB;

        public CommonPropertyDB()
        {
            UhDB = new UHSEntities();
        }

        public IEnumerable<GetDropDowns> GetPropertyResidenceTypeByVIDDropDown(int? uID)
        {
            List<GetDropDowns> result = new List<GetDropDowns>();
            result = UhDB.PropertyResidenceTypes.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select(p => new GetDropDowns { ID = p.proprestID, Value = p.Name, ExtraValue = p.OrderBy }).OrderBy(y => y.ExtraValue).ToList();
            return result;

        }

        public IEnumerable<GetDropDowns> GetPropertyByAreaDropDown(int? uID, int? propaID)
        {
            List<GetDropDowns> result = new List<GetDropDowns>();
            result = UhDB.Ventures.Where(x => x.uID == uID && x.propaID == propaID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select(p => new GetDropDowns { ID = p.vID, Value = p.Name, ExtraValue = p.OrderBy }).OrderBy(y => y.ExtraValue).ToList();
            return result;

        }

        public IEnumerable<GetDropDowns> GetPropertyDropDownByAreasID(int? uID, int? propaID,int? subAreaID)
        {
            List<GetDropDowns> result = new List<GetDropDowns>();
            result = UhDB.Ventures.Where(x => x.uID == uID && x.propaID==propaID && x.subAreaID==subAreaID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select(p => new GetDropDowns { ID = p.vID, Value = p.Name, ExtraValue = p.OrderBy }).OrderBy(y => y.ExtraValue).ToList();
            return result;

        }
    }
}