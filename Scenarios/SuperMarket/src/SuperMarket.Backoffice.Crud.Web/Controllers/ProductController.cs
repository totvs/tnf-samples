using Microsoft.AspNetCore.Mvc;
using SuperMarket.Backoffice.Crud.Domain.Entities;
using SuperMarket.Backoffice.Crud.Infra.Dtos;
using System;
using System.Threading.Tasks;
using Tnf.Domain.Services;
using Tnf.Dto;

namespace SuperMarket.Backoffice.Crud.Web.Controllers
{
    [Route("api/product")]
    public class ProductController : TnfController
    {
        private readonly IDomainService<Product, Guid> _productDomainService;

        public ProductController(IDomainService<Product, Guid> productDomainService)
        {
            _productDomainService = productDomainService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]RequestAllDto requestAll)
        {
            if (requestAll == null)
                return BadRequest(nameof(requestAll));

            var response = await _productDomainService.GetAllAsync<ProductDto>(requestAll);

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

            var response = await _productDomainService.GetAsync<ProductDto>(request);

            return CreateResponseOnGet(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProductDto product)
        {
            if (product == null)
                return BadRequest(nameof(product));

            var builder = Product.New(Notification)
                .WithDescription(product.Description)
                .WithValue(product.Value);

            product.Id = await _productDomainService.InsertAndGetIdAsync(builder);

            return CreateResponseOnPost(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]ProductDto product)
        {
            if (product == null)
                return BadRequest(nameof(product));

            if (id == Guid.Empty)
                return BadRequest(nameof(id));

            var builder = Product.New(Notification)
                .WithId(id)
                .WithDescription(product.Description)
                .WithValue(product.Value);

            await _productDomainService.UpdateAsync(builder);

            return CreateResponseOnPut(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(nameof(id));

            await _productDomainService.DeleteAsync(id);

            return CreateResponseOnDelete();
        }
    }
}
