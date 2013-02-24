using System.Web.Mvc;
using ControllersAndActions.Controllers;
using NUnit.Framework;

namespace ControllersAndActions.Tests
{
    [TestFixture]
    public class TestControllers
    {

        [Test]
        public void TestIndex()
        {
            // Arrange - create the controller
            var target = new ExampleController();

            // Act - call the action method
            ViewResult result = target.Index();

            // Assert - check the result
            Assert.AreEqual("Homepage", result.ViewName);
        }

        [Test]
        public void TestIndex2()
        {
            // Arrange - create the controller
            var target = new ExampleController();

            // Act - call the action method
            ViewResult result = target.Index2();

            // Assert - check the result
            Assert.AreEqual("Index2", result.ViewName);
            Assert.AreEqual("_AlternateLayoutPage", result.MasterName);
        }

        [Test]
        public void UseViewModelUntyped()
        {
            // Arrange - create the controller
            var target = new ExampleController();

            // Act - call the action method
            ViewResult result = target.UseViewModelUntyped();

            // Assert - check the result
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual("Hello, World", result.ViewData.Model);
        }


        [Test]
        public void UseViewBag()
        {
            // Arrange - create the controller
            var target = new ExampleController();

            // Act - call the action method
            ViewResult result = target.UseViewBag();

            // Assert - check the result
            Assert.AreEqual("Hello", result.ViewBag.Message);
        }

        [Test]
        public void RedirectTest()
        {
            // Arrange - create the controller
            var target = new ExampleController();

            // Act - call the action method
            RedirectResult result = target.Redirect();

            // Assert - check the result
            Assert.IsFalse(result.Permanent);
            Assert.AreEqual("/Example/Index", result.Url);
        }

        [Test]
        public void RedirectToRouteTest()
        {
            // Arrange - create the controller
            var target = new ExampleController();

            // Act - call the action method
            RedirectToRouteResult result = target.RedirectToRoute();

            // Assert - check the result
            Assert.IsFalse(result.Permanent);
            Assert.AreEqual("Example", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("MyID", result.RouteValues["ID"]);
        }

        [Test]
        public void RedirectToAction()
        {
            // Arrange - create the controller
            var target = new ExampleController();

            // Act - call the action method
            RedirectToRouteResult result = target.RedirectToAction();

            // Assert - check the result
            Assert.IsFalse(result.Permanent);
            Assert.AreEqual(null, result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
