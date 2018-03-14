using BasicCrud.Domain.Entities;
using BasicCrud.Domain.Interfaces.Repositories;
using BasicCrud.Infra.Oracle.Context;
using System;
using System.Threading.Tasks;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace BasicCrud.Infra.Oracle.Repositories
{
    public class ProductRepository : EfCoreRepositoryBase<BasicCrudDbContext, Product, Guid>, IProductRepository
    {
        public ProductRepository(IDbContextProvider<BasicCrudDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public async Task DeleteProductAsync(Guid id)
            => await DeleteAsync(id);

        public async Task<Guid> InsertProductAndGetIdAsync(Product product)
            => await InsertAndGetIdAsync(product);

        public async Task<Product> UpdateProductAsync(Product product)
            => await UpdateAsync(product);
    }
}
