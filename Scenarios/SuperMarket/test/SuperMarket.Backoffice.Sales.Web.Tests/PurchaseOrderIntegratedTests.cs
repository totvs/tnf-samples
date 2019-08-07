using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SuperMarket.Backoffice.Crud.Web.Controllers;
using SuperMarket.Backoffice.Sales.Application.Services.Interfaces;
using SuperMarket.Backoffice.Sales.Domain;
using SuperMarket.Backoffice.Sales.Domain.Entities;
using SuperMarket.Backoffice.Sales.Domain.Interfaces;
using SuperMarket.Backoffice.Sales.Dto;
using SuperMarket.Backoffice.Sales.Infra.Contexts;
using SuperMarket.Backoffice.Sales.Infra.Pocos;
using SuperMarket.Backoffice.Sales.Infra.Repositories.Interfaces;
using SuperMarket.Backoffice.Sales.Web.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Tnf;
using Tnf.Application.Services;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.AspNetCore.TestBase;
using Tnf.Dto;
using Tnf.EntityFrameworkCore;
using Tnf.Localization;
using Tnf.Notifications;
using Xunit;
using static SuperMarket.Backoffice.Sales.Dto.PurchaseOrderDto;

namespace SuperMarket.Backoffice.Sales.Web.Tests
{
    public class PurchaseOrderIntegratedTests : TnfAspNetCoreIntegratedTestBase<StartupIntegratedTest>
    {
        private readonly ILocalizationSource _localizationSource;
        private readonly ILocalizationSource _tnfLocalizationSource;
        private readonly CultureInfo _culture;

        private readonly Guid _numberGuid = Guid.NewGuid();
        private readonly Guid _productGuid = Guid.NewGuid();

        public PurchaseOrderIntegratedTests()
        {
            _localizationSource = ServiceProvider.GetService<ILocalizationManager>().GetSource(Constants.LocalizationSourceName);
            _tnfLocalizationSource = ServiceProvider.GetService<ILocalizationManager>().GetSource(TnfConsts.LocalizationSourceName);

            _culture = CultureInfo.GetCultureInfo("pt-BR");

            PopulateDataBase();
        }

