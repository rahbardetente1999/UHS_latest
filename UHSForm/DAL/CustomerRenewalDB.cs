using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UHSForm.Models.Data;
using UHSForm.Models;
using System.IO;
using System.Globalization;
using System.Data.Entity.Core.Objects;

namespace UHSForm.DAL
{
    public class CustomerRenewalDB
    {
        private UHSEntities UhDB;
        private CustomerDB objCustomerDB;
        private GeneralDB objGeneralDB;
        public CustomerRenewalDB()
        {
            UhDB = new UHSEntities();
            objCustomerDB = new CustomerDB();
            objGeneralDB = new GeneralDB();
        }

        public List<GetLastDatesOfRegularCleaning> CheckRenewal(int? cuID)
        {
            List<GetLastDatesOfRegularCleaning> result = new List<GetLastDatesOfRegularCleaning>();
            int CountRegularCleaning = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID && x.catID == 1 && x.catsubID == 1 && x.IsActive == true && x.IsDelete == false).Count();
            if (CountRegularCleaning == 0)
            {
                result = null;
            }
            else
            {
                int custODID = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID && x.catID == 1 && x.catsubID == 1 && x.IsActive == true && x.IsDelete == false).OrderByDescending(x => x.custODID).FirstOrDefault().custODID;
                int? packID = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().packID;
                if (packID == 2)
                {
                    result = null;
                }
                else
                {
                    var objCustomerLastServiceDates = UhDB.CustomerTimelines.Where(x => x.custID == cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).OrderByDescending(x => x.custTDID).ToList().Take(2);
                    int Count = 1;
                    foreach (var item in objCustomerLastServiceDates)
                    {
                        if (Count == 1)
                        {
                            result.Add(new GetLastDatesOfRegularCleaning { LastDate = item.StartDate });
                            Count = Count + 1;
                        }
                        else
                        {
                            result.Add(new GetLastDatesOfRegularCleaning { SecodnLastDate = item.StartDate });
                        }
                    }
                }
            }
            return result;
        }

        public string UpdateCustomerRenewal(CustomerRenewalModel customer)
        {
            string result = null;
            using (var trans = UhDB.Database.BeginTransaction())
            {
                try
                {
                    var objCustomerOfficalDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == customer.cuID && x.catID == 1 &&
                                                    x.catsubID == 1 && x.IsActive == true && x.IsDelete == false
                                                    && x.propaID == customer.propaID && x.proprestID == customer.proprestID && x.vID == customer.vID
                                                    && x.propType == customer.proTypeID).OrderByDescending(x => x.custODID).FirstOrDefault();
                    int custODID = objCustomerOfficalDetails.custODID;
                    int? teamID = UhDB.CustomerTimelines.Where(x => x.custID == customer.cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().teamID;
                    var objCustomerDateBlocks = UhDB.CustomerDateBlocks.Where(x => x.custID == customer.cuID && x.custODID == custODID &&
                                                x.IsActive == true && x.IsDelete == false).ToList();

                    CustomerOfficalDetail objUpdateCustomerOfficalDetail = new CustomerOfficalDetail();
                    objUpdateCustomerOfficalDetail.catID = objCustomerOfficalDetails.catID;
                    objUpdateCustomerOfficalDetail.catsubID = objCustomerOfficalDetails.catsubID;
                    objUpdateCustomerOfficalDetail.BuldleDays = objCustomerOfficalDetails.BuldleDays;
                    objUpdateCustomerOfficalDetail.ServiceStatus = true;
                    objUpdateCustomerOfficalDetail.SpecialService = objCustomerOfficalDetails.SpecialService;
                    objUpdateCustomerOfficalDetail.propaID = objCustomerOfficalDetails.propaID;
                    objUpdateCustomerOfficalDetail.subAreaID = objCustomerOfficalDetails.subAreaID;
                    objUpdateCustomerOfficalDetail.vID = objCustomerOfficalDetails.vID;
                    objUpdateCustomerOfficalDetail.proprestID = objCustomerOfficalDetails.proprestID;
                    objUpdateCustomerOfficalDetail.propType = objCustomerOfficalDetails.propType;
                    objUpdateCustomerOfficalDetail.AppartmentNumber = objCustomerOfficalDetails.AppartmentNumber;
                    objUpdateCustomerOfficalDetail.NoOfMonths = objCustomerOfficalDetails.NoOfMonths;
                    objUpdateCustomerOfficalDetail.carstID = objCustomerOfficalDetails.carstID;
                    objUpdateCustomerOfficalDetail.cartID = objCustomerOfficalDetails.cartID;
                    objUpdateCustomerOfficalDetail.IsCarWash = objCustomerOfficalDetails.IsCarWash;
                    objUpdateCustomerOfficalDetail.IsActive = customer.IsActive;
                    objUpdateCustomerOfficalDetail.IsDelete = customer.IsDelete;
                    objUpdateCustomerOfficalDetail.custID = customer.cuID;
                    objUpdateCustomerOfficalDetail.CreatedBy = customer.CreatedBy;
                    objUpdateCustomerOfficalDetail.CreatedOn = customer.CreatedOn;
                    objUpdateCustomerOfficalDetail.CreatedRole = customer.CreatedRole;
                    UhDB.CustomerOfficalDetails.Add(objUpdateCustomerOfficalDetail);
                    UhDB.SaveChanges();
                    int UpdatedCuODID = objUpdateCustomerOfficalDetail.custODID;

                    if (objUpdateCustomerOfficalDetail.propType == 2)
                    {
                        var objCustomerOtherProperty = UhDB.CustomerOtherProperties.Where(x => x.custID == customer.cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                        CustomerOtherProperty objUpdateCustomerOtherProperty = new CustomerOtherProperty();
                        objUpdateCustomerOtherProperty.custID = customer.cuID;
                        objUpdateCustomerOtherProperty.custODID = UpdatedCuODID;
                        objUpdateCustomerOtherProperty.TowerName = objCustomerOtherProperty.TowerName;
                        objUpdateCustomerOtherProperty.BuildingName = objCustomerOtherProperty.BuildingName;
                        objUpdateCustomerOtherProperty.Loacation = objCustomerOtherProperty.Loacation;
                        objUpdateCustomerOtherProperty.LocationLink = objCustomerOtherProperty.LocationLink;
                        objUpdateCustomerOtherProperty.StreetNumber = objCustomerOtherProperty.StreetNumber;
                        objUpdateCustomerOtherProperty.ZoneNumber = objCustomerOtherProperty.ZoneNumber;
                        objUpdateCustomerOtherProperty.IsActive = customer.IsActive;
                        objUpdateCustomerOtherProperty.IsDelete = customer.IsDelete;
                        objUpdateCustomerOtherProperty.CreatedBy = customer.CreatedBy;
                        objUpdateCustomerOtherProperty.CreatedOn = customer.CreatedOn;
                        objUpdateCustomerOtherProperty.CreatedRole = customer.CreatedRole;
                        UhDB.CustomerOtherProperties.Add(objUpdateCustomerOtherProperty);
                        UhDB.SaveChanges();
                    }

                    var objCustomerAvailability = UhDB.CustomerAvailabilities.Where(x => x.custID == customer.cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                    CustomerAvailability objUpdateCustomerAvailability = new CustomerAvailability();
                    objUpdateCustomerAvailability.Availability = objCustomerAvailability.Availability;
                    objUpdateCustomerAvailability.KeyCollection = objCustomerAvailability.KeyCollection;
                    objUpdateCustomerAvailability.ReceptionDate = objCustomerAvailability.ReceptionDate;
                    objUpdateCustomerAvailability.AccessProperty = objCustomerAvailability.AccessProperty;
                    objUpdateCustomerAvailability.custID = customer.cuID;
                    objUpdateCustomerAvailability.custODID = UpdatedCuODID;
                    objUpdateCustomerAvailability.IsActive = customer.IsActive;
                    objUpdateCustomerAvailability.IsDelete = customer.IsDelete;
                    objUpdateCustomerAvailability.CreatedBy = customer.CreatedBy;
                    objUpdateCustomerAvailability.CreatedOn = customer.CreatedOn;
                    objUpdateCustomerAvailability.CreatedRole = customer.CreatedRole;
                    UhDB.CustomerAvailabilities.Add(objUpdateCustomerAvailability);
                    UhDB.SaveChanges();

                    int? TaskNo = null, TempTaskNo = null;
                    int? packID = objCustomerDateBlocks.FirstOrDefault().packID;
                    int? parkID = objCustomerDateBlocks.FirstOrDefault().parkID;
                    foreach (var objCustomerDateBlock in objCustomerDateBlocks)
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

                        CustomerTimeline objCustomerTimeline = new CustomerTimeline();
                        objCustomerTimeline.custID = customer.cuID;
                        objCustomerTimeline.custODID = UpdatedCuODID;
                        objCustomerTimeline.packID = objCustomerDateBlock.packID;
                        objCustomerTimeline.parkID = objCustomerDateBlock.parkID;
                        objCustomerTimeline.TaskNo = TaskNo;
                        objCustomerTimeline.StatusOfWork = 2;
                        objCustomerTimeline.teamID = teamID;
                        objCustomerTimeline.StartDate = objCustomerDateBlock.StartDate;
                        objCustomerTimeline.StartTime = objCustomerDateBlock.StartTime;
                        objCustomerTimeline.EndTime = objCustomerDateBlock.EndTime;
                        objCustomerTimeline.IsActive = customer.IsActive;
                        objCustomerTimeline.IsDelete = customer.IsDelete;
                        objCustomerTimeline.CreatedBy = customer.CreatedBy;
                        objCustomerTimeline.CreatedOn = customer.CreatedOn;
                        UhDB.CustomerTimelines.Add(objCustomerTimeline);
                        UhDB.SaveChanges();
                    }
                    DateTime? BlockDate = null;
                    DateTime StartDate = Convert.ToDateTime(objCustomerDateBlocks.OrderByDescending(x => x.custDBID).FirstOrDefault().StartDate);
                    string StartDay = StartDate.DayOfWeek.ToString();
                    ListOfDays objAListOfDays = new ListOfDays();
                    string ABFirstDay = StartDate.DayOfWeek.ToString();
                    string ABSecondDay = StartDay;
                    int ABFirstDayID = objAListOfDays.Days().Where(x => x.Day == ABFirstDay).FirstOrDefault().ID;
                    int ABSecondDayID = objAListOfDays.Days().Where(x => x.Day == ABSecondDay).FirstOrDefault().ID;
                    StartDate = objCustomerDB.GetNextDateForDay(ABFirstDayID, ABSecondDayID, Convert.ToDateTime(StartDate));
                    StartDay = StartDate.DayOfWeek.ToString();
                    List<string> stringListOfDays = new List<string>();
                    if (objCustomerOfficalDetails.BuldleDays.Contains(","))
                    {
                        string[] stringArrayOfDays = objCustomerOfficalDetails.BuldleDays.Split(',');
                        stringListOfDays = stringArrayOfDays.ToList();
                    }
                    else
                    {
                        stringListOfDays.Add(objCustomerOfficalDetails.BuldleDays);
                    }
                    ListOfDays objListOfDays1 = new ListOfDays();
                    List<ListOfDisplayDays> ListOfDays = new List<ListOfDisplayDays>();
                    ListOfDays = objListOfDays1.Days();
                    List<string> BundleOfDays1 = stringListOfDays;
                    List<ListOfDisplayDays> filteredResult = ListOfDays.Where(day => BundleOfDays1.Contains(day.Day)).ToList();
                    List<ListOfDisplayDays> BundleOfDays2 = objCustomerDB.CircleOutDays(filteredResult, StartDay);
                    var BundleOfDays3 = BundleOfDays2.Select(x => x.Day).ToList();
                    int BundleCount = BundleOfDays3.Count();
                    List<BundleOfDays> objBundleOfDays = new List<BundleOfDays>();
                    var objGetDaysTimes = objCustomerDateBlocks.Take(BundleCount);
                    foreach (var objGetDayTime in objGetDaysTimes)
                    {
                        string Day = objGetDayTime.StartDate.Value.DayOfWeek.ToString();
                        CustomTimes objCustomDayTimes = new CustomTimes();
                        objCustomDayTimes.Start = objGetDayTime.StartTime;
                        objCustomDayTimes.End = objGetDayTime.EndTime;
                        objBundleOfDays.Add(new BundleOfDays { Days = Day, Times = objCustomDayTimes });

                    }
                    int? NoOfMonth = UhDB.CustomerRenewalMonths.Where(x => x.custrmID == objCustomerOfficalDetails.NoOfMonths && x.IsActive == true && x.IsDelete == false).FirstOrDefault().NoOfMonths;
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
                                    StartDate = objCustomerDB.GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(StartDate));
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
                                        AssignDate = objCustomerDB.GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
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
                                        AssignDate = objCustomerDB.GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
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
                                        AssignDate = objCustomerDB.GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
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
                                        AssignDate = objCustomerDB.GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
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
                                        AssignDate = objCustomerDB.GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
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
                                        AssignDate = objCustomerDB.GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
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
                                        AssignDate = objCustomerDB.GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
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
                                CustomTimes objCustomTimes = objBundleOfDays.Where(x => x.Days == BundleOfDay).FirstOrDefault().Times;
                                CustomerDateBlock objCustomerDateBlock = new CustomerDateBlock();
                                objCustomerDateBlock.custID = customer.cuID;
                                objCustomerDateBlock.custODID = UpdatedCuODID;
                                objCustomerDateBlock.packID = packID;
                                objCustomerDateBlock.parkID = parkID;
                                objCustomerDateBlock.StartDate = AssignDate;
                                BlockDate = AssignDate;
                                objCustomerDateBlock.StartTime = objCustomTimes.Start;
                                objCustomerDateBlock.EndTime = objCustomTimes.End;
                                objCustomerDateBlock.teamID = teamID;
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
                    CustomerInovice objCustomerInovice = new CustomerInovice();
                    objCustomerInovice.cuID = customer.cuID;
                    objCustomerInovice.custODID = UpdatedCuODID;
                    objCustomerInovice.InvoiceNumber = customer.InVoice;
                    objCustomerInovice.uID = 1;
                    objCustomerInovice.IsActive = customer.IsActive;
                    objCustomerInovice.IsDelete = customer.IsDelete;
                    objCustomerInovice.CreatedRole = customer.CreatedRole;
                    objCustomerInovice.CreatedBy = customer.CreatedBy;
                    objCustomerInovice.CreatedOn = customer.CreatedOn;
                    UhDB.CustomerInovices.Add(objCustomerInovice);
                    UhDB.SaveChanges();
                    if (teamID != null)
                    {
                        string CustomerName = null, CustomerEmail = null, CustomerMobile = null, PropertyName = null,
                        ApartmentName = null, Area = null;
                        int? propType = null;
                        var objStaffTeams = UhDB.StaffTeams.Where(x => x.teamID == teamID && x.IsActive == true && x.IsDelete == false).ToList();
                        propType = objCustomerOfficalDetails.propType;
                        CustomerName = objCustomerOfficalDetails.Customer.Name;
                        CustomerEmail = objCustomerOfficalDetails.Customer.Email;
                        CustomerMobile = objCustomerOfficalDetails.Customer.Mobile;
                        Area = UhDB.PropertyAreas.Where(x => x.propaID == objCustomerOfficalDetails.propaID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                        PropertyName = UhDB.Ventures.Where(x => x.vID == objCustomerOfficalDetails.vID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
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
                                var objCustomerOtherProperty = UhDB.CustomerOtherProperties.Where(x => x.custID == customer.cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                                string TowerName = objCustomerOtherProperty.TowerName;
                                string BuildingName = objCustomerOtherProperty.BuildingName;
                                string ZoneNumber = objCustomerOtherProperty.ZoneNumber;
                                string Location = objCustomerOtherProperty.Loacation;
                                string StreetNumber = objCustomerOtherProperty.StreetNumber;
                                string LocationLink = objCustomerOtherProperty.LocationLink;
                                string ApartmentNo = objCustomerOfficalDetails.AppartmentNumber;
                                string Description = objCustomerOfficalDetails.Remarks;
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

                    var objCustomerTransaction = UhDB.CustomerTransactions.Where(x => x.cuID == customer.cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();

                    PaymentRequest objPaymentRequest = new PaymentRequest();
                    objPaymentRequest.FirstName = objCustomerOfficalDetails.Customer.Name;
                    objPaymentRequest.LastName = objCustomerOfficalDetails.Customer.Name;
                    objPaymentRequest.Email = objCustomerOfficalDetails.Customer.Email;
                    objPaymentRequest.Phone = objCustomerOfficalDetails.Customer.Mobile;
                    objPaymentRequest.Street = UhDB.PropertyAreas.Where(x => x.propaID == objCustomerOfficalDetails.propaID && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                                UhDB.PropertyAreas.Where(x => x.propaID == objCustomerOfficalDetails.propaID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name : "N/A";
                    objPaymentRequest.Amount = objCustomerTransaction.TotalPrice.ToString();
                    objPaymentRequest.City = "Doha";
                    objPaymentRequest.State = "DL";
                    objPaymentRequest.PostalCode = "110015";
                    objPaymentRequest.Country = "QR";
                    objPaymentRequest.Custom1 = customer.cuID.ToString();
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



                    List<string> Signatures = objCustomerDB.CalculateSignature(objPaymentRequest);
                    string PaymentLink = null, id = null, TransactionID = null;
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
                    objPackageDetailsModel.SubCategoryName = UhDB.SubCategories.Where(x => x.catsubID == objCustomerOfficalDetails.catsubID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                    objPackageDetailsModel.AreaName = UhDB.PropertyAreas.Where(x => x.propaID == objCustomerOfficalDetails.propaID && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                                      UhDB.PropertyAreas.Where(x => x.propaID == objCustomerOfficalDetails.propaID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name : "N/A";
                    objPackageDetailsModel.InVoice = "# INV- " + customer.InVoice;
                    objPackageDetailsModel.PackageName = UhDB.Packages.Where(x => x.packID == packID && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                                         UhDB.Packages.Where(x => x.packID == packID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name : "N/A";
                    objPackageDetailsModel.PropName = UhDB.Ventures.Where(x => x.vID == objCustomerOfficalDetails.vID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                    objPackageDetailsModel.resdName = UhDB.PropertyResidenceTypes.Where(x => x.proprestID == objCustomerOfficalDetails.proprestID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;

                    if (objCustomerOfficalDetails.NoOfMonths != null && objCustomerOfficalDetails.NoOfMonths != 0)
                    {
                        objPackageDetailsModel.NoOfMonths = UhDB.CustomerRenewalMonths.Where(x => x.custrmID == objCustomerOfficalDetails.NoOfMonths && x.IsActive == true && x.IsDelete == false).FirstOrDefault().Name;
                    }
                    else
                    {
                        objPackageDetailsModel.NoOfMonths = "N/A";
                    }
                    objPackageDetailsModel.Price = objCustomerTransaction.Price.ToString();
                    objPackageDetailsModel.TotalPrice = objCustomerTransaction.TotalPrice.ToString();
                    string pdfBase64 = objCustomerDB.CreateInvoice(objPackageDetailsModel);
                    objGeneralDB.SendEmailWithAttachmentForBodyAttachment(pdfBase64, objCustomerOfficalDetails.Customer.Email, "Invoice", PaymentLink);

                    CustomerTransaction objUpdateCustomerTransaction = new CustomerTransaction();
                    objUpdateCustomerTransaction.cuID = customer.cuID;
                    objUpdateCustomerTransaction.custODID = UpdatedCuODID;
                    objUpdateCustomerTransaction.Quantity = objCustomerDateBlocks.Count();
                    objUpdateCustomerTransaction.TotalPrice = objCustomerTransaction.TotalPrice;
                    objUpdateCustomerTransaction.Price = objCustomerTransaction.Price;
                    objUpdateCustomerTransaction.TransactionID = TransactionID;
                    objUpdateCustomerTransaction.PayementID = id;
                    objUpdateCustomerTransaction.IsActive = customer.IsActive;
                    objUpdateCustomerTransaction.IsDelete = customer.IsDelete;
                    objUpdateCustomerTransaction.CreatedBy = customer.CreatedBy;
                    objUpdateCustomerTransaction.CreatedOn = customer.CreatedOn;
                    UhDB.CustomerTransactions.Add(objUpdateCustomerTransaction);
                    UhDB.SaveChanges();

                    foreach (var objCustomerDateBlock in objCustomerDateBlocks)
                    {
                        int custDBID = objCustomerDateBlock.custDBID;
                        var objUpdateobjCustomerDateBlock = UhDB.CustomerDateBlocks.Where(x => x.custDBID == custDBID).FirstOrDefault();
                        objUpdateobjCustomerDateBlock.IsActive = false;
                        objUpdateobjCustomerDateBlock.UpdatedBy = customer.CreatedBy;
                        objUpdateobjCustomerDateBlock.UpdatedOn = customer.CreatedOn;
                        objUpdateobjCustomerDateBlock.UpdatedRole = customer.CreatedRole;
                        UhDB.SaveChanges();
                    }

                    DateTime? RenewalStartDate = objCustomerDateBlocks.FirstOrDefault().StartDate;
                    DateTime? RenewalEndDate = objCustomerDateBlocks.OrderByDescending(x => x.custDBID).FirstOrDefault().StartDate;
                    
                    CustomerAlert objCustomerAlert = new CustomerAlert();
                    objCustomerAlert.custID = customer.cuID;
                    objCustomerAlert.custATID = 2;
                    objCustomerAlert.vID = objCustomerOfficalDetails.vID;
                    objCustomerAlert.catID = objCustomerOfficalDetails.catID;
                    objCustomerAlert.catsubID = objCustomerOfficalDetails.catsubID;
                    objCustomerAlert.Message = "Renewal Request From :"+ objUpdateCustomerOfficalDetail.Customer.Name +" From " +
                    RenewalStartDate?.ToString("dd-MMM-yyyy") +" To " +RenewalEndDate?.ToString("dd-MMM-yyyy");  

                    objCustomerAlert.IsActive = true;
                    objCustomerAlert.IsDelete = false;
                    objCustomerAlert.CreatedBy = customer.CreatedBy;
                    objCustomerAlert.CreatedOn = customer.CreatedOn;
                    UhDB.CustomerAlerts.Add(objCustomerAlert);
                    UhDB.SaveChanges();
                    trans.Commit();
                    if (customer.IsAdmin == true)
                    {
                        result = PaymentLink;
                    }
                    else
                    {
                        result = "SUCCESS";
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

        public CustomerRenewalInfo GetCustomerRenewalInfo(int? cuID, int? propaID, int? vID, int? proprestID, int? propTypeID)
        {
            CustomerRenewalInfo result = new CustomerRenewalInfo();
            var objCustomerOfficalDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID && x.catID == 1 && x.catsubID == 1
                                            && x.IsActive == true && x.IsDelete == false && x.propaID == propaID && x.vID == vID && x.proprestID == proprestID
                                            && x.propType == propTypeID).OrderByDescending(x => x.custODID).FirstOrDefault();
            int custODID = objCustomerOfficalDetails.custODID;
            var objCustomerDateBlocks = UhDB.CustomerDateBlocks.Where(x => x.custID == cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).ToList();

            result.Salution = objCustomerOfficalDetails.Customer.Salutaion;
            result.Name = objCustomerOfficalDetails.Customer.Name;
            result.Email = objCustomerOfficalDetails.Customer.Email;
            result.Mobile = objCustomerOfficalDetails.Customer.Mobile;
            result.ApartmentNo = objCustomerOfficalDetails.AppartmentNumber;
            result.SubCategory = objCustomerOfficalDetails.SubCategory.Name;
            result.NoOfMonths = objCustomerOfficalDetails.CustomerRenewalMonth.NoOfMonths.ToString();
            result.Days = objCustomerOfficalDetails.BuldleDays;
            DateTime? StartDate = objCustomerDateBlocks.FirstOrDefault().StartDate;
            DateTime? EndDate = objCustomerDateBlocks.OrderByDescending(x => x.custDBID).FirstOrDefault().StartDate;
            result.StartDate = Convert.ToDateTime(StartDate).ToString("dd/MM/yyyy");
            result.EndDate = Convert.ToDateTime(EndDate).ToString("dd/MM/yyyy");
            result.PackageName = objCustomerDateBlocks.FirstOrDefault().Package.Name;
            result.Price = objCustomerDateBlocks.FirstOrDefault().Pricing.Price.ToString();
            List<string> stringListOfDays = new List<string>();
            string StartDay = StartDate.Value.DayOfWeek.ToString();
            if (objCustomerOfficalDetails.BuldleDays.Contains(","))
            {
                string[] stringArrayOfDays = objCustomerOfficalDetails.BuldleDays.Split(',');
                stringListOfDays = stringArrayOfDays.ToList();
            }
            else
            {
                stringListOfDays.Add(objCustomerOfficalDetails.BuldleDays);
            }
            ListOfDays objListOfDays1 = new ListOfDays();
            List<ListOfDisplayDays> ListOfDays = new List<ListOfDisplayDays>();
            ListOfDays = objListOfDays1.Days();
            List<string> BundleOfDays1 = stringListOfDays;
            List<ListOfDisplayDays> filteredResult = ListOfDays.Where(day => BundleOfDays1.Contains(day.Day)).ToList();
            List<ListOfDisplayDays> BundleOfDays2 = objCustomerDB.CircleOutDays(filteredResult, StartDay);
            var BundleOfDays3 = BundleOfDays2.Select(x => x.Day).ToList();
            int BundleCount = BundleOfDays3.Count();
            var objGetDaysTimes = objCustomerDateBlocks.Take(BundleCount);
            List<CustomTimes> objCustomDayTimes = new List<CustomTimes>();
            foreach (var objGetDayTime in objGetDaysTimes)
            {
                objCustomDayTimes.Add(new CustomTimes { Start = objGetDayTime.StartTime, End = objGetDayTime.EndTime });
            }
            result.Times = objCustomDayTimes;
            result.TotalNoOfService = objCustomerDateBlocks.Count().ToString();
            result.TotalPrice = UhDB.CustomerTransactions.Where(x => x.cuID == cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().TotalPrice.ToString();
            result.Duration = objCustomerDateBlocks.FirstOrDefault().Pricing.Duration;
            result.TimeMeasurement = objCustomerDateBlocks.FirstOrDefault().Pricing.TimeMeasurement;
            return result;
        }

        public List<CustomerRenewalPropertyInfo> GetCustomerRenewalPropertyInfo(int? cuID)
        {
            List<CustomerRenewalPropertyInfo> result = new List<CustomerRenewalPropertyInfo>();
            var objCustomerOfficialDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID
                                               && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objCustomerOfficialDetail in objCustomerOfficialDetails)
            {
                int? subareaID = UhDB.SubAreas.Where(x => x.propaID == objCustomerOfficialDetail.propaID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().subAreaID;
                if (!result.Any(x => x.propaID == objCustomerOfficialDetail.propaID && x.vID == objCustomerOfficialDetail.vID && x.proprestID == objCustomerOfficialDetail.proprestID
                    && x.proTypeID == objCustomerOfficialDetail.propType && x.ApartmentName == objCustomerOfficialDetail.AppartmentNumber && x.subareaID==subareaID))
                {
                    result.Add(new CustomerRenewalPropertyInfo
                    {
                        subareaID= subareaID,
                        propaID = objCustomerOfficialDetail.propaID,
                        proprestID = objCustomerOfficialDetail.proprestID,
                        proTypeID = objCustomerOfficialDetail.propType,
                        vID = objCustomerOfficialDetail.vID,
                        PropertyArea = objCustomerOfficialDetail.PropertyArea.Name,
                        PropertyName = objCustomerOfficialDetail.Venture.Name,
                        PropertyResidency = objCustomerOfficialDetail.PropertyResidenceType.Name,
                        ApartmentName = objCustomerOfficialDetail.AppartmentNumber
                    });
                }

            }
            return result;
        }

        public List<GetDropDown> GetCustomerRenewalPropertyArea(int? cuID)
        {
            List<GetDropDown> result = new List<GetDropDown>();
            var objCustomerOfficialDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID
                                               && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objCustomerOfficialDetail in objCustomerOfficialDetails)
            {
                if (!result.Any(x => x.ID == objCustomerOfficialDetail.propaID))
                {
                    result.Add(new GetDropDown
                    {
                        ID = objCustomerOfficialDetail.propaID,
                        Value = objCustomerOfficialDetail.PropertyArea.Name
                    });
                }
            }
            return result;
        }

        public List<GetCustomerRenewalProperty> GetCustomerRenewalProperty(int? cuID, int? propaID)
        {
            List<GetCustomerRenewalProperty> result = new List<GetCustomerRenewalProperty>();
            var objCustomerOfficialDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID
                                               && x.IsActive == true && x.IsDelete == false && x.propaID == propaID).ToList();
            foreach (var objCustomerOfficialDetail in objCustomerOfficialDetails)
            {
                if (!result.Any(x => x.ID == objCustomerOfficialDetail.vID))
                {
                    result.Add(new GetCustomerRenewalProperty
                    {
                        ID = objCustomerOfficialDetail.vID,
                        Value = objCustomerOfficialDetail.Venture.Name,
                        propTypeID = objCustomerOfficialDetail.propType
                    });
                }
            }
            return result;
        }

        public List<GetCustomerRenewalPropertyResidencyType> GetCustomerRenewalPropertyResidencyType(int? cuID, int? propaID, int? vID)
        {
            List<GetCustomerRenewalPropertyResidencyType> result = new List<GetCustomerRenewalPropertyResidencyType>();
            var objCustomerOfficialDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == cuID
                                               && x.IsActive == true && x.IsDelete == false && x.propaID == propaID
                                               && x.vID == vID).ToList();
            foreach (var objCustomerOfficialDetail in objCustomerOfficialDetails)
            {
                if (!result.Any(x => x.ID == objCustomerOfficialDetail.proprestID))
                {
                    result.Add(new GetCustomerRenewalPropertyResidencyType
                    {
                        ID = objCustomerOfficialDetail.proprestID,
                        Value = objCustomerOfficialDetail.PropertyResidenceType.Name,
                        AppartmentName = objCustomerOfficialDetail.AppartmentNumber
                    });
                }
            }
            return result;
        }

        public List<GetCustomerRenewalFromAdmin> GetCustomerRenewalFromAdmin(int? uID)
        {
            List<GetCustomerRenewalFromAdmin> result = new List<GetCustomerRenewalFromAdmin>();
            var objCustomerOfficalDetails = UhDB.CustomerOfficalDetails.Where(x => x.Customer.uID == uID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objCustomerOfficalDetail in objCustomerOfficalDetails)
            {
                int? custID = objCustomerOfficalDetail.custID;
                int? custODID = objCustomerOfficalDetail.custODID;
                var objCustomerTimeLine = UhDB.CustomerTimelines.Where(x => x.custID == custID && x.custODID == custODID
                                          ).ToList();
                result.Add(new Models.GetCustomerRenewalFromAdmin
                {
                    Name = objCustomerOfficalDetail.Customer.Name,
                    Email = objCustomerOfficalDetail.Customer.Email,
                    Mobile = objCustomerOfficalDetail.Customer.Mobile,
                    WhatsAppNo = objCustomerOfficalDetail.Customer.WhatsAppNo,
                    Saluation = objCustomerOfficalDetail.Customer.Salutaion,
                    Files = UhDB.Files.Where(x => x.cuiD == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).Count() != 0 ?
                            UhDB.Files.Where(x => x.cuiD == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false && x.FileUse == 7).AsEnumerable()
                            .Select(s => new GetFileDetails
                            {
                                Name = s.Filename,
                                ContentType = s.FileContentType,
                                Size = s.FileSize,
                                Value = "https://urbanhospitalityserv.s3.amazonaws.com/UHS/Prod/CustomerUploads/" + s.FileFieldName
                            }).ToList() : null,
                    Area = objCustomerOfficalDetail.PropertyArea.Name,
                    PropertyName = objCustomerOfficalDetail.propType == 1 ? objCustomerOfficalDetail.Venture.Name :
                                   UhDB.CustomerOtherProperties.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().TowerName,
                    PropertyResidencyType = objCustomerOfficalDetail.proprestID != null ? objCustomerOfficalDetail.PropertyResidenceType.Name : null,
                    ApartmentName = objCustomerOfficalDetail.AppartmentNumber,
                    PaymentStatus = UhDB.CustomerTransactions.Where(x => x.cuID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).AsEnumerable()
                                      .Select(c => new GetCustomerPaymentStatus { PayementID = c.PayementID, PaymentStatus = c.PaymentStatus, Price = c.Price, TotalPrice = c.TotalPrice, TransactionID = c.TransactionID, Quantity = c.Quantity, custTrasID = c.custTrasID }).FirstOrDefault(),
                    TeamName = objCustomerTimeLine.FirstOrDefault().teamID != null ? objCustomerTimeLine.FirstOrDefault().Team.Name : null,
                    CustomerID = objCustomerOfficalDetail.Customer.CustomerID,
                    stfID = objCustomerOfficalDetail.stfID,
                    teamID = objCustomerTimeLine.FirstOrDefault().teamID,
                    propaID = objCustomerOfficalDetail.propaID,
                    vID = objCustomerOfficalDetail.vID,
                    proprestID = objCustomerOfficalDetail.proprestID,
                    propType = objCustomerOfficalDetail.propType,
                    custOPID = objCustomerOfficalDetail.propType == 2 ? (UhDB.CustomerOtherProperties.Where(x => x.custID == objCustomerOfficalDetail.custID && x.custODID == objCustomerOfficalDetail.custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().custODID) : null,
                    cuID = objCustomerOfficalDetail.custID,
                    cuODID = objCustomerOfficalDetail.custODID,
                    catID = objCustomerOfficalDetail.catID,
                    catsubID = objCustomerOfficalDetail.catsubID,
                    CompletdTaskCount = objCustomerTimeLine.Where(x => x.StatusOfWork == 3).Count(),
                    UnCompletdTaskCount = objCustomerTimeLine.Where(x => x.StatusOfWork == 2).Count(),
                    Status= objCustomerOfficalDetail.ServiceStatus == true ? "Active" : objCustomerOfficalDetail.ServiceStatus == false ? "InActive" : "Pending",
                });
            }
            return result;
        }

        public IEnumerable<GetCustomerModelV4> GetCustomersForCompletedTask(int? custID)
        {
            List<GetCustomerModelV4> result = new List<GetCustomerModelV4>();

            var objCustomerOfficialDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == custID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objCustomerOfficialDetail in objCustomerOfficialDetails)
            {
                int custODID = objCustomerOfficialDetail.custODID;
                var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == custID && x.StatusOfWork == 3 && x.IsActive == true
                                           && x.IsDelete == false && x.CustomerOfficalDetail.catID == 1
                                           && x.CustomerOfficalDetail.catsubID == 1 && x.custODID == custODID).AsEnumerable()
                                           .Select(p => new GetCustomerModelV4
                                           {
                                               PaymentStatus = UhDB.CustomerTransactions.Where(x => x.cuID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                                               (UhDB.CustomerTransactions.Where(x => x.cuID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().PaymentStatus).ToString() : null,
                                               PackageName = p.packID != null ? p.Package.Name : null,
                                               Price = p.parkID != null ? p.Pricing.Price.ToString() : null,
                                               Duration = p.parkID != null ? p.Pricing.Duration : null,
                                               StartTime = p.StartTime,
                                               EndTime = p.EndTime,
                                               TaskNo = p.TaskNo.ToString(),
                                               ServiceDate = p.StartDate != null ? Convert.ToDateTime(p.StartDate).ToString("MM/dd/yyyy") : null,
                                               Status = p.CustomerOfficalDetail.ServiceStatus == true ? "Active" : p.CustomerOfficalDetail.ServiceStatus == false ? "InActive" : "Pending",
                                               WorkingStatus = p.StatusOfWork == 1 ? "Open" : p.StatusOfWork == 2 ? "Pending" : "Closed",
                                               custTDID = p.custTDID
                                           }).ToList();
                result.AddRange(objCustomerTimelines);
            }


            return result;
        }

        public IEnumerable<GetCustomerModelV5> GetCustomersForUnCompletedTask(int? custID)
        {
            List<GetCustomerModelV5> result = new List<GetCustomerModelV5>();
            var objCustomerOfficialDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == custID && x.IsActive == true && x.IsDelete == false).ToList();
            foreach (var objCustomerOfficialDetail in objCustomerOfficialDetails)
            {
                int? custODID = objCustomerOfficialDetail.custODID;
                var objCustomerTimelines = UhDB.CustomerTimelines.Where(x => x.custID == custID && x.StatusOfWork == 2 && x.CustomerOfficalDetail.catID == 1
                                           && x.CustomerOfficalDetail.catsubID == 1 && x.IsActive == true && x.IsDelete == false && x.custODID == custODID).AsEnumerable()
                                           .Select(p => new GetCustomerModelV5
                                           {
                                               PaymentStatus = UhDB.CustomerTransactions.Where(x => x.cuID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).Count() != 0 ?
                                                               (UhDB.CustomerTransactions.Where(x => x.cuID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).FirstOrDefault().PaymentStatus).ToString() : null,
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
                                               cuID=custID,
                                               custODID=p.custODID,
                                               packID=p.packID,
                                               parkID=p.parkID,
                                               proprestID=p.CustomerOfficalDetail.proprestID,
                                               catID=p.CustomerOfficalDetail.catID,
                                               catsubID=p.CustomerOfficalDetail.catsubID,
                                               teamID=p.CustomerOfficalDetail.teamID,
                                               TimeMeasurement=p.Pricing.TimeMeasurement
                                           }).ToList();
                result.AddRange(objCustomerTimelines);
            }
            return result;
        }

        public bool? IsTeamAvaialble(CheckTeamAvailable customer)
        {
            bool? result = null;
            List<bool> objAddAvaialble = new List<bool>();
            var objCustomerOfficalDetails = UhDB.CustomerOfficalDetails.Where(x => x.custID == customer.cuID && x.catID == 1 && x.catsubID == 1
                                         && x.IsActive == true && x.IsDelete == false && x.propaID==customer.propaID && x.vID==customer.vID && x.proprestID==customer.proprestID).OrderByDescending(x => x.custODID).FirstOrDefault();
            int custODID = objCustomerOfficalDetails.custODID;
            var objCustomerDateBlocks = UhDB.CustomerDateBlocks.Where(x => x.custID == customer.cuID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).ToList();
            DateTime? BlockDate = null;
            DateTime StartDate = Convert.ToDateTime(objCustomerDateBlocks.LastOrDefault().StartDate);
            string StartDay = StartDate.DayOfWeek.ToString();
            ListOfDays objAListOfDays = new ListOfDays();
            string ABFirstDay = StartDate.DayOfWeek.ToString();
            string ABSecondDay = StartDay;
            int ABFirstDayID = objAListOfDays.Days().Where(x => x.Day == ABFirstDay).FirstOrDefault().ID;
            int ABSecondDayID = objAListOfDays.Days().Where(x => x.Day == ABSecondDay).FirstOrDefault().ID;
            StartDate = objCustomerDB.GetNextDateForDay(ABFirstDayID, ABSecondDayID, Convert.ToDateTime(StartDate));
            StartDay = StartDate.DayOfWeek.ToString();
            List<string> stringListOfDays = new List<string>();
            if (objCustomerOfficalDetails.BuldleDays.Contains(","))
            {
                string[] stringArrayOfDays = objCustomerOfficalDetails.BuldleDays.Split(',');
                stringListOfDays = stringArrayOfDays.ToList();
            }
            else
            {
                stringListOfDays.Add(objCustomerOfficalDetails.BuldleDays);
            }
            ListOfDays objListOfDays1 = new ListOfDays();
            List<ListOfDisplayDays> ListOfDays = new List<ListOfDisplayDays>();
            ListOfDays = objListOfDays1.Days();
            List<string> BundleOfDays1 = stringListOfDays;
            List<ListOfDisplayDays> filteredResult = ListOfDays.Where(day => BundleOfDays1.Contains(day.Day)).ToList();
            List<ListOfDisplayDays> BundleOfDays2 = objCustomerDB.CircleOutDays(filteredResult, StartDay);
            var BundleOfDays3 = BundleOfDays2.Select(x => x.Day).ToList();
            int BundleCount = BundleOfDays3.Count();
            List<BundleOfDays> objBundleOfDays = new List<BundleOfDays>();
            var objGetDaysTimes = objCustomerDateBlocks.Take(BundleCount);
            foreach (var objGetDayTime in objGetDaysTimes)
            {
                String Day = objGetDayTime.StartDate.Value.DayOfWeek.ToString();
                CustomTimes objCustomDayTimes = new CustomTimes();
                objCustomDayTimes.Start = objGetDayTime.StartTime;
                objCustomDayTimes.End = objGetDayTime.EndTime;
                objBundleOfDays.Add(new BundleOfDays { Days = Day, Times = objCustomDayTimes });

            }
            int NoOfMonths = Convert.ToInt32(objCustomerOfficalDetails.CustomerRenewalMonth.NoOfMonths) * 2;
            for (int k = 1; k <= NoOfMonths; k++)
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
                            StartDate = objCustomerDB.GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(StartDate));
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
                                AssignDate = objCustomerDB.GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
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
                                AssignDate = objCustomerDB.GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
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
                                AssignDate = objCustomerDB.GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
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
                                AssignDate = objCustomerDB.GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
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
                                AssignDate = objCustomerDB.GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
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
                                AssignDate = objCustomerDB.GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
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
                                AssignDate = objCustomerDB.GetNextDateForDay(FirstDayID, SecondDayID, Convert.ToDateTime(AssignDate));
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
                        CustomTimes objCustomTimes = objBundleOfDays.Where(x => x.Days == BundleOfDay).FirstOrDefault().Times;
                        int CountTeamAvailable = UhDB.CustomerTimelines.Where(x => x.StartDate == AssignDate && x.StartTime == objCustomTimes.Start && x.EndTime == objCustomTimes.End && x.IsActive == true && x.IsDelete == false && x.CustomerOfficalDetail.teamID == customer.teamID).Count();
                        int CountDateBlock = UhDB.CustomerTimelines.Where(x => x.StartDate == AssignDate && x.StartTime == objCustomTimes.Start && x.EndTime == objCustomTimes.End && x.IsActive == true && x.IsDelete == false && x.CustomerOfficalDetail.teamID == customer.teamID).Count();
                        if (CountDateBlock == 0 && CountTeamAvailable == 0)
                        {
                            objAddAvaialble.Add(true);
                        }
                        else
                        {
                            objAddAvaialble.Add(false);
                        }

                    }
                }
                StartDate = Convert.ToDateTime(BlockDate);
            }
            if (objAddAvaialble.Contains(false))
            {
                result = false;
            }
            else
            {
                result = true;
            }

            return result;
        }

        public List<GetBookedStartDates> GetRenewalBookedDates(ExistingBookedStartDates booked)
        {
            List<GetBookedStartDates> result = new List<GetBookedStartDates>();
            List<TempBookedStartDates> tempData = new List<TempBookedStartDates>();
            DateTime TodayDate = DateTime.Now;
            DateTime? StartDate = TodayDate.AddHours(24);
            List<GetTeamsDatesAndTimes> objGetTeamsDatesAndTimes = new List<GetTeamsDatesAndTimes>();
            int? teamID =booked.Teams;
            var objDates = UhDB.CustomerTimelines.Where(x => x.CustomerOfficalDetail.catID == booked.catID && x.CustomerOfficalDetail.catsubID == booked.catsubID && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                           && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
            var objBlockDates = UhDB.CustomerDateBlocks.Where(x => x.CustomerOfficalDetail.catID == booked.catID && x.CustomerOfficalDetail.catsubID == booked.catsubID && EntityFunctions.TruncateTime(x.StartDate) >= EntityFunctions.TruncateTime(StartDate)
                                && x.teamID == teamID).Select(p => new TempBookedStartDates { StartDate = p.StartDate, StartTime = p.StartTime, EndTime = p.EndTime }).ToList();
            if (objDates != null)
            {
                if (objDates.Count()!=0) 
                {
                    tempData.AddRange(objDates);
                }
            }
            if (objBlockDates != null)
            {
                if (objBlockDates.Count()!=0) 
                {
                    tempData.AddRange(objBlockDates);
                }
            }
            tempData = tempData.OrderBy(x => x.StartDate).ToList();
            if (tempData != null)
            {
                if (tempData.Count() != 0)
                {
                    var groupedDates = tempData.GroupBy(date => date.StartDate).Select(group => new
                    {
                        StartDate = group.Key,
                        Events = group.ToList()
                    });
                    List<GetDatesAndTimes> objGetDatesAndTimes = new List<GetDatesAndTimes>();
                    foreach (var group in groupedDates)
                    {
                        List<TimeRange> ResultimeRange = new List<TimeRange>();
                        List<TimeRange> StartDateTimes = new List<TimeRange>();
                        List<TimeRange> expectTimes = new List<TimeRange>();
                        foreach (var eventDetail in group.Events)
                        {
                            expectTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                            StartDateTimes.Add(new TimeRange { Start = ConvertToTimeSpans(eventDetail.StartTime), End = ConvertToTimeSpans(eventDetail.EndTime) });
                        }
                        var objTimeRange = UhDB.Pricings.Where(x => x.packID == booked.packID && x.proprestID == booked.propresID && x.IsActive == true && x.IsDelete == false).FirstOrDefault();
                        string TimeMeasurement = objTimeRange.TimeMeasurement;
                        int Time = 0;
                        if (TimeMeasurement == "Hours")
                        {
                            Time = Convert.ToInt32(objTimeRange.Duration) * 60;
                        }
                        else { Time = Convert.ToInt32(objTimeRange.Duration); }
                        TimeRange originalRange = new TimeRange();
                        originalRange.Start = ConvertToTimeSpans("8:00 AM");
                        originalRange.End = ConvertToTimeSpans("06:00 PM");
                        List<TimeRange> timeRange = RemoveIntervals(originalRange, expectTimes, Time);
                        if (StartDateTimes.Count != 0)
                        {
                            timeRange.RemoveAll(ot => StartDateTimes.Exists(interval => ot.OverlapsWith(interval)));
                        }
                        if (booked.Time != null)
                        {
                            if (Convert.ToDateTime(group.StartDate).ToString("dd/MM/yyyy") == Convert.ToDateTime(StartDate).ToString("dd/MM/yyyy"))
                            {
                                TimeSpan B1 = ConvertToTimeSpans(booked.Time);
                                timeRange = timeRange.Where(x => x.Start >= B1).ToList();
                            }
                        }
                        bool? IsAvailableDate = null;
                        if (timeRange != null)
                        {
                            if (timeRange.Count() != 0)
                            {
                                IsAvailableDate = true;
                            }
                            else
                            {
                                IsAvailableDate = false;
                            }
                        }
                        else
                        {
                            IsAvailableDate = false;
                        }
                        objGetDatesAndTimes.Add(new GetDatesAndTimes { StartDate = group.StartDate, TimeRange = timeRange, IsAvailableDate = IsAvailableDate });
                    }
                    objGetTeamsDatesAndTimes.Add(new GetTeamsDatesAndTimes { teamID = teamID, Dates = objGetDatesAndTimes });
                }
                else
                {
                    objGetTeamsDatesAndTimes.Add(new GetTeamsDatesAndTimes { teamID = teamID, Dates = null });
                }
            }
            if (objGetTeamsDatesAndTimes.Count() == 0)
            {
                result = null;
            }
            else if (objGetTeamsDatesAndTimes.Any(x => x.Dates == null))
            {
                result = null;
            }
            else
            {

                if (objGetTeamsDatesAndTimes.Count() == 1)
                {
                    foreach (var DatesAndTimes in objGetTeamsDatesAndTimes)
                    {
                        var D1 = DatesAndTimes.Dates;
                        foreach (var Dates in D1)
                        {
                            if (!result.Any(x => x.StartDate == Convert.ToDateTime(Dates.StartDate).ToString("yyyy/MM/dd"))) 
                            {
                                result.Add(new GetBookedStartDates { StartDate = Convert.ToDateTime(Dates.StartDate).ToString("yyyy/MM/dd"), IsDateAvailable = Dates.IsAvailableDate });
                            }
                        }

                    }
                }
                else
                {
                    // Find common dates across all teams
                    var commonDates = objGetTeamsDatesAndTimes
                        .Where(t => t.Dates != null)  // Only consider teams with non-null dates
                        .SelectMany(t => t.Dates.Select(d => d.StartDate))
                        .GroupBy(d => d)
                        .Where(g => g.Count() == objGetTeamsDatesAndTimes.Count(t => t.Dates != null))  // Ensure date is common across all teams
                        .Select(g => g.Key)
                        .ToList();

                    // Check availability for each common date
                    foreach (var date in commonDates)
                    {
                        bool isAvailable = objGetTeamsDatesAndTimes
                            .Where(t => t.Dates != null)  // Consider teams with non-null dates
                            .Any(t => t.Dates.Any(d => d.StartDate == date && d.IsAvailableDate == true));
                       
                        if (!result.Any(x => x.StartDate == Convert.ToDateTime(date).ToString("yyyy/MM/dd"))) 
                        {
                            result.Add(new GetBookedStartDates
                            {

                                StartDate = Convert.ToDateTime(date).ToString("yyyy/MM/dd"),
                                IsDateAvailable = isAvailable
                            });
                        }
                    }

                    // Extract uncommon dates
                    var allDates = objGetTeamsDatesAndTimes
                        .Where(t => t.Dates != null)
                        .SelectMany(t => t.Dates)
                        .GroupBy(d => d.StartDate)
                        .Where(g => g.Count() == 1)
                        .Select(g => g.First())
                        .ToList();
                    foreach (var Date in allDates)
                    {
                        if (!result.Any(x=>x.StartDate== Convert.ToDateTime(Date.StartDate).ToString("yyyy/MM/dd"))) 
                        {
                            result.Add(new GetBookedStartDates { StartDate = Convert.ToDateTime(Date.StartDate).ToString("yyyy/MM/dd"), IsDateAvailable = true });
                        }

                    }
                }

            }



            return result;
        }

        public TimeSpan ConvertToTimeSpans(string timeStrings)
        {
            TimeSpan timeSpans = new TimeSpan();
            string format = "h:mm tt";
            if (DateTime.TryParseExact(timeStrings, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
            {
                timeSpans = dateTime.TimeOfDay;
            }
            return timeSpans;
        }

        public List<TimeRange> RemoveIntervals(TimeRange originalRange, List<TimeRange> intervals, int Duration)
        {
            List<TimeRange> result = new List<TimeRange>();
            TimeSpan currentStart = originalRange.Start;
            foreach (var interval in intervals.OrderBy(i => i.Start))
            {
                // If the current interval is within the original range, exclude it
                if (interval.Start > currentStart)
                {
                    result.Add(new TimeRange
                    {
                        Start = currentStart,
                        End = interval.Start
                    });
                }
                // Move the current start time after the end of the interval
                currentStart = interval.End;
            }

            // If there's still time left between the last interval and the end of the original range
            if (currentStart < originalRange.End)
            {
                result.Add(new TimeRange
                {
                    Start = currentStart,
                    End = originalRange.End
                });
            }

            // Ensure a minimum one-hour gap
            List<TimeRange> finalResult = new List<TimeRange>();
            foreach (var range in result)
            {
                if (finalResult.Count == 0)
                {
                    if (range.Start == originalRange.Start)
                    {
                        TimeSpan T4 = new TimeSpan(0, 15, 0);
                        range.End = range.End.Subtract(T4);
                        TimeSpan SubtractRange = (range.Start - range.End);
                        int MinutesBeforIteration = Math.Abs(Convert.ToInt32(SubtractRange.TotalMinutes));
                        if (MinutesBeforIteration >= Duration)
                        {
                            TimeRange onlyOne = new TimeRange();
                            onlyOne.Start = range.Start;
                            onlyOne.End = range.End;
                            finalResult = GetTimeDifference1(onlyOne, Duration);
                        }
                    }
                    else
                    {
                        TimeSpan SubtractRange = (range.Start - range.End);
                        int MinutesBeforIteration = Math.Abs(Convert.ToInt32(SubtractRange.TotalMinutes));
                        if (MinutesBeforIteration >= Duration)
                        {
                            TimeRange onlyOne = new TimeRange();
                            TimeSpan T3 = new TimeSpan(0, 15, 0);
                            onlyOne.Start = range.Start.Add(T3);

                            TimeSpan T4 = new TimeSpan(0, 15, 0);
                            onlyOne.End = range.End.Subtract(T4);
                            finalResult = GetTimeDifference1(onlyOne, Duration);
                        }
                    }
                }
                else
                {
                    TimeSpan T3 = new TimeSpan(0, 15, 0);
                    range.Start = range.Start.Add(T3);
                    TimeSpan T4 = new TimeSpan(0, 15, 0);
                    if (range.End != originalRange.End)
                    {
                        range.End = range.End.Subtract(T4);
                    }
                    TimeSpan SubtractRange = (range.Start - range.End);
                    int MinutesBeforIteration = Math.Abs(Convert.ToInt32(SubtractRange.TotalMinutes));
                    if (MinutesBeforIteration >= Duration)
                    {
                        TimeSpan ESP = (range.Start - range.End);
                        int ESPMinutes = Convert.ToInt32(ESP.TotalMinutes);
                        int Count = Math.Abs(ESPMinutes / Convert.ToInt32(Duration));
                        for (int i = 0; i <= Count; i++)
                        {
                            if (i == 0)
                            {
                                TimeSpan TI1 = range.Start;
                                //TimeSpan T3 = new TimeSpan(0, 15, 0);
                                //TI1 = TI1.Add(T3);
                                TimeSpan T2 = new TimeSpan(0, Duration, 0);
                                TimeSpan TI2 = TI1.Add(T2);

                                if (range.Start <= originalRange.End)
                                {
                                    if (TI2 <= range.End)
                                    {
                                        finalResult.Add(new TimeRange { Start = TI1, End = TI2 });
                                        range.Start = TI2;
                                    }
                                }
                            }
                            else
                            {
                                TimeSpan TI1 = range.Start;
                                TimeSpan T5 = new TimeSpan(0, 15, 0);
                                TI1 = TI1.Add(T5);
                                TimeSpan T2 = new TimeSpan(0, Duration, 0);
                                TimeSpan TI2 = TI1.Add(T2);

                                if (range.Start <= originalRange.End)
                                {
                                    if (TI2 <= range.End)
                                    {
                                        finalResult.Add(new TimeRange { Start = TI1, End = TI2 });
                                        range.Start = TI2;
                                    }
                                }
                            }
                        }
                    }
                }

            }
            if (intervals.Count() == 1)
            {

            }
            return finalResult;
        }

        private List<TimeRange> GetTimeDifference1(TimeRange originalRange, int Time)
        {
            List<TimeRange> finalResult = new List<TimeRange>();
            TimeSpan ESP = (originalRange.End - originalRange.Start);
            int Count = Math.Abs(Convert.ToInt32(ESP.TotalHours));
            Count = Count + 3;
            for (int i = 0; i <= Count; i++)
            {

                TimeSpan TI1 = originalRange.Start;
                TimeSpan T2 = new TimeSpan(0, Time, 0);
                TimeSpan TI2 = TI1.Add(T2);
                if (originalRange.Start <= originalRange.End)
                {
                    if (TI2 <= originalRange.End)
                    {
                        finalResult.Add(new TimeRange { Start = TI1, End = TI2 });
                        originalRange.Start = TI2;
                    }
                }
            }
            return finalResult;
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
    }
}