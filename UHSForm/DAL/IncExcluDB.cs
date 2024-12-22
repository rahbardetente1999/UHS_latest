using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;

namespace UHSForm.DAL
{
    public class IncExcluDB
    {
        private UHSEntities UhDB;

        public IncExcluDB()
        {
            UhDB = new UHSEntities();
        }

        public string CreatIncExc(IncExcluModel incExclu)
        {
            string result = null;

            if (incExclu.incExcluTypes != null)
            {
                using (var trans = UhDB.Database.BeginTransaction())
                {
                    try
                    {
                        IncExclu objIncExclu = new IncExclu();
                        objIncExclu.catID = incExclu.catID;
                        objIncExclu.catsubID = incExclu.catsubID;
                        objIncExclu.sercatID = incExclu.servcatID;
                        objIncExclu.servsubcatID = incExclu.servsubcatID;
                        objIncExclu.uID = incExclu.uID;
                        if (incExclu.rID != 10)
                        {
                            objIncExclu.suID = incExclu.suID;
                        }
                        objIncExclu.IsActive = incExclu.IsActive;
                        objIncExclu.IsDelete = incExclu.IsDelete;
                        objIncExclu.CreatedBy = objIncExclu.CreatedBy;
                        objIncExclu.CreatedOn = objIncExclu.CreatedOn;
                        UhDB.IncExclus.Add(objIncExclu);
                        Save();
                        int? incexcID = objIncExclu.incexID;

                        foreach (var objincExcluTypes in incExclu.incExcluTypes)
                        {
                            RefIncExclu objRefIncExclu = new RefIncExclu();
                            objRefIncExclu.incexID = incexcID;
                            objRefIncExclu.Type = objincExcluTypes.Type;
                            objRefIncExclu.Name = objincExcluTypes.Name;
                            objRefIncExclu.IsActive = incExclu.IsActive;
                            objRefIncExclu.IsDelete = incExclu.IsDelete;
                            objRefIncExclu.CreatedBy = incExclu.CreatedBy;
                            objRefIncExclu.CreatedOn = incExclu.CreatedOn;
                            UhDB.RefIncExclus.Add(objRefIncExclu);
                            Save();
                        }
                        trans.Commit();
                        result = "SUCCESS";
                    }
                    catch (Exception ex)
                    {
                        trans.Commit();
                        result = "Exception";
                    }
                }
            }
            else
            {
                result = "Can't";
            }
            return result;
        }

        public IEnumerable<GetIncExcluModel> GetIncExclus(int? uID)
        {
            List<GetIncExcluModel> result = new List<GetIncExcluModel>();

            result = UhDB.IncExclus.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                             .Select(p => new GetIncExcluModel
                             {
                                 catID = p.catID,
                                 catsubID = p.catsubID,
                                 servcatID = p.sercatID,
                                 servsubcatID = p.servsubcatID,
                                 MainCategoryName = p.catID != null ? p.MainCategory.Name : "N/A",
                                 SubCategoryName = p.catsubID != null ? p.SubCategory.Name : "N/A",
                                 ServiceCategoryName = p.sercatID != null ? p.ServiceCategory.Name : "N/A",
                                 SubServiceCategoryName = p.servsubcatID != null ? p.ServiceSubCategory.Name : "N/A",
                                 incexID = p.incexID,
                                 CountExc = UhDB.RefIncExclus.Where(x => x.incexID == p.incexID && x.IsActive == true && x.IsDelete == false && x.Type == 2).Count(),
                                 CountInc = UhDB.RefIncExclus.Where(x => x.incexID == p.incexID && x.IsActive == true && x.IsDelete == false && x.Type == 1).Count(),
                                 CreatedBy = p.CreatedBy,
                                 CreatedOn = p.CreatedOn
                             }).ToList();

            return result;

        }

        public IEnumerable<GetIncExcluTypeModel> GetIncExcluType(int? incexID, int? Type)
        {
            List<GetIncExcluTypeModel> result = new List<GetIncExcluTypeModel>();

            result = UhDB.RefIncExclus.Where(x => x.incexID == incexID && x.Type == Type && x.IsActive == true && x.IsDelete == false).AsEnumerable()
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

        public string CreateRefIncExc(CreateIncExcluTypeModel incExcluType)
        {
            string result = null;
            RefIncExclu objRefIncExclu = new RefIncExclu();
            objRefIncExclu.incexID = incExcluType.incexID;
            objRefIncExclu.Name = incExcluType.Name;
            objRefIncExclu.Type = incExcluType.Type;
            objRefIncExclu.IsActive = incExcluType.IsActive;
            objRefIncExclu.IsDelete = incExcluType.IsDelete;
            objRefIncExclu.CreatedBy = incExcluType.CreatedBy;
            objRefIncExclu.CreatedOn = incExcluType.CreatedOn;
            UhDB.RefIncExclus.Add(objRefIncExclu);
            Save();
            result = "SUCCESS";
            return result;
        }

        public string UpdateRefIncExc(UpdateIncExcluTypeModel incExcluType)
        {
            string result = null;
            var objRefIncExclu = UhDB.RefIncExclus.Where(x => x.incexID == incExcluType.incexID && x.incexRefID == incExcluType.incexRefID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            objRefIncExclu.Name = incExcluType.Name;
            objRefIncExclu.Type = incExcluType.Type;
            objRefIncExclu.UpdatedBy = incExcluType.UpdatedBy;
            objRefIncExclu.UpdatedOn = incExcluType.UpdatedOn;
            Save();
            result = "SUCCESS";
            return result;
        }

        public string DeleteRefIncExc(DeleteIncExcluTypeModel incExcluType)
        {
            string result = null;
            var objRefIncExclu = UhDB.RefIncExclus.Where(x => x.incexID == incExcluType.incexID && x.incexRefID == incExcluType.incexRefID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            objRefIncExclu.IsActive = incExcluType.IsActive;
            objRefIncExclu.IsDelete = incExcluType.IsDelete;
            objRefIncExclu.UpdatedBy = incExcluType.UpdatedBy;
            objRefIncExclu.UpdatedOn = incExcluType.UpdatedOn;
            Save();
            result = "SUCCESS";
            return result;
        }

        private void Save()
        {
            UhDB.SaveChanges();
        }
    }
}