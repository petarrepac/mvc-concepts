using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;
using ControllersAndActions.Models;

namespace ControllersAndActions.Controllers
{
    public class ReturningDataController : Controller
    {
        public ContentResult Index()
        {
            string message = "This is plain text";
            return Content(message, "text/plain", Encoding.Default);
        }

        public object PlainTextDirectly()
        {
            return "This is plain text";
        }

        public ContentResult GetMoviesInXml()
        {
            Movie[] movies = Movie.GetMovies();
                
            var data = new XElement("MovieList", movies.Select(e => new XElement("Movie",
                                                                                 new XAttribute("title", e.Title),
                                                                                 new XAttribute("description", e.Description),
                                                                                 new XAttribute("directorName", e.DirectorName))));
            return Content(data.ToString(), "text/xml");
        }

        [HttpPost]
        public JsonResult GetMoviesInJSon()
        {
            Movie[] movies = Movie.GetMovies();
            return Json(movies);
        }
    }
}
