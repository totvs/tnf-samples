using ProdutoXyz.Dto;
using ProdutoXyz.Dto.Product;
using System;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Dto;

namespace ProdutoXyz.Application.Services.Interfaces
{
    // Para que essa interface seja registrada por convenção ela precisa herdar de alguma dessas interfaces: ITransientDependency, IScopedDependency, ISingletonDependency
    public interface IProductAppService : IApplicationService
    {
        Task<ProductDto> CreateProductAsync(ProductDto product);
        Task<ProductDto> UpdateProductAsync(Guid id, ProductDto product);
        Task DeleteProductAsync(Guid id);
        Task<ProductResponseDto> GetProductAsync(DefaultRequestDto id);
        Task<IListDto<ProductResponseDto>> GetAllProductAsync(ProductRequestAllDto request);
    }
}
