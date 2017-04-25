using Tnf.App.FluigData;
using Tnf.App.FluigData.Configuration;
using Tnf.Modules;
using Tnf.Architecture.Domain;
using Tnf.Reflection.Extensions;

namespace Tnf.Architecture.Data
{
    [DependsOn(
        typeof(DomainModule),
        typeof(TnfFluigDataModule))]
    public class FluigDataModule : TnfModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules
                .TnfFluigData()
                .Configure(new TnfFluigDataConfig()
                {
                    ApplicationId = "21af0700038211e79daf4a8136534b63",
                    TenantId = "8cd6e43115e9416eb23609486fa053e3",
                    BaseAddress = "https://totvshealth.carol.ai/api/v1",
                    Username = "josue.agnese@totvs.com.br",
                    Password = "Totvs@123"
                });

            base.PreInitialize();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FluigDataModule).GetAssembly());
        }
    }
}
