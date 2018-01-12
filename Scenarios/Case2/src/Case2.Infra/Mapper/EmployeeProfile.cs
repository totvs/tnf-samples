using AutoMapper;
using Case2.Infra.Dtos;
using Case2.Infra.Entities;

namespace Case2.Infra.Mapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>();
        }
    }
}
