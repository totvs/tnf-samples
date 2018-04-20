using BasicCrud.Web.Controllers;
using Shouldly;
using System;
using System.Threading.Tasks;
using Tnf.AspNetCore.TestBase;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using BasicCrud.Application.Services.Interfaces;
using Tnf.Dto;
using BasicCrud.Dto.Customer;
using BasicCrud.Web.Tests.Mocks;
using System.Net;

namespace BasicCrud.Web.Tests
{
    public class CustomerControllerTests : TnfAspNetCoreIntegratedTestBase<StartupControllerTest>
    {
        [Fact]
        public void Should_Resolve_All()
        {
            TnfSession.ShouldNotBeNull();
            ServiceProvider.GetService<CustomerController>().ShouldNotBeNull();
            ServiceProvider.GetService<ICustomerAppService>().ShouldNotBeNull();
        }


        [Fact]
        public async Task Should_GetAll()
        {
            // Act
            var response = await GetResponseAsObjectAsync<ListDto<CustomerDto>>(
                WebConstants.CustomerRouteName
            );

            // Assert
            Assert.False(response.HasNext);
            Assert.Equal(3, response.Items.Count);
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
        public async Task Should_Return_Null_On_Get_Not_Found()
        {
            // Act
            var response = await GetResponseAsObjectAsync<CustomerDto>(
                $"{WebConstants.CustomerRouteName}/{Guid.NewGuid()}",
                HttpStatusCode.NotFound
            );

            // Assert
            Assert.True(response.IsNullable());

            // Act
            response = await GetResponseAsObjectAsync<CustomerDto>(
                $"{WebConstants.CustomerRouteName}/{Guid.Empty}",
                HttpStatusCode.NotFound
            );

            // Assert
            Assert.True(response.IsNullable());
        }


        [Fact]
        public async Task Should_Create_Customer()
        {
            // Act
            var customer = await PostResponseAsObjectAsync<CustomerDto, CustomerDto>(
                WebConstants.CustomerRouteName,
                new CustomerDto() { Name = "Customer @" }
            );

            // Assert
            Assert.NotNull(customer);
        }

        [Fact]
        public async Task Should_Return_Null_On_Create()
        {
            // Act
            var response = await PostResponseAsObjectAsync<CustomerDto, CustomerDto>(
                WebConstants.CustomerRouteName,
                null
            );

            // Assert
            Assert.Null(response);
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
            Assert.Equal(CustomerAppServiceMock.customerGuid, customer.Id);
            Assert.Equal("Customer @", customer.Name);
        }

        [Fact]
        public async Task Should_Return_Null_On_Update()
        {
            // Act
            var response = await PutResponseAsObjectAsync<CustomerDto, CustomerDto>(
                $"{WebConstants.CustomerRouteName}/{Guid.Empty}",
                null
            );

            // Assert
            Assert.Null(response);
        }


        [Fact]
        public Task Should_Delete_Customer()
        {
            // Act
            return DeleteResponseAsync(
                $"{WebConstants.CustomerRouteName}/{CustomerAppServiceMock.customerGuid}"
            );
        }
    }
}
