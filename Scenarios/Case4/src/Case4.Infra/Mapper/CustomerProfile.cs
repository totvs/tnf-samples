using AutoMapper;
using Case4.Domain;
using Case4.Infra.Dtos;

namespace Case4.Infra.Mapper
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>();
        }
    }
}
