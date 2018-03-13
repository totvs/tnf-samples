using AutoMapper;
using BasicCrud.Domain.Entities;
using BasicCrud.Dto.Customer;

namespace BasicCrud.Infra.MapperProfiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>();
        }
    }
}
