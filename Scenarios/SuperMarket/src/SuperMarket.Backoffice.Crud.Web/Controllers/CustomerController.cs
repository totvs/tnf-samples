using Microsoft.AspNetCore.Mvc;
using SuperMarket.Backoffice.Crud.Domain.Entities;
using SuperMarket.Backoffice.Crud.Infra.Dtos;
using System;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.Domain.Services;
using Tnf.Dto;

namespace SuperMarket.Backoffice.Crud.Web.Controllers
{
    /// <summary>
    /// Customer API
    /// </summary>
    [Route(WebConstants.CustomerRouteName)]
    public class CustomerController : TnfController
    {
        private readonly IDomainService<Customer, Guid> _customerDomainService;
        private const string _name = "Customer";

        public CustomerController(IDomainService<Customer, Guid> customerDomainService)
        {
            _customerDomainService = customerDomainService;
        }


        /// <summary>
        /// Get all purchase orders
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IListDto<CustomerDto, Guid>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> GetAll([FromQuery]CustomerRequestAllDto requestAll)
        {
            if (requestAll == null)
                return BadRequest(ListDto<CustomerDto, Guid>.Empty());

            var response = await _customerDomainService.GetAllAsync<CustomerDto>(requestAll,
                (c) => requestAll.Name.IsNullOrEmpty() || c.Name.Contains(requestAll.Name));

            return CreateResponseOnGetAll(response, _name);
        }

        /// <summary>
        /// Get customer
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <param name="request">Request params</param>
        /// <returns>Customer</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Get(Guid id, [FromQuery]RequestDto<Guid> request)
        {
            if (request == null)
                return BadRequest(CustomerDto.NullInstance);

            if (id == Guid.Empty)
                return BadRequest(CustomerDto.NullInstance);

            request.WithId(id);

            var response = await _customerDomainService.GetAsync<CustomerDto>(request);

            return CreateResponseOnGet<CustomerDto, Guid>(response, _name);
        }

        /// <summary>
        /// Create a new Customer
        /// </summary>
        /// <param name="customer">Customer to create</param>
        /// <returns>Customer created</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Post([FromBody]CustomerDto customer)
        {
            if (customer == null)
                return BadRequest(CustomerDto.NullInstance);

            var builder = Customer.New(Notification)
                .WithName(customer.Name);

            customer.Id = await _customerDomainService.InsertAndGetIdAsync(builder);

            return CreateResponseOnPost<CustomerDto, Guid>(customer, _name);
        }

        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <param name="customer">Customer to update</param>
        /// <returns>Customer updated</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CustomerDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
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

            return CreateResponseOnPut<CustomerDto, Guid>(customer, _name);
        }

        /// <summary>
        /// Delete customer
        /// </summary>
        /// <param name="id">Customer id</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            await _customerDomainService.DeleteAsync(id);

            return CreateResponseOnDelete<CustomerDto, Guid>(_name);
        }
    }
}
