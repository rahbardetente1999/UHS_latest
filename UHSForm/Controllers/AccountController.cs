using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UHSForm.Models;

namespace UHSForm.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(String email, string Password)
        {
            string result = "";
            try
            {
                if (email == "admin@mail.com" && Password == "Admin123")
                {
                    result = "Admin";
                }
                else if (email == "staff@mail.com" && Password == "Staff123")
                {
                    result = "Staff";
                }

                else
                {
                    result = "NotAuthorized";

                }
            }
            catch (Exception ex)
            {
                result = "Exception";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PaymentRequest(PaymentRequest objPaymentRequest)
        {
            string result = "";

            string PaymentLink = CalculateSignature(objPaymentRequest);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public string CalculateSignature(PaymentRequest request)
        {
            string result = null;
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

                    result = result1.payUrl;

                }
            }
            catch (Exception ex)
            {
                result = "Exception";
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