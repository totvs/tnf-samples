using Dapper.Infra.Context;
using Dapper.Infra.Dto;
using Dapper.Infra.Entities;
using Dapper.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Tnf.AspNetCore.TestBase;
using Tnf.Dto;
using Tnf.EntityFrameworkCore;
using Xunit;

namespace Dapper.Web.Tests
{
    public class PurchaseOrderIntegratedTests : TnfAspNetCoreIntegratedTestBase<StartupIntegratedTest>
    {
        public PurchaseOrderIntegratedTests()
        {
            ServiceProvider.UsingDbContext<PurchaseOrderContext>(context =>
            {
                context.Customers.Add(new Customer("Alexandre"));
                context.Customers.Add(new Customer("Thiago"));
                context.Customers.Add(new Customer("Josue"));

                context.Products.Add(new Product("Shampoo", 10));
                context.Products.Add(new Product("Impressora", 230));
                context.Products.Add(new Product("Usb", 80));
                context.Products.Add(new Product("Pendrive 16GB", 20));
                context.Products.Add(new Product("Laranjada", 7.50m));
                context.Products.Add(new Product("Coca-cola", 6));
                context.Products.Add(new Product("Morango", 2.5m));
                context.Products.Add(new Product("Omeprazol", 12));
                context.Products.Add(new Product("Chá Verde", 9));
                context.Products.Add(new Product("Alcatra", 35));

                context.SaveChanges();

                context.PurchaseOrders.Add(new PurchaseOrder(new DateTime(2018, 3, 26), 1, 89.5m));
                context.PurchaseOrders.Add(new PurchaseOrder(new DateTime(2018, 3, 1), 2, 467.5m));
                context.PurchaseOrders.Add(new PurchaseOrder(new DateTime(2018, 3, 28), 3, 58.5m));

                context.SaveChanges();

                context.PurchaseOrderProducts.Add(new PurchaseOrderProduct(1, 1, 1, 10));
                context.PurchaseOrderProducts.Add(new PurchaseOrderProduct(1, 4, 3, 20));
                context.PurchaseOrderProducts.Add(new PurchaseOrderProduct(1, 5, 1, 7.5m));
                context.PurchaseOrderProducts.Add(new PurchaseOrderProduct(1, 6, 2, 6));
                context.PurchaseOrderProducts.Add(new PurchaseOrderProduct(2, 2, 2, 230));
                context.PurchaseOrderProducts.Add(new PurchaseOrderProduct(2, 7, 3, 2.5m));
                context.PurchaseOrderProducts.Add(new PurchaseOrderProduct(3, 3, 1, 8));
                context.PurchaseOrderProducts.Add(new PurchaseOrderProduct(3, 1, 5, 10));

                context.SaveChanges();
            });
        }

        [Fact]
        public void Should_Resolve_All()
        {
            TnfSession.ShouldNotBeNull();
            ServiceProvider.GetService<PurchaseOrderController>().ShouldNotBeNull();
            ServiceProvider.GetService<IOrderRepository>().ShouldNotBeNull();
        }


        [Fact]
        public async Task Should_GetAll_With_Paginated()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<PurchaseOrderDto>>(
                WebConstants.PurchaseOrderRouteName
            );

            // Assert
            Assert.False(response.HasNext);
            Assert.Equal(3, response.Items.Count);

            // Act
            response = await GetResponseAsObjectAsync<ListDto<PurchaseOrderDto>>(
                $"{WebConstants.PurchaseOrderRouteName}?pageSize=2"
            );

            // Assert
            Assert.True(response.HasNext);
            Assert.Equal(2, response.Items.Count);
        }

        [Fact]
        public async Task Should_GetAll_Sorted()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<PurchaseOrderDto>>(
                $"{WebConstants.PurchaseOrderRouteName}?order=-date"
            );

            // Assert
            Assert.Equal(3, response.Items.Count);
            Assert.Equal(new DateTime(2018, 3, 28), response.Items[0].Date);
            Assert.Equal(new DateTime(2018, 3, 1), response.Items.Last().Date);

            // Act
            response = await GetResponseAsObjectAsync<ListDto<PurchaseOrderDto>>(
                $"{WebConstants.PurchaseOrderRouteName}?pageSize=2&order=-date"
            );

