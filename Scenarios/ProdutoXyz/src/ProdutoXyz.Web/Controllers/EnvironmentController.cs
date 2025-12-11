using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProdutoXyz.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/environment")]
    public class EnvironmentController : TnfController
    {
        private readonly RacConfiguration _racConfiguration;
        private readonly IConfiguration _configuration;

        public EnvironmentController(RacConfiguration racConfiguration, IConfiguration configuration)
        {
            _racConfiguration = racConfiguration;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult Get()
        {
            if (_configuration.TryGetExternalAuthorityEndpoint(out string externalAuthorityEndpoint))
            {
                return Ok(new
                {
                    AuthorityEndPoint = externalAuthorityEndpoint
                });
            }

            return Ok(new
            {
                AuthorityEndPoint = _racConfiguration.AuthorityEndpoint
            });
        }
    }
}
