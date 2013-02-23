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
        public void Test1()
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
    }
}
