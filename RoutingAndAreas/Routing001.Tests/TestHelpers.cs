using System;
using System.Reflection;
using System.Web;
using System.Web.Routing;
using Moq;
using NUnit.Framework;

namespace Routing001.Tests
{
    public static class TestHelpers
    {

        public static HttpContextBase CreateHttpContext(string targetUrl = null, string httpMethod = "GET")
        {
            // create the mock request
            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

            // create the mock response
            var mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

            // create the mock context, using the request and response
            var mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            // return the mocked context
            return mockContext.Object;
        }

        private static bool ValueCompare(object v1, object v2)
        {
            return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0;
        }

        public static bool TestIncomingRouteResult(RouteData routeResult, string controller, string action, object propertySet = null)
        {
            bool controllerOk = ValueCompare(routeResult.Values["controller"], controller);
            bool actionOk = ValueCompare(routeResult.Values["action"], action);

            if (!controllerOk || !actionOk)
                return false;

            if (propertySet != null)
            {
                PropertyInfo[] propInfo = propertySet.GetType().GetProperties();
                foreach (PropertyInfo pi in propInfo)
                {
                    if (!routeResult.Values.ContainsKey(pi.Name))
                        return false;

                    if (!ValueCompare(routeResult.Values[pi.Name], pi.GetValue(propertySet, null)))
                        return false;
                }
            }
            return true;
        }

        public static void TestRouteMatch(Action<RouteCollection> registerRouteAction, 
            string url, string controller, string action, 
            object routeProperties = null, string httpMethod = "GET")
        {
            // Arrange
            var routes = new RouteCollection();
            registerRouteAction(routes);

            // Act - process the route
            RouteData result = routes.GetRouteData(CreateHttpContext(url, httpMethod));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, controller, action, routeProperties));
        }

        public static void TestRouteFail(Action<RouteCollection> registerRouteAction, string url)
        {
            // Arrange
            var routes = new RouteCollection();
            registerRouteAction(routes);

            // Act - process the route
            RouteData result = routes.GetRouteData(CreateHttpContext(url));

            // Assert
            Assert.IsTrue(result == null || result.Route == null);
        }
    }
}
