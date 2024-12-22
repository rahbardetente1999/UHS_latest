using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;
using System.Data.Entity.Core.Objects;

namespace UHSForm.DAL
{
    public class ServiceReportDB
    {
        private UHSEntities UhDB;

        public ServiceReportDB()
        {
            UhDB = new UHSEntities();
        }

        public int GetCountTotalService(int? uID)
        {
            int result = 0;
            result = UhDB.CustomerOfficalDetails.Where(x => x.Customer.uID == uID && x.IsActive == true && x.IsDelete == false).Count();
            return result;
        }

        public int[] GetCountService(ServiceCount service)
        {
            int[] result = new int[3];
            int TotalCount = 0, CancelledCount = 0, ReschduleCount = 0;
            if (service.EndDate != null)
            {
                TotalCount = UhDB.CustomerTimelines.Where(x => x.Customer.uID == service.uID && x.IsActive == true && x.IsDelete == false
                             && x.StatusOfWork == 3
                             && EntityFunctions.TruncateTime(x.UpdatedOn) >= EntityFunctions.TruncateTime(service.StartDate)
                             && EntityFunctions.TruncateTime(x.UpdatedOn) <= EntityFunctions.TruncateTime(service.EndDate)).Count();
                CancelledCount = UhDB.CustomerOfficalDetails.Where(x => x.Customer.uID == service.uID && x.IsActive == true && x.IsDelete == false
                                 && x.ServiceStatus == false
                                 && EntityFunctions.TruncateTime(x.UpdatedOn) >= EntityFunctions.TruncateTime(service.StartDate)
                                 && EntityFunctions.TruncateTime(x.UpdatedOn) <= EntityFunctions.TruncateTime(service.EndDate)).Count();
                ReschduleCount = UhDB.CustomerAlerts.Where(x => x.Customer.uID == service.uID && x.IsActive == true && x.IsDelete == false
                                 && x.custATID == 1
                                 && EntityFunctions.TruncateTime(x.CreatedOn) >= EntityFunctions.TruncateTime(service.StartDate)
                                 && EntityFunctions.TruncateTime(x.CreatedOn) <= EntityFunctions.TruncateTime(service.EndDate)).Count();
            }
            else
            {
                TotalCount = UhDB.CustomerTimelines.Where(x => x.Customer.uID == service.uID && x.IsActive == true && x.IsDelete == false && x.StatusOfWork==3
                             && EntityFunctions.TruncateTime(x.UpdatedOn) == EntityFunctions.TruncateTime(service.StartDate)).Count();
                CancelledCount = UhDB.CustomerOfficalDetails.Where(x => x.Customer.uID == service.uID && x.IsActive == true && x.IsDelete == false
                               && x.ServiceStatus == false
                               && EntityFunctions.TruncateTime(x.UpdatedOn) == EntityFunctions.TruncateTime(service.StartDate)).Count();

                ReschduleCount = UhDB.CustomerAlerts.Where(x => x.Customer.uID == service.uID && x.IsActive == true && x.IsDelete == false
                                 && x.custATID == 1
                                 && EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(service.StartDate)).Count();
            }
            result[0] = TotalCount;
            result[1] = CancelledCount;
            result[2] = ReschduleCount;
            return result;

        }

        public List<GetServiceData> GetServiceData(ServiceCount service)
        {
            List<GetServiceData> result = new List<GetServiceData>();
            List<DateTime> objDates = new List<DateTime>();
            List<string> objMonths = new List<string>();

            List<DateTime> objMonthYear = new List<DateTime>();
            bool IsDate = false, IsMonth = false, IsYear = false;
            string vName = null;
            if (service.EndDate == null)
            {
                objDates.Add(service.StartDate.Value);
                IsDate = true;
            }
            else
            {
                if (service.StartDate.HasValue && service.EndDate.HasValue)
                {
                    DateTime startDate = service.StartDate.Value;
                    DateTime endDate = service.EndDate.Value;
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
                    int TotalCompletedCount = UhDB.CustomerTimelines.Where(x => x.Customer.uID == service.uID && x.IsActive == true && x.IsDelete == false
                                              && EntityFunctions.TruncateTime(x.UpdatedOn) == EntityFunctions.TruncateTime(Date) && x.StatusOfWork == 3).Count();
                    int CancelledCount = UhDB.CustomerOfficalDetails.Where(x => x.Customer.uID == service.uID && x.IsActive == true && x.IsDelete == false
                                         && x.ServiceStatus == false
                                         && EntityFunctions.TruncateTime(x.UpdatedOn) == EntityFunctions.TruncateTime(Date)).Count();
                    int ReschduleCount = UhDB.CustomerAlerts.Where(x => x.Customer.uID == service.uID && x.IsActive == true && x.IsDelete == false
                                         && x.custATID == 1
                                         && EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(Date)).Count();

                    result.Add(new Models.GetServiceData { Month = Date.ToString("MM/dd/yyyy"), Completed = TotalCompletedCount, Cancelled = CancelledCount, Reschdule = ReschduleCount});
                }
            }
            if (IsMonth == true || IsYear == true)
            {
                DateTime startDate = service.StartDate.Value;
                DateTime endDate = service.EndDate.Value;
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
                    int TotalCompletedCount = UhDB.CustomerTimelines.Where(x => x.Customer.uID == service.uID && x.IsActive == true && x.IsDelete == false
                                              && EntityFunctions.TruncateTime(x.UpdatedOn) >= EntityFunctions.TruncateTime(currentMonthStart) && EntityFunctions.TruncateTime(x.UpdatedOn) <= EntityFunctions.TruncateTime(currentMonthEnd) && x.StatusOfWork == 3).Count();
                    int CancelledCount = UhDB.CustomerOfficalDetails.Where(x => x.Customer.uID == service.uID && x.IsActive == true && x.IsDelete == false
                                         && x.ServiceStatus == false
                                         && EntityFunctions.TruncateTime(x.UpdatedOn) >= EntityFunctions.TruncateTime(currentMonthStart) && EntityFunctions.TruncateTime(x.UpdatedOn) <= EntityFunctions.TruncateTime(currentMonthEnd)).Count();
                    int ReschduleCount = UhDB.CustomerAlerts.Where(x => x.Customer.uID == service.uID && x.IsActive == true && x.IsDelete == false
                                         && x.custATID == 1
                                         && EntityFunctions.TruncateTime(x.CreatedOn) >= EntityFunctions.TruncateTime(currentMonthStart) && EntityFunctions.TruncateTime(x.CreatedOn) <= EntityFunctions.TruncateTime(currentMonthEnd)).Count();
                    if (IsMonth == true && IsYear == false)
                    {
                        result.Add(new Models.GetServiceData { Month = currentMonth.ToString("MMMM"),Completed=TotalCompletedCount,Cancelled=CancelledCount,Reschdule=ReschduleCount });
                    }
                    else if (IsMonth == true && IsYear == true)
                    {
                        result.Add(new Models.GetServiceData { Month = currentMonth.ToString("MMMM yyyy"), Completed = TotalCompletedCount, Cancelled = CancelledCount, Reschdule = ReschduleCount });
                    }

                }
            }
            return result;
        }
    }
}