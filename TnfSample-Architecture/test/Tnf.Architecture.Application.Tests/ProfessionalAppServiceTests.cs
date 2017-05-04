using Tnf.App.EntityFrameworkCore.TestBase;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.EntityFrameworkCore;
using Tnf.Domain.Repositories;
using Xunit;
using Shouldly;

namespace Tnf.Architecture.Application.Tests
{
    public class ProfessionalAppServiceTests : TnfEfCoreIntegratedTestBase<EfCoreAppTestModule>
    {
        private readonly IRepository<Professional> _professionalRepository;

        public ProfessionalAppServiceTests()
        {
            _professionalRepository = Resolve<IRepository<Professional>>();

            // Setup
            UsingDbContext<LegacyDbContext>(
               context =>
               {
                   context.Professionals.Add(new Professional()
                   {
                       Address = "Rua do comercio",
                       AddressNumber = "123",
                       AddressComplement = "APT 123",
                       Email = "email@email.com",
                       Name = "João da Silva",
                       Phone = "55998765432",
                       ZipCode = "99888777"
                   });

                   context.Professionals.Add(new Professional()
                   {
                       Address = "Rua do comercio 2",
                       AddressNumber = "1234",
                       AddressComplement = "APT 1234",
                       Email = "email2@email.com",
                       Name = "Paulo da Silva",
                       Phone = "55998765432",
                       ZipCode = "99888777"
                   });

                   context.Professionals.Add(new Professional()
                   {
                       Address = "Rua do comercio 3",
                       AddressNumber = "12345",
                       AddressComplement = "APT 12345",
                       Email = "email3@email.com",
                       Name = "Pedro da Silva",
                       Phone = "55998765432",
                       ZipCode = "99888777"
                   });
               });
        }

        [Fact]
        public void Professional_Repository_Should_Be_Greater_Than_Count()
        {
            //Act

            var count = _professionalRepository.Count();

            //Assert

            count.ShouldBeGreaterThan(0);
        }
    }
}
