using BasicCrud.Application.Services.Interfaces;
using BasicCrud.Domain;
using BasicCrud.Domain.Entities;
using BasicCrud.Dto.Customer;
using BasicCrud.Infra.Context;
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
using Tnf.Notifications;
using Xunit;

namespace BasicCrud.Web.Tests
{
    public class CustomerIntegratedTests : TnfAspNetCoreIntegratedTestBase<StartupIntegratedTest>
    {
        public CustomerIntegratedTests()
        {
            var notificationHandler = new NotificationHandler(ServiceProvider);

            SetRequestCulture(CultureInfo.GetCultureInfo("pt-BR"));

            ServiceProvider.UsingDbContext<CrudDbContext>(context =>
            {
                context.Customers.Add(Customer.Create(notificationHandler)
                    .WithId(CustomerAppServiceMock.customerGuid)
                    .WithName("Customer A")
                    .Build());

                for (var i = 2; i < 21; i++)
                    context.Customers.Add(Customer.Create(notificationHandler)
                        .WithId(Guid.NewGuid())
                        .WithName($"Customer {NumberToAlphabetLetter(i, true)}")
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
            ServiceProvider.GetService<CustomerController>().ShouldNotBeNull();
            ServiceProvider.GetService<ICustomerAppService>().ShouldNotBeNull();
            ServiceProvider.GetService<IDomainService<Customer>>().ShouldNotBeNull();
        }


        [Fact]
        public async Task Should_GetAll_With_Paginated()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<CustomerDto>>(
                WebConstants.CustomerRouteName
            );

            // Assert
            Assert.True(response.HasNext);
            Assert.Equal(10, response.Items.Count);

            // Act
            response = await GetResponseAsObjectAsync<ListDto<CustomerDto>>(
                $"{WebConstants.CustomerRouteName}?pageSize=30"
            );

            // Assert
            Assert.False(response.HasNext);
            Assert.Equal(20, response.Items.Count);
        }

        [Fact]
        public async Task Should_GetAll_Sorted()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<CustomerDto>>(
                $"{WebConstants.CustomerRouteName}?pageSize=20&order=-name"
            );

            // Assert
            Assert.Equal(20, response.Items.Count);
            Assert.Equal("Customer T", response.Items[0].Name);
            Assert.Equal("Customer A", response.Items.Last().Name);

            // Act
            response = await GetResponseAsObjectAsync<ListDto<CustomerDto>>(
                $"{WebConstants.CustomerRouteName}?order=-name"
            );

            // Assert
            Assert.Equal(10, response.Items.Count);
            Assert.Equal("Customer T", response.Items[0].Name);
            Assert.Equal("Customer K", response.Items.Last().Name);
        }

        [Fact]
        public async Task Should_GetAll_By_Name()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<CustomerDto>>(
                $"{WebConstants.CustomerRouteName}?pageSize=20&name=Customer%20"
            );

            // Assert
            Assert.Equal(20, response.Items.Count);
            Assert.All(response.Items, p => p.Name.Contains("Customer "));

            // Act
            response = await GetResponseAsObjectAsync<ListDto<CustomerDto>>(
                $"{WebConstants.CustomerRouteName}?name=Customer%20C"
            );

            // Assert
            Assert.Equal(1, response.Items.Count);
            Assert.Equal("Customer C", response.Items[0].Name);
        }


        [Fact]
        public async Task Should_Get_Customer()
        {
            // Act
            var customer = await GetResponseAsObjectAsync<CustomerDto>(
                $"{WebConstants.CustomerRouteName}/{CustomerAppServiceMock.customerGuid}"
            );

            // Assert
            Assert.Equal(customer.Id, CustomerAppServiceMock.customerGuid);
            Assert.Equal("Customer A", customer.Name);
        }

        [Fact]
        public async Task Should_Get_Customer_Select_Fields()
        {
            // Act
            var customer = await GetResponseAsObjectAsync<CustomerDto>(
                $"{WebConstants.CustomerRouteName}/{CustomerAppServiceMock.customerGuid}?fields=name"
            );

            // Assert
            Assert.Equal(customer.Id, Guid.Empty);
            Assert.Equal("Customer A", customer.Name);
        }

        [Fact]
        public async Task Should_Return_Null_On_Get_Not_Found()
        {
            // Act
            var response = await GetResponseAsObjectAsync<CustomerDto>(
                $"{WebConstants.CustomerRouteName}/{Guid.NewGuid()}",
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
                $"{WebConstants.CustomerRouteName}/{Guid.Empty}",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Details.Count);

            var message = string.Format(GetLocalizedString(TnfConsts.LocalizationSourceName, TnfController.Error.AspNetCoreOnGetError), "Customer");
            Assert.Equal(message, response.Message);

            message = string.Format(GetLocalizedString(TnfConsts.LocalizationSourceName, ApplicationService.Error.ApplicationServiceOnInvalidIdError), "id");
            Assert.Contains(response.Details, n => n.Message == message);
        }


