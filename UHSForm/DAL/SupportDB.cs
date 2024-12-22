using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models;
using UHSForm.Models.Data;

namespace UHSForm.DAL
{
    public class SupportDB
    {
        private UHSEntities UhDb;

        public SupportDB()
        {
            UhDb = new UHSEntities();
        }


        public int CreateSupport(SupportDetailsModel Support)
        {
            int result = 0;
            Support objSupport = new Support();
            objSupport.Serverity = Support.Serverity;
            objSupport.Subject = Support.subject;
            objSupport.uID = Support.uID;
            if (Support.rID != 10)
            {
                objSupport.suID = Support.suID;
            }
            objSupport.Description = Support.Description;
            objSupport.Status = Support.Status;
            objSupport.Email = Support.Emails;
            objSupport.TicketID = Support.TicketNo;
            objSupport.IsActive = Support.IsActive;
            objSupport.IsDelete = Support.IsDelete;
            objSupport.CreatedBy = Support.CreatedBy;
            objSupport.CreatedOn = Support.CreatedOn;
            UhDb.Supports.Add(objSupport);
            Save();
            result = objSupport.stID;
            return result;
        }

        public IEnumerable<GetSupports> GetSupports(int? uID, int? rID, int? suID, int? stfID)
        {
            List<GetSupports> result = new List<GetSupports>();
            if (rID == 10)
            {
                result = UhDb.Supports.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false && x.suID == null).AsEnumerable()
                      .Select((p, q) => new GetSupports
                      {
                          index = q + 1,
                          Usersname = p.uID != null ? p.User.Name : "N/A",
                          AgentName = p.suID != null ? p.Admin_Sub.Name : "N/A",
                          RoleName = p.suID != null ? p.Admin_Sub.Login.Role.Name : p.User.Login.Role.Name,
                          ServerityName = p.Serverity == 0 ? "System" : "General",
                          Description = p.Description,
                          subject = p.Subject,
                          Emails = p.Email,
                          CreatedBy = p.CreatedBy,
                          CreatedOn = p.CreatedOn,
                          Status = p.Status,
                          uID = p.uID,
                          suID = p.suID,
                          rID = p.suID != null ? p.Admin_Sub.Login.rID : p.User.Login.rID,
                          Serverity = p.Serverity,
                          ID = p.stID,
                          TicketNo = p.TicketID,
                          Files = UhDb.Files.Where(x => x.uID == uID && x.FileUse == 6 && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                  UhDb.Files.Where(x => x.uID == uID && x.FileUse == 6 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                  .Select(r => new GetFileDetails
                                  {
                                      Name = r.Filename,
                                      Size = r.FileSize,
                                      ContentType = r.FileContentType,
                                      Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/Support/" + r.FileFieldName
                                  }).ToList() : null
                      }).ToList();
            }
            else if (rID == 11)
            {
                result = UhDb.Supports.Where(x => x.suID == suID && x.IsActive == true && x.IsDelete == false && x.suID == suID).AsEnumerable()
                         .Select((p, q) => new GetSupports
                         {
                             index = q + 1,
                             Usersname = p.uID != null ? p.User.Name : "N/A",
                             AgentName = p.suID != null ? p.Admin_Sub.Name : "N/A",
                             RoleName = p.suID != null ? p.Admin_Sub.Login.Role.Name : p.User.Login.Role.Name,
                             ServerityName = p.Serverity == 0 ? "System" : "General",
                             Description = p.Description,
                             subject = p.Subject,
                             Emails = p.Email,
                             CreatedBy = p.CreatedBy,
                             CreatedOn = p.CreatedOn,
                             Status = p.Status,
                             uID = p.uID,
                             suID = p.suID,
                             rID = p.suID != null ? p.Admin_Sub.Login.rID : p.User.Login.rID,
                             Serverity = p.Serverity,
                             ID = p.stID,
                             TicketNo = p.TicketID,
                             Files = UhDb.Files.Where(x => x.suID == suID && x.FileUse == 6 && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                  UhDb.Files.Where(x => x.suID == suID && x.FileUse == 6 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                  .Select(r => new GetFileDetails
                                  {
                                      Name = r.Filename,
                                      Size = r.FileSize,
                                      ContentType = r.FileContentType,
                                      Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/Support/" + r.FileFieldName
                                  }).ToList() : null
                         }).ToList();
            }
            else if (rID == 12)
            {
                result = UhDb.Supports.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false && x.suID == suID).AsEnumerable()
                         .Select((p, q) => new GetSupports
                         {
                             index = q + 1,
                             Usersname = p.uID != null ? p.User.Name : "N/A",
                             AgentName = p.suID != null ? p.Admin_Sub.Name : "N/A",
                             RoleName = p.suID != null ? p.Admin_Sub.Login.Role.Name : p.User.Login.Role.Name,
                             ServerityName = p.Serverity == 0 ? "System" : "General",
                             Description = p.Description,
                             subject = p.Subject,
                             Emails = p.Email,
                             CreatedBy = p.CreatedBy,
                             CreatedOn = p.CreatedOn,
                             Status = p.Status,
                             uID = p.uID,
                             suID = p.suID,
                             rID = p.suID != null ? p.Admin_Sub.Login.rID : p.User.Login.rID,
                             Serverity = p.Serverity,
                             ID = p.stID,
                             TicketNo = p.TicketID,
                             Files = UhDb.Files.Where(x => x.stfID == stfID && x.FileUse == 6 && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                     UhDb.Files.Where(x => x.suID == stfID && x.FileUse == 6 && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                     .Select(r => new GetFileDetails
                                     {
                                         Name = r.Filename,
                                         Size = r.FileSize,
                                         ContentType = r.FileContentType,
                                         Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/Support/" + r.FileFieldName
                                     }).ToList() : null
                         }).ToList();
            }
            return result;
        }

        private void Save()
        {
            UhDb.SaveChanges();
        }
    }
}