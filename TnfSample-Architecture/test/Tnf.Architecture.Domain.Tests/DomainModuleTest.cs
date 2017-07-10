using Castle.MicroKernel.Registration;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.App.TestBase;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Configuration.Startup;
using Tnf.Modules;
using Tnf.Reflection.Extensions;

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

            var presidentDto = new PresidentDto("1", "George Washington", new Address("Rua de teste", "123", "APT 12", new ZipCode("12345678")));

            var presidentList = new List<PresidentDto>()
            {
                presidentDto,
                new PresidentDto("2", "Bill Clinton", new Address("Rua de teste", "321", "APT 32", new ZipCode("87654321")))
            };

            var presidentPaging = new ListDto<PresidentDto, string> { Items = presidentList };

            var president = new President()
            {
                Id = presidentDto.Id,
                Name = presidentDto.Name,
                Address = presidentDto.Address
            };

            whiteHouseRepository.GetAllPresidents(Arg.Any<GetAllPresidentsDto>())
                .Returns(Task.FromResult(presidentPaging));

            whiteHouseRepository.GetPresidentById(Arg.Is<RequestDto<string>>(p => p.GetId() == "1"))
                .Returns(Task.FromResult(presidentDto));

            whiteHouseRepository.InsertPresidentsAsync(Arg.Any<List<President>>())
                .Returns(Task.FromResult(presidentList.Select(p => p.Id).ToList()));

            whiteHouseRepository.UpdatePresidentsAsync(Arg.Is<President>(p => p.Id == "1"))
                .Returns(president);

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

            var specialtyDto = new SpecialtyDto()
            {
                Id = 1,
                Description = "Cirurgia Vascular"
            };

            var specialtyList = new List<SpecialtyDto>() { specialtyDto };

            var specialtyPaging = new ListDto<SpecialtyDto> { Items = specialtyList };

            var specialty = new Specialty()
            {
                Id = specialtyDto.Id,
                Description = specialtyDto.Description
            };

            specialtyRepository.GetAllSpecialties(Arg.Any<GetAllSpecialtiesDto>())
                .Returns(specialtyPaging);

            specialtyRepository.GetSpecialty(Arg.Any<RequestDto<int>>())
                .Returns(specialtyDto);

            specialtyRepository.CreateSpecialty(Arg.Any<Specialty>())
                .Returns(1);

            specialtyRepository.UpdateSpecialty(Arg.Any<Specialty>())
                .Returns(specialty);

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

            var professionalPaging = new ListDto<ProfessionalDto, ProfessionalKeysDto> { Items = professionalList };

            var professional = new Professional()
            {
                ProfessionalId = professionalDto.ProfessionalId,
                Code = professionalDto.Code,
                Name = professionalDto.Name,
                Address = professionalDto.Address,
                Phone = professionalDto.Phone,
                Email = professionalDto.Email
            };

            profissionalRepository.GetAllProfessionals(Arg.Any<GetAllProfessionalsDto>())
                .Returns(professionalPaging);

            profissionalRepository.GetProfessional(Arg.Is<RequestDto<ProfessionalKeysDto>>(p => p.GetId().ProfessionalId == 1 && p.GetId().Code == Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637")))
                .Returns(professionalDto);

            profissionalRepository.CreateProfessional(Arg.Any<Professional>())
                .Returns(new ProfessionalKeysDto(1, Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637")));

            profissionalRepository.UpdateProfessional(Arg.Any<Professional>())
                .Returns(professional);

            profissionalRepository.DeleteProfessional(Arg.Is<ProfessionalKeysDto>(p => p.ProfessionalId == 1 && p.Code == Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637")))
                .Returns(true);

            profissionalRepository.ExistsProfessional(Arg.Is<ProfessionalKeysDto>(p => p.ProfessionalId == 1 && p.Code == Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637")))
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

            IocManager.RegisterAssemblyByConvention(typeof(DomainModuleTest).GetAssembly());
        }
    }
}
