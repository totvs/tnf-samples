using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tnf.App.Dto.Request;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.WhiteHouse;

namespace Tnf.Architecture.Web.Controllers
{
    [Route(RouteConsts.WhiteHouse)]
    public class WhiteHouseController : ArchitectureControllerBase
    {
        private readonly IWhiteHouseAppService _whiteHouseAppService;

        public WhiteHouseController(IWhiteHouseAppService whiteHouseAppService)
        {
            _whiteHouseAppService = whiteHouseAppService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get([FromQuery]GetAllPresidentsDto requestDto)
        {
            var response = await _whiteHouseAppService.GetAllPresidents(requestDto);

            return CreateResponse<PresidentDto>()
                        .FromErrorEnum(PresidentDto.Error.GetAllPresident)
                        .WithMessage(AppConsts.LocalizationSourceName, PresidentDto.Error.GetAllPresident)
                        .WithDto(response)
                        .Build();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id, [FromQuery]RequestDto<string> requestDto)
        {
            var response = await _whiteHouseAppService.GetPresidentById(requestDto.WithId(id));

            return CreateResponse<PresidentDto>()
                        .FromErrorEnum(PresidentDto.Error.GetPresident)
                        .WithMessage(AppConsts.LocalizationSourceName, PresidentDto.Error.GetPresident)
                        .WithDto(response)
                        .Build();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PresidentDto president)
        {
            var response = await _whiteHouseAppService.InsertPresidentAsync(president);

            return CreateResponse<PresidentDto>()
                        .FromErrorEnum(PresidentDto.Error.PostPresident)
                        .WithMessage(AppConsts.LocalizationSourceName, PresidentDto.Error.PostPresident)
                        .WithDto(response)
                        .Build();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]PresidentDto president)
        {
            var response = await _whiteHouseAppService.UpdatePresidentAsync(id, president);

            return CreateResponse<PresidentDto>()
                        .FromErrorEnum(PresidentDto.Error.PutPresident)
                        .WithMessage(AppConsts.LocalizationSourceName, PresidentDto.Error.PutPresident)
                        .WithDto(response)
                        .Build();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _whiteHouseAppService.DeletePresidentAsync(id);

            return CreateResponse<PresidentDto>()
                        .FromErrorEnum(PresidentDto.Error.DeletePresident)
                        .WithMessage(AppConsts.LocalizationSourceName, PresidentDto.Error.DeletePresident)
                        .Build();
        }
    }
}
