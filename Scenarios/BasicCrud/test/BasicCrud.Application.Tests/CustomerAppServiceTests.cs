using BasicCrud.Application.Services;
using BasicCrud.Application.Services.Interfaces;
using BasicCrud.Application.Tests.Mocks;
using BasicCrud.Domain;
using BasicCrud.Domain.Entities;
using BasicCrud.Dto.Customer;
using BasicCrud.Infra;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Domain.Services;
using Tnf.Dto;
using Tnf.Localization;
using Tnf.TestBase;
using Xunit;

namespace BasicCrud.Application.Tests
{
    public class CustomerAppServiceTests : TnfIntegratedTestBase
    {
        private ILocalizationSource _localizationSource;
        private readonly ICustomerAppService _appService;
        private readonly CultureInfo _culture;

        public CustomerAppServiceTests()
        {
            _appService = Resolve<ICustomerAppService>();

            _culture = CultureInfo.GetCultureInfo("pt-BR");
        }

        protected override void PreInitialize(IServiceCollection services)
        {
            base.PreInitialize(services);

            // Configura o mesmo Mapper para ser testado
            services.AddMapperDependency();

            // Registro dos serviços de Mock
            services.AddTransient<IDomainService<Customer, Guid>, CustomerDomainServiceMock>();

            // Registro dos serviços para teste
            services.AddTransient<ICustomerAppService, CustomerAppService>();
        }

        protected override void PostInitialize(IServiceProvider provider)
        {
            base.PostInitialize(provider);

            provider.ConfigureTnf().UseDomainLocalization();

            _localizationSource = provider.GetService<ILocalizationManager>().GetSource(Constants.LocalizationSourceName);
        }

        [Fact]
        public void Should_Resolve_All()
        {
            TnfSession.ShouldNotBeNull();
            ServiceProvider.GetService<ICustomerAppService>().ShouldNotBeNull();
            ServiceProvider.GetService<IDomainService<Customer, Guid>>().ShouldNotBeNull();
        }


        [Fact]
        public async Task Should_GetAll()
        {
            // Act
            var response = await _appService.GetAll(new CustomerRequestAllDto());

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.False(response.HasNext);
            Assert.Equal(3, response.Items.Count);
        }


        [Fact]
        public async Task Should_Get_Customer()
        {
            // Act
            var customer = await _appService.Get(CustomerDomainServiceMock.customerGuid.ToRequestDto());

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.Equal(CustomerDomainServiceMock.customerGuid, customer.Id);
            Assert.Equal("Customer A", customer.Name);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Get_Null()
        {
            // Act
            var customer = await _appService.Get(null);

            // Assert
            Assert.NotNull(customer);
            Assert.True(LocalNotification.HasNotification());
            var message = string.Format(LocalLocalizationSource.GetString(ApplicationService.Error.ApplicationServiceOnInvalidIdError, _culture), "request");
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);

            // Act
            customer = await _appService.Get(new RequestDto<Guid>());

            // Assert
            Assert.NotNull(customer);
            Assert.True(LocalNotification.HasNotification());
            message = string.Format(LocalLocalizationSource.GetString(ApplicationService.Error.ApplicationServiceOnInvalidIdError, _culture), "request");
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Get_Not_Found()
        {
            // Act
            var customer = await _appService.Get(Guid.NewGuid().ToRequestDto());

            // Assert
            Assert.Null(customer);
        }


        [Fact]
        public async Task Should_Create_Customer()
        {
            // Act
            var customer = await _appService.Create(new CustomerDto
            {
                Name = "Customer @"
            });

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.NotEqual(Guid.Empty, customer.Id);
            Assert.Equal("Customer @", customer.Name);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Create()
        {
            // Act
            var customer = await _appService.Create(null);

            // Assert
            Assert.NotNull(customer);
            Assert.True(LocalNotification.HasNotification());
            var message = string.Format(LocalLocalizationSource.GetString(ApplicationService.Error.ApplicationServiceOnInvalidDtoError, _culture), "dto");
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Create_With_Specifications()
        {
            // Act
            var customer = await _appService.Create(new CustomerDto());

            // Assert
            Assert.NotNull(customer);
            Assert.True(LocalNotification.HasNotification());
            var message = _localizationSource.GetString(Customer.Error.CustomerShouldHaveName, _culture);
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }


        [Fact]
        public async Task Should_Update_Customer()
        {
            // Act
            var customer = await _appService.Update(CustomerDomainServiceMock.customerGuid, new CustomerDto
            {
                Name = "Customer @"
            });

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.Equal(customer.Id, CustomerDomainServiceMock.customerGuid);
            Assert.Equal("Customer @", customer.Name);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Update()
        {
            // Act
            var customer = await _appService.Update(Guid.Empty, null);

            // Assert
            Assert.NotNull(customer);
            Assert.True(LocalNotification.HasNotification());
            Assert.Equal(2, LocalNotification.GetAll().Count());
            var message = string.Format(LocalLocalizationSource.GetString(ApplicationService.Error.ApplicationServiceOnInvalidDtoError, _culture), "dto");
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
            message = string.Format(LocalLocalizationSource.GetString(ApplicationService.Error.ApplicationServiceOnInvalidIdError, _culture), "id");
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Update_With_Specifications()
        {
            // Act
            var customer = await _appService.Update(CustomerDomainServiceMock.customerGuid, new CustomerDto());

            // Assert
            Assert.NotNull(customer);
            Assert.True(LocalNotification.HasNotification());
            var message = _localizationSource.GetString(Customer.Error.CustomerShouldHaveName, _culture);
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }


        [Fact]
        public async Task Should_Delete_Customer()
        {
            // Act
            await _appService.Delete(CustomerDomainServiceMock.customerGuid);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Delete()
        {
            // Act
            await _appService.Delete(Guid.Empty);

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var message = string.Format(LocalLocalizationSource.GetString(ApplicationService.Error.ApplicationServiceOnInvalidIdError, _culture), "id");
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }
    }
}
