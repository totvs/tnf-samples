using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tnf.Runtime.Session;

namespace ProdutoXyz.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/application")]
    public class ApplicationController : TnfController
    {
        private readonly ITnfSession _tnfSession;

        public ApplicationController(ITnfSession tnfSession)
        {
            _tnfSession = tnfSession;
        }

        [HttpGet("name")]
        [TnfAuthorize("ProdutoXyz.ApplicationName")]
        public IActionResult GetApplicationName()
        {
            return Ok("Produto XYZ");
        }

        [HttpGet("userInfo")]
        public async Task<IActionResult> UserInfo()
        {
            var accessToken = await HttpContext.GetTokenAsync();

            return Ok(new
            {
                accessToken,
                _tnfSession.TenantId,
                _tnfSession.UserId
            });
        }
    }
}
