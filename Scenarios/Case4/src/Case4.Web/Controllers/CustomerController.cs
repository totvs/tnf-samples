using Case4.Domain;
using Case4.Infra.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tnf.Domain.Services;
using Tnf.Dto;

namespace Case4.Web.Controllers
{
    [Route("api/customers")]
    public class CustomerController : TnfController
    {
        private readonly IDomainService<Customer, Guid> _crudService;

        public CustomerController(IDomainService<Customer, Guid> crudService)
        {
            _crudService = crudService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]RequestAllDto requestDto)
        {
            var response = await _crudService.GetAllAsync<CustomerDto>(requestDto);

            return CreateResponseOnGetAll(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, [FromQuery]RequestDto<Guid> requestDto)
        {
            requestDto.WithId(id);

            var response = await _crudService.GetAsync(requestDto);
            var customerDto = response.MapTo<CustomerDto>();

            return CreateResponseOnGet<CustomerDto, Guid>(customerDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CustomerDto customerDto)
        {
            var build = Customer.Create(Notification)
                .WithName(customerDto.Name)
                .WithEmail(customerDto.Email);

            customerDto.Id = await _crudService.InsertAndGetIdAsync(build);

            return CreateResponseOnPost<CustomerDto, Guid>(customerDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]CustomerDto customerDto)
        {
            var build = Customer.Create(Notification)
                .WithId(customerDto.Id)
                .WithName(customerDto.Name)
                .WithEmail(customerDto.Email);

            var response = await _crudService.UpdateAsync(build);

            customerDto = response.MapTo<CustomerDto>();

            return CreateResponseOnPut<CustomerDto, Guid>(customerDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _crudService.DeleteAsync(id);

            return CreateResponseOnDelete<CustomerDto, Guid>();
        }
    }
}
