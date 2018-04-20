using BasicCrud.Domain.Entities;
using BasicCrud.Dto;
using BasicCrud.Dto.Product;
using BasicCrud.Infra.Context;
using BasicCrud.Infra.ReadInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace BasicCrud.Infra.Repositories.ReadRepositories
{
    public class ProductReadRepository : EfCoreRepositoryBase<CrudDbContext, Product>, IProductReadRepository
    {
        public ProductReadRepository(IDbContextProvider<CrudDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public async Task<IListDto<ProductDto>> GetAllProductsAsync(ProductRequestAllDto key)
            => await GetAllAsync<ProductDto>(key, p => key.Description.IsNullOrEmpty() || p.Description.Contains(key.Description));

        public async Task<ProductDto> GetProductAsync(DefaultRequestDto key)
        {
            var entity = await GetAsync(key);

            return entity.MapTo<ProductDto>();
        }
    }
}
