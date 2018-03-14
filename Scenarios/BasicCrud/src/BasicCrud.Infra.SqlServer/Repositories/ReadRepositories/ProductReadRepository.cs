using BasicCrud.Domain.Entities;
using BasicCrud.Dto.Product;
using BasicCrud.Infra.ReadInterfaces;
using BasicCrud.Infra.SqlServer.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace BasicCrud.Infra.SqlServer.Repositories.ReadRepositories
{
    public class ProductReadRepository : EfCoreRepositoryBase<BasicCrudDbContext, Product, Guid>, IProductReadRepository
    {
        public ProductReadRepository(IDbContextProvider<BasicCrudDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public async Task<IListDto<ProductDto, Guid>> GetAllProductsAsync(ProductRequestAllDto key)
            => await GetAllAsync<ProductDto>(key, p => key.Description.IsNullOrEmpty() || key.Description.Contains(key.Description));

        public async Task<ProductDto> GetProductAsync(IRequestDto<Guid> key)
        {
            var entity = await GetAsync(key);

            return entity.MapTo<ProductDto>();
        }
    }
}
