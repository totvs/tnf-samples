using ProdutoXyz.Domain.Entities;
using ProdutoXyz.Domain.Interfaces.Repositories;
using ProdutoXyz.Infra.Context;
using System;
using System.Threading.Tasks;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace ProdutoXyz.Infra
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