            // Assert
            Assert.Equal(2, response.Items.Count);
            Assert.Equal(new DateTime(2018, 3, 28), response.Items[0].Date);
            Assert.Equal(new DateTime(2018, 3, 26), response.Items.Last().Date);
        }

        [Fact]
        public async Task Should_GetAll_By_Date()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<PurchaseOrderDto>>(
                $"{WebConstants.PurchaseOrderRouteName}?date=3%2F1%2F2018"
            );

            // Assert
            Assert.Equal(1, response.Items.Count);
            Assert.Equal(new DateTime(2018, 3, 1), response.Items[0].Date);
        }


        [Fact]
        public async Task Should_Get_PurchaseOrder()
        {
            // Act
            var purchaseOrder = await GetResponseAsObjectAsync<PurchaseOrderDto>(
                $"{WebConstants.PurchaseOrderRouteName}/2"
            );

            // Assert
            Assert.Equal(2, purchaseOrder.Id);
            Assert.Equal(new DateTime(2018, 3, 1), purchaseOrder.Date);
        }

        [Fact]
        public async Task Should_Return_Null_On_Get_Not_Found()
        {
            // Act
            var response = await GetResponseAsObjectAsync<PurchaseOrderDto>(
                $"{WebConstants.PurchaseOrderRouteName}/4"
            );

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_On_Get_Invalid()
        {
            // Act
            var response = await GetResponseAsObjectAsync<PurchaseOrderDto>(
                $"{WebConstants.PurchaseOrderRouteName}/0",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.Null(response);
        }


        [Fact]
        public async Task Should_Get_Customer_From_PurchaseOrder()
        {
            // Act
            var customer = await GetResponseAsObjectAsync<Customer>(
                $"{WebConstants.PurchaseOrderRouteName}/2/customer"
            );

            // Assert
            Assert.Equal(2, customer.Id);
            Assert.Equal("Thiago", customer.Name);
        }

        [Fact]
        public async Task Should_Return_Null_On_Get_Customer_From_PurchaseOrder_Not_Found()
        {
            // Act
            var response = await GetResponseAsObjectAsync<Customer>(
                $"{WebConstants.PurchaseOrderRouteName}/4/customer"
            );

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_On_Get_Customer_From_PurchaseOrder_Invalid()
        {
            // Act
            var response = await GetResponseAsObjectAsync<Customer>(
                $"{WebConstants.PurchaseOrderRouteName}/0/customer",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.Null(response);
        }


        [Fact]
        public async Task Should_Get_SumarizedOrder_From_Date()
        {
            // Act
            var sumarizedOrder = await GetResponseAsObjectAsync<SumarizedOrder>(
                $"{WebConstants.PurchaseOrderRouteName}/sumarized?date=3%2F1%2F2018"
            );

            // Assert
            Assert.Equal(new DateTime(2018, 3, 1), sumarizedOrder.Date);
            Assert.Equal(5, sumarizedOrder.TotalQuantity);
            Assert.Equal(467.5m, sumarizedOrder.TotalValue);
            Assert.Equal(2, sumarizedOrder.Products.Count());
            Assert.Contains(sumarizedOrder.Products, p => p.Description.Equals("Impressora"));
            Assert.Contains(sumarizedOrder.Products, p => p.Description.Equals("Morango"));
        }

        [Fact]
        public async Task Should_Return_Null_On_Get_SumarizedOrder_From_Date_Not_Found()
        {
            // Act
            var sumarizedOrder = await GetResponseAsObjectAsync<SumarizedOrder>(
                $"{WebConstants.PurchaseOrderRouteName}/sumarized?date=1%2F5%2F2016"
            );

            // Assert
            Assert.NotNull(sumarizedOrder);
            Assert.Equal(new DateTime(2016, 1, 5), sumarizedOrder.Date);
            Assert.Equal(0, sumarizedOrder.TotalQuantity);
            Assert.Equal(0, sumarizedOrder.TotalValue);
            Assert.Empty(sumarizedOrder.Products);
        }

        [Fact]
        public async Task Should_Return_Bad_Request_On_Get_SumarizedOrder_From_Date_Invalid()
        {
            // Act
            var sumarizedOrder = await GetResponseAsObjectAsync<SumarizedOrder>(
                $"{WebConstants.PurchaseOrderRouteName}/sumarized"
            );

            // Assert
            Assert.NotNull(sumarizedOrder);
            Assert.Equal(new DateTime(1, 1, 1), sumarizedOrder.Date);
            Assert.Equal(0, sumarizedOrder.TotalQuantity);
            Assert.Equal(0, sumarizedOrder.TotalValue);
            Assert.Empty(sumarizedOrder.Products);
        }
    }
}
