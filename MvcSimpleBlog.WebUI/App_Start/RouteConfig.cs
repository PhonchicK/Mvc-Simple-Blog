using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcSimpleBlog.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            #region Admin Routes
            routes.MapRoute(
                name: "AdminIndex",
                url: "admin/",
                new { controller = "Admin", action = "Index" }
            );

            routes.MapRoute(
                name: "Admin",
                url: "admin/{action}/",
                new { controller = "Admin" }
            );

            routes.MapRoute(
                name: "AdminBlogs",
                url: "admin/blogs/{page}/",
                new { controller = "Admin", action = "Blogs", page = UrlParameter.Optional }
            );
            #endregion
            #region Home Routes
            routes.MapRoute(
                name: "Home",
                url: "{page}/",
                new { controller = "Home", action = "Index", page = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "details",
                url: "details/{seoUrl}/",
                new { controller = "Home", action = "Details" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            #endregion
        }
    }
}
