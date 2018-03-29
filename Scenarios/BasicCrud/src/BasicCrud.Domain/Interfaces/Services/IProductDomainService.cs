using BasicCrud.Domain.Entities;
using System;
using System.Threading.Tasks;
using Tnf.Domain.Services;

namespace BasicCrud.Domain.Interfaces.Services
{
    // Para que essa interface seja registrada por convenção ela precisa herdar de alguma dessas interfaces: ITransientDependency, IScopedDependency, ISingletonDependency
    public interface IProductDomainService : IDomainService
    {
        Task<Product> InsertProductAsync(Product.Builder builder);

        Task<Product> UpdateProductAsync(Product.Builder builder);

        Task DeleteProductAsync(Guid id);
    }
}
