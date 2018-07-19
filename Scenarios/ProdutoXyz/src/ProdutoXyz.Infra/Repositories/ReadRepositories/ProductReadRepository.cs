using ProdutoXyz.Domain.Entities;
using ProdutoXyz.Dto;
using ProdutoXyz.Dto.Product;
using ProdutoXyz.Infra.Context;
using ProdutoXyz.Infra.ReadInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace ProdutoXyz.Infra.Repositories.ReadRepositories
{
    public class ProductReadRepository : EfCoreRepositoryBase<CrudDbContext, Product>, IProductReadRepository
    {
        public ProductReadRepository(IDbContextProvider<CrudDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public async Task<IListDto<ProductResponseDto>> GetAllProductsAsync(ProductRequestAllDto key)
            => await GetAllAsync<ProductResponseDto>(key, p => key.Description.IsNullOrEmpty() || p.Description.Contains(key.Description));

        public async Task<ProductResponseDto> GetProductAsync(DefaultRequestDto key)
        {
            var entity = await GetAsync(key);

            return entity.MapTo<ProductResponseDto>();
        }
    }
}
