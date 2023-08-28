using System.Web.Mvc;

namespace HydrusSharp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}