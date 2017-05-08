using AutoMapper;
using Tnf.Architecture.Dto.Registration;

namespace Tnf.Architecture.Mapper.Profiles
{
    public class DtoToDtoProfile : Profile
    {
        public DtoToDtoProfile()
        {
            CreateMap<ProfessionalUpdateDto, ProfessionalDto>();
        }
    }
}
