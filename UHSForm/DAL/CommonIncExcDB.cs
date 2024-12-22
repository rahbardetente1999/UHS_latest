using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models;
using UHSForm.Models.Data;

namespace UHSForm.DAL
{
    public class CommonIncExcDB
    {
        private UHSEntities UhDB;

        public CommonIncExcDB()
        {
            UhDB = new UHSEntities();
        }

        public IEnumerable<GetIncExcluTypeModel> GetIncExclusByService(int? uID, int? catID, int? catsubID)
        {
            List<GetIncExcluTypeModel> result = new List<GetIncExcluTypeModel>();

            result = UhDB.RefIncExclus.Where(x => x.IncExclu.uID == uID && x.IncExclu.catID == catID && x.IncExclu.catsubID == catsubID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                             .Select((p, q) => new GetIncExcluTypeModel
                             {
                                 incexID = p.incexID,
                                 Type = p.Type,
                                 Name = p.Name,
                                 TypeName = p.Type == 1 ? "Inculsion" : "Exculsion",
                                 incexRefID = p.incexRefID,
                                 index = q + 1,
                                 CreatedBy = p.CreatedBy,
                                 CreatedOn = p.CreatedOn
                             }).ToList();
            return result;

        }

        public IEnumerable<GetIncExcluTypeModel> GetIncExclusBySubService(int? uID, int? catID, int? catsubID, int? servcatID, int? servsubcatID)
        {
            List<GetIncExcluTypeModel> result = new List<GetIncExcluTypeModel>();

            result = UhDB.RefIncExclus.Where(x => x.IncExclu.uID == uID && x.IncExclu.catsubID == catsubID && x.IncExclu.catID == catID
                     && x.IncExclu.sercatID == servcatID && x.IncExclu.servsubcatID == servsubcatID && x.IsActive == true
                     && x.IsDelete == false).AsEnumerable()
                             .Select((p, q) => new GetIncExcluTypeModel
                             {
                                 incexID = p.incexID,
                                 Type = p.Type,
                                 Name = p.Name,
                                 TypeName = p.Type == 1 ? "Inculsion" : "Exculsion",
                                 incexRefID = p.incexRefID,
                                 index = q + 1,
                                 CreatedBy = p.CreatedBy,
                                 CreatedOn = p.CreatedOn
                             }).ToList();

            return result;

        }
    }
}