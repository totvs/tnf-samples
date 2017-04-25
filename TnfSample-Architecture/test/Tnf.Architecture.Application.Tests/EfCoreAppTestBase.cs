using Tnf.App.EntityFrameworkCore.TestBase;
using Tnf.Architecture.EntityFrameworkCore;
using Tnf.Architecture.EntityFrameworkCore.Entities;

namespace Tnf.Architecture.Application.Tests
{
    public class EfCoreAppTestBase : TnfEfCoreIntegratedTestBase<EfCoreAppTestModule>
    {
        protected override void InitializeIntegratedTest()
        {
            UsingDbContext<ArchitectureDbContext>(
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