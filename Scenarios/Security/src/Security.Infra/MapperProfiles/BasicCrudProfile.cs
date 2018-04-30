using AutoMapper;
using Security.Domain.Entities;
using Security.Dto.Customer;
using Security.Dto.Product;

namespace Security.Infra.MapperProfiles
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
