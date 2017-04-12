using Microsoft.AspNetCore.Mvc;
using Tnf.Domain.Uow;

namespace Tnf.Sample.Web.Controllers
{
    public class HomeController : SimpleTaskAppControllerBase
    {
        [UnitOfWork(IsDisabled = true)]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Tasks");
        }
    }
}