using BasicCrud.Application.AppServices.Interfaces;
using BasicCrud.Dto.Customer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tnf.Dto;

namespace BasicCrud.Web.Controllers
{
    [Route(WebConstants.CustomerRouteName)]
    public class CustomerController : TnfController
    {
        private readonly ICustomerAppService appService;
        private const string name = "Customer";

        public CustomerController(ICustomerAppService appService)
        {
            this.appService = appService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]CustomerRequestAllDto requestDto)
        {
            var response = await appService.GetAll(requestDto);

            return CreateResponseOnGetAll(response, name);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, [FromQuery]RequestDto<Guid> requestDto)
        {
            requestDto.WithId(id);

            var response = await appService.Get(requestDto);

            return CreateResponseOnGet<CustomerDto, Guid>(response, name);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CustomerDto customerDto)
        {
            customerDto = await appService.Create(customerDto);

            return CreateResponseOnPost<CustomerDto, Guid>(customerDto, name);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]CustomerDto customerDto)
        {
            customerDto = await appService.Update(id, customerDto);

            return CreateResponseOnPut<CustomerDto, Guid>(customerDto, name);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await appService.Delete(id);

            return CreateResponseOnDelete<CustomerDto, Guid>(name);
        }
    }
}
