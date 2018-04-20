using BasicCrud.Application.Services.Interfaces;
using BasicCrud.Dto;
using BasicCrud.Dto.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tnf.Dto;

namespace BasicCrud.Web.Tests.Mocks
{
    public class ProductAppServiceMock : IProductAppService
    {
        public static Guid productGuid = Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637");

        private List<ProductDto> list = new List<ProductDto>() {
            new ProductDto() { Id = productGuid, Description = "Product A", Value = 5 },
            new ProductDto() { Id = Guid.NewGuid(), Description = "Product B", Value = 10 },
            new ProductDto() { Id = Guid.NewGuid(), Description = "Product C", Value = 20 }
        };

        public Task<ProductDto> CreateProductAsync(ProductDto dto)
        {
            if (dto == null)
                return Task.FromResult<ProductDto>(null);

            dto.Id = Guid.NewGuid();
            list.Add(dto);

            return dto.AsTask();
        }

        public Task DeleteProductAsync(Guid id)
        {
            list.RemoveAll(c => c.Id == id);

            return Task.CompletedTask;
        }

        public Task<ProductDto> GetProductAsync(DefaultRequestDto id)
        {
            var dto = list.FirstOrDefault(c => c.Id == id.Id);

            return dto.AsTask();
        }

        public Task<IListDto<ProductDto>> GetAllProductAsync(ProductRequestAllDto request)
        {
            IListDto<ProductDto> result = new ListDto<ProductDto> { HasNext = false, Items = list };

            return result.AsTask();
        }

        public Task<ProductDto> UpdateProductAsync(Guid id, ProductDto dto)
        {
            if (dto == null)
                return Task.FromResult<ProductDto>(null);

            list.RemoveAll(c => c.Id == id);
            dto.Id = id;
            list.Add(dto);

            return dto.AsTask();
        }
    }
}
