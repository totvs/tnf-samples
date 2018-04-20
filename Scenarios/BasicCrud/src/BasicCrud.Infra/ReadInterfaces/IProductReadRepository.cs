using BasicCrud.Dto;
using BasicCrud.Dto.Product;
using System;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.Repositories;

namespace BasicCrud.Infra.ReadInterfaces
{
    // Para que essa interface seja registrada por convenção ela precisa herdar de alguma dessas interfaces: ITransientDependency, IScopedDependency, ISingletonDependency
    public interface IProductReadRepository : IRepository
    {
        Task<ProductDto> GetProductAsync(DefaultRequestDto key);

        Task<IListDto<ProductDto>> GetAllProductsAsync(ProductRequestAllDto key);
    }
}
