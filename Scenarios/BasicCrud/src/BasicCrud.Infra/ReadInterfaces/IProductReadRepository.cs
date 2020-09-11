using BasicCrud.Domain.Entities;
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
        Task<Product> GetProductAsync(DefaultRequestDto key);

        Task<Product> GetProductAsync(Guid id);

        Task<IListDto<ProductDto>> GetAllProductsAsync(ProductRequestAllDto key);
    }
}
