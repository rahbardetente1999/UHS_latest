using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UHSForm
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalFilters.Filters.Add(new AuthorizeAttribute());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            log4net.Config.XmlConfigurator.Configure();
            log4net.GlobalContext.Properties["Username"] = new HttpContextUserNameProvider();
        }
        public class HttpContextUserNameProvider
        {
            public override string ToString()
            {
                HttpContext context = HttpContext.Current;
                if (context != null && context.User != null && context.User.Identity.IsAuthenticated)
                {
                    return context.User.Identity.Name;
                }
                return "";
            }
        }
    }

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
