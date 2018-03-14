using BasicCrud.Application.AppServices.Interfaces;
using BasicCrud.Dto.Product;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tnf.Dto;

namespace BasicCrud.Web.Controllers
{
    [Route(WebConstants.ProductRouteName)]
    public class ProductController : TnfController
    {
        private readonly IProductAppService appService;
        private const string name = "Product";

        public ProductController(IProductAppService appService)
        {
            this.appService = appService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]ProductRequestAllDto requestDto)
        {
            var response = await appService.GetAllProductAsync(requestDto);

            return CreateResponseOnGetAll(response, name);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, [FromQuery]RequestDto<Guid> requestDto)
        {
            requestDto.WithId(id);

            var response = await appService.GetProductAsync(requestDto);

            return CreateResponseOnGet<ProductDto, Guid>(response, name);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProductDto customerDto)
        {
            customerDto = await appService.CreateProductAsync(customerDto);

            return CreateResponseOnPost<ProductDto, Guid>(customerDto, name);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]ProductDto customerDto)
        {
            customerDto = await appService.UpdateProductAsync(id, customerDto);

            return CreateResponseOnPut<ProductDto, Guid>(customerDto, name);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await appService.DeleteProductAsync(id);

            return CreateResponseOnDelete<ProductDto, Guid>(name);
        }
    }
}
