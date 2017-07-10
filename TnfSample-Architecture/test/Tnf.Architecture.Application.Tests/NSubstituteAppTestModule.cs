using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.App.Dto.Request;
using Tnf.App.Dto.Response;
using Tnf.App.TestBase;
using Tnf.Architecture.Domain.Interfaces.Repositories;
using Tnf.Architecture.Domain.WhiteHouse;
using Tnf.Architecture.Dto.ValueObjects;
using Tnf.Architecture.Dto.WhiteHouse;
using Tnf.Configuration.Startup;
using Tnf.Modules;
using Tnf.Reflection.Extensions;

namespace Tnf.Architecture.Application.Tests
{
    [DependsOn(
        typeof(AppModule),
        typeof(TnfAppTestBaseModule))]
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

                var presidentDto = new PresidentDto("1", "New President", new Address("Rua de teste", "123", "APT 12", new ZipCode("55833479")));

                var president = new President()
                {
                    Id = presidentDto.Id,
                    Name = "Alter President",
                    Address = presidentDto.Address
                };

                var ids = new List<string>() { "1" };

                var presidentsToGetAll = new ListDto<PresidentDto, string>();
                presidentsToGetAll.Items.Add(presidentDto);
                presidentsToGetAll.Items.Add(presidentDto);

                instance.GetPresidentById(Arg.Is<RequestDto<string>>(p => p.GetId() == "1"))
                    .Returns(Task.FromResult(presidentDto));

                instance.GetAllPresidents(Arg.Any<GetAllPresidentsDto>())
                    .Returns(Task.FromResult(presidentsToGetAll));

                instance.InsertPresidentsAsync(Arg.Any<List<President>>(), true)
                    .Returns(Task.FromResult(ids));

                instance.UpdatePresidentsAsync(Arg.Is<President>(p => p.Id == "1"))
                    .Returns(Task.FromResult(president));

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
