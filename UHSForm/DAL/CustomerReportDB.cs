using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UHSForm.Models;
using UHSForm.Models.Data;

namespace UHSForm.DAL
{
    public class CustomerReportDB
    {
        private UHSEntities UhDB;

        public CustomerReportDB()
        {
            UhDB = new UHSEntities();
        }

        public async Task<GetCustomerCount> GetCustomerCount(CustomerReportModel customer)
        {
            var GetCustomer = UhDB.CustomerOfficalDetails.Where(x => x.IsActive == true && x.IsDelete == false).AsQueryable();
            int newCustomerCount = 0, existingCustomerCount = 0, stoppedCustomerCount = 0;
            string Towername=null;
            if (customer.propaID == "All")
            {
                if (customer.vID == "All")
                {
                    var objPropertyAreas = await UhDB.PropertyAreas.Where(x => x.uID == customer.uID && x.IsActive == true && x.IsDelete == false).ToListAsync();
                    foreach (var objPropertyArea in objPropertyAreas)
                    {
                        int? propaID = objPropertyArea.propaID;
                        string PropertyArea = objPropertyArea.Name;
                        var objVentures = await  UhDB.Ventures.Where(x => x.uID == customer.uID && x.propaID == propaID && x.IsActive == true && x.IsDelete == false).ToListAsync();
                        foreach (var objVenture in objVentures)
                        {
                            int? vID = objVenture.vID;
                            Towername = objVenture.Name;
                            var GetCustomer1 = GetCustomer.Where(x => x.vID == vID && x.propaID == propaID);
                            int[] CustomerCount = GetCustomerCountByCustomerType(customer, ref GetCustomer1);
                            newCustomerCount += CustomerCount[0];
                            existingCustomerCount += CustomerCount[1];
                            stoppedCustomerCount += CustomerCount[2];
                        }
                    }
                }
                else
                {
                    int? vID = Convert.ToInt32(customer.vID);
                    GetCustomer = GetCustomer.Where(x => x.vID == vID);
                    int[] CustomerCount = GetCustomerCountByCustomerType(customer, ref GetCustomer);
                    newCustomerCount += CustomerCount[0];
                    existingCustomerCount += CustomerCount[1];
                    stoppedCustomerCount += CustomerCount[2];
                }
            }
            else
            {
                if (customer.vID == "All")
                {
                    int? propaID = Convert.ToInt32(customer.propaID);
                    var objVentures = await UhDB.Ventures.Where(x => x.uID == customer.uID && x.propaID == propaID && x.IsActive == true && x.IsDelete == false).ToListAsync();
                    foreach (var objVenture in objVentures)
                    {
                        int? vID = objVenture.vID;
                        GetCustomer = GetCustomer.Where(x => x.vID == vID && x.propaID == propaID);
                        int[] CustomerCount = GetCustomerCountByCustomerType(customer, ref GetCustomer);
                        newCustomerCount += CustomerCount[0];
                        existingCustomerCount += CustomerCount[1];
                        stoppedCustomerCount += CustomerCount[2];
                    }
                }
                else
                {
                    int? propaID = Convert.ToInt32(customer.propaID);
                    int? vID = Convert.ToInt32(customer.vID);
                    GetCustomer = GetCustomer.Where(x => x.vID == vID && x.propaID == propaID);
                    int[] CustomerCount = GetCustomerCountByCustomerType(customer, ref GetCustomer);
                    newCustomerCount += CustomerCount[0];
                    existingCustomerCount += CustomerCount[1];
                    stoppedCustomerCount += CustomerCount[2];
                }
            }
            return new GetCustomerCount
            {
                ventureName = Towername,
                NewCustomer = newCustomerCount,
                ExistingCustomer = existingCustomerCount,
                SuspendCustomer = stoppedCustomerCount
            };

        }

