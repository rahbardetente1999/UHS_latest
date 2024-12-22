using System.Web.Mvc;

namespace UHSForm.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Admin_default",
                url: "Admin/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "UHSForm.Areas.Admin.Controllers" } // Specify the namespace for the Admin controllers
            );
        }
        //public override void RegisterArea(AreaRegistrationContext context) 
        //{
        //    context.MapRoute(
        //        "Admin_default",
        //        "Admin/{controller}/{action}/{id}",
        //        new { action = "Index", id = UrlParameter.Optional }
        //    );
        //}
    }
}