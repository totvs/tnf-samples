using Tnf.App.Carol;
using Tnf.Architecture.Carol.Entities;
using Tnf.Modules;
using Tnf.Provider.Misc;

namespace Tnf.Architecture.Carol
{
    [DependsOn(
        typeof(TnfCarolModule))]
    public class CarolModule : TnfModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules
                .TnfCarol()
                .Configure(config => config
                    .SetBaseAddress(CarolIntegrationConfig.BaseAddress)
                    .SetCredentials(new UsernamePasswordCredential
                    (
                        CarolIntegrationConfig.AppId,
                        CarolIntegrationConfig.TenantId,
                        CarolIntegrationConfig.Subdomain,
                        CarolIntegrationConfig.Username,
                        CarolIntegrationConfig.Password
                    ))
                    .AddMapper<PresidentPocoMapper>()
                ).Register();

            base.PreInitialize();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CarolModule).Assembly);
        }
    }
}