        public async Task<IEnumerable<GetCustomerDataForGraph>> GetCustomerDataForGraph(CustomerReportModel customer)
        {
            List<GetCustomerDataForGraph> result = new List<GetCustomerDataForGraph>();
            var objGetCustomer = UhDB.CustomerOfficalDetails.Where(x => x.IsActive == true && x.IsDelete == false).AsQueryable();
           
            bool IsDate = false, IsMonth = false, IsYear = false;
            List<DateTime> objDates = new List<DateTime>();
            List<string> objMonths = new List<string>();

            List<DateTime> objMonthYear = new List<DateTime>();
            string vName = null;
            if (customer.EndDate == null)
            {
                objDates.Add(customer.StartDate.Value);
                IsDate = true;
            }
            else
            {
                if (customer.StartDate.HasValue && customer.EndDate.HasValue)
                {
                    DateTime startDate = customer.StartDate.Value;
                    DateTime endDate = customer.EndDate.Value;
                    var dateDifference = Math.Abs((startDate - endDate).Days);
                    if (dateDifference <= 29)
                    {
                        IsDate = true;
                        for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                        {
                            objDates.Add(date);
                        }
                    }
                    else if (dateDifference >= 30 && dateDifference <= 365)
                    {
                        IsMonth = true;
                        for (DateTime month = startDate; month <= endDate; month = month.AddMonths(1))
                        {
                            objMonths.Add(month.ToString("MMMM yyyy"));
                        }
                    }
                    else if (dateDifference >= 365)
                    {
                        IsMonth = true;
                        IsYear = true;
                        for (DateTime month = startDate; month <= endDate; month = month.AddMonths(1))
                        {
                            objMonths.Add(month.ToString("MMMM yyyy"));
                        }
                    }

                }
            }
            if (IsDate == true)
            {
                foreach (var objDate in objDates)
                {
                    DateTime Date = Convert.ToDateTime(objDate);
                    var objNewCustomerCount = objGetCustomer;
                    var objExistingCustomerCount = objGetCustomer;
                    var objStoppedCustomerCount = objGetCustomer;

                    objNewCustomerCount = objNewCustomerCount.Where(x => EntityFunctions.TruncateTime(x.Customer.CreatedOn) == EntityFunctions.TruncateTime(Date));
                    objExistingCustomerCount = objExistingCustomerCount.Where(x => EntityFunctions.TruncateTime(x.Customer.CustomerTypeOn) == EntityFunctions.TruncateTime(Date));
                    objStoppedCustomerCount = objStoppedCustomerCount.Where(x => EntityFunctions.TruncateTime(x.UpdatedOn) == EntityFunctions.TruncateTime(Date));
                    int NewCustomerCount = 0, ExistingCustomerCount = 0, StoppedCustomerCount = 0;
                    if (customer.propaID == "All")
                    {
                        if (customer.vID == "All")
                        {
                            var objPropertyAreas = await UhDB.PropertyAreas.Where(x => x.uID == customer.uID && x.IsActive == true && x.IsDelete == false).ToListAsync();
                            foreach (var objPropertyArea in objPropertyAreas)
                            {
                                int? propaID = objPropertyArea.propaID;
                                string PropertyArea = objPropertyArea.Name;
                                var objVentures = await UhDB.Ventures.Where(x => x.uID == customer.uID && x.propaID == propaID && x.IsActive == true && x.IsDelete == false).ToListAsync();
                                foreach (var objVenture in objVentures)
                                {

                                    int? vID = objVenture.vID;
                                    vName = objVenture.Name;
                                    if (customer.CustomerType != null)
                                    {
                                        if (customer.CustomerType == 1)
                                        {
                                            NewCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, customer.CustomerType);
                                        }
                                        else if (customer.CustomerType == 2)
                                        {
                                            ExistingCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, customer.CustomerType);
                                        }
                                        else
                                        {
                                            StoppedCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, customer.CustomerType);
                                        }
                                    }
                                    else
                                    {
                                        NewCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, 1);
                                        ExistingCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, 2);
                                        StoppedCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, 3);
                                    }
                                }
                            }
                        }
                        else
                        {
                            int? vID = Convert.ToInt32(customer.vID);
                            if (customer.CustomerType != null)
                            {
                                if (customer.CustomerType == 1)
                                {
                                    NewCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, null, customer.CustomerType);
                                }
                                else if (customer.CustomerType == 2)
                                {
                                    ExistingCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, null, customer.CustomerType);
                                }
                                else
                                {
                                    StoppedCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, null, null);
                                }
                            }
                            else
                            {
                                NewCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, null, 1);
                                ExistingCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, null, 2);
                                StoppedCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, null, null);
                            }
                        }
                    }
                    else
                    {
                        if (customer.vID == "All")
                        {
                            int? propaID = Convert.ToInt32(customer.propaID);
                            var objVentures = await UhDB.Ventures.Where(x => x.uID == customer.uID && x.propaID == propaID && x.IsActive == true && x.IsDelete == false).ToListAsync();
                            foreach (var objVenture in objVentures)
                            {
                                int? vID = objVenture.vID;
                                if (customer.CustomerType != null)
                                {
                                    if (customer.CustomerType == 1)
                                    {
                                        NewCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, customer.CustomerType);
                                    }
                                    else if (customer.CustomerType == 2)
                                    {
                                        ExistingCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, customer.CustomerType);
                                    }
                                    else
                                    {
                                        StoppedCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, null);
                                    }
                                }
                                else
                                {
                                    NewCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, 1);
                                    ExistingCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, 2);
                                    StoppedCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, null);
                                }

                            }
                        }
                        else
                        {
                            int? propaID = Convert.ToInt32(customer.propaID);
                            int? vID = Convert.ToInt32(customer.vID);
                            if (customer.CustomerType != null)
                            {
                                if (customer.CustomerType == 1)
                                {
                                    NewCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, customer.CustomerType);
                                }
                                else if (customer.CustomerType == 2)
                                {
                                    ExistingCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, customer.CustomerType);
                                }
                                else
                                {
                                    StoppedCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, null);
                                }
                            }
                            else
                            {
                                NewCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, 1);
                                ExistingCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, 2);
                                StoppedCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, null);

                            }
                        }
                    }

                    GetCustomerCount objGetCustomerCount = new GetCustomerCount();
                    objGetCustomerCount.NewCustomer = NewCustomerCount;
                    objGetCustomerCount.ExistingCustomer = ExistingCustomerCount;
                    objGetCustomerCount.SuspendCustomer = StoppedCustomerCount;
                    result.Add(new Models.GetCustomerDataForGraph { Month = Convert.ToDateTime(Date).ToString("dd/MM/yyyy"), GetCustomerCount = objGetCustomerCount });
                }
            } 
            if (IsMonth == true || IsYear==true)
            {
                DateTime startDate = customer.StartDate.Value;
                DateTime endDate = customer.EndDate.Value;
                for (DateTime currentMonth = startDate; currentMonth <= endDate; currentMonth = currentMonth.AddMonths(1))
                {
                    DateTime currentMonthStart = currentMonth.Month == startDate.Month && currentMonth.Year == startDate.Year
                                                 ? startDate
                                                 : new DateTime(currentMonth.Year, currentMonth.Month, 1);

                    DateTime currentMonthEnd = new DateTime(currentMonth.Year, currentMonth.Month, DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month));
                    if (currentMonthEnd > endDate)
                    {
                        currentMonthEnd = endDate;
                    }
                    objMonths.Add(currentMonthStart.ToString("MMMM yyyy"));
                    var objNewCustomerCount = objGetCustomer.Where(x => x.Customer.CreatedOn >= currentMonthStart && x.Customer.CreatedOn <= currentMonthEnd);
                    var objExistingCustomerCount = objGetCustomer.Where(x => x.Customer.CustomerTypeOn >= currentMonthStart && x.Customer.CustomerTypeOn <= currentMonthEnd);
                    var objStoppedCustomerCount = objGetCustomer.Where(x => x.UpdatedOn >= currentMonthStart && x.UpdatedOn <= currentMonthEnd);
                    int NewCustomerCount = 0, ExistingCustomerCount = 0, StoppedCustomerCount = 0;

                    if (customer.propaID == "All")
                    {
                        if (customer.vID == "All")
                        {
                            var objPropertyAreas = UhDB.PropertyAreas.Where(x => x.uID == customer.uID && x.IsActive == true && x.IsDelete == false).ToList();
                            foreach (var objPropertyArea in objPropertyAreas)
                            {
                                int? propaID = objPropertyArea.propaID;
                                string PropertyArea = objPropertyArea.Name;
                                var objVentures = UhDB.Ventures.Where(x => x.uID == customer.uID && x.propaID == propaID && x.IsActive == true && x.IsDelete == false).ToList();
                                foreach (var objVenture in objVentures)
                                {
                                    int? vID = objVenture.vID;
                                    if (customer.CustomerType != null)
                                    {
                                        if (customer.CustomerType == 1)
                                        {
                                            NewCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, customer.CustomerType);
                                        }
                                        else if (customer.CustomerType == 2)
                                        {
                                            ExistingCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, customer.CustomerType);
                                        }
                                        else
                                        {
                                            StoppedCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, null);
                                        }
                                    }
                                    else
                                    {
                                        NewCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, 1);
                                        ExistingCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, 2);
                                        StoppedCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, null);
                                    }
                                }
                            }
                        }
                        else
                        {

                            int? vID = Convert.ToInt32(customer.vID);
                            if (customer.CustomerType != null)
                            {
                                if (customer.CustomerType == 1)
                                {
                                    NewCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, null, customer.CustomerType);
                                }
                                else if (customer.CustomerType == 2)
                                {
                                    ExistingCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, null, customer.CustomerType);
                                }
                                else
                                {
                                    StoppedCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, null, null);
                                }
                            }
                            else
                            {
                                NewCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, null, 1);
                                ExistingCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, null, 2);
                                StoppedCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, null, null);
                            }
                        }
                    }
                    else
                    {
                        if (customer.vID == "All")
                        {
                            int? propaID = Convert.ToInt32(customer.propaID);
                            var objVentures = await UhDB.Ventures.Where(x => x.uID == customer.uID && x.propaID == propaID && x.IsActive == true && x.IsDelete == false).ToListAsync();
                            foreach (var objVenture in objVentures)
                            {
                                int? vID = objVenture.vID;
                                if (customer.CustomerType != null)
                                {
                                    if (customer.CustomerType == 1)
                                    {
                                        NewCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, customer.CustomerType);
                                    }
                                    else if (customer.CustomerType == 2)
                                    {
                                        ExistingCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, customer.CustomerType);
                                    }
                                    else
                                    {
                                        StoppedCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, null);
                                    }
                                }
                                else
                                {
                                    NewCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, 1);
                                    ExistingCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, 2);
                                    StoppedCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, null);
                                }
                            }
                        }
                        else
                        {
                            int? propaID = Convert.ToInt32(customer.propaID);
                            int? vID = Convert.ToInt32(customer.vID);
                            if (customer.CustomerType != null)
                            {
                                if (customer.CustomerType == 1)
                                {
                                    NewCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, customer.CustomerType);
                                }
                                else if (customer.CustomerType == 2)
                                {
                                    ExistingCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, customer.CustomerType);
                                }
                                else
                                {
                                    StoppedCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, null);
                                }
                            }
                            else
                            {
                                NewCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, 1);
                                ExistingCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, 2);
                                StoppedCustomerCount += GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, null);
                            }
                        }
                    }
                    GetCustomerCount objGetCustomerCount = new GetCustomerCount();
                    objGetCustomerCount.NewCustomer = NewCustomerCount;
                    objGetCustomerCount.ExistingCustomer = ExistingCustomerCount;
                    objGetCustomerCount.SuspendCustomer = StoppedCustomerCount;
                    if(IsMonth==true && IsYear==false)
                    {
                        result.Add(new Models.GetCustomerDataForGraph { Month = currentMonth.ToString("MMM"), GetCustomerCount = objGetCustomerCount });
                    }
                    else if(IsMonth==true && IsYear==true)
                    {
                        result.Add(new Models.GetCustomerDataForGraph { Month = currentMonth.ToString("MMM yyyy"), GetCustomerCount = objGetCustomerCount });
                    }
                    
                }        
            }
               return result;
        }

            private int[] GetCustomerCountByCustomerType(CustomerReportModel customer, ref IQueryable<CustomerOfficalDetail> GetCustomer1)
            {
                int[] result = new int[3];
                int newCustomerCount = 0, existingCustomerCount = 0, stoppedCustomerCount = 0;
                if (customer.CustomerType != null)
                {
                    if (customer.CustomerType == 1)
                    {
                        if (customer.EndDate != null)
                        {
                            GetCustomer1 = GetCustomer1.Where(X => EntityFunctions.TruncateTime(X.Customer.CreatedOn) >= EntityFunctions.TruncateTime(customer.StartDate) && EntityFunctions.TruncateTime(X.Customer.CreatedOn) <= EntityFunctions.TruncateTime(customer.EndDate));
                        }
                        else
                        {
                            GetCustomer1 = GetCustomer1.Where(x => EntityFunctions.TruncateTime(x.Customer.CreatedOn) == EntityFunctions.TruncateTime(customer.StartDate));
                        }
                         newCustomerCount = GetCustomer1.Where(x => x.Customer.CustomerType == customer.CustomerType).Select(y => y.custID).Distinct().Count();
                }
                    else if (customer.CustomerType == 2)
                    {
                        if (customer.EndDate != null)
                        {
                            GetCustomer1 = GetCustomer1.Where(X => EntityFunctions.TruncateTime(X.Customer.CustomerTypeOn) >= EntityFunctions.TruncateTime(customer.StartDate) && EntityFunctions.TruncateTime(X.Customer.CustomerTypeOn) <= EntityFunctions.TruncateTime(customer.EndDate));

                        }
                        else
                        {
                            GetCustomer1 = GetCustomer1.Where(x => EntityFunctions.TruncateTime(x.Customer.CustomerTypeOn) == EntityFunctions.TruncateTime(customer.StartDate));
                        }
                        existingCustomerCount = GetCustomer1.Where(x => x.Customer.CustomerType == customer.CustomerType).Select(y => y.custID).Distinct().Count();

                    }
                    else
                    {
                        if (customer.EndDate != null)
                        {
                            GetCustomer1 = GetCustomer1.Where(x => EntityFunctions.TruncateTime(x.UpdatedOn) >= EntityFunctions.TruncateTime(customer.StartDate) && EntityFunctions.TruncateTime(x.UpdatedOn) <= EntityFunctions.TruncateTime(customer.EndDate));
                        }
                        else
                        {
                            GetCustomer1= GetCustomer1.Where(x => EntityFunctions.TruncateTime(x.UpdatedOn) == EntityFunctions.TruncateTime(customer.StartDate));
                        }
                        stoppedCustomerCount = GetCustomer1.Where(x => x.ServiceStatus == false).ToList().Count();
                    }

                }
                else
                {
                    var objNewCustomerCount = GetCustomer1;
                    var objExistingCustomerCount = GetCustomer1;
                    var objStoppedCustomerCount = GetCustomer1;
                    if (customer.EndDate != null)
                    {
                        objNewCustomerCount = objNewCustomerCount.Where(X => EntityFunctions.TruncateTime(X.Customer.CreatedOn) >= EntityFunctions.TruncateTime(customer.StartDate) && EntityFunctions.TruncateTime(X.Customer.CreatedOn) <= EntityFunctions.TruncateTime(customer.EndDate));
                    }
                    else
                    {
                        objNewCustomerCount = objNewCustomerCount.Where(x => EntityFunctions.TruncateTime(x.Customer.CreatedOn) == EntityFunctions.TruncateTime(customer.StartDate));
                    }
                newCustomerCount = objNewCustomerCount.Where(x => x.Customer.CustomerType == 1).Select(y => y.custID).Distinct().Count();
                    
                    if (customer.EndDate != null)
                    {
                        objExistingCustomerCount = objExistingCustomerCount.Where(X => EntityFunctions.TruncateTime(X.Customer.CustomerTypeOn) >= EntityFunctions.TruncateTime(customer.StartDate) && EntityFunctions.TruncateTime(X.Customer.CustomerTypeOn) <= EntityFunctions.TruncateTime(customer.EndDate));
                    }
                    else
                    {
                        objExistingCustomerCount = objExistingCustomerCount.Where(x => EntityFunctions.TruncateTime(x.Customer.CustomerTypeOn) == EntityFunctions.TruncateTime(customer.StartDate));
                    }
            
                    existingCustomerCount = objExistingCustomerCount.Where(x => x.Customer.CustomerType == 2).Select(y => y.custID).Distinct().Count(); 
                    if (customer.EndDate != null)
                    {
                        objStoppedCustomerCount = objStoppedCustomerCount.Where(X => EntityFunctions.TruncateTime(X.UpdatedOn) >= EntityFunctions.TruncateTime(customer.StartDate) && EntityFunctions.TruncateTime(X.UpdatedOn) <= EntityFunctions.TruncateTime(customer.EndDate));

                    }
                    else
                    {
                        objStoppedCustomerCount = objStoppedCustomerCount.Where(x => EntityFunctions.TruncateTime(x.UpdatedOn) == EntityFunctions.TruncateTime(customer.StartDate));
                    }
                     stoppedCustomerCount = objStoppedCustomerCount.Count(x => x.ServiceStatus == false);
            }
                result[0] = newCustomerCount;
                result[1] = existingCustomerCount;
                result[2] = stoppedCustomerCount;
                return result;

            }

            private int GetCustomerCountByCustomerTypeByVenture(ref IQueryable<CustomerOfficalDetail> GetCustomerByV, int? vID, int? propaID, int? CustomerType)
            {
                int result = 0;

                if (CustomerType == 1 || CustomerType == 2)
                {
                    if (propaID != null)
                    {
                        result = GetCustomerByV.Where(x => x.Customer.CustomerType == CustomerType && x.vID == vID && x.propaID == propaID).Select(y=>y.custID).Distinct().Count();
                    }
                    else
                    {
                        result = GetCustomerByV.Where(x => x.Customer.CustomerType == CustomerType && x.vID == vID).Select(y => y.custID).Distinct().Count();
                }

                }
                else
                {
                    if (propaID != null)
                    {

                        result = GetCustomerByV.Where(x => x.ServiceStatus == false && x.vID == vID && x.propaID == propaID).Select(y => y.custID).Distinct().Count();
                    }
                    else
                    {

                        result = GetCustomerByV.Where(x => x.ServiceStatus == false && x.vID == vID).Select(y => y.custID).Distinct().Count();
                    }
                  }
                return result;

            }
            
            public int TotalCustomersCount()
            {
                return UhDB.Customers.Where(x => x.IsActive == true && x.IsDelete == false).Count();
            }

            public async Task<IEnumerable<GetCustomerDataForTable>> GetCustomerCountForTable(CustomerReportModel customer)
            {

            List<GetCustomerDataForTable> result = new List<GetCustomerDataForTable>();
            var objGetCustomer =  UhDB.CustomerOfficalDetails.Where(x => x.IsActive == true && x.IsDelete == false).AsQueryable();
            List<DateTime> objDates = new List<DateTime>();
            List<string> objMonths = new List<string>();

            bool isDate =false,isMonth = false,isYear=false;
            string TowerName = null;

            if (customer.StartDate != null && customer.EndDate != null)
            {
                DateTime startDate = customer.StartDate.Value;
                DateTime endDate = customer.EndDate.Value;
                var dateDifference = Math.Abs((startDate - endDate).Days);
                
                if (dateDifference <= 29)
                {
                    isDate = true;
                    for (DateTime date = (DateTime)customer.StartDate; date <= customer.EndDate; date = date.AddDays(1))
                    {
                      objDates.Add(date);
                    }
                }
                else if (dateDifference >= 30 && dateDifference <= 365)
                {
                    isMonth = true;
                   
                    for (DateTime month = startDate; month <= endDate; month = month.AddMonths(1))
                    {
                        objMonths.Add(month.ToString("MMMM yyyy"));
                    }
                }
                else if (dateDifference >= 365)
                {
                    isMonth = true;
                    isYear = true;
                    for (DateTime month = startDate; month <= endDate; month = month.AddMonths(1))
                    {
                        objMonths.Add(month.ToString("MMMM yyyy"));
                    }
                }
            }

            if(isDate==true)
                {
                        foreach (var objDate in objDates)
                        {
                            DateTime Date = Convert.ToDateTime(objDate);
                            var objNewCustomerCount = objGetCustomer;
                            var objExistingCustomerCount = objGetCustomer;
                            var objStoppedCustomerCount = objGetCustomer;

                            objNewCustomerCount = objNewCustomerCount.Where(x => EntityFunctions.TruncateTime(x.Customer.CreatedOn) == EntityFunctions.TruncateTime(Date));
                            objExistingCustomerCount = objExistingCustomerCount.Where(x => EntityFunctions.TruncateTime(x.Customer.CustomerTypeOn) == EntityFunctions.TruncateTime(Date));
                            objStoppedCustomerCount = objStoppedCustomerCount.Where(x => EntityFunctions.TruncateTime(x.UpdatedOn) == EntityFunctions.TruncateTime(Date));

                            int NewCustomerCount = 0, ExistingCustomerCount = 0, StoppedCustomerCount = 0;
                            if (customer.propaID == "All")
                            {
                                if (customer.vID == "All")
                                {
                                    var objPropertyAreas = await UhDB.PropertyAreas.Where(x => x.uID == customer.uID && x.IsActive == true && x.IsDelete == false).ToListAsync();
                                    foreach (var objPropertyArea in objPropertyAreas)
                                    {
                                        int? propaID = objPropertyArea.propaID;
                                        string PropertyArea = objPropertyArea.Name;
                                        var objVentures = await UhDB.Ventures.Where(x => x.uID == customer.uID && x.propaID == propaID && x.IsActive == true && x.IsDelete == false).ToListAsync();
                                        foreach (var objVenture in objVentures)
                                        {
                                            int? vID = objVenture.vID;
                                            TowerName = objVenture.Name;
                                            if (customer.CustomerType != null)
                                            {
                                                if (customer.CustomerType == 1)
                                                {
                                                    NewCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, customer.CustomerType);
                                                }
                                                else if (customer.CustomerType == 2)
                                                {
                                                    ExistingCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, customer.CustomerType);
                                                }
                                                else
                                                {
                                                    StoppedCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, customer.CustomerType);
                                                }
                                            }
                                            else
                                            {
                                                NewCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, 1);
                                                ExistingCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, 2);
                                                StoppedCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, 3);
                                            }
                                            GetCustomerTableCount objGetCustomerTableCount = new GetCustomerTableCount();
                                            objGetCustomerTableCount.Towers = TowerName;
                                            objGetCustomerTableCount.NewCustomer = NewCustomerCount;
                                            objGetCustomerTableCount.ExistingCustomer = ExistingCustomerCount;
                                            objGetCustomerTableCount.SuspendCustomer = StoppedCustomerCount;
                                            result.Add(new GetCustomerDataForTable { Month = Convert.ToDateTime(Date).ToString("dd/MM/yyyy"), TableData = objGetCustomerTableCount });
                                        }


                                    }
                                }
                                else
                                {

                                   // int? propaID = Convert.ToInt32(customer.propaID);
                                    int? vID = Convert.ToInt32(customer.vID);
                                    TowerName = UhDB.Ventures.Where(x => x.uID == customer.uID && x.vID == vID && x.IsActive == true && x.IsDelete == false).Select(p => p.Name).SingleOrDefault();

                                    if (customer.CustomerType != null)
                                    {
                                        if (customer.CustomerType == 1)
                                        {
                                            NewCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, null, customer.CustomerType);
                                        }
                                        else if (customer.CustomerType == 2)
                                        {
                                            ExistingCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, null, customer.CustomerType);
                                        }
                                        else
                                        {
                                            StoppedCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, null, null);
                                        }
                                    }
                                    else
                                    {
                                        NewCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, null, 1);
                                        ExistingCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, null, vID, 2);
                                        StoppedCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, null, 3);

                                    }
                                    GetCustomerTableCount objGetCustomerTableCount = new GetCustomerTableCount();
                                    objGetCustomerTableCount.Towers = TowerName;
                                    objGetCustomerTableCount.NewCustomer = NewCustomerCount;
                                    objGetCustomerTableCount.ExistingCustomer = ExistingCustomerCount;
                                    objGetCustomerTableCount.SuspendCustomer = StoppedCustomerCount;
                                    result.Add(new GetCustomerDataForTable { Month = Convert.ToDateTime(Date).ToString("dd/MM/yyyy"), TableData = objGetCustomerTableCount });
                                }
                            }
                            else
                            {
                                if (customer.vID == "All")
                                {
                                    int? propaID = Convert.ToInt32(customer.propaID);
                                    var objVentures = await UhDB.Ventures.Where(x => x.uID == customer.uID && x.IsActive == true && x.IsDelete == false).ToListAsync();
                                    foreach (var objVenture in objVentures)
                                    {
                                        int? vID = objVenture.vID;
                                        TowerName = UhDB.Ventures.Where(x => x.uID == customer.uID && x.vID == vID && x.propaID==propaID && x.IsActive == true && x.IsDelete == false).Select(p => p.Name).SingleOrDefault();
                                        if (customer.CustomerType != null)
                                        {
                                            if (customer.CustomerType == 1)
                                            {
                                                NewCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, customer.CustomerType);
                                            }
                                            else if (customer.CustomerType == 2)
                                            {
                                                ExistingCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, customer.CustomerType);
                                            }
                                            else
                                            {
                                                StoppedCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, null);
                                            }
                                        }
                                        else
                                        {
                                            NewCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, 1);
                                            ExistingCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, 2);
                                            StoppedCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, null);

                                        }
                                        GetCustomerTableCount objGetCustomerTableCount = new GetCustomerTableCount();
                                        objGetCustomerTableCount.Towers = TowerName;
                                        objGetCustomerTableCount.NewCustomer = NewCustomerCount;
                                        objGetCustomerTableCount.ExistingCustomer = ExistingCustomerCount;
                                        objGetCustomerTableCount.SuspendCustomer = StoppedCustomerCount;
                                        result.Add(new GetCustomerDataForTable { Month = Convert.ToDateTime(Date).ToString("dd/MM/yyyy"), TableData = objGetCustomerTableCount });
                                    }
                                }
                                else
                                {
                                    int? propaID = Convert.ToInt32(customer.propaID);
                                    int? vID = Convert.ToInt32(customer.vID);
                                    TowerName = UhDB.Ventures.Where(x => x.uID == customer.uID  && x.vID == vID  &&x.propaID==propaID && x.IsActive == true && x.IsDelete == false).Select(p => p.Name).SingleOrDefault();
                                    if (customer.CustomerType != null)
                                    {
                                        if (customer.CustomerType == 1)
                                        {
                                            NewCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, customer.CustomerType);
                                        }
                                        else if (customer.CustomerType == 2)
                                        {
                                            ExistingCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, customer.CustomerType);
                                        }
                                        else
                                        {
                                            StoppedCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, null);
                                        }
                                    }
                                    else
                                    {
                                        NewCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, 1);
                                        ExistingCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, 2);
                                        StoppedCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, null);
                                    }
                                    GetCustomerTableCount objGetCustomerTableCount = new GetCustomerTableCount();
                                    objGetCustomerTableCount.Towers = TowerName;
                                    objGetCustomerTableCount.NewCustomer = NewCustomerCount;
                                    objGetCustomerTableCount.ExistingCustomer = ExistingCustomerCount;
                                    objGetCustomerTableCount.SuspendCustomer = StoppedCustomerCount;
                                    result.Add(new GetCustomerDataForTable { Month = Convert.ToDateTime(Date).ToString("dd/MM/yyyy"), TableData = objGetCustomerTableCount });
                                }
                            }

                        }
                    }
            if (isMonth == true ||isYear==true)
            {
                DateTime startDate = customer.StartDate.Value;
                DateTime endDate = customer.EndDate.Value;
                for (DateTime currentMonth = startDate; currentMonth <= endDate; currentMonth = currentMonth.AddMonths(1))
                {
                    DateTime currentMonthStart = currentMonth.Month == startDate.Month && currentMonth.Year == startDate.Year
                                                 ? startDate
                                                 : new DateTime(currentMonth.Year, currentMonth.Month, 1);

                    DateTime currentMonthEnd = new DateTime(currentMonth.Year, currentMonth.Month, DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month));
                    if (currentMonthEnd > endDate)
                    {
                        currentMonthEnd = endDate;
                    }
                    objMonths.Add(currentMonthStart.ToString("MMMM yyyy"));
                    var objNewCustomerCount = objGetCustomer.Where(x => x.Customer.CreatedOn >= currentMonthStart && x.Customer.CreatedOn <= currentMonthEnd);
                    var objExistingCustomerCount = objGetCustomer.Where(x => x.Customer.CustomerTypeOn >= currentMonthStart && x.Customer.CustomerTypeOn <= currentMonthEnd);
                    var objStoppedCustomerCount = objGetCustomer.Where(x => x.UpdatedOn >= currentMonthStart && x.UpdatedOn <= currentMonthEnd);
                    int NewCustomerCount = 0, ExistingCustomerCount = 0, StoppedCustomerCount = 0;
                    if (customer.propaID == "All")
                    {
                        if (customer.vID == "All")
                        {
                            var objPropertyAreas = await UhDB.PropertyAreas.Where(x => x.uID == customer.uID && x.IsActive == true && x.IsDelete == false).ToListAsync();
                            foreach (var objPropertyArea in objPropertyAreas)
                            {
                                int? propaID = objPropertyArea.propaID;
                                string PropertyArea = objPropertyArea.Name;
                                var objVentures = await UhDB.Ventures.Where(x => x.uID == customer.uID && x.propaID == propaID && x.IsActive == true && x.IsDelete == false).ToListAsync();
                                foreach (var objVenture in objVentures)
                                {
                                    int? vID = objVenture.vID;
                                    TowerName = objVenture.Name;
                                    if (customer.CustomerType != null)
                                    {
                                        if (customer.CustomerType == 1)
                                        {
                                            NewCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, customer.CustomerType);
                                        }
                                        else if (customer.CustomerType == 2)
                                        {
                                            ExistingCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, customer.CustomerType);
                                        }
                                        else
                                        {
                                            StoppedCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, customer.CustomerType);
                                        }
                                    }
                                    else
                                    {
                                        NewCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, 1);
                                        ExistingCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, 2);
                                        StoppedCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, 3);
                                    }
                                    GetCustomerTableCount objGetCustomerTableCount = new GetCustomerTableCount();
                                    objGetCustomerTableCount.Towers = TowerName;
                                    objGetCustomerTableCount.NewCustomer = NewCustomerCount;
                                    objGetCustomerTableCount.ExistingCustomer = ExistingCustomerCount;
                                    objGetCustomerTableCount.SuspendCustomer = StoppedCustomerCount;
                                    if(isMonth==true && isYear==false)
                                    {
                                        result.Add(new GetCustomerDataForTable { Month = currentMonth.ToString("MMM"), TableData = objGetCustomerTableCount });
                                    }
                                    else if(isMonth==true&&isYear==true)
                                    {
                                        result.Add(new GetCustomerDataForTable { Month = currentMonth.ToString("MMM yyyy"), TableData = objGetCustomerTableCount });
                                    }
                                    
                                }
                            }
                        }
                        else
                        {

                            //int? propaID = Convert.ToInt32(customer.propaID);
                            int? vID = Convert.ToInt32(customer.vID);
                            TowerName = UhDB.Ventures.Where(x => x.uID == customer.uID &&  x.vID == vID && x.IsActive == true && x.IsDelete == false).Select(p => p.Name).SingleOrDefault();

                            if (customer.CustomerType != null)
                            {
                                if (customer.CustomerType == 1)
                                {
                                    NewCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, null, customer.CustomerType);
                                }
                                else if (customer.CustomerType == 2)
                                {
                                    ExistingCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, null, customer.CustomerType);
                                }
                                else
                                {
                                    StoppedCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, null, null);
                                }
                            }
                            else
                            {
                                NewCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, null, 1);
                                ExistingCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, null, vID, 2);
                                StoppedCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, null, 3);

                            }
                            GetCustomerTableCount objGetCustomerTableCount = new GetCustomerTableCount();
                            objGetCustomerTableCount.Towers = TowerName;
                            objGetCustomerTableCount.NewCustomer = NewCustomerCount;
                            objGetCustomerTableCount.ExistingCustomer = ExistingCustomerCount;
                            objGetCustomerTableCount.SuspendCustomer = StoppedCustomerCount;
                            if (isMonth == true && isYear == false)
                            {
                                result.Add(new GetCustomerDataForTable { Month = currentMonth.ToString("MMM"), TableData = objGetCustomerTableCount });
                            }
                            else if (isMonth == true && isYear == true)
                            {
                                result.Add(new GetCustomerDataForTable { Month = currentMonth.ToString("MMM yyyy"), TableData = objGetCustomerTableCount });
                            }
                        }
                    }
                    else
                    {
                        if (customer.vID == "All")
                        {
                            int? propaID = Convert.ToInt32(customer.propaID);
                            var objVentures = await UhDB.Ventures.Where(x => x.uID == customer.uID && x.propaID == propaID && x.IsActive == true && x.IsDelete == false).ToListAsync();
                            foreach (var objVenture in objVentures)
                            {
                                int? vID = objVenture.vID;
                                TowerName = UhDB.Ventures.Where(x => x.uID == customer.uID && x.propaID == propaID && x.vID == vID && x.IsActive == true && x.IsDelete == false).Select(p => p.Name).SingleOrDefault();
                                if (customer.CustomerType != null)
                                {
                                    if (customer.CustomerType == 1)
                                    {
                                        NewCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, customer.CustomerType);
                                    }
                                    else if (customer.CustomerType == 2)
                                    {
                                        ExistingCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, customer.CustomerType);
                                    }
                                    else
                                    {
                                        StoppedCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, null);
                                    }
                                }
                                else
                                {
                                    NewCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, 1);
                                    ExistingCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, 2);
                                    StoppedCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, null);

                                }
                                GetCustomerTableCount objGetCustomerTableCount = new GetCustomerTableCount();
                                objGetCustomerTableCount.Towers = TowerName;
                                objGetCustomerTableCount.NewCustomer = NewCustomerCount;
                                objGetCustomerTableCount.ExistingCustomer = ExistingCustomerCount;
                                objGetCustomerTableCount.SuspendCustomer = StoppedCustomerCount;
                                if (isMonth == true && isYear == false)
                                {
                                    result.Add(new GetCustomerDataForTable { Month = currentMonth.ToString("MMM"), TableData = objGetCustomerTableCount });
                                }
                                else if (isMonth == true && isYear == true)
                                {
                                    result.Add(new GetCustomerDataForTable { Month = currentMonth.ToString("MMM yyyy"), TableData = objGetCustomerTableCount });
                                }
                            }
                        }
                        else
                        {
                            int? propaID = Convert.ToInt32(customer.propaID);
                            int? vID = Convert.ToInt32(customer.vID);
                            TowerName = UhDB.Ventures.Where(x => x.uID == customer.uID && x.propaID == propaID && x.vID == vID && x.IsActive == true && x.IsDelete == false).Select(p => p.Name).SingleOrDefault();
                            if (customer.CustomerType != null)
                            {
                                if (customer.CustomerType == 1)
                                {
                                    NewCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, customer.CustomerType);
                                }
                                else if (customer.CustomerType == 2)
                                {
                                    ExistingCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, customer.CustomerType);
                                }
                                else
                                {
                                    StoppedCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, null);
                                }
                            }
                            else
                            {
                                NewCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objNewCustomerCount, vID, propaID, 1);
                                ExistingCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objExistingCustomerCount, vID, propaID, 2);
                                StoppedCustomerCount = GetCustomerCountByCustomerTypeByVenture(ref objStoppedCustomerCount, vID, propaID, null);
                            }
                            GetCustomerTableCount objGetCustomerTableCount = new GetCustomerTableCount();
                            objGetCustomerTableCount.Towers = TowerName;
                            objGetCustomerTableCount.NewCustomer = NewCustomerCount;
                            objGetCustomerTableCount.ExistingCustomer = ExistingCustomerCount;
                            objGetCustomerTableCount.SuspendCustomer = StoppedCustomerCount;
                            if (isMonth == true && isYear == false)
                            {
                                result.Add(new GetCustomerDataForTable { Month = currentMonth.ToString("MMM"), TableData = objGetCustomerTableCount });
                            }
                            else if (isMonth == true && isYear == true)
                            {
                                result.Add(new GetCustomerDataForTable { Month = currentMonth.ToString("MMM yyyy"), TableData = objGetCustomerTableCount });
                            }
                        }
                    }
                }
            }    
            return result;
        }
    } 
}
