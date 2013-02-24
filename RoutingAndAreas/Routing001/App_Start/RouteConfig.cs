using System.Web.Mvc;
using System.Web.Routing;
using Routing001.Infrastructure;

namespace Routing001
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.Add(new LegacyRoute(
                "~/some/oldRoute/to/test",
                "~/old/replaceThisOldRoute"));


            routes.MapRoute(null, "{controller}/{action}/{id}", 
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });

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