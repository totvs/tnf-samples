using BasicCrud.Domain.Entities;
using System;
using System.Threading.Tasks;
using Tnf.Repositories;

namespace BasicCrud.Domain.Interfaces.Repositories
{
    // Para que essa interface seja registrada por convenção ela precisa herdar de alguma dessas interfaces: ITransientDependency, IScopedDependency, ISingletonDependency
    public interface IProductRepository : IRepository
    {
        Task<Product> InsertProductAndGetIdAsync(Product product);

        Task<Product> UpdateProductAsync(Product product);

        Task DeleteProductAsync(Guid id);
    }
}
