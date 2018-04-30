using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tnf.Runtime.Session;

namespace Security.Web.Controllers
{
    [Route(WebConstants.SessionRouteName)]
    [Authorize]
    public class SessionController : TnfController
    {
        private readonly ITnfSession _tnfSession;

        public SessionController(ITnfSession tnfSession)
        {
            _tnfSession = tnfSession;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new
            {
                HttpContext.User.Identity.IsAuthenticated,
                _tnfSession.TenantId,
                _tnfSession.UserId
            });
        }
    }
}
