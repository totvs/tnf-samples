using Tnf.AspNetCore.TestBase;
using Tnf.Architecture.Web.Tests.App;
using Tnf.App.EntityFrameworkCore.TestBase;
using Tnf.Architecture.EntityFrameworkCore;
using Tnf.Architecture.EntityFrameworkCore.Entities;

namespace Tnf.Architecture.Web.Tests
{
    public abstract class AppTestBase : TnfAspNetCoreIntegratedTestBase<StartupTest>
    {
        protected override void InitializeIntegratedTest()
        {
            IocManager.UsingDbContext<ArchitectureDbContext>(
                context =>
                {
                    context.Countries.Add(new Country(1, "Brasil"));
                    context.Countries.Add(new Country(2, "EUA"));
                    context.Countries.Add(new Country(3, "Uruguai"));
                    context.Countries.Add(new Country(4, "Paraguai"));
                    context.Countries.Add(new Country(5, "Venezuela"));
                });
        }
    }
}