using System.Collections.Generic;
using System.Threading.Tasks;
using HelloWorld.SharedKernel;
using HelloWorld.SharedKernel.External;
using HelloWorld.SharedKernel.ValueObjects;
using Tnf.Notifications;

namespace HelloWorld.Application
{
    public class SearchAppService : ISearchAppService
    {
        private readonly ILocationService locationService;
        private readonly INotificationHandler notificationHandler;

        public SearchAppService(ILocationService locationService, INotificationHandler notificationHandler)
        {
            this.locationService = locationService;
            this.notificationHandler = notificationHandler;
        }

        public async Task<SearchLocationResponse> SearchLocationFromZipCode(ZipCode zipCode)
        {
            if (!zipCode.IsValid())
            {
                notificationHandler
                    .DefaultBuilder
                    .WithMessage(Constants.LocalizationSourceName, LocalizationServiceError.LocalizationInvalidZipCode)
                    .Raise();

                return null;
            }

            return await locationService.Search(zipCode);
        }

        public async Task<IEnumerable<SearchLocationResponse>> SearchLocation(string state, string city, string street)
        {
            if (string.IsNullOrWhiteSpace(state))
            {
                notificationHandler
                        .DefaultBuilder
                        .WithMessage(Constants.LocalizationSourceName, LocalizationServiceError.LocalizationInvalidState)
                        .Raise();
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                notificationHandler
                        .DefaultBuilder
                        .WithMessage(Constants.LocalizationSourceName, LocalizationServiceError.LocalizationInvalidCity)
                        .Raise();
            }

            if (string.IsNullOrWhiteSpace(street))
            {
                notificationHandler
                        .DefaultBuilder
                        .WithMessage(Constants.LocalizationSourceName, LocalizationServiceError.LocalizationInvalidStreet)
                        .Raise();
            }

            if (notificationHandler.HasNotification())
                return null;

            return await locationService.Search(state, city, street);
        }

        
    }
}
