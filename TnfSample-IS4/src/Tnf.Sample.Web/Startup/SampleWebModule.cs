using System.Reflection;
using Tnf.Sample.Configuration;
using Tnf.Sample.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Tnf.Modules;
using Tnf.AspNetCore;
using Tnf.AspNetCore.Configuration;
using Tnf.App.App.Configuration;
using Tnf.App;

namespace Tnf.Sample.Web.Startup
{
    [DependsOn(
        typeof(TnfAspNetCoreModule))]        
    public class SampleWebModule : TnfModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}