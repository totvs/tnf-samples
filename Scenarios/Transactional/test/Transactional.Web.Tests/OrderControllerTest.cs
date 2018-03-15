using Microsoft.AspNetCore.Localization;
using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.AspNetCore.TestBase;
using Tnf.EntityFrameworkCore;
using Tnf.Localization;
using Transactional.Domain.Entities;
using Transactional.Infra.Context;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Transactional.Domain;

namespace Transactional.Web.Tests
{
    public class OrderControllerTest : TnfAspNetCoreIntegratedTestBase<StartupTest>
    {
        private ILocalizationManager localizationManager;
        private CultureInfo requestCulture;

        private void SetRequestCulture(CultureInfo cultureInfo)
        {
            Client.DefaultRequestHeaders.Add(CookieRequestCultureProvider.DefaultCookieName, $"c={cultureInfo.Name}|uic={cultureInfo.Name}");

            localizationManager = ServiceProvider.GetService<ILocalizationManager>();

            requestCulture = cultureInfo;
        }

        [Fact]
        public async Task ShouldNotAllowNewOrderDuplicated()
        {
            // Arrange
            SetRequestCulture(CultureInfo.GetCultureInfo("pt-BR"));

            Order order = null;

            // Arrange
            await ServiceProvider.UsingDbContextAsync<OrderContext>(async context =>
            {
                order = new Order()
                {
                    ClientId = 1,
                    Data = DateTime.UtcNow.Date,
                    Discount = 0,
                    Tax = 10,
                    BaseValue = 100,
                    TotalValue = 110,
                };

                order.Products.Add(new ProductOrder()
                {
                    ProductId = 1,
                    Amount = 2,
                    UnitValue = 50.0m
                });

                order.Products.Add(new ProductOrder()
                {
                    ProductId = 2,
                    Amount = 5,
                    UnitValue = 10.0m
                });

                await context.Orders.AddAsync(order);

                await context.SaveChangesAsync();
            });

            // Act
            var response = await PostResponseAsObjectAsync<Order, ErrorResponse>(
                @"api/order",
                order,
                HttpStatusCode.BadRequest);

            // Assert
            var localizationSource = localizationManager.GetSource(Constants.LocalizationSourceName);

            var duplicatedMessage = localizationSource.GetString(GlobalizationKey.DuplicateOrder, requestCulture);

            Assert.Contains(response.Details, w => w.DetailedMessage == GlobalizationKey.DuplicateOrder.ToString());
            Assert.Contains(response.Details, w => w.Message == string.Format(duplicatedMessage, order.ClientId, order.Data));
        }
    }
}
