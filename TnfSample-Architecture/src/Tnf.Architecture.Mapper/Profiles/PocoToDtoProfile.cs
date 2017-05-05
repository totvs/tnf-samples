using AutoMapper;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Architecture.EntityFrameworkCore.Entities;

namespace Tnf.Architecture.Mapper.Profiles
{
    public class PocoToDtoProfile : Profile
    {
        public PocoToDtoProfile()
        {
            CreateMap<CountryPoco, CountryDto>()
                .ForMember(d => d.Id, s => s.MapFrom(p => p.Id));

            CreateMap<ProfessionalPoco, ProfessionalDto>()
                .ForMember(d => d.ZipCode, s => s.MapFrom(d => new ZipCode(d.ZipCode)));

            CreateMap<ProfessionalPoco, ProfessionalCreateDto>()
                .ForMember(d => d.ZipCode, s => s.MapFrom(d => new ZipCode(d.ZipCode)));
        }
    }
}
