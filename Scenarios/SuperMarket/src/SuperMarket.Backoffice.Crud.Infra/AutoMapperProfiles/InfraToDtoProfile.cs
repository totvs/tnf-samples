using AutoMapper;
using SuperMarket.Backoffice.Crud.Domain.Entities;
using SuperMarket.Backoffice.Crud.Infra.Dtos;

namespace SuperMarket.Backoffice.Crud.Infra.AutoMapperProfiles
{
    public class InfraToDtoProfile : Profile
    {
        public InfraToDtoProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Customer, CustomerDto>();
        }
    }
}
