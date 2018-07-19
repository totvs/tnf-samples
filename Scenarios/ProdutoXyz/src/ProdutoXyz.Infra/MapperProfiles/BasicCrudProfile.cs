using AutoMapper;
using ProdutoXyz.Domain.Entities;
using ProdutoXyz.Dto.Product;

namespace ProdutoXyz.Infra.MapperProfiles
{
    public class BasicCrudProfile : Profile
    {
        public BasicCrudProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Product, ProductResponseDto>();
        }
    }
}
