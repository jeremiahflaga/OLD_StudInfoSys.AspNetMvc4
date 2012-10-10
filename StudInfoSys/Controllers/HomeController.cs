using System.Web.Mvc;

namespace StudInfoSys.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "This app records student grades of from Preparatory level to College and Graduate levels.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "This app is a practice project I made where I used my new knowledge on Entity Framework Code First, Repository Pattern, etc. on ASP.NET MVC4";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Jeremiah M. Flaga";

            return View();
        }
    }
}
