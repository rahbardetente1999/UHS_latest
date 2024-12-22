using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;
namespace UHSForm.DAL
{
    public class CustomerServiceDB
    {
        private UHSEntities UhDB;

        public CustomerServiceDB()
        {
            UhDB = new UHSEntities();
        }

        public IEnumerable<GetDropDown> GetPropertyAreaByCustomer(int? cuID)
        {
            List<GetDropDown> result = new List<GetDropDown>();
            var objCustomerOfficialDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID && x.IsActive == true && x.IsDelete == false).GroupBy(g => new { propaID = g.propaID, Value = g.PropertyArea.Name }).AsEnumerable()
                     .Select(p => new GetDropDown { ID = p.Key.propaID, Value = p.Key.Value }).ToList().Distinct();
            if (objCustomerOfficialDetails != null)
            {
                result.AddRange(objCustomerOfficialDetails);
            }
            return result;
        }

        public IEnumerable<GetPropertyDropDown> GetPropertyByCustomerPropertyArea(int? cuID, int? propaID)
        {
            List<GetPropertyDropDown> result = new List<GetPropertyDropDown>();
            var objCustomerOfficialDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID && x.propaID == propaID && x.IsActive == true && x.IsDelete == false).GroupBy(g => new { vID = g.vID, Value = g.vID != null ? g.Venture.Name : null, propType = g.propType }).AsEnumerable()
                     .Select(p => new GetPropertyDropDown { ID = p.Key.vID, Value = p.Key.Value, PropertyType = p.Key.propType.ToString() }).ToList().Distinct();
            if (objCustomerOfficialDetails != null)
            {
                result.AddRange(objCustomerOfficialDetails);
            }
            return result;
        }

        public IEnumerable<GetDropDown> GetPropertyByCustomerPropertyResidenceType(int? cuID, int? propaID, int? vID)
        {
            List<GetDropDown> result = new List<GetDropDown>();
            var objCustomerOfficialDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID && x.propaID == propaID && x.vID == vID && x.IsActive == true && x.IsDelete == false).GroupBy(g => new { ID = g.proprestID, Value = g.proprestID != null ? g.PropertyResidenceType.Name : null }).AsEnumerable()
                     .Select(p => new GetDropDown { ID = p.Key.ID, Value = p.Key.Value }).ToList().Distinct();
            if (objCustomerOfficialDetails != null)
            {
                result.AddRange(objCustomerOfficialDetails);
            }
            return result;
        }

        public List<GetCustomerOtherPropertyDetails> GetPropertyByCustomerOtherProperty(int? cuID, int? propaID, int? vID, int? propType)
        {
            List<GetCustomerOtherPropertyDetails> result = new List<GetCustomerOtherPropertyDetails>();
            if (propType == 1)
            {
                var objCustomerOfficialDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID && x.propaID == propaID && x.vID == vID && x.propType == propType && x.IsActive == true && x.IsDelete == false).GroupBy(g => new { ApartmentName = g.AppartmentNumber }).AsEnumerable()
                     .Select(p => new GetCustomerOtherPropertyDetails { AppartmentName = p.Key.ApartmentName }).ToList().Distinct();
                if (objCustomerOfficialDetails != null)
                {
                    result.AddRange(objCustomerOfficialDetails);
                }
            }
            else
            {
                var objCustomerOfficialDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID && x.propaID == propaID && x.vID == vID && x.propType == propType && x.IsActive == true && x.IsDelete == false).GroupBy(g => new { custODID = g.custODID }).AsEnumerable()
                     .Select(p => new { custODID = p.Key.custODID }).ToList().Distinct();

                foreach (var item in objCustomerOfficialDetails)
                {
                    int? custODID = item.custODID;
                    var objCustomerOtherProperty = UhDB.CustomerOtherProperties.Where(x => x.custID == cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable().Select(p => new GetCustomerOtherPropertyDetails
                    {
                        TowerName = p.TowerName,
                        BuildingName = p.BuildingName,
                        StreetNumber = p.StreetNumber,
                        ZoneNumber = p.ZoneNumber,
                        Loacation = p.Loacation,
                        LocationLink = p.LocationLink,
                        AppartmentName = p.CustomerOfficalDetail.AppartmentNumber
                    }).ToList();
                    result.AddRange(objCustomerOtherProperty);
                }
            }

            return result;
        }
    }
}