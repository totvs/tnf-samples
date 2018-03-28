using BasicCrud.Application.Services.Interfaces;
using BasicCrud.Dto.Product;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;
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
        [ProducesResponseType(typeof(IListDto<ProductDto, Guid>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> GetAll([FromQuery]ProductRequestAllDto requestDto)
        {
            var response = await appService.GetAllProductAsync(requestDto);

            return CreateResponseOnGetAll(response, name);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Get(Guid id, [FromQuery]RequestDto<Guid> requestDto)
        {
            requestDto.WithId(id);

            var response = await appService.GetProductAsync(requestDto);

            return CreateResponseOnGet<ProductDto, Guid>(response, name);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Post([FromBody]ProductDto customerDto)
        {
            customerDto = await appService.CreateProductAsync(customerDto);

            return CreateResponseOnPost<ProductDto, Guid>(customerDto, name);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Put(Guid id, [FromBody]ProductDto customerDto)
        {
            customerDto = await appService.UpdateProductAsync(id, customerDto);

            return CreateResponseOnPut<ProductDto, Guid>(customerDto, name);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await appService.DeleteProductAsync(id);

            return CreateResponseOnDelete<ProductDto, Guid>(name);
        }
    }
}
