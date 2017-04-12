using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Tnf.AspNetCore.AspNetCore.Mvc.Controllers;
using Abp.AspNetCore.Mvc.Authorization;

namespace Tnf.Sample.Web.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class IdentityController : TnfController
    {
        [HttpGet]
        public ActionResult Get()
        {
            return new JsonResult(User.Claims.Select(
                c => new { c.Type, c.Value }));
        }
    }
}