using AutoMapper;
using System.Data.Common;

namespace Case6.Infra.Mappers
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<DbDataReader, Customer>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s["Id"]))
                .ForMember(d => d.Name, o => o.MapFrom(s => s["Name"]));
        }
    }
}
