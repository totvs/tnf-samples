using BasicCrud.Application.Services.Interfaces;
using BasicCrud.Domain;
using BasicCrud.Domain.Entities;
using BasicCrud.Domain.Interfaces.Repositories;
using BasicCrud.Domain.Interfaces.Services;
using BasicCrud.Dto.Product;
using BasicCrud.Infra.ReadInterfaces;
using BasicCrud.Infra.SqlServer.Context;
using BasicCrud.Web.Controllers;
using BasicCrud.Web.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Tnf;
using Tnf.Application.Services;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.AspNetCore.TestBase;
using Tnf.Domain.Services;
using Tnf.Dto;
using Tnf.EntityFrameworkCore;
using Tnf.Localization;
using Tnf.Notifications;
using Xunit;

namespace BasicCrud.Web.Tests
{
    public class ProductIntegratedTests : TnfAspNetCoreIntegratedTestBase<StartupIntegratedTest>
    {
        private readonly ILocalizationSource _localizationSource;
        private readonly ILocalizationSource _tnfLocalizationSource;
        private readonly CultureInfo _culture;

        public ProductIntegratedTests()
        {
            var notificationHandler = new NotificationHandler(ServiceProvider);

            _localizationSource = ServiceProvider.GetService<ILocalizationManager>().GetSource(Constants.LocalizationSourceName);
            _tnfLocalizationSource = ServiceProvider.GetService<ILocalizationManager>().GetSource(TnfConsts.LocalizationSourceName);

            _culture = CultureInfo.GetCultureInfo("pt-BR");

            ServiceProvider.UsingDbContext<BasicCrudDbContext>(context =>
            {
                context.Products.Add(Product.Create(notificationHandler)
                    .WithId(ProductAppServiceMock.productGuid)
                    .WithDescription("Product A")
                    .WithValue(5)
                    .Build());

                for (var i = 2; i < 21; i++)
                    context.Products.Add(Product.Create(notificationHandler)
                        .WithId(Guid.NewGuid())
                        .WithDescription($"Product {NumberToAlphabetLetter(i, true)}")
                        .WithValue(5 * i)
                        .Build());

                context.SaveChanges();
            });
        }

        private string NumberToAlphabetLetter(int number, bool isCaps)
        {
            Char c = (Char)((isCaps ? 65 : 97) + (number - 1));
            return c.ToString();
        }

        [Fact]
        public void Should_Resolve_All()
        {
            TnfSession.ShouldNotBeNull();
            ServiceProvider.GetService<ProductController>().ShouldNotBeNull();
            ServiceProvider.GetService<IProductAppService>().ShouldNotBeNull();
            ServiceProvider.GetService<IProductDomainService>().ShouldNotBeNull();
            ServiceProvider.GetService<IProductRepository>().ShouldNotBeNull();
            ServiceProvider.GetService<IProductReadRepository>().ShouldNotBeNull();
        }


        [Fact]
        public async Task Should_GetAll_With_Paginated()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<ProductDto, Guid>>(
                WebConstants.ProductRouteName
            );

            // Assert
            Assert.True(response.HasNext);
            Assert.Equal(10, response.Items.Count);

            // Act
            response = await GetResponseAsObjectAsync<ListDto<ProductDto, Guid>>(
                $"{WebConstants.ProductRouteName}?pageSize=30"
            );

            // Assert
            Assert.False(response.HasNext);
            Assert.Equal(20, response.Items.Count);
        }

        [Fact]
        public async Task Should_GetAll_Sorted()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<ProductDto, Guid>>(
                $"{WebConstants.ProductRouteName}?pageSize=20&order=-description"
            );

            // Assert
            Assert.Equal(20, response.Items.Count);
            Assert.Equal("Product T", response.Items[0].Description);
            Assert.Equal("Product A", response.Items.Last().Description);

            // Act
            response = await GetResponseAsObjectAsync<ListDto<ProductDto, Guid>>(
                $"{WebConstants.ProductRouteName}?order=-description"
            );

