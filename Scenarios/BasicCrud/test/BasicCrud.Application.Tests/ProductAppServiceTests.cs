using BasicCrud.Application.Services;
using BasicCrud.Application.Services.Interfaces;
using BasicCrud.Application.Tests.Mocks;
using BasicCrud.Domain;
using BasicCrud.Domain.Entities;
using BasicCrud.Domain.Interfaces.Services;
using BasicCrud.Dto;
using BasicCrud.Dto.Product;
using BasicCrud.Infra;
using BasicCrud.Infra.ReadInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.TestBase;
using Xunit;

namespace BasicCrud.Application.Tests
{
    public class ProductAppServiceTests : TnfIntegratedTestBase
    {
        private readonly IProductAppService _appService;
        private readonly CultureInfo _culture;

        public ProductAppServiceTests()
        {
            _appService = Resolve<IProductAppService>();

            _culture = CultureInfo.GetCultureInfo("pt-BR");
        }

        protected override void PreInitialize(IServiceCollection services)
        {
            base.PreInitialize(services);

            // Configura o mesmo Mapper para ser testado
            services.AddMapperDependency();

            // Registro dos serviços de Mock
            services.AddTransient<IProductDomainService, ProductServiceMock>();
            services.AddTransient<IProductReadRepository, ProductServiceMock>();

            // Registro dos serviços para teste
            services.AddTransient<IProductAppService, ProductAppService>();
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
            ServiceProvider.GetService<IProductAppService>().ShouldNotBeNull();
            ServiceProvider.GetService<IProductDomainService>().ShouldNotBeNull();
            ServiceProvider.GetService<IProductReadRepository>().ShouldNotBeNull();
        }


        [Fact]
        public async Task Should_GetAll()
        {
            // Act
            var response = await _appService.GetAllProductAsync(new ProductRequestAllDto());

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.False(response.HasNext);
            Assert.Equal(3, response.Items.Count);
        }


        [Fact]
        public async Task Should_Get_Product()
        {
            // Act
            var product = await _appService.GetProductAsync(new DefaultRequestDto(ProductServiceMock.productGuid));

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.Equal(ProductServiceMock.productGuid, product.Id);
            Assert.Equal("Product A", product.Description);
            Assert.Equal(5, product.Value);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Get_Null()
        {
            // Act
            var product = await _appService.GetProductAsync(null);

            // Assert
            Assert.Null(product);
            Assert.True(LocalNotification.HasNotification());
            var message = string.Format(LocalLocalizationSource.GetString(ApplicationService.Error.ApplicationServiceOnInvalidIdError, _culture), "request");
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);

            // Act
            product = await _appService.GetProductAsync(new DefaultRequestDto());

            // Assert
            Assert.Null(product);
            Assert.True(LocalNotification.HasNotification());
            message = string.Format(LocalLocalizationSource.GetString(ApplicationService.Error.ApplicationServiceOnInvalidIdError, _culture), "request");
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Get_Not_Found()
        {
            // Act
            var product = await _appService.GetProductAsync(new DefaultRequestDto(Guid.NewGuid()));

            // Assert
            Assert.Null(product);
        }


        [Fact]
        public async Task Should_Create_Product()
        {
            // Act
            var product = await _appService.CreateProductAsync(new ProductDto
            {
                Description = "Product @",
                Value = 20
            });

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
            var product = await _appService.CreateProductAsync(null);

            // Assert
            Assert.Null(product);
            Assert.True(LocalNotification.HasNotification());
            var message = string.Format(LocalLocalizationSource.GetString(ApplicationService.Error.ApplicationServiceOnInvalidDtoError, _culture), "dto");
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Create_With_Specifications()
        {
            // Act
            var product = await _appService.CreateProductAsync(new ProductDto());

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
            var product = await _appService.UpdateProductAsync(ProductServiceMock.productGuid, new ProductDto
            {
                Description = "Product @",
                Value = 20
            });

            // Assert
            Assert.False(LocalNotification.HasNotification());
            Assert.Equal(product.Id, ProductServiceMock.productGuid);
            Assert.Equal("Product @", product.Description);
            Assert.Equal(20, product.Value);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Update()
        {
            // Act
            var product = await _appService.UpdateProductAsync(Guid.Empty, null);

            // Assert
            Assert.Null(product);
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
            var product = await _appService.UpdateProductAsync(ProductServiceMock.productGuid, new ProductDto());

            // Assert
            Assert.NotNull(product);
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
            return _appService.DeleteProductAsync(ProductServiceMock.productGuid);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Delete()
        {
            // Act
            await _appService.DeleteProductAsync(Guid.Empty);

            // Assert
            Assert.True(LocalNotification.HasNotification());
            var message = string.Format(LocalLocalizationSource.GetString(ApplicationService.Error.ApplicationServiceOnInvalidIdError, _culture), "id");
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }
    }
}
