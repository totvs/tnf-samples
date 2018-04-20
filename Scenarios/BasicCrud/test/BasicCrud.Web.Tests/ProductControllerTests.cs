using BasicCrud.Web.Controllers;
using Shouldly;
using System;
using System.Threading.Tasks;
using Tnf.AspNetCore.TestBase;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using BasicCrud.Application.Services.Interfaces;
using Tnf.Dto;
using BasicCrud.Dto.Product;
using BasicCrud.Web.Tests.Mocks;
using System.Net;

namespace BasicCrud.Web.Tests
{
    public class ProductControllerTests : TnfAspNetCoreIntegratedTestBase<StartupControllerTest>
    {

        [Fact]
        public void Should_Resolve_All()
        {
            TnfSession.ShouldNotBeNull();
            ServiceProvider.GetService<ProductController>().ShouldNotBeNull();
            ServiceProvider.GetService<IProductAppService>().ShouldNotBeNull();
        }


        [Fact]
        public async Task Should_GetAll()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<ProductDto>>(
                WebConstants.ProductRouteName
            );

            // Assert
            Assert.False(response.HasNext);
            Assert.Equal(3, response.Items.Count);
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
        public async Task Should_Return_Null_On_Get_Not_Found()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ProductDto>(
                $"{WebConstants.ProductRouteName}/{Guid.NewGuid()}",
                HttpStatusCode.NotFound
            );

            // Assert
            Assert.True(response.IsNullable());

            // Act
            response = await GetResponseAsObjectAsync<ProductDto>(
                $"{WebConstants.ProductRouteName}/{Guid.Empty}",
                HttpStatusCode.NotFound
            );

            // Assert
            Assert.True(response.IsNullable());
        }


        [Fact]
        public async Task Should_Create_Product()
        {
            // Act
            var product = await PostResponseAsObjectAsync<ProductDto, ProductDto>(
                WebConstants.ProductRouteName,
                new ProductDto() { Description = "Product @", Value = 25 }
            );

            // Assert
            Assert.NotNull(product);
        }

        [Fact]
        public async Task Should_Return_Null_On_Create()
        {
            // Act
            var response = await PostResponseAsObjectAsync<ProductDto, ProductDto>(
                WebConstants.ProductRouteName,
                null
            );

            // Assert
            Assert.Null(response);
        }


        [Fact]
        public async Task Should_Update_Product()
        {
            // Act
            var product = await PutResponseAsObjectAsync<ProductDto, ProductDto>(
                $"{WebConstants.ProductRouteName}/{ProductAppServiceMock.productGuid}",
                new ProductDto() { Description = "Product @", Value = 25 }
            );

            // Assert
            Assert.Equal(ProductAppServiceMock.productGuid, product.Id);
            Assert.Equal("Product @", product.Description);
            Assert.Equal(25, product.Value);
        }

        [Fact]
        public async Task Should_Return_Null_On_Update()
        {
            // Act
            var response = await PutResponseAsObjectAsync<ProductDto, ProductDto>(
                $"{WebConstants.ProductRouteName}/{Guid.Empty}",
                null
            );

            // Assert
            Assert.Null(response);
        }


        [Fact]
        public Task Should_Delete_Product()
        {
            // Act
            return DeleteResponseAsync(
                $"{WebConstants.ProductRouteName}/{ProductAppServiceMock.productGuid}"
            );
        }
    }
}
