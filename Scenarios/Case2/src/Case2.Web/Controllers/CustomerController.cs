using Case2.Infra.Dtos;
using Case2.Infra.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.Repositories;

namespace Case2.Web.Controllers
{
    [Route("api/customers")]
    public class CustomerController : TnfController
    {
        private readonly IRepository<Customer, Guid> _customerRepository;

        public CustomerController(IRepository<Customer, Guid> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]RequestAllDto request)
        {
            var response = await _customerRepository.GetAllAsync<CustomerDto>(request);

            return CreateResponseOnGetAll(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, [FromQuery]RequestDto<Guid> requestDto)
        {
            requestDto.WithId(id);

            var response = await _customerRepository.GetAsync(requestDto);

            return CreateResponseOnGet<CustomerDto, Guid>(response.MapTo<CustomerDto>());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CustomerDto customer)
        {
            var entity = customer.MapTo<Customer>();

            entity.Id = await _customerRepository.InsertAndGetIdAsync(entity);

            return CreateResponseOnPost<CustomerDto, Guid>(entity.MapTo<CustomerDto>());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]CustomerDto employee)
        {
            var entity = employee.MapTo<Customer>();
            entity.Id = id;

            await _customerRepository.UpdateAsync(entity);

            return CreateResponseOnPut<CustomerDto, Guid>(entity.MapTo<CustomerDto>());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _customerRepository.DeleteAsync(w => w.Id == id);

            return CreateResponseOnDelete<CustomerDto, Guid>();
        }
    }
}
