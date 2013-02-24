using System;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;

namespace Routing001.Tests
{
    [TestFixture]
    public class TestOutgoingRoutes
    {

        [Test]
        public void TestOutgoingURLGeneration()
        {
            Action<RouteCollection> configAction = routes =>
                                                   routes.MapRoute(null, "{controller}/{action}/{id}",
                                                                   new
                                                                       {
                                                                           controller = "Home",
                                                                           action = "Index",
                                                                           id = UrlParameter.Optional
                                                                       });

            // Arrange
            var routeTable = new RouteCollection();
            configAction(routeTable);
            var context = new RequestContext(TestHelper.CreateHttpContext(null, "GET"), new RouteData());

            // Act - generate the URL
            string result = UrlHelper.GenerateUrl(null, "Index", "Home", null, routeTable, context, true);

            // Assert
            Assert.AreEqual("/", result);
        }

    }
}
