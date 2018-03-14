using BasicCrud.Dto.Product;
using System;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.Repositories;

namespace BasicCrud.Infra.ReadInterfaces
{
    public interface IProductReadRepository : IRepository
    {
        Task<ProductDto> GetProductAsync(IRequestDto<Guid> key);

        Task<IListDto<ProductDto, Guid>> GetAllProductsAsync(ProductRequestAllDto key);
    }
}
