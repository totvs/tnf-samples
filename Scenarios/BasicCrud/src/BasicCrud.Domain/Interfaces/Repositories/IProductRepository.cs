using BasicCrud.Domain.Entities;
using System;
using System.Threading.Tasks;
using Tnf.Repositories;

namespace BasicCrud.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IRepository
    {
        Task<Guid> InsertProductAndGetIdAsync(Product product);

        Task<Product> UpdateProductAsync(Product product);

        Task DeleteProductAsync(Guid id);
    }
}
