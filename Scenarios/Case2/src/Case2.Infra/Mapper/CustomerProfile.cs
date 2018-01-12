using AutoMapper;
using Case2.Infra.Dtos;
using Case2.Infra.Entities;

namespace Case2.Infra.Mapper
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>();
        }
    }
}
