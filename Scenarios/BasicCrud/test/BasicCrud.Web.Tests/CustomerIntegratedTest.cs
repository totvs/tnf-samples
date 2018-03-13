using BasicCrud.Application.AppServices.Interfaces;
using BasicCrud.Domain;
using BasicCrud.Domain.Entities;
using BasicCrud.Dto.Customer;
using BasicCrud.Infra.SqlServer;
using BasicCrud.Web.Controllers;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
    public class CustomerIntegratedTest : TnfAspNetCoreIntegratedTestBase<StartupTest>
    {
        private readonly Guid customerGuid = Guid.Parse("1b92f96f-6a71-4655-a0b9-93c5f6ad9637");
        private readonly ILocalizationSource _localizationSource;
        private readonly CultureInfo _culture;

        public CustomerIntegratedTest()
        {
            var notificationHandler = new NotificationHandler(ServiceProvider);

            _localizationSource = ServiceProvider.GetService<ILocalizationManager>().GetSource(DomainConstants.LocalizationSourceName);

            _culture = CultureInfo.GetCultureInfo("pt-BR");

            ServiceProvider.UsingDbContext<CustomerDbContext>(context =>
            {
                context.Customers.Add(Customer.Create(notificationHandler)
                    .WithId(customerGuid)
                    .WithName("Cliente 0")
                    .Build());

                for (var i = 1; i < 20; i++)
                    context.Customers.Add(Customer.Create(notificationHandler)
                        .WithId(Guid.NewGuid())
                        .WithName($"Cliente {i}")
                        .Build());

                context.SaveChanges();
            });
        }

        [Fact]
        public void Should_Resolve_All()
        {
            TnfSession.ShouldNotBeNull();
            ServiceProvider.GetService<CustomerController>().ShouldNotBeNull();
            ServiceProvider.GetService<ICustomerAppService>().ShouldNotBeNull();
            ServiceProvider.GetService<IDomainService<Customer, Guid>>().ShouldNotBeNull();
        }


        [Fact]
        public async Task Should_GetAll_With_Paginated()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<CustomerDto, Guid>>(
                "api/customers"
            );

            // Assert
            Assert.True(response.HasNext);
            Assert.Equal(10, response.Items.Count);

            // Act
            response = await GetResponseAsObjectAsync<ListDto<CustomerDto, Guid>>(
                "api/customers?pageSize=30"
            );

            // Assert
            Assert.False(response.HasNext);
            Assert.Equal(20, response.Items.Count);
        }

        [Fact]
        public async Task Should_GetAll_Sorted()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<CustomerDto, Guid>>(
                "api/customers?pageSize=20&order=-name"
            );

            // Assert
            Assert.Equal(20, response.Items.Count);
            Assert.Equal("Customer 19", response.Items[0].Name);
            Assert.Equal("Customer 0", response.Items.Last().Name);
        }

        [Fact]
        public async Task Should_GetAll_Sorted_And_Paginated()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<CustomerDto, Guid>>(
                "api/customers?pageSize=10&order=-name"
            );

            // Assert
            Assert.Equal(10, response.Items.Count);
            Assert.Equal("Customer 19", response.Items[0].Name);
            Assert.Equal("Customer 10", response.Items.Last().Name);
        }

        [Fact]
        public async Task Should_GetAll_By_Name()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<CustomerDto, Guid>>(
                "api/customers?pageSize=20&name=Customer%201"
            );

            // Assert
            Assert.Equal(1, response.Items.Count);
            Assert.True(response.Items.All(p => p.Name.Contains("Customer 1")));
        }


        [Fact]
        public async Task Should_Get_Customer()
        {
            // Act
            var customer = await GetResponseAsObjectAsync<CustomerDto>(
                $"api/customers/{customerGuid}"
            );

            // Assert
            Assert.Equal(customer.Id, customerGuid);
            Assert.Equal("Customer 0", customer.Name);
        }

        [Fact]
        public async Task Should_Get_Customer_Select_Fields()
        {
            // Act
            var customer = await GetResponseAsObjectAsync<CustomerDto>(
                $"api/customers/{customerGuid}?fields=name"
            );

            // Assert
            Assert.Equal(customer.Id, Guid.Empty);
            Assert.Equal("Customer 0", customer.Name);
        }

        [Fact]
        public async Task Should_Return_Null_On_Get_Not_Found()
        {
            // Act
            var customer = await GetResponseAsObjectAsync<CustomerDto>(
                $"api/customers/{Guid.NewGuid()}"
            );

            // Assert
            Assert.Null(customer);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Get_Null()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ErrorResponse>(
                $"api/customers/{Guid.Empty}",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Details.Count);

            var message = string.Format(_localizationSource.GetString(TnfController.Error.AspNetCoreOnGetError, _culture), "api/customers");
            Assert.Equal(message, response.Message);
            Assert.Contains(response.Details, n => n.DetailedMessage == ApplicationService.Error.ApplicationServiceOnInvalidIdError.ToString());
        }


        [Fact]
        public async Task Should_Create_Customer()
        {
            // Act
            var customer = await PostResponseAsObjectAsync<CustomerDto, CustomerDto>(
                GetUrl<CustomerController>(
                    nameof(CustomerController.Post)
                ),
                new CustomerDto() { Name = "Customer 20" },
                HttpStatusCode.BadRequest
            );

            var response = await GetResponseAsObjectAsync<ListDto<CustomerDto, Guid>>(
                "api/customers?pageSize=30"
            );

            // Assert
            Assert.NotNull(customer);
            Assert.False(response.HasNext);
            Assert.Equal(21, response.Items.Count);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Create()
        {
            // Act
            var response = await PostResponseAsObjectAsync<CustomerDto, ErrorResponse>(
                GetUrl<CustomerController>(
                    nameof(CustomerController.Post)
                ),
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Details.Count);

            var message = string.Format(_localizationSource.GetString(TnfController.Error.AspNetCoreOnPostError, _culture), "api/customers");
            Assert.Equal(message, response.Message);
            Assert.Contains(response.Details, n => n.DetailedMessage == ApplicationService.Error.ApplicationServiceOnInvalidDtoError.ToString());
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Create_With_Specifications()
        {
            // Act
            var response = await PostResponseAsObjectAsync<CustomerDto, ErrorResponse>(
                GetUrl<CustomerController>(
                    nameof(CustomerController.Post)
                ),
                new CustomerDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Details.Count);

            var message = string.Format(_localizationSource.GetString(TnfController.Error.AspNetCoreOnPostError, _culture), "api/customers");
            Assert.Equal(message, response.Message);
            Assert.Contains(response.Details, n => n.DetailedMessage == Customer.Error.CustomerShouldHaveName.ToString());
        }


        [Fact]
        public async Task Should_Update_Customer()
        {
            // Act
            var customer = await PutResponseAsObjectAsync<CustomerDto, CustomerDto>(
                $"api/customers/{customerGuid}",
                new CustomerDto() { Name = "Customer -1" }
            );

            // Assert
            Assert.Equal(customer.Id, customerGuid);
            Assert.Equal("Customer -1", customer.Name);

            // Act
            customer = await GetResponseAsObjectAsync<CustomerDto>(
                $"api/customers/{customerGuid}"
            );

            // Assert
            Assert.Equal(customer.Id, customerGuid);
            Assert.Equal("Customer -1", customer.Name);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Update()
        {
            // Act
            var response = await PutResponseAsObjectAsync<CustomerDto, ErrorResponse>(
                $"api/customers/{Guid.Empty}",
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(2, response.Details.Count);

            var message = string.Format(_localizationSource.GetString(TnfController.Error.AspNetCoreOnPutError, _culture), "api/customers");
            Assert.Equal(message, response.Message);
            Assert.Contains(response.Details, n => n.DetailedMessage == ApplicationService.Error.ApplicationServiceOnInvalidIdError.ToString());
            Assert.Contains(response.Details, n => n.DetailedMessage == ApplicationService.Error.ApplicationServiceOnInvalidDtoError.ToString());
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Update_NotFound()
        {
            // Act
            var response = await PutResponseAsObjectAsync<CustomerDto, ErrorResponse>(
                $"api/customers/{Guid.NewGuid()}",
                new CustomerDto() { Name = "Customer -1" },
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Details.Count);

            var message = string.Format(_localizationSource.GetString(TnfController.Error.AspNetCoreOnPutError, _culture), "api/customers");
            Assert.Equal(message, response.Message);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Update_With_Specifications()
        {
            // Act
            var response = await PutResponseAsObjectAsync<CustomerDto, ErrorResponse>(
                $"api/customers/{customerGuid}",
                new CustomerDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Details.Count);

            var message = string.Format(_localizationSource.GetString(TnfController.Error.AspNetCoreOnPutError, _culture), "api/customers");
            Assert.Equal(message, response.Message);
            Assert.Contains(response.Details, n => n.DetailedMessage == Customer.Error.CustomerShouldHaveName.ToString());
        }


        [Fact]
        public async Task Should_Delete_Customer()
        {
            // Act
            await DeleteResponseAsync(
                $"api/customers/{customerGuid}"
            );

            var response = await GetResponseAsObjectAsync<ListDto<CustomerDto, Guid>>(
                "api/customers?pageSize=30"
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
                $"api/customers/{Guid.Empty}",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Details.Count);

            var message = string.Format(_localizationSource.GetString(TnfController.Error.AspNetCoreOnDeleteError, _culture), "api/customers");
            Assert.Equal(message, response.Message);
            Assert.Contains(response.Details, n => n.DetailedMessage == ApplicationService.Error.ApplicationServiceOnInvalidIdError.ToString());
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Delete_NotFound()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<ErrorResponse>(
                $"api/customers/{Guid.NewGuid()}",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Details.Count);

            var message = string.Format(_localizationSource.GetString(TnfController.Error.AspNetCoreOnDeleteError, _culture), "api/customers");
            Assert.Equal(message, response.Message);
        }
    }
}
