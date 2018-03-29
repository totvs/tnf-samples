using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.AspNetCore.TestBase;
using Tnf.EntityFrameworkCore;
using Transactional.Domain.Entities;
using Transactional.Infra.Context;
using Xunit;
using Transactional.Domain;

namespace Transactional.Web.Tests
{
    public class PurchaseOrderControllerTest : TnfAspNetCoreIntegratedTestBase<StartupTest>
    {
        [Fact]
        public async Task ShouldNotAllowNewOrderDuplicated()
        {
            // Arrange
            var culture = CultureInfo.GetCultureInfo("pt-BR");

            SetRequestCulture(culture);

            PurchaseOrder purchaseOrder = null;

            // Arrange
            await ServiceProvider.UsingDbContextAsync<PurchaseOrderContext>(async context =>
            {
                purchaseOrder = new PurchaseOrder()
                {
                    ClientId = 1,
                    Data = DateTime.UtcNow.Date,
                    Discount = 0,
                    Tax = 10,
                    BaseValue = 100,
                    TotalValue = 110,
                };

                purchaseOrder.PurchaseOrderProducts.Add(new PurchaseOrderProduct()
                {
                    ProductId = 1,
                    Amount = 2,
                    UnitValue = 50.0m
                });

                purchaseOrder.PurchaseOrderProducts.Add(new PurchaseOrderProduct()
                {
                    ProductId = 2,
                    Amount = 5,
                    UnitValue = 10.0m
                });

                await context.PurchaseOrders.AddAsync(purchaseOrder);

                await context.SaveChangesAsync();
            });

            // Act
            var response = await PostResponseAsObjectAsync<PurchaseOrder, ErrorResponse>(
                @"api/order",
                purchaseOrder,
                HttpStatusCode.BadRequest);

            // Assert
            var duplicatedMessage = GetLocalizedString(Constants.LocalizationSourceName, GlobalizationKey.DuplicatePurchaseOrder, culture);

            Assert.Contains(response.Details, w => w.DetailedMessage == GlobalizationKey.DuplicatePurchaseOrder.ToString());
            Assert.Contains(response.Details, w => w.Message == string.Format(duplicatedMessage, purchaseOrder.ClientId, purchaseOrder.Data));
        }
    }
}
