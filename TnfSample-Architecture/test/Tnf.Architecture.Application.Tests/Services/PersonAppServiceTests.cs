using Shouldly;
using System.Linq;
using Tnf.App.Application.Enums;
using Tnf.App.Domain.Enums;
using Tnf.App.Dto.Request;
using Tnf.App.EntityFrameworkCore.TestBase;
using Tnf.Architecture.Application.Interfaces;
using Tnf.Architecture.Common.Enumerables;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.Dto.Registration;
using Tnf.Architecture.EntityFrameworkCore.Contexts;
using Xunit;

namespace Tnf.Architecture.Application.Tests.Services
{
    public sealed class PersonAppServiceTests : TnfEfCoreIntegratedTestBase<EfCoreAppTestModule>
    {
        private readonly IPersonAppService _personAppService;
        private readonly Person _person;

        public PersonAppServiceTests()
        {
            _personAppService = Resolve<IPersonAppService>();

            _person = new Person
            {
                Id = 1,
                Name = "John Doe"
            };

            // Setup
            UsingDbContext<ArchitectureDbContext>(context => context.People.Add(_person));
        }

        [Fact]
        public void Should_Get_All_People_With_Success()
        {
            //Act
            var response = _personAppService.GetAll(new GetAllPeopleDto() { PageSize = 10 });

            //Assert
            Assert.False(LocalNotification.HasNotification());
            response.Items.Count.ShouldBe(1);
        }

        [Fact]
        public void Should_Insert_Person_With_Success()
        {
            //Arrange
            var personDto = new PersonDto
            {
                Id = 2,
                Name = "John Doe"
            };

            //Act
            var result = _personAppService.Create(personDto);

            //Assert
            Assert.False(LocalNotification.HasNotification());
            result.Id.ShouldBe(2);
        }

        [Fact]
        public void Should_Insert_Person_With_Error()
        {
            // Act
            _personAppService.Create(new PersonDto());

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Person.Error.PersonNameMustHaveValue.ToString()));
        }

        [Fact]
        public void Should_Insert_Null_Person_With_Error()
        {
            // Act
            _personAppService.Create(null);

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(n => n.Message == AppApplicationErrors.AppApplicationErrorsInvalidDto.ToString()));
        }

        [Fact]
        public void Should_Update_Person_With_Success()
        {
            //Arrange
            var personDto = new PersonDto()
            {
                Id = 2,
                Name = "John Doe"
            };

            //Act
            var result = _personAppService.Create(personDto);

            //Assert
            Assert.False(LocalNotification.HasNotification());

            result.Id.ShouldBe(2);

            result.Name = "Mary Doe";

            result = _personAppService.Update(result.Id, result);

            //Assert
            Assert.False(LocalNotification.HasNotification());
            result.Name.ShouldBe("Mary Doe");
        }

        [Fact]
        public void Should_Update_Person_With_Error()
        {
            //Act
            _personAppService.Update(1, new PersonDto());

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == Person.Error.PersonNameMustHaveValue.ToString()));
        }

        [Fact]
        public void Should_Update_Person_Not_Found()
        {
            //Act
            _personAppService.Update(99, new PersonDto() { Name = "John Doe" });

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == AppDomainErrors.TnfAppDomainErrorOnUpdateCouldNotFind.ToString()));
        }

        [Fact]
        public void Should_Update_Invalid_Id_With_Error()
        {
            // Act
            _personAppService.Update(0, new PersonDto());

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(n => n.Message == AppApplicationErrors.AppApplicationErrorsInvalidId.ToString()));
        }

        [Fact]
        public void Should_Update_Null_Person_With_Error()
        {
            // Act
            _personAppService.Update(1, null);

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(n => n.Message == AppApplicationErrors.AppApplicationErrorsInvalidDto.ToString()));
        }

        [Fact]
        public void Should_Get_Person_With_Success()
        {
            //Act
            var response = _personAppService.Get(new RequestDto(1));

            //Assert
            Assert.False(LocalNotification.HasNotification());
            response.Id.ShouldBe(1);
            response.Name.ShouldBe(_person.Name);
        }

        [Fact]
        public void Should_Get_Person_With_Error()
        {
            // Act
            _personAppService.Get(new RequestDto(99));

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == AppDomainErrors.TnfAppDomainErrorOnGetCouldNotFind.ToString()));
        }

        [Fact]
        public void Should_Delete_Person_With_Success()
        {
            //Arrange
            var personDto = new PersonDto
            {
                Id = 2,
                Name = "John Doe"
            };

            //Act
            var result = _personAppService.Create(personDto);

            //Assert
            Assert.False(LocalNotification.HasNotification());
            result.Id.ShouldBe(2);

            //Act
            _personAppService.Delete(2);

            //Assert
            Assert.False(LocalNotification.HasNotification());
        }

        [Fact]
        public void Should_Delete_Person_With_Error()
        {
            // Act
            _personAppService.Delete(99);

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var notifications = LocalNotification.GetAll();
            Assert.True(notifications.Any(a => a.Message == AppDomainErrors.TnfAppDomainErrorOnDeleteCouldNotFind.ToString()));
        }
    }
}
