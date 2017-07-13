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
                    context.Countries.Add(new CountryPoco(1, "Brasil"));
                    context.Countries.Add(new CountryPoco(2, "EUA"));
                    context.Countries.Add(new CountryPoco(3, "Uruguai"));
                    context.Countries.Add(new CountryPoco(4, "Paraguai"));
                    context.Countries.Add(new CountryPoco(5, "Venezuela"));
                });
        }
    }
}