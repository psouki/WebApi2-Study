using System.Web.Mvc;

namespace BeerDev.Controllers
{
    public class AllBeersController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }
    }
}