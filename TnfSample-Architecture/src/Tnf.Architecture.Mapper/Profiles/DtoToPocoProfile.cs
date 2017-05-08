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

            CreateMap<ProfessionalCreateDto, ProfessionalPoco>()
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

            CreateMap<SpecialtyDto, SpecialtyPoco>();
        }
    }
}
