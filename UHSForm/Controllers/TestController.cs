using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UHSForm.DAL;
using UHSForm.Models.Data;
using UHSForm.Models;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace UHSForm.Controllers
{
    [AllowAnonymous]
    public class TestController : Controller
    {
        private static readonly string apiUrl = "https://messaging.ooredoo.qa/bms/soap/Messenger.asmx/HTTP_SendSms";
        public CommonCustomerTimeLineDB _objCommonCustomerTimeLineDB;
        private GeneralDB _objGeneralDB;
        public async Task<ActionResult> SendSms()
        {
            try
            {
                // Build the request URL with query parameters
                string customerID = "6264"; // Replace with your customer ID
                string userName = "UHSAdmin"; // Replace with your username
                string userPassword = "UHSms$007"; // Replace with your password
                string originator = "UHS"; // Replace with the originator
                string smsText = "Test SMS message";
                string recipientPhone = "97472119012"; // Replace with the recipient's phone numbers
                string defDate = DateTime.UtcNow.ToString("yyyyMMddhhmmss"); // For immediate delivery, use empty string

                string requestUrl = $"{apiUrl}?customerID={customerID}&userName={userName}&userPassword={userPassword}" +
                                    $"&originator={originator}&smsText={smsText}&recipientPhone={recipientPhone}" +
                                    $"&messageType=Latin&defDate={defDate}&blink=false&flash=false&Private=false";

                // Send the GET request
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(requestUrl);
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return Content(responseContent);
                }
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }

        public TestController()
        {
            _objGeneralDB = new GeneralDB();
            _objCommonCustomerTimeLineDB = new CommonCustomerTimeLineDB();
        }



        // GET: Test
        public ActionResult Index()
        {
            //string AppartmentNo = HttpUtility.HtmlEncode(_objGeneralDB.Encrypt(("S34"), "Lets1Make2It3Happen4"));
            return View();
        }

        [HttpGet]
        public ActionResult TestCode(int? packID, int? catID, int? catsubID, int? propresID)
        {
            try
            {

                var result = _objCommonCustomerTimeLineDB.GetResultsForTimeSlots(packID, catID, catsubID, propresID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult GetResultByTeam(GetResultByTeamModel teams)
        {
            try
            {
                var result = _objCommonCustomerTimeLineDB.GetResultByTeam(teams);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult GetResultForOtherTime(GetResultForOtherTime time)
        {
            try
            {
                var result = _objCommonCustomerTimeLineDB.GetResultForOtherTime(time);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult GetTime()
        {

            List<TimeRange> originalTime = new List<TimeRange>()
        {
            new TimeRange { Start = new TimeSpan(8, 0, 0), End = new TimeSpan(9, 0, 0) },
            new TimeRange { Start = new TimeSpan(9, 0, 0), End = new TimeSpan(10, 0, 0) },
            new TimeRange { Start = new TimeSpan(10, 0, 0), End = new TimeSpan(11, 0, 0) },
            new TimeRange { Start = new TimeSpan(11, 0, 0), End = new TimeSpan(12, 0, 0) },
            new TimeRange { Start = new TimeSpan(12, 0, 0), End = new TimeSpan(13, 0, 0) },
            new TimeRange { Start = new TimeSpan(13, 0, 0), End = new TimeSpan(14, 0, 0) },
            new TimeRange { Start = new TimeSpan(14, 0, 0), End = new TimeSpan(15, 0, 0) },
            new TimeRange { Start = new TimeSpan(15, 0, 0), End = new TimeSpan(16, 0, 0) },
            new TimeRange { Start = new TimeSpan(16, 0, 0), End = new TimeSpan(17, 0, 0) }
        };
            List<TimeRange> intervalTime = new List<TimeRange>()
        {
            new TimeRange { Start = new TimeSpan(8, 0, 0), End = new TimeSpan(9, 0, 0) },
            new TimeRange { Start = new TimeSpan(9, 15, 0), End = new TimeSpan(10, 15, 0) },
            new TimeRange { Start = new TimeSpan(11, 30, 0), End = new TimeSpan(12, 30, 0) },
             new TimeRange { Start = new TimeSpan(13, 45, 0), End = new TimeSpan(14, 45, 0) },
        };
            List<TimeRange> L3 = new List<TimeRange>();
            foreach (var interval in intervalTime)
            {
                if (L3.Count() == 0)
                {
                    int index = originalTime.FindIndex(p => p.Start == interval.Start && p.End == interval.End);
                    List<TimeRange> L4 = originalTime.GetRange(0, index);
                    L3.AddRange(L4);
                    originalTime.RemoveAt(index);
                    for (int i = index; i <= originalTime.Count() - 1; i++)
                    {
                        TimeRange TimeRange1 = originalTime[i];
                        TimeSpan T1 = new TimeSpan(0, 15, 0);
                        TimeRange1.Start = TimeRange1.Start.Add(T1);
                        TimeSpan T2 = new TimeSpan(0, 60, 0);
                        TimeRange1.End = TimeRange1.Start.Add(T2);
                        L3.Add(TimeRange1);
                    }
                    L3 = L3.OrderBy(x => x.Start).ToList();
                }
                else
                {
                    int index = L3.FindIndex(p => p.Start == interval.Start && p.End == interval.End);
                    List<TimeRange> L4 = L3.GetRange(0, index);
                    L3.AddRange(L4);
                    HashSet<TimeRange> unique = new HashSet<TimeRange>(L3);
                    List<TimeRange> L5 = new List<TimeRange>();
                    L5.AddRange(unique);
                    L5.RemoveAt(index);
                    TimeSpan EndTime = new TimeSpan(18, 0, 0);
                    for (int i = index; i <= L5.Count() - 1; i++)
                    {
                        TimeRange TimeRange1 = L5[i];
                        if (TimeRange1.Start < EndTime)
                        {
                            TimeSpan T1 = new TimeSpan(0, 15, 0);
                            TimeRange1.Start = TimeRange1.Start.Add(T1);
                            TimeSpan T2 = new TimeSpan(0, 60, 0);
                            TimeRange1.End = TimeRange1.Start.Add(T2);
                            L3[i] = TimeRange1;

                        }
                    }
                    L3 = L3.OrderBy(x => x.Start).ToList();
                    HashSet<TimeRange> unique1 = new HashSet<TimeRange>(L3);
                    List<TimeRange> L6 = new List<TimeRange>();
                    L6.AddRange(unique1);
                    L3 = L6;
                }
            }
            return Json(L3, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RemoveIntervals()
        {
            try
            {
                TimeRange originalRange = new TimeRange();
                originalRange.Start = ConvertToTimeSpans("8:00 AM");
                originalRange.End = ConvertToTimeSpans("06:00 PM");
                List<TimeRange> intervals = new List<TimeRange>()
                {
                        new TimeRange { Start = ConvertToTimeSpans("10:00 AM"), End = ConvertToTimeSpans("12:30 PM") },
                        new TimeRange { Start = ConvertToTimeSpans("9:00 AM"), End = ConvertToTimeSpans("10:00 PM") }
                };
                int Duration = 90;
                var result = RemoveIntervals(originalRange, intervals, Duration);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = null;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
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
                            onlyOne.End = range.End;
                            finalResult = GetTimeDifference1(onlyOne, Duration);
                        }
                    }
                }
                else
                {
                    TimeSpan T3 = new TimeSpan(0, 15, 0);
                    range.Start = range.Start.Add(T3);
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
                                TimeSpan T4 = new TimeSpan(0, 15, 0);
                                TI1 = TI1.Add(T4);
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

        [HttpGet]
        public string GetTeamsData() 
        {
            UHSEntities UhDB = new UHSEntities();
            string result = null;
            using (var trans=UhDB.Database.BeginTransaction())
            {
                try
                {
                    var objTeams = UhDB.CustomerOfficalDetails.Where(x => x.IsActive == true && x.IsDelete == false).ToList();
                    foreach (var objTeam in objTeams)
                    {
                        int? custID = objTeam.custID;
                        int? custODID = objTeam.custODID;
                        int? teamID = objTeam.teamID;
                        var objCustomerTimeLines = UhDB.CustomerTimelines.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).ToList();
                        foreach (var objCustomerTimeLine in objCustomerTimeLines)
                        {
                            var objUpdateCustomerTimeLine = UhDB.CustomerTimelines.Where(x => x.IsDelete == false && x.IsActive == true && x.custTDID == objCustomerTimeLine.custTDID).FirstOrDefault();
                            if (objUpdateCustomerTimeLine.teamID==null) 
                            {
                                if (teamID!=null) 
                                {
                                    objUpdateCustomerTimeLine.teamID = teamID;
                                    UhDB.SaveChanges();
                                }
                            }
                        }
                        var objCustomerDateBlocks = UhDB.CustomerDateBlocks.Where(x => x.custID == custID && x.custODID == custODID && x.IsActive == true && x.IsDelete == false).ToList();
                        foreach (var objCustomerDateBlock in objCustomerDateBlocks)
                        {
                            var objUpdateCustomerDateBlocks = UhDB.CustomerDateBlocks.Where(x => x.IsDelete == false && x.IsActive == true && x.custDBID == objCustomerDateBlock.custDBID).FirstOrDefault();
                            if (objUpdateCustomerDateBlocks.teamID==null) 
                            {
                                if (teamID!=null) 
                                {
                                    objUpdateCustomerDateBlocks.teamID = teamID;
                                    UhDB.SaveChanges();
                                }
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

    }
}