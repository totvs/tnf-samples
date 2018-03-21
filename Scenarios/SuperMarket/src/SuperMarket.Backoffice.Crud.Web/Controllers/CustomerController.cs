using Microsoft.AspNetCore.Mvc;
using SuperMarket.Backoffice.Crud.Domain.Entities;
using SuperMarket.Backoffice.Crud.Infra.Dtos;
using System;
using System.Threading.Tasks;
using Tnf.Domain.Services;
using Tnf.Dto;

namespace SuperMarket.Backoffice.Crud.Web.Controllers
{
    [Route(WebConstants.CustomerRouteName)]
    public class CustomerController : TnfController
    {
        private readonly IDomainService<Customer, Guid> _customerDomainService;
        private const string name = "Customer";

        public CustomerController(IDomainService<Customer, Guid> customerDomainService)
        {
            _customerDomainService = customerDomainService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]CustomerRequestAllDto requestAll)
        {
            if (requestAll == null)
                return BadRequest(ListDto<CustomerDto, Guid>.Empty());

            var response = await _customerDomainService.GetAllAsync<CustomerDto>(requestAll,
                (c) => requestAll.Name.IsNullOrEmpty() || c.Name.Contains(requestAll.Name));

            return CreateResponseOnGetAll(response, name);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, [FromQuery]RequestDto<Guid> request)
        {
            if (request == null)
                return BadRequest(CustomerDto.NullInstance);

            if (id == Guid.Empty)
                return BadRequest(CustomerDto.NullInstance);

            request.WithId(id);

            var response = await _customerDomainService.GetAsync<CustomerDto>(request);

            return CreateResponseOnGet<CustomerDto, Guid>(response, name);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CustomerDto customer)
        {
            if (customer == null)
                return BadRequest(CustomerDto.NullInstance);

            var builder = Customer.New(Notification)
                .WithName(customer.Name);

            customer.Id = await _customerDomainService.InsertAndGetIdAsync(builder);

            return CreateResponseOnPost<CustomerDto, Guid>(customer, name);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]CustomerDto customer)
        {
            if (customer == null)
                return BadRequest(CustomerDto.NullInstance);

            if (id == Guid.Empty)
                return BadRequest(CustomerDto.NullInstance);

            var builder = Customer.New(Notification)
                .WithId(id)
                .WithName(customer.Name);

            await _customerDomainService.UpdateAsync(builder);

            customer.Id = id;

            return CreateResponseOnPut<CustomerDto, Guid>(customer, name);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            await _customerDomainService.DeleteAsync(id);

            return CreateResponseOnDelete<CustomerDto, Guid>(name);
        }
    }
}