        private void PopulateDataBase()
        {
            var notificationHandler = new NotificationHandler(ServiceProvider);

            var product2 = Guid.NewGuid();
            var product3 = Guid.NewGuid();
            var product4 = Guid.NewGuid();

            // Registro dos serviços de Mock
            var priceTableMock = ServiceProvider.GetService<PriceTableMock>();
            priceTableMock.priceTable = PriceTable.CreateTable(new Dictionary<Guid, decimal>()
            {
                { _productGuid, 10 },
                { product2, 20 },
                { product3, 30 },
                { product4, 40 },
            });

            ServiceProvider.UsingDbContext<SalesContext>(context =>
            {
                context.PurchaseOrders.Add(new PurchaseOrderPoco()
                {
                    Id = PurchaseOrderAppServiceMock.purchaseOrderGuid,
                    CustomerId = Guid.NewGuid(),
                    Date = new DateTime(2018, 2, 28),
                    Discount = 10,
                    Number = Guid.NewGuid(),
                    Status = PurchaseOrder.PurchaseOrderStatus.Completed,
                    PurchaseOrderProducts = new List<PurchaseOrderProductPoco>
                    {
                        new PurchaseOrderProductPoco() { ProductId = _productGuid, PurchaseOrderId = PurchaseOrderAppServiceMock.purchaseOrderGuid, Quantity = 1, UnitValue = 10 },
                        new PurchaseOrderProductPoco() { ProductId = product2, PurchaseOrderId = PurchaseOrderAppServiceMock.purchaseOrderGuid, Quantity = 2, UnitValue = 20 },
                        new PurchaseOrderProductPoco() { ProductId = product3, PurchaseOrderId = PurchaseOrderAppServiceMock.purchaseOrderGuid, Quantity = 3, UnitValue = 30 },
                        new PurchaseOrderProductPoco() { ProductId = product4, PurchaseOrderId = PurchaseOrderAppServiceMock.purchaseOrderGuid, Quantity = 4, UnitValue = 40 }
                    }
                });

                var purchaseOrderId = Guid.NewGuid();

                context.PurchaseOrders.Add(new PurchaseOrderPoco()
                {
                    Id = purchaseOrderId,
                    CustomerId = Guid.NewGuid(),
                    Date = new DateTime(2018, 3, 1),
                    Discount = 20,
                    Number = _numberGuid,
                    Status = PurchaseOrder.PurchaseOrderStatus.Completed,
                    PurchaseOrderProducts = new List<PurchaseOrderProductPoco>
                    {
                        new PurchaseOrderProductPoco() { ProductId = _productGuid, PurchaseOrderId = purchaseOrderId, Quantity = 1, UnitValue = 10 },
                        new PurchaseOrderProductPoco() { ProductId = product2, PurchaseOrderId = purchaseOrderId, Quantity = 2, UnitValue = 20 },
                        new PurchaseOrderProductPoco() { ProductId = product3, PurchaseOrderId = purchaseOrderId, Quantity = 3, UnitValue = 30 },
                        new PurchaseOrderProductPoco() { ProductId = product4, PurchaseOrderId = purchaseOrderId, Quantity = 4, UnitValue = 40 }
                    }
                });

                for (var i = 2; i < 20; i++)
                {
                    purchaseOrderId = Guid.NewGuid();

                    context.PurchaseOrders.Add(new PurchaseOrderPoco()
                    {
                        Id = purchaseOrderId,
                        CustomerId = Guid.NewGuid(),
                        Date = new DateTime(2018, 3, i),
                        Discount = (i + 1) * 10,
                        Number = Guid.NewGuid(),
                        Status = PurchaseOrder.PurchaseOrderStatus.Completed,
                        PurchaseOrderProducts = new List<PurchaseOrderProductPoco>
                        {
                            new PurchaseOrderProductPoco() { ProductId = _productGuid, PurchaseOrderId = purchaseOrderId, Quantity = 1, UnitValue = 10 },
                            new PurchaseOrderProductPoco() { ProductId = product2, PurchaseOrderId = purchaseOrderId, Quantity = 2, UnitValue = 20 },
                            new PurchaseOrderProductPoco() { ProductId = product3, PurchaseOrderId = purchaseOrderId, Quantity = 3, UnitValue = 30 },
                            new PurchaseOrderProductPoco() { ProductId = product4, PurchaseOrderId = purchaseOrderId, Quantity = 4, UnitValue = 40 }
                        }
                    });
                }

                context.SaveChanges();
            });
        }

        [Fact]
        public void Should_Resolve_All()
        {
            Assert.NotNull(TnfSession);
            Assert.NotNull(ServiceProvider.GetService<PurchaseOrderController>());
            Assert.NotNull(ServiceProvider.GetService<IPurchaseOrderAppService>());
            Assert.NotNull(ServiceProvider.GetService<IPurchaseOrderService>());
            Assert.NotNull(ServiceProvider.GetService<IPurchaseOrderRepository>());
            Assert.NotNull(ServiceProvider.GetService<IPriceTableRepository>());
            Assert.NotNull(ServiceProvider.GetService<IPurchaseOrderReadRepository>());
        }


        [Fact]
        public async Task Should_GetAll_With_Paginated()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<PurchaseOrderDto>>(
                WebConstants.PurchaseOrderRouteName
            );

            // Assert
            Assert.True(response.HasNext);
            Assert.Equal(10, response.Items.Count);

            // Act
            response = await GetResponseAsObjectAsync<ListDto<PurchaseOrderDto>>(
                $"{WebConstants.PurchaseOrderRouteName}?pageSize=30"
            );

            // Assert
            Assert.False(response.HasNext);
            Assert.Equal(20, response.Items.Count);
        }

        [Fact]
        public async Task Should_GetAll_Sorted()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<PurchaseOrderDto>>(
                $"{WebConstants.PurchaseOrderRouteName}?pageSize=20&order=-discount"
            );

            // Assert
            Assert.Equal(20, response.Items.Count);
            Assert.Equal(200, response.Items[0].Discount);
            Assert.Equal(10, response.Items.Last().Discount);

