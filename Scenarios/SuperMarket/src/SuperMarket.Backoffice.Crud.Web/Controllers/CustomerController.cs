using Microsoft.AspNetCore.Mvc;
using SuperMarket.Backoffice.Crud.Domain.Entities;
using SuperMarket.Backoffice.Crud.Infra.Dtos;
using System;
using System.Threading.Tasks;
using Tnf.Domain.Services;
using Tnf.Dto;

namespace SuperMarket.Backoffice.Crud.Web.Controllers
{
    [Route("api/customer")]
    public class CustomerController : TnfController
    {
        private readonly IDomainService<Customer, Guid> _customerDomainService;

        public CustomerController(IDomainService<Customer, Guid> customerDomainService)
        {
            _customerDomainService = customerDomainService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]RequestAllDto requestAll)
        {
            if (requestAll == null)
                return BadRequest(nameof(requestAll));

            var response = await _customerDomainService.GetAllAsync<CustomerDto>(requestAll);

            return CreateResponseOnGetAll(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, [FromQuery]RequestDto<Guid> request)
        {
            if (request == null)
                return BadRequest(nameof(request));

            if (id == Guid.Empty)
                return BadRequest(nameof(id));

            request.WithId(id);

            var response = await _customerDomainService.GetAsync<CustomerDto>(request);

            return CreateResponseOnGet(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CustomerDto customer)
        {
            if (customer == null)
                return BadRequest(nameof(customer));

            var builder = Customer.New(Notification)
                .WithName(customer.Name);

            customer.Id = await _customerDomainService.InsertAndGetIdAsync(builder);

            return CreateResponseOnPost(customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]CustomerDto customer)
        {
            if (customer == null)
                return BadRequest(nameof(customer));

            if (id == Guid.Empty)
                return BadRequest(nameof(id));

            var builder = Customer.New(Notification)
                .WithId(id);

            await _customerDomainService.UpdateAsync(builder);

            return CreateResponseOnPut(customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(nameof(id));

            await _customerDomainService.DeleteAsync(id);

            return CreateResponseOnDelete();
        }
    }
}
