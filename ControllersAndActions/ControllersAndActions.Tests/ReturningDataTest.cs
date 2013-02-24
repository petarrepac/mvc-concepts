using System.Web.Mvc;
using ControllersAndActions.Controllers;
using NUnit.Framework;

namespace ControllersAndActions.Tests
{
    [TestFixture]
    public class ReturningDataTest
    {

        [Test]
        public void ContentTest()
        {
            // Arrange - create the controller
            var target = new ReturningDataController();

            // Act - call the action method
            ContentResult result = target.Index();

            // Assert - check the result
            Assert.AreEqual("text/plain", result.ContentType);
            Assert.AreEqual("This is plain text", result.Content);
        }

    }
}
