using AutoMapper;
using BasicCrud.Domain.Entities;
using BasicCrud.Dto.Customer;
using BasicCrud.Dto.Product;

namespace BasicCrud.Infra.MapperProfiles
{
    public class BasicCrudProfile : Profile
    {
        public BasicCrudProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<Product, ProductDto>();
        }
    }
}
