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

            string clientIp = Request.UserHostAddress;
            string oldId = Request.Form["oldId"];
            string newId = Request.Form["newId"];

            DateTime dateStamp = HttpContext.Timestamp;

            // Retrieve posted data from Request.Form

            return RedirectToAction("Index");
        }

        public ActionResult Search(string query = "all", int page = 1)
        {
            // ...
            return null;
        }

        public void Index2()
        {
            string controller = (string)RouteData.Values["controller"];
            string action = (string)RouteData.Values["action"];

            Response.Write(string.Format("Controller: {0}, Action: {1}", controller, action));
            // ... or ...
            // Response.Redirect("/Some/Other/Url");
        }
    }
}
