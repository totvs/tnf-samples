using Security.Domain.Entities;
using Security.Domain.Interfaces.Repositories;
using Security.Infra.Context;
using System;
using System.Threading.Tasks;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace Security.Infra
{
    public class ProductRepository : EfCoreRepositoryBase<CrudDbContext, Product>, IProductRepository
    {
        public ProductRepository(IDbContextProvider<CrudDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public async Task DeleteProductAsync(Guid id)
            => await DeleteAsync(w=>w.Id == id);

        public async Task<Product> InsertProductAndGetIdAsync(Product product)
            => await InsertAndSaveChangesAsync(product);

        public async Task<Product> UpdateProductAsync(Product product)
            => await UpdateAsync(product);
    }
}
