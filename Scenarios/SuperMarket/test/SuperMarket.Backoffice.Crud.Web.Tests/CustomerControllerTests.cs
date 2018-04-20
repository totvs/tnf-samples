using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SuperMarket.Backoffice.Crud.Domain.Entities;
using SuperMarket.Backoffice.Crud.Infra.Dtos;
using SuperMarket.Backoffice.Crud.Web.Controllers;
using SuperMarket.Backoffice.Crud.Web.Tests.Mocks;
using System;
using System.Net;
using System.Threading.Tasks;
using Tnf.AspNetCore.TestBase;
using Tnf.Domain.Services;
using Tnf.Dto;
using Xunit;

namespace SuperMarket.Backoffice.Crud.Web.Tests
{
    public class CustomerControllerTests : TnfAspNetCoreIntegratedTestBase<StartupControllerTest>
    {

        [Fact]
        public void Should_Resolve_All()
        {
            TnfSession.ShouldNotBeNull();
            ServiceProvider.GetService<CustomerController>().ShouldNotBeNull();
            ServiceProvider.GetService<IDomainService<Customer>>().ShouldNotBeNull();
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
                $"{WebConstants.CustomerRouteName}/{CustomerDomainServiceMock.customerGuid}"
            );

            // Assert
            Assert.Equal(customer.Id, CustomerDomainServiceMock.customerGuid);
            Assert.Equal("Customer A", customer.Name);
        }

        [Fact]
        public async Task Should_Return_Null_On_Get_Not_Found()
        {
            // Act
            var responseDto = await GetResponseAsObjectAsync<CustomerDto>(
                $"{WebConstants.CustomerRouteName}/{Guid.Empty}",
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.Null(responseDto);
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
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.Null(response);
        }


        [Fact]
        public async Task Should_Update_Customer()
        {
            // Act
            var customer = await PutResponseAsObjectAsync<CustomerDto, CustomerDto>(
                $"{WebConstants.CustomerRouteName}/{CustomerDomainServiceMock.customerGuid}",
                new CustomerDto() { Name = "Customer @" }
            );

            // Assert
            Assert.Equal(CustomerDomainServiceMock.customerGuid, customer.Id);
            Assert.Equal("Customer @", customer.Name);
        }

        [Fact]
        public async Task Should_Return_Null_On_Update()
        {
            // Act
            var response = await PutResponseAsObjectAsync<CustomerDto, CustomerDto>(
                $"{WebConstants.CustomerRouteName}/{Guid.Empty}",
                null,
                HttpStatusCode.BadRequest
            );

            // Assert
            Assert.Null(response);
        }


        [Fact]
        public Task Should_Delete_Customer()
        {
            // Act
            return DeleteResponseAsync(
                $"{WebConstants.CustomerRouteName}/{CustomerDomainServiceMock.customerGuid}"
            );
        }
    }
}
