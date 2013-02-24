using System;
using System.Web.Mvc;

namespace ControllersAndActions.Controllers
{
    public class ExampleController : Controller
    {
        public ViewResult Index()
        {
            return View("Homepage");
        }

        public ViewResult Index2()
        {
            return View("Index2", "_AlternateLayoutPage");
        }

        public ViewResult UseViewModelUntyped()
        {
            return View((object)"Hello, World");
        }

        public ViewResult UseViewBag()
        {
            ViewBag.Message = "Hello";
            ViewBag.Date = DateTime.Now;
            return View();
        }

        public RedirectResult Redirect()
        {
            return Redirect("/Example/Index");
        }

        public RedirectToRouteResult RedirectToRoute()
        {
            return RedirectToRoute(new
            {
                controller = "Example",
                action = "Index",
                ID = "MyID"
            });
        }

        public RedirectToRouteResult RedirectToAction()
        {
            return RedirectToAction("Index");
        }
    }
}
