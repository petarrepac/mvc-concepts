using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;
using ControllersAndActions.Infrastructure;
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

        public FileResult AnnualReport()
        {
            string filename = @"c:\AnnualReport.pdf";
            string contentType = "application/pdf";
            string downloadName = "AnnualReport2011.pdf";

            return File(filename, contentType, downloadName);
        }

        public RssActionResult RSS()
        {
            Movie[] movies = Movie.GetMovies();

            return new RssActionResult<Movie>("My movies", movies, 
                e => new XElement("item",
                    new XAttribute("title", e.Title),
                    new XAttribute("description", e.Description),
                    new XAttribute("directorName", e.DirectorName)));
        }
    }
}
