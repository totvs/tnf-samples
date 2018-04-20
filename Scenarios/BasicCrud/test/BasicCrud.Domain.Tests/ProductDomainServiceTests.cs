using BasicCrud.Domain.Entities;
using BasicCrud.Domain.Interfaces.Repositories;
using BasicCrud.Domain.Interfaces.Services;
using BasicCrud.Domain.Services;
using BasicCrud.Domain.Tests.Mocks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Tnf.Domain.Services;
using Tnf.TestBase;
using Xunit;

namespace BasicCrud.Domain.Tests
{
    public class ProductDomainServiceTests : TnfIntegratedTestBase
    {
        private readonly IProductDomainService _domainService;
        private readonly CultureInfo _culture;

        public ProductDomainServiceTests()
        {
            _domainService = Resolve<IProductDomainService>();

            _culture = CultureInfo.GetCultureInfo("pt-BR");
        }

        protected override void PreInitialize(IServiceCollection services)
        {
            base.PreInitialize(services);

            // Registro dos serviços de Mock
            services.AddTransient<IProductRepository, ProductRepositoryMock>();

            // Registro dos serviços para teste
            services.AddTransient<IProductDomainService, ProductDomainService>();
        }

        protected override void PostInitialize(IServiceProvider provider)
        {
            base.PostInitialize(provider);

            provider.ConfigureTnf().UseDomainLocalization();
        }

        [Fact]
        public void Should_Resolve_All()
        {
            TnfSession.ShouldNotBeNull();
            ServiceProvider.GetService<IProductDomainService>().ShouldNotBeNull();
            ServiceProvider.GetService<IProductRepository>().ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_Create_Product()
        {
            // Act
            var product = await _domainService.InsertProductAsync(
                Product.Create(LocalNotification)
                    .WithDescription("Product @")
                    .WithValue(20));

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.NotEqual(Guid.Empty, product.Id);
            Assert.Equal("Product @", product.Description);
            Assert.Equal(20, product.Value);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Create()
        {
            // Act
            var product = await _domainService.InsertProductAsync(null);

            
            // Assert
            Assert.Null(product);
            Assert.True(LocalNotification.HasNotification());

            var message = GetLocalizedString(Constants.LocalizationSourceName, DomainService.Error.DomainServiceOnInsertNullBuilderError, _culture);
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Create_With_Specifications()
        {
            // Act
            var product = await _domainService.InsertProductAsync(Product.Create(LocalNotification));

            // Assert
            Assert.Null(product);
            Assert.True(LocalNotification.HasNotification());

            var message = GetLocalizedString(Constants.LocalizationSourceName, Product.Error.ProductShouldHaveDescription, _culture);
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);

            message = GetLocalizedString(Constants.LocalizationSourceName, Product.Error.ProductShouldHaveValue, _culture);
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }


        [Fact]
        public async Task Should_Update_Product()
        {
            // Act
            var product = await _domainService.UpdateProductAsync(
                Product.Create(LocalNotification)
                    .WithId(ProductRepositoryMock.productGuid)
                    .WithDescription("Product @")
                    .WithValue(20));

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.Equal(product.Id, ProductRepositoryMock.productGuid);
            Assert.Equal("Product @", product.Description);
            Assert.Equal(20, product.Value);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Update()
        {
            // Act
            var product = await _domainService.UpdateProductAsync(null);

            // Assert
            Assert.Null(product);
            Assert.True(LocalNotification.HasNotification());
            Assert.Single(LocalNotification.GetAll());

            var message = GetLocalizedString(Constants.LocalizationSourceName, DomainService.Error.DomainServiceOnUpdateNullBuilderError, _culture);
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Update_With_Specifications()
        {
            // Act
            var product = await _domainService.UpdateProductAsync(Product.Create(LocalNotification));

            // Assert
            Assert.Null(product);
            Assert.True(LocalNotification.HasNotification());

            var message = GetLocalizedString(Constants.LocalizationSourceName, Product.Error.ProductShouldHaveDescription, _culture);
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);

            message = GetLocalizedString(Constants.LocalizationSourceName, Product.Error.ProductShouldHaveValue, _culture);
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }


        [Fact]
        public Task Should_Delete_Product()
        {
            // Act
            return _domainService.DeleteProductAsync(ProductRepositoryMock.productGuid);
        }

    }
}
