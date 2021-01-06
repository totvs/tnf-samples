using AutoMapper;
using SGDP.Domain.Entities;
using SGDP.Dto.Company;
using SGDP.Dto.Customer;

namespace SGDP.Infra.MapperProfiles
{
    public class SgdpProfile : Profile
    {
        public SgdpProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<Company, CompanyDto>();
        }
    }
}