            // Assert
            Assert.Equal(10, response.Items.Count);
            Assert.Equal("Product T", response.Items[0].Description);
            Assert.Equal("Product K", response.Items.Last().Description);
        }

        [Fact]
        public async Task Should_GetAll_By_Description()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<ProductDto, Guid>>(
                $"{WebConstants.ProductRouteName}?pageSize=20&description=Product%20"
            );

            // Assert
            Assert.Equal(20, response.Items.Count);
            Assert.All(response.Items, p => p.Description.Contains("Product "));

            // Act
            response = await GetResponseAsObjectAsync<ListDto<ProductDto, Guid>>(
                $"{WebConstants.ProductRouteName}?description=Product%20C"
            );

            // Assert
            Assert.Equal(1, response.Items.Count);
            Assert.Equal("Product C", response.Items[0].Description);
        }


        [Fact]
        public async Task Should_Get_Product()
        {
            // Act
            var product = await GetResponseAsObjectAsync<ProductDto>(
                $"{WebConstants.ProductRouteName}/{ProductAppServiceMock.productGuid}"
            );

            // Assert
            Assert.Equal(product.Id, ProductAppServiceMock.productGuid);
            Assert.Equal("Product A", product.Description);
            Assert.Equal(5, product.Value);
        }

        [Fact]
        public async Task Should_Get_Product_Select_Fields()
        {
            // Act
            var product = await GetResponseAsObjectAsync<ProductDto>(
                $"{WebConstants.ProductRouteName}/{ProductAppServiceMock.productGuid}?fields=description"
            );

            // Assert
            Assert.Equal(product.Id, Guid.Empty);
            Assert.Equal("Product A", product.Description);
            Assert.Equal(0, product.Value);

            // Act
            product = await GetResponseAsObjectAsync<ProductDto>(
                $"{WebConstants.ProductRouteName}/{ProductAppServiceMock.productGuid}?fields=value"
            );

            // Assert
            Assert.Equal(product.Id, Guid.Empty);
            Assert.Null(product.Description);
            Assert.Equal(5, product.Value);
        }

        [Fact]
        public async Task Should_Return_Null_On_Get_Not_Found()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ProductDto>(
                $"{WebConstants.ProductRouteName}/{Guid.NewGuid()}",
                HttpStatusCode.NotFound
            );

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Get_Null()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ErrorResponse>(
                $"{WebConstants.ProductRouteName}/{Guid.Empty}",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Details.Count);

            var message = string.Format(_tnfLocalizationSource.GetString(TnfController.Error.AspNetCoreOnGetError, _culture), "Product");
            Assert.Equal(message, response.Message);
            message = string.Format(_tnfLocalizationSource.GetString(ApplicationService.Error.ApplicationServiceOnInvalidIdError, _culture), "request");
            Assert.Contains(response.Details, n => n.Message == message);
        }


        [Fact]
        public async Task Should_Create_Product()
        {
            // Act
            var product = await PostResponseAsObjectAsync<ProductDto, ProductDto>(
                WebConstants.ProductRouteName,
                new ProductDto() { Description = "Product @", Value = 110}
            );

            var response = await GetResponseAsObjectAsync<ListDto<ProductDto, Guid>>(
                $"{WebConstants.ProductRouteName}?pageSize=30"
            );

            // Assert
            Assert.NotNull(product);
            Assert.False(response.HasNext);
            Assert.Equal(21, response.Items.Count);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Create()
        {
            // Act
            var response = await PostResponseAsObjectAsync<ProductDto, ErrorResponse>(
                WebConstants.ProductRouteName,
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Details.Count);

            var message = string.Format(_tnfLocalizationSource.GetString(TnfController.Error.AspNetCoreOnPostError, _culture), "Product");
            Assert.Equal(message, response.Message);
            message = string.Format(_tnfLocalizationSource.GetString(ApplicationService.Error.ApplicationServiceOnInvalidDtoError, _culture), "dto");
            Assert.Contains(response.Details, n => n.Message == message);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Create_With_Specifications()
        {
            // Act
            var response = await PostResponseAsObjectAsync<ProductDto, ErrorResponse>(
                WebConstants.ProductRouteName,
                new ProductDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(2, response.Details.Count);

            var message = string.Format(_tnfLocalizationSource.GetString(TnfController.Error.AspNetCoreOnPostError, _culture), "Product");
            Assert.Equal(message, response.Message);
            message = _localizationSource.GetString(Product.Error.ProductShouldHaveDescription, _culture);
            Assert.Contains(response.Details, n => n.Message == message);
            message = _localizationSource.GetString(Product.Error.ProductShouldHaveValue, _culture);
            Assert.Contains(response.Details, n => n.Message == message);
        }


        [Fact]
        public async Task Should_Update_Product()
        {
            // Act
            var product = await PutResponseAsObjectAsync<ProductDto, ProductDto>(
                $"{WebConstants.ProductRouteName}/{ProductAppServiceMock.productGuid}",
                new ProductDto() { Description = "Product @", Value = 110 }
            );

            // Assert
            Assert.Equal(product.Id, ProductAppServiceMock.productGuid);
            Assert.Equal("Product @", product.Description);
            Assert.Equal(110, product.Value);

            // Act
            product = await GetResponseAsObjectAsync<ProductDto>(
                $"{WebConstants.ProductRouteName}/{ProductAppServiceMock.productGuid}"
            );

            // Assert
            Assert.Equal(product.Id, ProductAppServiceMock.productGuid);
            Assert.Equal("Product @", product.Description);
            Assert.Equal(110, product.Value);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Update()
        {
            // Act
            var response = await PutResponseAsObjectAsync<ProductDto, ErrorResponse>(
                $"{WebConstants.ProductRouteName}/{Guid.Empty}",
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(2, response.Details.Count);

            var message = string.Format(_tnfLocalizationSource.GetString(TnfController.Error.AspNetCoreOnPutError, _culture), "Product");
            Assert.Equal(message, response.Message);
            message = string.Format(_tnfLocalizationSource.GetString(ApplicationService.Error.ApplicationServiceOnInvalidIdError, _culture), "id");
            Assert.Contains(response.Details, n => n.Message == message);
            message = string.Format(_tnfLocalizationSource.GetString(ApplicationService.Error.ApplicationServiceOnInvalidDtoError, _culture), "dto");
            Assert.Contains(response.Details, n => n.Message == message);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Update_With_Specifications()
        {
            // Act
            var response = await PutResponseAsObjectAsync<ProductDto, ErrorResponse>(
                $"{WebConstants.ProductRouteName}/{ProductAppServiceMock.productGuid}",
                new ProductDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(2, response.Details.Count);

            var message = string.Format(_tnfLocalizationSource.GetString(TnfController.Error.AspNetCoreOnPutError, _culture), "Product");
            Assert.Equal(message, response.Message);
            message = _localizationSource.GetString(Product.Error.ProductShouldHaveDescription, _culture);
            Assert.Contains(response.Details, n => n.Message == message);
            message = _localizationSource.GetString(Product.Error.ProductShouldHaveValue, _culture);
            Assert.Contains(response.Details, n => n.Message == message);
        }


        [Fact]
        public async Task Should_Delete_Product()
        {
            // Act
            await DeleteResponseAsync(
                $"{WebConstants.ProductRouteName}/{ProductAppServiceMock.productGuid}"
            );

            var response = await GetResponseAsObjectAsync<ListDto<ProductDto, Guid>>(
                $"{WebConstants.ProductRouteName}?pageSize=30"
            );

            // Assert
            Assert.False(response.HasNext);
            Assert.Equal(19, response.Items.Count);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Delete()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<ErrorResponse>(
                $"{WebConstants.ProductRouteName}/{Guid.Empty}",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Details.Count);

            var message = string.Format(_tnfLocalizationSource.GetString(TnfController.Error.AspNetCoreOnDeleteError, _culture), "Product");
            Assert.Equal(message, response.Message);
            message = string.Format(_tnfLocalizationSource.GetString(ApplicationService.Error.ApplicationServiceOnInvalidIdError, _culture), "id");
            Assert.Contains(response.Details, n => n.Message == message);
        }

        [Fact]
        public async Task Should_Return_Null_On_Delete_NotFound()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<ProductDto>(
                $"{WebConstants.ProductRouteName}/{Guid.NewGuid()}"
            );

            // Assert
            Assert.Null(response);
        }
    }
}
