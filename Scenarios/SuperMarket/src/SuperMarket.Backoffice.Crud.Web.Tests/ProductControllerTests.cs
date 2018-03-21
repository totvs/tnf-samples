using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SuperMarket.Backoffice.Crud.Domain.Entities;
using SuperMarket.Backoffice.Crud.Infra.Dtos;
using SuperMarket.Backoffice.Crud.Web.Controllers;
using SuperMarket.Backoffice.Crud.Web.Tests.Mocks;
using System;
using System.Net;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.AspNetCore.TestBase;
using Tnf.Domain.Services;
using Tnf.Dto;
using Xunit;

namespace SuperMarket.Backoffice.Crud.Web.Tests
{
    public class ProductControllerTests : TnfAspNetCoreIntegratedTestBase<StartupControllerTest>
    {

        [Fact]
        public void Should_Resolve_All()
        {
            TnfSession.ShouldNotBeNull();
            ServiceProvider.GetService<ProductController>().ShouldNotBeNull();
            ServiceProvider.GetService<IDomainService<Product, Guid>>().ShouldNotBeNull();
        }


        [Fact]
        public async Task Should_GetAll()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<ProductDto, Guid>>(
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
                $"{WebConstants.ProductRouteName}/{ProductDomainServiceMock.productGuid}"
            );

            // Assert
            Assert.Equal(product.Id, ProductDomainServiceMock.productGuid);
            Assert.Equal("Product A", product.Description);
            Assert.Equal(5, product.Value);
        }

        [Fact]
        public async Task Should_Return_Null_On_Get_Not_Found()
        {
            // Act
            var responseDto = await GetResponseAsObjectAsync<ProductDto>(
                $"{WebConstants.ProductRouteName}/{Guid.Empty}",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.False(responseDto.IsNullable());
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
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.False(response.IsNullable());
        }


        [Fact]
        public async Task Should_Update_Product()
        {
            // Act
            var product = await PutResponseAsObjectAsync<ProductDto, ProductDto>(
                $"{WebConstants.ProductRouteName}/{ProductDomainServiceMock.productGuid}",
                new ProductDto() { Description = "Product @", Value = 25 }
            );

            // Assert
            Assert.Equal(ProductDomainServiceMock.productGuid, product.Id);
            Assert.Equal("Product @", product.Description);
            Assert.Equal(25, product.Value);
        }

        [Fact]
        public async Task Should_Return_Null_On_Update()
        {
            // Act
            var response = await PutResponseAsObjectAsync<ProductDto, ProductDto>(
                $"{WebConstants.ProductRouteName}/{Guid.Empty}",
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.False(response.IsNullable());
        }


        [Fact]
        public async Task Should_Delete_Product()
        {
            // Act
            await DeleteResponseAsync(
                $"{WebConstants.ProductRouteName}/{ProductDomainServiceMock.productGuid}"
            );
        }
    }
}
