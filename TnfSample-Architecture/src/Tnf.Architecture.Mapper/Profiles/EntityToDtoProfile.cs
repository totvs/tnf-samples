using AutoMapper;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.Dto.WhiteHouse;

namespace Tnf.Architecture.Mapper.Profiles
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<Professional, ProfessionalDto>();
            CreateMap<Specialty, SpecialtyDto>();
            CreateMap<President, PresidentDto>();
            
            CreateMap<Person, PersonDto>()
                .ForMember(d => d.Id, s => s.MapFrom(p => p.Id));
        }
    }
}
