using System.Reflection;
using Tnf.App.FluigData;
using Tnf.App.FluigData.Configuration;
using Tnf.Modules;
using Tnf.Sample.Core;

namespace Tnf.Sample.Data.FluigData
{
    [DependsOn(
        typeof(SampleCoreModule),
        typeof(TnfFluigDataModule))]
    public class SampleDataModule : TnfModule
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
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
