using BasicCrud.Dto.Product;
using System;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Dto;

namespace BasicCrud.Application.AppServices.Interfaces
{
    public interface IProductAppService : IApplicationService
    {
        Task<ProductDto> CreateProductAsync(ProductDto product);
        Task<ProductDto> UpdateProductAsync(Guid id, ProductDto product);
        Task DeleteProductAsync(Guid id);
        Task<ProductDto> GetProductAsync(IRequestDto<Guid> id);
        Task<IListDto<ProductDto, Guid>> GetAllProductAsync(ProductRequestAllDto request);
    }
}
