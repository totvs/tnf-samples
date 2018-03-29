using Microsoft.AspNetCore.Mvc;
using SuperMarket.Backoffice.Crud.Domain.Entities;
using SuperMarket.Backoffice.Crud.Infra.Dtos;
using SuperMarket.Backoffice.Crud.Infra.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.Domain.Services;
using Tnf.Dto;

namespace SuperMarket.Backoffice.Crud.Web.Controllers
{
    /// <summary>
    /// Product API
    /// </summary>
    [Route(WebConstants.ProductRouteName)]
    public class ProductController : TnfController
    {
        private readonly IDomainService<Product, Guid> _productDomainService;
        private readonly IPriceTableRepository _priceTableRepository;
        private const string name = "Product";

        public ProductController(IDomainService<Product, Guid> productDomainService, IPriceTableRepository priceTableRepository)
        {
            _productDomainService = productDomainService;
            _priceTableRepository = priceTableRepository;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <param name="requestAll">Request all params</param>
        /// <returns>Customer list</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IListDto<ProductDto, Guid>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> GetAll([FromQuery]ProductRequestAllDto requestAll)
        {
            if (requestAll == null)
                return BadRequest(ListDto<ProductDto, Guid>.Empty());

            var response = await _productDomainService.GetAllAsync<ProductDto>(requestAll,
                (p) => requestAll.Description.IsNullOrEmpty() || p.Description.Contains(requestAll.Description));

            return CreateResponseOnGetAll(response, name);
        }

        /// <summary>
        /// Get product
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <param name="request">Request params</param>
        /// <returns>Product</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Get(Guid id, [FromQuery]RequestDto<Guid> request)
        {
            if (request == null)
                return BadRequest(ProductDto.NullInstance);

            if (id == Guid.Empty)
                return BadRequest(ProductDto.NullInstance);

            request.WithId(id);

            var response = await _productDomainService.GetAsync<ProductDto>(request);

            return CreateResponseOnGet<ProductDto, Guid>(response, name);
        }

        /// <summary>
        /// Get price table
        /// </summary>
        /// <returns>Price table</returns>
        [HttpGet("pricetable")]
        [ProducesResponseType(typeof(Dictionary<Guid, decimal>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> GetPriceTable()
        {
            var response = await _priceTableRepository.GetPriceTable();

            return CreateResponseOnGet(response);
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="product">Product to create</param>
        /// <returns>Created product</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Post([FromBody]ProductDto product)
        {
            if (product == null)
                return BadRequest(ProductDto.NullInstance);

            var builder = Product.New(Notification)
                .WithDescription(product.Description)
                .WithValue(product.Value);

            product.Id = await _productDomainService.InsertAndGetIdAsync(builder);

            return CreateResponseOnPost<ProductDto, Guid>(product, name);
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="product">Product to update values</param>
        /// <returns>Updated product</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Put(Guid id, [FromBody]ProductDto product)
        {
            if (product == null)
                return BadRequest(ProductDto.NullInstance);

            if (id == Guid.Empty)
                return BadRequest(ProductDto.NullInstance);

            var builder = Product.New(Notification)
                .WithId(id)
                .WithDescription(product.Description)
                .WithValue(product.Value);

            await _productDomainService.UpdateAsync(builder);

            product.Id = id;

            return CreateResponseOnPut<ProductDto, Guid>(product, name);
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            await _productDomainService.DeleteAsync(id);

            return CreateResponseOnDelete<CustomerDto, Guid>(name);
        }
    }
}
