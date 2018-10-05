using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tnf.Runtime.Session;

namespace ProdutoXyz.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/application")]
    [TnfAuthorize("ProdutoXyz.Application")]
    public class ApplicationController : TnfController
    {
        private readonly ITnfSession _tnfSession;

        public ApplicationController(ITnfSession tnfSession)
        {
            _tnfSession = tnfSession;
        }

        [HttpGet("name")]
        [TnfAuthorize("ProdutoXyz.Application.Name")]
        public IActionResult GetApplicationName()
        {
            return Ok("Produto XYZ");
        }

        [HttpGet("userInfo")]
        [TnfAuthorize("ProdutoXyz.Application.UserInfo")]
        public async Task<IActionResult> UserInfo()
        {
            var accessToken = await HttpContext.GetTokenAsync();
            var tenantName = HttpContext.GetTenantName();
            var userFullName = HttpContext.GetUserFullName();
            var userName = HttpContext.GetUserName();

            return Ok(new
            {
                accessToken,
                _tnfSession.TenantId,
                _tnfSession.UserId,
                TenantName = tenantName,
                UserFullName = userFullName,
                UserName = userName
            });
        }
    }
}
