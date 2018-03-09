using HelloWorld.SharedKernel;
using HelloWorld.SharedKernel.External;
using HelloWorld.SharedKernel.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Notifications;

namespace HelloWorld.Web.Tests.Mocks
{
    public class LocalizationServiceMock : ILocationService
    {
        private readonly INotificationHandler notificationHandler;

        public LocalizationServiceMock(INotificationHandler notificationHandler)
        {
            this.notificationHandler = notificationHandler;
        }

        public Task<SearchLocationResponse> Search(ZipCode zipCode)
        {
            SearchLocationResponse result = null;

            if (zipCode.Number == "11111111")
            {
                result = new SearchLocationResponse()
                {
                    City = "São Paulo",
                    State = "SP",
                    Street = "Praça da Sé",
                    ZipCode = zipCode
                };
            }
            else
            {
                notificationHandler
                    .DefaultBuilder
                    .WithNotFoundStatus()
                    .WithMessage(Constants.LocalizationSourceName, LocalizationServiceError.LocalizationInvalidZipCode)
                    .Raise();
            }

            return Task.FromResult(result);
        }

        public Task<IEnumerable<SearchLocationResponse>> Search(string state, string city, string street)
        {
            var results = new List<SearchLocationResponse>();

            if (state == "RS" && city == "Porto Alegre" && street == "Rua São")
            {
                results.Add(new SearchLocationResponse()
                {
                    City = "Porto Alegre",
                    State = "RS",
                    Street = "Rua São Rubbo",
                    ZipCode = new ZipCode("91420-000")
                });

                results.Add(new SearchLocationResponse()
                {
                    City = "Porto Alegre",
                    State = "RS",
                    Street = "Rua São Domingos",
                    ZipCode = new ZipCode("91420-270")
                });
            }
            else
            {
                notificationHandler
                    .DefaultBuilder
                    .WithNotFoundStatus()
                    .WithMessage(Constants.LocalizationSourceName, LocalizationServiceError.LocalizationInvalidCityOrStreet)
                    .Raise();
            }

            return Task.FromResult<IEnumerable<SearchLocationResponse>>(results);
        }
    }
}
