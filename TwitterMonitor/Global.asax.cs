using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TwitterMonitor.Models;
using TwitterMonitor.App_Start;

namespace TwitterMonitor
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            ModelBinders.Binders.DefaultBinder = new CustomModelBinderTrim();
        }

        protected void Application_Error()
        {
            Server.ClearError();
            Response.Redirect("/error");
        }
    }
}
