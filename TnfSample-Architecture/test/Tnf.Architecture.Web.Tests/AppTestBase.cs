using System;
using Tnf.App.EntityFrameworkCore;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.EntityFrameworkCore.Contexts;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.Architecture.Web.Tests.App;
using Tnf.AspNetCore.TestBase;

namespace Tnf.Architecture.Web.Tests
{
    public abstract class AppTestBase : TnfAspNetCoreIntegratedTestBase<StartupTest>
    {
        protected override void InitializeIntegratedTest()
        {
            IocManager.UsingDbContext<ArchitectureDbContext>(
                context =>
                {
                    context.Countries.Add(new CountryPoco(1, "Brasil"));
                    context.Countries.Add(new CountryPoco(2, "EUA"));
                    context.Countries.Add(new CountryPoco(3, "Uruguai"));
                    context.Countries.Add(new CountryPoco(4, "Paraguai"));
                    context.Countries.Add(new CountryPoco(5, "Venezuela"));

                    context.People.Add(new Person(1, "John Doe"));
                    context.People.Add(new Person(2, "Mary Doe"));
                    context.People.Add(new Person(3, "James Gunn"));
                    context.People.Add(new Person(4, "Abraham"));
                    context.People.Add(new Person(5, "Chloe"));
                });

            IocManager.UsingDbContext<LegacyDbContext>(
                context =>
                {
                    context.Professionals.Add(new ProfessionalPoco
                    {
                        ProfessionalId = 1,
                        Code = Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637"),
                        Address = "Rua do comercio",
                        AddressNumber = "123",
                        AddressComplement = "APT 123",
                        Email = "email@email.com",
                        Name = "João da Silva",
                        Phone = "55998765432",
                        ZipCode = "99888777"
                    });

                    context.Professionals.Add(new ProfessionalPoco
                    {
                        ProfessionalId = 2,
                        Code = Guid.Parse("f31a6ed8-9d0e-4a02-8b66-0c067981d72e"),
                        Address = "Rua do comercio 2",
                        AddressNumber = "1233",
                        AddressComplement = "APT 1234",
                        Email = "email2@email2.com",
                        Name = "José da Silva",
                        Phone = "15398264438",
                        ZipCode = "22888888"
                    });

                    context.Specialties.Add(new SpecialtyPoco()
                    {
                        Id = 1,
                        Description = "Cirurgia Vascular"
                    });

                    context.Specialties.Add(new SpecialtyPoco()
                    {
                        Id = 2,
                        Description = "Cirurgia Geral"
                    });

                    context.ProfessionalSpecialties.Add(new ProfessionalSpecialtiesPoco()
                    {
                        Id = 1,
                        Code = Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637"),
                        ProfessionalId = 1,
                        SpecialtyId = 1
                    });

                    context.ProfessionalSpecialties.Add(new ProfessionalSpecialtiesPoco()
                    {
                        Id = 2,
                        Code = Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637"),
                        ProfessionalId = 1,
                        SpecialtyId = 2
                    });
                });
        }
    }
}