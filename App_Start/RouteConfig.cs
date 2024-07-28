using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EmployeeRecordFourm
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "EditEmployee",
                url: "Home/Edit/{Id}",
                defaults: new { controller = "Home", action = "Edit" }
            );

            routes.MapRoute(
               name: "Delete",
               url: "Home/Delete/{Id}",
               defaults: new { controller = "Home", action = "ConfirmDelete", Id = UrlParameter.Optional }
            );

        }
    }
}