        [Fact]
        public async Task Should_Create_Customer()
        {
            // Act
            var customer = await PostResponseAsObjectAsync<CustomerDto, CustomerDto>(
                WebConstants.CustomerRouteName,
                new CustomerDto() { Name = "Customer @" }
            );

            var response = await GetResponseAsObjectAsync<ListDto<CustomerDto>>(
                $"{WebConstants.CustomerRouteName}?pageSize=30"
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
                WebConstants.CustomerRouteName,
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Details.Count);

            var message = string.Format(GetLocalizedString(TnfConsts.LocalizationSourceName, TnfController.Error.AspNetCoreOnPostError), "Customer");
            Assert.Equal(message, response.Message);

            message = string.Format(GetLocalizedString(TnfConsts.LocalizationSourceName, ApplicationService.Error.ApplicationServiceOnInvalidDtoError), "dto");
            Assert.Contains(response.Details, n => n.Message == message);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Create_With_Specifications()
        {
            // Act
            var response = await PostResponseAsObjectAsync<CustomerDto, ErrorResponse>(
                WebConstants.CustomerRouteName,
                new CustomerDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Details.Count);

            var message = string.Format(GetLocalizedString(TnfConsts.LocalizationSourceName, TnfController.Error.AspNetCoreOnPostError), "Customer");
            Assert.Equal(message, response.Message);

            message = GetLocalizedString(Constants.LocalizationSourceName, Customer.Error.CustomerShouldHaveName);
            Assert.Contains(response.Details, n => n.Message == message);
        }


        [Fact]
        public async Task Should_Update_Customer()
        {
            // Act
            var customer = await PutResponseAsObjectAsync<CustomerDto, CustomerDto>(
                $"{WebConstants.CustomerRouteName}/{CustomerAppServiceMock.customerGuid}",
                new CustomerDto() { Name = "Customer @" }
            );

            // Assert
            Assert.Equal(customer.Id, CustomerAppServiceMock.customerGuid);
            Assert.Equal("Customer @", customer.Name);

            // Act
            customer = await GetResponseAsObjectAsync<CustomerDto>(
                $"{WebConstants.CustomerRouteName}/{CustomerAppServiceMock.customerGuid}"
            );

            // Assert
            Assert.Equal(customer.Id, CustomerAppServiceMock.customerGuid);
            Assert.Equal("Customer @", customer.Name);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Update()
        {
            // Act
            var response = await PutResponseAsObjectAsync<CustomerDto, ErrorResponse>(
                $"{WebConstants.CustomerRouteName}/{Guid.Empty}",
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(2, response.Details.Count);

            var message = string.Format(GetLocalizedString(TnfConsts.LocalizationSourceName, TnfController.Error.AspNetCoreOnPutError), "Customer");
            Assert.Equal(message, response.Message);

            message = string.Format(GetLocalizedString(TnfConsts.LocalizationSourceName, ApplicationService.Error.ApplicationServiceOnInvalidIdError), "id");
            Assert.Contains(response.Details, n => n.Message == message);

            message = string.Format(GetLocalizedString(TnfConsts.LocalizationSourceName, ApplicationService.Error.ApplicationServiceOnInvalidDtoError), "dto");
            Assert.Contains(response.Details, n => n.Message == message);
        }

        [Fact]
        public async Task Should_Raise_Notification_On_Update_With_Specifications()
        {
            // Act
            var response = await PutResponseAsObjectAsync<CustomerDto, ErrorResponse>(
                $"{WebConstants.CustomerRouteName}/{CustomerAppServiceMock.customerGuid}",
                new CustomerDto(),
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Details.Count);

            var message = string.Format(GetLocalizedString(TnfConsts.LocalizationSourceName, TnfController.Error.AspNetCoreOnPutError), "Customer");
            Assert.Equal(message, response.Message);

            message = GetLocalizedString(Constants.LocalizationSourceName, Customer.Error.CustomerShouldHaveName);
            Assert.Contains(response.Details, n => n.Message == message);
        }


        [Fact]
        public async Task Should_Delete_Customer()
        {
            // Act
            await DeleteResponseAsync(
                $"{WebConstants.CustomerRouteName}/{CustomerAppServiceMock.customerGuid}"
            );

            var response = await GetResponseAsObjectAsync<ListDto<CustomerDto>>(
                $"{WebConstants.CustomerRouteName}?pageSize=30"
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
                $"{WebConstants.CustomerRouteName}/{Guid.Empty}",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.NotNull(response);
            Assert.Equal(1, response.Details.Count);

            var message = string.Format(GetLocalizedString(TnfConsts.LocalizationSourceName, TnfController.Error.AspNetCoreOnDeleteError), "Customer");
            Assert.Equal(message, response.Message);

            message = string.Format(GetLocalizedString(TnfConsts.LocalizationSourceName, ApplicationService.Error.ApplicationServiceOnInvalidIdError), "id");
            Assert.Contains(response.Details, n => n.Message == message);
        }

        [Fact]
        public async Task Should_Return_Null_On_Delete_NotFound()
        {
            // Act
            var response = await DeleteResponseAsObjectAsync<CustomerDto>(
                $"{WebConstants.CustomerRouteName}/{Guid.NewGuid()}"
            );

            // Assert
            Assert.Null(response);
        }
    }
}
