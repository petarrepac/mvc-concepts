using System.Web.Mvc;
using System.Web.Routing;

namespace Routing001
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("NameOfARoute", "{controller}/{action}");

            //var myRoute = new Route("{controller}/{action}", new MvcRouteHandler());
            //routes.Add("SomeRoute", myRoute);

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}