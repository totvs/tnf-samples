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
        private const string PERSON_NAME = "Person";

        public PersonController(IPersonAppService personAppService)
        {
            _personAppService = personAppService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]GetAllPeopleDto requestDto)
        {
            var response = _personAppService.GetAll(requestDto);

            return CreateResponseOnGetAll(response, PERSON_NAME);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromQuery]RequestDto requestDto)
        {
            requestDto.WithId(id);
            var response = _personAppService.Get(requestDto);
            
            return CreateResponseOnGet(response, PERSON_NAME);
        }

        [HttpPost]
        public IActionResult Post([FromBody]PersonDto person)
        {
            var response = _personAppService.Create(person);

            return CreateResponseOnPost(response, PERSON_NAME);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]PersonDto person)
        {
            var response = _personAppService.Update(id, person);

            return CreateResponseOnPut(response, PERSON_NAME);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personAppService.Delete(id);

            return CreateResponseOnDelete<PersonDto>(PERSON_NAME);
        }
    }
}
