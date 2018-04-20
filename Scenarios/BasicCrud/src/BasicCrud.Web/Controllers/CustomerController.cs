using BasicCrud.Application.Services.Interfaces;
using BasicCrud.Dto;
using BasicCrud.Dto.Customer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.Dto;

namespace BasicCrud.Web.Controllers
{
    /// <summary>
    /// Customer API
    /// </summary>
    [Route(WebConstants.CustomerRouteName)]
    public class CustomerController : TnfController
    {
        private readonly ICustomerAppService _appService;
        private const string _name = "Customer";

        public CustomerController(ICustomerAppService appService)
        {
            _appService = appService;
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <param name="requestDto">Request params</param>
        /// <returns>List of customers</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IListDto<CustomerDto>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> GetAll([FromQuery]CustomerRequestAllDto requestDto)
        {
            var response = await _appService.GetAllAsync(requestDto);

            return CreateResponseOnGetAll(response, _name);
        }

        /// <summary>
        /// Get customer
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <param name="requestDto">Request params</param>
        /// <returns>Customer requested</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Get(Guid id, [FromQuery]RequestDto requestDto)
        {
            var request = new DefaultRequestDto(id, requestDto);

            var response = await _appService.GetAsync(request);

            return CreateResponseOnGet(response, _name);
        }

        /// <summary>
        /// Create a new customer
        /// </summary>
        /// <param name="customerDto">Customer to create</param>
        /// <returns>Customer created</returns>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Post([FromBody]CustomerDto customerDto)
        {
            customerDto = await _appService.CreateAsync(customerDto);

            return CreateResponseOnPost(customerDto, _name);
        }

        /// <summary>
        /// Update a customer
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <param name="customerDto">Customer content to update</param>
        /// <returns>Updated customer</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CustomerDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Put(Guid id, [FromBody]CustomerDto customerDto)
        {
            customerDto = await _appService.UpdateAsync(id, customerDto);

            return CreateResponseOnPut(customerDto, _name);
        }

        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <param name="id">Customer id</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _appService.DeleteAsync(id);

            return CreateResponseOnDelete(_name);
        }
    }
}
