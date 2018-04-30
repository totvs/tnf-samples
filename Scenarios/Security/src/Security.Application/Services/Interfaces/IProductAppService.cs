using Security.Dto;
using Security.Dto.Product;
using System;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Dto;

namespace Security.Application.Services.Interfaces
{
    // Para que essa interface seja registrada por convenção ela precisa herdar de alguma dessas interfaces: ITransientDependency, IScopedDependency, ISingletonDependency
    public interface IProductAppService : IApplicationService
    {
        Task<ProductDto> CreateProductAsync(ProductDto product);
        Task<ProductDto> UpdateProductAsync(Guid id, ProductDto product);
        Task DeleteProductAsync(Guid id);
        Task<ProductDto> GetProductAsync(DefaultRequestDto id);
        Task<IListDto<ProductDto>> GetAllProductAsync(ProductRequestAllDto request);
    }
}
