using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Tnf.App.Bus.Notifications;
using Tnf.Architecture.Carol.Entities;
using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.AutoMapper;

namespace Tnf.Architecture.Mapper.Profiles
{
    public class PocoToEntityProfile : Profile
    {
        public PocoToEntityProfile()
        {
            CreateMap<ProfessionalPoco, Professional>()
                .ForMember(d => d.Address, s => s.Ignore())
                .ForMember(d => d.Specialties, s => s.Ignore())
                .AfterMap((s, d) =>
                {
                    IList<Specialty> specialties = null;
                    if (s.ProfessionalSpecialties != null)
                    {
                        specialties = s.ProfessionalSpecialties.Select(w => w.Specialty.MapTo<Specialty>()).ToList();
                    }

                    new ProfessionalBuilder(d, new NullNotificationHandler())
                        .WithAddress(new Address(s.Address, s.AddressNumber, s.AddressComplement,
                            new ZipCode(s.ZipCode)))
                        .WithSpecialties(specialties)
                        .Build();
                });

            CreateMap<SpecialtyPoco, Specialty>();

            CreateMap<PresidentPoco, President>();
        }
    }
}
