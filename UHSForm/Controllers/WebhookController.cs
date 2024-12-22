using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UHSForm.Models;
using UHSForm.DAL;
using Newtonsoft.Json.Linq;
using UHSForm.Models.Data;
using System.Net.Http;


namespace UHSForm.Controllers
{
    [AllowAnonymous]
    public class WebhookController : Controller
    {
        private UHSEntities UhDB;
        private CustomerTransactionsDB objCustomerTransactionsDB;

        public WebhookController()
        {
            UhDB = new UHSEntities();
            objCustomerTransactionsDB = new CustomerTransactionsDB();
        }
        private static log4net.ILog Log { get; set; }
        ILog lognet = log4net.LogManager.GetLogger(typeof(WebhookController));
       
        [HttpPost]
        public ActionResult ProcessWebhook()
        {
            // Validate the webhook request
            string requestBody = null;
            string result = null;
            try
            {
                using (var stream = new System.IO.StreamReader(Request.InputStream))
                {
                    requestBody = stream.ReadToEnd();
                }
                
                if (requestBody != null)
                {
                    var json = JObject.Parse(requestBody);
                    string PaymentID = json["PaymentId"].ToString();
                    string TransactionID = json["TransactionId"].ToString();
                    int? StatusId = Convert.ToInt32(json["StatusId"]);
                    CustomerPaymentModel objCustomerPaymentModel = new CustomerPaymentModel();
                    objCustomerPaymentModel.PaymentID = PaymentID;
                    objCustomerPaymentModel.TransactionID = TransactionID;
                    objCustomerPaymentModel.PaymentStatus = StatusId;
                    objCustomerPaymentModel.UpdatedOn = DateTime.Now;
                  
                    result = objCustomerTransactionsDB.UpdatePaymentStatus(objCustomerPaymentModel);
                }
            }
            catch (Exception ex)
            {
                lognet.Info("Exception" + ex.Message);
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetProcess(string ID)
        {
            string result = null;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

            // Method to calculate the webhook signature
        public string CalculateWebhookSignature(WebHookRequest request, string secretKey)
        {
            Guid g = Guid.NewGuid();
            var KeyId = "a3754add-d43b-4511-a739-91b3a8ed63dc";
            // Assuming request is an object
            request.Uid = g.ToString();
            request.KeyId = KeyId.ToString();
            var data = BuildWebhookData(request);
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secretKey);
            var hmacsha256 = new HMACSHA256(keyByte);
            byte[] messageBytes = encoding.GetBytes(data);
            return Convert.ToBase64String(hmacsha256.ComputeHash(messageBytes));
            //using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
            //{
            //    var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            //    return Convert.ToBase64String(hash);
            //}
        }

        // Method to build the data string from the webhook request
        private string BuildWebhookData(WebHookRequest request)
        {
            var data = new Dictionary<string, string>
            {
                { "Uid", request.Uid.ToString() },
                { "KeyId", request.KeyId.ToString() },
                { "PaymentId", request.PaymentId.ToString() },
                { "Amount", request.Amount },
                { "StatusId", request.StatusId.ToString() },
                { "TransactionId", request.TransactionId },
                { "Custom1", request.Custom1 },
                { "VisaId", request.VisaId }
            };

            var dataList = new List<string>();
            foreach (var item in data)
            {
                if (!string.IsNullOrEmpty(item.Value))
                {
                    dataList.Add($"{item.Key}={item.Value}");
                }
            }

            return string.Join(",", dataList);
        }

        // Method to validate the webhook request
        private bool IsValidWebhookRequest(System.Collections.Specialized.NameValueCollection headers, WebHookRequest request, string secretKey)
        {
            // Calculate the signature from the request
            var calculatedSignature = CalculateWebhookSignature(request, secretKey);

            // Get the signature provided in the request headers
            var providedSignature = headers["Authorization"];

            // Compare the calculated signature with the provided signature
            return calculatedSignature == providedSignature;
        }


        public string CalculateSignature(PaymentRequest request,string pid)
        {
            string result=null;
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
                string apiUrl = "https://skipcashtest.azurewebsites.net/api/v1/payments/"+pid;
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


                    if (responseBody != null)
                    {
                        var json = JObject.Parse(responseBody);
                        string PaymentID = json["PaymentId"].ToString();
                        string TransactionID = json["TransactionId"].ToString();
                        int? StatusId = Convert.ToInt32(json["StatusId"]);
                        
                        CustomerPaymentModel objCustomerPaymentModel = new CustomerPaymentModel();
                        objCustomerPaymentModel.PaymentID = PaymentID;
                        objCustomerPaymentModel.TransactionID = TransactionID;
                        objCustomerPaymentModel.PaymentStatus = StatusId;
                        objCustomerPaymentModel.UpdatedOn = DateTime.Now;
                        objCustomerTransactionsDB.UpdatePaymentStatus(objCustomerPaymentModel);
                        result = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                result = "Exception";
            }

            return result;

        }
        [HttpGet]
        public string GetPaymentDetailsByID(string pid)
        {
            PaymentRequest objPaymentRequest = new PaymentRequest();
            string result =null;
            
            var objCustomerTransaction = UhDB.CustomerTransactions.Where(x => x.PayementID == pid && x.IsActive == true && x.IsDelete == false).SingleOrDefault();

            if (objCustomerTransaction != null)
            {
                var objCustomer = UhDB.Customers.Where(x => x.cuID == objCustomerTransaction.cuID && x.IsActive == true && x.IsDelete == false).SingleOrDefault();
                var objCustomerOfficialDetails = UhDB.CustomerOfficalDetails.Where(x => x.custODID == objCustomerTransaction.custODID && x.IsActive == true && x.IsDelete == false).SingleOrDefault();
               
                objPaymentRequest.FirstName = objCustomer.Name ?? "N/A";
                objPaymentRequest.Email = objCustomer.Email ?? "N/A";
                objPaymentRequest.Phone = objCustomer.Mobile ?? "N/A";
                objPaymentRequest.City = objCustomer.Email ?? "N/A";
                objPaymentRequest.State = objCustomer.City ?? "N/A";
                objPaymentRequest.Country = objCustomer?.Country ?? "N/A";
                objPaymentRequest.PostalCode = objCustomer?.Pincode != null ? objCustomer.Pincode.ToString() : "N/A";
                objPaymentRequest.Amount = objCustomerTransaction.TotalPrice.ToString() ?? "0.00";
                result = CalculateSignature(objPaymentRequest, pid);

            }
            else
            {
                result = "Not Exists";
            }


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


    }
}