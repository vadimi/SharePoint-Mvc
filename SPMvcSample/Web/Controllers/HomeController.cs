using System.Web.Mvc;

namespace SPMvcSample.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Home action message";

            return View();
        }

        public ActionResult About()
        {
            ViewData["Message"] = "About action message";

            return View();
        }
    }
}
