using System.Web.Mvc;
using System.Web.Routing;

namespace WorkingWithAreas
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null, 
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "WorkingWithAreas.Controllers" }
            );
        }
    }
}