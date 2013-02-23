using System;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;

namespace Routing001.Tests
{
    [TestFixture]
    public class TestIncomingRoutes
    {

        [Test]
        public void RoutingIsConservative()
        {
            Action<RouteCollection> configAction = routes =>
                {
                    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
                    routes.MapRoute("NameOfARoute", "{controller}/{action}");
                };

            // check for the URL that we hope to receive
            TestHelper.TestRouteMatch(configAction, "~/Admin/Index", "Admin", "Index");

            // check that the values are being obtained from the segments
            TestHelper.TestRouteMatch(configAction, "~/One/Two", "One", "Two");

            // ensure that too many or too few segments fails to match
            TestHelper.TestRouteFail(configAction, "~/Admin/Index/Segment");
            TestHelper.TestRouteFail(configAction, "~/Admin");
            TestHelper.TestRouteFail(configAction, "~/");
        }

        [Test]
        public void DefaultValueForActionSegmentVariable()
        {
            Action<RouteCollection> configAction = routes =>
            {
                routes.MapRoute(null, "{controller}/{action}", new { action = "Index" });
            };

            TestHelper.TestRouteMatch(configAction, "~/Admin/Index", "Admin", "Index");
            TestHelper.TestRouteMatch(configAction, "~/Admin", "Admin", "Index");
            TestHelper.TestRouteMatch(configAction, "~/Article/List", "Article", "List");

            TestHelper.TestRouteFail(configAction, "~/Admin/Index/Segment");
            TestHelper.TestRouteFail(configAction, "~/");
        }

        [Test]
        public void DefaultValueForActionAndControllerSegmentVariables()
        {
            Action<RouteCollection> configAction = routes =>
            {
                routes.MapRoute(null, "{controller}/{action}", new { controller = "Home", action = "Index" });
            };

            TestHelper.TestRouteMatch(configAction, "~/Article/List", "Article", "List");
            TestHelper.TestRouteMatch(configAction, "~/Article", "Article", "Index");
            TestHelper.TestRouteMatch(configAction, "~/", "Home", "Index");

            TestHelper.TestRouteFail(configAction, "~/Home/Index/Segment");
        }

        [Test]
        public void StaticSegments()
        {
            Action<RouteCollection> configAction = routes =>
            {
                routes.MapRoute(null, "X{controller}/{action}");
                routes.MapRoute(null, "{controller}/{action}", new { controller = "Home", action = "Index" });
                routes.MapRoute(null, "Public/{controller}/{action}", new { controller = "Home", action = "Index" });
            };

            TestHelper.TestRouteMatch(configAction, "~/XArticle/List", "Article", "List");
            TestHelper.TestRouteMatch(configAction, "~/XArticle", "XArticle", "Index");
            TestHelper.TestRouteMatch(configAction, "~/Article", "Article", "Index");
            TestHelper.TestRouteMatch(configAction, "~/", "Home", "Index");
            TestHelper.TestRouteMatch(configAction, "~/Public", "Public", "Index");
            TestHelper.TestRouteMatch(configAction, "~/Public/Action", "Public", "Action");
            TestHelper.TestRouteMatch(configAction, "~/Public/Article/List", "Article", "List");

            TestHelper.TestRouteFail(configAction, "~/NotPublic/Index/Segment");
            TestHelper.TestRouteFail(configAction, "~/Public/S1/S1/S3");
        }

        [Test]
        public void CustomSegmentVariables()
        {
            Action<RouteCollection> configAction = routes =>
            {
                routes.MapRoute(null, "{controller}/{action}/{id}", 
                    new { controller = "Home", action = "Index", id = "DefaultId" });
            };


            TestHelper.TestRouteMatch(configAction, "~/", "Home", "Index", new {id = "DefaultId"});
            TestHelper.TestRouteMatch(configAction, "~/Article", "Article", "Index", new { id = "DefaultId" });
            TestHelper.TestRouteMatch(configAction, "~/Article/Detail", "Article", "Detail", new { id = "DefaultId" });
            TestHelper.TestRouteMatch(configAction, "~/Article/Detail/42", "Article", "Detail", new { id = "42" });

            TestHelper.TestRouteFail(configAction, "~/S1/S2/S3/S4");
        }

        [Test]
        public void OptionalSegmentVariables()
        {
            Action<RouteCollection> configAction = routes => 
                routes.MapRoute(null, "{controller}/{action}/{id}", 
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });


            TestHelper.TestRouteMatch(configAction, "~/", "Home", "Index");
            TestHelper.TestRouteMatch(configAction, "~/Article", "Article", "Index");
            TestHelper.TestRouteMatch(configAction, "~/Article/Detail", "Article", "Detail");
            TestHelper.TestRouteMatch(configAction, "~/Article/Detail/42", "Article", "Detail", new { id = "42" });

            TestHelper.TestRouteFail(configAction, "~/S1/S2/S3/S4");
        }

        [Test]
        public void VariableLengthsRoutes()
        {
            Action<RouteCollection> configAction = routes =>
                    routes.MapRoute(null, "{controller}/{action}/{id}/{*catchall}", 
                    new { controller = "Home", action = "Index", id = UrlParameter.Optional });


            TestHelper.TestRouteMatch(configAction, "~/", "Home", "Index");
            TestHelper.TestRouteMatch(configAction, "~/Article", "Article", "Index");
            TestHelper.TestRouteMatch(configAction, "~/Article/Detail", "Article", "Detail");
            TestHelper.TestRouteMatch(configAction, "~/Article/Detail/42", "Article", "Detail", new { id = "42" });
            TestHelper.TestRouteMatch(configAction, "~/S1/S2/S3/S4", "S1", "S2", new { id = "S3", catchall = "S4" });
            TestHelper.TestRouteMatch(configAction, "~/S1/S2/S3/S4/S5", "S1", "S2", new { id = "S3", catchall = "S4/S5" });
        }

        [Test]
        public void RegularExpressionsConstraints()
        {
            Action<RouteCollection> configAction = routes =>
                    routes.MapRoute(null, "{controller}/{action}/{id}/{*catchall}",
                    new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                    new { controller = "^H.*" });


            TestHelper.TestRouteMatch(configAction, "~/", "Home", "Index");
            TestHelper.TestRouteMatch(configAction, "~/Homer", "Homer", "Index");
            TestHelper.TestRouteMatch(configAction, "~/Homer/Simpson", "Homer", "Simpson");
            TestHelper.TestRouteFail(configAction, "~/Article");
            TestHelper.TestRouteFail(configAction, "~/Article/List");
        }

        [Test]
        public void ConstrainingByHttpMethod()
        {
            Action<RouteCollection> configAction = routes =>
                routes.MapRoute("MyRoute", "{controller}/{action}/{id}/{*catchall}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new { controller = "^H.*", action = "Index|About", 
                    httpMethod = new HttpMethodConstraint("GET, DELETE")},
                new[] { "URLsAndRoutes.Controllers" });

            TestHelper.TestRouteMatch(configAction, "~/", "Home", "Index", new {}, httpMethod: "GET");

            TestHelper.TestRouteFail(configAction, "~/", httpMethod: "POST");
        }

    }
}
