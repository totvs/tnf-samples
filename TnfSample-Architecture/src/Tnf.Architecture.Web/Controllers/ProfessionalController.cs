using Microsoft.AspNetCore.Mvc;
using Tnf.Architecture.Application.Interfaces;

namespace Tnf.Architecture.Web.Controllers
{
    [Route("api/professional")]
    public class ProfessionalController : ArchitectureControllerBase
    {
        private readonly IProfessionalAppService _professionalAppService;

        public ProfessionalController(IProfessionalAppService professionalAppService)
        {
            _professionalAppService = professionalAppService;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            var response = _professionalAppService.GetAllProfessionals();

            return Ok(response);
        }
    }
}
