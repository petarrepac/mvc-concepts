using System;
using System.Web.Mvc;

namespace ControllersAndActions.Controllers
{
    public class DerivedController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Hello from the DerivedController Index method";
            return View("MyView");
        }

        public ActionResult Redirect()
        {
            return new RedirectResult("~/Derived/Index");
        }

        public ActionResult Redirect2()
        {
            return Redirect("/Derived/Index");
        }

        public ActionResult GetDataFromConvenienceProperties()
        {
            string userName = User.Identity.Name;
            string serverName = Server.MachineName;
            string clientIP = Request.UserHostAddress;
            DateTime dateStamp = HttpContext.Timestamp;

            return RedirectToAction("Index");
        }
    }
}
