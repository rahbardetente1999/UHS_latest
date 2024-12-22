using HandlebarsDotNet;
using Newtonsoft.Json;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using UHSForm.Models;
using UHSForm.Models.Data;

namespace UHSForm.DAL
{
    public class CustomerDB
    {
        private UHSEntities UhDB;
        private GeneralDB objGeneralDB;

        public CustomerDB()
        {
            UhDB = new UHSEntities();
            objGeneralDB = new GeneralDB();
        }

        public List<string> CreateCustomerFirstTime(CustomerModel customer)
        {
            List<string> result = new List<string>();
            int CountEmail = UhDB.Logins.Where(x => x.Username == customer.Email && x.rID == 14 && x.IsActive == true && x.IsDelete == false).Count();
            if (CountEmail != 0)
            {
                result.Add("-1");
            }
            else
            {
                using (var trans = UhDB.Database.BeginTransaction())
                {
                    try
                    {
                        string Password = objGeneralDB.GeneratePassword(8);
                        System.Data.Entity.Core.Objects.ObjectParameter output = new System.Data.Entity.Core.Objects.ObjectParameter("responseMessage", typeof(string));
                        UhDB.SPmyCreateCustomer(Password, customer.Name, customer.Email, customer.Mobile, customer.AlternativeNo, customer.WhatsAppNo, customer.Salutaion, customer.CreatedRole, 14, null, 1, output);
                        if (output.Value.ToString() == "SUCCESS")
                        {
                            int? CustomerID = null;
                            int? TaskNo = null;
                            int? TempTaskNo = null;
                            int? cuID = null;
                            int? cuODID = null;
                            int CountCustomer = UhDB.Customers.Where(x => x.uID == 1 && x.IsActive == true && x.IsDelete == false && x.CustomerID != null).Count();
                            if (CountCustomer == 0)
                            {
                                CustomerID = 1;
                            }
                            else
                            {
                                CustomerID = UhDB.Customers.Where(x => x.uID == 1 && x.IsActive == true && x.IsDelete == false && x.CustomerID != null).OrderByDescending(x => x.cuID).FirstOrDefault().CustomerID;
                                CustomerID = CustomerID + 1;

                            }

                            var objCustomer = UhDB.Customers.Where(x => x.Email == customer.Email && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                            objCustomer.PhoneCode = customer.PhoneCode;
                            objCustomer.CustomerID = CustomerID;
                            objCustomer.CustomerType = 1;
                            objCustomer.UpdatedBy = customer.CreatedBy;
                            objCustomer.UpdatedOn = customer.CreatedOn;
                            objCustomer.UpdatedRole = customer.CreatedRole;
                            UhDB.SaveChanges();
                            cuID = objCustomer.cuID;

                            List<string> S1 = new List<string>();
                            if (customer.BundleOfDays != null) { }
                            var BuldleDays = customer.BundleOfDays;
                            foreach (var BuldleDay in BuldleDays)
                            {
                                S1.Add(BuldleDay.Days);
                            }


                            CustomerOfficalDetail objCustomerOfficalDetail = new CustomerOfficalDetail();
                            objCustomerOfficalDetail.catID = customer.catID;
                            objCustomerOfficalDetail.catsubID = customer.catsubID;
                            objCustomerOfficalDetail.BuldleDays = string.Join(",", S1);
                            objCustomerOfficalDetail.ServiceStatus = true;
                            objCustomerOfficalDetail.SpecialService = customer.SpecialService;
                            objCustomerOfficalDetail.propaID = customer.propaID;
                            objCustomerOfficalDetail.subAreaID = customer.subAreaID;
                            objCustomerOfficalDetail.vID = customer.vID;
                            objCustomerOfficalDetail.proprestID = customer.proprestID;
                            objCustomerOfficalDetail.propType = customer.propType;
                            objCustomerOfficalDetail.AppartmentNumber = customer.AppartmentNumber;
                            objCustomerOfficalDetail.Remarks = customer.Remarks;
                            objCustomerOfficalDetail.NoOfMonths = customer.monthlyCount;
                            objCustomerOfficalDetail.carstID = customer.carstID;
                            objCustomerOfficalDetail.cartID = customer.cartID;

                            objCustomerOfficalDetail.IsCarWash = customer.IsCarWash;
                            objCustomerOfficalDetail.carTRID = customer.carTRID;
                            objCustomerOfficalDetail.IsActive = customer.IsActive;
                            objCustomerOfficalDetail.IsDelete = customer.IsDelete;
                            objCustomerOfficalDetail.custID = cuID;
                            objCustomerOfficalDetail.CreatedBy = customer.CreatedBy;
                            objCustomerOfficalDetail.CreatedOn = customer.CreatedOn;
                            objCustomerOfficalDetail.CreatedRole = customer.CreatedRole;
                            UhDB.CustomerOfficalDetails.Add(objCustomerOfficalDetail);
                            UhDB.SaveChanges();
                            cuODID = objCustomerOfficalDetail.custODID;

                            if (customer.propType == 2)
                            {
                                CustomerOtherProperty objCustomerOtherProperty = new CustomerOtherProperty();
                                objCustomerOtherProperty.custID = cuID;
                                objCustomerOtherProperty.custODID = cuODID;
                                objCustomerOtherProperty.TowerName = customer.TowerName;
                                objCustomerOtherProperty.BuildingName = customer.BuildingName;
                                objCustomerOtherProperty.Loacation = customer.Location;
                                objCustomerOtherProperty.LocationLink = customer.LocationLink;
                                objCustomerOtherProperty.StreetNumber = customer.StreetNumber;
                                objCustomerOtherProperty.ZoneNumber = customer.ZoneNumber;
                                objCustomerOtherProperty.IsActive = customer.IsActive;
                                objCustomerOtherProperty.IsDelete = customer.IsDelete;
                                objCustomerOtherProperty.CreatedBy = customer.CreatedBy;
                                objCustomerOtherProperty.CreatedOn = customer.CreatedOn;
                                objCustomerOtherProperty.CreatedRole = customer.CreatedRole;
                                UhDB.CustomerOtherProperties.Add(objCustomerOtherProperty);
                                UhDB.SaveChanges();
                            }
                            if (customer.SpecialService == true)
                            {
                                foreach (var item in customer.ServiceSubCategory)
                                {
                                    CustomerSpecializedCleaning objCustomerSpecializedCleaning = new CustomerSpecializedCleaning();
                                    objCustomerSpecializedCleaning.custID = cuID;
                                    objCustomerSpecializedCleaning.custODID = cuODID;
                                    objCustomerSpecializedCleaning.servcatID = item.servcatID;
                                    objCustomerSpecializedCleaning.servsubcatID = item.servsubcatID;
                                    objCustomerSpecializedCleaning.Quantity = item.Quantity;
                                    objCustomerSpecializedCleaning.IsActive = customer.IsActive;
                                    objCustomerSpecializedCleaning.IsDelete = customer.IsDelete;
                                    objCustomerSpecializedCleaning.CreatedBy = customer.CreatedBy;
                                    objCustomerSpecializedCleaning.CreatedOn = customer.CreatedOn;
                                    objCustomerSpecializedCleaning.CreatedRole = customer.CreatedRole;
                                    UhDB.CustomerSpecializedCleanings.Add(objCustomerSpecializedCleaning);
                                    UhDB.SaveChanges();
                                }
                            }
                            if (customer.IsCarWash == true)
                            {
                                CustomerCarServiceDetail objCustomerCarServiceDetail = new CustomerCarServiceDetail();
                                objCustomerCarServiceDetail.custID = cuID;
                                objCustomerCarServiceDetail.custODID = cuODID;
                                objCustomerCarServiceDetail.ParkingLevel = customer.ParkingLevel;
                                objCustomerCarServiceDetail.ParkingNumber = customer.ParkingNumber;
                                objCustomerCarServiceDetail.VehicleBrand = customer.VehicleBrand;
                                objCustomerCarServiceDetail.VehicleColor = customer.VehicleColor;
                                objCustomerCarServiceDetail.VehicleNumber = customer.VehicleNumber;
                                objCustomerCarServiceDetail.IsActive = customer.IsActive;
                                objCustomerCarServiceDetail.IsDelete = customer.IsDelete;
                                objCustomerCarServiceDetail.CreatedBy = customer.CreatedBy;
                                objCustomerCarServiceDetail.CreatedOn = customer.CreatedOn;
                                UhDB.CustomerCarServiceDetails.Add(objCustomerCarServiceDetail);
                                UhDB.SaveChanges();
                            }


                            CustomerAvailability objCustomerAvailability = new CustomerAvailability();
                            objCustomerAvailability.Availability = customer.Availability;
                            objCustomerAvailability.KeyCollection = customer.KeyCollection;
                            objCustomerAvailability.ReceptionDate = customer.ReceptionDate;
                            objCustomerAvailability.AccessProperty = customer.AccessProperty;
                            objCustomerAvailability.custID = cuID;
                            objCustomerAvailability.custODID = cuODID;
                            objCustomerAvailability.IsActive = customer.IsActive;
                            objCustomerAvailability.IsDelete = customer.IsDelete;
                            objCustomerAvailability.CreatedBy = customer.CreatedBy;
                            objCustomerAvailability.CreatedOn = customer.CreatedOn;
                            objCustomerAvailability.CreatedRole = customer.CreatedRole;
                            UhDB.CustomerAvailabilities.Add(objCustomerAvailability);
                            UhDB.SaveChanges();

                            if (customer.catID == 1 && customer.catsubID == 1 && customer.Packages.IsCustomDays == null && customer.Packages.IsCustomSelectDate == null && customer.Packages.IsCustomTime == null && customer.SpecialService != true)
                            {
                                int? CountPack = UhDB.Packages.Where(x => x.packID == customer.Packages.packID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().RecursiveTime;
                                int? packID = customer.Packages.packID;
                                int? parkID = customer.Packages.parkID;
                                if (CountPack == 0)
                                {
                                    int CountCustomerTaskNo = UhDB.CustomerTimelines.Where(x => x.Customer.uID == 1 && x.TaskNo != null && x.IsActive == true && x.IsDelete == false).Count();
                                    if (CountCustomerTaskNo == 0)
                                    {
                                        TaskNo = 1;
                                    }
                                    else
                                    {
                                        TaskNo = UhDB.CustomerTimelines.Where(x => x.Customer.uID == 1 && x.TaskNo != null && x.IsActive == true && x.IsDelete == false).OrderByDescending(y => y.custTDID).FirstOrDefault().TaskNo;
                                        TaskNo = TaskNo + 1;
                                    }
                                    if (TempTaskNo == null)
                                    {
                                        TempTaskNo = TaskNo;
                                    }
                                    CustomTimes customerTimeLine = new CustomTimes();
                                    foreach (var BundleOfDays in customer.BundleOfDays)
                                    {
                                        customerTimeLine = BundleOfDays.Times;
                                    }
                                    CustomerTimeline objCustomerTimeline = new CustomerTimeline();
                                    objCustomerTimeline.custID = cuID;
                                    objCustomerTimeline.custODID = cuODID;
                                    objCustomerTimeline.custODID = cuODID;
                                    objCustomerTimeline.packID = packID;
                                    objCustomerTimeline.parkID = parkID;
                                    objCustomerTimeline.TaskNo = TaskNo;
                                    objCustomerTimeline.StatusOfWork = 2;
                                    objCustomerTimeline.teamID = customer.teamID;
                                    objCustomerTimeline.StartDate = customer.Packages.StartDate;
                                    objCustomerTimeline.StartTime = customerTimeLine.Start;
                                    objCustomerTimeline.EndTime = customerTimeLine.End;
                                    objCustomerTimeline.IsActive = customer.IsActive;
                                    objCustomerTimeline.IsDelete = customer.IsDelete;
                                    objCustomerTimeline.CreatedBy = customer.CreatedBy;
                                    objCustomerTimeline.CreatedOn = customer.CreatedOn;
                                    UhDB.CustomerTimelines.Add(objCustomerTimeline);
                                    UhDB.SaveChanges();
                                }
                                else
                                {
                                    DateTime? BlockDate = null;
                                    DateTime StartDate = Convert.ToDateTime(customer.Packages.StartDate);

                                    string StartDay = StartDate.DayOfWeek.ToString();
                                    ListOfDays objListOfDays1 = new ListOfDays();
                                    List<ListOfDisplayDays> ListOfDays = new List<ListOfDisplayDays>();
                                    ListOfDays = objListOfDays1.Days();
                                    List<string> BundleOfDays1 = customer.BundleOfDays.Select(x => x.Days).ToList();
                                    List<ListOfDisplayDays> filteredResult = ListOfDays.Where(day => BundleOfDays1.Contains(day.Day)).ToList();
                                    List<ListOfDisplayDays> BundleOfDays2 = CircleOutDays(filteredResult, StartDay);
                                    var BundleOfDays3 = BundleOfDays2.Select(x => x.Day).ToList();
                                    int BundleCount = BundleOfDays3.Count();
                                    int? NoOfMonth = UhDB.CustomerRenewalMonths.Where(x => x.custrmID == customer.monthlyCount && x.IsActive == true && x.IsDelete == false).FirstOrDefault().NoOfMonths;
                                    for (int k = 1; k <= NoOfMonth; k++)
                                    {
                                        DateTime? FirstDate = null;

                                        for (int i = 1; i <= 4; i++)
                                        {

                                            if (FirstDate != null)
                                            {
                                                DateTime TempDate = Convert.ToDateTime(FirstDate);
                                                int DateToAdd = 7 * (i - 1);
                                                TempDate = TempDate.AddDays(DateToAdd);
                                                StartDate = TempDate;
                                            }
                                            else
                                            {
                                                if (k > 1)
                                                {
                                                    ListOfDays objListOfDays = new ListOfDays();
                                                    string AFirstDay = StartDate.DayOfWeek.ToString();
                                                    string ASecondDay = StartDay;
                                                    int FirstDayID = objListOfDays.Days().Where(x => x.Day == AFirstDay).FirstOrDefault().ID;
                                                    int SecondDayID = objListOfDays.Days().Where(x => x.Day == ASecondDay).FirstOrDefault().ID;
                                                    StartDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(StartDate));
                                                }
                                            }
                                            string FirstDay = null;
                                            DateTime? AssignDate = null;
                                            List<string> EnteredDays = new List<string>();
                                            int EnterCount = 0;
                                            foreach (var BundleOfDay in BundleOfDays3)
                                            {
                                                string SecondDay = null;
                                                string Days = BundleOfDay;
                                                if ("Monday" == Days)
                                                {
                                                    if (FirstDay != null)
                                                    {
                                                        ListOfDays objListOfDays = new ListOfDays();
                                                        SecondDay = "Monday";
                                                        int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                        int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                        AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                        FirstDay = "Monday";
                                                    }
                                                    else
                                                    {
                                                        AssignDate = StartDate;
                                                        if (FirstDate == null)
                                                        {
                                                            FirstDate = AssignDate;
                                                        }
                                                        if (FirstDay == null)
                                                        {
                                                            FirstDay = "Monday";
                                                        }
                                                    }
                                                }
                                                else if ("Tuesday" == Days)
                                                {
                                                    if (FirstDay != null)
                                                    {
                                                        ListOfDays objListOfDays = new ListOfDays();
                                                        SecondDay = "Tuesday";
                                                        int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                        int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                        AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                        FirstDay = "Tuesday";
                                                    }
                                                    else
                                                    {
                                                        AssignDate = StartDate;
                                                        if (FirstDate == null)
                                                        {
                                                            FirstDate = AssignDate;
                                                        }
                                                        if (FirstDay == null)
                                                        {
                                                            FirstDay = "Tuesday";
                                                        }
                                                    }
                                                }
                                                else if ("Wednesday" == Days)
                                                {
                                                    if (FirstDay != null)
                                                    {
                                                        ListOfDays objListOfDays = new ListOfDays();
                                                        SecondDay = "Wednesday";
                                                        int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                        int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                        AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                        FirstDay = "Wednesday";
                                                    }
                                                    else
                                                    {
                                                        AssignDate = StartDate;
                                                        if (FirstDate == null)
                                                        {
                                                            FirstDate = AssignDate;
                                                        }
                                                        if (FirstDay == null)
                                                        {
                                                            FirstDay = "Wednesday";
                                                        }
                                                    }
                                                }
                                                else if ("Thursday" == Days)
                                                {
                                                    if (FirstDay != null)
                                                    {
                                                        ListOfDays objListOfDays = new ListOfDays();
                                                        SecondDay = "Thursday";
                                                        int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                        int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                        AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                        FirstDay = "Thursday";
                                                    }
                                                    else
                                                    {
                                                        AssignDate = StartDate;
                                                        if (FirstDate == null)
                                                        {
                                                            FirstDate = AssignDate;
                                                        }
                                                        if (FirstDay == null)
                                                        {
                                                            FirstDay = "Thursday";
                                                        }
                                                    }
                                                }
                                                else if ("Friday" == Days)
                                                {
                                                    if (FirstDay != null)
                                                    {
                                                        ListOfDays objListOfDays = new ListOfDays();
                                                        SecondDay = "Friday";
                                                        int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                        int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                        AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                        FirstDay = "Friday";
                                                    }
                                                    else
                                                    {
                                                        AssignDate = StartDate;
                                                        if (FirstDate == null)
                                                        {
                                                            FirstDate = AssignDate;
                                                        }
                                                        if (FirstDay == null)
                                                        {
                                                            FirstDay = "Friday";
                                                        }
                                                    }
                                                }
                                                else if ("Saturday" == Days)
                                                {
                                                    if (FirstDay != null)
                                                    {
                                                        ListOfDays objListOfDays = new ListOfDays();
                                                        SecondDay = "Saturday";
                                                        int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                        int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                        AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                        FirstDay = "Saturday";
                                                    }
                                                    else
                                                    {
                                                        AssignDate = StartDate;
                                                        if (FirstDate == null)
                                                        {
                                                            FirstDate = AssignDate;
                                                        }
                                                        if (FirstDay == null)
                                                        {
                                                            FirstDay = "Saturday";
                                                        }
                                                    }
                                                }
                                                else if ("Sunday" == Days)
                                                {
                                                    if (FirstDay != null)
                                                    {
                                                        ListOfDays objListOfDays = new ListOfDays();
                                                        SecondDay = "Sunday";
                                                        int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                        int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                        AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                        FirstDay = "Sunday";
                                                    }
                                                    else
                                                    {
                                                        AssignDate = StartDate;
                                                        if (FirstDate == null)
                                                        {
                                                            FirstDate = AssignDate;
                                                        }
                                                        if (FirstDay == null)
                                                        {
                                                            FirstDay = "Sunday";
                                                        }
                                                    }
                                                }

                                                int CountCustomerTaskNo = UhDB.CustomerTimelines.Where(x => x.Customer.uID == 1 && x.TaskNo != null && x.IsActive == true && x.IsDelete == false).Count();
                                                if (CountCustomerTaskNo == 0)
                                                {
                                                    TaskNo = 1;
                                                }
                                                else
                                                {
                                                    TaskNo = UhDB.CustomerTimelines.Where(x => x.Customer.uID == 1 && x.TaskNo != null && x.IsActive == true && x.IsDelete == false).OrderByDescending(y => y.custTDID).FirstOrDefault().TaskNo;
                                                    TaskNo = TaskNo + 1;
                                                }
                                                if (TempTaskNo == null)
                                                {
                                                    TempTaskNo = TaskNo;
                                                }
                                                CustomTimes objCustomTimes = customer.BundleOfDays.Where(x => x.Days == BundleOfDay).FirstOrDefault().Times;
                                                CustomerTimeline objCustomerTimeline = new CustomerTimeline();
                                                objCustomerTimeline.custID = cuID;
                                                objCustomerTimeline.custODID = cuODID;
                                                objCustomerTimeline.custODID = cuODID;
                                                objCustomerTimeline.packID = packID;
                                                objCustomerTimeline.parkID = parkID;
                                                objCustomerTimeline.TaskNo = TaskNo;
                                                objCustomerTimeline.StatusOfWork = 2;
                                                objCustomerTimeline.StartDate = AssignDate;
                                                objCustomerTimeline.teamID = customer.teamID;
                                                BlockDate = AssignDate;
                                                objCustomerTimeline.StartTime = objCustomTimes.Start;
                                                objCustomerTimeline.EndTime = objCustomTimes.End;
                                                objCustomerTimeline.IsActive = customer.IsActive;
                                                objCustomerTimeline.IsDelete = customer.IsDelete;
                                                objCustomerTimeline.CreatedBy = customer.CreatedBy;
                                                objCustomerTimeline.CreatedOn = customer.CreatedOn;
                                                UhDB.CustomerTimelines.Add(objCustomerTimeline);
                                                UhDB.SaveChanges();


                                            }

                                        }
                                        StartDate = Convert.ToDateTime(BlockDate);
                                    }
                                    if (BlockDate != null)
                                    {
                                        ListOfDays objAListOfDays = new ListOfDays();
                                        string ABFirstDay = BlockDate.Value.DayOfWeek.ToString();
                                        string ABSecondDay = StartDay;
                                        int ABFirstDayID = objAListOfDays.Days().Where(x => x.Day == ABFirstDay).FirstOrDefault().ID;
                                        int ABSecondDayID = objAListOfDays.Days().Where(x => x.Day == ABSecondDay).FirstOrDefault().ID;
                                        StartDate = GetNextDateForDay(ABFirstDayID, ABSecondDayID, Convert.ToDateTime(BlockDate));

                                        for (int k = 1; k <= NoOfMonth; k++)
                                        {
                                            DateTime? FirstDate = null;

                                            for (int i = 1; i <= 4; i++)
                                            {

                                                if (FirstDate != null)
                                                {
                                                    DateTime TempDate = Convert.ToDateTime(FirstDate);
                                                    int DateToAdd = 7 * (i - 1);
                                                    TempDate = TempDate.AddDays(DateToAdd);
                                                    StartDate = TempDate;
                                                }
                                                else
                                                {
                                                    if (k > 1)
                                                    {
                                                        ListOfDays objListOfDays = new ListOfDays();
                                                        string AFirstDay = StartDate.DayOfWeek.ToString();
                                                        string ASecondDay = StartDay;
                                                        int FirstDayID = objListOfDays.Days().Where(x => x.Day == AFirstDay).FirstOrDefault().ID;
                                                        int SecondDayID = objListOfDays.Days().Where(x => x.Day == ASecondDay).FirstOrDefault().ID;
                                                        StartDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(StartDate));

                                                    }
                                                }
                                                string FirstDay = null;
                                                DateTime? AssignDate = null;
                                                List<string> EnteredDays = new List<string>();
                                                int EnterCount = 0;
                                                foreach (var BundleOfDay in BundleOfDays3)
                                                {
                                                    string SecondDay = null;
                                                    string Days = BundleOfDay;
                                                    if ("Monday" == Days)
                                                    {
                                                        if (FirstDay != null)
                                                        {
                                                            ListOfDays objListOfDays = new ListOfDays();
                                                            SecondDay = "Monday";
                                                            int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                            int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                            AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                            FirstDay = "Monday";
                                                        }
                                                        else
                                                        {
                                                            AssignDate = StartDate;
                                                            if (FirstDate == null)
                                                            {
                                                                FirstDate = AssignDate;
                                                            }
                                                            if (FirstDay == null)
                                                            {
                                                                FirstDay = "Monday";
                                                            }
                                                        }
                                                    }
                                                    else if ("Tuesday" == Days)
                                                    {
                                                        if (FirstDay != null)
                                                        {
                                                            ListOfDays objListOfDays = new ListOfDays();
                                                            SecondDay = "Tuesday";
                                                            int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                            int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                            AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                            FirstDay = "Tuesday";
                                                        }
                                                        else
                                                        {
                                                            AssignDate = StartDate;
                                                            if (FirstDate == null)
                                                            {
                                                                FirstDate = AssignDate;
                                                            }
                                                            if (FirstDay == null)
                                                            {
                                                                FirstDay = "Tuesday";
                                                            }
                                                        }
                                                    }
                                                    else if ("Wednesday" == Days)
                                                    {
                                                        if (FirstDay != null)
                                                        {
                                                            ListOfDays objListOfDays = new ListOfDays();
                                                            SecondDay = "Wednesday";
                                                            int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                            int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                            AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                            FirstDay = "Wednesday";
                                                        }
                                                        else
                                                        {
                                                            AssignDate = StartDate;
                                                            if (FirstDate == null)
                                                            {
                                                                FirstDate = AssignDate;
                                                            }
                                                            if (FirstDay == null)
                                                            {
                                                                FirstDay = "Wednesday";
                                                            }
                                                        }
                                                    }
                                                    else if ("Thursday" == Days)
                                                    {
                                                        if (FirstDay != null)
                                                        {
                                                            ListOfDays objListOfDays = new ListOfDays();
                                                            SecondDay = "Thursday";
                                                            int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                            int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                            AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                            FirstDay = "Thursday";
                                                        }
                                                        else
                                                        {
                                                            AssignDate = StartDate;
                                                            if (FirstDate == null)
                                                            {
                                                                FirstDate = AssignDate;
                                                            }
                                                            if (FirstDay == null)
                                                            {
                                                                FirstDay = "Thursday";
                                                            }
                                                        }
                                                    }
                                                    else if ("Friday" == Days)
                                                    {
                                                        if (FirstDay != null)
                                                        {
                                                            ListOfDays objListOfDays = new ListOfDays();
                                                            SecondDay = "Friday";
                                                            int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                            int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                            AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                            FirstDay = "Friday";
                                                        }
                                                        else
                                                        {
                                                            AssignDate = StartDate;
                                                            if (FirstDate == null)
                                                            {
                                                                FirstDate = AssignDate;
                                                            }
                                                            if (FirstDay == null)
                                                            {
                                                                FirstDay = "Friday";
                                                            }
                                                        }
                                                    }
                                                    else if ("Saturday" == Days)
                                                    {
                                                        if (FirstDay != null)
                                                        {
                                                            ListOfDays objListOfDays = new ListOfDays();
                                                            SecondDay = "Saturday";
                                                            int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                            int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                            AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                            FirstDay = "Saturday";
                                                        }
                                                        else
                                                        {
                                                            AssignDate = StartDate;
                                                            if (FirstDate == null)
                                                            {
                                                                FirstDate = AssignDate;
                                                            }
                                                            if (FirstDay == null)
                                                            {
                                                                FirstDay = "Saturday";
                                                            }
                                                        }
                                                    }
                                                    else if ("Sunday" == Days)
                                                    {
                                                        if (FirstDay != null)
                                                        {
                                                            ListOfDays objListOfDays = new ListOfDays();
                                                            SecondDay = "Sunday";
                                                            int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                            int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                            AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                            FirstDay = "Sunday";
                                                        }
                                                        else
                                                        {
                                                            AssignDate = StartDate;
                                                            if (FirstDate == null)
                                                            {
                                                                FirstDate = AssignDate;
                                                            }
                                                            if (FirstDay == null)
                                                            {
                                                                FirstDay = "Sunday";
                                                            }
                                                        }
                                                    }

                                                    int CountCustomerTaskNo = UhDB.CustomerTimelines.Where(x => x.Customer.uID == 1 && x.TaskNo != null && x.IsActive == true && x.IsDelete == false).Count();
                                                    if (CountCustomerTaskNo == 0)
                                                    {
                                                        TaskNo = 1;
                                                    }
                                                    else
                                                    {
                                                        TaskNo = UhDB.CustomerTimelines.Where(x => x.Customer.uID == 1 && x.TaskNo != null && x.IsActive == true && x.IsDelete == false).OrderByDescending(y => y.custTDID).FirstOrDefault().TaskNo;
                                                        TaskNo = TaskNo + 1;
                                                    }
                                                    if (TempTaskNo == null)
                                                    {
                                                        TempTaskNo = TaskNo;
                                                    }
                                                    CustomTimes objCustomTimes = customer.BundleOfDays.Where(x => x.Days == BundleOfDay).FirstOrDefault().Times;
                                                    CustomerDateBlock objCustomerDateBlock = new CustomerDateBlock();
                                                    objCustomerDateBlock.custID = cuID;
                                                    objCustomerDateBlock.custODID = cuODID;
                                                    objCustomerDateBlock.custODID = cuODID;
                                                    objCustomerDateBlock.packID = packID;
                                                    objCustomerDateBlock.parkID = parkID;
                                                    objCustomerDateBlock.StartDate = AssignDate;
                                                    BlockDate = AssignDate;
                                                    objCustomerDateBlock.teamID = customer.teamID;
                                                    objCustomerDateBlock.StartTime = objCustomTimes.Start;
                                                    objCustomerDateBlock.EndTime = objCustomTimes.End;
                                                    objCustomerDateBlock.IsActive = customer.IsActive;
                                                    objCustomerDateBlock.IsDelete = customer.IsDelete;
                                                    objCustomerDateBlock.CreatedBy = customer.CreatedBy;
                                                    objCustomerDateBlock.CreatedOn = customer.CreatedOn;
                                                    UhDB.CustomerDateBlocks.Add(objCustomerDateBlock);
                                                    UhDB.SaveChanges();
                                                }
                                            }
                                            StartDate = Convert.ToDateTime(BlockDate);
                                        }
                                    }

                                }
                            }
                            else
                            {

                                int CountCustomerTaskNo = UhDB.CustomerTimelines.Where(x => x.Customer.uID == 1 && x.TaskNo != null && x.IsActive == true && x.IsDelete == false).Count();
                                if (CountCustomerTaskNo == 0)
                                {
                                    TaskNo = 1;
                                }
                                else
                                {
                                    TaskNo = UhDB.CustomerTimelines.Where(x => x.Customer.uID == 1 && x.TaskNo != null && x.IsActive == true && x.IsDelete == false).OrderByDescending(y => y.custTDID).FirstOrDefault().TaskNo;
                                    TaskNo = TaskNo + 1;
                                }
                                if (TempTaskNo == null)
                                {
                                    TempTaskNo = TaskNo;
                                }
                                if (customer.SpecialService == true)
                                {
                                    CustomerTimeline objCustomerTimeline = new CustomerTimeline();
                                    objCustomerTimeline.custID = cuID;
                                    objCustomerTimeline.custODID = cuODID;
                                    objCustomerTimeline.TaskNo = TaskNo;
                                    objCustomerTimeline.packID = customer.packID;
                                    objCustomerTimeline.parkID = customer.parkID;
                                    objCustomerTimeline.StatusOfWork = 1;
                                    objCustomerTimeline.StartDate = customer.StartDate;
                                    objCustomerTimeline.StartTime = customer.StartTime;
                                    objCustomerTimeline.EndTime = customer.EndTime;
                                    objCustomerTimeline.IsActive = customer.IsActive;
                                    objCustomerTimeline.IsDelete = customer.IsDelete;
                                    objCustomerTimeline.CreatedBy = customer.CreatedBy;
                                    objCustomerTimeline.CreatedOn = customer.CreatedOn;
                                    UhDB.CustomerTimelines.Add(objCustomerTimeline);
                                    UhDB.SaveChanges();
                                }
                                else
                                {
                                    int? packID = customer.Packages.packID;
                                    int? parkID = customer.Packages.parkID;
                                    CustomTimes customerTimeLine = new CustomTimes();
                                    foreach (var BundleOfDays in customer.BundleOfDays)
                                    {
                                        customerTimeLine = BundleOfDays.Times;
                                    }
                                    CustomerTimeline objCustomerTimeline = new CustomerTimeline();
                                    objCustomerTimeline.custID = cuID;
                                    objCustomerTimeline.custODID = cuODID;
                                    objCustomerTimeline.packID = packID;
                                    objCustomerTimeline.parkID = parkID;
                                    objCustomerTimeline.TaskNo = TaskNo;
                                    objCustomerTimeline.StatusOfWork = 1;
                                    objCustomerTimeline.StartDate = customer.Packages.StartDate;
                                    objCustomerTimeline.StartTime = customerTimeLine.Start;
                                    objCustomerTimeline.EndTime = customerTimeLine.End;
                                    objCustomerTimeline.IsActive = customer.IsActive;
                                    objCustomerTimeline.IsDelete = customer.IsDelete;
                                    objCustomerTimeline.CreatedBy = customer.CreatedBy;
                                    objCustomerTimeline.CreatedOn = customer.CreatedOn;
                                    UhDB.CustomerTimelines.Add(objCustomerTimeline);
                                    UhDB.SaveChanges();
                                }



                            }
                            CustomerInovice objCustomerInovice = new CustomerInovice();
                            objCustomerInovice.cuID = cuID;
                            objCustomerInovice.custODID = cuODID;
                            objCustomerInovice.InvoiceNumber = customer.InVoice;
                            objCustomerInovice.uID = 1;
                            objCustomerInovice.IsActive = customer.IsActive;
                            objCustomerInovice.IsDelete = customer.IsDelete;
                            objCustomerInovice.CreatedRole = customer.CreatedRole;
                            objCustomerInovice.CreatedBy = customer.CreatedBy;
                            objCustomerInovice.CreatedOn = customer.CreatedOn;
                            UhDB.CustomerInovices.Add(objCustomerInovice);
                            UhDB.SaveChanges();

                            if (customer.SpecialService == true)
                            {

                            }
                            else
                            {
                                if (customer.Packages != null && customer.Packages.IsCustomDays == null && customer.Packages.IsCustomSelectDate == null && customer.Packages.IsCustomTime == null && customer.catID == 1 && customer.catsubID == 1)
                                {
                                    if (customer.teamID != null)
                                    {
                                        string CustomerName = null, CustomerEmail = null, CustomerMobile = null, PropertyName = null,
                                               ApartmentName = null, Area = null;
                                        int? propType = null;

                                        var objCustomerOfficalDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID && x.custODID == cuODID && x.catID == customer.catID && x.catsubID == customer.catsubID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                        var objStaffTeams = UhDB.StaffTeams.Where(x => x.teamID == customer.teamID && x.IsActive == true && x.IsDelete == false).ToList();
                                        propType = objCustomerOfficalDetails.propType;
                                        CustomerName = objCustomerOfficalDetails.Customer.Name;
                                        CustomerEmail = objCustomerOfficalDetails.Customer.Email;
                                        CustomerMobile = objCustomerOfficalDetails.Customer.Mobile;
                                        Area = UhDB.PropertyAreas.Where(x => x.propaID == customer.propaID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                                        PropertyName = UhDB.Ventures.Where(x => x.vID == customer.vID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                                        ApartmentName = objCustomerOfficalDetails.AppartmentNumber;
                                        foreach (var item in objStaffTeams)
                                        {
                                            int? stfID = item.stfID;
                                            var objStaffs = UhDB.Staffs.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                            string StaffEmail = objStaffs.Email;
                                            string StaffName = objStaffs.Name;
                                            string EmailBody = null;
                                            if (customer.propType == 1)
                                            {
                                                EmailBody = EmailBodyCustomerInfoForStaff(Area, PropertyName, CustomerName, CustomerMobile, CustomerEmail, StaffName, objCustomerOfficalDetails.Remarks);
                                                if (StaffEmail != null)
                                                {
                                                    objGeneralDB.SentEmailFromAmazon(StaffEmail, EmailBody, "Task Assigned", StaffName);
                                                }
                                                else
                                                {

                                                }
                                            }
                                            else
                                            {
                                                string TowerName = customer.TowerName;
                                                string BuildingName = customer.BuildingName;
                                                string ZoneNumber = customer.ZoneNumber;
                                                string Location = customer.Location;
                                                string StreetNumber = customer.StreetNumber;
                                                string LocationLink = customer.LocationLink;
                                                string ApartmentNo = customer.AppartmentNumber;
                                                string Description = customer.Remarks;
                                                EmailBody = EmailBodyCustomerInfoForStaffForOtherProperty(CustomerName, CustomerEmail, CustomerMobile, Area, TowerName, BuildingName, StreetNumber, ZoneNumber, Location, LocationLink, StaffName, ApartmentNo, Description);
                                                if (StaffEmail != null)
                                                {
                                                    objGeneralDB.SentEmailFromAmazon(StaffEmail, EmailBody, "Task Assigned", StaffName);
                                                }
                                                else
                                                {

                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string body = EmailBodyForStaffNotAssign(CustomerID.ToString(), TempTaskNo.ToString());
                                        objGeneralDB.SentEmailFromAmazon(objCustomer.User.Email, body, "Staff Not Assign Yet", objCustomer.User.Name);
                                    }
                                }
                                else
                                {
                                    string body = EmailBodyForCustomerAssignDate(CustomerID.ToString(), TempTaskNo.ToString());
                                    objGeneralDB.SentEmailFromAmazon(objCustomer.User.Email, body, "Customer request for Custom Dates", objCustomer.User.Name);
                                }
                            }
                            string PaymentLink = null;
                            if (customer.SpecialService == true)
                            {
                                PaymentRequest objPaymentRequest = new PaymentRequest();
                                objPaymentRequest.FirstName = customer.Name;
                                objPaymentRequest.LastName = customer.Name;
                                objPaymentRequest.Email = customer.Email;
                                objPaymentRequest.Phone = customer.Mobile;
                                objPaymentRequest.Street = UhDB.PropertyAreas.Where(x => x.propaID == customer.propaID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                                objPaymentRequest.Amount = customer.Amount;
                                objPaymentRequest.City = "Doha";
                                objPaymentRequest.State = "DL";
                                objPaymentRequest.PostalCode = "110015";
                                objPaymentRequest.Country = "QR";
                                objPaymentRequest.Custom1 = cuID.ToString();
                                int CountTransactionID = UhDB.CustomerTransactions.Where(x => x.IsActive == true && x.IsDelete == false && x.Customer.uID == 1).Count();
                                if (CountTransactionID == 0)
                                {
                                    objPaymentRequest.TransactionId = "1";
                                }
                                else
                                {
                                    string TransID = UhDB.CustomerTransactions.Where(x => x.IsActive == true && x.IsDelete == false && x.Customer.uID == 1).OrderByDescending(x => x.custTrasID).FirstOrDefault().TransactionID;
                                    objPaymentRequest.TransactionId = (Convert.ToInt32(TransID) + 1).ToString();
                                }


                                List<string> Signatures = CalculateSignature(objPaymentRequest);
                                string id = null, TransactionID = null;
                                for (var i = 0; i < 3; i++)
                                {
                                    if (i == 0)
                                    {
                                        PaymentLink = Signatures[0];
                                    }
                                    else if (i == 1)
                                    {
                                        id = Signatures[1];
                                    }
                                    else if (i == 2)
                                    {
                                        TransactionID = Signatures[2];
                                    }
                                }
                                PackageDetailsModelV2 objPackageDetailsModel = new PackageDetailsModelV2();
                                objPackageDetailsModel.SubCategoryName = UhDB.SubCategories.Where(x => x.catsubID == customer.catsubID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                                objPackageDetailsModel.AreaName = UhDB.PropertyAreas.Where(x => x.propaID == customer.propaID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                                objPackageDetailsModel.InVoice = "# INV- " + customer.InVoice;
                                objPackageDetailsModel.PropName = UhDB.Ventures.Where(x => x.vID == customer.vID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                                objPackageDetailsModel.StartDate = Convert.ToDateTime(customer.StartDate).ToString("dd/MM/yyyy");
                                objPackageDetailsModel.StartTime = customer.StartTime;
                                List<SubServicesInvoiceDetails> ServiceSubCategory = new List<SubServicesInvoiceDetails>();
                                int TotalQuantity = 0;
                                double? TotalSelectedPrice = null;
                                foreach (var service in customer.ServiceSubCategory)
                                {
                                    int? servcatID = service.servcatID;
                                    int? servcatsubID = service.servsubcatID;
                                    string ServiceOptionName = UhDB.ServiceCategories.Where(x => x.servcatID == servcatID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                                    string ServiceSubCategoryName = UhDB.ServiceSubCategories.Where(x => x.servsubcatID == servcatsubID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                                    string Quantity = service.Quantity.ToString();
                                    TotalQuantity = TotalQuantity + (int)service.Quantity;
                                    string Price = service.EachServiceprice.ToString();
                                    string TotalPrice = service.TotalPrice.ToString();
                                    TotalSelectedPrice = service.TotalPrice;
                                    ServiceSubCategory.Add(new SubServicesInvoiceDetails { ServiceSubCategory = ServiceSubCategoryName, ServiceOption = ServiceOptionName, Quantity = Quantity, Price = Price, TotalPrice = TotalPrice });

                                }
                                objPackageDetailsModel.ServiceSubCategory = ServiceSubCategory;
                                string pdfBase64 = CreateInvoiceV2(objPackageDetailsModel);
                                objGeneralDB.SendEmailWithAttachmentForBodyAttachment(pdfBase64, customer.Email, "Invoice", PaymentLink);

                                CustomerTransaction objCustomerTransaction = new CustomerTransaction();
                                objCustomerTransaction.cuID = cuID;
                                objCustomerTransaction.custODID = cuODID;
                                objCustomerTransaction.Quantity = TotalQuantity;
                                objCustomerTransaction.TotalPrice = TotalSelectedPrice;
                                objCustomerTransaction.TransactionID = TransactionID;
                                objCustomerTransaction.PayementID = id;
                                objCustomerTransaction.IsActive = customer.IsActive;
                                objCustomerTransaction.IsDelete = customer.IsDelete;
                                objCustomerTransaction.CreatedBy = customer.CreatedBy;
                                objCustomerTransaction.CreatedOn = customer.CreatedOn;
                                UhDB.CustomerTransactions.Add(objCustomerTransaction);
                                UhDB.SaveChanges();

                            }
                            else
                            {
                                if (customer.Packages != null && customer.Packages.IsCustomDays == null && customer.Packages.IsCustomSelectDate == null && customer.Packages.IsCustomTime == null)
                                {
                                    PaymentRequest objPaymentRequest = new PaymentRequest();
                                    objPaymentRequest.FirstName = customer.Name;
                                    objPaymentRequest.LastName = customer.Name;
                                    objPaymentRequest.Email = customer.Email;
                                    objPaymentRequest.Phone = customer.Mobile;
                                    objPaymentRequest.Street = UhDB.PropertyAreas.Where(x => x.propaID == customer.propaID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                                    objPaymentRequest.Amount = customer.Amount.ToString();
                                    objPaymentRequest.City = "Doha";
                                    objPaymentRequest.State = "DL";
                                    objPaymentRequest.PostalCode = "110015";
                                    objPaymentRequest.Country = "QR";
                                    objPaymentRequest.Custom1 = cuID.ToString();
                                    int CountTransactionID = UhDB.CustomerTransactions.Where(x => x.IsActive == true && x.IsDelete == false && x.Customer.uID == 1).Count();
                                    if (CountTransactionID == 0)
                                    {
                                        objPaymentRequest.TransactionId = "1";
                                    }
                                    else
                                    {
                                        string TransID = UhDB.CustomerTransactions.Where(x => x.IsActive == true && x.IsDelete == false && x.Customer.uID == 1).OrderByDescending(x => x.custTrasID).FirstOrDefault().TransactionID;
                                        objPaymentRequest.TransactionId = (Convert.ToInt32(TransID) + 1).ToString();
                                    }


                                    List<string> Signatures = CalculateSignature(objPaymentRequest);
                                    string id = null, TransactionID = null;
                                    for (var i = 0; i < 3; i++)
                                    {
                                        if (i == 0)
                                        {
                                            PaymentLink = Signatures[0];
                                        }
                                        else if (i == 1)
                                        {
                                            id = Signatures[1];
                                        }
                                        else if (i == 2)
                                        {
                                            TransactionID = Signatures[2];
                                        }
                                    }
                                    PackageDetailsModel objPackageDetailsModel = new PackageDetailsModel();
                                    if (customer.catID == 2)
                                    {
                                        objPackageDetailsModel.SubCategoryName = UhDB.MainCategories.Where(x => x.catID == customer.catID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                                    }
                                    else
                                    {
                                        objPackageDetailsModel.SubCategoryName = UhDB.SubCategories.Where(x => x.catsubID == customer.catsubID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                                    }
                                    objPackageDetailsModel.AreaName = UhDB.PropertyAreas.Where(x => x.propaID == customer.propaID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                                    objPackageDetailsModel.InVoice = "# INV- " + customer.InVoice;
                                    objPackageDetailsModel.PackageName = UhDB.Packages.Where(x => x.packID == customer.Packages.packID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                                    objPackageDetailsModel.Price = customer.Packages.EachServiceprice.ToString();
                                    objPackageDetailsModel.PropName = UhDB.Ventures.Where(x => x.vID == customer.vID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                                    if (customer.catID == 2)
                                    {
                                        objPackageDetailsModel.resdName = "N/A";
                                    }
                                    else
                                    {
                                        objPackageDetailsModel.resdName = UhDB.PropertyResidenceTypes.Where(x => x.proprestID == customer.proprestID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                                    }
                                    if (customer.monthlyCount != null && customer.monthlyCount != 0)
                                    {
                                        objPackageDetailsModel.NoOfMonths = UhDB.CustomerRenewalMonths.Where(x => x.custrmID == customer.monthlyCount && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                                    }
                                    else
                                    {
                                        objPackageDetailsModel.NoOfMonths = "N/A";
                                    }
                                    objPackageDetailsModel.Price = customer.Price;
                                    objPackageDetailsModel.TotalPrice = customer.Amount;

                                    string pdfBase64 = CreateInvoice(objPackageDetailsModel);
                                    objGeneralDB.SendEmailWithAttachmentForBodyAttachment(pdfBase64, customer.Email, "Invoice", PaymentLink);

                                    CustomerTransaction objCustomerTransaction = new CustomerTransaction();
                                    objCustomerTransaction.cuID = cuID;
                                    objCustomerTransaction.custODID = cuODID;
                                    objCustomerTransaction.Quantity = customer.TotalNoOfService;
                                    objCustomerTransaction.TotalPrice = Convert.ToDouble(customer.Amount);
                                    objCustomerTransaction.TransactionID = TransactionID;
                                    objCustomerTransaction.PayementID = id;
                                    objCustomerTransaction.IsActive = customer.IsActive;
                                    objCustomerTransaction.IsDelete = customer.IsDelete;
                                    objCustomerTransaction.CreatedBy = customer.CreatedBy;
                                    objCustomerTransaction.CreatedOn = customer.CreatedOn;
                                    UhDB.CustomerTransactions.Add(objCustomerTransaction);
                                    UhDB.SaveChanges();

                                }
                            }

                            int CountTempID = UhDB.CustomerTimeBlocks.Where(x => x.MobileNo == customer.Mobile && EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(DateTime.Now) && x.IsActive == true && x.IsDelete == false).Count();
                            if (CountTempID != 0)
                            {
                                var objCustomerTimeBlocks = UhDB.CustomerTimeBlocks.Where(x => x.MobileNo == customer.Mobile && EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(DateTime.Now) && x.IsActive == true && x.IsDelete == false).ToList();
                                foreach (var objCustomerTimeBlock in objCustomerTimeBlocks)
                                {
                                    var objUpdateCustomerTimeBlock = UhDB.CustomerTimeBlocks.Where(x => x.custTBID == objCustomerTimeBlock.custTBID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                    objUpdateCustomerTimeBlock.IsActive = false;
                                    objUpdateCustomerTimeBlock.IsDelete = true;
                                    objUpdateCustomerTimeBlock.UpdatedBy = customer.CreatedBy;
                                    objUpdateCustomerTimeBlock.UpdatedOn = DateTime.Now;
                                    UhDB.SaveChanges();
                                }
                            }
                            //string WelcomeBody = EmailBodyWelcome(customer.Name);
                            //objGeneralDB.SentEmailFromAmazon(customer.Email, WelcomeBody, "Welcome to Urban Hospitality Service", customer.Name);
                            string PasswordBody = EmailBodyForSendPassword(customer.Email, Password);
                            objGeneralDB.SentEmailFromAmazon(customer.Email, PasswordBody, "Login Credentials for Urban Hospitality Service", customer.Name);

                            if (customer.Remarks != null)
                            {
                                CustomerAlert objCustomerAlert = new CustomerAlert();
                                objCustomerAlert.custID = cuID;
                                objCustomerAlert.custATID = 4;
                                objCustomerAlert.vID = customer.vID;
                                objCustomerAlert.catID = customer.catID;
                                objCustomerAlert.catsubID = customer.catsubID;
                                objCustomerAlert.Message = customer.Remarks;
                                objCustomerAlert.IsActive = customer.IsActive;
                                objCustomerAlert.IsDelete = customer.IsDelete;
                                objCustomerAlert.CreatedBy = customer.CreatedBy;
                                objCustomerAlert.CreatedOn = customer.CreatedOn;
                                UhDB.CustomerAlerts.Add(objCustomerAlert);
                                UhDB.SaveChanges();
                            }

                            trans.Commit();
                            result.Add(cuID.ToString());
                            result.Add(cuODID.ToString());
                            result.Add(CustomerID.ToString());
                            result.Add(TempTaskNo.ToString());
                            result.Add(PaymentLink);
                        }
                        else
                        {
                            trans.Rollback();
                            result.Add("-2");

                        }
                    }
                    catch (Exception ex)
                    {

                        trans.Rollback();
                        result.Add("-2");
                    }
                }
            }

            return result;
        }

        public IEnumerable<GetCustomerModelV2> GetCustomers(int? uID)
        {

            List<GetCustomerModelV2> result = new List<GetCustomerModelV2>();
            var objCustomerOfficalDetails = UhDB.CustomerOfficalDetails.Where(x => x.Customer.uID == uID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objCustomerOfficalDetail in objCustomerOfficalDetails)
            {
                int? custID = objCustomerOfficalDetail.custID;
                int? custODID = objCustomerOfficalDetail.custODID;
                var objCustomerTimeLine = UhDB.CustomerTimelines.Where(x => x.custID == custID && x.custODID == custODID).FirstOrDefault();
                result.Add(new GetCustomerModelV2
                {
                    Name = objCustomerOfficalDetail.Customer.Name,
                    Email = objCustomerOfficalDetail.Customer.Email,
                    Mobile = objCustomerOfficalDetail.Customer.Mobile,
                    WhatsAppNo = objCustomerOfficalDetail.Customer.WhatsAppNo,
                    AlternativeNo = objCustomerOfficalDetail.Customer.AlternativeNo,
                    Saluation = objCustomerOfficalDetail.Customer.Salutaion,
                    WorkStatus = objCustomerOfficalDetail.StatusOfWork,
                    MainCategory = objCustomerOfficalDetail.MainCategory.Name,
                    SubCategory = objCustomerOfficalDetail.catsubID != null ? objCustomerOfficalDetail.SubCategory.Name : "N/A",
                    Area = objCustomerOfficalDetail.PropertyArea.Name,
                    PropertyName = objCustomerOfficalDetail.propType == 1 ? objCustomerOfficalDetail.Venture.Name :
                                   UhDB.CustomerOtherProperties.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().TowerName,
                    PropertyResidencyType = objCustomerOfficalDetail.proprestID != null ? objCustomerOfficalDetail.PropertyResidenceType.Name : null,
                    Remarks = objCustomerOfficalDetail.Remarks,
                    ApartmentName = objCustomerOfficalDetail.AppartmentNumber,
                    carstID = objCustomerOfficalDetail.carstID,
                    cartID = objCustomerOfficalDetail.cartID,
                    IsCarWash = objCustomerOfficalDetail.IsCarWash,
                    carTRID = objCustomerOfficalDetail.carTRID,
                    CarWashTimes = objCustomerOfficalDetail.carTRID != null ? objCustomerOfficalDetail.CarWashTimeRange.Name + " " + objCustomerOfficalDetail.CarWashTimeRange.Timing : null,
                    CarType = objCustomerOfficalDetail.CarType != null ? objCustomerOfficalDetail.CarType.Name : "N/A",
                    CarServiceType = objCustomerOfficalDetail.CarServiceType != null ? objCustomerOfficalDetail.CarServiceType.Name : "N/A",
                    ServiceDays = objCustomerOfficalDetail.BuldleDays,
                    EndDate = Convert.ToDateTime(UhDB.CustomerTimelines.Where(x => x.custID == custID && x.custODID == custODID).OrderByDescending(j => j.custTDID).FirstOrDefault().StartDate).ToString("MM/dd/yyyy"),
                    CustomerType = objCustomerOfficalDetail.Customer.CustomerType != null ? objCustomerOfficalDetail.Customer.CustomerType1.Name : null,
                    StartTime = objCustomerTimeLine.StartTime,
                    EndTime = objCustomerTimeLine.EndTime,
                    TeamName = objCustomerTimeLine.teamID != null ? objCustomerTimeLine.Team.Name : null,
                    staffName = objCustomerOfficalDetail.stfID != null ? objCustomerOfficalDetail.Staff.Name : null,
                    TaskNo = objCustomerTimeLine.TaskNo,
                    NoOfMonths = objCustomerOfficalDetail.NoOfMonths != null ? objCustomerOfficalDetail.CustomerRenewalMonth.Name : "N/A",
                    CreatedOn = Convert.ToDateTime(objCustomerTimeLine.CreatedOn).ToString("MM/dd/yyyy"),
                    CustomerID = objCustomerOfficalDetail.Customer.CustomerID,
                    ServiceStatus = objCustomerOfficalDetail.ServiceStatus == true ? "Active" : objCustomerOfficalDetail.ServiceStatus == false ? "InActive" : "Pending",
                    stfID = objCustomerOfficalDetail.stfID,
                    teamID = objCustomerTimeLine.teamID,
                    propaID = objCustomerOfficalDetail.propaID,
                    vID = objCustomerOfficalDetail.vID,
                    proprestID = objCustomerOfficalDetail.proprestID,
                    propType = objCustomerOfficalDetail.propType,
                    custOPID = objCustomerOfficalDetail.propType == 2 ? (UhDB.CustomerOtherProperties.Where(x => x.custID == objCustomerOfficalDetail.custID && x.custODID == objCustomerOfficalDetail.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().custODID) : null,
                    cuID = objCustomerOfficalDetail.custID,
                    cuODID = objCustomerOfficalDetail.custODID,
                    catID = objCustomerOfficalDetail.catID,
                    catsubID = objCustomerOfficalDetail.catsubID,
                    OtherLocation = objCustomerOfficalDetail.propType == 2 ? UhDB.CustomerOtherProperties.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                       .Select(u => new GetOtherLocationModel
                                       {
                                           TowerName = u.TowerName,
                                           BuildingName = u.BuildingName,
                                           StreetNumber = u.StreetNumber,
                                           ZoneNumber = u.ZoneNumber,
                                           Loacation = u.Loacation,
                                           LocationLink = u.LocationLink
                                       }).FirstOrDefault() : null,
                    GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                    .Select(r => new GetCustomerAvailabilityModel
                                                    {
                                                        custSCID = r.custSCID,
                                                        Availability = r.Availability,
                                                        KeyCollection = r.KeyCollection,
                                                        AccessProperty = r.AccessProperty,
                                                        ReceptionDate = r.ReceptionDate
                                                    }).FirstOrDefault(),
                    PaymentStatus = UhDB.CustomerTransactions.Where(x => x.cuID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                      .Select(c => new GetCustomerPaymentStatus { PayementID = c.PayementID, PaymentStatus = c.PaymentStatus, Price = c.Price, TotalPrice = c.TotalPrice, TransactionID = c.TransactionID, Quantity = c.Quantity, custTrasID = c.custTrasID }).FirstOrDefault(),
                    CustomDates = UhDB.CustomerCustomDateTimes.Where(x => x.cuID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                    .Select(b => new GetCustomerCustomeDate { CustomDays = b.CustomDays, CustomEndTime = b.CustomEndTime, CustomStartDate = b.CustomStartDate != null ? Convert.ToDateTime(b.CustomStartDate).ToString("dd/MM/yyyy") : null, CustomStartTime = b.CustomStartTime, custDTID = b.custDTID }).FirstOrDefault(),
                    GetServices = UhDB.CustomerSpecializedCleanings.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable().
                                       Select(t => new GetServiceSubCategoryModel
                                       {
                                           custSCID = t.custSCID,
                                           servcatID = t.servcatID,
                                           servsubcatID = t.servsubcatID,
                                           ServiceCategoryName = t.ServiceCategory.Name,
                                           ServiceSubCategoryName = t.ServiceSubCategory.Name,
                                           Quantity = t.Quantity
                                       }).ToList(),
                    PackageName = objCustomerTimeLine.packID != null ? objCustomerTimeLine.Package.Name : null,
                    Price = objCustomerTimeLine.parkID != null ? objCustomerTimeLine.Pricing.Price.ToString() : null,
                    Duration = objCustomerTimeLine.parkID != null ? objCustomerTimeLine.Pricing.Duration : null,
                    Measurement = objCustomerTimeLine.parkID != null ? objCustomerTimeLine.Pricing.TimeMeasurement : null,
                    packID = objCustomerTimeLine.packID,
                    parkID = objCustomerTimeLine.parkID,
                    Date = Convert.ToDateTime(objCustomerTimeLine.StartDate).ToString("MM/dd/yyyy"),
                    WeeklyCounts = objCustomerTimeLine.packID != null ? objCustomerTimeLine.Package.RecursiveTime.ToString() : null,
                    CustomCarDetails = objCustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                       .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,
                    Files = UhDB.Files.Where(x => x.cuiD == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                            UhDB.Files.Where(x => x.cuiD == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                            .Select(s => new GetFileDetails
                            {
                                Name = s.Filename,
                                ContentType = s.FileContentType,
                                Size = s.FileSize,
                                Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName
                            }).ToList() : null,


                });
            }
            return result;
        }

        public IEnumerable<GetCustomerModelV8> GetCustomersTodaysForAdmin(int? uID)
        {

            List<GetCustomerModelV8> result = new List<GetCustomerModelV8>();
            DateTime TodaysDate = DateTime.Now;
            int CountCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.Customer.uID == uID && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(TodaysDate) && x.IsActive == true && x.IsDelete == false).Count();
            if (CountCustomerTimelines!=0) 
            {
                var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.Customer.uID == uID && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(TodaysDate) && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                           .Select(p => new
                                           {
                                               CustomerID = p.Customer.CustomerID,
                                               Name = p.Customer.Name,
                                               Email = p.Customer.Email,
                                               Mobile = p.Customer.Mobile,
                                               CustomerType = p.Customer.CustomerType != null ? p.Customer.CustomerType1.Name : null,
                                               ServiceDate = p.StartDate != null ? Convert.ToDateTime(p.StartDate).ToString("MM/dd/yyyy") : null,
                                               StartTime = p.StartTime,
                                               EndTime = p.EndTime,
                                               ApartmentName = p.CustomerOfficalDetail.AppartmentNumber,
                                               PropertyResidencyType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : null,
                                               TeamName = p.teamID != null ? p.Team.Name : "N/A",
                                               PaymentStatus = UhDB.CustomerTransactions.Where(x => x.cuID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                                               (UhDB.CustomerTransactions.Where(x => x.cuID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().PaymentStatus).ToString() : "N/A",
                                               Duration = p.parkID != null ? p.Pricing.Duration : null,
                                               Address = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.PropertyArea.Name + "," + p.CustomerOfficalDetail.SubArea.Name + ","
                                                          + p.CustomerOfficalDetail.Venture.Name : p.CustomerOfficalDetail.PropertyArea.Name + "," + p.CustomerOfficalDetail.SubArea.Name + ","
                                                          + UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().TowerName,
                                               proprestID = p.CustomerOfficalDetail.proprestID,
                                               propType = p.CustomerOfficalDetail.propType,
                                               teamID = p.teamID,
                                               propaID = p.CustomerOfficalDetail.propaID,
                                               vID = p.CustomerOfficalDetail.vID,
                                               cuID = p.CustomerOfficalDetail.custID,
                                               cuODID = p.CustomerOfficalDetail.custODID,
                                               catID = p.CustomerOfficalDetail.catID,
                                               catsubID = p.CustomerOfficalDetail.catsubID,
                                               ServiceType = p.CustomerOfficalDetail.catsubID!=null? p.CustomerOfficalDetail.SubCategory.Name:"N/A",
                                               Measurement = p.parkID != null ? p.Pricing.TimeMeasurement : "N/A",
                                               ServiceStatus = p.CustomerOfficalDetail.ServiceStatus == true ? "Active" : p.CustomerOfficalDetail.ServiceStatus == false ? "InActive" : "Pending",
                                               packID=p.packID,
                                               parkID=p.parkID
                                           }).ToList();

                foreach (var objCustomerTimeline in objCustomerTimelines)
                {
                    if (objCustomerTimeline.catsubID == 3)
                    {
                        int? custID = objCustomerTimeline.cuID;
                        int? custODID = objCustomerTimeline.cuODID;
                        var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).ToList();
                        foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
                        {
                            result.Add(new GetCustomerModelV8
                            {
                                CustomerID = objCustomerTimeline.CustomerID,
                                CustomerType = objCustomerTimeline.CustomerType,
                                Name = objCustomerTimeline.Name,
                                Email = objCustomerTimeline.Email,
                                Mobile = objCustomerTimeline.Mobile,
                                ServiceDate = objCustomerTimeline.ServiceDate,
                                StartTime = objCustomerTimeline.StartTime,
                                EndTime = objCustomerTimeline.EndTime,
                                ApartmentName = objCustomerTimeline.ApartmentName,
                                PropertyResidencyType = objCustomerTimeline.PropertyResidencyType,
                                TeamName = objCustomerTimeline.TeamName,
                                PaymentStatus = objCustomerTimeline.PaymentStatus,
                                Duration = objCustomerTimeline.Duration,
                                Address = objCustomerTimeline.Address,
                                ServiceType = objCustomerSpecializedCleaning.ServiceCategory.Name,
                                proprestID = objCustomerTimeline.proprestID,
                                propType = objCustomerTimeline.propType,
                                teamID = objCustomerTimeline.teamID,
                                propaID = objCustomerTimeline.propaID,
                                vID = objCustomerTimeline.vID,
                                cuID = objCustomerTimeline.cuID,
                                cuODID = objCustomerTimeline.cuODID,
                                catID = objCustomerTimeline.catID,
                                catsubID = objCustomerTimeline.catsubID,
                                ServiceStatus = objCustomerTimeline.ServiceStatus,
                                Measurement = objCustomerTimeline.Measurement,
                                packID=objCustomerTimeline.packID,
                                parkID= objCustomerTimeline.parkID,
                            });
                        }
                    }
                    else
                    {
                        result.Add(new GetCustomerModelV8
                        {
                            CustomerID = objCustomerTimeline.CustomerID,
                            CustomerType = objCustomerTimeline.CustomerType,
                            Name = objCustomerTimeline.Name,
                            Email = objCustomerTimeline.Email,
                            Mobile = objCustomerTimeline.Mobile,
                            ServiceDate = objCustomerTimeline.ServiceDate,
                            StartTime = objCustomerTimeline.StartTime,
                            EndTime = objCustomerTimeline.EndTime,
                            ApartmentName = objCustomerTimeline.ApartmentName,
                            PropertyResidencyType = objCustomerTimeline.PropertyResidencyType,
                            TeamName = objCustomerTimeline.TeamName,
                            PaymentStatus = objCustomerTimeline.PaymentStatus,
                            Duration = objCustomerTimeline.Duration,
                            Address = objCustomerTimeline.Address,
                            ServiceType = objCustomerTimeline.ServiceType,
                            proprestID = objCustomerTimeline.proprestID,
                            propType = objCustomerTimeline.propType,
                            teamID = objCustomerTimeline.teamID,
                            propaID = objCustomerTimeline.propaID,
                            vID = objCustomerTimeline.vID,
                            cuID = objCustomerTimeline.cuID,
                            cuODID = objCustomerTimeline.cuODID,
                            catID = objCustomerTimeline.catID,
                            catsubID = objCustomerTimeline.catsubID,
                            ServiceStatus = objCustomerTimeline.ServiceStatus,
                            Measurement = objCustomerTimeline.Measurement,
                            packID = objCustomerTimeline.packID,
                            parkID = objCustomerTimeline.parkID
                        });
                    }
                }
            }

            return result;
        }

        public IEnumerable<GetCustomerModelV8> GetCustomersByDateForAdmin(int? uID, DateTime Date)
        {

            List<GetCustomerModelV8> result = new List<GetCustomerModelV8>();
            int CountCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.Customer.uID == uID && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(Date) && x.IsActive == true && x.IsDelete == false).Count();
            if (CountCustomerTimelines!=0) 
            {
                var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.Customer.uID == uID && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(Date) && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                           .Select(p => new
                                           {
                                               CustomerID = p.Customer.CustomerID,
                                               Name = p.Customer.Name,
                                               Email = p.Customer.Email,
                                               Mobile = p.Customer.Mobile,
                                               CustomerType = p.Customer.CustomerType != null ? p.Customer.CustomerType1.Name : null,
                                               ServiceDate = p.StartDate != null ? Convert.ToDateTime(p.StartDate).ToString("MM/dd/yyyy") : null,
                                               StartTime = p.StartTime,
                                               EndTime = p.EndTime,
                                               ApartmentName = p.CustomerOfficalDetail.AppartmentNumber,
                                               PropertyResidencyType = p.CustomerOfficalDetail.proprestID != null ? p.CustomerOfficalDetail.PropertyResidenceType.Name : null,
                                               TeamName = p.teamID != null ? p.Team.Name : "N/A",
                                               PaymentStatus = UhDB.CustomerTransactions.Where(x => x.cuID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                                               (UhDB.CustomerTransactions.Where(x => x.cuID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().PaymentStatus).ToString() : "N/A",
                                               Duration = p.parkID != null ? p.Pricing.Duration : null,
                                               Address = p.CustomerOfficalDetail.propType == 1 ? p.CustomerOfficalDetail.PropertyArea.Name + "," + p.CustomerOfficalDetail.SubArea.Name + ","
                                                          + p.CustomerOfficalDetail.Venture.Name : p.CustomerOfficalDetail.PropertyArea.Name + "," + p.CustomerOfficalDetail.SubArea.Name + ","
                                                          + UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().TowerName,
                                               proprestID = p.CustomerOfficalDetail.proprestID,
                                               propType = p.CustomerOfficalDetail.propType,
                                               teamID = p.teamID,
                                               propaID = p.CustomerOfficalDetail.propaID,
                                               vID = p.CustomerOfficalDetail.vID,
                                               cuID = p.CustomerOfficalDetail.custID,
                                               cuODID = p.CustomerOfficalDetail.custODID,
                                               catID = p.CustomerOfficalDetail.catID,
                                               catsubID = p.CustomerOfficalDetail.catsubID,
                                               ServiceType = p.CustomerOfficalDetail.catsubID!=null? p.CustomerOfficalDetail.SubCategory.Name:"N/A",
                                               Measurement = p.parkID != null ? p.Pricing.TimeMeasurement : "N/A",
                                               ServiceStatus = p.CustomerOfficalDetail.ServiceStatus == true ? "Active" : p.CustomerOfficalDetail.ServiceStatus == false ? "InActive" : "Pending",
                                               parkID=p.parkID,
                                               packID=p.packID,
                                           }).ToList();

                foreach (var objCustomerTimeline in objCustomerTimelines)
                {
                    if (objCustomerTimeline.catsubID == 3)
                    {
                        int? custID = objCustomerTimeline.cuID;
                        int? custODID = objCustomerTimeline.cuODID;
                        var objCustomerSpecializedCleanings = UhDB.CustomerSpecializedCleanings.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).ToList();
                        foreach (var objCustomerSpecializedCleaning in objCustomerSpecializedCleanings)
                        {
                            result.Add(new GetCustomerModelV8
                            {
                                CustomerID = objCustomerTimeline.CustomerID,
                                CustomerType = objCustomerTimeline.CustomerType,
                                Name = objCustomerTimeline.Name,
                                Email = objCustomerTimeline.Email,
                                Mobile = objCustomerTimeline.Mobile,
                                ServiceDate = objCustomerTimeline.ServiceDate,
                                StartTime = objCustomerTimeline.StartTime,
                                EndTime = objCustomerTimeline.EndTime,
                                ApartmentName = objCustomerTimeline.ApartmentName,
                                PropertyResidencyType = objCustomerTimeline.PropertyResidencyType,
                                TeamName = objCustomerTimeline.TeamName,
                                PaymentStatus = objCustomerTimeline.PaymentStatus,
                                Duration = objCustomerTimeline.Duration,
                                Address = objCustomerTimeline.Address,
                                ServiceType = objCustomerSpecializedCleaning.ServiceCategory.Name,
                                proprestID = objCustomerTimeline.proprestID,
                                propType = objCustomerTimeline.propType,
                                teamID = objCustomerTimeline.teamID,
                                propaID = objCustomerTimeline.propaID,
                                vID = objCustomerTimeline.vID,
                                cuID = objCustomerTimeline.cuID,
                                cuODID = objCustomerTimeline.cuODID,
                                catID = objCustomerTimeline.catID,
                                catsubID = objCustomerTimeline.catsubID,
                                Measurement = objCustomerTimeline.Measurement,
                                ServiceStatus = objCustomerTimeline.ServiceStatus,
                                packID=objCustomerTimeline.packID,
                                parkID=objCustomerTimeline.parkID
                            });
                        }
                    }
                    else
                    {
                        result.Add(new GetCustomerModelV8
                        {
                            CustomerID = objCustomerTimeline.CustomerID,
                            CustomerType = objCustomerTimeline.CustomerType,
                            Name = objCustomerTimeline.Name,
                            Email = objCustomerTimeline.Email,
                            Mobile = objCustomerTimeline.Mobile,
                            ServiceDate = objCustomerTimeline.ServiceDate,
                            StartTime = objCustomerTimeline.StartTime,
                            EndTime = objCustomerTimeline.EndTime,
                            ApartmentName = objCustomerTimeline.ApartmentName,
                            PropertyResidencyType = objCustomerTimeline.PropertyResidencyType,
                            TeamName = objCustomerTimeline.TeamName,
                            PaymentStatus = objCustomerTimeline.PaymentStatus,
                            Duration = objCustomerTimeline.Duration,
                            Address = objCustomerTimeline.Address,
                            ServiceType = objCustomerTimeline.ServiceType,
                            proprestID = objCustomerTimeline.proprestID,
                            propType = objCustomerTimeline.propType,
                            teamID = objCustomerTimeline.teamID,
                            propaID = objCustomerTimeline.propaID,
                            vID = objCustomerTimeline.vID,
                            cuID = objCustomerTimeline.cuID,
                            cuODID = objCustomerTimeline.cuODID,
                            catID = objCustomerTimeline.catID,
                            catsubID = objCustomerTimeline.catsubID,
                            Measurement = objCustomerTimeline.Measurement,
                            ServiceStatus = objCustomerTimeline.ServiceStatus,
                            packID = objCustomerTimeline.packID,
                            parkID = objCustomerTimeline.parkID
                        });
                    }
                }
            }

            return result;
        }

        public IEnumerable<GetCustomerModelV2> GetCustomerDetail(int? custID, int? custODID)
        {

            List<GetCustomerModelV2> result = new List<GetCustomerModelV2>();
            var objCustomerOfficalDetail = UhDB.CustomerOfficalDetails.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
            var objCustomerTimeLine = UhDB.CustomerTimelines.Where(x => x.custID == custID && x.custODID == custODID).FirstOrDefault();
            result.Add(new GetCustomerModelV2
            {
                Name = objCustomerOfficalDetail.Customer.Name,
                Email = objCustomerOfficalDetail.Customer.Email,
                CustomerType = objCustomerOfficalDetail.Customer.CustomerType != null ? objCustomerOfficalDetail.Customer.CustomerType1.Name : null,
                CustomerID = objCustomerOfficalDetail.Customer.CustomerID,
                Mobile = objCustomerOfficalDetail.Customer.Mobile,
                WhatsAppNo = objCustomerOfficalDetail.Customer.WhatsAppNo,
                AlternativeNo = objCustomerOfficalDetail.Customer.AlternativeNo,
                Saluation = objCustomerOfficalDetail.Customer.Salutaion,
                WorkStatus = objCustomerOfficalDetail.StatusOfWork,
                MainCategory = objCustomerOfficalDetail.MainCategory.Name,
                SubCategory = objCustomerOfficalDetail.catsubID != null ? objCustomerOfficalDetail.SubCategory.Name : "N/A",
                PropertyResidencyType = objCustomerOfficalDetail.proprestID != null ? objCustomerOfficalDetail.PropertyResidenceType.Name : null,
                Remarks = objCustomerOfficalDetail.Remarks,
                carstID = objCustomerOfficalDetail.carstID,
                cartID = objCustomerOfficalDetail.cartID,
                IsCarWash = objCustomerOfficalDetail.IsCarWash,
                carTRID = objCustomerOfficalDetail.carTRID,
                CarWashTimes = objCustomerOfficalDetail.carTRID != null ? objCustomerOfficalDetail.CarWashTimeRange.Name + " " + objCustomerOfficalDetail.CarWashTimeRange.Timing : null,
                CarType = objCustomerOfficalDetail.CarType != null ? objCustomerOfficalDetail.CarType.Name : "N/A",
                CarServiceType = objCustomerOfficalDetail.CarServiceType != null ? objCustomerOfficalDetail.CarServiceType.Name : "N/A",
                ServiceDays = objCustomerOfficalDetail.BuldleDays,
                EndDate = Convert.ToDateTime(UhDB.CustomerTimelines.Where(x => x.custID == custID && x.custODID == custODID).OrderByDescending(j => j.custTDID).FirstOrDefault().StartDate).ToString("MM/dd/yyyy"),
                StartTime = objCustomerTimeLine.StartTime,
                EndTime = objCustomerTimeLine.EndTime,
                NoOfMonths = objCustomerOfficalDetail.NoOfMonths != null ? objCustomerOfficalDetail.CustomerRenewalMonth.Name : "N/A",
                CreatedOn = Convert.ToDateTime(objCustomerTimeLine.CreatedOn).ToString("MM/dd/yyyy"),
                ServiceStatus = objCustomerOfficalDetail.ServiceStatus == true ? "Active" : objCustomerOfficalDetail.ServiceStatus == false ? "InActive" : "Pending",
                stfID = objCustomerOfficalDetail.stfID,
                teamID = objCustomerTimeLine.teamID,
                propaID = objCustomerOfficalDetail.propaID,
                vID = objCustomerOfficalDetail.vID,
                proprestID = objCustomerOfficalDetail.proprestID,
                propType = objCustomerOfficalDetail.propType,
                custOPID = objCustomerOfficalDetail.propType == 2 ? (UhDB.CustomerOtherProperties.Where(x => x.custID == objCustomerOfficalDetail.custID && x.custODID == objCustomerOfficalDetail.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().custODID) : null,
                cuID = objCustomerOfficalDetail.custID,
                cuODID = objCustomerOfficalDetail.custODID,
                catID = objCustomerOfficalDetail.catID,
                catsubID = objCustomerOfficalDetail.catsubID,
                OtherLocation = objCustomerOfficalDetail.propType == 2 ? UhDB.CustomerOtherProperties.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                   .Select(u => new GetOtherLocationModel
                                   {
                                       TowerName = u.TowerName,
                                       BuildingName = u.BuildingName,
                                       StreetNumber = u.StreetNumber,
                                       ZoneNumber = u.ZoneNumber,
                                       Loacation = u.Loacation,
                                       LocationLink = u.LocationLink
                                   }).FirstOrDefault() : null,
                GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                .Select(r => new GetCustomerAvailabilityModel
                                                {
                                                    custSCID = r.custSCID,
                                                    Availability = r.Availability,
                                                    KeyCollection = r.KeyCollection,
                                                    AccessProperty = r.AccessProperty,
                                                    ReceptionDate = r.ReceptionDate
                                                }).FirstOrDefault(),
                PackageName = objCustomerTimeLine.packID != null ? objCustomerTimeLine.Package.Name : null,
                Price = objCustomerTimeLine.parkID != null ? objCustomerTimeLine.Pricing.Price.ToString() : null,
                Duration = objCustomerTimeLine.parkID != null ? objCustomerTimeLine.Pricing.Duration : null,
                Measurement = objCustomerTimeLine.parkID != null ? objCustomerTimeLine.Pricing.TimeMeasurement : null,
                packID = objCustomerTimeLine.packID,
                parkID = objCustomerTimeLine.parkID,
                WeeklyCounts = objCustomerTimeLine.packID != null ? objCustomerTimeLine.Package.RecursiveTime.ToString() : null,
                CustomCarDetails = objCustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                   .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,
                Files = UhDB.Files.Where(x => x.cuiD == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                        UhDB.Files.Where(x => x.cuiD == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                        .Select(s => new GetFileDetails
                        {
                            Name = s.Filename,
                            ContentType = s.FileContentType,
                            Size = s.FileSize,
                            Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName
                        }).ToList() : null,
            });

            return result;
        }

        public IEnumerable<GetCustomerModelV2> GetCustomersByCustomerID(int? custID)
        {

            List<GetCustomerModelV2> result = new List<GetCustomerModelV2>();
            var objCustomerOfficalDetails = UhDB.CustomerOfficalDetails.Where(x => x.Customer.cuID == custID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objCustomerOfficalDetail in objCustomerOfficalDetails)
            {
                int? custODID = objCustomerOfficalDetail.custODID;
                var objCustomerTimeLine = UhDB.CustomerTimelines.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                result.Add(new GetCustomerModelV2
                {
                    Name = objCustomerOfficalDetail.Customer.Name,
                    Email = objCustomerOfficalDetail.Customer.Email,
                    Mobile = objCustomerOfficalDetail.Customer.Mobile,
                    WhatsAppNo = objCustomerOfficalDetail.Customer.WhatsAppNo,
                    AlternativeNo = objCustomerOfficalDetail.Customer.AlternativeNo,
                    Saluation = objCustomerOfficalDetail.Customer.Salutaion,
                    WorkStatus = objCustomerOfficalDetail.StatusOfWork,
                    Files = UhDB.Files.Where(x => x.cuiD == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                            UhDB.Files.Where(x => x.cuiD == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                            .Select(s => new GetFileDetails
                            {
                                Name = s.Filename,
                                ContentType = s.FileContentType,
                                Size = s.FileSize,
                                Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName
                            }).ToList() : null,
                    MainCategory = objCustomerOfficalDetail.MainCategory.Name,
                    SubCategory = objCustomerOfficalDetail.catsubID != null ? objCustomerOfficalDetail.SubCategory.Name : "N/A",
                    Area = objCustomerOfficalDetail.PropertyArea.Name,
                    PropertyName = objCustomerOfficalDetail.propType == 1 ? objCustomerOfficalDetail.Venture.Name :
                                   UhDB.CustomerOtherProperties.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().TowerName,
                    PropertyResidencyType = objCustomerOfficalDetail.proprestID != null ? objCustomerOfficalDetail.PropertyResidenceType.Name : null,
                    Remarks = objCustomerOfficalDetail.Remarks,
                    ApartmentName = objCustomerOfficalDetail.AppartmentNumber,
                    OtherLocation = objCustomerOfficalDetail.propType == 2 ? UhDB.CustomerOtherProperties.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                       .Select(u => new GetOtherLocationModel
                                       {
                                           TowerName = u.TowerName,
                                           BuildingName = u.BuildingName,
                                           StreetNumber = u.StreetNumber,
                                           ZoneNumber = u.ZoneNumber,
                                           Loacation = u.Loacation,
                                           LocationLink = u.LocationLink
                                       }).FirstOrDefault() : null,
                    GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                    .Select(r => new GetCustomerAvailabilityModel
                                                    {
                                                        custSCID = r.custSCID,
                                                        Availability = r.Availability,
                                                        KeyCollection = r.KeyCollection,
                                                        AccessProperty = r.AccessProperty,
                                                        ReceptionDate = r.ReceptionDate
                                                    }).FirstOrDefault(),
                    PaymentStatus = UhDB.CustomerTransactions.Where(x => x.cuID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                      .Select(c => new GetCustomerPaymentStatus { PayementID = c.PayementID, PaymentStatus = c.PaymentStatus, Price = c.Price, TotalPrice = c.TotalPrice, TransactionID = c.TransactionID, Quantity = c.Quantity, custTrasID = c.custTrasID }).FirstOrDefault(),
                    CustomDates = UhDB.CustomerCustomDateTimes.Where(x => x.cuID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                    .Select(b => new GetCustomerCustomeDate { CustomDays = b.CustomDays, CustomEndTime = b.CustomEndTime, CustomStartDate = b.CustomStartDate != null ? Convert.ToDateTime(b.CustomStartDate).ToString("dd/MM/yyyy") : null, CustomStartTime = b.CustomStartTime, custDTID = b.custDTID }).FirstOrDefault(),
                    GetServices = UhDB.CustomerSpecializedCleanings.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable().
                                       Select(t => new GetServiceSubCategoryModel
                                       {
                                           custSCID = t.custSCID,
                                           servcatID = t.servcatID,
                                           servsubcatID = t.servsubcatID,
                                           ServiceCategoryName = t.ServiceCategory.Name,
                                           ServiceSubCategoryName = t.ServiceSubCategory.Name,
                                           Quantity = t.Quantity
                                       }).ToList(),
                    carstID = objCustomerOfficalDetail.carstID,
                    cartID = objCustomerOfficalDetail.cartID,
                    IsCarWash = objCustomerOfficalDetail.IsCarWash,
                    CarType = objCustomerOfficalDetail.CarType != null ? objCustomerOfficalDetail.CarType.Name : "N/A",
                    CarServiceType = objCustomerOfficalDetail.CarServiceType != null ? objCustomerOfficalDetail.CarServiceType.Name : "N/A",
                    CustomCarDetails = objCustomerOfficalDetail.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                       .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,
                    PackageName = objCustomerTimeLine.packID != null ? objCustomerTimeLine.Package.Name : null,
                    Price = objCustomerTimeLine.parkID != null ? objCustomerTimeLine.Pricing.Price.ToString() : null,
                    Duration = objCustomerTimeLine.parkID != null ? objCustomerTimeLine.Pricing.Duration : null,
                    Measurement = objCustomerTimeLine.parkID != null ? objCustomerTimeLine.Pricing.TimeMeasurement : null,
                    packID = objCustomerTimeLine.packID,
                    parkID = objCustomerTimeLine.parkID,
                    Date = Convert.ToDateTime(objCustomerTimeLine.StartDate).ToString("MM/dd/yyyy"),
                    WeeklyCounts = objCustomerTimeLine.parkID != null ? objCustomerTimeLine.Package.RecursiveTime.ToString() : null,
                    ServiceDays = objCustomerOfficalDetail.BuldleDays,
                    EndDate = Convert.ToDateTime(UhDB.CustomerTimelines.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).OrderByDescending(j => j.custTDID).FirstOrDefault().StartDate).ToString("MM/dd/yyyy"),
                    CustomerType = objCustomerOfficalDetail.Customer.CustomerType != null ? objCustomerOfficalDetail.Customer.CustomerType1.Name : null,
                    StartTime = objCustomerTimeLine.StartTime,
                    EndTime = objCustomerTimeLine.EndTime,
                    TeamName = objCustomerTimeLine.teamID != null ? objCustomerTimeLine.Team.Name : null,
                    staffName = objCustomerOfficalDetail.stfID != null ? objCustomerOfficalDetail.Staff.Name : null,
                    TaskNo = objCustomerTimeLine.TaskNo,
                    NoOfMonths = objCustomerOfficalDetail.NoOfMonths != null ? objCustomerOfficalDetail.CustomerRenewalMonth.Name : "N/A",
                    CreatedOn = Convert.ToDateTime(objCustomerTimeLine.CreatedOn).ToString("MM/dd/yyyy"),
                    CustomerID = objCustomerOfficalDetail.Customer.CustomerID,
                    ServiceStatus = objCustomerOfficalDetail.ServiceStatus == true ? "Active" : objCustomerOfficalDetail.ServiceStatus == false ? "InActive" : "Pending",
                    stfID = objCustomerOfficalDetail.stfID,
                    teamID = objCustomerTimeLine.teamID,
                    propaID = objCustomerOfficalDetail.propaID,
                    vID = objCustomerOfficalDetail.vID,
                    proprestID = objCustomerOfficalDetail.proprestID,
                    propType = objCustomerOfficalDetail.propType,
                    custOPID = objCustomerOfficalDetail.propType == 2 ? (UhDB.CustomerOtherProperties.Where(x => x.custID == objCustomerOfficalDetail.custID && x.custODID == objCustomerOfficalDetail.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().custODID) : null,
                    cuID = objCustomerOfficalDetail.custID,
                    cuODID = objCustomerOfficalDetail.custODID,
                    catID = objCustomerOfficalDetail.catID,
                    catsubID = objCustomerOfficalDetail.catsubID
                });
            }



            return result;
        }

        public IEnumerable<GetCustomerModelV4> GetCustomersForTimeLineCustomerID(int? custID, int? custODID)
        {
            List<GetCustomerModelV4> result = new List<GetCustomerModelV4>();

            result = UhDB.CustomerTimelines.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select(p => new GetCustomerModelV4
                     {
                         PaymentStatus = UhDB.CustomerTransactions.Where(x => x.cuID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                         (UhDB.CustomerTransactions.Where(x => x.cuID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().PaymentStatus).ToString() : "N/A",
                         PackageName = p.packID != null ? p.Package.Name : null,
                         Price = p.parkID != null ? p.Pricing.Price.ToString() : null,
                         Duration = p.parkID != null ? p.Pricing.Duration : null,
                         StartTime = p.StartTime,
                         EndTime = p.EndTime,
                         TaskNo = p.TaskNo.ToString(),
                         ServiceDate = p.StartDate != null ? Convert.ToDateTime(p.StartDate).ToString("MM/dd/yyyy") : null,
                         Status = p.CustomerOfficalDetail.ServiceStatus == true ? "Active" : p.CustomerOfficalDetail.ServiceStatus == false ? "InActive" : "Pending",
                         WorkingStatus = p.StatusOfWork == 1 ? "Open" : p.StatusOfWork == 2 ? "Pending" : "Closed",
                         custTDID = p.custTDID,
                         TimeMeasurement = p.parkID != null ? p.Pricing.TimeMeasurement : null,
                         TeamName = p.teamID != null ? p.Team.Name : null,
                         StartDate = p.StartDate
                     }).OrderBy(x => x.StartDate).ToList();
            return result;
        }

        public IEnumerable<GetCustomerForSubCategoryModel> GetCustomersBySubCategory(int? cuID, int? catID, int? catsubID)
        {
            List<GetCustomerForSubCategoryModel> result = new List<GetCustomerForSubCategoryModel>();
            result = UhDB.CustomerOfficalDetails.Where(x => x.Customer.cuID == cuID && x.catID == catID && x.catsubID == catsubID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                    .Select(p => new GetCustomerForSubCategoryModel
                    {
                        SubCategory = p.SubCategory.Name,
                        Area = p.PropertyArea.Name,
                        PropertyName = p.propType == 1 ? p.Venture.Name :
                        UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().TowerName,
                        PropertyResidencyType = p.PropertyResidenceType.Name,
                        GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == cuID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                 .Select(r => new GetCustomerAvailabilityModel
                                 {
                                     custSCID = r.custSCID,
                                     Availability = r.Availability,
                                     KeyCollection = r.KeyCollection,
                                     AccessProperty = r.AccessProperty,
                                     ReceptionDate = r.ReceptionDate
                                 }).FirstOrDefault(),
                        Packages = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                   .Select(s => new GetPackagesForSubCategoryModel
                                   {
                                       PackageName = s.Package.Name,
                                       Price = s.Pricing.Price.ToString(),
                                       Duration = s.Pricing.Duration,
                                       Measurement = s.Pricing.TimeMeasurement,
                                       packID = s.packID,
                                       parkID = s.parkID,
                                       Date = Convert.ToDateTime(s.StartDate).ToString("MM/dd/yyyy"),
                                       StartTime = s.StartTime,
                                       EndTime = s.EndTime

                                   }).ToList(),
                        propaID = p.propaID,
                        vID = p.vID,
                        proprestID = p.proprestID,
                        propType = p.propType,
                        custOPID = p.propType == 2 ? (UhDB.CustomerOtherProperties.Where(x => x.custID == cuID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().custODID) : null

                    }).ToList();


            return result;
        }

        public IEnumerable<GetCustomerForServiceCategoryModel> GetCustomersByServiceSubCategory(int? cuID, int? catID, int? catsubID)
        {
            List<GetCustomerForServiceCategoryModel> result = new List<GetCustomerForServiceCategoryModel>();
            result = UhDB.CustomerOfficalDetails.Where(x => x.Customer.cuID == cuID && x.catID == catID && x.catsubID == catsubID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                    .Select(p => new GetCustomerForServiceCategoryModel
                    {
                        SubCategory = p.SubCategory.Name,
                        Area = p.PropertyArea.Name,
                        PropertyName = p.propType == 1 ? p.Venture.Name :
                        UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().TowerName,
                        GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == cuID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                   .Select(r => new GetCustomerAvailabilityModel
                                                   {
                                                       custSCID = r.custSCID,
                                                       Availability = r.Availability,
                                                       KeyCollection = r.KeyCollection,
                                                       AccessProperty = r.AccessProperty,
                                                       ReceptionDate = r.ReceptionDate
                                                   }).FirstOrDefault(),
                        Packages = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                   .Select(s => new GetPackagesForSubCategoryModel
                                   {
                                       PackageName = s.Package.Name,
                                       Price = s.Pricing.Price.ToString(),
                                       Duration = s.Pricing.Duration,
                                       Measurement = s.Pricing.TimeMeasurement,
                                       packID = s.packID,
                                       parkID = s.parkID,
                                       Date = Convert.ToDateTime(s.StartDate).ToString("MM/dd/yyyy"),
                                       StartTime = s.StartTime,
                                       EndTime = s.EndTime

                                   }).ToList(),
                        GetServices = UhDB.CustomerSpecializedCleanings.Where(x => x.custID == cuID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable().
                                    Select(t => new GetServiceSubCategoryModel
                                    {
                                        custSCID = t.custSCID,
                                        servcatID = t.servcatID,
                                        servsubcatID = t.servsubcatID,
                                        ServiceCategoryName = t.ServiceCategory.Name,
                                        ServiceSubCategoryName = t.ServiceSubCategory.Name,
                                        Quantity = t.Quantity
                                    }).ToList(),

                        propaID = p.propaID,
                        vID = p.vID,
                        propType = p.propType,
                        custOPID = p.propType == 2 ? (UhDB.CustomerOtherProperties.Where(x => x.custID == cuID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().custODID) : null

                    }).ToList();


            return result;
        }

        public IEnumerable<GetDropDown> GetDropdownForCustomerIDs(int? uID)
        {
            List<GetDropDown> result = new List<GetDropDown>();
            result = UhDB.Customers.Where(x => x.uID == uID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                   .Select(p => new GetDropDown { ID = p.cuID, Value = p.CustomerID.ToString() }).ToList();
            return result;
        }

        public List<string> CreateAppointment(CreateAppointmentModel customer)
        {
            List<string> result = new List<string>();
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    int? cuID = customer.cuID;
                    int? cuODID = null;
                    int? TempTaskNo = null;
                    int? TaskNo = null;
                    int? CustomerID = null;
                    bool? Continue = null;


                    List<string> S1 = new List<string>();
                    var BuldleDays = customer.BundleOfDays;
                    foreach (var BuldleDay in BuldleDays)
                    {
                        S1.Add(BuldleDay.Days);
                    }

                    CustomerOfficalDetail objCustomerOfficalDetail = new CustomerOfficalDetail();
                    objCustomerOfficalDetail.catID = customer.catID;
                    objCustomerOfficalDetail.catsubID = customer.catsubID;
                    objCustomerOfficalDetail.BuldleDays = string.Join(", ", S1);
                    objCustomerOfficalDetail.ServiceStatus = true;
                    objCustomerOfficalDetail.SpecialService = customer.SpecialService;
                    objCustomerOfficalDetail.propaID = customer.propaID;
                    objCustomerOfficalDetail.subAreaID = customer.subAreaID;
                    objCustomerOfficalDetail.vID = customer.vID;
                    objCustomerOfficalDetail.proprestID = customer.proprestID;
                    objCustomerOfficalDetail.propType = customer.propType;
                    objCustomerOfficalDetail.AppartmentNumber = customer.AppartmentNumber;
                    objCustomerOfficalDetail.Remarks = customer.Remarks;
                    objCustomerOfficalDetail.NoOfMonths = customer.monthlyCount;
                    objCustomerOfficalDetail.carstID = customer.carstID;
                    objCustomerOfficalDetail.cartID = customer.cartID;
                    objCustomerOfficalDetail.IsCarWash = customer.IsCarWash;

                    objCustomerOfficalDetail.carTRID = customer.carTRID;
                    objCustomerOfficalDetail.IsActive = customer.IsActive;
                    objCustomerOfficalDetail.IsDelete = customer.IsDelete;
                    objCustomerOfficalDetail.custID = cuID;
                    objCustomerOfficalDetail.CreatedBy = customer.CreatedBy;
                    objCustomerOfficalDetail.CreatedOn = customer.CreatedOn;
                    objCustomerOfficalDetail.CreatedRole = customer.CreatedRole;
                    UhDB.CustomerOfficalDetails.Add(objCustomerOfficalDetail);
                    UhDB.SaveChanges();
                    cuODID = objCustomerOfficalDetail.custODID;
                    CustomerID = UhDB.Customers.Where(x => x.cuID == customer.cuID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().CustomerID;
                    if (customer.propType == 2)
                    {
                        CustomerOtherProperty objCustomerOtherProperty = new CustomerOtherProperty();
                        objCustomerOtherProperty.custID = cuID;
                        objCustomerOtherProperty.custODID = cuODID;
                        objCustomerOtherProperty.TowerName = customer.TowerName;
                        objCustomerOtherProperty.BuildingName = customer.BuildingName;
                        objCustomerOtherProperty.Loacation = customer.Location;
                        objCustomerOtherProperty.LocationLink = customer.LocationLink;
                        objCustomerOtherProperty.StreetNumber = customer.StreetNumber;
                        objCustomerOtherProperty.ZoneNumber = customer.ZoneNumber;
                        objCustomerOtherProperty.IsActive = customer.IsActive;
                        objCustomerOtherProperty.IsDelete = customer.IsDelete;
                        objCustomerOtherProperty.CreatedBy = customer.CreatedBy;
                        objCustomerOtherProperty.CreatedOn = customer.CreatedOn;
                        objCustomerOtherProperty.CreatedRole = customer.CreatedRole;
                        UhDB.CustomerOtherProperties.Add(objCustomerOtherProperty);
                        UhDB.SaveChanges();
                    }
                    if (customer.SpecialService == true)
                    {
                        foreach (var service in customer.ServiceSubCategory)
                        {
                            CustomerSpecializedCleaning objCustomerSpecializedCleaning = new CustomerSpecializedCleaning();
                            objCustomerSpecializedCleaning.custID = cuID;
                            objCustomerSpecializedCleaning.custODID = cuODID;
                            objCustomerSpecializedCleaning.servcatID = service.servcatID;
                            objCustomerSpecializedCleaning.servsubcatID = service.servsubcatID;
                            objCustomerSpecializedCleaning.Quantity = service.Quantity;
                            objCustomerSpecializedCleaning.IsActive = customer.IsActive;
                            objCustomerSpecializedCleaning.IsDelete = customer.IsDelete;
                            objCustomerSpecializedCleaning.CreatedBy = customer.CreatedBy;
                            objCustomerSpecializedCleaning.CreatedOn = customer.CreatedOn;
                            objCustomerSpecializedCleaning.CreatedRole = customer.CreatedRole;
                            UhDB.CustomerSpecializedCleanings.Add(objCustomerSpecializedCleaning);
                            UhDB.SaveChanges();

                        }

                    }
                    if (customer.IsCarWash == true)
                    {
                        CustomerCarServiceDetail objCustomerCarServiceDetail = new CustomerCarServiceDetail();
                        objCustomerCarServiceDetail.custID = cuID;
                        objCustomerCarServiceDetail.custODID = cuODID;
                        objCustomerCarServiceDetail.ParkingLevel = customer.ParkingLevel;
                        objCustomerCarServiceDetail.ParkingNumber = customer.ParkingNumber;
                        objCustomerCarServiceDetail.VehicleBrand = customer.VehicleBrand;
                        objCustomerCarServiceDetail.VehicleColor = customer.VehicleColor;
                        objCustomerCarServiceDetail.VehicleNumber = customer.VehicleNumber;
                        objCustomerCarServiceDetail.IsActive = customer.IsActive;
                        objCustomerCarServiceDetail.IsDelete = customer.IsDelete;
                        objCustomerCarServiceDetail.CreatedBy = customer.CreatedBy;
                        objCustomerCarServiceDetail.CreatedOn = customer.CreatedOn;
                        UhDB.CustomerCarServiceDetails.Add(objCustomerCarServiceDetail);
                        UhDB.SaveChanges();
                    }

                    CustomerAvailability objCustomerAvailability = new CustomerAvailability();
                    objCustomerAvailability.Availability = customer.Availability;
                    objCustomerAvailability.KeyCollection = customer.KeyCollection;
                    objCustomerAvailability.ReceptionDate = customer.ReceptionDate;
                    objCustomerAvailability.AccessProperty = customer.AccessProperty;
                    objCustomerAvailability.custID = cuID;
                    objCustomerAvailability.custODID = cuODID;
                    objCustomerAvailability.IsActive = customer.IsActive;
                    objCustomerAvailability.IsDelete = customer.IsDelete;
                    objCustomerAvailability.CreatedBy = customer.CreatedBy;
                    objCustomerAvailability.CreatedOn = customer.CreatedOn;
                    objCustomerAvailability.CreatedRole = customer.CreatedRole;
                    UhDB.CustomerAvailabilities.Add(objCustomerAvailability);
                    UhDB.SaveChanges();

                    if (customer.catID == 1 && customer.catsubID == 1 && customer.Packages.IsCustomDays == null && customer.Packages.IsCustomSelectDate == null && customer.Packages.IsCustomTime == null && customer.SpecialService != true)
                    {
                        int? CountPack = UhDB.Packages.Where(x => x.packID == customer.Packages.packID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().RecursiveTime;
                        int? packID = customer.Packages.packID;
                        int? parkID = customer.Packages.parkID;

                        if (CountPack == 0)
                        {
                            int CountCustomerTaskNo = UhDB.CustomerTimelines.Where(x => x.Customer.uID == 1 && x.TaskNo != null && x.IsActive == true && x.IsDelete == false).Count();
                            if (CountCustomerTaskNo == 0)
                            {
                                TaskNo = 1;
                            }
                            else
                            {
                                TaskNo = UhDB.CustomerTimelines.Where(x => x.Customer.uID == 1 && x.TaskNo != null && x.IsActive == true && x.IsDelete == false).OrderByDescending(y => y.custTDID).FirstOrDefault().TaskNo;
                                TaskNo = TaskNo + 1;
                            }
                            if (TempTaskNo == null)
                            {
                                TempTaskNo = TaskNo;
                            }
                            CustomTimes customerTimeLine = new CustomTimes();
                            foreach (var BundleOfDays in customer.BundleOfDays)
                            {
                                customerTimeLine = BundleOfDays.Times;
                            }

                            CustomerTimeline objCustomerTimeline = new CustomerTimeline();
                            objCustomerTimeline.custID = cuID;
                            objCustomerTimeline.custODID = cuODID;
                            objCustomerTimeline.packID = packID;
                            objCustomerTimeline.parkID = parkID;
                            objCustomerTimeline.TaskNo = TaskNo;
                            objCustomerTimeline.StatusOfWork = 2;
                            objCustomerTimeline.teamID = customer.teamID;
                            objCustomerTimeline.StartDate = customer.Packages.StartDate;
                            objCustomerTimeline.StartTime = customerTimeLine.Start;
                            objCustomerTimeline.EndTime = customerTimeLine.End;
                            objCustomerTimeline.IsActive = customer.IsActive;
                            objCustomerTimeline.IsDelete = customer.IsDelete;
                            objCustomerTimeline.CreatedBy = customer.CreatedBy;
                            objCustomerTimeline.CreatedOn = customer.CreatedOn;
                            UhDB.CustomerTimelines.Add(objCustomerTimeline);
                            UhDB.SaveChanges();
                        }
                        else
                        {
                            DateTime? BlockDate = null;
                            DateTime StartDate = Convert.ToDateTime(customer.Packages.StartDate);
                            string StartDay = StartDate.DayOfWeek.ToString();
                            ListOfDays objListOfDays1 = new ListOfDays();
                            List<ListOfDisplayDays> ListOfDays = new List<ListOfDisplayDays>();
                            ListOfDays = objListOfDays1.Days();
                            List<string> BundleOfDays1 = customer.BundleOfDays.Select(x => x.Days).ToList();
                            List<ListOfDisplayDays> filteredResult = ListOfDays.Where(day => BundleOfDays1.Contains(day.Day)).ToList();
                            List<ListOfDisplayDays> BundleOfDays2 = CircleOutDays(filteredResult, StartDay);
                            var BundleOfDays3 = BundleOfDays2.Select(x => x.Day).ToList();
                            int BundleCount = BundleOfDays3.Count();
                            int? NoOfMonth = UhDB.CustomerRenewalMonths.Where(x => x.custrmID == customer.monthlyCount && x.IsActive == true && x.IsDelete == false).FirstOrDefault().NoOfMonths;
                            for (int k = 1; k <= NoOfMonth; k++)
                            {
                                DateTime? FirstDate = null;

                                for (int i = 1; i <= 4; i++)
                                {

                                    if (FirstDate != null)
                                    {
                                        DateTime TempDate = Convert.ToDateTime(FirstDate);
                                        int DateToAdd = 7 * (i - 1);
                                        TempDate = TempDate.AddDays(DateToAdd);
                                        StartDate = TempDate;
                                    }
                                    else
                                    {
                                        if (k > 1)
                                        {
                                            ListOfDays objListOfDays = new ListOfDays();
                                            string AFirstDay = StartDate.DayOfWeek.ToString();
                                            string ASecondDay = StartDay;
                                            int FirstDayID = objListOfDays.Days().Where(x => x.Day == AFirstDay).FirstOrDefault().ID;
                                            int SecondDayID = objListOfDays.Days().Where(x => x.Day == ASecondDay).FirstOrDefault().ID;
                                            StartDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(StartDate));
                                        }
                                    }
                                    string FirstDay = null;
                                    DateTime? AssignDate = null;
                                    List<string> EnteredDays = new List<string>();
                                    int EnterCount = 0;
                                    foreach (var BundleOfDay in BundleOfDays3)
                                    {
                                        string SecondDay = null;
                                        string Days = BundleOfDay;
                                        if ("Monday" == Days)
                                        {
                                            if (FirstDay != null)
                                            {
                                                ListOfDays objListOfDays = new ListOfDays();
                                                SecondDay = "Monday";
                                                int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                FirstDay = "Monday";
                                            }
                                            else
                                            {
                                                AssignDate = StartDate;
                                                if (FirstDate == null)
                                                {
                                                    FirstDate = AssignDate;
                                                }
                                                if (FirstDay == null)
                                                {
                                                    FirstDay = "Monday";
                                                }
                                            }
                                        }
                                        else if ("Tuesday" == Days)
                                        {
                                            if (FirstDay != null)
                                            {
                                                ListOfDays objListOfDays = new ListOfDays();
                                                SecondDay = "Tuesday";
                                                int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                FirstDay = "Tuesday";
                                            }
                                            else
                                            {
                                                AssignDate = StartDate;
                                                if (FirstDate == null)
                                                {
                                                    FirstDate = AssignDate;
                                                }
                                                if (FirstDay == null)
                                                {
                                                    FirstDay = "Tuesday";
                                                }
                                            }
                                        }
                                        else if ("Wednesday" == Days)
                                        {
                                            if (FirstDay != null)
                                            {
                                                ListOfDays objListOfDays = new ListOfDays();
                                                SecondDay = "Wednesday";
                                                int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                FirstDay = "Wednesday";
                                            }
                                            else
                                            {
                                                AssignDate = StartDate;
                                                if (FirstDate == null)
                                                {
                                                    FirstDate = AssignDate;
                                                }
                                                if (FirstDay == null)
                                                {
                                                    FirstDay = "Wednesday";
                                                }
                                            }
                                        }
                                        else if ("Thursday" == Days)
                                        {
                                            if (FirstDay != null)
                                            {
                                                ListOfDays objListOfDays = new ListOfDays();
                                                SecondDay = "Thursday";
                                                int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                FirstDay = "Thursday";
                                            }
                                            else
                                            {
                                                AssignDate = StartDate;
                                                if (FirstDate == null)
                                                {
                                                    FirstDate = AssignDate;
                                                }
                                                if (FirstDay == null)
                                                {
                                                    FirstDay = "Thursday";
                                                }
                                            }
                                        }
                                        else if ("Friday" == Days)
                                        {
                                            if (FirstDay != null)
                                            {
                                                ListOfDays objListOfDays = new ListOfDays();
                                                SecondDay = "Friday";
                                                int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                FirstDay = "Friday";
                                            }
                                            else
                                            {
                                                AssignDate = StartDate;
                                                if (FirstDate == null)
                                                {
                                                    FirstDate = AssignDate;
                                                }
                                                if (FirstDay == null)
                                                {
                                                    FirstDay = "Friday";
                                                }
                                            }
                                        }
                                        else if ("Saturday" == Days)
                                        {
                                            if (FirstDay != null)
                                            {
                                                ListOfDays objListOfDays = new ListOfDays();
                                                SecondDay = "Saturday";
                                                int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                FirstDay = "Saturday";
                                            }
                                            else
                                            {
                                                AssignDate = StartDate;
                                                if (FirstDate == null)
                                                {
                                                    FirstDate = AssignDate;
                                                }
                                                if (FirstDay == null)
                                                {
                                                    FirstDay = "Saturday";
                                                }
                                            }
                                        }
                                        else if ("Sunday" == Days)
                                        {
                                            if (FirstDay != null)
                                            {
                                                ListOfDays objListOfDays = new ListOfDays();
                                                SecondDay = "Sunday";
                                                int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                FirstDay = "Sunday";
                                            }
                                            else
                                            {
                                                AssignDate = StartDate;
                                                if (FirstDate == null)
                                                {
                                                    FirstDate = AssignDate;
                                                }
                                                if (FirstDay == null)
                                                {
                                                    FirstDay = "Sunday";
                                                }
                                            }
                                        }

                                        int CountCustomerTaskNo = UhDB.CustomerTimelines.Where(x => x.Customer.uID == 1 && x.TaskNo != null && x.IsActive == true && x.IsDelete == false).Count();
                                        if (CountCustomerTaskNo == 0)
                                        {
                                            TaskNo = 1;
                                        }
                                        else
                                        {
                                            TaskNo = UhDB.CustomerTimelines.Where(x => x.Customer.uID == 1 && x.TaskNo != null && x.IsActive == true && x.IsDelete == false).OrderByDescending(y => y.custTDID).FirstOrDefault().TaskNo;
                                            TaskNo = TaskNo + 1;
                                        }
                                        if (TempTaskNo == null)
                                        {
                                            TempTaskNo = TaskNo;
                                        }
                                        CustomTimes objCustomTimes = customer.BundleOfDays.Where(x => x.Days == BundleOfDay).FirstOrDefault().Times;
                                        CustomerTimeline objCustomerTimeline = new CustomerTimeline();
                                        objCustomerTimeline.custID = cuID;
                                        objCustomerTimeline.custODID = cuODID;
                                        objCustomerTimeline.custODID = cuODID;
                                        objCustomerTimeline.packID = packID;
                                        objCustomerTimeline.parkID = parkID;
                                        objCustomerTimeline.TaskNo = TaskNo;
                                        objCustomerTimeline.StatusOfWork = 2;
                                        objCustomerTimeline.teamID = customer.teamID;
                                        objCustomerTimeline.StartDate = AssignDate;
                                        BlockDate = AssignDate;
                                        objCustomerTimeline.StartTime = objCustomTimes.Start;
                                        objCustomerTimeline.EndTime = objCustomTimes.End;
                                        objCustomerTimeline.IsActive = customer.IsActive;
                                        objCustomerTimeline.IsDelete = customer.IsDelete;
                                        objCustomerTimeline.CreatedBy = customer.CreatedBy;
                                        objCustomerTimeline.CreatedOn = customer.CreatedOn;
                                        UhDB.CustomerTimelines.Add(objCustomerTimeline);
                                        UhDB.SaveChanges();
                                    }
                                }
                                StartDate = Convert.ToDateTime(BlockDate);
                            }
                            if (BlockDate != null)
                            {
                                ListOfDays objAListOfDays = new ListOfDays();
                                string ABFirstDay = BlockDate.Value.DayOfWeek.ToString();
                                string ABSecondDay = StartDay;
                                int ABFirstDayID = objAListOfDays.Days().Where(x => x.Day == ABFirstDay).FirstOrDefault().ID;
                                int ABSecondDayID = objAListOfDays.Days().Where(x => x.Day == ABSecondDay).FirstOrDefault().ID;
                                StartDate = GetNextDateForDay(ABFirstDayID, ABSecondDayID, Convert.ToDateTime(BlockDate));
                                for (int k = 1; k <= NoOfMonth; k++)
                                {
                                    DateTime? FirstDate = null;

                                    for (int i = 1; i <= 4; i++)
                                    {

                                        if (FirstDate != null)
                                        {
                                            DateTime TempDate = Convert.ToDateTime(FirstDate);
                                            int DateToAdd = 7 * (i - 1);
                                            TempDate = TempDate.AddDays(DateToAdd);
                                            StartDate = TempDate;
                                        }
                                        else
                                        {
                                            if (k > 1)
                                            {
                                                ListOfDays objListOfDays = new ListOfDays();
                                                string AFirstDay = StartDate.DayOfWeek.ToString();
                                                string ASecondDay = StartDay;
                                                int FirstDayID = objListOfDays.Days().Where(x => x.Day == AFirstDay).FirstOrDefault().ID;
                                                int SecondDayID = objListOfDays.Days().Where(x => x.Day == ASecondDay).FirstOrDefault().ID;
                                                StartDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(StartDate));

                                            }
                                        }
                                        string FirstDay = null;
                                        DateTime? AssignDate = null;
                                        List<string> EnteredDays = new List<string>();
                                        int EnterCount = 0;
                                        foreach (var BundleOfDay in BundleOfDays3)
                                        {
                                            string SecondDay = null;
                                            string Days = BundleOfDay;
                                            if ("Monday" == Days)
                                            {
                                                if (FirstDay != null)
                                                {
                                                    ListOfDays objListOfDays = new ListOfDays();
                                                    SecondDay = "Monday";
                                                    int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                    int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                    AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                    FirstDay = "Monday";
                                                }
                                                else
                                                {
                                                    AssignDate = StartDate;
                                                    if (FirstDate == null)
                                                    {
                                                        FirstDate = AssignDate;
                                                    }
                                                    if (FirstDay == null)
                                                    {
                                                        FirstDay = "Monday";
                                                    }
                                                }
                                            }
                                            else if ("Tuesday" == Days)
                                            {
                                                if (FirstDay != null)
                                                {
                                                    ListOfDays objListOfDays = new ListOfDays();
                                                    SecondDay = "Tuesday";
                                                    int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                    int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                    AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                    FirstDay = "Tuesday";
                                                }
                                                else
                                                {
                                                    AssignDate = StartDate;
                                                    if (FirstDate == null)
                                                    {
                                                        FirstDate = AssignDate;
                                                    }
                                                    if (FirstDay == null)
                                                    {
                                                        FirstDay = "Tuesday";
                                                    }
                                                }
                                            }
                                            else if ("Wednesday" == Days)
                                            {
                                                if (FirstDay != null)
                                                {
                                                    ListOfDays objListOfDays = new ListOfDays();
                                                    SecondDay = "Wednesday";
                                                    int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                    int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                    AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                    FirstDay = "Wednesday";
                                                }
                                                else
                                                {
                                                    AssignDate = StartDate;
                                                    if (FirstDate == null)
                                                    {
                                                        FirstDate = AssignDate;
                                                    }
                                                    if (FirstDay == null)
                                                    {
                                                        FirstDay = "Wednesday";
                                                    }
                                                }
                                            }
                                            else if ("Thursday" == Days)
                                            {
                                                if (FirstDay != null)
                                                {
                                                    ListOfDays objListOfDays = new ListOfDays();
                                                    SecondDay = "Thursday";
                                                    int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                    int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                    AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                    FirstDay = "Thursday";
                                                }
                                                else
                                                {
                                                    AssignDate = StartDate;
                                                    if (FirstDate == null)
                                                    {
                                                        FirstDate = AssignDate;
                                                    }
                                                    if (FirstDay == null)
                                                    {
                                                        FirstDay = "Thursday";
                                                    }
                                                }
                                            }
                                            else if ("Friday" == Days)
                                            {
                                                if (FirstDay != null)
                                                {
                                                    ListOfDays objListOfDays = new ListOfDays();
                                                    SecondDay = "Friday";
                                                    int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                    int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                    AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                    FirstDay = "Friday";
                                                }
                                                else
                                                {
                                                    AssignDate = StartDate;
                                                    if (FirstDate == null)
                                                    {
                                                        FirstDate = AssignDate;
                                                    }
                                                    if (FirstDay == null)
                                                    {
                                                        FirstDay = "Friday";
                                                    }
                                                }
                                            }
                                            else if ("Saturday" == Days)
                                            {
                                                if (FirstDay != null)
                                                {
                                                    ListOfDays objListOfDays = new ListOfDays();
                                                    SecondDay = "Saturday";
                                                    int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                    int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                    AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                    FirstDay = "Saturday";
                                                }
                                                else
                                                {
                                                    AssignDate = StartDate;
                                                    if (FirstDate == null)
                                                    {
                                                        FirstDate = AssignDate;
                                                    }
                                                    if (FirstDay == null)
                                                    {
                                                        FirstDay = "Saturday";
                                                    }
                                                }
                                            }
                                            else if ("Sunday" == Days)
                                            {
                                                if (FirstDay != null)
                                                {
                                                    ListOfDays objListOfDays = new ListOfDays();
                                                    SecondDay = "Sunday";
                                                    int FirstDayID = objListOfDays.Days().Where(x => x.Day == FirstDay).FirstOrDefault().ID;
                                                    int SecondDayID = objListOfDays.Days().Where(x => x.Day == SecondDay).FirstOrDefault().ID;
                                                    AssignDate = GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
                                                    FirstDay = "Sunday";
                                                }
                                                else
                                                {
                                                    AssignDate = StartDate;
                                                    if (FirstDate == null)
                                                    {
                                                        FirstDate = AssignDate;
                                                    }
                                                    if (FirstDay == null)
                                                    {
                                                        FirstDay = "Sunday";
                                                    }
                                                }
                                            }

                                            int CountCustomerTaskNo = UhDB.CustomerTimelines.Where(x => x.Customer.uID == 1 && x.TaskNo != null && x.IsActive == true && x.IsDelete == false).Count();
                                            if (CountCustomerTaskNo == 0)
                                            {
                                                TaskNo = 1;
                                            }
                                            else
                                            {
                                                TaskNo = UhDB.CustomerTimelines.Where(x => x.Customer.uID == 1 && x.TaskNo != null && x.IsActive == true && x.IsDelete == false).OrderByDescending(y => y.custTDID).FirstOrDefault().TaskNo;
                                                TaskNo = TaskNo + 1;
                                            }
                                            if (TempTaskNo == null)
                                            {
                                                TempTaskNo = TaskNo;
                                            }
                                            CustomTimes objCustomTimes = customer.BundleOfDays.Where(x => x.Days == BundleOfDay).FirstOrDefault().Times;
                                            CustomerDateBlock objCustomerDateBlock = new CustomerDateBlock();
                                            objCustomerDateBlock.custID = cuID;
                                            objCustomerDateBlock.custODID = cuODID;
                                            objCustomerDateBlock.custODID = cuODID;
                                            objCustomerDateBlock.packID = packID;
                                            objCustomerDateBlock.parkID = parkID;
                                            objCustomerDateBlock.StartDate = AssignDate;
                                            BlockDate = AssignDate;
                                            objCustomerDateBlock.teamID = customer.teamID;
                                            objCustomerDateBlock.StartTime = objCustomTimes.Start;
                                            objCustomerDateBlock.EndTime = objCustomTimes.End;
                                            objCustomerDateBlock.IsActive = customer.IsActive;
                                            objCustomerDateBlock.IsDelete = customer.IsDelete;
                                            objCustomerDateBlock.CreatedBy = customer.CreatedBy;
                                            objCustomerDateBlock.CreatedOn = customer.CreatedOn;
                                            UhDB.CustomerDateBlocks.Add(objCustomerDateBlock);
                                            UhDB.SaveChanges();
                                        }
                                    }
                                    StartDate = Convert.ToDateTime(BlockDate);
                                }
                            }
                        }
                    }
                    else
                    {

                        int CountCustomerTaskNo = UhDB.CustomerTimelines.Where(x => x.Customer.uID == 1 && x.TaskNo != null && x.IsActive == true && x.IsDelete == false).Count();
                        if (CountCustomerTaskNo == 0)
                        {
                            TaskNo = 1;
                        }
                        else
                        {
                            TaskNo = UhDB.CustomerTimelines.Where(x => x.Customer.uID == 1 && x.TaskNo != null && x.IsActive == true && x.IsDelete == false).OrderByDescending(y => y.custTDID).FirstOrDefault().TaskNo;
                            TaskNo = TaskNo + 1;
                        }
                        if (TempTaskNo == null)
                        {
                            TempTaskNo = TaskNo;
                        }
                        if (customer.SpecialService == true)
                        {
                            CustomerTimeline objCustomerTimeline = new CustomerTimeline();
                            objCustomerTimeline.custID = cuID;
                            objCustomerTimeline.custODID = cuODID;
                            objCustomerTimeline.TaskNo = TaskNo;
                            objCustomerTimeline.StatusOfWork = 1;
                            objCustomerTimeline.parkID = customer.parkID;
                            objCustomerTimeline.packID = customer.packID;
                            objCustomerTimeline.StartDate = customer.StartDate;
                            objCustomerTimeline.StartTime = customer.StartTime;
                            objCustomerTimeline.EndTime = customer.EndTime;
                            objCustomerTimeline.IsActive = customer.IsActive;
                            objCustomerTimeline.IsDelete = customer.IsDelete;
                            objCustomerTimeline.CreatedBy = customer.CreatedBy;
                            objCustomerTimeline.CreatedOn = customer.CreatedOn;
                            UhDB.CustomerTimelines.Add(objCustomerTimeline);
                            UhDB.SaveChanges();
                        }
                        else
                        {
                            int? packID = customer.Packages.packID;
                            int? parkID = customer.Packages.parkID;
                            CustomTimes customerTimeLine = new CustomTimes();
                            foreach (var BundleOfDays in customer.BundleOfDays)
                            {
                                customerTimeLine = BundleOfDays.Times;
                            }
                            CustomerTimeline objCustomerTimeline = new CustomerTimeline();
                            objCustomerTimeline.custID = cuID;
                            objCustomerTimeline.custODID = cuODID;
                            objCustomerTimeline.TaskNo = TaskNo;
                            objCustomerTimeline.parkID = parkID;
                            objCustomerTimeline.packID = packID;
                            objCustomerTimeline.StatusOfWork = 1;
                            objCustomerTimeline.StartDate = customer.Packages.StartDate;
                            objCustomerTimeline.StartTime = customerTimeLine.Start;
                            objCustomerTimeline.EndTime = customerTimeLine.End;
                            objCustomerTimeline.IsActive = customer.IsActive;
                            objCustomerTimeline.IsDelete = customer.IsDelete;
                            objCustomerTimeline.CreatedBy = customer.CreatedBy;
                            objCustomerTimeline.CreatedOn = customer.CreatedOn;
                            UhDB.CustomerTimelines.Add(objCustomerTimeline);
                            UhDB.SaveChanges();
                        }

                    }
                    CustomerInovice objCustomerInovice = new CustomerInovice();
                    objCustomerInovice.cuID = cuID;
                    objCustomerInovice.custODID = cuODID;
                    objCustomerInovice.InvoiceNumber = customer.InVoice;
                    objCustomerInovice.uID = 1;
                    objCustomerInovice.IsActive = customer.IsActive;
                    objCustomerInovice.IsDelete = customer.IsDelete;
                    objCustomerInovice.CreatedRole = customer.CreatedRole;
                    objCustomerInovice.CreatedBy = customer.CreatedBy;
                    objCustomerInovice.CreatedOn = customer.CreatedOn;
                    UhDB.CustomerInovices.Add(objCustomerInovice);
                    UhDB.SaveChanges();
                    var objCustomer = UhDB.Customers.Where(x => x.cuID == cuID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();

                    if (customer.SpecialService == true)
                    {

                    }
                    else
                    {
                        if (customer.catID == 1 && customer.catsubID == 1 && customer.Packages.IsCustomDays == null && customer.Packages.IsCustomSelectDate == null && customer.Packages.IsCustomTime == null && customer.SpecialService != true)
                        {
                            if (customer.teamID != null)
                            {
                                string CustomerName = null, CustomerEmail = null, CustomerMobile = null, PropertyName = null,
                                    ApartmentName = null, Area = null;
                                int? propType = null;

                                var objCustomerOfficalDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID && x.custODID == cuODID && x.catID == customer.catID && x.catsubID == customer.catsubID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                var objStaffTeams = UhDB.StaffTeams.Where(x => x.teamID == customer.teamID && x.IsActive == true && x.IsDelete == false).ToList();
                                propType = objCustomerOfficalDetails.propType;
                                CustomerName = objCustomerOfficalDetails.Customer.Name;
                                CustomerEmail = objCustomerOfficalDetails.Customer.Email;
                                CustomerMobile = objCustomerOfficalDetails.Customer.Mobile;
                                Area = UhDB.PropertyAreas.Where(x => x.propaID == customer.propaID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                                PropertyName = UhDB.Ventures.Where(x => x.vID == customer.vID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                                ApartmentName = objCustomerOfficalDetails.AppartmentNumber;
                                foreach (var item in objStaffTeams)
                                {
                                    int? stfID = item.stfID;
                                    var objStaffs = UhDB.Staffs.Where(x => x.stfID == stfID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                    string StaffEmail = objStaffs.Email;
                                    string StaffName = objStaffs.Name;
                                    string EmailBody = null;
                                    if (propType == 1)
                                    {
                                        EmailBody = EmailBodyCustomerInfoForStaff(Area, PropertyName, CustomerName, CustomerMobile, CustomerEmail, StaffName, objCustomerOfficalDetails.Remarks);
                                        if (StaffEmail != null)
                                        {
                                            objGeneralDB.SentEmailFromAmazon(StaffEmail, EmailBody, "Task Assigned", StaffName);
                                        }
                                        else
                                        {

                                        }
                                    }
                                    else
                                    {
                                        string TowerName = customer.TowerName;
                                        string BuildingName = customer.BuildingName;
                                        string ZoneNumber = customer.ZoneNumber;
                                        string Location = customer.Location;
                                        string StreetNumber = customer.StreetNumber;
                                        string LocationLink = customer.LocationLink;
                                        string ApartmentNo = customer.AppartmentNumber;
                                        string Description = customer.Remarks;
                                        EmailBody = EmailBodyCustomerInfoForStaffForOtherProperty(CustomerName, CustomerEmail, CustomerMobile, Area, TowerName, BuildingName, StreetNumber, ZoneNumber, Location, LocationLink, StaffName, ApartmentNo, Description);
                                        if (StaffEmail != null)
                                        {
                                            objGeneralDB.SentEmailFromAmazon(StaffEmail, EmailBody, "Task Assigned", StaffName);
                                        }
                                        else
                                        {

                                        }
                                    }
                                }
                            }
                            else
                            {
                                string body = EmailBodyForStaffNotAssign(CustomerID.ToString(), TempTaskNo.ToString());
                                // objGeneralDB.SentEmailFromAmazon(objCustomer.User.Email, body, "Staff Not Assign Yet", objCustomer.User.Name);
                            }
                        }
                        else
                        {
                            string body = EmailBodyForCustomerAssignDate(CustomerID.ToString(), TempTaskNo.ToString());
                            objGeneralDB.SentEmailFromAmazon(objCustomer.User.Email, body, "Customer request for Custom Dates", objCustomer.User.Name);
                        }
                    }
                    string PaymentLink = null;
                    if (customer.SpecialService == true)
                    {
                        PaymentRequest objPaymentRequest = new PaymentRequest();
                        objPaymentRequest.FirstName = objCustomer.Name;
                        objPaymentRequest.LastName = objCustomer.Name;
                        objPaymentRequest.Email = objCustomer.Email;
                        objPaymentRequest.Phone = objCustomer.Mobile;
                        objPaymentRequest.Street = UhDB.PropertyAreas.Where(x => x.propaID == customer.propaID && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                                   UhDB.PropertyAreas.Where(x => x.propaID == customer.propaID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name : "N/A";
                        objPaymentRequest.Amount = customer.Amount;
                        objPaymentRequest.City = "Doha";
                        objPaymentRequest.State = "DL";
                        objPaymentRequest.PostalCode = "110015";
                        objPaymentRequest.Country = "QR";
                        objPaymentRequest.Custom1 = cuID.ToString();
                        int CountTransactionID = UhDB.CustomerTransactions.Where(x => x.IsActive == true && x.IsDelete == false && x.Customer.uID == 1).Count();
                        if (CountTransactionID == 0)
                        {
                            objPaymentRequest.TransactionId = "1";
                        }
                        else
                        {
                            string TransID = UhDB.CustomerTransactions.Where(x => x.IsActive == true && x.IsDelete == false && x.Customer.uID == 1).OrderByDescending(x => x.custTrasID).FirstOrDefault().TransactionID;
                            objPaymentRequest.TransactionId = (Convert.ToInt32(TransID) + 1).ToString();
                        }


                        List<string> Signatures = CalculateSignature(objPaymentRequest);
                        string id = null, TransactionID = null;
                        for (var i = 0; i < 3; i++)
                        {
                            if (i == 0)
                            {
                                PaymentLink = Signatures[0];
                            }
                            else if (i == 1)
                            {
                                id = Signatures[1];
                            }
                            else if (i == 2)
                            {
                                TransactionID = Signatures[2];
                            }
                        }
                        PackageDetailsModelV2 objPackageDetailsModel = new PackageDetailsModelV2();
                        objPackageDetailsModel.SubCategoryName = UhDB.SubCategories.Where(x => x.catsubID == customer.catsubID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                        objPackageDetailsModel.AreaName = UhDB.PropertyAreas.Where(x => x.propaID == customer.propaID && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                                          UhDB.PropertyAreas.Where(x => x.propaID == customer.propaID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name : "N/A";
                        ;
                        objPackageDetailsModel.InVoice = "# INV- " + customer.InVoice;
                        objPackageDetailsModel.PropName = UhDB.Ventures.Where(x => x.vID == customer.vID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                        objPackageDetailsModel.StartDate = Convert.ToDateTime(customer.StartDate).ToString("dd/MM/yyyy");
                        objPackageDetailsModel.StartTime = customer.StartTime;
                        List<SubServicesInvoiceDetails> ServiceSubCategory = new List<SubServicesInvoiceDetails>();
                        int TotalQuantity = 0;
                        double? TotalSelectedPrice = null;
                        foreach (var service in customer.ServiceSubCategory)
                        {
                            int? servcatID = service.servcatID;
                            int? servcatsubID = service.servsubcatID;
                            string ServiceOptionName = UhDB.ServiceCategories.Where(x => x.servcatID == servcatID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                            string ServiceSubCategoryName = UhDB.ServiceSubCategories.Where(x => x.servsubcatID == servcatsubID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                            string Quantity = service.Quantity.ToString();
                            TotalQuantity = TotalQuantity + (int)service.Quantity;
                            string Price = service.EachServiceprice.ToString();
                            string TotalPrice = service.TotalPrice.ToString();
                            TotalSelectedPrice = service.TotalPrice;
                            ServiceSubCategory.Add(new SubServicesInvoiceDetails { ServiceSubCategory = ServiceSubCategoryName, ServiceOption = ServiceOptionName, Quantity = Quantity, Price = Price, TotalPrice = TotalPrice });

                        }
                        objPackageDetailsModel.ServiceSubCategory = ServiceSubCategory;
                        string pdfBase64 = CreateInvoiceV2(objPackageDetailsModel);
                        objGeneralDB.SendEmailWithAttachmentForBodyAttachment(pdfBase64, objCustomer.Email, "Invoice", PaymentLink);

                        CustomerTransaction objCustomerTransaction = new CustomerTransaction();
                        objCustomerTransaction.cuID = cuID;
                        objCustomerTransaction.custODID = cuODID;
                        objCustomerTransaction.Quantity = TotalQuantity;
                        objCustomerTransaction.TotalPrice = TotalSelectedPrice;
                        objCustomerTransaction.TransactionID = TransactionID;
                        objCustomerTransaction.PayementID = id;
                        objCustomerTransaction.IsActive = customer.IsActive;
                        objCustomerTransaction.IsDelete = customer.IsDelete;
                        objCustomerTransaction.CreatedBy = customer.CreatedBy;
                        objCustomerTransaction.CreatedOn = customer.CreatedOn;
                        UhDB.CustomerTransactions.Add(objCustomerTransaction);
                        UhDB.SaveChanges();
                    }
                    else
                    {
                        if (customer.Packages != null && customer.Packages.IsCustomDays == null && customer.Packages.IsCustomSelectDate == null && customer.Packages.IsCustomTime == null)
                        {
                            PaymentRequest objPaymentRequest = new PaymentRequest();
                            objPaymentRequest.FirstName = objCustomer.Name;
                            objPaymentRequest.LastName = objCustomer.Name;
                            objPaymentRequest.Email = objCustomer.Email;
                            objPaymentRequest.Phone = objCustomer.Mobile;
                            objPaymentRequest.Street = UhDB.PropertyAreas.Where(x => x.propaID == customer.propaID && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                                        UhDB.PropertyAreas.Where(x => x.propaID == customer.propaID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name : "N/A";
                            objPaymentRequest.Amount = customer.Packages.TotalPrice.ToString();
                            objPaymentRequest.City = "Doha";
                            objPaymentRequest.State = "DL";
                            objPaymentRequest.PostalCode = "110015";
                            objPaymentRequest.Country = "QR";
                            objPaymentRequest.Custom1 = cuID.ToString();
                            int CountTransactionID = UhDB.CustomerTransactions.Where(x => x.IsActive == true && x.IsDelete == false && x.Customer.uID == 1).Count();
                            if (CountTransactionID == 0)
                            {
                                objPaymentRequest.TransactionId = "1";
                            }
                            else
                            {
                                string TransID = UhDB.CustomerTransactions.Where(x => x.IsActive == true && x.IsDelete == false && x.Customer.uID == 1).OrderByDescending(x => x.custTrasID).FirstOrDefault().TransactionID;
                                objPaymentRequest.TransactionId = (Convert.ToInt32(TransID) + 1).ToString();
                            }


                            List<string> Signatures = CalculateSignature(objPaymentRequest);
                            string id = null, TransactionID = null;
                            for (var i = 0; i < 3; i++)
                            {
                                if (i == 0)
                                {
                                    PaymentLink = Signatures[0];
                                }
                                else if (i == 1)
                                {
                                    id = Signatures[1];
                                }
                                else if (i == 2)
                                {
                                    TransactionID = Signatures[2];
                                }
                            }
                            PackageDetailsModel objPackageDetailsModel = new PackageDetailsModel();
                            if (customer.catID == 2)
                            {
                                objPackageDetailsModel.SubCategoryName = UhDB.MainCategories.Where(x => x.catID == customer.catID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                            }
                            else
                            {
                                objPackageDetailsModel.SubCategoryName = UhDB.SubCategories.Where(x => x.catsubID == customer.catsubID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                            }
                            objPackageDetailsModel.AreaName = UhDB.PropertyAreas.Where(x => x.propaID == customer.propaID && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                                              UhDB.PropertyAreas.Where(x => x.propaID == customer.propaID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name : "N/A";
                            objPackageDetailsModel.InVoice = "# INV- " + customer.InVoice;
                            objPackageDetailsModel.PackageName = UhDB.Packages.Where(x => x.packID == customer.Packages.packID && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                                                 UhDB.Packages.Where(x => x.packID == customer.Packages.packID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name : "N/A";
                            objPackageDetailsModel.Price = customer.Packages.EachServiceprice.ToString();
                            objPackageDetailsModel.PropName = UhDB.Ventures.Where(x => x.vID == customer.vID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                            if (customer.catID == 2)
                            {
                                objPackageDetailsModel.resdName = "N/A";
                            }
                            else
                            {
                                objPackageDetailsModel.resdName = UhDB.PropertyResidenceTypes.Where(x => x.proprestID == customer.proprestID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                            }
                            if (customer.monthlyCount != null && customer.monthlyCount != 0)
                            {
                                objPackageDetailsModel.NoOfMonths = UhDB.CustomerRenewalMonths.Where(x => x.custrmID == customer.monthlyCount && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                            }
                            else
                            {
                                objPackageDetailsModel.NoOfMonths = "N/A";
                            }
                            objPackageDetailsModel.Price = customer.Price;
                            objPackageDetailsModel.TotalPrice = customer.Amount;
                            string pdfBase64 = CreateInvoice(objPackageDetailsModel);
                            objGeneralDB.SendEmailWithAttachmentForBodyAttachment(pdfBase64, objCustomer.Email, "Invoice", PaymentLink);

                            CustomerTransaction objCustomerTransaction = new CustomerTransaction();
                            objCustomerTransaction.cuID = cuID;
                            objCustomerTransaction.custODID = cuODID;
                            objCustomerTransaction.Quantity = customer.TotalNoOfService;
                            objCustomerTransaction.TotalPrice = customer.Packages.TotalPrice;
                            objCustomerTransaction.Price = customer.Packages.EachServiceprice;
                            objCustomerTransaction.TransactionID = TransactionID;
                            objCustomerTransaction.PayementID = id;
                            objCustomerTransaction.IsActive = customer.IsActive;
                            objCustomerTransaction.IsDelete = customer.IsDelete;
                            objCustomerTransaction.CreatedBy = customer.CreatedBy;
                            objCustomerTransaction.CreatedOn = customer.CreatedOn;
                            UhDB.CustomerTransactions.Add(objCustomerTransaction);
                            UhDB.SaveChanges();

                        }
                        else
                        {
                            CustomerCustomDateTime objCustomerCustomDateTime = new CustomerCustomDateTime();
                            objCustomerCustomDateTime.cuID = cuID;
                            objCustomerCustomDateTime.custODID = cuODID;
                            objCustomerCustomDateTime.CustomDays = customer.Packages.CustomDays.Length != 0 ? customer.Packages.CustomDays.ToString() : null;
                            objCustomerCustomDateTime.CustomStartDate = customer.Packages.CustomSelectDate != null ? customer.Packages.CustomSelectDate : null;
                            objCustomerCustomDateTime.CustomStartTime = customer.Packages.CustomTime != null ? customer.Packages.CustomTime[0] : null;
                            objCustomerCustomDateTime.CustomEndTime = customer.Packages.CustomTime != null ? customer.Packages.CustomTime[1] : null;
                            objCustomerCustomDateTime.IsActive = customer.IsActive;
                            objCustomerCustomDateTime.IsDelete = customer.IsDelete;
                            objCustomerCustomDateTime.CreatedBy = customer.CreatedBy;
                            objCustomerCustomDateTime.CreatedOn = customer.CreatedOn;
                            UhDB.CustomerCustomDateTimes.Add(objCustomerCustomDateTime);
                            UhDB.SaveChanges();

                        }
                    }

                    int CountTempID = UhDB.CustomerTimeBlocks.Where(x => x.MobileNo == objCustomer.Mobile && EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(DateTime.Now) && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountTempID != 0)
                    {
                        var objCustomerTimeBlocks = UhDB.CustomerTimeBlocks.Where(x => x.MobileNo == objCustomer.Mobile && EntityFunctions.TruncateTime(x.CreatedOn) == EntityFunctions.TruncateTime(DateTime.Now) && x.IsActive == true && x.IsDelete == false).ToList();
                        foreach (var objCustomerTimeBlock in objCustomerTimeBlocks)
                        {
                            var objUpdateCustomerTimeBlock = UhDB.CustomerTimeBlocks.Where(x => x.custTBID == objCustomerTimeBlock.custTBID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                            objUpdateCustomerTimeBlock.IsActive = false;
                            objUpdateCustomerTimeBlock.IsDelete = true;
                            objUpdateCustomerTimeBlock.UpdatedBy = customer.CreatedBy;
                            objUpdateCustomerTimeBlock.UpdatedOn = DateTime.Now;
                            UhDB.SaveChanges();
                        }
                    }
                    if (customer.Remarks != null)
                    {
                        CustomerAlert objCustomerAlert = new CustomerAlert();
                        objCustomerAlert.custID = cuID;
                        objCustomerAlert.custATID = 4;
                        objCustomerAlert.vID = customer.vID;
                        objCustomerAlert.catID = customer.catID;
                        objCustomerAlert.catsubID = customer.catsubID;
                        objCustomerAlert.Message = customer.Remarks;
                        objCustomerAlert.IsActive = customer.IsActive;
                        objCustomerAlert.IsDelete = customer.IsDelete;
                        objCustomerAlert.CreatedBy = customer.CreatedBy;
                        objCustomerAlert.CreatedOn = customer.CreatedOn;
                        UhDB.CustomerAlerts.Add(objCustomerAlert);
                        UhDB.SaveChanges();
                    }
                    trans.Commit();
                    result.Add(cuID.ToString());
                    result.Add(cuODID.ToString());
                    result.Add(CustomerID.ToString());
                    result.Add(TempTaskNo.ToString());
                    result.Add(PaymentLink);

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    result[0] = "-2";
                }
            }

            return result;
        }

        public string AssignTeamCustomer(AssignTeamCustomer customer)
        {
            string result = null, CustomerName = null, CustomerEmail = null, CustomerMobile = null, PropertyName = null,
                            ApartmentName = null, Area = null;
            int? propType = null;
            if (customer.IsTeam == true)
            {
                using (var trans = UhDB.Database.BeginTransaction())
                {
                    try
                    {
                        //var objCustomerOfficalDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == customer.cuID &&
                        //        x.custODID == customer.custODID && x.catID == customer.catID && x.catsubID == customer.catsubID
                        //        && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                        //objCustomerOfficalDetails.teamID = customer.teamID;
                        //objCustomerOfficalDetails.UpdatedBy = customer.UpdatedBy;
                        //objCustomerOfficalDetails.UpdatedOn = customer.UpdatedOn;
                        //objCustomerOfficalDetails.UpdatedRole = customer.UpdatedRole;
                        //UhDB.SaveChanges();

                        if (customer.catsubID == 1)
                        {
                            if (customer.IsTeamPermanent == false)
                            {
                                var objUpdateCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.custID == customer.cuID && x.custODID == customer.custODID && x.custTDID == customer.custTDID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                objUpdateCustomerTimeLines.teamID = customer.teamID;
                                objUpdateCustomerTimeLines.UpdatedBy = customer.UpdatedBy;
                                objUpdateCustomerTimeLines.UpdatedOn = customer.UpdatedOn;
                                objUpdateCustomerTimeLines.UpdatedRole = customer.UpdatedRole;
                                UhDB.SaveChanges();
                            }
                            else
                            {
                                DateTime TodaysDate = DateTime.Now;
                                var objCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.custID == customer.cuID && x.custODID == customer.custODID && x.IsActive == true && x.IsDelete == false
                                                           && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(TodaysDate)).ToList();
                                foreach (var objCustomerTimeLine in objCustomerTimeLines)
                                {
                                    var objUpdateCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.custTDID == objCustomerTimeLine.custTDID).FirstOrDefault();
                                    objUpdateCustomerTimeLines.teamID = customer.teamID;
                                    objUpdateCustomerTimeLines.UpdatedBy = customer.UpdatedBy;
                                    objUpdateCustomerTimeLines.UpdatedOn = customer.UpdatedOn;
                                    objUpdateCustomerTimeLines.UpdatedRole = customer.UpdatedRole;
                                    UhDB.SaveChanges();
                                }

                            }
                        }
                        else
                        {

                            var objUpdateCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.custID == customer.cuID && x.custODID == customer.custODID && x.IsActive == true && x.IsDelete == false).ToList();
                            foreach (var objUpdateCustomerTimeLine in objUpdateCustomerTimeLines)
                            {
                                int? custTDID = objUpdateCustomerTimeLine.custTDID;
                                var objUpdateWorkStatus = UhDB.CustomerTimelines.Where(x => x.custTDID == custTDID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                if (customer.IsTeamReAssign == false)
                                {
                                    objUpdateWorkStatus.StatusOfWork = 2;
                                }
                                objUpdateWorkStatus.teamID = customer.teamID;
                                objUpdateWorkStatus.UpdatedBy = customer.UpdatedBy;
                                objUpdateWorkStatus.UpdatedOn = customer.UpdatedOn;
                                objUpdateWorkStatus.UpdatedRole = customer.UpdatedRole;
                                UhDB.SaveChanges();
                            }
                        }


                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                    }
                }
            }
            else { }

            result = "SUCCESS";
            return result;
        }

        public bool? CountSameTeam(CountSameTeamModel team)
        {
            bool? result = null;
            int CountSameTeam = UhDB.CustomerOfficalDetails.Where(x => x.custID == team.cuID && x.catID == team.catID && x.catsubID == team.catsubID && x.IsActive == true && x.IsDelete == false).Count();
            if (CountSameTeam > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public string ClosedTask(ClosedTaskModel task)
        {
            string result = null;
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custTDID == task.custTDID
                                               && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    objCustomerTimelines.StatusOfWork = 3;
                    objCustomerTimelines.ClosingDate = DateTime.Now;
                    // objCustomerTimelines.ClosingRemarks = task.Remarks;
                    objCustomerTimelines.UpdatedBy = task.UpdatedBy;
                    objCustomerTimelines.UpdatedOn = task.UpdatedOn;
                    objCustomerTimelines.UpdatedRole = task.UpdatedRole;
                    UhDB.SaveChanges();
                    var objCustomer = UhDB.Customers.Where(x => x.cuID == task.cuID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    if (objCustomer.CustomerType == 1)
                    {
                        var objUpdateCustomer = UhDB.Customers.Where(x => x.cuID == task.cuID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                        objUpdateCustomer.CustomerType = 2;
                        objUpdateCustomer.CustomerTypeOn = task.UpdatedOn;
                        objUpdateCustomer.UpdatedBy = task.UpdatedBy;
                        objUpdateCustomer.UpdatedOn = task.UpdatedOn;
                        objUpdateCustomer.UpdatedRole = task.UpdatedRole;
                        UhDB.SaveChanges();
                    }
                    int? custODID = objCustomerTimelines.custODID;
                    int? stfID = objCustomerTimelines.CustomerOfficalDetail.stfID;
                    int? teamID = objCustomerTimelines.teamID;
                    int? propType = objCustomerTimelines.CustomerOfficalDetail.propType;
                    string CustomerName = objCustomerTimelines.CustomerOfficalDetail.Customer.Name;
                    string CustomerMobile = objCustomerTimelines.CustomerOfficalDetail.Customer.Mobile;
                    string CustomerEmail = objCustomerTimelines.CustomerOfficalDetail.Customer.Email;
                    string CustomerID = objCustomerTimelines.CustomerOfficalDetail.Customer.CustomerID.ToString();
                    string CustomerTaskNo = objCustomerTimelines.TaskNo.ToString();
                    if (propType == 1)
                    {
                        if (stfID != null)
                        {
                            string Area = objCustomerTimelines.CustomerOfficalDetail.PropertyArea.Name;
                            string PropertyName = objCustomerTimelines.CustomerOfficalDetail.Venture.Name;
                            string StaffName = objCustomerTimelines.CustomerOfficalDetail.Staff.Name;
                            string StaffEmail = objCustomerTimelines.CustomerOfficalDetail.Staff.Email;
                            string AdminEmail = objCustomerTimelines.CustomerOfficalDetail.Customer.User.Email;
                            string AdminName = objCustomerTimelines.CustomerOfficalDetail.Customer.User.Name;
                            string ApartmentNo = objCustomerTimelines.CustomerOfficalDetail.AppartmentNumber;
                            string Date = Convert.ToDateTime(objCustomerTimelines.StartDate).ToString("dd/MM/yyyy");
                            string body = EmailBodyCustomerClosingInfoForStaff(Area, PropertyName, CustomerName, CustomerMobile, CustomerEmail, StaffName, task.Remarks, CustomerID, CustomerTaskNo, ApartmentNo, Date);
                            string CustomerBody = EmailBodyCustomerClosingforCustomer(Area, PropertyName, CustomerName, CustomerMobile, CustomerEmail, StaffName, CustomerID, CustomerTaskNo, objCustomerTimelines.CustomerOfficalDetail.Remarks, ApartmentNo, Convert.ToDateTime(objCustomerTimelines.StartDate).ToString("dd/MM/yyyy"));
                            objGeneralDB.SentEmailFromAmazon(CustomerEmail, CustomerBody, CustomerTaskNo + " is completed for CustomerID : " + CustomerID, CustomerName);
                            objGeneralDB.SentEmailFromAmazon(AdminEmail, body, CustomerTaskNo + " is completed for CustomerID : " + CustomerID, AdminName);
                            objGeneralDB.SentEmailFromAmazon(StaffEmail, body, CustomerTaskNo + " is completed", StaffName);
                        }
                        else
                        {
                            string Area = objCustomerTimelines.CustomerOfficalDetail.PropertyArea.Name;
                            string PropertyName = objCustomerTimelines.CustomerOfficalDetail.Venture.Name;
                            string AdminEmail = objCustomerTimelines.CustomerOfficalDetail.Customer.User.Email;
                            string AdminName = objCustomerTimelines.CustomerOfficalDetail.Customer.User.Name;
                            string TeamName = objCustomerTimelines.Team.Name;
                            string ApartmentNo = objCustomerTimelines.CustomerOfficalDetail.AppartmentNumber;
                            string Date = Convert.ToDateTime(objCustomerTimelines.StartDate).ToString("dd/MM/yyyy");
                            string body = EmailBodyCustomerClosingInfoForStaff(Area, PropertyName, CustomerName, CustomerMobile, CustomerEmail, TeamName, task.Remarks, CustomerID, CustomerTaskNo, ApartmentNo, Date);
                            string CustomerBody = EmailBodyCustomerClosingforCustomer(Area, PropertyName, CustomerName, CustomerMobile, CustomerEmail, TeamName, CustomerID, CustomerTaskNo, objCustomerTimelines.CustomerOfficalDetail.Remarks, ApartmentNo, Convert.ToDateTime(objCustomerTimelines.StartDate).ToString("dd/MM/yyyy"));
                            objGeneralDB.SentEmailFromAmazon(CustomerEmail, CustomerBody, CustomerTaskNo + " is completed for CustomerID : " + CustomerID, CustomerName);
                            objGeneralDB.SentEmailFromAmazon(AdminEmail, body, CustomerTaskNo + " is completed for CustomerID : " + CustomerID, AdminName);
                            var objTeams = UhDB.StaffTeams.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).ToList();
                            foreach (var item in objTeams)
                            {
                                int? OneStfID = item.stfID;
                                var objStaff = UhDB.Staffs.Where(x => x.stfID == OneStfID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                objGeneralDB.SentEmailFromAmazon(objStaff.Email, body, CustomerTaskNo + " is completed", objStaff.Name);
                            }
                        }
                    }
                    else
                    {
                        if (stfID != null)
                        {
                            var objCustomerOtherProperty = UhDB.CustomerOtherProperties.Where(x => x.custID == task.cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                            string Area = objCustomerTimelines.CustomerOfficalDetail.PropertyArea.Name;
                            string TowerName = objCustomerOtherProperty.TowerName;
                            string BuildingName = objCustomerOtherProperty.BuildingName;
                            string StreetNumber = objCustomerOtherProperty.StreetNumber;
                            string ZoneNumber = objCustomerOtherProperty.ZoneNumber;
                            string ApartmentNo = objCustomerTimelines.CustomerOfficalDetail.AppartmentNumber;
                            string Location = objCustomerOtherProperty.Loacation;
                            string LocationLink = objCustomerOtherProperty.LocationLink;
                            string StaffName = objCustomerTimelines.CustomerOfficalDetail.Staff.Name;
                            string StaffEmail = objCustomerTimelines.CustomerOfficalDetail.Staff.Email;
                            string AdminEmail = objCustomerTimelines.CustomerOfficalDetail.Customer.User.Email;
                            string AdminName = objCustomerTimelines.CustomerOfficalDetail.Customer.User.Name;
                            string body = EmailBodyCustomerInfoForClosingStaffForOtherProperty(CustomerName, CustomerMobile, CustomerEmail, Area, TowerName, BuildingName, StreetNumber, ZoneNumber, Location, LocationLink, StaffName, task.Remarks, CustomerID, CustomerTaskNo);
                            string CustomerBody = EmailBodyCustomerIClosingForOtherPropertyCustomer(CustomerName, CustomerMobile, CustomerEmail, Area, TowerName, BuildingName, StreetNumber, ZoneNumber, Location, LocationLink, StaffName, CustomerID, CustomerTaskNo, ApartmentNo, Convert.ToDateTime(objCustomerTimelines.StartDate).ToString("dd/MM/yyyy"));
                            objGeneralDB.SentEmailFromAmazon(CustomerEmail, CustomerBody, CustomerTaskNo + " is completed for CustomerID : " + CustomerID, CustomerName);
                            objGeneralDB.SentEmailFromAmazon(AdminEmail, body, CustomerTaskNo + " is completed for CustomerID : " + CustomerID, AdminName);
                            objGeneralDB.SentEmailFromAmazon(StaffEmail, body, CustomerTaskNo + " is completed", StaffName);
                        }
                        else
                        {

                            var objCustomerOtherProperty = UhDB.CustomerOtherProperties.Where(x => x.custID == task.cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                            string Area = objCustomerTimelines.CustomerOfficalDetail.PropertyArea.Name;
                            string TowerName = objCustomerOtherProperty.TowerName;
                            string BuildingName = objCustomerOtherProperty.BuildingName;
                            string StreetNumber = objCustomerOtherProperty.StreetNumber;
                            string ZoneNumber = objCustomerOtherProperty.ZoneNumber;
                            string Location = objCustomerOtherProperty.Loacation;
                            string LocationLink = objCustomerOtherProperty.LocationLink;
                            string ApartmentNo = objCustomerTimelines.CustomerOfficalDetail.AppartmentNumber;
                            string TeamName = objCustomerTimelines.Team.Name;
                            string AdminEmail = objCustomerTimelines.CustomerOfficalDetail.Customer.User.Email;
                            string AdminName = objCustomerTimelines.CustomerOfficalDetail.Customer.User.Name;
                            string body = EmailBodyCustomerInfoForClosingStaffForOtherProperty(CustomerName, CustomerMobile, CustomerEmail, Area, TowerName, BuildingName, StreetNumber, ZoneNumber, Location, LocationLink, TeamName, task.Remarks, CustomerID, CustomerTaskNo);
                            string CustomerBody = EmailBodyCustomerIClosingForOtherPropertyCustomer(CustomerName, CustomerMobile, CustomerEmail, Area, TowerName, BuildingName, StreetNumber, ZoneNumber, Location, LocationLink, TeamName, CustomerID, CustomerTaskNo, ApartmentNo, Convert.ToDateTime(objCustomerTimelines.StartDate).ToString("dd/MM/yyyy"));
                            objGeneralDB.SentEmailFromAmazon(CustomerEmail, CustomerBody, CustomerTaskNo + " is completed for CustomerID : " + CustomerID, CustomerName);
                            objGeneralDB.SentEmailFromAmazon(AdminEmail, body, CustomerTaskNo + " is completed for CustomerID : " + CustomerID, AdminName);
                            var objTeams = UhDB.StaffTeams.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).ToList();
                            foreach (var item in objTeams)
                            {
                                int? OneStfID = item.stfID;
                                var objStaff = UhDB.Staffs.Where(x => x.stfID == OneStfID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                objGeneralDB.SentEmailFromAmazon(objStaff.Email, body, CustomerTaskNo + " is completed", objStaff.Name);
                            }
                        }
                    }


                    trans.Commit();
                    result = "SUCCESS";
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    result = "Exception";

                }
            }
            return result;
        }

        public int? AssignedTeamAuto(DateTime? Date, string StartTime, string EndTime, int? Area, int? catID, int? catsubID, int? servcatID, int? servsubcatID, bool? SpecialService)
        {
            int? teamID = null;
            if (SpecialService == true)
            {
                var objPropetyAreaTeamAssign = UhDB.StaffServices.Where(x => x.propaID == Area && x.catID == catID && x.catsubID == catsubID && x.servcatID == servcatID && x.servsubcatID == servsubcatID).ToList();
                foreach (var item in objPropetyAreaTeamAssign)
                {
                    int? TempTeamID = item.teamID;
                    int CountTempTeamID = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.teamID == TempTeamID
                                          && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(Date)
                                          && x.StartTime == StartTime && x.EndTime == EndTime
                                          && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountTempTeamID == 0)
                    {
                        teamID = TempTeamID;
                    }
                    else
                    {
                        var objOtherPropertyAreaTeams = UhDB.StaffServices.Where(x => x.catID == catID && x.catsubID == catsubID && x.propaID != Area
                                                       && x.IsActive == true && x.IsDelete == false).ToList();
                        foreach (var objOtherPropertyAreaTeam in objOtherPropertyAreaTeams)
                        {
                            TempTeamID = objOtherPropertyAreaTeam.teamID;
                            TimeSpan NewStartTime = TimeSpan.Parse(StartTime);
                            TimeSpan NewEndTime = TimeSpan.Parse(EndTime);
                            TimeSpan C1 = new TimeSpan(0, 15, 0);
                            TimeSpan AddStartTime = NewStartTime.Subtract(C1);
                            TimeSpan SubtractEndtime = NewEndTime.Add(C1);
                            string StringAddStartTime = AddStartTime.ToString();
                            string StringSubtractEndtime = SubtractEndtime.ToString();
                            CountTempTeamID = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.teamID == TempTeamID
                                              && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(Date)
                                              && x.StartTime == StringAddStartTime && x.EndTime == StringSubtractEndtime
                                              && x.IsActive == true && x.IsDelete == false).Count();
                            if (CountTempTeamID == 0)
                            {
                                teamID = TempTeamID;
                            }
                        }

                    }
                }
            }
            else
            {
                var objPropetyAreaTeamAssign = UhDB.StaffServices.Where(x => x.propaID == Area && x.catID == catID && x.catsubID == catsubID).ToList();
                foreach (var item in objPropetyAreaTeamAssign)
                {
                    int? TempTeamID = item.teamID;
                    int CountTempTeamID = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.teamID == TempTeamID
                                          && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(Date)
                                          && x.StartTime == StartTime && x.EndTime == EndTime
                                          && x.IsActive == true && x.IsDelete == false).Count();
                    if (CountTempTeamID == 0)
                    {
                        teamID = TempTeamID;
                    }
                    else
                    {
                        var objOtherPropertyAreaTeams = UhDB.StaffServices.Where(x => x.catID == catID && x.catsubID == catsubID && x.propaID != Area
                                                       && x.IsActive == true && x.IsDelete == false).ToList();
                        foreach (var objOtherPropertyAreaTeam in objOtherPropertyAreaTeams)
                        {
                            TempTeamID = objOtherPropertyAreaTeam.teamID;
                            TimeSpan NewStartTime = TimeSpan.Parse(StartTime);
                            TimeSpan NewEndTime = TimeSpan.Parse(EndTime);
                            TimeSpan C1 = new TimeSpan(0, 15, 0);
                            TimeSpan AddStartTime = NewStartTime.Subtract(C1);
                            TimeSpan SubtractEndtime = NewEndTime.Add(C1);
                            string StringAddStartTime = AddStartTime.ToString();
                            string StringSubtractEndtime = SubtractEndtime.ToString();
                            CountTempTeamID = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.teamID == TempTeamID
                                              && EntityFunctions.TruncateTime(x.StartDate) == EntityFunctions.TruncateTime(Date)
                                              && x.StartTime == StringAddStartTime && x.EndTime == StringSubtractEndtime
                                              && x.IsActive == true && x.IsDelete == false).Count();
                            if (CountTempTeamID == 0)
                            {
                                teamID = TempTeamID;
                            }
                        }

                    }
                }
            }
            return teamID;
        }

        private int GetNextWeekday(int firstDay, int secondDay)
        {
            int firstDayValue = (int)firstDay;
            int secondDayValue = (int)secondDay;

            // If the first day is before the second day in the week
            if (firstDayValue < secondDayValue)
            {
                return secondDayValue - firstDayValue;
            }
            // If the first day is after the second day in the week
            else
            {
                return 9 - firstDayValue + secondDayValue;
            }
        }

        public string CreateInvoice(PackageDetailsModel details)
        {
            string result = null;
            HtmlToPdf htmlToPdf = new HtmlToPdf();
            htmlToPdf.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

            Handlebars.RegisterHelper("formatPrice", (writer, context, parameters) =>
            {
                string price = parameters[0]?.ToString() ?? string.Empty;
                writer.WriteSafeString($"{price} QR");
            });
            string source =
                            @"<html><head>
                                <style>
                                    @import url(""https://fonts.googleapis.com/css2?family=Inter:wght@400;600;700&display=swap"");*,::after,::before,legend{-webkit-box-sizing:border-box}code,kbd,pre,samp{font-family:monospace,monospace}button,hr,input{overflow:visible}.cs-table_baseline,progress,sub,sup{vertical-align:baseline}ol,ul{padding-left:20px}blockquote,cite,dfn,em,i{font-style:italic}img,legend{max-width:100%}a,a:hover{text-decoration:none}a,button,legend{color:inherit}.cs-accent_color,.cs-accent_color_hover:hover,a:hover{color:#2ad19d}.cs-bar_list,.cs-bar_list li,.cs-special_item,sub,sup{position:relative}*,::after,::before{box-sizing:border-box}html{-webkit-text-size-adjust:100%}.cs-m0,body{margin:0}details,main{display:block}hr{-webkit-box-sizing:content-box;box-sizing:content-box;height:0}a{background-color:transparent;-webkit-transition:.3s;transition:.3s}abbr[title]{border-bottom:none;text-decoration:underline;-webkit-text-decoration:underline dotted;text-decoration:underline dotted}code,kbd,samp{font-size:1em}small{font-size:80%}sub,sup{font-size:75%;line-height:0}sub{bottom:-.25em}sup{top:-.5em}button,input,optgroup,select,textarea{font-family:inherit;font-size:100%;line-height:1.15;margin:0}button,select{text-transform:none}[type=button],[type=reset],[type=submit],button{-webkit-appearance:button}[type=button]::-moz-focus-inner,[type=reset]::-moz-focus-inner,[type=submit]::-moz-focus-inner,button::-moz-focus-inner{border-style:none;padding:0}[type=button]:-moz-focusring,[type=reset]:-moz-focusring,[type=submit]:-moz-focusring,button:-moz-focusring{outline:ButtonText dotted 1px}fieldset{padding:.35em .75em .625em}legend{box-sizing:border-box;display:table;padding:0;white-space:normal}textarea{overflow:auto}[type=checkbox],[type=radio]{-webkit-box-sizing:border-box;box-sizing:border-box;padding:0}[type=number]::-webkit-inner-spin-button,[type=number]::-webkit-outer-spin-button{height:auto}[type=search]{-webkit-appearance:textfield;outline-offset:-2px}[type=search]::-webkit-search-decoration{-webkit-appearance:none}::-webkit-file-upload-button{-webkit-appearance:button;font:inherit}summary{display:list-item}[hidden],template{display:none}body,html{color:#777;font-family:Inter,sans-serif;font-size:14px;font-weight:400;line-height:1.5em;overflow-x:hidden;background-color:#f5f7ff}h1,h2,h3,h4,h5,h6{clear:both;color:#111;padding:0;margin:0 0 20px;font-weight:500;line-height:1.2em}h1{font-size:60px}h2{font-size:48px}h3{font-size:30px}.cs-f24,h4{font-size:24px}.cs-f18,h5{font-size:18px}.cs-f16,h6{font-size:16px}div,p{margin-top:0;line-height:1.5em}.cs-mb15,p{margin-bottom:15px}ul{margin:0 0 25px;list-style:square}ol{margin-bottom:25px}blockquote{margin:0;font-size:20px;line-height:1.6em}address{margin:0 0 15px}img{border:0;height:auto;vertical-align:middle}a:hover{color:inherit}button{-webkit-transition:.3s;transition:.3s}table{width:100%;caption-side:bottom;border-collapse:collapse}th{text-align:left}.cs-border_top,td{border-top:1px solid #eaeaea}td,th{padding:10px 15px;line-height:1.55em}.cs-mb25,dl{margin-bottom:25px}.cs-semi_bold,dl dt{font-weight:600}.cs-bold,b,strong{font-weight:700}pre{color:#777;border:1px solid #eaeaea;font-size:18px;padding:25px;border-radius:5px}kbd{font-size:100%;background-color:#777;border-radius:5px}.cs-f10{font-size:10px}.cs-f11{font-size:11px}.cs-f12{font-size:12px}.cs-f13{font-size:13px}.cs-f14{font-size:14px}.cs-f15{font-size:15px}.cs-f17{font-size:17px}.cs-f19{font-size:19px}.cs-f20{font-size:20px}.cs-f21{font-size:21px}.cs-f22{font-size:22px}.cs-f23{font-size:23px}.cs-f25{font-size:25px}.cs-f26{font-size:26px}.cs-f27{font-size:27px}.cs-f28{font-size:28px}.cs-f29{font-size:29px}.cs-light{font-weight:300}.cs-normal{font-weight:400}.cs-medium{font-weight:500}.cs-mb0{margin-bottom:0}.cs-mb1{margin-bottom:1px}.cs-mb2{margin-bottom:2px}.cs-mb3{margin-bottom:3px}.cs-mb4{margin-bottom:4px}.cs-mb5{margin-bottom:5px}.cs-mb6{margin-bottom:6px}.cs-mb7{margin-bottom:7px}.cs-mb8{margin-bottom:8px}.cs-mb9{margin-bottom:9px}.cs-bar_list li:not(:last-child),.cs-mb10{margin-bottom:10px}.cs-mb11{margin-bottom:11px}.cs-mb12{margin-bottom:12px}.cs-mb13{margin-bottom:13px}.cs-mb14{margin-bottom:14px}.cs-mb16{margin-bottom:16px}.cs-mb17{margin-bottom:17px}.cs-mb18{margin-bottom:18px}.cs-mb19{margin-bottom:19px}.cs-mb20{margin-bottom:20px}.cs-mb21{margin-bottom:21px}.cs-mb22{margin-bottom:22px}.cs-mb23{margin-bottom:23px}.cs-mb24{margin-bottom:24px}.cs-mb26{margin-bottom:26px}.cs-mb27{margin-bottom:27px}.cs-mb28{margin-bottom:28px}.cs-mb29{margin-bottom:29px}.cs-mb30{margin-bottom:30px}.cs-pt25{padding-top:25px}.cs-width_1{width:8.33333333%}.cs-width_2{width:16.66666667%}.cs-width_3{width:25%}.cs-width_4{width:33.33333333%}.cs-width_5{width:41.66666667%}.cs-width_6{width:50%}.cs-width_7{width:58.33333333%}.cs-width_8{width:66.66666667%}.cs-width_9{width:75%}.cs-width_10{width:83.33333333%}.cs-width_11{width:91.66666667%}.cs-width_12{width:100%}.cs-accent_bg,.cs-accent_bg_hover:hover{background-color:#2ad19d}.cs-primary_color{color:#111}.cs-secondary_color{color:#777}.cs-ternary_color{color:#353535;border-color:#eaeaea}.cs-focus_bg{background:#f6f6f6}.cs-accent_10_bg{background-color:rgba(42,209,157,.1)}.cs-container{max-width:880px;padding:30px 15px;margin-left:auto;margin-right:auto}.cs-text_center{text-align:center}.cs-text_right{text-align:right}.cs-border_bottom_0{border-bottom:0}.cs-border_bottom,.cs-heading.cs-style1{border-bottom:1px solid #eaeaea}.cs-border_top_0{border-top:0}.cs-border_left{border-left:1px solid #eaeaea}.cs-border_right{border-right:1px solid #eaeaea}.cs-round_border{border:1px solid #eaeaea;overflow:hidden;border-radius:6px}.cs-border_none,.cs-table.cs-style2 td{border:none}.cs-border_left_none{border-left-width:0}.cs-border_right_none{border-right-width:0}.cs-invoice.cs-style1{background:#fff;border-radius:10px;padding:50px}.cs-invoice.cs-style1 .cs-invoice_head{display:-webkit-box;display:-ms-flexbox;display:flex;-webkit-box-pack:justify;-ms-flex-pack:justify;justify-content:space-between}.cs-invoice.cs-style1 .cs-invoice_head.cs-type1{-webkit-box-align:end;-ms-flex-align:end;align-items:flex-end;padding-bottom:25px;border-bottom:1px solid #eaeaea}.cs-invoice.cs-style1 .cs-invoice_footer,.cs-list.cs-style1 li,.cs-list.cs-style2 li,.cs-ticket_wrap{display:-webkit-box;display:-ms-flexbox;display:flex}.cs-invoice.cs-style1 .cs-invoice_footer table{margin-top:-1px}.cs-invoice.cs-style1 .cs-left_footer{width:55%;padding:10px 15px}.cs-invoice.cs-style1 .cs-right_footer{width:46%}.cs-invoice.cs-style1 .cs-note{display:-webkit-box;display:-ms-flexbox;display:flex;-webkit-box-align:start;-ms-flex-align:start;align-items:flex-start;margin-top:40px}.cs-invoice.cs-style1 .cs-note_left{margin-right:10px;margin-top:-25px;margin-left:-5px;display:-webkit-box;display:-ms-flexbox;display:flex}.cs-invoice.cs-style1 .cs-note_left svg{width:32px}.cs-invoice.cs-style1 .cs-invoice_left{max-width:55%}.cs-invoice_btns{display:-webkit-box;display:-ms-flexbox;display:flex;-webkit-box-pack:center;-ms-flex-pack:center;justify-content:center;margin-top:30px}.cs-invoice_btns .cs-invoice_btn:first-child{border-radius:5px 0 0 5px}.cs-invoice_btns .cs-invoice_btn:last-child{border-radius:0 5px 5px 0}.cs-invoice_btn{display:-webkit-inline-box;display:-ms-inline-flexbox;display:inline-flex;-webkit-box-align:center;-ms-flex-align:center;align-items:center;border:none;font-weight:600;padding:8px 20px;cursor:pointer}.cs-bar_list,.cs-grid_row,.cs-list.cs-style1{padding:0;list-style:none}.cs-invoice_btn svg{width:24px;margin-right:5px}.cs-invoice_btn.cs-color1{color:#111;background:rgba(42,209,157,.15)}.cs-invoice_btn.cs-color1:hover{background-color:rgba(42,209,157,.3)}.cs-invoice_btn.cs-color2{color:#fff;background:#2ad19d}.cs-bar_list li:before,.cs-bar_list::before,.cs-special_item:after{content:'';background-color:#eaeaea;position:absolute}.cs-invoice_btn.cs-color2:hover{background-color:rgba(42,209,157,.8)}.cs-table_responsive{overflow-x:auto}.cs-table_responsive>table{min-width:600px}.cs-50_col>*{width:50%;-webkit-box-flex:0;-ms-flex:none;flex:none}.cs-bar_list{margin:0}.cs-bar_list::before{height:75%;width:2px;left:4px;top:50%;-webkit-transform:translateY(-50%);transform:translateY(-50%)}.cs-bar_list li{padding-left:25px}.cs-bar_list li:before{height:10px;width:10px;border-radius:50%;left:0;top:6px}.cs-table.cs-style1.cs-type1{padding:10px 30px}.cs-table.cs-style1.cs-type1 tr:first-child td{border-top:none}.cs-table.cs-style1.cs-type1 tr td:first-child{padding-left:0}.cs-table.cs-style1.cs-type1 tr td:last-child{padding-right:0}.cs-table.cs-style1.cs-type2>*{padding:0 10px}.cs-table.cs-style1.cs-type2 .cs-table_title{padding:20px 0 0 15px;margin-bottom:-5px}.cs-table.cs-style2 td,.cs-table.cs-style2 th{padding:12px 15px;line-height:1.55em}.cs-table.cs-style2 tr:not(:first-child){border-top:1px dashed #eaeaea}.cs-list.cs-style1{margin:0}.cs-list.cs-style1 li:not(:last-child){border-bottom:1px dashed #eaeaea}.cs-list.cs-style1 li>*{-webkit-box-flex:0;-ms-flex:none;flex:none;width:50%;padding:7px 0}.cs-list.cs-style2{list-style:none;margin:0 0 30px;padding:12px 0;border:1px solid #eaeaea;border-radius:5px}.cs-list.cs-style2 li>*{-webkit-box-flex:1;-ms-flex:1;flex:1;padding:5px 25px}.cs-heading.cs-style1{line-height:1.5em;border-top:1px solid #eaeaea;padding:10px 0}.cs-no_border{border:none!important}.cs-grid_row{display:-ms-grid;display:grid;grid-gap:20px}.cs-col_2{-ms-grid-columns:(1fr)[2];grid-template-columns:repeat(2,1fr)}.cs-col_3{-ms-grid-columns:(1fr)[3];grid-template-columns:repeat(3,1fr)}.cs-col_4{-ms-grid-columns:(1fr)[4];grid-template-columns:repeat(4,1fr)}.cs-border_less td{border-color:transparent}.cs-special_item:after{height:52px;width:1px;top:50%;-webkit-transform:translateY(-50%);transform:translateY(-50%);right:0}.cs-table.cs-style1 .cs-table.cs-style1 tr:not(:first-child) td{border-color:#eaeaea}.cs-box.cs-style2 .cs-table.cs-style2 td,.cs-table.cs-style1 .cs-table.cs-style2 td{padding:12px 0}.cs-ticket_left{-webkit-box-flex:1;-ms-flex:1;flex:1}.cs-ticket_right{-webkit-box-flex:0;-ms-flex:none;flex:none;width:215px}.cs-box.cs-style1{border:2px solid #eaeaea;border-radius:5px;padding:20px 10px;min-width:150px}.cs-box.cs-style1.cs-type1{padding:12px 10px 10px}.cs-max_w_150{max-width:150px}.cs-left_auto{margin-left:auto}.cs-title_1{display:inline-block;border-bottom:1px solid #eaeaea;min-width:60%;padding-bottom:5px;margin-bottom:10px}.cs-box2_wrap{display:-ms-grid;display:grid;grid-gap:30px;list-style:none;padding:0;-ms-grid-columns:(1fr)[2];grid-template-columns:repeat(2,1fr)}.cs-box.cs-style2{border:1px solid #eaeaea;padding:25px 30px;border-radius:5px}@media print{.cs-hide_print{display:none!important}}@media (max-width:767px){.cs-mobile_hide{display:none}.cs-invoice.cs-style1{padding:30px 20px}.cs-invoice.cs-style1 .cs-right_footer{width:100%}}@media (max-width:500px){.cs-invoice.cs-style1 .cs-logo{margin-bottom:10px}.cs-invoice.cs-style1 .cs-invoice_head,.cs-list.cs-style2 li{-webkit-box-orient:vertical;-webkit-box-direction:normal;-ms-flex-direction:column;flex-direction:column}.cs-invoice.cs-style1 .cs-invoice_head.cs-type1{-webkit-box-orient:vertical;-webkit-box-direction:reverse;-ms-flex-direction:column-reverse;flex-direction:column-reverse;-webkit-box-align:center;-ms-flex-align:center;align-items:center;text-align:center}.cs-invoice.cs-style1 .cs-invoice_head .cs-text_right{text-align:left}.cs-list.cs-style2 li>*{padding:5px 20px}.cs-grid_row{grid-gap:0px}.cs-box2_wrap,.cs-col_2,.cs-col_3,.cs-col_4{-ms-grid-columns:(1fr)[1];grid-template-columns:repeat(1,1fr)}.cs-table.cs-style1.cs-type1{padding:0 20px}.cs-box.cs-style1.cs-type1{max-width:100%;width:100%}.cs-invoice.cs-style1 .cs-invoice_left{max-width:100%}}
                                </style>    
                            </head><body>
                            <div class=""cs-container"">
                            <div class=""cs-invoice cs-style1"">
                            <div class=""cs-invoice_in"" id=""download_section"">
                            <div class=""cs-invoice_head cs-type1 cs-mb25"">
                            <div class=""cs-invoice_left"">
                            <div class=""cs-logo cs-mb5""><img src=""https://uhs.detentech.com/Images/logo.png"" style=""height:100px;"" alt=""Logo""></div>
                            <p class=""mt-1 mb-1""><b>Urban Hospitality Services</b></p>
                            <p>2nd Floor, Al Hitmi Building, C-Ring Road, P.O Box: 7218,<br /> Doha - Qatar</p>
                            </div>
                            <div class=""cs-invoice_right cs-text_right"">
                                <p class=""cs-invoice_number cs-primary_color cs-mb5 cs-f16""><b class=""cs - primary_color"">Invoice : {{Items.[0].InVoice}} </b></p>
                                  <p class=""cs-invoice_date cs-primary_color""><b class=""cs-primary_color"">Date: </b>{{today}}</p>

                            </div>
                            </div>
                            <div class=""cs-table cs-style1"">
                            <div class=""cs-round_border"">
                                <div class=""cs-table_responsive"">
                                   <table>
                                        <thead>
                                            <tr>
                                                <th class=""cs-width_8 cs-semi_bold cs-primary_color cs-focus_bg"">Service</th>
                                                <th class=""cs-width_8 cs-semi_bold cs-primary_color cs-focus_bg"">Frequency</th>
                                                <th class=""cs-width_4 cs-semi_bold cs-primary_color cs-focus_bg"">Area</th>
                                                <th class=""cs-width_4 cs-semi_bold cs-primary_color cs-focus_bg"">Property Name</th>
                                                <th class=""cs-width_2 cs-semi_bold cs-primary_color cs-focus_bg"">Residential Type</th>
                                                 <th class=""cs-width_4 cs-semi_bold cs-primary_color cs-focus_bg"">No Of Months</th>
                                                <th class=""cs-width_4 cs-semi_bold cs-primary_color cs-focus_bg"">Price</th>
                                                <th class=""cs-width_4 cs-semi_bold cs-primary_color cs-focus_bg"">Total Price</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            {{#each Items}}
                                            <tr>
                                                <td class=""cs-width_8"">{{SubCategoryName}}</td>
                                                <td class=""cs-width_8"">{{PackageName}}</td>
                                                <td class=""cs-width_4"">{{AreaName}}</td>
                                                <td class=""cs-width_4"">{{PropName}}</td>
                                                <td class=""cs-width_2"">{{resdName}}</td>
                                                <td class=""cs-width_4"">{{NoOfMonths}}</td>
                                                <td class=""cs-width_4"">{{formatPrice Price}}</td>
                                                <td class=""cs-width_4"">{{formatPrice TotalPrice}}</td>
                                            </tr>
                                            {{/each}}
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            </div>
                            </div>
                            </div>
                            </div>
                            </body>
                            </html>";

            var template = Handlebars.Compile(source);
            DateTime today = DateTime.Today;
            var data = new
            {
                today = today.ToString("dd MMMM,yyyy"),
                Items = new[] { details }
            };
            var getdata = template(data);
            PdfDocument pdfDocument = htmlToPdf.ConvertHtmlString(getdata);
            result = ConvertHtmlToBase64Pdf(getdata);

            return result;
        }

        public string CreateInvoiceV2(PackageDetailsModelV2 details)
        {
            string result = null;
            HtmlToPdf htmlToPdf = new HtmlToPdf();
            htmlToPdf.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

            Handlebars.RegisterHelper("formatPrice", (writer, context, parameters) =>
            {
                string price = parameters[0]?.ToString() ?? string.Empty;
                writer.WriteSafeString($"{price} QR");
            });
            string source =
                            @"<html><head>
                                <style>
                                    @import url(""https://fonts.googleapis.com/css2?family=Inter:wght@400;600;700&display=swap"");*,::after,::before,legend{-webkit-box-sizing:border-box}code,kbd,pre,samp{font-family:monospace,monospace}button,hr,input{overflow:visible}.cs-table_baseline,progress,sub,sup{vertical-align:baseline}ol,ul{padding-left:20px}blockquote,cite,dfn,em,i{font-style:italic}img,legend{max-width:100%}a,a:hover{text-decoration:none}a,button,legend{color:inherit}.cs-accent_color,.cs-accent_color_hover:hover,a:hover{color:#2ad19d}.cs-bar_list,.cs-bar_list li,.cs-special_item,sub,sup{position:relative}*,::after,::before{box-sizing:border-box}html{-webkit-text-size-adjust:100%}.cs-m0,body{margin:0}details,main{display:block}hr{-webkit-box-sizing:content-box;box-sizing:content-box;height:0}a{background-color:transparent;-webkit-transition:.3s;transition:.3s}abbr[title]{border-bottom:none;text-decoration:underline;-webkit-text-decoration:underline dotted;text-decoration:underline dotted}code,kbd,samp{font-size:1em}small{font-size:80%}sub,sup{font-size:75%;line-height:0}sub{bottom:-.25em}sup{top:-.5em}button,input,optgroup,select,textarea{font-family:inherit;font-size:100%;line-height:1.15;margin:0}button,select{text-transform:none}[type=button],[type=reset],[type=submit],button{-webkit-appearance:button}[type=button]::-moz-focus-inner,[type=reset]::-moz-focus-inner,[type=submit]::-moz-focus-inner,button::-moz-focus-inner{border-style:none;padding:0}[type=button]:-moz-focusring,[type=reset]:-moz-focusring,[type=submit]:-moz-focusring,button:-moz-focusring{outline:ButtonText dotted 1px}fieldset{padding:.35em .75em .625em}legend{box-sizing:border-box;display:table;padding:0;white-space:normal}textarea{overflow:auto}[type=checkbox],[type=radio]{-webkit-box-sizing:border-box;box-sizing:border-box;padding:0}[type=number]::-webkit-inner-spin-button,[type=number]::-webkit-outer-spin-button{height:auto}[type=search]{-webkit-appearance:textfield;outline-offset:-2px}[type=search]::-webkit-search-decoration{-webkit-appearance:none}::-webkit-file-upload-button{-webkit-appearance:button;font:inherit}summary{display:list-item}[hidden],template{display:none}body,html{color:#777;font-family:Inter,sans-serif;font-size:14px;font-weight:400;line-height:1.5em;overflow-x:hidden;background-color:#f5f7ff}h1,h2,h3,h4,h5,h6{clear:both;color:#111;padding:0;margin:0 0 20px;font-weight:500;line-height:1.2em}h1{font-size:60px}h2{font-size:48px}h3{font-size:30px}.cs-f24,h4{font-size:24px}.cs-f18,h5{font-size:18px}.cs-f16,h6{font-size:16px}div,p{margin-top:0;line-height:1.5em}.cs-mb15,p{margin-bottom:15px}ul{margin:0 0 25px;list-style:square}ol{margin-bottom:25px}blockquote{margin:0;font-size:20px;line-height:1.6em}address{margin:0 0 15px}img{border:0;height:auto;vertical-align:middle}a:hover{color:inherit}button{-webkit-transition:.3s;transition:.3s}table{width:100%;caption-side:bottom;border-collapse:collapse}th{text-align:left}.cs-border_top,td{border-top:1px solid #eaeaea}td,th{padding:10px 15px;line-height:1.55em}.cs-mb25,dl{margin-bottom:25px}.cs-semi_bold,dl dt{font-weight:600}.cs-bold,b,strong{font-weight:700}pre{color:#777;border:1px solid #eaeaea;font-size:18px;padding:25px;border-radius:5px}kbd{font-size:100%;background-color:#777;border-radius:5px}.cs-f10{font-size:10px}.cs-f11{font-size:11px}.cs-f12{font-size:12px}.cs-f13{font-size:13px}.cs-f14{font-size:14px}.cs-f15{font-size:15px}.cs-f17{font-size:17px}.cs-f19{font-size:19px}.cs-f20{font-size:20px}.cs-f21{font-size:21px}.cs-f22{font-size:22px}.cs-f23{font-size:23px}.cs-f25{font-size:25px}.cs-f26{font-size:26px}.cs-f27{font-size:27px}.cs-f28{font-size:28px}.cs-f29{font-size:29px}.cs-light{font-weight:300}.cs-normal{font-weight:400}.cs-medium{font-weight:500}.cs-mb0{margin-bottom:0}.cs-mb1{margin-bottom:1px}.cs-mb2{margin-bottom:2px}.cs-mb3{margin-bottom:3px}.cs-mb4{margin-bottom:4px}.cs-mb5{margin-bottom:5px}.cs-mb6{margin-bottom:6px}.cs-mb7{margin-bottom:7px}.cs-mb8{margin-bottom:8px}.cs-mb9{margin-bottom:9px}.cs-bar_list li:not(:last-child),.cs-mb10{margin-bottom:10px}.cs-mb11{margin-bottom:11px}.cs-mb12{margin-bottom:12px}.cs-mb13{margin-bottom:13px}.cs-mb14{margin-bottom:14px}.cs-mb16{margin-bottom:16px}.cs-mb17{margin-bottom:17px}.cs-mb18{margin-bottom:18px}.cs-mb19{margin-bottom:19px}.cs-mb20{margin-bottom:20px}.cs-mb21{margin-bottom:21px}.cs-mb22{margin-bottom:22px}.cs-mb23{margin-bottom:23px}.cs-mb24{margin-bottom:24px}.cs-mb26{margin-bottom:26px}.cs-mb27{margin-bottom:27px}.cs-mb28{margin-bottom:28px}.cs-mb29{margin-bottom:29px}.cs-mb30{margin-bottom:30px}.cs-pt25{padding-top:25px}.cs-width_1{width:8.33333333%}.cs-width_2{width:16.66666667%}.cs-width_3{width:25%}.cs-width_4{width:33.33333333%}.cs-width_5{width:41.66666667%}.cs-width_6{width:50%}.cs-width_7{width:58.33333333%}.cs-width_8{width:66.66666667%}.cs-width_9{width:75%}.cs-width_10{width:83.33333333%}.cs-width_11{width:91.66666667%}.cs-width_12{width:100%}.cs-accent_bg,.cs-accent_bg_hover:hover{background-color:#2ad19d}.cs-primary_color{color:#111}.cs-secondary_color{color:#777}.cs-ternary_color{color:#353535;border-color:#eaeaea}.cs-focus_bg{background:#f6f6f6}.cs-accent_10_bg{background-color:rgba(42,209,157,.1)}.cs-container{max-width:880px;padding:30px 15px;margin-left:auto;margin-right:auto}.cs-text_center{text-align:center}.cs-text_right{text-align:right}.cs-border_bottom_0{border-bottom:0}.cs-border_bottom,.cs-heading.cs-style1{border-bottom:1px solid #eaeaea}.cs-border_top_0{border-top:0}.cs-border_left{border-left:1px solid #eaeaea}.cs-border_right{border-right:1px solid #eaeaea}.cs-round_border{border:1px solid #eaeaea;overflow:hidden;border-radius:6px}.cs-border_none,.cs-table.cs-style2 td{border:none}.cs-border_left_none{border-left-width:0}.cs-border_right_none{border-right-width:0}.cs-invoice.cs-style1{background:#fff;border-radius:10px;padding:50px}.cs-invoice.cs-style1 .cs-invoice_head{display:-webkit-box;display:-ms-flexbox;display:flex;-webkit-box-pack:justify;-ms-flex-pack:justify;justify-content:space-between}.cs-invoice.cs-style1 .cs-invoice_head.cs-type1{-webkit-box-align:end;-ms-flex-align:end;align-items:flex-end;padding-bottom:25px;border-bottom:1px solid #eaeaea}.cs-invoice.cs-style1 .cs-invoice_footer,.cs-list.cs-style1 li,.cs-list.cs-style2 li,.cs-ticket_wrap{display:-webkit-box;display:-ms-flexbox;display:flex}.cs-invoice.cs-style1 .cs-invoice_footer table{margin-top:-1px}.cs-invoice.cs-style1 .cs-left_footer{width:55%;padding:10px 15px}.cs-invoice.cs-style1 .cs-right_footer{width:46%}.cs-invoice.cs-style1 .cs-note{display:-webkit-box;display:-ms-flexbox;display:flex;-webkit-box-align:start;-ms-flex-align:start;align-items:flex-start;margin-top:40px}.cs-invoice.cs-style1 .cs-note_left{margin-right:10px;margin-top:-25px;margin-left:-5px;display:-webkit-box;display:-ms-flexbox;display:flex}.cs-invoice.cs-style1 .cs-note_left svg{width:32px}.cs-invoice.cs-style1 .cs-invoice_left{max-width:55%}.cs-invoice_btns{display:-webkit-box;display:-ms-flexbox;display:flex;-webkit-box-pack:center;-ms-flex-pack:center;justify-content:center;margin-top:30px}.cs-invoice_btns .cs-invoice_btn:first-child{border-radius:5px 0 0 5px}.cs-invoice_btns .cs-invoice_btn:last-child{border-radius:0 5px 5px 0}.cs-invoice_btn{display:-webkit-inline-box;display:-ms-inline-flexbox;display:inline-flex;-webkit-box-align:center;-ms-flex-align:center;align-items:center;border:none;font-weight:600;padding:8px 20px;cursor:pointer}.cs-bar_list,.cs-grid_row,.cs-list.cs-style1{padding:0;list-style:none}.cs-invoice_btn svg{width:24px;margin-right:5px}.cs-invoice_btn.cs-color1{color:#111;background:rgba(42,209,157,.15)}.cs-invoice_btn.cs-color1:hover{background-color:rgba(42,209,157,.3)}.cs-invoice_btn.cs-color2{color:#fff;background:#2ad19d}.cs-bar_list li:before,.cs-bar_list::before,.cs-special_item:after{content:'';background-color:#eaeaea;position:absolute}.cs-invoice_btn.cs-color2:hover{background-color:rgba(42,209,157,.8)}.cs-table_responsive{overflow-x:auto}.cs-table_responsive>table{min-width:600px}.cs-50_col>*{width:50%;-webkit-box-flex:0;-ms-flex:none;flex:none}.cs-bar_list{margin:0}.cs-bar_list::before{height:75%;width:2px;left:4px;top:50%;-webkit-transform:translateY(-50%);transform:translateY(-50%)}.cs-bar_list li{padding-left:25px}.cs-bar_list li:before{height:10px;width:10px;border-radius:50%;left:0;top:6px}.cs-table.cs-style1.cs-type1{padding:10px 30px}.cs-table.cs-style1.cs-type1 tr:first-child td{border-top:none}.cs-table.cs-style1.cs-type1 tr td:first-child{padding-left:0}.cs-table.cs-style1.cs-type1 tr td:last-child{padding-right:0}.cs-table.cs-style1.cs-type2>*{padding:0 10px}.cs-table.cs-style1.cs-type2 .cs-table_title{padding:20px 0 0 15px;margin-bottom:-5px}.cs-table.cs-style2 td,.cs-table.cs-style2 th{padding:12px 15px;line-height:1.55em}.cs-table.cs-style2 tr:not(:first-child){border-top:1px dashed #eaeaea}.cs-list.cs-style1{margin:0}.cs-list.cs-style1 li:not(:last-child){border-bottom:1px dashed #eaeaea}.cs-list.cs-style1 li>*{-webkit-box-flex:0;-ms-flex:none;flex:none;width:50%;padding:7px 0}.cs-list.cs-style2{list-style:none;margin:0 0 30px;padding:12px 0;border:1px solid #eaeaea;border-radius:5px}.cs-list.cs-style2 li>*{-webkit-box-flex:1;-ms-flex:1;flex:1;padding:5px 25px}.cs-heading.cs-style1{line-height:1.5em;border-top:1px solid #eaeaea;padding:10px 0}.cs-no_border{border:none!important}.cs-grid_row{display:-ms-grid;display:grid;grid-gap:20px}.cs-col_2{-ms-grid-columns:(1fr)[2];grid-template-columns:repeat(2,1fr)}.cs-col_3{-ms-grid-columns:(1fr)[3];grid-template-columns:repeat(3,1fr)}.cs-col_4{-ms-grid-columns:(1fr)[4];grid-template-columns:repeat(4,1fr)}.cs-border_less td{border-color:transparent}.cs-special_item:after{height:52px;width:1px;top:50%;-webkit-transform:translateY(-50%);transform:translateY(-50%);right:0}.cs-table.cs-style1 .cs-table.cs-style1 tr:not(:first-child) td{border-color:#eaeaea}.cs-box.cs-style2 .cs-table.cs-style2 td,.cs-table.cs-style1 .cs-table.cs-style2 td{padding:12px 0}.cs-ticket_left{-webkit-box-flex:1;-ms-flex:1;flex:1}.cs-ticket_right{-webkit-box-flex:0;-ms-flex:none;flex:none;width:215px}.cs-box.cs-style1{border:2px solid #eaeaea;border-radius:5px;padding:20px 10px;min-width:150px}.cs-box.cs-style1.cs-type1{padding:12px 10px 10px}.cs-max_w_150{max-width:150px}.cs-left_auto{margin-left:auto}.cs-title_1{display:inline-block;border-bottom:1px solid #eaeaea;min-width:60%;padding-bottom:5px;margin-bottom:10px}.cs-box2_wrap{display:-ms-grid;display:grid;grid-gap:30px;list-style:none;padding:0;-ms-grid-columns:(1fr)[2];grid-template-columns:repeat(2,1fr)}.cs-box.cs-style2{border:1px solid #eaeaea;padding:25px 30px;border-radius:5px}@media print{.cs-hide_print{display:none!important}}@media (max-width:767px){.cs-mobile_hide{display:none}.cs-invoice.cs-style1{padding:30px 20px}.cs-invoice.cs-style1 .cs-right_footer{width:100%}}@media (max-width:500px){.cs-invoice.cs-style1 .cs-logo{margin-bottom:10px}.cs-invoice.cs-style1 .cs-invoice_head,.cs-list.cs-style2 li{-webkit-box-orient:vertical;-webkit-box-direction:normal;-ms-flex-direction:column;flex-direction:column}.cs-invoice.cs-style1 .cs-invoice_head.cs-type1{-webkit-box-orient:vertical;-webkit-box-direction:reverse;-ms-flex-direction:column-reverse;flex-direction:column-reverse;-webkit-box-align:center;-ms-flex-align:center;align-items:center;text-align:center}.cs-invoice.cs-style1 .cs-invoice_head .cs-text_right{text-align:left}.cs-list.cs-style2 li>*{padding:5px 20px}.cs-grid_row{grid-gap:0px}.cs-box2_wrap,.cs-col_2,.cs-col_3,.cs-col_4{-ms-grid-columns:(1fr)[1];grid-template-columns:repeat(1,1fr)}.cs-table.cs-style1.cs-type1{padding:0 20px}.cs-box.cs-style1.cs-type1{max-width:100%;width:100%}.cs-invoice.cs-style1 .cs-invoice_left{max-width:100%}}
                                </style>    
                            </head><body>
                            <div class=""cs-container"">
                            <div class=""cs-invoice cs-style1"">
                            <div class=""cs-invoice_in"" id=""download_section"">
                            <div class=""cs-invoice_head cs-type1 cs-mb25"">
                            <div class=""cs-invoice_left"">
                            <div class=""cs-logo cs-mb5""><img src=""https://uhs.detentech.com/Images/logo.png"" style=""height:100px;"" alt=""Logo""></div>
                            <p class=""mt-1 mb-1""><b>Urban Hospitality Services</b></p>
                            <p>2nd Floor, Al Hitmi Building, C-Ring Road, P.O Box: 7218,<br /> Doha - Qatar</p>
                            </div>
                            <div class=""cs-invoice_right cs-text_right"">
                                <p class=""cs-invoice_number cs-primary_color cs-mb5 cs-f16""><b class=""cs - primary_color"">Invoice : {{InVoice}} </b></p>
                                  <p class=""cs-invoice_date cs-primary_color""><b class=""cs-primary_color"">Date: </b>{{today}}</p>

                            </div>
                            </div>
                            <div class=""cs-table cs-style1"">
                            <div class=""cs-round_border"">
                                <div class=""cs-table_responsive"">
                                   <table>
                                        <thead>
                                            <tr>
                                               <th class=""cs-width_8 cs-semi_bold cs-primary_color cs-focus_bg"">Service Type</th>
                                                <th class=""cs-width_8 cs-semi_bold cs-primary_color cs-focus_bg"">Sub Service Name</th>
                                                <th class=""cs-width_4 cs-semi_bold cs-primary_color cs-focus_bg"">Service Option</th>
                                                <th class=""cs-width_4 cs-semi_bold cs-primary_color cs-focus_bg"">Total Quantity</th>
   <th class=""cs-width_4 cs-semi_bold cs-primary_color cs-focus_bg"">Start Date & Time</th>
                                                <th class=""cs-width_2 cs-semi_bold cs-primary_color cs-focus_bg"">Price(per unit)</th>
                                             
                                                <th class=""cs-width_4 cs-semi_bold cs-primary_color cs-focus_bg"">Total Price</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            {{#each Items}}
                                            <tr>
                                                <td class=""cs-width_8"">{{SubCategory}}</td>
                                                <td class=""cs-width_8"">{{ServiceOption}}</td>
                                                <td class=""cs-width_8"">{{ServiceSubCategory}}</td>
                                                <td class=""cs-width_4"">{{Quantity}}</td>
                                                <td class=""cs-width_4"">{{StartDate}}</td>
                                                <td class=""cs-width_4"">{{formatPrice Price}}</td>
                                                <td class=""cs-width_4"">{{formatPrice TotalPrice}}</td>
                                            </tr>
                                            {{/each}}
 <tr><td colspan=""6"" class=""cs-width_8"">Total Price</td>
 <td class=""cs-width_4"">{{formatPrice TotalPrice}}</ td ></tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            </div>
                            </div>
                            </div>
                            </div>
                            </body>
                            </html>";

            var template = Handlebars.Compile(source);
            DateTime today = DateTime.Today;
            var items = details.ServiceSubCategory.Select(item => new
            {
                item.ServiceOption,
                item.ServiceSubCategory,
                item.Quantity,
                item.Price,
                TotalPrice = double.TryParse(item.TotalPrice, out var parsedPrice) ? parsedPrice : 0.0,
                SubCategory = details.SubCategoryName,
                StartDate = details.StartDate + " " + details.StartTime
            }).ToList();
            // Calculate the total price
            var totalPrice = items.Sum(item => item.TotalPrice);
            var data = new
            {
                InVoice = details.InVoice,
                today = today.ToString("dd MMMM,yyyy"),
                TotalPrice = totalPrice,
                Items = items
            };

            var getdata = template(data);
            PdfDocument pdfDocument = htmlToPdf.ConvertHtmlString(getdata);
            result = ConvertHtmlToBase64Pdf(getdata);

            return result;
        }

        private string ConvertHtmlToBase64Pdf(string html)
        {
            HtmlToPdf converter = new HtmlToPdf();
            PdfDocument pdfDocument = converter.ConvertHtmlString(html);
            byte[] pdfBytes;

            using (MemoryStream stream = new MemoryStream())
            {
                pdfDocument.Save(stream);
                pdfBytes = stream.ToArray();
            }

            return Convert.ToBase64String(pdfBytes);
        }

        public List<string> CalculateSignature(PaymentRequest request)
        {
            List<string> result = new List<string>();
            try
            {
                Guid g = Guid.NewGuid();
                var KeyId = "3167f16a-1bf6-4846-a78c-a12b829ae30d";
                // Assuming request is an object
                request.Uid = g.ToString();
                request.KeyId = KeyId.ToString();
                var data = BuildData(request);
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] keyByte = encoding.GetBytes("h0WSCOUbdKVcY+CMLoQLLqEGk0rxpes2EpGI+b5dyEGZC5HN7R6ahGNzSbU36XfJHyv/r+Xa8L91zDPwrJ9OzT24Ft08oc8mF+qXCFMxcTvqtmiYf2/jIrOKtiATHFCUI17t+1GxNJ0C0EfrVPbv3UAkzY8zLcFSwvyPd+KHAsl9zj3kAC2gOu1PsWnJfTKacsJDBE+LKpXS/YJHHobfWDGnnqXdJsdqHlz9UZNKA6s7/vKWqz8FS8ETeOpP831rYF4SEoEsTEJ04iZeeDX4IhaQn5pDJBh+Lm4YQdglzG+lxQwuFiuvspGmbusTLzs5q99Z+tKuPsO8CkYAYKr1V69eddFb3X95CVXF/8cN2WLew8hIyd3taiDRd/dMnyMmsuvbUKBrz3b4G5WNdnPY0N40kZtGvsjkjntYIaTOD2E8ZBRZLP0m+2UyeP5AmxJ2lsl2vfJKtB8jjPThPA72rbLTZWAPFkOYXf7ctXxFRoMZO5l0YRH1BgegLOILxWE0ewpfdYXiOnGkk+gtyxqG0A==");
                var hmacsha256 = new HMACSHA256(keyByte);
                byte[] messageBytes = encoding.GetBytes(data);
                string signature = Convert.ToBase64String(hmacsha256.ComputeHash(messageBytes));
                string apiUrl = "https://skipcashtest.azurewebsites.net/api/v1/payments";
                using (HttpClient httpClient = new HttpClient())
                {
                    // Create the request message with the necessary headers
                    HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, apiUrl);
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", signature);

                    var newPostJson = JsonConvert.SerializeObject(request);
                    var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");

                    req.Content = payload;

                    // Send the request and get the response
                    HttpResponseMessage response = httpClient.SendAsync(req).Result;
                    string responseBody = response.Content.ReadAsStringAsync().Result;

                    // Print the response to the console
                    ResponseBody deserialized = JsonConvert.DeserializeObject<ResponseBody>(responseBody);
                    ResultObj result1 = JsonConvert.DeserializeObject<ResultObj>(deserialized.resultObj.ToString());

                    result.Add(result1.payUrl);
                    result.Add(result1.id);
                    result.Add(result1.transactionId);

                }
            }
            catch (Exception ex)
            {
                result.Add("Exception");
            }
            return result;

        }

        public List<GetCustomerV3> GetCustomerShortDetails(int? cuID)
        {
            List<GetCustomerV3> result = new List<GetCustomerV3>();
            var objCustomerOfficialDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objCustomerOfficialDetail in objCustomerOfficialDetails)
            {
                int? custODID = objCustomerOfficialDetail.custODID;

                string PackageName = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().packID != null ?
                                     UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Package.Name : "Specialized Cleaning";
                string PerviousDate = null;
                int? StatusOfWork = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().StatusOfWork;
                if (StatusOfWork != 1 || StatusOfWork != 2 || StatusOfWork != null)
                {
                    PerviousDate = Convert.ToDateTime(UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().StartDate).ToString("dd/MM/yyyy");
                }
                string NextDate = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false && (x.StatusOfWork == 1 || x.StatusOfWork == 2 || x.StatusOfWork != null)).Count() != 0 ?
                                  Convert.ToDateTime(UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false && (x.StatusOfWork == 1 || x.StatusOfWork == 2 || x.StatusOfWork != null)).FirstOrDefault().StartDate).ToString("dd/MM/yyyy") : null;
                string MainCategoryName = objCustomerOfficialDetail.MainCategory.Name;
                string SubCategoryName = objCustomerOfficialDetail.catsubID != null ? objCustomerOfficialDetail.SubCategory.Name : "N/A";
                string NoOfMOnths = objCustomerOfficialDetail.NoOfMonths != null ? objCustomerOfficialDetail.CustomerRenewalMonth.Name : "N/A";
                string AsignTo = objCustomerOfficialDetail.stfID != null ? objCustomerOfficialDetail.Staff.Name : objCustomerOfficialDetail.teamID != null ? objCustomerOfficialDetail.Team.Name : null;
                result.Add(new GetCustomerV3 { MainCategoryName = MainCategoryName, SubCategoryName = SubCategoryName, PerviousDate = PerviousDate, NextDate = NextDate, RecursiveTime = PackageName, AssignedCleaner = AsignTo, cuID = objCustomerOfficialDetail.custID, cuODID = objCustomerOfficialDetail.custODID, NoOfMonths = NoOfMOnths });
            }

            return result;
        }

        public GetCustomerModelV3 GetCustomersByIDs(int? cuID, int? cuODID)
        {

            GetCustomerModelV3 result = new GetCustomerModelV3();
            result = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select(p => new GetCustomerModelV3
                     {
                         Name = p.Customer.Name,
                         Email = p.Customer.Email,
                         Mobile = p.Customer.Mobile,
                         WhatsAppNo = p.Customer.WhatsAppNo,
                         AlternativeNo = p.Customer.AlternativeNo,
                         Saluation = p.Customer.Salutaion,
                         Files = UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                                           UhDB.Files.Where(x => x.cuiD == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                                           .Select(s => new GetFileDetails
                                           {
                                               Name = s.Filename,
                                               ContentType = s.FileContentType,
                                               Size = s.FileSize,
                                               Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName

                                           }).ToList() : null,
                         MainCategory = p.MainCategory.Name,
                         SubCategory = p.catsubID != null ? p.SubCategory.Name : "N/A",
                         Area = p.PropertyArea.Name,
                         PropertyName = p.propType == 1 ? p.Venture.Name :
                                                    UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().TowerName,
                         PropertyResidencyType = p.proprestID != null ? p.PropertyResidenceType.Name : null,
                         Remarks = p.Remarks,
                         ApartmentName = p.AppartmentNumber,
                         OtherLocation = p.propType == 2 ? UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                   .Select(u => new GetOtherLocationModel
                                                   {
                                                       TowerName = u.TowerName,
                                                       BuildingName = u.BuildingName,
                                                       StreetNumber = u.StreetNumber,
                                                       ZoneNumber = u.ZoneNumber,
                                                       Loacation = u.Loacation,
                                                       LocationLink = u.LocationLink
                                                   }).FirstOrDefault() : null,
                         GetCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                                .Select(r => new GetCustomerAvailabilityModel
                                                                {
                                                                    custSCID = r.custSCID,
                                                                    Availability = r.Availability,
                                                                    KeyCollection = r.KeyCollection,
                                                                    AccessProperty = r.AccessProperty,
                                                                    ReceptionDate = r.ReceptionDate
                                                                }).FirstOrDefault(),
                         PaymentStatus = UhDB.CustomerTransactions.Where(x => x.cuID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                  .Select(c => new GetCustomerPaymentStatus { PayementID = c.PayementID, PaymentStatus = c.PaymentStatus, Price = c.Price, TotalPrice = c.TotalPrice, TransactionID = c.TransactionID, Quantity = c.Quantity, custTrasID = c.custTrasID }).FirstOrDefault(),
                         CustomDates = UhDB.CustomerCustomDateTimes.Where(x => x.cuID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                                .Select(b => new GetCustomerCustomeDate { CustomDays = b.CustomDays, CustomEndTime = b.CustomEndTime, CustomStartDate = b.CustomStartDate != null ? Convert.ToDateTime(b.CustomStartDate).ToString("dd/MM/yyyy") : null, CustomStartTime = b.CustomStartTime, custDTID = b.custDTID }).FirstOrDefault(),
                         GetServices = UhDB.CustomerSpecializedCleanings.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable().
                                                   Select(t => new GetServiceSubCategoryModel
                                                   {
                                                       custSCID = t.custSCID,
                                                       servcatID = t.servcatID,
                                                       servsubcatID = t.servsubcatID,
                                                       ServiceCategoryName = t.ServiceCategory.Name,
                                                       ServiceSubCategoryName = t.ServiceSubCategory.Name,
                                                       Quantity = t.Quantity
                                                   }).ToList(),
                         Packages = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == cuODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                   .Select(y => new GetPackagesForSubCategoryForParticularCustomerModel
                                   {
                                       PackageName = y.packID != null ? y.Package.Name : null,
                                       Price = y.parkID != null ? y.Pricing.Price.ToString() : null,
                                       Duration = y.parkID != null ? y.Pricing.Duration : null,
                                       Measurement = y.parkID != null ? y.Pricing.TimeMeasurement : null,
                                       packID = y.packID,
                                       parkID = y.parkID,
                                       Date = Convert.ToDateTime(y.StartDate).ToString("MM/dd/yyyy"),
                                       StartTime = y.StartTime,
                                       EndTime = y.EndTime,
                                       TaskNo = y.TaskNo.ToString(),
                                       WorkStatus = p.StatusOfWork.ToString()

                                   }).ToList(),
                         carstID = p.carstID,
                         cartID = p.cartID,
                         IsCarWash = p.IsCarWash,
                         CarType = p.CarType != null ? p.CarType.Name : "N/A",
                         CarServiceType = p.CarServiceType != null ? p.CarServiceType.Name : "N/A",
                         CustomCarDetails = p.IsCarWash == true ? UhDB.CustomerCarServiceDetails.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                          .Select(h => new GetCustomerCarDetails { ParkingLevel = h.ParkingLevel, ParkingNumber = h.ParkingNumber, VehicleBrand = h.VehicleBrand, VehicleColor = h.VehicleColor, VehicleNumber = h.VehicleNumber }).FirstOrDefault() : null,
                         TeamName = p.teamID != null ? p.Team.Name : null,
                         staffName = p.stfID != null ? p.Staff.Name : null,
                         NoOfMonths = p.NoOfMonths != null ? p.CustomerRenewalMonth.Name : "N/A",
                         CustomerID = p.Customer.CustomerID,
                         ServiceStatus = p.ServiceStatus == true ? "Active" : p.ServiceStatus == false ? "InActive" : "Pending",
                         stfID = p.stfID,
                         teamID = p.teamID,
                         propaID = p.propaID,
                         vID = p.vID,
                         proprestID = p.proprestID,
                         propType = p.propType,
                         custOPID = p.propType == 2 ? (UhDB.CustomerOtherProperties.Where(x => x.custID == p.custID && x.custODID == p.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().custODID) : null,
                         cuID = p.custID,
                         cuODID = p.custODID,
                         catID = p.catID,
                         catsubID = p.catsubID
                     }).FirstOrDefault();



            return result;
        }

        private string BuildData(PaymentRequest request)
        {

            var list = new List<string>();
            AppendData(list, "Uid", request.Uid);
            AppendData(list, "KeyId", request.KeyId);
            AppendData(list, "Amount", request.Amount);
            AppendData(list, "FirstName", request.FirstName);
            AppendData(list, "LastName", request.LastName);
            AppendData(list, "Phone", request.Phone);
            AppendData(list, "Email", request.Email);
            AppendData(list, "Street", request.Street);
            AppendData(list, "City", request.City);
            AppendData(list, "State", request.State);
            AppendData(list, "Country", request.Country);
            AppendData(list, "PostalCode", request.PostalCode);
            AppendData(list, "TransactionId", request.TransactionId);
            AppendData(list, "Custom1", request.Custom1);
            return string.Join(",", list);
        }

        private void AppendData(List<string> list, string name, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                list.Add($"{name}={value}");
            }
        }

        public DateTime GetNextDateForDay(int firstDay, int secondDay, DateTime AssignedDate)
        {
            // Calculate the number of days to add to get to the target day
            int daysUntilTarget = ((int)secondDay - (int)firstDay + 7) % 7;

            // If today is the target day, return today; otherwise, find the next occurrence
            if (daysUntilTarget == 0)
                daysUntilTarget = 7;

            return AssignedDate.AddDays(daysUntilTarget);
        }

        public List<ListOfDisplayDays> CircleOutDays(List<ListOfDisplayDays> days, string startDay)
        {
            // Find the index of the start day
            int startIndex = days.FindIndex(d => d.Day.Equals(startDay, StringComparison.OrdinalIgnoreCase));

            // If the day is not found, return the original list
            if (startIndex == -1) return days;

            // Create the "circled" list by combining the two parts
            List<ListOfDisplayDays> circledDays = new List<ListOfDisplayDays>();

            // Add days from the start day to the end of the list
            circledDays.AddRange(days.GetRange(startIndex, days.Count - startIndex));

            // Add days from the beginning of the list up to the start day
            circledDays.AddRange(days.GetRange(0, startIndex));

            return circledDays;
        }

        public GetCustomerModelV6 GetCustomerDetailsForComplain(int? custID, int? custODID)
        {
            GetCustomerModelV6 result = new GetCustomerModelV6();
            result = UhDB.CustomerOfficalDetails.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                     .Select(p => new GetCustomerModelV6
                     {
                         Name = p.Customer.Name,
                         Email = p.Customer.Email,
                         Mobile = p.Customer.Mobile,
                         WhatsAppNo = p.Customer.WhatsAppNo,
                         AlternativeNo = p.Customer.AlternativeNo,
                         Area = p.PropertyArea.Name,
                         PropertyName = p.Venture.Name,
                         PropertyResidencyType = p.PropertyResidenceType.Name,
                         ApartmentName = p.AppartmentNumber,
                         Saluation = p.Customer.Salutaion,
                         PropertyType = p.propType,
                         OtherLocation = p.propType == 2 ? UhDB.CustomerOtherProperties.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                       .Select(u => new GetOtherLocationModel
                                       {
                                           TowerName = u.TowerName,
                                           BuildingName = u.BuildingName,
                                           StreetNumber = u.StreetNumber,
                                           ZoneNumber = u.ZoneNumber,
                                           Loacation = u.Loacation,
                                           LocationLink = u.LocationLink
                                       }).FirstOrDefault() : null

                     }).FirstOrDefault();

            return result;
        }

        public string SuspendCustomerService(SuspendCustomerServiceModel customer)
        {
            string result = null;
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    System.Data.Entity.Core.Objects.ObjectParameter output = new System.Data.Entity.Core.Objects.ObjectParameter("responseMessage", typeof(string));
                    UhDB.SPmyLogin("LOGIN", customer.Username, customer.Password, null, output);
                    if (output.Value.ToString() == "User successfully logged in")
                    {
                        var objCustomerOfficialDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == customer.cuID && x.custODID == customer.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                        objCustomerOfficialDetails.ServiceStatus = false;
                        objCustomerOfficialDetails.UpdatedBy = customer.UpdatedBy;
                        objCustomerOfficialDetails.UpdatedOn = customer.UpdatedOn;
                        objCustomerOfficialDetails.UpdatedRole = customer.UpdatedRole;
                        UhDB.SaveChanges();

                        var objCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.custID == customer.cuID && x.custODID == customer.custODID && x.IsActive == true && x.IsDelete == false && (x.StatusOfWork == 2 || x.StatusOfWork == 1)).ToList();
                        foreach (var objCustomerTimeLine in objCustomerTimeLines)
                        {
                            var objUpdateCustomerTimeLine = UhDB.CustomerTimelines.Where(x => x.custTDID == objCustomerTimeLine.custTDID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                            objUpdateCustomerTimeLine.IsDelete = customer.IsDelete;
                            objUpdateCustomerTimeLine.IsActive = customer.IsActive;
                            objUpdateCustomerTimeLine.UpdatedBy = customer.UpdatedBy;
                            objUpdateCustomerTimeLine.UpdatedOn = customer.UpdatedOn;
                            objUpdateCustomerTimeLine.UpdatedRole = customer.UpdatedRole;
                            UhDB.SaveChanges();
                        }
                        var objCustomerDateBlocks = UhDB.CustomerDateBlocks.Where(x => x.custID == customer.cuID && x.custODID == customer.custODID && x.IsActive == true && x.IsDelete == false).ToList();
                        foreach (var objCustomerDateBlock in objCustomerDateBlocks)
                        {
                            var objUpdateCustomerDateBlock = UhDB.CustomerDateBlocks.Where(x => x.custDBID == objCustomerDateBlock.custDBID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                            objUpdateCustomerDateBlock.IsActive = customer.IsActive;
                            objUpdateCustomerDateBlock.IsDelete = customer.IsDelete;
                            objUpdateCustomerDateBlock.UpdatedBy = customer.UpdatedBy;
                            objUpdateCustomerDateBlock.UpdatedOn = customer.UpdatedOn;
                            objUpdateCustomerDateBlock.UpdatedRole = customer.UpdatedRole;
                            UhDB.SaveChanges();
                        }
                        string Name = objCustomerTimeLines.FirstOrDefault().Customer.Name;
                        string VentureName = objCustomerTimeLines.FirstOrDefault().CustomerOfficalDetail.vID != null ? objCustomerTimeLines.FirstOrDefault().CustomerOfficalDetail.Venture.Name : "N/A";
                        string PropertyArea = objCustomerTimeLines.FirstOrDefault().CustomerOfficalDetail.propaID != null ? objCustomerTimeLines.FirstOrDefault().CustomerOfficalDetail.PropertyArea.Name : "N/A";
                        string StaffRescheduleBody = EmailBodyStaffSuspendCustomerBody(Name, VentureName, PropertyArea);
                        if (objCustomerTimeLines.FirstOrDefault().Customer.Email != null)
                        {
                            string CustomerRescheduleBody = EmailBodyCustomerSuspend(objCustomerTimeLines.FirstOrDefault().Customer.Name, objCustomerTimeLines.FirstOrDefault().CustomerOfficalDetail.Venture.Name, objCustomerTimeLines.FirstOrDefault().CustomerOfficalDetail.PropertyArea.Name);
                            objGeneralDB.SentEmailFromAmazon(objCustomerTimeLines.FirstOrDefault().Customer.Email, CustomerRescheduleBody, "You service has been stoped", objCustomerTimeLines.FirstOrDefault().Customer.Name);
                        }
                        else
                        {
                            string TextMessage = "Your service has suspended for property " + objCustomerTimeLines.FirstOrDefault().CustomerOfficalDetail.PropertyArea.Name + " " + objCustomerTimeLines.FirstOrDefault().CustomerOfficalDetail.Venture.Name;
                            string MobileNo = objCustomerTimeLines.FirstOrDefault().Customer.PhoneCode + objCustomerTimeLines.FirstOrDefault().Customer.Mobile;
                            MobileNo = MobileNo.Replace(" ", "");
                            objGeneralDB.SendSMS(MobileNo, TextMessage);
                        }
                        int? teamID = objCustomerTimeLines.FirstOrDefault().CustomerOfficalDetail.teamID;
                        var objStaffs = UhDB.StaffTeams.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).ToList();
                        foreach (var objStaff in objStaffs)
                        {
                            if (objStaff.Staff.Email != null)
                            {
                                objGeneralDB.SentEmailFromAmazon(objStaff.Staff.Email, StaffRescheduleBody, "Customer has reschedule the service", objStaff.Staff.Name);
                            }
                            else
                            {
                                string TextMessage = objCustomerTimeLines.FirstOrDefault().Customer.Name + " has suspended the service for property " + objCustomerTimeLines.FirstOrDefault().CustomerOfficalDetail.PropertyArea.Name + " " + objCustomerTimeLines.FirstOrDefault().CustomerOfficalDetail.Venture.Name;
                                string MobileNo = objStaff.Staff.PhoneCode + objStaff.Staff.Mobile;
                                MobileNo = MobileNo.Replace(" ", "");
                                objGeneralDB.SendSMS(MobileNo, TextMessage);
                            }
                        }
                        trans.Commit();
                        result = "SUCCESS";
                    }
                    else
                    {
                        trans.Rollback();
                        result = "PWDInCorrect";
                    }


                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    result = "Exception";
                }
            }

            return result;
        }

        public string EmailBodyCustomerSuspend(string CustomerName, string PropertyName, string PropertyArea)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/SuspendCustomer.html")))//using streamreader for reading my htmltemplate  
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{PropertyName}", PropertyName); //replacing the required things  
            body = body.Replace("{PropertyArea}", PropertyArea);
            body = body.Replace("{CustomerName}", CustomerName);
            return body;
        }

        public string EmailBodyStaffSuspendCustomerBody(string CustomerName, string PropertyName, string PropertyArea)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/SuspendCustomerForStaff.html")))//using streamreader for reading my htmltemplate  
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{PropertyName}", PropertyName); //replacing the required things  
            body = body.Replace("{PropertyArea}", PropertyArea);
            body = body.Replace("{CustomerName}", CustomerName);
            return body;
        }
        private string EmailBodyForSendPassword(string Username, string Password)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/CustomerSendPassword.html")))//using streamreader for reading my htmltemplate  
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Username}", Username); //replacing the required things  
            body = body.Replace("{Password}", Password);
            return body;
        }
        private string EmailBodyCustomerInfoForStaff(string Area, string TowerName, string CustomerName, string CustomerMobile, string CustomerEmail, string StaffName, string Description)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/SendLocation.html")))//using streamreader for reading my htmltemplate  
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Area}", Area); //replacing the required things  
            body = body.Replace("{TowerName}", TowerName);
            body = body.Replace("{StaffName}", StaffName);
            body = body.Replace("{CustomerName}", CustomerName);
            body = body.Replace("{CustomerMobile}", CustomerMobile);
            body = body.Replace("{CustomerEmail}", CustomerEmail);
            body = body.Replace("{Description}", Description);

            return body;
        }
        private string EmailBodyCustomerInfoForStaffForOtherProperty(string CustomerName, string CustomerMobile, string CustomerEmail, string Area, string TowerName, string BuildingName, string StreetNumber, string ZoneNumber, string Location, string LoacationLink, string StaffName, string ApartmentNo, string Description)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/SendLocationOtherProperty.html")))//using streamreader for reading my htmltemplate  
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Area}", Area); //replacing the required things  
            body = body.Replace("{TowerName}", TowerName);
            body = body.Replace("{StaffName}", StaffName);
            body = body.Replace("{BuildingName}", BuildingName);
            body = body.Replace("{ZoneNumber}", ZoneNumber);
            body = body.Replace("{Location}", Location);
            body = body.Replace("{LoacationLink}", LoacationLink);
            body = body.Replace("{CustomerName}", CustomerName);
            body = body.Replace("{CustomerMobile}", CustomerMobile);
            body = body.Replace("{CustomerEmail}", CustomerEmail);
            body = body.Replace("{ApartmentNo}", ApartmentNo);
            body = body.Replace("{Description}", Description);
            return body;
        }
        private string EmailBodyCustomerClosingInfoForStaff(string Area, string TowerName, string CustomerName, string CustomerMobile, string CustomerEmail, string StaffName, string ClosingRemarks, string CustomerID, string TaskNo, string ApartmentNo, string Date)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/ClosingTaskFromStaff.html")))//using streamreader for reading my htmltemplate  
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Area}", Area); //replacing the required things  
            body = body.Replace("{TowerName}", TowerName);
            body = body.Replace("{StaffName}", StaffName);
            body = body.Replace("{CustomerName}", CustomerName);
            body = body.Replace("{CustomerMobile}", CustomerMobile);
            body = body.Replace("{CustomerEmail}", CustomerEmail);
            body = body.Replace("{CustomerID}", CustomerID);
            body = body.Replace("{TaskNo}", TaskNo);
            body = body.Replace("{ClosingRemarks}", ClosingRemarks);
            body = body.Replace("{ApartmentNo}", ApartmentNo);
            body = body.Replace("{Date}", Date);
            return body;
        }
        private string EmailBodyCustomerClosingforCustomer(string Area, string TowerName, string CustomerName, string CustomerMobile, string CustomerEmail, string StaffName, string CustomerID, string TaskNo, string Description, string ApartmentNo, string ClosingDate)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/ClosingTaskFromStaffForCustomer.html")))//using streamreader for reading my htmltemplate  
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Area}", Area); //replacing the required things  
            body = body.Replace("{TowerName}", TowerName);
            body = body.Replace("{StaffName}", StaffName);
            body = body.Replace("{CustomerName}", CustomerName);
            body = body.Replace("{CustomerMobile}", CustomerMobile);
            body = body.Replace("{CustomerEmail}", CustomerEmail);
            body = body.Replace("{CustomerID}", CustomerID);
            body = body.Replace("{TaskNo}", TaskNo);
            body = body.Replace("{Description}", Description);
            body = body.Replace("{ApartmentNo}", ApartmentNo);
            body = body.Replace("{ClosingDate}", ClosingDate);
            return body;
        }
        private string EmailBodyCustomerInfoForClosingStaffForOtherProperty(string CustomerName, string CustomerMobile, string CustomerEmail, string Area, string TowerName, string BuildingName, string StreetNumber, string ZoneNumber, string Location, string LoacationLink, string StaffName, string ClosingRemarks, string CustomerID, string TaskNo)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/ClosingTaskFromStaffForAnotherLocation.html")))//using streamreader for reading my htmltemplate   
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Area}", Area); //replacing the required things  
            body = body.Replace("{TowerName}", TowerName);
            body = body.Replace("{StaffName}", StaffName);
            body = body.Replace("{BuildingName}", BuildingName);
            body = body.Replace("{ZoneNumber}", ZoneNumber);
            body = body.Replace("{Location}", Location);
            body = body.Replace("{LoacationLink}", LoacationLink);
            body = body.Replace("{CustomerName}", CustomerName);
            body = body.Replace("{CustomerMobile}", CustomerMobile);
            body = body.Replace("{CustomerEmail}", CustomerEmail);
            body = body.Replace("{CustomerID}", CustomerID);
            body = body.Replace("{TaskNo}", TaskNo);
            body = body.Replace("{ClosingRemarks}", ClosingRemarks);
            return body;
        }
        private string EmailBodyCustomerIClosingForOtherPropertyCustomer(string CustomerName, string CustomerMobile, string CustomerEmail, string Area, string TowerName, string BuildingName, string StreetNumber, string ZoneNumber, string Location, string LoacationLink, string StaffName, string CustomerID, string TaskNo, string ApartmentNo, string ClosingDate)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/ClosingTaskFromStaffForAnotherLocationCustomer.html")))//using streamreader for reading my htmltemplate   
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Area}", Area); //replacing the required things  
            body = body.Replace("{TowerName}", TowerName);
            body = body.Replace("{StaffName}", StaffName);
            body = body.Replace("{BuildingName}", BuildingName);
            body = body.Replace("{ZoneNumber}", ZoneNumber);
            body = body.Replace("{Location}", Location);
            body = body.Replace("{LoacationLink}", LoacationLink);
            body = body.Replace("{CustomerName}", CustomerName);
            body = body.Replace("{CustomerMobile}", CustomerMobile);
            body = body.Replace("{CustomerEmail}", CustomerEmail);
            body = body.Replace("{CustomerID}", CustomerID);
            body = body.Replace("{TaskNo}", TaskNo);
            body = body.Replace("{ApartmentNo}", ApartmentNo);
            body = body.Replace("{ClosingDate}", ClosingDate);
            return body;
        }
        private string EmailBodyWelcome(string Name)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/Welcome.html")))//using streamreader for reading my htmltemplate  
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Name}", Name); //replacing the required things  
            return body;
        }
        private string EmailBodyForStaffNotAssign(string CustomerID, string TaskNo)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/StaffNotAssign.html")))//using streamreader for reading my htmltemplate  
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{CustomerID}", CustomerID); //replacing the required things  
            body = body.Replace("{TaskNo}", TaskNo);
            return body;
        }
        private string EmailBodyForCustomerAssignDate(string CustomerID, string TaskNo)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/CustomerAssignDate.html")))//using streamreader for reading my htmltemplate  
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{CustomerID}", CustomerID); //replacing the required things  
            body = body.Replace("{TaskNo}", TaskNo);
            return body;
        }


    }
}