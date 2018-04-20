using BasicCrud.Application.Services;
using BasicCrud.Application.Services.Interfaces;
using BasicCrud.Application.Tests.Mocks;
using BasicCrud.Domain;
using BasicCrud.Domain.Entities;
using BasicCrud.Dto;
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
using Tnf.TestBase;
using Xunit;

namespace BasicCrud.Application.Tests
{
    public class CustomerAppServiceTests : TnfIntegratedTestBase
    {
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
            services.AddTransient<IDomainService<Customer>, CustomerDomainServiceMock>();

            // Registro dos serviços para teste
            services.AddTransient<ICustomerAppService, CustomerAppService>();
        }

        protected override void PostInitialize(IServiceProvider provider)
        {
            base.PostInitialize(provider);

            provider.ConfigureTnf(config =>
            {
                config.UseDomainLocalization();
            });
        }

        [Fact]
        public void Should_Resolve_All()
        {
            TnfSession.ShouldNotBeNull();
            ServiceProvider.GetService<ICustomerAppService>().ShouldNotBeNull();
            ServiceProvider.GetService<IDomainService<Customer>>().ShouldNotBeNull();
        }


        [Fact]
        public async Task Should_GetAll()
        {
            // Act
            var response = await _appService.GetAllAsync(new CustomerRequestAllDto());

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.False(response.HasNext);
            Assert.Equal(3, response.Items.Count);
        }


        [Fact]
        public async Task Should_Get_Customer()
        {
            // Act
            var customer = await _appService.GetAsync(new DefaultRequestDto(CustomerDomainServiceMock.customerGuid));

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.Equal(CustomerDomainServiceMock.customerGuid, customer.Id);
            Assert.Equal("Customer A", customer.Name);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Get_Null()
        {
            // Act
            var customer = await _appService.GetAsync(null);

            // Assert
            Assert.Null(customer);
            Assert.True(LocalNotification.HasNotification());

            var message = string.Format(LocalLocalizationSource.GetString(ApplicationService.Error.ApplicationServiceOnInvalidIdError, _culture), "request");
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);

            // Act
            customer = await _appService.GetAsync(new DefaultRequestDto());

            // Assert
            Assert.Null(customer);
            Assert.True(LocalNotification.HasNotification());

            message = string.Format(LocalLocalizationSource.GetString(ApplicationService.Error.ApplicationServiceOnInvalidIdError, _culture), "request");
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Get_Not_Found()
        {
            // Act
            var customer = await _appService.GetAsync(new DefaultRequestDto(Guid.NewGuid()));

            // Assert
            Assert.Null(customer);
        }


        [Fact]
        public async Task Should_Create_Customer()
        {
            // Act
            var customer = await _appService.CreateAsync(new CustomerDto
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
            var customer = await _appService.CreateAsync(null);

            // Assert
            Assert.Null(customer);
            Assert.True(LocalNotification.HasNotification());
            var message = string.Format(LocalLocalizationSource.GetString(ApplicationService.Error.ApplicationServiceOnInvalidDtoError, _culture), "dto");
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Create_With_Specifications()
        {
            // Act
            var customer = await _appService.CreateAsync(new CustomerDto());

            // Assert
            Assert.Null(customer);
            Assert.True(LocalNotification.HasNotification());

            var message = GetLocalizedString(Constants.LocalizationSourceName, Customer.Error.CustomerShouldHaveName, _culture);
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }


        [Fact]
        public async Task Should_Update_Customer()
        {
            // Act
            var customer = await _appService.UpdateAsync(CustomerDomainServiceMock.customerGuid, new CustomerDto
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
            var customer = await _appService.UpdateAsync(Guid.Empty, null);

            // Assert
            Assert.Null(customer);
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
            var customer = await _appService.UpdateAsync(CustomerDomainServiceMock.customerGuid, new CustomerDto());

            // Assert
            Assert.NotNull(customer);
            Assert.True(LocalNotification.HasNotification());

            var message = GetLocalizedString(Constants.LocalizationSourceName, Customer.Error.CustomerShouldHaveName, _culture);
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }


        [Fact]
        public Task Should_Delete_Customer()
        {
            // Act
            return _appService.DeleteAsync(CustomerDomainServiceMock.customerGuid);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Delete()
        {
            // Act
            await _appService.DeleteAsync(Guid.Empty);

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var message = string.Format(LocalLocalizationSource.GetString(ApplicationService.Error.ApplicationServiceOnInvalidIdError, _culture), "id");
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }
    }
}
