using System.Web.Mvc;

namespace Routing001.Controllers
{
    public class LegacyController : Controller
    {
        public ActionResult GetLegacyUrl(string legacyUrl)
        {
            return View((object)legacyUrl);
        }
    }
}
