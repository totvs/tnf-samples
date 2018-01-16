using Case1.Domain;
using Case1.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Case1.Web.Controllers
{
    [Route("api/hash")]
    public class HashController : TnfController
    {
        private readonly IHashService _hashService;
        private readonly ICustomService _customService;

        public HashController(IHashService hashService, ICustomService customService)
        {
            _hashService = hashService;
            _customService = customService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]string value)
        {
            var response = _hashService.CalculateHash(value);

            return CreateResponseOnGet(new HashResponseDto(response));
        }
    }
}
