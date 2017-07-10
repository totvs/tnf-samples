using AutoMapper;
using System.Linq;
using Tnf.Architecture.Carol.Entities;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Architecture.Dto.WhiteHouse;
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
                .ForMember(d => d.Id, s => s.Ignore())
                .ForMember(d => d.Address, s => s.Ignore())
                .ForMember(d => d.Specialties, s => s.Ignore())
                .AfterMap((s, d) =>
                {
                    d.Address = new Address(s.Address, s.AddressNumber, s.AddressComplement, new ZipCode(s.ZipCode));

                    if (s.ProfessionalSpecialties != null)
                    {
                        d.Specialties = s.ProfessionalSpecialties.Select(w => new SpecialtyDto()
                        {
                            Id = w.SpecialtyId,
                            Description = w.Specialty.Description
                        }).ToList();
                    }
                });

            CreateMap<SpecialtyPoco, SpecialtyDto>();
            CreateMap<PresidentPoco, PresidentDto>();
        }
    }
}
