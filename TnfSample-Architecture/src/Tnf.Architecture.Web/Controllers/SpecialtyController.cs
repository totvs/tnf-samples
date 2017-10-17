using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tnf.App.Dto.Request;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Common;
using Tnf.Architecture.Dto.Registration;

namespace Tnf.Architecture.Web.Controllers
{
    [Route(RouteConsts.Specialty)]
    public class SpecialtyController : ArchitectureControllerBase
    {
        private readonly ISpecialtyAppService _specialtyAppService;

        public SpecialtyController(ISpecialtyAppService specialtyAppService)
        {
            _specialtyAppService = specialtyAppService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetAllSpecialtiesDto requestDto)
        {
            var response = await _specialtyAppService.GetAllSpecialties(requestDto).ForAwait();

            return CreateResponse<SpecialtyDto>()
                        .FromErrorEnum(SpecialtyDto.Error.GetAllSpecialty)
                        .WithMessage(AppConsts.LocalizationSourceName, SpecialtyDto.Error.GetAllSpecialty)
                        .WithDto(response)
                        .Build();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromQuery]RequestDto requestDto)
        {
            requestDto.WithId(id);
            var response = await _specialtyAppService.GetSpecialty(requestDto).ForAwait();

            return CreateResponse<SpecialtyDto>()
                        .FromErrorEnum(SpecialtyDto.Error.GetSpecialty)
                        .WithMessage(AppConsts.LocalizationSourceName, SpecialtyDto.Error.GetSpecialty)
                        .WithDto(response)
                        .Build();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]SpecialtyDto specialty)
        {
            var response = await _specialtyAppService.CreateSpecialty(specialty).ForAwait();

            return CreateResponse<SpecialtyDto>()
                        .FromErrorEnum(SpecialtyDto.Error.PostSpecialty)
                        .WithMessage(AppConsts.LocalizationSourceName, SpecialtyDto.Error.PostSpecialty)
                        .WithDto(response)
                        .Build();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]SpecialtyDto specialty)
        {
            var response = await _specialtyAppService.UpdateSpecialty(id, specialty).ForAwait();

            return CreateResponse<SpecialtyDto>()
                        .FromErrorEnum(SpecialtyDto.Error.PutSpecialty)
                        .WithMessage(AppConsts.LocalizationSourceName, SpecialtyDto.Error.PutSpecialty)
                        .WithDto(response)
                        .Build();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _specialtyAppService.DeleteSpecialty(id).ForAwait();

            return CreateResponse<SpecialtyDto>()
                        .FromErrorEnum(SpecialtyDto.Error.DeleteSpecialty)
                        .WithMessage(AppConsts.LocalizationSourceName, SpecialtyDto.Error.DeleteSpecialty)
                        .Build();
        }
    }
}
