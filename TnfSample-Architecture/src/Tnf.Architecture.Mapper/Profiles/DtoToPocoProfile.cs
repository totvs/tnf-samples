using AutoMapper;
using Tnf.Architecture.Dto;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.EntityFrameworkCore.Entities;

namespace Tnf.Architecture.Mapper.Profiles
{
    public class DtoToPocoProfile : Profile
    {
        public DtoToPocoProfile()
        {
            CreateMap<CountryDto, CountryPoco>()
                .ForMember(d => d.Id, s => s.MapFrom(p => p.Id));

            CreateMap<ProfessionalDto, ProfessionalPoco>()
                .ForMember(d => d.ZipCode, s => s.MapFrom(p => p.ZipCode));

            CreateMap<ProfessionalCreateDto, ProfessionalPoco>()
                .ForMember(d => d.ZipCode, s => s.MapFrom(p => p.ZipCode));
        }
    }
}