            // Act
            response = await GetResponseAsObjectAsync<ListDto<PurchaseOrderDto>>(
                $"{WebConstants.PurchaseOrderRouteName}?order=-discount"
            );

            // Assert
            Assert.Equal(10, response.Items.Count);
            Assert.Equal(200, response.Items[0].Discount);
            Assert.Equal(110, response.Items.Last().Discount);
        }

        [Fact]
        public async Task Should_GetAll_By_Number()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<PurchaseOrderDto>>(
                $"{WebConstants.PurchaseOrderRouteName}?number={_numberGuid}"
            );

            // Assert
            Assert.Equal(1, response.Items.Count);
            Assert.Equal(20, response.Items[0].Discount);
        }

        [Fact]
        public async Task Should_GetAll_By_Date()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<PurchaseOrderDto>>(
                $"{WebConstants.PurchaseOrderRouteName}?startDate=2018/3/1"
            );

            // Assert
            Assert.Equal(1, response.Items.Count);
            Assert.Equal(20, response.Items[0].Discount);

            // Act
            response = await GetResponseAsObjectAsync<ListDto<PurchaseOrderDto>>(
                $"{WebConstants.PurchaseOrderRouteName}?startDate=2018/3/1&endDate=2018/3/20"
            );

            // Assert
            Assert.Equal(10, response.Items.Count);

            // Act
            response = await GetResponseAsObjectAsync<ListDto<PurchaseOrderDto>>(
                $"{WebConstants.PurchaseOrderRouteName}?pageSize=20&startDate=2018/3/1&endDate=2018/3/20"
            );

            // Assert
            Assert.Equal(19, response.Items.Count);
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
            Assert.NotEqual(Guid.Empty, purchaseOrder.CustomerId);
        }

        [Fact]
        public async Task Should_Get_PurchaseOrder_Select_Fields()
        {
            // Act
            var purchaseOrder = await GetResponseAsObjectAsync<PurchaseOrderDto>(
                $"{WebConstants.PurchaseOrderRouteName}/{PurchaseOrderAppServiceMock.purchaseOrderGuid}?fields=discount"
            );

            // Assert
            Assert.Equal(Guid.Empty, purchaseOrder.Id);
            Assert.Equal(10, purchaseOrder.Discount);
            Assert.Equal(Guid.Empty, purchaseOrder.CustomerId);

            // Act
            purchaseOrder = await GetResponseAsObjectAsync<PurchaseOrderDto>(
                $"{WebConstants.PurchaseOrderRouteName}/{PurchaseOrderAppServiceMock.purchaseOrderGuid}?fields=customerId"
            );

            // Assert
            Assert.Equal(Guid.Empty, purchaseOrder.Id);
            Assert.Equal(0, purchaseOrder.Discount);
            Assert.NotEqual(Guid.Empty, purchaseOrder.CustomerId);
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
            Assert.Null(response);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Get_Null()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ErrorResponse>(
                $"{WebConstants.PurchaseOrderRouteName}/{Guid.Empty}",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Details.Count);

            var message = string.Format(_tnfLocalizationSource.GetString(TnfController.Error.AspNetCoreOnGetError, _culture), "PurchaseOrder");
            Assert.Equal(message, response.Message);
            message = string.Format(_tnfLocalizationSource.GetString(ApplicationService.Error.ApplicationServiceOnInvalidIdError, _culture), "id");
            Assert.Contains(response.Details, n => n.Message == message);
        }


        [Fact]
        public async Task Should_Create_PurchaseOrder()
        {
            // Act
            var purchaseOrder = await PostResponseAsObjectAsync<PurchaseOrderDto, PurchaseOrderDto>(
                WebConstants.PurchaseOrderRouteName,
                new PurchaseOrderDto
                {
                    CustomerId = Guid.NewGuid(),
                    Discount = 5,
                    Products = new List<ProductDto>()
                    {
                        new ProductDto(_productGuid, 1)
                    }
                }
            );

            // Assert
            Assert.NotNull(purchaseOrder);
            Assert.NotEqual(Guid.Empty, purchaseOrder.Id);
            Assert.Equal(5, purchaseOrder.Discount);

            var response = await GetResponseAsObjectAsync<ListDto<PurchaseOrderDto>>(
                $"{WebConstants.PurchaseOrderRouteName}?pageSize=30"
            );

            // Assert
            Assert.False(response.HasNext);
            Assert.Equal(21, response.Items.Count);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Create()
        {
            // Act
            var response = await PostResponseAsObjectAsync<PurchaseOrderDto, ErrorResponse>(
                WebConstants.PurchaseOrderRouteName,
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Details.Count);

            var message = string.Format(_tnfLocalizationSource.GetString(TnfController.Error.AspNetCoreOnPostError, _culture), "PurchaseOrder");
            Assert.Equal(message, response.Message);
            message = string.Format(_tnfLocalizationSource.GetString(ApplicationService.Error.ApplicationServiceOnInvalidDtoError, _culture), "dto");
            Assert.Contains(response.Details, n => n.Message == message);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Create_With_Specifications()
        {
            // Act
            var response = await PostResponseAsObjectAsync<PurchaseOrderDto, ErrorResponse>(
                WebConstants.PurchaseOrderRouteName,
                new PurchaseOrderDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(2, response.Details.Count);

            var message = _localizationSource.GetString(PurchaseOrder.Error.PurchaseOrderMustHaveCustomer, _culture);
            Assert.Contains(response.Details, n => n.Message == message);
            message = _localizationSource.GetString(PurchaseOrder.Error.PurchaseOrderMustBeLines, _culture);
            Assert.Contains(response.Details, n => n.Message == message);
        }


        [Fact]
        public async Task Should_Update_PurchaseOrder()
        {
            // Act
            var purchaseOrder = await PutResponseAsObjectAsync<PurchaseOrderDto, PurchaseOrderDto>(
                $"{WebConstants.PurchaseOrderRouteName}/{PurchaseOrderAppServiceMock.purchaseOrderGuid}",
                new PurchaseOrderDto
                {
                    Discount = 5
                }
            );

            // Assert
            Assert.Equal(purchaseOrder.Id, PurchaseOrderAppServiceMock.purchaseOrderGuid);
            Assert.Equal(5, purchaseOrder.Discount);

            // Act
            purchaseOrder = await GetResponseAsObjectAsync<PurchaseOrderDto>(
                $"{WebConstants.PurchaseOrderRouteName}/{PurchaseOrderAppServiceMock.purchaseOrderGuid}"
            );

            // Assert
            Assert.Equal(purchaseOrder.Id, PurchaseOrderAppServiceMock.purchaseOrderGuid);
            Assert.Equal(5, purchaseOrder.Discount);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Update()
        {
            // Act
            var response = await PutResponseAsObjectAsync<PurchaseOrderDto, ErrorResponse>(
                $"{WebConstants.PurchaseOrderRouteName}/{Guid.Empty}",
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(2, response.Details.Count);

            var message = string.Format(_tnfLocalizationSource.GetString(TnfController.Error.AspNetCoreOnPutError, _culture), "PurchaseOrder");
            Assert.Equal(message, response.Message);
            message = string.Format(_tnfLocalizationSource.GetString(ApplicationService.Error.ApplicationServiceOnInvalidIdError, _culture), "id");
            Assert.Contains(response.Details, n => n.Message == message);
            message = string.Format(_tnfLocalizationSource.GetString(ApplicationService.Error.ApplicationServiceOnInvalidDtoError, _culture), "dto");
            Assert.Contains(response.Details, n => n.Message == message);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Update_With_Specifications()
        {
            // Arrange
            var invalidProductId = Guid.NewGuid();
            var dto = new PurchaseOrderDto();
            dto.Products.Add(new ProductDto(invalidProductId, 1));

            // Act
            var response = await PutResponseAsObjectAsync<PurchaseOrderDto, ErrorResponse>(
                $"{WebConstants.PurchaseOrderRouteName}/{PurchaseOrderAppServiceMock.purchaseOrderGuid}",
                dto,
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Details.Count);

            var message = string.Format(_localizationSource.GetString(PurchaseOrder.Error.ProductsThatAreNotInThePriceTable, _culture), invalidProductId);
            Assert.Contains(response.Details, n => n.Message == message);
        }
    }
}
