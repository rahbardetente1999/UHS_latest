using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UHSForm.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["UserSession"] == null) // Replace "UserSession" with your session key
            {
                filterContext.Result = new HttpStatusCodeResult(401, "Session Timeout");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}