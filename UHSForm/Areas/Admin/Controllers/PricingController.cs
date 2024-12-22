using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UHSForm.Areas.Admin.Controllers
{
    [Authorize(Roles = "10,11")]
    public class PricingController : Controller
    {
        // GET: Admin/Pricing
        public ActionResult Index()
        {
            return View();
        }
    }
}