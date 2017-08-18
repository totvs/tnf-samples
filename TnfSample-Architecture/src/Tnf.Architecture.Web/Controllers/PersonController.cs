using Microsoft.AspNetCore.Mvc;
using Tnf.App.Dto.Request;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Common;
using Tnf.Architecture.Dto.Registration;

namespace Tnf.Architecture.Web.Controllers
{
    [Route(RouteConsts.Person)]
    public class PersonController : ArchitectureControllerBase
    {
        private readonly IPersonAppService _personAppService;

        public PersonController(IPersonAppService personAppService)
        {
            _personAppService = personAppService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]GetAllPeopleDto requestDto)
        {
            var response = _personAppService.GetAll(requestDto);

            return CreateResponse<PersonDto>()
                        .FromErrorEnum(PersonDto.Error.GetAllPeople)
                        .WithMessage(AppConsts.LocalizationSourceName, PersonDto.Error.GetAllPeople)
                        .WithDto(response)
                        .Build();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromQuery]RequestDto requestDto)
        {
            requestDto.WithId(id);
            var response = _personAppService.Get(requestDto);

            return CreateResponse<PersonDto>()
                        .FromErrorEnum(PersonDto.Error.GetPerson)
                        .WithMessage(AppConsts.LocalizationSourceName, PersonDto.Error.GetPerson)
                        .WithDto(response)
                        .Build();
        }

        [HttpPost]
        public IActionResult Post([FromBody]PersonDto person)
        {
            var response = _personAppService.Create(person);

            return CreateResponse<PersonDto>()
                        .FromErrorEnum(PersonDto.Error.PostPerson)
                        .WithMessage(AppConsts.LocalizationSourceName, PersonDto.Error.PostPerson)
                        .WithDto(response)
                        .Build();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]PersonDto person)
        {
            var response = _personAppService.Update(id, person);

            return CreateResponse<PersonDto>()
                        .FromErrorEnum(PersonDto.Error.PutPerson)
                        .WithMessage(AppConsts.LocalizationSourceName, PersonDto.Error.PutPerson)
                        .WithDto(response)
                        .Build();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personAppService.Delete(id);

            return CreateResponse<PersonDto>()
                        .FromErrorEnum(PersonDto.Error.DeletePerson)
                        .WithMessage(AppConsts.LocalizationSourceName, PersonDto.Error.DeletePerson)
                        .Build();
        }
    }
}
