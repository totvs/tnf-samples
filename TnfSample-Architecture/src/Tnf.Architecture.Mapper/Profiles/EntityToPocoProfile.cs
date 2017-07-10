using AutoMapper;
using Tnf.Architecture.Carol.Entities;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.Architecture.EntityFrameworkCore.Entities;

namespace Tnf.Architecture.Mapper.Profiles
{
    public class EntityToPocoProfile : Profile
    {
        public EntityToPocoProfile()
        {
            CreateMap<Professional, ProfessionalPoco>()
                .ForMember(d => d.Address, s => s.Ignore())
                .ForMember(d => d.AddressComplement, s => s.Ignore())
                .ForMember(d => d.AddressNumber, s => s.Ignore())
                .ForMember(d => d.ZipCode, s => s.Ignore())
                .ForMember(d => d.ProfessionalSpecialties, s => s.Ignore())
                .AfterMap((s, d) =>
                {
                    d.Address = s.Address.Street;
                    d.AddressComplement = s.Address.Complement;
                    d.AddressNumber = s.Address.Number;
                    d.ZipCode = s.Address.ZipCode.Number;
                });

            CreateMap<Specialty, SpecialtyPoco>();

            CreateMap<President, PresidentPoco>();
        }
    }
}
