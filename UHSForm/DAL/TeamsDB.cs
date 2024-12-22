using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;

namespace UHSForm.DAL
{
    public class TeamsDB
    {
        private UHSEntities UhDB;

        public TeamsDB()
        {
            UhDB = new UHSEntities();
        }

        public string CreateTeams(TeamsModel teams)
        {
            string result = null;
            Team objTeam = new Team();
            objTeam.Name = teams.Name;
            objTeam.teamTyID = teams.teamTyID;
            objTeam.uID = teams.uID;
            objTeam.Remarks = teams.Remarks;
            if (teams.CreatedRole != 10)
            {
                objTeam.suID = teams.suID;
            }
            objTeam.IsActive = teams.IsActive;
            objTeam.IsDelete = teams.IsDelete;
            objTeam.CreatedBy = teams.CreatedBy;
            objTeam.CreatedOn = teams.CreatedOn;
            UhDB.Teams.Add(objTeam);
            Save();
            result = "SUCCESS";
            return result;

        }

        public string UpdateTeams(UpdateTeamsModel teams)
        {
            string result = null;
            var objTeams = UhDB.Teams.Where(x => x.teamID == teams.teamID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            objTeams.Name = teams.Name;
            objTeams.teamTyID = teams.teamTyID;
            objTeams.Remarks = teams.Remarks;
            objTeams.UpdatedRole = teams.UpdatedRole;
            objTeams.UpdatedBy = teams.UpdatedBy;
            objTeams.UpdatedOn = teams.UpdatedOn;
            Save();
            result = "SUCCESS";
            return result;

        }

        public string DeleteTeams(DeleteTeamsModel teams)
        {
            string result = null;

            var objTeams = UhDB.Teams.Where(x => x.teamID == teams.teamID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            if (objTeams.Status == true)
            {
                result = "Can't";
            }
            else
            {
                var objDeleteTeams = UhDB.Teams.Where(x => x.teamID == teams.teamID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                objDeleteTeams.IsActive = teams.IsActive;
                objDeleteTeams.IsDelete = teams.IsDelete;
                objDeleteTeams.UpdatedRole = teams.UpdatedRole;
                objDeleteTeams.UpdatedBy = teams.UpdatedBy;
                objDeleteTeams.UpdatedOn = teams.UpdatedOn;
                Save();
                result = "SUCCESS";
            }

            return result;

        }

        public IEnumerable<GetTeamsModel> GetTeams(int? uID)
        {
            List<GetTeamsModel> result = new List<GetTeamsModel>();

            result = UhDB.Teams.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                      .Select(p => new Models.GetTeamsModel
                      {
                          teamID = p.teamID,
                          teamTyID=p.teamTyID,
                          TeamName=p.teamTyID!=null?p.TeamType.Name:"N/A",
                          Name = p.Name,
                          CreatedBy = p.CreatedBy,
                          CreatedOn = p.CreatedOn,
                          Remarks = p.Remarks
                      }).ToList();

            return result;
        }

        public GetTeamsModel GetTeamsByTeamID(int? uID, int? teamID)
        {
            GetTeamsModel result = new GetTeamsModel();

            result = UhDB.Teams.Where(x => x.uID == uID && x.teamID == teamID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                      .Select(p => new Models.GetTeamsModel
                      {
                          Name = p.Name,
                          teamTyID = p.teamTyID,
                          TeamName = p.teamTyID != null ? p.TeamType.Name : "N/A",
                          CreatedBy = p.CreatedBy,
                          CreatedOn = p.CreatedOn,
                          Remarks = p.Remarks
                      }).FirstOrDefault();

            return result;
        }

        public IEnumerable<GetDropDown> GetTeamsDropDown(int? uID)
        {
            List<GetDropDown> result = new List<GetDropDown>();

            result = UhDB.Teams.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                      .Select(p => new Models.GetDropDown
                      {
                          Value = p.Name,
                          ID = p.teamID
                      }).ToList();

            return result;
        }

        private void Save()
        {
            UhDB.SaveChanges();
        }

    }
}