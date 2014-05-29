using System.Web.Mvc;

namespace CodeWarrior.App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("_Layout");
        }
    }
}