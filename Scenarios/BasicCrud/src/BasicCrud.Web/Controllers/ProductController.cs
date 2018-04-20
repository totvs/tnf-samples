using BasicCrud.Application.Services.Interfaces;
using BasicCrud.Dto;
using BasicCrud.Dto.Product;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.Dto;

namespace BasicCrud.Web.Controllers
{
    /// <summary>
    /// Product API
    /// </summary>
    [Route(WebConstants.ProductRouteName)]
    public class ProductController : TnfController
    {
        private readonly IProductAppService _appService;
        private const string _name = "Product";

        public ProductController(IProductAppService appService)
        {
            _appService = appService;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <param name="requestDto">Request params</param>
        /// <returns>List of products</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IListDto<ProductDto>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> GetAll([FromQuery]ProductRequestAllDto requestDto)
        {
            var response = await _appService.GetAllProductAsync(requestDto);

            return CreateResponseOnGetAll(response, _name);
        }

        /// <summary>
        /// Get product
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="requestDto">Request params</param>
        /// <returns>Product requested</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Get(Guid id, [FromQuery]RequestDto requestDto)
        {
            var request = new DefaultRequestDto(id, requestDto);

            var response = await _appService.GetProductAsync(request);

            return CreateResponseOnGet(response, _name);
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="customerDto">Product to create</param>
        /// <returns>Product created</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Post([FromBody]ProductDto customerDto)
        {
            customerDto = await _appService.CreateProductAsync(customerDto);

            return CreateResponseOnPost(customerDto, _name);
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="customerDto">Product content to update</param>
        /// <returns>Updated product</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Put(Guid id, [FromBody]ProductDto customerDto)
        {
            customerDto = await _appService.UpdateProductAsync(id, customerDto);

            return CreateResponseOnPut(customerDto, _name);
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id">Product id</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _appService.DeleteProductAsync(id);

            return CreateResponseOnDelete(_name);
        }
    }
}
