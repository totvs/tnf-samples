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
            Configuration.ReplaceService<IWhiteHouseRepository>(() =>
            {
                var instance = Substitute.For<IWhiteHouseRepository>();
                instance.DeletePresidentsAsync("6").Returns(Task.FromResult<object>(null));

                var presidentsToInsert = new List<PresidentDto>()
                {
                    new PresidentDto("7", "New President", "55833479")
                };

                instance.InsertPresidentsAsync(presidentsToInsert, true)
                    .Returns(Task.FromResult(presidentsToInsert));

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
