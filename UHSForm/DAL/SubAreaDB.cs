using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models;
using UHSForm.Models.Data;


namespace UHSForm.DAL
{
    public class SubAreaDB
    {
        private UHSEntities UhDB;

        public SubAreaDB()
        {
            UhDB = new UHSEntities();
        }

        public string CreateSubArea(SubAreaModel area)
        {
            string result = null;
            SubArea objSubArea = new SubArea();
            objSubArea.Name = area.SubAreaName;
            objSubArea.propaID = area.propaID;
            objSubArea.ScoreID = area.ScoreID;
            objSubArea.uID = area.uID;
            if (area.rID != 10)
            {
                objSubArea.suID = area.suID;
            }
            objSubArea.IsActive = area.IsActive;
            objSubArea.IsDelete = area.IsDelete;
            objSubArea.CreatedBy = area.CreatedBy;
            objSubArea.CreatedOn = area.CreatedOn;
            UhDB.SubAreas.Add(objSubArea);
            UhDB.SaveChanges();
            result = "SUCCESS";
            return result;
        }

        public string UpdateSubArea(UpdateSubAreaModel area)
        {
            string result = null;
            var objSubAreas = UhDB.SubAreas.Where(x => x.subAreaID == area.subAreaID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            objSubAreas.Name = area.SubAreaName;
            objSubAreas.ScoreID = area.ScoreID;
            objSubAreas.propaID = area.propaID;
            objSubAreas.UpdatedBy = area.UpdatedBy;
            objSubAreas.UpdatedOn = area.UpdatedOn;
            UhDB.SaveChanges();
            result = "SUCCESS";
            return result;
        }

        public string DeleteSubArea(DeleteSubAreaModel area)
        {
            string result = null;
            int CountSubAreas = UhDB.SubAreas.Where(x => x.subAreaID == area.subAreaID && x.IsActive == true && x.IsDelete == false && x.Status == true).Count();
            if (CountSubAreas != 0)
            {
                result = "Can't";
            }
            else
            {
                var objSubAreas = UhDB.SubAreas.Where(x => x.subAreaID == area.subAreaID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                objSubAreas.IsActive = area.IsActive;
                objSubAreas.IsDelete = area.IsDelete;
                objSubAreas.UpdatedBy = area.UpdatedBy;
                objSubAreas.UpdatedOn = area.UpdatedOn;
                UhDB.SaveChanges();
                result = "SUCCESS";
            }
            return result;
        }

        public List<GetSubAreaModel> GetSubAreas (int? uID)
        {
            List<GetSubAreaModel> result = new List<GetSubAreaModel>();
            result = UhDB.SubAreas.Where(x => x.uID == uID && x.IsDelete == false && x.IsActive == true).AsEnumerable()
                     .Select(p => new GetSubAreaModel { SubAreaName = p.Name, subAreaID = p.subAreaID,propaID=p.propaID,AddedBy = p.CreatedBy, AddedOn = p.CreatedOn,AreaName=p.PropertyArea.Name,ScoreID=p.ScoreID }).ToList();
            return result;
        }

        public List<GetDropDown> GetSubAreaDropdown(int? uID)
        {
            List<GetDropDown> result = new List<GetDropDown>();
            result = UhDB.SubAreas.Where(x => x.uID == uID && x.IsDelete == false && x.IsActive == true).AsEnumerable()
                     .Select(p => new GetDropDown { Value = p.Name, ID = p.subAreaID }).ToList();
            return result;
        }

        public List<GetDropDown> GetSubAreaDropdownByPropertyArea(int? uID, int? propaID)
        {
            List<GetDropDown> result = new List<GetDropDown>();
            result = UhDB.SubAreas.Where(x => x.uID == uID && x.propaID == propaID && x.IsDelete == false && x.IsActive == true).AsEnumerable()
                     .Select(p => new GetDropDown { Value = p.Name, ID = p.subAreaID }).ToList();
            return result;
        }
    }
}