using Microsoft.AspNetCore.Mvc;
using SuperMarket.Backoffice.Crud.Domain.Entities;
using SuperMarket.Backoffice.Crud.Infra.Dtos;
using SuperMarket.Backoffice.Crud.Infra.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tnf.Caching;
using Tnf.Domain.Services;
using Tnf.Dto;

namespace SuperMarket.Backoffice.Crud.Web.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]ProductRequestAllDto requestAll)
        {
            if (requestAll == null)
                return BadRequest(ListDto<ProductDto, Guid>.Empty());

            var response = await _productDomainService.GetAllAsync<ProductDto>(requestAll,
                (p) => requestAll.Description.IsNullOrEmpty() || p.Description.Contains(requestAll.Description));

            return CreateResponseOnGetAll(response, name);
        }

        [HttpGet("{id}")]
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

        [HttpGet("pricetable")]
        public async Task<IActionResult> GetPriceTable([FromQuery]ProductRequestAllDto requestAll)
        {
            var response = await _priceTableRepository.GetPriceTable();

            return CreateResponseOnGet(response);
        }

        [HttpPost]
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            await _productDomainService.DeleteAsync(id);

            return CreateResponseOnDelete<CustomerDto, Guid>(name);
        }
    }
}
