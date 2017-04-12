using Microsoft.AspNetCore.Mvc;
using Tnf.AspNetCore.AspNetCore.Mvc.Controllers;

namespace Tnf.Sample.Web.Controllers
{
    public class HomeController : TnfController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
