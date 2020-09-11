using BasicCrud.Application.Services.Interfaces;
using BasicCrud.Dto;
using BasicCrud.Dto.Product;
using Microsoft.AspNetCore.JsonPatch;
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
        /// <param name="productDto">Product to create</param>
        /// <returns>Product created</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Post([FromBody]ProductDto productDto)
        {
            productDto = await _appService.CreateProductAsync(productDto);

            return CreateResponseOnPost(productDto, _name);
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="productDto">Product content to update</param>
        /// <returns>Updated product</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Put(Guid id, [FromBody]ProductDto productDto)
        {
            productDto = await _appService.UpdateProductAsync(id, productDto);

            return CreateResponseOnPut(productDto, _name);
        }

        /// <summary>
        /// Patch a product
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="patch">Product patch operation</param>
        /// <returns>Patched product</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument patch)
        {
            var productDto = await _appService.PatchProductAsync(id, patch);

            return CreateResponseOnPut(productDto, _name);
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
