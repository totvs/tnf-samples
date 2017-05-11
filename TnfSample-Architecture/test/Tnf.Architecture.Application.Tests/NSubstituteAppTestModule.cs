using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Modules;
using Tnf.TestBase;
using Tnf.Configuration.Startup;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Dto;
using NSubstitute;
using Castle.MicroKernel.Registration;
using Tnf.Reflection.Extensions;
using Castle.Core.Logging;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Architecture.Dto.ValueObjects;
using System.Linq;

namespace Tnf.Architecture.Application.Tests
{
    [DependsOn(
        typeof(AppModule),
        typeof(TnfTestBaseModule))]
    public class NSubstituteAppTestModule : TnfModule
    {
        public override void PreInitialize()
        {
            // Mock repositories
            Configuration.ReplaceService<ILogger>(() =>
            {
                IocManager.IocContainer.Register(
                    Component
                    .For<ILogger>()
                    .Instance(Substitute.For<ILogger>())
                    .LifestyleTransient()
                );
            });

            Configuration.ReplaceService<IWhiteHouseRepository>(() =>
            {
                var instance = Substitute.For<IWhiteHouseRepository>();

                var president = new PresidentDto("1", "New President", new Address("Rua de teste", "123", "APT 12", new ZipCode("55833479")));
                var alterPresident = new PresidentDto("1", "Alter President", new Address("Rua de teste", "123", "APT 12", new ZipCode("55833479")));

                var presidentsToInsert = new List<PresidentDto>() { president };

                var presidentsToGetAll = new PagingResponseDto<PresidentDto>();
                presidentsToGetAll.Data.Add(president);
                presidentsToGetAll.Data.Add(president);

                instance.GetPresidentById("1")
                    .Returns(Task.FromResult(president));

                instance.GetAllPresidents(Arg.Any<GetAllPresidentsDto>())
                    .Returns(Task.FromResult(presidentsToGetAll));

                instance.InsertPresidentsAsync(Arg.Any<List<PresidentDto>>(), true)
                    .Returns(Task.FromResult(presidentsToInsert.ToList()));

                instance.UpdatePresidentsAsync(Arg.Is<PresidentDto>(p => p.Id == "1"))
                    .Returns(Task.FromResult(alterPresident));

                instance.DeletePresidentsAsync("1")
                    .Returns(Task.FromResult(true));

                IocManager.IocContainer.Register(
                    Component
                    .For<IWhiteHouseRepository>()
                    .Instance(instance)
                    .LifestyleTransient()
                );
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(NSubstituteAppTestModule).GetAssembly());
        }
    }
}
