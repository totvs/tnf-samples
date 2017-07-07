using NSubstitute;
using System;
using System.Collections.Generic;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.App.TestBase;
using System.Linq;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Modules;
using Tnf.Reflection.Extensions;
using Tnf.Configuration.Startup;
using Castle.MicroKernel.Registration;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Architecture.Domain.WhiteHouse;
using System.Threading.Tasks;
using Castle.Core.Logging;

namespace Tnf.Architecture.Domain.Tests
{
    [DependsOn(
        typeof(DomainModule),
        typeof(TnfAppTestBaseModule))]
    public class DomainModuleTest : TnfModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<ILogger, NullLogger>();

            Configuration.ReplaceService<IProfessionalRepository>(() =>
            {
                ReplaceProfessionalRepository();
            });

            Configuration.ReplaceService<ISpecialtyRepository>(() =>
            {
                ReplaceSpecialtyRepository();
            });

            Configuration.ReplaceService<IWhiteHouseRepository>(() =>
            {
                ReplaceWhiteHouseRepository();
            });
        }

        private void ReplaceWhiteHouseRepository()
        {
            var _whiteHouseRepository = Substitute.For<IWhiteHouseRepository>();

            var presidentDto = new PresidentDto("1", "George Washington", new Address("Rua de teste", "123", "APT 12", new ZipCode("12345678")));

            var presidentList = new List<PresidentDto>()
            {
                presidentDto,
                new PresidentDto("2", "Bill Clinton", new Address("Rua de teste", "321", "APT 32", new ZipCode("87654321")))
            };

            var presidentPaging = new ListDto<PresidentDto, string>();
            presidentPaging.Items = presidentList;

            var president = new President()
            {
                Id = presidentDto.Id,
                Name = presidentDto.Name,
                Address = presidentDto.Address
            };

            _whiteHouseRepository.GetAllPresidents(Arg.Any<GetAllPresidentsDto>())
                .Returns(Task.FromResult(presidentPaging));

            _whiteHouseRepository.GetPresidentById(Arg.Is<RequestDto<string>>(p => p.GetId() == "1"))
                .Returns(Task.FromResult(presidentDto));

            _whiteHouseRepository.InsertPresidentsAsync(Arg.Any<List<President>>())
                .Returns(Task.FromResult(presidentList.Select(p => p.Id).ToList()));

            _whiteHouseRepository.UpdatePresidentsAsync(Arg.Is<President>(p => p.Id == "1"))
                .Returns(president);

            _whiteHouseRepository.DeletePresidentsAsync(Arg.Is("1"))
                .Returns(Task.FromResult(true));

            IocManager.IocContainer.Register(
                Component.For<IWhiteHouseRepository>()
                    .Instance(_whiteHouseRepository)
                    .LifestyleTransient()
            );
        }

        private void ReplaceSpecialtyRepository()
        {
            var _specialtyRepository = Substitute.For<ISpecialtyRepository>();

            var specialtyDto = new SpecialtyDto()
            {
                Id = 1,
                Description = "Cirurgia Vascular"
            };

            var specialtyList = new List<SpecialtyDto>() { specialtyDto };

            var specialtyPaging = new ListDto<SpecialtyDto>();
            specialtyPaging.Items = specialtyList;

            var specialty = new Specialty()
            {
                Id = specialtyDto.Id,
                Description = specialtyDto.Description
            };

            _specialtyRepository.GetAllSpecialties(Arg.Any<GetAllSpecialtiesDto>())
                .Returns(specialtyPaging);

            _specialtyRepository.GetSpecialty(Arg.Any<RequestDto<int>>())
                .Returns(specialtyDto);

            _specialtyRepository.CreateSpecialty(Arg.Any<Specialty>())
                .Returns(1);

            _specialtyRepository.UpdateSpecialty(Arg.Any<Specialty>())
                .Returns(specialty);

            _specialtyRepository.DeleteSpecialty(Arg.Any<int>());

            _specialtyRepository.ExistsSpecialty(Arg.Is(1))
                .Returns(true);

            IocManager.IocContainer.Register(
                Component.For<ISpecialtyRepository>()
                    .Instance(_specialtyRepository)
                    .LifestyleTransient()
            );
        }

        private void ReplaceProfessionalRepository()
        {
            var _profissionalRepository = Substitute.For<IProfessionalRepository>();

            var professionalDto = new ProfessionalDto()
            {
                ProfessionalId = 1,
                Name = "João da Silva",
                Phone = "99997654",
                Email = "email@email.com",
                Code = Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637"),
                Address = new Address("Rua de teste", "321", "APT 32", new ZipCode("87654321"))
            };

            var professionalList = new List<ProfessionalDto>() { professionalDto };

            var professionalPaging = new ListDto<ProfessionalDto, ProfessionalKeysDto>();
            professionalPaging.Items = professionalList;

            var professional = new Professional()
            {
                ProfessionalId = professionalDto.ProfessionalId,
                Code = professionalDto.Code,
                Name = professionalDto.Name,
                Address = professionalDto.Address,
                Phone = professionalDto.Phone,
                Email = professionalDto.Email
            };

            _profissionalRepository.GetAllProfessionals(Arg.Any<GetAllProfessionalsDto>())
                .Returns(professionalPaging);

            _profissionalRepository.GetProfessional(Arg.Is<RequestDto<ProfessionalKeysDto>>(p => p.GetId().ProfessionalId == 1 && p.GetId().Code == Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637")))
                .Returns(professionalDto);

            _profissionalRepository.CreateProfessional(Arg.Any<Professional>())
                .Returns(new ProfessionalKeysDto(1, Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637")));

            _profissionalRepository.UpdateProfessional(Arg.Any<Professional>())
                .Returns(professional);

            _profissionalRepository.DeleteProfessional(Arg.Is<ProfessionalKeysDto>(p => p.ProfessionalId == 1 && p.Code == Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637")))
                .Returns(true);

            _profissionalRepository.ExistsProfessional(Arg.Is<ProfessionalKeysDto>(p => p.ProfessionalId == 1 && p.Code == Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637")))
                .Returns(true);

            IocManager.IocContainer.Register(
                Component.For<IProfessionalRepository>()
                    .Instance(_profissionalRepository)
                    .LifestyleTransient()
            );
        }

        public override void Initialize()
        {
            base.Initialize();

            IocManager.RegisterAssemblyByConvention(typeof(DomainModuleTest).GetAssembly());
        }
    }
}
