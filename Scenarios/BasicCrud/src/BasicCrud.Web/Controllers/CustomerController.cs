using BasicCrud.Application.AppServices.Interfaces;
using BasicCrud.Dto.Customer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tnf.Dto;

namespace BasicCrud.Web.Controllers
{
    [Route("api/customers")]
    public class CustomerController : TnfController
    {
        private readonly ICustomerAppService appService;

        public CustomerController(ICustomerAppService appService)
        {
            this.appService = appService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]CustomerRequestAllDto requestDto)
        {
            var response = await appService.GetAll(requestDto);

            return CreateResponseOnGetAll(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, [FromQuery]RequestDto<Guid> requestDto)
        {
            requestDto.WithId(id);

            var response = await appService.Get(requestDto);

            return CreateResponseOnGet<CustomerDto, Guid>(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CustomerDto customerDto)
        {
            customerDto = await appService.Create(customerDto);

            return CreateResponseOnPost<CustomerDto, Guid>(customerDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]CustomerDto customerDto)
        {
            customerDto = await appService.Update(id, customerDto);

            return CreateResponseOnPut<CustomerDto, Guid>(customerDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await appService.Delete(id);

            return CreateResponseOnDelete();
        }
    }
}
