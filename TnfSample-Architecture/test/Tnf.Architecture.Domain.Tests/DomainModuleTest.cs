using Castle.MicroKernel.Registration;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Tnf.App.Dto.Request;
using Tnf.App.TestBase;
using Tnf.Architecture.Common.ValueObjects;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.Configuration.Startup;
using Tnf.Modules;

namespace Tnf.Architecture.Domain.Tests
{
    [DependsOn(
        typeof(DomainModule),
        typeof(TnfAppTestBaseModule))]
    public class DomainModuleTest : TnfModule
    {
        public override void PreInitialize()
        {
            Configuration.ReplaceService<IProfessionalRepository>(ReplaceProfessionalRepository);

            Configuration.ReplaceService<ISpecialtyRepository>(ReplaceSpecialtyRepository);

            Configuration.ReplaceService<IWhiteHouseRepository>(ReplaceWhiteHouseRepository);
        }

        private void ReplaceWhiteHouseRepository()
        {
            var whiteHouseRepository = Substitute.For<IWhiteHouseRepository>();
            
            var president = new President
            {
                Id = "1",
                Name = "George Washington",
                Address = new Address("Rua de teste", "123", "APT 12", new ZipCode("12345678"))
            };

            whiteHouseRepository.GetPresidentById(Arg.Is<RequestDto<string>>(p => p.GetId() == "1"))
                .Returns(Task.FromResult(president));

            whiteHouseRepository.InsertPresidentsAsync(Arg.Any<President>())
                .Returns(Task.FromResult(president.Id));

            whiteHouseRepository.UpdatePresidentsAsync(Arg.Is<President>(p => p.Id == "1"))
                .Returns(Task.FromResult(president));

            whiteHouseRepository.DeletePresidentsAsync(Arg.Is("1"))
                .Returns(Task.FromResult(true));

            IocManager.IocContainer.Register(
                Component.For<IWhiteHouseRepository>()
                    .Instance(whiteHouseRepository)
                    .LifestyleTransient()
            );
        }

        private void ReplaceSpecialtyRepository()
        {
            var specialtyRepository = Substitute.For<ISpecialtyRepository>();
            
            var specialty = new Specialty
            {
                Id = 1,
                Description = "Cirurgia Vascular"
            };

            specialtyRepository.GetSpecialty(Arg.Any<RequestDto>())
                .Returns(specialty);

            specialtyRepository.CreateSpecialty(Arg.Any<Specialty>())
                .Returns(1);

            specialtyRepository.UpdateSpecialty(Arg.Any<Specialty>());

            specialtyRepository.DeleteSpecialty(Arg.Any<int>());

            specialtyRepository.ExistsSpecialty(Arg.Is(1))
                .Returns(true);

            IocManager.IocContainer.Register(
                Component.For<ISpecialtyRepository>()
                    .Instance(specialtyRepository)
                    .LifestyleTransient()
            );
        }

        private void ReplaceProfessionalRepository()
        {
            var profissionalRepository = Substitute.For<IProfessionalRepository>();
            
            var professional = new Professional
            {
                ProfessionalId = 1,
                Name = "João da Silva",
                Phone = "99997654",
                Email = "email@email.com",
                Code = Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637"),
                Address = new Address("Rua de teste", "321", "APT 32", new ZipCode("87654321"))
            };
            
            profissionalRepository.GetProfessional(Arg.Is<RequestDto<ComposeKey<Guid, decimal>>>(p => p.GetId().SecundaryKey == 1 && p.GetId().PrimaryKey == Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637")))
                .Returns(professional);

            profissionalRepository.CreateProfessional(Arg.Any<Professional>())
                .Returns(new ComposeKey<Guid, decimal>(Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637"), 1));

            profissionalRepository.UpdateProfessional(Arg.Any<Professional>());

            profissionalRepository.DeleteProfessional(Arg.Is<ComposeKey<Guid, decimal>>(p => p.SecundaryKey == 1 && p.PrimaryKey == Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637")))
                .Returns(true);

            profissionalRepository.ExistsProfessional(Arg.Is<ComposeKey<Guid, decimal>>(p => p.SecundaryKey == 1 && p.PrimaryKey == Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637")))
                .Returns(true);

            IocManager.IocContainer.Register(
                Component.For<IProfessionalRepository>()
                    .Instance(profissionalRepository)
                    .LifestyleTransient()
            );
        }

        public override void Initialize()
        {
            base.Initialize();

            IocManager.RegisterAssemblyByConvention(typeof(DomainModuleTest).Assembly);
        }
    }
}
