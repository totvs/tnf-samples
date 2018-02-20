using AutoMapper;
using Case5.Infra.Dtos;
using Case5.Infra.Entities;

namespace Case5.Infra.Mapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>();
        }
    }
}
