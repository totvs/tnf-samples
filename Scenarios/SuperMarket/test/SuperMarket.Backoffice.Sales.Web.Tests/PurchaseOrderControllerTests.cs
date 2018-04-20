using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SuperMarket.Backoffice.Crud.Web.Controllers;
using SuperMarket.Backoffice.Sales.Application.Services.Interfaces;
using SuperMarket.Backoffice.Sales.Dto;
using SuperMarket.Backoffice.Sales.Web.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Tnf.AspNetCore.TestBase;
using Tnf.Dto;
using Xunit;

namespace SuperMarket.Backoffice.Sales.Web.Tests
{
    public class PurchaseOrderControllerTests : TnfAspNetCoreIntegratedTestBase<StartupControllerTest>
    {

        [Fact]
        public void Should_Resolve_All()
        {
            TnfSession.ShouldNotBeNull();
            ServiceProvider.GetService<PurchaseOrderController>().ShouldNotBeNull();
            ServiceProvider.GetService<IPurchaseOrderAppService>().ShouldNotBeNull();
        }


        [Fact]
        public async Task Should_GetAll()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<PurchaseOrderDto>>(
                WebConstants.PurchaseOrderRouteName
            );

            // Assert
            Assert.False(response.HasNext);
            Assert.Equal(3, response.Items.Count);
        }


        [Fact]
        public async Task Should_Get_PurchaseOrder()
        {
            // Act
            var purchaseOrder = await GetResponseAsObjectAsync<PurchaseOrderDto>(
                $"{WebConstants.PurchaseOrderRouteName}/{PurchaseOrderAppServiceMock.purchaseOrderGuid}"
            );

            // Assert
            Assert.Equal(purchaseOrder.Id, PurchaseOrderAppServiceMock.purchaseOrderGuid);
            Assert.Equal(10, purchaseOrder.Discount);
        }

        [Fact]
        public async Task Should_Return_Null_On_Get_Not_Found()
        {
            // Act
            var response = await GetResponseAsObjectAsync<PurchaseOrderDto>(
                $"{WebConstants.PurchaseOrderRouteName}/{Guid.NewGuid()}",
                HttpStatusCode.NotFound
            );

            // Assert
            Assert.True(response.IsNullable());

            // Act
            response = await GetResponseAsObjectAsync<PurchaseOrderDto>(
                $"{WebConstants.PurchaseOrderRouteName}/{Guid.Empty}",
                HttpStatusCode.NotFound
            );

            // Assert
            Assert.True(response.IsNullable());
        }


        [Fact]
        public async Task Should_Create_PurchaseOrder()
        {
            // Act
            var purchaseOrder = await PostResponseAsObjectAsync<PurchaseOrderDto, PurchaseOrderDto>(
                WebConstants.PurchaseOrderRouteName,
                new PurchaseOrderDto() { Discount = 30, CustomerId = Guid.NewGuid() }
            );

            // Assert
            Assert.NotNull(purchaseOrder);
            Assert.NotEqual(Guid.Empty, purchaseOrder.Id);
            Assert.Equal(30, purchaseOrder.Discount);
        }

        [Fact]
        public async Task Should_Return_Null_On_Create()
        {
            // Act
            var response = await PostResponseAsObjectAsync<PurchaseOrderDto, PurchaseOrderDto>(
                WebConstants.PurchaseOrderRouteName,
                null
            );

            // Assert
            Assert.Null(response);
        }


        [Fact]
        public async Task Should_Update_PurchaseOrder()
        {
            // Act
            var purchaseOrder = await PutResponseAsObjectAsync<PurchaseOrderDto, PurchaseOrderDto>(
                $"{WebConstants.PurchaseOrderRouteName}/{PurchaseOrderAppServiceMock.purchaseOrderGuid}",
                new PurchaseOrderDto() { Discount = 25 }
            );

            // Assert
            Assert.Equal(PurchaseOrderAppServiceMock.purchaseOrderGuid, purchaseOrder.Id);
            Assert.Equal(25, purchaseOrder.Discount);
        }

        [Fact]
        public async Task Should_Return_Null_On_Update()
        {
            // Act
            var response = await PutResponseAsObjectAsync<PurchaseOrderDto, PurchaseOrderDto>(
                $"{WebConstants.PurchaseOrderRouteName}/{Guid.Empty}",
                null
            );

            // Assert
            Assert.Null(response);
        }
    }
}
