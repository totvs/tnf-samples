using Microsoft.AspNetCore.Mvc;

namespace Tnf.Architecture.Web.Controllers
{
    public class HomeController : ArchitectureControllerBase
    {
        public ActionResult Index()
        {
            ViewBag.Title = L("President_Title");

            return View();
        }
    }
}
